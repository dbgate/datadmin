using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.effiproz
{
    [PluginHandler]
    public class EfzPluginHandler : PluginHandlerBase
    {
        public override void OnLoadedAllPlugins()
        {
            HTree.FilterFolderFiles += HTree_FilterFolderFiles;
        }

        void HTree_FilterFolderFiles(List<IVirtualFile> files)
        {
            const string SUFFIX = ".properties";
            var propfiles = files.FindAll(f => f.DataDiskPath != null && f.DataDiskPath.ToLower().EndsWith(SUFFIX));
            foreach (var pf in propfiles)
            {
                string pfnoext = pf.DataDiskPath.Substring(0, pf.DataDiskPath.Length - SUFFIX.Length);
                files.RemoveIf(f => f.DataDiskPath.StartsWith(pfnoext + ".") && f.DataDiskPath != pf.DataDiskPath);
            }
        }
        public override void OnLoadAssembly()
        {
            HSplash.CallAddModuleInfo(StdIcons.effiproz, "EffiProz");
        }
    }
}
