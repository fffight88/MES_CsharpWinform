using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DC00_assm;
using DC00_WinForm;
using DC00_PuMan;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using DC_POPUP;



/**************************************************************************
 * From ID   : PP_ActureOutput
 * Form Name : 생산 실적 등록
 * date      : 2022-07-05
 * Mkaer     : 동상현
 * 
 * 수정사항  : 
 * *************************************************************************/
namespace KFQS_Form
{
    public partial class PP_ActureOutput : DC00_WinForm.BaseMDIChildForm
    {

        #region <  MEMBER AREA  >
        DataTable dtTemp          = new DataTable();
        UltraGridUtil _GridUtil   = new UltraGridUtil();   // 그리드 셋팅 클래스.
        private string sPlantCode = LoginInfo.PlantCode;   // 로그인 한 공장 정보.
        #endregion

        public PP_ActureOutput()
        {
            InitializeComponent();
        }

        private void PP_ActureOutput_Load(object sender, EventArgs e)
        {
            // Grid 셋팅. 
            _GridUtil.InitializeGrid(this.grid1);

            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",      "공장",            GridColDataType_emu.VarChar,    120,   HAlign.Left,  false,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERCODE", "작업장코드",      GridColDataType_emu.VarChar,    120,   HAlign.Left,  false,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERNAME", "작업장명",        GridColDataType_emu.VarChar,    130,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "ORDERNO",        "작업지시번호",    GridColDataType_emu.VarChar,    220,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",       "품번",            GridColDataType_emu.VarChar,    150,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME",       "품명",            GridColDataType_emu.VarChar,    150,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "ORDERQTY",       "작업지시수량",    GridColDataType_emu.Double,     120,   HAlign.Right, true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "PRODQTY",        "양품수량",        GridColDataType_emu.Double,     120,   HAlign.Right, true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "BADQTY",         "불량수량",        GridColDataType_emu.Double,     150,   HAlign.Right, true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE",       "단위",            GridColDataType_emu.VarChar,    130,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKSTATUSCODE", "가동비가동코드",  GridColDataType_emu.VarChar,    130,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKSTATUS",     "상태",            GridColDataType_emu.VarChar,    130,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "MATLOTNO",       "투입LOT",         GridColDataType_emu.VarChar,    270,   HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "COMPONENTQTY",   "투입잔량",        GridColDataType_emu.Double,     130,   HAlign.Right, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKER",         "작업자ID",        GridColDataType_emu.VarChar,    130,   HAlign.Left,  false, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKERNAME",     "작업자명",        GridColDataType_emu.VarChar,    130,   HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "STARTDATE",      "지시시작일시",    GridColDataType_emu.DateTime24, 220,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "ENDDATE",        "지시종료일시",    GridColDataType_emu.DateTime24, 220,   HAlign.Left,  true,  false);

            _GridUtil.SetInitUltraGridBind(grid1);


            // 콤보박스 셋팅.


            #region < 콤보박스 셋팅 설명 >
            // 공장
            // 공장에 대한 시스템 공통 코드 내역을 DB 에서 가져온다. => dtTemp 데이터 테이블에 담는다. 
            dtTemp = Common.StandardCODE("PLANTCODE");
            // 콤보박스 에 받아온 데이터를 밸류멤버와 디스플레이멤버로 표현한다.  
            Common.FillComboboxMaster(cboPlantCode_H,dtTemp);
            // 그리드의 컬럼에 받아온 데이터를 콤보박스 형식으로 셋팅한다.
            UltraGridUtil.SetComboUltraGrid(grid1, "PLANTCODE", dtTemp);
            #endregion



            // 단위
            dtTemp = Common.StandardCODE("UNITCODE");
            UltraGridUtil.SetComboUltraGrid(grid1, "UNITCODE", dtTemp);

            // 작업장 마스터 콤보박스
            dtTemp = Common.GET_Workcenter_Code();
            Common.FillComboboxMaster(cboWorkcenterCode_H, dtTemp);

