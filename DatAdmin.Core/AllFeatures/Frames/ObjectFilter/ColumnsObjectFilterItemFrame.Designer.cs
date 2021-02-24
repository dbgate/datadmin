namespace DatAdmin
{
    partial class ColumnsObjectFilterItemFrame
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
            this.tbxList = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // chbEnabled
            // 
            this.chbEnabled.AutoSize = true;
            this.chbEnabled.Location = new System.Drawing.Point(3, 3);
            this.chbEnabled.Name = "chbEnabled";
            this.chbEnabled.Size = new System.Drawing.Size(83, 17);
            this.chbEnabled.TabIndex = 6;
            this.chbEnabled.Text = "chbEnabled";
            this.chbEnabled.UseVisualStyleBackColor = true;
            this.chbEnabled.CheckedChanged += new System.EventHandler(this.chbEnabled_CheckedChanged);
            // 
            // tbxList
            // 
            this.tbxList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxList.Location = new System.Drawing.Point(3, 26);
            this.tbxList.Multiline = true;
            this.tbxList.Name = "tbxList";
            this.tbxList.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbxList.Size = new System.Drawing.Size(262, 159);
            this.tbxList.TabIndex = 5;
            this.tbxList.WordWrap = false;
            this.tbxList.TextChanged += new System.EventHandler(this.tbxList_TextChanged);
            // 
            // ColumnsObjectFilterItemFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chbEnabled);
            this.Controls.Add(this.tbxList);
            this.Name = "ColumnsObjectFilterItemFrame";
            this.Size = new System.Drawing.Size(268, 188);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbEnabled;
        private System.Windows.Forms.TextBox tbxList;
    }
}
