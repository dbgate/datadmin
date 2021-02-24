namespace DatAdmin
{
    partial class CopyTableForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CopyTableForm));
            this.label1 = new System.Windows.Forms.Label();
            this.cbcopydata = new System.Windows.Forms.CheckBox();
            this.btok = new System.Windows.Forms.Button();
            this.btcancel = new System.Windows.Forms.Button();
            this.tbltblname = new System.Windows.Forms.TextBox();
            this.tableCopyOptionsFrame1 = new DatAdmin.TableCopyOptionsFrame();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "s_table_name";
            // 
            // cbcopydata
            // 
            this.cbcopydata.AutoSize = true;
            this.cbcopydata.Location = new System.Drawing.Point(15, 50);
            this.cbcopydata.Name = "cbcopydata";
            this.cbcopydata.Size = new System.Drawing.Size(87, 17);
            this.cbcopydata.TabIndex = 1;
            this.cbcopydata.Text = "s_copy_data";
            this.cbcopydata.UseVisualStyleBackColor = true;
            this.cbcopydata.CheckedChanged += new System.EventHandler(this.cbcopydata_CheckedChanged);
            // 
            // btok
            // 
            this.btok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btok.Location = new System.Drawing.Point(55, 191);
            this.btok.Name = "btok";
            this.btok.Size = new System.Drawing.Size(75, 23);
            this.btok.TabIndex = 2;
            this.btok.Text = "s_ok";
            this.btok.UseVisualStyleBackColor = true;
            this.btok.Click += new System.EventHandler(this.button1_Click);
            // 
            // btcancel
            // 
            this.btcancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btcancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btcancel.Location = new System.Drawing.Point(136, 191);
            this.btcancel.Name = "btcancel";
            this.btcancel.Size = new System.Drawing.Size(75, 23);
            this.btcancel.TabIndex = 3;
            this.btcancel.Text = "s_cancel";
            this.btcancel.UseVisualStyleBackColor = true;
            this.btcancel.Click += new System.EventHandler(this.button2_Click);
            // 
            // tbltblname
            // 
            this.tbltblname.Location = new System.Drawing.Point(127, 18);
            this.tbltblname.Name = "tbltblname";
            this.tbltblname.Size = new System.Drawing.Size(100, 20);
            this.tbltblname.TabIndex = 4;
            // 
            // tableCopyOptionsFrame1
            // 
            this.tableCopyOptionsFrame1.Location = new System.Drawing.Point(11, 81);
            this.tableCopyOptionsFrame1.Name = "tableCopyOptionsFrame1";
            this.tableCopyOptionsFrame1.Size = new System.Drawing.Size(243, 94);
            this.tableCopyOptionsFrame1.TabIndex = 5;
            // 
            // CopyTableForm
            // 
            this.AcceptButton = this.btok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btcancel;
            this.ClientSize = new System.Drawing.Size(269, 226);
            this.Controls.Add(this.tableCopyOptionsFrame1);
            this.Controls.Add(this.tbltblname);
            this.Controls.Add(this.btcancel);
            this.Controls.Add(this.btok);
            this.Controls.Add(this.cbcopydata);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CopyTableForm";
            this.Text = "s_copy_table";
            this.Shown += new System.EventHandler(this.CopyTableForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbcopydata;
        private System.Windows.Forms.Button btok;
        private System.Windows.Forms.Button btcancel;
        private System.Windows.Forms.TextBox tbltblname;
        private TableCopyOptionsFrame tableCopyOptionsFrame1;
    }
}