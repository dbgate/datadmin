using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    //[AttributeUsage(AttributeTargets.Class)]
    //public class FileFormatAttribute : RegisterAttribute
    //{
    //    //public string Extension;
    //    //public string Description;
    //}

    //public interface IFileFormat
    //{
    //    bool CanCreate { get;}
    //    bool CanLoad { get;}
    //    /// <summary>
    //    /// File extension - lowercase without dot (eg. "xml")
    //    /// </summary>
    //    string Extension { get;}
    //    string Description { get;}
    //    void Create(string path);
    //    void Load(string path);
    //}

    //[AddonType]
    //public class FileFormatAddonType : AddonType
    //{
    //    public override string Name
    //    {
    //        get { return "fileformat"; }
    //    }

    //    public override Type InterfaceType
    //    {
    //        get { return typeof(IFileFormat); }
    //    }

    //    public override Type RegisterAttributeType
    //    {
    //        get { return typeof(FileFormatAttribute); }
    //    }

    //    public static readonly FileFormatAddonType Instance = new FileFormatAddonType();

    //    public IFileFormat FindByExtension(string extension)
    //    {
    //        foreach (var item in CommonSpace.GetAllAddons())
    //        {
    //            IFileFormat fmt = (IFileFormat)item.InstanceModel;
    //            if (fmt.Extension == extension) return fmt;
    //        }
    //        throw new KeyNotFoundException(String.Format("Extension {0} not found", extension));
    //    }
    //}
}
