#region <USING AREA>
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Configuration;
using System.Xml;
using DC00_assm;
#endregion

#region < KFQS_MES_2022 >
namespace KFQS_MES_2022
{
    public partial class ZZ0200 : Form
    {
        #region < FIELD >
        private Configuration appConfig;
        private string _sSite = string.Empty;
        #endregion

        #region < CONSTRUCTOR >
        public ZZ0200()
        {
            InitializeComponent();

            appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            btnDBConfig.Text = Common.getLangText(btnDBConfig.Text);
            btnSave.Text = Common.getLangText(btnSave.Text);
            btnClose.Text = Common.getLangText(btnClose.Text);
        }

        public ZZ0200(string sSite)
        {
            InitializeComponent();

            _sSite = sSite;
            appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            btnDBConfig.Text = Common.getLangText(btnDBConfig.Text);
            btnSave.Text = Common.getLangText(btnSave.Text);
            btnClose.Text = Common.getLangText(btnClose.Text);
        }
        #endregion

        #region < EVENT AREA >
        private void ZZ0200_Load(object sender, EventArgs e)
        {
            string conStr = appConfig.ConnectionStrings.ConnectionStrings["ConnectionString"].ConnectionString;
            if (appConfig.AppSettings.Settings["ENCRYTION"].Value == "YES")
            {
                conStr = Common.DecryptString(conStr);
                string[] sData = conStr.Split(';');
                for (int idx = 0; idx < sData.Length; idx++)
                {
                    string[] sTmp = sData[idx].Split('=');

                    if (sTmp == null)
                        continue;
                    if (sTmp.Length == 0)
                        continue;

                    switch (sTmp[0])
                    {
                        case "Data Source":
                        case "Server":
                        case "Address":
                        case "Addr":
                        case "Network Address":
                            this.txtDBSERVER1.Text = sTmp[1];
                            break;
                        case "Initial Catalog":
                        case "Database":
                            this.txtDATABASE1.Text = sTmp[1];
                            break;
                        case "User ID":
                        case "UID":
                            this.txtDBUSER1.Text = sTmp[1];
                            break;
                        case "Password":
                        case "PWD":
                            this.txtDBPASSWORD1.Text = sTmp[1];
                            break;
                        case "Connection Timeout":
                        case "Connect Timeout":
                            this.txtCONNECTTIMEOUT1.Text = sTmp[1];
                            break;
                    }
                }
                txtPROVIDER1.Text = appConfig.ConnectionStrings.ConnectionStrings["ConnectionString"].ProviderName;
            }

        }

        private void ShowDialog(string messageid, DC00_WinForm.DialogForm.DialogType sType = DC00_WinForm.DialogForm.DialogType.OK)
        {
            DC00_WinForm.DialogForm dialogform = new DC00_WinForm.DialogForm(messageid, sType);

            dialogform.ShowDialog();
        }

        private void btnDBConfig_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionstring = "Persist Security Info=True"
                                 + ";Data Source=" + this.txtDBSERVER1.Text.Trim()
                                 + ";Initial Catalog=" + this.txtDATABASE1.Text.Trim()
                                 + ";User ID=" + this.txtDBUSER1.Text.Trim()
                                 + ";Password=" + this.txtDBPASSWORD1.Text.Trim()
                                 + ";Connect Timeout=" + this.txtCONNECTTIMEOUT1.Text.Trim();
                SqlConnection conn = new SqlConnection(connectionstring);
                conn.Open();
                conn.Close();
                ShowDialog(Common.getLangText("정상연결되었습니다."));
            }
            catch
            {
                ShowDialog(Common.getLangText("연결설정오류입니다."));
            }
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionstring = string.Empty;

                if (txtDBSERVER1.Text == "")
                {
                    ShowDialog(Common.getLangText("연결설정오류입니다. 저장되지 않습니다."));
                    txtDBSERVER1.Focus();
                    return;
                }
                if (txtDATABASE1.Text == "")
                {
                    ShowDialog(Common.getLangText("연결설정오류입니다. 저장되지 않습니다."));
                    txtDATABASE1.Focus();
                    return;
                }
                if (txtDBUSER1.Text == "")
                {
                    ShowDialog(Common.getLangText("연결설정오류입니다. 저장되지 않습니다."));
                    txtDBUSER1.Focus();
                    return;
                }

                connectionstring = "Persist Security Info=True"
                                 + ";Data Source=" + this.txtDBSERVER1.Text.Trim()
                                 + ";Initial Catalog=" + this.txtDATABASE1.Text.Trim()
                                 + ";User ID=" + this.txtDBUSER1.Text.Trim()
                                 + ";Password=" + this.txtDBPASSWORD1.Text.Trim()
                                 + ";Connect Timeout=" + this.txtCONNECTTIMEOUT1.Text.Trim();
                string sEnccon = Common.EncryptString(connectionstring);
                for (int i = 0; i < appConfig.ConnectionStrings.ConnectionStrings.Count; i++)
                {
                    appConfig.ConnectionStrings.ConnectionStrings[i].ConnectionString = sEnccon;
                    appConfig.ConnectionStrings.ConnectionStrings[i].ProviderName = txtPROVIDER1.Text.Trim();
                }
                appConfig.Save();

                // site DB 연결 정보 변경
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                xmlDoc.SelectSingleNode("//site/add[@key='" + _sSite + "']").Attributes["value"].Value = sEnccon;
                xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                ConfigurationManager.RefreshSection("site");
            }
            catch (Exception)
            {
                ShowDialog(Common.getLangText("연결설정오류입니다. 저장되지 않습니다."));
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
#endregion