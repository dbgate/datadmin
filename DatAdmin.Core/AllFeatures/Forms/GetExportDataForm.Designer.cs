namespace DatAdmin
{
    partial class GetExportDataForm
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
            this.rbtAll = new System.Windows.Forms.RadioButton();
            this.rbtSelectedRows = new System.Windows.Forms.RadioButton();
            this.rbtSelectedRowsColumns = new System.Windows.Forms.RadioButton();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "s_export";
            // 
            // rbtAll
            // 
            this.rbtAll.AutoSize = true;
            this.rbtAll.Location = new System.Drawing.Point(15, 42);
            this.rbtAll.Name = "rbtAll";
            this.rbtAll.Size = new System.Drawing.Size(46, 17);
            this.rbtAll.TabIndex = 1;
            this.rbtAll.Text = "s_all";
            this.rbtAll.UseVisualStyleBackColor = true;
            // 
            // rbtSelectedRows
            // 
            this.rbtSelectedRows.AutoSize = true;
            this.rbtSelectedRows.Location = new System.Drawing.Point(15, 65);
            this.rbtSelectedRows.Name = "rbtSelectedRows";
            this.rbtSelectedRows.Size = new System.Drawing.Size(165, 17);
            this.rbtSelectedRows.TabIndex = 2;
            this.rbtSelectedRows.Text = "s_selected_rows_all_columns";
            this.rbtSelectedRows.UseVisualStyleBackColor = true;
            // 
            // rbtSelectedRowsColumns
            // 
            this.rbtSelectedRowsColumns.AutoSize = true;
            this.rbtSelectedRowsColumns.Checked = true;
            this.rbtSelectedRowsColumns.Location = new System.Drawing.Point(15, 88);
            this.rbtSelectedRowsColumns.Name = "rbtSelectedRowsColumns";
            this.rbtSelectedRowsColumns.Size = new System.Drawing.Size(195, 17);
            this.rbtSelectedRowsColumns.TabIndex = 3;
            this.rbtSelectedRowsColumns.TabStop = true;
            this.rbtSelectedRowsColumns.Text = "s_selected_columns_selected_rows";
            this.rbtSelectedRowsColumns.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(194, 122);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "s_ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(275, 122);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "s_cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // GetExportDataForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(362, 154);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.rbtSelectedRowsColumns);
            this.Controls.Add(this.rbtSelectedRows);
            this.Controls.Add(this.rbtAll);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "GetExportDataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "s_export";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbtAll;
        private System.Windows.Forms.RadioButton rbtSelectedRows;
        private System.Windows.Forms.RadioButton rbtSelectedRowsColumns;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}