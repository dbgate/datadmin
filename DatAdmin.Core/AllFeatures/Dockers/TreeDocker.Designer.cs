namespace DatAdmin
{
    partial class TreeDocker
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
            DatAdmin.TreeBehaviour treeBehaviour1 = new DatAdmin.TreeBehaviour();
            this.daTreeView1 = new DatAdmin.DATreeView();
            this.SuspendLayout();
            // 
            // daTreeView1
            // 
            this.daTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.daTreeView1.Location = new System.Drawing.Point(0, 0);
            this.daTreeView1.Name = "daTreeView1";
            this.daTreeView1.Root = null;
            this.daTreeView1.RootPath = null;
            this.daTreeView1.Size = new System.Drawing.Size(284, 264);
            this.daTreeView1.TabIndex = 0;
            this.daTreeView1.TreeBehaviour = treeBehaviour1;
            this.daTreeView1.TreeStyle = DatAdmin.TreeStyle.TreeView;
            this.daTreeView1.ActiveNodeChange += new System.EventHandler(this.daTreeView1_ActiveNodeChange);
            // 
            // TreeDocker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.daTreeView1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "TreeDocker";
            this.Size = new System.Drawing.Size(284, 264);
            this.ResumeLayout(false);

        }

        #endregion

        private DATreeView daTreeView1;


    }
}