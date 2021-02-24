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
    public partial class ColumnMapFrame : UserControl
    {
        IColumnMapFrame m_frmvar;
        Dictionary<string, AddonHolder> m_addonByTitle = new Dictionary<string, AddonHolder>();
        public ColumnMapFrame()
        {
            InitializeComponent();
            FillTransforms();
            EnableButtons();
        }

        public void SetBindings(ITabularDataStore source, ITabularDataStore target)
        {
            if (m_frmvar != null) ((UserControl)m_frmvar).Dispose();
            if (target.AvailableRowFormat)
            {
                ColumnMapFrame_FixedTarget frmvar = new ColumnMapFrame_FixedTarget();
                panel1.Controls.Add(frmvar);
                frmvar.Dock = DockStyle.Fill;
                m_frmvar = frmvar;
                frmvar.SetBindings(source, target);
            }
            else
            {
                ColumnMapFrame_VarTarget frmvar = new ColumnMapFrame_VarTarget();
                panel1.Controls.Add(frmvar);
                frmvar.Dock = DockStyle.Fill;
                m_frmvar = frmvar;
                frmvar.SetBindings(source, target);
            }
            Translating.TranslateControl(this);
        }

        public IRowTransform GetTransform()
        {
            var res = m_frmvar.GetTransform();
            if (res is GenericTransform)
            {
                ((GenericTransform)res).Script = codeEditor1.Text;
            }
            return res;
        }

        public ITableStructure GetTargetRowFormat()
        {
            return m_frmvar.GetTargetRowFormat();
        }

        private void FillTransforms()
        {
            m_addonByTitle.Clear();
            cbxSavedTransforms.Items.Clear();
            try
            {
                foreach (var addon in RowTransformAddonType.Instance.CommonSpace.GetFilteredAddons(RegisterItemUsage.DirectUse))
                {
                    m_addonByTitle[addon.Title] = addon;
                    cbxSavedTransforms.Items.Add(addon.Title);
                }
            }
            catch { }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var fn = RowTransformAddonType.Instance.GetFullFileName(cbxSavedTransforms.Text);
            var tr = RowTransformAddonType.Instance.LoadRowTransform(fn, m_frmvar.SourceOnInput, m_frmvar.TargetOnInput);
            m_frmvar.LoadFromTransform(tr);
            if (tr is GenericTransform)
            {
                var t = tr as GenericTransform;
                codeEditor1.Text = t.Script;
            }
            else
            {
                codeEditor1.Text = "";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string fn = RowTransformAddonType.Instance.GetFullFileName(cbxSavedTransforms.Text);
            RowTransformAddonType.Instance.WantCommonFolder();
            var tr = GetTransform();
            ((AddonBase)tr).SaveToFile(fn);
            RowTransformAddonType.Instance.CommonSpace.ClearCache();
            FillTransforms();
            cbxSavedTransforms.Text = Path.GetFileName(fn);
        }

        private void cbxSavedTransforms_Click(object sender, EventArgs e)
        {
            EnableButtons();
        }

        private void EnableButtons()
        {
            btnSave.Enabled = cbxSavedTransforms.Text != "";
            string fn = RowTransformAddonType.Instance.GetFullFileName(cbxSavedTransforms.Text);
            btnLoad.Enabled = File.Exists(fn);
        }
    }

    public interface IColumnMapFrame
    {
        IRowTransform GetTransform();
        ITableStructure GetTargetRowFormat();
        void LoadFromTransform(IRowTransform tr);
        ITableStructure SourceOnInput { get; }
        ITableStructure TargetOnInput { get; }
    }
}