            BizTextBoxManager BizTxtPopup = new BizTextBoxManager();
            BizTxtPopup.PopUpAdd(txtWorkerID_H, txtWorkerName_H, "WORKER_MASTER");
            
            // 공장 로그인 정보로 자동 셋팅.
            cboPlantCode_H.Value = sPlantCode;
            
        } 

        #region < 작업장 조회 (현재 상태 및 작업지시 내역 포함) >
        public override void DoInquire()
        {
            SetWorkcenter();
        }

        private void SetWorkcenter()
        {
            // 1. 작업장 조회 

            // 툴바의 조회 버튼 클릭.
            DBHelper helper = new DBHelper(false);

            try
            {
                string sPlantCode_     = Convert.ToString(cboPlantCode_H.Value);      // 공장
                string sWorkcenterCode = Convert.ToString(cboWorkcenterCode_H.Value); // 품목코드

                dtTemp = helper.FillTable("TEAM04_PP_ActualOutput_S", CommandType.StoredProcedure
                                          , helper.CreateParameter("@PLANTCODE",      sPlantCode_)
                                          , helper.CreateParameter("@WORKCENTERCODE", sWorkcenterCode)
                                          );

                // 받아온 데이터 그리드 매핑
                if (dtTemp.Rows.Count == 0)
                {
                    // 그리드 초기화.
                    _GridUtil.Grid_Clear(grid1);
                    ShowDialog("조회할 데이터가 없습니다.", DialogForm.DialogType.OK);
                    return;
                }

                grid1.DataSource = dtTemp; 
                grid1.DataBinds(dtTemp);

            }
            catch (Exception ex)
            {
                ShowDialog(ex.ToString());
            }
            finally
            {
                helper.Close();
            }

        }
        #endregion

        private void cboWorkcenterCode_H_ValueChanged(object sender, EventArgs e)
        {
            // 작업장 콤보박스 변경 시 해당 작업장 내역만 조회 되도록 설정.
            SetWorkcenter();
        }
         

        #region < 2. 작업자 등록 >
        private void btnWorkerReg_Click(object sender, EventArgs e)
        {
            // 1. 작업장 선택 여부 확인
            if (grid1.Rows.Count == 0) return;
            
            if (grid1.ActiveRow == null)
            {
                ShowDialog("작업장을 선택 후 진행하세요.");
                return;
            }

            string sWorkerId = txtWorkerID_H.Text;   // 작업자 id
            if (sWorkerId == "")
            {
                ShowDialog("작업자를 선택 후 진행하세요.");
                return;
            }

            string sPlantCode      = Convert.ToString(grid1.ActiveRow.Cells["PLANTCODE"].Value); // 공장
            string sWorkcenterCode = Convert.ToString(grid1.ActiveRow.Cells["WORKCENTERCODE"].Value); // 작업장.


            DBHelper helper = new DBHelper("",true);
            try
            {
                // 선택한 작업장에 작업자를 등록
                helper.ExecuteNoneQuery("00PP_ActureOutput_I1", CommandType.StoredProcedure
                                       , helper.CreateParameter("@PLANTCODE",      sPlantCode)
                                       , helper.CreateParameter("@WORKCENTERCODE", sWorkcenterCode)
                                       , helper.CreateParameter("@WORKERID",       sWorkerId)
                                       );
                if (helper.RSCODE != "S")
                {
                    throw new Exception($"작업자 등록중 오류가 발생하였습니다. \r\n{helper.RSMSG}");
                }
                helper.Commit();
                ShowDialog("정상적으로 등록을 완료하였습니다.");
                SetWorkcenter(); // 정상 완료 후 재조회.
            }
            catch (Exception ex)
            {
                helper.Rollback();
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                helper.Close();
            }

        }
        #endregion

