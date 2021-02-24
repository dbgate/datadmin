namespace Plugin.mssql
{
    partial class MsSqlBackupDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbxDatabase = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxBackupType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbxBackupSetName = new System.Windows.Forms.TextBox();
            this.tbxBackupSetDescription = new System.Windows.Forms.TextBox();
            this.tbxBackupSetFolder = new System.Windows.Forms.TextBox();
            this.tbxBackupSetFile = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnMakeScript = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Database";
            // 
            // tbxDatabase
            // 
            this.tbxDatabase.Location = new System.Drawing.Point(144, 23);
            this.tbxDatabase.Name = "tbxDatabase";
            this.tbxDatabase.ReadOnly = true;
            this.tbxDatabase.Size = new System.Drawing.Size(191, 20);
            this.tbxDatabase.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Backup type";
            // 
            // cbxBackupType
            // 
            this.cbxBackupType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBackupType.FormattingEnabled = true;
            this.cbxBackupType.Items.AddRange(new object[] {
            "Full",
            "Differential",
            "Transaction Log"});
            this.cbxBackupType.Location = new System.Drawing.Point(144, 50);
            this.cbxBackupType.Name = "cbxBackupType";
            this.cbxBackupType.Size = new System.Drawing.Size(191, 21);
            this.cbxBackupType.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Backup Set";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Description";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 198);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Destination";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 227);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Folder";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(39, 253);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "File";
            // 
            // tbxBackupSetName
            // 
            this.tbxBackupSetName.Location = new System.Drawing.Point(144, 135);
            this.tbxBackupSetName.Name = "tbxBackupSetName";
            this.tbxBackupSetName.Size = new System.Drawing.Size(191, 20);
            this.tbxBackupSetName.TabIndex = 10;
            // 
            // tbxBackupSetDescription
            // 
            this.tbxBackupSetDescription.Location = new System.Drawing.Point(144, 161);
            this.tbxBackupSetDescription.Name = "tbxBackupSetDescription";
            this.tbxBackupSetDescription.Size = new System.Drawing.Size(191, 20);
            this.tbxBackupSetDescription.TabIndex = 11;
            // 
            // tbxBackupSetFolder
            // 
            this.tbxBackupSetFolder.Location = new System.Drawing.Point(144, 224);
            this.tbxBackupSetFolder.Name = "tbxBackupSetFolder";
            this.tbxBackupSetFolder.Size = new System.Drawing.Size(191, 20);
            this.tbxBackupSetFolder.TabIndex = 12;
            // 
            // tbxBackupSetFile
            // 
            this.tbxBackupSetFile.Location = new System.Drawing.Point(144, 250);
            this.tbxBackupSetFile.Name = "tbxBackupSetFile";
            this.tbxBackupSetFile.Size = new System.Drawing.Size(191, 20);
            this.tbxBackupSetFile.TabIndex = 13;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(179, 304);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 14;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(260, 304);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnMakeScript
            // 
            this.btnMakeScript.Location = new System.Drawing.Point(97, 304);
            this.btnMakeScript.Name = "btnMakeScript";
            this.btnMakeScript.Size = new System.Drawing.Size(75, 23);
            this.btnMakeScript.TabIndex = 16;
            this.btnMakeScript.Text = "SQL Script";
            this.btnMakeScript.UseVisualStyleBackColor = true;
            this.btnMakeScript.Click += new System.EventHandler(this.btnMakeScript_Click);
            // 
            // MsSqlBackupDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(355, 339);
            this.Controls.Add(this.btnMakeScript);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tbxBackupSetFile);
            this.Controls.Add(this.tbxBackupSetFolder);
            this.Controls.Add(this.tbxBackupSetDescription);
            this.Controls.Add(this.tbxBackupSetName);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbxBackupType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxDatabase);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MsSqlBackupDialog";
            this.Text = "Backup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxDatabase;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxBackupType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbxBackupSetName;
        private System.Windows.Forms.TextBox tbxBackupSetDescription;
        private System.Windows.Forms.TextBox tbxBackupSetFolder;
        private System.Windows.Forms.TextBox tbxBackupSetFile;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnMakeScript;
    }
}