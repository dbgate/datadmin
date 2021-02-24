namespace Plugin.diagrams
{
    partial class EntityStyleFrame
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
            Plugin.diagrams.GradientDef gradientDef1 = new Plugin.diagrams.GradientDef();
            Plugin.diagrams.GradientDef gradientDef2 = new Plugin.diagrams.GradientDef();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxExisting = new System.Windows.Forms.ComboBox();
            this.chbHeader = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bgBody = new Plugin.diagrams.BackgroundColorFrame();
            this.bgHeader = new Plugin.diagrams.BackgroundColorFrame();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbtDefineOwn = new System.Windows.Forms.RadioButton();
            this.rbtUseInherited = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cbxExisting);
            this.groupBox1.Controls.Add(this.chbHeader);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.bgBody);
            this.groupBox1.Controls.Add(this.bgHeader);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(3, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(199, 361);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "s_style";
            // 
            // cbxExisting
            // 
            this.cbxExisting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxExisting.FormattingEnabled = true;
            this.cbxExisting.Location = new System.Drawing.Point(7, 20);
            this.cbxExisting.Name = "cbxExisting";
            this.cbxExisting.Size = new System.Drawing.Size(186, 21);
            this.cbxExisting.TabIndex = 4;
            this.cbxExisting.TextChanged += new System.EventHandler(this.cbxExisting_TextChanged);
            // 
            // chbHeader
            // 
            this.chbHeader.AutoSize = true;
            this.chbHeader.Location = new System.Drawing.Point(6, 58);
            this.chbHeader.Name = "chbHeader";
            this.chbHeader.Size = new System.Drawing.Size(70, 17);
            this.chbHeader.TabIndex = 3;
            this.chbHeader.Text = "s_header";
            this.chbHeader.UseVisualStyleBackColor = true;
            this.chbHeader.CheckedChanged += new System.EventHandler(this.chbHeader_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 216);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "s_body";
            // 
            // bgBody
            // 
            this.bgBody.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            gradientDef1.Angle = 0F;
            gradientDef1.BgColor = System.Drawing.SystemColors.Control;
            gradientDef1.GradientColor = System.Drawing.SystemColors.Control;
            gradientDef1.IsGradient = false;
            this.bgBody.Gradient = gradientDef1;
            this.bgBody.Location = new System.Drawing.Point(6, 232);
            this.bgBody.Name = "bgBody";
            this.bgBody.Size = new System.Drawing.Size(187, 114);
            this.bgBody.TabIndex = 1;
            this.bgBody.Changed += new System.EventHandler(this.bgBody_Changed);
            // 
            // bgHeader
            // 
            this.bgHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            gradientDef2.Angle = 0F;
            gradientDef2.BgColor = System.Drawing.SystemColors.Control;
            gradientDef2.GradientColor = System.Drawing.SystemColors.Control;
            gradientDef2.IsGradient = false;
            this.bgHeader.Gradient = gradientDef2;
            this.bgHeader.Location = new System.Drawing.Point(6, 81);
            this.bgHeader.Name = "bgHeader";
            this.bgHeader.Size = new System.Drawing.Size(187, 132);
            this.bgHeader.TabIndex = 0;
            this.bgHeader.Changed += new System.EventHandler(this.bgHeader_Changed);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.rbtDefineOwn);
            this.panel1.Controls.Add(this.rbtUseInherited);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(199, 43);
            this.panel1.TabIndex = 5;
            // 
            // rbtDefineOwn
            // 
            this.rbtDefineOwn.AutoSize = true;
            this.rbtDefineOwn.Location = new System.Drawing.Point(9, 23);
            this.rbtDefineOwn.Name = "rbtDefineOwn";
            this.rbtDefineOwn.Size = new System.Drawing.Size(91, 17);
            this.rbtDefineOwn.TabIndex = 1;
            this.rbtDefineOwn.Text = "s_define_own";
            this.rbtDefineOwn.UseVisualStyleBackColor = true;
            this.rbtDefineOwn.CheckedChanged += new System.EventHandler(this.chbOverrideTableStyle_CheckedChanged);
            // 
            // rbtUseInherited
            // 
            this.rbtUseInherited.AutoSize = true;
            this.rbtUseInherited.Checked = true;
            this.rbtUseInherited.Location = new System.Drawing.Point(9, 3);
            this.rbtUseInherited.Name = "rbtUseInherited";
            this.rbtUseInherited.Size = new System.Drawing.Size(136, 17);
            this.rbtUseInherited.TabIndex = 0;
            this.rbtUseInherited.TabStop = true;
            this.rbtUseInherited.Text = "s_use_inherited_values";
            this.rbtUseInherited.UseVisualStyleBackColor = true;
            this.rbtUseInherited.CheckedChanged += new System.EventHandler(this.chbOverrideTableStyle_CheckedChanged);
            // 
            // EntityStyleFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "EntityStyleFrame";
            this.Size = new System.Drawing.Size(205, 416);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chbHeader;
        private System.Windows.Forms.Label label1;
        private BackgroundColorFrame bgBody;
        private BackgroundColorFrame bgHeader;
        private System.Windows.Forms.ComboBox cbxExisting;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbtDefineOwn;
        private System.Windows.Forms.RadioButton rbtUseInherited;
    }
}
