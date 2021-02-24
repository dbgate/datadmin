using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Xml;

namespace DatAdmin
{
    public static class OnlineHelpManager
    {
        public class ButtonInfo
        {
            public bool ShowText;
        }

        public class Application
        {
            public readonly string Name;
            public List<ToolStripDropDownButton> Buttons = new List<ToolStripDropDownButton>();
            public List<Item> Items = new List<Item>();

            public class Item
            {
                public string Title;
                public string Link;
            }

            public Application(string name)
            {
                this.Name = name;
            }

            public void DownloadFeed()
            {
                var req = WebRequest.Create("http://help.datadmin.com/dafeed/" + Name);
                using (var resp = req.GetResponse())
                {
                    var doc = new XmlDocument();
                    doc.Load(resp.GetResponseStream());
                    var items = new List<Item>();
                    foreach (XmlElement item in doc.SelectNodes("/rss/channel/item"))
                    {
                        var it = new Item();
                        it.Title = item.GetTextContent("title");
                        it.Link = item.GetTextContent("link");
                        items.Add(it);
                    }
                    Items = items;
                    MainWindow.Instance.RunInMainWindow(DispatchChanged);
                }
            }

            private void DispatchChanged()
            {
                var bts = new List<ToolStripDropDownButton>();
                lock (Buttons)
                {
                    bts.AddRange(Buttons);
                }
                foreach (var btn in bts)
                {
                    UpdateButton(btn);
                }
            }

            public void RegisterButton(ToolStripDropDownButton button, ButtonInfo bi)
            {
                lock (Buttons)
                {
                    Buttons.Add(button);
                }
                button.Disposed += new EventHandler(button_Disposed);
                button.Tag = bi;
                button.Text = bi.ShowText ? Texts.Get("s_tips") : "";
                button.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                UpdateButton(button);
            }

            void button_Disposed(object sender, EventArgs e)
            {
                lock (Buttons)
                {
                    Buttons.Remove((ToolStripDropDownButton)sender);
                }
            }

            private void UpdateButton(ToolStripDropDownButton btn)
            {
                btn.DropDownItems.Clear();
                var items = Items;
                var bi = (ButtonInfo)btn.Tag;
                if (items.Count > 0)
                {
                    btn.Enabled = true;
                    if (bi.ShowText) btn.Text = String.Format("{0} ({1})", Texts.Get("s_tips"), items.Count);
                    else btn.Text = String.Format("({0})", items.Count);
                    foreach (var item in items)
                    {
                        var it = btn.DropDownItems.Add(item.Title);
                        it.Tag = item;
                        it.Click += new EventHandler(it_Click);
                    }
                    btn.ToolTipText = Texts.Get("s_online_help");
                }
                else
                {
                    btn.Enabled = false;
                    btn.Text = bi.ShowText ? Texts.Get("s_tips") : "";
                    btn.ToolTipText = Texts.Get("s_no_help_available");
                }
            }

            void it_Click(object sender, EventArgs e)
            {
                var btn = (ToolStripItem)sender;
                var item = (Item)btn.Tag;
                UsageStats.Usage("context_online_help", "url", item.Link);
                System.Diagnostics.Process.Start(item.Link);
            }
        }

        static Dictionary<string, Application> m_apps = new Dictionary<string, Application>();
        static WaitQueue<Application> m_downloadQueue = new WaitQueue<Application>();
        static Thread m_thread;

        private static void ThreadProc()
        {
            for (; ; )
            {
                var elem = m_downloadQueue.Get();
                try
                {
                    elem.DownloadFeed();
                }
                catch
                {
                    Logging.Error("Error downloading online help feed:" + elem.Name);
                }
            }
        }

        private static Application WantApp(string appname)
        {
            lock (m_apps)
            {
                if (!m_apps.ContainsKey(appname))
                {
                    var app = new Application(appname);
                    m_apps[appname] = app;
                    if (m_thread == null)
                    {
                        m_thread = new Thread(ThreadProc);
                        m_thread.IsBackground = true;
                        m_thread.Start();
                    }
                    m_downloadQueue.Put(app);
                }
                return m_apps[appname];
            }
        }

        public static void RegisterHelpButton(ToolStripDropDownButton button, string appname)
        {
            RegisterHelpButton(button, appname, false);
        }


        public static void RegisterHelpButton(ToolStripDropDownButton button, string appname, bool showtext)
        {
            var app = WantApp(appname);
            app.RegisterButton(button, new ButtonInfo { ShowText = showtext });
        }
    }
}
