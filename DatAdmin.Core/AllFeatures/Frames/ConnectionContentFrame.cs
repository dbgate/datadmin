using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class ConnectionContentFrame : ContentFrame
    {
        IPhysicalConnectionFactory m_connFact;
        protected IPhysicalConnection m_conn;

        public ConnectionContentFrame()
        {
            InitializeComponent();
        }

        public IPhysicalConnectionFactory CurrentConnFactory
        {
            get { return m_connFact; }
            set { m_connFact = value; }
        }

        protected void CallOpen()
        {
            if (m_connFact == null)
            {
                Opened(null);
                return;
            }
            m_conn = ConnPack.GetConnection(m_connFact, true);
            if (ConnPack.IsOpened(m_connFact))
            {
                Opened(null);
            }
            else
            {
                m_conn.BeginOpen(Async.CreateInvokeCallback(m_invoker, Opened));
            }
        }

        private void Opened(IAsyncResult async)
        {
            try
            {
                if (async != null)
                {
                    ((IStandaloneAsyncResult)async).EndInvoke();
                }
                OnOpenedConnection();
            }
            catch (Exception err)
            {
                OnOpenedFail(err);
            }
        }

        protected virtual void OnOpenedConnection()
        {
        }

        protected virtual void OnOpenedFail(Exception err)
        {
            Errors.Report(err);
        }
    }
}
