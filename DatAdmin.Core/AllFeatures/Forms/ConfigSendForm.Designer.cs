namespace DatAdmin
{
    partial class ConfigSendForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigSendForm));
            this.configSelectionFrame1 = new DatAdmin.ConfigSelectionFrame();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxSubject = new System.Windows.Forms.TextBox();
            this.emailToFrame1 = new DatAdmin.EmailToFrame();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxBody = new System.Windows.Forms.TextBox();
            this.chbAddHowTo = new System.Windows.Forms.CheckBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.emailFromFrame1 = new DatAdmin.EmailFromFrame();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // configSelectionFrame1
            // 
            this.configSelectionFrame1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.configSelectionFrame1.FileSystem = null;
            this.configSelectionFrame1.Location = new System.Drawing.Point(12, 34);
            this.configSelectionFrame1.Name = "configSelectionFrame1";
            this.configSelectionFrame1.Size = new System.Drawing.Size(373, 401);
            this.configSelectionFrame1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "s_configuration_to_be_sent";
            // 
            // tbxSubject
            // 
            this.tbxSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxSubject.Location = new System.Drawing.Point(409, 133);
            this.tbxSubject.Name = "tbxSubject";
            this.tbxSubject.Size = new System.Drawing.Size(312, 20);
            this.tbxSubject.TabIndex = 4;
            // 
            // emailToFrame1
            // 
            this.emailToFrame1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.emailToFrame1.EmailTo = "";
            this.emailToFrame1.Location = new System.Drawing.Point(409, 94);
            this.emailToFrame1.Name = "emailToFrame1";
            this.emailToFrame1.Size = new System.Drawing.Size(312, 20);
            this.emailToFrame1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(406, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "s_email_to";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(406, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "s_subject";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(406, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "s_body";
            // 
            // tbxBody
            // 
            this.tbxBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxBody.Location = new System.Drawing.Point(409, 172);
            this.tbxBody.Multiline = true;
            this.tbxBody.Name = "tbxBody";
            this.tbxBody.Size = new System.Drawing.Size(312, 240);
            this.tbxBody.TabIndex = 5;
            this.tbxBody.WordWrap = false;
            // 
            // chbAddHowTo
            // 
            this.chbAddHowTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbAddHowTo.AutoSize = true;
            this.chbAddHowTo.Checked = true;
            this.chbAddHowTo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbAddHowTo.Location = new System.Drawing.Point(409, 418);
            this.chbAddHowTo.Name = "chbAddHowTo";
            this.chbAddHowTo.Size = new System.Drawing.Size(90, 17);
            this.chbAddHowTo.TabIndex = 8;
            this.chbAddHowTo.Text = "s_add_howto";
            this.chbAddHowTo.UseVisualStyleBackColor = true;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(565, 446);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 10;
            this.btnSend.Text = "s_send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(646, 446);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "s_cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(517, 418);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(55, 13);
            this.linkLabel1.TabIndex = 12;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "s_preview";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(406, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "s_compose_email";
            // 
            // emailFromFrame1
            // 
            this.emailFromFrame1.Location = new System.Drawing.Point(409, 55);
            this.emailFromFrame1.Name = "emailFromFrame1";
            this.emailFromFrame1.Size = new System.Drawing.Size(312, 20);
            this.emailFromFrame1.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(406, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "s_email_from";
            // 
            // ConfigSendForm
            // 
            this.AcceptButton = this.btnSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(733, 481);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.emailFromFrame1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.chbAddHowTo);
            this.Controls.Add(this.tbxBody);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.emailToFrame1);
            this.Controls.Add(this.tbxSubject);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.configSelectionFrame1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigSendForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "s_send_configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ConfigSelectionFrame configSelectionFrame1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxSubject;
        private DatAdmin.EmailToFrame emailToFrame1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxBody;
        private System.Windows.Forms.CheckBox chbAddHowTo;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label5;
        private EmailFromFrame emailFromFrame1;
        private System.Windows.Forms.Label label6;
    }
}