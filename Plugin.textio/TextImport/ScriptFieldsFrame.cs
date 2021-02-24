using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Plugin.textio
{
    public partial class ScriptFieldsFrame : FieldAnalyseFrame
    {
        public ScriptFieldsFrame(TextImportFrame frame)
            : base(frame)
        {
            InitializeComponent();
        }

        public override FieldAnalyser CreateAnalyser()
        {
            ScriptAnalyser res = new ScriptAnalyser { Code = codeEditor1.Text };
            return res;
        }

        public override string ComboTitle()
        {
            return "s_python_script";
        }

        public override void LoadFromAnalyser(FieldAnalyser analyser)
        {
            var a = analyser as ScriptAnalyser;
            codeEditor1.Text = a.Code;
        }

        private void codeEditor1_TextChanged(object sender, EventArgs e)
        {
            m_frame.Save();
        }
    }
}
