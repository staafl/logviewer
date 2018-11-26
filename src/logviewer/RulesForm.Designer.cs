namespace logviewer
{
    partial class RulesForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvColumnRules = new System.Windows.Forms.DataGridView();
            this.columnRuleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnRegex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnColor = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.columnFieldDelimiter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnFieldIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvRowRules = new System.Windows.Forms.DataGridView();
            this.rowRulesColumnRuleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rowRulesColumnIncludeRegex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rowRulesColumnExcludeRegex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonImport = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumnRules)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRowRules)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(850, 531);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvColumnRules);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(842, 502);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Column Rules";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvColumnRules
            // 
            this.dgvColumnRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColumnRules.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnRuleName,
            this.columnColumnName,
            this.columnRegex,
            this.columnColor,
            this.columnFieldDelimiter,
            this.columnFieldIndex});
            this.dgvColumnRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvColumnRules.Location = new System.Drawing.Point(3, 3);
            this.dgvColumnRules.Name = "dgvColumnRules";
            this.dgvColumnRules.RowTemplate.Height = 24;
            this.dgvColumnRules.Size = new System.Drawing.Size(836, 496);
            this.dgvColumnRules.TabIndex = 0;
            // 
            // columnRuleName
            // 
            this.columnRuleName.HeaderText = "Rule Name";
            this.columnRuleName.Name = "columnRuleName";
            // 
            // columnColumnName
            // 
            this.columnColumnName.HeaderText = "Column Name";
            this.columnColumnName.Name = "columnColumnName";
            // 
            // columnRegex
            // 
            this.columnRegex.HeaderText = "Regex";
            this.columnRegex.Name = "columnRegex";
            // 
            // columnColor
            // 
            this.columnColor.HeaderText = "Color";
            this.columnColor.Name = "columnColor";
            // 
            // columnFieldDelimiter
            // 
            this.columnFieldDelimiter.HeaderText = "Field Delimiter";
            this.columnFieldDelimiter.Name = "columnFieldDelimiter";
            // 
            // columnFieldIndex
            // 
            this.columnFieldIndex.HeaderText = "Field Index";
            this.columnFieldIndex.Name = "columnFieldIndex";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvRowRules);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(842, 502);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Row Rules";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvRowRules
            // 
            this.dgvRowRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRowRules.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rowRulesColumnRuleName,
            this.rowRulesColumnIncludeRegex,
            this.rowRulesColumnExcludeRegex});
            this.dgvRowRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRowRules.Location = new System.Drawing.Point(3, 3);
            this.dgvRowRules.Name = "dgvRowRules";
            this.dgvRowRules.RowTemplate.Height = 24;
            this.dgvRowRules.Size = new System.Drawing.Size(836, 496);
            this.dgvRowRules.TabIndex = 0;
            // 
            // rowRulesColumnRuleName
            // 
            this.rowRulesColumnRuleName.HeaderText = "Rule Name";
            this.rowRulesColumnRuleName.Name = "rowRulesColumnRuleName";
            // 
            // rowRulesColumnIncludeRegex
            // 
            this.rowRulesColumnIncludeRegex.HeaderText = "Include Regex";
            this.rowRulesColumnIncludeRegex.Name = "rowRulesColumnIncludeRegex";
            // 
            // rowRulesColumnExcludeRegex
            // 
            this.rowRulesColumnExcludeRegex.HeaderText = "Exclude Regex";
            this.rowRulesColumnExcludeRegex.Name = "rowRulesColumnExcludeRegex";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.buttonExport);
            this.panel1.Controls.Add(this.buttonImport);
            this.panel1.Controls.Add(this.buttonOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 482);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(850, 49);
            this.panel1.TabIndex = 1;
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(771, 8);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 34);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExport.Location = new System.Drawing.Point(611, 8);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(75, 34);
            this.buttonExport.TabIndex = 2;
            this.buttonExport.Text = "&Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonImport
            // 
            this.buttonImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonImport.Location = new System.Drawing.Point(530, 8);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(75, 34);
            this.buttonImport.TabIndex = 1;
            this.buttonImport.Text = "&Import";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(691, 8);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 34);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "&Apply";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // RulesForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(850, 531);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.Name = "RulesForm";
            this.Text = "Column and Row Rules";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumnRules)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRowRules)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvColumnRules;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnRuleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnRegex;
        private System.Windows.Forms.DataGridViewComboBoxColumn columnColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnFieldDelimiter;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnFieldIndex;
        private System.Windows.Forms.DataGridView dgvRowRules;
        private System.Windows.Forms.DataGridViewTextBoxColumn rowRulesColumnRuleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn rowRulesColumnIncludeRegex;
        private System.Windows.Forms.DataGridViewTextBoxColumn rowRulesColumnExcludeRegex;
    }
}