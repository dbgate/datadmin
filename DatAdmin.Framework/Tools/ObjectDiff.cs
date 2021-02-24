using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Reflection;
using System.Collections;
using System.Drawing;
using System.IO;

namespace DatAdmin
{
    public static class ObjectDiff
    {
        private static IEnumerable<PropertyInfo> GetSerializableProperties(object target, object diffBase)
        {
            if (target == null || diffBase == null)
            {
                yield break;
            }
            Type type = target.GetType();
            if (type != diffBase.GetType()) throw new Exception("DAE-00310 diffing different objects");
            foreach (PropertyInfo prop in type.GetProperties())
            {
                if (prop.GetGetMethod() == null || prop.GetSetMethod() == null) continue;
                if (prop.IsIndexProperty()) continue;
                if (prop.GetCustomAttributes(typeof(XmlIgnoreAttribute), true).Length > 0) continue;
                yield return prop;
            }
        }

        public static void SaveDiff(object target, object diffBase, XmlElement xml)
        {
            using (NeutralCultureObject nco = new NeutralCultureObject())
            {
                foreach (PropertyInfo prop in GetSerializableProperties(target, diffBase))
                {
                    Type proptype = prop.PropertyType;

                    if (typeof(IList).IsAssignableFrom(proptype))
                    {
                        XmlElement lst = xml.OwnerDocument.CreateElement("List");
                        IList dstlist = (IList)prop.CallGet(target);
                        IList srclist = (IList)prop.CallGet(diffBase);
                        for (int i = 0; i < dstlist.Count; i++)
                        {
                            XmlElement item = xml.OwnerDocument.CreateElement("Item");
                            object dstitem = dstlist[i];
                            object srcitem;
                            if (i < srclist.Count) srcitem = srclist[i];
                            else srcitem = proptype.CreateNewInstance();
                            SaveDiff(dstitem, srcitem, item);
                            if (item.ChildNodes.Count > 0)
                            {
                                item.SetAttribute("index", i.ToString());
                                lst.AppendChild(item);
                            }
                        }
                        if (lst.ChildNodes.Count > 0)
                        {
                            lst.SetAttribute("name", prop.Name);
                            xml.AppendChild(lst);
                        }
                    }
                    else if (proptype.IsEnum)
                    {
                        SaveProperty<object>(prop, target, diffBase, xml, val => Enum.GetName(proptype, val));
                    }
                    else if (proptype == typeof(string) || proptype.IsPrimitive)
                    {
                        SaveProperty<object>(prop, target, diffBase, xml, val => val.ToString());
                    }
                    else if (proptype.IsClass)
                    {
                        XmlElement mem = xml.OwnerDocument.CreateElement("Member");
                        SaveDiff(prop.CallGet(target), prop.CallGet(diffBase), mem);
                        if (mem.ChildNodes.Count > 0)
                        {
                            mem.SetAttribute("name", prop.Name);
                            xml.AppendChild(mem);
                        }
                    }
                    else if (proptype == typeof(Color))
                    {
                        SaveProperty<Color>(prop, target, diffBase, xml, val => val.Name);
                    }
                }
            }
        }

        private static void SaveProperty<T>(PropertyInfo prop, object target, object diffBase, XmlElement xml, Func<T, string> getvalue)
        {
            if (!prop.CallGet(target).Equals(prop.CallGet(diffBase)))
            {
                object value = prop.CallGet(target);
                XmlElement fld = XmlTool.AddChild(xml, "Property");
                fld.SetAttribute("name", prop.Name);
                fld.SetAttribute("value", getvalue((T)value));
            }
        }

        private static void LoadProperty<T>(PropertyInfo prop, object target, XmlElement xml, Func<string, T> getvalue)
        {
            string value = xml.GetAttribute("value");
            T val = getvalue(value);
            prop.CallSet(target, val);
        }

