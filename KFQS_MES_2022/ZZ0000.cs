#region <USING AREA>
using System;
using System.Configuration;
using System.Net;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows.Forms;
using DC00_assm;
using DC00_WinForm ;

#endregion

#region < KFQS_MES_2022 >
namespace KFQS_MES_2022
{
    #region < ZZ0000 >
    public partial class ZZ0000 : Form
    {
        private const int LOGIN_COUNT = 3;
        private bool ismove = false;
        private System.Drawing.Point temp;
        private Configuration appConfig;
        private DataTable dtSite;
        public DbCommand InsertCmd;
        private int loginCnt = 0;
        public string UserID = "SYSTEM";
        public string UserName = "";
        public string UserPlantCode = string.Empty;

        #region < CONSTRUCTOR >
        public ZZ0000()
        {
            InitializeComponent();
           
            
            Common.ProcessName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            System.Collections.Specialized.NameValueCollection sitecollection = (System.Collections.Specialized.NameValueCollection)ConfigurationManager.GetSection("site");
            System.Collections.Specialized.NameValueCollection sitenamecollection = (System.Collections.Specialized.NameValueCollection)ConfigurationManager.GetSection("sitename");

            dtSite = new DataTable();
            dtSite.Columns.Add("SITE");
            dtSite.Columns.Add("SITENAME");

            for (int i = 0; i < sitenamecollection.Count; i++)
            {
                DataRow row = dtSite.NewRow();
                row["SITE"] = sitenamecollection.Keys[i];
                row["SITENAME"] = sitenamecollection[sitenamecollection.Keys[i]];
                dtSite.Rows.Add(row);
            }

            this.cboSite.DataSource = dtSite;
            this.cboSite.ValueMember = "SITE";
            this.cboSite.DisplayMember = "SITENAME";

            this.cboSite.Value = appConfig.AppSettings.Settings["SITE"].Value;

            string ConnStr = string.Empty;
            if (appConfig.AppSettings.Settings["PLACE"].Value.ToString() == "DEV") // 개발 PC
            {
                ConnStr = @"Data Source=222.235.141.8; Initial Catalog=DC_EDU_SH;User ID=kfqs;Password=1234";
                Configuration appConfig1 = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                appConfig1.ConnectionStrings.ConnectionStrings["ConnectionString"].ConnectionString = ConnStr;
                appConfig1.Save();

                ConfigurationManager.RefreshSection("connectionStrings");
                ConfigurationManager.RefreshSection("configuration");

                Application.DoEvents();
            }
            else
            {
                {
                    ShowDialog("접속 DDNS 가 명확하지 않습니다. 관리자에게 문의 하세요", DialogForm.DialogType.OK);
                    this.DialogResult = DialogResult.Cancel;
                    return;
                }
            }

            this.txtWorkerID.Text = appConfig.AppSettings.Settings["LOGINID"].Value.ToString();
            this.loginCnt = LOGIN_COUNT;
            this.ShowDialog();
            this.txtPassword.Focus();
            
        }
        #endregion

        #region < EVENT AREA >
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.Password_Confirm();
        }

