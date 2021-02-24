using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace DatAdmin
{
    public abstract class FilePlaceBase : AddonBase, IFilePlace
    {
        [Browsable(false)]
        public override AddonType AddonType
        {
            get { return FilePlaceAddonType.Instance; }
        }

        #region IFilePlace Members

        [Browsable(false)]
        public IExtendedFileNameHolderInfo ContainerInfo { get; protected set; }

        [Browsable(false)]
        public virtual int Priority
        {
            get { return 0; }
        }

        public virtual bool SupportsSave(IExtendedFileNameHolderInfo info)
        {
            return false;
        }

        public virtual bool SupportsLoad(IExtendedFileNameHolderInfo info)
        {
            return false;
        }

        [Browsable(false)]
        public IProgressInfo ProgressInfo { get; set; }

        public abstract bool LoadVirtualFile(string virtualFile, IExtendedFileNameHolderInfo holder);

        public abstract string GetWorkingFileName();

        public virtual void FinalizeFileName()
        {
        }

        public abstract string GetVirtualFile();

        public virtual void Dispose()
        {
        }

        public virtual void CleanUp()
        {
        }

        public virtual void CheckInput() { }
        public virtual void CheckOutput() { }

        public virtual void SetFileHolderInfo(IExtendedFileNameHolderInfo info)
        {
            ContainerInfo = info;
        }

        public virtual void DeleteFile() { }

        #endregion

        public override string ToString()
        {
            return GetVirtualFile();
        }

        protected static string LoadProp(string virtualFile, string prop)
        {
            var re = new Regex(String.Format(@"\[{0}\](.*)\[\/{0}\]", prop));
            var m = re.Match(virtualFile);
            if (m.Success) return m.Groups[1].Value;
            return "";
        }
    }

    public abstract class TempFilePlaceBase : FilePlaceBase
    {
        protected string m_tempFile;

        public override string GetWorkingFileName()
        {
            PreparePlace();
            m_tempFile = Core.GetTempFile(ContainerInfo.FileExtension);
            if (!ContainerInfo.DirectionIsSave) PrepareReadFileContent(m_tempFile);
            return m_tempFile;
        }
        protected virtual void PrepareReadFileContent(string file) { }
        protected virtual void AfterWriteAction(string file) { }
        protected virtual void PreparePlace() { }
        public override void FinalizeFileName()
        {
            if (ContainerInfo.DirectionIsSave) AfterWriteAction(m_tempFile);
            CleanUp();
        }

        public override void Dispose()
        {
            CleanUp();
        }
    }
}
