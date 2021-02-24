namespace Plugin.querytool
{
    partial class QueryHistoryDocker
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
            this.splitContainerEx1 = new DatAdmin.SplitContainerEx();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.codeEditor1 = new DatAdmin.CodeEditor();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnClearHistory = new System.Windows.Forms.ToolStripButton();
            this.btnOpenInEditor = new System.Windows.Forms.ToolStripButton();
            this.btnSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.splitContainerEx1.Panel1.SuspendLayout();
            this.splitContainerEx1.Panel2.SuspendLayout();
            this.splitContainerEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefresh,
            this.btnClearHistory,
            this.btnOpenInEditor,
            this.btnSettings});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(481, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // splitContainerEx1
            // 
            this.splitContainerEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerEx1.Location = new System.Drawing.Point(0, 25);
            this.splitContainerEx1.MoreFixedPanel = DatAdmin.SplitContainerEx.PanelType.First;
            this.splitContainerEx1.Name = "splitContainerEx1";
            this.splitContainerEx1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerEx1.Panel1
            // 
            this.splitContainerEx1.Panel1.Controls.Add(this.dataGridView1);
            this.splitContainerEx1.Panel1DynamicCollapsed = false;
            this.splitContainerEx1.Panel1MinDynamicSize = 0;
            // 
            // splitContainerEx1.Panel2
            // 
            this.splitContainerEx1.Panel2.Controls.Add(this.codeEditor1);
            this.splitContainerEx1.Panel2DynamicCollapsed = false;
            this.splitContainerEx1.Panel2MinDynamicSize = 0;
            this.splitContainerEx1.Size = new System.Drawing.Size(481, 299);
            this.splitContainerEx1.SplitterDistance = 160;
            this.splitContainerEx1.SplitterWidth = 6;
            this.splitContainerEx1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(481, 160);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.btnOpenInEditor_Click);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridViewEx1_SelectionChanged);
            // 
            // codeEditor1
            // 
            this.codeEditor1.Dialect = null;
            this.codeEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeEditor1.IsReadOnly = true;
            this.codeEditor1.Language = DatAdmin.CodeLanguage.Sql;
            this.codeEditor1.Location = new System.Drawing.Point(0, 0);
            this.codeEditor1.Modified = true;
            this.codeEditor1.Name = "codeEditor1";
            this.codeEditor1.ReadOnly = true;
            this.codeEditor1.Size = new System.Drawing.Size(481, 133);
            this.codeEditor1.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::Plugin.querytool.StdIcons.refresh;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(73, 22);
            this.btnRefresh.Text = "s_refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClearHistory
            // 
            this.btnClearHistory.Image = global::Plugin.querytool.StdIcons.recycle_bin;
            this.btnClearHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClearHistory.Name = "btnClearHistory";
            this.btnClearHistory.Size = new System.Drawing.Size(62, 22);
            this.btnClearHistory.Text = "s_clear";
            this.btnClearHistory.Click += new System.EventHandler(this.btnClearHistory_Click);
            // 
            // btnOpenInEditor
            // 
            this.btnOpenInEditor.Image = global::Plugin.querytool.StdIcons.sql;
            this.btnOpenInEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenInEditor.Name = "btnOpenInEditor";
            this.btnOpenInEditor.Size = new System.Drawing.Size(115, 22);
            this.btnOpenInEditor.Text = "s_open_in_editor";
            this.btnOpenInEditor.Click += new System.EventHandler(this.btnOpenInEditor_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Image = global::Plugin.querytool.StdIcons.settings;
            this.btnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(78, 22);
            this.btnSettings.Text = "s_settings";
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // QueryHistoryDocker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerEx1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "QueryHistoryDocker";
            this.Size = new System.Drawing.Size(481, 324);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainerEx1.Panel1.ResumeLayout(false);
            this.splitContainerEx1.Panel2.ResumeLayout(false);
            this.splitContainerEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private DatAdmin.SplitContainerEx splitContainerEx1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DatAdmin.CodeEditor codeEditor1;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.ToolStripButton btnClearHistory;
        private System.Windows.Forms.ToolStripButton btnOpenInEditor;
        private System.Windows.Forms.ToolStripButton btnSettings;
    }
}
