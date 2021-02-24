namespace DatAdmin
{
    partial class TunnelConnWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TunnelConnWizard));
            this.wizWizard = new Gui.Wizard.Wizard();
            this.wpgTunnel = new Gui.Wizard.WizardPage();
            this.addonSelectFrame1 = new DatAdmin.AddonSelectFrame();
            this.header1 = new Gui.Wizard.Header();
            this.wizardPage1 = new Gui.Wizard.WizardPage();
            this.infoPage1 = new Gui.Wizard.InfoPage();
            this.pgdbconn = new Gui.Wizard.WizardPage();
            this.tunnelCommonFrame1 = new DatAdmin.GenericTunnelFrame();
            this.header2 = new Gui.Wizard.Header();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.wizWizard.SuspendLayout();
            this.wpgTunnel.SuspendLayout();
            this.wizardPage1.SuspendLayout();
            this.pgdbconn.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizWizard
            // 
            this.wizWizard.Controls.Add(this.wpgTunnel);
            this.wizWizard.Controls.Add(this.wizardPage1);
            this.wizWizard.Controls.Add(this.pgdbconn);
            this.wizWizard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizWizard.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wizWizard.Location = new System.Drawing.Point(0, 0);
            this.wizWizard.Name = "wizWizard";
            this.wizWizard.PageIndex = 0;
            this.wizWizard.Pages.AddRange(new Gui.Wizard.WizardPage[] {
            this.wpgTunnel,
            this.pgdbconn,
            this.wizardPage1});
            this.wizWizard.Size = new System.Drawing.Size(670, 498);
            this.wizWizard.TabIndex = 0;
            this.wizWizard.OnTranslate += new Gui.Wizard.TranslateEventHandler(this.wizWizard_OnTranslate);
            // 
            // wpgTunnel
            // 
            this.wpgTunnel.Controls.Add(this.addonSelectFrame1);
            this.wpgTunnel.Controls.Add(this.header1);
            this.wpgTunnel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wpgTunnel.IsFinishPage = false;
            this.wpgTunnel.Location = new System.Drawing.Point(0, 0);
            this.wpgTunnel.Name = "wpgTunnel";
            this.wpgTunnel.Size = new System.Drawing.Size(670, 450);
            this.wpgTunnel.TabIndex = 1;
            this.wpgTunnel.CloseFromNext += new Gui.Wizard.PageEventHandler(this.wpgHttp_CloseFromNext);
            // 
            // addonSelectFrame1
            // 
            this.addonSelectFrame1.AddonTypeName = "tunneldriver";
            this.addonSelectFrame1.AllowSwitchDesign = false;
            this.addonSelectFrame1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.addonSelectFrame1.CanSaveAsTemplate = false;
            this.addonSelectFrame1.CompactDesign = true;
            this.addonSelectFrame1.DefaultAddonHolder = "http_tunnel";
            this.addonSelectFrame1.Location = new System.Drawing.Point(12, 79);
            this.addonSelectFrame1.Name = "addonSelectFrame1";
            this.addonSelectFrame1.Size = new System.Drawing.Size(646, 368);
            this.addonSelectFrame1.TabIndex = 1;
            this.addonSelectFrame1.TypeTitle = "s_type";
            this.addonSelectFrame1.ChangedSelectedObject += new System.EventHandler(this.addonSelectFrame1_ChangedSelectedObject);
            // 
            // header1
            // 
            this.header1.BackColor = System.Drawing.SystemColors.Control;
            this.header1.CausesValidation = false;
            this.header1.Description = "";
            this.header1.Dock = System.Windows.Forms.DockStyle.Top;
            this.header1.Image = ((System.Drawing.Image)(resources.GetObject("header1.Image")));
            this.header1.Location = new System.Drawing.Point(0, 0);
            this.header1.Name = "header1";
            this.header1.Size = new System.Drawing.Size(670, 64);
            this.header1.TabIndex = 0;
            this.header1.Title = "s_tunnel_file_properties";
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.infoPage1);
            this.wizardPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage1.IsFinishPage = false;
            this.wizardPage1.Location = new System.Drawing.Point(0, 0);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new System.Drawing.Size(670, 450);
            this.wizardPage1.TabIndex = 3;
            // 
            // infoPage1
            // 
            this.infoPage1.BackColor = System.Drawing.Color.White;
            this.infoPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoPage1.Image = ((System.Drawing.Image)(resources.GetObject("infoPage1.Image")));
            this.infoPage1.Location = new System.Drawing.Point(0, 0);
            this.infoPage1.Name = "infoPage1";
            this.infoPage1.PageText = "s_click_finish_to_create_connection_file";
            this.infoPage1.PageTitle = "s_phptunnel_wizard_complete";
            this.infoPage1.Size = new System.Drawing.Size(670, 450);
            this.infoPage1.TabIndex = 0;
            // 
            // pgdbconn
            // 
            this.pgdbconn.Controls.Add(this.tunnelCommonFrame1);
            this.pgdbconn.Controls.Add(this.header2);
            this.pgdbconn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgdbconn.IsFinishPage = false;
            this.pgdbconn.Location = new System.Drawing.Point(0, 0);
            this.pgdbconn.Name = "pgdbconn";
            this.pgdbconn.Size = new System.Drawing.Size(670, 450);
            this.pgdbconn.TabIndex = 2;
            this.pgdbconn.CloseFromNext += new Gui.Wizard.PageEventHandler(this.pgdbconn_CloseFromNext);
            // 
            // tunnelCommonFrame1
            // 
            this.tunnelCommonFrame1.Connection = null;
            this.tunnelCommonFrame1.Location = new System.Drawing.Point(0, 61);
            this.tunnelCommonFrame1.Name = "tunnelCommonFrame1";
            this.tunnelCommonFrame1.Size = new System.Drawing.Size(512, 271);
            this.tunnelCommonFrame1.TabIndex = 1;
            // 
            // header2
            // 
            this.header2.BackColor = System.Drawing.SystemColors.Control;
            this.header2.CausesValidation = false;
            this.header2.Description = "s_database_connection_properties_desc";
            this.header2.Dock = System.Windows.Forms.DockStyle.Top;
            this.header2.Image = ((System.Drawing.Image)(resources.GetObject("header2.Image")));
            this.header2.Location = new System.Drawing.Point(0, 0);
            this.header2.Name = "header2";
            this.header2.Size = new System.Drawing.Size(670, 64);
            this.header2.TabIndex = 0;
            this.header2.Title = "s_database_connection_properties";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "phptunnel.php|phptunnel.php";
            // 
            // TunnelConnWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 498);
            this.Controls.Add(this.wizWizard);
            this.Name = "TunnelConnWizard";
            this.Text = "s_tunnelling_connection";
            this.wizWizard.ResumeLayout(false);
            this.wpgTunnel.ResumeLayout(false);
            this.wizardPage1.ResumeLayout(false);
            this.pgdbconn.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Gui.Wizard.Wizard wizWizard;
        private Gui.Wizard.WizardPage wpgTunnel;
        private Gui.Wizard.WizardPage pgdbconn;
        private Gui.Wizard.Header header1;
        private Gui.Wizard.Header header2;
        private Gui.Wizard.WizardPage wizardPage1;
        private Gui.Wizard.InfoPage infoPage1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private GenericTunnelFrame tunnelCommonFrame1;
        private AddonSelectFrame addonSelectFrame1;
    }
}