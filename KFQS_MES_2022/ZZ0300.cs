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
using DC00_assm;
#endregion

#region < KFQS_MES_2022 >
namespace KFQS_MES_2022
{
    public partial class ZZ0300 : Form
    {
        #region < FIELD >
        private System.Drawing.Point temp;
        private bool ismove = false;
        #endregion

        #region < CONSTRUCTOR >
        public ZZ0300()
        {
            InitializeComponent();


 
            txtID.Text = "";
        }

        public ZZ0300(string sWorkerID)
        {
            InitializeComponent();
            sLabel2.Text = Common.getLangText(sLabel2.Text);
            sLabel5.Text = Common.getLangText(sLabel5.Text);
            sLabel4.Text = Common.getLangText(sLabel4.Text);
            sLabel3.Text = Common.getLangText(sLabel3.Text);
            btnSave.Text = Common.getLangText(btnSave.Text);
            btnClose.Text = Common.getLangText(btnClose.Text);
            this.Text = Common.getLangText(this.Text);
            txtID.Text = sWorkerID.Trim();
        }
        #endregion

        #region < EVENT AREA >
           private void pnlSplash_MouseUp(object sender, MouseEventArgs e)
        {
            this.ismove = false;
        }

        private void pnlSplash_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.ismove)
            {
                System.Drawing.Point d = Control.MousePosition;
                d.X = d.X - temp.X;
                d.Y = d.Y - temp.Y;
                this.Location = d;
            }
        }

        private void pnlSplash_MouseDown(object sender, MouseEventArgs e)
        {
            this.ismove = true;

            temp.X = Control.MousePosition.X - this.Location.X;
            temp.Y = Control.MousePosition.Y - this.Location.Y;
        }

        private void ShowDialog(string messageid, DC00_WinForm.DialogForm.DialogType sType = DC00_WinForm.DialogForm.DialogType.OK)
        {
            DC00_WinForm.DialogForm dialogform = new DC00_WinForm.DialogForm(messageid, sType);

            dialogform.ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DBHelper helper = new DBHelper("",true);
           // helper.Transaction = helper._sConn.BeginTransaction();

            try
            {
                if ( txtPWDChk.Text.Trim() != txtPwdChg.Text.Trim())
                {
                    ShowDialog(Common.getLangText("변경할 비밀번호와 비밀번호 확인이 다릅니다."));
                    return;
                }

                Common common = new Common();
                DataTable dt1 = common.Standard_CODE("ENCRYTION");
                string encryt = "";

                if (dt1.Rows.Count == 1)
                {
                    encryt = dt1.Rows[0]["CODE_ID"].ToString();

                }
                encryt = "MD5";
                string pwd = txtPwdNow.Text;
                string pwd1 = txtPwdChg.Text;
                if (encryt == "MD5")
                {
                    pwd = Common.MD5Hash(pwd);
                    pwd1 = Common.MD5Hash(pwd1);
                }
                else if (encryt == "AES")
                {
                    pwd = Common.EncryptString(pwd);
                    pwd1 = Common.EncryptString(pwd1);
                }


                helper.ExecuteNoneQuery("PORC_ZZ0300_U1", CommandType.StoredProcedure
                                                    , helper.CreateParameter("pWorkerID", txtID.Text, DbType.String, ParameterDirection.Input)
                                                    , helper.CreateParameter("pPWD",      pwd, DbType.String, ParameterDirection.Input)
                                                    , helper.CreateParameter("pChgPWD",   pwd1, DbType.String, ParameterDirection.Input));

                if (helper.RSCODE == "S")
                {
                    helper.Commit();
                    this.Close();
                }
                else
                {
                    helper.Rollback();
                    ShowDialog("비밀번호 변경을 실패 하였습니다.\r\n" + helper.RSMSG);
                }
            }
            catch (Exception ex)
            {
                helper.Rollback();
                ShowDialog(ex.ToString());
            }
            finally
            {
                 helper.Close(); 
            }
        }
        #endregion



    }
}
#endregion