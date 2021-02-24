namespace DatAdmin
{
    partial class SaveDataForm
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
            this.saveDataFrame1 = new DatAdmin.SaveDataFrame();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnOnBackground = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chbDontShowAgain = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // saveDataFrame1
            // 
            this.saveDataFrame1.Location = new System.Drawing.Point(12, 12);
            this.saveDataFrame1.Name = "saveDataFrame1";
            this.saveDataFrame1.Progress = null;
            this.saveDataFrame1.Size = new System.Drawing.Size(271, 214);
            this.saveDataFrame1.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOk.Enabled = false;
            this.btnOk.Image = global::DatAdmin.StdIcons.ok;
            this.btnOk.Location = new System.Drawing.Point(39, 232);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(239, 23);
            this.btnOk.TabIndex = 19;
            this.btnOk.Text = "s_ok";
            this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnOnBackground
            // 
            this.btnOnBackground.Location = new System.Drawing.Point(39, 261);
            this.btnOnBackground.Name = "btnOnBackground";
            this.btnOnBackground.Size = new System.Drawing.Size(239, 23);
            this.btnOnBackground.TabIndex = 18;
            this.btnOnBackground.Text = "s_on_background";
            this.btnOnBackground.UseVisualStyleBackColor = true;
            this.btnOnBackground.Click += new System.EventHandler(this.btnOnBackground_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::DatAdmin.StdIcons.question;
            this.pictureBox1.Location = new System.Drawing.Point(39, 290);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // chbDontShowAgain
            // 
            this.chbDontShowAgain.AutoSize = true;
            this.chbDontShowAgain.Location = new System.Drawing.Point(70, 290);
            this.chbDontShowAgain.Name = "chbDontShowAgain";
            this.chbDontShowAgain.Size = new System.Drawing.Size(121, 17);
            this.chbDontShowAgain.TabIndex = 20;
            this.chbDontShowAgain.Text = "s_dont_show_again";
            this.chbDontShowAgain.UseVisualStyleBackColor = true;
            this.chbDontShowAgain.CheckedChanged += new System.EventHandler(this.chbDontShowAgain_CheckedChanged);
            // 
            // SaveDataForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOk;
            this.ClientSize = new System.Drawing.Size(309, 311);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.chbDontShowAgain);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnOnBackground);
            this.Controls.Add(this.saveDataFrame1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SaveDataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "s_save_progress";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DatAdmin.SaveDataFrame saveDataFrame1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnOnBackground;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox chbDontShowAgain;
    }
}