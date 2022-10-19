using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Telerik.Reporting;
using System.Configuration;
using DC00_assm; 

namespace DC_POPUP
{
    public partial class ReportViewr2 : DC00_WinForm.BasePopupForm
    {
        #region [ 선언자 ]
        DataTable rtnDtTemp = new DataTable();
        DataTable DtChange1 = new DataTable();
        Report_BanChul[] MM0300X_Report = new Report_BanChul[10];

        Report_BanChul MM0300X_Report_N;
        Telerik.Reporting.ObjectDataSource objectDataSource = new Telerik.Reporting.ObjectDataSource();
        Telerik.Reporting.InstanceReportSource viewerInstance = new Telerik.Reporting.InstanceReportSource();

        private string sPlantCode = string.Empty;
        Configuration appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        private string sTransNo = string.Empty;
        private string sTraTypeName = string.Empty;
        private string sPrePrintFlag = string.Empty;


        #endregion

        #region [ 생성자 ]

        //재발행시
        public ReportViewr2(DataTable dtTemp, string PrePrintFlag)
        {
            InitializeComponent();
            sPrePrintFlag = PrePrintFlag;

            sPlantCode = appConfig.AppSettings.Settings["Site"].Value.ToString();
            //Create the report. You can also use a loop to create instances of multiple reports. 
            var reportBook = new ReportBook();
            var instanceReportSource = new Telerik.Reporting.InstanceReportSource();

            int a = 0;
            foreach (DataRow dr_Grid in dtTemp.Rows)
            {

                DBHelper helper = new DBHelper(false);

                //출력 조회SP

                rtnDtTemp = helper.FillTable("USP_MM0300X_R1", CommandType.StoredProcedure
                                , helper.CreateParameter("PlantCode", sPlantCode, DbType.String, ParameterDirection.Input)             // 공장
                                , helper.CreateParameter("TraNo", dr_Grid["TRANO"], DbType.String, ParameterDirection.Input)               // 반출번호
                                , helper.CreateParameter("WorkerID", DC00_assm.LoginInfo.UserID, DbType.String, ParameterDirection.Input) // 출력자
                                , helper.CreateParameter("PrePrintFlag", sPrePrintFlag, DbType.String, ParameterDirection.Input)          // 발행 재발행 여부
                            );

                //데이터 바인딩
                MM0300X_Report[a] = new Report_BanChul();
                MM0300X_Report[a].DataSource = rtnDtTemp;
                
                //Add the report objects to the report book. 
                reportBook.Reports.Add(MM0300X_Report[a]);
                a++;

            }

            instanceReportSource.ReportDocument = reportBook;
            viewerInstance.ReportDocument = reportBook;
            ReportViewer.ReportSource = instanceReportSource;

            ReportViewer.RefreshReport();
        }

        public ReportViewr2(string PlantCode, string TraNo, string TraTypeName, string PrePrintFlag)
        {
            InitializeComponent();

            sPlantCode = PlantCode;
            sTransNo = TraNo;
            sTraTypeName = TraTypeName;
            sPrePrintFlag = PrePrintFlag;

        }
        #endregion

        #region [ Form Load ]
        private void ReportViewr2_Load(object sender, EventArgs e)
        {
            txtTraNo.Text = sTransNo;
            txtTraType.Text = sTraTypeName;
            txtPrintingCount.Text = "2";

            //신규 발행
            if (sPrePrintFlag == "N")
            {
                if (!Inquire())     // 출력 내용이없는 경우 종료
                    this.Close();
            }
            //재 발행
            else
            {
                lblTraType.Visible = false;
                lblTroNa.Visible   = false;
                txtTraNo.Visible   = false;
                txtTraType.Visible = false;
                sLabel1.Visible    = false;
                txtPrintingCount.Visible = false;
            }
        }

        #endregion

        #region [ User Method Area ]
        private Boolean Inquire()
        {
            bool flgInquire_Succeed = true;

            DateTime sNow = DateTime.Now;
            string sDate = string.Empty;
            sDate = sNow.ToString("yyyy") + "년 " + sNow.ToString("MM") + "월 " + sNow.ToString("dd") + "일 " + string.Format("{0:t}", sNow);

            DBHelper helper = new DBHelper(false);

            
            try
            {
                rtnDtTemp = helper.FillTable("USP_MM0300X_R1", CommandType.StoredProcedure
                                                , helper.CreateParameter("PlantCode",    sPlantCode, DbType.String, ParameterDirection.Input)             // 공장
                                                , helper.CreateParameter("TraNo",        sTransNo, DbType.String, ParameterDirection.Input)               // 반출번호
                                                , helper.CreateParameter("WorkerID",     LoginInfo.UserID, DbType.String, ParameterDirection.Input) // 출력자
                                                , helper.CreateParameter("PrePrintFlag", sPrePrintFlag, DbType.String, ParameterDirection.Input)          // 발행 재발행 여부
                                            );
                if (helper.RSCODE != "S")
                {
                    SException ex = new SException(helper.RSCODE, null);
                    throw ex;
                }

                if (rtnDtTemp.Rows.Count < 1)
                {
                    this.ClosePrgForm();
                    SException ex = new SException("R00111", null); // 조회할 데이터가 존재하지 않습니다.
                }
                else
                {
                    //원본
                    MM0300X_Report_N = new Report_BanChul();

                    objectDataSource.DataSource = rtnDtTemp;
                    MM0300X_Report_N.DataSource = objectDataSource;
                    viewerInstance.ReportDocument = MM0300X_Report_N.Report;

                    //viewerInstance.Parameters.Add("TraPlanDate", sTraPlanDate);

                    ReportViewer.ReportSource = viewerInstance;
                    ReportViewer.RefreshReport();
                }
            }
            catch (SException ex)
            {
                flgInquire_Succeed = false;
                this.ClosePrgForm();
                this.ShowErrorMessage(ex);
            }
            catch (Exception ex)
            {
                flgInquire_Succeed = false;
                throw ex;
            }
            finally
            {
                this.ClosePrgForm();
                helper.Close();
            }

            if (flgInquire_Succeed == true)
                return true;
            else
                return false;
        }

        #endregion

        #region [ Event Area ]
        private void btnPrint_Click(object sender, EventArgs e)
        {
            
            Telerik.Reporting.IReportDocument myReport = new Report_BanChul();
            System.Drawing.Printing.PrinterSettings printerSettings = new System.Drawing.Printing.PrinterSettings();
            System.Drawing.Printing.PrintController standardPrintController = new System.Drawing.Printing.StandardPrintController();
            Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();

            reportProcessor.PrintController = standardPrintController;
            printerSettings.Collate = true;

            reportProcessor.PrintReport(viewerInstance, printerSettings);

            //출력매수 설정
            int iPrintingCount = Convert.ToInt16(txtPrintingCount.Text.Trim());
            try
            {
                for (int iCount = 0; iCount < iPrintingCount; iCount++)
                {
                    reportProcessor.PrintReport(viewerInstance, printerSettings);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("출력내역이 존재하지 않습니다.", "ERROR");
                return;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
