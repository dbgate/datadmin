namespace Plugin.textio
{
    partial class CodeWriterFrame
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
            this.codeEditor1 = new DatAdmin.CodeEditor();
            this.SuspendLayout();
            // 
            // codeEditor1
            // 
            this.codeEditor1.Dialect = null;
            this.codeEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeEditor1.IsReadOnly = false;
            this.codeEditor1.Language = DatAdmin.CodeLanguage.None;
            this.codeEditor1.Location = new System.Drawing.Point(0, 0);
            this.codeEditor1.Modified = true;
            this.codeEditor1.Name = "codeEditor1";
            this.codeEditor1.ReadOnly = false;
            this.codeEditor1.Size = new System.Drawing.Size(400, 307);
            this.codeEditor1.TabIndex = 0;
            this.codeEditor1.TextChanged += new System.EventHandler(this.codeEditor1_TextChanged);
            // 
            // CodeWriterFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.codeEditor1);
            this.Name = "CodeWriterFrame";
            this.Size = new System.Drawing.Size(400, 307);
            this.ResumeLayout(false);

        }

        #endregion

        private DatAdmin.CodeEditor codeEditor1;
    }
}
