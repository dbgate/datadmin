namespace DatAdmin
{
    partial class DriverManagerFrame
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.lbdrivers = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.edname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.edfile = new System.Windows.Forms.TextBox();
            this.btbrowse = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.edfactory = new System.Windows.Forms.ComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.edinvname = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(482, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::DatAdmin.StdIcons.add;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(56, 22);
            this.toolStripButton1.Text = "s_add";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::DatAdmin.StdIcons.remove;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(74, 22);
            this.toolStripButton2.Text = "s_remove";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // lbdrivers
            // 
            this.lbdrivers.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbdrivers.FormattingEnabled = true;
            this.lbdrivers.Location = new System.Drawing.Point(0, 25);
            this.lbdrivers.Name = "lbdrivers";
            this.lbdrivers.Size = new System.Drawing.Size(222, 394);
            this.lbdrivers.TabIndex = 1;
            this.lbdrivers.SelectedIndexChanged += new System.EventHandler(this.lbdrivers_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(246, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "s_name";
            // 
            // edname
            // 
            this.edname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edname.Location = new System.Drawing.Point(249, 55);
            this.edname.Name = "edname";
            this.edname.Size = new System.Drawing.Size(214, 20);
            this.edname.TabIndex = 3;
            this.edname.TextChanged += new System.EventHandler(this.edname_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(249, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "s_file";
            // 
            // edfile
            // 
            this.edfile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edfile.Location = new System.Drawing.Point(249, 99);
            this.edfile.Name = "edfile";
            this.edfile.ReadOnly = true;
            this.edfile.Size = new System.Drawing.Size(168, 20);
            this.edfile.TabIndex = 5;
            // 
            // btbrowse
            // 
            this.btbrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btbrowse.Location = new System.Drawing.Point(423, 96);
            this.btbrowse.Name = "btbrowse";
            this.btbrowse.Size = new System.Drawing.Size(40, 23);
            this.btbrowse.TabIndex = 6;
            this.btbrowse.Text = "...";
            this.btbrowse.UseVisualStyleBackColor = true;
            this.btbrowse.Click += new System.EventHandler(this.btbrowse_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(249, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "s_factory_class";
            // 
            // edfactory
            // 
            this.edfactory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edfactory.FormattingEnabled = true;
            this.edfactory.Location = new System.Drawing.Point(249, 143);
            this.edfactory.Name = "edfactory";
            this.edfactory.Size = new System.Drawing.Size(214, 21);
            this.edfactory.TabIndex = 8;
            this.edfactory.DropDown += new System.EventHandler(this.edfactory_DropDown);
            this.edfactory.TextChanged += new System.EventHandler(this.edfactory_TextChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(249, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "s_invariant_name";
            // 
            // edinvname
            // 
            this.edinvname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edinvname.Location = new System.Drawing.Point(249, 194);
            this.edinvname.Name = "edinvname";
            this.edinvname.Size = new System.Drawing.Size(214, 20);
            this.edinvname.TabIndex = 10;
            this.edinvname.TextChanged += new System.EventHandler(this.edinvname_TextChanged);
            // 
            // DriverManagerFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.edinvname);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.edfactory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btbrowse);
            this.Controls.Add(this.edfile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.edname);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbdrivers);
            this.Controls.Add(this.toolStrip1);
            this.Name = "DriverManagerFrame";
            this.Size = new System.Drawing.Size(482, 429);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ListBox lbdrivers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox edfile;
        private System.Windows.Forms.Button btbrowse;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox edfactory;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox edinvname;
    }
}
