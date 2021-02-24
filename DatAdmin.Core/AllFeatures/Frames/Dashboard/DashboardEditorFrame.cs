using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DatAdmin
{
    public partial class DashboardEditorFrame : UserControl
    {
        DockPanelDashboard m_dashboard;
        bool m_machineChangingOverride = true;
        public DashboardEditorFrame(DockPanelDashboard dashboard)
        {
            InitializeComponent();
            m_dashboard = dashboard;
            ReloadProps();
        }

        private void ReloadProps()
        {
            m_machineChangingOverride = true;
            addonSelectFrame1.SelectObject(m_dashboard.Filter);
            tbxPriority.Text = m_dashboard.Priority.ToString();
            rbtOverride.Enabled = rbtUseDefault.Enabled = m_dashboard.IsInLibDirectory() || m_dashboard.HasLibVariant();
            if (rbtOverride.Enabled)
            {
                rbtUseDefault.Checked = m_dashboard.IsInLibDirectory();
                rbtOverride.Checked = !rbtUseDefault.Checked;
            }
            m_machineChangingOverride = false;
        }

        private void addonSelectFrame1_ChangedSelectedObject(object sender, EventArgs e)
        {
            m_dashboard.Filter = (ObjectFilterBase)((AddonSelectFrame)sender).SelectedObject;
        }

        private void tbxPriority_TextChanged(object sender, EventArgs e)
        {
            try { m_dashboard.Priority = Int32.Parse(tbxPriority.Text); }
            catch { }
        }

        private void rbtOverride_CheckedChanged(object sender, EventArgs e)
        {
            tbxPriority.Enabled = addonSelectFrame1.Enabled = rbtOverride.Checked;
            if (!rbtOverride.Enabled) return;
            if (m_machineChangingOverride) return;
            if (rbtOverride.Checked && m_dashboard.IsInLibDirectory())
            {
                m_dashboard.RedirectToCfgDirectory();
            }
            if (rbtUseDefault.Checked && m_dashboard.IsInCfgDirectory())
            {
                if (StdDialog.YesNoDialog("s_this_action_will_delete_your_own_settings_continue"))
                {
                    if (m_dashboard.IsInCfgDirectory()) File.Delete(m_dashboard.AddonFileName);
                    m_dashboard.RedirectToLibDirectory();
                    ((IAddonInstance)m_dashboard).LoadFromFile(m_dashboard.AddonFileName);
                    ReloadProps();
                }
                else
                {
                    m_machineChangingOverride = true;
                    rbtOverride.Checked = true;
                    m_machineChangingOverride = false;
                }
            }
        }
    }
}
