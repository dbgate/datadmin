namespace DatAdmin
{
    partial class MessageLogFrame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageLogFrame));
            this.lvMessages = new System.Windows.Forms.ListView();
            this.colSeverity = new System.Windows.Forms.ColumnHeader();
            this.colCategory = new System.Windows.Forms.ColumnHeader();
            this.colMessage = new System.Windows.Forms.ColumnHeader();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cbxStyle = new System.Windows.Forms.ToolStripComboBox();
            this.codeEditor1 = new DatAdmin.CodeEditor();
            this.btnCopyToClipboard = new System.Windows.Forms.ToolStripButton();
            this.btnFind = new System.Windows.Forms.ToolStripButton();
            this.btnShowDetail = new System.Windows.Forms.ToolStripButton();
            this.btnAutoScroll = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvMessages
            // 
            this.lvMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSeverity,
            this.colCategory,
            this.colMessage});
            this.lvMessages.FullRowSelect = true;
            this.lvMessages.Location = new System.Drawing.Point(66, 105);
            this.lvMessages.Name = "lvMessages";
            this.lvMessages.Size = new System.Drawing.Size(356, 156);
            this.lvMessages.SmallImageList = this.imageList1;
            this.lvMessages.TabIndex = 0;
            this.lvMessages.UseCompatibleStateImageBehavior = false;
            this.lvMessages.View = System.Windows.Forms.View.Details;
            this.lvMessages.DoubleClick += new System.EventHandler(this.lvMessages_DoubleClick);
            // 
            // colSeverity
            // 
            this.colSeverity.Text = "s_severity";
            this.colSeverity.Width = 100;
            // 
            // colCategory
            // 
            this.colCategory.Text = "s_category";
            this.colCategory.Width = 100;
            // 
            // colMessage
            // 
            this.colMessage.Text = "s_message";
            this.colMessage.Width = 300;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "info.png");
            this.imageList1.Images.SetKeyName(1, "warning.png");
            this.imageList1.Images.SetKeyName(2, "error.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCopyToClipboard,
            this.btnFind,
            this.cbxStyle,
            this.btnShowDetail,
            this.btnAutoScroll});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(605, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cbxStyle
            // 
            this.cbxStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStyle.Name = "cbxStyle";
            this.cbxStyle.Size = new System.Drawing.Size(121, 25);
            this.cbxStyle.SelectedIndexChanged += new System.EventHandler(this.cbxStyle_SelectedIndexChanged);
            // 
            // codeEditor1
            // 
            this.codeEditor1.Dialect = null;
            this.codeEditor1.IsReadOnly = true;
            this.codeEditor1.Language = DatAdmin.CodeLanguage.None;
            this.codeEditor1.Location = new System.Drawing.Point(20, 41);
            this.codeEditor1.Modified = true;
            this.codeEditor1.Name = "codeEditor1";
            this.codeEditor1.ReadOnly = true;
            this.codeEditor1.Size = new System.Drawing.Size(361, 158);
            this.codeEditor1.TabIndex = 2;
            this.codeEditor1.Text = "codeEditor1";
            // 
            // btnCopyToClipboard
            // 
            this.btnCopyToClipboard.Image = global::DatAdmin.StdIcons.copy;
            this.btnCopyToClipboard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopyToClipboard.Name = "btnCopyToClipboard";
            this.btnCopyToClipboard.Size = new System.Drawing.Size(134, 22);
            this.btnCopyToClipboard.Text = "s_copy_to_clipboard";
            this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
            // 
            // btnFind
            // 
            this.btnFind.Image = global::DatAdmin.StdIcons.find;
            this.btnFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(71, 22);
            this.btnFind.Text = "s_search";
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnShowDetail
            // 
            this.btnShowDetail.Image = global::DatAdmin.StdIcons.properties;
            this.btnShowDetail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowDetail.Name = "btnShowDetail";
            this.btnShowDetail.Size = new System.Drawing.Size(99, 22);
            this.btnShowDetail.Text = "s_show_detail";
            this.btnShowDetail.Click += new System.EventHandler(this.btnShowDetail_Click);
            // 
            // btnAutoScroll
            // 
            this.btnAutoScroll.Checked = true;
            this.btnAutoScroll.CheckOnClick = true;
            this.btnAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnAutoScroll.Image = global::DatAdmin.StdIcons.autoscroll;
            this.btnAutoScroll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAutoScroll.Name = "btnAutoScroll";
            this.btnAutoScroll.Size = new System.Drawing.Size(94, 22);
            this.btnAutoScroll.Text = "s_auto_scroll";
            // 
            // MessageLogFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvMessages);
            this.Controls.Add(this.codeEditor1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MessageLogFrame";
            this.Size = new System.Drawing.Size(605, 277);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvMessages;
        private System.Windows.Forms.ColumnHeader colCategory;
        private System.Windows.Forms.ColumnHeader colMessage;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader colSeverity;
        private System.Windows.Forms.ToolStripButton btnCopyToClipboard;
        private System.Windows.Forms.ToolStripComboBox cbxStyle;
        private CodeEditor codeEditor1;
        private System.Windows.Forms.ToolStripButton btnFind;
        private System.Windows.Forms.ToolStripButton btnShowDetail;
        private System.Windows.Forms.ToolStripButton btnAutoScroll;
    }
}
