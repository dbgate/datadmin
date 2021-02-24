namespace DatAdmin
{
    partial class TabularDataNewTableFrame
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
            this.tableFromTabularDataFrame1 = new DatAdmin.TableFromTabularDataFrame();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxDatabase = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableFromTabularDataFrame1
            // 
            this.tableFromTabularDataFrame1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableFromTabularDataFrame1.Location = new System.Drawing.Point(9, 19);
            this.tableFromTabularDataFrame1.Name = "tableFromTabularDataFrame1";
            this.tableFromTabularDataFrame1.Size = new System.Drawing.Size(232, 107);
            this.tableFromTabularDataFrame1.TabIndex = 0;
            this.tableFromTabularDataFrame1.ChangedProperties += new System.EventHandler(this.tableFromTabularDataFrame1_ChangedProperties);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "s_database";
            // 
            // tbxDatabase
            // 
            this.tbxDatabase.Location = new System.Drawing.Point(15, 21);
            this.tbxDatabase.Name = "tbxDatabase";
            this.tbxDatabase.ReadOnly = true;
            this.tbxDatabase.Size = new System.Drawing.Size(257, 20);
            this.tbxDatabase.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tableFromTabularDataFrame1);
            this.groupBox1.Location = new System.Drawing.Point(15, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 132);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "s_new_table";
            // 
            // TabularDataNewTableFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbxDatabase);
            this.Controls.Add(this.label1);
            this.Name = "TabularDataNewTableFrame";
            this.Size = new System.Drawing.Size(281, 376);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TableFromTabularDataFrame tableFromTabularDataFrame1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxDatabase;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
