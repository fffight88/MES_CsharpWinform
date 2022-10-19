using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win;
using DC00_assm;
using DC00_WinForm;

namespace DC_POPUP
{
    public partial class POP_WORKORDER : BasePopupForm
    {
        #region <  MEMBER AREA  >
        DataTable      dtTemp   = new DataTable();
        UltraGridUtil _GridUtil = new UltraGridUtil();   // 그리드 셋팅 클래스.
        private string sPlantCode = LoginInfo.PlantCode; // 로그인 한 공장 정보.

        private string sWorkcenterCode = string.Empty; // 팝업에서 사용할 작업장 코드 변수
        private string sWorkcenterName = string.Empty; // 팝업에서 사용할 작업장 명 변수
        #endregion


        public POP_WORKORDER()
        {
            InitializeComponent();
        }
        public POP_WORKORDER(string Workcentercode,  string WorkcenterName)
        {
            InitializeComponent();
            sWorkcenterCode = Workcentercode;
            sWorkcenterName = WorkcenterName;
        }

        private void POP_WORKORDER_Load(object sender, EventArgs e)
        {
            // Grid 셋팅. 
            _GridUtil.InitializeGrid(this.grid1);

            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",      "공장",            GridColDataType_emu.VarChar, 120,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERCODE", "작업장코드",      GridColDataType_emu.VarChar, 120,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERNAME", "작업장명",        GridColDataType_emu.VarChar, 130,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "ORDERDATE",      "지시일자",        GridColDataType_emu.VarChar, 160,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "ORDERNO",        "작업지시번호",    GridColDataType_emu.VarChar, 160,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",       "품번",            GridColDataType_emu.VarChar, 150,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME",       "품명",            GridColDataType_emu.VarChar, 150,   HAlign.Left,  true,  false);
            _GridUtil.InitColumnUltraGrid(grid1, "ORDERQTY",       "작업지시수량",    GridColDataType_emu.Double,  120,   HAlign.Right, true,  false);

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

            
            // 공장 로그인 정보로 자동 셋팅.
            cboPlantCode_H.Value = sPlantCode;

            // 생산실적 등록 화면에서 선택한 작업장 표시
            txtWorkcenterCode_H.Text = sWorkcenterCode;
            txtWorkcenterName_H.Text = sWorkcenterName;

            Search();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            // 1. 작업장 조회 

            // 툴바의 조회 버튼 클릭.
            DBHelper helper = new DBHelper(false);

            try
            {
                string sPlantCode_      = Convert.ToString(cboPlantCode_H.Value);         // 공장
                string sWorkcwentercode = txtWorkcenterCode_H.Text;                       // 작업장 코드
                string sWorkcenterName  = txtWorkcenterName_H.Text;                       // 작업장 명
                string sStartDate       = string.Format("{0:yyyy-MM-dd}", dtpStart.Value); // 지시 시작일자
                string sEndDate         = string.Format("{0:yyyy-MM-dd}", dtpEnd.Value);   // 지시 종료일자

                dtTemp = helper.FillTable("USP_OrderNo_POP", CommandType.StoredProcedure
                                          , helper.CreateParameter("@PLANTCODE",      sPlantCode_)
                                          , helper.CreateParameter("@WORKCENTERCODE", sWorkcwentercode)
                                          , helper.CreateParameter("@WORKCENTERNAME", sWorkcenterName)
                                          , helper.CreateParameter("@STARTDATE",      sStartDate)
                                          , helper.CreateParameter("@ENDDATE",        sEndDate)
                                          );

                // 받아온 데이터 그리드 매핑
                if (dtTemp.Rows.Count == 0)
                {
                    // 그리드 초기화.
                    _GridUtil.Grid_Clear(grid1);
                    MessageBox.Show("작업장에 내려진 작업지시가 없습니다.");
                    return;
                }

                grid1.DataSource = dtTemp;
                grid1.DataBinds(dtTemp);

            }
            catch (Exception ex)
            {
            }
            finally
            {
                helper.Close();
            }
        }

        private void grid1_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if (grid1.Rows.Count == 0) return;
            this.Tag = Convert.ToString(grid1.ActiveRow.Cells["ORDERNO"].Value);
            this.Close();
        }
    }
}
