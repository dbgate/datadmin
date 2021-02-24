using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.versiondb
{
    public partial class VdbUpgradeScriptFrame : UserControl
    {
        bool m_loaded;
        GenSqlUpgradeVersionDb m_script;
        public VdbUpgradeScriptFrame(GenSqlUpgradeVersionDb script)
        {
            InitializeComponent();
            m_script = script;
            cbxSource.Items.Add(Texts.Get("s_none"));
            foreach (var ver in m_script.m_vdb.Versions)
            {
                cbxSource.Items.Add(ver.Name);
                cbxTarget.Items.Add(ver.Name);
            }
            if (cbxSource.Items.Count > 0) cbxSource.SelectedIndex = 0;
            if (cbxTarget.Items.Count > 0) cbxTarget.SelectedIndex = 0;
            m_loaded = true;
            m_script.CallChangedProperties();
        }

        private void cbxSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_script.FromVersion = cbxSource.SelectedIndex == 0 ? null : cbxSource.SelectedItem.ToString();
            if (m_loaded) m_script.CallChangedProperties();
        }

        private void cbxTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_script.ToVersion = cbxTarget.SelectedItem.ToString();
            if (m_loaded) m_script.CallChangedProperties();
        }
    }
}
