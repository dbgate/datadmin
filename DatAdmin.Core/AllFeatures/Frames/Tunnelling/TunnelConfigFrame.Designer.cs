namespace DatAdmin
{
    partial class TunnelConfigFrame
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
            this.rbtDirectConnection = new System.Windows.Forms.RadioButton();
            this.rbtUseTunnel = new System.Windows.Forms.RadioButton();
            this.addonSelectFrame1 = new DatAdmin.AddonSelectFrame();
            this.SuspendLayout();
            // 
            // rbtDirectConnection
            // 
            this.rbtDirectConnection.AutoSize = true;
            this.rbtDirectConnection.Checked = true;
            this.rbtDirectConnection.Location = new System.Drawing.Point(3, 3);
            this.rbtDirectConnection.Name = "rbtDirectConnection";
            this.rbtDirectConnection.Size = new System.Drawing.Size(121, 17);
            this.rbtDirectConnection.TabIndex = 1;
            this.rbtDirectConnection.TabStop = true;
            this.rbtDirectConnection.Text = "s_direct_connection";
            this.rbtDirectConnection.UseVisualStyleBackColor = true;
            this.rbtDirectConnection.CheckedChanged += new System.EventHandler(this.rbtUseTunnel_CheckedChanged);
            // 
            // rbtUseTunnel
            // 
            this.rbtUseTunnel.AutoSize = true;
            this.rbtUseTunnel.Location = new System.Drawing.Point(161, 3);
            this.rbtUseTunnel.Name = "rbtUseTunnel";
            this.rbtUseTunnel.Size = new System.Drawing.Size(88, 17);
            this.rbtUseTunnel.TabIndex = 2;
            this.rbtUseTunnel.Text = "s_use_tunnel";
            this.rbtUseTunnel.UseVisualStyleBackColor = true;
            this.rbtUseTunnel.CheckedChanged += new System.EventHandler(this.rbtUseTunnel_CheckedChanged);
            // 
            // addonSelectFrame1
            // 
            this.addonSelectFrame1.AddonTypeName = "tunneldriver";
            this.addonSelectFrame1.AllowSwitchDesign = false;
            this.addonSelectFrame1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.addonSelectFrame1.CanSaveAsTemplate = false;
            this.addonSelectFrame1.CompactDesign = true;
            this.addonSelectFrame1.DefaultAddonHolder = "http_tunnel";
            this.addonSelectFrame1.Enabled = false;
            this.addonSelectFrame1.Location = new System.Drawing.Point(0, 26);
            this.addonSelectFrame1.Name = "addonSelectFrame1";
            this.addonSelectFrame1.Size = new System.Drawing.Size(440, 362);
            this.addonSelectFrame1.TabIndex = 0;
            this.addonSelectFrame1.TypeTitle = "s_type";
            this.addonSelectFrame1.ChangedSelectedObject += new System.EventHandler(this.addonSelectFrame1_ChangedSelectedObject);
            // 
            // TunnelConfigFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rbtUseTunnel);
            this.Controls.Add(this.rbtDirectConnection);
            this.Controls.Add(this.addonSelectFrame1);
            this.Name = "TunnelConfigFrame";
            this.Size = new System.Drawing.Size(443, 391);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AddonSelectFrame addonSelectFrame1;
        private System.Windows.Forms.RadioButton rbtDirectConnection;
        private System.Windows.Forms.RadioButton rbtUseTunnel;
    }
}
