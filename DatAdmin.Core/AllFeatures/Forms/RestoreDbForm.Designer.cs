namespace DatAdmin
{
    partial class RestoreDbForm
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnExportAsJob = new System.Windows.Forms.Button();
            this.infoBoxFrame1 = new DatAdmin.InfoBoxFrame();
            this.cbxDatabase = new System.Windows.Forms.ComboBox();
            this.btnChooseOtherServer = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxDestServer = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "s_destination_database";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(77, 253);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "s_ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(158, 253);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "s_cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnExportAsJob
            // 
            this.btnExportAsJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportAsJob.Location = new System.Drawing.Point(239, 253);
            this.btnExportAsJob.Name = "btnExportAsJob";
            this.btnExportAsJob.Size = new System.Drawing.Size(116, 23);
            this.btnExportAsJob.TabIndex = 4;
            this.btnExportAsJob.Text = "s_export_as_job";
            this.btnExportAsJob.UseVisualStyleBackColor = true;
            this.btnExportAsJob.Click += new System.EventHandler(this.btnExportAsJob_Click);
            // 
            // infoBoxFrame1
            // 
            this.infoBoxFrame1.InfoText = "s_info_about_restore_database";
            this.infoBoxFrame1.Location = new System.Drawing.Point(12, 12);
            this.infoBoxFrame1.Name = "infoBoxFrame1";
            this.infoBoxFrame1.Padding = new System.Windows.Forms.Padding(8);
            this.infoBoxFrame1.Size = new System.Drawing.Size(338, 83);
            this.infoBoxFrame1.TabIndex = 5;
            // 
            // cbxDatabase
            // 
            this.cbxDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxDatabase.FormattingEnabled = true;
            this.cbxDatabase.Location = new System.Drawing.Point(51, 176);
            this.cbxDatabase.Name = "cbxDatabase";
            this.cbxDatabase.Size = new System.Drawing.Size(304, 21);
            this.cbxDatabase.TabIndex = 6;
            // 
            // btnChooseOtherServer
            // 
            this.btnChooseOtherServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChooseOtherServer.Location = new System.Drawing.Point(158, 215);
            this.btnChooseOtherServer.Name = "btnChooseOtherServer";
            this.btnChooseOtherServer.Size = new System.Drawing.Size(197, 23);
            this.btnChooseOtherServer.TabIndex = 7;
            this.btnChooseOtherServer.Text = "s_choose_another_server";
            this.btnChooseOtherServer.UseVisualStyleBackColor = true;
            this.btnChooseOtherServer.Click += new System.EventHandler(this.btnChooseOtherServer_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "s_destination_server";
            // 
            // tbxDestServer
            // 
            this.tbxDestServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxDestServer.Location = new System.Drawing.Point(51, 125);
            this.tbxDestServer.Name = "tbxDestServer";
            this.tbxDestServer.ReadOnly = true;
            this.tbxDestServer.Size = new System.Drawing.Size(304, 20);
            this.tbxDestServer.TabIndex = 9;
            // 
            // RestoreDbForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 288);
            this.Controls.Add(this.tbxDestServer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnChooseOtherServer);
            this.Controls.Add(this.cbxDatabase);
            this.Controls.Add(this.infoBoxFrame1);
            this.Controls.Add(this.btnExportAsJob);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "RestoreDbForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "s_restore";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RestoreDbForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnExportAsJob;
        private InfoBoxFrame infoBoxFrame1;
        private System.Windows.Forms.ComboBox cbxDatabase;
        private System.Windows.Forms.Button btnChooseOtherServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxDestServer;
    }
}