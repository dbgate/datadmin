namespace Plugin.versiondb
{
    partial class FilterByTypeTransformFrame
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
            this.chbRemoveSelected = new System.Windows.Forms.CheckBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // chbRemoveSelected
            // 
            this.chbRemoveSelected.AutoSize = true;
            this.chbRemoveSelected.Location = new System.Drawing.Point(12, 14);
            this.chbRemoveSelected.Name = "chbRemoveSelected";
            this.chbRemoveSelected.Size = new System.Drawing.Size(118, 17);
            this.chbRemoveSelected.TabIndex = 0;
            this.chbRemoveSelected.Text = "s_remove";
            this.chbRemoveSelected.UseVisualStyleBackColor = true;
            this.chbRemoveSelected.CheckedChanged += new System.EventHandler(this.chbRemoveSelected_CheckedChanged);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(10, 37);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(381, 259);
            this.checkedListBox1.TabIndex = 1;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            // 
            // FilterByTypeTransformFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.chbRemoveSelected);
            this.Name = "FilterByTypeTransformFrame";
            this.Size = new System.Drawing.Size(406, 308);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbRemoveSelected;
        private System.Windows.Forms.CheckedListBox checkedListBox1;


    }
}
