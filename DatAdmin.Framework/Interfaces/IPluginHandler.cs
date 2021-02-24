using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginHandlerAttribute : Attribute
    {
    }

    public interface IPluginHandler
    {
        void OnLoadAssembly();
        void OnLoadedAllPlugins();
    }

    public abstract class PluginHandlerBase : IPluginHandler
    {
        #region IPluginHandler Members

        public virtual void OnLoadedAllPlugins()
        {
        }

        public virtual void OnLoadAssembly()
        {
        }

        #endregion
    }
}
