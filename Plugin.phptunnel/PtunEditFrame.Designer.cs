namespace Plugin.phptunnel
{
    partial class PtunEditFrame
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
            this.cbxExtendedSafety = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btmSavePhpTunnel = new System.Windows.Forms.Button();
            this.tbxUrl = new System.Windows.Forms.TextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbxHttpPassword = new System.Windows.Forms.TextBox();
            this.tbxHttpLogin = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxEncodingStyle = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxEncoding = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxExtendedSafety
            // 
            this.cbxExtendedSafety.AutoSize = true;
            this.cbxExtendedSafety.Location = new System.Drawing.Point(18, 70);
            this.cbxExtendedSafety.Name = "cbxExtendedSafety";
            this.cbxExtendedSafety.Size = new System.Drawing.Size(207, 17);
            this.cbxExtendedSafety.TabIndex = 12;
            this.cbxExtendedSafety.Text = "s_extended_safety_unique_tunnel_file";
            this.cbxExtendedSafety.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "s_type_url_of_tunnel_url";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(231, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "s_first_save_tunnel_file_and_upload_it_to_web";
            // 
            // btmSavePhpTunnel
            // 
            this.btmSavePhpTunnel.Location = new System.Drawing.Point(18, 27);
            this.btmSavePhpTunnel.Name = "btmSavePhpTunnel";
            this.btmSavePhpTunnel.Size = new System.Drawing.Size(150, 23);
            this.btmSavePhpTunnel.TabIndex = 9;
            this.btmSavePhpTunnel.Text = "s_save_phptunnel_file";
            this.btmSavePhpTunnel.UseVisualStyleBackColor = true;
            this.btmSavePhpTunnel.Click += new System.EventHandler(this.btmSavePhpTunnel_Click);
            // 
            // tbxUrl
            // 
            this.tbxUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxUrl.Location = new System.Drawing.Point(15, 118);
            this.tbxUrl.Name = "tbxUrl";
            this.tbxUrl.Size = new System.Drawing.Size(476, 20);
            this.tbxUrl.TabIndex = 8;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "phptunnel.php|phptunnel.php";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tbxHttpPassword);
            this.groupBox1.Controls.Add(this.tbxHttpLogin);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(15, 144);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(476, 83);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "s_http_authentization";
            // 
            // tbxHttpPassword
            // 
            this.tbxHttpPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxHttpPassword.Location = new System.Drawing.Point(100, 50);
            this.tbxHttpPassword.Name = "tbxHttpPassword";
            this.tbxHttpPassword.Size = new System.Drawing.Size(370, 20);
            this.tbxHttpPassword.TabIndex = 7;
            this.tbxHttpPassword.UseSystemPasswordChar = true;
            // 
            // tbxHttpLogin
            // 
            this.tbxHttpLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxHttpLogin.Location = new System.Drawing.Point(100, 24);
            this.tbxHttpLogin.Name = "tbxHttpLogin";
            this.tbxHttpLogin.Size = new System.Drawing.Size(370, 20);
            this.tbxHttpLogin.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "s_password";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "s_login";
            // 
            // cbxEncodingStyle
            // 
            this.cbxEncodingStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEncodingStyle.FormattingEnabled = true;
            this.cbxEncodingStyle.Location = new System.Drawing.Point(115, 239);
            this.cbxEncodingStyle.Name = "cbxEncodingStyle";
            this.cbxEncodingStyle.Size = new System.Drawing.Size(204, 21);
            this.cbxEncodingStyle.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 242);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "s_encoding_style";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 271);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "s_encoding";
            // 
            // cbxEncoding
            // 
            this.cbxEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEncoding.FormattingEnabled = true;
            this.cbxEncoding.Location = new System.Drawing.Point(115, 268);
            this.cbxEncoding.Name = "cbxEncoding";
            this.cbxEncoding.Size = new System.Drawing.Size(204, 21);
            this.cbxEncoding.TabIndex = 20;
            // 
            // PtunEditFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbxEncoding);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxEncodingStyle);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbxExtendedSafety);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btmSavePhpTunnel);
            this.Controls.Add(this.tbxUrl);
            this.Name = "PtunEditFrame";
            this.Size = new System.Drawing.Size(506, 319);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbxExtendedSafety;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btmSavePhpTunnel;
        private System.Windows.Forms.TextBox tbxUrl;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbxHttpPassword;
        private System.Windows.Forms.TextBox tbxHttpLogin;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbxEncodingStyle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxEncoding;
    }
}
