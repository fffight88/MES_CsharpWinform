using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Configuration;
using DC00_assm;
namespace KFQS_MES_2022
{

    static class Program
    {
        [DllImport("user32.dll")]
        public static extern void BringWindowToTop(IntPtr hWnd);

        [DllImport("User32", EntryPoint = "SetForegroundWindow")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("User32")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string userid = string.Empty;
            string userName = string.Empty;
            string userPlantCode = string.Empty;
            Configuration appConfig;

            // 동일프로그램 실행 확인(동일프로그램일 경우, 실행중인 프로그램을 맨앞으로 가지고 온다.)
            appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            // RESTART인 경우, 로그인을 하지 않고 시작
            if (KFQS_MES_2022.Program.CheckMultiProcess()) return;

            // 프로그램 전체에서 사용되어지는 Style 파일을 정의
            Microsoft.Win32.RegistryKey rkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Sytopia_PMS\UI");
            if (rkey != null)
            {
                Common.UIStyle = rkey.GetValue("STYLE").ToString();
            }
            // 프로그램 전체에서 사용되어지는 Style 파일을 정의
            if (Common.UIStyle != "")
                Infragistics.Win.AppStyling.StyleManager.Load(Application.StartupPath + @"\" + Common.UIStyle + ".isl");
            else
            {
                if (System.IO.File.Exists(Application.StartupPath + @"\DC_Style.isl"))
                    Infragistics.Win.AppStyling.StyleManager.Load(Application.StartupPath + @"\DC_Style.isl");
                else
                {
                    Infragistics.Win.AppStyling.StyleManager.Load(Application.StartupPath + @"\Style.isl");
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Convert.ToString(appConfig.AppSettings.Settings["STATUS"].Value) == "START")
            {

                // 로그인 실패시 종료
                ZZ0000 zz0000 = new ZZ0000();
                if (zz0000.DialogResult != DialogResult.OK)
                    return;

                userid        = zz0000.UserID;
                userName      = zz0000.UserName;
                userPlantCode = zz0000.UserPlantCode;

                ZZ0100 zz0100 = new ZZ0100();
                // 라이브 업데이트시 LOCK이 걸린 경우 RESTART
                if (zz0100.DialogResult == DialogResult.Cancel)
                {
                    //MessageBox.Show("프로그램 구성이 바뀌었습니다. 재실행하겠습니다.");
                    Application.Restart();

                    return;
                }
            }
            else
            {
                appConfig.AppSettings.Settings["STATUS"].Value = "START";
                appConfig.Save();
                userid = Convert.ToString(appConfig.AppSettings.Settings["LOGINID"].Value);
            }

            // 프로그램 실행
            KFQS_MES_2022.Program.RunApplication(new string[] { userid, userName });
        }

        /// <summary>
        /// 중복프로그램 실행 확인
        /// </summary>
        /// <returns></returns>
        public static Boolean CheckMultiProcess()
        {
            int thisID = System.Diagnostics.Process.GetCurrentProcess().Id; // 현재 기동한 프로그램 id

            //실행중인 프로그램중 현재 기동한 프로그램과 같은 프로그램들 수집
            System.Diagnostics.Process[] p = System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName);

            if (p.Length > 1)
            {
                for (int i = 0; i < p.Length; i++)
                {
                    if (p[i].Id == thisID) continue;

                    ShowWindow(p[i].MainWindowHandle, 1);
                    BringWindowToTop(p[i].MainWindowHandle);
                    SetForegroundWindow(p[i].MainWindowHandle);

                    break;
                }
                return true;
            }

            return false;
        }

        /// <summary>
        /// APPLICATION 실행
        /// </summary>
        /// <param name="args"></param>
        public static void RunApplication(params object[] args)
        {
            Assembly assembly;
            Type typeForm;
            Form newForm;
            Configuration appconfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            assembly = Assembly.LoadFrom(Application.StartupPath + @"\" + appconfig.AppSettings.Settings["STARTFORMFILE"].Value.ToString());
            typeForm = assembly.GetType(appconfig.AppSettings.Settings["STARTFORM"].Value.ToString(), true);
            newForm = (Form)Activator.CreateInstance(typeForm, args);
            Application.Run(newForm);
        }
    }
}
