using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

namespace DatAdmin
{
    [AppObject(Name = "favorite")]
    public class FavoriteAppObject : FileAppObjectBase
    {
        /// <summary>
        /// file name relative to favorites folder
        /// </summary>
        [XmlElem]
        public string FileName { get; set; }

        public override string TypeName
        {
            get { return "favorite"; }
        }

        public override IVirtualFile GetFile()
        {
            return new DiskFile(Path.Combine(Core.FavoritesDirectory, FileName));
        }

        public override string TypeTitle
        {
            get { return "s_favorite"; }
        }

        public override Bitmap Image
        {
            get { return CoreIcons.favorite; }
        }

        public string GetGroup()
        {
            int pos = FileName.IndexOfAny(new char[] { '/', '\\' });
            if (pos >= 0) return FileName.Substring(0, pos).ToLower();
            return "";
        }

        public FavoriteHolder LoadHolder()
        {
            return new FavoriteHolder(Path.Combine(Core.FavoritesDirectory, FileName), GetGroup());
        }

        public override void RenameObject(string newname)
        {
            base.RenameObject(newname);
            Favorites.NotifyChanged();
        }

        public override void DoDelete()
        {
            base.DoDelete();
            Favorites.NotifyChanged();
        }

        [PopupMenu("s_open", ImageName = CoreIcons.runName)]
        public void Open()
        {
            LoadHolder().Favorite.Open();
        }

        public override bool DefaultAction()
        {
            Open();
            return true;
        }

        [DragDropOperationVisible(Name = "move_before")]
        [DragDropOperationVisible(Name = "move_after")]
        public bool DragDropVisible(AppObject appobj)
        {
            return appobj is FavoriteAppObject;
        }

        [DragDropOperation(Name = "move_before", Title = "s_move_before")]
        public void MoveBefore(AppObject appobj)
        {
            MoveCommand(appobj, 0);
        }

        [DragDropOperation(Name = "move_after", Title = "s_move_after")]
        public void MoveAfter(AppObject appobj)
        {
            MoveCommand(appobj, 1);
        }

        private void MoveCommand(AppObject appobj, int d)
        {
            var holder = LoadHolder();
            var f = appobj as FavoriteAppObject;
            //var n = node as FavoriteItemTreeNode;
            var hld = f.LoadHolder();
            var newCopy = hld.Clone();
            newCopy.ChangeGroup(holder.Group);
            Favorites.Move(holder, hld, newCopy, d);
            CallCompleteChanged();
            //Parent.CompleteRefresh();
            //hld.Parent.CompleteRefresh();
            Favorites.NotifyChanged();
        }
    }
}
