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


namespace KFQS_Form
{
    public partial class PP_WCStatus : DC00_WinForm.BaseMDIChildForm
    {
        public PP_WCStatus()
        {
            InitializeComponent();
        }

        #region <  MEMBER AREA  >
        DataTable dtTemp = new DataTable();
        UltraGridUtil _GridUtil = new UltraGridUtil();   // 그리드 셋팅 클래스.
        private string sPlantCode = LoginInfo.PlantCode;   // 로그인 한 공장 정보.
        #endregion

        private void PP_WCStatus_Load(object sender, EventArgs e)
        {
            // Grid 셋팅. 
            _GridUtil.InitializeGrid(this.grid1);

            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",      "공장",           GridColDataType_emu.VarChar,    130, HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERNAME", "작업장명",       GridColDataType_emu.VarChar,    130, HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERCODE", "작업장코드",     GridColDataType_emu.VarChar,    130, HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ORDERNO",        "작업지시번호",   GridColDataType_emu.VarChar,    150, HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "RECDATE",        "고장일자",       GridColDataType_emu.VarChar,    100, HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ERRORSEQ",       "고장순번",       GridColDataType_emu.VarChar,     80, HAlign.Right, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ERRORDT",        "고장등록시간",   GridColDataType_emu.DateTime24, 130, HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ERRORWORKER",    "고장등록작업자", GridColDataType_emu.VarChar,    130, HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "STARTRPDT",      "수리시작시간",   GridColDataType_emu.DateTime24, 130, HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "STARTRPWORKER",  "수리시작등록자", GridColDataType_emu.DateTime24, 130, HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "REMARKDETAIL",   "상세고장사유",   GridColDataType_emu.VarChar,    150, HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "EDITDATE",       "수정일시",       GridColDataType_emu.DateTime24, 140, HAlign.Left,  false, false);

            _GridUtil.SetInitUltraGridBind(grid1);



            // 콤보박스 셋팅.


            #region < 콤보박스 셋팅 설명 >
            // 공장
            // 공장에 대한 시스템 공통 코드 내역을 DB 에서 가져온다. => dtTemp 데이터 테이블에 담는다. 
            dtTemp = Common.StandardCODE("PLANTCODE");
            // 콤보박스 에 받아온 데이터를 밸류멤버와 디스플레이멤버로 표현한다.  
            Common.FillComboboxMaster(cboPlantCode_H, dtTemp);
            // 그리드의 컬럼에 받아온 데이터를 콤보박스 형식으로 셋팅한다.
            UltraGridUtil.SetComboUltraGrid(grid1, "PLANTCODE", dtTemp);
            #endregion

            // 작업장
            dtTemp = Common.GET_Workcenter_Code();
            Common.FillComboboxMaster(cboWorkcenter_H, dtTemp);



            // 공장 로그인 정보로 자동 셋팅.
            cboPlantCode_H.Value = sPlantCode;


        }
        public override void DoInquire()
        {
            // 툴바의 조회 버튼 클릭.
            DBHelper helper = new DBHelper(false);

            try
            {
                string sPlantCode_      = Convert.ToString(cboPlantCode_H.Value);                // 공장
                string sWorkcenterCode_ = Convert.ToString(cboWorkcenter_H.Value);

                dtTemp = helper.FillTable("TEAM04_PP_WCStatus_S", CommandType.StoredProcedure
                                          , helper.CreateParameter("@PLANTCODE",      sPlantCode_)
                                          , helper.CreateParameter("@WORKCENTERCODE", sWorkcenterCode_)
                                          );

                // 받아온 데이터 그리드 매핑
                if (dtTemp.Rows.Count == 0)

                {
                    // 그리드 초기화.
                    _GridUtil.Grid_Clear(grid1);
                    ShowDialog("조회할 고장 및 수리내역이 없습니다.", DialogForm.DialogType.OK);
                    return;
                }

                grid1.DataSource = dtTemp;

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
        

        private void btnRepairStart_Click(object sender, EventArgs e)
        {


            if (grid1.Rows.Count == 0) return;
            if (grid1.ActiveRow == null) return;



            DBHelper helper = new DBHelper("", true);
            try
            {
                string sPlantCode      = Convert.ToString(grid1.ActiveRow.Cells["PLANTCODE"].Value);
                string sWorkcentercode = Convert.ToString(grid1.ActiveRow.Cells["WORKCENTERCODE"].Value);
                



                helper.ExecuteNoneQuery("TEAM04_PP_WCStatus_I", CommandType.StoredProcedure
                                          , helper.CreateParameter("@PLANTCODE", sPlantCode)
                                          , helper.CreateParameter("@WORKCENTERCODE", sWorkcentercode)
                                          , helper.CreateParameter("@EDITOR", LoginInfo.UserID)
                                                );
                if (helper.RSCODE != "S")
                {
                    throw new Exception($"작업자 도착 시간 등록중 오류가 발생하였습니다. \r\n{helper.RSMSG}");
                }
                helper.Commit();
                ShowDialog("정상적으로 등록을 완료하였습니다.");
                DoInquire(); // 정상 완료후 재조회.
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

        private void btnFailureR_Click(object sender, EventArgs e)
        {
            if (grid1.Rows.Count == 0) return;
            if (grid1.ActiveRow == null) return;

            string sFailureR = Convert.ToString(txtFailureR_H.Text);



            // 고장 상세 사유를 입력하지 않은경우
            if (sFailureR == null)
            {
                ShowDialog("고장 사유를 입력 하세요. ");
                return;
            }

            // 고장 상세 사유 등록
            DBHelper helper = new DBHelper("", true);
            try
            {
                if (ShowDialog("고장 상세 사유를 등록 하시겠습니까?") == DialogResult.Cancel)
                {

                    return;
                }
                grid1.ActiveRow.Cells["REMARKDETAIL"].Value = sFailureR;

                string sPlantCode = Convert.ToString(grid1.ActiveRow.Cells["PLANTCODE"].Value);
                string sWorkcentercode = Convert.ToString(grid1.ActiveRow.Cells["WORKCENTERCODE"].Value);
                string sStartRPWorker = Convert.ToString(grid1.ActiveRow.Cells["STARTRPWORKER"].Value);
                string sRemarkdetail = txtFailureR_H.Text;
                string sRecDate = Convert.ToString(grid1.ActiveRow.Cells["RECDATE"].Value);
                string sRecSeq = Convert.ToString(grid1.ActiveRow.Cells["ERRORSEQ"].Value);

                // 상세 고장 사유등록을 위한 프로시져.
                helper.ExecuteNoneQuery("TEAM04_PP_WCStatus_I3", CommandType.StoredProcedure
                                                , helper.CreateParameter("@PLANTCODE", sPlantCode)
                                                , helper.CreateParameter("@WORKCENTERCODE", sWorkcentercode)
                                                , helper.CreateParameter("@STARTRPWORKER", sStartRPWorker)
                                                , helper.CreateParameter("@REMARKDETAIL", sRemarkdetail)
                                                , helper.CreateParameter("@RECDATE", sRecDate)
                                                , helper.CreateParameter("@ERRORSEQ", sRecSeq)
                                                );



                if (helper.RSCODE != "S")
                {
                    throw new Exception($"상세 고장사유 등록 중 오류가 발생하였습니다. \r\n{helper.RSMSG}");
                }
                helper.Commit();
                ShowDialog("정상적으로 등록을 완료하였습니다.");
                DoInquire(); // 정상 완료후 재조회.

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

        private void grid1_AfterRowActivate(object sender, EventArgs e)
        {
            txtFailureR_H.Text = Convert.ToString(grid1.ActiveRow.Cells["REMARKDETAIL"].Value);
        }
    }

  
}

    
   

