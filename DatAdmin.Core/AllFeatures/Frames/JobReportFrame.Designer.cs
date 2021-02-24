namespace DatAdmin
{
    partial class JobReportFrame
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
            this.lbxAvailableReports = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbxUsedReports = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.propertyFrame1 = new DatAdmin.PropertyFrame();
            this.btnAddReport = new System.Windows.Forms.Button();
            this.btnRemoveReport = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbxAvailableReports
            // 
            this.lbxAvailableReports.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lbxAvailableReports.FormattingEnabled = true;
            this.lbxAvailableReports.Location = new System.Drawing.Point(3, 31);
            this.lbxAvailableReports.Name = "lbxAvailableReports";
            this.lbxAvailableReports.Size = new System.Drawing.Size(120, 355);
            this.lbxAvailableReports.TabIndex = 0;
            this.lbxAvailableReports.DoubleClick += new System.EventHandler(this.btnAddReport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "s_available_reports";
            // 
            // lbxUsedReports
            // 
            this.lbxUsedReports.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lbxUsedReports.FormattingEnabled = true;
            this.lbxUsedReports.Location = new System.Drawing.Point(200, 31);
            this.lbxUsedReports.Name = "lbxUsedReports";
            this.lbxUsedReports.Size = new System.Drawing.Size(120, 355);
            this.lbxUsedReports.TabIndex = 2;
            this.lbxUsedReports.SelectedIndexChanged += new System.EventHandler(this.lbxUsedReports_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(211, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "s_used_reports";
            // 
            // propertyFrame1
            // 
            this.propertyFrame1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyFrame1.Location = new System.Drawing.Point(326, 31);
            this.propertyFrame1.Name = "propertyFrame1";
            this.propertyFrame1.SelectedObject = null;
            this.propertyFrame1.Size = new System.Drawing.Size(342, 326);
            this.propertyFrame1.TabIndex = 4;
            // 
            // btnAddReport
            // 
            this.btnAddReport.Location = new System.Drawing.Point(129, 31);
            this.btnAddReport.Name = "btnAddReport";
            this.btnAddReport.Size = new System.Drawing.Size(65, 23);
            this.btnAddReport.TabIndex = 5;
            this.btnAddReport.Text = ">";
            this.btnAddReport.UseVisualStyleBackColor = true;
            this.btnAddReport.Click += new System.EventHandler(this.btnAddReport_Click);
            // 
            // btnRemoveReport
            // 
            this.btnRemoveReport.Location = new System.Drawing.Point(129, 60);
            this.btnRemoveReport.Name = "btnRemoveReport";
            this.btnRemoveReport.Size = new System.Drawing.Size(65, 23);
            this.btnRemoveReport.TabIndex = 6;
            this.btnRemoveReport.Text = "<";
            this.btnRemoveReport.UseVisualStyleBackColor = true;
            this.btnRemoveReport.Click += new System.EventHandler(this.btnRemoveReport_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheck.Location = new System.Drawing.Point(593, 363);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(75, 23);
            this.btnCheck.TabIndex = 7;
            this.btnCheck.Text = "s_check";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // JobReportFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.btnRemoveReport);
            this.Controls.Add(this.btnAddReport);
            this.Controls.Add(this.propertyFrame1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbxUsedReports);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbxAvailableReports);
            this.Name = "JobReportFrame";
            this.Size = new System.Drawing.Size(671, 397);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbxAvailableReports;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbxUsedReports;
        private System.Windows.Forms.Label label2;
        private DatAdmin.PropertyFrame propertyFrame1;
        private System.Windows.Forms.Button btnAddReport;
        private System.Windows.Forms.Button btnRemoveReport;
        private System.Windows.Forms.Button btnCheck;
    }
}
