namespace DatAdmin
{
    partial class ColumnMapFrame
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
            this.cbxSavedTransforms = new System.Windows.Forms.ToolStripComboBox();
            this.btnLoad = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.splitContainerEx1 = new DatAdmin.SplitContainerEx();
            this.panel1 = new System.Windows.Forms.Panel();
            this.codeEditor1 = new DatAdmin.CodeEditor();
            this.infoBoxFrame1 = new DatAdmin.InfoBoxFrame();
            this.toolStrip1.SuspendLayout();
            this.splitContainerEx1.Panel1.SuspendLayout();
            this.splitContainerEx1.Panel2.SuspendLayout();
            this.splitContainerEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbxSavedTransforms,
            this.btnLoad,
            this.btnSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(650, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cbxSavedTransforms
            // 
            this.cbxSavedTransforms.Name = "cbxSavedTransforms";
            this.cbxSavedTransforms.Size = new System.Drawing.Size(121, 25);
            this.cbxSavedTransforms.SelectedIndexChanged += new System.EventHandler(this.cbxSavedTransforms_Click);
            this.cbxSavedTransforms.TextUpdate += new System.EventHandler(this.cbxSavedTransforms_Click);
            this.cbxSavedTransforms.DropDownClosed += new System.EventHandler(this.cbxSavedTransforms_Click);
            this.cbxSavedTransforms.Click += new System.EventHandler(this.cbxSavedTransforms_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Image = global::DatAdmin.StdIcons.open;
            this.btnLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(60, 22);
            this.btnLoad.Text = "s_load";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
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
            this.splitContainerEx1.Panel1.Controls.Add(this.panel1);
            this.splitContainerEx1.Panel1DynamicCollapsed = false;
            this.splitContainerEx1.Panel1MinDynamicSize = 0;
            // 
            // splitContainerEx1.Panel2
            // 
            this.splitContainerEx1.Panel2.Controls.Add(this.codeEditor1);
            this.splitContainerEx1.Panel2.Controls.Add(this.infoBoxFrame1);
            this.splitContainerEx1.Panel2DynamicCollapsed = false;
            this.splitContainerEx1.Panel2MinDynamicSize = 0;
            this.splitContainerEx1.Size = new System.Drawing.Size(650, 390);
            this.splitContainerEx1.SplitterDistance = 219;
            this.splitContainerEx1.SplitterWidth = 6;
            this.splitContainerEx1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(650, 219);
            this.panel1.TabIndex = 0;
            // 
            // codeEditor1
            // 
            this.codeEditor1.Dialect = null;
            this.codeEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeEditor1.IsReadOnly = false;
            this.codeEditor1.Language = DatAdmin.CodeLanguage.Python;
            this.codeEditor1.Location = new System.Drawing.Point(0, 72);
            this.codeEditor1.Modified = true;
            this.codeEditor1.Name = "codeEditor1";
            this.codeEditor1.ReadOnly = false;
            this.codeEditor1.Size = new System.Drawing.Size(650, 93);
            this.codeEditor1.TabIndex = 1;
            // 
            // infoBoxFrame1
            // 
            this.infoBoxFrame1.Dock = System.Windows.Forms.DockStyle.Top;
            this.infoBoxFrame1.InfoText = "s_column_map_script_info";
            this.infoBoxFrame1.Location = new System.Drawing.Point(0, 0);
            this.infoBoxFrame1.Name = "infoBoxFrame1";
            this.infoBoxFrame1.Padding = new System.Windows.Forms.Padding(8);
            this.infoBoxFrame1.Size = new System.Drawing.Size(650, 72);
            this.infoBoxFrame1.TabIndex = 0;
            // 
            // ColumnMapFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerEx1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ColumnMapFrame";
            this.Size = new System.Drawing.Size(650, 415);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainerEx1.Panel1.ResumeLayout(false);
            this.splitContainerEx1.Panel2.ResumeLayout(false);
            this.splitContainerEx1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private InfoBoxFrame infoBoxFrame1;
        private CodeEditor codeEditor1;
        private SplitContainerEx splitContainerEx1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox cbxSavedTransforms;
        private System.Windows.Forms.ToolStripButton btnLoad;
        private System.Windows.Forms.ToolStripButton btnSave;

    }
}
