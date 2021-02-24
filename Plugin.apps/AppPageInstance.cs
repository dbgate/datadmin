using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.apps
{
    public class AppPageInstance : Panel
    {
        AppPage m_page;
        IDatabaseSource m_db;
        AppEnv m_env;
        List<IWidgetControl> m_ctrls = new List<IWidgetControl>();

        public AppPageInstance(AppPage page, IDatabaseSource db, AppEnv env)
        {
            Width = page.SavedWidth;
            Height = page.SavedHeight;
            m_page = page;
            m_db = db;
            m_env = env;

            foreach (var widget in page.Widgets)
            {
                var child = widget.CreateControl(this);
                child.Control.Left = widget.Left;
                child.Control.Top = widget.Top;
                child.Control.Width = widget.Width;
                child.Control.Height = widget.Height;
                child.Control.Anchor = widget.Anchor;
                Controls.Add(child.Control);
                m_ctrls.Add(child);
            }
        }

        public AppEnv AppEnv { get { return m_env; } }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
        }

        public AppPage Page { get { return m_page; } }
        public IDatabaseSource Database { get { return m_db; } }

        public void GetPageNamespace(Dictionary<string, object> names)
        {
            foreach (var c in m_ctrls)
            {
                names[c.Widget.Name] = c.CreateScriptingControl();
            }
        }

        public IWidgetControl FindControl(string name)
        {
            foreach (var c in m_ctrls) if (c.Widget.Name == name) return c;
            return null;
        }

        public void CallNotifyWidgets(IWidgetControl sender)
        {
            if (sender.Widget.NotifyWidgets == null) return;
            foreach (string name in sender.Widget.NotifyWidgets.Split(','))
            {
                var ctrl = FindControl(name);
                if (ctrl == null) continue;
                ctrl.ProcessNotify();
            }
        }
    }
}
