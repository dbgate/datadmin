namespace DatAdmin
{
    partial class ImageCellDataFrame
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
            this.propertyGridImage = new System.Windows.Forms.PropertyGrid();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // propertyGridImage
            // 
            this.propertyGridImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.propertyGridImage.HelpVisible = false;
            this.propertyGridImage.Location = new System.Drawing.Point(0, 0);
            this.propertyGridImage.Name = "propertyGridImage";
            this.propertyGridImage.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propertyGridImage.Size = new System.Drawing.Size(206, 355);
            this.propertyGridImage.TabIndex = 2;
            this.propertyGridImage.ToolbarVisible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(206, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(394, 355);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // ImageCellDataFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.propertyGridImage);
            this.Name = "ImageCellDataFrame";
            this.Size = new System.Drawing.Size(600, 355);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGridImage;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
