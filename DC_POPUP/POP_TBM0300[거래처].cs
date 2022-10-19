using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DC00_assm;
using DC00_PuMan;

namespace DC_POPUP
{
    public partial class POP_TBM0300 : DC00_WinForm.BasePopupForm
    {
        string[] argument;

        #region [ 선언자 ]
        //그리드 객체 생성
        UltraGridUtil _GridUtil = new UltraGridUtil();

        //비지니스 로직 객체 생성
        PopUp_Biz _biz = new PopUp_Biz();

        //임시로 사용할 데이터테이블 생성
        DataTable _DtTemp = new DataTable();
        #endregion

        public POP_TBM0300(string[] param)
        {
            InitializeComponent();

            argument = new string[param.Length];

            Common _Common = new Common();

            //DataTable rtnDtTemp = _Common.GET_TBM0000_CODE("PLANTCODE");  //사업장
            //SAMMI.Common.FillComboboxMaster(this.cboPlantCode_H, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", null);

            DataTable rtnDtTemp = _Common.Standard_CODE("CUSTTYPE");  //사업장
            Common.FillComboboxMaster(this.cboCustType_H, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");

            rtnDtTemp = _Common.Standard_CODE("USEFLAG");  //사업장
            Common.FillComboboxMaster(this.cboUseFlag_H, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");

            for (int i = 0; i < param.Length; i++)
            {
                argument[i] = param[i];

                #region [가래선코드 및 명 Parameter Show]
                switch (i)
                {
                    case 0:
                        txtCustCode.Text = argument[0].ToUpper(); //거래처코드
                        break;
                    case 1:
                        txtCustName.Text = argument[1].ToUpper(); //거래처명
                        break;
                    case 2:
                        cboCustType_H.Text = argument[2] == "" ? "ALL" : argument[2];
                        break;
                    case 3:
                        cboUseFlag_H.Text = argument[3] == "" ? "ALL" : argument[3];
                        break;
                }
                #endregion
            }
        }

        private void POP_TBM0300_Load(object sender, EventArgs e)
        {
            _GridUtil.InitializeGrid(this.Grid1);

            _GridUtil.InitColumnUltraGrid(Grid1, "CustType",   "구분",         false, GridColDataType_emu.VarChar, 90, 100, Infragistics.Win.HAlign.Left, true, false, null, null, null, null, null);
            _GridUtil.InitColumnUltraGrid(Grid1, "CustTypeNm", "구분명",       false, GridColDataType_emu.VarChar, 80, 100, Infragistics.Win.HAlign.Center, false, false, null, null, null, null, null);
            _GridUtil.InitColumnUltraGrid(Grid1, "CustCode",   "거래처코드",   false, GridColDataType_emu.VarChar, 90, 100, Infragistics.Win.HAlign.Default, true, false, null, null, null, null, null);
            _GridUtil.InitColumnUltraGrid(Grid1, "CustName",   "거래처명",     false, GridColDataType_emu.VarChar, 200, 100, Infragistics.Win.HAlign.Default, true, false, null, null, null, null, null);
            _GridUtil.InitColumnUltraGrid(Grid1, "CustEName",  "업체명(영문)", false, GridColDataType_emu.VarChar, 200, 100, Infragistics.Win.HAlign.Default, true, false, null, null, null, null, null);    

            _GridUtil.SetInitUltraGridBind(Grid1);

            search();
         
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            search();
        }

        private void search()
        {
            string RS_CODE   = string.Empty, RS_MSG = string.Empty;
            string sCustType = string.Empty;
            string sUseFlag  = string.Empty;
            string sCustCode = txtCustCode.Text.Trim();
            string sCustName = txtCustName.Text.Trim();

            sCustType = Convert.ToString(cboCustType_H.Value);
            sUseFlag = Convert.ToString(cboUseFlag_H.Value);
            DataTable rtnDtTemp = new DataTable(); // return DataTable 공통
            Common _Common      = new Common();
            rtnDtTemp           = _Common.Standard_CODE("CUSTTYPE");  //거래처
            UltraGridUtil.SetComboUltraGrid(this.Grid1, "CustType", rtnDtTemp, "CODE_ID", "CODE_NAME");
          

            _DtTemp = _biz.SEL_TBM0300(sCustCode, sCustName, sCustType, sUseFlag);

            Grid1.DataSource = _DtTemp;
            Grid1.DataBind();
        }
        private void Grid1_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            DataTable TmpDt = new DataTable();
            TmpDt.Columns.Add("CustCode", typeof(string));
            TmpDt.Columns.Add("CustName", typeof(string));

            TmpDt.Rows.Add(new object[] { e.Row.Cells["CustCode"].Value, e.Row.Cells["CustName"].Value });

            this.Tag = TmpDt;
            this.Close();
        }

        private void txtCustCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                search();
            }
        }

        private void txtCustName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                search();
            }
        }


    }
}
