using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public class FrameworkFormEx : Form
    {
        protected IInvoker m_invoker;

        public FrameworkFormEx()
        {
            Load += new EventHandler(FormEx_Load);
            m_invoker = new ControlInvoker(this);
            Usage = new UsageBuilder(UsageEventName);
            HUsage.ClosingApp += HUsage_ClosingApp;
        }

        void HUsage_ClosingApp()
        {
            Usage.Send();
        }

        void FormEx_Load(object sender, EventArgs e)
        {
            Translating.TranslateControl(this);
        }

        public virtual string UsageEventName { get { return "form:" + GetType().FullName; } }
        public UsageBuilder Usage;

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Usage.Dispose();
            HUsage.ClosingApp -= HUsage_ClosingApp;
        }
    }
}
