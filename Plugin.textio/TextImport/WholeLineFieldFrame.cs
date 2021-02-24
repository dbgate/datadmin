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
    public partial class WholeLineFieldFrame : FieldAnalyseFrame
    {
        public WholeLineFieldFrame(TextImportFrame frame)
            : base(frame)
        {
            InitializeComponent();
        }

        public override string ComboTitle()
        {
            return "s_whole_line";
        }

        public override FieldAnalyser CreateAnalyser()
        {
            WholeLineAnalyser res = new WholeLineAnalyser { FieldName = tbxFieldName.Text };
            return res;
        }

        public override void LoadFromAnalyser(FieldAnalyser analyser)
        {
            var a = analyser as WholeLineAnalyser;
            tbxFieldName.Text = a.FieldName;
        }

        private void tbxFieldName_TextChanged(object sender, EventArgs e)
        {
            m_frame.Save();
        }
    }
}
