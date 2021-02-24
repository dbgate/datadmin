namespace Plugin.dbmodel
{
    partial class SynchronizeStructureForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SynchronizeStructureForm));
            this.splitContainerEx1 = new DatAdmin.SplitContainerEx();
            this.objectListTables = new BrightIdeasSoftware.ObjectListView();
            this.colObjectType = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn2 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn3 = new BrightIdeasSoftware.OLVColumn();
            this.colRelation = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn5 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn6 = new BrightIdeasSoftware.OLVColumn();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.diffControl1 = new Menees.DiffUtils.Controls.DiffControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.objectListColumns = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn11 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn1 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn4 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn7 = new BrightIdeasSoftware.OLVColumn();
            this.colColumnRelation = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn8 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn9 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn10 = new BrightIdeasSoftware.OLVColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.objectListIndexesKeys = new BrightIdeasSoftware.ObjectListView();
            this.colConstraintType = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn13 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn14 = new BrightIdeasSoftware.OLVColumn();
            this.colConstraintRelation = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn16 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn17 = new BrightIdeasSoftware.OLVColumn();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.codeEditor1 = new DatAdmin.CodeEditor();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.objectListViewActions = new BrightIdeasSoftware.ObjectListView();
            this.colObjType = new BrightIdeasSoftware.OLVColumn();
            this.colSql = new BrightIdeasSoftware.OLVColumn();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.messageLogFrame1 = new DatAdmin.MessageLogFrame();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnSynchronize = new System.Windows.Forms.ToolStripButton();
            this.btnSwap = new System.Windows.Forms.ToolStripButton();
            this.btnSettings = new System.Windows.Forms.ToolStripButton();
            this.btnAddToFavorites = new System.Windows.Forms.ToolStripButton();
            this.btnGenerateSql = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.swindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.srefreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sswapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSynchronize = new System.Windows.Forms.ToolStripMenuItem();
            this.sgeneratesqlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.scloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.soptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ssettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saddtofavoritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnRefreshSource = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labSourceDb = new System.Windows.Forms.Label();
            this.labSourceServer = new System.Windows.Forms.Label();
            this.cbxSelectSource = new System.Windows.Forms.ComboBox();
            this.btnSelectSourceDb = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cbxSelectTarget = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSelectTargetDb = new System.Windows.Forms.Button();
            this.labTargetDb = new System.Windows.Forms.Label();
            this.labTargetServer = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.labSynActions = new System.Windows.Forms.Label();
            this.btnUncheckAll = new System.Windows.Forms.Button();
            this.btnCheckAll = new System.Windows.Forms.Button();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnAdded = new System.Windows.Forms.ToolStripButton();
            this.btnRemoved = new System.Windows.Forms.ToolStripButton();
            this.btnChanged = new System.Windows.Forms.ToolStripButton();
            this.btnEqual = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.splitContainerEx1.Panel1.SuspendLayout();
            this.splitContainerEx1.Panel2.SuspendLayout();
            this.splitContainerEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListTables)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListColumns)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListIndexesKeys)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewActions)).BeginInit();
            this.tabPage6.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerEx1
            // 
            this.splitContainerEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerEx1.Location = new System.Drawing.Point(0, 128);
            this.splitContainerEx1.MoreFixedPanel = DatAdmin.SplitContainerEx.PanelType.First;
            this.splitContainerEx1.Name = "splitContainerEx1";
            this.splitContainerEx1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerEx1.Panel1
            // 
            this.splitContainerEx1.Panel1.Controls.Add(this.objectListTables);
            this.splitContainerEx1.Panel1DynamicCollapsed = false;
            this.splitContainerEx1.Panel1MinDynamicSize = 0;
            // 
            // splitContainerEx1.Panel2
            // 
            this.splitContainerEx1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainerEx1.Panel2DynamicCollapsed = false;
            this.splitContainerEx1.Panel2MinDynamicSize = 0;
            this.splitContainerEx1.Size = new System.Drawing.Size(776, 282);
            this.splitContainerEx1.SplitterDistance = 190;
            this.splitContainerEx1.SplitterWidth = 6;
            this.splitContainerEx1.TabIndex = 0;
            // 
            // objectListTables
            // 
            this.objectListTables.AllColumns.Add(this.colObjectType);
            this.objectListTables.AllColumns.Add(this.olvColumn2);
            this.objectListTables.AllColumns.Add(this.olvColumn3);
            this.objectListTables.AllColumns.Add(this.colRelation);
            this.objectListTables.AllColumns.Add(this.olvColumn5);
            this.objectListTables.AllColumns.Add(this.olvColumn6);
            this.objectListTables.CheckBoxes = true;
            this.objectListTables.CheckedAspectName = "IsChecked";
            this.objectListTables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colObjectType,
            this.olvColumn2,
            this.olvColumn3,
            this.colRelation,
            this.olvColumn5,
            this.olvColumn6});
            this.objectListTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListTables.FullRowSelect = true;
            this.objectListTables.HideSelection = false;
            this.objectListTables.Location = new System.Drawing.Point(0, 0);
            this.objectListTables.Name = "objectListTables";
            this.objectListTables.ShowGroups = false;
            this.objectListTables.ShowImagesOnSubItems = true;
            this.objectListTables.Size = new System.Drawing.Size(776, 190);
            this.objectListTables.SmallImageList = this.imageList1;
            this.objectListTables.TabIndex = 1;
            this.objectListTables.UseCompatibleStateImageBehavior = false;
            this.objectListTables.View = System.Windows.Forms.View.Details;
            this.objectListTables.SelectedIndexChanged += new System.EventHandler(this.objectListView1_SelectedIndexChanged);
            // 
            // colObjectType
            // 
            this.colObjectType.AspectName = "ObjectType";
            this.colObjectType.ImageAspectName = "";
            this.colObjectType.Text = "s_type";
            this.colObjectType.Width = 100;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "SourceSchema";
            this.olvColumn2.FillsFreeSpace = true;
            this.olvColumn2.Text = "s_schema";
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "SourceName";
            this.olvColumn3.FillsFreeSpace = true;
            this.olvColumn3.Text = "s_name";
            this.olvColumn3.Width = 120;
            // 
            // colRelation
            // 
            this.colRelation.AspectName = "";
            this.colRelation.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colRelation.ImageAspectName = "";
            this.colRelation.Text = "s_relation";
            this.colRelation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "TargetName";
            this.olvColumn5.FillsFreeSpace = true;
            this.olvColumn5.Text = "s_name";
            this.olvColumn5.Width = 120;
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "TargetSchema";
            this.olvColumn6.FillsFreeSpace = true;
            this.olvColumn6.Text = "s_schema";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 86);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.diffControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(768, 60);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "DDL";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // diffControl1
            // 
            this.diffControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diffControl1.Location = new System.Drawing.Point(3, 3);
            this.diffControl1.Name = "diffControl1";
            this.diffControl1.ShowWhitespaceInLineDiff = true;
            this.diffControl1.Size = new System.Drawing.Size(762, 54);
            this.diffControl1.TabIndex = 0;
            this.diffControl1.ViewFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.objectListColumns);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(768, 60);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "s_columns";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // objectListColumns
            // 
            this.objectListColumns.AllColumns.Add(this.olvColumn11);
            this.objectListColumns.AllColumns.Add(this.olvColumn1);
            this.objectListColumns.AllColumns.Add(this.olvColumn4);
            this.objectListColumns.AllColumns.Add(this.olvColumn7);
            this.objectListColumns.AllColumns.Add(this.colColumnRelation);
            this.objectListColumns.AllColumns.Add(this.olvColumn8);
            this.objectListColumns.AllColumns.Add(this.olvColumn9);
            this.objectListColumns.AllColumns.Add(this.olvColumn10);
            this.objectListColumns.CheckBoxes = true;
            this.objectListColumns.CheckedAspectName = "IsChecked";
            this.objectListColumns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn11,
            this.olvColumn1,
            this.olvColumn4,
            this.olvColumn7,
            this.colColumnRelation,
            this.olvColumn8,
            this.olvColumn9,
            this.olvColumn10});
            this.objectListColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListColumns.FullRowSelect = true;
            this.objectListColumns.HideSelection = false;
            this.objectListColumns.Location = new System.Drawing.Point(3, 3);
            this.objectListColumns.Name = "objectListColumns";
            this.objectListColumns.ShowGroups = false;
            this.objectListColumns.ShowImagesOnSubItems = true;
            this.objectListColumns.Size = new System.Drawing.Size(762, 54);
            this.objectListColumns.SmallImageList = this.imageList1;
            this.objectListColumns.TabIndex = 0;
            this.objectListColumns.UseCompatibleStateImageBehavior = false;
            this.objectListColumns.UseSubItemCheckBoxes = true;
            this.objectListColumns.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn11
            // 
            this.olvColumn11.Text = "";
            this.olvColumn11.Width = 100;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "SourceColumnName";
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.Text = "s_name";
            this.olvColumn1.Width = 120;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "SourceColumnType";
            this.olvColumn4.FillsFreeSpace = true;
            this.olvColumn4.Text = "s_type";
            this.olvColumn4.Width = 120;
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "SourceColumnNullable";
            this.olvColumn7.CheckBoxes = true;
            this.olvColumn7.Text = "s_nullable";
            // 
            // colColumnRelation
            // 
            this.colColumnRelation.Text = "s_relation";
            // 
            // olvColumn8
            // 
            this.olvColumn8.AspectName = "TargetColumnNullable";
            this.olvColumn8.CheckBoxes = true;
            this.olvColumn8.Text = "s_nullable";
            // 
            // olvColumn9
            // 
            this.olvColumn9.AspectName = "TargetColumnType";
            this.olvColumn9.FillsFreeSpace = true;
            this.olvColumn9.Text = "s_type";
            this.olvColumn9.Width = 120;
            // 
            // olvColumn10
            // 
            this.olvColumn10.AspectName = "TargetColumnName";
            this.olvColumn10.FillsFreeSpace = true;
            this.olvColumn10.Text = "s_name";
            this.olvColumn10.Width = 120;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.objectListIndexesKeys);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(768, 60);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "s_indexes_keys";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // objectListIndexesKeys
            // 
            this.objectListIndexesKeys.AllColumns.Add(this.colConstraintType);
            this.objectListIndexesKeys.AllColumns.Add(this.olvColumn13);
            this.objectListIndexesKeys.AllColumns.Add(this.olvColumn14);
            this.objectListIndexesKeys.AllColumns.Add(this.colConstraintRelation);
            this.objectListIndexesKeys.AllColumns.Add(this.olvColumn16);
            this.objectListIndexesKeys.AllColumns.Add(this.olvColumn17);
            this.objectListIndexesKeys.CheckBoxes = true;
            this.objectListIndexesKeys.CheckedAspectName = "IsChecked";
            this.objectListIndexesKeys.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colConstraintType,
            this.olvColumn13,
            this.olvColumn14,
            this.colConstraintRelation,
            this.olvColumn16,
            this.olvColumn17});
            this.objectListIndexesKeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListIndexesKeys.FullRowSelect = true;
            this.objectListIndexesKeys.HideSelection = false;
            this.objectListIndexesKeys.Location = new System.Drawing.Point(3, 3);
            this.objectListIndexesKeys.Name = "objectListIndexesKeys";
            this.objectListIndexesKeys.ShowGroups = false;
            this.objectListIndexesKeys.ShowImagesOnSubItems = true;
            this.objectListIndexesKeys.Size = new System.Drawing.Size(762, 54);
            this.objectListIndexesKeys.SmallImageList = this.imageList1;
            this.objectListIndexesKeys.TabIndex = 0;
            this.objectListIndexesKeys.UseCompatibleStateImageBehavior = false;
            this.objectListIndexesKeys.View = System.Windows.Forms.View.Details;
            // 
            // colConstraintType
            // 
            this.colConstraintType.AspectName = "ConstraintType";
            this.colConstraintType.Text = "s_type";
            this.colConstraintType.Width = 100;
            // 
            // olvColumn13
            // 
            this.olvColumn13.AspectName = "SourceConstraintName";
            this.olvColumn13.FillsFreeSpace = true;
            this.olvColumn13.Text = "s_name";
            // 
            // olvColumn14
            // 
            this.olvColumn14.AspectName = "SourceConstraintColumns";
            this.olvColumn14.FillsFreeSpace = true;
            this.olvColumn14.Text = "s_columns";
            // 
            // colConstraintRelation
            // 
            this.colConstraintRelation.Text = "s_relation";
            // 
            // olvColumn16
            // 
            this.olvColumn16.AspectName = "TargetConstraintColumns";
            this.olvColumn16.FillsFreeSpace = true;
            this.olvColumn16.Text = "s_columns";
            // 
            // olvColumn17
            // 
            this.olvColumn17.AspectName = "TargetConstraintName";
            this.olvColumn17.FillsFreeSpace = true;
            this.olvColumn17.Text = "s_name";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.codeEditor1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(768, 60);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "s_script";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // codeEditor1
            // 
            this.codeEditor1.Connection = null;
            this.codeEditor1.Dialect = null;
            this.codeEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeEditor1.IsReadOnly = false;
            this.codeEditor1.Language = DatAdmin.CodeLanguage.Sql;
            this.codeEditor1.Location = new System.Drawing.Point(3, 3);
            this.codeEditor1.Modified = true;
            this.codeEditor1.Name = "codeEditor1";
            this.codeEditor1.ReadOnly = false;
            this.codeEditor1.Size = new System.Drawing.Size(762, 54);
            this.codeEditor1.TabIndex = 0;
            this.codeEditor1.Text = "codeEditor1";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.objectListViewActions);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(768, 60);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "s_actions";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // objectListViewActions
            // 
            this.objectListViewActions.AllColumns.Add(this.colObjType);
            this.objectListViewActions.AllColumns.Add(this.colSql);
            this.objectListViewActions.CheckBoxes = true;
            this.objectListViewActions.CheckedAspectName = "IsChecked";
            this.objectListViewActions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colObjType,
            this.colSql});
            this.objectListViewActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListViewActions.FullRowSelect = true;
            this.objectListViewActions.HideSelection = false;
            this.objectListViewActions.Location = new System.Drawing.Point(3, 3);
            this.objectListViewActions.Name = "objectListViewActions";
            this.objectListViewActions.ShowGroups = false;
            this.objectListViewActions.ShowImagesOnSubItems = true;
            this.objectListViewActions.Size = new System.Drawing.Size(762, 54);
            this.objectListViewActions.SmallImageList = this.imageList1;
            this.objectListViewActions.TabIndex = 1;
            this.objectListViewActions.UseCompatibleStateImageBehavior = false;
            this.objectListViewActions.View = System.Windows.Forms.View.Details;
            // 
            // colObjType
            // 
            this.colObjType.Text = "s_type";
            this.colObjType.Width = 100;
            // 
            // colSql
            // 
            this.colSql.AspectName = "ActionSqlTitle";
            this.colSql.FillsFreeSpace = true;
            this.colSql.Text = "SQL";
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.messageLogFrame1);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(768, 60);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "s_trace";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // messageLogFrame1
            // 
            this.messageLogFrame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageLogFrame1.Location = new System.Drawing.Point(3, 3);
            this.messageLogFrame1.Name = "messageLogFrame1";
            this.messageLogFrame1.Size = new System.Drawing.Size(762, 54);
            this.messageLogFrame1.Source = null;
            this.messageLogFrame1.Style = DatAdmin.MessageFrameStyle.List;
            this.messageLogFrame1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefresh,
            this.btnSynchronize,
            this.btnSwap,
            this.btnSettings,
            this.btnAddToFavorites,
            this.btnGenerateSql});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(776, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::Plugin.dbmodel.ModRes.refresh;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(73, 22);
            this.btnRefresh.Text = "s_refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSynchronize
            // 
            this.btnSynchronize.Image = global::Plugin.dbmodel.ModRes.synchronize;
            this.btnSynchronize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSynchronize.Name = "btnSynchronize";
            this.btnSynchronize.Size = new System.Drawing.Size(100, 22);
            this.btnSynchronize.Text = "s_synchronize";
            this.btnSynchronize.Click += new System.EventHandler(this.btnSynchronize_Click);
            // 
            // btnSwap
            // 
            this.btnSwap.Image = global::Plugin.dbmodel.ModRes.swap;
            this.btnSwap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSwap.Name = "btnSwap";
            this.btnSwap.Size = new System.Drawing.Size(64, 22);
            this.btnSwap.Text = "s_swap";
            this.btnSwap.Click += new System.EventHandler(this.sswapToolStripMenuItem_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Image = global::Plugin.dbmodel.ModRes.properties;
            this.btnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(78, 22);
            this.btnSettings.Text = "s_settings";
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnAddToFavorites
            // 
            this.btnAddToFavorites.Image = global::Plugin.dbmodel.ModRes.favorite_add;
            this.btnAddToFavorites.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddToFavorites.Name = "btnAddToFavorites";
            this.btnAddToFavorites.Size = new System.Drawing.Size(123, 22);
            this.btnAddToFavorites.Text = "s_add_to_favorites";
            this.btnAddToFavorites.Click += new System.EventHandler(this.btnAddToFavorites_Click);
            // 
            // btnGenerateSql
            // 
            this.btnGenerateSql.Image = global::Plugin.dbmodel.ModRes.generate_sql;
            this.btnGenerateSql.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerateSql.Name = "btnGenerateSql";
            this.btnGenerateSql.Size = new System.Drawing.Size(103, 22);
            this.btnGenerateSql.Text = "s_generate_sql";
            this.btnGenerateSql.Click += new System.EventHandler(this.btnGenerateSql_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.swindowToolStripMenuItem,
            this.soptionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(776, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // swindowToolStripMenuItem
            // 
            this.swindowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.srefreshToolStripMenuItem,
            this.sswapToolStripMenuItem,
            this.mnuSynchronize,
            this.sgeneratesqlToolStripMenuItem,
            this.toolStripMenuItem1,
            this.scloseToolStripMenuItem});
            this.swindowToolStripMenuItem.Name = "swindowToolStripMenuItem";
            this.swindowToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.swindowToolStripMenuItem.Text = "s_synchronize";
            // 
            // srefreshToolStripMenuItem
            // 
            this.srefreshToolStripMenuItem.Image = global::Plugin.dbmodel.ModRes.refresh;
            this.srefreshToolStripMenuItem.Name = "srefreshToolStripMenuItem";
            this.srefreshToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.srefreshToolStripMenuItem.Text = "s_refresh";
            this.srefreshToolStripMenuItem.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // sswapToolStripMenuItem
            // 
            this.sswapToolStripMenuItem.Name = "sswapToolStripMenuItem";
            this.sswapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.sswapToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.sswapToolStripMenuItem.Text = "s_swap";
            this.sswapToolStripMenuItem.Click += new System.EventHandler(this.sswapToolStripMenuItem_Click);
            // 
            // mnuSynchronize
            // 
            this.mnuSynchronize.Image = global::Plugin.dbmodel.ModRes.synchronize;
            this.mnuSynchronize.Name = "mnuSynchronize";
            this.mnuSynchronize.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.mnuSynchronize.Size = new System.Drawing.Size(192, 22);
            this.mnuSynchronize.Text = "s_synchronize";
            this.mnuSynchronize.Click += new System.EventHandler(this.btnSynchronize_Click);
            // 
            // sgeneratesqlToolStripMenuItem
            // 
            this.sgeneratesqlToolStripMenuItem.Image = global::Plugin.dbmodel.ModRes.generate_sql;
            this.sgeneratesqlToolStripMenuItem.Name = "sgeneratesqlToolStripMenuItem";
            this.sgeneratesqlToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.sgeneratesqlToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.sgeneratesqlToolStripMenuItem.Text = "s_generate_sql";
            this.sgeneratesqlToolStripMenuItem.Click += new System.EventHandler(this.btnGenerateSql_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(189, 6);
            // 
            // scloseToolStripMenuItem
            // 
            this.scloseToolStripMenuItem.Name = "scloseToolStripMenuItem";
            this.scloseToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.scloseToolStripMenuItem.Text = "s_close";
            this.scloseToolStripMenuItem.Click += new System.EventHandler(this.scloseToolStripMenuItem_Click);
            // 
            // soptionsToolStripMenuItem
            // 
            this.soptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ssettingsToolStripMenuItem,
            this.saddtofavoritesToolStripMenuItem});
            this.soptionsToolStripMenuItem.Name = "soptionsToolStripMenuItem";
            this.soptionsToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.soptionsToolStripMenuItem.Text = "s_options";
            // 
            // ssettingsToolStripMenuItem
            // 
            this.ssettingsToolStripMenuItem.Image = global::Plugin.dbmodel.ModRes.properties;
            this.ssettingsToolStripMenuItem.Name = "ssettingsToolStripMenuItem";
            this.ssettingsToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.ssettingsToolStripMenuItem.Text = "s_settings";
            this.ssettingsToolStripMenuItem.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // saddtofavoritesToolStripMenuItem
            // 
            this.saddtofavoritesToolStripMenuItem.Image = global::Plugin.dbmodel.ModRes.favorite_add;
            this.saddtofavoritesToolStripMenuItem.Name = "saddtofavoritesToolStripMenuItem";
            this.saddtofavoritesToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.saddtofavoritesToolStripMenuItem.Text = "s_add_to_favorites";
            this.saddtofavoritesToolStripMenuItem.Click += new System.EventHandler(this.btnAddToFavorites_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 72);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 56);
            this.panel1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(776, 56);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(417, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(44, 46);
            this.panel2.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnRefreshSource);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.labSourceDb);
            this.panel3.Controls.Add(this.labSourceServer);
            this.panel3.Controls.Add(this.cbxSelectSource);
            this.panel3.Controls.Add(this.btnSelectSourceDb);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(107, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(302, 46);
            this.panel3.TabIndex = 1;
            this.panel3.Resize += new System.EventHandler(this.panel3_Resize);
            // 
            // btnRefreshSource
            // 
            this.btnRefreshSource.Image = global::Plugin.dbmodel.ModRes.refresh;
            this.btnRefreshSource.Location = new System.Drawing.Point(118, 23);
            this.btnRefreshSource.Name = "btnRefreshSource";
            this.btnRefreshSource.Size = new System.Drawing.Size(38, 23);
            this.btnRefreshSource.TabIndex = 4;
            this.btnRefreshSource.UseVisualStyleBackColor = true;
            this.btnRefreshSource.Click += new System.EventHandler(this.btnRefreshSource_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(19, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "s_source";
            // 
            // labSourceDb
            // 
            this.labSourceDb.AutoSize = true;
            this.labSourceDb.Location = new System.Drawing.Point(242, 28);
            this.labSourceDb.Name = "labSourceDb";
            this.labSourceDb.Size = new System.Drawing.Size(35, 13);
            this.labSourceDb.TabIndex = 1;
            this.labSourceDb.Text = "label2";
            // 
            // labSourceServer
            // 
            this.labSourceServer.AutoSize = true;
            this.labSourceServer.Location = new System.Drawing.Point(242, 3);
            this.labSourceServer.Name = "labSourceServer";
            this.labSourceServer.Size = new System.Drawing.Size(35, 13);
            this.labSourceServer.TabIndex = 0;
            this.labSourceServer.Text = "label1";
            // 
            // cbxSelectSource
            // 
            this.cbxSelectSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSelectSource.FormattingEnabled = true;
            this.cbxSelectSource.Location = new System.Drawing.Point(4, 24);
            this.cbxSelectSource.Name = "cbxSelectSource";
            this.cbxSelectSource.Size = new System.Drawing.Size(108, 21);
            this.cbxSelectSource.TabIndex = 5;
            this.cbxSelectSource.SelectedIndexChanged += new System.EventHandler(this.cbxSelectSource_SelectedIndexChanged);
            // 
            // btnSelectSourceDb
            // 
            this.btnSelectSourceDb.Location = new System.Drawing.Point(3, 23);
            this.btnSelectSourceDb.Name = "btnSelectSourceDb";
            this.btnSelectSourceDb.Size = new System.Drawing.Size(109, 23);
            this.btnSelectSourceDb.TabIndex = 2;
            this.btnSelectSourceDb.Text = "s_select_database";
            this.btnSelectSourceDb.UseVisualStyleBackColor = true;
            this.btnSelectSourceDb.Click += new System.EventHandler(this.tbnChangeSource_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.cbxSelectTarget);
            this.panel4.Controls.Add(this.button1);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.btnSelectTargetDb);
            this.panel4.Controls.Add(this.labTargetDb);
            this.panel4.Controls.Add(this.labTargetServer);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(469, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(302, 46);
            this.panel4.TabIndex = 2;
            // 
            // cbxSelectTarget
            // 
            this.cbxSelectTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxSelectTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSelectTarget.FormattingEnabled = true;
            this.cbxSelectTarget.Location = new System.Drawing.Point(191, 25);
            this.cbxSelectTarget.Name = "cbxSelectTarget";
            this.cbxSelectTarget.Size = new System.Drawing.Size(108, 21);
            this.cbxSelectTarget.TabIndex = 6;
            this.cbxSelectTarget.SelectedIndexChanged += new System.EventHandler(this.cbxSelectTarget_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Image = global::Plugin.dbmodel.ModRes.refresh;
            this.button1.Location = new System.Drawing.Point(146, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(38, 23);
            this.button1.TabIndex = 5;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnRefreshTarget_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(203, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "s_target";
            // 
            // btnSelectTargetDb
            // 
            this.btnSelectTargetDb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectTargetDb.Location = new System.Drawing.Point(190, 23);
            this.btnSelectTargetDb.Name = "btnSelectTargetDb";
            this.btnSelectTargetDb.Size = new System.Drawing.Size(109, 23);
            this.btnSelectTargetDb.TabIndex = 3;
            this.btnSelectTargetDb.Text = "s_select_database";
            this.btnSelectTargetDb.UseVisualStyleBackColor = true;
            this.btnSelectTargetDb.Click += new System.EventHandler(this.btnChangeTarget_Click);
            // 
            // labTargetDb
            // 
            this.labTargetDb.AutoSize = true;
            this.labTargetDb.Location = new System.Drawing.Point(7, 27);
            this.labTargetDb.Name = "labTargetDb";
            this.labTargetDb.Size = new System.Drawing.Size(35, 13);
            this.labTargetDb.TabIndex = 1;
            this.labTargetDb.Text = "label4";
            // 
            // labTargetServer
            // 
            this.labTargetServer.AutoSize = true;
            this.labTargetServer.Location = new System.Drawing.Point(7, 4);
            this.labTargetServer.Name = "labTargetServer";
            this.labTargetServer.Size = new System.Drawing.Size(35, 13);
            this.labTargetServer.TabIndex = 0;
            this.labTargetServer.Text = "label3";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.labSynActions);
            this.panel5.Controls.Add(this.btnUncheckAll);
            this.panel5.Controls.Add(this.btnCheckAll);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(5, 5);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(94, 46);
            this.panel5.TabIndex = 3;
            // 
            // labSynActions
            // 
            this.labSynActions.AutoSize = true;
            this.labSynActions.Location = new System.Drawing.Point(9, 3);
            this.labSynActions.Name = "labSynActions";
            this.labSynActions.Size = new System.Drawing.Size(24, 13);
            this.labSynActions.TabIndex = 2;
            this.labSynActions.Text = "0/0";
            // 
            // btnUncheckAll
            // 
            this.btnUncheckAll.Image = global::Plugin.dbmodel.ModRes.checkall_no;
            this.btnUncheckAll.Location = new System.Drawing.Point(39, 23);
            this.btnUncheckAll.Name = "btnUncheckAll";
            this.btnUncheckAll.Size = new System.Drawing.Size(29, 23);
            this.btnUncheckAll.TabIndex = 1;
            this.btnUncheckAll.UseVisualStyleBackColor = true;
            this.btnUncheckAll.Click += new System.EventHandler(this.btnUncheckAll_Click);
            // 
            // btnCheckAll
            // 
            this.btnCheckAll.Image = global::Plugin.dbmodel.ModRes.checkall;
            this.btnCheckAll.Location = new System.Drawing.Point(3, 23);
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.Size = new System.Drawing.Size(30, 23);
            this.btnCheckAll.TabIndex = 0;
            this.btnCheckAll.UseVisualStyleBackColor = true;
            this.btnCheckAll.Click += new System.EventHandler(this.btnCheckAll_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdded,
            this.btnRemoved,
            this.btnChanged,
            this.btnEqual,
            this.toolStripSeparator1});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip2.Location = new System.Drawing.Point(0, 49);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(776, 23);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnAdded
            // 
            this.btnAdded.Checked = true;
            this.btnAdded.CheckOnClick = true;
            this.btnAdded.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnAdded.Image = global::Plugin.dbmodel.ModRes.add;
            this.btnAdded.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdded.Name = "btnAdded";
            this.btnAdded.Size = new System.Drawing.Size(70, 20);
            this.btnAdded.Text = "s_added";
            this.btnAdded.Click += new System.EventHandler(this.btnObjFilter_Click);
            // 
            // btnRemoved
            // 
            this.btnRemoved.Checked = true;
            this.btnRemoved.CheckOnClick = true;
            this.btnRemoved.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnRemoved.Image = global::Plugin.dbmodel.ModRes.remove;
            this.btnRemoved.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoved.Name = "btnRemoved";
            this.btnRemoved.Size = new System.Drawing.Size(84, 20);
            this.btnRemoved.Text = "s_removed";
            this.btnRemoved.Click += new System.EventHandler(this.btnObjFilter_Click);
            // 
            // btnChanged
            // 
            this.btnChanged.Checked = true;
            this.btnChanged.CheckOnClick = true;
            this.btnChanged.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnChanged.Image = global::Plugin.dbmodel.ModRes.pen;
            this.btnChanged.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnChanged.Name = "btnChanged";
            this.btnChanged.Size = new System.Drawing.Size(83, 20);
            this.btnChanged.Text = "s_changed";
            this.btnChanged.Click += new System.EventHandler(this.btnObjFilter_Click);
            // 
            // btnEqual
            // 
            this.btnEqual.CheckOnClick = true;
            this.btnEqual.Image = global::Plugin.dbmodel.ModRes.equals;
            this.btnEqual.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEqual.Name = "btnEqual";
            this.btnEqual.Size = new System.Drawing.Size(66, 20);
            this.btnEqual.Text = "s_equal";
            this.btnEqual.Click += new System.EventHandler(this.btnObjFilter_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::Plugin.dbmodel.ModRes.right2;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(44, 46);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // SynchronizeStructureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 410);
            this.Controls.Add(this.splitContainerEx1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SynchronizeStructureForm";
            this.Text = "s_synchronize_structure";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SynchronizeStructureForm_KeyDown);
            this.splitContainerEx1.Panel1.ResumeLayout(false);
            this.splitContainerEx1.Panel2.ResumeLayout(false);
            this.splitContainerEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectListTables)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectListColumns)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectListIndexesKeys)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewActions)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DatAdmin.SplitContainerEx splitContainerEx1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem swindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scloseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem srefreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.ToolStripMenuItem sswapToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnSynchronize;
        private System.Windows.Forms.ToolStripMenuItem mnuSynchronize;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton btnSwap;
        private System.Windows.Forms.ToolStripButton btnSettings;
        private System.Windows.Forms.ToolStripButton btnAddToFavorites;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private Menees.DiffUtils.Controls.DiffControl diffControl1;
        private DatAdmin.CodeEditor codeEditor1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label labSourceServer;
        private System.Windows.Forms.Label labSourceDb;
        private System.Windows.Forms.Label labTargetDb;
        private System.Windows.Forms.Label labTargetServer;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnAdded;
        private System.Windows.Forms.ToolStripButton btnRemoved;
        private System.Windows.Forms.ToolStripButton btnEqual;
        private System.Windows.Forms.ToolStripButton btnChanged;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private BrightIdeasSoftware.ObjectListView objectListTables;
        private BrightIdeasSoftware.OLVColumn colObjectType;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn colRelation;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private System.Windows.Forms.Button btnSelectSourceDb;
        private System.Windows.Forms.Button btnSelectTargetDb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private BrightIdeasSoftware.ObjectListView objectListColumns;
        private BrightIdeasSoftware.OLVColumn olvColumn11;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn7;
        private BrightIdeasSoftware.OLVColumn colColumnRelation;
        private BrightIdeasSoftware.OLVColumn olvColumn8;
        private BrightIdeasSoftware.OLVColumn olvColumn9;
        private BrightIdeasSoftware.OLVColumn olvColumn10;
        private System.Windows.Forms.Button btnRefreshSource;
        private System.Windows.Forms.Button button1;
        private BrightIdeasSoftware.ObjectListView objectListIndexesKeys;
        private BrightIdeasSoftware.OLVColumn colConstraintType;
        private BrightIdeasSoftware.OLVColumn olvColumn13;
        private BrightIdeasSoftware.OLVColumn olvColumn14;
        private BrightIdeasSoftware.OLVColumn colConstraintRelation;
        private BrightIdeasSoftware.OLVColumn olvColumn16;
        private BrightIdeasSoftware.OLVColumn olvColumn17;
        private BrightIdeasSoftware.ObjectListView objectListViewActions;
        private BrightIdeasSoftware.OLVColumn colObjType;
        private BrightIdeasSoftware.OLVColumn colSql;
        private System.Windows.Forms.ToolStripButton btnGenerateSql;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnUncheckAll;
        private System.Windows.Forms.Button btnCheckAll;
        private System.Windows.Forms.Label labSynActions;
        private System.Windows.Forms.TabPage tabPage6;
        private DatAdmin.MessageLogFrame messageLogFrame1;
        private System.Windows.Forms.ToolStripMenuItem sgeneratesqlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem soptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ssettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saddtofavoritesToolStripMenuItem;
        private System.Windows.Forms.ComboBox cbxSelectSource;
        private System.Windows.Forms.ComboBox cbxSelectTarget;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}