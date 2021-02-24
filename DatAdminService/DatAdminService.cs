using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace DatAdmin
{
    public partial class DatAdminService : ServiceBase
    {
        Thread m_thread;
        ManualResetEvent m_shutdownEvent;

        public DatAdminService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            m_shutdownEvent = new ManualResetEvent(false);
            m_thread = new Thread(Run);
            m_thread.Start();
            SystemLogTool.Info("DatAdmin Service started.");
        }

        protected override void OnStop()
        {
            m_shutdownEvent.Set();
            m_thread.Join(10000);
            //m_thread.Abort();
            base.OnStop();
            SystemLogTool.Info("DatAdmin Service stopped.");
        }

        protected void Run()
        {
            while (true)
            {
                bool signaled = m_shutdownEvent.WaitOne(TimeSpan.FromMinutes(1), true);

                if (signaled == true)
                    break;

                HJob.CallEveryMinute();
            }
        }
    }
}
