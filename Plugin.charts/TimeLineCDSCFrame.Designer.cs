namespace Plugin.charts
{
    partial class TimeLineCDSCFrame
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
            this.cbxStep = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxColumn = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.chbAdvanced = new System.Windows.Forms.CheckBox();
            this.panAdvanced = new System.Windows.Forms.Panel();
            this.cbxStep2 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panAdvanced.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "s_step";
            // 
            // cbxStep
            // 
            this.cbxStep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStep.FormattingEnabled = true;
            this.cbxStep.Location = new System.Drawing.Point(110, 0);
            this.cbxStep.Name = "cbxStep";
            this.cbxStep.Size = new System.Drawing.Size(173, 21);
            this.cbxStep.TabIndex = 1;
            this.cbxStep.SelectedIndexChanged += new System.EventHandler(this.cbxStep_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "s_column_with_date";
            // 
            // cbxColumn
            // 
            this.cbxColumn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxColumn.FormattingEnabled = true;
            this.cbxColumn.Location = new System.Drawing.Point(110, 28);
            this.cbxColumn.Name = "cbxColumn";
            this.cbxColumn.Size = new System.Drawing.Size(173, 21);
            this.cbxColumn.TabIndex = 3;
            this.cbxColumn.SelectedIndexChanged += new System.EventHandler(this.cbxColumn_SelectedIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::Plugin.charts.StdIcons.add;
            this.btnAdd.Location = new System.Drawing.Point(6, 172);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "s_add";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // chbAdvanced
            // 
            this.chbAdvanced.AutoSize = true;
            this.chbAdvanced.Location = new System.Drawing.Point(7, 59);
            this.chbAdvanced.Name = "chbAdvanced";
            this.chbAdvanced.Size = new System.Drawing.Size(109, 17);
            this.chbAdvanced.TabIndex = 6;
            this.chbAdvanced.Text = "s_structured_time";
            this.chbAdvanced.UseVisualStyleBackColor = true;
            this.chbAdvanced.CheckedChanged += new System.EventHandler(this.chbAdvanced_CheckedChanged);
            // 
            // panAdvanced
            // 
            this.panAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panAdvanced.Controls.Add(this.cbxStep2);
            this.panAdvanced.Controls.Add(this.label3);
            this.panAdvanced.Location = new System.Drawing.Point(3, 82);
            this.panAdvanced.Name = "panAdvanced";
            this.panAdvanced.Size = new System.Drawing.Size(279, 25);
            this.panAdvanced.TabIndex = 7;
            // 
            // cbxStep2
            // 
            this.cbxStep2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxStep2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStep2.FormattingEnabled = true;
            this.cbxStep2.Location = new System.Drawing.Point(107, 3);
            this.cbxStep2.Name = "cbxStep2";
            this.cbxStep2.Size = new System.Drawing.Size(169, 21);
            this.cbxStep2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "s_date_element";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(3, 113);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(276, 53);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "s_charts";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(4, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(266, 27);
            this.panel1.TabIndex = 0;
            // 
            // TimeLineCDSCFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panAdvanced);
            this.Controls.Add(this.chbAdvanced);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cbxColumn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxStep);
            this.Controls.Add(this.label1);
            this.Name = "TimeLineCDSCFrame";
            this.Size = new System.Drawing.Size(283, 227);
            this.panAdvanced.ResumeLayout(false);
            this.panAdvanced.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxStep;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxColumn;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.CheckBox chbAdvanced;
        private System.Windows.Forms.Panel panAdvanced;
        private System.Windows.Forms.ComboBox cbxStep2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
    }
}
