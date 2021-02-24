namespace Plugin.apps
{
    partial class AppDesignForm
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
            DatAdmin.TreeBehaviour treeBehaviour1 = new DatAdmin.TreeBehaviour();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppDesignForm));
            this.daTreeView1 = new DatAdmin.DATreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnAddPage = new System.Windows.Forms.ToolStripButton();
            this.btnRemovePage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tbxDatabase = new System.Windows.Forms.ToolStripTextBox();
            this.btnChangeDatabase = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sapplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ssaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertyFrame1 = new DatAdmin.PropertyFrame();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.swidgetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.snextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // daTreeView1
            // 
            this.daTreeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.daTreeView1.Location = new System.Drawing.Point(0, 49);
            this.daTreeView1.Name = "daTreeView1";
            this.daTreeView1.Root = null;
            this.daTreeView1.RootPath = null;
            this.daTreeView1.Size = new System.Drawing.Size(194, 265);
            this.daTreeView1.TabIndex = 0;
            this.daTreeView1.TreeBehaviour = treeBehaviour1;
            this.daTreeView1.TreeStyle = DatAdmin.TreeStyle.TreeView;
            this.daTreeView1.ActiveNodeChange += new System.EventHandler(this.daTreeView1_ActiveNodeChange);
            this.daTreeView1.Click += new System.EventHandler(this.daTreeView1_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.btnAddPage,
            this.btnRemovePage,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tbxDatabase,
            this.btnChangeDatabase});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(623, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSave
            // 
            this.btnSave.Image = global::Plugin.apps.StdIcons.save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 22);
            this.btnSave.Text = "s_save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAddPage
            // 
            this.btnAddPage.Image = global::Plugin.apps.StdIcons.add;
            this.btnAddPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddPage.Name = "btnAddPage";
            this.btnAddPage.Size = new System.Drawing.Size(88, 22);
            this.btnAddPage.Text = "s_add_page";
            this.btnAddPage.Click += new System.EventHandler(this.btnAddPage_Click);
            // 
            // btnRemovePage
            // 
            this.btnRemovePage.Image = global::Plugin.apps.StdIcons.remove;
            this.btnRemovePage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemovePage.Name = "btnRemovePage";
            this.btnRemovePage.Size = new System.Drawing.Size(108, 22);
            this.btnRemovePage.Text = "s_remove_page";
            this.btnRemovePage.Click += new System.EventHandler(this.btnRemovePage_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(64, 22);
            this.toolStripLabel1.Text = "s_database";
            // 
            // tbxDatabase
            // 
            this.tbxDatabase.Name = "tbxDatabase";
            this.tbxDatabase.ReadOnly = true;
            this.tbxDatabase.Size = new System.Drawing.Size(100, 25);
            // 
            // btnChangeDatabase
            // 
            this.btnChangeDatabase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnChangeDatabase.Image = ((System.Drawing.Image)(resources.GetObject("btnChangeDatabase.Image")));
            this.btnChangeDatabase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnChangeDatabase.Name = "btnChangeDatabase";
            this.btnChangeDatabase.Size = new System.Drawing.Size(23, 22);
            this.btnChangeDatabase.Text = "...";
            this.btnChangeDatabase.Click += new System.EventHandler(this.btnChangeDatabase_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sapplicationToolStripMenuItem,
            this.swidgetToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(623, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sapplicationToolStripMenuItem
            // 
            this.sapplicationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ssaveToolStripMenuItem,
            this.scloseToolStripMenuItem});
            this.sapplicationToolStripMenuItem.Name = "sapplicationToolStripMenuItem";
            this.sapplicationToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.sapplicationToolStripMenuItem.Text = "s_application";
            // 
            // ssaveToolStripMenuItem
            // 
            this.ssaveToolStripMenuItem.Image = global::Plugin.apps.StdIcons.save;
            this.ssaveToolStripMenuItem.Name = "ssaveToolStripMenuItem";
            this.ssaveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.ssaveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ssaveToolStripMenuItem.Text = "s_save";
            this.ssaveToolStripMenuItem.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // scloseToolStripMenuItem
            // 
            this.scloseToolStripMenuItem.Name = "scloseToolStripMenuItem";
            this.scloseToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.scloseToolStripMenuItem.Text = "s_close";
            this.scloseToolStripMenuItem.Click += new System.EventHandler(this.scloseToolStripMenuItem_Click);
            // 
            // propertyFrame1
            // 
            this.propertyFrame1.Dock = System.Windows.Forms.DockStyle.Right;
            this.propertyFrame1.Location = new System.Drawing.Point(468, 49);
            this.propertyFrame1.Name = "propertyFrame1";
            this.propertyFrame1.SelectedObject = null;
            this.propertyFrame1.Size = new System.Drawing.Size(155, 265);
            this.propertyFrame1.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(194, 49);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(274, 265);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(266, 239);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(266, 239);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // swidgetToolStripMenuItem
            // 
            this.swidgetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.snextToolStripMenuItem});
            this.swidgetToolStripMenuItem.Name = "swidgetToolStripMenuItem";
            this.swidgetToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.swidgetToolStripMenuItem.Text = "s_widget";
            // 
            // snextToolStripMenuItem
            // 
            this.snextToolStripMenuItem.Name = "snextToolStripMenuItem";
            this.snextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)));
            this.snextToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.snextToolStripMenuItem.Text = "s_next";
            this.snextToolStripMenuItem.Click += new System.EventHandler(this.snextToolStripMenuItem_Click);
            // 
            // AppDesignForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 314);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.propertyFrame1);
            this.Controls.Add(this.daTreeView1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AppDesignForm";
            this.Text = "s_application";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DatAdmin.DATreeView daTreeView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sapplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ssaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scloseToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnAddPage;
        private System.Windows.Forms.ToolStripButton btnRemovePage;
        private DatAdmin.PropertyFrame propertyFrame1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tbxDatabase;
        private System.Windows.Forms.ToolStripButton btnChangeDatabase;
        private System.Windows.Forms.ToolStripMenuItem swidgetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem snextToolStripMenuItem;
    }
}