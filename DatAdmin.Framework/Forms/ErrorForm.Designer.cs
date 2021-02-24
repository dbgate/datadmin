namespace DatAdmin
{
    partial class ErrorForm
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
            this.labText = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbxSendError = new System.Windows.Forms.CheckBox();
            this.labMessage = new System.Windows.Forms.TextBox();
            this.chbShowDetails = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSupportRequest = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labText
            // 
            this.labText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labText.Location = new System.Drawing.Point(12, 109);
            this.labText.Multiline = true;
            this.labText.Name = "labText";
            this.labText.ReadOnly = true;
            this.labText.Size = new System.Drawing.Size(551, 206);
            this.labText.TabIndex = 0;
            this.labText.WordWrap = false;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOk.Location = new System.Drawing.Point(488, 321);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "s_ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DatAdmin.StdIcons.error_big;
            this.pictureBox1.Location = new System.Drawing.Point(12, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(39, 36);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // cbxSendError
            // 
            this.cbxSendError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxSendError.AutoSize = true;
            this.cbxSendError.Checked = true;
            this.cbxSendError.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxSendError.Location = new System.Drawing.Point(13, 321);
            this.cbxSendError.Name = "cbxSendError";
            this.cbxSendError.Size = new System.Drawing.Size(132, 17);
            this.cbxSendError.TabIndex = 6;
            this.cbxSendError.Text = "s_allow_to_send_error";
            this.cbxSendError.UseVisualStyleBackColor = true;
            // 
            // labMessage
            // 
            this.labMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.labMessage.Location = new System.Drawing.Point(71, 25);
            this.labMessage.Multiline = true;
            this.labMessage.Name = "labMessage";
            this.labMessage.ReadOnly = true;
            this.labMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.labMessage.Size = new System.Drawing.Size(492, 55);
            this.labMessage.TabIndex = 9;
            this.labMessage.Text = "labMessage";
            // 
            // chbShowDetails
            // 
            this.chbShowDetails.AutoSize = true;
            this.chbShowDetails.Checked = true;
            this.chbShowDetails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbShowDetails.Location = new System.Drawing.Point(13, 86);
            this.chbShowDetails.Name = "chbShowDetails";
            this.chbShowDetails.Size = new System.Drawing.Size(98, 17);
            this.chbShowDetails.TabIndex = 10;
            this.chbShowDetails.Text = "s_show_details";
            this.chbShowDetails.UseVisualStyleBackColor = true;
            this.chbShowDetails.CheckedChanged += new System.EventHandler(this.chbShowDetails_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "s_unexpected_error_occured";
            // 
            // btnSupportRequest
            // 
            this.btnSupportRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSupportRequest.AutoSize = true;
            this.btnSupportRequest.Location = new System.Drawing.Point(226, 322);
            this.btnSupportRequest.Name = "btnSupportRequest";
            this.btnSupportRequest.Size = new System.Drawing.Size(94, 13);
            this.btnSupportRequest.TabIndex = 12;
            this.btnSupportRequest.TabStop = true;
            this.btnSupportRequest.Text = "s_support_request";
            this.btnSupportRequest.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnSupportRequest_LinkClicked);
            // 
            // ErrorForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOk;
            this.ClientSize = new System.Drawing.Size(575, 356);
            this.Controls.Add(this.btnSupportRequest);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chbShowDetails);
            this.Controls.Add(this.labMessage);
            this.Controls.Add(this.cbxSendError);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.labText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ErrorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "s_error";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ErrorForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox labText;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox cbxSendError;
        private System.Windows.Forms.TextBox labMessage;
        private System.Windows.Forms.CheckBox chbShowDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel btnSupportRequest;
    }
}