        #region < 3. 작업지시 선택 및 등록 > 
        private void btnWorkOrderReg_Click(object sender, EventArgs e)
        {
            // 작업지시 선택 팝업 호출
            if (grid1.Rows.Count == 0) return;
            if (grid1.ActiveRow == null) return;

            // 1. 작업자 등록 여부 확인.
            string sWorkerId = Convert.ToString(grid1.ActiveRow.Cells["WORKER"].Value);
            if (sWorkerId == "")
            {
                ShowDialog("현재 작업장에 등록된 작업자 내역이 없습니다.\r\n 작업자 등록 후 진행하세요");
                return;
            }

            // 2. 작업장의 현재 상태가 비가동 상태 인지 확인.
            string sRunStop = Convert.ToString(grid1.ActiveRow.Cells["WORKSTATUSCODE"].Value);
            if (sRunStop != "S")
            {
                ShowDialog("현재 작업장이 가동 상태 입니다. \r\n 비가동 등록 후 진행하세요.");
                return;
            }

            // 3. 투입된 lot 정보가 있는지 확인.
            string sMatLotno = Convert.ToString(grid1.ActiveRow.Cells["MATLOTNO"].Value);
            if (sMatLotno != "")
            {
                ShowDialog("현재 투입된 원자재 LOT 가 있습니다. \r\n  투입LOT 를 투입취소 후 진행하세요.");
                return;
            }

            // 4. 작업지시 선택 팝업 호출. 
            string sWorkcenterCode = Convert.ToString(grid1.ActiveRow.Cells["WORKCENTERCODE"].Value);
            string sWorkcenterName = Convert.ToString(grid1.ActiveRow.Cells["WORKCENTERNAME"].Value);

            POP_WORKORDER POP_order = new POP_WORKORDER(sWorkcenterCode, sWorkcenterName);
            POP_order.ShowDialog();

            // 작업지시 팝업에서 선택 한 작업지시 내역이 없을경우. 
            if (POP_order.Tag == null) return;

            string sOrderNo = Convert.ToString(POP_order.Tag);

            DBHelper helper = new DBHelper("", true);
            try
            {
                // 선택한 작업장에 작업지시를 등록
                helper.ExecuteNoneQuery("00PP_ActureOutput_I2", CommandType.StoredProcedure
                                       , helper.CreateParameter("@PLANTCODE",      sPlantCode)
                                       , helper.CreateParameter("@WORKCENTERCODE", sWorkcenterCode)
                                       , helper.CreateParameter("@ORDERNO",        sOrderNo)
                                       , helper.CreateParameter("@WORKERID",       sWorkerId)
                                       );
                if (helper.RSCODE != "S")
                {
                    throw new Exception($"작업지시 등록중 오류가 발생하였습니다. \r\n{helper.RSMSG}");
                }
                helper.Commit();
                ShowDialog("정상적으로 등록을 완료하였습니다.");
                SetWorkcenter(); // 정상 완료 후 재조회.
            }
            catch (Exception ex)
            {
                helper.Rollback();
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                helper.Close();
            }
        }
        #endregion

        private void grid1_AfterRowActivate(object sender, EventArgs e)
        {
            // 그리드의 행을 선택 후 일어나는 이벤트. 

            // 1. 등록 된 작업자를 작업자 텍스트 박스에 표시.
            txtWorkerID_H.Text   = Convert.ToString(grid1.ActiveRow.Cells["WORKER"].Value);
            txtWorkerName_H.Text = Convert.ToString(grid1.ActiveRow.Cells["WORKERNAME"].Value);


            // 2. 투입된 원자재 LOT 가 있을경우 투입 취소 버튼 이름으로 변경.
            string sMatlotno = Convert.ToString(grid1.ActiveRow.Cells["MATLOTNO"].Value);
            if (sMatlotno == "")
            {
                txtROHLotNO_H.Text = "";
                btnROHLotReg.Text = "(4) LOT 투입";
                btnROHLotReg.Tag = "IN";
            }
            else
            {
                txtROHLotNO_H.Text = sMatlotno;
                btnROHLotReg.Text = "(4) LOT 투입 취소";
                btnROHLotReg.Tag = "CAN";
            }

            // 3. 현재 작업장 가동/비가동/고장 상태에 따라 R/S/F 버튼 표시하기.
            if (Convert.ToString(grid1.ActiveRow.Cells["WORKSTATUSCODE"].Value) == "R")
            {
                btnRunStop_H.Text = "(5) 비가동";
                btnRunStop_H.Tag = "S";
                btnErrorReg_H.Tag = "F";
                btnRunStop_H.Enabled = true;
                btnProdReg.Enabled = true;
                btnErrorReg_H.Text = "고장등록";
            }
            else if (Convert.ToString(grid1.ActiveRow.Cells["WORKSTATUSCODE"].Value) == "S")
            {
                btnRunStop_H.Text = "(5) 가동";
                btnRunStop_H.Tag = "R";
                btnRunStop_H.Enabled = true;
                btnProdReg.Enabled = true;
                btnErrorReg_H.Text = "고장등록";
                btnErrorReg_H.Tag = "F";
            }
            else if (Convert.ToString(grid1.ActiveRow.Cells["WORKSTATUSCODE"].Value) == "F")
            {
                btnErrorReg_H.Text = "수리완료";
                btnErrorReg_H.Tag = "R";
                btnRunStop_H.Enabled = false;
                btnProdReg.Enabled = false;

            }

        }

