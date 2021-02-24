namespace DatAdmin
{
    partial class ImportSqlDumpForm
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
            this.tbxFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSqlFile = new System.Windows.Forms.Button();
            this.cbxContinueOnErrors = new System.Windows.Forms.CheckBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnExportAsJob = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxLoader = new System.Windows.Forms.ComboBox();
            this.propertyFrame1 = new DatAdmin.PropertyFrame();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbxFileName
            // 
            this.tbxFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxFileName.Location = new System.Drawing.Point(126, 82);
            this.tbxFileName.Name = "tbxFileName";
            this.tbxFileName.ReadOnly = true;
            this.tbxFileName.Size = new System.Drawing.Size(330, 20);
            this.tbxFileName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "s_sql_file";
            // 
            // btnSqlFile
            // 
            this.btnSqlFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSqlFile.Location = new System.Drawing.Point(459, 80);
            this.btnSqlFile.Name = "btnSqlFile";
            this.btnSqlFile.Size = new System.Drawing.Size(47, 23);
            this.btnSqlFile.TabIndex = 2;
            this.btnSqlFile.Text = "...";
            this.btnSqlFile.UseVisualStyleBackColor = true;
            this.btnSqlFile.Click += new System.EventHandler(this.btnSqlFile_Click);
            // 
            // cbxContinueOnErrors
            // 
            this.cbxContinueOnErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxContinueOnErrors.AutoSize = true;
            this.cbxContinueOnErrors.Checked = true;
            this.cbxContinueOnErrors.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxContinueOnErrors.Location = new System.Drawing.Point(12, 376);
            this.cbxContinueOnErrors.Name = "cbxContinueOnErrors";
            this.cbxContinueOnErrors.Size = new System.Drawing.Size(128, 17);
            this.cbxContinueOnErrors.TabIndex = 3;
            this.cbxContinueOnErrors.Text = "s_continue_on_errors";
            this.cbxContinueOnErrors.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(65, 13);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(382, 66);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = "s_import_sql_dump_desc";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DatAdmin.StdIcons.import_sql_dump;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(12, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(47, 39);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(350, 399);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "s_ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(431, 399);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "s_cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnExportAsJob
            // 
            this.btnExportAsJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportAsJob.Location = new System.Drawing.Point(185, 399);
            this.btnExportAsJob.Name = "btnExportAsJob";
            this.btnExportAsJob.Size = new System.Drawing.Size(159, 23);
            this.btnExportAsJob.TabIndex = 8;
            this.btnExportAsJob.Text = "s_export_as_job";
            this.btnExportAsJob.UseVisualStyleBackColor = true;
            this.btnExportAsJob.Click += new System.EventHandler(this.btnExportAsJob_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "s_dump_loader";
            // 
            // cbxLoader
            // 
            this.cbxLoader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxLoader.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLoader.FormattingEnabled = true;
            this.cbxLoader.Location = new System.Drawing.Point(126, 108);
            this.cbxLoader.Name = "cbxLoader";
            this.cbxLoader.Size = new System.Drawing.Size(330, 21);
            this.cbxLoader.TabIndex = 10;
            this.cbxLoader.SelectedIndexChanged += new System.EventHandler(this.cbxLoader_SelectedIndexChanged);
            // 
            // propertyFrame1
            // 
            this.propertyFrame1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyFrame1.Location = new System.Drawing.Point(12, 160);
            this.propertyFrame1.Name = "propertyFrame1";
            this.propertyFrame1.SelectedObject = null;
            this.propertyFrame1.Size = new System.Drawing.Size(497, 210);
            this.propertyFrame1.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "s_loader_properties";
            // 
            // ImportSqlDumpForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(521, 437);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.propertyFrame1);
            this.Controls.Add(this.cbxLoader);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnExportAsJob);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.cbxContinueOnErrors);
            this.Controls.Add(this.btnSqlFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxFileName);
            this.Name = "ImportSqlDumpForm";
            this.Text = "s_import_sql_dump";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSqlFile;
        private System.Windows.Forms.CheckBox cbxContinueOnErrors;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnExportAsJob;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxLoader;
        private PropertyFrame propertyFrame1;
        private System.Windows.Forms.Label label3;
    }
}