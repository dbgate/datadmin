namespace DatAdmin
{
    partial class ToolboxDocker
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
            this.components = new System.ComponentModel.Container();
            this.hostToolbox1 = new IP.Components.HostToolbox(false);
            this.SuspendLayout();
            // 
            // hostToolbox1
            // 
            this.hostToolbox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hostToolbox1.Location = new System.Drawing.Point(0, 0);
            this.hostToolbox1.Name = "hostToolbox1";
            this.hostToolbox1.SelectedCategory = "";
            this.hostToolbox1.ShowPointer = false;
            this.hostToolbox1.Size = new System.Drawing.Size(210, 212);
            this.hostToolbox1.TabIndex = 0;
            this.hostToolbox1.Text = "hostToolbox1";
            this.hostToolbox1.DoubleClick += new System.EventHandler(this.hostToolbox1_DoubleClick);
            // 
            // ToolboxDocker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hostToolbox1);
            this.Name = "ToolboxDocker";
            this.Size = new System.Drawing.Size(210, 212);
            this.ResumeLayout(false);

        }

        #endregion

        private IP.Components.HostToolbox hostToolbox1;
    }
}
