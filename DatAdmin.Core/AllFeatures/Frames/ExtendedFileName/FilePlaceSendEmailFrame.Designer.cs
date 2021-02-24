namespace DatAdmin
{
    partial class FilePlaceSendEmailFrame
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxBody = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxAttachment = new System.Windows.Forms.TextBox();
            this.tbxSubject = new DatAdmin.NameTemplateCompactFrame();
            this.emailToFrame1 = new DatAdmin.EmailToFrame();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "s_subject";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "s_email_to";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "s_body";
            // 
            // tbxBody
            // 
            this.tbxBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxBody.Location = new System.Drawing.Point(70, 54);
            this.tbxBody.Multiline = true;
            this.tbxBody.Name = "tbxBody";
            this.tbxBody.Size = new System.Drawing.Size(290, 122);
            this.tbxBody.TabIndex = 2;
            this.tbxBody.TextChanged += new System.EventHandler(this.tbxBody_TextChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "s_attachment_name";
            // 
            // tbxAttachment
            // 
            this.tbxAttachment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxAttachment.Location = new System.Drawing.Point(119, 182);
            this.tbxAttachment.Name = "tbxAttachment";
            this.tbxAttachment.Size = new System.Drawing.Size(241, 20);
            this.tbxAttachment.TabIndex = 3;
            this.tbxAttachment.TextChanged += new System.EventHandler(this.tbxAttachment_TextChanged);
            // 
            // tbxSubject
            // 
            this.tbxSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxSubject.Location = new System.Drawing.Point(70, 28);
            this.tbxSubject.Name = "tbxSubject";
            this.tbxSubject.NameTemplate = "";
            this.tbxSubject.Size = new System.Drawing.Size(287, 20);
            this.tbxSubject.TabIndex = 1;
            this.tbxSubject.NameTemplateChanged += new System.EventHandler(this.tbxSubject_NameTemplateChanged);
            // 
            // emailToFrame1
            // 
            this.emailToFrame1.EmailTo = "";
            this.emailToFrame1.Location = new System.Drawing.Point(70, 2);
            this.emailToFrame1.Name = "emailToFrame1";
            this.emailToFrame1.Size = new System.Drawing.Size(287, 20);
            this.emailToFrame1.TabIndex = 7;
            this.emailToFrame1.EmailToChanged += new System.EventHandler(this.tbxTo_TextChanged);
            // 
            // FilePlaceSendEmailFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.emailToFrame1);
            this.Controls.Add(this.tbxSubject);
            this.Controls.Add(this.tbxAttachment);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbxBody);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FilePlaceSendEmailFrame";
            this.Size = new System.Drawing.Size(360, 205);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxBody;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxAttachment;
        private NameTemplateCompactFrame tbxSubject;
        private DatAdmin.EmailToFrame emailToFrame1;
    }
}