        private void btnROHLotReg_Click(object sender, EventArgs e)
        {
            // 원자재 LOT 투입.
            if (grid1.Rows.Count == 0) return;
            if (grid1.ActiveRow == null) return;

            string sOrderNo = Convert.ToString(grid1.ActiveRow.Cells["ORDERNO"].Value);
            if (sOrderNo == "")
            {
                ShowDialog("작업장에 작업지시가 선택 되지 않았습니다.");
                return;
            }
            string sWorkerid = Convert.ToString(grid1.ActiveRow.Cells["WORKER"].Value);
            if (sWorkerid == "")
            {
                ShowDialog("등록된 작업자가 없습니다. 작업자 선택 후 진행하세요.");
                return;
            }

            DBHelper helper = new DBHelper("", true);
            try
            {
                string sItemCode       = Convert.ToString(grid1.ActiveRow.Cells["ITEMCODE"].Value);
                string sLotNo          = txtROHLotNO_H.Text;
                string sWorkcenterCode = Convert.ToString(grid1.ActiveRow.Cells["WORKCENTERCODE"].Value); 
                string sUnitCode       = Convert.ToString(grid1.ActiveRow.Cells["UNITCODE"].Value);

                // 선택한 작업장에 원자재 LOT 투입/취소
                helper.ExecuteNoneQuery("00PP_ActureOutput_I3", CommandType.StoredProcedure
                                       , helper.CreateParameter("@PLANTCODE",      sPlantCode)
                                       , helper.CreateParameter("@WORKCENTERCODE", sWorkcenterCode)
                                       , helper.CreateParameter("@ITEMCODE",       sItemCode)
                                       , helper.CreateParameter("@ORDERNO",        sOrderNo)
                                       , helper.CreateParameter("@LOTNO",          sLotNo)
                                       , helper.CreateParameter("@UNITCODE",       sUnitCode)
                                       , helper.CreateParameter("@WORKERID",       sWorkerid)
                                       , helper.CreateParameter("@INFLAG",         Convert.ToString(btnROHLotReg.Tag))
                                       );
                if (helper.RSCODE != "S")
                {
                    throw new Exception($"LOT 투입/취소 등록중 오류가 발생하였습니다. \r\n{helper.RSMSG}");
                }
                helper.Commit();
                ShowDialog("정상적으로 등록을 완료하였습니다.");
                SetWorkcenter(); // 정상 완료 후 재조회.
            }
            catch (Exception ex)
            {
                helper.Rollback();
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                helper.Close();
            }

        }

