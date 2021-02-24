namespace DatAdmin
{
    partial class FilePlaceFileSystemTemplateFrame
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
            this.tbxFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.nameTemplateFrame1 = new DatAdmin.NameTemplateFrame();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "s_folder";
            // 
            // tbxFolder
            // 
            this.tbxFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxFolder.Location = new System.Drawing.Point(75, 4);
            this.tbxFolder.Name = "tbxFolder";
            this.tbxFolder.Size = new System.Drawing.Size(365, 20);
            this.tbxFolder.TabIndex = 2;
            this.tbxFolder.TextChanged += new System.EventHandler(this.tbxFolder_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "s_file";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(446, 3);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(23, 23);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // nameTemplateFrame1
            // 
            this.nameTemplateFrame1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.nameTemplateFrame1.Location = new System.Drawing.Point(75, 30);
            this.nameTemplateFrame1.Name = "nameTemplateFrame1";
            this.nameTemplateFrame1.NameTemplate = "";
            this.nameTemplateFrame1.Size = new System.Drawing.Size(397, 48);
            this.nameTemplateFrame1.TabIndex = 0;
            this.nameTemplateFrame1.ChangedNameTemplate += new System.EventHandler(this.nameTemplateFrame1_ChangedNameTemplate);
            // 
            // FilePlaceFileSystemTemplateFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nameTemplateFrame1);
            this.Name = "FilePlaceFileSystemTemplateFrame";
            this.Size = new System.Drawing.Size(472, 83);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DatAdmin.NameTemplateFrame nameTemplateFrame1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}
