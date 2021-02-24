using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public interface IExtendedFileNameHolderInfo
    {
        string Filter { get; }
        string FileExtension { get; }
        bool DirectionIsSave { get; }
        AppObject RelatedObject { get; }
        IPhysicalConnectionFactory RelatedConnection { get; }
        string RelatedDatabase { get; }
    }

    public class ExtendedFileNameHolderInfo : IExtendedFileNameHolderInfo
    {
        public string Filter { get; set; }
        public string FileExtension { get; set; }
        public bool DirectionIsSave { get; set; }
        public AppObject RelatedObject { get; set; }
        public IPhysicalConnectionFactory RelatedConnection { get; set; }
        public string RelatedDatabase { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class FilePlaceAttribute : RegisterAttribute
    {
    }

    public interface IFilePlace : IDisposable
    {
        int Priority { get; }
        bool SupportsSave(IExtendedFileNameHolderInfo place);
        bool SupportsLoad(IExtendedFileNameHolderInfo place);
        IProgressInfo ProgressInfo { get; set; }

        IExtendedFileNameHolderInfo ContainerInfo { get; }
        bool LoadVirtualFile(string virtualFile, IExtendedFileNameHolderInfo holder);
        string GetWorkingFileName();
        void FinalizeFileName();
        string GetVirtualFile();

        void CheckInput();
        void CheckOutput();

        void CleanUp();
        void SetFileHolderInfo(IExtendedFileNameHolderInfo info);

        void DeleteFile();
    }

    [AddonType]
    public class FilePlaceAddonType : AddonType
    {
        public override string Name
        {
            get { return "fileplace"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(IFilePlace); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(FilePlaceAttribute); }
        }

        public static readonly FilePlaceAddonType Instance = new FilePlaceAddonType();

        public static IFilePlace PlaceFromVirtualFile(string virtualFile)
        {
            return PlaceFromVirtualFile(virtualFile, null);
        }

        public static IFilePlace PlaceFromVirtualFile(string virtualFile, IExtendedFileNameHolderInfo holder)
        {
            var lst = new List<IFilePlace>();
            foreach (var hld in Instance.CommonSpace.GetAllAddons())
            {
                lst.Add((IFilePlace)hld.CreateInstance());
            }
            lst.SortByKey(p => -p.Priority);
            foreach (var pl in lst)
            {
                if (pl.LoadVirtualFile(virtualFile, holder)) return pl;
            }
            throw new UnknownVirtualPathError("DAE-00188 Virtual path cannot be parsed:" + virtualFile);
        }
    }
}
