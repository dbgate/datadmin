namespace DatAdmin
{
    partial class LoadDataErrorForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxDetails = new System.Windows.Forms.TextBox();
            this.btnTryAgain = new System.Windows.Forms.Button();
            this.btnClearSettings = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbxSendError = new System.Windows.Forms.CheckBox();
            this.chbShowDetails = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DatAdmin.StdIcons.error_big;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(41, 37);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "s_error_in_loading_data";
            // 
            // tbxDetails
            // 
            this.tbxDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxDetails.Location = new System.Drawing.Point(13, 55);
            this.tbxDetails.Multiline = true;
            this.tbxDetails.Name = "tbxDetails";
            this.tbxDetails.ReadOnly = true;
            this.tbxDetails.Size = new System.Drawing.Size(550, 110);
            this.tbxDetails.TabIndex = 3;
            // 
            // btnTryAgain
            // 
            this.btnTryAgain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTryAgain.Location = new System.Drawing.Point(13, 204);
            this.btnTryAgain.Name = "btnTryAgain";
            this.btnTryAgain.Size = new System.Drawing.Size(104, 23);
            this.btnTryAgain.TabIndex = 4;
            this.btnTryAgain.Text = "s_try_again";
            this.btnTryAgain.UseVisualStyleBackColor = true;
            this.btnTryAgain.Click += new System.EventHandler(this.btnTryAgain_Click);
            // 
            // btnClearSettings
            // 
            this.btnClearSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearSettings.Location = new System.Drawing.Point(12, 233);
            this.btnClearSettings.Name = "btnClearSettings";
            this.btnClearSettings.Size = new System.Drawing.Size(103, 23);
            this.btnClearSettings.TabIndex = 5;
            this.btnClearSettings.Text = "s_reset_settings";
            this.btnClearSettings.UseVisualStyleBackColor = true;
            this.btnClearSettings.Click += new System.EventHandler(this.btnClearSettings_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(12, 264);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(103, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "s_cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbxSendError
            // 
            this.cbxSendError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxSendError.AutoSize = true;
            this.cbxSendError.Location = new System.Drawing.Point(13, 181);
            this.cbxSendError.Name = "cbxSendError";
            this.cbxSendError.Size = new System.Drawing.Size(132, 17);
            this.cbxSendError.TabIndex = 7;
            this.cbxSendError.Text = "s_allow_to_send_error";
            this.cbxSendError.UseVisualStyleBackColor = true;
            // 
            // chbShowDetails
            // 
            this.chbShowDetails.AutoSize = true;
            this.chbShowDetails.Checked = true;
            this.chbShowDetails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbShowDetails.Location = new System.Drawing.Point(74, 32);
            this.chbShowDetails.Name = "chbShowDetails";
            this.chbShowDetails.Size = new System.Drawing.Size(98, 17);
            this.chbShowDetails.TabIndex = 8;
            this.chbShowDetails.Text = "s_show_details";
            this.chbShowDetails.UseVisualStyleBackColor = true;
            this.chbShowDetails.CheckedChanged += new System.EventHandler(this.chbShowDetails_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "s_tblerr_try_again_hlp";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(136, 238);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "s_tblerr_clear_settings_hlp";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(136, 269);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "s_tblerr_cancel_hlp";
            // 
            // LoadDataErrorForm
            // 
            this.AcceptButton = this.btnTryAgain;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(575, 299);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chbShowDetails);
            this.Controls.Add(this.cbxSendError);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClearSettings);
            this.Controls.Add(this.btnTryAgain);
            this.Controls.Add(this.tbxDetails);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "LoadDataErrorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "s_error";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LoadDataErrorForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxDetails;
        private System.Windows.Forms.Button btnTryAgain;
        private System.Windows.Forms.Button btnClearSettings;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbxSendError;
        private System.Windows.Forms.CheckBox chbShowDetails;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}