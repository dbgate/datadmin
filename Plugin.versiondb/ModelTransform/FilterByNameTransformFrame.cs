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
    public partial class FilterByNameTransformFrame : UserControl
    {
        FilterByNameTransform m_obj;
        List<AddonHolder> m_addons = new List<AddonHolder>();
        public FilterByNameTransformFrame(FilterByNameTransform obj)
        {
            InitializeComponent();
            m_obj = obj;

            m_addons.AddRange(SpecificRepresentationAddonType.Instance.CommonSpace.GetAllAddons());
            int tindex = 0;
            foreach (var hld in m_addons)
            {
                if (hld.Name == (m_obj.ObjectType ?? "table")) tindex = cbxObjectType.Items.Count;
                cbxObjectType.Items.Add(hld.Title != null ? Texts.Get(hld.Title) : hld.Name);
            }

            cbxObjectType.SelectedIndex = tindex;
            objectFilterFrame1.Filter = obj.NameFilter; 
        }

        private void cbxObjectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxObjectType.SelectedIndex < 0) return;
            m_obj.ObjectType = m_addons[cbxObjectType.SelectedIndex].Name;
        }
    }
}
