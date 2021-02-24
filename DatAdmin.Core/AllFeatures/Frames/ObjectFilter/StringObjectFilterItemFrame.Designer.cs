namespace DatAdmin
{
    partial class StringObjectFilterItemFrame
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
            this.tbxCondition = new System.Windows.Forms.TextBox();
            this.chbRegex = new System.Windows.Forms.CheckBox();
            this.chbEnabled = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tbxCondition
            // 
            this.tbxCondition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxCondition.Location = new System.Drawing.Point(111, 0);
            this.tbxCondition.Name = "tbxCondition";
            this.tbxCondition.Size = new System.Drawing.Size(263, 20);
            this.tbxCondition.TabIndex = 0;
            this.tbxCondition.TextChanged += new System.EventHandler(this.tbxCondition_TextChanged);
            // 
            // chbRegex
            // 
            this.chbRegex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbRegex.Location = new System.Drawing.Point(380, 2);
            this.chbRegex.Name = "chbRegex";
            this.chbRegex.Size = new System.Drawing.Size(130, 17);
            this.chbRegex.TabIndex = 1;
            this.chbRegex.Text = "s_regex";
            this.chbRegex.UseVisualStyleBackColor = true;
            this.chbRegex.CheckedChanged += new System.EventHandler(this.chbRegex_CheckedChanged);
            // 
            // chbEnabled
            // 
            this.chbEnabled.AutoSize = true;
            this.chbEnabled.Location = new System.Drawing.Point(3, 2);
            this.chbEnabled.Name = "chbEnabled";
            this.chbEnabled.Size = new System.Drawing.Size(83, 17);
            this.chbEnabled.TabIndex = 2;
            this.chbEnabled.Text = "chbEnabled";
            this.chbEnabled.UseVisualStyleBackColor = true;
            this.chbEnabled.CheckedChanged += new System.EventHandler(this.chbEnabled_CheckedChanged);
            // 
            // StringObjectFilterItemFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chbEnabled);
            this.Controls.Add(this.chbRegex);
            this.Controls.Add(this.tbxCondition);
            this.Name = "StringObjectFilterItemFrame";
            this.Size = new System.Drawing.Size(510, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxCondition;
        private System.Windows.Forms.CheckBox chbRegex;
        private System.Windows.Forms.CheckBox chbEnabled;
    }
}
