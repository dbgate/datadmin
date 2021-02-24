namespace DatAdmin
{
    partial class DataSqlGenColumnsFrame
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnChooseColumns = new System.Windows.Forms.Button();
            this.rbtChooseColumns = new System.Windows.Forms.RadioButton();
            this.rbtAllColumns = new System.Windows.Forms.RadioButton();
            this.rbtNoKeyColumns = new System.Windows.Forms.RadioButton();
            this.rbtPrimaryKey = new System.Windows.Forms.RadioButton();
            this.rbtSelectedColumns = new System.Windows.Forms.RadioButton();
            this.rbtNoSelectedColumns = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtNoSelectedColumns);
            this.groupBox1.Controls.Add(this.btnChooseColumns);
            this.groupBox1.Controls.Add(this.rbtChooseColumns);
            this.groupBox1.Controls.Add(this.rbtAllColumns);
            this.groupBox1.Controls.Add(this.rbtNoKeyColumns);
            this.groupBox1.Controls.Add(this.rbtPrimaryKey);
            this.groupBox1.Controls.Add(this.rbtSelectedColumns);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(145, 190);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // btnChooseColumns
            // 
            this.btnChooseColumns.Enabled = false;
            this.btnChooseColumns.Location = new System.Drawing.Point(23, 157);
            this.btnChooseColumns.Name = "btnChooseColumns";
            this.btnChooseColumns.Size = new System.Drawing.Size(99, 23);
            this.btnChooseColumns.TabIndex = 11;
            this.btnChooseColumns.Text = "s_choose";
            this.btnChooseColumns.UseVisualStyleBackColor = true;
            this.btnChooseColumns.Click += new System.EventHandler(this.btnChooseColumns_Click);
            // 
            // rbtChooseColumns
            // 
            this.rbtChooseColumns.AutoSize = true;
            this.rbtChooseColumns.Location = new System.Drawing.Point(6, 133);
            this.rbtChooseColumns.Name = "rbtChooseColumns";
            this.rbtChooseColumns.Size = new System.Drawing.Size(71, 17);
            this.rbtChooseColumns.TabIndex = 10;
            this.rbtChooseColumns.Text = "s_choose";
            this.rbtChooseColumns.UseVisualStyleBackColor = true;
            this.rbtChooseColumns.CheckedChanged += new System.EventHandler(this.rbtChooseColumns_CheckedChanged);
            // 
            // rbtAllColumns
            // 
            this.rbtAllColumns.AutoSize = true;
            this.rbtAllColumns.Location = new System.Drawing.Point(6, 110);
            this.rbtAllColumns.Name = "rbtAllColumns";
            this.rbtAllColumns.Size = new System.Drawing.Size(91, 17);
            this.rbtAllColumns.TabIndex = 9;
            this.rbtAllColumns.Text = "s_all_columns";
            this.rbtAllColumns.UseVisualStyleBackColor = true;
            this.rbtAllColumns.CheckedChanged += new System.EventHandler(this.rbtSelectedColumns_CheckedChanged);
            // 
            // rbtNoKeyColumns
            // 
            this.rbtNoKeyColumns.AutoSize = true;
            this.rbtNoKeyColumns.Location = new System.Drawing.Point(6, 87);
            this.rbtNoKeyColumns.Name = "rbtNoKeyColumns";
            this.rbtNoKeyColumns.Size = new System.Drawing.Size(116, 17);
            this.rbtNoKeyColumns.TabIndex = 8;
            this.rbtNoKeyColumns.Text = "s_no_key_columns";
            this.rbtNoKeyColumns.UseVisualStyleBackColor = true;
            this.rbtNoKeyColumns.CheckedChanged += new System.EventHandler(this.rbtSelectedColumns_CheckedChanged);
            // 
            // rbtPrimaryKey
            // 
            this.rbtPrimaryKey.AutoSize = true;
            this.rbtPrimaryKey.Location = new System.Drawing.Point(6, 64);
            this.rbtPrimaryKey.Name = "rbtPrimaryKey";
            this.rbtPrimaryKey.Size = new System.Drawing.Size(92, 17);
            this.rbtPrimaryKey.TabIndex = 7;
            this.rbtPrimaryKey.Text = "s_primary_key";
            this.rbtPrimaryKey.UseVisualStyleBackColor = true;
            this.rbtPrimaryKey.CheckedChanged += new System.EventHandler(this.rbtSelectedColumns_CheckedChanged);
            // 
            // rbtSelectedColumns
            // 
            this.rbtSelectedColumns.AutoSize = true;
            this.rbtSelectedColumns.Checked = true;
            this.rbtSelectedColumns.Location = new System.Drawing.Point(6, 18);
            this.rbtSelectedColumns.Name = "rbtSelectedColumns";
            this.rbtSelectedColumns.Size = new System.Drawing.Size(121, 17);
            this.rbtSelectedColumns.TabIndex = 6;
            this.rbtSelectedColumns.TabStop = true;
            this.rbtSelectedColumns.Text = "s_selected_columns";
            this.rbtSelectedColumns.UseVisualStyleBackColor = true;
            this.rbtSelectedColumns.CheckedChanged += new System.EventHandler(this.rbtSelectedColumns_CheckedChanged);
            // 
            // rbtNoSelectedColumns
            // 
            this.rbtNoSelectedColumns.AutoSize = true;
            this.rbtNoSelectedColumns.Location = new System.Drawing.Point(6, 41);
            this.rbtNoSelectedColumns.Name = "rbtNoSelectedColumns";
            this.rbtNoSelectedColumns.Size = new System.Drawing.Size(139, 17);
            this.rbtNoSelectedColumns.TabIndex = 12;
            this.rbtNoSelectedColumns.TabStop = true;
            this.rbtNoSelectedColumns.Text = "s_no_selected_columns";
            this.rbtNoSelectedColumns.UseVisualStyleBackColor = true;
            this.rbtNoSelectedColumns.CheckedChanged += new System.EventHandler(this.rbtSelectedColumns_CheckedChanged);
            // 
            // DataSqlGenColumnsFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "DataSqlGenColumnsFrame";
            this.Size = new System.Drawing.Size(145, 190);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnChooseColumns;
        private System.Windows.Forms.RadioButton rbtChooseColumns;
        private System.Windows.Forms.RadioButton rbtAllColumns;
        private System.Windows.Forms.RadioButton rbtNoKeyColumns;
        private System.Windows.Forms.RadioButton rbtPrimaryKey;
        private System.Windows.Forms.RadioButton rbtSelectedColumns;
        private System.Windows.Forms.RadioButton rbtNoSelectedColumns;

    }
}