        public static void LoadDiff(object target, XmlElement xml)
        {
            using (NeutralCultureObject nco = new NeutralCultureObject())
            {

                Type type = target.GetType();
                foreach (XmlElement child in xml)
                {
                    string name = child.GetAttribute("name");
                    PropertyInfo prop = type.GetProperty(name);
                    if (prop == null) continue;
                    if (prop.GetCustomAttributes(typeof(XmlIgnoreAttribute), true).Length > 0) continue;
                    Type proptype = prop.PropertyType;

                    if (child.Name == "Member")
                    {
                        LoadDiff(prop.CallGet(target), child);
                    }
                    else if (child.Name == "Property")
                    {
                        if (proptype.IsEnum)
                        {
                            LoadProperty<object>(prop, target, child, val => Enum.Parse(proptype, val));
                        }
                        else if (proptype == typeof(string) || proptype.IsPrimitive)
                        {
                            LoadProperty<object>(prop, target, child, val => Convert.ChangeType(val, proptype));
                        }
                        else if (proptype == typeof(Color))
                        {
                            LoadProperty<Color>(prop, target, child, val => Color.FromName(val));
                        }
                    }
                    else if (child.Name == "List")
                    {
                        IList dstlist = (IList)prop.CallGet(target);
                        foreach (XmlElement item in child)
                        {
                            int index = Int32.Parse(item.GetAttribute("index"));
                            while (dstlist.Count <= index) dstlist.Add(proptype.CreateNewInstance());
                            LoadDiff(dstlist[index], item);
                        }
                    }
                }
            }
        }

        public static string DiffToCsharp3(Type type, XmlElement xml)
        {
            StringWriter sw = new StringWriter();
            int varcount = 0;
            DiffToCsharp3(type, xml, sw, type.Name.ToLower() + "1", ref varcount);
            return sw.ToString();
        }

        public static void DiffToCsharp3(Type type, XmlElement xml, TextWriter tw, string varname, ref int varcount)
        {
            using (NeutralCultureObject nco = new NeutralCultureObject())
            {
                foreach (XmlElement child in xml)
                {
                    string name = child.GetAttribute("name");
                    PropertyInfo prop = type.GetProperty(name);
                    if (prop == null) continue;
                    if (prop.GetCustomAttributes(typeof(XmlIgnoreAttribute), true).Length > 0) continue;
                    Type proptype = prop.PropertyType;

                    if (child.Name == "Member")
                    {
                        string newvar = String.Format("v{0}", varcount);
                        varcount++;
                        tw.Write("var {0} = {1}.{2};\n", newvar, varname, name);
                        DiffToCsharp3(proptype, child, tw, newvar, ref varcount);
                    }
                    else if (child.Name == "Property")
                    {
                        if (proptype.IsEnum)
                        {
                            tw.Write("{0}.{1} = {2}.{3};\n", varname, name, proptype.FullName.Replace("+", "."), child.GetAttribute("value"));
                        }
                        else if (proptype == typeof(string))
                        {
                            tw.Write("{0}.{1} = \"{2}\";\n", varname, name, child.GetAttribute("value"));
                        }
                        else if (proptype == typeof(bool))
                        {
                            tw.Write("{0}.{1} = {2};\n", varname, name, child.GetAttribute("value") == "True" ? "true" : "false");
                        }
                        else if (proptype.IsPrimitive)
                        {
                            tw.Write("{0}.{1} = {2};\n", varname, name, child.GetAttribute("value"));
                        }
                        else if (proptype == typeof(Color))
                        {
                            tw.Write("{0}.{1} = Color.FromName(\"{2}\");\n", varname, name, child.GetAttribute("value"));
                        }
                    }
                    else if (child.Name == "List")
                    {
                        foreach (XmlElement item in child)
                        {
                            int index = Int32.Parse(item.GetAttribute("index"));
                            string newvar = String.Format("v{0}", varcount);
                            varcount++;
                            tw.Write("var {0} = {1}.{2}[{3}];\n", newvar, varname, name, index);
                            DiffToCsharp3(proptype.GetElementType(), item, tw, newvar, ref varcount);
                        }
                    }
                }
            }
        }
    }
}
