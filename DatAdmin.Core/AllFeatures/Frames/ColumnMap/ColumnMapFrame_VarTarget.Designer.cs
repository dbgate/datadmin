namespace DatAdmin
{
    partial class ColumnMapFrame_VarTarget
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
            this.lbsource = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.lbtarget = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.lbtarget)).BeginInit();
            this.SuspendLayout();
            // 
            // lbsource
            // 
            this.lbsource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lbsource.FormattingEnabled = true;
            this.lbsource.Location = new System.Drawing.Point(18, 38);
            this.lbsource.Name = "lbsource";
            this.lbsource.Size = new System.Drawing.Size(158, 212);
            this.lbsource.TabIndex = 0;
            this.lbsource.DoubleClick += new System.EventHandler(this.button1_Click);
            this.lbsource.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbsource_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "s_source";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(283, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "s_target";
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::DatAdmin.StdIcons.add;
            this.btnAdd.Location = new System.Drawing.Point(182, 38);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(98, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "s_add";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Image = global::DatAdmin.StdIcons.remove;
            this.btnRemove.Location = new System.Drawing.Point(182, 67);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(98, 23);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.Text = "s_remove";
            this.btnRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnUp
            // 
            this.btnUp.Image = global::DatAdmin.StdIcons.up1;
            this.btnUp.Location = new System.Drawing.Point(183, 111);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(98, 23);
            this.btnUp.TabIndex = 6;
            this.btnUp.Text = "s_up";
            this.btnUp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnDown
            // 
            this.btnDown.Image = global::DatAdmin.StdIcons.down1;
            this.btnDown.Location = new System.Drawing.Point(183, 140);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(98, 23);
            this.btnDown.TabIndex = 7;
            this.btnDown.Text = "s_down";
            this.btnDown.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.button4_Click);
            // 
            // lbtarget
            // 
            this.lbtarget.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbtarget.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lbtarget.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colType,
            this.colValue});
            this.lbtarget.Location = new System.Drawing.Point(287, 38);
            this.lbtarget.Name = "lbtarget";
            this.lbtarget.Size = new System.Drawing.Size(383, 219);
            this.lbtarget.TabIndex = 8;
            // 
            // colName
            // 
            this.colName.HeaderText = "s_name";
            this.colName.Name = "colName";
            // 
            // colType
            // 
            this.colType.HeaderText = "s_type";
            this.colType.Name = "colType";
            this.colType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colType.Width = 120;
            // 
            // colValue
            // 
            this.colValue.HeaderText = "s_value";
            this.colValue.Name = "colValue";
            // 
            // ColumnMapFrame_VarTarget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbtarget);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbsource);
            this.Name = "ColumnMapFrame_VarTarget";
            this.Size = new System.Drawing.Size(683, 271);
            ((System.ComponentModel.ISupportInitialize)(this.lbtarget)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbsource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.DataGridView lbtarget;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewComboBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
    }
}
