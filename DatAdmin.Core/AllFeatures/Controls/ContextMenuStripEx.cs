using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public class ContextMenuStripEx : ContextMenuStrip
    {
        bool m_translated;
        public ContextMenuStripEx() { }

        public ContextMenuStripEx(System.ComponentModel.IContainer container)
            : base(container)
        {
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            else
            {
                return base.ProcessDialogKey(keyData);
            }
        }

        protected override void OnOpening(System.ComponentModel.CancelEventArgs e)
        {
            base.OnOpening(e);
            if (!m_translated)
            {
                foreach (ToolStripItem item in Items)
                {
                    Translating.TranslateToolStrip(item);
                }
                m_translated = true;
            }
        }
    }
}
