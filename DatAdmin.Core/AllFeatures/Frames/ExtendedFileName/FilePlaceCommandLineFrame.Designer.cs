namespace DatAdmin
{
    partial class FilePlaceCommandLineFrame
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
            this.btnBrowseApp = new System.Windows.Forms.Button();
            this.tbxArguments = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxApplication = new System.Windows.Forms.TextBox();
            this.openFileDialogExe = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnBrowseApp
            // 
            this.btnBrowseApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseApp.Location = new System.Drawing.Point(283, 4);
            this.btnBrowseApp.Name = "btnBrowseApp";
            this.btnBrowseApp.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseApp.TabIndex = 16;
            this.btnBrowseApp.Text = "s_browse";
            this.btnBrowseApp.UseVisualStyleBackColor = true;
            this.btnBrowseApp.Click += new System.EventHandler(this.btnBrowseApp_Click);
            // 
            // tbxArguments
            // 
            this.tbxArguments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxArguments.Location = new System.Drawing.Point(78, 33);
            this.tbxArguments.Name = "tbxArguments";
            this.tbxArguments.Size = new System.Drawing.Size(280, 20);
            this.tbxArguments.TabIndex = 15;
            this.tbxArguments.Text = "#FILE#";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "s_arguments";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "s_application";
            // 
            // tbxApplication
            // 
            this.tbxApplication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxApplication.Location = new System.Drawing.Point(78, 6);
            this.tbxApplication.Name = "tbxApplication";
            this.tbxApplication.Size = new System.Drawing.Size(199, 20);
            this.tbxApplication.TabIndex = 12;
            // 
            // FilePlaceCommandLineFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnBrowseApp);
            this.Controls.Add(this.tbxArguments);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxApplication);
            this.Name = "FilePlaceCommandLineFrame";
            this.Size = new System.Drawing.Size(371, 59);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowseApp;
        private System.Windows.Forms.TextBox tbxArguments;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxApplication;
        private System.Windows.Forms.OpenFileDialog openFileDialogExe;
    }
}
