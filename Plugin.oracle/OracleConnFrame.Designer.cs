namespace Plugin.oracle
{
    partial class OracleConnFrame
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxConnectionType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxLogin = new System.Windows.Forms.TextBox();
            this.tbxPassword = new System.Windows.Forms.TextBox();
            this.tbxService = new System.Windows.Forms.TextBox();
            this.labHost = new System.Windows.Forms.Label();
            this.labPort = new System.Windows.Forms.Label();
            this.tbxHost = new System.Windows.Forms.TextBox();
            this.tbxPort = new System.Windows.Forms.TextBox();
            this.labService = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chbSID = new System.Windows.Forms.RadioButton();
            this.chbServiceName = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbxConnectionType, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbxLogin, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbxPassword, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbxService, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labHost, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.labPort, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.tbxHost, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.tbxPort, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.labService, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(343, 193);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "s_connection_type";
            // 
            // cbxConnectionType
            // 
            this.cbxConnectionType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxConnectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxConnectionType.FormattingEnabled = true;
            this.cbxConnectionType.Location = new System.Drawing.Point(174, 55);
            this.cbxConnectionType.Name = "cbxConnectionType";
            this.cbxConnectionType.Size = new System.Drawing.Size(166, 21);
            this.cbxConnectionType.TabIndex = 2;
            this.cbxConnectionType.SelectedIndexChanged += new System.EventHandler(this.cbxConnectionType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "s_login";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "s_password";
            // 
            // tbxLogin
            // 
            this.tbxLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxLogin.Location = new System.Drawing.Point(174, 3);
            this.tbxLogin.Name = "tbxLogin";
            this.tbxLogin.Size = new System.Drawing.Size(166, 20);
            this.tbxLogin.TabIndex = 0;
            // 
            // tbxPassword
            // 
            this.tbxPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxPassword.Location = new System.Drawing.Point(174, 29);
            this.tbxPassword.Name = "tbxPassword";
            this.tbxPassword.Size = new System.Drawing.Size(166, 20);
            this.tbxPassword.TabIndex = 1;
            this.tbxPassword.UseSystemPasswordChar = true;
            // 
            // tbxService
            // 
            this.tbxService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxService.Location = new System.Drawing.Point(174, 82);
            this.tbxService.Name = "tbxService";
            this.tbxService.Size = new System.Drawing.Size(166, 20);
            this.tbxService.TabIndex = 3;
            // 
            // labHost
            // 
            this.labHost.AutoSize = true;
            this.labHost.Location = new System.Drawing.Point(3, 131);
            this.labHost.Name = "labHost";
            this.labHost.Size = new System.Drawing.Size(38, 13);
            this.labHost.TabIndex = 8;
            this.labHost.Text = "s_host";
            // 
            // labPort
            // 
            this.labPort.AutoSize = true;
            this.labPort.Location = new System.Drawing.Point(3, 157);
            this.labPort.Name = "labPort";
            this.labPort.Size = new System.Drawing.Size(36, 13);
            this.labPort.TabIndex = 9;
            this.labPort.Text = "s_port";
            // 
            // tbxHost
            // 
            this.tbxHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxHost.Location = new System.Drawing.Point(174, 134);
            this.tbxHost.Name = "tbxHost";
            this.tbxHost.Size = new System.Drawing.Size(166, 20);
            this.tbxHost.TabIndex = 5;
            // 
            // tbxPort
            // 
            this.tbxPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxPort.Location = new System.Drawing.Point(174, 160);
            this.tbxPort.Name = "tbxPort";
            this.tbxPort.Size = new System.Drawing.Size(166, 20);
            this.tbxPort.TabIndex = 6;
            // 
            // labService
            // 
            this.labService.AutoSize = true;
            this.labService.Location = new System.Drawing.Point(3, 79);
            this.labService.Name = "labService";
            this.labService.Size = new System.Drawing.Size(52, 13);
            this.labService.TabIndex = 6;
            this.labService.Text = "s_service";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chbSID);
            this.panel1.Controls.Add(this.chbServiceName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(174, 108);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(166, 20);
            this.panel1.TabIndex = 4;
            // 
            // chbSID
            // 
            this.chbSID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbSID.AutoSize = true;
            this.chbSID.Location = new System.Drawing.Point(111, 4);
            this.chbSID.Name = "chbSID";
            this.chbSID.Size = new System.Drawing.Size(43, 17);
            this.chbSID.TabIndex = 1;
            this.chbSID.Text = "SID";
            this.chbSID.UseVisualStyleBackColor = true;
            // 
            // chbServiceName
            // 
            this.chbServiceName.AutoSize = true;
            this.chbServiceName.Checked = true;
            this.chbServiceName.Location = new System.Drawing.Point(4, 4);
            this.chbServiceName.Name = "chbServiceName";
            this.chbServiceName.Size = new System.Drawing.Size(102, 17);
            this.chbServiceName.TabIndex = 0;
            this.chbServiceName.TabStop = true;
            this.chbServiceName.Text = "s_service_name";
            this.chbServiceName.UseVisualStyleBackColor = true;
            // 
            // OracleConnFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "OracleConnFrame";
            this.Size = new System.Drawing.Size(346, 207);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxConnectionType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxLogin;
        private System.Windows.Forms.TextBox tbxPassword;
        private System.Windows.Forms.Label labService;
        private System.Windows.Forms.TextBox tbxService;
        private System.Windows.Forms.Label labHost;
        private System.Windows.Forms.Label labPort;
        private System.Windows.Forms.TextBox tbxHost;
        private System.Windows.Forms.TextBox tbxPort;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton chbSID;
        private System.Windows.Forms.RadioButton chbServiceName;
    }
}
