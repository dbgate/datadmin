namespace DatAdmin
{
    partial class SqlConfirmForm
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
            DatAdmin.GenericDialect genericDialect1 = new DatAdmin.GenericDialect();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlConfirmForm));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.codeEditor1 = new DatAdmin.CodeEditor();
            this.messageLogFrame1 = new DatAdmin.MessageLogFrame();
            this.label2 = new System.Windows.Forms.Label();
            this.chbDontShowAgain = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(468, 418);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "s_ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(549, 418);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "s_cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "s_really_run_sql";
            // 
            // codeEditor1
            // 
            this.codeEditor1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.codeEditor1.Dialect = genericDialect1;
            this.codeEditor1.IsReadOnly = true;
            this.codeEditor1.Language = DatAdmin.CodeLanguage.Sql;
            this.codeEditor1.Location = new System.Drawing.Point(12, 26);
            this.codeEditor1.Margin = new System.Windows.Forms.Padding(4);
            this.codeEditor1.Modified = false;
            this.codeEditor1.Name = "codeEditor1";
            this.codeEditor1.ReadOnly = true;
            this.codeEditor1.Size = new System.Drawing.Size(612, 289);
            this.codeEditor1.TabIndex = 0;
            // 
            // messageLogFrame1
            // 
            this.messageLogFrame1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.messageLogFrame1.Location = new System.Drawing.Point(13, 340);
            this.messageLogFrame1.Name = "messageLogFrame1";
            this.messageLogFrame1.Size = new System.Drawing.Size(613, 72);
            this.messageLogFrame1.Source = null;
            this.messageLogFrame1.Style = DatAdmin.MessageFrameStyle.Simple;
            this.messageLogFrame1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 321);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "s_warnings";
            // 
            // chbDontShowAgain
            // 
            this.chbDontShowAgain.AutoSize = true;
            this.chbDontShowAgain.Location = new System.Drawing.Point(38, 422);
            this.chbDontShowAgain.Name = "chbDontShowAgain";
            this.chbDontShowAgain.Size = new System.Drawing.Size(121, 17);
            this.chbDontShowAgain.TabIndex = 6;
            this.chbDontShowAgain.Text = "s_dont_show_again";
            this.chbDontShowAgain.UseVisualStyleBackColor = true;
            this.chbDontShowAgain.CheckedChanged += new System.EventHandler(this.chbDontShowAgain_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::DatAdmin.StdIcons.question;
            this.pictureBox1.Location = new System.Drawing.Point(16, 422);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // SqlConfirmForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(632, 453);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.chbDontShowAgain);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.messageLogFrame1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.codeEditor1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SqlConfirmForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "s_confirm_sql";
            this.Shown += new System.EventHandler(this.SqlConfirmForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CodeEditor codeEditor1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private MessageLogFrame messageLogFrame1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chbDontShowAgain;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}