        #region < 5. 가동/ 비가동 클릭 >
        private void btnRunStop_H_Click(object sender, EventArgs e)
        {
            if (grid1.Rows.Count == 0) return;
            if (grid1.ActiveRow == null) return; 


            DBHelper helper = new DBHelper("", true);
            try
            {
                string sItemCode       = Convert.ToString(grid1.ActiveRow.Cells["ITEMCODE"].Value);
                string sWorkcenterCode = Convert.ToString(grid1.ActiveRow.Cells["WORKCENTERCODE"].Value);
                string sOrderNo        = Convert.ToString(grid1.ActiveRow.Cells["ORDERNO"].Value);

                // 선택한 작업장에 원자재 LOT 투입/취소
                helper.ExecuteNoneQuery("00PP_ActureOutput_I4", CommandType.StoredProcedure
                                       , helper.CreateParameter("@PLANTCODE",      sPlantCode)
                                       , helper.CreateParameter("@WORKCENTERCODE", sWorkcenterCode)
                                       , helper.CreateParameter("@ORDERNO",        sOrderNo)
                                       , helper.CreateParameter("@ITEMCODE",       sItemCode)
                                       , helper.CreateParameter("@STATUS",         Convert.ToString(btnRunStop_H.Tag))
                                       );
                if (helper.RSCODE != "S")
                {
                    throw new Exception($"작업장 가동/비가동 등록중 오류가 발생하였습니다. \r\n{helper.RSMSG}");
                }
                helper.Commit();
                ShowDialog("정상적으로 등록을 완료하였습니다.");
                SetWorkcenter(); // 정상 완료 후 재조회.
            }
            catch (Exception ex)
            {
                helper.Rollback();
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                helper.Close();
            }
        }
        #endregion


        #region < 6. 생산 실적 등록 >
        private void btnProdReg_Click(object sender, EventArgs e)
        {
            if (grid1.Rows.Count == 0) return;
            if (grid1.ActiveRow == null) return;

            // 작업지시 수량 대비 양품 불량 수량 입력 가능여부 체크.
            double dProdQty      = 0; // 누적 양품수량.
            double dErrorQty     = 0; // 누적 불량 수량.
            double dTextProdQty  = 0; // 입력한 양품수량
            double dTextErrorQty = 0; // 입력한 불량 수량.
            double dOrderQty     = 0; // 작업지시 수량


            // 그리드의 누적 양품 수량 
            string sProdQty = Convert.ToString(grid1.ActiveRow.Cells["PRODQTY"].Value).Replace(",", "");
            Double.TryParse(sProdQty, out dProdQty);

            // 그리드의 총 누적 불량 수량
            string sBadQty = Convert.ToString(grid1.ActiveRow.Cells["BADQTY"].Value).Replace(",", "");
            Double.TryParse(sBadQty, out dErrorQty);

            // 입력 한 양품 수량 
            string sTextQty = Convert.ToString(txtProdQty_H.Text);
            Double.TryParse(sTextQty, out dTextProdQty);

            // 입력 한 불량 수량 
            string sTextErrorQty = Convert.ToString(txtBadQty_H.Text);
            Double.TryParse(sTextErrorQty, out dTextErrorQty);

            // 작업지시 수량.
            string sOrderQty = Convert.ToString(grid1.ActiveRow.Cells["ORDERQTY"].Value).Replace(",", "");
            Double.TryParse(sOrderQty, out dOrderQty);

            // 투입 된 원자재 LOT 가 존재 하는지 확인.
            string sMatLotNo = Convert.ToString(grid1.ActiveRow.Cells["MATLOTNO"].Value);
            if (sMatLotNo == "")
            {
                ShowDialog("투입한 원자재 LOT 가 없습니다.  LOT 투입 후 진행하세요.");
                return;
            }


            // 양품 또는 불량 수량을 하나도 입력하지 않은경우
            if (dTextProdQty + dTextErrorQty == 0)
            {
                ShowDialog("실적 수량을 입력 하세요.");
                return;
            }

            // 지시 수량 보다 양품 수량이 많은 경우. 
            if (dOrderQty  < dProdQty + dTextProdQty)
            {
                ShowDialog("총 생산 수량이 지시수량보다 많습니다.");
                return;
            }



            // 생산 실적 등록 
            DBHelper helper = new DBHelper("", true);
            try
            {
                string sItemCode       = Convert.ToString(grid1.ActiveRow.Cells["ITEMCODE"].Value);
                string sWorkcenterCode = Convert.ToString(grid1.ActiveRow.Cells["WORKCENTERCODE"].Value);
                string sOrderNo        = Convert.ToString(grid1.ActiveRow.Cells["ORDERNO"].Value);
                string sUnitCode       = Convert.ToString(grid1.ActiveRow.Cells["UNITCODE"].Value);

                // 생산 실적 등록을 위한 프로시져. (불량 관리) 
                helper.ExecuteNoneQuery("00PP_ActureOutput_I5", CommandType.StoredProcedure
                                       , helper.CreateParameter("@PLANTCODE",      sPlantCode)
                                       , helper.CreateParameter("@WORKCENTERCODE", sWorkcenterCode)
                                       , helper.CreateParameter("@ORDERNO",        sOrderNo)
                                       , helper.CreateParameter("@ITEMCODE",       sItemCode)
                                       , helper.CreateParameter("@UNITCODE",       sUnitCode)
                                       , helper.CreateParameter("@PRODQTY",        dTextProdQty)
                                       , helper.CreateParameter("@BADQTY",         dTextErrorQty)
                                       , helper.CreateParameter("@MATLOTNO",       sMatLotNo)
                                       );

                if (helper.RSCODE != "S")
                {
                    throw new Exception($"생산실적 등록 중 오류가 발생하였습니다. \r\n{helper.RSMSG}");
                }
                helper.Commit();
                ShowDialog("정상적으로 등록을 완료하였습니다.");
                SetWorkcenter(); // 정상 완료 후 재조회.
                txtProdQty_H.Text = "";
                txtBadQty_H.Text  = "";

                if (helper.RSMSG != "")
                {
                    PrintFertBarcode(helper.RSMSG);
                }
            }
            catch (Exception ex)
            {
                helper.Rollback();
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                helper.Close();
            }
        }
        #endregion

