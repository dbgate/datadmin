namespace DatAdmin
{
    partial class BulkCopyWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BulkCopyWizard));
            this.wizard1 = new Gui.Wizard.Wizard();
            this.wpsource = new Gui.Wizard.WizardPage();
            this.source = new DatAdmin.DataStoreFrame();
            this.header2 = new Gui.Wizard.Header();
            this.wptarget = new Gui.Wizard.WizardPage();
            this.target = new DatAdmin.DataStoreFrame();
            this.header3 = new Gui.Wizard.Header();
            this.wpcolmap = new Gui.Wizard.WizardPage();
            this.columnMapFrame1 = new DatAdmin.ColumnMapFrame();
            this.header4 = new Gui.Wizard.Header();
            this.wpfinish = new Gui.Wizard.WizardPage();
            this.tableCopyOptionsFrame1 = new DatAdmin.TableCopyOptionsFrame();
            this.labTarget = new System.Windows.Forms.TextBox();
            this.labSource = new System.Windows.Forms.TextBox();
            this.btnExportAsJob = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.header1 = new Gui.Wizard.Header();
            this.wizard1.SuspendLayout();
            this.wpsource.SuspendLayout();
            this.wptarget.SuspendLayout();
            this.wpcolmap.SuspendLayout();
            this.wpfinish.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizard1
            // 
            this.wizard1.Controls.Add(this.wptarget);
            this.wizard1.Controls.Add(this.wpsource);
            this.wizard1.Controls.Add(this.wpcolmap);
            this.wizard1.Controls.Add(this.wpfinish);
            this.wizard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizard1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wizard1.Location = new System.Drawing.Point(0, 0);
            this.wizard1.Name = "wizard1";
            this.wizard1.PageIndex = 1;
            this.wizard1.Pages.AddRange(new Gui.Wizard.WizardPage[] {
            this.wpsource,
            this.wptarget,
            this.wpcolmap,
            this.wpfinish});
            this.wizard1.Size = new System.Drawing.Size(729, 532);
            this.wizard1.TabIndex = 0;
            this.wizard1.OnTranslate += new Gui.Wizard.TranslateEventHandler(this.wizard1_OnTranslate);
            // 
            // wpsource
            // 
            this.wpsource.Controls.Add(this.source);
            this.wpsource.Controls.Add(this.header2);
            this.wpsource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wpsource.IsFinishPage = false;
            this.wpsource.Location = new System.Drawing.Point(0, 0);
            this.wpsource.Name = "wpsource";
            this.wpsource.Size = new System.Drawing.Size(729, 484);
            this.wpsource.TabIndex = 1;
            this.wpsource.CloseFromNext += new Gui.Wizard.PageEventHandler(this.wpsource_CloseFromNext);
            // 
            // source
            // 
            this.source.CompactDesign = false;
            this.source.DataStoreMode = DatAdmin.TabularDataStoreMode.Read;
            this.source.Dock = System.Windows.Forms.DockStyle.Fill;
            this.source.Location = new System.Drawing.Point(0, 64);
            this.source.Name = "source";
            this.source.Size = new System.Drawing.Size(729, 420);
            this.source.TabIndex = 1;
            // 
            // header2
            // 
            this.header2.BackColor = System.Drawing.SystemColors.Control;
            this.header2.CausesValidation = false;
            this.header2.Description = "s_configure_source_properties";
            this.header2.Dock = System.Windows.Forms.DockStyle.Top;
            this.header2.Image = ((System.Drawing.Image)(resources.GetObject("header2.Image")));
            this.header2.Location = new System.Drawing.Point(0, 0);
            this.header2.Name = "header2";
            this.header2.Size = new System.Drawing.Size(729, 64);
            this.header2.TabIndex = 0;
            this.header2.Title = "s_source";
            // 
            // wptarget
            // 
            this.wptarget.Controls.Add(this.target);
            this.wptarget.Controls.Add(this.header3);
            this.wptarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wptarget.IsFinishPage = false;
            this.wptarget.Location = new System.Drawing.Point(0, 0);
            this.wptarget.Name = "wptarget";
            this.wptarget.Size = new System.Drawing.Size(729, 484);
            this.wptarget.TabIndex = 2;
            this.wptarget.CloseFromNext += new Gui.Wizard.PageEventHandler(this.wptarget_CloseFromNext);
            this.wptarget.ShowFromNext += new System.EventHandler(this.wptarget_ShowFromNext);
            // 
            // target
            // 
            this.target.CompactDesign = false;
            this.target.DataStoreMode = DatAdmin.TabularDataStoreMode.Write;
            this.target.Dock = System.Windows.Forms.DockStyle.Fill;
            this.target.Location = new System.Drawing.Point(0, 64);
            this.target.Name = "target";
            this.target.Size = new System.Drawing.Size(729, 420);
            this.target.TabIndex = 1;
            // 
            // header3
            // 
            this.header3.BackColor = System.Drawing.SystemColors.Control;
            this.header3.CausesValidation = false;
            this.header3.Description = "s_configure_target_properties";
            this.header3.Dock = System.Windows.Forms.DockStyle.Top;
            this.header3.Image = ((System.Drawing.Image)(resources.GetObject("header3.Image")));
            this.header3.Location = new System.Drawing.Point(0, 0);
            this.header3.Name = "header3";
            this.header3.Size = new System.Drawing.Size(729, 64);
            this.header3.TabIndex = 0;
            this.header3.Title = "s_target";
            // 
            // wpcolmap
            // 
            this.wpcolmap.Controls.Add(this.columnMapFrame1);
            this.wpcolmap.Controls.Add(this.header4);
            this.wpcolmap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wpcolmap.IsFinishPage = false;
            this.wpcolmap.Location = new System.Drawing.Point(0, 0);
            this.wpcolmap.Name = "wpcolmap";
            this.wpcolmap.Size = new System.Drawing.Size(729, 484);
            this.wpcolmap.TabIndex = 5;
            this.wpcolmap.CloseFromNext += new Gui.Wizard.PageEventHandler(this.wpcolmap_CloseFromNext);
            this.wpcolmap.ShowFromNext += new System.EventHandler(this.wpcolmap_ShowFromNext);
            // 
            // columnMapFrame1
            // 
            this.columnMapFrame1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.columnMapFrame1.Location = new System.Drawing.Point(12, 70);
            this.columnMapFrame1.Name = "columnMapFrame1";
            this.columnMapFrame1.Size = new System.Drawing.Size(714, 400);
            this.columnMapFrame1.TabIndex = 1;
            // 
            // header4
            // 
            this.header4.BackColor = System.Drawing.SystemColors.Control;
            this.header4.CausesValidation = false;
            this.header4.Description = "s_column_mapping_description";
            this.header4.Dock = System.Windows.Forms.DockStyle.Top;
            this.header4.Image = ((System.Drawing.Image)(resources.GetObject("header4.Image")));
            this.header4.Location = new System.Drawing.Point(0, 0);
            this.header4.Name = "header4";
            this.header4.Size = new System.Drawing.Size(729, 64);
            this.header4.TabIndex = 0;
            this.header4.Title = "s_column_mapping";
            // 
            // wpfinish
            // 
            this.wpfinish.Controls.Add(this.tableCopyOptionsFrame1);
            this.wpfinish.Controls.Add(this.labTarget);
            this.wpfinish.Controls.Add(this.labSource);
            this.wpfinish.Controls.Add(this.btnExportAsJob);
            this.wpfinish.Controls.Add(this.label2);
            this.wpfinish.Controls.Add(this.label1);
            this.wpfinish.Controls.Add(this.header1);
            this.wpfinish.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wpfinish.IsFinishPage = true;
            this.wpfinish.Location = new System.Drawing.Point(0, 0);
            this.wpfinish.Name = "wpfinish";
            this.wpfinish.Size = new System.Drawing.Size(729, 484);
            this.wpfinish.TabIndex = 6;
            this.wpfinish.CloseFromNext += new Gui.Wizard.PageEventHandler(this.wpfinish_CloseFromNext);
            this.wpfinish.ShowFromNext += new System.EventHandler(this.wpfinish_ShowFromNext);
            // 
            // tableCopyOptionsFrame1
            // 
            this.tableCopyOptionsFrame1.Location = new System.Drawing.Point(29, 163);
            this.tableCopyOptionsFrame1.Name = "tableCopyOptionsFrame1";
            this.tableCopyOptionsFrame1.Size = new System.Drawing.Size(243, 94);
            this.tableCopyOptionsFrame1.TabIndex = 6;
            // 
            // labTarget
            // 
            this.labTarget.Location = new System.Drawing.Point(122, 110);
            this.labTarget.Name = "labTarget";
            this.labTarget.ReadOnly = true;
            this.labTarget.Size = new System.Drawing.Size(377, 21);
            this.labTarget.TabIndex = 5;
            // 
            // labSource
            // 
            this.labSource.Location = new System.Drawing.Point(122, 83);
            this.labSource.Name = "labSource";
            this.labSource.ReadOnly = true;
            this.labSource.Size = new System.Drawing.Size(377, 21);
            this.labSource.TabIndex = 4;
            // 
            // btnExportAsJob
            // 
            this.btnExportAsJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportAsJob.Image = global::DatAdmin.StdIcons.job;
            this.btnExportAsJob.Location = new System.Drawing.Point(519, 441);
            this.btnExportAsJob.Name = "btnExportAsJob";
            this.btnExportAsJob.Size = new System.Drawing.Size(198, 23);
            this.btnExportAsJob.TabIndex = 3;
            this.btnExportAsJob.Text = "s_export_as_job";
            this.btnExportAsJob.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExportAsJob.UseVisualStyleBackColor = true;
            this.btnExportAsJob.Click += new System.EventHandler(this.btnExportAsJob_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "s_target";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "s_source";
            // 
            // header1
            // 
            this.header1.BackColor = System.Drawing.SystemColors.Control;
            this.header1.CausesValidation = false;
            this.header1.Description = "s_finish_bulkcopy_desc";
            this.header1.Dock = System.Windows.Forms.DockStyle.Top;
            this.header1.Image = ((System.Drawing.Image)(resources.GetObject("header1.Image")));
            this.header1.Location = new System.Drawing.Point(0, 0);
            this.header1.Name = "header1";
            this.header1.Size = new System.Drawing.Size(729, 64);
            this.header1.TabIndex = 0;
            this.header1.Title = "s_finish_bulkcopy";
            // 
            // BulkCopyWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 532);
            this.Controls.Add(this.wizard1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BulkCopyWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "s_bulkcopy_wizard";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BulkCopyWizard_FormClosed);
            this.wizard1.ResumeLayout(false);
            this.wpsource.ResumeLayout(false);
            this.wptarget.ResumeLayout(false);
            this.wpcolmap.ResumeLayout(false);
            this.wpfinish.ResumeLayout(false);
            this.wpfinish.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Gui.Wizard.Wizard wizard1;
        private Gui.Wizard.WizardPage wptarget;
        private Gui.Wizard.WizardPage wpsource;
        private Gui.Wizard.WizardPage wpcolmap;
        private Gui.Wizard.WizardPage wpfinish;
        private Gui.Wizard.Header header1;
        private Gui.Wizard.Header header2;
        private Gui.Wizard.Header header3;
        private Gui.Wizard.Header header4;
        private DatAdmin.DataStoreFrame source;
        private DatAdmin.DataStoreFrame target;
        private ColumnMapFrame columnMapFrame1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox labTarget;
        private System.Windows.Forms.TextBox labSource;
        private System.Windows.Forms.Button btnExportAsJob;
        private TableCopyOptionsFrame tableCopyOptionsFrame1;
    }
}