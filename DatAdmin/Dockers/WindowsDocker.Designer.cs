namespace DatAdmin
{
    partial class WindowsDocker
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.btnCloseAll = new System.Windows.Forms.ToolStripButton();
            this.lsvWindows = new System.Windows.Forms.ListView();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.colType = new System.Windows.Forms.ColumnHeader();
            this.colNumber = new System.Windows.Forms.ColumnHeader();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose,
            this.btnCloseAll});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(346, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnClose
            // 
            this.btnClose.Image = global::DatAdmin.Icons.close;
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(64, 22);
            this.btnClose.Text = "s_close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCloseAll
            // 
            this.btnCloseAll.Image = global::DatAdmin.Icons.closeall;
            this.btnCloseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCloseAll.Name = "btnCloseAll";
            this.btnCloseAll.Size = new System.Drawing.Size(81, 22);
            this.btnCloseAll.Text = "s_close_all";
            this.btnCloseAll.Click += new System.EventHandler(this.btnCloseAll_Click);
            // 
            // lsvWindows
            // 
            this.lsvWindows.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colType,
            this.colNumber});
            this.lsvWindows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvWindows.FullRowSelect = true;
            this.lsvWindows.HideSelection = false;
            this.lsvWindows.Location = new System.Drawing.Point(0, 25);
            this.lsvWindows.Name = "lsvWindows";
            this.lsvWindows.Size = new System.Drawing.Size(346, 246);
            this.lsvWindows.SmallImageList = this.imageList1;
            this.lsvWindows.TabIndex = 1;
            this.lsvWindows.UseCompatibleStateImageBehavior = false;
            this.lsvWindows.View = System.Windows.Forms.View.Details;
            this.lsvWindows.SelectedIndexChanged += new System.EventHandler(this.lsvWindows_SelectedIndexChanged);
            // 
            // colName
            // 
            this.colName.Text = "s_name";
            this.colName.Width = 120;
            // 
            // colType
            // 
            this.colType.Text = "s_type";
            this.colType.Width = 80;
            // 
            // colNumber
            // 
            this.colNumber.Text = "s_number";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // WindowsDocker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lsvWindows);
            this.Controls.Add(this.toolStrip1);
            this.Name = "WindowsDocker";
            this.Size = new System.Drawing.Size(346, 271);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnClose;
        private System.Windows.Forms.ToolStripButton btnCloseAll;
        private System.Windows.Forms.ListView lsvWindows;
        private System.Windows.Forms.ColumnHeader colNumber;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ImageList imageList1;
    }
}
