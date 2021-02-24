using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Web;

namespace DatAdmin
{
    public class HtmlPanelEx : HtmlPanel
    {
        public Dictionary<string, object> Procedures { get; private set; }

        public HtmlPanelEx()
        {
            Procedures = new Dictionary<string, object>();
        }

        protected override void OnLinkClick(LinkClickEventArgs e)
        {
            base.OnLinkClick(e);

            string url = e.Href;
            if (url.StartsWith("'") && url.EndsWith("'")) url = url.Substring(1, url.Length - 2);
            if (url.StartsWith("\"") && url.EndsWith("\"")) url = url.Substring(1, url.Length - 2);
            if (url.StartsWith("http://") || url.StartsWith("mailto:"))
            {
                try { System.Diagnostics.Process.Start(url); }
                catch
                {
                    StdDialog.ShowError(Texts.Get("s_cannot_open$url", "url", url));
                }
                e.Handled = true;
            }
            if (url.StartsWith("callback://"))
            {
                e.Handled = true;
                try
                {
                    string mtdname = url.Substring("callback://".Length);
                    Dictionary<string, string> pars = null;
                    if (mtdname.Contains("?"))
                    {
                        pars = new Dictionary<string, string>();
                        string[] pair = mtdname.Split('?');
                        mtdname = pair[0];
                        foreach (string parstr in pair[1].Split('&'))
                        {
                            string[] p2 = parstr.Split(':');
                            if (p2.Length != 2) continue;
                            pars[p2[0]] = HttpUtility.UrlDecode(p2[1]);
                        }
                    }
                    if (mtdname.EndsWith("/")) mtdname = mtdname.Substring(0, mtdname.Length - 1);
                    object mtd = Procedures[mtdname];
                    if (mtd is Action)
                    {
                        ((Action)mtd)();
                    }
                    else if (mtd is Action<Dictionary<string, string>>)
                    {
                        ((Action<Dictionary<string, string>>)mtd)(pars);
                    }
                    else
                    {
                        // using reflection we call method, which name is after callback://
                        Type tp = mtd.GetType();
                        MethodInfo call = tp.GetMethod("Call", new Type[] { (new object[] { }).GetType() });
                        if (call != null) call.Invoke(mtd, new object[] { new object[] { } });
                        call = tp.GetMethod("Call", new Type[] { (new object[] { typeof(Dictionary<string, string>) }).GetType() });
                        if (call != null) call.Invoke(mtd, new object[] { new object[] { pars } });
                    }
                }
                catch (Exception err)
                {
                    Errors.Report(err);
                }
            }
        }
    }
}