        #region < 7. 작업지시 종료 >
        private void btnWorkOrderClose_H_Click(object sender, EventArgs e)
        {
            if (grid1.Rows.Count == 0) return;
            if (grid1.ActiveRow == null) return;

            // 투입 된 원자재 LOT 가 있을 경우 종료 되지 않도록 막기.
            if (Convert.ToString(grid1.ActiveRow.Cells["MATLOTNO"].Value) != "")
            {
                ShowDialog("투입된 원자재 LOT 가 존재 합니다. 취소 후 진행하세요.");
                return;
            }

            // 가동 중 일경우 비가동 등록 후 종료 
            if (Convert.ToString(grid1.ActiveRow.Cells["WORKSTATUSCODE"].Value) == "R")
            {
                ShowDialog("비가동 등록 후 종료 하세요.");
                return;
            }

            // 작업지시 종료 시작.
            DBHelper helper = new DBHelper("", true);
            try
            {
                string sWorkcenterCode = Convert.ToString(grid1.ActiveRow.Cells["WORKCENTERCODE"].Value);
                string sOrderNo = Convert.ToString(grid1.ActiveRow.Cells["ORDERNO"].Value);


                helper.ExecuteNoneQuery("00PP_ActureOutput_I6", CommandType.StoredProcedure
                                       , helper.CreateParameter("@PLANTCODE",      sPlantCode)
                                       , helper.CreateParameter("@WORKCENTERCODE", sWorkcenterCode)
                                       , helper.CreateParameter("@ORDERNO",        sOrderNo)
                                       );
                if (helper.RSCODE != "S")
                {
                    throw new Exception($"작업지시 종료중 오류가 발생하였습니다. \r\n{helper.RSMSG}");
                }
                helper.Commit();
                ShowDialog("정상적으로 등록을 완료하였습니다.");
                SetWorkcenter(); // 정상 완료 후 재조회.
            }
            catch (Exception ex)
            {
                helper.Rollback();
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                helper.Close();
            }

        }
        #endregion


