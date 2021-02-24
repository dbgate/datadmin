namespace DatAdmin
{
    partial class TemplateTextForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnInsertNewline = new System.Windows.Forms.Button();
            this.btnInsertValue = new System.Windows.Forms.Button();
            this.btnInsertName = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "s_template";
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox1.Location = new System.Drawing.Point(12, 25);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(294, 353);
            this.textBox1.TabIndex = 3;
            this.textBox1.WordWrap = false;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(263, 385);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "s_ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(344, 385);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "s_cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnInsertNewline
            // 
            this.btnInsertNewline.Location = new System.Drawing.Point(312, 25);
            this.btnInsertNewline.Name = "btnInsertNewline";
            this.btnInsertNewline.Size = new System.Drawing.Size(107, 23);
            this.btnInsertNewline.TabIndex = 6;
            this.btnInsertNewline.Text = "s_insert_newline";
            this.btnInsertNewline.UseVisualStyleBackColor = true;
            this.btnInsertNewline.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // btnInsertValue
            // 
            this.btnInsertValue.Location = new System.Drawing.Point(312, 55);
            this.btnInsertValue.Name = "btnInsertValue";
            this.btnInsertValue.Size = new System.Drawing.Size(107, 23);
            this.btnInsertValue.TabIndex = 7;
            this.btnInsertValue.Text = "s_insert_value";
            this.btnInsertValue.UseVisualStyleBackColor = true;
            this.btnInsertValue.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnInsertName
            // 
            this.btnInsertName.Location = new System.Drawing.Point(313, 85);
            this.btnInsertName.Name = "btnInsertName";
            this.btnInsertName.Size = new System.Drawing.Size(106, 23);
            this.btnInsertName.TabIndex = 8;
            this.btnInsertName.Text = "s_insert_name";
            this.btnInsertName.UseVisualStyleBackColor = true;
            this.btnInsertName.Click += new System.EventHandler(this.button2_Click);
            // 
            // TemplateTextForm
            // 
            this.AcceptButton = this.btnOk;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(431, 412);
            this.Controls.Add(this.btnInsertName);
            this.Controls.Add(this.btnInsertValue);
            this.Controls.Add(this.btnInsertNewline);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "TemplateTextForm";
            this.Text = "s_template_text";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnInsertNewline;
        private System.Windows.Forms.Button btnInsertValue;
        private System.Windows.Forms.Button btnInsertName;
    }
}
