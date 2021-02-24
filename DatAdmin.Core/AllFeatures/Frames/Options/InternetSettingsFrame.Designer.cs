namespace DatAdmin
{
    partial class InternetSettingsFrame
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
            this.chbUseProxyServer = new System.Windows.Forms.CheckBox();
            this.chbUseSystemDefaultProxy = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxServer = new System.Windows.Forms.TextBox();
            this.tbxLogin = new System.Windows.Forms.TextBox();
            this.tbxPassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // chbUseProxyServer
            // 
            this.chbUseProxyServer.AutoSize = true;
            this.chbUseProxyServer.Location = new System.Drawing.Point(17, 16);
            this.chbUseProxyServer.Name = "chbUseProxyServer";
            this.chbUseProxyServer.Size = new System.Drawing.Size(120, 17);
            this.chbUseProxyServer.TabIndex = 0;
            this.chbUseProxyServer.Text = "s_use_proxy_server";
            this.chbUseProxyServer.UseVisualStyleBackColor = true;
            this.chbUseProxyServer.CheckedChanged += new System.EventHandler(this.chbUseProxyServer_CheckedChanged);
            // 
            // chbUseSystemDefaultProxy
            // 
            this.chbUseSystemDefaultProxy.AutoSize = true;
            this.chbUseSystemDefaultProxy.Location = new System.Drawing.Point(17, 40);
            this.chbUseSystemDefaultProxy.Name = "chbUseSystemDefaultProxy";
            this.chbUseSystemDefaultProxy.Size = new System.Drawing.Size(110, 17);
            this.chbUseSystemDefaultProxy.TabIndex = 1;
            this.chbUseSystemDefaultProxy.Text = "s_use_ie_settings";
            this.chbUseSystemDefaultProxy.UseVisualStyleBackColor = true;
            this.chbUseSystemDefaultProxy.CheckedChanged += new System.EventHandler(this.chbUseSystemDefaultProxy_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "s_server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "s_login";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "s_password";
            // 
            // tbxServer
            // 
            this.tbxServer.Location = new System.Drawing.Point(108, 74);
            this.tbxServer.Name = "tbxServer";
            this.tbxServer.Size = new System.Drawing.Size(169, 20);
            this.tbxServer.TabIndex = 6;
            this.tbxServer.TextChanged += new System.EventHandler(this.tbxServer_TextChanged);
            // 
            // tbxLogin
            // 
            this.tbxLogin.Location = new System.Drawing.Point(108, 100);
            this.tbxLogin.Name = "tbxLogin";
            this.tbxLogin.Size = new System.Drawing.Size(169, 20);
            this.tbxLogin.TabIndex = 8;
            this.tbxLogin.TextChanged += new System.EventHandler(this.tbxLogin_TextChanged);
            // 
            // tbxPassword
            // 
            this.tbxPassword.Location = new System.Drawing.Point(108, 126);
            this.tbxPassword.Name = "tbxPassword";
            this.tbxPassword.Size = new System.Drawing.Size(169, 20);
            this.tbxPassword.TabIndex = 9;
            this.tbxPassword.UseSystemPasswordChar = true;
            this.tbxPassword.TextChanged += new System.EventHandler(this.tbxPassword_TextChanged);
            // 
            // InternetSettingsFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbxPassword);
            this.Controls.Add(this.tbxLogin);
            this.Controls.Add(this.tbxServer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chbUseSystemDefaultProxy);
            this.Controls.Add(this.chbUseProxyServer);
            this.Name = "InternetSettingsFrame";
            this.Size = new System.Drawing.Size(405, 305);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbUseProxyServer;
        private System.Windows.Forms.CheckBox chbUseSystemDefaultProxy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxServer;
        private System.Windows.Forms.TextBox tbxLogin;
        private System.Windows.Forms.TextBox tbxPassword;
    }
}
