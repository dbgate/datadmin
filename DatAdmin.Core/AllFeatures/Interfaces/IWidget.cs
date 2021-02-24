using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DatAdmin
{
    [AttributeUsage(AttributeTargets.Class)]
    public class WidgetAttribute : RegisterAttribute
    {
        public string Category;
    }

    public interface IWidget : IAddonInstance
    {
        /// <summary>
        /// short title, used as page title
        /// </summary>
        string PageTitle { get; }
        /// <summary>
        /// name of widget displayed in toolbox
        /// </summary>
        string ToolDisplayName { get; }
        /// <summary>
        /// category used in toolbox
        /// </summary>
        string ToolCategory { get; }
        /// <summary>
        /// weight; if smaller, widget is in left position in tabbed dashboard
        /// </summary>
        int Weight { get; }
        /// <summary>
        /// gets associated control
        /// control must not be created until this method is called
        /// </summary>
        WidgetBaseFrame GetControl();
        Bitmap Image { get; }
        /// <summary>
        /// gets type of associated control
        /// </summary>
        Type GetControlType();
        /// <summary>
        /// loads data from application object and shows it in control, if created
        /// </summary>
        void LoadWidgetData(AppObject ao);
        /// <summary>
        /// connection pack associated with widget
        /// </summary>
        ConnectionPack ConnPack { get; set; }

        /// <summary>
        /// returns whether this widget's control can be replaced with given widget
        /// replacing saves destroying and creating control
        /// </summary>
        bool CanReplaceWith(IWidget widget);
        /// <summary>
        /// replaces widget in associated control ( calls GetControl().SetWidget(widget) )
        /// </summary>
        /// <param name="widget">new widget of control</param>
        void ReplaceControl(IWidget widget);
        /// <summary>
        /// determines whether widget is currectly designing
        /// </summary>
        bool IsDesigning { get; set; }
    }

    //public interface IWidgetProvider
    //{
    //    List<IWidget> GetStandardWidgets();
    //    //void DefineAddWidgetMenu(IMenuBuilder mb);
    //}

    [AddonType]
    public class WidgetAddonType : AddonType
    {
        public override string Name
        {
            get { return "widget"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(IWidget); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(WidgetAttribute); }
        }

        public static readonly WidgetAddonType Instance = new WidgetAddonType();
    }
}
