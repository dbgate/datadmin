namespace DatAdmin
{
    partial class DialectObjectFilterItemFrame
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
            this.chbEnabled = new System.Windows.Forms.CheckBox();
            this.cbxDialect = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // chbEnabled
            // 
            this.chbEnabled.AutoSize = true;
            this.chbEnabled.Location = new System.Drawing.Point(3, 1);
            this.chbEnabled.Name = "chbEnabled";
            this.chbEnabled.Size = new System.Drawing.Size(83, 17);
            this.chbEnabled.TabIndex = 3;
            this.chbEnabled.Text = "chbEnabled";
            this.chbEnabled.UseVisualStyleBackColor = true;
            this.chbEnabled.CheckedChanged += new System.EventHandler(this.chbEnabled_CheckedChanged);
            // 
            // cbxDialect
            // 
            this.cbxDialect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxDialect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDialect.FormattingEnabled = true;
            this.cbxDialect.Location = new System.Drawing.Point(111, 0);
            this.cbxDialect.Name = "cbxDialect";
            this.cbxDialect.Size = new System.Drawing.Size(299, 21);
            this.cbxDialect.TabIndex = 4;
            this.cbxDialect.SelectedIndexChanged += new System.EventHandler(this.cbxDialect_SelectedIndexChanged);
            // 
            // DialectObjectFilterItemFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbxDialect);
            this.Controls.Add(this.chbEnabled);
            this.Name = "DialectObjectFilterItemFrame";
            this.Size = new System.Drawing.Size(413, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxDialect;
        private System.Windows.Forms.CheckBox chbEnabled;
    }
}
