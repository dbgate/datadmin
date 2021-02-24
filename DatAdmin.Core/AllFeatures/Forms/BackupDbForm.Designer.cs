namespace DatAdmin
{
    partial class BackupDbForm
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
            DatAdmin.TreeBehaviour treeBehaviour1 = new DatAdmin.TreeBehaviour();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupDbForm));
            this.wizard1 = new Gui.Wizard.Wizard();
            this.wpProps = new Gui.Wizard.WizardPage();
            this.label2 = new System.Windows.Forms.Label();
            this.treObjects = new DatAdmin.DATreeView();
            this.header1 = new Gui.Wizard.Header();
            this.wpTarget = new Gui.Wizard.WizardPage();
            this.addonSelectFrame1 = new DatAdmin.AddonSelectFrame();
            this.header3 = new Gui.Wizard.Header();
            this.wpFinish = new Gui.Wizard.WizardPage();
            this.btnExportAsJob = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.labProperties = new System.Windows.Forms.TextBox();
            this.labTarget = new System.Windows.Forms.TextBox();
            this.labSource = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.header4 = new Gui.Wizard.Header();
            this.wizard1.SuspendLayout();
            this.wpProps.SuspendLayout();
            this.wpTarget.SuspendLayout();
            this.wpFinish.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizard1
            // 
            this.wizard1.Controls.Add(this.wpTarget);
            this.wizard1.Controls.Add(this.wpFinish);
            this.wizard1.Controls.Add(this.wpProps);
            this.wizard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizard1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wizard1.Location = new System.Drawing.Point(0, 0);
            this.wizard1.Name = "wizard1";
            this.wizard1.PageIndex = 0;
            this.wizard1.Pages.AddRange(new Gui.Wizard.WizardPage[] {
            this.wpTarget,
            this.wpProps,
            this.wpFinish});
            this.wizard1.Size = new System.Drawing.Size(626, 516);
            this.wizard1.TabIndex = 0;
            this.wizard1.OnTranslate += new Gui.Wizard.TranslateEventHandler(this.wizard1_OnTranslate);
            // 
            // wpProps
            // 
            this.wpProps.Controls.Add(this.label2);
            this.wpProps.Controls.Add(this.treObjects);
            this.wpProps.Controls.Add(this.header1);
            this.wpProps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wpProps.IsFinishPage = false;
            this.wpProps.Location = new System.Drawing.Point(0, 0);
            this.wpProps.Name = "wpProps";
            this.wpProps.Size = new System.Drawing.Size(626, 468);
            this.wpProps.TabIndex = 2;
            this.wpProps.ShowFromNext += new System.EventHandler(this.wpProps_ShowFromNext);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "s_choose_objects";
            // 
            // treObjects
            // 
            this.treObjects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.treObjects.Location = new System.Drawing.Point(12, 101);
            this.treObjects.Name = "treObjects";
            this.treObjects.Root = null;
            this.treObjects.RootPath = null;
            this.treObjects.Size = new System.Drawing.Size(282, 350);
            this.treObjects.TabIndex = 3;
            this.treObjects.TreeBehaviour = treeBehaviour1;
            this.treObjects.TreeStyle = DatAdmin.TreeStyle.SelectMany;
            // 
            // header1
            // 
            this.header1.BackColor = System.Drawing.SystemColors.Control;
            this.header1.CausesValidation = false;
            this.header1.Description = "s_configure_copy_properties";
            this.header1.Dock = System.Windows.Forms.DockStyle.Top;
            this.header1.Image = ((System.Drawing.Image)(resources.GetObject("header1.Image")));
            this.header1.Location = new System.Drawing.Point(0, 0);
            this.header1.Name = "header1";
            this.header1.Size = new System.Drawing.Size(626, 64);
            this.header1.TabIndex = 1;
            this.header1.Title = "s_properties";
            // 
            // wpTarget
            // 
            this.wpTarget.Controls.Add(this.addonSelectFrame1);
            this.wpTarget.Controls.Add(this.header3);
            this.wpTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wpTarget.IsFinishPage = false;
            this.wpTarget.Location = new System.Drawing.Point(0, 0);
            this.wpTarget.Name = "wpTarget";
            this.wpTarget.Size = new System.Drawing.Size(626, 468);
            this.wpTarget.TabIndex = 1;
            this.wpTarget.CloseFromNext += new Gui.Wizard.PageEventHandler(this.wpTarget_CloseFromNext);
            // 
            // addonSelectFrame1
            // 
            this.addonSelectFrame1.AddonTypeName = null;
            this.addonSelectFrame1.AllowSwitchDesign = true;
            this.addonSelectFrame1.CanSaveAsTemplate = false;
            this.addonSelectFrame1.CompactDesign = false;
            this.addonSelectFrame1.DefaultAddonHolder = "dbk";
            this.addonSelectFrame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addonSelectFrame1.Location = new System.Drawing.Point(0, 82);
            this.addonSelectFrame1.Name = "addonSelectFrame1";
            this.addonSelectFrame1.ShowInfoFrame = true;
            this.addonSelectFrame1.Size = new System.Drawing.Size(626, 386);
            this.addonSelectFrame1.TabIndex = 3;
            this.addonSelectFrame1.TypeTitle = "s_backup_format";
            this.addonSelectFrame1.ChangedSelectedObject += new System.EventHandler(this.addonSelectFrame1_ChangedSelectedObject);
            this.addonSelectFrame1.FilterAddon += new DatAdmin.FilterAddonEvent(this.addonSelectFrame1_FilterAddon);
            // 
            // header3
            // 
            this.header3.BackColor = System.Drawing.SystemColors.Control;
            this.header3.CausesValidation = false;
            this.header3.Description = "s_backup_target_description";
            this.header3.Dock = System.Windows.Forms.DockStyle.Top;
            this.header3.Image = ((System.Drawing.Image)(resources.GetObject("header3.Image")));
            this.header3.Location = new System.Drawing.Point(0, 0);
            this.header3.Name = "header3";
            this.header3.Size = new System.Drawing.Size(626, 82);
            this.header3.TabIndex = 2;
            this.header3.Title = "s_target";
            // 
            // wpFinish
            // 
            this.wpFinish.Controls.Add(this.btnExportAsJob);
            this.wpFinish.Controls.Add(this.label5);
            this.wpFinish.Controls.Add(this.labProperties);
            this.wpFinish.Controls.Add(this.labTarget);
            this.wpFinish.Controls.Add(this.labSource);
            this.wpFinish.Controls.Add(this.label4);
            this.wpFinish.Controls.Add(this.label3);
            this.wpFinish.Controls.Add(this.header4);
            this.wpFinish.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wpFinish.IsFinishPage = false;
            this.wpFinish.Location = new System.Drawing.Point(0, 0);
            this.wpFinish.Name = "wpFinish";
            this.wpFinish.Size = new System.Drawing.Size(626, 468);
            this.wpFinish.TabIndex = 3;
            this.wpFinish.CloseFromNext += new Gui.Wizard.PageEventHandler(this.wpFinish_CloseFromNext);
            this.wpFinish.ShowFromNext += new System.EventHandler(this.wpFinish_ShowFromNext);
            // 
            // btnExportAsJob
            // 
            this.btnExportAsJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportAsJob.Location = new System.Drawing.Point(411, 423);
            this.btnExportAsJob.Name = "btnExportAsJob";
            this.btnExportAsJob.Size = new System.Drawing.Size(195, 23);
            this.btnExportAsJob.TabIndex = 15;
            this.btnExportAsJob.Text = "s_export_as_job";
            this.btnExportAsJob.UseVisualStyleBackColor = true;
            this.btnExportAsJob.Click += new System.EventHandler(this.btnExportAsJob_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "s_properties";
            // 
            // labProperties
            // 
            this.labProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labProperties.Location = new System.Drawing.Point(108, 165);
            this.labProperties.Multiline = true;
            this.labProperties.Name = "labProperties";
            this.labProperties.ReadOnly = true;
            this.labProperties.Size = new System.Drawing.Size(498, 237);
            this.labProperties.TabIndex = 13;
            // 
            // labTarget
            // 
            this.labTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labTarget.Location = new System.Drawing.Point(108, 109);
            this.labTarget.Name = "labTarget";
            this.labTarget.ReadOnly = true;
            this.labTarget.Size = new System.Drawing.Size(498, 21);
            this.labTarget.TabIndex = 12;
            // 
            // labSource
            // 
            this.labSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labSource.Location = new System.Drawing.Point(108, 81);
            this.labSource.Name = "labSource";
            this.labSource.ReadOnly = true;
            this.labSource.Size = new System.Drawing.Size(498, 21);
            this.labSource.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "s_target";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "s_source";
            // 
            // header4
            // 
            this.header4.BackColor = System.Drawing.SystemColors.Control;
            this.header4.CausesValidation = false;
            this.header4.Description = "s_finish_backupdb_desc";
            this.header4.Dock = System.Windows.Forms.DockStyle.Top;
            this.header4.Image = ((System.Drawing.Image)(resources.GetObject("header4.Image")));
            this.header4.Location = new System.Drawing.Point(0, 0);
            this.header4.Name = "header4";
            this.header4.Size = new System.Drawing.Size(626, 64);
            this.header4.TabIndex = 2;
            this.header4.Title = "s_finish_backupdb";
            // 
            // BackupDbForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 516);
            this.Controls.Add(this.wizard1);
            this.Name = "BackupDbForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "s_backup_db";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BackupDbForm_FormClosed);
            this.wizard1.ResumeLayout(false);
            this.wpProps.ResumeLayout(false);
            this.wpProps.PerformLayout();
            this.wpTarget.ResumeLayout(false);
            this.wpFinish.ResumeLayout(false);
            this.wpFinish.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Gui.Wizard.Wizard wizard1;
        private Gui.Wizard.WizardPage wpTarget;
        private Gui.Wizard.WizardPage wpProps;
        private Gui.Wizard.Header header3;
        private Gui.Wizard.Header header1;
        private Gui.Wizard.WizardPage wpFinish;
        private Gui.Wizard.Header header4;
        private System.Windows.Forms.Label label2;
        private DATreeView treObjects;
        private System.Windows.Forms.Button btnExportAsJob;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox labProperties;
        private System.Windows.Forms.TextBox labTarget;
        private System.Windows.Forms.TextBox labSource;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private AddonSelectFrame addonSelectFrame1;


    }
}