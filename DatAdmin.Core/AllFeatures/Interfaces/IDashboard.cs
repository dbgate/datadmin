using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DatAdmin
{
    public class DashboardCaps
    {
        public bool ShowNodeToolbar;

        public bool AllFlags
        {
            set
            {
                ShowNodeToolbar = value;
            }
        }
    }

    public interface IDashboard : IAddonInstance
    {
        bool SuitableFor(AppObject appobj);
        //AppObject SelectedObject { get; set; }
        Control CreateControl(DashboardInstanceParams pars);
        int Priority { get; }
        DashboardCaps Caps { get; }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class DashboardAttribute : RegisterAttribute
    {
    }

    [AddonType]
    public class DashboardAddonType : AddonType
    {
        public override string Name
        {
            get { return "dashboard"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(IDashboard); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(DashboardAttribute); }
        }

        public override string FileExtension
        {
            get { return ".das"; }
        }

        public override string GetDirectory()
        {
            return Core.DashboardsDirectory;
        }

        public static readonly DashboardAddonType Instance = new DashboardAddonType();
    }
}
