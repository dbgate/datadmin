namespace DatAdmin
{
    partial class DashboardFrame
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
            WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
            WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin1 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient1 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient2 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient3 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient4 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient5 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient3 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient6 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient7 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnDesign = new System.Windows.Forms.ToolStripButton();
            this.btnRevert = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnSettings = new System.Windows.Forms.ToolStripButton();
            this.btnAddToFavorites = new System.Windows.Forms.ToolStripButton();
            this.dockPanel1 = new DatAdmin.DockPanelEx();
            this.toolStripDesign = new System.Windows.Forms.ToolStrip();
            this.btnSaveCloseDesign = new System.Windows.Forms.ToolStripButton();
            this.btnCloseDesign = new System.Windows.Forms.ToolStripButton();
            this.btnShowToolbox = new System.Windows.Forms.ToolStripButton();
            this.btnShowProperties = new System.Windows.Forms.ToolStripButton();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.toolStripDesign.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefresh,
            this.btnDesign,
            this.btnRevert,
            this.btnSettings,
            this.btnAddToFavorites});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(777, 23);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnDesign
            // 
            this.btnDesign.Image = global::DatAdmin.StdIcons.design;
            this.btnDesign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDesign.Name = "btnDesign";
            this.btnDesign.Size = new System.Drawing.Size(72, 20);
            this.btnDesign.Text = "s_design";
            this.btnDesign.Click += new System.EventHandler(this.btnDesign_Click);
            // 
            // btnRevert
            // 
            this.btnRevert.Image = global::DatAdmin.StdIcons.undo;
            this.btnRevert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRevert.Name = "btnRevert";
            this.btnRevert.Size = new System.Drawing.Size(76, 20);
            this.btnRevert.Text = "s_revert";
            // 
            // btnSettings
            // 
            this.btnSettings.Image = global::DatAdmin.StdIcons.settings;
            this.btnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(78, 20);
            this.btnSettings.Text = "s_settings";
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnAddToFavorites
            // 
            this.btnAddToFavorites.Image = global::DatAdmin.StdIcons.favorite_add;
            this.btnAddToFavorites.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddToFavorites.Name = "btnAddToFavorites";
            this.btnAddToFavorites.Size = new System.Drawing.Size(123, 20);
            this.btnAddToFavorites.Text = "s_add_to_favorites";
            this.btnAddToFavorites.Click += new System.EventHandler(this.btnAddToFavorites_Click);
            // 
            // dockPanel1
            // 
            this.dockPanel1.ActiveAutoHideContent = null;
            this.dockPanel1.AllowDrop = true;
            this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel1.DockBackColor = System.Drawing.SystemColors.Control;
            this.dockPanel1.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.dockPanel1.Location = new System.Drawing.Point(0, 46);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.ShowDocumentIcon = true;
            this.dockPanel1.Size = new System.Drawing.Size(777, 440);
            dockPanelGradient1.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient1.StartColor = System.Drawing.SystemColors.ControlLight;
            autoHideStripSkin1.DockStripGradient = dockPanelGradient1;
            tabGradient1.EndColor = System.Drawing.SystemColors.Control;
            tabGradient1.StartColor = System.Drawing.SystemColors.Control;
            tabGradient1.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            autoHideStripSkin1.TabGradient = tabGradient1;
            dockPanelSkin1.AutoHideStripSkin = autoHideStripSkin1;
            tabGradient2.EndColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.StartColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.ActiveTabGradient = tabGradient2;
            dockPanelGradient2.EndColor = System.Drawing.SystemColors.Control;
            dockPanelGradient2.StartColor = System.Drawing.SystemColors.Control;
            dockPaneStripGradient1.DockStripGradient = dockPanelGradient2;
            tabGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.InactiveTabGradient = tabGradient3;
            dockPaneStripSkin1.DocumentGradient = dockPaneStripGradient1;
            tabGradient4.EndColor = System.Drawing.SystemColors.ActiveCaption;
            tabGradient4.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient4.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
            tabGradient4.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            dockPaneStripToolWindowGradient1.ActiveCaptionGradient = tabGradient4;
            tabGradient5.EndColor = System.Drawing.SystemColors.Control;
            tabGradient5.StartColor = System.Drawing.SystemColors.Control;
            tabGradient5.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.ActiveTabGradient = tabGradient5;
            dockPanelGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            dockPaneStripToolWindowGradient1.DockStripGradient = dockPanelGradient3;
            tabGradient6.EndColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient6.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.InactiveCaptionGradient = tabGradient6;
            tabGradient7.EndColor = System.Drawing.Color.Transparent;
            tabGradient7.StartColor = System.Drawing.Color.Transparent;
            tabGradient7.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            dockPaneStripToolWindowGradient1.InactiveTabGradient = tabGradient7;
            dockPaneStripSkin1.ToolWindowGradient = dockPaneStripToolWindowGradient1;
            dockPanelSkin1.DockPaneStripSkin = dockPaneStripSkin1;
            this.dockPanel1.Skin = dockPanelSkin1;
            this.dockPanel1.TabIndex = 0;
            this.dockPanel1.ActiveContentChanged += new System.EventHandler(this.dockPanel1_ActiveContentChanged);
            this.dockPanel1.DragOver += new System.Windows.Forms.DragEventHandler(this.dockPanel1_DragOver);
            this.dockPanel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.dockPanel1_DragDrop);
            // 
            // toolStripDesign
            // 
            this.toolStripDesign.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSaveCloseDesign,
            this.btnCloseDesign,
            this.btnShowToolbox,
            this.btnShowProperties});
            this.toolStripDesign.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStripDesign.Location = new System.Drawing.Point(0, 23);
            this.toolStripDesign.Name = "toolStripDesign";
            this.toolStripDesign.Size = new System.Drawing.Size(777, 23);
            this.toolStripDesign.TabIndex = 2;
            this.toolStripDesign.Text = "toolStrip2";
            // 
            // btnSaveCloseDesign
            // 
            this.btnSaveCloseDesign.Image = global::DatAdmin.StdIcons.save;
            this.btnSaveCloseDesign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveCloseDesign.Name = "btnSaveCloseDesign";
            this.btnSaveCloseDesign.Size = new System.Drawing.Size(132, 20);
            this.btnSaveCloseDesign.Text = "s_save_close_design";
            this.btnSaveCloseDesign.Click += new System.EventHandler(this.btnSaveCloseDesign_Click);
            // 
            // btnCloseDesign
            // 
            this.btnCloseDesign.Image = global::DatAdmin.StdIcons.close;
            this.btnCloseDesign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCloseDesign.Name = "btnCloseDesign";
            this.btnCloseDesign.Size = new System.Drawing.Size(104, 20);
            this.btnCloseDesign.Text = "s_close_design";
            this.btnCloseDesign.Click += new System.EventHandler(this.btnCloseDesign_Click);
            // 
            // btnShowToolbox
            // 
            this.btnShowToolbox.Image = global::DatAdmin.StdIcons.toolbox;
            this.btnShowToolbox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowToolbox.Name = "btnShowToolbox";
            this.btnShowToolbox.Size = new System.Drawing.Size(77, 20);
            this.btnShowToolbox.Text = "s_toolbox";
            this.btnShowToolbox.Click += new System.EventHandler(this.tbnShowToolbox_Click);
            // 
            // btnShowProperties
            // 
            this.btnShowProperties.Image = global::DatAdmin.StdIcons.properties;
            this.btnShowProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowProperties.Name = "btnShowProperties";
            this.btnShowProperties.Size = new System.Drawing.Size(90, 20);
            this.btnShowProperties.Text = "s_properties";
            this.btnShowProperties.Click += new System.EventHandler(this.btnShowProperties_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::DatAdmin.StdIcons.refresh;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(73, 20);
            this.btnRefresh.Text = "s_refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // DashboardFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.toolStripDesign);
            this.Controls.Add(this.toolStrip1);
            this.Name = "DashboardFrame";
            this.Size = new System.Drawing.Size(777, 486);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStripDesign.ResumeLayout(false);
            this.toolStripDesign.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DatAdmin.DockPanelEx dockPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSettings;
        private System.Windows.Forms.ToolStripButton btnDesign;
        private System.Windows.Forms.ToolStripButton btnAddToFavorites;
        private System.Windows.Forms.ToolStrip toolStripDesign;
        private System.Windows.Forms.ToolStripButton btnSaveCloseDesign;
        private System.Windows.Forms.ToolStripButton btnCloseDesign;
        private System.Windows.Forms.ToolStripButton btnShowToolbox;
        private System.Windows.Forms.ToolStripButton btnShowProperties;
        private System.Windows.Forms.ToolStripDropDownButton btnRevert;
        private System.Windows.Forms.ToolStripButton btnRefresh;


    }
}
