#region < HEADER AREA >
// *---------------------------------------------------------------------------------------------*
//   Form ID      : WM_TradingManager
//   Form Name    : 제품 출고 현황 및 거래명세서 발행
//   Name Space   : DC_WM
//   Created Date : 2021.05
//   Made By      : 
//   Description  : 
// *---------------------------------------------------------------------------------------------*
#endregion

#region <USING AREA>
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using DC00_assm;
using DC_POPUP;
using DC00_WinForm;
using DC00_Component;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinEditors;
#endregion

namespace KFQS_Form
{
    public partial class WM_TradingManager : DC00_WinForm.BaseMDIChildForm
    {
        #region [ 생성자 ]
        DataTable rtnDtTemp = new DataTable(); // return DataTable 공통
        UltraGridUtil _GridUtil = new UltraGridUtil();  //그리드 객체 생성
        #endregion

        #region [ 선언자 ]
        public WM_TradingManager()
        {
            InitializeComponent();
        }

        #endregion

        #region [ Form Load ]
        private void WM_TradingManager_Load(object sender, EventArgs e)
        {
            #region ▶ GRID ◀
            _GridUtil.InitializeGrid(this.grid1, true, true, false, "", false);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",   "공장",       true, GridColDataType_emu.VarChar,     120, 120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "TRADINGNO",   "명세서번호", true, GridColDataType_emu.VarChar,     200, 120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "TRADINGDATE", "출고일자",   true, GridColDataType_emu.VarChar,     140, 120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "CARNO",       "차량번호",   true, GridColDataType_emu.VarChar,     140, 120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",    "등록일시",   true, GridColDataType_emu.DateTime24,  200, 120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKER",       "등록자",     true, GridColDataType_emu.VarChar,     100, 120, Infragistics.Win.HAlign.Left,  true, false);
            _GridUtil.SetInitUltraGridBind(grid1);
            
            
            _GridUtil.InitializeGrid(this.grid2, true, true, false, "", false);
            _GridUtil.InitColumnUltraGrid(grid2, "PLANTCODE",  "공장",     true, GridColDataType_emu.VarChar,    100, 120, Infragistics.Win.HAlign.Left,  false, false);
            _GridUtil.InitColumnUltraGrid(grid2, "TRADINGNO",  "명세번호", true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left,  false, false);
            _GridUtil.InitColumnUltraGrid(grid2, "TRADINGSEQ", "명세순번", true, GridColDataType_emu.VarChar,     80, 120, Infragistics.Win.HAlign.Center, true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "SHIPNO",     "상차번호", true, GridColDataType_emu.VarChar,    160, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "SHIPSEQ",    "상차순번", true, GridColDataType_emu.VarChar,     80, 120, Infragistics.Win.HAlign.Center, true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "WORKER",     "상차자",   true, GridColDataType_emu.VarChar,    160, 120, Infragistics.Win.HAlign.Left,  false, false);
            _GridUtil.InitColumnUltraGrid(grid2, "WORKERNAME", "상차자",   true, GridColDataType_emu.VarChar,     80, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "CUSTCODE",   "거래처",   true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "CUSTNAME",   "거래처명", true, GridColDataType_emu.VarChar,    100, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "LOTNO",      "LOTNO",    true, GridColDataType_emu.VarChar,    150, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "ITEMCODE",   "품목코드", true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "ITEMNAME",   "품목명",   true, GridColDataType_emu.VarChar,    150, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "TRADINGQTY", "출고수량", true, GridColDataType_emu.Double,     100, 120, Infragistics.Win.HAlign.Right,  true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "BASEUNIT",   "단위",     true, GridColDataType_emu.VarChar,    100, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid2, "MAKEDATE",   "등록일시", true, GridColDataType_emu.DateTime24, 200, 120, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.SetInitUltraGridBind(grid2);
            #endregion

            #region ▶ COMBOBOX ◀
            Common _Common = new Common();
            rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  // 사업장
            Common.FillComboboxMaster(this.cboPlantCode_H, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

            rtnDtTemp = _Common.Standard_CODE("UNITCODE");     //단위
            UltraGridUtil.SetComboUltraGrid(this.grid2, "UNITCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

            #endregion

            #region ▶ POP-UP ◀
            #endregion

            #region ▶ ENTER-MOVE ◀
            cboPlantCode_H.Value = "1000";
            #endregion
        }
        #endregion

        #region [ TOOL BAR AREA ]
        /// <summary>
        /// ToolBar의 조회 버튼 클릭
        /// </summary>

        public override void DoInquire()
        {
            DBHelper helper = new DBHelper(false);

            try
            {
                _GridUtil.Grid_Clear(grid1);
                _GridUtil.Grid_Clear(grid2);

                string sPlantCode = Convert.ToString(this.cboPlantCode_H.Value);
                string sTradingNo = Convert.ToString(txtTradingNo.Text);
                string sCarNo     = Convert.ToString(txtCarNo.Text);
                string sStartDate = string.Format("{0:yyyy-MM-dd}", dtStartDate.Value);
                string sEndDate   = string.Format("{0:yyyy-MM-dd}", dtEnddate.Value);
                
                

                rtnDtTemp = helper.FillTable("WM_TradingManager_S1", CommandType.StoredProcedure
                                                             , helper.CreateParameter("PLANTCODE",  sPlantCode, DbType.String, ParameterDirection.Input)
                                                             , helper.CreateParameter("TRADINGNO",  sTradingNo, DbType.String, ParameterDirection.Input)
                                                             , helper.CreateParameter("CARNO",      sCarNo,     DbType.String, ParameterDirection.Input)
                                                             , helper.CreateParameter("STARTDATE",  sStartDate, DbType.String, ParameterDirection.Input)
                                                             , helper.CreateParameter("ENDDATE",    sEndDate,   DbType.String, ParameterDirection.Input)

                                                             );
                if (rtnDtTemp.Rows.Count > 0)
                {
                    grid1.DataSource = rtnDtTemp;
                    grid1.DataBinds(rtnDtTemp);
                }
                else
                {
                    _GridUtil.Grid_Clear(grid1);
                    this.ShowDialog("R00111", DialogForm.DialogType.OK);    // 조회할 데이터가 없습니다.
                    return;
                }
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

        
        private void grid1_AfterRowActivate(object sender, EventArgs e)
        {
            DBHelper helper = new DBHelper(false); 
            try
            {
                string sPlantCode = Convert.ToString(this.grid1.ActiveRow.Cells["PLANTCODE"].Value);
                string sTradingNo    = Convert.ToString(this.grid1.ActiveRow.Cells["TRADINGNO"].Value);



                rtnDtTemp = helper.FillTable("WM_TradingManager_S2", CommandType.StoredProcedure
                                                             , helper.CreateParameter("PLANTCODE", sPlantCode, DbType.String, ParameterDirection.Input)
                                                             , helper.CreateParameter("TRADINGNO", sTradingNo, DbType.String, ParameterDirection.Input)
                                                             );
                if (rtnDtTemp.Rows.Count > 0)
                {
                    grid2.DataSource = rtnDtTemp;
                    grid2.DataBinds(rtnDtTemp);
                }
                else
                {
                    
                }
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

        #region [ User Method Area ]
        #endregion

        private void btnWorker_Click(object sender, EventArgs e)
        {
            // 거래명세 데이터 조회
            DBHelper helper = new DBHelper(false);
            try
            {
                string sPlantCode = Convert.ToString(this.grid1.ActiveRow.Cells["PLANTCODE"].Value);
                string sTradingNo = Convert.ToString(this.grid1.ActiveRow.Cells["TRADINGNO"].Value);



                rtnDtTemp = helper.FillTable("WM_TradingManager_S3", CommandType.StoredProcedure
                                                             , helper.CreateParameter("PLANTCODE", sPlantCode, DbType.String, ParameterDirection.Input)
                                                             , helper.CreateParameter("TRADINGNO", sTradingNo, DbType.String, ParameterDirection.Input)
                                                             );
                if (rtnDtTemp.Rows.Count > 0)
                {
                    // 바코드 디자인 선언
                    Report_BanChul sReport_Trading = new Report_BanChul();
                    Telerik.Reporting.ReportBook reportBook = new Telerik.Reporting.ReportBook();
                    sReport_Trading.DataSource = rtnDtTemp;
                    reportBook.Reports.Add(sReport_Trading);

                    ReportViewer ReportViewer = new ReportViewer(reportBook, 1);
                    ReportViewer.ShowDialog();
                }


                ////거래처 별로 거래 명세서 발행(보완 필요 06 - 18)
                //if (rtnDtTemp.Rows.Count > 0)
                //{
                //    // 바코드 디자인 선언
                //    Report_BanChul[] sReport_Trading = new Report_BanChul[10];
                //    Telerik.Reporting.ReportBook reportBook = new Telerik.Reporting.ReportBook();
                //    DataTable ArrayTable = new DataTable();
                //    string sCustName = Convert.ToString(rtnDtTemp.Rows[0]["CUSTNAME"]);
                //    int i = 0;
                //    for (i = 0; i < rtnDtTemp.Rows.Count; i++)
                //    {
                //        if (sCustName != Convert.ToString(rtnDtTemp.Rows[i]["CUSTNAME"]))
                //        {
                //            sReport_Trading[i].DataSource = rtnDtTemp;
                //            reportBook.Reports.Add(sReport_Trading[i]);
                //        }

                //    }
                //    ReportViewer ReportViewer = new ReportViewer(reportBook);
                //    ReportViewer.ShowDialog();
                //}
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
    }
}