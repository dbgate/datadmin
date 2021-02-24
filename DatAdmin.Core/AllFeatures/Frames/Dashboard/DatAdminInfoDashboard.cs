using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class DatAdminInfoDashboardFrame : ContentFrame
    {
        public DatAdminInfoDashboardFrame()
        {
            InitializeComponent();

            HtmlGenerator gen = new HtmlGenerator();
            gen.BeginHtml(VersionInfo.ProgramTitle, HtmlGenerator.HtmlObjectViewStyle);
            gen.Heading(VersionInfo.ProgramTitle, 2);
            gen.PropsTableBegin();
            gen.PropTableRow("s_version", VersionInfo.VERSION);
            gen.PropTableRow("s_revision", VersionInfo.SVN_REVISION);
            gen.PropTableRow("s_build_at", VersionInfo.BuildAt.ToString("d"));
            gen.PropTableRow("s_edition", LicenseTool.EditionText());
            //gen.PropTableRow("s_license_valid_to", LicenseTool. Registration.EditionValidTo != null ? Registration.EditionValidTo.Value.ToString() : "");
            gen.PropsTableEnd();
            gen.BeginUl();
            gen.Li(String.Format("<a href='callback://open_newconn_dialog'>{0}</a>", Texts.Get("s_create_connection")));
            gen.Li(String.Format("<a href='callback://licenses'>{0}</a>", Texts.Get("s_licenses")));
            gen.Li(String.Format("<a href='http://datadmin.com'>{0}</a>", Texts.Get("s_datadmin_on_web")));
            gen.Li(String.Format("<a href='callback://support'>{0}</a>", Texts.Get("s_support")));
            gen.EndUl();

            bool showlic = true;
            if (LicenseTool.HidePurchaseLinks() && VersionInfo.HideLicenseInfo) showlic = false;
            if (showlic)
            {
                gen.Write(VersionInfo.LicenseInfo);
            }

            gen.EndHtml();
            htmlPanelEx1.Procedures["open_newconn_dialog"] = (Action)delegate() { MainWindow.Instance.CreateNewConnectionDialog(); };
            htmlPanelEx1.Procedures["licenses"] = (Action)delegate() { AboutForm.RunLicenses(); };
            htmlPanelEx1.Procedures["support"] = (Action)delegate() { SupportConnector.SupportRequest(); };
            htmlPanelEx1.Text = gen.HtmlText;
        }
    }

    public class DatAdminInfoDashboard : DashboardBase
    {
        public override bool SuitableFor(AppObject appobj)
        {
            return true;
        }

        //protected override void SetSelectedObject(AppObject obj)
        //{
        //}

        public override Control CreateControl(DashboardInstanceParams pars)
        {
            return new DatAdminInfoDashboardFrame();
        }

        [Browsable(false)]
        public override int Priority { get { return -10; } }
    }
}
