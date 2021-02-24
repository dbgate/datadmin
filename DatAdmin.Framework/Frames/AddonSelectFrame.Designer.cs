namespace DatAdmin
{
    partial class AddonSelectFrame
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
            this.btsaveastemplate = new System.Windows.Forms.Button();
            this.propertyFrame1 = new DatAdmin.PropertyFrame();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxTypes = new System.Windows.Forms.ComboBox();
            this.panelCompact = new System.Windows.Forms.Panel();
            this.btnSwitchDesign2 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelLarge = new System.Windows.Forms.Panel();
            this.btnSwitchDesign1 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.infoBoxFrame1 = new DatAdmin.InfoBoxFrame();
            this.lbxTypes = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelCompact.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelLarge.SuspendLayout();
            this.SuspendLayout();
            // 
            // btsaveastemplate
            // 
            this.btsaveastemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btsaveastemplate.Location = new System.Drawing.Point(420, 3);
            this.btsaveastemplate.Name = "btsaveastemplate";
            this.btsaveastemplate.Size = new System.Drawing.Size(151, 23);
            this.btsaveastemplate.TabIndex = 8;
            this.btsaveastemplate.Text = "s_save_as_template";
            this.btsaveastemplate.UseVisualStyleBackColor = true;
            this.btsaveastemplate.Click += new System.EventHandler(this.btsaveastemplate_Click);
            // 
            // propertyFrame1
            // 
            this.propertyFrame1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyFrame1.CacheCustomEditors = true;
            this.propertyFrame1.Location = new System.Drawing.Point(3, 6);
            this.propertyFrame1.Name = "propertyFrame1";
            this.propertyFrame1.SelectedObject = null;
            this.propertyFrame1.Size = new System.Drawing.Size(606, 234);
            this.propertyFrame1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "s_type";
            // 
            // cbxTypes
            // 
            this.cbxTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTypes.FormattingEnabled = true;
            this.cbxTypes.Location = new System.Drawing.Point(99, 3);
            this.cbxTypes.Name = "cbxTypes";
            this.cbxTypes.Size = new System.Drawing.Size(315, 21);
            this.cbxTypes.TabIndex = 9;
            this.cbxTypes.SelectedIndexChanged += new System.EventHandler(this.CurrentList_SelectedIndexChanged);
            // 
            // panelCompact
            // 
            this.panelCompact.Controls.Add(this.btnSwitchDesign2);
            this.panelCompact.Controls.Add(this.label1);
            this.panelCompact.Controls.Add(this.cbxTypes);
            this.panelCompact.Controls.Add(this.btsaveastemplate);
            this.panelCompact.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCompact.Location = new System.Drawing.Point(0, 130);
            this.panelCompact.Name = "panelCompact";
            this.panelCompact.Size = new System.Drawing.Size(609, 27);
            this.panelCompact.TabIndex = 10;
            // 
            // btnSwitchDesign2
            // 
            this.btnSwitchDesign2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSwitchDesign2.Location = new System.Drawing.Point(577, 1);
            this.btnSwitchDesign2.Name = "btnSwitchDesign2";
            this.btnSwitchDesign2.Size = new System.Drawing.Size(29, 25);
            this.btnSwitchDesign2.TabIndex = 10;
            this.btnSwitchDesign2.Text = "+";
            this.btnSwitchDesign2.UseVisualStyleBackColor = true;
            this.btnSwitchDesign2.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.propertyFrame1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 157);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(609, 243);
            this.panel2.TabIndex = 11;
            // 
            // panelLarge
            // 
            this.panelLarge.Controls.Add(this.btnSwitchDesign1);
            this.panelLarge.Controls.Add(this.button1);
            this.panelLarge.Controls.Add(this.infoBoxFrame1);
            this.panelLarge.Controls.Add(this.lbxTypes);
            this.panelLarge.Controls.Add(this.label2);
            this.panelLarge.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLarge.Location = new System.Drawing.Point(0, 0);
            this.panelLarge.Name = "panelLarge";
            this.panelLarge.Size = new System.Drawing.Size(609, 130);
            this.panelLarge.TabIndex = 12;
            this.panelLarge.Visible = false;
            this.panelLarge.Resize += new System.EventHandler(this.panelLarge_Resize);
            // 
            // btnSwitchDesign1
            // 
            this.btnSwitchDesign1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSwitchDesign1.Location = new System.Drawing.Point(577, 5);
            this.btnSwitchDesign1.Name = "btnSwitchDesign1";
            this.btnSwitchDesign1.Size = new System.Drawing.Size(29, 22);
            this.btnSwitchDesign1.TabIndex = 4;
            this.btnSwitchDesign1.Text = "-";
            this.btnSwitchDesign1.UseVisualStyleBackColor = true;
            this.btnSwitchDesign1.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(420, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(151, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "s_save_as_template";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btsaveastemplate_Click);
            // 
            // infoBoxFrame1
            // 
            this.infoBoxFrame1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.infoBoxFrame1.InfoText = "";
            this.infoBoxFrame1.Location = new System.Drawing.Point(298, 34);
            this.infoBoxFrame1.Name = "infoBoxFrame1";
            this.infoBoxFrame1.Size = new System.Drawing.Size(308, 82);
            this.infoBoxFrame1.TabIndex = 2;
            // 
            // lbxTypes
            // 
            this.lbxTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxTypes.FormattingEnabled = true;
            this.lbxTypes.Location = new System.Drawing.Point(6, 34);
            this.lbxTypes.Name = "lbxTypes";
            this.lbxTypes.Size = new System.Drawing.Size(272, 82);
            this.lbxTypes.TabIndex = 1;
            this.lbxTypes.SelectedIndexChanged += new System.EventHandler(this.CurrentList_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "s_type";
            // 
            // AddonSelectFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelCompact);
            this.Controls.Add(this.panelLarge);
            this.Name = "AddonSelectFrame";
            this.Size = new System.Drawing.Size(609, 400);
            this.panelCompact.ResumeLayout(false);
            this.panelCompact.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panelLarge.ResumeLayout(false);
            this.panelLarge.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btsaveastemplate;
        private PropertyFrame propertyFrame1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxTypes;
        private System.Windows.Forms.Panel panelCompact;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelLarge;
        private System.Windows.Forms.Button button1;
        private InfoBoxFrame infoBoxFrame1;
        private System.Windows.Forms.ListBox lbxTypes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSwitchDesign1;
        private System.Windows.Forms.Button btnSwitchDesign2;
    }
}
