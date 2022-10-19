#region < HEADER AREA >
// *---------------------------------------------------------------------------------------------*
//   Form ID      : PP_Repair
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
    public partial class PP_Repair : DC00_WinForm.BaseMDIChildForm
    {

        #region < MEMBER AREA >
        DataTable rtnDtTemp = new DataTable(); // 
        UltraGridUtil _GridUtil = new UltraGridUtil();  //그리드 객체 생성
        Common _Common = new Common();
        string plantCode = LoginInfo.PlantCode;

        #endregion


        #region < CONSTRUCTOR >
        public PP_Repair()
        {
            InitializeComponent();
        }
        #endregion


        #region < FORM EVENTS >
        private void PP_Repair_Load(object sender, EventArgs e)
        {
            #region ▶ GRID ◀
            _GridUtil.InitializeGrid(this.grid1);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE", "공장", GridColDataType_emu.VarChar, 170, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERCODE", "작업장", GridColDataType_emu.VarChar, 170, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ORDERNO", "작업지시번호", GridColDataType_emu.VarChar, 200, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.SetInitUltraGridBind(grid1);


            _GridUtil.InitializeGrid(this.grid2);
            _GridUtil.InitColumnUltraGrid(grid2, "RECDATE", "고장일자", GridColDataType_emu.VarChar, 160, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "ERRORSEQ", "고장순번", GridColDataType_emu.VarChar, 160, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "ERRORDT", "고장일시", GridColDataType_emu.DateTime24, 190, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "STARTRPDT", "수리시작일시", GridColDataType_emu.DateTime24, 190, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "ENDRPDT", "수리완료일시", GridColDataType_emu.DateTime24, 190, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "REMARKDETAIL", "상세고장사유", GridColDataType_emu.VarChar, 190, Infragistics.Win.HAlign.Left, true, true);

            _GridUtil.SetInitUltraGridBind(grid2);

            /*  _GridUtil.InitializeGrid(this.grid3);
              _GridUtil.InitColumnUltraGrid(grid3, "상세지표",  "1",         GridColDataType_emu.VarChar,        55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY1",   "1",            GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY2",   "2",            GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY3",   "3",            GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY4",   "4",            GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY5",   "5",            GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY6",   "6",            GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY7",   "7",            GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY8",   "8",            GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY9",   "9",            GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY10",  "10",           GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY11",  "11",           GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY12",  "12",           GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY13",  "13",           GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY14",  "14",           GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY15",  "15",           GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY16",  "16",           GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY17",  "17",           GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY18",  "18",           GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY19",  "19",           GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY21",  "21",           GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY22",  "22",           GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY23",  "23",           GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY24",  "24",           GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY25",  "25",           GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY26",  "26",           GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY27",  "27",           GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY28",  "28",           GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY29",  "29",           GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY30",  "30",           GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false);
              _GridUtil.InitColumnUltraGrid(grid3, "DAY31",  "31",           GridColDataType_emu.VarChar,         55,  Infragistics.Win.HAlign.Left, true, false); */






            #endregion

            #region ▶ COMBOBOX ◀

            //      공장

            //      공장에 대한 시스템 공통 코드 내역을 DB에서 가져온다. => dtTemp에 담는다. 

            DataTable dtTemp = new DataTable();
            dtTemp = Common.StandardCODE("PLANTCODE");

            // 콤보박스에 받아온 데이터를 밸류 멤버와 디스플레이멤버로 표현한다
            Common.FillComboboxMaster(this.cboPlantCode_H, dtTemp);

            // 그리드의 컬럼에 받아온 데이터를 콤보박스 형식으로 세팅한다
            UltraGridUtil.SetComboUltraGrid(grid1, "PLANTCODE", dtTemp);


            // 작업장
            dtTemp = Common.GET_Workcenter_Code();
            Common.FillComboboxMaster(this.cboWorkCenter_H, dtTemp);
            UltraGridUtil.SetComboUltraGrid(grid1, "WORKCENTERCODE", dtTemp);
            #endregion

            #region ▶ POP-UP ◀
            BizTextBoxManager btbManager = new BizTextBoxManager();

            #endregion

            #region ▶ ENTER-MOVE ◀
            cboPlantCode_H.Value = plantCode;
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
                string sPlantCode = Convert.ToString(cboPlantCode_H.Value);
                string sWorkCenterCode = Convert.ToString(cboWorkCenter_H.Value);
                string sOrderNo = Convert.ToString(txtOrderNo.Text);
                string sYearMonth = Convert.ToString(cboYearMonth_H.Value);

                rtnDtTemp = helper.FillTable("TEAM04_PP_Repair_S1", CommandType.StoredProcedure
                                    , helper.CreateParameter("PLANTCODE", sPlantCode)
                                    , helper.CreateParameter("WORKCENTERCODE", sWorkCenterCode)
                                    , helper.CreateParameter("ORDERNO", sOrderNo)
                                    , helper.CreateParameter("YEARMONTH", sYearMonth)

                                    );

                // 받아온 데이터 그리드 매핑
                if (rtnDtTemp.Rows.Count == 0)
                {
                    // 그리드 초기화.
                    _GridUtil.Grid_Clear(grid1);
                    ShowDialog("조회할 데이터가 없습니다.", DialogForm.DialogType.OK);
                    return;
                }

                grid1.DataSource = rtnDtTemp;
                grid1.DataBinds(rtnDtTemp); this.ClosePrgForm();
                this.grid1.DataSource = rtnDtTemp;
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

        #endregion


        public override void DoSave()
        {

            //  1.  그리드에 있는 갱신 이력이 있는 행들만 추출


            DataTable dt = grid2.chkChange();
            if (dt == null) return;

            //      데이터 베이스 오픈 및 트랜잭션 설정
            DBHelper helper = new DBHelper("", true);

            try
            {
                //  해당 내역을 저장하시겠습니까?
                if (ShowDialog("해당 내역을 저장하시겠습니까?") == System.Windows.Forms.DialogResult.Cancel) return;

                //  갱신 이력이 담긴 데이터 테이블에서 한 행씩 뽑아와서 처리한다.
                foreach (DataRow dr in dt.Rows)
                {
                    //  추출한 행의 상태가
                    switch (dr.RowState)
                    {


                        //   추가된 상태이면,
                        case DataRowState.Added:



                        //  수정된 상태이면
                        case DataRowState.Modified:
                            //  작업지시 확정 및 취소
                            string sWorkcenterCode = Convert.ToString(grid1.ActiveRow.Cells["WORKCENTERCODE"].Value);
                            string sPlantCode      = Convert.ToString(grid1.ActiveRow.Cells["PLANTCODE"].Value);
                            string sRcDate       = Convert.ToString(grid2.ActiveRow.Cells["RECDATE"].Value);
                            string sErrorSeq     = Convert.ToString(grid2.ActiveRow.Cells["ERRORSEQ"].Value);
                            string sREMARKDETAIL = Convert.ToString(grid2.ActiveRow.Cells["REMARKDETAIL"].Value);


                            if (sWorkcenterCode == "")
                                throw new Exception("작업장을 선택하지 않았습니다.");


                            helper.ExecuteNoneQuery("TEAM04_PP_Repair_U1", CommandType.StoredProcedure
                                                    , helper.CreateParameter("@PLANTCODE",      sPlantCode)
                                                    , helper.CreateParameter("@WORKCENTERCODE", sWorkcenterCode)
                                                    , helper.CreateParameter("@RECDATE",        sRcDate)
                                                    , helper.CreateParameter("@ERRORSEQ",       sErrorSeq)
                                                    , helper.CreateParameter("@REMARKDETAIL",   sREMARKDETAIL));

                            break;
                    }

                    if (helper.RSCODE != "S")
                    {
                        throw new Exception(helper.RSMSG);
                    }
                }

                //  데이터 베이스 등록 완료
                helper.Commit();
                //
                ShowDialog("정상적으로 등록되었습니다.");

                DoInquire();    //  완료된 내역 재조회






            }
            catch (Exception ex)
            {
                //  데이터 등록 복구
                helper.Rollback();
                ShowDialog(ex.ToString());
            }
            finally
            {
                //  데이터 베이스 닫기
                helper.Close();
            }
        }

        private void grid1_AfterRowActivate(object sender, EventArgs e)
        {
            DBHelper helper = new DBHelper(false);
            try
            {
                _GridUtil.Grid_Clear(grid2);
                _GridUtil.Grid_Clear(grid3);


                string sPlantCode = Convert.ToString(this.grid1.ActiveRow.Cells["PLANTCODE"].Value);
                string sWorkcenterCode = Convert.ToString(this.grid1.ActiveRow.Cells["WORKCENTERCODE"].Value);
                string sOrderNo = Convert.ToString(this.grid1.ActiveRow.Cells["ORDERNO"].Value);
                string sYM = Convert.ToString(cboYearMonth_H.Text);


                DataTable rtnDtTemp2 = helper.FillTable("TEAM04_PP_Repair_S2", CommandType.StoredProcedure
                                    , helper.CreateParameter("@PLANTCODE", sPlantCode)
                                    , helper.CreateParameter("@WORKCENTERCODE", sWorkcenterCode)
                                    //                     , helper.CreateParameter("@ORDERNO",        sOrderNo)
                                    //                     , helper.CreateParameter("@YEARMONTH",      sYM)
                                    );
                this.grid2.DataSource = rtnDtTemp2;
                grid2.DataBinds(rtnDtTemp2);

                DataTable rtnDtTemp3 = helper.FillTable("TEAM04_PP_Repair_S3", CommandType.StoredProcedure
                                    , helper.CreateParameter("@PLANTCODE", sPlantCode)
                                    , helper.CreateParameter("@WORKCENTERCODE", sWorkcenterCode)
                                    //                     , helper.CreateParameter("@ORDERNO", sOrderNo)
                                    , helper.CreateParameter("@YEARMONTH", sYM)
                                    );


                this.grid3.DataSource = rtnDtTemp3;

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




