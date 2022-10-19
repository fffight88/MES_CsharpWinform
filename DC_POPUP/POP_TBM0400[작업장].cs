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
    public partial class POP_TBM0400 : DC00_WinForm.BasePopupForm
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

        public POP_TBM0400(string[] param)
        {
            InitializeComponent();

            argument = new string[param.Length];
            Common _Common = new Common();

            DataTable rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  //사업장
            Common.FillComboboxMaster(this.cboPlantCode_H, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.Grid1, "PlantCode", rtnDtTemp, "CODE_ID", "CODE_NAME");

            
            rtnDtTemp = _Common.Standard_CODE("USEFLAG");     //사용여부
            Common.FillComboboxMaster(this.cboUseFlag_H, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.Grid1, "UseFlag", rtnDtTemp, "CODE_ID", "CODE_NAME");

            for (int i = 0; i < param.Length; i++)
            {
                argument[i] = param[i];

                #region [품목 및 명 Parameter Show]
                switch (i)
                {
                    case 0:
                        cboPlantCode_H.Text = argument[0].ToUpper() == "" ? "ALL" : argument[0].ToUpper(); //plant
                        break;

                    case 1:
                        txtOpCode.Text = argument[1].ToUpper(); //작업장코드
                        break;

                    case 2:
                        txtOpName.Text = argument[2].ToUpper(); //작업장명
                        break;

                    case 3:
                        cboUseFlag_H.Text = argument[3].ToUpper() == "" ? "ALL" : argument[3].ToUpper(); //사용여부
                        break;
                }
                #endregion
            }
        }

        private void POP_TBM0400_Load(object sender, EventArgs e)
        {
            DataTable rtnDtTemp = new DataTable(); // return DataTable 공통
            Common _Common = new Common();
            _GridUtil.InitializeGrid(this.Grid1);

            _GridUtil.InitColumnUltraGrid(Grid1, "PlantCode",       "사업장", false, GridColDataType_emu.VarChar, 90, 100, Infragistics.Win.HAlign.Left, true, false, null, null, null, null, null);
            _GridUtil.InitColumnUltraGrid(Grid1, "WORKCENTERCODE",  "작업장코드", false, GridColDataType_emu.VarChar, 110, 100, Infragistics.Win.HAlign.Center, true, false, null, null, null, null, null);
            _GridUtil.InitColumnUltraGrid(Grid1, "WORKCENTERNAME",  "작업장명", false, GridColDataType_emu.VarChar, 250, 100, Infragistics.Win.HAlign.Left, true, false, null, null, null, null, null);
            _GridUtil.InitColumnUltraGrid(Grid1, "UseFlag",         "사용유무", false, GridColDataType_emu.VarChar, 100, 100, Infragistics.Win.HAlign.Left, true, false, null, null, null, null, null);

            _GridUtil.SetInitUltraGridBind(Grid1);

              
            search();
         
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            search();
        }

        private void search()
        {
            string RS_CODE    = string.Empty, RS_MSG = string.Empty;
            string sPlantCode = string.Empty;
            string sUseFlag   = string.Empty;
            string sOpCode    = txtOpCode.Text.Trim();
            string sOpName    = txtOpName.Text.Trim();

            sPlantCode = Convert.ToString(this.cboPlantCode_H.Value);
            sUseFlag = Convert.ToString(this.cboUseFlag_H.Value);
            if (sPlantCode == "ALL") sPlantCode = "";
            if (sUseFlag == "ALL") sUseFlag = "";

            DataTable rtnDtTemp = new DataTable(); // return DataTable 공통
            Common _Common = new Common();
            rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  //사업장
            UltraGridUtil.SetComboUltraGrid(this.Grid1, "PlantCode", rtnDtTemp, "CODE_ID", "CODE_NAME");
            rtnDtTemp = _Common.Standard_CODE("USEFLAG");  // 사용유무
            UltraGridUtil.SetComboUltraGrid(this.Grid1, "UseFlag", rtnDtTemp, "CODE_ID", "CODE_NAME");


            _DtTemp = _biz.SEL_TBM0400(sPlantCode, sOpCode, sOpName, sUseFlag);

            Grid1.DataSource = _DtTemp;
            Grid1.DataBind();
        }
        private void Grid1_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            DataTable TmpDt = new DataTable();
            TmpDt.Columns.Add("OpCode", typeof(string));
            TmpDt.Columns.Add("OpName", typeof(string));

            TmpDt.Rows.Add(new object[] { e.Row.Cells["WORKCENTERCODE"].Value, e.Row.Cells["WORKCENTERNAME"].Value });

            this.Tag = TmpDt;
            this.Close();
        }

        private void txtOpCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                search();
            }
        }

        private void txtOpName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                search();
            }
        }


    }
}
