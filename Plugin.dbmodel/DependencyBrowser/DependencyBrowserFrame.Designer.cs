namespace Plugin.dbmodel
{
    partial class DependencyBrowserFrame 
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
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnBack = new System.Windows.Forms.ToolStripButton();
            this.btnForward = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.depSelect = new Plugin.dbmodel.DependencyItemsView();
            this.depDependsOnSelected = new Plugin.dbmodel.DependencyItemsView();
            this.depSelectedDependsOn = new Plugin.dbmodel.DependencyItemsView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbxDepTypes = new System.Windows.Forms.ToolStripComboBox();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefresh,
            this.btnBack,
            this.btnForward,
            this.cbxDepTypes});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(547, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::Plugin.dbmodel.ModRes.refresh;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(73, 22);
            this.btnRefresh.Text = "s_refresh";
            this.btnRefresh.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // btnBack
            // 
            this.btnBack.Image = global::Plugin.dbmodel.ModRes.left1;
            this.btnBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(62, 22);
            this.btnBack.Text = "s_back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnForward
            // 
            this.btnForward.Image = global::Plugin.dbmodel.ModRes.right1;
            this.btnForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(78, 22);
            this.btnForward.Text = "s_forward";
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.depSelect, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.depDependsOnSelected, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.depSelectedDependsOn, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(547, 278);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // depSelect
            // 
            this.depSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.depSelect.Items = null;
            this.depSelect.Location = new System.Drawing.Point(3, 3);
            this.depSelect.Name = "depSelect";
            this.depSelect.SelectedItem = null;
            this.depSelect.Size = new System.Drawing.Size(176, 272);
            this.depSelect.TabIndex = 0;
            this.depSelect.Title = "s_selected_object";
            this.depSelect.ToolBarVisible = false;
            this.depSelect.ChangeSelectedItem += new System.EventHandler(this.depSelect_ChangeSelectedItem);
            // 
            // depDependsOnSelected
            // 
            this.depDependsOnSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.depDependsOnSelected.Items = null;
            this.depDependsOnSelected.Location = new System.Drawing.Point(185, 3);
            this.depDependsOnSelected.Name = "depDependsOnSelected";
            this.depDependsOnSelected.SelectedItem = null;
            this.depDependsOnSelected.Size = new System.Drawing.Size(176, 272);
            this.depDependsOnSelected.TabIndex = 1;
            this.depDependsOnSelected.Title = "s_depends_on_selected";
            this.depDependsOnSelected.ToolBarVisible = true;
            this.depDependsOnSelected.CallSelect += new System.EventHandler(this.depDependsOnSelected_CallSelect);
            // 
            // depSelectedDependsOn
            // 
            this.depSelectedDependsOn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.depSelectedDependsOn.Items = null;
            this.depSelectedDependsOn.Location = new System.Drawing.Point(367, 3);
            this.depSelectedDependsOn.Name = "depSelectedDependsOn";
            this.depSelectedDependsOn.SelectedItem = null;
            this.depSelectedDependsOn.Size = new System.Drawing.Size(177, 272);
            this.depSelectedDependsOn.TabIndex = 2;
            this.depSelectedDependsOn.Title = "s_selected_depends_on";
            this.depSelectedDependsOn.ToolBarVisible = true;
            this.depSelectedDependsOn.CallSelect += new System.EventHandler(this.depSelectedDependsOn_CallSelect);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(547, 278);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::Plugin.dbmodel.ModRes.big_loading_icon;
            this.pictureBox1.Location = new System.Drawing.Point(0, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(547, 278);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // cbxDepTypes
            // 
            this.cbxDepTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDepTypes.Name = "cbxDepTypes";
            this.cbxDepTypes.Size = new System.Drawing.Size(121, 25);
            this.cbxDepTypes.SelectedIndexChanged += new System.EventHandler(this.depSelect_ChangeSelectedItem);
            // 
            // DependencyBrowserFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "DependencyBrowserFrame";
            this.Size = new System.Drawing.Size(547, 303);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private DependencyItemsView depSelect;
        private DependencyItemsView depDependsOnSelected;
        private DependencyItemsView depSelectedDependsOn;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.ToolStripButton btnBack;
        private System.Windows.Forms.ToolStripButton btnForward;
        private System.Windows.Forms.ToolStripComboBox cbxDepTypes;
    }
}
