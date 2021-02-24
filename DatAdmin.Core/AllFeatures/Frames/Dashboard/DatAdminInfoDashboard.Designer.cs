namespace DatAdmin
{
    partial class DatAdminInfoDashboardFrame
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
            this.htmlPanelEx1 = new DatAdmin.HtmlPanelEx();
            this.SuspendLayout();
            // 
            // htmlPanelEx1
            // 
            this.htmlPanelEx1.AutoScroll = true;
            this.htmlPanelEx1.AutoScrollMinSize = new System.Drawing.Size(150, 18);
            this.htmlPanelEx1.BackColor = System.Drawing.SystemColors.Window;
            this.htmlPanelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlPanelEx1.Location = new System.Drawing.Point(0, 0);
            this.htmlPanelEx1.Name = "htmlPanelEx1";
            this.htmlPanelEx1.Size = new System.Drawing.Size(150, 150);
            this.htmlPanelEx1.TabIndex = 0;
            this.htmlPanelEx1.Text = "htmlPanelEx1";
            // 
            // DatAdminInfoDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.htmlPanelEx1);
            this.Name = "DatAdminInfoDashboard";
            this.ResumeLayout(false);

        }

        #endregion

        private HtmlPanelEx htmlPanelEx1;

    }
}
