namespace DatAdmin
{
    partial class DashboardEditorFrame
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbxPriority = new System.Windows.Forms.TextBox();
            this.addonSelectFrame1 = new DatAdmin.AddonSelectFrame();
            this.rbtUseDefault = new System.Windows.Forms.RadioButton();
            this.rbtOverride = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "s_priority";
            // 
            // tbxPriority
            // 
            this.tbxPriority.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxPriority.Location = new System.Drawing.Point(98, 34);
            this.tbxPriority.Name = "tbxPriority";
            this.tbxPriority.Size = new System.Drawing.Size(303, 20);
            this.tbxPriority.TabIndex = 2;
            this.tbxPriority.TextChanged += new System.EventHandler(this.tbxPriority_TextChanged);
            // 
            // addonSelectFrame1
            // 
            this.addonSelectFrame1.AddonTypeName = "objectfilter";
            this.addonSelectFrame1.AllowSwitchDesign = true;
            this.addonSelectFrame1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.addonSelectFrame1.CanSaveAsTemplate = false;
            this.addonSelectFrame1.CompactDesign = true;
            this.addonSelectFrame1.DefaultAddonHolder = null;
            this.addonSelectFrame1.Location = new System.Drawing.Point(0, 60);
            this.addonSelectFrame1.Name = "addonSelectFrame1";
            this.addonSelectFrame1.ShowInfoFrame = true;
            this.addonSelectFrame1.Size = new System.Drawing.Size(401, 293);
            this.addonSelectFrame1.TabIndex = 0;
            this.addonSelectFrame1.TypeTitle = "s_type";
            this.addonSelectFrame1.ChangedSelectedObject += new System.EventHandler(this.addonSelectFrame1_ChangedSelectedObject);
            // 
            // rbtUseDefault
            // 
            this.rbtUseDefault.AutoSize = true;
            this.rbtUseDefault.Location = new System.Drawing.Point(6, 4);
            this.rbtUseDefault.Name = "rbtUseDefault";
            this.rbtUseDefault.Size = new System.Drawing.Size(91, 17);
            this.rbtUseDefault.TabIndex = 3;
            this.rbtUseDefault.TabStop = true;
            this.rbtUseDefault.Text = "s_use_default";
            this.rbtUseDefault.UseVisualStyleBackColor = true;
            this.rbtUseDefault.CheckedChanged += new System.EventHandler(this.rbtOverride_CheckedChanged);
            // 
            // rbtOverride
            // 
            this.rbtOverride.AutoSize = true;
            this.rbtOverride.Location = new System.Drawing.Point(178, 4);
            this.rbtOverride.Name = "rbtOverride";
            this.rbtOverride.Size = new System.Drawing.Size(74, 17);
            this.rbtOverride.TabIndex = 4;
            this.rbtOverride.TabStop = true;
            this.rbtOverride.Text = "s_override";
            this.rbtOverride.UseVisualStyleBackColor = true;
            this.rbtOverride.CheckedChanged += new System.EventHandler(this.rbtOverride_CheckedChanged);
            // 
            // DashboardEditorFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rbtOverride);
            this.Controls.Add(this.rbtUseDefault);
            this.Controls.Add(this.tbxPriority);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addonSelectFrame1);
            this.Name = "DashboardEditorFrame";
            this.Size = new System.Drawing.Size(404, 353);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AddonSelectFrame addonSelectFrame1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxPriority;
        private System.Windows.Forms.RadioButton rbtUseDefault;
        private System.Windows.Forms.RadioButton rbtOverride;
    }
}
