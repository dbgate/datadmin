namespace Plugin.apps
{
    partial class AppDesigner
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
            this.btnRemoveWidget = new System.Windows.Forms.ToolStripButton();
            this.cbxSelectedWidget = new System.Windows.Forms.ToolStripComboBox();
            this.panel1 = new DatAdmin.PanelEx();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRemoveWidget,
            this.cbxSelectedWidget});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(579, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnRemoveWidget
            // 
            this.btnRemoveWidget.Image = global::Plugin.apps.StdIcons.delete2;
            this.btnRemoveWidget.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoveWidget.Name = "btnRemoveWidget";
            this.btnRemoveWidget.Size = new System.Drawing.Size(69, 22);
            this.btnRemoveWidget.Text = "s_delete";
            this.btnRemoveWidget.Click += new System.EventHandler(this.btnRemoveWidget_Click);
            // 
            // cbxSelectedWidget
            // 
            this.cbxSelectedWidget.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cbxSelectedWidget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSelectedWidget.Name = "cbxSelectedWidget";
            this.cbxSelectedWidget.Size = new System.Drawing.Size(121, 25);
            this.cbxSelectedWidget.SelectedIndexChanged += new System.EventHandler(this.cbxSelectedControl_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Location = new System.Drawing.Point(12, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(552, 330);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // AppDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "AppDesigner";
            this.Size = new System.Drawing.Size(579, 381);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AppDesigner_MouseDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private DatAdmin.PanelEx panel1;
        private System.Windows.Forms.ToolStripButton btnRemoveWidget;
        private System.Windows.Forms.ToolStripComboBox cbxSelectedWidget;
    }
}
