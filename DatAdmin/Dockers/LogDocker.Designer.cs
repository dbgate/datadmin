namespace DatAdmin
{
    partial class LogDocker
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
            this.messageLogFrame1 = new DatAdmin.MessageLogFrame();
            this.SuspendLayout();
            // 
            // messageLogFrame1
            // 
            this.messageLogFrame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageLogFrame1.Location = new System.Drawing.Point(0, 0);
            this.messageLogFrame1.Name = "messageLogFrame1";
            this.messageLogFrame1.Size = new System.Drawing.Size(284, 264);
            this.messageLogFrame1.Source = null;
            this.messageLogFrame1.Style = DatAdmin.MessageFrameStyle.List;
            this.messageLogFrame1.TabIndex = 1;
            // 
            // LogDocker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 264);
            this.Controls.Add(this.messageLogFrame1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "LogDocker";
            this.Text = "LogDocker";
            this.ResumeLayout(false);

        }

        #endregion

        private MessageLogFrame messageLogFrame1;
    }
}