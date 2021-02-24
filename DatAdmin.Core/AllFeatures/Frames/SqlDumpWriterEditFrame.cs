using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace DatAdmin
{
    public partial class SqlDumpWriterEditFrame : UserControl, ITabbedEditor
    {
        SqlDatabaseWriter m_obj;
        Dictionary<string, SqlDumpWriterConfig> m_dialectConfigs = new Dictionary<string, SqlDumpWriterConfig>();
        Dictionary<string, int> m_dialectIndexes = new Dictionary<string, int>();

        public SqlDumpWriterEditFrame()
        {
            InitializeComponent();
            Translating.TranslateControl(this);
            foreach (var item in DialectAddonType.Instance.CommonSpace.GetFilteredAddons(RegisterItemUsage.DirectUse))
            {
                m_dialectIndexes[item.Name] = cbxDialect.Items.Count;
                cbxDialect.Items.Add(item);
            }
            cbxDialect.SelectedIndex = 0;
        }

        #region ITabbedEditor Members

        public void LoadFromObject(object obj, PropertyInfo prop)
        {
            m_obj = (SqlDatabaseWriter)obj;
            cbxDialect.SelectedIndex = -1;
            if (m_obj != null && m_obj.Dialect != null && m_dialectIndexes.ContainsKey(m_obj.Dialect.DialectName))
            {
                m_dialectConfigs[m_obj.Dialect.DialectName] = m_obj.DumpConfig;
                cbxDialect.SelectedIndex = m_dialectIndexes[m_obj.Dialect.DialectName];
            }
        }

        public string PageTitle
        {
            get { return "s_settings"; }
        }

        #endregion

        private void cbxDialect_SelectedIndexChanged(object sender, EventArgs e)
        {
            var addon = cbxDialect.SelectedItem as AddonHolder;
            if (addon == null) return;
            var dialect = (ISqlDialect)addon.CreateInstance();
            if (!m_dialectConfigs.ContainsKey(dialect.DialectName))
            {
                m_dialectConfigs[dialect.DialectName] = dialect.CreateDumpWriterConfig();
            }
            propertyFrame1.SelectedObject = m_dialectConfigs[dialect.DialectName];
            if (m_obj != null)
            {
                m_obj.UsedDialect = dialect;
                m_obj.DumpConfig = m_dialectConfigs[dialect.DialectName];
            }
        }
    }
}
