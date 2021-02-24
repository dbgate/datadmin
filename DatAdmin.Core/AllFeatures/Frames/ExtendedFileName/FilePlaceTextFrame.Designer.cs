namespace DatAdmin
{
    partial class FilePlaceTextFrame
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
            this.tbxMemo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbxMemo
            // 
            this.tbxMemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxMemo.Location = new System.Drawing.Point(0, 0);
            this.tbxMemo.Multiline = true;
            this.tbxMemo.Name = "tbxMemo";
            this.tbxMemo.Size = new System.Drawing.Size(397, 212);
            this.tbxMemo.TabIndex = 0;
            this.tbxMemo.TextChanged += new System.EventHandler(this.tbxMemo_TextChanged);
            // 
            // FilePlaceTextFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbxMemo);
            this.Name = "FilePlaceTextFrame";
            this.Size = new System.Drawing.Size(397, 212);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxMemo;
    }
}
