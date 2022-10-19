#region < HEADER AREA >
// *---------------------------------------------------------------------------------------------*
//   Form ID      : AP_OrderPerProduct
//   Form Name    : 작업지시 별 생산 실적 현황 및 달성율
//   Name Space   : KFQS_Form
//   Created Date : 2020/08
//   Made By      : DSH
//   Description  : 
// *---------------------------------------------------------------------------------------------*
#endregion

#region < USING AREA >
using System;
using System.Data;
using System.Windows.Forms;

using DC00_assm;
using DC00_WinForm;

using Infragistics.Win.UltraWinGrid;

using Infragistics.Win;
#endregion

namespace KFQS_Form
{
    public partial class AP_OrderPerProduct : DC00_WinForm.BaseMDIChildForm
    {

        #region < MEMBER AREA >
        DataTable rtnDtTemp        = new DataTable(); // return DataTable 공통
        private DataTable DtChange = null;
        UltraGridUtil _GridUtil    = new UltraGridUtil();  //그리드 셋팅 클래스 선언
        string plantCode           = LoginInfo.PlantCode;

        #endregion


        #region < CONSTRUCTOR >
        public AP_OrderPerProduct()
        {
            InitializeComponent();
        }
        #endregion


        #region < FORM EVENTS >
        private void AP_OrderPerProduct_Load(object sender, EventArgs e)
        {
            #region ▶ GRID ◀
            //_GridUtil.InitializeGrid(this.grid1, true, true, false, "", false);
            _GridUtil.InitializeGrid(this.grid1);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",      "공장",                GridColDataType_emu.VarChar,      120,  HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ORDERNO",        "작업지시번호",        GridColDataType_emu.VarChar,      120,  HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",       "생산 품목",           GridColDataType_emu.VarChar,      120,  HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME",       "생산 품명",           GridColDataType_emu.VarChar,      160,  HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERCODE", "작업장",              GridColDataType_emu.VarChar,      100,  HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERNAME", "작업장명",            GridColDataType_emu.VarChar,      130,  HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ORDERQTY",       "지시수량",            GridColDataType_emu.Double,       100,  HAlign.Right,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE",       "단위",                GridColDataType_emu.VarChar,      100,  HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ORDERDATE",      "지시일자",            GridColDataType_emu.VarChar,      100,  HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "PRODQTY",        "양품수량",            GridColDataType_emu.Double,       100,  HAlign.Right,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "BADQTY",         "불량수량",            GridColDataType_emu.Double,       100,  HAlign.Right,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "TOTALPRODQTY",   "총생산수량",          GridColDataType_emu.Double,       100,  HAlign.Right,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ACTRATE",        "지시달성률",          GridColDataType_emu.VarChar,      100,  HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "FIRSTSTARTDATE", "지시시작일시",        GridColDataType_emu.DateTime24,   160,  HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ORDERCLOSEDATE", "지시종료일시",        GridColDataType_emu.DateTime24,   160,  HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "TOTALRUNTIME",   "지시운영시간(분)",    GridColDataType_emu.Double,       100,  HAlign.Right,   true, false);
            _GridUtil.SetInitUltraGridBind(grid1);
            #endregion

            #region ▶ COMBOBOX ◀
            rtnDtTemp = Common.StandardCODE("PLANTCODE");  // 사업장
            Common.FillComboboxMaster(this.cboPlantCode, rtnDtTemp);
            UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", rtnDtTemp);

            rtnDtTemp = Common.StandardCODE("UNITCODE");     //단위
            UltraGridUtil.SetComboUltraGrid(this.grid1, "UNITCODE", rtnDtTemp ); 

            rtnDtTemp = Common.Get_ItemCode(new string[] {"FERT"}); // 완제품 품목
            Common.FillComboboxMaster(this.cboItemCode, rtnDtTemp );

            rtnDtTemp = Common.GET_Workcenter_Code();     //작업장
            Common.FillComboboxMaster(this.cboWorkcenter, rtnDtTemp );
            UltraGridUtil.SetComboUltraGrid(this.grid1, "WORKCENTERCODE", rtnDtTemp); 
            #endregion

            #region ▶ POP-UP ◀
            #endregion

            #region ▶ ENTER-MOVE ◀
            cboPlantCode.Value = plantCode;
            #endregion
        }
        #endregion

         
        public override void DoInquire()
        { 
            DBHelper helper = new DBHelper(false);
            try
            {
                base.DoInquire();
                _GridUtil.Grid_Clear(grid1);
                string sPlantCode      = Convert.ToString(this.cboPlantCode.Value);
                string sWorkcenterCode = Convert.ToString(cboWorkcenter.Value);
                string sItemCode       = Convert.ToString(cboItemCode.Value);
                string sOrderNo        = txtOrderNo.Text;
                string sStartDate      = string.Format("{0:yyyy-MM-dd}", dtStartDate.Value);
                string sEndDate        = string.Format("{0:yyyy-MM-dd}", dtEnddate.Value);

                rtnDtTemp = helper.FillTable("00AP_OrderPerProduct_S1", CommandType.StoredProcedure
                                    , helper.CreateParameter("PLANTCODE",      sPlantCode)
                                    , helper.CreateParameter("WORKCENTERCODE", sWorkcenterCode)
                                    , helper.CreateParameter("ITEMCODE",       sItemCode)
                                    , helper.CreateParameter("ORDERNO",        sOrderNo)
                                    , helper.CreateParameter("STARTDATE",      sStartDate)
                                    , helper.CreateParameter("ENDDATE",        sEndDate)
                                    );

               this.ClosePrgForm();
                this.grid1.DataSource = rtnDtTemp;
            }
            catch (Exception ex)
            {
                ShowDialog(ex.ToString(),DialogForm.DialogType.OK);    
            }
            finally
            {
                helper.Close();
            }
        } 
    }
}




