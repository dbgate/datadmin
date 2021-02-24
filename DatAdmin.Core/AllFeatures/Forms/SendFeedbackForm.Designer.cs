namespace DatAdmin
{
    partial class SendFeedbackForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxSubject = new System.Windows.Forms.TextBox();
            this.tbxBody = new System.Windows.Forms.TextBox();
            this.chbSendMeAnswer = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxEmail = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.infoBoxFrame1 = new DatAdmin.InfoBoxFrame();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(406, 323);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "s_cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "s_subject";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "s_body";
            // 
            // tbxSubject
            // 
            this.tbxSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxSubject.Location = new System.Drawing.Point(91, 96);
            this.tbxSubject.Name = "tbxSubject";
            this.tbxSubject.Size = new System.Drawing.Size(390, 20);
            this.tbxSubject.TabIndex = 3;
            // 
            // tbxBody
            // 
            this.tbxBody.AcceptsReturn = true;
            this.tbxBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxBody.Location = new System.Drawing.Point(91, 122);
            this.tbxBody.Multiline = true;
            this.tbxBody.Name = "tbxBody";
            this.tbxBody.Size = new System.Drawing.Size(390, 137);
            this.tbxBody.TabIndex = 4;
            // 
            // chbSendMeAnswer
            // 
            this.chbSendMeAnswer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbSendMeAnswer.AutoSize = true;
            this.chbSendMeAnswer.Location = new System.Drawing.Point(12, 265);
            this.chbSendMeAnswer.Name = "chbSendMeAnswer";
            this.chbSendMeAnswer.Size = new System.Drawing.Size(137, 17);
            this.chbSendMeAnswer.TabIndex = 5;
            this.chbSendMeAnswer.Text = "s_please_send_answer";
            this.chbSendMeAnswer.UseVisualStyleBackColor = true;
            this.chbSendMeAnswer.CheckedChanged += new System.EventHandler(this.chbSendMeAnswer_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 289);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "s_email";
            // 
            // tbxEmail
            // 
            this.tbxEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxEmail.Enabled = false;
            this.tbxEmail.Location = new System.Drawing.Point(91, 289);
            this.tbxEmail.Name = "tbxEmail";
            this.tbxEmail.Size = new System.Drawing.Size(390, 20);
            this.tbxEmail.TabIndex = 7;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(325, 323);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "s_ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // infoBoxFrame1
            // 
            this.infoBoxFrame1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.infoBoxFrame1.InfoText = "s_send_feedback_info";
            this.infoBoxFrame1.Location = new System.Drawing.Point(12, 12);
            this.infoBoxFrame1.Name = "infoBoxFrame1";
            this.infoBoxFrame1.Padding = new System.Windows.Forms.Padding(8);
            this.infoBoxFrame1.Size = new System.Drawing.Size(469, 78);
            this.infoBoxFrame1.TabIndex = 9;
            // 
            // SendFeedbackForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(493, 358);
            this.Controls.Add(this.infoBoxFrame1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tbxEmail);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chbSendMeAnswer);
            this.Controls.Add(this.tbxBody);
            this.Controls.Add(this.tbxSubject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Name = "SendFeedbackForm";
            this.Text = "s_send_feedback";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SendFeedbackForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxSubject;
        private System.Windows.Forms.TextBox tbxBody;
        private System.Windows.Forms.CheckBox chbSendMeAnswer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxEmail;
        private System.Windows.Forms.Button btnOk;
        private InfoBoxFrame infoBoxFrame1;
    }
}