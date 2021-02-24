using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

//namespace DatAdmin
//{
//    [AttributeUsage(AttributeTargets.Class)]
//    public class ObjectViewAttribute : RegisterAttribute
//    {
//    }

//    public interface IObjectView
//    {
//        string PageTitle { get;}
//        int Position { get;}
//        Control CreateView(AppObject ao, ConnectionPack connpack);
//        Control CreateView();
//        Bitmap Image { get; }
//        Type GetPageType(); // if page types are equal, pages can be reused
//    }

//    public interface IReusablePage
//    {
//        void ReloadView(AppObject ao, IObjectView view);
//    }

//    [AddonType]
//    public class ObjectViewAddonType : AddonType
//    {
//        public override string Name
//        {
//            get { return "objectview"; }
//        }

//        public override Type InterfaceType
//        {
//            get { return typeof(IObjectView); }
//        }

//        public override Type RegisterAttributeType
//        {
//            get { return typeof(ObjectViewAttribute); }
//        }

//        public static readonly ObjectViewAddonType Instance = new ObjectViewAddonType();
//    }
//}
