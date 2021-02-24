using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace DatAdmin
{
    //public class ConfigLoaderState
    //{
    //    public string Mode = "appdata";
    //}

    //public interface IConfigFileSpaceReader
    //{
    //    string[] GetFiles(string folder);
    //    string[] GetFolders(string folder);
    //    Stream ReadFile(string file);
    //}

    public interface IConfigNodeHandler
    {
        ConfigFileNode LoadFile(ITreeNode parent, IVirtualFile file);
        ConfigFolderNode LoadFolder(ITreeNode parent, IVirtualFolder folder);
        ConfigFolderNode LoadSubFolder(ConfigFileNode parent, IVirtualFolder folder);
        ConfigFileNode LoadSubFile(ConfigFileNode parent, IVirtualFile file);
    }

    //public class TransferConfigNode
    //{
    //    public string Path;
    //    public string  
    //}

    //public interface ITransferConfigHandler
    //{

    //}

    public class ConfigNodeHandlerAttribute : RegisterAttribute { }

    [AddonType]
    public class ConfigNodeHandlerAddonType : AddonType
    {
        public override string Name
        {
            get { return "confignodehandler"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(IConfigNodeHandler); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(ConfigNodeHandlerAttribute); }
        }

        public static readonly ConfigNodeHandlerAddonType Instance = new ConfigNodeHandlerAddonType();

        public ConfigFileNode LoadFile(ITreeNode parent, IVirtualFile file)
        {
            foreach (var holder in CommonSpace.GetAllAddons())
            {
                var hnd = (IConfigNodeHandler)holder.InstanceModel;
                var node = hnd.LoadFile(parent, file);
                if (node != null) return node;
            }
            return null;
        }

        public ConfigFolderNode LoadSubFolder(ConfigFileNode parent, IVirtualFolder folder)
        {
            foreach (var holder in CommonSpace.GetAllAddons())
            {
                var hnd = (IConfigNodeHandler)holder.InstanceModel;
                var node = hnd.LoadSubFolder(parent, folder);
                if (node != null) return node;
            }
            return null;
        }

        public ConfigFolderNode LoadFolder(ITreeNode parent, IVirtualFolder folder)
        {
            foreach (var holder in CommonSpace.GetAllAddons())
            {
                var hnd = (IConfigNodeHandler)holder.InstanceModel;
                var node = hnd.LoadFolder(parent, folder);
                if (node != null) return node;
            }
            return null;
        }

        public ConfigFileNode LoadSubFile(ConfigFileNode parent, IVirtualFile file)
        {
            foreach (var holder in CommonSpace.GetAllAddons())
            {
                var hnd = (IConfigNodeHandler)holder.InstanceModel;
                var node = hnd.LoadSubFile(parent, file);
                if (node != null) return node;
            }
            return null;
        }
    }
}
