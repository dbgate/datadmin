namespace DatAdmin
{
    partial class ChooseVisibleColumnsForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.lbxVisibleColumns = new System.Windows.Forms.ListBox();
            this.lbxAvailableColumns = new System.Windows.Forms.ListBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddVisible = new System.Windows.Forms.Button();
            this.btnAddAllVisible = new System.Windows.Forms.Button();
            this.btnRemoveVisible = new System.Windows.Forms.Button();
            this.btnRemoveAllVisible = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "s_visible_columns";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(336, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "s_available_columns";
            // 
            // lbxVisibleColumns
            // 
            this.lbxVisibleColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lbxVisibleColumns.FormattingEnabled = true;
            this.lbxVisibleColumns.Location = new System.Drawing.Point(24, 39);
            this.lbxVisibleColumns.Name = "lbxVisibleColumns";
            this.lbxVisibleColumns.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbxVisibleColumns.Size = new System.Drawing.Size(215, 251);
            this.lbxVisibleColumns.TabIndex = 2;
            this.lbxVisibleColumns.DoubleClick += new System.EventHandler(this.btnRemoveVisible_Click);
            // 
            // lbxAvailableColumns
            // 
            this.lbxAvailableColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lbxAvailableColumns.FormattingEnabled = true;
            this.lbxAvailableColumns.Location = new System.Drawing.Point(327, 39);
            this.lbxAvailableColumns.Name = "lbxAvailableColumns";
            this.lbxAvailableColumns.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbxAvailableColumns.Size = new System.Drawing.Size(215, 251);
            this.lbxAvailableColumns.TabIndex = 3;
            this.lbxAvailableColumns.DoubleClick += new System.EventHandler(this.btnAddVisible_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(386, 303);
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
            this.btnCancel.Location = new System.Drawing.Point(467, 303);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "s_cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnAddVisible
            // 
            this.btnAddVisible.Location = new System.Drawing.Point(245, 39);
            this.btnAddVisible.Name = "btnAddVisible";
            this.btnAddVisible.Size = new System.Drawing.Size(75, 23);
            this.btnAddVisible.TabIndex = 6;
            this.btnAddVisible.Text = "<";
            this.btnAddVisible.UseVisualStyleBackColor = true;
            this.btnAddVisible.Click += new System.EventHandler(this.btnAddVisible_Click);
            // 
            // btnAddAllVisible
            // 
            this.btnAddAllVisible.Location = new System.Drawing.Point(246, 68);
            this.btnAddAllVisible.Name = "btnAddAllVisible";
            this.btnAddAllVisible.Size = new System.Drawing.Size(75, 23);
            this.btnAddAllVisible.TabIndex = 7;
            this.btnAddAllVisible.Text = "<<";
            this.btnAddAllVisible.UseVisualStyleBackColor = true;
            this.btnAddAllVisible.Click += new System.EventHandler(this.btnAddAllVisible_Click);
            // 
            // btnRemoveVisible
            // 
            this.btnRemoveVisible.Location = new System.Drawing.Point(245, 97);
            this.btnRemoveVisible.Name = "btnRemoveVisible";
            this.btnRemoveVisible.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveVisible.TabIndex = 8;
            this.btnRemoveVisible.Text = ">";
            this.btnRemoveVisible.UseVisualStyleBackColor = true;
            this.btnRemoveVisible.Click += new System.EventHandler(this.btnRemoveVisible_Click);
            // 
            // btnRemoveAllVisible
            // 
            this.btnRemoveAllVisible.Location = new System.Drawing.Point(246, 126);
            this.btnRemoveAllVisible.Name = "btnRemoveAllVisible";
            this.btnRemoveAllVisible.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveAllVisible.TabIndex = 9;
            this.btnRemoveAllVisible.Text = ">>";
            this.btnRemoveAllVisible.UseVisualStyleBackColor = true;
            this.btnRemoveAllVisible.Click += new System.EventHandler(this.btnRemoveAllVisible_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Image = global::DatAdmin.StdIcons.up1;
            this.btnMoveUp.Location = new System.Drawing.Point(24, 303);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(75, 23);
            this.btnMoveUp.TabIndex = 10;
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Image = global::DatAdmin.StdIcons.down1;
            this.btnMoveDown.Location = new System.Drawing.Point(106, 302);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(75, 23);
            this.btnMoveDown.TabIndex = 11;
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // ChooseVisibleColumnsForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(566, 338);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.btnRemoveAllVisible);
            this.Controls.Add(this.btnRemoveVisible);
            this.Controls.Add(this.btnAddAllVisible);
            this.Controls.Add(this.btnAddVisible);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lbxAvailableColumns);
            this.Controls.Add(this.lbxVisibleColumns);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ChooseVisibleColumnsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "s_choose_visible_columns";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbxVisibleColumns;
        private System.Windows.Forms.ListBox lbxAvailableColumns;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAddVisible;
        private System.Windows.Forms.Button btnAddAllVisible;
        private System.Windows.Forms.Button btnRemoveVisible;
        private System.Windows.Forms.Button btnRemoveAllVisible;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
    }
}