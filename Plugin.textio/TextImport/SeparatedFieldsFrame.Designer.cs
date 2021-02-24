namespace Plugin.textio
{
    partial class SeparatedFieldsFrame
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbxSeparator = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.chbIsRegex = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colField = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxColName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "s_separator";
            // 
            // tbxSeparator
            // 
            this.tbxSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxSeparator.Location = new System.Drawing.Point(94, 4);
            this.tbxSeparator.Name = "tbxSeparator";
            this.tbxSeparator.Size = new System.Drawing.Size(257, 20);
            this.tbxSeparator.TabIndex = 1;
            this.tbxSeparator.TextChanged += new System.EventHandler(this.tbxSeparator_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(-15, -15);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 17);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // chbIsRegex
            // 
            this.chbIsRegex.AutoSize = true;
            this.chbIsRegex.Location = new System.Drawing.Point(3, 20);
            this.chbIsRegex.Name = "chbIsRegex";
            this.chbIsRegex.Size = new System.Drawing.Size(86, 17);
            this.chbIsRegex.TabIndex = 3;
            this.chbIsRegex.Text = "s_use_regex";
            this.chbIsRegex.UseVisualStyleBackColor = true;
            this.chbIsRegex.CheckedChanged += new System.EventHandler(this.tbxSeparator_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colField,
            this.colPosition});
            this.dataGridView1.Location = new System.Drawing.Point(7, 85);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(344, 96);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // colField
            // 
            this.colField.HeaderText = "s_field";
            this.colField.Name = "colField";
            // 
            // colPosition
            // 
            this.colPosition.HeaderText = "s_position";
            this.colPosition.Name = "colPosition";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "s_column_name";
            // 
            // tbxColName
            // 
            this.tbxColName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxColName.Location = new System.Drawing.Point(94, 51);
            this.tbxColName.Name = "tbxColName";
            this.tbxColName.Size = new System.Drawing.Size(257, 20);
            this.tbxColName.TabIndex = 6;
            this.tbxColName.Text = "Column#COLINDEX#";
            this.tbxColName.TextChanged += new System.EventHandler(this.tbxSeparator_TextChanged);
            // 
            // SeparatedFieldsFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbxColName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.chbIsRegex);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.tbxSeparator);
            this.Controls.Add(this.label1);
            this.Name = "SeparatedFieldsFrame";
            this.Size = new System.Drawing.Size(354, 184);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxSeparator;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox chbIsRegex;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colField;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPosition;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxColName;
    }
}
