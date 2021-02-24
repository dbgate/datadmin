using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DatAdmin
{
    public class FavoriteGroupTreeNode : VirtualFolderTreeNode
    {
        FavoriteGroup m_group;

        public FavoriteGroupTreeNode(ITreeNode parent, FavoriteGroup group)
            : base(parent, new DiskFolder(System.IO.Path.Combine(Core.FavoritesDirectory, group.Name)), group.Name)
        {
            m_group = group;
        }

        public FavoriteGroup Group
        {
            get { return m_group; }
        }

        public override string Title
        {
            get { return Texts.Get(m_group.Title); }
        }

        public override Bitmap Image
        {
            get { return CoreIcons.favorite; }
        }

        public override Bitmap ExpandedImage
        {
            get { return CoreIcons.favorite; }
        }

        //public override ITreeNode[] GetChildren()
        //{
        //    var res = new List<ITreeNode>();
        //    foreach (var fav in Favorites.Instance.GetItemsForGroup(Name))
        //    {
        //        res.Add(new FavoriteItemTreeNode(this, fav));
        //    }
        //    return res.ToArray();
        //}

        public override string TypeTitle
        {
            get { return "s_favorites"; }
        }

        //public override bool AllowDragDrop(ITreeNode draggingNode)
        //{
        //    return draggingNode is FavoriteItemTreeNode;
        //}

        public override bool AllowGenericFolderExtenders()
        {
            return false;
        }

        //public override bool DragDrop_FileVisible(ITreeNode draggingNode)
        //{
        //    return false;
        //}

        [DragDropOperationVisible(Name = "copy_first")]
        [DragDropOperationVisible(Name = "copy_last")]
        public bool CopyDragDropVisible(AppObject appobj)
        {
            return appobj is FavoriteAppObject;
        }

        [DragDropOperation(Name = "copy_first", Title = "s_copy_first")]
        public void CopyFirst(AppObject appobj)
        {
            var n = appobj as FavoriteAppObject;
            var fav = n.LoadHolder().Clone();
            fav.ChangeGroup(m_group.Name);
            Favorites.AddFirst(fav);
            Favorites.NotifyChanged();
            this.CompleteRefresh();
        }

        [DragDropOperation(Name = "copy_last", Title = "s_copy_last")]
        public void CopyLast(AppObject appobj)
        {
            var n = appobj as FavoriteAppObject;
            var fav = n.LoadHolder().Clone();
            fav.ChangeGroup(m_group.Name);
            Favorites.AddLast(fav);
            Favorites.NotifyChanged();
            this.CompleteRefresh();
        }

        protected override int CompareFileNodes(ITreeNode a, ITreeNode b)
        {
            var fa = a as FavoriteItemTreeNode;
            var fb = b as FavoriteItemTreeNode;
            if (fa != null && fb != null) return fa.Favorite.Position - fb.Favorite.Position;
            return base.CompareFileNodes(a, b);
        }

        public override bool AllowDelete()
        {
            return false;
        }

        public override bool AllowRename()
        {
            return false;
        }
    }

    public class FavoriteItemTreeNode : VirtualFileTreeNodeBase
    {
        FavoriteHolder m_favorite;

        public FavoriteItemTreeNode(ITreeNode parent, IFileHandler fhandler, FavoriteHolder favorite)
            : base(parent, fhandler)
        {
            m_favorite = favorite;
            SetAppObject(new FavoriteAppObject { FileName = favorite.File });
        }

        public FavoriteHolder Favorite { get { return m_favorite; } }

        public override Bitmap Image
        {
            get { return m_favorite.Favorite.Image; }
        }

        public override string Title
        {
            get { return m_favorite.Name; }
        }

        public override List<IWidget> GetWidgets()
        {
            var res = base.GetWidgets();
            m_favorite.Favorite.GetWidgets(res);
            return res;
        }

        public override bool DoDelete()
        {
            if (base.DoDelete())
            {
                Favorites.NotifyChanged();
                return true;
            }
            return false;
        }


        public override string TypeTitle
        {
            get { return "s_favorites"; }
        }

        public override ITreeNode[] GetChildren()
        {
            return new ITreeNode[] { };
        }
    }

    [FileHandler(Name = "favorite")]
    public class FavoriteFileHandler : FileHandlerBase
    {
        public override string Extension
        {
            get { return "fav"; }
        }

        public override ITreeNode CreateNode(ITreeNode parent)
        {
            var appobj = new FavoriteAppObject { FileName = IOTool.RelativePathTo(Core.FavoritesDirectory, m_file.DataDiskPath) };
            return new FavoriteItemTreeNode(parent, this, appobj.LoadHolder());
        }

        public override FileHandlerCaps Caps
        {
            get
            {
                return new FileHandlerCaps
                {
                    AllFlags = false,
                    CreateNode = true
                };
            }
        }
    }

    //[NodeFactory(Name = "favorite")]
    //public class FavoriteNodeFactory : NodeFactoryBase
    //{
    //    public override ITreeNode FromFile(ITreeNode parent, string file)
    //    {
    //        string fn = file.ToLower();
    //        var favgrp = parent as FavoriteGroupTreeNode;

    //        if (fn.EndsWith(".fav") && favgrp != null)
    //        {
    //            try
    //            {
    //                return new FavoriteItemTreeNode(parent, new FavoriteHolder(file, favgrp.Group.Name));
    //            }
    //            catch (Exception)
    //            {
    //                return null;
    //            }
    //        }
    //        return null;
    //    }
    //}
}
