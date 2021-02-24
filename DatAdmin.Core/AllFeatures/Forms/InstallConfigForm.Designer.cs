namespace DatAdmin
{
    partial class InstallConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstallConfigForm));
            this.chbAllowUploadStats = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lnkProducts = new System.Windows.Forms.LinkLabel();
            this.grpEval = new System.Windows.Forms.GroupBox();
            this.chbProEval = new System.Windows.Forms.CheckBox();
            this.chbDataSynEval = new System.Windows.Forms.CheckBox();
            this.chbVersionDbEval = new System.Windows.Forms.CheckBox();
            this.lnkPro = new System.Windows.Forms.LinkLabel();
            this.lnkDataSyn = new System.Windows.Forms.LinkLabel();
            this.lnkVersionDb = new System.Windows.Forms.LinkLabel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.rbtProfessional = new System.Windows.Forms.RadioButton();
            this.rbtPersonal = new System.Windows.Forms.RadioButton();
            this.infoBoxFrame1 = new DatAdmin.InfoBoxFrame();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.linkFacebook = new System.Windows.Forms.LinkLabel();
            this.linkTwitter = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.grpEval.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // chbAllowUploadStats
            // 
            this.chbAllowUploadStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbAllowUploadStats.AutoSize = true;
            this.chbAllowUploadStats.Location = new System.Drawing.Point(29, 426);
            this.chbAllowUploadStats.Name = "chbAllowUploadStats";
            this.chbAllowUploadStats.Size = new System.Drawing.Size(162, 17);
            this.chbAllowUploadStats.TabIndex = 0;
            this.chbAllowUploadStats.Text = "s_allow_upload_usage_stats";
            this.chbAllowUploadStats.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.linkTwitter);
            this.groupBox1.Controls.Add(this.linkFacebook);
            this.groupBox1.Controls.Add(this.lnkProducts);
            this.groupBox1.Controls.Add(this.grpEval);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.rbtProfessional);
            this.groupBox1.Controls.Add(this.rbtPersonal);
            this.groupBox1.Location = new System.Drawing.Point(12, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(383, 341);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "s_select_usage";
            // 
            // lnkProducts
            // 
            this.lnkProducts.AutoSize = true;
            this.lnkProducts.Location = new System.Drawing.Point(36, 307);
            this.lnkProducts.Name = "lnkProducts";
            this.lnkProducts.Size = new System.Drawing.Size(103, 13);
            this.lnkProducts.TabIndex = 12;
            this.lnkProducts.TabStop = true;
            this.lnkProducts.Text = "s_products_on_web";
            this.lnkProducts.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkProducts_LinkClicked);
            // 
            // grpEval
            // 
            this.grpEval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpEval.Controls.Add(this.chbProEval);
            this.grpEval.Controls.Add(this.chbDataSynEval);
            this.grpEval.Controls.Add(this.chbVersionDbEval);
            this.grpEval.Controls.Add(this.lnkPro);
            this.grpEval.Controls.Add(this.lnkDataSyn);
            this.grpEval.Controls.Add(this.lnkVersionDb);
            this.grpEval.Location = new System.Drawing.Point(17, 202);
            this.grpEval.Name = "grpEval";
            this.grpEval.Size = new System.Drawing.Size(343, 102);
            this.grpEval.TabIndex = 11;
            this.grpEval.TabStop = false;
            this.grpEval.Text = "s_request_evaluation_license";
            // 
            // chbProEval
            // 
            this.chbProEval.AutoSize = true;
            this.chbProEval.Location = new System.Drawing.Point(22, 26);
            this.chbProEval.Name = "chbProEval";
            this.chbProEval.Size = new System.Drawing.Size(132, 17);
            this.chbProEval.TabIndex = 4;
            this.chbProEval.Text = "DatAdmin Professional";
            this.chbProEval.UseVisualStyleBackColor = true;
            // 
            // chbDataSynEval
            // 
            this.chbDataSynEval.AutoSize = true;
            this.chbDataSynEval.Location = new System.Drawing.Point(22, 49);
            this.chbDataSynEval.Name = "chbDataSynEval";
            this.chbDataSynEval.Size = new System.Drawing.Size(176, 17);
            this.chbDataSynEval.TabIndex = 5;
            this.chbDataSynEval.Text = "DatAdmin Data Synchronization";
            this.chbDataSynEval.UseVisualStyleBackColor = true;
            // 
            // chbVersionDbEval
            // 
            this.chbVersionDbEval.AutoSize = true;
            this.chbVersionDbEval.Location = new System.Drawing.Point(22, 72);
            this.chbVersionDbEval.Name = "chbVersionDbEval";
            this.chbVersionDbEval.Size = new System.Drawing.Size(173, 17);
            this.chbVersionDbEval.TabIndex = 7;
            this.chbVersionDbEval.Text = "DatAdmin Database Versioning";
            this.chbVersionDbEval.UseVisualStyleBackColor = true;
            // 
            // lnkPro
            // 
            this.lnkPro.AutoSize = true;
            this.lnkPro.Location = new System.Drawing.Point(218, 27);
            this.lnkPro.Name = "lnkPro";
            this.lnkPro.Size = new System.Drawing.Size(62, 13);
            this.lnkPro.TabIndex = 8;
            this.lnkPro.TabStop = true;
            this.lnkPro.Text = "s_purchase";
            this.lnkPro.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkPro_LinkClicked);
            // 
            // lnkDataSyn
            // 
            this.lnkDataSyn.AutoSize = true;
            this.lnkDataSyn.Location = new System.Drawing.Point(218, 50);
            this.lnkDataSyn.Name = "lnkDataSyn";
            this.lnkDataSyn.Size = new System.Drawing.Size(62, 13);
            this.lnkDataSyn.TabIndex = 9;
            this.lnkDataSyn.TabStop = true;
            this.lnkDataSyn.Text = "s_purchase";
            this.lnkDataSyn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDataSyn_LinkClicked);
            // 
            // lnkVersionDb
            // 
            this.lnkVersionDb.AutoSize = true;
            this.lnkVersionDb.Location = new System.Drawing.Point(218, 73);
            this.lnkVersionDb.Name = "lnkVersionDb";
            this.lnkVersionDb.Size = new System.Drawing.Size(62, 13);
            this.lnkVersionDb.TabIndex = 10;
            this.lnkVersionDb.TabStop = true;
            this.lnkVersionDb.Text = "s_purchase";
            this.lnkVersionDb.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkVersionDb_LinkClicked);
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(126, 31);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(234, 76);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "s_welcome_personal_info";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(126, 125);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(234, 70);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "s_welcome_professional_info";
            // 
            // rbtProfessional
            // 
            this.rbtProfessional.AutoSize = true;
            this.rbtProfessional.Location = new System.Drawing.Point(17, 125);
            this.rbtProfessional.Name = "rbtProfessional";
            this.rbtProfessional.Size = new System.Drawing.Size(82, 17);
            this.rbtProfessional.TabIndex = 1;
            this.rbtProfessional.TabStop = true;
            this.rbtProfessional.Text = "Professional";
            this.rbtProfessional.UseVisualStyleBackColor = true;
            this.rbtProfessional.CheckedChanged += new System.EventHandler(this.rbtPersonal_CheckedChanged);
            // 
            // rbtPersonal
            // 
            this.rbtPersonal.AutoSize = true;
            this.rbtPersonal.Location = new System.Drawing.Point(17, 31);
            this.rbtPersonal.Name = "rbtPersonal";
            this.rbtPersonal.Size = new System.Drawing.Size(66, 17);
            this.rbtPersonal.TabIndex = 0;
            this.rbtPersonal.TabStop = true;
            this.rbtPersonal.Text = "Personal";
            this.rbtPersonal.UseVisualStyleBackColor = true;
            this.rbtPersonal.CheckedChanged += new System.EventHandler(this.rbtPersonal_CheckedChanged);
            // 
            // infoBoxFrame1
            // 
            this.infoBoxFrame1.Dock = System.Windows.Forms.DockStyle.Top;
            this.infoBoxFrame1.InfoText = "s_welcome_message";
            this.infoBoxFrame1.Location = new System.Drawing.Point(0, 0);
            this.infoBoxFrame1.Name = "infoBoxFrame1";
            this.infoBoxFrame1.Size = new System.Drawing.Size(411, 63);
            this.infoBoxFrame1.TabIndex = 2;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOk.Image = global::DatAdmin.StdIcons.run;
            this.btnOk.Location = new System.Drawing.Point(127, 464);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(131, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "s_continue";
            this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuit.Image = global::DatAdmin.StdIcons.cancel;
            this.btnQuit.Location = new System.Drawing.Point(264, 464);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(131, 23);
            this.btnQuit.TabIndex = 4;
            this.btnQuit.Text = "s_quit";
            this.btnQuit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // linkFacebook
            // 
            this.linkFacebook.AutoSize = true;
            this.linkFacebook.Location = new System.Drawing.Point(194, 307);
            this.linkFacebook.Name = "linkFacebook";
            this.linkFacebook.Size = new System.Drawing.Size(52, 13);
            this.linkFacebook.TabIndex = 13;
            this.linkFacebook.TabStop = true;
            this.linkFacebook.Text = "facebook";
            this.linkFacebook.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkFacebook_LinkClicked);
            // 
            // linkTwitter
            // 
            this.linkTwitter.AutoSize = true;
            this.linkTwitter.Location = new System.Drawing.Point(299, 307);
            this.linkTwitter.Name = "linkTwitter";
            this.linkTwitter.Size = new System.Drawing.Size(35, 13);
            this.linkTwitter.TabIndex = 14;
            this.linkTwitter.TabStop = true;
            this.linkTwitter.Text = "twitter";
            this.linkTwitter.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkTwitter_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DatAdmin.StdIcons.facebook;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(172, 307);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DatAdmin.StdIcons.twitter;
            this.pictureBox2.InitialImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(277, 307);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.TabIndex = 16;
            this.pictureBox2.TabStop = false;
            // 
            // InstallConfigForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOk;
            this.ClientSize = new System.Drawing.Size(411, 499);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.infoBoxFrame1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chbAllowUploadStats);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InstallConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DatAdmin";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpEval.ResumeLayout(false);
            this.grpEval.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbAllowUploadStats;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton rbtProfessional;
        private System.Windows.Forms.RadioButton rbtPersonal;
        private System.Windows.Forms.CheckBox chbVersionDbEval;
        private System.Windows.Forms.CheckBox chbDataSynEval;
        private System.Windows.Forms.CheckBox chbProEval;
        private System.Windows.Forms.TextBox textBox2;
        private InfoBoxFrame infoBoxFrame1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.LinkLabel lnkVersionDb;
        private System.Windows.Forms.LinkLabel lnkDataSyn;
        private System.Windows.Forms.LinkLabel lnkPro;
        private System.Windows.Forms.GroupBox grpEval;
        private System.Windows.Forms.LinkLabel lnkProducts;
        private System.Windows.Forms.LinkLabel linkTwitter;
        private System.Windows.Forms.LinkLabel linkFacebook;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}