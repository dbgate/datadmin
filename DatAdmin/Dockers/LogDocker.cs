using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DatAdmin
{
    public partial class LogDocker : DockerBase
    {
        CachingLogger m_mainWinLog;

        public LogDocker(IDockerFactory factory)
            : base(factory)
        {
            InitializeComponent();
            m_mainWinLog = new CachingLogger(GlobalSettings.Pages.Log().WindowLogLevel, GlobalSettings.Pages.Log().WindowCacheSize);
            Logging.MultiCast.AddLogger(m_mainWinLog);
            if (!Core.IsMono)
            {
                foreach (var rec in Logging.GetFeedbackLastLogEntries())
                {
                    // add log entries from startup
                    m_mainWinLog.LogMessage(rec);
                }
            }
            messageLogFrame1.Source = m_mainWinLog;
            HSettings.ReloadSettings += HSettings_ReloadSettings;
        }

        void HSettings_ReloadSettings()
        {
            m_mainWinLog.MinLogLevel = GlobalSettings.Pages.Log().WindowLogLevel;
        }
    }

    [DockerFactory(Title = "Log window", Name = "log_window")]
    public class LogDockerFactory : DockerFactoryBase
    {
        public override IDocker CreateDocker()
        {
            return new LogDocker(this);
        }

        public override string MenuTitle
        {
            get { return "s_log"; }
        }

        public override Bitmap Icon
        {
            get { return CoreIcons.log; }
        }

        public override DockerState InitialState
        {
            get { return DockerState.DockBottom; }
        }

        public override Keys Shortcut
        {
            get { return Keys.Control | Keys.Alt | Keys.L; }
        }
    }
}
