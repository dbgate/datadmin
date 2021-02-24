namespace DatAdmin
{
    partial class CreateDialog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateDialog));
            this.lbxGroups = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lbxItems = new System.Windows.Forms.ListView();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbxNewName = new System.Windows.Forms.TextBox();
            this.infoBoxFrame1 = new DatAdmin.InfoBoxFrame();
            this.tbxFileName = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.rbtNameInTree = new System.Windows.Forms.RadioButton();
            this.rbtFileOnDisk = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // lbxGroups
            // 
            this.lbxGroups.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lbxGroups.HideSelection = false;
            this.lbxGroups.LargeImageList = this.imageList1;
            this.lbxGroups.Location = new System.Drawing.Point(12, 12);
            this.lbxGroups.Name = "lbxGroups";
            this.lbxGroups.Size = new System.Drawing.Size(210, 352);
            this.lbxGroups.TabIndex = 0;
            this.lbxGroups.UseCompatibleStateImageBehavior = false;
            this.lbxGroups.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "bigfolder.bmp");
            // 
            // lbxItems
            // 
            this.lbxItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxItems.HideSelection = false;
            this.lbxItems.LargeImageList = this.imageList2;
            this.lbxItems.Location = new System.Drawing.Point(228, 12);
            this.lbxItems.Name = "lbxItems";
            this.lbxItems.Size = new System.Drawing.Size(458, 224);
            this.lbxItems.TabIndex = 1;
            this.lbxItems.UseCompatibleStateImageBehavior = false;
            this.lbxItems.SelectedIndexChanged += new System.EventHandler(this.lbxItems_SelectedIndexChanged);
            // 
            // imageList2
            // 
            this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList2.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(530, 376);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "s_ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(611, 376);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "s_cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tbxNewName
            // 
            this.tbxNewName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxNewName.Location = new System.Drawing.Point(476, 316);
            this.tbxNewName.Name = "tbxNewName";
            this.tbxNewName.Size = new System.Drawing.Size(210, 20);
            this.tbxNewName.TabIndex = 4;
            this.toolTip1.SetToolTip(this.tbxNewName, "s_choose_object_name_hlp");
            // 
            // infoBoxFrame1
            // 
            this.infoBoxFrame1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.infoBoxFrame1.InfoText = "";
            this.infoBoxFrame1.Location = new System.Drawing.Point(228, 242);
            this.infoBoxFrame1.Name = "infoBoxFrame1";
            this.infoBoxFrame1.Size = new System.Drawing.Size(458, 68);
            this.infoBoxFrame1.TabIndex = 2;
            this.infoBoxFrame1.TabStop = false;
            // 
            // tbxFileName
            // 
            this.tbxFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxFileName.Enabled = false;
            this.tbxFileName.Location = new System.Drawing.Point(476, 342);
            this.tbxFileName.Name = "tbxFileName";
            this.tbxFileName.Size = new System.Drawing.Size(173, 20);
            this.tbxFileName.TabIndex = 6;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(655, 342);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(30, 23);
            this.btnBrowse.TabIndex = 7;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // rbtNameInTree
            // 
            this.rbtNameInTree.AutoSize = true;
            this.rbtNameInTree.Checked = true;
            this.rbtNameInTree.Location = new System.Drawing.Point(292, 317);
            this.rbtNameInTree.Name = "rbtNameInTree";
            this.rbtNameInTree.Size = new System.Drawing.Size(159, 17);
            this.rbtNameInTree.TabIndex = 3;
            this.rbtNameInTree.TabStop = true;
            this.rbtNameInTree.Text = "s_name_in_connection_tree";
            this.toolTip1.SetToolTip(this.rbtNameInTree, "s_choose_object_name_hlp");
            this.rbtNameInTree.UseVisualStyleBackColor = true;
            this.rbtNameInTree.CheckedChanged += new System.EventHandler(this.cbxDestination_SelectedIndexChanged);
            // 
            // rbtFileOnDisk
            // 
            this.rbtFileOnDisk.AutoSize = true;
            this.rbtFileOnDisk.Location = new System.Drawing.Point(292, 343);
            this.rbtFileOnDisk.Name = "rbtFileOnDisk";
            this.rbtFileOnDisk.Size = new System.Drawing.Size(107, 17);
            this.rbtFileOnDisk.TabIndex = 5;
            this.rbtFileOnDisk.Text = "s_new_file_name";
            this.rbtFileOnDisk.UseVisualStyleBackColor = true;
            this.rbtFileOnDisk.CheckedChanged += new System.EventHandler(this.cbxDestination_SelectedIndexChanged);
            // 
            // CreateDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(698, 405);
            this.Controls.Add(this.rbtFileOnDisk);
            this.Controls.Add(this.rbtNameInTree);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tbxFileName);
            this.Controls.Add(this.infoBoxFrame1);
            this.Controls.Add(this.tbxNewName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lbxItems);
            this.Controls.Add(this.lbxGroups);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreateDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "s_create_new";
            this.Load += new System.EventHandler(this.CreateDialog_Load);
            this.Shown += new System.EventHandler(this.CreateDialog_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lbxGroups;
        private System.Windows.Forms.ListView lbxItems;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.TextBox tbxNewName;
        private InfoBoxFrame infoBoxFrame1;
        private System.Windows.Forms.TextBox tbxFileName;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.RadioButton rbtNameInTree;
        private System.Windows.Forms.RadioButton rbtFileOnDisk;
    }
}
