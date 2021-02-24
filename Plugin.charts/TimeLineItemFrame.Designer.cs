namespace Plugin.charts
{
    partial class TimeLineItemFrame
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
            this.cbxAggr = new System.Windows.Forms.ComboBox();
            this.cbxColumn = new System.Windows.Forms.ComboBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.cbxAggr2 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbxAggr
            // 
            this.cbxAggr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAggr.FormattingEnabled = true;
            this.cbxAggr.Location = new System.Drawing.Point(0, 0);
            this.cbxAggr.Name = "cbxAggr";
            this.cbxAggr.Size = new System.Drawing.Size(77, 21);
            this.cbxAggr.TabIndex = 0;
            this.cbxAggr.SelectedIndexChanged += new System.EventHandler(this.cbxAggr_SelectedIndexChanged);
            // 
            // cbxColumn
            // 
            this.cbxColumn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxColumn.FormattingEnabled = true;
            this.cbxColumn.Location = new System.Drawing.Point(158, 0);
            this.cbxColumn.Name = "cbxColumn";
            this.cbxColumn.Size = new System.Drawing.Size(163, 21);
            this.cbxColumn.TabIndex = 1;
            this.cbxColumn.SelectedIndexChanged += new System.EventHandler(this.cbxColumn_SelectedIndexChanged);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Image = global::Plugin.charts.StdIcons.remove;
            this.btnRemove.Location = new System.Drawing.Point(327, 0);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(32, 23);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // cbxAggr2
            // 
            this.cbxAggr2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAggr2.FormattingEnabled = true;
            this.cbxAggr2.Location = new System.Drawing.Point(83, 0);
            this.cbxAggr2.Name = "cbxAggr2";
            this.cbxAggr2.Size = new System.Drawing.Size(69, 21);
            this.cbxAggr2.TabIndex = 3;
            // 
            // TimeLineItemFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbxAggr2);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.cbxColumn);
            this.Controls.Add(this.cbxAggr);
            this.Name = "TimeLineItemFrame";
            this.Size = new System.Drawing.Size(359, 23);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxAggr;
        private System.Windows.Forms.ComboBox cbxColumn;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ComboBox cbxAggr2;
    }
}
