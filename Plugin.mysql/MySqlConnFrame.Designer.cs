namespace Plugin.mysql
{
    partial class MySqlConnFrame
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
            this.label2 = new System.Windows.Forms.Label();
            this.tbxPassword = new System.Windows.Forms.TextBox();
            this.tbxLogin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxDataSource = new System.Windows.Forms.TextBox();
            this.layTable = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxCharacterSet = new System.Windows.Forms.ComboBox();
            this.layTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 52;
            this.label2.Text = "s_host";
            // 
            // tbxPassword
            // 
            this.tbxPassword.AcceptsReturn = true;
            this.tbxPassword.Location = new System.Drawing.Point(206, 55);
            this.tbxPassword.Name = "tbxPassword";
            this.tbxPassword.UseSystemPasswordChar = true;
            this.tbxPassword.Size = new System.Drawing.Size(193, 20);
            this.tbxPassword.TabIndex = 47;
            this.tbxPassword.TextChanged += new System.EventHandler(this.datasource_TextChanged);
            // 
            // tbxLogin
            // 
            this.tbxLogin.Location = new System.Drawing.Point(206, 29);
            this.tbxLogin.Name = "tbxLogin";
            this.tbxLogin.Size = new System.Drawing.Size(193, 20);
            this.tbxLogin.TabIndex = 46;
            this.tbxLogin.TextChanged += new System.EventHandler(this.datasource_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 45;
            this.label4.Text = "s_password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 44;
            this.label3.Text = "s_login";
            // 
            // tbxDataSource
            // 
            this.tbxDataSource.Location = new System.Drawing.Point(206, 3);
            this.tbxDataSource.Name = "tbxDataSource";
            this.tbxDataSource.Size = new System.Drawing.Size(193, 20);
            this.tbxDataSource.TabIndex = 43;
            this.tbxDataSource.TextChanged += new System.EventHandler(this.datasource_TextChanged);
            // 
            // layTable
            // 
            this.layTable.ColumnCount = 2;
            this.layTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layTable.Controls.Add(this.label2, 0, 1);
            this.layTable.Controls.Add(this.tbxDataSource, 1, 1);
            this.layTable.Controls.Add(this.label3, 0, 2);
            this.layTable.Controls.Add(this.tbxLogin, 1, 2);
            this.layTable.Controls.Add(this.tbxPassword, 1, 3);
            this.layTable.Controls.Add(this.tbxPort, 1, 4);
            this.layTable.Controls.Add(this.cbxCharacterSet, 1, 5);
            this.layTable.Controls.Add(this.label5, 0, 5);
            this.layTable.Controls.Add(this.label1, 0, 4);
            this.layTable.Controls.Add(this.label4, 0, 3);
            this.layTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layTable.Location = new System.Drawing.Point(0, 0);
            this.layTable.Name = "layTable";
            this.layTable.RowCount = 6;
            this.layTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layTable.Size = new System.Drawing.Size(407, 128);
            this.layTable.TabIndex = 53;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 53;
            this.label1.Text = "s_port";
            // 
            // tbxPort
            // 
            this.tbxPort.Location = new System.Drawing.Point(206, 81);
            this.tbxPort.Name = "tbxPort";
            this.tbxPort.Size = new System.Drawing.Size(193, 20);
            this.tbxPort.TabIndex = 54;
            this.tbxPort.Text = "3306";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 55;
            this.label5.Text = "s_character_set";
            // 
            // cbxCharacterSet
            // 
            this.cbxCharacterSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCharacterSet.FormattingEnabled = true;
            this.cbxCharacterSet.Location = new System.Drawing.Point(206, 107);
            this.cbxCharacterSet.Name = "cbxCharacterSet";
            this.cbxCharacterSet.Size = new System.Drawing.Size(193, 21);
            this.cbxCharacterSet.TabIndex = 56;
            // 
            // ConnFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layTable);
            this.Name = "ConnFrame";
            this.Size = new System.Drawing.Size(407, 128);
            this.layTable.ResumeLayout(false);
            this.layTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxPassword;
        private System.Windows.Forms.TextBox tbxLogin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxDataSource;
        private System.Windows.Forms.TableLayoutPanel layTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxPort;
        private System.Windows.Forms.ComboBox cbxCharacterSet;
        private System.Windows.Forms.Label label5;
    }
}
