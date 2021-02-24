namespace DatAdmin
{
    partial class NameTemplateFrame
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
            this.tbxTemplate = new System.Windows.Forms.TextBox();
            this.btnDropDown = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxSample = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // tbxTemplate
            // 
            this.tbxTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxTemplate.Location = new System.Drawing.Point(0, 0);
            this.tbxTemplate.Name = "tbxTemplate";
            this.tbxTemplate.Size = new System.Drawing.Size(224, 20);
            this.tbxTemplate.TabIndex = 0;
            this.tbxTemplate.TextChanged += new System.EventHandler(this.tbxTemplate_TextChanged);
            // 
            // btnDropDown
            // 
            this.btnDropDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDropDown.Image = global::DatAdmin.StdIcons.dropdown;
            this.btnDropDown.Location = new System.Drawing.Point(230, 0);
            this.btnDropDown.Name = "btnDropDown";
            this.btnDropDown.Size = new System.Drawing.Size(22, 22);
            this.btnDropDown.TabIndex = 1;
            this.btnDropDown.UseVisualStyleBackColor = true;
            this.btnDropDown.Click += new System.EventHandler(this.btnDropDown_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "s_sample";
            // 
            // tbxSample
            // 
            this.tbxSample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxSample.Location = new System.Drawing.Point(60, 26);
            this.tbxSample.Name = "tbxSample";
            this.tbxSample.ReadOnly = true;
            this.tbxSample.Size = new System.Drawing.Size(192, 20);
            this.tbxSample.TabIndex = 3;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // NameTemplateFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbxSample);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDropDown);
            this.Controls.Add(this.tbxTemplate);
            this.Name = "NameTemplateFrame";
            this.Size = new System.Drawing.Size(255, 48);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxTemplate;
        private System.Windows.Forms.Button btnDropDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxSample;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}
