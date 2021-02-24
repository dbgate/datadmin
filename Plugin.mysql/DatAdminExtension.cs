using System;
using System.Collections.Generic;
using System.Text;
using Plugin.mysql;

namespace DatAdmin
{
    public static class DatAdminExtension
    {
        public static void MySet(this AbstractObjectStructure obj, string name, string value)
        {
            obj.SpecificData["mysql." + name] = value;
        }

        public static void MySet(this ColumnReference colref, string name, string value)
        {
            colref.SpecificData["mysql." + name] = value;
        }
    }
}
