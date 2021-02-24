namespace DatAdmin
{
    partial class SelectReferenceForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.rbtMaster = new System.Windows.Forms.RadioButton();
            this.tbxBaseTable = new System.Windows.Forms.TextBox();
            this.rbtDetail = new System.Windows.Forms.RadioButton();
            this.lsvDetailReferences = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lsvMasterReferences = new System.Windows.Forms.ListView();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "s_base_table";
            // 
            // rbtMaster
            // 
            this.rbtMaster.AutoSize = true;
            this.rbtMaster.Location = new System.Drawing.Point(25, 221);
            this.rbtMaster.Name = "rbtMaster";
            this.rbtMaster.Size = new System.Drawing.Size(67, 17);
            this.rbtMaster.TabIndex = 4;
            this.rbtMaster.Text = "s_master";
            this.rbtMaster.UseVisualStyleBackColor = true;
            // 
            // tbxBaseTable
            // 
            this.tbxBaseTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxBaseTable.Location = new System.Drawing.Point(52, 30);
            this.tbxBaseTable.Name = "tbxBaseTable";
            this.tbxBaseTable.ReadOnly = true;
            this.tbxBaseTable.Size = new System.Drawing.Size(479, 20);
            this.tbxBaseTable.TabIndex = 1;
            this.tbxBaseTable.TabStop = false;
            // 
            // rbtDetail
            // 
            this.rbtDetail.AutoSize = true;
            this.rbtDetail.Checked = true;
            this.rbtDetail.Location = new System.Drawing.Point(25, 55);
            this.rbtDetail.Name = "rbtDetail";
            this.rbtDetail.Size = new System.Drawing.Size(61, 17);
            this.rbtDetail.TabIndex = 2;
            this.rbtDetail.Text = "s_detail";
            this.rbtDetail.UseVisualStyleBackColor = true;
            // 
            // lsvDetailReferences
            // 
            this.lsvDetailReferences.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvDetailReferences.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lsvDetailReferences.FullRowSelect = true;
            this.lsvDetailReferences.Location = new System.Drawing.Point(52, 78);
            this.lsvDetailReferences.MultiSelect = false;
            this.lsvDetailReferences.Name = "lsvDetailReferences";
            this.lsvDetailReferences.Size = new System.Drawing.Size(479, 124);
            this.lsvDetailReferences.TabIndex = 3;
            this.lsvDetailReferences.UseCompatibleStateImageBehavior = false;
            this.lsvDetailReferences.View = System.Windows.Forms.View.Details;
            this.lsvDetailReferences.SelectedIndexChanged += new System.EventHandler(this.lsvDetailReferences_SelectedIndexChanged);
            this.lsvDetailReferences.DoubleClick += new System.EventHandler(this.lsvDetailReferences_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "s_table";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "s_foreign_key";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "s_columns";
            this.columnHeader3.Width = 150;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(375, 396);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.TabStop = false;
            this.btnOk.Text = "s_ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(456, 396);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "s_cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lsvMasterReferences
            // 
            this.lsvMasterReferences.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvMasterReferences.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lsvMasterReferences.FullRowSelect = true;
            this.lsvMasterReferences.Location = new System.Drawing.Point(52, 244);
            this.lsvMasterReferences.MultiSelect = false;
            this.lsvMasterReferences.Name = "lsvMasterReferences";
            this.lsvMasterReferences.Size = new System.Drawing.Size(479, 131);
            this.lsvMasterReferences.TabIndex = 5;
            this.lsvMasterReferences.UseCompatibleStateImageBehavior = false;
            this.lsvMasterReferences.View = System.Windows.Forms.View.Details;
            this.lsvMasterReferences.SelectedIndexChanged += new System.EventHandler(this.lsvMasterReferences_SelectedIndexChanged);
            this.lsvMasterReferences.DoubleClick += new System.EventHandler(this.lsvMasterReferences_DoubleClick);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "s_table";
            this.columnHeader4.Width = 150;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "s_foreign_key";
            this.columnHeader5.Width = 150;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "s_columns";
            this.columnHeader6.Width = 150;
            // 
            // SelectReferenceForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(572, 451);
            this.Controls.Add(this.lsvMasterReferences);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lsvDetailReferences);
            this.Controls.Add(this.rbtDetail);
            this.Controls.Add(this.tbxBaseTable);
            this.Controls.Add(this.rbtMaster);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SelectReferenceForm";
            this.Text = "s_select_reference";
            this.Shown += new System.EventHandler(this.SelectReferenceForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbtMaster;
        private System.Windows.Forms.TextBox tbxBaseTable;
        private System.Windows.Forms.RadioButton rbtDetail;
        private System.Windows.Forms.ListView lsvDetailReferences;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListView lsvMasterReferences;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
    }
}