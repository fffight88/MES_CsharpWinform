#region < HEADER AREA >
// *---------------------------------------------------------------------------------------------*
//   Form ID      : PP_STockHALB
//   Form Name    : 재공 재고 조회
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
#endregion

namespace KFQS_Form
{
    public partial class PP_STockHALB : DC00_WinForm.BaseMDIChildForm
    {

        #region < MEMBER AREA >
        DataTable rtnDtTemp        = new DataTable(); // 
        UltraGridUtil _GridUtil    = new UltraGridUtil();  //그리드 객체 생성 
        string plantCode           = LoginInfo.PlantCode;

        #endregion


        #region < CONSTRUCTOR >
        public PP_STockHALB()
        {
            InitializeComponent();
        }
        #endregion


        #region < FORM EVENTS >
        private void PP_STockHALB_Load(object sender, EventArgs e)
        {
            #region ▶ GRID ◀
            //_GridUtil.InitializeGrid(this.grid1, true, true, false, "", false);
            //_GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE", "공장", true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitializeGrid(this.grid1);
            
            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",      "공장",      GridColDataType_emu.VarChar,    120, Infragistics.Win.HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",       "품목",      GridColDataType_emu.VarChar,    140, Infragistics.Win.HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME",       "품목명",    GridColDataType_emu.VarChar,    140, Infragistics.Win.HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMTYPE",       "품목구분",  GridColDataType_emu.VarChar,    120, Infragistics.Win.HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "LOTNO",          "LOTNO",     GridColDataType_emu.VarChar,    150, Infragistics.Win.HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERCODE", "작업장",    GridColDataType_emu.VarChar,    120, Infragistics.Win.HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERNAME", "작업장명",  GridColDataType_emu.VarChar,    120, Infragistics.Win.HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "STOCKQTY",       "재고수량",  GridColDataType_emu.Double,     100, Infragistics.Win.HAlign.Right,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE",       "단위",      GridColDataType_emu.VarChar,    100, Infragistics.Win.HAlign.Left,    true, false);
            _GridUtil.SetInitUltraGridBind(grid1);
            #endregion

            #region ▶ COMBOBOX ◀
            rtnDtTemp = Common.StandardCODE("PLANTCODE");  // 사업장
            Common.FillComboboxMaster(this.cboPlantCode, rtnDtTemp );
            UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", rtnDtTemp );

            rtnDtTemp = Common.StandardCODE("ITEMTYPE");     //품목 구분
            Common.FillComboboxMaster(this.cboItemType, rtnDtTemp );
            UltraGridUtil.SetComboUltraGrid(this.grid1, "ITEMTYPE", rtnDtTemp);

            #endregion

            #region ▶ POP-UP ◀
            BizTextBoxManager btbManager = new BizTextBoxManager();
            btbManager.PopUpAdd(txtItemCode_H, txtItemName_H, "ITEM_MASTER");
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
                base.DoInquire();
                _GridUtil.Grid_Clear(grid1);
                string sPlantCode  = Convert.ToString(cboPlantCode.Value);
                string sItemType   = Convert.ToString(cboItemType.Value);
                string sLotNo      = Convert.ToString(txtLotNo.Text);
                string sItemCode   = Convert.ToString(txtItemCode_H.Text);


                rtnDtTemp = helper.FillTable("00PP_STockHALB_S1", CommandType.StoredProcedure
                                                                   , helper.CreateParameter("PLANTCODE", sPlantCode )                  
                                                                   , helper.CreateParameter("ITEMTYPE",  sItemType )
                                                                   , helper.CreateParameter("LOTNO",     sLotNo    )
                                                                   , helper.CreateParameter("ITEMCODE",  sItemCode )
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
    }
}




