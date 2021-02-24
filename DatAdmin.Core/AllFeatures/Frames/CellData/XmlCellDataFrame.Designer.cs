namespace DatAdmin
{
    partial class XmlCellDataFrame
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
            this.xPathNavigatorTreeView1 = new DatAdmin.XPathNavigatorTreeView();
            this.SuspendLayout();
            // 
            // xPathNavigatorTreeView1
            // 
            this.xPathNavigatorTreeView1.AutoExpand = true;
            this.xPathNavigatorTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xPathNavigatorTreeView1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.xPathNavigatorTreeView1.ForceHideRoot = false;
            this.xPathNavigatorTreeView1.HideSelection = false;
            this.xPathNavigatorTreeView1.Location = new System.Drawing.Point(0, 0);
            this.xPathNavigatorTreeView1.Name = "xPathNavigatorTreeView1";
            this.xPathNavigatorTreeView1.Navigator = null;
            this.xPathNavigatorTreeView1.Size = new System.Drawing.Size(402, 329);
            this.xPathNavigatorTreeView1.TabIndex = 1;
            // 
            // XmlCellDataFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xPathNavigatorTreeView1);
            this.Name = "XmlCellDataFrame";
            this.Size = new System.Drawing.Size(402, 329);
            this.ResumeLayout(false);

        }

        #endregion

        private XPathNavigatorTreeView xPathNavigatorTreeView1;
    }
}
