using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class LinesDialog : FrameworkFormEx
    {
        public LinesDialog()
        {
            InitializeComponent();
        }

        public static List<string> Run(string caption, string label, IEnumerable<string> lines, bool onlyNotEmpty)
        {
            LinesDialog win = new LinesDialog();
            List<string> src = new List<string>();
            if (lines != null)
            {
                foreach (string l in lines) src.Add(l);
            }
            win.tbxLines.Lines = src.ToArray();
            win.Text = Texts.Get(caption);
            win.tbxLabel.Text = Texts.Get(label);
            if (win.ShowDialogEx() == DialogResult.OK)
            {
                var res = new List<string>();
                foreach (string line in win.tbxLines.Lines)
                {
                    string s = line;
                    if (onlyNotEmpty)
                    {
                        s = s.Trim();
                        if (s == "") continue;
                    }
                    res.Add(s);
                }
                return res;
            }
            return null;
        }
    }
}
