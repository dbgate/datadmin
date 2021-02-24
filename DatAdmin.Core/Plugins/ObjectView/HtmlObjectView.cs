using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

namespace DatAdmin
{
    public interface IHtmlWidget : IWidget
    {
        string CreateHtml(AppObject appobj, ConnectionPack connpack, IDictionary<string, object> outnames);
    }

    public abstract class HtmlWidgetBase : WidgetBase, IHtmlWidget
    {
        protected override WidgetBaseFrame CreateControl()
        {
            return new HtmlWidgetFrame(this);
        }

        public override Type GetControlType()
        {
            return typeof(HtmlWidgetFrame);
        }

        public abstract string CreateHtml(AppObject appobj, ConnectionPack connpack, IDictionary<string, object> outnames);

        public static bool IsSupported { get { return true; } }

        public override System.Drawing.Bitmap DefaultImage
        {
            get { return CoreIcons.info; }
        }
    }
}
