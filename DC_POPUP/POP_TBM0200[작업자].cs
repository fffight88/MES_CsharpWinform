using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DC00_assm;
using DC00_PuMan;

namespace DC_POPUP
{
    public partial class POP_TBM0200 : DC00_WinForm.BasePopupForm
    {

        #region [ 선언자 ]
        //그리드 객체 생성
        UltraGridUtil _GridUtil = new UltraGridUtil();

        ////비지니스 로직 객체 생성
        PopUp_Biz _biz = new PopUp_Biz();

        //임시로 사용할 데이터테이블 생성
        DataTable _DtTemp = new DataTable();
        #endregion

        public POP_TBM0200(string[] param)
        {
            InitializeComponent();

 
            Common _Common = new Common();

            DataTable rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  //사업장
            Common.FillComboboxMaster(this.cboPlantCode_H, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");

            rtnDtTemp = _Common.Standard_CODE("USEFLAG");
            Common.FillComboboxMaster(this.cboUseFlag_H, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            
            //rtnDtTemp = _Common.GET_TBM0000_CODE("RPQDEPT");
            //SAMMI.Common.FillComboboxMaster(this.cboDept, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
           
            // [작업자 및 명 Parameter Show 7=0 사업장, 1, 공정코드, 2,라인코드, 3,작업장, 4, 작업자ID, 5. 작업자, 6.사용여부]
            cboPlantCode_H.Text = param[0] == "" ? "ALL" : param[0];
            txtWorkerID.Text = param[4];
            txtWorkerName.Text = param[5];
            cboUseFlag_H.Text = param[6] == "" ? "ALL" : param[6];

 
        }

        private void POP_TBM0200_Load(object sender, EventArgs e)
        {
            _GridUtil.InitializeGrid(this.Grid1);
            
            _GridUtil.InitColumnUltraGrid(Grid1,"PlantCode",     "사업장",      true, GridColDataType_emu.VarChar, 100, 100, Infragistics.Win.HAlign.Center, true, false, null, null, null, null, null);
            _GridUtil.InitColumnUltraGrid(Grid1,"PlantCodeNm",   "공장명",      true, GridColDataType_emu.VarChar, 100, 100, Infragistics.Win.HAlign.Left, false, false, null, null, null, null, null);
            _GridUtil.InitColumnUltraGrid(Grid1,"OPCode",        "공정코드",    true, GridColDataType_emu.VarChar, 100, 100, Infragistics.Win.HAlign.Center, false, false, null, null, null, null, null);
            _GridUtil.InitColumnUltraGrid(Grid1,"LineCode",      "라인",        true, GridColDataType_emu.VarChar, 100, 100, Infragistics.Win.HAlign.Center, false, false, null, null, null, null, null);
            _GridUtil.InitColumnUltraGrid(Grid1,"WorkCenterCode","작업장코드",  true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Center, false, false, null, null, null, null, null);
            _GridUtil.InitColumnUltraGrid(Grid1,"WorkerID",      "작업자ID",    true, GridColDataType_emu.VarChar, 100, 100, Infragistics.Win.HAlign.Left, true, false, null, null, null, null, null);
            _GridUtil.InitColumnUltraGrid(Grid1,"WorkerName",    "작업자명",    true, GridColDataType_emu.VarChar, 200, 100, Infragistics.Win.HAlign.Left, true, false, null, null, null, null, null);

            _GridUtil.SetInitUltraGridBind(Grid1);

            search();
         
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            search();
        }
        private void search()
        {
            string RS_CODE         = string.Empty, RS_MSG = string.Empty;
            string sPlantCode      = Convert.ToString(cboPlantCode_H.Value);
            string sOPCode         = ""; // cboOPCode_H.Value.ToString() == "ALL" ? "" : cboOPCode_H.Value.ToString();            // 공정코드
            string sLineCode       = ""; // cboLineCode_H.Value.ToString() == "ALL" ? "" : cboLineCode_H.Value.ToString();            // 라인코드
            string sWorkCenterCode = ""; // cboWorkCenterCode_H.Value.ToString() == "ALL" ? "" : cboWorkCenterCode_H.Value.ToString();  // 작업장코드
            string sWorkerID       = txtWorkerID.Text.Trim();                                                                  // 작업자 ID
            string sWorkerName     = txtWorkerName.Text.Trim();                                                                // 작업자명
            string sUseFlag        = Convert.ToString(cboUseFlag_H.Value);
            //string sDeptCode       = Convert.ToString(cboDept.SelectedValue);

            DataTable rtnDtTemp = new DataTable(); // return DataTable 공통
            Common _Common      = new Common();
            rtnDtTemp           = _Common.Standard_CODE("PLANTCODE");  //사업장
            UltraGridUtil.SetComboUltraGrid(this.Grid1, "PlantCode", rtnDtTemp, "CODE_ID", "CODE_NAME");


            _DtTemp = _biz.SEL_TBM0200(sPlantCode, sOPCode, sLineCode, sWorkCenterCode, sWorkerID, sWorkerName, sUseFlag);

            Grid1.DataSource = _DtTemp;
            Grid1.DataBind();
        }
 
        private void Grid1_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            DataTable TmpDt = new DataTable();
            TmpDt.Columns.Add("WorkerID", typeof(string));
            TmpDt.Columns.Add("WorkerName", typeof(string));
            TmpDt.Columns.Add("OPCode", typeof(string));
            TmpDt.Columns.Add("LineCode", typeof(string));
            TmpDt.Columns.Add("WorkCenterCode", typeof(string));
            
            TmpDt.Rows.Add(new object[] { e.Row.Cells["WorkerID"].Value, e.Row.Cells["WorkerName"].Value
                , e.Row.Cells["OPCode"].Value, e.Row.Cells["LineCode"].Value, e.Row.Cells["WorkCenterCode"].Value });

            this.Tag = TmpDt;
            this.Close();
        }

        private void txtWorkerID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                search();
            }
        }

        private void txtWorkerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                search();
            }
        }
    }
}
