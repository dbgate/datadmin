namespace DatAdmin
{
    partial class TextDiffForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.diffControl1 = new Menees.DiffUtils.Controls.DiffControl();
            this.SuspendLayout();
            // 
            // diffControl1
            // 
            this.diffControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diffControl1.Location = new System.Drawing.Point(0, 0);
            this.diffControl1.Name = "diffControl1";
            this.diffControl1.ShowWhitespaceInLineDiff = true;
            this.diffControl1.Size = new System.Drawing.Size(655, 389);
            this.diffControl1.TabIndex = 0;
            this.diffControl1.ViewFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // TextDiffForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 389);
            this.Controls.Add(this.diffControl1);
            this.KeyPreview = true;
            this.Name = "TextDiffForm";
            this.Text = "s_diference";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextDiffForm_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private Menees.DiffUtils.Controls.DiffControl diffControl1;
    }
}