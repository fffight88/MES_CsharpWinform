#region < HEADER AREA >
// *---------------------------------------------------------------------------------------------*
//   Form ID      : WM_StockShip
//   Form Name    : 제품 재고 관리
//   Name Space   : DC_WM
//   Created Date : 2021.05
//   Made By      : 
//   Description  : 제품관리 제품 재고 관리.
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
    public partial class WM_StockShip : DC00_WinForm.BaseMDIChildForm
    {
        #region [ 생성자 ]
        DataTable rtnDtTemp = new DataTable(); // return DataTable 공통
        UltraGridUtil _GridUtil = new UltraGridUtil();  //그리드 객체 생성
        #endregion

        #region [ 선언자 ]
        public WM_StockShip()
        {
            InitializeComponent();
            BizTextBoxManager btbManager = new BizTextBoxManager();
            btbManager.PopUpAdd(txtItemCode_H, txtItemName_H, "ITEM_MASTER",  new object[] { "1000", "" }); 
        }

        #endregion

        #region [ Form Load ]
        private void WM_StockShip_Load(object sender, EventArgs e)
        {
            #region ▶ GRID ◀
            _GridUtil.InitializeGrid(this.grid1, true, true, false, "", false);
            _GridUtil.InitColumnUltraGrid(grid1, "CHK",       "상차 등록",  true, GridColDataType_emu.CheckBox, 120, 120, Infragistics.Win.HAlign.Left,  true, true);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE", "공장",       true, GridColDataType_emu.VarChar,  120, 120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",  "품목",       true, GridColDataType_emu.VarChar,  140, 120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME",  "품목명",     true, GridColDataType_emu.VarChar,  140, 120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "LOTNO",     "LOTNO",      true, GridColDataType_emu.VarChar,  120, 120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WHCODE",    "입고창고",   true, GridColDataType_emu.VarChar,  120, 120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "STOCKQTY",  "재고수량",   true, GridColDataType_emu.Double,   100, 120, Infragistics.Win.HAlign.Right, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE",  "단위",       true, GridColDataType_emu.VarChar,  100, 120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "INDATE",    "입고일자",   true, GridColDataType_emu.VarChar,  100, 120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",  "등록일시",   true, GridColDataType_emu.VarChar,  150, 120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKER",     "등록자",     true, GridColDataType_emu.VarChar,  100, 120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.SetInitUltraGridBind(grid1);
            #endregion

            #region ▶ COMBOBOX ◀
            Common _Common = new Common();
            rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  // 사업장
            Common.FillComboboxMaster(this.cboPlantCode_H, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

            rtnDtTemp = _Common.Standard_CODE("UNITCODE");     //단위
            UltraGridUtil.SetComboUltraGrid(this.grid1, "UNITCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

            rtnDtTemp = _Common.Standard_CODE("WHCODE");     //입고 창고
            UltraGridUtil.SetComboUltraGrid(this.grid1, "WHCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

            #endregion

            #region ▶ POP-UP ◀
            BizTextBoxManager btbManager = new BizTextBoxManager();
            btbManager.PopUpAdd(txtWorker_H, txtWorkerName_H, "WORKER_MASTER", new object[] { "", "", "", "", "" });
            btbManager.PopUpAdd(txtCustCode_H, txtCustName_H, "CUST_MASTER", new object[] { cboPlantCode_H, "", "", "" });
            #endregion

            #region ▶ ENTER-MOVE ◀
            cboPlantCode_H.Value = "1000";
            #endregion
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
                string sPlantCode = Convert.ToString(this.cboPlantCode_H.Value);
                string sItemCode  = Convert.ToString(txtItemCode_H.Text);
                string sLotNo     = txtLotNo.Text.ToString();
                string sStartDate = string.Format("{0:yyyy-MM-dd}", dtStartDate.Value);
                string sEndDate   = string.Format("{0:yyyy-MM-dd}", dtEnddate.Value);

                rtnDtTemp = helper.FillTable("WM_StockShip_S1", CommandType.StoredProcedure
                                                             , helper.CreateParameter("PLANTCODE",  sPlantCode,  DbType.String, ParameterDirection.Input)
                                                             , helper.CreateParameter("ITEMCODE",   sItemCode,   DbType.String, ParameterDirection.Input)
                                                             , helper.CreateParameter("LOTNO",      sLotNo,      DbType.String, ParameterDirection.Input)
                                                             , helper.CreateParameter("STARTDATE",  sStartDate,  DbType.String, ParameterDirection.Input)
                                                             , helper.CreateParameter("ENDDATE",    sEndDate,    DbType.String, ParameterDirection.Input)
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
        public override void DoSave()
        {
            this.grid1.UpdateData();
            DataTable dt = grid1.chkChange();
            if (dt == null)
                return;
            DBHelper helper = new DBHelper("", true);
            try
            {
                if (this.ShowDialog("선택하신 내역을 상차 등록 하시겠습니까 ?") == System.Windows.Forms.DialogResult.Cancel) return;

                string CarNo     = txtCarNo.Text;
                string sWorker   = txtWorker_H.Text;
                string sCustCode = txtCustCode_H.Text;
                if (CarNo == "")
                {
                    ShowDialog("차량 정보를 입력하지 않았습니다.", DialogForm.DialogType.OK);
                    return;
                }
                if (sWorker == "")
                {
                    ShowDialog("상차 작업자 정보를 입력하지 않았습니다.", DialogForm.DialogType.OK);
                    return;
                }
                if (sCustCode == "")
                {
                    ShowDialog("거래처 정보를 입력하지 않았습니다.", DialogForm.DialogType.OK);
                    return;
                }
                string sShipNo = string.Empty;
                foreach (DataRow drRow in dt.Rows)
                {
                    switch (drRow.RowState)
                    {
                        case DataRowState.Deleted:
                            #region 삭제 
                            #endregion
                            break;
                        case DataRowState.Added:
                            #region 추가

                            #endregion
                            break;
                        case DataRowState.Modified:
                            #region 수정 
                            if (Convert.ToString(drRow["CHK"]) != "1") continue;
                            helper.ExecuteNoneQuery("WM_StockShip_U1", CommandType.StoredProcedure
                                                  , helper.CreateParameter("PLANTCODE",  Convert.ToString(drRow["PLANTCODE"]), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("LOTNO",      Convert.ToString(drRow["LOTNO"]),     DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("CARNO",      CarNo,                                DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("CUSTCODE",   sCustCode,                            DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("WORKER",     sWorker,                              DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("ITEMCODE",   Convert.ToString(drRow["ITEMCODE"]),  DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("SHIPQTY",    Convert.ToString(drRow["STOCKQTY"]),  DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("SHIPNO",     sShipNo,                              DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("MAKER",      LoginInfo.UserID,                     DbType.String, ParameterDirection.Input)
                                                  );

                            if (helper.RSCODE == "S")
                            {
                                sShipNo = helper.RSMSG;
                            }
                            else break;
                            #endregion
                            break;
                    }
                    if (helper.RSCODE != "S") break;
                }
                if (helper.RSCODE != "S")
                {
                    this.ClosePrgForm();
                    helper.Rollback();
                    this.ShowDialog(helper.RSMSG, DialogForm.DialogType.OK);
                    return;
                }
                helper.Commit();
                this.ClosePrgForm();
                this.ShowDialog("데이터가 저장 되었습니다.", DialogForm.DialogType.OK);
                DoInquire();
            }
            catch (Exception ex)
            {
                CancelProcess = true;
                helper.Rollback();
                ShowDialog(ex.ToString());
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