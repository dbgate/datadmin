using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Reflection;

namespace DatAdmin
{
    public class ListTypeConverter : TypeConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<string> res = new List<string>();
            res.Add("");
            try
            {
                foreach (Attribute attr in context.PropertyDescriptor.Attributes)
                {
                    var lm = attr as ListMethodAttribute;
                    if (lm == null) continue;
                    MethodInfo mtd = context.Instance.GetType().GetMethod(lm.MethodName, new Type[] { });
                    if (mtd != null)
                    {
                        object vals = mtd.Invoke(context.Instance, new object[] { });
                        res.AddRange((IEnumerable<string>)vals);
                    }
                }
            }
            catch { }

            System.ComponentModel.TypeConverter.StandardValuesCollection svc = new System.ComponentModel.TypeConverter.StandardValuesCollection(res);
            return svc;

        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ListMethodAttribute : Attribute
    {
        public readonly string MethodName;
        public ListMethodAttribute(string method)
        {
            MethodName = method;
        }
    }
}