        private void PrintFertBarcode(string LotNo)
        {
            DBHelper helper = new DBHelper(false);
            try
            { 
                string sLotNo     = LotNo;

                DataTable rtnDtTemp = helper.FillTable("PP_StockPP_S2", CommandType.StoredProcedure
                                    , helper.CreateParameter("PLANTCODE", sPlantCode)
                                    , helper.CreateParameter("LOTNO", sLotNo )
                                    );
                if (rtnDtTemp.Rows.Count == 0)
                {
                    this.ShowDialog("바코드 정보를 조회 할 내용이 없습니다.", DialogForm.DialogType.OK);
                    return;
                }
                // 바코드 디자인 선언
             //   Report_LotBacodeFERT sReport_LotBacode = new Report_LotBacodeFERT();
                Telerik.Reporting.ReportBook reportBook = new Telerik.Reporting.ReportBook();
             //   sReport_LotBacode.DataSource = rtnDtTemp;
             //   reportBook.Reports.Add(sReport_LotBacode);

                ReportViewer BARCODE_REPORTBOOK = new ReportViewer(reportBook, 1);
                BARCODE_REPORTBOOK.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowDialog(ex.ToString(), DialogForm.DialogType.OK);
            }
            finally
            {
                helper.Close();
            }
        }
               

        /**************************************
         * 고장 버튼/수리 완료 버튼을 눌렀을 때
         * ************************************/
        
        private void btnErrorReg_H_Click(object sender, EventArgs e)
        {
            if (grid1.Rows.Count == 0) return;
            if (grid1.ActiveRow == null) return;
                        
            if (btnErrorReg_H.Tag == "F")
            {
                if (ShowDialog("고장등록을 하시겠습니까?") == DialogResult.Cancel) return; 

                // 고장 등록 
                DBHelper helper = new DBHelper("", true);
                try
                {
                    string sOrderNo        = Convert.ToString(grid1.ActiveRow.Cells["ORDERNO"].Value);
                    string sWorkcenterCode = Convert.ToString(grid1.ActiveRow.Cells["WORKCENTERCODE"].Value);
                    string sItemCode       = Convert.ToString(grid1.ActiveRow.Cells["ITEMCODE"].Value);

                    // 고장 등록 프로시져.

                    helper.ExecuteNoneQuery("TEAM04_PP_ERRORREG", CommandType.StoredProcedure
                                           , helper.CreateParameter("@PLANTCODE", sPlantCode)
                                           , helper.CreateParameter("@WORKCENTERCODE", sWorkcenterCode)
                                           , helper.CreateParameter("@ORDERNO", sOrderNo)
                                           , helper.CreateParameter("@ITEMCODE", sItemCode)
                                           );


                    helper.Commit();
                    ShowDialog("정상적으로 처리되었습니다.");
                    SetWorkcenter(); // 정상 완료 후 재조회.

                }
                catch (Exception ex)
                {
                    helper.Rollback();
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    helper.Close();
                }
            }
            else if (btnErrorReg_H.Tag != "F")
            {
                if (ShowDialog("수리완료처리 하시겠습니까?") == DialogResult.Cancel) return;

                // 수리완료처리
                DBHelper helper = new DBHelper("", true);
                try
                {
                    string sOrderNo = Convert.ToString(grid1.ActiveRow.Cells["ORDERNO"].Value);
                    string sWorkcenterCode = Convert.ToString(grid1.ActiveRow.Cells["WORKCENTERCODE"].Value);
                    string sItemCode = Convert.ToString(grid1.ActiveRow.Cells["ITEMCODE"].Value);

                    // 수리완료처리 프로시져.

                    helper.ExecuteNoneQuery("TEAM04_PP_REPAIRCOMPLETE", CommandType.StoredProcedure
                                           , helper.CreateParameter("@PLANTCODE", sPlantCode)
                                           , helper.CreateParameter("@WORKCENTERCODE", sWorkcenterCode)
                                           , helper.CreateParameter("@ORDERNO", sOrderNo)
                                           , helper.CreateParameter("@ITEMCODE", sItemCode)
                                           );


                    helper.Commit();
                    ShowDialog("정상적으로 처리되었습니다.");
                    SetWorkcenter(); // 정상 완료 후 재조회.


                }
                catch (Exception ex)
                {
                    helper.Rollback();
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    helper.Close();
                }
            }
            
            
        }
    }
}
