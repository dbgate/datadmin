using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using DatAdmin;

namespace Plugin.apps
{
    public class ApplicationWidgetAttribute : RegisterAttribute { }

    //public interface IApplicationWidgetFactory
    //{
    //    string Title { get; }
    //    string Name { get; }
    //    Bitmap Image { get; }
    //}

    [AddonType]
    public class ApplicationWidgetAddonType : AddonType
    {
        public override string Name
        {
            get { return "appwidget"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(AppWidget); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(ApplicationWidgetAttribute); }
        }

        public static readonly ApplicationWidgetAddonType Instance = new ApplicationWidgetAddonType();
    }

    public interface IWidgetControl
    {
        AppWidget Widget { get; }
        Control Control { get; }
        AppPageInstance PageInstance { get; }
        DatAdmin.Scripting.AppWidget CreateScriptingControl();
        void ProcessNotify();
    }
}
