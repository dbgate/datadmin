namespace Plugin.access
{
    partial class AccessConnAuthFrame
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbxSystemDatabase = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labPassword
            // 
            this.labPassword.AutoSize = true;
            this.labPassword.Location = new System.Drawing.Point(0, 57);
            this.labPassword.Name = "labPassword";
            this.labPassword.Size = new System.Drawing.Size(63, 13);
            this.labPassword.TabIndex = 24;
            this.labPassword.Text = "s_password";
            // 
            // tbxPassword
            // 
            this.tbxPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxPassword.Enabled = false;
            this.tbxPassword.Location = new System.Drawing.Point(103, 54);
            this.tbxPassword.Name = "tbxPassword";
            this.tbxPassword.Size = new System.Drawing.Size(254, 20);
            this.tbxPassword.TabIndex = 23;
            this.tbxPassword.UseSystemPasswordChar = true;
            // 
            // labLogin
            // 
            this.labLogin.AutoSize = true;
            this.labLogin.Location = new System.Drawing.Point(0, 31);
            this.labLogin.Name = "labLogin";
            this.labLogin.Size = new System.Drawing.Size(40, 13);
            this.labLogin.TabIndex = 22;
            this.labLogin.Text = "s_login";
            // 
            // cbxUseAutentization
            // 
            this.cbxUseAutentization.AutoSize = true;
            this.cbxUseAutentization.Location = new System.Drawing.Point(3, 3);
            this.cbxUseAutentization.Name = "cbxUseAutentization";
            this.cbxUseAutentization.Size = new System.Drawing.Size(120, 17);
            this.cbxUseAutentization.TabIndex = 21;
            this.cbxUseAutentization.Text = "s_use_autentization";
            this.cbxUseAutentization.UseVisualStyleBackColor = true;
            this.cbxUseAutentization.CheckedChanged += new System.EventHandler(this.cbxUseAutentization_CheckedChanged);
            // 
            // tbxLogin
            // 
            this.tbxLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxLogin.Enabled = false;
            this.tbxLogin.Location = new System.Drawing.Point(103, 28);
            this.tbxLogin.Name = "tbxLogin";
            this.tbxLogin.Size = new System.Drawing.Size(254, 20);
            this.tbxLogin.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-3, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "s_system_database";
            // 
            // tbxSystemDatabase
            // 
            this.tbxSystemDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxSystemDatabase.Enabled = false;
            this.tbxSystemDatabase.Location = new System.Drawing.Point(103, 80);
            this.tbxSystemDatabase.Name = "tbxSystemDatabase";
            this.tbxSystemDatabase.Size = new System.Drawing.Size(254, 20);
            this.tbxSystemDatabase.TabIndex = 26;
            // 
            // AccessConnAuthFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbxSystemDatabase);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labPassword);
            this.Controls.Add(this.tbxPassword);
            this.Controls.Add(this.labLogin);
            this.Controls.Add(this.cbxUseAutentization);
            this.Controls.Add(this.tbxLogin);
            this.Name = "AccessConnAuthFrame";
            this.Size = new System.Drawing.Size(360, 108);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labPassword;
        private System.Windows.Forms.TextBox tbxPassword;
        private System.Windows.Forms.Label labLogin;
        private System.Windows.Forms.CheckBox cbxUseAutentization;
        private System.Windows.Forms.TextBox tbxLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxSystemDatabase;
    }
}
