namespace DC_POPUP
{ 
    partial class POP_TBM0400
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
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance86 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance85 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboPlantCode_H = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.cboUseFlag_H = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.btnFind = new System.Windows.Forms.Button();
            this.lblOpName = new DC00_Component.SLabel();
            this.txtOpName = new System.Windows.Forms.TextBox();
            this.lblOpCode = new DC00_Component.SLabel();
            this.lblUseFlag = new DC00_Component.SLabel();
            this.lblPlantCode = new DC00_Component.SLabel();
            this.txtOpCode = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Grid1 = new DC00_Component.Grid(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlantCode_H)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUseFlag_H)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboPlantCode_H);
            this.panel1.Controls.Add(this.cboUseFlag_H);
            this.panel1.Controls.Add(this.btnFind);
            this.panel1.Controls.Add(this.lblOpName);
            this.panel1.Controls.Add(this.txtOpName);
            this.panel1.Controls.Add(this.lblOpCode);
            this.panel1.Controls.Add(this.lblUseFlag);
            this.panel1.Controls.Add(this.lblPlantCode);
            this.panel1.Controls.Add(this.txtOpCode);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(881, 108);
            this.panel1.TabIndex = 0;
            // 
            // cboPlantCode_H
            // 
            this.cboPlantCode_H.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cboPlantCode_H.Location = new System.Drawing.Point(153, 18);
            this.cboPlantCode_H.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboPlantCode_H.Name = "cboPlantCode_H";
            this.cboPlantCode_H.Size = new System.Drawing.Size(176, 31);
            this.cboPlantCode_H.TabIndex = 1;
            // 
            // cboUseFlag_H
            // 
            this.cboUseFlag_H.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cboUseFlag_H.Location = new System.Drawing.Point(486, 18);
            this.cboUseFlag_H.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboUseFlag_H.Name = "cboUseFlag_H";
            this.cboUseFlag_H.Size = new System.Drawing.Size(176, 31);
            this.cboUseFlag_H.TabIndex = 2;
            // 
            // btnFind
            // 
            this.btnFind.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnFind.Location = new System.Drawing.Point(720, 0);
            this.btnFind.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(161, 108);
            this.btnFind.TabIndex = 5;
            this.btnFind.Text = "조회";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // lblOpName
            // 
            appearance1.TextHAlignAsString = "Right";
            appearance1.TextVAlignAsString = "Middle";
            this.lblOpName.Appearance = appearance1;
            this.lblOpName.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
            this.lblOpName.DbField = null;
            this.lblOpName.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblOpName.Location = new System.Drawing.Point(337, 60);
            this.lblOpName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblOpName.Name = "lblOpName";
            this.lblOpName.RequireFlag = DC00_Component.SLabel.RequireFlagEnum.NO;
            this.lblOpName.Size = new System.Drawing.Size(142, 31);
            this.lblOpName.TabIndex = 0;
            this.lblOpName.Text = "작업장 명";
            // 
            // txtOpName
            // 
            this.txtOpName.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtOpName.Location = new System.Drawing.Point(486, 62);
            this.txtOpName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtOpName.Name = "txtOpName";
            this.txtOpName.Size = new System.Drawing.Size(175, 29);
            this.txtOpName.TabIndex = 4;
            this.txtOpName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOpName_KeyPress);
            // 
            // lblOpCode
            // 
            appearance13.TextHAlignAsString = "Right";
            appearance13.TextVAlignAsString = "Middle";
            this.lblOpCode.Appearance = appearance13;
            this.lblOpCode.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
            this.lblOpCode.DbField = null;
            this.lblOpCode.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblOpCode.Location = new System.Drawing.Point(27, 60);
            this.lblOpCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblOpCode.Name = "lblOpCode";
            this.lblOpCode.RequireFlag = DC00_Component.SLabel.RequireFlagEnum.NO;
            this.lblOpCode.Size = new System.Drawing.Size(119, 31);
            this.lblOpCode.TabIndex = 0;
            this.lblOpCode.Text = "작업장 코드";
            // 
            // lblUseFlag
            // 
            appearance86.TextHAlignAsString = "Right";
            appearance86.TextVAlignAsString = "Middle";
            this.lblUseFlag.Appearance = appearance86;
            this.lblUseFlag.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
            this.lblUseFlag.DbField = null;
            this.lblUseFlag.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblUseFlag.Location = new System.Drawing.Point(337, 18);
            this.lblUseFlag.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblUseFlag.Name = "lblUseFlag";
            this.lblUseFlag.RequireFlag = DC00_Component.SLabel.RequireFlagEnum.NO;
            this.lblUseFlag.Size = new System.Drawing.Size(145, 31);
            this.lblUseFlag.TabIndex = 0;
            this.lblUseFlag.Text = "사용여부";
            // 
            // lblPlantCode
            // 
            appearance85.TextHAlignAsString = "Right";
            appearance85.TextVAlignAsString = "Middle";
            this.lblPlantCode.Appearance = appearance85;
            this.lblPlantCode.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
            this.lblPlantCode.DbField = null;
            this.lblPlantCode.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblPlantCode.Location = new System.Drawing.Point(27, 18);
            this.lblPlantCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblPlantCode.Name = "lblPlantCode";
            this.lblPlantCode.RequireFlag = DC00_Component.SLabel.RequireFlagEnum.NO;
            this.lblPlantCode.Size = new System.Drawing.Size(119, 31);
            this.lblPlantCode.TabIndex = 0;
            this.lblPlantCode.Text = "공장";
            // 
            // txtOpCode
            // 
            this.txtOpCode.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtOpCode.Location = new System.Drawing.Point(153, 62);
            this.txtOpCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtOpCode.Name = "txtOpCode";
            this.txtOpCode.Size = new System.Drawing.Size(175, 29);
            this.txtOpCode.TabIndex = 3;
            this.txtOpCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOpCode_KeyPress);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Grid1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 108);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(881, 530);
            this.panel2.TabIndex = 1;
            // 
            // Grid1
            // 
            this.Grid1.AutoResizeColumn = true;
            this.Grid1.AutoUserColumn = true;
            this.Grid1.ContextMenuCopyEnabled = true;
            this.Grid1.ContextMenuDeleteEnabled = true;
            this.Grid1.ContextMenuExcelEnabled = true;
            this.Grid1.ContextMenuInsertEnabled = true;
            this.Grid1.ContextMenuPasteEnabled = true;
            this.Grid1.DeleteButtonEnable = true;
            appearance14.BackColor = System.Drawing.SystemColors.Window;
            appearance14.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.Grid1.DisplayLayout.Appearance = appearance14;
            this.Grid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.Grid1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.Grid1.DisplayLayout.DefaultSelectedBackColor = System.Drawing.Color.Empty;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.Grid1.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.Grid1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance4;
            this.Grid1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.Grid1.DisplayLayout.GroupByBox.Hidden = true;
            appearance3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance3.BackColor2 = System.Drawing.SystemColors.Control;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.Grid1.DisplayLayout.GroupByBox.PromptAppearance = appearance3;
            this.Grid1.DisplayLayout.MaxColScrollRegions = 1;
            this.Grid1.DisplayLayout.MaxRowScrollRegions = 1;
            appearance9.BackColor = System.Drawing.SystemColors.Window;
            appearance9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Grid1.DisplayLayout.Override.ActiveCellAppearance = appearance9;
            appearance5.BackColor = System.Drawing.SystemColors.Highlight;
            appearance5.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.Grid1.DisplayLayout.Override.ActiveRowAppearance = appearance5;
            this.Grid1.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.True;
            this.Grid1.DisplayLayout.Override.AllowMultiCellOperations = ((Infragistics.Win.UltraWinGrid.AllowMultiCellOperation)((((((((Infragistics.Win.UltraWinGrid.AllowMultiCellOperation.Copy | Infragistics.Win.UltraWinGrid.AllowMultiCellOperation.CopyWithHeaders) 
            | Infragistics.Win.UltraWinGrid.AllowMultiCellOperation.Cut) 
            | Infragistics.Win.UltraWinGrid.AllowMultiCellOperation.Delete) 
            | Infragistics.Win.UltraWinGrid.AllowMultiCellOperation.Paste) 
            | Infragistics.Win.UltraWinGrid.AllowMultiCellOperation.Undo) 
            | Infragistics.Win.UltraWinGrid.AllowMultiCellOperation.Redo) 
            | Infragistics.Win.UltraWinGrid.AllowMultiCellOperation.Reserved)));
            this.Grid1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.Grid1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance12.BackColor = System.Drawing.SystemColors.Window;
            this.Grid1.DisplayLayout.Override.CardAreaAppearance = appearance12;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.Grid1.DisplayLayout.Override.CellAppearance = appearance8;
            this.Grid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.Grid1.DisplayLayout.Override.CellPadding = 0;
            appearance6.BackColor = System.Drawing.SystemColors.Control;
            appearance6.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance6.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance6.BorderColor = System.Drawing.SystemColors.Window;
            this.Grid1.DisplayLayout.Override.GroupByRowAppearance = appearance6;
            appearance7.TextHAlignAsString = "Left";
            this.Grid1.DisplayLayout.Override.HeaderAppearance = appearance7;
            this.Grid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.Grid1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance10.BackColor = System.Drawing.SystemColors.Window;
            appearance10.BorderColor = System.Drawing.Color.Silver;
            this.Grid1.DisplayLayout.Override.RowAppearance = appearance10;
            this.Grid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance11.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Grid1.DisplayLayout.Override.TemplateAddRowAppearance = appearance11;
            this.Grid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.Grid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.Grid1.DisplayLayout.SelectionOverlayBorderThickness = 2;
            this.Grid1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.Grid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid1.EnterNextRowEnable = true;
            this.Grid1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Grid1.Location = new System.Drawing.Point(0, 0);
            this.Grid1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Grid1.Name = "Grid1";
            this.Grid1.Size = new System.Drawing.Size(881, 530);
            this.Grid1.TabIndex = 0;
            this.Grid1.Text = "grid1";
            this.Grid1.TextRenderingMode = Infragistics.Win.TextRenderingMode.GDI;
            this.Grid1.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnCellChange;
            this.Grid1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.Grid1.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.Grid1.DoubleClickRow += new Infragistics.Win.UltraWinGrid.DoubleClickRowEventHandler(this.Grid1_DoubleClickRow);
            // 
            // POP_TBM0400
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 638);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "POP_TBM0400";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "작업장 정보";
            this.Load += new System.EventHandler(this.POP_TBM0400_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlantCode_H)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUseFlag_H)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DC00_Component.Grid Grid1;
        private DC00_Component.SLabel lblOpName;
        private System.Windows.Forms.TextBox txtOpName;
        private DC00_Component.SLabel lblOpCode;
        private DC00_Component.SLabel lblUseFlag;
        private DC00_Component.SLabel lblPlantCode;
        private System.Windows.Forms.TextBox txtOpCode;
        private System.Windows.Forms.Button btnFind;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor cboUseFlag_H;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor cboPlantCode_H;
    }
}