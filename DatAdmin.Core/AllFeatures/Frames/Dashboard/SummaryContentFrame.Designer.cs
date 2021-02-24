namespace DatAdmin
{
    partial class SummaryContentFrame
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
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStripDashboard = new System.Windows.Forms.ToolStrip();
            this.btnMore = new System.Windows.Forms.ToolStripButton();
            this.btnRevert = new System.Windows.Forms.ToolStripDropDownButton();
            this.labPathRoot = new System.Windows.Forms.LinkLabel();
            this.cbxDashboardType = new System.Windows.Forms.ComboBox();
            this.lbtitle = new System.Windows.Forms.Label();
            this.lbtype = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.panel1.SuspendLayout();
            this.toolStripDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.toolStripDashboard);
            this.panel1.Controls.Add(this.labPathRoot);
            this.panel1.Controls.Add(this.cbxDashboardType);
            this.panel1.Controls.Add(this.lbtitle);
            this.panel1.Controls.Add(this.lbtype);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(385, 55);
            this.panel1.TabIndex = 1;
            // 
            // toolStripDashboard
            // 
            this.toolStripDashboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStripDashboard.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripDashboard.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMore,
            this.btnRevert});
            this.toolStripDashboard.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStripDashboard.Location = new System.Drawing.Point(198, 27);
            this.toolStripDashboard.Name = "toolStripDashboard";
            this.toolStripDashboard.Size = new System.Drawing.Size(184, 25);
            this.toolStripDashboard.TabIndex = 6;
            this.toolStripDashboard.Resize += new System.EventHandler(this.toolStripDashboard_Resize);
            // 
            // btnMore
            // 
            this.btnMore.CheckOnClick = true;
            this.btnMore.Image = global::DatAdmin.StdIcons.more_down;
            this.btnMore.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMore.Name = "btnMore";
            this.btnMore.Size = new System.Drawing.Size(65, 22);
            this.btnMore.Text = "s_more";
            this.btnMore.Click += new System.EventHandler(this.btnMore_CheckedChanged);
            // 
            // btnRevert
            // 
            this.btnRevert.Image = global::DatAdmin.StdIcons.undo;
            this.btnRevert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRevert.Name = "btnRevert";
            this.btnRevert.Size = new System.Drawing.Size(76, 22);
            this.btnRevert.Text = "s_revert";
            // 
            // labPathRoot
            // 
            this.labPathRoot.AutoSize = true;
            this.labPathRoot.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labPathRoot.Location = new System.Drawing.Point(45, 33);
            this.labPathRoot.Name = "labPathRoot";
            this.labPathRoot.Size = new System.Drawing.Size(69, 16);
            this.labPathRoot.TabIndex = 7;
            this.labPathRoot.TabStop = true;
            this.labPathRoot.Text = "linkLabel1";
            // 
            // cbxDashboardType
            // 
            this.cbxDashboardType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxDashboardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDashboardType.FormattingEnabled = true;
            this.cbxDashboardType.Location = new System.Drawing.Point(208, 3);
            this.cbxDashboardType.Name = "cbxDashboardType";
            this.cbxDashboardType.Size = new System.Drawing.Size(174, 21);
            this.cbxDashboardType.TabIndex = 5;
            this.cbxDashboardType.SelectedIndexChanged += new System.EventHandler(this.cbxDashboardType_SelectedIndexChanged);
            // 
            // lbtitle
            // 
            this.lbtitle.AutoSize = true;
            this.lbtitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lbtitle.Location = new System.Drawing.Point(128, 9);
            this.lbtitle.Name = "lbtitle";
            this.lbtitle.Size = new System.Drawing.Size(46, 17);
            this.lbtitle.TabIndex = 3;
            this.lbtitle.Text = "label2";
            // 
            // lbtype
            // 
            this.lbtype.AutoSize = true;
            this.lbtype.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lbtype.Location = new System.Drawing.Point(45, 9);
            this.lbtype.Name = "lbtype";
            this.lbtype.Size = new System.Drawing.Size(52, 17);
            this.lbtype.TabIndex = 1;
            this.lbtype.Text = "label1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(14, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 26);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 55);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(385, 0);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // SummaryContentFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "SummaryContentFrame";
            this.Size = new System.Drawing.Size(385, 307);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStripDashboard.ResumeLayout(false);
            this.toolStripDashboard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbtype;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbtitle;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ComboBox cbxDashboardType;
        private System.Windows.Forms.ToolStrip toolStripDashboard;
        private System.Windows.Forms.ToolStripButton btnMore;
        private System.Windows.Forms.ToolStripDropDownButton btnRevert;
         private System.Windows.Forms.LinkLabel labPathRoot;
   }
}
