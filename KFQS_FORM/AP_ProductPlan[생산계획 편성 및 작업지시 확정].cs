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
 * From ID   : AP_ProductPlan
 * Form Name : 생산계획 편성및 작업지시 확정
 * date      : 2022-07-01
 * Mkaer     : 동상현
 * 
 * 수정사항  : 
 * *************************************************************************/
namespace KFQS_Form
{
    public partial class AP_ProductPlan : DC00_WinForm.BaseMDIChildForm
    {

        #region <  MEMBER AREA  >
        DataTable dtTemp          = new DataTable();
        UltraGridUtil _GridUtil   = new UltraGridUtil();   // 그리드 셋팅 클래스.
        private string sPlantCode = LoginInfo.PlantCode;   // 로그인 한 공장 정보.
        #endregion

        public AP_ProductPlan()
        {
            InitializeComponent();
        }

        private void AP_ProductPlan_Load(object sender, EventArgs e)
        {
            // Grid 셋팅. 
            _GridUtil.InitializeGrid(this.grid1);

            // 생산 계획 등록 부분. 
            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",   "공장",         GridColDataType_emu.VarChar, 130,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANNO",      "생산계획번호", GridColDataType_emu.VarChar, 130,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",    "품목코드",     GridColDataType_emu.VarChar, 130,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANQTY",     "계획수량",     GridColDataType_emu.Double,  130,   HAlign.Right, true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE",    "단위",         GridColDataType_emu.VarChar, 130,   HAlign.Left,  true,  false);
            
            // 작업지시 확정 부분.
            _GridUtil.InitColumnUltraGrid(grid1, "CHK",            "확정",         GridColDataType_emu.CheckBox,   100,   HAlign.Center, true,  true);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERCODE", "작업장",       GridColDataType_emu.VarChar,    130,   HAlign.Left,   true,  true);
            _GridUtil.InitColumnUltraGrid(grid1, "ORDERNO",        "작업지시번호", GridColDataType_emu.VarChar,    130,   HAlign.Left,   true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "ORDERDATE",      "확정일시",     GridColDataType_emu.DateTime24, 130,   HAlign.Left,   true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "ORDERWORKER",    "확정자",       GridColDataType_emu.VarChar,    130,   HAlign.Left,   true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "ORDERCLOSEFLAG", "지시종료여부", GridColDataType_emu.VarChar,    130,   HAlign.Left,   true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "FIRSTSTARTDATE",  "지시시작일시", GridColDataType_emu.DateTime24, 130,   HAlign.Left,   true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "ORDERCLOSEDATE",  "지시종료일시", GridColDataType_emu.DateTime24, 130,   HAlign.Left,   true,  false);

            _GridUtil.InitColumnUltraGrid(grid1, "MAKER",          "생성자",       GridColDataType_emu.VarChar,    130,   HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",       "생성일시",     GridColDataType_emu.DateTime24, 130,   HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "EDITOR",         "수정자",       GridColDataType_emu.VarChar,    130,   HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "EDITDATE",       "수정일시",     GridColDataType_emu.DateTime24, 130,   HAlign.Left, true, false);

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


            // 지시 종료 여부 
            dtTemp = Common.StandardCODE("YESNO");
            Common.FillComboboxMaster(cboOrderCloseFlag_H,dtTemp);
            UltraGridUtil.SetComboUltraGrid(grid1, "ORDERCLOSEFLAG", dtTemp);


            // 작업장.
            dtTemp = Common.GET_Workcenter_Code();
            Common.FillComboboxMaster(cboWorkcenter_H, dtTemp);
            UltraGridUtil.SetComboUltraGrid(grid1, "WORKCENTERCODE", dtTemp);


            // 품목 타입
            // FERT :  완제품,    ROH : 원자재,    HALB : 반제품,   OM : 외주가공품.
            dtTemp = Common.Get_ItemCode(new string[] { "FERT" });
            Common.FillComboboxMaster(cboItemCode_H, dtTemp);
            UltraGridUtil.SetComboUltraGrid(grid1, "ITEMCODE", dtTemp);


            // 공장 로그인 정보로 자동 셋팅.
            cboPlantCode_H.Value = sPlantCode;
        }

        #region < ToolBar Area >
        public override void DoInquire()
        {
            // 툴바의 조회 버튼 클릭.
            DBHelper helper = new DBHelper(false);

            try
            {
                string sPlantCode_     = Convert.ToString(cboPlantCode_H.Value);      // 공장
                string sWorkcenterCode = Convert.ToString(cboWorkcenter_H.Value);     // 작업장.
                string sOrderNo        = Convert.ToString(txtOrderNo_H.Text);         // 작업지시 번호 
                string sOrderCloseFlag = Convert.ToString(cboOrderCloseFlag_H.Value); // 지시 종료 여부
                string sItemCode       = Convert.ToString(cboItemCode_H.Value);       // 품목코드


                dtTemp = helper.FillTable("00AP_ProductPlan_S", CommandType.StoredProcedure
                                          , helper.CreateParameter("@PLANTCODE",        sPlantCode_)
                                          , helper.CreateParameter("@WORKCENTERCODE",   sWorkcenterCode)
                                          , helper.CreateParameter("@ORDERNO",          sOrderNo)
                                          , helper.CreateParameter("@ORDERCLOSEFLAG",   sOrderCloseFlag)
                                          , helper.CreateParameter("@ITEMCODE",         sItemCode)
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



        public override void DoNew()
        {
            base.DoNew();

            // 그리드에 새로운 행을 하나 생성한다. 
            this.grid1.InsertRow();
            
            // 생성된 행에 디폴트 데이터를 입력한다. 
            this.grid1.ActiveRow.Cells["PLANTCODE"].Value = sPlantCode;

            // 수정을 하지 못하도록 컬럼 제어. 
            // 신규 추가시에는 생산 계획 편성 데이터만 등록 할 수 있도록 함.
            grid1.ActiveRow.Cells["PLANNO"].Activation         = Activation.Disabled; // 생산계획번호
            grid1.ActiveRow.Cells["CHK"].Activation            = Activation.Disabled; // 확정 체크박스
            grid1.ActiveRow.Cells["WORKCENTERCODE"].Activation = Activation.Disabled; // 작업장
            grid1.ActiveRow.Cells["ORDERDATE"].Activation      = Activation.Disabled; // 작업지시일자
            grid1.ActiveRow.Cells["ORDERWORKER"].Activation    = Activation.Disabled; // 작업지시확정자
            grid1.ActiveRow.Cells["ORDERCLOSEFLAG"].Activation = Activation.Disabled; // 작업지시종료여부
            grid1.ActiveRow.Cells["ORDERCLOSEDATE"].Activation = Activation.Disabled; // 작업지시종료일시

            grid1.ActiveRow.Cells["MAKEDATE"].Activation = Activation.Disabled;
            grid1.ActiveRow.Cells["MAKER"].Activation    = Activation.Disabled;
            grid1.ActiveRow.Cells["EDITDATE"].Activation = Activation.Disabled;
            grid1.ActiveRow.Cells["EDITOR"].Activation   = Activation.Disabled;

        }


        public override void DoDelete()
        { 
            if (Convert.ToString(grid1.ActiveRow.Cells["ORDERNO"].Value) != "")
            {
                ShowDialog("작업지시 확정 내역을 취소후 진행 하세요.");
                return;
            }

            base.DoDelete();
            this.grid1.DeleteRow();
        }


        public override void DoSave()
        {
            // 1. 그리드에 있는 갱신 이력이 있는 행들만 추출.
            DataTable dt = grid1.chkChange();
            if (dt == null) return;


            // 데이터베이스 오픈 및 트랜잭션 설정.
            DBHelper helper = new DBHelper("", true);

            try
            {
                // 해당 내역을 저장 하시겠습니까 ? 
                if (ShowDialog("해당 내역을 저장 하시겠습니까?") == DialogResult.Cancel)
                {
                    return;
                }

                // 갱신 이력이 담긴 데이터테이블 에서 한 행씩 뽑아와서 처리한다. 
                foreach (DataRow dr in dt.Rows)
                {
                    // 추출한 행의 상태가
                    switch (dr.RowState)
                    {
                        // 삭제 된 상태 이면.
                        case DataRowState.Deleted:
                            dr.RejectChanges();

                            helper.ExecuteNoneQuery("00AP_ProductPlan_D", CommandType.StoredProcedure,
                                                    helper.CreateParameter("@PLANTCODE", dr["PLANTCODE"]),
                                                    helper.CreateParameter("@PLANNO",    dr["PLANNO"]));


                            break;
                        // 추가 된 상태 이면.
                        case DataRowState.Added:
                            // 품목과 수량을 입력하였는지 확인.
                            string sMessage = string.Empty;
                            if (Convert.ToString(dr["ITEMCODE"]) == "") sMessage += "품목 ";
                            if (Convert.ToString(dr["PLANQTY"])  == "") sMessage += "수량 ";
                            if (sMessage != "") throw new Exception($"{sMessage} 을 입력하지 않았습니다.");

                            helper.ExecuteNoneQuery("00AP_ProductPlan_I", CommandType.StoredProcedure
                                                    , helper.CreateParameter("@PLANTCODE", Convert.ToString(dr["PLANTCODE"]))
                                                    , helper.CreateParameter("@ITEMCODE",  Convert.ToString(dr["ITEMCODE"]))
                                                    , helper.CreateParameter("@PLANQTY",   Convert.ToString(dr["PLANQTY"]).Replace(",",""))
                                                    , helper.CreateParameter("@UNITCODE",  Convert.ToString(dr["UNITCODE"]))
                                                    , helper.CreateParameter("@MAKER",     LoginInfo.UserID));
                            break;
                        // 수정 된 상태이면
                        case DataRowState.Modified:
                            // 작업지시 확정 및 취소.
                            if (Convert.ToString(dr["WORKCENTERCODE"]) == "") throw new Exception("작업장을 선택하지 않았습니다.");

                            // 체크박스에 체크가 되어 있으면 확정 로직으로 간주
                            // 체크박스에 체크가 해제 되어 있으면 확정 취소 로직으로 간주.
                            string sOrderFlag = "N";
                            if (Convert.ToString(dr["CHK"]) == "1") sOrderFlag = "Y";
                            if (Convert.ToString(dr["CHK"]) == "0") sOrderFlag = "N";

                            helper.ExecuteNoneQuery("00AP_ProductPlan_U", CommandType.StoredProcedure
                                                    , helper.CreateParameter("@PLANTCODE",      Convert.ToString(dr["PLANTCODE"]))
                                                    , helper.CreateParameter("@PLANNO",         Convert.ToString(dr["PLANNO"]))
                                                    , helper.CreateParameter("@ORDERFLAG",      sOrderFlag)
                                                    , helper.CreateParameter("@WORKCENTERCODE", Convert.ToString(dr["WORKCENTERCODE"]))
                                                    , helper.CreateParameter("@EDITOR",         LoginInfo.UserID));


                            break;
                    }
                    if (helper.RSCODE != "S")
                    {
                        throw new Exception(helper.RSMSG);
                    }
                }

                // 데이터 베이스 등록 완료.

                helper.Commit();
                ShowDialog("정상적으로 등록 하였습니다.");
                DoInquire(); // 완료된 내역 재조회.
            }
            catch (Exception ex)
            {
                // 데이터 등록 복구
                helper.Rollback();
                ShowDialog(ex.ToString());
            }
            finally
            {
                // 데이터 베이스 닫기
                helper.Close();
            }
        }
        #endregion

    }
}
