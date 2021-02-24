namespace DatAdmin
{
    partial class QueryFrame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryFrame));
            DatAdmin.GenericDialect genericDialect1 = new DatAdmin.GenericDialect();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnTransaction = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tbxServer = new System.Windows.Forms.ToolStripTextBox();
            this.btnChangeServer = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cbxDatabase = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExecute = new System.Windows.Forms.ToolStripButton();
            this.btnAddToFavorite = new System.Windows.Forms.ToolStripButton();
            this.btnCancel = new System.Windows.Forms.ToolStripButton();
            this.btnTableStyle = new System.Windows.Forms.ToolStripButton();
            this.btnInsertVariable = new System.Windows.Forms.ToolStripButton();
            this.btnGetSchemaTable = new System.Windows.Forms.ToolStripButton();
            this.btnOnlineHelp = new System.Windows.Forms.ToolStripDropDownButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.cbQueryCode = new System.Windows.Forms.ToolStripButton();
            this.cbQueryBuilder = new System.Windows.Forms.ToolStripButton();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.labStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.labServer = new System.Windows.Forms.ToolStripStatusLabel();
            this.labDatabase = new System.Windows.Forms.ToolStripStatusLabel();
            this.labUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.labTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.resultTabs = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.messageLogFrame1 = new DatAdmin.MessageLogFrame();
            this.tbquery = new DatAdmin.CodeEditor();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.splitContainer1 = new DatAdmin.SplitContainerEx();
            this.timerQueryDuration = new System.Windows.Forms.Timer(this.components);
            this.toolStripTransaction = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCommit = new System.Windows.Forms.ToolStripButton();
            this.btnRollback = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAutoCommit = new System.Windows.Forms.ToolStripButton();
            this.btnContinueOnErrors = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.resultTabs.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStripTransaction.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "table_data.png");
            this.imageList1.Images.SetKeyName(1, "log.gif");
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnTransaction,
            this.toolStripLabel2,
            this.tbxServer,
            this.btnChangeServer,
            this.toolStripLabel1,
            this.cbxDatabase,
            this.toolStripSeparator1,
            this.btnExecute,
            this.btnSave,
            this.btnAddToFavorite,
            this.btnCancel,
            this.btnTableStyle,
            this.btnInsertVariable,
            this.btnGetSchemaTable,
            this.btnOnlineHelp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(820, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnTransaction
            // 
            this.btnTransaction.CheckOnClick = true;
            this.btnTransaction.Image = global::DatAdmin.StdIcons.transaction;
            this.btnTransaction.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTransaction.Name = "btnTransaction";
            this.btnTransaction.Size = new System.Drawing.Size(96, 22);
            this.btnTransaction.Text = "s_transaction";
            this.btnTransaction.Click += new System.EventHandler(this.btnTransaction_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Image = global::DatAdmin.StdIcons.dbserver;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(64, 22);
            this.toolStripLabel2.Text = "s_server";
            // 
            // tbxServer
            // 
            this.tbxServer.Name = "tbxServer";
            this.tbxServer.ReadOnly = true;
            this.tbxServer.Size = new System.Drawing.Size(120, 25);
            // 
            // btnChangeServer
            // 
            this.btnChangeServer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnChangeServer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnChangeServer.Name = "btnChangeServer";
            this.btnChangeServer.Size = new System.Drawing.Size(23, 22);
            this.btnChangeServer.Text = "...";
            this.btnChangeServer.Click += new System.EventHandler(this.btnChangeServer_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Image = global::DatAdmin.StdIcons.database;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(80, 22);
            this.toolStripLabel1.Text = "s_database";
            // 
            // cbxDatabase
            // 
            this.cbxDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDatabase.Enabled = false;
            this.cbxDatabase.Name = "cbxDatabase";
            this.cbxDatabase.Size = new System.Drawing.Size(160, 25);
            this.cbxDatabase.SelectedIndexChanged += new System.EventHandler(this.lbdatabase_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExecute
            // 
            this.btnExecute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExecute.Image = global::DatAdmin.StdIcons.query_execute;
            this.btnExecute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(23, 22);
            this.btnExecute.Text = "s_execute";
            this.btnExecute.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // btnAddToFavorite
            // 
            this.btnAddToFavorite.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddToFavorite.Image = global::DatAdmin.StdIcons.favorite_add;
            this.btnAddToFavorite.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddToFavorite.Name = "btnAddToFavorite";
            this.btnAddToFavorite.Size = new System.Drawing.Size(23, 22);
            this.btnAddToFavorite.Text = "s_add_to_favorites";
            this.btnAddToFavorite.Click += new System.EventHandler(this.btnAddToFavorite_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancel.Image = global::DatAdmin.StdIcons.stop;
            this.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(23, 22);
            this.btnCancel.Text = "s_stop_loading";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnTableStyle
            // 
            this.btnTableStyle.CheckOnClick = true;
            this.btnTableStyle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTableStyle.Image = global::DatAdmin.StdIcons.table_data;
            this.btnTableStyle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTableStyle.Name = "btnTableStyle";
            this.btnTableStyle.Size = new System.Drawing.Size(23, 22);
            this.btnTableStyle.Text = "Browsable result";
            this.btnTableStyle.ToolTipText = "Browsable result";
            // 
            // btnInsertVariable
            // 
            this.btnInsertVariable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnInsertVariable.Image = global::DatAdmin.StdIcons.variable;
            this.btnInsertVariable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInsertVariable.Name = "btnInsertVariable";
            this.btnInsertVariable.Size = new System.Drawing.Size(23, 22);
            this.btnInsertVariable.Text = "s_insert_variable";
            this.btnInsertVariable.Click += new System.EventHandler(this.btnInsertVariable_Click);
            // 
            // btnGetSchemaTable
            // 
            this.btnGetSchemaTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGetSchemaTable.Image = global::DatAdmin.StdIcons.debug;
            this.btnGetSchemaTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGetSchemaTable.Name = "btnGetSchemaTable";
            this.btnGetSchemaTable.Size = new System.Drawing.Size(23, 22);
            this.btnGetSchemaTable.Text = "toolStripButton1";
            this.btnGetSchemaTable.Click += new System.EventHandler(this.btnGetSchemaTable_Click);
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
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "{s_sql_file} (*.sql)|*.sql";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbQueryCode,
            this.cbQueryBuilder});
            this.toolStrip2.Location = new System.Drawing.Point(820, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(24, 452);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            this.toolStrip2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
            // 
            // cbQueryCode
            // 
            this.cbQueryCode.Checked = true;
            this.cbQueryCode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbQueryCode.Image = global::DatAdmin.StdIcons.sql;
            this.cbQueryCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cbQueryCode.Name = "cbQueryCode";
            this.cbQueryCode.Size = new System.Drawing.Size(21, 98);
            this.cbQueryCode.Text = "s_query_code";
            this.cbQueryCode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cbQueryCode.Click += new System.EventHandler(this.cbQueryCode_Click);
            // 
            // cbQueryBuilder
            // 
            this.cbQueryBuilder.Image = global::DatAdmin.StdIcons.querydesign;
            this.cbQueryBuilder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cbQueryBuilder.Name = "cbQueryBuilder";
            this.cbQueryBuilder.Size = new System.Drawing.Size(21, 109);
            this.cbQueryBuilder.Text = "s_query_builder";
            this.cbQueryBuilder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cbQueryBuilder.Click += new System.EventHandler(this.cbQueryBuilder_Click);
            // 
            // statusStrip2
            // 
            this.statusStrip2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labStatus,
            this.labServer,
            this.labDatabase,
            this.labUser,
            this.labTime});
            this.statusStrip2.Location = new System.Drawing.Point(0, 452);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(844, 22);
            this.statusStrip2.TabIndex = 1;
            this.statusStrip2.Text = "statusStrip2";
            this.statusStrip2.Resize += new System.EventHandler(this.statusStrip2_Resize);
            // 
            // labStatus
            // 
            this.labStatus.AutoSize = false;
            this.labStatus.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(150, 17);
            this.labStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labServer
            // 
            this.labServer.AutoSize = false;
            this.labServer.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.labServer.Name = "labServer";
            this.labServer.Size = new System.Drawing.Size(118, 17);
            this.labServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labDatabase
            // 
            this.labDatabase.AutoSize = false;
            this.labDatabase.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.labDatabase.Name = "labDatabase";
            this.labDatabase.Size = new System.Drawing.Size(118, 17);
            this.labDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labUser
            // 
            this.labUser.AutoSize = false;
            this.labUser.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.labUser.Name = "labUser";
            this.labUser.Size = new System.Drawing.Size(118, 17);
            this.labUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labTime
            // 
            this.labTime.AutoSize = false;
            this.labTime.Name = "labTime";
            this.labTime.Size = new System.Drawing.Size(118, 17);
            this.labTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // resultTabs
            // 
            this.resultTabs.Controls.Add(this.tabPage2);
            this.resultTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultTabs.ImageList = this.imageList1;
            this.resultTabs.Location = new System.Drawing.Point(0, 0);
            this.resultTabs.Name = "resultTabs";
            this.resultTabs.SelectedIndex = 0;
            this.resultTabs.Size = new System.Drawing.Size(820, 216);
            this.resultTabs.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.messageLogFrame1);
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(812, 189);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "s_messages";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // messageLogFrame1
            // 
            this.messageLogFrame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageLogFrame1.Location = new System.Drawing.Point(3, 3);
            this.messageLogFrame1.Name = "messageLogFrame1";
            this.messageLogFrame1.Size = new System.Drawing.Size(806, 183);
            this.messageLogFrame1.Source = null;
            this.messageLogFrame1.Style = DatAdmin.MessageFrameStyle.List;
            this.messageLogFrame1.TabIndex = 0;
            this.messageLogFrame1.MessageDoubleClick += new DatAdmin.LogMessageEventHandler(this.messageLogFrame1_MessageDoubleClick);
            // 
            // tbquery
            // 
            this.tbquery.Connection = null;
            this.tbquery.ContextMenuStrip = this.contextMenuStrip1;
            this.tbquery.Dialect = genericDialect1;
            this.tbquery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbquery.IsReadOnly = false;
            this.tbquery.Language = DatAdmin.CodeLanguage.Sql;
            this.tbquery.Location = new System.Drawing.Point(0, 0);
            this.tbquery.Margin = new System.Windows.Forms.Padding(4);
            this.tbquery.Modified = true;
            this.tbquery.Name = "tbquery";
            this.tbquery.ReadOnly = false;
            this.tbquery.Size = new System.Drawing.Size(820, 205);
            this.tbquery.TabIndex = 0;
            this.tbquery.DatabaseConnectionNeeded += new System.EventHandler(this.tbquery_DatabaseConnectionNeeded);
            this.tbquery.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbquery_KeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
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
            this.splitContainer1.Panel1.Controls.Add(this.tbquery);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            this.splitContainer1.Panel1DynamicCollapsed = false;
            this.splitContainer1.Panel1MinDynamicSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.resultTabs);
            this.splitContainer1.Panel2DynamicCollapsed = false;
            this.splitContainer1.Panel2MinDynamicSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(820, 427);
            this.splitContainer1.SplitterDistance = 205;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // timerQueryDuration
            // 
            this.timerQueryDuration.Tick += new System.EventHandler(this.timerQueryDuration_Tick);
            // 
            // toolStripTransaction
            // 
            this.toolStripTransaction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.toolStripSeparator2,
            this.btnCommit,
            this.btnRollback,
            this.toolStripSeparator3,
            this.btnAutoCommit,
            this.btnContinueOnErrors});
            this.toolStripTransaction.Location = new System.Drawing.Point(0, 25);
            this.toolStripTransaction.Name = "toolStripTransaction";
            this.toolStripTransaction.Size = new System.Drawing.Size(820, 25);
            this.toolStripTransaction.TabIndex = 1;
            this.toolStripTransaction.Text = "toolStrip3";
            this.toolStripTransaction.Visible = false;
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Image = global::DatAdmin.StdIcons.transaction;
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(92, 22);
            this.toolStripLabel3.Text = "s_transaction";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCommit
            // 
            this.btnCommit.Image = global::DatAdmin.StdIcons.ok;
            this.btnCommit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(79, 22);
            this.btnCommit.Text = "s_commit";
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
            // 
            // btnRollback
            // 
            this.btnRollback.Image = global::DatAdmin.StdIcons.cancel;
            this.btnRollback.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRollback.Name = "btnRollback";
            this.btnRollback.Size = new System.Drawing.Size(79, 22);
            this.btnRollback.Text = "s_rollback";
            this.btnRollback.Click += new System.EventHandler(this.btnRollback_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAutoCommit
            // 
            this.btnAutoCommit.CheckOnClick = true;
            this.btnAutoCommit.Image = global::DatAdmin.StdIcons.autodetect;
            this.btnAutoCommit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAutoCommit.Name = "btnAutoCommit";
            this.btnAutoCommit.Size = new System.Drawing.Size(103, 22);
            this.btnAutoCommit.Text = "s_autocommit";
            this.btnAutoCommit.Click += new System.EventHandler(this.btnAutoCommit_Click);
            // 
            // btnContinueOnErrors
            // 
            this.btnContinueOnErrors.Image = global::DatAdmin.StdIcons.query_execute;
            this.btnContinueOnErrors.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnContinueOnErrors.Name = "btnContinueOnErrors";
            this.btnContinueOnErrors.Size = new System.Drawing.Size(138, 22);
            this.btnContinueOnErrors.Text = "s_continue_on_errors";
            this.btnContinueOnErrors.Click += new System.EventHandler(this.btnContinueOnErrors_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = global::DatAdmin.StdIcons.save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Text = "s_save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // QueryFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStripTransaction);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.statusStrip2);
            this.Name = "QueryFrame";
            this.Size = new System.Drawing.Size(844, 474);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.resultTabs.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.toolStripTransaction.ResumeLayout(false);
            this.toolStripTransaction.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cbxDatabase;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnExecute;
        private System.Windows.Forms.ToolStripButton btnTransaction;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton cbQueryCode;
        private System.Windows.Forms.ToolStripButton cbQueryBuilder;
        private System.Windows.Forms.ToolStripButton btnCancel;
        private System.Windows.Forms.ToolStripButton btnTableStyle;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox tbxServer;
        private System.Windows.Forms.ToolStripButton btnChangeServer;
        private System.Windows.Forms.ToolStripButton btnInsertVariable;
        private System.Windows.Forms.ToolStripButton btnGetSchemaTable;
        private System.Windows.Forms.ToolStripButton btnAddToFavorite;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel labStatus;
        private System.Windows.Forms.ToolStripStatusLabel labServer;
        private System.Windows.Forms.ToolStripStatusLabel labDatabase;
        private System.Windows.Forms.ToolStripStatusLabel labUser;
        private System.Windows.Forms.ToolStripStatusLabel labTime;
        private System.Windows.Forms.TabControl resultTabs;
        private System.Windows.Forms.TabPage tabPage2;
        private MessageLogFrame messageLogFrame1;
        private CodeEditor tbquery;
        private SplitContainerEx splitContainer1;
        private System.Windows.Forms.Timer timerQueryDuration;
        private System.Windows.Forms.ToolStripDropDownButton btnOnlineHelp;
        private System.Windows.Forms.ToolStrip toolStripTransaction;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnAutoCommit;
        private System.Windows.Forms.ToolStripButton btnCommit;
        private System.Windows.Forms.ToolStripButton btnRollback;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnContinueOnErrors;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripButton btnSave;
    }
}
