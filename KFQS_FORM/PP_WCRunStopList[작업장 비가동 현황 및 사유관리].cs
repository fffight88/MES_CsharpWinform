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



/**************************************************************************
 * From ID   : PP_WCRunStopList
 * Form Name : 작업장 비가동 현황 및 사유 관리.
 * date      : 2022-07-09
 * Mkaer     : 동상현
 * 
 * 수정사항  : 
 * *************************************************************************/
namespace KFQS_Form
{
    public partial class PP_WCRunStopList : DC00_WinForm.BaseMDIChildForm
    {

        #region <  MEMBER AREA  >
        DataTable dtTemp          = new DataTable();
        UltraGridUtil _GridUtil   = new UltraGridUtil();   // 그리드 셋팅 클래스.
        private string sPlantCode = LoginInfo.PlantCode;   // 로그인 한 공장 정보.
        #endregion

        public PP_WCRunStopList()
        {
            InitializeComponent();
        }

        private void PP_WCRunStopList_Load(object sender, EventArgs e)
        {
            // Grid 셋팅. 
            _GridUtil.InitializeGrid(this.grid1);

            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",      "공장",              GridColDataType_emu.VarChar, 120, HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "RSSEQ",          "작업장지시별 순번", GridColDataType_emu.VarChar, 160, HAlign.Left, false, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERCODE", "작업장코드",        GridColDataType_emu.VarChar, 150, HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERNAME", "작업장명",          GridColDataType_emu.VarChar, 150, HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ORDERNO",        "작업지시번호",      GridColDataType_emu.VarChar, 150, HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",       "품목코드",          GridColDataType_emu.VarChar, 140, HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME",       "품목명",            GridColDataType_emu.VarChar, 150, HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKER",         "작업자",            GridColDataType_emu.VarChar, 150, HAlign.Left, true, false);

            _GridUtil.InitColumnUltraGrid(grid1, "STATUS",         "가동/비가동",       GridColDataType_emu.VarChar, 120, HAlign.Left, false, false);
            _GridUtil.InitColumnUltraGrid(grid1, "STATUSNAME",     "가동/비가동",       GridColDataType_emu.VarChar, 120, HAlign.Left, true, false);

            _GridUtil.InitColumnUltraGrid(grid1, "RSSTARTDATE", "시작일시",             GridColDataType_emu.VarChar, 120, HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "RSENDDATE",   "종료일시",             GridColDataType_emu.VarChar, 150, HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "TIMEDIFF",    "총소요시간(분)",       GridColDataType_emu.Double,  130, HAlign.Right, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "PRODQTY",     "양품수량",             GridColDataType_emu.Double,  130, HAlign.Right, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "BADQTY",      "불량수량",             GridColDataType_emu.Double,  130, HAlign.Right, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "REMARK",      "사유",                 GridColDataType_emu.VarChar, 130, HAlign.Left,  true, true);

            _GridUtil.InitColumnUltraGrid(grid1, "MAKER",    "생성자",                  GridColDataType_emu.VarChar,    130, HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE", "생성일시",                GridColDataType_emu.DateTime24, 160, HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "EDITOR",   "수정자",                  GridColDataType_emu.VarChar,    130, HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "EDITDATE", "수정일시",                GridColDataType_emu.DateTime24, 160, HAlign.Left, true, false);

            _GridUtil.SetInitUltraGridBind(grid1);




            // 그리드 합병.
            this.grid1.DisplayLayout.Override.MergedCellContentArea                     = MergedCellContentArea.VisibleRect;
            this.grid1.DisplayLayout.Bands[0].Columns["PLANTCODE"].MergedCellStyle      = MergedCellStyle.Always;
            this.grid1.DisplayLayout.Bands[0].Columns["WORKCENTERCODE"].MergedCellStyle = MergedCellStyle.Always;
            this.grid1.DisplayLayout.Bands[0].Columns["WORKCENTERNAME"].MergedCellStyle = MergedCellStyle.Always;
            this.grid1.DisplayLayout.Bands[0].Columns["ORDERNO"].MergedCellStyle        = MergedCellStyle.Always;
            this.grid1.DisplayLayout.Bands[0].Columns["ITEMCODE"].MergedCellStyle       = MergedCellStyle.Always;
            this.grid1.DisplayLayout.Bands[0].Columns["ITEMNAME"].MergedCellStyle       = MergedCellStyle.Always;
            this.grid1.DisplayLayout.Bands[0].Columns["WORKER"].MergedCellStyle         = MergedCellStyle.Always;

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

            dtTemp = Common.GET_Workcenter_Code();
            Common.FillComboboxMaster(cboWorkcenter_H, dtTemp);

            // 가동 / 비가동 정보 콤보박스 셋팅
            UltraGridUtil.SetComboUltraGrid(grid1, "REMARK", GetStopList());


            // 공장 로그인 정보로 자동 셋팅.
            cboPlantCode_H.Value = sPlantCode;

            dtpStart_H.Value = string.Format("{0:yyyy-MM-01}", DateTime.Now);
            
        }

        private DataTable GetStopList()
        {
            DataTable TempTable = new DataTable();
            // 비가동 항목 리스트 조회
            DBHelper helper = new DBHelper(false);
            try
            {
                string Sql = string.Empty;
                Sql =  "     SELECT STOPCODE                         AS CODE_ID ";
                Sql += "          ,'[' + STOPCODE + '] ' + STOPNAME  AS CODE_NAME ";
                Sql += "      FROM TB_StopListMaster WITH(NOLOCK) ";
                Sql += "     WHERE USEFLAG = 'Y' ";
                TempTable = helper.FillTable(Sql, CommandType.Text);


                return TempTable;
            }
            catch (Exception ex)
            {
                return TempTable;
            }
            finally
            {
                helper.Close();
            }
        }

        #region < ToolBar Area >
        public override void DoInquire()
        {
            // 툴바의 조회 버튼 클릭.
            DBHelper helper = new DBHelper(false);

            try
            {
                string sPlantCode_       = Convert.ToString(cboPlantCode_H.Value);            // 공장
                string sWorkcenterCode   = Convert.ToString(cboWorkcenter_H.Value);           // 작업장 코드  
                string sStartdate        = string.Format("{0:yyyy-MM-dd}", dtpStart_H.Value); // 가동/비가동 시작일시
                string sEnddate          = string.Format("{0:yyyy-MM-dd}", dtpEnd_H.Value);   // 가동/비가동 종료일시

                dtTemp = helper.FillTable("00PP_WCRunStopList_S", CommandType.StoredProcedure
                                          , helper.CreateParameter("@PLANTCODE",      sPlantCode_)
                                          , helper.CreateParameter("@WORKCENTERCODE", sWorkcenterCode)
                                          , helper.CreateParameter("@STARTDATE",      sStartdate)
                                          , helper.CreateParameter("@ENDDATE",        sEnddate)
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


        public override void DoSave()
        {
            this.grid1.UpdateData();
            DataTable dt = grid1.chkChange();
            if (dt == null)
                return;
            DBHelper helper = new DBHelper("", true);
            try
            {
                if (this.ShowDialog("비가동 사유를 등록 하시겠습니까 ?") == System.Windows.Forms.DialogResult.Cancel)
                {
                    CancelProcess = true;
                    return;
                }

                base.DoSave();

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

                            if (Convert.ToString(drRow["STATUS"]) == "R") continue;

                            helper.ExecuteNoneQuery("00PP_WCTRunStopList_U", CommandType.StoredProcedure
                                                  , helper.CreateParameter("@PLANTCODE",      Convert.ToString(drRow["PLANTCODE"]) )
                                                  , helper.CreateParameter("@WORKCENTERCODE", Convert.ToString(drRow["WORKCENTERCODE"]) )
                                                  , helper.CreateParameter("@ORDERNO",        Convert.ToString(drRow["ORDERNO"]) )
                                                  , helper.CreateParameter("@RSSEQ",          Convert.ToInt32(drRow["RSSEQ"]) )
                                                  , helper.CreateParameter("@REMARK",         Convert.ToString(drRow["REMARK"]) )
                                                  , helper.CreateParameter("@EDITOR",         LoginInfo.UserID )
                                                  );


                            #endregion
                            break;
                    }
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

        private void grid1_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            CustomMergedCellEvalutor CM1 = new CustomMergedCellEvalutor("ORDERNO", "ITEMCODE");
            e.Layout.Bands[0].Columns["ITEMCODE"].MergedCellEvaluator = CM1;
            e.Layout.Bands[0].Columns["ITEMNAME"].MergedCellEvaluator = CM1;
            e.Layout.Bands[0].Columns["WORKER"].MergedCellEvaluator   = CM1;
        }
    }
}
