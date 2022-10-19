namespace SAMMI.PopUp
{
    partial class POP_MM0300Y
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.TypeReportSource typeReportSource1 = new Telerik.Reporting.TypeReportSource();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ReportViewer = new Telerik.ReportViewer.WinForms.ReportViewer();
            this.txtTraType = new System.Windows.Forms.TextBox();
            this.sLabel2 = new SAMMI.Control.SLabel();
            this.txtPrintingCount = new System.Windows.Forms.TextBox();
            this.sLabel1 = new SAMMI.Control.SLabel();
            this.txtTraNo = new System.Windows.Forms.TextBox();
            this.lblPlantCode = new SAMMI.Control.SLabel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 425);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(687, 56);
            this.panel1.TabIndex = 0;
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("맑은 고딕", 11F);
            this.btnPrint.Location = new System.Drawing.Point(473, 6);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(102, 46);
            this.btnPrint.TabIndex = 558;
            this.btnPrint.TabStop = false;
            this.btnPrint.Text = "반출증 발행";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("맑은 고딕", 11F);
            this.btnClose.Location = new System.Drawing.Point(581, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(102, 46);
            this.btnClose.TabIndex = 557;
            this.btnClose.TabStop = false;
            this.btnClose.Text = "닫기";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(687, 422);
            this.panel2.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.ReportViewer);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 152);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(687, 270);
            this.panel4.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(687, 152);
            this.panel3.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTraType);
            this.groupBox1.Controls.Add(this.sLabel2);
            this.groupBox1.Controls.Add(this.txtPrintingCount);
            this.groupBox1.Controls.Add(this.sLabel1);
            this.groupBox1.Controls.Add(this.txtTraNo);
            this.groupBox1.Controls.Add(this.lblPlantCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(687, 152);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "반출증 정보";
            // 
            // ReportViewer
            // 
            this.ReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportViewer.Location = new System.Drawing.Point(0, 0);
            this.ReportViewer.Name = "ReportViewer";
            typeReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("TraNo", null));
            typeReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("PrintDate", null));
            typeReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("FaxNo", null));
            typeReportSource1.TypeName = "P4017.MM2300X_R, P4017, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            this.ReportViewer.ReportSource = typeReportSource1;
            this.ReportViewer.Size = new System.Drawing.Size(687, 270);
            this.ReportViewer.TabIndex = 2;
            // 
            // txtTraType
            // 
            this.txtTraType.BackColor = System.Drawing.SystemColors.Window;
            this.txtTraType.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.txtTraType.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtTraType.Location = new System.Drawing.Point(347, 26);
            this.txtTraType.MaxLength = 2;
            this.txtTraType.Name = "txtTraType";
            this.txtTraType.Size = new System.Drawing.Size(138, 25);
            this.txtTraType.TabIndex = 553;
            // 
            // sLabel2
            // 
            appearance3.FontData.BoldAsString = "False";
            appearance3.FontData.UnderlineAsString = "False";
            appearance3.ForeColor = System.Drawing.Color.Black;
            appearance3.TextHAlignAsString = "Right";
            appearance3.TextVAlignAsString = "Middle";
            this.sLabel2.Appearance = appearance3;
            this.sLabel2.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
            this.sLabel2.DbField = null;
            this.sLabel2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.sLabel2.Location = new System.Drawing.Point(261, 26);
            this.sLabel2.Name = "sLabel2";
            this.sLabel2.RequireFlag = SAMMI.Control.SLabel.RequireFlagEnum.NO;
            this.sLabel2.Size = new System.Drawing.Size(80, 25);
            this.sLabel2.TabIndex = 552;
            this.sLabel2.Text = "반출유형";
            // 
            // txtPrintingCount
            // 
            this.txtPrintingCount.BackColor = System.Drawing.SystemColors.Window;
            this.txtPrintingCount.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.txtPrintingCount.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtPrintingCount.Location = new System.Drawing.Point(577, 26);
            this.txtPrintingCount.MaxLength = 2;
            this.txtPrintingCount.Name = "txtPrintingCount";
            this.txtPrintingCount.Size = new System.Drawing.Size(48, 25);
            this.txtPrintingCount.TabIndex = 551;
            // 
            // sLabel1
            // 
            appearance4.FontData.BoldAsString = "False";
            appearance4.FontData.UnderlineAsString = "False";
            appearance4.ForeColor = System.Drawing.Color.Black;
            appearance4.TextHAlignAsString = "Right";
            appearance4.TextVAlignAsString = "Middle";
            this.sLabel1.Appearance = appearance4;
            this.sLabel1.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
            this.sLabel1.DbField = null;
            this.sLabel1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.sLabel1.Location = new System.Drawing.Point(491, 26);
            this.sLabel1.Name = "sLabel1";
            this.sLabel1.RequireFlag = SAMMI.Control.SLabel.RequireFlagEnum.NO;
            this.sLabel1.Size = new System.Drawing.Size(80, 25);
            this.sLabel1.TabIndex = 550;
            this.sLabel1.Text = "출력 매수";
            // 
            // txtTraNo
            // 
            this.txtTraNo.BackColor = System.Drawing.SystemColors.Window;
            this.txtTraNo.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.txtTraNo.Location = new System.Drawing.Point(95, 26);
            this.txtTraNo.Name = "txtTraNo";
            this.txtTraNo.Size = new System.Drawing.Size(160, 25);
            this.txtTraNo.TabIndex = 549;
            // 
            // lblPlantCode
            // 
            appearance2.FontData.BoldAsString = "False";
            appearance2.FontData.UnderlineAsString = "False";
            appearance2.ForeColor = System.Drawing.Color.Black;
            appearance2.TextHAlignAsString = "Right";
            appearance2.TextVAlignAsString = "Middle";
            this.lblPlantCode.Appearance = appearance2;
            this.lblPlantCode.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
            this.lblPlantCode.DbField = null;
            this.lblPlantCode.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblPlantCode.Location = new System.Drawing.Point(9, 26);
            this.lblPlantCode.Name = "lblPlantCode";
            this.lblPlantCode.RequireFlag = SAMMI.Control.SLabel.RequireFlagEnum.NO;
            this.lblPlantCode.Size = new System.Drawing.Size(80, 25);
            this.lblPlantCode.TabIndex = 548;
            this.lblPlantCode.Text = "반출증 번호";
            // 
            // POP_MM0300Y
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 484);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "POP_MM0300Y";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "반출증 발행";
            this.Load += new System.EventHandler(this.POP_MM0300Y_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClose;
        private Telerik.ReportViewer.WinForms.ReportViewer ReportViewer;
        private System.Windows.Forms.TextBox txtTraType;
        private Control.SLabel sLabel2;
        private System.Windows.Forms.TextBox txtPrintingCount;
        private Control.SLabel sLabel1;
        private System.Windows.Forms.TextBox txtTraNo;
        private Control.SLabel lblPlantCode;
    }
}