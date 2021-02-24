namespace Plugin.charts
{
    partial class ValuesCDSCFrame
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
            this.label2 = new System.Windows.Forms.Label();
            this.cbxLabelColumn = new System.Windows.Forms.ComboBox();
            this.chbValues = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "s_label";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "s_values";
            // 
            // cbxLabelColumn
            // 
            this.cbxLabelColumn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxLabelColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLabelColumn.FormattingEnabled = true;
            this.cbxLabelColumn.Location = new System.Drawing.Point(58, 3);
            this.cbxLabelColumn.Name = "cbxLabelColumn";
            this.cbxLabelColumn.Size = new System.Drawing.Size(121, 21);
            this.cbxLabelColumn.TabIndex = 2;
            this.cbxLabelColumn.SelectedIndexChanged += new System.EventHandler(this.cbxLabelColumn_SelectedIndexChanged);
            // 
            // chbValues
            // 
            this.chbValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chbValues.CheckOnClick = true;
            this.chbValues.FormattingEnabled = true;
            this.chbValues.Location = new System.Drawing.Point(58, 30);
            this.chbValues.Name = "chbValues";
            this.chbValues.Size = new System.Drawing.Size(120, 124);
            this.chbValues.TabIndex = 3;
            this.chbValues.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chbValues_ItemCheck);
            // 
            // ValuesCDSCFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chbValues);
            this.Controls.Add(this.cbxLabelColumn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ValuesCDSCFrame";
            this.Size = new System.Drawing.Size(182, 158);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxLabelColumn;
        private System.Windows.Forms.CheckedListBox chbValues;
    }
}
