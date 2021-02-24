namespace DatAdmin
{
    partial class SqlFormatPropsFrame
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
            this.chbBinaryStrings = new System.Windows.Forms.CheckBox();
            this.cbxBinaryEncoding = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbtUpperCase = new System.Windows.Forms.RadioButton();
            this.rbtLowerCase = new System.Windows.Forms.RadioButton();
            this.chbQuoteIdentifiers = new System.Windows.Forms.CheckBox();
            this.chbOmitSchema = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chbBinaryStrings
            // 
            this.chbBinaryStrings.AutoSize = true;
            this.chbBinaryStrings.Location = new System.Drawing.Point(4, 4);
            this.chbBinaryStrings.Name = "chbBinaryStrings";
            this.chbBinaryStrings.Size = new System.Drawing.Size(101, 17);
            this.chbBinaryStrings.TabIndex = 0;
            this.chbBinaryStrings.Text = "s_binary_strings";
            this.chbBinaryStrings.UseVisualStyleBackColor = true;
            this.chbBinaryStrings.CheckedChanged += new System.EventHandler(this.Ev_ChangedAnyProperty);
            // 
            // cbxBinaryEncoding
            // 
            this.cbxBinaryEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxBinaryEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBinaryEncoding.Enabled = false;
            this.cbxBinaryEncoding.FormattingEnabled = true;
            this.cbxBinaryEncoding.Location = new System.Drawing.Point(19, 28);
            this.cbxBinaryEncoding.Name = "cbxBinaryEncoding";
            this.cbxBinaryEncoding.Size = new System.Drawing.Size(190, 21);
            this.cbxBinaryEncoding.TabIndex = 1;
            this.cbxBinaryEncoding.SelectedIndexChanged += new System.EventHandler(this.Ev_ChangedAnyProperty);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "s_keywords";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.rbtLowerCase);
            this.panel1.Controls.Add(this.rbtUpperCase);
            this.panel1.Location = new System.Drawing.Point(19, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(190, 25);
            this.panel1.TabIndex = 3;
            // 
            // rbtUpperCase
            // 
            this.rbtUpperCase.AutoSize = true;
            this.rbtUpperCase.Checked = true;
            this.rbtUpperCase.Location = new System.Drawing.Point(3, 5);
            this.rbtUpperCase.Name = "rbtUpperCase";
            this.rbtUpperCase.Size = new System.Drawing.Size(63, 17);
            this.rbtUpperCase.TabIndex = 0;
            this.rbtUpperCase.TabStop = true;
            this.rbtUpperCase.Text = "s_upper";
            this.rbtUpperCase.UseVisualStyleBackColor = true;
            this.rbtUpperCase.CheckedChanged += new System.EventHandler(this.Ev_ChangedAnyProperty);
            // 
            // rbtLowerCase
            // 
            this.rbtLowerCase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbtLowerCase.AutoSize = true;
            this.rbtLowerCase.Location = new System.Drawing.Point(126, 5);
            this.rbtLowerCase.Name = "rbtLowerCase";
            this.rbtLowerCase.Size = new System.Drawing.Size(61, 17);
            this.rbtLowerCase.TabIndex = 1;
            this.rbtLowerCase.Text = "s_lower";
            this.rbtLowerCase.UseVisualStyleBackColor = true;
            this.rbtLowerCase.CheckedChanged += new System.EventHandler(this.Ev_ChangedAnyProperty);
            // 
            // chbQuoteIdentifiers
            // 
            this.chbQuoteIdentifiers.AutoSize = true;
            this.chbQuoteIdentifiers.Checked = true;
            this.chbQuoteIdentifiers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbQuoteIdentifiers.Location = new System.Drawing.Point(4, 100);
            this.chbQuoteIdentifiers.Name = "chbQuoteIdentifiers";
            this.chbQuoteIdentifiers.Size = new System.Drawing.Size(114, 17);
            this.chbQuoteIdentifiers.TabIndex = 4;
            this.chbQuoteIdentifiers.Text = "s_quote_identifiers";
            this.chbQuoteIdentifiers.UseVisualStyleBackColor = true;
            this.chbQuoteIdentifiers.CheckedChanged += new System.EventHandler(this.Ev_ChangedAnyProperty);
            // 
            // chbOmitSchema
            // 
            this.chbOmitSchema.AutoSize = true;
            this.chbOmitSchema.Location = new System.Drawing.Point(4, 125);
            this.chbOmitSchema.Name = "chbOmitSchema";
            this.chbOmitSchema.Size = new System.Drawing.Size(99, 17);
            this.chbOmitSchema.TabIndex = 5;
            this.chbOmitSchema.Text = "s_omit_schema";
            this.chbOmitSchema.UseVisualStyleBackColor = true;
            this.chbOmitSchema.CheckedChanged += new System.EventHandler(this.Ev_ChangedAnyProperty);
            // 
            // SqlFormatPropsFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chbOmitSchema);
            this.Controls.Add(this.chbQuoteIdentifiers);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxBinaryEncoding);
            this.Controls.Add(this.chbBinaryStrings);
            this.Name = "SqlFormatPropsFrame";
            this.Size = new System.Drawing.Size(212, 145);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbBinaryStrings;
        private System.Windows.Forms.ComboBox cbxBinaryEncoding;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbtLowerCase;
        private System.Windows.Forms.RadioButton rbtUpperCase;
        private System.Windows.Forms.CheckBox chbQuoteIdentifiers;
        private System.Windows.Forms.CheckBox chbOmitSchema;
    }
}
