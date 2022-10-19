namespace KFQS_MES_2022
{
    partial class ZZ0300
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
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
            this.pnlSplash = new Infragistics.Win.Misc.UltraPanel();
            this.txtPWDChk = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.sLabel3 = new DC00_Component.SLabel();
            this.txtID = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.sLabel2 = new DC00_Component.SLabel();
            this.txtPwdChg = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.sLabel4 = new DC00_Component.SLabel();
            this.txtPwdNow = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.sLabel5 = new DC00_Component.SLabel();
            this.btnClose = new Infragistics.Win.Misc.UltraButton();
            this.btnSave = new Infragistics.Win.Misc.UltraButton();
            this.pnlSplash.ClientArea.SuspendLayout();
            this.pnlSplash.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPWDChk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPwdChg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPwdNow)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSplash
            // 
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BorderColor = System.Drawing.Color.LightGray;
            this.pnlSplash.Appearance = appearance1;
            this.pnlSplash.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            // 
            // pnlSplash.ClientArea
            // 
            this.pnlSplash.ClientArea.Controls.Add(this.txtPWDChk);
            this.pnlSplash.ClientArea.Controls.Add(this.sLabel3);
            this.pnlSplash.ClientArea.Controls.Add(this.txtID);
            this.pnlSplash.ClientArea.Controls.Add(this.sLabel2);
            this.pnlSplash.ClientArea.Controls.Add(this.txtPwdChg);
            this.pnlSplash.ClientArea.Controls.Add(this.sLabel4);
            this.pnlSplash.ClientArea.Controls.Add(this.txtPwdNow);
            this.pnlSplash.ClientArea.Controls.Add(this.sLabel5);
            this.pnlSplash.ClientArea.Controls.Add(this.btnClose);
            this.pnlSplash.ClientArea.Controls.Add(this.btnSave);
            this.pnlSplash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSplash.Location = new System.Drawing.Point(0, 0);
            this.pnlSplash.Name = "pnlSplash";
            this.pnlSplash.Size = new System.Drawing.Size(355, 203);
            this.pnlSplash.TabIndex = 4;
            this.pnlSplash.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlSplash_MouseDown);
            this.pnlSplash.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlSplash_MouseMove);
            this.pnlSplash.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlSplash_MouseUp);
            // 
            // txtPWDChk
            // 
            appearance14.FontData.Name = "맑은 고딕";
            appearance14.FontData.SizeInPoints = 10F;
            this.txtPWDChk.Appearance = appearance14;
            this.txtPWDChk.AutoSize = false;
            this.txtPWDChk.Location = new System.Drawing.Point(155, 110);
            this.txtPWDChk.Name = "txtPWDChk";
            this.txtPWDChk.PasswordChar = '*';
            this.txtPWDChk.Size = new System.Drawing.Size(178, 27);
            this.txtPWDChk.TabIndex = 30;
            // 
            // sLabel3
            // 
            appearance15.FontData.BoldAsString = "True";
            appearance15.FontData.UnderlineAsString = "True";
            appearance15.ForeColor = System.Drawing.Color.Blue;
            appearance15.TextHAlignAsString = "Right";
            appearance15.TextVAlignAsString = "Middle";
            this.sLabel3.Appearance = appearance15;
            this.sLabel3.DbField = null;
            this.sLabel3.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.sLabel3.Location = new System.Drawing.Point(34, 113);
            this.sLabel3.Name = "sLabel3";
            this.sLabel3.RequireFlag = DC00_Component.SLabel.RequireFlagEnum.YES;
            this.sLabel3.Size = new System.Drawing.Size(116, 23);
            this.sLabel3.TabIndex = 29;
            this.sLabel3.Text = "비밀번호 확인";
            // 
            // txtID
            // 
            appearance17.FontData.Name = "맑은 고딕";
            appearance17.FontData.SizeInPoints = 10F;
            this.txtID.Appearance = appearance17;
            this.txtID.AutoSize = false;
            this.txtID.Location = new System.Drawing.Point(155, 11);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(178, 27);
            this.txtID.TabIndex = 24;
            // 
            // sLabel2
            // 
            appearance19.FontData.BoldAsString = "True";
            appearance19.FontData.UnderlineAsString = "True";
            appearance19.ForeColor = System.Drawing.Color.Blue;
            appearance19.TextHAlignAsString = "Right";
            appearance19.TextVAlignAsString = "Middle";
            this.sLabel2.Appearance = appearance19;
            this.sLabel2.DbField = null;
            this.sLabel2.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.sLabel2.Location = new System.Drawing.Point(50, 12);
            this.sLabel2.Name = "sLabel2";
            this.sLabel2.RequireFlag = DC00_Component.SLabel.RequireFlagEnum.YES;
            this.sLabel2.Size = new System.Drawing.Size(100, 23);
            this.sLabel2.TabIndex = 23;
            this.sLabel2.Text = "사용자 ID";
            // 
            // txtPwdChg
            // 
            appearance20.FontData.Name = "맑은 고딕";
            appearance20.FontData.SizeInPoints = 10F;
            this.txtPwdChg.Appearance = appearance20;
            this.txtPwdChg.AutoSize = false;
            this.txtPwdChg.Location = new System.Drawing.Point(155, 77);
            this.txtPwdChg.Name = "txtPwdChg";
            this.txtPwdChg.PasswordChar = '*';
            this.txtPwdChg.Size = new System.Drawing.Size(178, 27);
            this.txtPwdChg.TabIndex = 28;
            // 
            // sLabel4
            // 
            appearance21.FontData.BoldAsString = "True";
            appearance21.FontData.UnderlineAsString = "True";
            appearance21.ForeColor = System.Drawing.Color.Blue;
            appearance21.TextHAlignAsString = "Right";
            appearance21.TextVAlignAsString = "Middle";
            this.sLabel4.Appearance = appearance21;
            this.sLabel4.DbField = null;
            this.sLabel4.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.sLabel4.Location = new System.Drawing.Point(34, 80);
            this.sLabel4.Name = "sLabel4";
            this.sLabel4.RequireFlag = DC00_Component.SLabel.RequireFlagEnum.YES;
            this.sLabel4.Size = new System.Drawing.Size(116, 23);
            this.sLabel4.TabIndex = 27;
            this.sLabel4.Text = "변경 비밀번호";
            // 
            // txtPwdNow
            // 
            appearance22.FontData.Name = "맑은 고딕";
            appearance22.FontData.SizeInPoints = 10F;
            this.txtPwdNow.Appearance = appearance22;
            this.txtPwdNow.AutoSize = false;
            this.txtPwdNow.Location = new System.Drawing.Point(155, 44);
            this.txtPwdNow.Name = "txtPwdNow";
            this.txtPwdNow.PasswordChar = '*';
            this.txtPwdNow.Size = new System.Drawing.Size(178, 27);
            this.txtPwdNow.TabIndex = 26;
            // 
            // sLabel5
            // 
            appearance23.FontData.BoldAsString = "True";
            appearance23.FontData.UnderlineAsString = "True";
            appearance23.ForeColor = System.Drawing.Color.Blue;
            appearance23.TextHAlignAsString = "Right";
            appearance23.TextVAlignAsString = "Middle";
            this.sLabel5.Appearance = appearance23;
            this.sLabel5.DbField = null;
            this.sLabel5.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.sLabel5.Location = new System.Drawing.Point(34, 45);
            this.sLabel5.Name = "sLabel5";
            this.sLabel5.RequireFlag = DC00_Component.SLabel.RequireFlagEnum.YES;
            this.sLabel5.Size = new System.Drawing.Size(116, 23);
            this.sLabel5.TabIndex = 25;
            this.sLabel5.Text = "현재 비밀번호";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(263, 143);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 43);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "닫기";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(187, 143);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 43);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "저장";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ZZ0300
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(355, 203);
            this.Controls.Add(this.pnlSplash);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ZZ0300";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "비밀번호 변경";
            this.pnlSplash.ClientArea.ResumeLayout(false);
            this.pnlSplash.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtPWDChk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPwdChg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPwdNow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraPanel pnlSplash;
        private Infragistics.Win.Misc.UltraButton btnClose;
        private Infragistics.Win.Misc.UltraButton btnSave;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtPWDChk;
        private DC00_Component.SLabel sLabel3;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtID;
        private DC00_Component.SLabel sLabel2;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtPwdChg;
        private DC00_Component.SLabel sLabel4;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtPwdNow;
        private DC00_Component.SLabel sLabel5;

    }
}

