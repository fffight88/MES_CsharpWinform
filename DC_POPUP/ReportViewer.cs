using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.Reporting;
namespace DC_POPUP
{
    public partial class ReportViewer : Form
    {
        #region [ 선언자 ]
        // 레포트 북 데이터 소스 및 인스턴스 선언
        InstanceReportSource instanceReportSource = new InstanceReportSource(); // 데이터 출력 인스턴스 선언
        ReportBook rReportBook;
        int PrintCount = 0;
        #endregion

        public ReportViewer(ReportBook TReportBook , int sPrintCount = 1)
        {
            InitializeComponent();
            rReportBook = TReportBook;
            PrintCount  = sPrintCount;
        }
        #region [ Form Load ]
        private void KULS_BARCODE_EDU_REPORTBOOK_Load(object sender, EventArgs e)
        {
            // 레포트 북 미리보기 창에 바인딩 (표시)
            instanceReportSource.ReportDocument = rReportBook;
            reportViewer1.ReportSource = instanceReportSource;
            reportViewer1.RefreshReport();
        }
        #endregion

        #region [ Event Area ]
        private void btnPrint_Click(object sender, EventArgs e)
        {
            
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void reportViewer1_Print(object sender, CancelEventArgs e)
        {
            // 기본 프린터로 출력 
            System.Drawing.Printing.PrinterSettings printerSettings         = new System.Drawing.Printing.PrinterSettings();
            System.Drawing.Printing.PrintController standardPrintController = new System.Drawing.Printing.StandardPrintController();
            Telerik.Reporting.Processing.ReportProcessor reportProcessor    = new Telerik.Reporting.Processing.ReportProcessor();

            reportProcessor.PrintController = standardPrintController;
            printerSettings.Collate = true;

            // 출력매수 설정
            try
            {
                for (int iCount = 0; iCount < PrintCount; iCount++)
                {
                    reportProcessor.PrintReport(instanceReportSource, printerSettings);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("바코드 발행 중 오류가 발생하였습니다." + ex.Message, "거래명세서 발행 오류.");
                return;
            }

            MessageBox.Show("바코드 발행이 완료 되었습니다.");
            e.Cancel = true;
            this.Close();
        }
    }
}
