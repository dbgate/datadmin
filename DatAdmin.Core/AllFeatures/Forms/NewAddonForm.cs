using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class NewAddonForm : FormEx
    {
        AddonType m_type;
        IAddonInstance m_addon;
        public NewAddonForm(AddonType type)
        {
            InitializeComponent();
            m_type = type;
            foreach (var hld in m_type.CommonSpace.GetAllAddons())
            {
                cbxType.Items.Add(hld);
            }
        }

        public static NamedAddonInstance Run(AddonType type)
        {
            var win = new NewAddonForm(type);
            if (win.ShowDialogEx() == DialogResult.OK)
            {
                return new NamedAddonInstance(win.m_addon, win.tbxNewName.Text);
            }
            return null;
        }

        private void cbxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sel = cbxType.SelectedItem as AddonHolder;
            if (sel == null) return;
            if (sel.Attrib != null && sel.Attrib.Description != null)
            {
                infoBoxFrame1.InfoText = Texts.Get(sel.Attrib.Description);
            }
            else
            {
                infoBoxFrame1.InfoText = "";
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var sel = cbxType.SelectedItem as AddonHolder;
            if (sel == null)
            {
                StdDialog.ShowError("s_please_choose_type");
                return;
            }
            if (tbxNewName.Text.IsEmpty())
            {
                StdDialog.ShowError("s_please_fill_name");
                return;
            }
            DialogResult = DialogResult.OK;
            m_addon = sel.CreateInstance();
            Close();
        }
    }
}
