namespace DatAdmin
{
    partial class TablePerspectiveEditorFrame
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtInnerJoin = new System.Windows.Forms.RadioButton();
            this.rbtLeftJoin = new System.Windows.Forms.RadioButton();
            this.tbxAlias = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labBindingExpression = new System.Windows.Forms.Label();
            this.cbxReferencedTable = new System.Windows.Forms.ComboBox();
            this.lsvRelations = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAddRelation = new System.Windows.Forms.ToolStripButton();
            this.btnRemoveRelation = new System.Windows.Forms.ToolStripButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridViewColumns = new DatAdmin.DataGridViewEx();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDataColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colLookupColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnAddColumn = new System.Windows.Forms.ToolStripButton();
            this.btnRemoveColumn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnMoveUp = new System.Windows.Forms.ToolStripButton();
            this.btnMoveDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAddTableColumns = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.objectFilterFrame1 = new DatAdmin.ObjectFilterFrame();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewColumns)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(834, 366);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.objectFilterFrame1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(826, 340);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "s_conditions";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.tbxAlias);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.labBindingExpression);
            this.tabPage1.Controls.Add(this.cbxReferencedTable);
            this.tabPage1.Controls.Add(this.lsvRelations);
            this.tabPage1.Controls.Add(this.toolStrip1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(826, 340);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "s_linked_data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtInnerJoin);
            this.groupBox1.Controls.Add(this.rbtLeftJoin);
            this.groupBox1.Location = new System.Drawing.Point(687, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(121, 65);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "s_join_type";
            // 
            // rbtInnerJoin
            // 
            this.rbtInnerJoin.AutoSize = true;
            this.rbtInnerJoin.Location = new System.Drawing.Point(7, 39);
            this.rbtInnerJoin.Name = "rbtInnerJoin";
            this.rbtInnerJoin.Size = new System.Drawing.Size(86, 17);
            this.rbtInnerJoin.TabIndex = 1;
            this.rbtInnerJoin.Text = "INNER JOIN";
            this.rbtInnerJoin.UseVisualStyleBackColor = true;
            // 
            // rbtLeftJoin
            // 
            this.rbtLeftJoin.AutoSize = true;
            this.rbtLeftJoin.Checked = true;
            this.rbtLeftJoin.Location = new System.Drawing.Point(7, 16);
            this.rbtLeftJoin.Name = "rbtLeftJoin";
            this.rbtLeftJoin.Size = new System.Drawing.Size(78, 17);
            this.rbtLeftJoin.TabIndex = 0;
            this.rbtLeftJoin.TabStop = true;
            this.rbtLeftJoin.Text = "LEFT JOIN";
            this.rbtLeftJoin.UseVisualStyleBackColor = true;
            // 
            // tbxAlias
            // 
            this.tbxAlias.Location = new System.Drawing.Point(541, 58);
            this.tbxAlias.Name = "tbxAlias";
            this.tbxAlias.Size = new System.Drawing.Size(121, 20);
            this.tbxAlias.TabIndex = 10;
            this.tbxAlias.TextChanged += new System.EventHandler(this.tbxAlias_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(538, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "s_table_alias";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "s_relations";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(538, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "s_referenced_table";
            // 
            // labBindingExpression
            // 
            this.labBindingExpression.AutoSize = true;
            this.labBindingExpression.Location = new System.Drawing.Point(684, 149);
            this.labBindingExpression.Name = "labBindingExpression";
            this.labBindingExpression.Size = new System.Drawing.Size(108, 13);
            this.labBindingExpression.TabIndex = 6;
            this.labBindingExpression.Text = "s_binding_expression";
            // 
            // cbxReferencedTable
            // 
            this.cbxReferencedTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxReferencedTable.FormattingEnabled = true;
            this.cbxReferencedTable.Location = new System.Drawing.Point(541, 141);
            this.cbxReferencedTable.Name = "cbxReferencedTable";
            this.cbxReferencedTable.Size = new System.Drawing.Size(121, 21);
            this.cbxReferencedTable.TabIndex = 5;
            this.cbxReferencedTable.SelectedIndexChanged += new System.EventHandler(this.cbxReferencedTable_SelectedIndexChanged);
            // 
            // lsvRelations
            // 
            this.lsvRelations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lsvRelations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lsvRelations.FullRowSelect = true;
            this.lsvRelations.HideSelection = false;
            this.lsvRelations.Location = new System.Drawing.Point(21, 53);
            this.lsvRelations.Name = "lsvRelations";
            this.lsvRelations.Size = new System.Drawing.Size(501, 281);
            this.lsvRelations.TabIndex = 3;
            this.lsvRelations.UseCompatibleStateImageBehavior = false;
            this.lsvRelations.View = System.Windows.Forms.View.Details;
            this.lsvRelations.SelectedIndexChanged += new System.EventHandler(this.lsvRelations_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "s_binding_table";
            this.columnHeader1.Width = 160;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "s_referenced_table";
            this.columnHeader2.Width = 160;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "s_alias";
            this.columnHeader3.Width = 160;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddRelation,
            this.btnRemoveRelation});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(820, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAddRelation
            // 
            this.btnAddRelation.Image = global::DatAdmin.StdIcons.add;
            this.btnAddRelation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddRelation.Name = "btnAddRelation";
            this.btnAddRelation.Size = new System.Drawing.Size(98, 22);
            this.btnAddRelation.Text = "s_add_relation";
            this.btnAddRelation.Click += new System.EventHandler(this.btnAddRelation_Click);
            // 
            // btnRemoveRelation
            // 
            this.btnRemoveRelation.Image = global::DatAdmin.StdIcons.remove;
            this.btnRemoveRelation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoveRelation.Name = "btnRemoveRelation";
            this.btnRemoveRelation.Size = new System.Drawing.Size(116, 22);
            this.btnRemoveRelation.Text = "s_remove_relation";
            this.btnRemoveRelation.Click += new System.EventHandler(this.btnRemoveRelation_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridViewColumns);
            this.tabPage2.Controls.Add(this.toolStrip2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(826, 340);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "s_columns";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridViewColumns
            // 
            this.dataGridViewColumns.AllowUserToAddRows = false;
            this.dataGridViewColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.colDataColumn,
            this.colLookupColumn});
            this.dataGridViewColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewColumns.Location = new System.Drawing.Point(3, 28);
            this.dataGridViewColumns.Name = "dataGridViewColumns";
            this.dataGridViewColumns.Size = new System.Drawing.Size(820, 309);
            this.dataGridViewColumns.TabIndex = 0;
            this.dataGridViewColumns.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewColumns_DataError);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "s_name";
            this.Column1.Name = "Column1";
            this.Column1.Width = 150;
            // 
            // colDataColumn
            // 
            this.colDataColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.colDataColumn.HeaderText = "s_data";
            this.colDataColumn.Name = "colDataColumn";
            this.colDataColumn.Width = 150;
            // 
            // colLookupColumn
            // 
            this.colLookupColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.colLookupColumn.HeaderText = "s_lookup";
            this.colLookupColumn.Name = "colLookupColumn";
            this.colLookupColumn.Width = 150;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddColumn,
            this.btnRemoveColumn,
            this.toolStripSeparator1,
            this.btnMoveUp,
            this.btnMoveDown,
            this.toolStripSeparator2,
            this.btnAddTableColumns});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(820, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnAddColumn
            // 
            this.btnAddColumn.Image = global::DatAdmin.StdIcons.add;
            this.btnAddColumn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddColumn.Name = "btnAddColumn";
            this.btnAddColumn.Size = new System.Drawing.Size(95, 22);
            this.btnAddColumn.Text = "s_add_column";
            this.btnAddColumn.Click += new System.EventHandler(this.btnAddColumn_Click);
            // 
            // btnRemoveColumn
            // 
            this.btnRemoveColumn.Image = global::DatAdmin.StdIcons.remove;
            this.btnRemoveColumn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoveColumn.Name = "btnRemoveColumn";
            this.btnRemoveColumn.Size = new System.Drawing.Size(113, 22);
            this.btnRemoveColumn.Text = "s_remove_column";
            this.btnRemoveColumn.Click += new System.EventHandler(this.btnRemoveColumn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveUp.Image = global::DatAdmin.StdIcons.up1;
            this.btnMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(23, 22);
            this.btnMoveUp.Text = "s_move_up";
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveDown.Image = global::DatAdmin.StdIcons.down1;
            this.btnMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(23, 22);
            this.btnMoveDown.Text = "s_move_down";
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAddTableColumns
            // 
            this.btnAddTableColumns.Image = global::DatAdmin.StdIcons.table;
            this.btnAddTableColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddTableColumns.Name = "btnAddTableColumns";
            this.btnAddTableColumns.Size = new System.Drawing.Size(95, 22);
            this.btnAddTableColumns.Text = "s_add_table";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::DatAdmin.StdIcons.add;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(102, 22);
            this.toolStripButton1.Text = "s_add_relation";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::DatAdmin.StdIcons.remove;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(122, 22);
            this.toolStripButton2.Text = "s_remove_relation";
            // 
            // objectFilterFrame1
            // 
            this.objectFilterFrame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectFilterFrame1.Filter = null;
            this.objectFilterFrame1.Location = new System.Drawing.Point(3, 3);
            this.objectFilterFrame1.Name = "objectFilterFrame1";
            this.objectFilterFrame1.Size = new System.Drawing.Size(820, 334);
            this.objectFilterFrame1.TabIndex = 0;
            // 
            // TablePerspectiveEditorFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "TablePerspectiveEditorFrame";
            this.Size = new System.Drawing.Size(834, 366);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewColumns)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAddRelation;
        private System.Windows.Forms.ToolStripButton btnRemoveRelation;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private DataGridViewEx dataGridViewColumns;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnAddColumn;
        private System.Windows.Forms.ToolStripButton btnRemoveColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewComboBoxColumn colDataColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn colLookupColumn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnMoveUp;
        private System.Windows.Forms.ToolStripButton btnMoveDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton btnAddTableColumns;
        private System.Windows.Forms.ListView lsvRelations;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labBindingExpression;
        private System.Windows.Forms.ComboBox cbxReferencedTable;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TextBox tbxAlias;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtInnerJoin;
        private System.Windows.Forms.RadioButton rbtLeftJoin;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private ObjectFilterFrame objectFilterFrame1;
    }
}
