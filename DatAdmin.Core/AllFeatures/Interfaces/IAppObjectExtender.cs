using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DatAdmin
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AppObjectExtenderAttribute : RegisterAttribute
    {
    }

    public interface IAppObjectExtender : IAddonInstance
    {
        void GetAppObjectExtendObjects(AppObject appobj, List<object> objs);
        Bitmap GetImageOverride(AppObject appobj);
        void GetExtendedWidgets(AppObject appobj, List<IWidget> objviews);
    }

    public abstract class AppObjectExtenderBase : AddonBase, IAppObjectExtender
    {
        public override AddonType AddonType
        {
            get { return AppObjectExtenderAddonType.Instance; }
        }

        #region IAppObjectExtender Members

        public virtual void GetAppObjectExtendObjects(AppObject appobj, List<object> objs) { }
        public virtual Bitmap GetImageOverride(AppObject appobj) { return null; }
        public virtual void GetExtendedWidgets(AppObject appobj, List<IWidget> objviews) { }

        #endregion

        public class AppObjectExtenderInternalBase
        {
            protected readonly AppObject m_appobj;
            public AppObjectExtenderInternalBase(AppObject appobj)
            {
                m_appobj = appobj;
            }
        }
    }

    [AddonType]
    public class AppObjectExtenderAddonType : AddonType
    {
        public override string Name
        {
            get { return "appobjectextender"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(IAppObjectExtender); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(AppObjectExtenderAttribute); }
        }

        public static readonly AppObjectExtenderAddonType Instance = new AppObjectExtenderAddonType();
    }
}
