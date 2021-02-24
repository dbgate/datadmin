namespace DatAdmin
{
    partial class SendUsageForm
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnViewUsageFolder = new System.Windows.Forms.Button();
            this.cbxAllowUpload = new System.Windows.Forms.CheckBox();
            this.cbxDontAskNext = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(448, 127);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "s_info_about_usage_stats";
            // 
            // btnViewUsageFolder
            // 
            this.btnViewUsageFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewUsageFolder.Location = new System.Drawing.Point(12, 145);
            this.btnViewUsageFolder.Name = "btnViewUsageFolder";
            this.btnViewUsageFolder.Size = new System.Drawing.Size(448, 23);
            this.btnViewUsageFolder.TabIndex = 1;
            this.btnViewUsageFolder.Text = "s_view_usage_data";
            this.btnViewUsageFolder.UseVisualStyleBackColor = true;
            this.btnViewUsageFolder.Click += new System.EventHandler(this.btnViewUsageFolder_Click);
            // 
            // cbxAllowUpload
            // 
            this.cbxAllowUpload.AutoSize = true;
            this.cbxAllowUpload.Checked = true;
            this.cbxAllowUpload.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxAllowUpload.Location = new System.Drawing.Point(12, 174);
            this.cbxAllowUpload.Name = "cbxAllowUpload";
            this.cbxAllowUpload.Size = new System.Drawing.Size(99, 17);
            this.cbxAllowUpload.TabIndex = 2;
            this.cbxAllowUpload.Text = "s_allow_upload";
            this.cbxAllowUpload.UseVisualStyleBackColor = true;
            // 
            // cbxDontAskNext
            // 
            this.cbxDontAskNext.AutoSize = true;
            this.cbxDontAskNext.Location = new System.Drawing.Point(13, 198);
            this.cbxDontAskNext.Name = "cbxDontAskNext";
            this.cbxDontAskNext.Size = new System.Drawing.Size(126, 17);
            this.cbxDontAskNext.TabIndex = 3;
            this.cbxDontAskNext.Text = "s_dont_ask_nexttime";
            this.cbxDontAskNext.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(385, 224);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "s_close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // SendUsageForm
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(472, 259);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cbxDontAskNext);
            this.Controls.Add(this.cbxAllowUpload);
            this.Controls.Add(this.btnViewUsageFolder);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SendUsageForm";
            this.Text = "s_send_usage";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SendUsageForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnViewUsageFolder;
        private System.Windows.Forms.CheckBox cbxAllowUpload;
        private System.Windows.Forms.CheckBox cbxDontAskNext;
        private System.Windows.Forms.Button btnClose;
    }
}