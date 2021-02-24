using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Xml;
using System.Linq;

namespace DatAdmin
{
    public class TunnelConnectionStringBuilder : DbConnectionStringBuilder
    {
        public new string this[string arg]
        {
            get
            {
                if (ContainsKey(arg)) return base[arg].ToString();
                return null;
            }
            set { base[arg] = value; }
        }

        public string Get(string arg, string defvalue)
        {
            if (ContainsKey(arg)) return this[arg];
            return defvalue;
        }

        //public Dictionary<string, string> AsDictionary
        //{
        //    get
        //    {
        //        var res = new Dictionary<string, string>();
        //        foreach (string key in Keys) res[key] = this[key];
        //        return res;
        //    }
        //    set
        //    {
        //        Clear();
        //        foreach (string key in value.Keys) this[key] = value[key];
        //    }
        //}

        //public void Save(XmlElement xml)
        //{
        //    foreach (string key in Keys)
        //    {
        //        if (key.ToLower().Contains("password"))
        //        {
        //            xml.AddChild(key).InnerText = XmlTool.SafeEncodeString(this[key]);
        //        }
        //        else
        //        {
        //            xml.AddChild(key).InnerText = this[key];
        //        }
        //    }
        //}

        //public void Load(XmlElement xml)
        //{
        //    Clear();
        //    var sup = this.SupportedKeys;
        //    sup = new HashSetEx<string>(from s in sup select s.ToLower());
        //    foreach (XmlElement child in xml)
        //    {
        //        if (!sup.Contains(child.Name.ToLower())) continue;
        //        if (child.Name.ToLower().Contains("pass"))
        //        {
        //            this[child.Name] = XmlTool.SafeDecodeString(child.InnerText);
        //        }
        //        else
        //        {
        //            this[child.Name] = child.InnerText;
        //        }
        //    }
        //}

        public string Host
        {
            get { return this["Host"]; }
            set { this["Host"] = value; }
        }

        public string Login
        {
            get { return this["Login"]; }
            set { this["Login"] = value; }
        }

        public string Password
        {
            get { return this["Password"]; }
            set { this["Password"] = value; }
        }

        public string InitialDatabase
        {
            get { return this["InitialDatabase"]; }
            set { this["InitialDatabase"] = value; }
        }

        public string Engine
        {
            get { return this["Engine"]; }
            set { this["Engine"] = value; }
        }

        public int Port
        {
            get
            {
                if (ContainsKey("Port")) return Int32.Parse(this["Port"]);
                return 0;
            }
            set
            {
                if (value == 0) Remove("Port");
                else this["Port"] = value.ToString();
            }
        }

        //public virtual HashSetEx<string> SupportedKeys
        //{
        //    get
        //    {
        //        var res = new HashSetEx<string>();
        //        res.Add("Host");
        //        res.Add("Login");
        //        res.Add("Password");
        //        res.Add("InitialDatabase");
        //        res.Add("Engine");
        //        return res;
        //    }
        //}
    }
}
