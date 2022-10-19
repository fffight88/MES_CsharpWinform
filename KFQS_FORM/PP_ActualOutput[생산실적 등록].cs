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

/**********************************************
* FORM ID      : PP_ActualOutput
* FORM NAME    : 생산실적 등록
* DATE         : 2022-07-06
* MAKER        : 백두산
* 
* 수정사항     :
* ********************************************/

namespace KFQS_Form
{
    
    public partial class PP_ActualOutput : DC00_WinForm.BaseMDIChildForm
    {
        #region <MEMBER AREA>

        DataTable dtTemp            = new DataTable();
        UltraGridUtil GridUtil      = new UltraGridUtil();       // 그리드 세팅 클래스
        private string sPlantCode   = LoginInfo.PlantCode;       // 로그인한 공장 정보

        #endregion



        public PP_ActualOutput()
        {
            InitializeComponent();
        }

        private void PP_ActualOutput_Load(object sender, EventArgs e)
        {
            // Grid 세팅

            
            GridUtil.InitializeGrid(this.grid1);

            GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE"     , "공장"            , GridColDataType_emu.VarChar     , 120, HAlign.Left  , false, false);
            GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERCODE", "작업장코드"      , GridColDataType_emu.VarChar     , 120, HAlign.Left  , false, false);
            GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERNAME", "작업장명"        , GridColDataType_emu.VarChar     , 130, HAlign.Left  , true , false);
            GridUtil.InitColumnUltraGrid(grid1, "ORDERNO"       , "작업지시번호"    , GridColDataType_emu.VarChar     , 220, HAlign.Left  , true , false);
            GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE"      , "품번"            , GridColDataType_emu.VarChar     , 150, HAlign.Left  , true , false);
            GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME"      , "품명"            , GridColDataType_emu.VarChar     , 150, HAlign.Left  , true , false);
            GridUtil.InitColumnUltraGrid(grid1, "ORDERQTY"      , "작업지시수량"    , GridColDataType_emu.Double      , 120, HAlign.Right , true , false);
            GridUtil.InitColumnUltraGrid(grid1, "PRODQTY"       , "양품수량"        , GridColDataType_emu.Double      , 120, HAlign.Right , true , false);
            GridUtil.InitColumnUltraGrid(grid1, "BADQTY"        , "불량수량"        , GridColDataType_emu.Double      , 120, HAlign.Right , true , false);
            GridUtil.InitColumnUltraGrid(grid1, "UNITCODE"      , "단위"            , GridColDataType_emu.VarChar     , 130, HAlign.Left  , true , false);
            GridUtil.InitColumnUltraGrid(grid1, "WORKSTATUSCODE", "가동비가동코드"  , GridColDataType_emu.VarChar     , 130, HAlign.Left  , false, false);
            GridUtil.InitColumnUltraGrid(grid1, "WORKSTATUS"    , "상태"            , GridColDataType_emu.VarChar     , 120, HAlign.Left  , true , false);
            GridUtil.InitColumnUltraGrid(grid1, "MATLOTNO"      , "투입LOT"         , GridColDataType_emu.VarChar     , 270, HAlign.Left  , true , false);
            GridUtil.InitColumnUltraGrid(grid1, "COMPONENTQTY"  , "투입잔량"        , GridColDataType_emu.Double      , 130, HAlign.Right , true , false);
            GridUtil.InitColumnUltraGrid(grid1, "WORKER"        , "작업자ID"        , GridColDataType_emu.VarChar     , 130, HAlign.Left  , false, false);
            GridUtil.InitColumnUltraGrid(grid1, "WORKERNAME"    , "작업자명"        , GridColDataType_emu.VarChar     , 130, HAlign.Left  , true , false);
            GridUtil.InitColumnUltraGrid(grid1, "STARTDATE"     , "지시시작일시"    , GridColDataType_emu.DateTime24  , 130, HAlign.Left  , true , false);
            GridUtil.InitColumnUltraGrid(grid1, "ENDDATE"       , "지시종료일시"    , GridColDataType_emu.DateTime24  , 160, HAlign.Left  , true , false);


            GridUtil.SetInitUltraGridBind(grid1);  // 엔터키를 안쳐도 데이터를 입력한 것으로 인식한다.

            // 콤보박스 세팅

            #region < 콤보박스 세팅 설명 >

            // 공장

            // 공장에 대한 시스템 공통 코드 내역을 DB에서 가져온다. => dtTemp 데이터 테이블에 담는다.
            dtTemp = Common.StandardCODE("PLANTCODE");

            // 콤보박스에 받아온 데이터를 밸류멤버와 디스플레이멤버로 표현
            Common.FillComboboxMaster(this.cboPlantCode_H, dtTemp);

            // 그리드의 컬럼에 받아온 데이터를 콤보박스 형식으로 세팅
            UltraGridUtil.SetComboUltraGrid(grid1, "PLANTCODE", dtTemp);

            #endregion 

            // 단위
            dtTemp = Common.StandardCODE("UNITCODE");
            UltraGridUtil.SetComboUltraGrid(grid1, "UNITCODE", dtTemp);

            // 작업장 마스터 콤보박스
            dtTemp = Common.GET_Workcenter_Code();
            Common.FillComboboxMaster(cboWorkCenterCode_H, dtTemp);

            BizTextBoxManager bizTextPopUp = new BizTextBoxManager();
            bizTextPopUp.PopUpAdd(txtWorkerID_H, txtWorkerName_H,"WORKER_MASTER");

            // 공장 로그인 정보로 자동 세팅
            cboPlantCode_H.Value = sPlantCode;

        }
        

        
        #region < 작업장 조회 ( 현재 상태 및 작업지시 내역 포함 ) >
        public override void DoInquire()
        {
            SetWorkCenter();
        }
        
