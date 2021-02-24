using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Menees.DiffUtils;
using Menees.DiffUtils.Controls;

namespace DatAdmin
{
    public partial class TextDiffForm : FormEx
    {
        public TextDiffForm()
        {
            InitializeComponent();
        }

        public static void Run(string a, string b)
        {
            var win = new TextDiffForm();
            win.diffControl1.ShowDiff(a, b);
            win.ShowDialog();
        }

        private void TextDiffForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }
    }

    public static class DiffControlExtension
    {
        public static void ShowDiff(this DiffControl ctrl, string a, string b)
        {
            TextDiff diff = new TextDiff(HashType.HashCode, true, true, 0, false);
            List<string> la = new List<string>(a.Split('\n'));
            List<string> lb = new List<string>(b.Split('\n'));
            EditScript script = diff.Execute(la, lb);
            ctrl.SetData(la, lb, script);
        }
    }
}
