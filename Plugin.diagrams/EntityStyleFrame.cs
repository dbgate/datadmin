using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using DatAdmin;

namespace Plugin.diagrams
{
    public partial class EntityStyleFrame : UserControl
    {
        EntityStyle m_parentStyle;
        Func<EntityStyle[]> m_getter;
        Action<EntityStyle> m_setter;
        Func<string, EntityStyle[]> m_enumNamedStyles;
        int m_loadingData;

        public EntityStyleFrame()
        {
            InitializeComponent();
        }

        public void ShowEntityStyle(EntityStyle parentStyle, Func<string, EntityStyle[]> enumNamedStyles, Func<EntityStyle[]> getter, Action<EntityStyle> setter)
        {
            try
            {
                m_loadingData++;
                m_parentStyle = parentStyle;
                m_getter = getter;
                m_setter = setter;
                m_enumNamedStyles = enumNamedStyles;

                var es = m_getter().FirstOrDefault();
                OverrideStyle = es != null;

                cbxExisting.Enabled = m_enumNamedStyles != null;
                ReloadStyleNames();
                _LoadStyle(es);
            }
            finally
            {
                m_loadingData--;
            }
        }

        bool OverrideStyle
        {
            get { return rbtDefineOwn.Checked; }
            set
            {
                if (value) rbtDefineOwn.Checked = true;
                else rbtUseInherited.Checked = true;
            }
        }

        private void ReloadStyleNames()
        {
            if (m_enumNamedStyles == null) return;
            try
            {
                m_loadingData++;
                var names = new HashSetEx<string>();
                foreach (var es in m_enumNamedStyles(null))
                {
                    if (!es.StyleName.IsEmpty()) names.Add(es.StyleName);
                }
                string text = cbxExisting.Text;
                int pos = cbxExisting.SelectionStart;
                cbxExisting.Items.Clear();
                foreach (string name in names)
                {
                    cbxExisting.Items.Add(name);
                }
                cbxExisting.Text = text;
                cbxExisting.SelectionStart = pos;
            }
            finally
            {
                m_loadingData--;
            }
        }

        private void _LoadStyle(EntityStyle es)
        {
            try
            {
                m_loadingData++;
                cbxExisting.Text = es != null ? (es.StyleName ?? "") : "";
                if (es == null) es = m_parentStyle;
                if (es != null)
                {
                    bgHeader.Gradient = es.HeaderBg;
                    bgBody.Gradient = es.BodyBg;
                    chbHeader.Checked = es.IsDefinedHeader;
                }
            }
            finally
            {
                m_loadingData--;
            }
        }

        private void chbOverrideTableStyle_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = OverrideStyle;
            if (m_loadingData > 0) return;
            if (OverrideStyle)
            {
                var es = m_parentStyle.Clone();
                _LoadStyle(es);
                m_setter(es);
            }
            else
            {
                m_setter(null);
            }
            CallChanged();
        }

        private void CallChanged()
        {
            if (Changed != null) Changed(this, EventArgs.Empty);
        }

        public event EventHandler Changed;

        private void DispatchStyleChange(Action<EntityStyle> proc)
        {
            if (m_loadingData > 0) return;
            var ents = m_getter();
            if (ents == null) return;
            try
            {
                foreach (var ent in ents) proc(ent);
                var es = ents.FirstOrDefault();
                if (es == null) return;
                if (m_enumNamedStyles == null) return;
                if (String.IsNullOrEmpty(es.StyleName)) return;
                var styles = m_enumNamedStyles(es.StyleName);
                if (styles == null) return;
                foreach (var style in styles)
                {
                    proc(style);
                }
            }
            finally
            {
                CallChanged();
            }
        }

        private void bgHeader_Changed(object sender, EventArgs e)
        {
            DispatchStyleChange(s => s.HeaderBg = bgHeader.Gradient);
        }

        private void bgBody_Changed(object sender, EventArgs e)
        {
            DispatchStyleChange(s => s.BodyBg = bgBody.Gradient);
        }

        private void chbHeader_CheckedChanged(object sender, EventArgs e)
        {
            DispatchStyleChange(s => s.IsDefinedHeader = chbHeader.Checked);
        }

        private void cbxExisting_TextChanged(object sender, EventArgs e)
        {
            if (m_loadingData > 0) return;

            if (m_enumNamedStyles == null) return;
            if (cbxExisting.Text.IsEmpty())
            {
                foreach (var st in m_getter())
                {
                    st.StyleName = "";
                }
                ReloadStyleNames();
                CallChanged();
                return;
            }
            var ents = m_enumNamedStyles(cbxExisting.Text);
            if (ents == null || ents.Length == 0)
            {
                foreach (var st in m_getter())
                {
                    st.StyleName = cbxExisting.Text;
                }
            }
            else
            {
                _LoadStyle(ents[0]);
                m_setter(ents[0]);
            }
            CallChanged();
            ReloadStyleNames();
        }
    }
}
