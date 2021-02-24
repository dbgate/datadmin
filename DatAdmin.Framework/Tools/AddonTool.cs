using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.ComponentModel;

namespace DatAdmin
{
    public static class AddonTool
    {
        public static string ExtractDesctiption(object o)
        {
            if (o == null) return null;
            foreach (RegisterAttribute attr in o.GetType().GetCustomAttributes(typeof(RegisterAttribute), true))
            {
                if (attr.Description != null) return attr.Description;
            }
            foreach (DescriptionAttribute attr in o.GetType().GetCustomAttributes(typeof(DescriptionAttribute), true))
            {
                if (attr != null) return attr.Description;
            }
            return null;
        }

        public static string ExtractAddonName(object o)
        {
            if (o == null) return null;
            foreach (RegisterAttribute attr in o.GetType().GetCustomAttributes(typeof(RegisterAttribute), true))
            {
                if (attr.Name != null) return attr.Name;
            }
            return null;
        }

    }
}
