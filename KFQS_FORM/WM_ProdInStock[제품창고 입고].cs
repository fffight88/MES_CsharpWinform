#region < HEADER AREA >
// *---------------------------------------------------------------------------------------------*
//   Form ID      : WM_ProdInStock
//   Form Name    : 제품 창고 입고
//   Name Space   : DC_WM
//   Created Date : 2020.08
//   Made By      : DC00
//   Description  : 기준정보 ( 품목 마스터 ) 정보 관리 폼 
// *---------------------------------------------------------------------------------------------*
#endregion

#region <USING AREA>
using System; 
using System.Data; 
using DC00_assm; 
using DC00_WinForm;
using DC00_PuMan;
#endregion

namespace KFQS_Form
{
    public partial class WM_ProdInStock : DC00_WinForm.BaseMDIChildForm
    {
        #region [ 생성자 ]
        DataTable rtnDtTemp = new DataTable(); // return DataTable 공통
        UltraGridUtil _GridUtil = new UltraGridUtil();  //그리드 객체 생성
         
        #endregion

        #region [ 선언자 ]
        public WM_ProdInStock()
        {
            InitializeComponent(); 
            BizTextBoxManager btbManager = new BizTextBoxManager();
            btbManager.PopUpAdd(txtItemCode_H, txtItemName_H, "ITEM_MASTER" ); 
        }

        #endregion

        #region [ Form Load ]
        private void WM_ProdInStock_Load(object sender, EventArgs e)
        {
            #region Grid1 셋팅
            _GridUtil.InitializeGrid(this.grid1, true, true, false, "", false);
            _GridUtil.InitColumnUltraGrid(grid1, "CHK",       "선택",     GridColDataType_emu.CheckBox,    80, Infragistics.Win.HAlign.Center,   true, true);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE", "공장",     GridColDataType_emu.VarChar,    130, Infragistics.Win.HAlign.Left,     true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "LOTNO",     "LOTNO",    GridColDataType_emu.VarChar,    130, Infragistics.Win.HAlign.Left,     true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",  "품목코드", GridColDataType_emu.VarChar,    130, Infragistics.Win.HAlign.Left,     true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME",  "품목명",   GridColDataType_emu.VarChar,    130, Infragistics.Win.HAlign.Left,     true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMTYPE",  "품목구분", GridColDataType_emu.VarChar,    130, Infragistics.Win.HAlign.Left,     true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WHCODE",    "창고명",   GridColDataType_emu.VarChar,    130, Infragistics.Win.HAlign.Left,     true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "STOCKQTY",  "LOT수량",  GridColDataType_emu.VarChar,    130, Infragistics.Win.HAlign.Right,    true, false,"#,###");
            _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE",  "단위",     GridColDataType_emu.VarChar,     80, Infragistics.Win.HAlign.Left,     true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKER",    "등록자",   GridColDataType_emu.VarChar,    100, Infragistics.Win.HAlign.Left,     true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",  "등록일시", GridColDataType_emu.DateTime24, 140, Infragistics.Win.HAlign.Center,   true, false);

            _GridUtil.SetInitUltraGridBind(grid1);
             

            #endregion

            #region 콤보박스
            Common _Common = new Common();
            rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  //사업장
            Common.FillComboboxMaster(this.cboPlantCode_H, rtnDtTemp );
            UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", rtnDtTemp );
            #endregion
            cboPlantCode_H.Value = LoginInfo.PlantCode;
        }
        #endregion

        #region [ TOOL BAR AREA ]
        /// <summary>
        /// ToolBar의 조회 버튼 클릭
        /// </summary>

        public override void DoInquire()
        {
            DBHelper helper = new DBHelper(false);

            try
            {
                base.DoInquire();

                string sPlantCode = Convert.ToString(this.cboPlantCode_H.Value);
                string sItemCode  = txtItemCode_H.Text;
                string sItemName  = txtItemName_H.Text;
                string sStartDate = string.Format("{0:yyyy-MM-dd}", dtStartDate);
                string sEndDate   = string.Format("{0:yyyy-MM-dd}", dtEnddate);

                rtnDtTemp = helper.FillTable("00WM_ProdInStock_S1", CommandType.StoredProcedure
                                                             , helper.CreateParameter("PLANTCODE",  sPlantCode )
                                                             , helper.CreateParameter("ITEMCODE",   sItemCode  )
                                                             , helper.CreateParameter("ITEMNAME",   sItemName )
                                                             , helper.CreateParameter("STARTDATE",  sItemName )
                                                             , helper.CreateParameter("ENDDATE",    sItemName  )
                                                             );
                if (rtnDtTemp.Rows.Count > 0)
                {
                    grid1.DataSource = rtnDtTemp;
                    grid1.DataBinds(rtnDtTemp);
                }
                else
                {
                    _GridUtil.Grid_Clear(grid1);
                    this.ShowDialog("R00111", DialogForm.DialogType.OK);    // 조회할 데이터가 없습니다.
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                helper.Close();
            }
        }


        /// <summary>
        /// ToolBar의 저장 버튼 Click
        /// </summary>
        public override void DoSave()
        {
            DataTable dt = grid1.chkChange();
            if (dt == null)
                return;
            DBHelper helper = new DBHelper("", true);
            try
            {
                // 변경된 사항을 저장 하시겠습니까?
                if (this.ShowDialog("C:Q00009") == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                base.DoSave();

                this.grid1.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.DeactivateCell);
                foreach (DataRow drRow in dt.Rows)
                {
                    switch (drRow.RowState)
                    {
                        case DataRowState.Deleted:
                            #region 삭제
                            drRow.RejectChanges();

                            #endregion
                            break;
                        case DataRowState.Added:
                            #region 추가

                            #endregion
                            break;
                        case DataRowState.Modified:
                            #region 수정
                            helper.ExecuteNoneQuery("00WM_ProdInStock_U1"
                                                    , CommandType.StoredProcedure
                                                    , helper.CreateParameter("PLANTCODE",  drRow["PLANTCODE"]  )   //  공장
                                                    , helper.CreateParameter("LOTNO",      drRow["LOTNO"]     )   //  LOTNO
                                                    , helper.CreateParameter("ITEMCODE",   drRow["ITEMCODE"]  )   //  ITEMCODE
                                                    , helper.CreateParameter("INOUTQTY",   drRow["STOCKQTY"]   )   //  수량
                                                    , helper.CreateParameter("UNITCODE",   drRow["UNITCODE"]  )   //  단위
                                                    , helper.CreateParameter("MAKER",      LoginInfo.UserID   )); //  등록자
                            #endregion
                            break;
                    }
                    if (helper.RSCODE != "S")
                    {
                        break;
                    }
                }

                if (helper.RSCODE == "S")
                {
                    this.ClosePrgForm();
                    this.ShowDialog("R00002", DialogForm.DialogType.OK);    // 데이터가 저장 되었습니다.
                    helper.Commit();
                    DoInquire();
                }
                else if (helper.RSCODE == "E")
                {
                    this.ClosePrgForm();
                    helper.Rollback();
                    this.ShowDialog(helper.RSMSG, DialogForm.DialogType.OK);
                }

            }
            catch (Exception ex)
            {
                helper.Rollback();
                this.ShowDialog(ex.ToString());
            }
            finally
            {
                helper.Close();
            }
        }
        #endregion

        #region [ User Method Area ]
        #endregion
    }
}