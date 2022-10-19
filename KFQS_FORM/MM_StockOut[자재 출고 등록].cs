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
 * From ID   : MM_StockOut
 * Form Name : 자재 생산 출고 등록
 * date      : 2022-07-05
 * Mkaer     : 동상현
 * 
 * 수정사항  : 
 * *************************************************************************/
namespace KFQS_Form
{
    public partial class MM_StockOut : DC00_WinForm.BaseMDIChildForm
    {

        #region <  MEMBER AREA  >
        DataTable dtTemp          = new DataTable();
        UltraGridUtil _GridUtil   = new UltraGridUtil();   // 그리드 셋팅 클래스.
        private string sPlantCode = LoginInfo.PlantCode;   // 로그인 한 공장 정보.
        #endregion

        public MM_StockOut()
        {
            InitializeComponent();
        }

        private void MM_StockOut_Load(object sender, EventArgs e)
        {
            // Grid 셋팅. 
            _GridUtil.InitializeGrid(this.grid1);

            _GridUtil.InitColumnUltraGrid(grid1, "CHK",         "선택",         GridColDataType_emu.CheckBox,   80,    HAlign.Center,  true,  true);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",   "공장",         GridColDataType_emu.VarChar,    120,   HAlign.Left,    true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "MATLOTNO",    "LOTNO",        GridColDataType_emu.VarChar,    160,   HAlign.Left,    true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",    "품목코드",     GridColDataType_emu.VarChar,    140,   HAlign.Left,    true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME",    "품목명",       GridColDataType_emu.VarChar,    150,   HAlign.Left,    true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "INDATE",      "입고일자",     GridColDataType_emu.VarChar,    120,   HAlign.Left,    true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "WHCODE",      "창고",         GridColDataType_emu.VarChar,    120,   HAlign.Left,    true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "STOCKQTY",    "재고수량",     GridColDataType_emu.Double,     130,   HAlign.Right,   true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "BASEUNIT",    "단위",         GridColDataType_emu.VarChar,    130,   HAlign.Left,    true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKER",       "생성자",       GridColDataType_emu.VarChar,    130,   HAlign.Left,    true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",    "생성일시",     GridColDataType_emu.DateTime24, 160,   HAlign.Left,    true,  false);

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

            // 창고 코드 
            dtTemp = Common.StandardCODE("WHCODE");
            UltraGridUtil.SetComboUltraGrid(grid1, "WHCODE", dtTemp);

            // 품목 타입
            // FERT :  완제품,    ROH : 원자재,    HALB : 반제품,   OM : 외주가공품.
            dtTemp = Common.Get_ItemCode(new string[] { "ROH" });
            Common.FillComboboxMaster(cboItemCode_H, dtTemp);




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
                string sPlantCode_ = Convert.ToString(cboPlantCode_H.Value);            // 공장
                string sItemCode   = Convert.ToString(cboItemCode_H.Value);             // 품목코드
                string sLotNO      = Convert.ToString(txtLotNO_H.Text);                 // LOTNO
                string sStartdate  = string.Format("{0:yyyy-MM-dd}", dtpStart_H.Value); // 입/출고 시작일시
                string sEnddate    = string.Format("{0:yyyy-MM-dd}", dtpEnd_H.Value);   // 입/출고 종료일시

                dtTemp = helper.FillTable("00MM_StockOut_S", CommandType.StoredProcedure
                                          , helper.CreateParameter("@PLANTCODE",        sPlantCode_)
                                          , helper.CreateParameter("@LOTNO",            sLotNO)
                                          , helper.CreateParameter("@ITEMCODE",         sItemCode)
                                          , helper.CreateParameter("@STARTDATE",        sStartdate)
                                          , helper.CreateParameter("@ENDDATE",          sEnddate)
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


        public override void DoSave()
        {
            base.DoSave();

            // 1. 그리드에서 변경된 내역이 있는 행만 추출.
            DataTable dt = grid1.chkChange();
            if (dt == null) return;

            DBHelper helper = new DBHelper("", true);

            try
            {
                // 변경 내역을 저장 하시겠습니까 ? 
                if (ShowDialog("변경 내역을 저장 하시겠습니까?") == DialogResult.Cancel)
                {
                    return;
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["CHK"]) == "0") continue;

                    helper.ExecuteNoneQuery("00MM_Stockout_I", CommandType.StoredProcedure
                                            , helper.CreateParameter("@PLANTCODE", Convert.ToString(dt.Rows[i]["PLANTCODE"]))
                                            , helper.CreateParameter("@LOTNO",     Convert.ToString(dt.Rows[i]["MATLOTNO"]))
                                            , helper.CreateParameter("@ITEMCODE",  Convert.ToString(dt.Rows[i]["ITEMCODE"]))
                                            , helper.CreateParameter("@QTY",       Convert.ToString(dt.Rows[i]["STOCKQTY"]))
                                            , helper.CreateParameter("@UNITCODE",  Convert.ToString(dt.Rows[i]["UNITCODE"]))
                                            , helper.CreateParameter("@WORKERID",  LoginInfo.UserID)
                                            );

                    if (helper.RSCODE != "S")
                        throw new Exception("생산 출고 등록 중 오류가 발생하였습니다.");
                }
                helper.Commit();
                DoInquire(); // 조회
                ShowDialog("정상적으로 등록 하였습니다.");
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
    }
}
