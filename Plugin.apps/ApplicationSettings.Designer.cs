namespace Plugin.apps
{
    partial class ApplicationSettingsFrame
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.infoBoxFrame1 = new DatAdmin.InfoBoxFrame();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 79);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(387, 198);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // infoBoxFrame1
            // 
            this.infoBoxFrame1.Dock = System.Windows.Forms.DockStyle.Top;
            this.infoBoxFrame1.InfoText = "s_applications_settings_info";
            this.infoBoxFrame1.Location = new System.Drawing.Point(0, 0);
            this.infoBoxFrame1.Name = "infoBoxFrame1";
            this.infoBoxFrame1.Padding = new System.Windows.Forms.Padding(8);
            this.infoBoxFrame1.Size = new System.Drawing.Size(387, 79);
            this.infoBoxFrame1.TabIndex = 1;
            // 
            // ApplicationSettingsFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.infoBoxFrame1);
            this.Name = "ApplicationSettingsFrame";
            this.Size = new System.Drawing.Size(387, 277);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private DatAdmin.InfoBoxFrame infoBoxFrame1;
    }
}
