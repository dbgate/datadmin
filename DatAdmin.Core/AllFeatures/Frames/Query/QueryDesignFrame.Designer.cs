namespace DatAdmin
{
    partial class QueryDesignFrame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryDesignFrame));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            DatAdmin.GenericDialect genericDialect2 = new DatAdmin.GenericDialect();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClearQuery = new System.Windows.Forms.ToolStripButton();
            this.btnShowSql = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUniqueRows = new System.Windows.Forms.ToolStripButton();
            this.btnShowTables = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new DatAdmin.SplitContainerEx();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labDragAndDrop = new System.Windows.Forms.Label();
            this.panelTables = new System.Windows.Forms.Panel();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lbxAvailableTables = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lbxAvailableViews = new System.Windows.Forms.ListBox();
            this.btnRefreshTables = new System.Windows.Forms.Button();
            this.btnAddTable = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabColumns = new System.Windows.Forms.TabPage();
            this.dataGridColumns = new System.Windows.Forms.DataGridView();
            this.colColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAlias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOutput = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colGrouping = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnMoveUp = new System.Windows.Forms.ToolStripButton();
            this.btnMoveDown = new System.Windows.Forms.ToolStripButton();
            this.btnRemoveColumn = new System.Windows.Forms.ToolStripButton();
            this.tabFilter = new System.Windows.Forms.TabPage();
            this.tabSorting = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnRemoveFromSort = new System.Windows.Forms.Button();
            this.btnAddToSort = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbxSortAvailable = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbxSortedColumns = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnSortOrder = new System.Windows.Forms.Button();
            this.btnSortDown = new System.Windows.Forms.Button();
            this.btnSortUp = new System.Windows.Forms.Button();
            this.tabSql = new System.Windows.Forms.TabPage();
            this.codeEditor1 = new DatAdmin.CodeEditor();
            this.tabMessages = new System.Windows.Forms.TabPage();
            this.messageLogFrame1 = new DatAdmin.MessageLogFrame();
            this.mnuColumns = new DatAdmin.ContextMenuStripEx(this.components);
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelTables.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabColumns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridColumns)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.tabSorting.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabSql.SuspendLayout();
            this.tabMessages.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "table.png");
            this.imageList1.Images.SetKeyName(1, "column.png");
            this.imageList1.Images.SetKeyName(2, "order_asc.png");
            this.imageList1.Images.SetKeyName(3, "sql.png");
            this.imageList1.Images.SetKeyName(4, "log.gif");
            this.imageList1.Images.SetKeyName(5, "filter2.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClearQuery,
            this.btnShowSql,
            this.toolStripSeparator1,
            this.btnUniqueRows,
            this.btnShowTables});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(559, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnClearQuery
            // 
            this.btnClearQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClearQuery.Image = global::DatAdmin.StdIcons._new;
            this.btnClearQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClearQuery.Name = "btnClearQuery";
            this.btnClearQuery.Size = new System.Drawing.Size(23, 22);
            this.btnClearQuery.Text = "s_clear_query";
            this.btnClearQuery.Click += new System.EventHandler(this.btnClearQuery_Click);
            // 
            // btnShowSql
            // 
            this.btnShowSql.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowSql.Image = global::DatAdmin.StdIcons.sql;
            this.btnShowSql.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowSql.Name = "btnShowSql";
            this.btnShowSql.Size = new System.Drawing.Size(23, 22);
            this.btnShowSql.Text = "s_show_sql";
            this.btnShowSql.Click += new System.EventHandler(this.btnShowSql_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnUniqueRows
            // 
            this.btnUniqueRows.CheckOnClick = true;
            this.btnUniqueRows.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnUniqueRows.Image = ((System.Drawing.Image)(resources.GetObject("btnUniqueRows.Image")));
            this.btnUniqueRows.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUniqueRows.Name = "btnUniqueRows";
            this.btnUniqueRows.Size = new System.Drawing.Size(88, 22);
            this.btnUniqueRows.Text = "s_unique_rows";
            this.btnUniqueRows.Click += new System.EventHandler(this.btnUniqueRows_Click);
            // 
            // btnShowTables
            // 
            this.btnShowTables.Checked = true;
            this.btnShowTables.CheckOnClick = true;
            this.btnShowTables.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnShowTables.Image = global::DatAdmin.StdIcons.table;
            this.btnShowTables.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowTables.Name = "btnShowTables";
            this.btnShowTables.Size = new System.Drawing.Size(68, 22);
            this.btnShowTables.Text = "s_tables";
            this.btnShowTables.Click += new System.EventHandler(this.btnShowTables_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.MoreFixedPanel = DatAdmin.SplitContainerEx.PanelType.First;
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.panelTables);
            this.splitContainer1.Panel1DynamicCollapsed = false;
            this.splitContainer1.Panel1MinDynamicSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2DynamicCollapsed = false;
            this.splitContainer1.Panel2MinDynamicSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(559, 427);
            this.splitContainer1.SplitterDistance = 209;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.labDragAndDrop);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(394, 209);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.DragOver += new System.Windows.Forms.DragEventHandler(this.panel1_DragOver);
            this.panel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.panel1_DragDrop);
            // 
            // labDragAndDrop
            // 
            this.labDragAndDrop.AutoSize = true;
            this.labDragAndDrop.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labDragAndDrop.Location = new System.Drawing.Point(59, 39);
            this.labDragAndDrop.Name = "labDragAndDrop";
            this.labDragAndDrop.Size = new System.Drawing.Size(279, 25);
            this.labDragAndDrop.TabIndex = 0;
            this.labDragAndDrop.Text = "s_drag_and_drop_tables_here";
            // 
            // panelTables
            // 
            this.panelTables.Controls.Add(this.tabControl2);
            this.panelTables.Controls.Add(this.btnRefreshTables);
            this.panelTables.Controls.Add(this.btnAddTable);
            this.panelTables.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelTables.Location = new System.Drawing.Point(394, 0);
            this.panelTables.Name = "panelTables";
            this.panelTables.Size = new System.Drawing.Size(165, 209);
            this.panelTables.TabIndex = 1;
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Location = new System.Drawing.Point(3, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(158, 170);
            this.tabControl2.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lbxAvailableTables);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(150, 144);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "s_tables";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lbxAvailableTables
            // 
            this.lbxAvailableTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxAvailableTables.FormattingEnabled = true;
            this.lbxAvailableTables.Location = new System.Drawing.Point(3, 3);
            this.lbxAvailableTables.Name = "lbxAvailableTables";
            this.lbxAvailableTables.Size = new System.Drawing.Size(144, 134);
            this.lbxAvailableTables.TabIndex = 1;
            this.lbxAvailableTables.DoubleClick += new System.EventHandler(this.btnAddTable_Click);
            this.lbxAvailableTables.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbxAvailableTables_MouseDown);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lbxAvailableViews);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(150, 144);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "s_dbviews";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lbxAvailableViews
            // 
            this.lbxAvailableViews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxAvailableViews.FormattingEnabled = true;
            this.lbxAvailableViews.Location = new System.Drawing.Point(3, 3);
            this.lbxAvailableViews.Name = "lbxAvailableViews";
            this.lbxAvailableViews.Size = new System.Drawing.Size(144, 134);
            this.lbxAvailableViews.TabIndex = 0;
            this.lbxAvailableViews.DoubleClick += new System.EventHandler(this.btnAddTable_Click);
            this.lbxAvailableViews.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbxAvailableViews_MouseDown);
            // 
            // btnRefreshTables
            // 
            this.btnRefreshTables.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefreshTables.Image = global::DatAdmin.StdIcons.refresh;
            this.btnRefreshTables.Location = new System.Drawing.Point(86, 179);
            this.btnRefreshTables.Name = "btnRefreshTables";
            this.btnRefreshTables.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshTables.TabIndex = 3;
            this.btnRefreshTables.Text = "s_refresh";
            this.btnRefreshTables.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefreshTables.UseVisualStyleBackColor = true;
            this.btnRefreshTables.Click += new System.EventHandler(this.btnRefreshTables_Click);
            // 
            // btnAddTable
            // 
            this.btnAddTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddTable.Image = global::DatAdmin.StdIcons.add;
            this.btnAddTable.Location = new System.Drawing.Point(3, 179);
            this.btnAddTable.Name = "btnAddTable";
            this.btnAddTable.Size = new System.Drawing.Size(75, 23);
            this.btnAddTable.TabIndex = 2;
            this.btnAddTable.Text = "s_add";
            this.btnAddTable.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddTable.UseVisualStyleBackColor = true;
            this.btnAddTable.Click += new System.EventHandler(this.btnAddTable_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabColumns);
            this.tabControl1.Controls.Add(this.tabFilter);
            this.tabControl1.Controls.Add(this.tabSorting);
            this.tabControl1.Controls.Add(this.tabSql);
            this.tabControl1.Controls.Add(this.tabMessages);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(559, 212);
            this.tabControl1.TabIndex = 0;
            // 
            // tabColumns
            // 
            this.tabColumns.Controls.Add(this.dataGridColumns);
            this.tabColumns.Controls.Add(this.toolStrip2);
            this.tabColumns.ImageIndex = 1;
            this.tabColumns.Location = new System.Drawing.Point(4, 23);
            this.tabColumns.Name = "tabColumns";
            this.tabColumns.Padding = new System.Windows.Forms.Padding(3);
            this.tabColumns.Size = new System.Drawing.Size(551, 185);
            this.tabColumns.TabIndex = 0;
            this.tabColumns.Text = "s_columns";
            this.tabColumns.UseVisualStyleBackColor = true;
            // 
            // dataGridColumns
            // 
            this.dataGridColumns.AllowUserToAddRows = false;
            this.dataGridColumns.AllowUserToOrderColumns = true;
            this.dataGridColumns.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridColumns.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colColName,
            this.colAlias,
            this.colTable,
            this.colOutput,
            this.colGrouping});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridColumns.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridColumns.Location = new System.Drawing.Point(3, 3);
            this.dataGridColumns.Name = "dataGridColumns";
            this.dataGridColumns.Size = new System.Drawing.Size(521, 179);
            this.dataGridColumns.TabIndex = 0;
            this.dataGridColumns.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridColumns_CellValueChanged);
            this.dataGridColumns.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridColumns_MouseDown);
            this.dataGridColumns.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridColumns_UserDeletedRow);
            // 
            // colColName
            // 
            this.colColName.HeaderText = "s_column";
            this.colColName.Name = "colColName";
            this.colColName.ReadOnly = true;
            // 
            // colAlias
            // 
            this.colAlias.HeaderText = "s_alias";
            this.colAlias.Name = "colAlias";
            // 
            // colTable
            // 
            this.colTable.HeaderText = "s_table";
            this.colTable.Name = "colTable";
            this.colTable.ReadOnly = true;
            // 
            // colOutput
            // 
            this.colOutput.HeaderText = "s_output";
            this.colOutput.Name = "colOutput";
            // 
            // colGrouping
            // 
            this.colGrouping.HeaderText = "s_grouping";
            this.colGrouping.Items.AddRange(new object[] {
            "-",
            "GROUP BY",
            "MIN",
            "MAX",
            "AVG",
            "COUNT",
            "SUM"});
            this.colGrouping.Name = "colGrouping";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMoveUp,
            this.btnMoveDown,
            this.btnRemoveColumn});
            this.toolStrip2.Location = new System.Drawing.Point(524, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(24, 179);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveUp.Image = global::DatAdmin.StdIcons.up1;
            this.btnMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(21, 20);
            this.btnMoveUp.Text = "s_move_up";
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveDown.Image = global::DatAdmin.StdIcons.down1;
            this.btnMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(21, 20);
            this.btnMoveDown.Text = "s_move_down";
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnRemoveColumn
            // 
            this.btnRemoveColumn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRemoveColumn.Image = global::DatAdmin.StdIcons.remove;
            this.btnRemoveColumn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoveColumn.Name = "btnRemoveColumn";
            this.btnRemoveColumn.Size = new System.Drawing.Size(21, 20);
            this.btnRemoveColumn.Text = "s_remove";
            this.btnRemoveColumn.Click += new System.EventHandler(this.btnRemoveColumn_Click);
            // 
            // tabFilter
            // 
            this.tabFilter.AutoScroll = true;
            this.tabFilter.ImageIndex = 5;
            this.tabFilter.Location = new System.Drawing.Point(4, 23);
            this.tabFilter.Name = "tabFilter";
            this.tabFilter.Padding = new System.Windows.Forms.Padding(3);
            this.tabFilter.Size = new System.Drawing.Size(551, 185);
            this.tabFilter.TabIndex = 4;
            this.tabFilter.Text = "s_filter";
            this.tabFilter.UseVisualStyleBackColor = true;
            // 
            // tabSorting
            // 
            this.tabSorting.Controls.Add(this.tableLayoutPanel1);
            this.tabSorting.ImageIndex = 2;
            this.tabSorting.Location = new System.Drawing.Point(4, 23);
            this.tabSorting.Name = "tabSorting";
            this.tabSorting.Padding = new System.Windows.Forms.Padding(3);
            this.tabSorting.Size = new System.Drawing.Size(551, 185);
            this.tabSorting.TabIndex = 2;
            this.tabSorting.Text = "s_sorting";
            this.tabSorting.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(545, 179);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnRemoveFromSort);
            this.panel2.Controls.Add(this.btnAddToSort);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(225, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(44, 173);
            this.panel2.TabIndex = 1;
            // 
            // btnRemoveFromSort
            // 
            this.btnRemoveFromSort.Image = global::DatAdmin.StdIcons.left1;
            this.btnRemoveFromSort.Location = new System.Drawing.Point(4, 33);
            this.btnRemoveFromSort.Name = "btnRemoveFromSort";
            this.btnRemoveFromSort.Size = new System.Drawing.Size(36, 23);
            this.btnRemoveFromSort.TabIndex = 1;
            this.btnRemoveFromSort.UseVisualStyleBackColor = true;
            this.btnRemoveFromSort.Click += new System.EventHandler(this.btnRemoveFromSort_Click);
            // 
            // btnAddToSort
            // 
            this.btnAddToSort.Image = global::DatAdmin.StdIcons.right1;
            this.btnAddToSort.Location = new System.Drawing.Point(3, 3);
            this.btnAddToSort.Name = "btnAddToSort";
            this.btnAddToSort.Size = new System.Drawing.Size(37, 23);
            this.btnAddToSort.TabIndex = 0;
            this.btnAddToSort.UseVisualStyleBackColor = true;
            this.btnAddToSort.Click += new System.EventHandler(this.btnAddToSort_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lbxSortAvailable);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(216, 173);
            this.panel3.TabIndex = 2;
            // 
            // lbxSortAvailable
            // 
            this.lbxSortAvailable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxSortAvailable.FormattingEnabled = true;
            this.lbxSortAvailable.Location = new System.Drawing.Point(0, 13);
            this.lbxSortAvailable.Name = "lbxSortAvailable";
            this.lbxSortAvailable.Size = new System.Drawing.Size(216, 160);
            this.lbxSortAvailable.TabIndex = 1;
            this.lbxSortAvailable.DoubleClick += new System.EventHandler(this.btnAddToSort_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "s_available_columns";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lbxSortedColumns);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(275, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(216, 173);
            this.panel4.TabIndex = 3;
            // 
            // lbxSortedColumns
            // 
            this.lbxSortedColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxSortedColumns.FormattingEnabled = true;
            this.lbxSortedColumns.Location = new System.Drawing.Point(0, 13);
            this.lbxSortedColumns.Name = "lbxSortedColumns";
            this.lbxSortedColumns.Size = new System.Drawing.Size(216, 160);
            this.lbxSortedColumns.TabIndex = 1;
            this.lbxSortedColumns.DoubleClick += new System.EventHandler(this.btnRemoveFromSort_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "s_sorted_columns";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnSortOrder);
            this.panel5.Controls.Add(this.btnSortDown);
            this.panel5.Controls.Add(this.btnSortUp);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(497, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(45, 173);
            this.panel5.TabIndex = 4;
            // 
            // btnSortOrder
            // 
            this.btnSortOrder.Image = global::DatAdmin.StdIcons.order_asc;
            this.btnSortOrder.Location = new System.Drawing.Point(4, 63);
            this.btnSortOrder.Name = "btnSortOrder";
            this.btnSortOrder.Size = new System.Drawing.Size(38, 23);
            this.btnSortOrder.TabIndex = 2;
            this.btnSortOrder.UseVisualStyleBackColor = true;
            this.btnSortOrder.Click += new System.EventHandler(this.btnSortOrder_Click);
            // 
            // btnSortDown
            // 
            this.btnSortDown.Image = global::DatAdmin.StdIcons.down1;
            this.btnSortDown.Location = new System.Drawing.Point(4, 33);
            this.btnSortDown.Name = "btnSortDown";
            this.btnSortDown.Size = new System.Drawing.Size(38, 23);
            this.btnSortDown.TabIndex = 1;
            this.btnSortDown.UseVisualStyleBackColor = true;
            this.btnSortDown.Click += new System.EventHandler(this.btnSortDown_Click);
            // 
            // btnSortUp
            // 
            this.btnSortUp.Image = global::DatAdmin.StdIcons.up1;
            this.btnSortUp.Location = new System.Drawing.Point(4, 3);
            this.btnSortUp.Name = "btnSortUp";
            this.btnSortUp.Size = new System.Drawing.Size(38, 23);
            this.btnSortUp.TabIndex = 0;
            this.btnSortUp.UseVisualStyleBackColor = true;
            this.btnSortUp.Click += new System.EventHandler(this.btnSortUp_Click);
            // 
            // tabSql
            // 
            this.tabSql.Controls.Add(this.codeEditor1);
            this.tabSql.ImageIndex = 3;
            this.tabSql.Location = new System.Drawing.Point(4, 23);
            this.tabSql.Name = "tabSql";
            this.tabSql.Padding = new System.Windows.Forms.Padding(3);
            this.tabSql.Size = new System.Drawing.Size(551, 185);
            this.tabSql.TabIndex = 1;
            this.tabSql.Text = "SQL";
            this.tabSql.UseVisualStyleBackColor = true;
            // 
            // codeEditor1
            // 
            this.codeEditor1.Connection = null;
            this.codeEditor1.Dialect = genericDialect2;
            this.codeEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeEditor1.IsReadOnly = true;
            this.codeEditor1.Language = DatAdmin.CodeLanguage.Sql;
            this.codeEditor1.Location = new System.Drawing.Point(3, 3);
            this.codeEditor1.Modified = true;
            this.codeEditor1.Name = "codeEditor1";
            this.codeEditor1.ReadOnly = true;
            this.codeEditor1.Size = new System.Drawing.Size(545, 179);
            this.codeEditor1.TabIndex = 0;
            // 
            // tabMessages
            // 
            this.tabMessages.Controls.Add(this.messageLogFrame1);
            this.tabMessages.ImageIndex = 4;
            this.tabMessages.Location = new System.Drawing.Point(4, 23);
            this.tabMessages.Name = "tabMessages";
            this.tabMessages.Padding = new System.Windows.Forms.Padding(3);
            this.tabMessages.Size = new System.Drawing.Size(551, 185);
            this.tabMessages.TabIndex = 3;
            this.tabMessages.Text = "s_messages";
            this.tabMessages.UseVisualStyleBackColor = true;
            // 
            // messageLogFrame1
            // 
            this.messageLogFrame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageLogFrame1.Location = new System.Drawing.Point(3, 3);
            this.messageLogFrame1.Name = "messageLogFrame1";
            this.messageLogFrame1.Size = new System.Drawing.Size(545, 179);
            this.messageLogFrame1.Source = null;
            this.messageLogFrame1.Style = DatAdmin.MessageFrameStyle.List;
            this.messageLogFrame1.TabIndex = 0;
            // 
            // mnuColumns
            // 
            this.mnuColumns.Name = "mnuColumns";
            this.mnuColumns.Size = new System.Drawing.Size(61, 4);
            // 
            // QueryDesignFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "QueryDesignFrame";
            this.Size = new System.Drawing.Size(559, 452);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelTables.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabColumns.ResumeLayout(false);
            this.tabColumns.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridColumns)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tabSorting.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.tabSql.ResumeLayout(false);
            this.tabMessages.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DatAdmin.SplitContainerEx splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabColumns;
        private System.Windows.Forms.TabPage tabSql;
        private System.Windows.Forms.DataGridView dataGridColumns;
        private System.Windows.Forms.Panel panel1;
        private CodeEditor codeEditor1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnShowSql;
        private System.Windows.Forms.ToolStripButton btnClearQuery;
        private System.Windows.Forms.TabPage tabSorting;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnRemoveFromSort;
        private System.Windows.Forms.Button btnAddToSort;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListBox lbxSortAvailable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnSortOrder;
        private System.Windows.Forms.Button btnSortDown;
        private System.Windows.Forms.Button btnSortUp;
        private System.Windows.Forms.ListBox lbxSortedColumns;
        private System.Windows.Forms.Label labDragAndDrop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnUniqueRows;
        private DatAdmin.ContextMenuStripEx mnuColumns;
        private System.Windows.Forms.TabPage tabMessages;
        private MessageLogFrame messageLogFrame1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panelTables;
        private System.Windows.Forms.Button btnAddTable;
        private System.Windows.Forms.ListBox lbxAvailableTables;
        private System.Windows.Forms.Button btnRefreshTables;
        private System.Windows.Forms.ToolStripButton btnShowTables;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnMoveUp;
        private System.Windows.Forms.ToolStripButton btnMoveDown;
        private System.Windows.Forms.TabPage tabFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn colColName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAlias;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTable;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colOutput;
        private System.Windows.Forms.DataGridViewComboBoxColumn colGrouping;
        private System.Windows.Forms.ToolStripButton btnRemoveColumn;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox lbxAvailableViews;
    }
}
