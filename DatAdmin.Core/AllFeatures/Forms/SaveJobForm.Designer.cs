namespace DatAdmin
{
    partial class SaveJobForm
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
            this.rbtCreateNewJob = new System.Windows.Forms.RadioButton();
            this.rbtAppendToExistingJob = new System.Windows.Forms.RadioButton();
            this.tbxJob = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbxJobs = new System.Windows.Forms.ListBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chbAddToFavorites = new System.Windows.Forms.CheckBox();
            this.addToFavoritesFrame1 = new DatAdmin.AddToFavoritesFrame();
            this.SuspendLayout();
            // 
            // rbtCreateNewJob
            // 
            this.rbtCreateNewJob.AutoSize = true;
            this.rbtCreateNewJob.Checked = true;
            this.rbtCreateNewJob.Location = new System.Drawing.Point(12, 12);
            this.rbtCreateNewJob.Name = "rbtCreateNewJob";
            this.rbtCreateNewJob.Size = new System.Drawing.Size(112, 17);
            this.rbtCreateNewJob.TabIndex = 0;
            this.rbtCreateNewJob.TabStop = true;
            this.rbtCreateNewJob.Text = "s_create_new_job";
            this.rbtCreateNewJob.UseVisualStyleBackColor = true;
            this.rbtCreateNewJob.CheckedChanged += new System.EventHandler(this.rbtCreateNewJob_CheckedChanged);
            // 
            // rbtAppendToExistingJob
            // 
            this.rbtAppendToExistingJob.AutoSize = true;
            this.rbtAppendToExistingJob.Location = new System.Drawing.Point(12, 61);
            this.rbtAppendToExistingJob.Name = "rbtAppendToExistingJob";
            this.rbtAppendToExistingJob.Size = new System.Drawing.Size(148, 17);
            this.rbtAppendToExistingJob.TabIndex = 1;
            this.rbtAppendToExistingJob.Text = "s_append_to_existing_job";
            this.rbtAppendToExistingJob.UseVisualStyleBackColor = true;
            this.rbtAppendToExistingJob.CheckedChanged += new System.EventHandler(this.rbtCreateNewJob_CheckedChanged);
            // 
            // tbxJob
            // 
            this.tbxJob.Location = new System.Drawing.Point(122, 35);
            this.tbxJob.Name = "tbxJob";
            this.tbxJob.Size = new System.Drawing.Size(204, 20);
            this.tbxJob.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "s_job_name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "s_select_job";
            // 
            // lbxJobs
            // 
            this.lbxJobs.Enabled = false;
            this.lbxJobs.FormattingEnabled = true;
            this.lbxJobs.Location = new System.Drawing.Point(122, 90);
            this.lbxJobs.Name = "lbxJobs";
            this.lbxJobs.Size = new System.Drawing.Size(205, 134);
            this.lbxJobs.TabIndex = 5;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(170, 326);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "s_ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(252, 326);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "s_cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // chbAddToFavorites
            // 
            this.chbAddToFavorites.AutoSize = true;
            this.chbAddToFavorites.Location = new System.Drawing.Point(12, 238);
            this.chbAddToFavorites.Name = "chbAddToFavorites";
            this.chbAddToFavorites.Size = new System.Drawing.Size(116, 17);
            this.chbAddToFavorites.TabIndex = 8;
            this.chbAddToFavorites.Text = "s_add_to_favorites";
            this.chbAddToFavorites.UseVisualStyleBackColor = true;
            this.chbAddToFavorites.CheckedChanged += new System.EventHandler(this.chbAddToFavorites_CheckedChanged);
            // 
            // addToFavoritesFrame1
            // 
            this.addToFavoritesFrame1.Enabled = false;
            this.addToFavoritesFrame1.Location = new System.Drawing.Point(38, 261);
            this.addToFavoritesFrame1.Name = "addToFavoritesFrame1";
            this.addToFavoritesFrame1.Size = new System.Drawing.Size(291, 49);
            this.addToFavoritesFrame1.TabIndex = 9;
            // 
            // SaveJobForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(341, 361);
            this.Controls.Add(this.addToFavoritesFrame1);
            this.Controls.Add(this.chbAddToFavorites);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lbxJobs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxJob);
            this.Controls.Add(this.rbtAppendToExistingJob);
            this.Controls.Add(this.rbtCreateNewJob);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SaveJobForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "s_save_job";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtCreateNewJob;
        private System.Windows.Forms.RadioButton rbtAppendToExistingJob;
        private System.Windows.Forms.TextBox tbxJob;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbxJobs;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chbAddToFavorites;
        private AddToFavoritesFrame addToFavoritesFrame1;
    }
}