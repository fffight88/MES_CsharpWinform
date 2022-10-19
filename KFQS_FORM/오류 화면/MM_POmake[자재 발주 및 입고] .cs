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

/*****************************************************************
 * Form ID   : MM_POmake
 * Form Name : 원자재 발주 및 입고
 * Date      : 2022-07-04
 * Maker     : 장준혁
 * 
 * 수정사항  :
 * ***************************************************************/

namespace KFQS_Form
{
    public partial class MM_POmake : DC00_WinForm.BaseMDIChildForm
    {

        #region < MEMBER AREA > 
        DataTable dtTemp = new DataTable();
        UltraGridUtil _Gridutil = new UltraGridUtil();   // 그리드 세팅 클래스
        private string sPlantCode = LoginInfo.PlantCode;   // 로그인한 공장 정보

        #endregion

        public MM_POmake()
        {
            InitializeComponent();
        }

        private void MM_POmake_Load(object sender, EventArgs e)
        {
            // Grid 세팅
            // 자재 발주 부분
            _Gridutil.InitializeGrid(this.grid1);
            _Gridutil.InitColumnUltraGrid(grid1, "PLANTCODE", "공장", GridColDataType_emu.VarChar, 130, HAlign.Left, true, false);
            _Gridutil.InitColumnUltraGrid(grid1, "PONO", "발주번호", GridColDataType_emu.VarChar, 130, HAlign.Left, true, false);
            _Gridutil.InitColumnUltraGrid(grid1, "ITEMCODE", "품목 코드", GridColDataType_emu.VarChar, 130, HAlign.Left, true, false);
            _Gridutil.InitColumnUltraGrid(grid1, "PODATE", "발주일자", GridColDataType_emu.YearMonthDay, 130, HAlign.Left, true, false);
            _Gridutil.InitColumnUltraGrid(grid1, "POQTY", "발주수량", GridColDataType_emu.Double, 130, HAlign.Right, true, false);
            _Gridutil.InitColumnUltraGrid(grid1, "UNITCODE", "단위", GridColDataType_emu.VarChar, 130, HAlign.Left, true, false);
            _Gridutil.InitColumnUltraGrid(grid1, "CUSTCODE", "거래처(협력)", GridColDataType_emu.VarChar, 130, HAlign.Left, true, false);

            // 원자재 입고 부분
            _Gridutil.InitColumnUltraGrid(grid1, "CHK", "입고", GridColDataType_emu.CheckBox, 100, HAlign.Center, true, true);
            _Gridutil.InitColumnUltraGrid(grid1, "INQTY", "입고수량", GridColDataType_emu.Double, 130, HAlign.Right, true, true);
            _Gridutil.InitColumnUltraGrid(grid1, "LOTNO", "LOT 번호", GridColDataType_emu.VarChar, 150, HAlign.Left, true, false);
            _Gridutil.InitColumnUltraGrid(grid1, "INDATE", "입고일자", GridColDataType_emu.YearMonthDay, 130, HAlign.Left, true, false);
            _Gridutil.InitColumnUltraGrid(grid1, "INWORKER", "입고자", GridColDataType_emu.VarChar, 130, HAlign.Left, true, false);


            _Gridutil.InitColumnUltraGrid(grid1, "MAKER", "생성자", GridColDataType_emu.VarChar, 130, HAlign.Left, true, false);
            _Gridutil.InitColumnUltraGrid(grid1, "MAKEDATE", "생성일시", GridColDataType_emu.DateTime24, 130, HAlign.Left, true, false);
            _Gridutil.InitColumnUltraGrid(grid1, "EDITOR", "수정자", GridColDataType_emu.VarChar, 130, HAlign.Left, true, false);
            _Gridutil.InitColumnUltraGrid(grid1, "EDITDATE", "수정일시", GridColDataType_emu.DateTime24, 130, HAlign.Left, true, false);

            _Gridutil.SetInitUltraGridBind(grid1);

            // 콥보박스 세팅 
            #region < 콤보박스 세팅 설명 >
            // 공장

            // 공장에 대한 시스템 공통 코드 내역을 DB에서 가져온다 > dtTemp라는 데이터  테이블에 담는다
            dtTemp = Common.StandardCODE("PLANTCODE");

            // 콤보박스에 받아온 데이터를 밸류 멤버와 디스플레이멤버로 표현한다
            Common.FillComboboxMaster(this.cboPlantCode_H, dtTemp);

            // 그리드의 컬럼에 받아온 데이터를 콤보박스 형식으로 세팅한다
            UltraGridUtil.SetComboUltraGrid(grid1, "PLANTCODE", dtTemp);

            #endregion

            // 단위
            dtTemp = Common.StandardCODE("UNITCODE");
            UltraGridUtil.SetComboUltraGrid(grid1, "UNITCODE", dtTemp);

            // 거래처코드 
            dtTemp = Common.GET_Cust_Code("V"); // V : 협력업체, C : 고객사
            Common.FillComboboxMaster(cboCust_H, dtTemp);
            UltraGridUtil.SetComboUltraGrid(grid1, "CUSTCODE", dtTemp);

            // 품목 타입
            // FERT : 완제품, ROH : 원자재, HALB : 반제품, OM : 외주가공품
            dtTemp = Common.Get_ItemCode(new string[] { "ROH" });
            Common.FillComboboxMaster(cboItemCode_H, dtTemp);
            UltraGridUtil.SetComboUltraGrid(grid1, "ITEMCODE", dtTemp);

            // 공장 로그인 정보로 자동 세팅
            cboPlantCode_H.Value = sPlantCode;

            // 발주 시작일자 설정
            dtpStart_H.Value = string.Format("{0:yyyy-MM-01}", DateTime.Now);


        }

