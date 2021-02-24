using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class SyntaxEditorForm : FormEx
    {
        public SyntaxEditorForm()
        {
            InitializeComponent();
        }

        public static string Run(string caption, string label, string text, CodeLanguage lang)
        {
            SyntaxEditorForm win = new SyntaxEditorForm();
            win.codeEditor1.Language = lang;
            win.codeEditor1.Text = text;
            win.textBox1.Text = Texts.Get(label);
            win.Text = Texts.Get(caption);
            if (win.ShowDialogEx() == DialogResult.OK)
            {
                return win.codeEditor1.Text;
            }
            return null;
        }

    }
}
