namespace Plugin.textio
{
    partial class TextImportFrame
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cbxEncoding = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxTextInput = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tbxUrl = new System.Windows.Forms.TextBox();
            this.tbxFile = new System.Windows.Forms.TextBox();
            this.rbtTextInput = new System.Windows.Forms.RadioButton();
            this.rbtUrl = new System.Windows.Forms.RadioButton();
            this.rbtFile = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridViewRows = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPreview = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxLineRegexGroup = new System.Windows.Forms.TextBox();
            this.tbxLineRegex = new System.Windows.Forms.TextBox();
            this.rbtLineRegex = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxLineLimitEnd = new System.Windows.Forms.TextBox();
            this.tbxLineLimitBegin = new System.Windows.Forms.TextBox();
            this.chbLineSepRegex = new System.Windows.Forms.CheckBox();
            this.tbxLineSeparator = new System.Windows.Forms.TextBox();
            this.rbtLineLimitation = new System.Windows.Forms.RadioButton();
            this.rbtLineSeparator = new System.Windows.Forms.RadioButton();
            this.rbtLines = new System.Windows.Forms.RadioButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxFieldType = new System.Windows.Forms.ComboBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridViewPreview = new DatAdmin.BinaryGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRows)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(497, 345);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cbxEncoding);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.tbxTextInput);
            this.tabPage1.Controls.Add(this.btnBrowse);
            this.tabPage1.Controls.Add(this.tbxUrl);
            this.tabPage1.Controls.Add(this.tbxFile);
            this.tabPage1.Controls.Add(this.rbtTextInput);
            this.tabPage1.Controls.Add(this.rbtUrl);
            this.tabPage1.Controls.Add(this.rbtFile);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(489, 319);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "s_source";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cbxEncoding
            // 
            this.cbxEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEncoding.FormattingEnabled = true;
            this.cbxEncoding.Location = new System.Drawing.Point(98, 277);
            this.cbxEncoding.Name = "cbxEncoding";
            this.cbxEncoding.Size = new System.Drawing.Size(308, 21);
            this.cbxEncoding.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 280);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "s_encoding";
            // 
            // tbxTextInput
            // 
            this.tbxTextInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxTextInput.Enabled = false;
            this.tbxTextInput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbxTextInput.Location = new System.Drawing.Point(98, 67);
            this.tbxTextInput.Multiline = true;
            this.tbxTextInput.Name = "tbxTextInput";
            this.tbxTextInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbxTextInput.Size = new System.Drawing.Size(308, 199);
            this.tbxTextInput.TabIndex = 6;
            this.tbxTextInput.WordWrap = false;
            this.tbxTextInput.TextChanged += new System.EventHandler(this.SourceChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(365, 15);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(41, 23);
            this.btnBrowse.TabIndex = 5;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // tbxUrl
            // 
            this.tbxUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxUrl.Enabled = false;
            this.tbxUrl.Location = new System.Drawing.Point(98, 41);
            this.tbxUrl.Name = "tbxUrl";
            this.tbxUrl.Size = new System.Drawing.Size(308, 20);
            this.tbxUrl.TabIndex = 4;
            this.tbxUrl.TextChanged += new System.EventHandler(this.SourceChanged);
            // 
            // tbxFile
            // 
            this.tbxFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxFile.Location = new System.Drawing.Point(98, 18);
            this.tbxFile.Name = "tbxFile";
            this.tbxFile.Size = new System.Drawing.Size(261, 20);
            this.tbxFile.TabIndex = 3;
            this.tbxFile.TextChanged += new System.EventHandler(this.SourceChanged);
            // 
            // rbtTextInput
            // 
            this.rbtTextInput.AutoSize = true;
            this.rbtTextInput.Location = new System.Drawing.Point(16, 64);
            this.rbtTextInput.Name = "rbtTextInput";
            this.rbtTextInput.Size = new System.Drawing.Size(53, 17);
            this.rbtTextInput.TabIndex = 2;
            this.rbtTextInput.TabStop = true;
            this.rbtTextInput.Text = "s_text";
            this.rbtTextInput.UseVisualStyleBackColor = true;
            this.rbtTextInput.CheckedChanged += new System.EventHandler(this.rbtOnSource_CheckedChanged);
            // 
            // rbtUrl
            // 
            this.rbtUrl.AutoSize = true;
            this.rbtUrl.Location = new System.Drawing.Point(16, 41);
            this.rbtUrl.Name = "rbtUrl";
            this.rbtUrl.Size = new System.Drawing.Size(47, 17);
            this.rbtUrl.TabIndex = 1;
            this.rbtUrl.TabStop = true;
            this.rbtUrl.Text = "s_url";
            this.rbtUrl.UseVisualStyleBackColor = true;
            this.rbtUrl.CheckedChanged += new System.EventHandler(this.rbtOnSource_CheckedChanged);
            // 
            // rbtFile
            // 
            this.rbtFile.AutoSize = true;
            this.rbtFile.Checked = true;
            this.rbtFile.Location = new System.Drawing.Point(16, 18);
            this.rbtFile.Name = "rbtFile";
            this.rbtFile.Size = new System.Drawing.Size(49, 17);
            this.rbtFile.TabIndex = 0;
            this.rbtFile.TabStop = true;
            this.rbtFile.Text = "s_file";
            this.rbtFile.UseVisualStyleBackColor = true;
            this.rbtFile.CheckedChanged += new System.EventHandler(this.rbtOnSource_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridViewRows);
            this.tabPage2.Controls.Add(this.btnPreview);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.tbxLineRegexGroup);
            this.tabPage2.Controls.Add(this.tbxLineRegex);
            this.tabPage2.Controls.Add(this.rbtLineRegex);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.tbxLineLimitEnd);
            this.tabPage2.Controls.Add(this.tbxLineLimitBegin);
            this.tabPage2.Controls.Add(this.chbLineSepRegex);
            this.tabPage2.Controls.Add(this.tbxLineSeparator);
            this.tabPage2.Controls.Add(this.rbtLineLimitation);
            this.tabPage2.Controls.Add(this.rbtLineSeparator);
            this.tabPage2.Controls.Add(this.rbtLines);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(489, 319);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "s_rows";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridViewRows
            // 
            this.dataGridViewRows.AllowUserToAddRows = false;
            this.dataGridViewRows.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewRows.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRows.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridViewRows.Location = new System.Drawing.Point(19, 206);
            this.dataGridViewRows.Name = "dataGridViewRows";
            this.dataGridViewRows.ReadOnly = true;
            this.dataGridViewRows.Size = new System.Drawing.Size(452, 107);
            this.dataGridViewRows.TabIndex = 14;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "s_text";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(19, 177);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(75, 23);
            this.btnPreview.TabIndex = 13;
            this.btnPreview.Text = "s_preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(180, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "s_group";
            // 
            // tbxLineRegexGroup
            // 
            this.tbxLineRegexGroup.Location = new System.Drawing.Point(238, 147);
            this.tbxLineRegexGroup.Name = "tbxLineRegexGroup";
            this.tbxLineRegexGroup.Size = new System.Drawing.Size(178, 20);
            this.tbxLineRegexGroup.TabIndex = 11;
            this.tbxLineRegexGroup.TextChanged += new System.EventHandler(this.AnyTextChanged);
            // 
            // tbxLineRegex
            // 
            this.tbxLineRegex.Location = new System.Drawing.Point(182, 120);
            this.tbxLineRegex.Name = "tbxLineRegex";
            this.tbxLineRegex.Size = new System.Drawing.Size(234, 20);
            this.tbxLineRegex.TabIndex = 10;
            this.tbxLineRegex.TextChanged += new System.EventHandler(this.AnyTextChanged);
            // 
            // rbtLineRegex
            // 
            this.rbtLineRegex.AutoSize = true;
            this.rbtLineRegex.Location = new System.Drawing.Point(19, 121);
            this.rbtLineRegex.Name = "rbtLineRegex";
            this.rbtLineRegex.Size = new System.Drawing.Size(124, 17);
            this.rbtLineRegex.TabIndex = 9;
            this.rbtLineRegex.TabStop = true;
            this.rbtLineRegex.Text = "s_regular_expression";
            this.rbtLineRegex.UseVisualStyleBackColor = true;
            this.rbtLineRegex.CheckedChanged += new System.EventHandler(this.rbtOnRows_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(179, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "s_end";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(179, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "s_begin";
            // 
            // tbxLineLimitEnd
            // 
            this.tbxLineLimitEnd.Location = new System.Drawing.Point(238, 94);
            this.tbxLineLimitEnd.Name = "tbxLineLimitEnd";
            this.tbxLineLimitEnd.Size = new System.Drawing.Size(178, 20);
            this.tbxLineLimitEnd.TabIndex = 6;
            this.tbxLineLimitEnd.TextChanged += new System.EventHandler(this.AnyTextChanged);
            // 
            // tbxLineLimitBegin
            // 
            this.tbxLineLimitBegin.Location = new System.Drawing.Point(238, 71);
            this.tbxLineLimitBegin.Name = "tbxLineLimitBegin";
            this.tbxLineLimitBegin.Size = new System.Drawing.Size(178, 20);
            this.tbxLineLimitBegin.TabIndex = 5;
            this.tbxLineLimitBegin.TextChanged += new System.EventHandler(this.AnyTextChanged);
            // 
            // chbLineSepRegex
            // 
            this.chbLineSepRegex.AutoSize = true;
            this.chbLineSepRegex.Location = new System.Drawing.Point(422, 46);
            this.chbLineSepRegex.Name = "chbLineSepRegex";
            this.chbLineSepRegex.Size = new System.Drawing.Size(52, 17);
            this.chbLineSepRegex.TabIndex = 4;
            this.chbLineSepRegex.Text = "regex";
            this.chbLineSepRegex.UseVisualStyleBackColor = true;
            this.chbLineSepRegex.CheckedChanged += new System.EventHandler(this.AnyTextChanged);
            // 
            // tbxLineSeparator
            // 
            this.tbxLineSeparator.Location = new System.Drawing.Point(179, 46);
            this.tbxLineSeparator.Name = "tbxLineSeparator";
            this.tbxLineSeparator.Size = new System.Drawing.Size(237, 20);
            this.tbxLineSeparator.TabIndex = 3;
            this.tbxLineSeparator.TextChanged += new System.EventHandler(this.AnyTextChanged);
            // 
            // rbtLineLimitation
            // 
            this.rbtLineLimitation.AutoSize = true;
            this.rbtLineLimitation.Location = new System.Drawing.Point(19, 71);
            this.rbtLineLimitation.Name = "rbtLineLimitation";
            this.rbtLineLimitation.Size = new System.Drawing.Size(76, 17);
            this.rbtLineLimitation.TabIndex = 2;
            this.rbtLineLimitation.TabStop = true;
            this.rbtLineLimitation.Text = "s_limitation";
            this.rbtLineLimitation.UseVisualStyleBackColor = true;
            this.rbtLineLimitation.CheckedChanged += new System.EventHandler(this.rbtOnRows_CheckedChanged);
            // 
            // rbtLineSeparator
            // 
            this.rbtLineSeparator.AutoSize = true;
            this.rbtLineSeparator.Location = new System.Drawing.Point(19, 47);
            this.rbtLineSeparator.Name = "rbtLineSeparator";
            this.rbtLineSeparator.Size = new System.Drawing.Size(80, 17);
            this.rbtLineSeparator.TabIndex = 1;
            this.rbtLineSeparator.TabStop = true;
            this.rbtLineSeparator.Text = "s_separator";
            this.rbtLineSeparator.UseVisualStyleBackColor = true;
            this.rbtLineSeparator.CheckedChanged += new System.EventHandler(this.rbtOnRows_CheckedChanged);
            // 
            // rbtLines
            // 
            this.rbtLines.AutoSize = true;
            this.rbtLines.Checked = true;
            this.rbtLines.Location = new System.Drawing.Point(19, 23);
            this.rbtLines.Name = "rbtLines";
            this.rbtLines.Size = new System.Drawing.Size(57, 17);
            this.rbtLines.TabIndex = 0;
            this.rbtLines.TabStop = true;
            this.rbtLines.Text = "s_lines";
            this.rbtLines.UseVisualStyleBackColor = true;
            this.rbtLines.CheckedChanged += new System.EventHandler(this.rbtOnRows_CheckedChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panel1);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.cbxFieldType);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(489, 319);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "s_fields";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(18, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(390, 239);
            this.panel1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "s_type";
            // 
            // cbxFieldType
            // 
            this.cbxFieldType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFieldType.FormattingEnabled = true;
            this.cbxFieldType.Location = new System.Drawing.Point(82, 15);
            this.cbxFieldType.Name = "cbxFieldType";
            this.cbxFieldType.Size = new System.Drawing.Size(185, 21);
            this.cbxFieldType.TabIndex = 0;
            this.cbxFieldType.SelectedIndexChanged += new System.EventHandler(this.cbxFieldType_SelectedIndexChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dataGridViewPreview);
            this.tabPage4.Controls.Add(this.button1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(489, 319);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "s_preview";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "s_refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridViewPreview
            // 
            this.dataGridViewPreview.AllowUserToAddRows = false;
            this.dataGridViewPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPreview.Location = new System.Drawing.Point(6, 35);
            this.dataGridViewPreview.Name = "dataGridViewPreview";
            this.dataGridViewPreview.Size = new System.Drawing.Size(477, 278);
            this.dataGridViewPreview.TabIndex = 2;
            this.dataGridViewPreview.VirtualMode = true;
            // 
            // TextImportFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "TextImportFrame";
            this.Size = new System.Drawing.Size(497, 345);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRows)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RadioButton rbtUrl;
        private System.Windows.Forms.RadioButton rbtFile;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox tbxTextInput;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox tbxUrl;
        private System.Windows.Forms.TextBox tbxFile;
        private System.Windows.Forms.RadioButton rbtTextInput;
        private System.Windows.Forms.RadioButton rbtLineSeparator;
        private System.Windows.Forms.RadioButton rbtLines;
        private System.Windows.Forms.RadioButton rbtLineLimitation;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ComboBox cbxEncoding;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxFieldType;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbxLineRegex;
        private System.Windows.Forms.RadioButton rbtLineRegex;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxLineLimitEnd;
        private System.Windows.Forms.TextBox tbxLineLimitBegin;
        private System.Windows.Forms.CheckBox chbLineSepRegex;
        private System.Windows.Forms.TextBox tbxLineSeparator;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxLineRegexGroup;
        private System.Windows.Forms.DataGridView dataGridViewRows;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button button1;
        private DatAdmin.BinaryGridView dataGridViewPreview;
    }
}
