using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.apps
{
    public partial class ApplicationSettingsFrame : UserControl
    {
        ApplicationSettings m_settings;
        public ApplicationSettingsFrame(ApplicationSettings settings)
        {
            InitializeComponent();
            m_settings = settings;
            textBox1.Text = m_settings.AppList;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            m_settings.AppList = textBox1.Text;
        }
    }

    [SettingsPage(Name = "app", Title = "s_application", Targets = SettingsTargets.Database | SettingsTargets.Dialect, ImageName = CoreIcons.browseName)]
    public class ApplicationSettings : SettingsPageBase, ICustomPropertyPage
    {
        #region ICustomPropertyPage Members

        public Control CreatePropertyPage()
        {
            return new ApplicationSettingsFrame(this);
        }

        #endregion

        [Category("s_application")]
        [SettingsKey("app.applist")]
        [XmlElem]
        public string AppList { get; set; }
    }
}
