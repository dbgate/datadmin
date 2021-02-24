namespace DatAdmin
{
    partial class SqlDumpWriterEditFrame
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
            this.cbxDialect = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.propertyFrame1 = new DatAdmin.PropertyFrame();
            this.SuspendLayout();
            // 
            // cbxDialect
            // 
            this.cbxDialect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDialect.FormattingEnabled = true;
            this.cbxDialect.Location = new System.Drawing.Point(83, 4);
            this.cbxDialect.Name = "cbxDialect";
            this.cbxDialect.Size = new System.Drawing.Size(167, 21);
            this.cbxDialect.TabIndex = 0;
            this.cbxDialect.SelectedIndexChanged += new System.EventHandler(this.cbxDialect_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "s_dialect";
            // 
            // propertyFrame1
            // 
            this.propertyFrame1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyFrame1.Location = new System.Drawing.Point(3, 39);
            this.propertyFrame1.Name = "propertyFrame1";
            this.propertyFrame1.SelectedObject = null;
            this.propertyFrame1.Size = new System.Drawing.Size(458, 351);
            this.propertyFrame1.TabIndex = 2;
            // 
            // SqlDumpWriterEditFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.propertyFrame1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxDialect);
            this.Name = "SqlDumpWriterEditFrame";
            this.Size = new System.Drawing.Size(464, 390);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxDialect;
        private System.Windows.Forms.Label label1;
        private PropertyFrame propertyFrame1;
    }
}
