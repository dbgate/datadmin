namespace DatAdmin
{
    partial class InsertJoinForm
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
            this.lbxExistingTable = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxAlias = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.codeEditor1 = new DatAdmin.CodeEditor();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtRight = new System.Windows.Forms.RadioButton();
            this.rbtLeft = new System.Windows.Forms.RadioButton();
            this.rbtInner = new System.Windows.Forms.RadioButton();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.olvNewTable = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn2 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn3 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn4 = new BrightIdeasSoftware.OLVColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvNewTable)).BeginInit();
            this.SuspendLayout();
            // 
            // lbxExistingTable
            // 
            this.lbxExistingTable.FormattingEnabled = true;
            this.lbxExistingTable.Location = new System.Drawing.Point(16, 38);
            this.lbxExistingTable.Name = "lbxExistingTable";
            this.lbxExistingTable.Size = new System.Drawing.Size(120, 134);
            this.lbxExistingTable.TabIndex = 0;
            this.lbxExistingTable.SelectedIndexChanged += new System.EventHandler(this.lbxExistingTable_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "s_existing_table";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(163, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "s_new_table";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(703, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "s_alias";
            // 
            // tbxAlias
            // 
            this.tbxAlias.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxAlias.Location = new System.Drawing.Point(682, 38);
            this.tbxAlias.Name = "tbxAlias";
            this.tbxAlias.Size = new System.Drawing.Size(120, 20);
            this.tbxAlias.TabIndex = 2;
            this.tbxAlias.TextChanged += new System.EventHandler(this.tbxAlias_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(13, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "1.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(139, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "2.";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(679, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 16);
            this.label6.TabIndex = 8;
            this.label6.Text = "3.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 184);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "s_press_enter_to_confirm";
            // 
            // codeEditor1
            // 
            this.codeEditor1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.codeEditor1.Connection = null;
            this.codeEditor1.Dialect = null;
            this.codeEditor1.IsReadOnly = true;
            this.codeEditor1.Language = DatAdmin.CodeLanguage.Sql;
            this.codeEditor1.Location = new System.Drawing.Point(16, 233);
            this.codeEditor1.Modified = true;
            this.codeEditor1.Name = "codeEditor1";
            this.codeEditor1.ReadOnly = true;
            this.codeEditor1.Size = new System.Drawing.Size(786, 170);
            this.codeEditor1.TabIndex = 10;
            this.codeEditor1.TabStop = false;
            this.codeEditor1.Text = "codeEditor1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 217);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "s_preview";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rbtRight);
            this.groupBox1.Controls.Add(this.rbtLeft);
            this.groupBox1.Controls.Add(this.rbtInner);
            this.groupBox1.Location = new System.Drawing.Point(682, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(120, 97);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "JOIN";
            // 
            // rbtRight
            // 
            this.rbtRight.AutoSize = true;
            this.rbtRight.Location = new System.Drawing.Point(7, 68);
            this.rbtRight.Name = "rbtRight";
            this.rbtRight.Size = new System.Drawing.Size(59, 17);
            this.rbtRight.TabIndex = 2;
            this.rbtRight.Text = "RIGHT";
            this.rbtRight.UseVisualStyleBackColor = true;
            this.rbtRight.CheckedChanged += new System.EventHandler(this.rbtInner_CheckedChanged);
            // 
            // rbtLeft
            // 
            this.rbtLeft.AutoSize = true;
            this.rbtLeft.Location = new System.Drawing.Point(7, 44);
            this.rbtLeft.Name = "rbtLeft";
            this.rbtLeft.Size = new System.Drawing.Size(51, 17);
            this.rbtLeft.TabIndex = 1;
            this.rbtLeft.Text = "LEFT";
            this.rbtLeft.UseVisualStyleBackColor = true;
            this.rbtLeft.CheckedChanged += new System.EventHandler(this.rbtInner_CheckedChanged);
            // 
            // rbtInner
            // 
            this.rbtInner.AutoSize = true;
            this.rbtInner.Checked = true;
            this.rbtInner.Location = new System.Drawing.Point(7, 20);
            this.rbtInner.Name = "rbtInner";
            this.rbtInner.Size = new System.Drawing.Size(59, 17);
            this.rbtInner.TabIndex = 0;
            this.rbtInner.TabStop = true;
            this.rbtInner.Text = "INNER";
            this.rbtInner.UseVisualStyleBackColor = true;
            this.rbtInner.CheckedChanged += new System.EventHandler(this.rbtInner_CheckedChanged);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(646, 423);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "s_ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(727, 423);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "s_cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // olvNewTable
            // 
            this.olvNewTable.AllColumns.Add(this.olvColumn1);
            this.olvNewTable.AllColumns.Add(this.olvColumn2);
            this.olvNewTable.AllColumns.Add(this.olvColumn3);
            this.olvNewTable.AllColumns.Add(this.olvColumn4);
            this.olvNewTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.olvNewTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4});
            this.olvNewTable.FullRowSelect = true;
            this.olvNewTable.HideSelection = false;
            this.olvNewTable.Location = new System.Drawing.Point(143, 38);
            this.olvNewTable.Name = "olvNewTable";
            this.olvNewTable.ShowGroups = false;
            this.olvNewTable.Size = new System.Drawing.Size(533, 134);
            this.olvNewTable.SortGroupItemsByPrimaryColumn = false;
            this.olvNewTable.TabIndex = 1;
            this.olvNewTable.UseCompatibleStateImageBehavior = false;
            this.olvNewTable.View = System.Windows.Forms.View.Details;
            this.olvNewTable.SelectedIndexChanged += new System.EventHandler(this.olvNewTable_SelectedIndexChanged);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "ExistingColumn";
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.Text = "s_column";
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "TableName";
            this.olvColumn2.FillsFreeSpace = true;
            this.olvColumn2.Text = "s_table";
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "NewColumn";
            this.olvColumn3.FillsFreeSpace = true;
            this.olvColumn3.Text = "s_column";
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "ForeignKey";
            this.olvColumn4.FillsFreeSpace = true;
            this.olvColumn4.Text = "s_foreign_key";
            // 
            // InsertJoinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(817, 458);
            this.Controls.Add(this.olvNewTable);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.codeEditor1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbxAlias);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbxExistingTable);
            this.KeyPreview = true;
            this.Name = "InsertJoinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "s_insert_sql_join";
            this.Shown += new System.EventHandler(this.InsertJoinForm_Shown);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.InsertJoinForm_PreviewKeyDown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InsertJoinForm_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvNewTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbxExistingTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxAlias;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private CodeEditor codeEditor1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtRight;
        private System.Windows.Forms.RadioButton rbtLeft;
        private System.Windows.Forms.RadioButton rbtInner;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private BrightIdeasSoftware.ObjectListView olvNewTable;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
    }
}