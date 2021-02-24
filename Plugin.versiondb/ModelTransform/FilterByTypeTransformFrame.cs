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
    public partial class FilterByTypeTransformFrame : UserControl
    {
        FilterByTypeTransform m_obj;
        List<AddonHolder> m_addons = new List<AddonHolder>();
        public FilterByTypeTransformFrame(FilterByTypeTransform obj)
        {
            InitializeComponent();
            m_obj = obj;
            chbRemoveSelected.Checked = obj.RemoveSelected;
            m_addons.AddRange(SpecificRepresentationAddonType.Instance.CommonSpace.GetAllAddons());
            foreach (var hld in m_addons)
            {
                checkedListBox1.Items.Add(hld.Title != null ? Texts.Get(hld.Title) : hld.Name);
            }
            foreach (string selected in obj.SelectedItems)
            {
                int index = m_addons.IndexOfIf(h => h.Name == selected);
                if (index >= 0) checkedListBox1.SetItemChecked(index, true);
            }
        }

        private void chbRemoveSelected_CheckedChanged(object sender, EventArgs e)
        {
            m_obj.RemoveSelected = chbRemoveSelected.Checked;
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var hld = m_addons[e.Index];
            if (e.NewValue == CheckState.Checked)
            {
                if (!m_obj.SelectedItems.Contains(hld.Name)) m_obj.SelectedItems.Add(hld.Name);
            }
            if (e.NewValue == CheckState.Unchecked)
            {
                if (m_obj.SelectedItems.Contains(hld.Name)) m_obj.SelectedItems.Remove(hld.Name);
            }
        }
    }
}
