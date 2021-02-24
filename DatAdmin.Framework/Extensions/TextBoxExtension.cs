using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public static class TextBoxExtension
    {
        public static string GetCurrentLineData(this TextBoxBase textbox)
        {
            int pos = textbox.SelectionStart;
            int line = textbox.GetLineFromCharIndex(pos);
            if (line >= 0 && line < textbox.Lines.Length) return textbox.Lines[line];
            return null;
        }
    }
}
