namespace DatAdmin
{
    partial class PropertiesToolForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctlProperties = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // ctlProperties
            // 
            this.ctlProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlProperties.Location = new System.Drawing.Point(0, 0);
            this.ctlProperties.Name = "ctlProperties";
            this.ctlProperties.Size = new System.Drawing.Size(390, 379);
            this.ctlProperties.TabIndex = 0;
            this.ctlProperties.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // PropertiesToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 379);
            this.Controls.Add(this.ctlProperties);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "PropertiesToolForm";
            this.Text = "s_properties";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid ctlProperties;
    }
}