using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public class ReformatToolkit
    {
        SqlFormatProperties m_reformatProps;
        ISqlDialect m_dialect;
        CodeEditor m_editor;

        public ReformatToolkit(ISqlDialect dialect, CodeEditor editor)
        {
            m_dialect = dialect;
            if (m_dialect == null) m_dialect = new GenericDialect();
            m_editor = editor;
        }

        public SqlFormatProperties ReformatProps
        {
            get
            {
                if (m_reformatProps == null)
                {
                    m_reformatProps = GlobalSettings.DialectSettings[m_dialect.DialectName].SqlFormat().CreateCopy();
                }
                return m_reformatProps;
            }
        }

        public void Reformat()
        {
            if (EditPropertiesForm.Run(ReformatProps, true))
            {
                string sql = m_editor.Text;
                ISqlParser parser = m_dialect.CreateParser(sql);
                try
                {
                    var code = parser.ParseCommand();
                    if (!parser.Tokernizer.IsEof) throw new ParseError("DAE-00294 EOF expected");
                    string res = m_dialect.GenerateScript(code.GenerateSql, ReformatProps);
                    m_editor.SetCodeText(res, true);
                }
                catch (Exception err)
                {
                    Errors.Report(err);
                }
            }
        }
    }
}