        #region < ToolBar Area >
        public override void DoInquire()
        {
            // 툴바의 조회 버튼 클릭
            DBHelper helper = new DBHelper(false);

            try
            {
                string sPlantCode = Convert.ToString(cboPlantCode_H.Value);
                string sWorkCenterCode = Convert.ToString(cboCust_H.Value);
                string sOrderNo = Convert.ToString(txtOrderNo_H.Text);
                //string sOrderCloseFlag = Convert.ToString(cboOrderCloseFlag_H.Value);
                string sItemCode = Convert.ToString(cboItemCode_H.Value);

                dtTemp = helper.FillTable("20MM_POmake_S", CommandType.StoredProcedure
                                                              , helper.CreateParameter("@PLANTCODE", sPlantCode)
                                                              , helper.CreateParameter("@WORKCENTERCODE", sWorkCenterCode)
                                                              , helper.CreateParameter("@ORDERNO", sOrderNo)
                                                              , helper.CreateParameter("@ITEMCODE", sItemCode));


                // 받아온 데이터 그리드 매핑
                if (dtTemp.Rows.Count == 0)
                {
                    // 그리드 초기화
                    _Gridutil.Grid_Clear(grid1);
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

            //그리드에 새로운 행을 하나 생성한다
            this.grid1.InsertRow();

            // 생성된 행에 디폴트 데이터를 입력한다
            this.grid1.ActiveRow.Cells["PLANTCODE"].Value = sPlantCode;

            // 수정을 하지 못하도록 컬럼제어
            // 신규 추가시에는 생산 계획 편성 데이터만 등록할 수 있도록 함
            grid1.ActiveRow.Cells["PLANNO"].Activation = Activation.Disabled;
            grid1.ActiveRow.Cells["CHK"].Activation = Activation.Disabled;
            grid1.ActiveRow.Cells["WORKCENTERCODE"].Activation = Activation.Disabled;
            grid1.ActiveRow.Cells["ORDERDATE"].Activation = Activation.Disabled;
            grid1.ActiveRow.Cells["ORDERWORKER"].Activation = Activation.Disabled;
            grid1.ActiveRow.Cells["ORDERCLOSEFLAG"].Activation = Activation.Disabled;
            grid1.ActiveRow.Cells["ORDERCLOSEDATE"].Activation = Activation.Disabled;

            grid1.ActiveRow.Cells["MAKEDATE"].Activation = Activation.Disabled;
            grid1.ActiveRow.Cells["MAKER"].Activation = Activation.Disabled;
            grid1.ActiveRow.Cells["EDITOR"].Activation = Activation.Disabled;
            grid1.ActiveRow.Cells["EDITDATE"].Activation = Activation.Disabled;
        }

        public override void DoDelete()
        {

            if (Convert.ToString(grid1.ActiveRow.Cells["ORDERNO"].Value) != "")
            {
                ShowDialog("작업지시 확정 내역을 취소 후 진행하세요");
                return;
            }

            base.DoDelete();
            this.grid1.DeleteRow();
        }

        public override void DoSave()
        {
            // 1. 그리드에 있는 갱신 이력이 있는 행들만 호출
            DataTable dt = grid1.chkChange();
            if (dt == null) return;

            // 데이터베이스 오픈 및 트랜잭션 설정
            DBHelper helper = new DBHelper("", true);

            try
            {
                // 해당 내역 저장 ㄱ?
                if (ShowDialog("해당 내역을 저장하시겠습니까?") == DialogResult.Cancel)
                {
                    return;
                }

                //갱신 이력이 담긴 데이터 테이블에서 한 행씩 뽑아와서 처리한다
                foreach (DataRow dr in dt.Rows)
                {
                    // 추출한 행의 상태가
                    switch (dr.RowState)
                    {
                        // 삭제된 상태이면
                        case DataRowState.Deleted:
                            dr.RejectChanges();


                            helper.ExecuteNoneQuery("20MM_POmake_D", CommandType.StoredProcedure, helper.CreateParameter("@PLANTCODE", Convert.ToString(dr["PLANTCODE"]), DbType.String, ParameterDirection.Input)
                                                                                                     , helper.CreateParameter("@PLANNO", Convert.ToString(dr["PLANNO"]), DbType.String, ParameterDirection.Input));
                            break;


                        // 추가된 상태이면
                        case DataRowState.Added:
                            // 품목과 수량을 입력하였는지 확인
                            string sMessage = string.Empty;
                            if (Convert.ToString(dr["ITEMCODE"]) == "") sMessage += "품목";
                            if (Convert.ToString(dr["PLANQTY"]) == "") sMessage += "수량";
                            if (sMessage != "") throw new Exception($"{sMessage} 을 입력하지 않았습니다.");

                            helper.ExecuteNoneQuery("20MM_POmake_I", CommandType.StoredProcedure, helper.CreateParameter("@PLANTCODE", Convert.ToString(dr["PLANTCODE"]))
                                                                                                     , helper.CreateParameter("@ITEMCODE", Convert.ToString(dr["ITEMCODE"]))
                                                                                                     , helper.CreateParameter("@PLANQTY", Convert.ToString(dr["PLANQTY"]).Replace(",", ""))
                                                                                                     , helper.CreateParameter("@UNITCODE", Convert.ToString(dr["UNITCODE"]))
                                                                                                     , helper.CreateParameter("@MAKER", LoginInfo.UserID));
                            break;


                        // 수정된 상태이면
                        case DataRowState.Modified:
                            // 작업지시 확정 및 취소
                            if (Convert.ToString(dr["WORKCENTERCODE"]) == "") throw new Exception("작업장을 선택하지않았습니다.");

                            // 체크박스에 체크가 되어있으면 확정 로직으로 간주
                            // 체크박으에 체크가 안 되어있으면 
                            string sOrderFlag = "N";
                            if (Convert.ToString(dr["CHK"]) == "1") sOrderFlag = "Y";

                            helper.ExecuteNoneQuery("20MM_POmake_U", CommandType.StoredProcedure, helper.CreateParameter("@PLANTCODE", Convert.ToString(dr["PLANTCODE"]))
                                                                                                     , helper.CreateParameter("@PLANNO", Convert.ToString(dr["PLANNO"]))
                                                                                                     , helper.CreateParameter("@ORDERFLAG", sOrderFlag)
                                                                                                     , helper.CreateParameter("@WORKCENTERCODE", Convert.ToString(dr["WORKCENTERCODE"]))
                                                                                                     , helper.CreateParameter("@EDITOR", LoginInfo.UserID));


                            break;
                    }
                    if (helper.RSCODE != "S")
                    {
                        throw new Exception(helper.RSMSG);
                    }
                }


                // 데이터베이스 등록 완료
                helper.Commit();
                ShowDialog("정상적으로 등록하였습니다.");
            }
            catch (Exception ex)
            {
                // 데이터 등록 복구
                helper.Rollback();
                ShowDialog(ex.ToString());
            }
            finally
            {
                // 데이터베이스 닫기
                helper.Rollback();
            }
        }

    }
        #endregion
    }


