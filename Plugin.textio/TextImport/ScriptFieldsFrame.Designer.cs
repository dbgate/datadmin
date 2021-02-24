namespace Plugin.textio
{
    partial class ScriptFieldsFrame
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
            this.infoBoxFrame1 = new DatAdmin.InfoBoxFrame();
            this.SuspendLayout();
            // 
            // codeEditor1
            // 
            this.codeEditor1.Dialect = null;
            this.codeEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeEditor1.IsReadOnly = false;
            this.codeEditor1.Language = DatAdmin.CodeLanguage.Python;
            this.codeEditor1.Location = new System.Drawing.Point(0, 74);
            this.codeEditor1.Modified = true;
            this.codeEditor1.Name = "codeEditor1";
            this.codeEditor1.ReadOnly = false;
            this.codeEditor1.Size = new System.Drawing.Size(297, 103);
            this.codeEditor1.TabIndex = 0;
            this.codeEditor1.TextChanged += new System.EventHandler(this.codeEditor1_TextChanged);
            // 
            // infoBoxFrame1
            // 
            this.infoBoxFrame1.Dock = System.Windows.Forms.DockStyle.Top;
            this.infoBoxFrame1.InfoText = "s_textio_import_python_script_help";
            this.infoBoxFrame1.Location = new System.Drawing.Point(0, 0);
            this.infoBoxFrame1.Name = "infoBoxFrame1";
            this.infoBoxFrame1.Padding = new System.Windows.Forms.Padding(8);
            this.infoBoxFrame1.Size = new System.Drawing.Size(297, 74);
            this.infoBoxFrame1.TabIndex = 1;
            // 
            // ScriptFieldsFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.codeEditor1);
            this.Controls.Add(this.infoBoxFrame1);
            this.Name = "ScriptFieldsFrame";
            this.Size = new System.Drawing.Size(297, 177);
            this.ResumeLayout(false);

        }

        #endregion

        private DatAdmin.CodeEditor codeEditor1;
        private DatAdmin.InfoBoxFrame infoBoxFrame1;
    }
}
