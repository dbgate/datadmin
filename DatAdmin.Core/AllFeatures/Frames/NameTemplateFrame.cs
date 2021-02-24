using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using IronPython.Hosting;

namespace DatAdmin
{
    public partial class NameTemplateFrame : UserControl
    {
        PythonEngine m_engine;

        public NameTemplateFrame()
        {
            InitializeComponent();
            m_engine = new PythonEngine();
            Disposed += new EventHandler(NameTemplateFrame_Disposed);
        }

        void NameTemplateFrame_Disposed(object sender, EventArgs e)
        {
            m_engine.Dispose();
        }

        private void btnDropDown_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Items.Clear();
            var mb = new MenuBuilder();
            mb.NaturalOrder = true;
            mb.AddObject(this);
            mb.GetMenuItems(contextMenuStrip1.Items);
            contextMenuStrip1.Show(btnDropDown, new Point(0, btnDropDown.Height));
        }

        private void InsertText(string value)
        {
            tbxTemplate.SelectedText = value;
            RefreshSample();
        }

        [PopupMenu("GUID")]
        public void InsertGuid()
        {
            InsertText("{guid()}");
        }

        [PopupMenu("Date(YYYY-MM-DD)")]
        public void InsertDate()
        {
            InsertText("{date(\"yyyy'-'MM'-'dd\")}");
        }

        [PopupMenu("Date(YYYY-MM-DD-HH-mm-ss)")]
        public void InsertDateTime()
        {
            InsertText("{date(\"yyyy'-'MM'-'dd'-'HH'-'mm'-'ss\")}");
        }

        private void RefreshSample()
        {
            tbxSample.Text = NameTemplateEngine.Eval(tbxTemplate.Text);
        }

        private void tbxTemplate_TextChanged(object sender, EventArgs e)
        {
            RefreshSample();
            if (ChangedNameTemplate != null) ChangedNameTemplate(this, e);
        }

        public string NameTemplate
        {
            get { return tbxTemplate.Text; }
            set
            {
                tbxTemplate.Text = value;
                RefreshSample();
            }
        }

        public event EventHandler ChangedNameTemplate;
    }
}
