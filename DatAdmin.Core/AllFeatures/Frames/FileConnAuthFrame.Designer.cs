namespace DatAdmin
{
    partial class FileConnAuthFrame
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
            this.labPassword = new System.Windows.Forms.Label();
            this.tbxPassword = new System.Windows.Forms.TextBox();
            this.labLogin = new System.Windows.Forms.Label();
            this.cbxUseAutentization = new System.Windows.Forms.CheckBox();
            this.tbxLogin = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labPassword
            // 
            this.labPassword.AutoSize = true;
            this.labPassword.Location = new System.Drawing.Point(0, 57);
            this.labPassword.Name = "labPassword";
            this.labPassword.Size = new System.Drawing.Size(63, 13);
            this.labPassword.TabIndex = 19;
            this.labPassword.Text = "s_password";
            // 
            // tbxPassword
            // 
            this.tbxPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxPassword.Enabled = false;
            this.tbxPassword.Location = new System.Drawing.Point(100, 54);
            this.tbxPassword.Name = "tbxPassword";
            this.tbxPassword.Size = new System.Drawing.Size(202, 20);
            this.tbxPassword.TabIndex = 18;
            this.tbxPassword.UseSystemPasswordChar = true;
            // 
            // labLogin
            // 
            this.labLogin.AutoSize = true;
            this.labLogin.Location = new System.Drawing.Point(0, 31);
            this.labLogin.Name = "labLogin";
            this.labLogin.Size = new System.Drawing.Size(40, 13);
            this.labLogin.TabIndex = 17;
            this.labLogin.Text = "s_login";
            // 
            // cbxUseAutentization
            // 
            this.cbxUseAutentization.AutoSize = true;
            this.cbxUseAutentization.Location = new System.Drawing.Point(3, 3);
            this.cbxUseAutentization.Name = "cbxUseAutentization";
            this.cbxUseAutentization.Size = new System.Drawing.Size(120, 17);
            this.cbxUseAutentization.TabIndex = 16;
            this.cbxUseAutentization.Text = "s_use_autentization";
            this.cbxUseAutentization.UseVisualStyleBackColor = true;
            this.cbxUseAutentization.CheckedChanged += new System.EventHandler(this.cbxUseAutentization_CheckedChanged);
            // 
            // tbxLogin
            // 
            this.tbxLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxLogin.Enabled = false;
            this.tbxLogin.Location = new System.Drawing.Point(100, 28);
            this.tbxLogin.Name = "tbxLogin";
            this.tbxLogin.Size = new System.Drawing.Size(202, 20);
            this.tbxLogin.TabIndex = 15;
            // 
            // FileConnAuthFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labPassword);
            this.Controls.Add(this.tbxPassword);
            this.Controls.Add(this.labLogin);
            this.Controls.Add(this.cbxUseAutentization);
            this.Controls.Add(this.tbxLogin);
            this.Name = "FileConnAuthFrame";
            this.Size = new System.Drawing.Size(302, 78);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labPassword;
        private System.Windows.Forms.TextBox tbxPassword;
        private System.Windows.Forms.Label labLogin;
        private System.Windows.Forms.CheckBox cbxUseAutentization;
        private System.Windows.Forms.TextBox tbxLogin;
    }
}
