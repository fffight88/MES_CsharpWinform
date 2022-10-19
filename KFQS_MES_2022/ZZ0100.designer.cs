namespace KFQS_MES_2022
{
    partial class ZZ0100
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            this.indProgress = new DC00_Component.CircularProgressControl();
            this.lblCOPY = new Infragistics.Win.Misc.UltraLabel();
            this.gbxLogo = new Infragistics.Win.Misc.UltraLabel();
            this.pnlSplash = new Infragistics.Win.Misc.UltraPanel();
            this.txtLogo = new System.Windows.Forms.RichTextBox();
            this.pnlSplash.ClientArea.SuspendLayout();
            this.pnlSplash.SuspendLayout();
            this.SuspendLayout();
            // 
            // indProgress
            // 
            this.indProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.indProgress.BackColor = System.Drawing.Color.Black;
            this.indProgress.CenterMessage = "100%";
            this.indProgress.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.indProgress.ForeColor = System.Drawing.Color.YellowGreen;
            this.indProgress.InnerRadiousFactor = 0.65F;
            this.indProgress.Interval = 50;
            this.indProgress.IsShowMessage = false;
            this.indProgress.Location = new System.Drawing.Point(3, 2);
            this.indProgress.Message = "";
            this.indProgress.MessagePosition = DC00_Component.CircularProgressControl.MessagePositionType.Right;
            this.indProgress.MinimumSize = new System.Drawing.Size(28, 28);
            this.indProgress.Name = "indProgress";
            this.indProgress.OuterRadiousFactor = 0.84F;
            this.indProgress.Percent = 100D;
            this.indProgress.Rotation = DC00_Component.CircularProgressControl.Direction.ANTICLOCKWISE;
            this.indProgress.ShowTime = true;
            this.indProgress.Size = new System.Drawing.Size(102, 197);
            this.indProgress.SpokesCount = 100;
            this.indProgress.SpokeThick = 2;
            this.indProgress.StartAngle = 270F;
            this.indProgress.TabIndex = 0;
            this.indProgress.TickColor = System.Drawing.Color.YellowGreen;
            this.indProgress.Type = DC00_Component.CircularProgressControl.ControlType.PROGRESS;
            // 
            // lblCOPY
            // 
            appearance1.FontData.BoldAsString = "True";
            appearance1.FontData.Name = "Tahoma";
            appearance1.FontData.SizeInPoints = 6F;
            appearance1.ForeColor = System.Drawing.Color.Silver;
            appearance1.TextHAlignAsString = "Right";
            appearance1.TextVAlignAsString = "Middle";
            this.lblCOPY.Appearance = appearance1;
            this.lblCOPY.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblCOPY.Location = new System.Drawing.Point(0, 178);
            this.lblCOPY.Name = "lblCOPY";
            this.lblCOPY.Size = new System.Drawing.Size(583, 23);
            this.lblCOPY.TabIndex = 1;
            this.lblCOPY.Visible = false;
            // 
            // gbxLogo
            // 
            this.gbxLogo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            appearance2.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Stretched;
            this.gbxLogo.Appearance = appearance2;
            this.gbxLogo.Location = new System.Drawing.Point(3, 2);
            this.gbxLogo.Name = "gbxLogo";
            this.gbxLogo.Size = new System.Drawing.Size(578, 26);
            this.gbxLogo.TabIndex = 2;
            // 
            // pnlSplash
            // 
            appearance3.BorderColor = System.Drawing.Color.DarkGray;
            this.pnlSplash.Appearance = appearance3;
            this.pnlSplash.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            // 
            // pnlSplash.ClientArea
            // 
            this.pnlSplash.ClientArea.Controls.Add(this.txtLogo);
            this.pnlSplash.ClientArea.Controls.Add(this.indProgress);
            this.pnlSplash.ClientArea.Controls.Add(this.gbxLogo);
            this.pnlSplash.ClientArea.Controls.Add(this.lblCOPY);
            this.pnlSplash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSplash.Location = new System.Drawing.Point(0, 0);
            this.pnlSplash.Name = "pnlSplash";
            this.pnlSplash.Size = new System.Drawing.Size(585, 203);
            this.pnlSplash.TabIndex = 3;
            // 
            // txtLogo
            // 
            this.txtLogo.BackColor = System.Drawing.SystemColors.WindowText;
            this.txtLogo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLogo.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtLogo.ForeColor = System.Drawing.Color.YellowGreen;
            this.txtLogo.Location = new System.Drawing.Point(100, 2);
            this.txtLogo.Name = "txtLogo";
            this.txtLogo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.txtLogo.Size = new System.Drawing.Size(481, 197);
            this.txtLogo.TabIndex = 4;
            this.txtLogo.Text = "";
            this.txtLogo.WordWrap = false;
            // 
            // ZZ0100
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(585, 203);
            this.Controls.Add(this.pnlSplash);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ZZ0100";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SPLASH";
            this.Load += new System.EventHandler(this.ZZ0100_Load);
            this.pnlSplash.ClientArea.ResumeLayout(false);
            this.pnlSplash.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private DC00_Component.CircularProgressControl indProgress;
        private Infragistics.Win.Misc.UltraLabel lblCOPY;
        private Infragistics.Win.Misc.UltraLabel gbxLogo;
        private Infragistics.Win.Misc.UltraPanel pnlSplash;
        private System.Windows.Forms.RichTextBox txtLogo;
    }
}

