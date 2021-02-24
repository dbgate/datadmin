namespace DatAdmin
{
    partial class CommonConnectionEditFrame
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
            this.layout1 = new System.Windows.Forms.TableLayoutPanel();
            this.connectionstring = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbdatabase = new DatAdmin.DatabaseDropDown();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxDialect = new System.Windows.Forms.ComboBox();
            this.layout1.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout1
            // 
            this.layout1.ColumnCount = 2;
            this.layout1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layout1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layout1.Controls.Add(this.label6, 0, 1);
            this.layout1.Controls.Add(this.lbdatabase, 1, 1);
            this.layout1.Controls.Add(this.label1, 0, 0);
            this.layout1.Controls.Add(this.label5, 0, 2);
            this.layout1.Controls.Add(this.cbxDialect, 1, 0);
            this.layout1.Controls.Add(this.connectionstring, 1, 2);
            this.layout1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout1.Location = new System.Drawing.Point(0, 0);
            this.layout1.Name = "layout1";
            this.layout1.RowCount = 3;
            this.layout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.layout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.layout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.layout1.Size = new System.Drawing.Size(380, 78);
            this.layout1.TabIndex = 0;
            // 
            // connectionstring
            // 
            this.connectionstring.Location = new System.Drawing.Point(193, 55);
            this.connectionstring.Name = "connectionstring";
            this.connectionstring.ReadOnly = true;
            this.connectionstring.Size = new System.Drawing.Size(181, 20);
            this.connectionstring.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "s_database";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "s_connection_string";
            // 
            // lbdatabase
            // 
            this.lbdatabase.Connection = null;
            this.lbdatabase.FormattingEnabled = true;
            this.lbdatabase.Location = new System.Drawing.Point(193, 29);
            this.lbdatabase.Name = "lbdatabase";
            this.lbdatabase.Size = new System.Drawing.Size(181, 21);
            this.lbdatabase.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "s_dialect";
            // 
            // cbxDialect
            // 
            this.cbxDialect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDialect.FormattingEnabled = true;
            this.cbxDialect.Location = new System.Drawing.Point(193, 3);
            this.cbxDialect.Name = "cbxDialect";
            this.cbxDialect.Size = new System.Drawing.Size(181, 21);
            this.cbxDialect.TabIndex = 1;
            // 
            // CommonConnectionEditFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layout1);
            this.Name = "CommonConnectionEditFrame";
            this.Size = new System.Drawing.Size(380, 78);
            this.layout1.ResumeLayout(false);
            this.layout1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel layout1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private DatAdmin.DatabaseDropDown lbdatabase;
        private System.Windows.Forms.TextBox connectionstring;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxDialect;
    }
}
