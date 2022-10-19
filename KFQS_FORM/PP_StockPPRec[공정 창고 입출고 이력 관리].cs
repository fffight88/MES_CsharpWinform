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
 * From ID   : PP_StockPPRec
 * Form Name : 공정 재고 입출고 이력 관리
 * date      : 2022-07-05
 * Mkaer     : 동상현
 * 
 * 수정사항  : 
 * *************************************************************************/
namespace KFQS_Form
{
    public partial class PP_StockPPRec : DC00_WinForm.BaseMDIChildForm
    {

        #region <  MEMBER AREA  >
        DataTable dtTemp          = new DataTable();
        UltraGridUtil _GridUtil   = new UltraGridUtil();   // 그리드 셋팅 클래스.
        private string sPlantCode = LoginInfo.PlantCode;   // 로그인 한 공장 정보.
        #endregion

        public PP_StockPPRec()
        {
            InitializeComponent();
        }

        private void PP_StockPPRec_Load(object sender, EventArgs e)
        {
            // Grid 셋팅. 
            _GridUtil.InitializeGrid(this.grid1);

            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",   "공장",         GridColDataType_emu.VarChar,    120,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "MATLOTNO",    "LOTNO",        GridColDataType_emu.VarChar,    160,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",    "품목코드",     GridColDataType_emu.VarChar,    140,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME",    "품목명",       GridColDataType_emu.VarChar,    150,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "INOUTDATE",   "입/출일자",    GridColDataType_emu.VarChar,    120,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "WHCODE",      "창고",         GridColDataType_emu.VarChar,    120,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "INOUTCODE",   "입출유형",     GridColDataType_emu.VarChar,    150,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "INOUTFLAG",   "입출구분",     GridColDataType_emu.VarChar,    130,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "INOUTQTY",    "입출수량",     GridColDataType_emu.Double,     130,   HAlign.Right, true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "BASEUNIT",    "단위",         GridColDataType_emu.VarChar,    130,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKER",       "생성자",       GridColDataType_emu.VarChar,    130,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",    "생성일시",     GridColDataType_emu.DateTime24, 160,   HAlign.Left,  true,  false);

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
            UltraGridUtil.SetComboUltraGrid(grid1, "BASEUNIT", dtTemp);


            // 창고 코드 
            dtTemp = Common.StandardCODE("WHCODE");
            UltraGridUtil.SetComboUltraGrid(grid1, "WHCODE", dtTemp);

            /* 입출 유형 TB_Standard
            10  원자재 입고
            15  원자재 입고 취소
            20  자재 생산 출고
            25  자재 생산 출고 취소
            30  재공 투입
            35  재공 투입 취소
            40  생산실적등록 차감
            45  생산입고
            50  제품창고 입고
            55  제품창고 입고 취소
            57  상차 등록
            60  제품 출고
            65  제품 출고 취소 */

            dtTemp = Common.StandardCODE("INOUTCODE");
            UltraGridUtil.SetComboUltraGrid(grid1, "INOUTCODE", dtTemp);

            // 입출 구분
            dtTemp = Common.StandardCODE("INOUTFLAG");
            UltraGridUtil.SetComboUltraGrid(grid1, "INOUTFLAG", dtTemp);

            // 품목 타입
            // FERT :  완제품,    ROH : 원자재,    HALB : 반제품,   OM : 외주가공품.
            dtTemp = Common.Get_ItemCode(new string[] { "FERT", "ROH" });
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

                dtTemp = helper.FillTable("00PP_StockPPRec_S", CommandType.StoredProcedure
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
        #endregion
    }
}
