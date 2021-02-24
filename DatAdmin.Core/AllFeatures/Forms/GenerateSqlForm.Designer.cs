namespace DatAdmin
{
    partial class GenerateSqlForm
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
            DatAdmin.SqlFormatProperties sqlFormatProperties1 = new DatAdmin.SqlFormatProperties();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenerateSqlForm));
            this.splitContainerEx1 = new DatAdmin.SplitContainerEx();
            this.lbxRows = new System.Windows.Forms.ListBox();
            this.labRows = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxDialect = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbxGenerator = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataSqlGeneratorFrame1 = new DatAdmin.DataSqlGeneratorFrame();
            this.propertyFrame1 = new DatAdmin.PropertyFrame();
            this.splitContainerEx2 = new DatAdmin.SplitContainerEx();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOkPreviewRows = new System.Windows.Forms.Button();
            this.tbxRows = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.codeEditor1 = new DatAdmin.CodeEditor();
            this.label9 = new System.Windows.Forms.Label();
            this.sqlFormatPropsFrame1 = new DatAdmin.SqlFormatPropsFrame();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.addonSelectFrame1 = new DatAdmin.AddonSelectFrame();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnSaveAsJob = new System.Windows.Forms.Button();
            this.btnRefreshPreview = new System.Windows.Forms.Button();
            this.splitContainerEx1.Panel1.SuspendLayout();
            this.splitContainerEx1.Panel2.SuspendLayout();
            this.splitContainerEx1.SuspendLayout();
            this.splitContainerEx2.Panel1.SuspendLayout();
            this.splitContainerEx2.Panel2.SuspendLayout();
            this.splitContainerEx2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerEx1
            // 
            this.splitContainerEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerEx1.Location = new System.Drawing.Point(3, 2);
            this.splitContainerEx1.MoreFixedPanel = DatAdmin.SplitContainerEx.PanelType.First;
            this.splitContainerEx1.Name = "splitContainerEx1";
            // 
            // splitContainerEx1.Panel1
            // 
            this.splitContainerEx1.Panel1.Controls.Add(this.lbxRows);
            this.splitContainerEx1.Panel1.Controls.Add(this.labRows);
            this.splitContainerEx1.Panel1.Controls.Add(this.label6);
            this.splitContainerEx1.Panel1.Controls.Add(this.label3);
            this.splitContainerEx1.Panel1.Controls.Add(this.cbxDialect);
            this.splitContainerEx1.Panel1.Controls.Add(this.label2);
            this.splitContainerEx1.Panel1.Controls.Add(this.lbxGenerator);
            this.splitContainerEx1.Panel1.Controls.Add(this.label1);
            this.splitContainerEx1.Panel1.Controls.Add(this.dataSqlGeneratorFrame1);
            this.splitContainerEx1.Panel1.Controls.Add(this.propertyFrame1);
            this.splitContainerEx1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainerPanel_Paint);
            this.splitContainerEx1.Panel1DynamicCollapsed = false;
            this.splitContainerEx1.Panel1MinDynamicSize = 0;
            // 
            // splitContainerEx1.Panel2
            // 
            this.splitContainerEx1.Panel2.Controls.Add(this.splitContainerEx2);
            this.splitContainerEx1.Panel2DynamicCollapsed = false;
            this.splitContainerEx1.Panel2MinDynamicSize = 0;
            this.splitContainerEx1.Size = new System.Drawing.Size(924, 505);
            this.splitContainerEx1.SplitterDistance = 200;
            this.splitContainerEx1.SplitterWidth = 6;
            this.splitContainerEx1.TabIndex = 0;
            // 
            // lbxRows
            // 
            this.lbxRows.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxRows.FormattingEnabled = true;
            this.lbxRows.Location = new System.Drawing.Point(31, 242);
            this.lbxRows.Name = "lbxRows";
            this.lbxRows.Size = new System.Drawing.Size(155, 95);
            this.lbxRows.TabIndex = 9;
            this.lbxRows.SelectedIndexChanged += new System.EventHandler(this.lbxRows_SelectedIndexChanged);
            // 
            // labRows
            // 
            this.labRows.AutoSize = true;
            this.labRows.Location = new System.Drawing.Point(28, 226);
            this.labRows.Name = "labRows";
            this.labRows.Size = new System.Drawing.Size(40, 13);
            this.labRows.TabIndex = 8;
            this.labRows.Text = "s_rows";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(9, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 20);
            this.label6.TabIndex = 6;
            this.label6.Text = "1.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 206);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "s_properties";
            // 
            // cbxDialect
            // 
            this.cbxDialect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxDialect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDialect.FormattingEnabled = true;
            this.cbxDialect.Location = new System.Drawing.Point(12, 182);
            this.cbxDialect.Name = "cbxDialect";
            this.cbxDialect.Size = new System.Drawing.Size(174, 21);
            this.cbxDialect.TabIndex = 3;
            this.cbxDialect.SelectedIndexChanged += new System.EventHandler(this.cbxDialect_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "s_dialect";
            // 
            // lbxGenerator
            // 
            this.lbxGenerator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxGenerator.FormattingEnabled = true;
            this.lbxGenerator.Location = new System.Drawing.Point(12, 29);
            this.lbxGenerator.Name = "lbxGenerator";
            this.lbxGenerator.Size = new System.Drawing.Size(174, 134);
            this.lbxGenerator.TabIndex = 1;
            this.lbxGenerator.SelectedIndexChanged += new System.EventHandler(this.lbxGenerator_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(41, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "s_generator";
            // 
            // dataSqlGeneratorFrame1
            // 
            this.dataSqlGeneratorFrame1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataSqlGeneratorFrame1.AutoScroll = true;
            this.dataSqlGeneratorFrame1.Generator = null;
            this.dataSqlGeneratorFrame1.Location = new System.Drawing.Point(12, 343);
            this.dataSqlGeneratorFrame1.Name = "dataSqlGeneratorFrame1";
            this.dataSqlGeneratorFrame1.Size = new System.Drawing.Size(174, 150);
            this.dataSqlGeneratorFrame1.TabIndex = 7;
            this.dataSqlGeneratorFrame1.SettingsChanged += new System.EventHandler(this.dataSqlGeneratorFrame1_SettingsChanged);
            // 
            // propertyFrame1
            // 
            this.propertyFrame1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyFrame1.CacheCustomEditors = false;
            this.propertyFrame1.Location = new System.Drawing.Point(12, 222);
            this.propertyFrame1.Name = "propertyFrame1";
            this.propertyFrame1.SelectedObject = null;
            this.propertyFrame1.Size = new System.Drawing.Size(174, 271);
            this.propertyFrame1.TabIndex = 5;
            // 
            // splitContainerEx2
            // 
            this.splitContainerEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerEx2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerEx2.MoreFixedPanel = DatAdmin.SplitContainerEx.PanelType.Second;
            this.splitContainerEx2.Name = "splitContainerEx2";
            // 
            // splitContainerEx2.Panel1
            // 
            this.splitContainerEx2.Panel1.Controls.Add(this.panel1);
            this.splitContainerEx2.Panel1.Controls.Add(this.pictureBox1);
            this.splitContainerEx2.Panel1.Controls.Add(this.label7);
            this.splitContainerEx2.Panel1.Controls.Add(this.label4);
            this.splitContainerEx2.Panel1.Controls.Add(this.codeEditor1);
            this.splitContainerEx2.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainerPanel_Paint);
            this.splitContainerEx2.Panel1DynamicCollapsed = false;
            this.splitContainerEx2.Panel1MinDynamicSize = 0;
            // 
            // splitContainerEx2.Panel2
            // 
            this.splitContainerEx2.Panel2.Controls.Add(this.label9);
            this.splitContainerEx2.Panel2.Controls.Add(this.sqlFormatPropsFrame1);
            this.splitContainerEx2.Panel2.Controls.Add(this.label8);
            this.splitContainerEx2.Panel2.Controls.Add(this.label5);
            this.splitContainerEx2.Panel2.Controls.Add(this.addonSelectFrame1);
            this.splitContainerEx2.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainerPanel_Paint);
            this.splitContainerEx2.Panel2DynamicCollapsed = false;
            this.splitContainerEx2.Panel2MinDynamicSize = 0;
            this.splitContainerEx2.Size = new System.Drawing.Size(718, 505);
            this.splitContainerEx2.SplitterDistance = 518;
            this.splitContainerEx2.SplitterWidth = 6;
            this.splitContainerEx2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRefreshPreview);
            this.panel1.Controls.Add(this.btnOkPreviewRows);
            this.panel1.Controls.Add(this.tbxRows);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Location = new System.Drawing.Point(7, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(499, 25);
            this.panel1.TabIndex = 10;
            // 
            // btnOkPreviewRows
            // 
            this.btnOkPreviewRows.Location = new System.Drawing.Point(308, 0);
            this.btnOkPreviewRows.Name = "btnOkPreviewRows";
            this.btnOkPreviewRows.Size = new System.Drawing.Size(46, 23);
            this.btnOkPreviewRows.TabIndex = 2;
            this.btnOkPreviewRows.Text = "s_ok";
            this.btnOkPreviewRows.UseVisualStyleBackColor = true;
            this.btnOkPreviewRows.Click += new System.EventHandler(this.btnOkPreviewRows_Click);
            // 
            // tbxRows
            // 
            this.tbxRows.Location = new System.Drawing.Point(202, 2);
            this.tbxRows.Name = "tbxRows";
            this.tbxRows.Size = new System.Drawing.Size(100, 20);
            this.tbxRows.TabIndex = 1;
            this.tbxRows.Text = "10";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(113, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "s_preview_rows";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = global::DatAdmin.StdIcons.big_loading_icon;
            this.pictureBox1.Location = new System.Drawing.Point(7, 60);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(499, 430);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(3, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 20);
            this.label7.TabIndex = 8;
            this.label7.Text = "2.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(31, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "s_preview";
            // 
            // codeEditor1
            // 
            this.codeEditor1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.codeEditor1.Dialect = null;
            this.codeEditor1.IsReadOnly = true;
            this.codeEditor1.Language = DatAdmin.CodeLanguage.Sql;
            this.codeEditor1.Location = new System.Drawing.Point(6, 60);
            this.codeEditor1.Modified = true;
            this.codeEditor1.Name = "codeEditor1";
            this.codeEditor1.ReadOnly = true;
            this.codeEditor1.Size = new System.Drawing.Size(500, 430);
            this.codeEditor1.TabIndex = 6;
            this.codeEditor1.Text = "codeEditor1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "s_properties";
            // 
            // sqlFormatPropsFrame1
            // 
            this.sqlFormatPropsFrame1.AutoScroll = true;
            this.sqlFormatPropsFrame1.Location = new System.Drawing.Point(20, 45);
            this.sqlFormatPropsFrame1.Name = "sqlFormatPropsFrame1";
            this.sqlFormatPropsFrame1.Size = new System.Drawing.Size(162, 155);
            this.sqlFormatPropsFrame1.TabIndex = 12;
            sqlFormatProperties1.BinaryEncoding = "utf-8";
            sqlFormatProperties1.BinaryStrings = false;
            sqlFormatProperties1.CleanupSpecificObjectCode = true;
            sqlFormatProperties1.CommandSeparator = null;
            sqlFormatProperties1.DumpWriterConfig = null;
            sqlFormatProperties1.IdentifierCase = DatAdmin.CharacterCase.Original;
            sqlFormatProperties1.IdentifierQuoteMode = DatAdmin.SqlIdentifierQuoteMode.Quoted;
            sqlFormatProperties1.Indentation = 4;
            sqlFormatProperties1.IndentationLevel = DatAdmin.SqlIndentationLevel.Large;
            sqlFormatProperties1.KeywordIdentifierCase = DatAdmin.CharacterCase.Original;
            sqlFormatProperties1.OmitVersionTests = false;
            sqlFormatProperties1.QualifierMode = DatAdmin.SqlQualifierMode.Original;
            sqlFormatProperties1.RealBinaryEncoding = ((System.Text.Encoding)(resources.GetObject("sqlFormatProperties1.RealBinaryEncoding")));
            sqlFormatProperties1.SqlCommandCase = DatAdmin.CharacterCase.Upper;
            sqlFormatProperties1.UseDomains = true;
            sqlFormatProperties1.UseOriginalValues = true;
            this.sqlFormatPropsFrame1.Value = sqlFormatProperties1;
            this.sqlFormatPropsFrame1.UserChangedProperties += new System.EventHandler(this.sqlFormatPropsFrame1_UserChangedProperties);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(3, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 20);
            this.label8.TabIndex = 11;
            this.label8.Text = "3.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(31, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "s_output";
            // 
            // addonSelectFrame1
            // 
            this.addonSelectFrame1.AddonTypeName = "fileplace";
            this.addonSelectFrame1.AllowSwitchDesign = false;
            this.addonSelectFrame1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.addonSelectFrame1.CanSaveAsTemplate = false;
            this.addonSelectFrame1.CompactDesign = false;
            this.addonSelectFrame1.DefaultAddonHolder = "openinnewwindow";
            this.addonSelectFrame1.Location = new System.Drawing.Point(3, 206);
            this.addonSelectFrame1.Name = "addonSelectFrame1";
            this.addonSelectFrame1.ShowInfoFrame = false;
            this.addonSelectFrame1.Size = new System.Drawing.Size(179, 296);
            this.addonSelectFrame1.TabIndex = 9;
            this.addonSelectFrame1.TypeTitle = "s_type";
            this.addonSelectFrame1.ChangedSelectedObject += new System.EventHandler(this.addonSelectFrame1_ChangedSelectedObject);
            this.addonSelectFrame1.FilterAddon += new DatAdmin.FilterAddonEvent(this.addonSelectFrame1_FilterAddon);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(848, 513);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "s_cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(767, 513);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "s_ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnSaveAsJob
            // 
            this.btnSaveAsJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAsJob.Location = new System.Drawing.Point(636, 513);
            this.btnSaveAsJob.Name = "btnSaveAsJob";
            this.btnSaveAsJob.Size = new System.Drawing.Size(125, 23);
            this.btnSaveAsJob.TabIndex = 11;
            this.btnSaveAsJob.Text = "s_save_as_job";
            this.btnSaveAsJob.UseVisualStyleBackColor = true;
            this.btnSaveAsJob.Click += new System.EventHandler(this.btnSaveAsJob_Click);
            // 
            // btnRefreshPreview
            // 
            this.btnRefreshPreview.Image = global::DatAdmin.StdIcons.refresh;
            this.btnRefreshPreview.Location = new System.Drawing.Point(3, 0);
            this.btnRefreshPreview.Name = "btnRefreshPreview";
            this.btnRefreshPreview.Size = new System.Drawing.Size(104, 23);
            this.btnRefreshPreview.TabIndex = 3;
            this.btnRefreshPreview.Text = "s_refresh";
            this.btnRefreshPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefreshPreview.UseVisualStyleBackColor = true;
            this.btnRefreshPreview.Click += new System.EventHandler(this.btnRefreshPreview_Click);
            // 
            // GenerateSqlForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(927, 548);
            this.Controls.Add(this.btnSaveAsJob);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.splitContainerEx1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GenerateSqlForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "s_generate_sql";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainerEx1.Panel1.ResumeLayout(false);
            this.splitContainerEx1.Panel1.PerformLayout();
            this.splitContainerEx1.Panel2.ResumeLayout(false);
            this.splitContainerEx1.ResumeLayout(false);
            this.splitContainerEx2.Panel1.ResumeLayout(false);
            this.splitContainerEx2.Panel1.PerformLayout();
            this.splitContainerEx2.Panel2.ResumeLayout(false);
            this.splitContainerEx2.Panel2.PerformLayout();
            this.splitContainerEx2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainerEx splitContainerEx1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbxGenerator;
        private System.Windows.Forms.Label label1;
        private PropertyFrame propertyFrame1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxDialect;
        private SplitContainerEx splitContainerEx2;
        private System.Windows.Forms.Label label5;
        private AddonSelectFrame addonSelectFrame1;
        private System.Windows.Forms.Label label4;
        private CodeEditor codeEditor1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSaveAsJob;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DataSqlGeneratorFrame dataSqlGeneratorFrame1;
        private System.Windows.Forms.ListBox lbxRows;
        private System.Windows.Forms.Label labRows;
        private System.Windows.Forms.Label label9;
        private SqlFormatPropsFrame sqlFormatPropsFrame1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOkPreviewRows;
        private System.Windows.Forms.TextBox tbxRows;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnRefreshPreview;
    }
}