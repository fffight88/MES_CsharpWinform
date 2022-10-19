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

namespace SAMMI.PopUp
{
    public partial class POP_MM0300Y : SAMMI.Forms.BasePopupForm
    {
        #region [ 선언자 ]
        DataTable rtnDtTemp = new DataTable();
        DataTable DtChange1 = new DataTable();

        MM0300X_R MM0300X_Report;
        Telerik.Reporting.ObjectDataSource objectDataSource = new Telerik.Reporting.ObjectDataSource();
        Telerik.Reporting.InstanceReportSource viewerInstance = new Telerik.Reporting.InstanceReportSource();

        private string sPlantCode = string.Empty;
        private string sTransNo = string.Empty;
        private string sTraTypeName = string.Empty;
        private string sPrePrintFlag = string.Empty;

        #endregion

        #region [ 생성자 ]
        public POP_MM0300Y()
        {
            InitializeComponent();

        }

        public POP_MM0300Y(DataRow drRow)
        {
            InitializeComponent();

        }
        #endregion

        #region [ Form Load ]
        private void POP_MM0300Y_Load(object sender, EventArgs e)
        {
            
        }

        #endregion

        #region [ User Method Area ]
        /// <summary>
        /// 숫자입력
        /// </summary>
        /// <param name="_RecVal"></param>
        /// <returns></returns>
        public static bool _IsNumber(string _RecVal)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"[^\d.]+");

            if (!regex.IsMatch(_RecVal))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region [ Event Area ]
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void btnLotDivide_Click(object sender, EventArgs e)
        {
                
            
        }

        private void txtDivisionQty_TextChanged(object sender, EventArgs e)
        {
            
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
        }

        
        private void btnPrint_Click(object sender, EventArgs e)
        {
            DoPrint();
        }

        //수정 필요
        private void DoPrint()
        {
            int iPrintingCount = Convert.ToInt16(txtPrintingCount.Text.Trim());

            Telerik.Reporting.IReportDocument myReport = new MM0300X_R();
            System.Drawing.Printing.PrinterSettings printerSettings = new System.Drawing.Printing.PrinterSettings();
            System.Drawing.Printing.PrintController standardPrintController = new System.Drawing.Printing.StandardPrintController();
            Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();

            reportProcessor.PrintController = standardPrintController;
            printerSettings.Collate = true;
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

        //수정 필요
        private void btnSave_Click(object sender, EventArgs e)
        {
        }

        #endregion
    }
}
