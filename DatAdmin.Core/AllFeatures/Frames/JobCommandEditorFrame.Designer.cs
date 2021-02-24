namespace DatAdmin
{
    partial class JobCommandEditorFrame
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
            this.propertyFrame1 = new DatAdmin.PropertyFrame();
            this.SuspendLayout();
            // 
            // propertyFrame1
            // 
            this.propertyFrame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyFrame1.Location = new System.Drawing.Point(0, 0);
            this.propertyFrame1.Name = "propertyFrame1";
            this.propertyFrame1.SelectedObject = null;
            this.propertyFrame1.Size = new System.Drawing.Size(209, 228);
            this.propertyFrame1.TabIndex = 0;
            // 
            // JobCommandEditorFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.propertyFrame1);
            this.Name = "JobCommandEditorFrame";
            this.Size = new System.Drawing.Size(209, 228);
            this.ResumeLayout(false);

        }

        #endregion

        private PropertyFrame propertyFrame1;
    }
}
