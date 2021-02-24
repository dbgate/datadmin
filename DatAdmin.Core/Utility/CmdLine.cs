using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace DatAdmin
{
    public class CmdLine
    {
        public static ICommandLineCommandInstance LoadCommand(string[] args)
        {
            if (args.Length < 1) throw new CommandLineError("DAE-00260 Missing command parameter");
            ICommandLineCommand cmd = FindCommand(args[0]);
            if (cmd != null)
            {
                ICommandLineCommandInstance res = cmd.CreateInstance();
                Dictionary<string, string> extparams = null;
                if (cmd.AllowExtParams) extparams = new Dictionary<string, string>();
                LoadParameters(PyList.SliceFrom(args, 1), new object[] { res }, extparams);
                res.ExtParams = extparams;
                return res;
            }
            throw new CommandLineError("DAE-00261 Command not defined:" + args[0]);
        }

        public static ICommandLineCommand FindCommand(string cmdname)
        {
            try
            {
                return (ICommandLineCommand)CommandLineCommandAddonType.Instance.FindHolder(cmdname).InstanceModel;
            }
            catch
            {
                return null;
            }
        }

        public static void LoadParameters(string[] args, object[] paramObjs, Dictionary<string,string> extparams)
        {
            int position = 0;
            List<ParamHolder> holders = LoadHolders(paramObjs);
            Dictionary<string, bool> specifiedPars = new Dictionary<string, bool>();
            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                if (arg.StartsWith("-"))
                { // named parameter
                    bool islong = arg.StartsWith("--");
                    string argname = islong ? arg.Substring(2) : arg.Substring(1);
                    bool found = false;
                    foreach (ParamHolder hld in holders)
                    {
                        if ((islong && hld.Name == argname) || (!islong && hld.ShortName == argname))
                        {
                            specifiedPars[hld.Name] = true;
                            if (hld.RequiresValue)
                            {
                                i++;
                                if (i >= args.Length) throw new CommandLineError("DAE-00262 Missing parameter value:" + hld.Name);
                                hld.SetValue(args[i]);
                            }
                            else
                            {
                                hld.SetSwitchOn();
                            }
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        if (extparams != null)
                        {
                            i++;
                            extparams[argname] = args[i];
                        }
                        else
                        {
                            throw new CommandLineError("DAE-00263 Parameter " + argname + " not defined");
                        }
                    }
                }
                else
                { // positional parameter
                    bool found = false;
                    foreach (ParamHolder hld in holders)
                    {
                        if (hld.Position == position)
                        {
                            specifiedPars[hld.Name] = true;
                            hld.SetValue(arg);
                            found = true;
                            position++;
                            break;
                        }
                    }
                    if (!found) throw new CommandLineError("DAE-00264 Parameter #" + position.ToString() + " not defined");
                }
            }
            foreach (ParamHolder hld in holders)
            {
                if (!specifiedPars.ContainsKey(hld.Name))
                {
                    if (hld.DefaultValue != null) hld.SetValue(hld.DefaultValue);
                    else
                    {
                        if (hld.Mandatory)
                        {
                            throw new CommandLineError("DAE-00265 Mandatory parameter " + hld.Name + " not specified");
                        }
                    }
                }
            }
        }

        public static List<ParamHolder> LoadHolders(object[] paramObjs)
        {
            List<ParamHolder> res = new List<ParamHolder>();
            foreach (object obj in paramObjs)
            {
                LoadHolders(obj, res, null);
            }
            return res;
        }

        public static void LoadHolders(object obj, List<ParamHolder> res, string prefix)
        {
            int position = 0;
            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                foreach (CommandLineParameterAttribute attr in prop.GetCustomAttributes(typeof(CommandLineParameterAttribute), true))
                {
                    ParamHolder hld = new ParamHolder();
                    hld.m_attrib = attr;
                    hld.m_property = prop;
                    hld.m_object = obj;
                    hld.m_prefix = prefix;

                    if (attr.Positional)
                    {
                        hld.m_position = position;
                        position++;
                    }
                    res.Add(hld);
                }
                foreach (CommandLineParameterCollectionAttribute attr in prop.GetCustomAttributes(typeof(CommandLineParameterCollectionAttribute), true))
                {
                    object pval = prop.GetGetMethod().Invoke(obj, new object[] { });
                    LoadHolders(pval, res, attr.Prefix != null ? (prefix ?? "") + attr.Prefix : prefix);
                }
            }
        }

        public static void PrintHelp()
        {
            Console.Out.WriteLine("List of commands");
            foreach(var item in CommandLineCommandAddonType.Instance.CommonSpace.GetAllAddons())
            {
                ICommandLineCommand cmd = (ICommandLineCommand)item.InstanceModel;
                Console.Out.WriteLine("  " + cmd.Name);
            }
            Console.Out.WriteLine("Try \"daci help command\" for help about command");
        }

        public class ParamHolder
        {
            internal CommandLineParameterAttribute m_attrib;
            internal int? m_position;
            internal PropertyInfo m_property;
            internal string m_prefix;
            internal object m_object;

            public void SetSwitchOn()
            {
                m_property.SetValue(m_object, true, null);
            }

            public void SetValue(string value)
            {
                m_property.SetValue(m_object, Convert.ChangeType(value, m_property.PropertyType), null);
            }

            public bool RequiresValue { get { return m_property.PropertyType != typeof(bool); } }
            public int? Position { get { return m_position; } }
            public string Name { get { return (m_prefix ?? "") + m_attrib.Name; } }
            public string ShortName { get { return m_prefix == null ? m_attrib.ShortName : null; } }
            public string DefaultValue { get { return m_attrib.DefaultValue; } }
            public bool Mandatory { get { return m_attrib.Mandatory; } }
            public string Description { get { return m_attrib.Description; } }
        }
    }
}
