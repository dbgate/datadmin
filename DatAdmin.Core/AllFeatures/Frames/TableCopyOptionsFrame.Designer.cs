namespace DatAdmin
{
    partial class TableCopyOptionsFrame
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chbTruncateBeforeCopy = new System.Windows.Forms.CheckBox();
            this.chbAllowBulkCopy = new System.Windows.Forms.CheckBox();
            this.chbDisableConstraints = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chbDisableConstraints);
            this.groupBox1.Controls.Add(this.chbTruncateBeforeCopy);
            this.groupBox1.Controls.Add(this.chbAllowBulkCopy);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 94);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "s_table_options";
            // 
            // chbTruncateBeforeCopy
            // 
            this.chbTruncateBeforeCopy.AutoSize = true;
            this.chbTruncateBeforeCopy.Location = new System.Drawing.Point(16, 43);
            this.chbTruncateBeforeCopy.Name = "chbTruncateBeforeCopy";
            this.chbTruncateBeforeCopy.Size = new System.Drawing.Size(182, 17);
            this.chbTruncateBeforeCopy.TabIndex = 7;
            this.chbTruncateBeforeCopy.Text = "s_clear_destination_before_copy";
            this.chbTruncateBeforeCopy.UseVisualStyleBackColor = true;
            // 
            // chbAllowBulkCopy
            // 
            this.chbAllowBulkCopy.AutoSize = true;
            this.chbAllowBulkCopy.Checked = true;
            this.chbAllowBulkCopy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbAllowBulkCopy.Location = new System.Drawing.Point(16, 19);
            this.chbAllowBulkCopy.Name = "chbAllowBulkCopy";
            this.chbAllowBulkCopy.Size = new System.Drawing.Size(116, 17);
            this.chbAllowBulkCopy.TabIndex = 6;
            this.chbAllowBulkCopy.Text = "s_allow_bulk_copy";
            this.chbAllowBulkCopy.UseVisualStyleBackColor = true;
            // 
            // chbDisableConstraints
            // 
            this.chbDisableConstraints.AutoSize = true;
            this.chbDisableConstraints.Location = new System.Drawing.Point(16, 67);
            this.chbDisableConstraints.Name = "chbDisableConstraints";
            this.chbDisableConstraints.Size = new System.Drawing.Size(127, 17);
            this.chbDisableConstraints.TabIndex = 8;
            this.chbDisableConstraints.Text = "s_disable_constraints";
            this.chbDisableConstraints.UseVisualStyleBackColor = true;
            // 
            // TableCopyOptionsFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "TableCopyOptionsFrame";
            this.Size = new System.Drawing.Size(242, 94);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chbAllowBulkCopy;
        private System.Windows.Forms.CheckBox chbTruncateBeforeCopy;
        private System.Windows.Forms.CheckBox chbDisableConstraints;
    }
}
