#region < HEADER AREA >
// *---------------------------------------------------------------------------------------------*
//   Form ID      : PP_LotTracking
//   Form Name    : LOT 추적 관리
//   Name Space   : DC_PP
//   Created Date : 2020/08
//   Made By      : DSH
//   Description  : 
// *---------------------------------------------------------------------------------------------*
#endregion

#region < USING AREA >
using System;
using System.Data;
using DC00_PuMan;

using DC00_assm;
using DC00_WinForm;

using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinToolbars;
#endregion

namespace KFQS_Form
{
    public partial class PP_LotTracking : DC00_WinForm.BaseMDIChildForm
    {

        #region < MEMBER AREA >
        DataTable rtnDtTemp        = new DataTable(); // 
        UltraGridUtil _GridUtil    = new UltraGridUtil();  //그리드 객체 생성
        Common _Common             = new Common();
        string plantCode           = LoginInfo.PlantCode;

        #endregion


        #region < CONSTRUCTOR >
        public PP_LotTracking()
        {
            InitializeComponent();
        }
        #endregion


        #region < FORM EVENTS >
        private void PP_LotTracking_Load(object sender, EventArgs e)
        {
            #region ▶ GRID ◀
            _GridUtil.InitializeGrid(this.grid1);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",      "공장",         GridColDataType_emu.VarChar,    100,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ORDERNO",        "작업지시번호", GridColDataType_emu.VarChar,    130,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "LOTNO",          "생산LOT",      GridColDataType_emu.VarChar,    130,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERCODE", "작업장",       GridColDataType_emu.VarChar,    100,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERNAME", "작업장명",     GridColDataType_emu.VarChar,    140,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",       "생산품목",     GridColDataType_emu.VarChar,    130,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME",       "생산품명",     GridColDataType_emu.VarChar,    150,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "PRODQTY",        "생산수량",     GridColDataType_emu.Double,      80,  Infragistics.Win.HAlign.Right, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE",       "단위",         GridColDataType_emu.VarChar,    100,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "CLOTNO",         "투입LOT",      GridColDataType_emu.VarChar,    130,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "CITEMCODE",      "투입품목",     GridColDataType_emu.VarChar,    130,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "CITEMNAME",      "투입품명",     GridColDataType_emu.VarChar,    150,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "INQTY",          "투입수량",     GridColDataType_emu.Double,     120,  Infragistics.Win.HAlign.Right, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "CUNITCODE",      "투입단위",     GridColDataType_emu.VarChar,    100,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",       "등록일시",     GridColDataType_emu.DateTime24, 160,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKER",          "등록자",       GridColDataType_emu.VarChar,    130,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.SetInitUltraGridBind(grid1);


            _GridUtil.InitializeGrid(this.grid2);
            _GridUtil.InitColumnUltraGrid(grid2, "PLANTCODE",  "공장",        GridColDataType_emu.VarChar,    100,  Infragistics.Win.HAlign.Left, false, false);
            _GridUtil.InitColumnUltraGrid(grid2, "INOUTDATE",  "입출일자",    GridColDataType_emu.VarChar,    130,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "ITEMCODE",   "품목",        GridColDataType_emu.VarChar,    130,  Infragistics.Win.HAlign.Left, false, false);
            _GridUtil.InitColumnUltraGrid(grid2, "ITEMNAME",   "품명",        GridColDataType_emu.VarChar,    100,  Infragistics.Win.HAlign.Left, false, false);
            _GridUtil.InitColumnUltraGrid(grid2, "LOTNO",      "LOTNO",       GridColDataType_emu.VarChar,    140,  Infragistics.Win.HAlign.Left, false, false);
            _GridUtil.InitColumnUltraGrid(grid2, "WHNAME",     "창고",        GridColDataType_emu.VarChar,    130,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "INOUTCNAME", "입출유형",    GridColDataType_emu.VarChar,    150,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "INOUTFNAME", "입출구분",    GridColDataType_emu.VarChar,     80,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "INOUTQTY",   "입출수량",    GridColDataType_emu.Double,     100,  Infragistics.Win.HAlign.Right, true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "INOUTWNAME", "작업자",      GridColDataType_emu.VarChar,    130,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "MAKEDATE",   "등록일시",    GridColDataType_emu.DateTime24, 130,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.SetInitUltraGridBind(grid2);

            _GridUtil.InitializeGrid(this.grid3);
            _GridUtil.InitColumnUltraGrid(grid3, "PLANTCODE",  "공장",          GridColDataType_emu.VarChar,    100,  Infragistics.Win.HAlign.Left, false, false);
            _GridUtil.InitColumnUltraGrid(grid3, "INOUTDATE",  "입출일자",      GridColDataType_emu.VarChar,    130,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid3, "ITEMCODE",   "품목",          GridColDataType_emu.VarChar,    130,  Infragistics.Win.HAlign.Left, false, false);
            _GridUtil.InitColumnUltraGrid(grid3, "ITEMNAME",   "품명",          GridColDataType_emu.VarChar,    100,  Infragistics.Win.HAlign.Left, false, false);
            _GridUtil.InitColumnUltraGrid(grid3, "LOTNO",      "LOTNO",         GridColDataType_emu.VarChar,    140,  Infragistics.Win.HAlign.Left, false, false);
            _GridUtil.InitColumnUltraGrid(grid3, "WHNAME",     "창고",          GridColDataType_emu.VarChar,    130,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid3, "INOUTCNAME", "입출유형",      GridColDataType_emu.VarChar,    150,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid3, "INOUTFNAME", "입출구분",      GridColDataType_emu.VarChar,     80,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid3, "INOUTQTY",   "입출수량",      GridColDataType_emu.Double,     100,  Infragistics.Win.HAlign.Right, true, false);
            _GridUtil.InitColumnUltraGrid(grid3, "INOUTWNAME", "작업자",        GridColDataType_emu.VarChar,    130,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid3, "SHIPNO",     "상차/명세번호", GridColDataType_emu.VarChar,    150,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid3, "CARNO",      "차량번호",      GridColDataType_emu.VarChar,    120,  Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid3, "MAKEDATE",   "등록일시",      GridColDataType_emu.DateTime24, 130,  Infragistics.Win.HAlign.Left,  true, false); 
            _GridUtil.SetInitUltraGridBind(grid3);


            
            #endregion

            #region ▶ COMBOBOX ◀
            rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  // 사업장
            Common.FillComboboxMaster(this.cboPlantCode, rtnDtTemp );
            UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", rtnDtTemp );
            #endregion

            #region ▶ POP-UP ◀
            BizTextBoxManager btbManager = new BizTextBoxManager();
            btbManager.PopUpAdd(txtPItemCode_H, txtPItemName_H, "ITEM_MASTER" );
            btbManager.PopUpAdd(txtCItemCode,   txtCItemName,   "ITEM_MASTER" );

            #endregion

            #region ▶ ENTER-MOVE ◀
            cboPlantCode.Value = plantCode;
            #endregion
        }
        #endregion


        #region < TOOL BAR AREA >
        public override void DoInquire()
        {
            DoFind();
        }
        private void DoFind()
        {
            DBHelper helper = new DBHelper(false);
            try
            {                
                _GridUtil.Grid_Clear(grid1);
                _GridUtil.Grid_Clear(grid2);
                _GridUtil.Grid_Clear(grid3);
                string sPlantCode   = Convert.ToString(cboPlantCode.Value);
                string sPLotNo      = Convert.ToString(txtPLotNo.Text);
                string sPItemCode   = Convert.ToString(txtPItemCode_H.Text);
                string sCLotNo      = Convert.ToString(txtCLotNo.Text);
                string sCItemCode   = Convert.ToString(txtCItemCode.Text);

                rtnDtTemp = helper.FillTable("PP_LotTracking_S1", CommandType.StoredProcedure
                                    , helper.CreateParameter("PLANTCODE",    sPlantCode )
                                    , helper.CreateParameter("PLOTNO",       sPLotNo )
                                    , helper.CreateParameter("PITEMCODE",    sPItemCode  )
                                    , helper.CreateParameter("CLOTNO",       sCLotNo )
                                    , helper.CreateParameter("CITEMCODE",    sCItemCode  )
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
        
        #endregion
         
        private void grid1_AfterRowActivate(object sender, EventArgs e)
        {
            DBHelper helper = new DBHelper(false);
            try
            {                
                _GridUtil.Grid_Clear(grid2);
                _GridUtil.Grid_Clear(grid3);
                string sPlantCode   = Convert.ToString(this.grid1.ActiveRow.Cells["PLANTCODE"].Value);
                
                string sCPLotNo = Convert.ToString(this.grid1.ActiveRow.Cells["CLOTNO"].Value);

                DataTable rtnDtTemp2 = helper.FillTable("PP_LotTracking_S2", CommandType.StoredProcedure
                                    , helper.CreateParameter("PLANTCODE", sPlantCode )
                                    , helper.CreateParameter("CLOTNO",    sCPLotNo )
                                    );

                this.grid2.DataSource = rtnDtTemp2;


                string sPLotNo = Convert.ToString(this.grid1.ActiveRow.Cells["LOTNO"].Value);

                DataTable rtnDtTemp3 = helper.FillTable("PP_LotTracking_S3", CommandType.StoredProcedure
                                    , helper.CreateParameter("PLANTCODE", sPlantCode )
                                    , helper.CreateParameter("PLOTNO",    sPLotNo )
                                    );


                this.grid3.DataSource = rtnDtTemp3;

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




