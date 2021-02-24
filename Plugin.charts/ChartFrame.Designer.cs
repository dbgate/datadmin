namespace Plugin.charts
{
    partial class ChartFrame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartFrame));
            this.splitContainer1 = new DatAdmin.SplitContainerEx();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbxStyle = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chbAutoRefresh = new System.Windows.Forms.CheckBox();
            this.panConfigurator = new System.Windows.Forms.Panel();
            this.cbxDataSourceType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableDataFrame1 = new DatAdmin.TableDataFrame();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnSaveImage = new System.Windows.Forms.ToolStripButton();
            this.btnShowFolder = new System.Windows.Forms.ToolStripButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.MoreFixedPanel = DatAdmin.SplitContainerEx.PanelType.First;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1DynamicCollapsed = false;
            this.splitContainer1.Panel1MinDynamicSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2DynamicCollapsed = false;
            this.splitContainer1.Panel2MinDynamicSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(636, 455);
            this.splitContainer1.SplitterDistance = 211;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lbxStyle);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 157);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "s_style";
            // 
            // lbxStyle
            // 
            this.lbxStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxStyle.HideSelection = false;
            this.lbxStyle.LargeImageList = this.imageList1;
            this.lbxStyle.Location = new System.Drawing.Point(10, 20);
            this.lbxStyle.MultiSelect = false;
            this.lbxStyle.Name = "lbxStyle";
            this.lbxStyle.Size = new System.Drawing.Size(184, 131);
            this.lbxStyle.TabIndex = 0;
            this.lbxStyle.UseCompatibleStateImageBehavior = false;
            this.lbxStyle.SelectedIndexChanged += new System.EventHandler(this.lbxStyle_SelectedIndexChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "area");
            this.imageList1.Images.SetKeyName(1, "bar");
            this.imageList1.Images.SetKeyName(2, "line");
            this.imageList1.Images.SetKeyName(3, "pie");
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chbAutoRefresh);
            this.groupBox1.Controls.Add(this.panConfigurator);
            this.groupBox1.Controls.Add(this.cbxDataSourceType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 166);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 286);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "s_data";
            // 
            // chbAutoRefresh
            // 
            this.chbAutoRefresh.AutoSize = true;
            this.chbAutoRefresh.Checked = true;
            this.chbAutoRefresh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbAutoRefresh.Location = new System.Drawing.Point(10, 45);
            this.chbAutoRefresh.Name = "chbAutoRefresh";
            this.chbAutoRefresh.Size = new System.Drawing.Size(96, 17);
            this.chbAutoRefresh.TabIndex = 3;
            this.chbAutoRefresh.Text = "s_auto_refresh";
            this.chbAutoRefresh.UseVisualStyleBackColor = true;
            this.chbAutoRefresh.CheckedChanged += new System.EventHandler(this.chbAutoRefresh_CheckedChanged);
            // 
            // panConfigurator
            // 
            this.panConfigurator.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panConfigurator.Location = new System.Drawing.Point(6, 68);
            this.panConfigurator.Name = "panConfigurator";
            this.panConfigurator.Size = new System.Drawing.Size(188, 212);
            this.panConfigurator.TabIndex = 2;
            // 
            // cbxDataSourceType
            // 
            this.cbxDataSourceType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxDataSourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDataSourceType.FormattingEnabled = true;
            this.cbxDataSourceType.Location = new System.Drawing.Point(51, 17);
            this.cbxDataSourceType.Name = "cbxDataSourceType";
            this.cbxDataSourceType.Size = new System.Drawing.Size(143, 21);
            this.cbxDataSourceType.TabIndex = 1;
            this.cbxDataSourceType.SelectedIndexChanged += new System.EventHandler(this.cbxDataSourceType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "s_type";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList2;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(419, 455);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.zedGraphControl1);
            this.tabPage1.ImageIndex = 0;
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(411, 428);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "s_chart";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControl1.Location = new System.Drawing.Point(3, 3);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0;
            this.zedGraphControl1.ScrollMaxX = 0;
            this.zedGraphControl1.ScrollMaxY = 0;
            this.zedGraphControl1.ScrollMaxY2 = 0;
            this.zedGraphControl1.ScrollMinX = 0;
            this.zedGraphControl1.ScrollMinY = 0;
            this.zedGraphControl1.ScrollMinY2 = 0;
            this.zedGraphControl1.Size = new System.Drawing.Size(405, 422);
            this.zedGraphControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableDataFrame1);
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(411, 428);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "s_data";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableDataFrame1
            // 
            this.tableDataFrame1.Detached = false;
            this.tableDataFrame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableDataFrame1.ForceReadOnly = false;
            this.tableDataFrame1.HideToolbars = false;
            this.tableDataFrame1.IsContentVisible = false;
            this.tableDataFrame1.IsLoadingIcon = false;
            this.tableDataFrame1.Location = new System.Drawing.Point(3, 3);
            this.tableDataFrame1.MasterFrame = null;
            this.tableDataFrame1.Name = "tableDataFrame1";
            this.tableDataFrame1.Size = new System.Drawing.Size(405, 422);
            this.tableDataFrame1.TabIndex = 0;
            this.tableDataFrame1.TabularData = null;
            this.tableDataFrame1.WinId = null;
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "chart.png");
            this.imageList2.Images.SetKeyName(1, "table_data.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefresh,
            this.btnSave,
            this.btnSaveImage,
            this.btnShowFolder});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(636, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::Plugin.charts.StdIcons.refresh;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(73, 22);
            this.btnRefresh.Text = "s_refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::Plugin.charts.StdIcons.save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 22);
            this.btnSave.Text = "s_save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSaveImage
            // 
            this.btnSaveImage.Image = global::Plugin.charts.StdIcons.picture;
            this.btnSaveImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.Size = new System.Drawing.Size(124, 22);
            this.btnSaveImage.Text = "s_export_as_image";
            this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
            // 
            // btnShowFolder
            // 
            this.btnShowFolder.Image = global::Plugin.charts.StdIcons.folder;
            this.btnShowFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowFolder.Name = "btnShowFolder";
            this.btnShowFolder.Size = new System.Drawing.Size(68, 22);
            this.btnShowFolder.Text = "s_folder";
            this.btnShowFolder.Click += new System.EventHandler(this.btnShowFolder_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "{s_chart} (*.cha)|*.cha";
            // 
            // ChartFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ChartFrame";
            this.Size = new System.Drawing.Size(636, 480);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DatAdmin.SplitContainerEx splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.ComboBox cbxDataSourceType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel panConfigurator;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.ToolStripButton btnSaveImage;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.CheckBox chbAutoRefresh;
        private System.Windows.Forms.ListView lbxStyle;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton btnShowFolder;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ImageList imageList2;
        private DatAdmin.TableDataFrame tableDataFrame1;
    }
}
