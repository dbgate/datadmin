namespace Plugin.diagrams
{
    partial class DiagramEditFrame
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnAlterTable = new System.Windows.Forms.ToolStripButton();
            this.btnEditTables = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnOnlineHelp = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnAddToFavorites = new System.Windows.Forms.ToolStripButton();
            this.btnExportAsImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cbxStyle = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.cbxZoom = new System.Windows.Forms.ToolStripComboBox();
            this.btnReload = new System.Windows.Forms.ToolStripButton();
            this.drawPanelContainer = new System.Windows.Forms.Panel();
            this.labDragAndDrop = new System.Windows.Forms.Label();
            this.drawPanel = new DatAdmin.PanelEx();
            this.contextMenuStrip1 = new DatAdmin.ContextMenuStripEx(this.components);
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRename = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDesign = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new DatAdmin.SplitContainerEx();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.entityStyleFrame1 = new Plugin.diagrams.EntityStyleFrame();
            this.tbxStyleTableName = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.propertyFrame1 = new DatAdmin.PropertyFrame();
            this.toolStrip1.SuspendLayout();
            this.drawPanelContainer.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDelete,
            this.btnAlterTable,
            this.btnEditTables,
            this.toolStripSeparator1,
            this.btnOnlineHelp,
            this.btnAddToFavorites,
            this.btnExportAsImage,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.cbxStyle,
            this.toolStripSeparator3,
            this.toolStripLabel2,
            this.cbxZoom,
            this.btnReload});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(912, 23);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::Plugin.diagrams.StdIcons.delete;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(69, 20);
            this.btnDelete.Text = "s_delete";
            this.btnDelete.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // btnAlterTable
            // 
            this.btnAlterTable.Image = global::Plugin.diagrams.StdIcons.table_edit;
            this.btnAlterTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAlterTable.Name = "btnAlterTable";
            this.btnAlterTable.Size = new System.Drawing.Size(72, 20);
            this.btnAlterTable.Text = "s_design";
            this.btnAlterTable.Click += new System.EventHandler(this.btnAlterTable_Click);
            // 
            // btnEditTables
            // 
            this.btnEditTables.CheckOnClick = true;
            this.btnEditTables.Image = global::Plugin.diagrams.StdIcons.table;
            this.btnEditTables.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditTables.Name = "btnEditTables";
            this.btnEditTables.Size = new System.Drawing.Size(93, 20);
            this.btnEditTables.Text = "s_edit_tables";
            this.btnEditTables.Click += new System.EventHandler(this.btnEditTables_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // btnOnlineHelp
            // 
            this.btnOnlineHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOnlineHelp.Image = global::Plugin.diagrams.StdIcons.question;
            this.btnOnlineHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOnlineHelp.Name = "btnOnlineHelp";
            this.btnOnlineHelp.Size = new System.Drawing.Size(29, 20);
            this.btnOnlineHelp.Text = "toolStripDropDownButton1";
            // 
            // btnAddToFavorites
            // 
            this.btnAddToFavorites.Image = global::Plugin.diagrams.StdIcons.favorite_add;
            this.btnAddToFavorites.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddToFavorites.Name = "btnAddToFavorites";
            this.btnAddToFavorites.Size = new System.Drawing.Size(123, 20);
            this.btnAddToFavorites.Text = "s_add_to_favorites";
            this.btnAddToFavorites.Click += new System.EventHandler(this.btnAddToFavorites_Click);
            // 
            // btnExportAsImage
            // 
            this.btnExportAsImage.Image = global::Plugin.diagrams.StdIcons.save;
            this.btnExportAsImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportAsImage.Name = "btnExportAsImage";
            this.btnExportAsImage.Size = new System.Drawing.Size(124, 20);
            this.btnExportAsImage.Text = "s_export_as_image";
            this.btnExportAsImage.Click += new System.EventHandler(this.btnExportAsImage_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(41, 15);
            this.toolStripLabel1.Text = "s_style";
            // 
            // cbxStyle
            // 
            this.cbxStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStyle.Name = "cbxStyle";
            this.cbxStyle.Size = new System.Drawing.Size(121, 23);
            this.cbxStyle.SelectedIndexChanged += new System.EventHandler(this.cbxStyle_SelectedIndexChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 23);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(47, 15);
            this.toolStripLabel2.Text = "s_zoom";
            // 
            // cbxZoom
            // 
            this.cbxZoom.Items.AddRange(new object[] {
            "10 %",
            "20 %",
            "50 %",
            "65 %",
            "80 %",
            "100 %",
            "150 %",
            "200 %"});
            this.cbxZoom.Name = "cbxZoom";
            this.cbxZoom.Size = new System.Drawing.Size(75, 23);
            this.cbxZoom.TextChanged += new System.EventHandler(this.cbxZoom_TextChanged);
            // 
            // btnReload
            // 
            this.btnReload.Image = global::Plugin.diagrams.StdIcons.refresh;
            this.btnReload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(70, 20);
            this.btnReload.Text = "s_reload";
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // drawPanelContainer
            // 
            this.drawPanelContainer.AllowDrop = true;
            this.drawPanelContainer.AutoScroll = true;
            this.drawPanelContainer.BackColor = System.Drawing.Color.White;
            this.drawPanelContainer.Controls.Add(this.labDragAndDrop);
            this.drawPanelContainer.Controls.Add(this.drawPanel);
            this.drawPanelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawPanelContainer.Location = new System.Drawing.Point(0, 0);
            this.drawPanelContainer.Name = "drawPanelContainer";
            this.drawPanelContainer.Size = new System.Drawing.Size(689, 507);
            this.drawPanelContainer.TabIndex = 1;
            this.drawPanelContainer.DragOver += new System.Windows.Forms.DragEventHandler(this.drawPanel_DragOver);
            this.drawPanelContainer.DragDrop += new System.Windows.Forms.DragEventHandler(this.drawPanel_DragDrop);
            this.drawPanelContainer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawPanelContainer_MouseDown);
            // 
            // labDragAndDrop
            // 
            this.labDragAndDrop.AutoSize = true;
            this.labDragAndDrop.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labDragAndDrop.Location = new System.Drawing.Point(56, 103);
            this.labDragAndDrop.Name = "labDragAndDrop";
            this.labDragAndDrop.Size = new System.Drawing.Size(279, 25);
            this.labDragAndDrop.TabIndex = 0;
            this.labDragAndDrop.Text = "s_drag_and_drop_tables_here";
            // 
            // drawPanel
            // 
            this.drawPanel.AllowDrop = true;
            this.drawPanel.BackColor = System.Drawing.Color.White;
            this.drawPanel.ContextMenuStrip = this.contextMenuStrip1;
            this.drawPanel.Location = new System.Drawing.Point(0, 0);
            this.drawPanel.Name = "drawPanel";
            this.drawPanel.Size = new System.Drawing.Size(200, 100);
            this.drawPanel.TabIndex = 0;
            this.drawPanel.TabStop = true;
            this.drawPanel.DoubleClick += new System.EventHandler(this.btnAlterTable_Click);
            this.drawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.drawPanel_Paint);
            this.drawPanel.DragOver += new System.Windows.Forms.DragEventHandler(this.drawPanel_DragOver);
            this.drawPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawPanel_MouseMove);
            this.drawPanel.Leave += new System.EventHandler(this.drawPanel_Leave);
            this.drawPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.drawPanel_DragDrop);
            this.drawPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawPanel_MouseDown);
            this.drawPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawPanel_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDelete,
            this.mnuRename,
            this.mnuDesign});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(144, 70);
            // 
            // mnuDelete
            // 
            this.mnuDelete.Image = global::Plugin.diagrams.StdIcons.delete;
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.mnuDelete.Size = new System.Drawing.Size(143, 22);
            this.mnuDelete.Text = "s_delete";
            this.mnuDelete.Click += new System.EventHandler(this.sdeleteToolStripMenuItem_Click);
            // 
            // mnuRename
            // 
            this.mnuRename.Image = global::Plugin.diagrams.StdIcons.rename;
            this.mnuRename.Name = "mnuRename";
            this.mnuRename.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.mnuRename.Size = new System.Drawing.Size(143, 22);
            this.mnuRename.Text = "s_rename";
            this.mnuRename.Click += new System.EventHandler(this.srenameToolStripMenuItem_Click);
            // 
            // mnuDesign
            // 
            this.mnuDesign.Image = global::Plugin.diagrams.StdIcons.design;
            this.mnuDesign.Name = "mnuDesign";
            this.mnuDesign.Size = new System.Drawing.Size(143, 22);
            this.mnuDesign.Text = "s_design";
            this.mnuDesign.Click += new System.EventHandler(this.sdesignToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 23);
            this.splitContainer1.MoreFixedPanel = DatAdmin.SplitContainerEx.PanelType.First;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.drawPanelContainer);
            this.splitContainer1.Panel1DynamicCollapsed = false;
            this.splitContainer1.Panel1MinDynamicSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2DynamicCollapsed = false;
            this.splitContainer1.Panel2MinDynamicSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(912, 507);
            this.splitContainer1.SplitterDistance = 689;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(217, 507);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.entityStyleFrame1);
            this.tabPage2.Controls.Add(this.tbxStyleTableName);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(209, 481);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "s_table";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // entityStyleFrame1
            // 
            this.entityStyleFrame1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.entityStyleFrame1.Location = new System.Drawing.Point(3, 41);
            this.entityStyleFrame1.Name = "entityStyleFrame1";
            this.entityStyleFrame1.Size = new System.Drawing.Size(203, 414);
            this.entityStyleFrame1.TabIndex = 1;
            this.entityStyleFrame1.Changed += new System.EventHandler(this.entityStyleFrame1_Changed);
            // 
            // tbxStyleTableName
            // 
            this.tbxStyleTableName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxStyleTableName.Location = new System.Drawing.Point(19, 15);
            this.tbxStyleTableName.Name = "tbxStyleTableName";
            this.tbxStyleTableName.ReadOnly = true;
            this.tbxStyleTableName.Size = new System.Drawing.Size(172, 20);
            this.tbxStyleTableName.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.propertyFrame1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(209, 458);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "s_diagram";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // propertyFrame1
            // 
            this.propertyFrame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyFrame1.Location = new System.Drawing.Point(3, 3);
            this.propertyFrame1.Name = "propertyFrame1";
            this.propertyFrame1.SelectedObject = null;
            this.propertyFrame1.Size = new System.Drawing.Size(203, 452);
            this.propertyFrame1.TabIndex = 0;
            // 
            // DiagramEditFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "DiagramEditFrame";
            this.Size = new System.Drawing.Size(912, 530);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DiagramEditFrame_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.drawPanelContainer.ResumeLayout(false);
            this.drawPanelContainer.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel drawPanelContainer;
        private DatAdmin.PanelEx drawPanel;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private DatAdmin.ContextMenuStripEx contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuDelete;
        private System.Windows.Forms.ToolStripButton btnEditTables;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cbxStyle;
        private System.Windows.Forms.Label labDragAndDrop;
        private System.Windows.Forms.ToolStripComboBox cbxZoom;
        private System.Windows.Forms.ToolStripButton btnAlterTable;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripMenuItem mnuRename;
        private System.Windows.Forms.ToolStripMenuItem mnuDesign;
        private System.Windows.Forms.ToolStripButton btnAddToFavorites;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnExportAsImage;
        private DatAdmin.SplitContainerEx splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox tbxStyleTableName;
        private EntityStyleFrame entityStyleFrame1;
        private System.Windows.Forms.ToolStripButton btnReload;
        private System.Windows.Forms.ToolStripDropDownButton btnOnlineHelp;
        private System.Windows.Forms.TabPage tabPage1;
        private DatAdmin.PropertyFrame propertyFrame1;
    }
}