        private void SetWorkCenter()
        {
            // 1. 작업장 조회
            
            // 툴바의 조회버튼 클릭
            DBHelper helper = new DBHelper(false);

            try
            {
                string sPlantCode_      = Convert.ToString(cboPlantCode_H.Value);              // 공장          
                string sWorkCenterCode  = Convert.ToString(cboWorkCenterCode_H.Value);         // 품목코드

                dtTemp = helper.FillTable("09PP_ActualOutput_S", CommandType.StoredProcedure
                                          , helper.CreateParameter("@PLANTCODE"     , sPlantCode_)
                                          , helper.CreateParameter("@WORKCENTERCODE", sWorkCenterCode)
                                          );

                // 받아온 데이터 그리드 매핑
                if (dtTemp.Rows.Count == 0)
                {
                    // 그리드 초기화
                    GridUtil.Grid_Clear(grid1);
                    ShowDialog("조회할 테이터가 없습니다.", DialogForm.DialogType.OK);
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

        private void cboWorkCenterCode_H_ValueChanged(object sender, EventArgs e)
        {
            // 작업장 콤보박스 변경시 해당 작업장 내역만 조회하도록 설정
            SetWorkCenter();
        }


        #region < 2. 작업자 등록 >

        private void btnWorkerReg_Click(object sender, EventArgs e)
        {
            // 1. 작업장 선택여부 확인
            if (grid1.Rows.Count == 0) return;

            if (grid1.ActiveRow == null)
            {
                ShowDialog("작업장을 선택 후 진행하세요.");
                return;
            }

            string sWorkerID = txtWorkerID_H.Text;  // 작업자 ID
            if (sWorkerID == "")
            {
                ShowDialog("작업자를 선택 후 진행하세요.");
                return;
            }

            string splantCode       = Convert.ToString(grid1.ActiveRow.Cells["PLANTCODE"].Value);           // 공장
            string sWorkCenterCode  = Convert.ToString(grid1.ActiveRow.Cells["WORKCENTERCODE"].Value);      // 작업장

            DBHelper helper = new DBHelper("", true);

            try
            {
                // 선택한 작업장에 작업자를 등록
                helper.ExecuteNoneQuery("09PP_ACTUALOUTPUT_I1", CommandType.StoredProcedure
                                        ,helper.CreateParameter("@PLANTCODE", sPlantCode)
                                        ,helper.CreateParameter("@WORKCENTERCODE", sWorkCenterCode)
                                        ,helper.CreateParameter("@WORKERID", sWorkerID)
                                        );

                if (helper.RSCODE != "S") throw new Exception($"작업자 등록 중 오류가 발생했습니다. \r\n{helper.RSMSG}");
                
                helper.Commit();
                
                ShowDialog("정상적으로 등록을 완료하였습니다.");

                SetWorkCenter();    // 정상 완료 후 재조회

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
        private void btnGetWorkOrder_H_Click(object sender, EventArgs e)
        {
            //  작업지시 선택 팝업 호출
            if (grid1.Rows.Count == 0) return;
            if (grid1.ActiveRow == null)
            {
            
                return;
            }

            // 1. 작업자 등록 여부 확인
            string sWorkerID = Convert.ToString(grid1.ActiveRow.Cells["WORKER"].Value);
            if (sWorkerID == "")
            {
                ShowDialog("현재 작업장에 등록된 작업자 내역이 없습니다. \r\n작업자 등록 후 진행하세요.");
                return;
            }

            // 2. 작업장의 현재 상태가 비가동 상태인지 확인
               string sRunStop = Convert.ToString(grid1.ActiveRow.Cells["WORKSTATUSCODE"].Value);
               if (sRunStop != "S")
               {
                   ShowDialog("현재 작업장이 가동 상태입니다. \r\n비가동 등록 후 진행하세요.");
                   return;
               }

               // 3. 투입된 LOT 정보가 있는지 확인
               string sMatLotNo = Convert.ToString(grid1.ActiveRow.Cells["MATLOTNO"].Value);
               if (sMatLotNo != "")
               {
                   ShowDialog("현재 투입된 원자재 LOT가 있습니다. \r\n투입된 LOT를 투입취소 후 진행하세요.");
                   return;
               }
            
            // 4. 작업지시 선택 팝업 호출
            string sWorkCenterCode = Convert.ToString(grid1.ActiveRow.Cells["WORKCENTERCODE"].Value);
            string sWorkCenterName = Convert.ToString(grid1.ActiveRow.Cells["WORKCENTERNAME"].Value);

            POP_WORKORDER pop_order = new POP_WORKORDER(sWorkCenterCode, sWorkCenterName);
            pop_order.ShowDialog();

            // 작업지시 팝업에서 선택한 작업지시 내역이 없을 때
            if (pop_order.Tag == null) return;

            string sOrderNo = Convert.ToString(pop_order.Tag);

            DBHelper helper = new DBHelper("", true);

            try
            {
                // 선택한 작업장에 작업자를 등록
                helper.ExecuteNoneQuery("09PP_ACTUALOUTPUT_I2", CommandType.StoredProcedure
                                        ,helper.CreateParameter("@PLANTCODE", sPlantCode)
                                        ,helper.CreateParameter("@WORKCENTERCODE", sWorkCenterCode)
                                        ,helper.CreateParameter("@WORKERID", sWorkerID)
                                        ,helper.CreateParameter("@ORDERNO", sOrderNo)
                                        );

                if (helper.RSCODE != "S") throw new Exception($"작업자 등록 중 오류가 발생했습니다. \r\n{helper.RSMSG}");
                
                helper.Commit();
                
                ShowDialog("정상적으로 등록을 완료하였습니다.");

                SetWorkCenter();    // 정상 완료 후 재조회

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
            // 그리드의 행을 선택 후 일어나는 이벤트

            // 1. 등록된 작업자를 작업자 텍스트박스에 표시
            txtWorkerID_H.Text      =   Convert.ToString(grid1.ActiveRow.Cells["WORKER"].Value);
            txtWorkerName_H.Text    =   Convert.ToString(grid1.ActiveRow.Cells["WORKERNAME"].Value);

            // 2. 투입된 원자재 LOT가 있으면 투입 취소 버튼으로 이름 변경
            String sMatLotNo = Convert.ToString(grid1.ActiveRow.Cells["MATLOTNO"].Value);
            if (sMatLotNo == "")
            {
                txtROHLOTNO_H.Text  = "";
                btnROHLOTReg_H.Text = "(4) LOT 투입";
                btnROHLOTReg_H.Tag  = "IN";
            }
            else
            {
                txtROHLOTNO_H.Text  = sMatLotNo;
                btnROHLOTReg_H.Text = "(4) LOT 투입 취소";
                btnROHLOTReg_H.Tag  = "CAN";
            }

            // 3. 현재 작업장 가동/비가동 상태에 따라 R/S 버튼표시
            if (Convert.ToString(grid1.ActiveRow.Cells["WORKSTATUSCODE"].Value) == "R")
            {
                btnRunStop_H.Text = "(5) 비가동";
                btnRunStop_H.Tag  = "S";
            }
            else
            {
                btnRunStop_H.Text = "(5) 가동";
                btnRunStop_H.Tag = "R";
            }



        }

        private void btnROHLOTReg_H_Click(object sender, EventArgs e)
        {
            // 원자재 LOT 투입
            if (grid1.Rows.Count == 0) return;
            if (grid1.ActiveRow == null) return;

            string sOrderNo = Convert.ToString(grid1.ActiveRow.Cells["ORDERNO"].Value);
            if (sOrderNo == "")
            {
                ShowDialog("작업장에 작업지시가 선택되지 않았습니다.");
                return;
            }
            string sWorkerID = Convert.ToString(grid1.ActiveRow.Cells["WORKER"].Value);
            if (sWorkerID == "")
            {
                ShowDialog("작업자를 등록하지 않았습니다. 작업자 선택 후 진행하세요.");
                return;
            }

            // 원자재 LOT 투입 상황
                        
            DBHelper helper = new DBHelper("", true);

            try
            {
                string sItemCode        = Convert.ToString(grid1.ActiveRow.Cells["ITEMCODE"].Value);
                string sLotNo           = txtROHLOTNO_H.Text;
                string sWorkCenterCode  = Convert.ToString(grid1.ActiveRow.Cells["WORKCENTERCODE"].Value);
                string sUnitCode        = Convert.ToString(grid1.ActiveRow.Cells["UNITCODE"].Value);

                // 선택한 작업장에 원자재 LOT 투입/취소
                helper.ExecuteNoneQuery("09PP_ACTUALOUTPUT_I3", CommandType.StoredProcedure
                                        , helper.CreateParameter("@PLANTCODE"     , sPlantCode)
                                        , helper.CreateParameter("@WORKCENTERCODE", sWorkCenterCode)
                                        , helper.CreateParameter("@ITEMCODE"      , sItemCode)
                                        , helper.CreateParameter("@LOTNO"         , sLotNo)
                                        , helper.CreateParameter("@ORDERNO"       , sOrderNo)
                                        , helper.CreateParameter("@UNITCODE"      , sUnitCode)
                                        , helper.CreateParameter("@WORKERID"      , sWorkerID)
                                        , helper.CreateParameter("@INFLAG"        , Convert.ToString(btnROHLOTReg_H.Tag))
                                        );

                if (helper.RSCODE != "S") throw new Exception($"LOT 투입/취소 중 오류가 발생했습니다. \r\n{helper.RSMSG}");

                helper.Commit();
                
                ShowDialog("정상적으로 등록을 완료하였습니다.");

                SetWorkCenter();    // 정상 완료 후 재조회

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

        #region < 5. 가동 / 비가동 클릭 >

        private void btnRunStop_H_Click(object sender, EventArgs e)
        {
            if (grid1.Rows.Count == 0) return;
            if (grid1.ActiveRow == null) return;

            DBHelper helper = new DBHelper("", true);

            try
            {
                string sItemCode        = Convert.ToString(grid1.ActiveRow.Cells["ITEMCODE"].Value);
                string sWorkCenterCode  = Convert.ToString(grid1.ActiveRow.Cells["WORKCENTERCODE"].Value);
                string sOrderNo         = Convert.ToString(grid1.ActiveRow.Cells["ORDERNO"].Value);

                // 선택한 작업장에 원자재 LOT 투입/취소
                helper.ExecuteNoneQuery("09PP_ACTUALOUTPUT_I4", CommandType.StoredProcedure
                                        , helper.CreateParameter("@PLANTCODE"       , sPlantCode)
                                        , helper.CreateParameter("@WORKCENTERCODE"  , sWorkCenterCode)
                                        , helper.CreateParameter("@ITEMCODE"        , sItemCode)
                                        , helper.CreateParameter("@ORDERNO"         , sOrderNo)
                                        , helper.CreateParameter("@STATUS"          , Convert.ToString(btnRunStop_H.Tag))
                                        );

                if (helper.RSCODE != "S") throw new Exception($"작업장 가동/비가동 설정 중 오류가 발생했습니다. \r\n{helper.RSMSG}");

                helper.Commit();
                
                ShowDialog("정상적으로 등록을 완료하였습니다.");

                SetWorkCenter();    // 정상 완료 후 재조회

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

        #region < 6. 생산실적 등록 >

        private void btnProdReg_H_Click(object sender, EventArgs e)
        {
            if (grid1.Rows.Count == 0)   return;
            if (grid1.ActiveRow == null) return;

            // 작업지시 수량 대비 양품 불량 수량 입력 가능여부 체크

            double dProdQty      = 0;  // 누적 양룸수량
            double dErrorQty     = 0;  // 누적 불량수량
            double dTextProdQty  = 0;  // 입력한 양품수량
            double dTextErrorQty = 0;  // 입력한 양품수량
            double dOrderQty     = 0;  // 작업지시 수량

            // 그리드의 누적 양품수량
            string sProdQty = Convert.ToString(grid1.ActiveRow.Cells["PRODQTY"].Value).Replace(",", "");
            Double.TryParse(sProdQty, out dProdQty);

            // 그리드의 누적 불량 수량
            string sBaddQty = Convert.ToString(grid1.ActiveRow.Cells["BADQTY"].Value).Replace(",", "");
            Double.TryParse(sBaddQty, out dErrorQty);

            // 입력한 양품 수량
            string sTextQty = Convert.ToString(txtProdQty_H.Text);
            Double.TryParse(sTextQty, out dTextProdQty);

            // 입력한 불량 수량
            string sTextErrorQty = Convert.ToString(txtBadQty_H.Text);
            Double.TryParse(sTextErrorQty, out dTextErrorQty);

            // 작업지시 수량
            string sOrderQty = Convert.ToString(grid1.ActiveRow.Cells["ORDERQTY"].Value).Replace(",", "");
            Double.TryParse(sOrderQty, out dOrderQty);

            // 투입된 원자재 LOT가 존재하는 확인
            string sMatLotNo = Convert.ToString(grid1.ActiveRow.Cells["MATLOTNO"].Value);
            if (sMatLotNo == "")
            {
                ShowDialog("투입한 원자재 LOT가 없습니다. LOT 투입 후 진행하세요.");
                return;
            }

            // 양품 또는 불량 수량을 하나도 입력하지 않은 경우
            if (dTextProdQty + dTextErrorQty == 0)
            {
                ShowDialog("실적 수량을 입력 하세요.");
                return;
            }

            // 지시 수량보다 양품 수량이 많은 경우
            if (dOrderQty < dProdQty + dTextProdQty)
            {
                ShowDialog("총 생산 수량이 지시수량보다 많습니다.");
                return;
            }

            DBHelper helper = new DBHelper("", true);

            try
            {
                string sItemCode        = Convert.ToString(grid1.ActiveRow.Cells["ITEMCODE"].Value);
                string sWorkCenterCode  = Convert.ToString(grid1.ActiveRow.Cells["WORKCENTERCODE"].Value);
                string sOrderNo         = Convert.ToString(grid1.ActiveRow.Cells["ORDERNO"].Value);
                string sUnitCode        = Convert.ToString(grid1.ActiveRow.Cells["UNITCODE"].Value);

                // 생산 실적 등록 프로시저
                helper.ExecuteNoneQuery("09PP_ACTUALOUTPUT_I5", CommandType.StoredProcedure
                                        , helper.CreateParameter("@PLANTCODE"       , sPlantCode)
                                        , helper.CreateParameter("@WORKCENTERCODE"  , sWorkCenterCode)
                                        , helper.CreateParameter("@ITEMCODE"        , sItemCode)
                                        , helper.CreateParameter("@UNITCODE"        , sUnitCode)
                                        , helper.CreateParameter("@ORDERNO"         , sOrderNo)
                                        , helper.CreateParameter("@PRODQTY"         , dTextProdQty)
                                        , helper.CreateParameter("@BADQTY"          , dTextErrorQty)
                                        , helper.CreateParameter("@MATLOTNO"        , sMatLotNo)
                                        );

                if (helper.RSCODE != "S") throw new Exception($"생산실적 등록 중 오류가 발생했습니다. \r\n{helper.RSMSG}");

                helper.Commit();

                ShowDialog("정상적으로 등록을 완료하였습니다.");

                PrintFertBarcode(helper.RSMSG);

                SetWorkCenter();    // 정상 완료 후 재조회

                txtProdQty_H.Text = "";
                txtBadQty_H.Text  = "";

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


        #region < 작업지시 종료 >

        private void btnWorkOrderClose_H_Click(object sender, EventArgs e)
        {
            if (grid1.Rows.Count == 0) return;
            if (grid1.ActiveRow == null) return;

            // 투입된 원자재 LOT가 있으면 종료되지 않도록 막기
            if (Convert.ToString(grid1.ActiveRow.Cells["MATLOTNO"].Value) != "")
            {
                ShowDialog("투입된 원자재 LOT가 존재합니다. 취소 후 진행하세요.");
                return;
            }

            // 가동중이면 비가동 등록하도록 유도
            if (Convert.ToString(grid1.ActiveRow.Cells["WORKSTATUSCODE"].Value) != "S")
            {
                ShowDialog("비가동 등록 후 종료하세요.");
                return;
            } 


            // 작업지시 종료

            DBHelper helper = new DBHelper("", true);

            try
            {
                string sWorkCenterCode = Convert.ToString(grid1.ActiveRow.Cells["WORKCENTERCODE"].Value);
                string sOrderNo = Convert.ToString(grid1.ActiveRow.Cells["ORDERNO"].Value);

                // 생산 실적 등록 프로시저
                helper.ExecuteNoneQuery("09PP_ACTUALOUTPUT_I6", CommandType.StoredProcedure
                                        , helper.CreateParameter("@PLANTCODE"       , sPlantCode)
                                        , helper.CreateParameter("@WORKCENTERCODE"  , sWorkCenterCode)
                                        , helper.CreateParameter("@ORDERNO"         , sOrderNo)
                                        );

                if (helper.RSCODE != "S") throw new Exception($"작업지시 종료 중 오류가 발생했습니다. \r\n{helper.RSMSG}");

                helper.Commit();

                ShowDialog("정상적으로 등록을 완료하였습니다.");

                SetWorkCenter();    // 정상 완료 후 재조회

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
                string sLotNo = LotNo;

                DataTable rtnDtTemp = helper.FillTable("PP_StockPP_S2", CommandType.StoredProcedure
                                    , helper.CreateParameter("PLANTCODE", sPlantCode)
                                    , helper.CreateParameter("LOTNO", sLotNo)
                                    );
                if (rtnDtTemp.Rows.Count == 0)
                {
                    this.ShowDialog("바코드 정보를 조회 할 내용이 없습니다.", DialogForm.DialogType.OK);
                    return;
                }
                // 바코드 디자인 선언
                Report_LotBacodeFERT sReport_LotBacode = new Report_LotBacodeFERT();
                Telerik.Reporting.ReportBook reportBook = new Telerik.Reporting.ReportBook();
                sReport_LotBacode.DataSource = rtnDtTemp;
                reportBook.Reports.Add(sReport_LotBacode);

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

    }
}
