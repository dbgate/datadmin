using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public static class FormExtension
    {
        public static DialogResult ShowDialogEx(this Form window)
        {
            return window.ShowDialog();
            //return MacroManager.ShowDialog(window);
        }

        public static DialogResult ShowDialogEx(this CommonDialog dialog)
        {
            Translating.TranslateDialog(dialog);
            return dialog.ShowDialog();
            //return MacroManager.ShowCommonDialog(dialog);
        }
    }
}
