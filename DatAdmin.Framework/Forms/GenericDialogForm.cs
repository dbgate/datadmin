using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class GenericDialogForm : FrameworkFormEx
    {
        Control m_content;
        public GenericDialogForm(Control content, string title)
        {
            InitializeComponent();
            m_content = content;
            Controls.Add(content);
            Width = m_content.Width + 10;
            Height += m_content.Height;
            Text = Texts.Get(title);
            m_content.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
        }

        public static DialogResult Run(Control content, string title, GenericDialogType type)
        {
            var win = new GenericDialogForm(content, title);
            switch (type)
            {
                case GenericDialogType.OkCancel:
                    break;
                case GenericDialogType.Close:
                    win.btnOk.Visible = false;
                    win.btnCancel.Text = Texts.Get("s_close");
                    break;
            }
            return win.ShowDialogEx();
        }
    }
}
