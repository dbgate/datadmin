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
    public partial class CodeWriterFrame : FileWriterFrame
    {
        TextGeneratorLanguage m_lang;
        public CodeWriterFrame(PolyFileDbWriterFrame frame, TextGeneratorLanguage lang)
            : base(frame)
        {
            InitializeComponent();
            m_lang = lang;
            codeEditor1.Language = lang == TextGeneratorLanguage.Python ? CodeLanguage.Python : CodeLanguage.Template;
        }

        public override string ComboTitle()
        {
            if (m_lang == TextGeneratorLanguage.Python) return "Python";
            return "s_template";
        }

        public override void LoadContent()
        {
            codeEditor1.Text = ((TextWriterContentBase)m_content).Code;
        }

        private void codeEditor1_TextChanged(object sender, EventArgs e)
        {
            if (m_loading) return;
            ((TextWriterContentBase)m_content).Code = codeEditor1.Text;
        }

        public override WriterFileContentBase CreateContent()
        {
            if (m_lang == TextGeneratorLanguage.Python) Content = new PythonFileContent();
            else Content = new TemplateFileContent();
            return Content;
        }
    }
}
