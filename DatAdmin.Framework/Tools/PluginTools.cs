using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace DatAdmin
{
    public delegate void LoadPluginNotifyDelegate(string plugin, int index, int count);

    public static class PluginTools
    {
        static List<IPluginHandler> m_pluginHandlers = new List<IPluginHandler>();

        public static void LoadPlugins()
        {
            LoadPlugins(null);
        }
        public static void LoadPluginHandlers(Assembly asm)
        {
            foreach (var tp in asm.GetTypes())
            {
                foreach (var attr in tp.GetCustomAttributes(typeof(PluginHandlerAttribute), true))
                {
                    var hnd = (IPluginHandler)tp.CreateNewInstance();
                    m_pluginHandlers.Add(hnd);
                    hnd.OnLoadAssembly();
                }
            }
        }
        public static void LoadPlugins(LoadPluginNotifyDelegate callback)
        {
            LoadPluginHandlers(Assembly.GetAssembly(typeof(Framework)));
            foreach (var asm in AddonRegister.CoreAssemblies) LoadPluginHandlers(asm);

            List<string> plugins = new List<string>();
            foreach (string file in Directory.GetFiles(Framework.PluginsDirectory))
            {
                string name = Path.GetFileName(file);
                if (name.ToLower().StartsWith("plugin.") && name.ToLower().EndsWith(".dll") && VersionInfo.AllowLoadPlugin(name))
                {
                    // old plugin - ignore
                    if (name.ToLower() == "plugin.phptunnel.dll") continue;
                    plugins.Add(file);
                }
            }

            var asms = new List<Assembly>();
            int index = 0;
            foreach (string file in plugins)
            {
                if (callback != null) callback(Path.GetFileName(file), index, plugins.Count);
                try
                {
                    Assembly asm = Assembly.LoadFile(file);
                    LoadPluginHandlers(asm);
                    AddonRegister.Instance.AddAssembly(asm);
                    asms.Add(asm);
                }
                catch (Exception err)
                {
                    Logging.Error("Error loading plugin {0}: {1}", Path.GetFileName(file), err.Message);
                }
                //Plugins.AddAssembly(asm);
                index++;
            }

            foreach (var hnd in m_pluginHandlers)
            {
                hnd.OnLoadedAllPlugins();
            }
        }

        public static void AddMasterAssembly(Assembly assembly)
        {
            PluginTools.LoadPluginHandlers(assembly);
            AddonRegister.Instance.AddAssembly(assembly);
        }
    }
}
