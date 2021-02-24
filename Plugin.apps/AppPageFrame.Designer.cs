namespace Plugin.apps
{
    partial class AppPageFrame
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabDesign = new System.Windows.Forms.TabPage();
            this.appDesigner1 = new Plugin.apps.AppDesigner();
            this.tabPreview = new System.Windows.Forms.TabPage();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabDesign.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabDesign);
            this.tabControl1.Controls.Add(this.tabPreview);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(349, 261);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabDesign
            // 
            this.tabDesign.Controls.Add(this.appDesigner1);
            this.tabDesign.Location = new System.Drawing.Point(4, 23);
            this.tabDesign.Name = "tabDesign";
            this.tabDesign.Padding = new System.Windows.Forms.Padding(3);
            this.tabDesign.Size = new System.Drawing.Size(341, 234);
            this.tabDesign.TabIndex = 0;
            this.tabDesign.Text = "s_design";
            this.tabDesign.UseVisualStyleBackColor = true;
            // 
            // appDesigner1
            // 
            this.appDesigner1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.appDesigner1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.appDesigner1.Location = new System.Drawing.Point(3, 3);
            this.appDesigner1.Name = "appDesigner1";
            this.appDesigner1.Page = null;
            this.appDesigner1.SelectedWidget = null;
            this.appDesigner1.Size = new System.Drawing.Size(335, 228);
            this.appDesigner1.TabIndex = 0;
            // 
            // tabPreview
            // 
            this.tabPreview.Location = new System.Drawing.Point(4, 23);
            this.tabPreview.Name = "tabPreview";
            this.tabPreview.Padding = new System.Windows.Forms.Padding(3);
            this.tabPreview.Size = new System.Drawing.Size(341, 234);
            this.tabPreview.TabIndex = 1;
            this.tabPreview.Text = "s_preview";
            this.tabPreview.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(341, 234);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = ".NET design";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(23, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 171);
            this.panel1.TabIndex = 0;
            // 
            // AppPageFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "AppPageFrame";
            this.Size = new System.Drawing.Size(349, 261);
            this.tabControl1.ResumeLayout(false);
            this.tabDesign.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabDesign;
        private System.Windows.Forms.TabPage tabPreview;
        private System.Windows.Forms.ImageList imageList1;
        private AppDesigner appDesigner1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel1;
    }
}