        private void Password_Confirm()
        {
            string myip = Common.GetIPAddress();

            this.txtWorkerID.Enabled = false;
            this.txtPassword.Enabled = false;
            this.btnConfirm.Enabled = false;
            this.btnConfig.Enabled = false;
            this.btnClose.Enabled = false;
            this.btnChange.Enabled = false;
            
            try
            {

                DBHelper helper = new DBHelper(false);
                DataTable dt = helper.FillTable("SPROC_ZGETWORKERINFO_S", CommandType.StoredProcedure
                                                            , helper.CreateParameter("pWorkerID", this.txtWorkerID.Text, DbType.String, ParameterDirection.Input));

                if (dt.Rows.Count == 0)
                {
                    ShowDialog(Common.getLangText("MEMBER ID를 확인하세요."));
                    this.txtWorkerID.Enabled = true;
                    this.txtPassword.Enabled = true;
                    this.btnConfirm.Enabled = true;
                    this.btnConfig.Enabled = true;
                    this.btnClose.Enabled = true;
                    this.btnChange.Enabled = true;
                    return;
                }

                UserName = Convert.ToString(dt.Rows[0]["WorkerName"]);
                string pwd = Convert.ToString(dt.Rows[0]["Password"]);

                bool bpwok = false;
                if (pwd == Common.MD5Hash(this.txtPassword.Text))
                    bpwok = true;

                if (bpwok==false)
                {
                    loginCnt--;
                    if (loginCnt == 0)
                    {
                        ShowDialog(Common.getLangText("로그인에 실패 했습니다. 시스템을 종료합니다."), DC00_WinForm.DialogForm.DialogType.OK);
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                        this.Close();
                    }
                    else
                    {
                        ShowDialog(Common.getLangText("사용자 아이디나, 패스워드가 틀립니다."));
                        this.txtWorkerID.Enabled = true;
                        this.txtPassword.Enabled = true;
                        this.btnConfirm.Enabled = true;
                        this.btnConfig.Enabled = true;
                        this.btnClose.Enabled = true;
                        this.btnChange.Enabled = true;
                        this.txtPassword.Focus();
                    }
                    return;
                }
            }
            catch (SqlException)
            {
                ShowDialog(Common.getLangText("연결 상태를 확인하세요."));
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
                return;
            }
            catch (Exception ex)
            {
                ShowDialog(ex.Message);
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.UserID = this.txtWorkerID.Text;

            SaveDoWorkType(this.UserID, myip, "LOGIN");

            appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            appConfig.AppSettings.Settings["LOGINID"].Value = this.txtWorkerID.Text;
            appConfig.Save();
        }

        private void SaveDoWorkType(string userid, string IP, string sType)
        {
            try
            {
                DBHelper Helper = new DBHelper("",true);
                try
                {
                    Helper.ExecuteNoneQuery("SPROC_SystemHandleImfo", CommandType.StoredProcedure
                                                    , Helper.CreateParameter("PROGRAMID", "MAIN", DbType.String, ParameterDirection.Input)
                                                    , Helper.CreateParameter("WORKERID", userid, DbType.String, ParameterDirection.Input)
                                                    , Helper.CreateParameter("WORKTYPE", sType, DbType.String, ParameterDirection.Input)
                                                    , Helper.CreateParameter("IP", IP, DbType.String, ParameterDirection.Input)
                                                    , Helper.CreateParameter("COMNAME", System.Environment.MachineName.ToString(), DbType.String, ParameterDirection.Input));

                    Helper.Commit();
                }
                catch (Exception ex)
                {
                    Helper.Rollback();
                    ShowDialog(ex.ToString());
                }
                finally
                {
                    Helper.Close();
                }
            }
            catch (Exception)
            {
                ShowDialog(Common.getLangText("Network 연결을 확인하세요."));
            }
        }

        private void ShowDialog(string messageid, DC00_WinForm.DialogForm.DialogType sType = DialogForm.DialogType.OK)
        {
            DialogForm dialogform = new DialogForm(messageid, sType);

            dialogform.ShowDialog();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            ZZ0200 zz0200 = new ZZ0200();
            zz0200.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void ZZ0000_Load(object sender, EventArgs e)
        {
            Common.DefaultLang = "KO";
            Common.Lang=appConfig.AppSettings.Settings["LANGUAGE"].Value;
            cboLangSet();
        }

        #region cboLangSet
        /// <summary>
        /// 사용자 언어 설정을 DATABASE에서 가져옴.
        /// </summary>
        /// <param name="loginid"></param>
        public void cboLangSet()
        {
            try
            {
                DBHelper helper = new DBHelper(true);
                cboLang.DataSource = helper.FillTable("SPROC_LangSeeting", CommandType.StoredProcedure);
                cboLang.Value = appConfig.AppSettings.Settings["LANGUAGE"].Value;
            }
            catch (Exception ex)
            {
            }
        }
        #endregion


        private void cboSite_ValueChanged(object sender, EventArgs e)
        {
            Configuration appConfig1 = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            System.Collections.Specialized.NameValueCollection sitecollection = (System.Collections.Specialized.NameValueCollection)ConfigurationManager.GetSection("site");
            for (int i = 0; i < appConfig1.ConnectionStrings.ConnectionStrings.Count; i++)
                appConfig1.ConnectionStrings.ConnectionStrings[i].ConnectionString = Convert.ToString(sitecollection[Convert.ToString(cboSite.Value)]);


            appConfig1.AppSettings.Settings["SITE"].Value = this.cboSite.Value.ToString();
            appConfig1.Save();

            ConfigurationManager.RefreshSection("connectionStrings");
            ConfigurationManager.RefreshSection("configuration");
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            ZZ0300 zz0300 = new ZZ0300(txtWorkerID.Text.Trim());
            zz0300.ShowDialog();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Password_Confirm();
            }
        }

        private void cboLang_ValueChanged(object sender, EventArgs e)
        {
            Configuration appConfig1 = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            appConfig1.AppSettings.Settings["LANGUAGE"].Value = this.cboLang.Value.ToString();
            appConfig1.Save();

            Common.Lang = this.cboLang.Value.ToString();
            btnChange.Text = Common.getLangText("비밀번호 변경");
            btnConfig.Text = Common.getLangText("연결상태");
            lblSite.Text = Common.getLangText("사업장");



        }

        private void lblPassword_DoubleClick(object sender, EventArgs e)
        {
            if (txtWorkerID.Text.Equals("SYSTEM"))
                txtPassword.Text = "12@!";
        }

    }
    #endregion
}
#endregion