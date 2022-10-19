using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Net;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DC00_assm;


namespace KFQS_MES_2022
{
    public partial class ZZ0100 : Form
    {
        #region < MEMBER >
        private bool ismove = false;
        private System.Drawing.Point temp;
        //private SqlConnection        conn;
        private BackgroundWorker splashBackGroundWorker;
        //private Configuration        appConfig;
        private string luServer = string.Empty;
        private string luUserID = string.Empty;
        private string luPassword = string.Empty;
        private string luPort = string.Empty;
        private string luPath = string.Empty;
        private string projectID = string.Empty;
        private AutoResetEvent eventflag = new AutoResetEvent(false);
        private long downloadFileSize = 0;
        private string applicationpath = Application.StartupPath;
        private string backuppath = Application.StartupPath + @"\BACKUP";
        private string temppath = Application.StartupPath + @"\TEMP";
        private string rollbackpath = Application.StartupPath + @"\ROLLBACK";
        private string updatepath = Application.StartupPath + @"\UPDATE";
        private string clientID = String.Empty;
        private string loginid = string.Empty;
        private string lustatus = "SUCCESS";

        private string gPlantCode = string.Empty;
        private string gSystemID = string.Empty;
        private string gVersion = string.Empty;
        #endregion

        #region < CONSTRUCTOR >
        public ZZ0100()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint, true);

            //공용 전역변수
            gPlantCode = ConfigurationManager.AppSettings.GetValues("PLANTCODE")[0];
            gSystemID = ConfigurationManager.AppSettings.GetValues("SYSTEMID")[0];

            // DEBUG 모드인 경우, 라이브업데이트 하지 않음. 
            if (ConfigurationManager.AppSettings.GetValues("MODE")[0] == "DEBUG")
            {
                this.DialogResult = DialogResult.OK;
                return;
            }

            // SYSTEM의 라이브 업데이트 작업을 하기 위한 BACK GROUND WORKER
            this.splashBackGroundWorker = new BackgroundWorker();
            this.splashBackGroundWorker.DoWork += new DoWorkEventHandler(splashBackGroundWorker_DoWork);
            this.splashBackGroundWorker.WorkerReportsProgress = true;
            this.splashBackGroundWorker.WorkerSupportsCancellation = true;

            this.clientID = Dns.GetHostName();
            string ip = String.Empty;

            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            for (int i = 0; i < ipHost.AddressList.Length; i++)
            {
                if (ipHost.AddressList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ip = ipHost.AddressList[i].ToString();
                    break;
                }
            }

