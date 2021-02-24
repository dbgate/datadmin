using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class ObjectFilterFrame : UserControl
    {
        ObjectFilterBase m_filter;

        public ObjectFilterFrame()
        {
            InitializeComponent();
        }

        public ObjectFilterFrame(ObjectFilterBase filter)
        {
            InitializeComponent();
            Filter = filter;
        }

        public ObjectFilterBase Filter
        {
            get { return m_filter; }
            set
            {
                if (Controls != null) Controls.Clear();
                m_filter = value;
                if (m_filter == null) return;
                int acty = 0;
                var items = m_filter.GetItems();
                foreach (var item in items)
                {
                    var editor = item.CreateEditor();
                    Controls.Add(editor);
                    editor.Left = 0;
                    editor.Width = Width;
                    editor.Top = acty;
                    editor.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                    acty += editor.Height;
                }
            }
        }
    }
}
