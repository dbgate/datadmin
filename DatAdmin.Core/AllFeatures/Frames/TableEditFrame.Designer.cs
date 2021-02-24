namespace DatAdmin
{
    partial class TableEditFrame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableEditFrame));
            DatAdmin.GenericDialect genericDialect1 = new DatAdmin.GenericDialect();
            this.splitContainer1 = new DatAdmin.SplitContainerEx();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabColumns = new System.Windows.Forms.TabPage();
            this.columnsGrid = new DatAdmin.DataGridViewEx();
            this.FieldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.IsNullable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsPk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsAutoInc = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.References = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.tabIndexesKeys = new System.Windows.Forms.TabPage();
            this.indexGrid = new DatAdmin.DataGridViewEx();
            this.s_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.s_type = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.s_columns = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbixaddcols = new System.Windows.Forms.ComboBox();
            this.btnAddIndexCol = new System.Windows.Forms.Button();
            this.btnDeleteIndexCol = new System.Windows.Forms.Button();
            this.lbixcols = new System.Windows.Forms.ListBox();
            this.tabRelations = new System.Windows.Forms.TabPage();
            this.fkGrid = new DatAdmin.DataGridViewEx();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnRemoveFkCol = new System.Windows.Forms.Button();
            this.btnAddFkCol = new System.Windows.Forms.Button();
            this.fkcolsPanel = new System.Windows.Forms.Panel();
            this.pktablename = new System.Windows.Forms.ComboBox();
            this.fktablename = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabCheckConstraints = new System.Windows.Forms.TabPage();
            this.checkGrid = new DatAdmin.DataGridViewEx();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabTableProps = new System.Windows.Forms.TabPage();
            this.propertyFrame2 = new DatAdmin.PropertyFrame();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.propertyFrame1 = new DatAdmin.PropertyFrame();
            this.tabSql = new System.Windows.Forms.TabPage();
            this.tbxAlterSql = new DatAdmin.CodeEditor();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.messageLogFrame1 = new DatAdmin.MessageLogFrame();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnRemove = new System.Windows.Forms.ToolStripButton();
            this.btnMoveUp = new System.Windows.Forms.ToolStripButton();
            this.btnMoveDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.rbowned = new System.Windows.Forms.ToolStripButton();
            this.rbreferenced = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAllowRecreate = new System.Windows.Forms.ToolStripButton();
            this.btnOnlineHelp = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabColumns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.columnsGrid)).BeginInit();
            this.tabIndexesKeys.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.indexGrid)).BeginInit();
            this.panel3.SuspendLayout();
            this.tabRelations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fkGrid)).BeginInit();
            this.panel5.SuspendLayout();
            this.tabCheckConstraints.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkGrid)).BeginInit();
            this.tabTableProps.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabSql.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel1DynamicCollapsed = false;
            this.splitContainer1.Panel1MinDynamicSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl2);
            this.splitContainer1.Panel2DynamicCollapsed = false;
            this.splitContainer1.Panel2MinDynamicSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(738, 433);
            this.splitContainer1.SplitterDistance = 205;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabColumns);
            this.tabControl1.Controls.Add(this.tabIndexesKeys);
            this.tabControl1.Controls.Add(this.tabRelations);
            this.tabControl1.Controls.Add(this.tabCheckConstraints);
            this.tabControl1.Controls.Add(this.tabTableProps);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(738, 205);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabColumns
            // 
            this.tabColumns.Controls.Add(this.columnsGrid);
            this.tabColumns.ImageIndex = 2;
            this.tabColumns.Location = new System.Drawing.Point(4, 23);
            this.tabColumns.Name = "tabColumns";
            this.tabColumns.Padding = new System.Windows.Forms.Padding(3);
            this.tabColumns.Size = new System.Drawing.Size(730, 178);
            this.tabColumns.TabIndex = 0;
            this.tabColumns.Text = "s_columns";
            this.tabColumns.UseVisualStyleBackColor = true;
            // 
            // columnsGrid
            // 
            this.columnsGrid.AllowUserToAddRows = false;
            this.columnsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.columnsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FieldName,
            this.DataType,
            this.IsNullable,
            this.IsPk,
            this.colLength,
            this.IsAutoInc,
            this.References});
            this.columnsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.columnsGrid.Location = new System.Drawing.Point(3, 3);
            this.columnsGrid.Name = "columnsGrid";
            this.columnsGrid.Size = new System.Drawing.Size(724, 172);
            this.columnsGrid.TabIndex = 0;
            this.columnsGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.columnsGrid_CellValueChanged);
            this.columnsGrid.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.columnsGrid_UserDeletingRow);
            this.columnsGrid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.columnsGrid_CellBeginEdit);
            this.columnsGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.columnsGrid_RowEnter);
            this.columnsGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.columnsGrid_DataError);
            // 
            // FieldName
            // 
            this.FieldName.HeaderText = "s_name";
            this.FieldName.Name = "FieldName";
            // 
            // DataType
            // 
            this.DataType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.DataType.HeaderText = "s_type";
            this.DataType.Name = "DataType";
            this.DataType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // IsNullable
            // 
            this.IsNullable.HeaderText = "s_nullable";
            this.IsNullable.Name = "IsNullable";
            // 
            // IsPk
            // 
            this.IsPk.HeaderText = "s_primary_key";
            this.IsPk.Name = "IsPk";
            // 
            // colLength
            // 
            this.colLength.HeaderText = "s_length";
            this.colLength.Name = "colLength";
            // 
            // IsAutoInc
            // 
            this.IsAutoInc.HeaderText = "s_autoincrement";
            this.IsAutoInc.Name = "IsAutoInc";
            // 
            // References
            // 
            this.References.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.References.HeaderText = "s_references";
            this.References.Name = "References";
            // 
            // tabIndexesKeys
            // 
            this.tabIndexesKeys.Controls.Add(this.indexGrid);
            this.tabIndexesKeys.Controls.Add(this.panel3);
            this.tabIndexesKeys.ImageIndex = 3;
            this.tabIndexesKeys.Location = new System.Drawing.Point(4, 23);
            this.tabIndexesKeys.Name = "tabIndexesKeys";
            this.tabIndexesKeys.Padding = new System.Windows.Forms.Padding(3);
            this.tabIndexesKeys.Size = new System.Drawing.Size(730, 178);
            this.tabIndexesKeys.TabIndex = 1;
            this.tabIndexesKeys.Text = "s_indexes_keys";
            this.tabIndexesKeys.UseVisualStyleBackColor = true;
            // 
            // indexGrid
            // 
            this.indexGrid.AllowUserToAddRows = false;
            this.indexGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.indexGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.s_name,
            this.s_type,
            this.s_columns});
            this.indexGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.indexGrid.Location = new System.Drawing.Point(3, 3);
            this.indexGrid.Name = "indexGrid";
            this.indexGrid.Size = new System.Drawing.Size(558, 172);
            this.indexGrid.TabIndex = 0;
            this.indexGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.indexGrid_CellValueChanged);
            this.indexGrid.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.indexGrid_UserDeletingRow);
            this.indexGrid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.indexGrid_CellBeginEdit);
            this.indexGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.indexGrid_RowEnter);
            this.indexGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.indexGrid_DataError);
            // 
            // s_name
            // 
            this.s_name.HeaderText = "s_name";
            this.s_name.Name = "s_name";
            // 
            // s_type
            // 
            this.s_type.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.s_type.HeaderText = "s_type";
            this.s_type.Name = "s_type";
            // 
            // s_columns
            // 
            this.s_columns.HeaderText = "s_columns";
            this.s_columns.Name = "s_columns";
            this.s_columns.ReadOnly = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lbixaddcols);
            this.panel3.Controls.Add(this.btnAddIndexCol);
            this.panel3.Controls.Add(this.btnDeleteIndexCol);
            this.panel3.Controls.Add(this.lbixcols);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(561, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(166, 172);
            this.panel3.TabIndex = 3;
            // 
            // lbixaddcols
            // 
            this.lbixaddcols.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbixaddcols.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lbixaddcols.FormattingEnabled = true;
            this.lbixaddcols.Location = new System.Drawing.Point(87, 141);
            this.lbixaddcols.Name = "lbixaddcols";
            this.lbixaddcols.Size = new System.Drawing.Size(76, 21);
            this.lbixaddcols.TabIndex = 3;
            // 
            // btnAddIndexCol
            // 
            this.btnAddIndexCol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddIndexCol.Location = new System.Drawing.Point(6, 139);
            this.btnAddIndexCol.Name = "btnAddIndexCol";
            this.btnAddIndexCol.Size = new System.Drawing.Size(75, 23);
            this.btnAddIndexCol.TabIndex = 2;
            this.btnAddIndexCol.Text = "s_add";
            this.btnAddIndexCol.UseVisualStyleBackColor = true;
            this.btnAddIndexCol.Click += new System.EventHandler(this.button5_Click);
            // 
            // btnDeleteIndexCol
            // 
            this.btnDeleteIndexCol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteIndexCol.Location = new System.Drawing.Point(6, 110);
            this.btnDeleteIndexCol.Name = "btnDeleteIndexCol";
            this.btnDeleteIndexCol.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteIndexCol.TabIndex = 1;
            this.btnDeleteIndexCol.Text = "s_delete";
            this.btnDeleteIndexCol.UseVisualStyleBackColor = true;
            this.btnDeleteIndexCol.Click += new System.EventHandler(this.button4_Click);
            // 
            // lbixcols
            // 
            this.lbixcols.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbixcols.FormattingEnabled = true;
            this.lbixcols.Location = new System.Drawing.Point(6, 6);
            this.lbixcols.Name = "lbixcols";
            this.lbixcols.Size = new System.Drawing.Size(157, 69);
            this.lbixcols.TabIndex = 0;
            // 
            // tabRelations
            // 
            this.tabRelations.Controls.Add(this.fkGrid);
            this.tabRelations.Controls.Add(this.panel5);
            this.tabRelations.ImageIndex = 4;
            this.tabRelations.Location = new System.Drawing.Point(4, 23);
            this.tabRelations.Name = "tabRelations";
            this.tabRelations.Padding = new System.Windows.Forms.Padding(3);
            this.tabRelations.Size = new System.Drawing.Size(730, 178);
            this.tabRelations.TabIndex = 2;
            this.tabRelations.Text = "s_relations";
            this.tabRelations.UseVisualStyleBackColor = true;
            // 
            // fkGrid
            // 
            this.fkGrid.AllowUserToAddRows = false;
            this.fkGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fkGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column4,
            this.Column2,
            this.Column3});
            this.fkGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fkGrid.Location = new System.Drawing.Point(3, 3);
            this.fkGrid.Name = "fkGrid";
            this.fkGrid.Size = new System.Drawing.Size(509, 172);
            this.fkGrid.TabIndex = 1;
            this.fkGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.fkGrid_CellValueChanged);
            this.fkGrid.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.fkGrid_UserDeletingRow);
            this.fkGrid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.fkGrid_CellBeginEdit);
            this.fkGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.fkgrid_RowEnter);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "s_name";
            this.Column1.Name = "Column1";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "s_source_table";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "s_target_table";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "s_columns";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnRemoveFkCol);
            this.panel5.Controls.Add(this.btnAddFkCol);
            this.panel5.Controls.Add(this.fkcolsPanel);
            this.panel5.Controls.Add(this.pktablename);
            this.panel5.Controls.Add(this.fktablename);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(512, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(215, 172);
            this.panel5.TabIndex = 2;
            // 
            // btnRemoveFkCol
            // 
            this.btnRemoveFkCol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveFkCol.Location = new System.Drawing.Point(87, 145);
            this.btnRemoveFkCol.Name = "btnRemoveFkCol";
            this.btnRemoveFkCol.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveFkCol.TabIndex = 6;
            this.btnRemoveFkCol.Text = "s_remove";
            this.btnRemoveFkCol.UseVisualStyleBackColor = true;
            this.btnRemoveFkCol.Click += new System.EventHandler(this.btnRemoveFkCol_Click);
            // 
            // btnAddFkCol
            // 
            this.btnAddFkCol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddFkCol.Location = new System.Drawing.Point(6, 146);
            this.btnAddFkCol.Name = "btnAddFkCol";
            this.btnAddFkCol.Size = new System.Drawing.Size(75, 23);
            this.btnAddFkCol.TabIndex = 5;
            this.btnAddFkCol.Text = "s_add";
            this.btnAddFkCol.UseVisualStyleBackColor = true;
            this.btnAddFkCol.Click += new System.EventHandler(this.btnAddFkCol_Click);
            // 
            // fkcolsPanel
            // 
            this.fkcolsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fkcolsPanel.AutoScroll = true;
            this.fkcolsPanel.Location = new System.Drawing.Point(6, 46);
            this.fkcolsPanel.Name = "fkcolsPanel";
            this.fkcolsPanel.Size = new System.Drawing.Size(205, 93);
            this.fkcolsPanel.TabIndex = 4;
            // 
            // pktablename
            // 
            this.pktablename.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pktablename.FormattingEnabled = true;
            this.pktablename.Location = new System.Drawing.Point(111, 19);
            this.pktablename.Name = "pktablename";
            this.pktablename.Size = new System.Drawing.Size(100, 21);
            this.pktablename.TabIndex = 3;
            this.pktablename.SelectedIndexChanged += new System.EventHandler(this.pktablename_SelectedIndexChanged);
            // 
            // fktablename
            // 
            this.fktablename.Location = new System.Drawing.Point(6, 19);
            this.fktablename.Name = "fktablename";
            this.fktablename.ReadOnly = true;
            this.fktablename.Size = new System.Drawing.Size(99, 20);
            this.fktablename.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "s_foreign_key_table";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(109, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "s_primary_key_table";
            // 
            // tabCheckConstraints
            // 
            this.tabCheckConstraints.Controls.Add(this.checkGrid);
            this.tabCheckConstraints.ImageIndex = 5;
            this.tabCheckConstraints.Location = new System.Drawing.Point(4, 23);
            this.tabCheckConstraints.Name = "tabCheckConstraints";
            this.tabCheckConstraints.Padding = new System.Windows.Forms.Padding(3);
            this.tabCheckConstraints.Size = new System.Drawing.Size(730, 178);
            this.tabCheckConstraints.TabIndex = 3;
            this.tabCheckConstraints.Text = "s_check_constraints";
            this.tabCheckConstraints.UseVisualStyleBackColor = true;
            // 
            // checkGrid
            // 
            this.checkGrid.AllowUserToAddRows = false;
            this.checkGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.checkGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column6});
            this.checkGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkGrid.Location = new System.Drawing.Point(3, 3);
            this.checkGrid.Name = "checkGrid";
            this.checkGrid.Size = new System.Drawing.Size(724, 172);
            this.checkGrid.TabIndex = 0;
            this.checkGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.checkGrid_CellValueChanged);
            this.checkGrid.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.checkGrid_UserDeletingRow);
            this.checkGrid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.checkGrid_CellBeginEdit);
            this.checkGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.checkGrid_RowEnter);
            // 
            // Column5
            // 
            this.Column5.HeaderText = "s_name";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "s_expression";
            this.Column6.Name = "Column6";
            this.Column6.Width = 300;
            // 
            // tabTableProps
            // 
            this.tabTableProps.Controls.Add(this.propertyFrame2);
            this.tabTableProps.ImageIndex = 0;
            this.tabTableProps.Location = new System.Drawing.Point(4, 23);
            this.tabTableProps.Name = "tabTableProps";
            this.tabTableProps.Padding = new System.Windows.Forms.Padding(3);
            this.tabTableProps.Size = new System.Drawing.Size(730, 178);
            this.tabTableProps.TabIndex = 4;
            this.tabTableProps.Text = "s_properties";
            this.tabTableProps.UseVisualStyleBackColor = true;
            // 
            // propertyFrame2
            // 
            this.propertyFrame2.CacheCustomEditors = false;
            this.propertyFrame2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyFrame2.Location = new System.Drawing.Point(3, 3);
            this.propertyFrame2.Name = "propertyFrame2";
            this.propertyFrame2.SelectedObject = null;
            this.propertyFrame2.Size = new System.Drawing.Size(724, 172);
            this.propertyFrame2.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "properties.png");
            this.imageList1.Images.SetKeyName(1, "sql.png");
            this.imageList1.Images.SetKeyName(2, "column.png");
            this.imageList1.Images.SetKeyName(3, "primary_key.png");
            this.imageList1.Images.SetKeyName(4, "foreign_key.png");
            this.imageList1.Images.SetKeyName(5, "check.png");
            this.imageList1.Images.SetKeyName(6, "info.png");
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabSql);
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.ImageList = this.imageList1;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(738, 222);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.propertyFrame1);
            this.tabPage3.ImageIndex = 0;
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(730, 195);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "s_properties";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // propertyFrame1
            // 
            this.propertyFrame1.CacheCustomEditors = false;
            this.propertyFrame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyFrame1.Location = new System.Drawing.Point(3, 3);
            this.propertyFrame1.Name = "propertyFrame1";
            this.propertyFrame1.SelectedObject = null;
            this.propertyFrame1.Size = new System.Drawing.Size(724, 189);
            this.propertyFrame1.TabIndex = 0;
            this.propertyFrame1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyFrame1_PropertyValueChanged);
            // 
            // tabSql
            // 
            this.tabSql.Controls.Add(this.tbxAlterSql);
            this.tabSql.Controls.Add(this.toolStrip2);
            this.tabSql.ImageIndex = 1;
            this.tabSql.Location = new System.Drawing.Point(4, 23);
            this.tabSql.Name = "tabSql";
            this.tabSql.Padding = new System.Windows.Forms.Padding(3);
            this.tabSql.Size = new System.Drawing.Size(730, 195);
            this.tabSql.TabIndex = 1;
            this.tabSql.Text = "SQL";
            this.tabSql.UseVisualStyleBackColor = true;
            // 
            // tbxAlterSql
            // 
            this.tbxAlterSql.Connection = null;
            this.tbxAlterSql.Dialect = genericDialect1;
            this.tbxAlterSql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxAlterSql.IsReadOnly = true;
            this.tbxAlterSql.Language = DatAdmin.CodeLanguage.Sql;
            this.tbxAlterSql.Location = new System.Drawing.Point(3, 28);
            this.tbxAlterSql.Modified = true;
            this.tbxAlterSql.Name = "tbxAlterSql";
            this.tbxAlterSql.ReadOnly = true;
            this.tbxAlterSql.Size = new System.Drawing.Size(724, 164);
            this.tbxAlterSql.TabIndex = 0;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(724, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::DatAdmin.StdIcons.refresh;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "s_refresh";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::DatAdmin.StdIcons.sql;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "s_open_in_sql_window";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click_1);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.messageLogFrame1);
            this.tabPage1.ImageIndex = 6;
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(730, 195);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "s_messages";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // messageLogFrame1
            // 
            this.messageLogFrame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageLogFrame1.Location = new System.Drawing.Point(3, 3);
            this.messageLogFrame1.Name = "messageLogFrame1";
            this.messageLogFrame1.Size = new System.Drawing.Size(724, 189);
            this.messageLogFrame1.Source = null;
            this.messageLogFrame1.Style = DatAdmin.MessageFrameStyle.Simple;
            this.messageLogFrame1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefresh,
            this.btnSave,
            this.btnAdd,
            this.btnRemove,
            this.btnMoveUp,
            this.btnMoveDown,
            this.toolStripSeparator1,
            this.rbowned,
            this.rbreferenced,
            this.toolStripSeparator2,
            this.btnAllowRecreate,
            this.btnOnlineHelp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(738, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::DatAdmin.StdIcons.refresh;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(73, 22);
            this.btnRefresh.Text = "s_refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::DatAdmin.StdIcons.add;
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(57, 22);
            this.btnAdd.Text = "s_add";
            this.btnAdd.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Image = global::DatAdmin.StdIcons.remove;
            this.btnRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(77, 22);
            this.btnRemove.Text = "s_remove";
            this.btnRemove.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveUp.Image = global::DatAdmin.StdIcons.up1;
            this.btnMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(23, 22);
            this.btnMoveUp.Text = "s_move_up";
            this.btnMoveUp.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveDown.Image = global::DatAdmin.StdIcons.down1;
            this.btnMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(23, 22);
            this.btnMoveDown.Text = "s_move_down";
            this.btnMoveDown.Click += new System.EventHandler(this.button2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // rbowned
            // 
            this.rbowned.Checked = true;
            this.rbowned.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rbowned.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.rbowned.Image = ((System.Drawing.Image)(resources.GetObject("rbowned.Image")));
            this.rbowned.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rbowned.Name = "rbowned";
            this.rbowned.Size = new System.Drawing.Size(57, 22);
            this.rbowned.Text = "s_owned";
            this.rbowned.Click += new System.EventHandler(this.rbowned_Click);
            // 
            // rbreferenced
            // 
            this.rbreferenced.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.rbreferenced.Image = ((System.Drawing.Image)(resources.GetObject("rbreferenced.Image")));
            this.rbreferenced.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rbreferenced.Name = "rbreferenced";
            this.rbreferenced.Size = new System.Drawing.Size(77, 22);
            this.rbreferenced.Text = "s_referenced";
            this.rbreferenced.Click += new System.EventHandler(this.rbreferenced_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAllowRecreate
            // 
            this.btnAllowRecreate.Image = global::DatAdmin.StdIcons.warning;
            this.btnAllowRecreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAllowRecreate.Name = "btnAllowRecreate";
            this.btnAllowRecreate.Size = new System.Drawing.Size(112, 22);
            this.btnAllowRecreate.Text = "s_allow_recreate";
            this.btnAllowRecreate.Click += new System.EventHandler(this.btnAllowRecreate_Click);
            // 
            // btnOnlineHelp
            // 
            this.btnOnlineHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOnlineHelp.Image = global::DatAdmin.StdIcons.question;
            this.btnOnlineHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOnlineHelp.Name = "btnOnlineHelp";
            this.btnOnlineHelp.Size = new System.Drawing.Size(29, 22);
            this.btnOnlineHelp.Text = "toolStripDropDownButton1";
            // 
            // btnSave
            // 
            this.btnSave.Image = global::DatAdmin.StdIcons.save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 22);
            this.btnSave.Text = "s_save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // TableEditFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TableEditFrame";
            this.Size = new System.Drawing.Size(738, 458);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabColumns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.columnsGrid)).EndInit();
            this.tabIndexesKeys.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.indexGrid)).EndInit();
            this.panel3.ResumeLayout(false);
            this.tabRelations.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fkGrid)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tabCheckConstraints.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkGrid)).EndInit();
            this.tabTableProps.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabSql.ResumeLayout(false);
            this.tabSql.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DatAdmin.SplitContainerEx splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabColumns;
        private DatAdmin.DataGridViewEx columnsGrid;
        private System.Windows.Forms.TabPage tabIndexesKeys;
        private System.Windows.Forms.TabPage tabRelations;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private PropertyFrame propertyFrame1;
        private DatAdmin.DataGridViewEx indexGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn s_name;
        private System.Windows.Forms.DataGridViewComboBoxColumn s_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn s_columns;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListBox lbixcols;
        private System.Windows.Forms.ComboBox lbixaddcols;
        private System.Windows.Forms.Button btnAddIndexCol;
        private System.Windows.Forms.Button btnDeleteIndexCol;
        private DatAdmin.DataGridViewEx fkGrid;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TabPage tabCheckConstraints;
        private DatAdmin.DataGridViewEx checkGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.ComboBox pktablename;
        private System.Windows.Forms.TextBox fktablename;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRemoveFkCol;
        private System.Windows.Forms.Button btnAddFkCol;
        private System.Windows.Forms.Panel fkcolsPanel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnRemove;
        private System.Windows.Forms.ToolStripButton btnMoveUp;
        private System.Windows.Forms.ToolStripButton btnMoveDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton rbowned;
        private System.Windows.Forms.ToolStripButton rbreferenced;
        private System.Windows.Forms.TabPage tabSql;
        private CodeEditor tbxAlterSql;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.TabPage tabTableProps;
        private System.Windows.Forms.ImageList imageList1;
        private PropertyFrame propertyFrame2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnAllowRecreate;
        private System.Windows.Forms.TabPage tabPage1;
        private MessageLogFrame messageLogFrame1;
        private System.Windows.Forms.DataGridViewTextBoxColumn FieldName;
        private System.Windows.Forms.DataGridViewComboBoxColumn DataType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsNullable;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsPk;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLength;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsAutoInc;
        private System.Windows.Forms.DataGridViewComboBoxColumn References;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.ToolStripDropDownButton btnOnlineHelp;
        private System.Windows.Forms.ToolStripButton btnSave;

    }
}
