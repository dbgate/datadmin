using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public class FormEx : FrameworkFormEx, IConnectionPackHolder
    {
        public FormEx(){
            Shown += new EventHandler(FormEx_Shown);
            ConnPack = new ConnectionPack(this);
            Disposed += new EventHandler(FormEx_Disposed);
        }

        ConnectionPack m_connPack;
        public ConnectionPack ConnPack
        {
            get { return m_connPack; }
            set
            {
                if (m_connPack != null) m_connPack.Release();
                m_connPack = value;
                if (m_connPack != null) m_connPack.AddRef();
            }
        }

        void FormEx_Disposed(object sender, EventArgs e)
        {
            ConnPack.Dispose();
        }

        void FormEx_Shown(object sender, EventArgs e)
        {
            MacroManager.RunDialogMacro(this);
        }
    }
}
