using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.textio
{
    public partial class FieldAnalyseFrame : UserControl
    {
        protected TextImportFrame m_frame;
        public FieldAnalyseFrame()
        {
            InitializeComponent();
        }

        public FieldAnalyseFrame(TextImportFrame frame)
        {
            InitializeComponent();
            m_frame = frame;
        }

        public virtual string ComboTitle() { return ""; }
        public virtual FieldAnalyser CreateAnalyser() { return null; }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            Translating.TranslateControl(this);
        }

        public virtual void LoadFromAnalyser(FieldAnalyser analyser)
        {
        }
    }

    public class FieldAnalyseComboItem
    {
        public FieldAnalyseFrame Frame;

        public override string ToString()
        {
            return Texts.Get(Frame.ComboTitle());
        }
    }
}
