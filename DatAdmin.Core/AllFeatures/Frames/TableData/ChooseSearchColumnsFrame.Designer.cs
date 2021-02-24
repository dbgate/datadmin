namespace DatAdmin
{
    partial class ChooseSearchColumnsFrame
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
            this.btnCheckAll = new System.Windows.Forms.ToolStripButton();
            this.btnUncheckAll = new System.Windows.Forms.ToolStripButton();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chbExactMatch = new System.Windows.Forms.CheckBox();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCheckAll,
            this.btnUncheckAll});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 268);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(224, 23);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnCheckAll
            // 
            this.btnCheckAll.Image = global::DatAdmin.StdIcons.checkall;
            this.btnCheckAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.Size = new System.Drawing.Size(81, 20);
            this.btnCheckAll.Text = "s_check_all";
            this.btnCheckAll.Click += new System.EventHandler(this.btnCheckAll_Click);
            // 
            // btnUncheckAll
            // 
            this.btnUncheckAll.Image = global::DatAdmin.StdIcons.checkall_no;
            this.btnUncheckAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUncheckAll.Name = "btnUncheckAll";
            this.btnUncheckAll.Size = new System.Drawing.Size(93, 20);
            this.btnUncheckAll.Text = "s_uncheck_all";
            this.btnUncheckAll.Click += new System.EventHandler(this.btnUncheckAll_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(0, 29);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(224, 229);
            this.checkedListBox1.TabIndex = 1;
            this.checkedListBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.checkedListBox1_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chbExactMatch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(224, 29);
            this.panel1.TabIndex = 2;
            // 
            // chbExactMatch
            // 
            this.chbExactMatch.AutoSize = true;
            this.chbExactMatch.Location = new System.Drawing.Point(4, 4);
            this.chbExactMatch.Name = "chbExactMatch";
            this.chbExactMatch.Size = new System.Drawing.Size(98, 17);
            this.chbExactMatch.TabIndex = 0;
            this.chbExactMatch.Text = "s_exact_match";
            this.chbExactMatch.UseVisualStyleBackColor = true;
            // 
            // ChooseSearchColumnsFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "ChooseSearchColumnsFrame";
            this.Size = new System.Drawing.Size(224, 291);
            this.Leave += new System.EventHandler(this.ChooseSearchColumnsFrame_Leave);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.checkedListBox1_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.ToolStripButton btnCheckAll;
        private System.Windows.Forms.ToolStripButton btnUncheckAll;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chbExactMatch;
    }
}
