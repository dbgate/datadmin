using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace DatAdmin
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class PersistentImage
    {
        [Browsable(false)]
        public Bitmap GdiBitmap
        {
            get
            {
                if (String.IsNullOrEmpty(CoreIcon)) return null;
                try
                {
                    return CoreIcons.IconTable[CoreIcon];
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        string m_coreIcon = "";
        [TypeConverter(typeof(CoreIconConverter))]
        public string CoreIcon
        {
            get { return m_coreIcon; }
            set { m_coreIcon = value; }
        }

        [Browsable(false)]
        public bool IsDefined
        {
            get
            {
                return GdiBitmap != null;
            }
        }

        public override string ToString()
        {
            if (IsDefined) return "None";
            return CoreIcon;
        }
    }

    public class CoreIconConverter : TypeConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new System.ComponentModel.TypeConverter.StandardValuesCollection(CoreIcons.IconTable.Keys.Sorted());
        }
    }
}
