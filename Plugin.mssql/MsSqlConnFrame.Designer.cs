namespace Plugin.mssql
{
    partial class MsSqlConnFrame
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
            this.layTable = new System.Windows.Forms.TableLayoutPanel();
            this.tbxPassword = new System.Windows.Forms.TextBox();
            this.cbxAuthentization = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxLogin = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxDataSource = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.layTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // layTable
            // 
            this.layTable.ColumnCount = 2;
            this.layTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layTable.Controls.Add(this.tbxPassword, 1, 3);
            this.layTable.Controls.Add(this.cbxAuthentization, 1, 1);
            this.layTable.Controls.Add(this.label4, 0, 3);
            this.layTable.Controls.Add(this.tbxLogin, 1, 2);
            this.layTable.Controls.Add(this.label1, 0, 0);
            this.layTable.Controls.Add(this.tbxDataSource, 1, 0);
            this.layTable.Controls.Add(this.label3, 0, 2);
            this.layTable.Controls.Add(this.label2, 0, 1);
            this.layTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layTable.Location = new System.Drawing.Point(0, 0);
            this.layTable.Name = "layTable";
            this.layTable.RowCount = 4;
            this.layTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layTable.Size = new System.Drawing.Size(420, 108);
            this.layTable.TabIndex = 0;
            // 
            // tbxPassword
            // 
            this.tbxPassword.AcceptsReturn = true;
            this.tbxPassword.Location = new System.Drawing.Point(230, 82);
            this.tbxPassword.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.tbxPassword.Name = "tbxPassword";
            this.tbxPassword.UseSystemPasswordChar = true;
            this.tbxPassword.Size = new System.Drawing.Size(187, 20);
            this.tbxPassword.TabIndex = 26;
            this.tbxPassword.TextChanged += new System.EventHandler(this.datasource_TextChanged);
            // 
            // cbxAuthentization
            // 
            this.cbxAuthentization.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAuthentization.FormattingEnabled = true;
            this.cbxAuthentization.Items.AddRange(new object[] {
            "Windows (SSPI)",
            "SQL Server"});
            this.cbxAuthentization.Location = new System.Drawing.Point(213, 29);
            this.cbxAuthentization.Name = "cbxAuthentization";
            this.cbxAuthentization.Size = new System.Drawing.Size(204, 21);
            this.cbxAuthentization.TabIndex = 27;
            this.cbxAuthentization.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "s_password";
            // 
            // tbxLogin
            // 
            this.tbxLogin.Location = new System.Drawing.Point(230, 56);
            this.tbxLogin.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.tbxLogin.Name = "tbxLogin";
            this.tbxLogin.Size = new System.Drawing.Size(187, 20);
            this.tbxLogin.TabIndex = 25;
            this.tbxLogin.TextChanged += new System.EventHandler(this.datasource_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "s_host";
            // 
            // tbxDataSource
            // 
            this.tbxDataSource.Location = new System.Drawing.Point(213, 3);
            this.tbxDataSource.Name = "tbxDataSource";
            this.tbxDataSource.Size = new System.Drawing.Size(204, 20);
            this.tbxDataSource.TabIndex = 22;
            this.tbxDataSource.TextChanged += new System.EventHandler(this.datasource_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "s_login";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "s_authentization";
            // 
            // ConnFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layTable);
            this.Name = "ConnFrame";
            this.Size = new System.Drawing.Size(420, 108);
            this.layTable.ResumeLayout(false);
            this.layTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel layTable;
        private System.Windows.Forms.ComboBox cbxAuthentization;
        private System.Windows.Forms.TextBox tbxPassword;
        private System.Windows.Forms.TextBox tbxLogin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxDataSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
