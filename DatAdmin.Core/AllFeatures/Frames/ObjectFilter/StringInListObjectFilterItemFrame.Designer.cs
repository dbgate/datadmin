namespace DatAdmin
{
    partial class StringInListObjectFilterItemFrame
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
            this.rbtRegex = new System.Windows.Forms.RadioButton();
            this.rbtList = new System.Windows.Forms.RadioButton();
            this.tbxRegexVal = new System.Windows.Forms.TextBox();
            this.tbxList = new System.Windows.Forms.TextBox();
            this.chbEnabled = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // rbtRegex
            // 
            this.rbtRegex.AutoSize = true;
            this.rbtRegex.Location = new System.Drawing.Point(27, 26);
            this.rbtRegex.Name = "rbtRegex";
            this.rbtRegex.Size = new System.Drawing.Size(62, 17);
            this.rbtRegex.TabIndex = 0;
            this.rbtRegex.TabStop = true;
            this.rbtRegex.Text = "s_regex";
            this.rbtRegex.UseVisualStyleBackColor = true;
            this.rbtRegex.CheckedChanged += new System.EventHandler(this.rbtRegex_CheckedChanged);
            // 
            // rbtList
            // 
            this.rbtList.AutoSize = true;
            this.rbtList.Location = new System.Drawing.Point(27, 52);
            this.rbtList.Name = "rbtList";
            this.rbtList.Size = new System.Drawing.Size(48, 17);
            this.rbtList.TabIndex = 1;
            this.rbtList.TabStop = true;
            this.rbtList.Text = "s_list";
            this.rbtList.UseVisualStyleBackColor = true;
            this.rbtList.CheckedChanged += new System.EventHandler(this.rbtRegex_CheckedChanged);
            // 
            // tbxRegexVal
            // 
            this.tbxRegexVal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxRegexVal.Location = new System.Drawing.Point(140, 25);
            this.tbxRegexVal.Name = "tbxRegexVal";
            this.tbxRegexVal.Size = new System.Drawing.Size(228, 20);
            this.tbxRegexVal.TabIndex = 2;
            this.tbxRegexVal.TextChanged += new System.EventHandler(this.tbxRegexVal_TextChanged);
            // 
            // tbxList
            // 
            this.tbxList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxList.Location = new System.Drawing.Point(140, 51);
            this.tbxList.Multiline = true;
            this.tbxList.Name = "tbxList";
            this.tbxList.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbxList.Size = new System.Drawing.Size(228, 103);
            this.tbxList.TabIndex = 3;
            this.tbxList.WordWrap = false;
            this.tbxList.TextChanged += new System.EventHandler(this.tbxList_TextChanged);
            // 
            // chbEnabled
            // 
            this.chbEnabled.AutoSize = true;
            this.chbEnabled.Location = new System.Drawing.Point(3, 3);
            this.chbEnabled.Name = "chbEnabled";
            this.chbEnabled.Size = new System.Drawing.Size(83, 17);
            this.chbEnabled.TabIndex = 4;
            this.chbEnabled.Text = "chbEnabled";
            this.chbEnabled.UseVisualStyleBackColor = true;
            this.chbEnabled.CheckedChanged += new System.EventHandler(this.chbEnabled_CheckedChanged);
            // 
            // StringInListObjectFilterItemFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chbEnabled);
            this.Controls.Add(this.tbxList);
            this.Controls.Add(this.tbxRegexVal);
            this.Controls.Add(this.rbtList);
            this.Controls.Add(this.rbtRegex);
            this.Name = "StringInListObjectFilterItemFrame";
            this.Size = new System.Drawing.Size(371, 157);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtRegex;
        private System.Windows.Forms.RadioButton rbtList;
        private System.Windows.Forms.TextBox tbxRegexVal;
        private System.Windows.Forms.TextBox tbxList;
        private System.Windows.Forms.CheckBox chbEnabled;
    }
}
