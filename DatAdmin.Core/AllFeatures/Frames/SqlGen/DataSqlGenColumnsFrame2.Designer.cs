namespace DatAdmin
{
    partial class DataSqlGenColumnsFrame2
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnCheckAll = new System.Windows.Forms.ToolStripButton();
            this.btnUncheckAll = new System.Windows.Forms.ToolStripButton();
            this.btnPrimaryKey = new System.Windows.Forms.ToolStripButton();
            this.btnNoKeyColumns = new System.Windows.Forms.ToolStripButton();
            this.btnSelectedColumns = new System.Windows.Forms.ToolStripButton();
            this.btnNoSelectedColumns = new System.Windows.Forms.ToolStripButton();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkedListBox1);
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(203, 221);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(3, 41);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(197, 169);
            this.checkedListBox1.TabIndex = 1;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCheckAll,
            this.btnUncheckAll,
            this.btnPrimaryKey,
            this.btnNoKeyColumns,
            this.btnSelectedColumns,
            this.btnNoSelectedColumns});
            this.toolStrip1.Location = new System.Drawing.Point(3, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(197, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnCheckAll
            // 
            this.btnCheckAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCheckAll.Image = global::DatAdmin.StdIcons.checkall;
            this.btnCheckAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.Size = new System.Drawing.Size(23, 22);
            this.btnCheckAll.Text = "s_check_all";
            this.btnCheckAll.Click += new System.EventHandler(this.btnCheckAll_Click);
            // 
            // btnUncheckAll
            // 
            this.btnUncheckAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUncheckAll.Image = global::DatAdmin.StdIcons.checkall_no;
            this.btnUncheckAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUncheckAll.Name = "btnUncheckAll";
            this.btnUncheckAll.Size = new System.Drawing.Size(23, 22);
            this.btnUncheckAll.Text = "s_uncheck_all";
            this.btnUncheckAll.Click += new System.EventHandler(this.btnUncheckAll_Click);
            // 
            // btnPrimaryKey
            // 
            this.btnPrimaryKey.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrimaryKey.Image = global::DatAdmin.StdIcons.primary_key;
            this.btnPrimaryKey.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrimaryKey.Name = "btnPrimaryKey";
            this.btnPrimaryKey.Size = new System.Drawing.Size(23, 22);
            this.btnPrimaryKey.Text = "s_primary_key";
            this.btnPrimaryKey.Click += new System.EventHandler(this.btnPrimaryKey_Click);
            // 
            // btnNoKeyColumns
            // 
            this.btnNoKeyColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNoKeyColumns.Image = global::DatAdmin.StdIcons.primary_key_no;
            this.btnNoKeyColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNoKeyColumns.Name = "btnNoKeyColumns";
            this.btnNoKeyColumns.Size = new System.Drawing.Size(23, 22);
            this.btnNoKeyColumns.Text = "s_no_key_columns";
            this.btnNoKeyColumns.Click += new System.EventHandler(this.btnNoKeyColumns_Click);
            // 
            // btnSelectedColumns
            // 
            this.btnSelectedColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSelectedColumns.Image = global::DatAdmin.StdIcons.table_data;
            this.btnSelectedColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectedColumns.Name = "btnSelectedColumns";
            this.btnSelectedColumns.Size = new System.Drawing.Size(23, 22);
            this.btnSelectedColumns.Text = "s_selected_columns";
            this.btnSelectedColumns.Click += new System.EventHandler(this.btnSelectedColumns_Click);
            // 
            // btnNoSelectedColumns
            // 
            this.btnNoSelectedColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNoSelectedColumns.Image = global::DatAdmin.StdIcons.table_data_no;
            this.btnNoSelectedColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNoSelectedColumns.Name = "btnNoSelectedColumns";
            this.btnNoSelectedColumns.Size = new System.Drawing.Size(23, 22);
            this.btnNoSelectedColumns.Text = "s_no_selected_columns";
            this.btnNoSelectedColumns.Click += new System.EventHandler(this.btnNoSelectedColumns_Click);
            // 
            // DataSqlGenColumnsFrame2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "DataSqlGenColumnsFrame2";
            this.Size = new System.Drawing.Size(203, 221);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.ToolStripButton btnCheckAll;
        private System.Windows.Forms.ToolStripButton btnUncheckAll;
        private System.Windows.Forms.ToolStripButton btnPrimaryKey;
        private System.Windows.Forms.ToolStripButton btnNoKeyColumns;
        private System.Windows.Forms.ToolStripButton btnSelectedColumns;
        private System.Windows.Forms.ToolStripButton btnNoSelectedColumns;
    }
}
