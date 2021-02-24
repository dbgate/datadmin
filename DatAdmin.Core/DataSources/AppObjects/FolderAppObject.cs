using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DatAdmin
{
    [AppObject(Name = "folder")]
    public class FolderAppObject : AppObject
    {
        public IVirtualFolder Folder { get; set; }

        public override bool SupportSerialize
        {
            get { return Folder == null || Folder.FileSystem != null; }
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            xml.AddChild("Path").InnerText = Folder.FullPath;
            Folder.FileSystem.SaveToXml(xml.AddChild("FileSystem"));
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            var fs = (IVirtualFileSystem)VirtualFileSystemAddonType.Instance.LoadAddon(xml.FindElement("FileSystem"));
            Folder = fs.GetFolder(xml.FindElement("Path").InnerText);
        }

        public override string TypeName
        {
            get { return "folder"; }
        }

        public override string TypeTitle
        {
            get { return "s_folder"; }
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.img_folder; ; }
        }

        [DragDropOperationVisible(Name = "movefile")]
        [DragDropOperationVisible(Name = "copyfile")]
        public virtual bool DragDrop_FileVisible(AppObject appobj)
        {
            return appobj is FileAppObject;
        }

        [DragDropOperation(Name = "movefile", Title = "s_move_here", MultiMode = MultipleMode.Sequencable)]
        public void DragDrop_MoveFile(AppObject appobj)
        {
            DragDropFile(appobj, CopyFileMode.Move);
        }

        [DragDropOperation(Name = "copyfile", Title = "s_copy_here", MultiMode = MultipleMode.Sequencable)]
        public void DragDrop_CopyFile(AppObject appobj)
        {
            DragDropFile(appobj, CopyFileMode.Copy);
        }

        private void DragDropFile(AppObject appobj, CopyFileMode mode)
        {
            if (appobj is FileAppObjectBase)
            {
                var ao = (FileAppObjectBase)appobj;
                var src = ao.GetFile();
                var newfile = Folder.GetFile(src.Name);
                if (newfile.Exists())
                {
                    if (!StdDialog.ReallyOverwriteFile(newfile.Name)) return;
                }
                ao.CopyFileTo(newfile, mode);
                //src.CopyFileTo(newfile, mode);
                //CopyVirtualFileTo(src, newfile, mode);
                CallCompleteChanged();
            }
        }

        [DragDropOperation(Name = "create_data_archive", Title = "s_create_data_archive")]
        public void DragDrop_CreateDataArchive(AppObject appobj)
        {
            var dobj = appobj as DatabaseAppObject;
            if (dobj == null) return;
            string dbname = dobj.DatabaseName;
            string fn = System.IO.Path.Combine(Folder.FolderDiskPath, (dbname ?? "") + ".dbk");
            CopyDbWizard.Run(dobj.FindDatabaseConnection(ConnPack).CloneSource(),
                new DataArchiveWriter { FilePlace = FilePlaceAddonType.PlaceFromVirtualFile(fn) }
                );
        }

        [DragDropOperationVisible(Name = "create_data_archive")]
        public bool DragDrop_DbVisible(AppObject appobj)
        {
            return appobj is DatabaseAppObject && Folder.FolderDiskPath != null;
        }

        protected override string GetConfirmDeleteMessage()
        {
            return Texts.Get("s_really_delete$folder", "folder", Folder.ToString("F"));
        }

        public override bool AllowDelete()
        {
            return Folder.Caps.Remove;
        }

        public override void DoDelete()
        {
            try { Folder.Remove(); }
            catch (DeleteError) { }
        }

        public override bool AllowRename()
        {
            return Folder.Caps.Rename;
        }

        public override void RenameObject(string newname)
        {
            Folder.RenameTo(newname);
        }

        public override string ToString()
        {
            return Folder != null ? Folder.Name : null;
        }

        public override string GetRenamingText()
        {
            return Folder != null ? Folder.Name : null;
        }
    }
}