            this.clientID = this.clientID + "[" + ip + "]";
            this.ShowDialog();
        }

        #endregion

        #region < EVENT AREA >
        void downLoader_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            eventflag.Set();
        }

        void downLoader_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            downloadFileSize = e.BytesReceived;
        }

        private void pnlSplash_MouseUp(object sender, MouseEventArgs e)
        {
            this.ismove = false;
        }

        void request_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            eventflag.Set();
        }

        void request_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {

        }

        private void pnlSplash_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void pnlSplash_MouseDown(object sender, MouseEventArgs e)
        {
            this.ismove = true;

            temp.X = Control.MousePosition.X - this.Location.X;
            temp.Y = Control.MousePosition.Y - this.Location.Y;
        }

        private void ZZ0100_Load(object sender, EventArgs e)
        {
            this.splashBackGroundWorker.RunWorkerAsync();
        }

        private void splashBackGroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable dtUpdate;     //LiveUpdate 정보(파일, 버전)
            int iChk = 0;           //LiveUpdate 대상파일 수
            bool[] bChk;            //LiveUpdate 대상파일 위치(Index)

            this.indProgress.Start();

            try
            {
                this.SetMessage("GET CLIENT ID(IP ADDRESS).");
                this.GetClientID();

                this.SetMessage("GET LIVEUPDATE CONFIGURATION.");
                this.GetLuConfig();

                this.SetMessage("START LIVEUPDATE.");
                this.UpdateLog("LIVE UPDATE START");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                // LIVE UPDATE 정보 가지고 오기 
                dtUpdate = this.GetLUData(this.projectID,  this.gVersion,this.clientID);

                //파일정보를 읽어와서 파일버전 비교 / 업데이트 준비
                bChk = new bool[dtUpdate.Rows.Count];

                for (int i = 0; i < dtUpdate.Rows.Count; i++)
                {
                    //DB 파일 명/버전 정보
                    string FileName = Convert.ToString(dtUpdate.Rows[i]["FILEID"]);
                    double nfFileVersion = Convert.ToInt32(Convert.ToString(dtUpdate.Rows[i]["VER"]).Replace(".", ""));

                    //Local 파일 명/버전 정보 확인 후 버전 비교하여 업데이트 위치(Index) 체크
                    if (File.Exists(Application.StartupPath + @"\" + FileName) == true)
                    {

                        FileInfo fileinfo = new FileInfo(Application.StartupPath + @"\" + FileName);
                        double pfFileVersion = 0;

                        if (Double.TryParse(Convert.ToString(System.Diagnostics.FileVersionInfo.GetVersionInfo(fileinfo.FullName).FileVersion.Replace(".", "")), out pfFileVersion) == false)
                            pfFileVersion = nfFileVersion;

                        //int pfFileVersion = Convert.ToInt32(System.Diagnostics.FileVersionInfo.GetVersionInfo(fileinfo.FullName).FileVersion.Replace(", ", ".").Replace(".", ""));

                        if (pfFileVersion < nfFileVersion)
                        {
                            iChk = iChk + 1;
                            bChk[i] = true;
                        }
                    }
                    else
                    {
                        iChk = iChk + 1;
                        bChk[i] = true;
                    }
                }

                if (iChk > 0)
                {
                    this.InitiateUpdate();
                    this.BackUPFile(dtUpdate, bChk);
                    this.DownLoadFile(dtUpdate, bChk);
                }
            }
            catch(Exception ex)
            {
                this.UpdateLog("ERROR : " + Environment.NewLine + ex.Message);
                this.lustatus = "FAIL";
                this.FinishUpdate();

                System.Threading.Thread.Sleep(50);
                return;
            }

            try
            {
                if (dtUpdate.Rows.Count > 0 && iChk > 0)
                {
                    this.SetMessage("Updating.");
                    this.UpdateSystem(dtUpdate, bChk);
                }
            }
            catch (Exception ex)
            {
                this.SetMessage("UPDATE 오류 : " + Environment.NewLine + ex.Message);
                this.UpdateLog("UPDATE 오류 : " + Environment.NewLine + ex.Message);
                this.lustatus = "FAIL";
                RollBackSystem();
            }

            if (this.lustatus == "SUCCESS")
            {

            }
            // 프로그램 구성 체크
            if (this.re == true)
            {
                this.SetMessage("프로그램 구성이 바뀌었습니다. 프로그램을 재실행주세요.");
                ConfigurationManager.AppSettings["STATUS"] = "RESTART";
            }

            this.SetMessage("LiveUpdate Complete.");
            this.UpdateLog("LIVE UPDATE END");
            this.FinishUpdate();

            System.Threading.Thread.Sleep(500);
        }
        #endregion

        public void GetLuConfig()
        {
            this.SetMessage("GET SERVER INFORMATION.");
            this.projectID = ConfigurationManager.AppSettings.GetValues("SYSTEMID")[0];

        }

        public DataTable GetLUData(string projectid, string version, string clientid)
        {
            this.SetMessage("GET UPDATE INFORMATION.");

            DBHelper helper = new DBHelper(false);
            DataSet ds = new DataSet();

            try
            {
                ds = helper.FillDataSet("SPROC_GetPatchFileImfo", CommandType.StoredProcedure
                                              , helper.CreateParameter("SystemID", projectid, DbType.String, ParameterDirection.Input)
                                              , helper.CreateParameter("VER",      version,   DbType.String, ParameterDirection.Input)
                                              , helper.CreateParameter("ClientID", clientid,  DbType.String, ParameterDirection.Input));

                return ds.Tables[0];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
            finally
            {
                helper.Close();
            }
        }

        //??????????????????
        public DataTable GetLUFileData(string projectid, string fileid)
        {
            this.SetMessage("GET UPDATE FILE DATA");
            DBHelper helper = new DBHelper(false);

            DataSet ds = new DataSet();

            try
            {
                ds = helper.FillDataSet("SPROC_GetPatchVersion", CommandType.StoredProcedure
                                              , helper.CreateParameter("AS_SYSTEMID", projectid, DbType.String, ParameterDirection.Input)
                                              , helper.CreateParameter("AS_FILEID", fileid, DbType.String, ParameterDirection.Input));
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
            finally
            {
                helper.Close();
            }
        }

        public void InitiateUpdate()
        {
            try
            {
                //this.UpdateLog("INITIALIZE");

                // 업데이트 DIRECTORY 초기화
                this.SetMessage("Initiate Update Folder.");
                this.InitateDirectory(this.updatepath);

                // 백업 DIRECTORY 초기화
                this.SetMessage("Initiate BACKUP Folder.");
                this.InitateDirectory(this.backuppath);

                // TEMP 영역 초기화
                this.SetMessage("Initiate TEMP Folder.");
                this.InitateDirectory(this.temppath);

                // ROLLBACK 영역
                this.SetMessage("Initiate ROLLBACK Folder.");
                this.InitateDirectory(this.rollbackpath);

            }
            catch (Exception ex)
            {
                this.UpdateLog("INITIATE UPDATE ERROR" + Environment.NewLine + ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 작업용 DIRECTORY 생성 및 삭제
        /// </summary>
        /// <param name="directory"></param>
        public void InitateDirectory(string path)
        {
            if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);

            DirectoryInfo dir = new DirectoryInfo(path);

            foreach (System.IO.FileInfo file in dir.GetFiles("*.*", SearchOption.TopDirectoryOnly))
            {
                try
                {
                    file.Attributes = FileAttributes.Normal;
                    file.Delete();
                    System.Threading.Thread.Sleep(50);
                }
                catch (Exception ex)
                {
                    this.UpdateLog("DELETE 오류 >> " + ex.Message);
                }
            }
        }

        private void FinishUpdate()
        {
            // Version Info Write to XML
            FileInfo fileinfo = new FileInfo(Application.StartupPath + @"\KFQS_MES_2022.exe");
            Configuration appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            appConfig.AppSettings.Settings["VERSION"].Value = System.Diagnostics.FileVersionInfo.GetVersionInfo(fileinfo.FullName).FileVersion;
            appConfig.Save();

            // 시스템 재시작 처리
            if (ConfigurationManager.AppSettings.GetValues("STATUS")[0] == "RESTART")
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            else
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void BackUPFile(DataTable dtUpdate, bool[] Chk)
        {
            // Download 대상 파일 백업
            this.SetMessage("BackUp File.");

            for (int i = 0; i < dtUpdate.Rows.Count; i++)
            {
                try
                {
                    if (Chk[i] == false) continue;

                    string FileName = Convert.ToString(dtUpdate.Rows[i]["FILEID"]);

                    if (File.Exists(this.applicationpath + @"\" + FileName) == false) continue;

                    FileInfo fi = new FileInfo(this.applicationpath + @"\" + FileName);
                    System.IO.File.Copy(fi.FullName, this.backuppath + fi.FullName.Replace(this.applicationpath, ""), true);

                    this.UpdateLog("BackUp " + dtUpdate.Rows[i]["FILEID"].ToString());

                    System.Threading.Thread.Sleep(50);
                }
                catch (Exception ex)
                {
                    this.UpdateLog("BackUp Error : " + dtUpdate.Rows[i]["FILEID"].ToString() + Environment.NewLine + ex.ToString());
                    throw ex;
                }
            }
        }

        private void DownLoadFile(DataTable dtUpdate, bool[] Chk)
        {
            this.SetMessage("Download File.");

            DataTable dtUpdateFile;

            for (int i = 0; i < dtUpdate.Rows.Count; i++)
            {
                try
                {
                    if (Chk[i] == false) continue;

                    // LIVE UPDATE 정보 가지고 오기 
                    //dtUpdateFile = this.GetLUFileData(this.projectID, Convert.ToString(dtUpdate.Rows[i]["FILEID"]));

                    //if (dtUpdateFile.Rows.Count == 0)
                    //{
                    //    this.SetMessage("Download Error : " + dtUpdate.Rows[i]["FileID"].ToString() + " NOT EXSITS.");
                    //    this.UpdateLog("Download Error : " + dtUpdate.Rows[i]["FileID"].ToString() + " NOT EXSITS.");
                    //    continue;
                    //}

                    byte[] fileimage = new byte[0];
                    fileimage = (byte[])dtUpdate.Rows[i]["FILEIMAGE"];

                    FileStream fs = new FileStream(Application.StartupPath + @"\UPDATE\" + dtUpdate.Rows[i]["FILEID"].ToString(),
                                                   FileMode.OpenOrCreate, FileAccess.Write);
                    fs.Write(fileimage, 0, fileimage.Length);
                    fs.Close();

                    this.UpdateLog("Download " + dtUpdate.Rows[i]["FILEID"].ToString());
                }
                catch (Exception ex)
                {
                    this.UpdateLog("Download Error : " + dtUpdate.Rows[i]["FILEID"].ToString() + Environment.NewLine + ex.ToString());
                    throw ex;
                }
            }
        }

        public void UpdateSystem(DataTable dtUpdate, bool[] Chk)
        {
            for (int i = 0; i < dtUpdate.Rows.Count; i++)
            {
                System.Threading.Thread.Sleep(50);

                if (Chk[i] == false) continue;

                this.SetMessage("UPDATE " + dtUpdate.Rows[i]["FILEID"].ToString());
                this.UpdateLog("UPDATE " + dtUpdate.Rows[i]["FILEID"].ToString() + "(" + dtUpdate.Rows[i]["VER"].ToString() + ")");

                switch (dtUpdate.Rows[i]["PROCGB"].ToString())
                {
                    case "COPY":
                        try
                        {
                            // 업데이트 대상 폴더가 없을 경우 대상 폴더를 생성한다.
                            if (!System.IO.Directory.Exists(Application.StartupPath + dtUpdate.Rows[i]["CPath"].ToString()))
                                System.IO.Directory.CreateDirectory(Application.StartupPath + dtUpdate.Rows[i]["CPath"].ToString());

                            // UPDATE 대상파일을 삭제한다.
                            System.IO.File.Delete(Application.StartupPath
                                                + dtUpdate.Rows[i]["CPath"].ToString()
                                                + dtUpdate.Rows[i]["FileID"].ToString());
                        }
                        catch
                        {
                            this.UpdateLog("SET RESTART : " + dtUpdate.Rows[i]["FileID"].ToString());

                            // 대상 파일이 사용 중인 경우 대상 파일을 Temp 영역으로 이동
                            System.IO.File.Move(Application.StartupPath
                                              + dtUpdate.Rows[i]["CPath"].ToString()
                                              + dtUpdate.Rows[i]["FileID"].ToString()
                                               , Application.StartupPath + @"\TEMP\"
                                              + dtUpdate.Rows[i]["FileID"].ToString());

                            ConfigurationManager.AppSettings.Set("STATUS", "RESTART");
                        }
                        finally
                        {
                            // 업데이트 파일을 복사 한다.
                            System.IO.File.Copy(Application.StartupPath + @"\UPDATE\"
                                              + dtUpdate.Rows[i]["FileID"].ToString()
                                              , Application.StartupPath
                                              + dtUpdate.Rows[i]["CPath"].ToString()
                                              + dtUpdate.Rows[i]["FileID"].ToString()
                                                );
                        }

                        break;
                    case "EXEC":
                        // 실행 중인 파일(Process)의 경우 삭제가 되지 않기 때문에 이름을 변경한다. (파일명_TEMP.exe)
                        // ex) Sytopia_PMS.exe → Sytopia_PMS_Temp.exe
                        string FileName = Convert.ToString(dtUpdate.Rows[i]["FileID"]);
                        string newFileName = FileName.Replace(".exe", "") + "_TEMP" + ".exe";

                        if (File.Exists(Application.StartupPath + @"\" + newFileName) == true)
                            File.Delete(Application.StartupPath + @"\" + newFileName);

                        if (File.Exists(Application.StartupPath + @"\" + FileName) == true)
                            File.Move(Application.StartupPath + @"\" + FileName, Application.StartupPath + @"\" + newFileName);

                        // 업데이트 파일을 복사 한다.
                        System.IO.File.Copy(Application.StartupPath + @"\UPDATE\" + dtUpdate.Rows[i]["FileID"].ToString()
                                          , Application.StartupPath + dtUpdate.Rows[i]["CPath"].ToString() + dtUpdate.Rows[i]["FileID"].ToString());

                        this.re = true; // 프로그램 재구성
                        break;
                    // SQL 명령을 실행하는 경우
                    case "SQL":
                        break;
                }
            }
        }

        public void RollBackSystem()
        {
            this.UpdateLog("ROLLBACK SYSTEM");
            // 현재 프로그램을 RollBack Temp영역으로 이동
            foreach (string file in System.IO.Directory.GetFiles(this.applicationpath))
            {
                try
                {
                    System.IO.File.Move(file, this.rollbackpath + file.Replace(applicationpath, ""));
                    System.Threading.Thread.Sleep(50);
                }
                catch (Exception ex)
                {
                    this.UpdateLog("ROLLBACK TEMP MOVE 오류 : " + ex.Message);
                }
            }


            // BACK UP 영역 파일을 프로그램 영역으로 이동
            foreach (string file in System.IO.Directory.GetFiles(this.backuppath))
                try
                {
                    System.IO.File.Copy(file, this.applicationpath + file.Replace(this.backuppath, ""));
                    System.Threading.Thread.Sleep(50);
                }
                catch (Exception ex)
                {
                    this.UpdateLog("ROLLBACK MOVE 오류 : " + ex.Message);
                }

            ConfigurationManager.AppSettings.Set("STATUS", "RESTART");
        }

        public void SetMessage(string message)
        {
            System.Threading.Thread.Sleep(100);
            if (this.txtLogo.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    message = "> " + message + Environment.NewLine;
                    this.txtLogo.Text = message.ToUpper() + this.txtLogo.Text;

                    this.txtLogo.Select(0, message.Length);
                    this.txtLogo.SelectionFont = new Font("Tahoma", 11);
                    this.txtLogo.SelectionColor = Color.Lime;
                    this.txtLogo.Select(message.Length - 1, this.txtLogo.Text.Length - message.Length);
                    this.txtLogo.SelectionFont = new Font("Tahoma", 11);
                    this.txtLogo.SelectionColor = Color.YellowGreen;
                }));
                return;
            }

            this.txtLogo.Text = message + Environment.NewLine + this.txtLogo.Text;
        }

        public bool re = false;
        public void UpdateLog(string message)
        {
            DBHelper helper = new DBHelper("", true);

            try
            {
                helper.ExecuteNoneQuery("SPROC_CheckPatchVersion", CommandType.StoredProcedure
                                                        , helper.CreateParameter("SYSTEMID",  this.projectID, DbType.String, ParameterDirection.Input)
                                                        , helper.CreateParameter("CLIENTID",  this.clientID, DbType.String, ParameterDirection.Input)
                                                        , helper.CreateParameter("DESCRIPT",  message, DbType.String, ParameterDirection.Input)
                                                        , helper.CreateParameter("MAKER",     this.loginid, DbType.String, ParameterDirection.Input)
                                                        , helper.CreateParameter("EDITOR",    this.loginid, DbType.String, ParameterDirection.Input));
                helper.Commit();

                string[] msg = message.Split(new char[] { '.', '(' });
                for (int i = 0; i < msg.Length; i++)
                {
                    if (msg[i] == "Config" || msg[i] == "config")
                    {
                        re = true;
                    }
                }
            }
            catch (Exception ex)
            {
                helper.Rollback();
            }
            finally
            {
                helper.Close();
            }
        }

        public void GetClientID()
        {
            DBHelper helper = new DBHelper(false);

            try
            {
                this.clientID = Common.GetIPAddress();
            }
            catch (Exception ex)
            {
                SetMessage(ex.Message);
            }
            finally
            {
                helper.Close();
            }
        }
    }
}
