namespace Plugin.textio
{
    partial class MultiTableDatabaseWriterFrame
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
            this.dataStoreFrame1 = new DatAdmin.DataStoreFrame();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxFileName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // dataStoreFrame1
            // 
            this.dataStoreFrame1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataStoreFrame1.CompactDesign = true;
            this.dataStoreFrame1.DataStoreMode = DatAdmin.TabularDataStoreMode.WriteStream;
            this.dataStoreFrame1.Location = new System.Drawing.Point(3, 35);
            this.dataStoreFrame1.Name = "dataStoreFrame1";
            this.dataStoreFrame1.Size = new System.Drawing.Size(434, 382);
            this.dataStoreFrame1.TabIndex = 0;
            this.dataStoreFrame1.ChangedDataStore += new System.EventHandler(this.dataStoreFrame1_ChangedDataStore);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "s_filename_template";
            // 
            // tbxFileName
            // 
            this.tbxFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxFileName.Location = new System.Drawing.Point(138, 9);
            this.tbxFileName.Name = "tbxFileName";
            this.tbxFileName.Size = new System.Drawing.Size(299, 20);
            this.tbxFileName.TabIndex = 2;
            this.tbxFileName.TextChanged += new System.EventHandler(this.tbxFileName_TextChanged);
            // 
            // MultiTableDatabaseWriterFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbxFileName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataStoreFrame1);
            this.Name = "MultiTableDatabaseWriterFrame";
            this.Size = new System.Drawing.Size(440, 417);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DatAdmin.DataStoreFrame dataStoreFrame1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxFileName;
    }
}
