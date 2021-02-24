namespace Plugin.textio
{
    partial class DataStoreWriterFrame
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
            this.tbxQuery = new System.Windows.Forms.TextBox();
            this.dataStoreFrame1 = new DatAdmin.DataStoreFrame();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "s_query";
            // 
            // tbxQuery
            // 
            this.tbxQuery.Location = new System.Drawing.Point(65, 3);
            this.tbxQuery.Name = "tbxQuery";
            this.tbxQuery.Size = new System.Drawing.Size(318, 20);
            this.tbxQuery.TabIndex = 2;
            this.tbxQuery.TextChanged += new System.EventHandler(this.tbxQuery_TextChanged);
            // 
            // dataStoreFrame1
            // 
            this.dataStoreFrame1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataStoreFrame1.DataStoreMode = DatAdmin.TabularDataStoreMode.Unknown;
            this.dataStoreFrame1.Location = new System.Drawing.Point(3, 23);
            this.dataStoreFrame1.Name = "dataStoreFrame1";
            this.dataStoreFrame1.Size = new System.Drawing.Size(380, 270);
            this.dataStoreFrame1.TabIndex = 0;
            this.dataStoreFrame1.ChangedDataStore += new System.EventHandler(this.dataStoreFrame1_ChangedDataStore);
            // 
            // DataStoreWriterFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbxQuery);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataStoreFrame1);
            this.Name = "DataStoreWriterFrame";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DatAdmin.DataStoreFrame dataStoreFrame1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxQuery;
    }
}
