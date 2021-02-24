namespace DatAdmin
{
    partial class ProcessListDocker
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnCancelProcess = new System.Windows.Forms.ToolStripButton();
            this.btnShowProcessWindow = new System.Windows.Forms.ToolStripButton();
            this.lswProcess = new System.Windows.Forms.ListView();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.colCurWork = new System.Windows.Forms.ColumnHeader();
            this.colEstimate = new System.Windows.Forms.ColumnHeader();
            this.colDuration = new System.Windows.Forms.ColumnHeader();
            this.colState = new System.Windows.Forms.ColumnHeader();
            this.colDescription = new System.Windows.Forms.ColumnHeader();
            this.colJob = new System.Windows.Forms.ColumnHeader();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefresh,
            this.btnCancelProcess,
            this.btnShowProcessWindow});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(350, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnCancelProcess
            // 
            this.btnCancelProcess.Image = global::DatAdmin.StdIcons.cancel;
            this.btnCancelProcess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelProcess.Name = "btnCancelProcess";
            this.btnCancelProcess.Size = new System.Drawing.Size(71, 22);
            this.btnCancelProcess.Text = "s_cancel";
            this.btnCancelProcess.Click += new System.EventHandler(this.btnCancelProcess_Click);
            // 
            // btnShowProcessWindow
            // 
            this.btnShowProcessWindow.Image = global::DatAdmin.StdIcons.alone_window;
            this.btnShowProcessWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowProcessWindow.Name = "btnShowProcessWindow";
            this.btnShowProcessWindow.Size = new System.Drawing.Size(112, 22);
            this.btnShowProcessWindow.Text = "s_show_window";
            this.btnShowProcessWindow.Click += new System.EventHandler(this.btnShowProcessWindow_Click);
            // 
            // lswProcess
            // 
            this.lswProcess.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colCurWork,
            this.colEstimate,
            this.colDuration,
            this.colState,
            this.colDescription,
            this.colJob});
            this.lswProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lswProcess.FullRowSelect = true;
            this.lswProcess.Location = new System.Drawing.Point(0, 25);
            this.lswProcess.Name = "lswProcess";
            this.lswProcess.Size = new System.Drawing.Size(350, 239);
            this.lswProcess.TabIndex = 3;
            this.lswProcess.UseCompatibleStateImageBehavior = false;
            this.lswProcess.View = System.Windows.Forms.View.Details;
            this.lswProcess.DoubleClick += new System.EventHandler(this.btnShowProcessWindow_Click);
            this.lswProcess.Click += new System.EventHandler(this.lswProcess_Click);
            // 
            // colName
            // 
            this.colName.Text = "s_name";
            this.colName.Width = 200;
            // 
            // colCurWork
            // 
            this.colCurWork.Text = "s_curwork";
            this.colCurWork.Width = 100;
            // 
            // colEstimate
            // 
            this.colEstimate.Text = "s_estimate";
            // 
            // colDuration
            // 
            this.colDuration.Text = "s_duration";
            // 
            // colState
            // 
            this.colState.Text = "s_state";
            // 
            // colDescription
            // 
            this.colDescription.Text = "s_description";
            this.colDescription.Width = 200;
            // 
            // colJob
            // 
            this.colJob.Text = "s_job";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::DatAdmin.StdIcons.refresh;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(73, 22);
            this.btnRefresh.Text = "s_refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // ProcessListDocker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lswProcess);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "ProcessListDocker";
            this.Size = new System.Drawing.Size(350, 264);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnCancelProcess;
        private System.Windows.Forms.ToolStripButton btnShowProcessWindow;
        private System.Windows.Forms.ListView lswProcess;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colCurWork;
        private System.Windows.Forms.ColumnHeader colEstimate;
        private System.Windows.Forms.ColumnHeader colDuration;
        private System.Windows.Forms.ColumnHeader colState;
        private System.Windows.Forms.ColumnHeader colDescription;
        private System.Windows.Forms.ColumnHeader colJob;
        private System.Windows.Forms.ToolStripButton btnRefresh;
    }
}