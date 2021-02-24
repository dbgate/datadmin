using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    [AttributeUsage(AttributeTargets.Class)]
    public class QuickExportAttribute : RegisterAttribute
    {
    }

    public interface IQuickExport : IAddonInstance
    {
        ITabularDataStore GetDataStore();
    }

    [AddonType]
    public class QuickExportAddonType : AddonType
    {
        public override string Name
        {
            get { return "quickexport"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(IQuickExport); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(QuickExportAttribute); }
        }

        public static readonly QuickExportAddonType Instance = new QuickExportAddonType();
    }
}
