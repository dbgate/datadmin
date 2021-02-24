namespace Plugin.mssql
{
    partial class MsSqlRestoreDialog
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
            this.btnMakeScript = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSource = new System.Windows.Forms.TabPage();
            this.tbxFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxBackup = new System.Windows.Forms.ComboBox();
            this.cbxDatabase = new System.Windows.Forms.ComboBox();
            this.rbtFile = new System.Windows.Forms.RadioButton();
            this.rbtDatabase = new System.Windows.Forms.RadioButton();
            this.tabTarget = new System.Windows.Forms.TabPage();
            this.chbOverrideDbFileNames = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbxDatabase = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabSource.SuspendLayout();
            this.tabTarget.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnMakeScript
            // 
            this.btnMakeScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMakeScript.Location = new System.Drawing.Point(349, 283);
            this.btnMakeScript.Name = "btnMakeScript";
            this.btnMakeScript.Size = new System.Drawing.Size(75, 23);
            this.btnMakeScript.TabIndex = 19;
            this.btnMakeScript.Text = "SQL Script";
            this.btnMakeScript.UseVisualStyleBackColor = true;
            this.btnMakeScript.Click += new System.EventHandler(this.btnMakeScript_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(512, 283);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(431, 283);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 17;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabSource);
            this.tabControl1.Controls.Add(this.tabTarget);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(575, 256);
            this.tabControl1.TabIndex = 20;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabSource
            // 
            this.tabSource.Controls.Add(this.tbxFile);
            this.tabSource.Controls.Add(this.label2);
            this.tabSource.Controls.Add(this.cbxBackup);
            this.tabSource.Controls.Add(this.cbxDatabase);
            this.tabSource.Controls.Add(this.rbtFile);
            this.tabSource.Controls.Add(this.rbtDatabase);
            this.tabSource.Location = new System.Drawing.Point(4, 22);
            this.tabSource.Name = "tabSource";
            this.tabSource.Padding = new System.Windows.Forms.Padding(3);
            this.tabSource.Size = new System.Drawing.Size(567, 230);
            this.tabSource.TabIndex = 1;
            this.tabSource.Text = "Source";
            this.tabSource.UseVisualStyleBackColor = true;
            // 
            // tbxFile
            // 
            this.tbxFile.Location = new System.Drawing.Point(128, 75);
            this.tbxFile.Name = "tbxFile";
            this.tbxFile.Size = new System.Drawing.Size(280, 20);
            this.tbxFile.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Backup";
            // 
            // cbxBackup
            // 
            this.cbxBackup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBackup.FormattingEnabled = true;
            this.cbxBackup.Location = new System.Drawing.Point(128, 47);
            this.cbxBackup.Name = "cbxBackup";
            this.cbxBackup.Size = new System.Drawing.Size(280, 21);
            this.cbxBackup.TabIndex = 3;
            // 
            // cbxDatabase
            // 
            this.cbxDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDatabase.FormattingEnabled = true;
            this.cbxDatabase.Location = new System.Drawing.Point(128, 19);
            this.cbxDatabase.Name = "cbxDatabase";
            this.cbxDatabase.Size = new System.Drawing.Size(280, 21);
            this.cbxDatabase.TabIndex = 2;
            this.cbxDatabase.SelectedIndexChanged += new System.EventHandler(this.cbxDatabase_SelectedIndexChanged);
            // 
            // rbtFile
            // 
            this.rbtFile.AutoSize = true;
            this.rbtFile.Location = new System.Drawing.Point(17, 79);
            this.rbtFile.Name = "rbtFile";
            this.rbtFile.Size = new System.Drawing.Size(41, 17);
            this.rbtFile.TabIndex = 1;
            this.rbtFile.Text = "File";
            this.rbtFile.UseVisualStyleBackColor = true;
            this.rbtFile.CheckedChanged += new System.EventHandler(this.rbtDatabase_CheckedChanged);
            // 
            // rbtDatabase
            // 
            this.rbtDatabase.AutoSize = true;
            this.rbtDatabase.Checked = true;
            this.rbtDatabase.Location = new System.Drawing.Point(17, 19);
            this.rbtDatabase.Name = "rbtDatabase";
            this.rbtDatabase.Size = new System.Drawing.Size(71, 17);
            this.rbtDatabase.TabIndex = 0;
            this.rbtDatabase.TabStop = true;
            this.rbtDatabase.Text = "Database";
            this.rbtDatabase.UseVisualStyleBackColor = true;
            this.rbtDatabase.CheckedChanged += new System.EventHandler(this.rbtDatabase_CheckedChanged);
            // 
            // tabTarget
            // 
            this.tabTarget.Controls.Add(this.chbOverrideDbFileNames);
            this.tabTarget.Controls.Add(this.dataGridView1);
            this.tabTarget.Controls.Add(this.tbxDatabase);
            this.tabTarget.Controls.Add(this.label1);
            this.tabTarget.Location = new System.Drawing.Point(4, 22);
            this.tabTarget.Name = "tabTarget";
            this.tabTarget.Padding = new System.Windows.Forms.Padding(3);
            this.tabTarget.Size = new System.Drawing.Size(567, 230);
            this.tabTarget.TabIndex = 0;
            this.tabTarget.Text = "Target";
            this.tabTarget.UseVisualStyleBackColor = true;
            // 
            // chbOverrideDbFileNames
            // 
            this.chbOverrideDbFileNames.AutoSize = true;
            this.chbOverrideDbFileNames.Location = new System.Drawing.Point(10, 57);
            this.chbOverrideDbFileNames.Name = "chbOverrideDbFileNames";
            this.chbOverrideDbFileNames.Size = new System.Drawing.Size(163, 17);
            this.chbOverrideDbFileNames.TabIndex = 8;
            this.chbOverrideDbFileNames.Text = "Override database file names";
            this.chbOverrideDbFileNames.UseVisualStyleBackColor = true;
            this.chbOverrideDbFileNames.CheckedChanged += new System.EventHandler(this.chbOverrideDbFileNames_CheckedChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column1,
            this.Column2});
            this.dataGridView1.Location = new System.Drawing.Point(7, 92);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(554, 110);
            this.dataGridView1.TabIndex = 7;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Logical name";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Source file";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 200;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Target file";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 200;
            // 
            // tbxDatabase
            // 
            this.tbxDatabase.Location = new System.Drawing.Point(153, 10);
            this.tbxDatabase.Name = "tbxDatabase";
            this.tbxDatabase.Size = new System.Drawing.Size(220, 20);
            this.tbxDatabase.TabIndex = 6;
            this.tbxDatabase.TextChanged += new System.EventHandler(this.tbxDatabase_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Destination database";
            // 
            // MsSqlRestoreDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(599, 318);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnMakeScript);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MsSqlRestoreDialog";
            this.Text = "Restore";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MsSqlRestoreDialog_FormClosed);
            this.tabControl1.ResumeLayout(false);
            this.tabSource.ResumeLayout(false);
            this.tabSource.PerformLayout();
            this.tabTarget.ResumeLayout(false);
            this.tabTarget.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMakeScript;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabTarget;
        private System.Windows.Forms.TabPage tabSource;
        private System.Windows.Forms.CheckBox chbOverrideDbFileNames;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.TextBox tbxDatabase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxBackup;
        private System.Windows.Forms.ComboBox cbxDatabase;
        private System.Windows.Forms.RadioButton rbtFile;
        private System.Windows.Forms.RadioButton rbtDatabase;
    }
}