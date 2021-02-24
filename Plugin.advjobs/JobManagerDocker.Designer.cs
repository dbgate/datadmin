namespace Plugin.advjobs
{
    partial class JobManagerDocker
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
            this.splitContainerEx1 = new DatAdmin.SplitContainerEx();
            this.lsvJobs = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnNewJob = new System.Windows.Forms.ToolStripButton();
            this.btnRefreshJobs = new System.Windows.Forms.ToolStripButton();
            this.btnRunJob = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuSchedule = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeleteSchedule = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuManageSchedule = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDeleteJob = new System.Windows.Forms.ToolStripButton();
            this.btnOpenJobInTree = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAddToFavorites = new System.Windows.Forms.ToolStripButton();
            this.btnShellScriptJob = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuSaveJobShellToFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCopyJobSchellToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.tbnReport = new System.Windows.Forms.ToolStripButton();
            this.lsvCommands = new System.Windows.Forms.ListView();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefreshCommands = new System.Windows.Forms.ToolStripButton();
            this.btnRunCommand = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteCommand = new System.Windows.Forms.ToolStripButton();
            this.btnOpenCommandInTree = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDuplicateCommand = new System.Windows.Forms.ToolStripButton();
            this.btnEditCommand = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnMoveCommandUp = new System.Windows.Forms.ToolStripButton();
            this.btnMoveCommandDown = new System.Windows.Forms.ToolStripButton();
            this.btnShellScriptCommand = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnSaveCommandShellToFile = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCopyCommandShellToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.splitContainerEx1.Panel1.SuspendLayout();
            this.splitContainerEx1.Panel2.SuspendLayout();
            this.splitContainerEx1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerEx1
            // 
            this.splitContainerEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerEx1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerEx1.MoreFixedPanel = DatAdmin.SplitContainerEx.PanelType.First;
            this.splitContainerEx1.Name = "splitContainerEx1";
            this.splitContainerEx1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerEx1.Panel1
            // 
            this.splitContainerEx1.Panel1.Controls.Add(this.lsvJobs);
            this.splitContainerEx1.Panel1.Controls.Add(this.toolStrip1);
            this.splitContainerEx1.Panel1DynamicCollapsed = false;
            this.splitContainerEx1.Panel1MinDynamicSize = 0;
            // 
            // splitContainerEx1.Panel2
            // 
            this.splitContainerEx1.Panel2.Controls.Add(this.lsvCommands);
            this.splitContainerEx1.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainerEx1.Panel2DynamicCollapsed = false;
            this.splitContainerEx1.Panel2MinDynamicSize = 0;
            this.splitContainerEx1.Size = new System.Drawing.Size(661, 451);
            this.splitContainerEx1.SplitterDistance = 220;
            this.splitContainerEx1.SplitterWidth = 6;
            this.splitContainerEx1.TabIndex = 0;
            // 
            // lsvJobs
            // 
            this.lsvJobs.AllowDrop = true;
            this.lsvJobs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader1});
            this.lsvJobs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvJobs.FullRowSelect = true;
            this.lsvJobs.HideSelection = false;
            this.lsvJobs.Location = new System.Drawing.Point(0, 25);
            this.lsvJobs.Name = "lsvJobs";
            this.lsvJobs.Size = new System.Drawing.Size(661, 195);
            this.lsvJobs.SmallImageList = this.imageList1;
            this.lsvJobs.TabIndex = 1;
            this.lsvJobs.UseCompatibleStateImageBehavior = false;
            this.lsvJobs.View = System.Windows.Forms.View.Details;
            this.lsvJobs.SelectedIndexChanged += new System.EventHandler(this.lsbJobs_SelectedIndexChanged);
            this.lsvJobs.DoubleClick += new System.EventHandler(this.btnRunJob_Click);
            this.lsvJobs.DragDrop += new System.Windows.Forms.DragEventHandler(this.lsvJobs_DragDrop);
            this.lsvJobs.DragOver += new System.Windows.Forms.DragEventHandler(this.lsvJobs_DragOver);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "s_job_file";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "s_commands";
            this.columnHeader3.Width = 100;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "s_report";
            this.columnHeader1.Width = 100;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripSeparator1,
            this.btnNewJob,
            this.btnRefreshJobs,
            this.btnRunJob,
            this.toolStripDropDownButton1,
            this.btnDeleteJob,
            this.btnOpenJobInTree,
            this.toolStripSeparator3,
            this.btnAddToFavorites,
            this.btnShellScriptJob,
            this.tbnReport});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(661, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(39, 22);
            this.toolStripLabel1.Text = "s_jobs";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnNewJob
            // 
            this.btnNewJob.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewJob.Image = global::Plugin.advjobs.StdIcons._new;
            this.btnNewJob.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewJob.Name = "btnNewJob";
            this.btnNewJob.Size = new System.Drawing.Size(23, 22);
            this.btnNewJob.Text = "s_new";
            this.btnNewJob.Click += new System.EventHandler(this.btnNewJob_Click);
            // 
            // btnRefreshJobs
            // 
            this.btnRefreshJobs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefreshJobs.Image = global::Plugin.advjobs.StdIcons.refresh;
            this.btnRefreshJobs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefreshJobs.Name = "btnRefreshJobs";
            this.btnRefreshJobs.Size = new System.Drawing.Size(23, 22);
            this.btnRefreshJobs.Text = "s_refresh";
            this.btnRefreshJobs.Click += new System.EventHandler(this.btnRefreshJobs_Click);
            // 
            // btnRunJob
            // 
            this.btnRunJob.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRunJob.Image = global::Plugin.advjobs.StdIcons.run;
            this.btnRunJob.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRunJob.Name = "btnRunJob";
            this.btnRunJob.Size = new System.Drawing.Size(23, 22);
            this.btnRunJob.Text = "s_run";
            this.btnRunJob.Click += new System.EventHandler(this.btnRunJob_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSchedule,
            this.mnuDeleteSchedule,
            this.mnuManageSchedule});
            this.toolStripDropDownButton1.Image = global::Plugin.advjobs.StdIcons.clock;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // mnuSchedule
            // 
            this.mnuSchedule.Name = "mnuSchedule";
            this.mnuSchedule.Size = new System.Drawing.Size(168, 22);
            this.mnuSchedule.Text = "s_schedule";
            this.mnuSchedule.Click += new System.EventHandler(this.mnuSchedule_Click);
            // 
            // mnuDeleteSchedule
            // 
            this.mnuDeleteSchedule.Name = "mnuDeleteSchedule";
            this.mnuDeleteSchedule.Size = new System.Drawing.Size(168, 22);
            this.mnuDeleteSchedule.Text = "s_delete_schedule";
            this.mnuDeleteSchedule.Click += new System.EventHandler(this.mnuDeleteSchedule_Click);
            // 
            // mnuManageSchedule
            // 
            this.mnuManageSchedule.Name = "mnuManageSchedule";
            this.mnuManageSchedule.Size = new System.Drawing.Size(168, 22);
            this.mnuManageSchedule.Text = "s_manage";
            this.mnuManageSchedule.Click += new System.EventHandler(this.mnuManageSchedule_Click);
            // 
            // btnDeleteJob
            // 
            this.btnDeleteJob.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeleteJob.Image = global::Plugin.advjobs.StdIcons.delete;
            this.btnDeleteJob.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteJob.Name = "btnDeleteJob";
            this.btnDeleteJob.Size = new System.Drawing.Size(23, 22);
            this.btnDeleteJob.Text = "s_delete";
            this.btnDeleteJob.Click += new System.EventHandler(this.btnDeleteJob_Click);
            // 
            // btnOpenJobInTree
            // 
            this.btnOpenJobInTree.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpenJobInTree.Image = global::Plugin.advjobs.StdIcons.tree;
            this.btnOpenJobInTree.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenJobInTree.Name = "btnOpenJobInTree";
            this.btnOpenJobInTree.Size = new System.Drawing.Size(23, 22);
            this.btnOpenJobInTree.Text = "s_open_in_tree";
            this.btnOpenJobInTree.Visible = false;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAddToFavorites
            // 
            this.btnAddToFavorites.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddToFavorites.Image = global::Plugin.advjobs.StdIcons.favorite_add;
            this.btnAddToFavorites.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddToFavorites.Name = "btnAddToFavorites";
            this.btnAddToFavorites.Size = new System.Drawing.Size(23, 22);
            this.btnAddToFavorites.Text = "s_add_to_favorites";
            this.btnAddToFavorites.Click += new System.EventHandler(this.btnAddToFavorites_Click);
            // 
            // btnShellScriptJob
            // 
            this.btnShellScriptJob.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShellScriptJob.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSaveJobShellToFile,
            this.mnuCopyJobSchellToClipboard});
            this.btnShellScriptJob.Image = global::Plugin.advjobs.StdIcons.commandprompt;
            this.btnShellScriptJob.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShellScriptJob.Name = "btnShellScriptJob";
            this.btnShellScriptJob.Size = new System.Drawing.Size(29, 22);
            this.btnShellScriptJob.Text = "s_shell_script";
            // 
            // mnuSaveJobShellToFile
            // 
            this.mnuSaveJobShellToFile.Name = "mnuSaveJobShellToFile";
            this.mnuSaveJobShellToFile.Size = new System.Drawing.Size(181, 22);
            this.mnuSaveJobShellToFile.Text = "s_save_to_file";
            this.mnuSaveJobShellToFile.Click += new System.EventHandler(this.mnuSaveJobShellToFile_Click);
            // 
            // mnuCopyJobSchellToClipboard
            // 
            this.mnuCopyJobSchellToClipboard.Name = "mnuCopyJobSchellToClipboard";
            this.mnuCopyJobSchellToClipboard.Size = new System.Drawing.Size(181, 22);
            this.mnuCopyJobSchellToClipboard.Text = "s_copy_to_clipboard";
            this.mnuCopyJobSchellToClipboard.Click += new System.EventHandler(this.mnuCopyJobSchellToClipboard_Click);
            // 
            // tbnReport
            // 
            this.tbnReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbnReport.Image = global::Plugin.advjobs.StdIcons.report;
            this.tbnReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbnReport.Name = "tbnReport";
            this.tbnReport.Size = new System.Drawing.Size(23, 22);
            this.tbnReport.Text = "s_report";
            this.tbnReport.Click += new System.EventHandler(this.tbnReport_Click);
            // 
            // lsvCommands
            // 
            this.lsvCommands.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
            this.lsvCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvCommands.FullRowSelect = true;
            this.lsvCommands.HideSelection = false;
            this.lsvCommands.Location = new System.Drawing.Point(0, 25);
            this.lsvCommands.Name = "lsvCommands";
            this.lsvCommands.Size = new System.Drawing.Size(661, 200);
            this.lsvCommands.SmallImageList = this.imageList1;
            this.lsvCommands.TabIndex = 1;
            this.lsvCommands.UseCompatibleStateImageBehavior = false;
            this.lsvCommands.View = System.Windows.Forms.View.Details;
            this.lsvCommands.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lsvCommands_MouseDown);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "s_type";
            this.columnHeader5.Width = 100;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "s_summary";
            this.columnHeader6.Width = 300;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.toolStripSeparator2,
            this.btnRefreshCommands,
            this.btnRunCommand,
            this.btnDeleteCommand,
            this.btnOpenCommandInTree,
            this.toolStripSeparator4,
            this.btnDuplicateCommand,
            this.btnEditCommand,
            this.toolStripSeparator5,
            this.btnMoveCommandUp,
            this.btnMoveCommandDown,
            this.btnShellScriptCommand});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(661, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(114, 22);
            this.toolStripLabel2.Text = "s_commands_in_job";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnRefreshCommands
            // 
            this.btnRefreshCommands.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefreshCommands.Image = global::Plugin.advjobs.StdIcons.refresh;
            this.btnRefreshCommands.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefreshCommands.Name = "btnRefreshCommands";
            this.btnRefreshCommands.Size = new System.Drawing.Size(23, 22);
            this.btnRefreshCommands.Text = "s_refresh";
            this.btnRefreshCommands.Click += new System.EventHandler(this.btnRefreshCommands_Click);
            // 
            // btnRunCommand
            // 
            this.btnRunCommand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRunCommand.Image = global::Plugin.advjobs.StdIcons.run;
            this.btnRunCommand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRunCommand.Name = "btnRunCommand";
            this.btnRunCommand.Size = new System.Drawing.Size(23, 22);
            this.btnRunCommand.Text = "s_run";
            this.btnRunCommand.Click += new System.EventHandler(this.btnRunCommand_Click);
            // 
            // btnDeleteCommand
            // 
            this.btnDeleteCommand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeleteCommand.Image = global::Plugin.advjobs.StdIcons.delete;
            this.btnDeleteCommand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteCommand.Name = "btnDeleteCommand";
            this.btnDeleteCommand.Size = new System.Drawing.Size(23, 22);
            this.btnDeleteCommand.Text = "s_delete";
            this.btnDeleteCommand.Click += new System.EventHandler(this.btnDeleteCommand_Click);
            // 
            // btnOpenCommandInTree
            // 
            this.btnOpenCommandInTree.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpenCommandInTree.Image = global::Plugin.advjobs.StdIcons.tree;
            this.btnOpenCommandInTree.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenCommandInTree.Name = "btnOpenCommandInTree";
            this.btnOpenCommandInTree.Size = new System.Drawing.Size(23, 22);
            this.btnOpenCommandInTree.Text = "s_open_in_tree";
            this.btnOpenCommandInTree.Visible = false;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnDuplicateCommand
            // 
            this.btnDuplicateCommand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDuplicateCommand.Image = global::Plugin.advjobs.StdIcons.duplicate;
            this.btnDuplicateCommand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDuplicateCommand.Name = "btnDuplicateCommand";
            this.btnDuplicateCommand.Size = new System.Drawing.Size(23, 22);
            this.btnDuplicateCommand.Text = "s_duplicate";
            this.btnDuplicateCommand.Click += new System.EventHandler(this.btnDuplicateCommand_Click);
            // 
            // btnEditCommand
            // 
            this.btnEditCommand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditCommand.Image = global::Plugin.advjobs.StdIcons.design;
            this.btnEditCommand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditCommand.Name = "btnEditCommand";
            this.btnEditCommand.Size = new System.Drawing.Size(23, 22);
            this.btnEditCommand.Text = "s_edit";
            this.btnEditCommand.Click += new System.EventHandler(this.btnEditCommand_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // btnMoveCommandUp
            // 
            this.btnMoveCommandUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveCommandUp.Image = global::Plugin.advjobs.StdIcons.up1;
            this.btnMoveCommandUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveCommandUp.Name = "btnMoveCommandUp";
            this.btnMoveCommandUp.Size = new System.Drawing.Size(23, 22);
            this.btnMoveCommandUp.Text = "s_move_up";
            this.btnMoveCommandUp.Click += new System.EventHandler(this.btnMoveCommandUp_Click);
            // 
            // btnMoveCommandDown
            // 
            this.btnMoveCommandDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveCommandDown.Image = global::Plugin.advjobs.StdIcons.down1;
            this.btnMoveCommandDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveCommandDown.Name = "btnMoveCommandDown";
            this.btnMoveCommandDown.Size = new System.Drawing.Size(23, 22);
            this.btnMoveCommandDown.Text = "s_move_down";
            this.btnMoveCommandDown.Click += new System.EventHandler(this.btnMoveCommandDown_Click);
            // 
            // btnShellScriptCommand
            // 
            this.btnShellScriptCommand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShellScriptCommand.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSaveCommandShellToFile,
            this.btnCopyCommandShellToClipboard});
            this.btnShellScriptCommand.Image = global::Plugin.advjobs.StdIcons.commandprompt;
            this.btnShellScriptCommand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShellScriptCommand.Name = "btnShellScriptCommand";
            this.btnShellScriptCommand.Size = new System.Drawing.Size(29, 22);
            this.btnShellScriptCommand.Text = "s_shell_script";
            // 
            // btnSaveCommandShellToFile
            // 
            this.btnSaveCommandShellToFile.Name = "btnSaveCommandShellToFile";
            this.btnSaveCommandShellToFile.Size = new System.Drawing.Size(181, 22);
            this.btnSaveCommandShellToFile.Text = "s_save_to_file";
            this.btnSaveCommandShellToFile.Click += new System.EventHandler(this.btnSaveCommandShellToFile_Click);
            // 
            // btnCopyCommandShellToClipboard
            // 
            this.btnCopyCommandShellToClipboard.Name = "btnCopyCommandShellToClipboard";
            this.btnCopyCommandShellToClipboard.Size = new System.Drawing.Size(181, 22);
            this.btnCopyCommandShellToClipboard.Text = "s_copy_to_clipboard";
            this.btnCopyCommandShellToClipboard.Click += new System.EventHandler(this.btnCopyCommandShellToClipboard_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "{s_shell_scripts} (*.cmd)|*.cmd";
            // 
            // JobManagerDocker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerEx1);
            this.Name = "JobManagerDocker";
            this.Size = new System.Drawing.Size(661, 451);
            this.Resize += new System.EventHandler(this.JobManagerDocker_Resize);
            this.splitContainerEx1.Panel1.ResumeLayout(false);
            this.splitContainerEx1.Panel1.PerformLayout();
            this.splitContainerEx1.Panel2.ResumeLayout(false);
            this.splitContainerEx1.Panel2.PerformLayout();
            this.splitContainerEx1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DatAdmin.SplitContainerEx splitContainerEx1;
        private System.Windows.Forms.ListView lsvJobs;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnRunJob;
        private System.Windows.Forms.ToolStripButton btnDeleteJob;
        private System.Windows.Forms.ToolStripButton btnOpenJobInTree;
        private System.Windows.Forms.ListView lsvCommands;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnRunCommand;
        private System.Windows.Forms.ToolStripButton btnDeleteCommand;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ToolStripButton btnOpenCommandInTree;
        private System.Windows.Forms.ToolStripButton btnEditCommand;
        private System.Windows.Forms.ToolStripButton btnAddToFavorites;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnRefreshJobs;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnRefreshCommands;
        private System.Windows.Forms.ToolStripButton btnMoveCommandUp;
        private System.Windows.Forms.ToolStripButton btnMoveCommandDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton btnDuplicateCommand;
        private System.Windows.Forms.ToolStripButton btnNewJob;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem mnuSchedule;
        private System.Windows.Forms.ToolStripMenuItem mnuDeleteSchedule;
        private System.Windows.Forms.ToolStripMenuItem mnuManageSchedule;
        private System.Windows.Forms.ToolStripDropDownButton btnShellScriptJob;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveJobShellToFile;
        private System.Windows.Forms.ToolStripMenuItem mnuCopyJobSchellToClipboard;
        private System.Windows.Forms.ToolStripDropDownButton btnShellScriptCommand;
        private System.Windows.Forms.ToolStripMenuItem btnSaveCommandShellToFile;
        private System.Windows.Forms.ToolStripMenuItem btnCopyCommandShellToClipboard;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripButton tbnReport;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}
