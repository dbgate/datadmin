namespace DatAdmin
{
    partial class QueryDesignTableFrame
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labTableName = new System.Windows.Forms.Label();
            this.contextMenuStripTable = new DatAdmin.ContextMenuStripEx(this.components);
            this.mnuRemoveTable = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddAllColumns = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRemoveAllColumns = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddReferencedTable = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRemoveTable = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnMenu = new System.Windows.Forms.Button();
            this.panColParent = new System.Windows.Forms.Panel();
            this.panCols = new DatAdmin.PanelEx();
            this.panel1.SuspendLayout();
            this.contextMenuStripTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panColParent.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.labTableName);
            this.panel1.Controls.Add(this.btnRemoveTable);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(123, 18);
            this.panel1.TabIndex = 1;
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.windowTitle_MouseMove);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.windowTitle_MouseDown);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.windowTitle_MouseUp);
            // 
            // labTableName
            // 
            this.labTableName.ContextMenuStrip = this.contextMenuStripTable;
            this.labTableName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labTableName.Location = new System.Drawing.Point(0, 0);
            this.labTableName.Name = "labTableName";
            this.labTableName.Size = new System.Drawing.Size(105, 18);
            this.labTableName.TabIndex = 1;
            this.labTableName.Text = "TABLE NAME";
            this.labTableName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.windowTitle_MouseMove);
            this.labTableName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.windowTitle_MouseDown);
            this.labTableName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.windowTitle_MouseUp);
            // 
            // contextMenuStripTable
            // 
            this.contextMenuStripTable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRemoveTable,
            this.mnuAddAllColumns,
            this.mnuRemoveAllColumns,
            this.mnuAddReferencedTable,
            this.mnuRefresh});
            this.contextMenuStripTable.Name = "contextMenuStrip1";
            this.contextMenuStripTable.Size = new System.Drawing.Size(197, 114);
            // 
            // mnuRemoveTable
            // 
            this.mnuRemoveTable.Name = "mnuRemoveTable";
            this.mnuRemoveTable.Size = new System.Drawing.Size(196, 22);
            this.mnuRemoveTable.Text = "s_remove_table";
            this.mnuRemoveTable.Click += new System.EventHandler(this.btnRemoveTable_Click);
            // 
            // mnuAddAllColumns
            // 
            this.mnuAddAllColumns.Name = "mnuAddAllColumns";
            this.mnuAddAllColumns.Size = new System.Drawing.Size(196, 22);
            this.mnuAddAllColumns.Text = "s_add_all_columns";
            this.mnuAddAllColumns.Click += new System.EventHandler(this.mnuAddAllColumns_Click);
            // 
            // mnuRemoveAllColumns
            // 
            this.mnuRemoveAllColumns.Name = "mnuRemoveAllColumns";
            this.mnuRemoveAllColumns.Size = new System.Drawing.Size(196, 22);
            this.mnuRemoveAllColumns.Text = "s_remove_all_columns";
            this.mnuRemoveAllColumns.Click += new System.EventHandler(this.mnuRemoveAllColumns_Click);
            // 
            // mnuAddReferencedTable
            // 
            this.mnuAddReferencedTable.Name = "mnuAddReferencedTable";
            this.mnuAddReferencedTable.Size = new System.Drawing.Size(196, 22);
            this.mnuAddReferencedTable.Text = "s_add_referenced_table";
            this.mnuAddReferencedTable.Click += new System.EventHandler(this.mnuAddReferencedTable_Click);
            // 
            // mnuRefresh
            // 
            this.mnuRefresh.Name = "mnuRefresh";
            this.mnuRefresh.Size = new System.Drawing.Size(196, 22);
            this.mnuRefresh.Text = "s_refresh";
            this.mnuRefresh.Click += new System.EventHandler(this.mnuRefresh_Click);
            // 
            // btnRemoveTable
            // 
            this.btnRemoveTable.BackColor = System.Drawing.SystemColors.Control;
            this.btnRemoveTable.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRemoveTable.Image = global::DatAdmin.StdIcons.close;
            this.btnRemoveTable.Location = new System.Drawing.Point(105, 0);
            this.btnRemoveTable.Margin = new System.Windows.Forms.Padding(0);
            this.btnRemoveTable.Name = "btnRemoveTable";
            this.btnRemoveTable.Size = new System.Drawing.Size(18, 18);
            this.btnRemoveTable.TabIndex = 0;
            this.btnRemoveTable.UseVisualStyleBackColor = false;
            this.btnRemoveTable.Click += new System.EventHandler(this.btnRemoveTable_Click);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Location = new System.Drawing.Point(0, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(123, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.pictureBox1.Image = global::DatAdmin.StdIcons.resize;
            this.pictureBox1.Location = new System.Drawing.Point(107, 141);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // btnMenu
            // 
            this.btnMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMenu.Location = new System.Drawing.Point(-1, 138);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(102, 19);
            this.btnMenu.TabIndex = 7;
            this.btnMenu.Text = "...";
            this.btnMenu.UseVisualStyleBackColor = true;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // panColParent
            // 
            this.panColParent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panColParent.AutoScroll = true;
            this.panColParent.BackColor = System.Drawing.SystemColors.Window;
            this.panColParent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panColParent.Controls.Add(this.panCols);
            this.panColParent.Location = new System.Drawing.Point(0, 35);
            this.panColParent.Name = "panColParent";
            this.panColParent.Size = new System.Drawing.Size(124, 100);
            this.panColParent.TabIndex = 6;
            this.panColParent.Scroll += new System.Windows.Forms.ScrollEventHandler(this.panColParent_Scroll);
            this.panColParent.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panColParent_MouseUp);
            // 
            // panCols
            // 
            this.panCols.AllowDrop = true;
            this.panCols.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panCols.Location = new System.Drawing.Point(0, 0);
            this.panCols.Name = "panCols";
            this.panCols.Size = new System.Drawing.Size(101, 74);
            this.panCols.TabIndex = 0;
            this.panCols.MouseLeave += new System.EventHandler(this.panCols_MouseLeave);
            this.panCols.Paint += new System.Windows.Forms.PaintEventHandler(this.panCols_Paint);
            this.panCols.DragOver += new System.Windows.Forms.DragEventHandler(this.panCols_DragOver);
            this.panCols.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panCols_MouseMove);
            this.panCols.DragDrop += new System.Windows.Forms.DragEventHandler(this.panCols_DragDrop);
            this.panCols.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panCols_MouseDown);
            this.panCols.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panCols_MouseUp);
            // 
            // QueryDesignTableFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panColParent);
            this.Controls.Add(this.btnMenu);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panel1);
            this.Name = "QueryDesignTableFrame";
            this.Size = new System.Drawing.Size(123, 157);
            this.panel1.ResumeLayout(false);
            this.contextMenuStripTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panColParent.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRemoveTable;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label labTableName;
        private DatAdmin.ContextMenuStripEx contextMenuStripTable;
        private System.Windows.Forms.ToolStripMenuItem mnuRemoveTable;
        private System.Windows.Forms.ToolStripMenuItem mnuAddAllColumns;
        private System.Windows.Forms.ToolStripMenuItem mnuRemoveAllColumns;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panColParent;
        private DatAdmin.PanelEx panCols;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuAddReferencedTable;
        private System.Windows.Forms.ToolStripMenuItem mnuRefresh;
    }
}
