namespace Plugin.datasyn
{
    partial class DataSynDefItemFrame
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxSourceTableWhere = new System.Windows.Forms.TextBox();
            this.cbxSourceView = new System.Windows.Forms.ComboBox();
            this.cbxSourceTable = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxTargetWhere = new System.Windows.Forms.TextBox();
            this.lbxKeyColumns = new System.Windows.Forms.CheckedListBox();
            this.rbtDefineOwnKey = new System.Windows.Forms.RadioButton();
            this.rbtUsePrimaryKey = new System.Windows.Forms.RadioButton();
            this.lbxAllColumns = new System.Windows.Forms.ListBox();
            this.chbSelectedColumns = new System.Windows.Forms.CheckedListBox();
            this.chbNotSelectedColumns = new System.Windows.Forms.CheckedListBox();
            this.dataGridView1 = new DatAdmin.DataGridViewEx();
            this.colSource = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colTarget = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSkip = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colDefValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbxCompareColumns = new System.Windows.Forms.CheckedListBox();
            this.rbtCompareAllExceptSelected = new System.Windows.Forms.RadioButton();
            this.rbtCompareOnlySelected = new System.Windows.Forms.RadioButton();
            this.rbtCompareAllColumns = new System.Windows.Forms.RadioButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSourceAndTarget = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tbxTargetTable = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbxSourceViewWhere = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.codeEditorQuery = new DatAdmin.CodeEditor();
            this.rbtNone = new System.Windows.Forms.RadioButton();
            this.rbtView = new System.Windows.Forms.RadioButton();
            this.rbtQuery = new System.Windows.Forms.RadioButton();
            this.rbtTable = new System.Windows.Forms.RadioButton();
            this.tabColumns = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbtCustomMapping = new System.Windows.Forms.RadioButton();
            this.rbtAllExceptSelected = new System.Windows.Forms.RadioButton();
            this.rbtOnlySelected = new System.Windows.Forms.RadioButton();
            this.rbtAllColumns = new System.Windows.Forms.RadioButton();
            this.tabKey = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tabOnlyInSource = new System.Windows.Forms.TabPage();
            this.tblOnlyInSource = new DatAdmin.TableDataFrame();
            this.tabOnlyInTarget = new System.Windows.Forms.TabPage();
            this.tabModified = new System.Windows.Forms.TabPage();
            this.tabEqual = new System.Windows.Forms.TabPage();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tblOnlyInTarget = new DatAdmin.TableDataFrame();
            this.tblModified = new DatAdmin.TableDataFrame();
            this.tblEqual = new DatAdmin.TableDataFrame();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabSourceAndTarget.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabColumns.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabKey.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabOnlyInSource.SuspendLayout();
            this.tabOnlyInTarget.SuspendLayout();
            this.tabModified.SuspendLayout();
            this.tabEqual.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(263, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "WHERE";
            // 
            // tbxSourceTableWhere
            // 
            this.tbxSourceTableWhere.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxSourceTableWhere.Location = new System.Drawing.Point(317, 41);
            this.tbxSourceTableWhere.Name = "tbxSourceTableWhere";
            this.tbxSourceTableWhere.Size = new System.Drawing.Size(373, 20);
            this.tbxSourceTableWhere.TabIndex = 7;
            this.tbxSourceTableWhere.TextChanged += new System.EventHandler(this.tbxSourceWhere_TextChanged);
            // 
            // cbxSourceView
            // 
            this.cbxSourceView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSourceView.FormattingEnabled = true;
            this.cbxSourceView.Location = new System.Drawing.Point(116, 63);
            this.cbxSourceView.Name = "cbxSourceView";
            this.cbxSourceView.Size = new System.Drawing.Size(142, 21);
            this.cbxSourceView.TabIndex = 4;
            this.cbxSourceView.SelectedIndexChanged += new System.EventHandler(this.cbxSourceView_SelectedIndexChanged);
            // 
            // cbxSourceTable
            // 
            this.cbxSourceTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSourceTable.FormattingEnabled = true;
            this.cbxSourceTable.Location = new System.Drawing.Point(116, 40);
            this.cbxSourceTable.Name = "cbxSourceTable";
            this.cbxSourceTable.Size = new System.Drawing.Size(142, 21);
            this.cbxSourceTable.TabIndex = 2;
            this.cbxSourceTable.SelectedIndexChanged += new System.EventHandler(this.cbxSourceTable_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(261, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "WHERE";
            // 
            // tbxTargetWhere
            // 
            this.tbxTargetWhere.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxTargetWhere.Location = new System.Drawing.Point(315, 13);
            this.tbxTargetWhere.Name = "tbxTargetWhere";
            this.tbxTargetWhere.Size = new System.Drawing.Size(373, 20);
            this.tbxTargetWhere.TabIndex = 3;
            this.tbxTargetWhere.TextChanged += new System.EventHandler(this.tbxTargetWhere_TextChanged);
            // 
            // lbxKeyColumns
            // 
            this.lbxKeyColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxKeyColumns.CheckOnClick = true;
            this.lbxKeyColumns.FormattingEnabled = true;
            this.lbxKeyColumns.Location = new System.Drawing.Point(177, 19);
            this.lbxKeyColumns.MultiColumn = true;
            this.lbxKeyColumns.Name = "lbxKeyColumns";
            this.lbxKeyColumns.Size = new System.Drawing.Size(397, 79);
            this.lbxKeyColumns.TabIndex = 3;
            this.lbxKeyColumns.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lbxKeyColumns_ItemCheck);
            // 
            // rbtDefineOwnKey
            // 
            this.rbtDefineOwnKey.AutoSize = true;
            this.rbtDefineOwnKey.Location = new System.Drawing.Point(10, 42);
            this.rbtDefineOwnKey.Name = "rbtDefineOwnKey";
            this.rbtDefineOwnKey.Size = new System.Drawing.Size(91, 17);
            this.rbtDefineOwnKey.TabIndex = 2;
            this.rbtDefineOwnKey.TabStop = true;
            this.rbtDefineOwnKey.Text = "s_define_own";
            this.rbtDefineOwnKey.UseVisualStyleBackColor = true;
            this.rbtDefineOwnKey.CheckedChanged += new System.EventHandler(this.rbtKey_CheckedChanged);
            // 
            // rbtUsePrimaryKey
            // 
            this.rbtUsePrimaryKey.AutoSize = true;
            this.rbtUsePrimaryKey.Location = new System.Drawing.Point(10, 19);
            this.rbtUsePrimaryKey.Name = "rbtUsePrimaryKey";
            this.rbtUsePrimaryKey.Size = new System.Drawing.Size(115, 17);
            this.rbtUsePrimaryKey.TabIndex = 1;
            this.rbtUsePrimaryKey.TabStop = true;
            this.rbtUsePrimaryKey.Text = "s_use_primary_key";
            this.rbtUsePrimaryKey.UseVisualStyleBackColor = true;
            this.rbtUsePrimaryKey.CheckedChanged += new System.EventHandler(this.rbtKey_CheckedChanged);
            // 
            // lbxAllColumns
            // 
            this.lbxAllColumns.FormattingEnabled = true;
            this.lbxAllColumns.Location = new System.Drawing.Point(3, 0);
            this.lbxAllColumns.MultiColumn = true;
            this.lbxAllColumns.Name = "lbxAllColumns";
            this.lbxAllColumns.Size = new System.Drawing.Size(100, 95);
            this.lbxAllColumns.TabIndex = 0;
            this.lbxAllColumns.Visible = false;
            // 
            // chbSelectedColumns
            // 
            this.chbSelectedColumns.CheckOnClick = true;
            this.chbSelectedColumns.FormattingEnabled = true;
            this.chbSelectedColumns.Location = new System.Drawing.Point(109, 3);
            this.chbSelectedColumns.MultiColumn = true;
            this.chbSelectedColumns.Name = "chbSelectedColumns";
            this.chbSelectedColumns.Size = new System.Drawing.Size(100, 94);
            this.chbSelectedColumns.TabIndex = 0;
            this.chbSelectedColumns.Visible = false;
            this.chbSelectedColumns.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chbSelectedColumns_ItemCheck);
            // 
            // chbNotSelectedColumns
            // 
            this.chbNotSelectedColumns.CheckOnClick = true;
            this.chbNotSelectedColumns.FormattingEnabled = true;
            this.chbNotSelectedColumns.Location = new System.Drawing.Point(215, 3);
            this.chbNotSelectedColumns.MultiColumn = true;
            this.chbNotSelectedColumns.Name = "chbNotSelectedColumns";
            this.chbNotSelectedColumns.Size = new System.Drawing.Size(100, 94);
            this.chbNotSelectedColumns.TabIndex = 0;
            this.chbNotSelectedColumns.Visible = false;
            this.chbNotSelectedColumns.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chbNotSelectedColumns_ItemCheck);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSource,
            this.colTarget,
            this.colSkip,
            this.colDefValue});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(3, 101);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Size = new System.Drawing.Size(312, 227);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.Visible = false;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // colSource
            // 
            this.colSource.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.colSource.HeaderText = "s_source";
            this.colSource.Name = "colSource";
            // 
            // colTarget
            // 
            this.colTarget.HeaderText = "s_target";
            this.colTarget.Name = "colTarget";
            this.colTarget.ReadOnly = true;
            // 
            // colSkip
            // 
            this.colSkip.HeaderText = "s_skip";
            this.colSkip.Name = "colSkip";
            // 
            // colDefValue
            // 
            this.colDefValue.HeaderText = "s_default";
            this.colDefValue.Name = "colDefValue";
            // 
            // lbxCompareColumns
            // 
            this.lbxCompareColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxCompareColumns.CheckOnClick = true;
            this.lbxCompareColumns.FormattingEnabled = true;
            this.lbxCompareColumns.Location = new System.Drawing.Point(177, 19);
            this.lbxCompareColumns.Name = "lbxCompareColumns";
            this.lbxCompareColumns.Size = new System.Drawing.Size(397, 64);
            this.lbxCompareColumns.TabIndex = 4;
            this.lbxCompareColumns.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lbxCompareColumns_ItemCheck);
            // 
            // rbtCompareAllExceptSelected
            // 
            this.rbtCompareAllExceptSelected.AutoSize = true;
            this.rbtCompareAllExceptSelected.Location = new System.Drawing.Point(6, 65);
            this.rbtCompareAllExceptSelected.Name = "rbtCompareAllExceptSelected";
            this.rbtCompareAllExceptSelected.Size = new System.Drawing.Size(130, 17);
            this.rbtCompareAllExceptSelected.TabIndex = 3;
            this.rbtCompareAllExceptSelected.TabStop = true;
            this.rbtCompareAllExceptSelected.Text = "s_all_except_selected";
            this.rbtCompareAllExceptSelected.UseVisualStyleBackColor = true;
            this.rbtCompareAllExceptSelected.CheckedChanged += new System.EventHandler(this.rbtCompareAllColumns_CheckedChanged);
            // 
            // rbtCompareOnlySelected
            // 
            this.rbtCompareOnlySelected.AutoSize = true;
            this.rbtCompareOnlySelected.Location = new System.Drawing.Point(6, 42);
            this.rbtCompareOnlySelected.Name = "rbtCompareOnlySelected";
            this.rbtCompareOnlySelected.Size = new System.Drawing.Size(101, 17);
            this.rbtCompareOnlySelected.TabIndex = 2;
            this.rbtCompareOnlySelected.TabStop = true;
            this.rbtCompareOnlySelected.Text = "s_only_selected";
            this.rbtCompareOnlySelected.UseVisualStyleBackColor = true;
            this.rbtCompareOnlySelected.CheckedChanged += new System.EventHandler(this.rbtCompareAllColumns_CheckedChanged);
            // 
            // rbtCompareAllColumns
            // 
            this.rbtCompareAllColumns.AutoSize = true;
            this.rbtCompareAllColumns.Location = new System.Drawing.Point(6, 19);
            this.rbtCompareAllColumns.Name = "rbtCompareAllColumns";
            this.rbtCompareAllColumns.Size = new System.Drawing.Size(46, 17);
            this.rbtCompareAllColumns.TabIndex = 1;
            this.rbtCompareAllColumns.TabStop = true;
            this.rbtCompareAllColumns.Text = "s_all";
            this.rbtCompareAllColumns.UseVisualStyleBackColor = true;
            this.rbtCompareAllColumns.CheckedChanged += new System.EventHandler(this.rbtCompareAllColumns_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabSourceAndTarget);
            this.tabControl1.Controls.Add(this.tabColumns);
            this.tabControl1.Controls.Add(this.tabKey);
            this.tabControl1.Controls.Add(this.tabOnlyInSource);
            this.tabControl1.Controls.Add(this.tabOnlyInTarget);
            this.tabControl1.Controls.Add(this.tabModified);
            this.tabControl1.Controls.Add(this.tabEqual);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(749, 445);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabSourceAndTarget
            // 
            this.tabSourceAndTarget.AutoScroll = true;
            this.tabSourceAndTarget.Controls.Add(this.groupBox2);
            this.tabSourceAndTarget.Controls.Add(this.groupBox1);
            this.tabSourceAndTarget.Location = new System.Drawing.Point(4, 23);
            this.tabSourceAndTarget.Name = "tabSourceAndTarget";
            this.tabSourceAndTarget.Padding = new System.Windows.Forms.Padding(3);
            this.tabSourceAndTarget.Size = new System.Drawing.Size(741, 418);
            this.tabSourceAndTarget.TabIndex = 0;
            this.tabSourceAndTarget.Text = "s_source_and_target";
            this.tabSourceAndTarget.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Controls.Add(this.tbxTargetTable);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.tbxTargetWhere);
            this.groupBox2.Location = new System.Drawing.Point(24, 235);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(694, 85);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "s_target";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Plugin.datasyn.StdRes.table;
            this.pictureBox1.Location = new System.Drawing.Point(7, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(19, 22);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // tbxTargetTable
            // 
            this.tbxTargetTable.Location = new System.Drawing.Point(114, 13);
            this.tbxTargetTable.Name = "tbxTargetTable";
            this.tbxTargetTable.ReadOnly = true;
            this.tbxTargetTable.Size = new System.Drawing.Size(142, 20);
            this.tbxTargetTable.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "s_table";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tbxSourceViewWhere);
            this.groupBox1.Controls.Add(this.tbxSourceTableWhere);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.codeEditorQuery);
            this.groupBox1.Controls.Add(this.cbxSourceView);
            this.groupBox1.Controls.Add(this.cbxSourceTable);
            this.groupBox1.Controls.Add(this.rbtNone);
            this.groupBox1.Controls.Add(this.rbtView);
            this.groupBox1.Controls.Add(this.rbtQuery);
            this.groupBox1.Controls.Add(this.rbtTable);
            this.groupBox1.Location = new System.Drawing.Point(22, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(696, 202);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "s_source";
            // 
            // tbxSourceViewWhere
            // 
            this.tbxSourceViewWhere.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxSourceViewWhere.Location = new System.Drawing.Point(317, 64);
            this.tbxSourceViewWhere.Name = "tbxSourceViewWhere";
            this.tbxSourceViewWhere.Size = new System.Drawing.Size(373, 20);
            this.tbxSourceViewWhere.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(263, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "WHERE";
            // 
            // codeEditorQuery
            // 
            this.codeEditorQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.codeEditorQuery.Connection = null;
            this.codeEditorQuery.Dialect = null;
            this.codeEditorQuery.Language = DatAdmin.CodeLanguage.Sql;
            this.codeEditorQuery.Location = new System.Drawing.Point(116, 93);
            this.codeEditorQuery.Modified = true;
            this.codeEditorQuery.Name = "codeEditorQuery";
            this.codeEditorQuery.ReadOnly = false;
            this.codeEditorQuery.Size = new System.Drawing.Size(574, 103);
            this.codeEditorQuery.TabIndex = 5;
            this.codeEditorQuery.Leave += new System.EventHandler(this.codeEditorQuery_Leave);
            // 
            // rbtNone
            // 
            this.rbtNone.Image = global::Plugin.datasyn.StdRes.cancel;
            this.rbtNone.Location = new System.Drawing.Point(6, 19);
            this.rbtNone.Name = "rbtNone";
            this.rbtNone.Size = new System.Drawing.Size(104, 24);
            this.rbtNone.TabIndex = 3;
            this.rbtNone.TabStop = true;
            this.rbtNone.Text = "s_none";
            this.rbtNone.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.rbtNone.UseVisualStyleBackColor = true;
            this.rbtNone.CheckedChanged += new System.EventHandler(this.rbtNone_CheckedChanged);
            // 
            // rbtView
            // 
            this.rbtView.Image = global::Plugin.datasyn.StdRes.view;
            this.rbtView.Location = new System.Drawing.Point(6, 65);
            this.rbtView.Name = "rbtView";
            this.rbtView.Size = new System.Drawing.Size(104, 24);
            this.rbtView.TabIndex = 2;
            this.rbtView.TabStop = true;
            this.rbtView.Text = "s_dbview";
            this.rbtView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.rbtView.UseVisualStyleBackColor = true;
            this.rbtView.CheckedChanged += new System.EventHandler(this.rbtNone_CheckedChanged);
            // 
            // rbtQuery
            // 
            this.rbtQuery.Image = global::Plugin.datasyn.StdRes.sql;
            this.rbtQuery.Location = new System.Drawing.Point(6, 88);
            this.rbtQuery.Name = "rbtQuery";
            this.rbtQuery.Size = new System.Drawing.Size(104, 24);
            this.rbtQuery.TabIndex = 1;
            this.rbtQuery.TabStop = true;
            this.rbtQuery.Text = "s_query";
            this.rbtQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.rbtQuery.UseVisualStyleBackColor = true;
            this.rbtQuery.CheckedChanged += new System.EventHandler(this.rbtNone_CheckedChanged);
            // 
            // rbtTable
            // 
            this.rbtTable.Image = global::Plugin.datasyn.StdRes.table;
            this.rbtTable.Location = new System.Drawing.Point(6, 42);
            this.rbtTable.Name = "rbtTable";
            this.rbtTable.Size = new System.Drawing.Size(104, 24);
            this.rbtTable.TabIndex = 0;
            this.rbtTable.TabStop = true;
            this.rbtTable.Text = "s_table";
            this.rbtTable.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.rbtTable.UseVisualStyleBackColor = true;
            this.rbtTable.CheckedChanged += new System.EventHandler(this.rbtNone_CheckedChanged);
            // 
            // tabColumns
            // 
            this.tabColumns.Controls.Add(this.panel1);
            this.tabColumns.Controls.Add(this.groupBox3);
            this.tabColumns.Location = new System.Drawing.Point(4, 23);
            this.tabColumns.Name = "tabColumns";
            this.tabColumns.Padding = new System.Windows.Forms.Padding(3);
            this.tabColumns.Size = new System.Drawing.Size(741, 418);
            this.tabColumns.TabIndex = 1;
            this.tabColumns.Text = "s_columns";
            this.tabColumns.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.chbNotSelectedColumns);
            this.panel1.Controls.Add(this.lbxAllColumns);
            this.panel1.Controls.Add(this.chbSelectedColumns);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(245, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(348, 345);
            this.panel1.TabIndex = 7;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbtCustomMapping);
            this.groupBox3.Controls.Add(this.rbtAllExceptSelected);
            this.groupBox3.Controls.Add(this.rbtOnlySelected);
            this.groupBox3.Controls.Add(this.rbtAllColumns);
            this.groupBox3.Location = new System.Drawing.Point(19, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 162);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "s_columns";
            // 
            // rbtCustomMapping
            // 
            this.rbtCustomMapping.AutoSize = true;
            this.rbtCustomMapping.Location = new System.Drawing.Point(6, 90);
            this.rbtCustomMapping.Name = "rbtCustomMapping";
            this.rbtCustomMapping.Size = new System.Drawing.Size(116, 17);
            this.rbtCustomMapping.TabIndex = 3;
            this.rbtCustomMapping.TabStop = true;
            this.rbtCustomMapping.Text = "s_custom_mapping";
            this.rbtCustomMapping.UseVisualStyleBackColor = true;
            this.rbtCustomMapping.CheckedChanged += new System.EventHandler(this.tbcColumns_SelectedIndexChanged);
            // 
            // rbtAllExceptSelected
            // 
            this.rbtAllExceptSelected.AutoSize = true;
            this.rbtAllExceptSelected.Location = new System.Drawing.Point(7, 67);
            this.rbtAllExceptSelected.Name = "rbtAllExceptSelected";
            this.rbtAllExceptSelected.Size = new System.Drawing.Size(130, 17);
            this.rbtAllExceptSelected.TabIndex = 2;
            this.rbtAllExceptSelected.TabStop = true;
            this.rbtAllExceptSelected.Text = "s_all_except_selected";
            this.rbtAllExceptSelected.UseVisualStyleBackColor = true;
            this.rbtAllExceptSelected.CheckedChanged += new System.EventHandler(this.tbcColumns_SelectedIndexChanged);
            // 
            // rbtOnlySelected
            // 
            this.rbtOnlySelected.AutoSize = true;
            this.rbtOnlySelected.Location = new System.Drawing.Point(7, 44);
            this.rbtOnlySelected.Name = "rbtOnlySelected";
            this.rbtOnlySelected.Size = new System.Drawing.Size(101, 17);
            this.rbtOnlySelected.TabIndex = 1;
            this.rbtOnlySelected.TabStop = true;
            this.rbtOnlySelected.Text = "s_only_selected";
            this.rbtOnlySelected.UseVisualStyleBackColor = true;
            this.rbtOnlySelected.CheckedChanged += new System.EventHandler(this.tbcColumns_SelectedIndexChanged);
            // 
            // rbtAllColumns
            // 
            this.rbtAllColumns.AutoSize = true;
            this.rbtAllColumns.Location = new System.Drawing.Point(7, 20);
            this.rbtAllColumns.Name = "rbtAllColumns";
            this.rbtAllColumns.Size = new System.Drawing.Size(91, 17);
            this.rbtAllColumns.TabIndex = 0;
            this.rbtAllColumns.TabStop = true;
            this.rbtAllColumns.Text = "s_all_columns";
            this.rbtAllColumns.UseVisualStyleBackColor = true;
            this.rbtAllColumns.CheckedChanged += new System.EventHandler(this.tbcColumns_SelectedIndexChanged);
            // 
            // tabKey
            // 
            this.tabKey.AutoScroll = true;
            this.tabKey.Controls.Add(this.groupBox5);
            this.tabKey.Controls.Add(this.groupBox4);
            this.tabKey.Location = new System.Drawing.Point(4, 23);
            this.tabKey.Name = "tabKey";
            this.tabKey.Padding = new System.Windows.Forms.Padding(3);
            this.tabKey.Size = new System.Drawing.Size(741, 418);
            this.tabKey.TabIndex = 2;
            this.tabKey.Text = "s_key_and_comparing";
            this.tabKey.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.lbxCompareColumns);
            this.groupBox5.Controls.Add(this.rbtCompareAllExceptSelected);
            this.groupBox5.Controls.Add(this.rbtCompareOnlySelected);
            this.groupBox5.Controls.Add(this.rbtCompareAllColumns);
            this.groupBox5.Location = new System.Drawing.Point(9, 129);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(589, 93);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "s_compare_columns";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.lbxKeyColumns);
            this.groupBox4.Controls.Add(this.rbtDefineOwnKey);
            this.groupBox4.Controls.Add(this.rbtUsePrimaryKey);
            this.groupBox4.Location = new System.Drawing.Point(9, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(589, 117);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "s_key";
            // 
            // tabOnlyInSource
            // 
            this.tabOnlyInSource.Controls.Add(this.tblOnlyInSource);
            this.tabOnlyInSource.Location = new System.Drawing.Point(4, 23);
            this.tabOnlyInSource.Name = "tabOnlyInSource";
            this.tabOnlyInSource.Padding = new System.Windows.Forms.Padding(3);
            this.tabOnlyInSource.Size = new System.Drawing.Size(741, 418);
            this.tabOnlyInSource.TabIndex = 3;
            this.tabOnlyInSource.Text = "s_only_in_source";
            this.tabOnlyInSource.UseVisualStyleBackColor = true;
            // 
            // tblOnlyInSource
            // 
            this.tblOnlyInSource.Detached = false;
            this.tblOnlyInSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblOnlyInSource.IsContentVisible = false;
            this.tblOnlyInSource.IsLoadingIcon = false;
            this.tblOnlyInSource.Location = new System.Drawing.Point(3, 3);
            this.tblOnlyInSource.MasterFrame = null;
            this.tblOnlyInSource.Name = "tblOnlyInSource";
            this.tblOnlyInSource.Size = new System.Drawing.Size(735, 412);
            this.tblOnlyInSource.TabIndex = 0;
            this.tblOnlyInSource.TabularData = null;
            this.tblOnlyInSource.WinId = null;
            // 
            // tabOnlyInTarget
            // 
            this.tabOnlyInTarget.Controls.Add(this.tblOnlyInTarget);
            this.tabOnlyInTarget.Location = new System.Drawing.Point(4, 23);
            this.tabOnlyInTarget.Name = "tabOnlyInTarget";
            this.tabOnlyInTarget.Padding = new System.Windows.Forms.Padding(3);
            this.tabOnlyInTarget.Size = new System.Drawing.Size(741, 418);
            this.tabOnlyInTarget.TabIndex = 4;
            this.tabOnlyInTarget.Text = "s_only_in_target";
            this.tabOnlyInTarget.UseVisualStyleBackColor = true;
            // 
            // tabModified
            // 
            this.tabModified.Controls.Add(this.tblModified);
            this.tabModified.Location = new System.Drawing.Point(4, 23);
            this.tabModified.Name = "tabModified";
            this.tabModified.Padding = new System.Windows.Forms.Padding(3);
            this.tabModified.Size = new System.Drawing.Size(741, 418);
            this.tabModified.TabIndex = 5;
            this.tabModified.Text = "s_modified";
            this.tabModified.UseVisualStyleBackColor = true;
            // 
            // tabEqual
            // 
            this.tabEqual.Controls.Add(this.tblEqual);
            this.tabEqual.Location = new System.Drawing.Point(4, 23);
            this.tabEqual.Name = "tabEqual";
            this.tabEqual.Padding = new System.Windows.Forms.Padding(3);
            this.tabEqual.Size = new System.Drawing.Size(741, 418);
            this.tabEqual.TabIndex = 6;
            this.tabEqual.Text = "s_equal";
            this.tabEqual.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tblOnlyInTarget
            // 
            this.tblOnlyInTarget.Detached = false;
            this.tblOnlyInTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblOnlyInTarget.IsContentVisible = false;
            this.tblOnlyInTarget.IsLoadingIcon = false;
            this.tblOnlyInTarget.Location = new System.Drawing.Point(3, 3);
            this.tblOnlyInTarget.MasterFrame = null;
            this.tblOnlyInTarget.Name = "tblOnlyInTarget";
            this.tblOnlyInTarget.Size = new System.Drawing.Size(735, 412);
            this.tblOnlyInTarget.TabIndex = 0;
            this.tblOnlyInTarget.TabularData = null;
            this.tblOnlyInTarget.WinId = null;
            // 
            // tblModified
            // 
            this.tblModified.Detached = false;
            this.tblModified.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblModified.IsContentVisible = false;
            this.tblModified.IsLoadingIcon = false;
            this.tblModified.Location = new System.Drawing.Point(3, 3);
            this.tblModified.MasterFrame = null;
            this.tblModified.Name = "tblModified";
            this.tblModified.Size = new System.Drawing.Size(735, 412);
            this.tblModified.TabIndex = 0;
            this.tblModified.TabularData = null;
            this.tblModified.WinId = null;
            // 
            // tblEqual
            // 
            this.tblEqual.Detached = false;
            this.tblEqual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblEqual.IsContentVisible = false;
            this.tblEqual.IsLoadingIcon = false;
            this.tblEqual.Location = new System.Drawing.Point(3, 3);
            this.tblEqual.MasterFrame = null;
            this.tblEqual.Name = "tblEqual";
            this.tblEqual.Size = new System.Drawing.Size(735, 412);
            this.tblEqual.TabIndex = 0;
            this.tblEqual.TabularData = null;
            this.tblEqual.WinId = null;
            // 
            // DataSynDefItemFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "DataSynDefItemFrame";
            this.Size = new System.Drawing.Size(749, 445);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabSourceAndTarget.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabColumns.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabKey.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabOnlyInSource.ResumeLayout(false);
            this.tabOnlyInTarget.ResumeLayout(false);
            this.tabModified.ResumeLayout(false);
            this.tabEqual.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxSourceTable;
        private System.Windows.Forms.ComboBox cbxSourceView;
        private System.Windows.Forms.RadioButton rbtDefineOwnKey;
        private System.Windows.Forms.RadioButton rbtUsePrimaryKey;
        private System.Windows.Forms.CheckedListBox lbxKeyColumns;
        private System.Windows.Forms.ListBox lbxAllColumns;
        private DatAdmin.DataGridViewEx dataGridView1;
        private System.Windows.Forms.CheckedListBox chbSelectedColumns;
        private System.Windows.Forms.CheckedListBox chbNotSelectedColumns;
        private System.Windows.Forms.DataGridViewComboBoxColumn colSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTarget;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSkip;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDefValue;
        private System.Windows.Forms.CheckedListBox lbxCompareColumns;
        private System.Windows.Forms.RadioButton rbtCompareAllExceptSelected;
        private System.Windows.Forms.RadioButton rbtCompareOnlySelected;
        private System.Windows.Forms.RadioButton rbtCompareAllColumns;
        private System.Windows.Forms.TextBox tbxSourceTableWhere;
        private System.Windows.Forms.TextBox tbxTargetWhere;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabSourceAndTarget;
        private System.Windows.Forms.TabPage tabColumns;
        private System.Windows.Forms.TabPage tabKey;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtView;
        private System.Windows.Forms.RadioButton rbtQuery;
        private System.Windows.Forms.RadioButton rbtTable;
        private System.Windows.Forms.RadioButton rbtNone;
        private System.Windows.Forms.Label label7;
        private DatAdmin.CodeEditor codeEditorQuery;
        private System.Windows.Forms.TextBox tbxSourceViewWhere;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbxTargetTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbtCustomMapping;
        private System.Windows.Forms.RadioButton rbtAllExceptSelected;
        private System.Windows.Forms.RadioButton rbtOnlySelected;
        private System.Windows.Forms.RadioButton rbtAllColumns;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabPage tabOnlyInSource;
        private System.Windows.Forms.TabPage tabOnlyInTarget;
        private System.Windows.Forms.TabPage tabModified;
        private System.Windows.Forms.TabPage tabEqual;
        private DatAdmin.TableDataFrame tblOnlyInSource;
        private DatAdmin.TableDataFrame tblOnlyInTarget;
        private DatAdmin.TableDataFrame tblModified;
        private DatAdmin.TableDataFrame tblEqual;
    }
}
