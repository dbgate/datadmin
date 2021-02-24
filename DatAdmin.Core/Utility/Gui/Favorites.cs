using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Xml;
using System.Drawing;

namespace DatAdmin
{
    //public class Favorites
    //{
    //    public static Favorites Instance = new Favorites();

    //    public List<IFavorite> Items = new List<IFavorite>();
    //    public List<FavoriteGroup> Groups = new List<FavoriteGroup>();

    //    public static string FavoriteFile
    //    {
    //        get { return Path.Combine(Core.ConfigDirectory, "favorites.xml"); }
    //    }

    //    public Favorites()
    //    {
    //        Groups.Add(new FavoriteGroup(this, "toolbar", "s_toolbar", CoreIcons.favorite));
    //        Groups.Add(new FavoriteGroup(this, "menu", "s_menu", CoreIcons.favorite));
    //    }

    //    public List<IFavorite> GetItemsForGroup(string grp)
    //    {
    //        return (from f in Items where f.Group == grp select f).ToList();
    //    }

    //    public void Load()
    //    {
    //        try
    //        {
    //            if (!File.Exists(FavoriteFile)) return;
    //            var doc = new XmlDocument();
    //            doc.Load(FavoriteFile);
    //            foreach (XmlElement node in doc.DocumentElement.SelectNodes("Favorite"))
    //            {
    //                try
    //                {
    //                    var fav = (IFavorite)FavoriteAddonType.Instance.LoadAddon(node);
    //                    Items.Add(fav);
    //                }
    //                catch (Exception errinn)
    //                {
    //                    Logging.Warning("Errort loading favorite:" + errinn.Message);
    //                }
    //            }
    //        }
    //        catch (Exception err)
    //        {
    //            Logging.Warning("Errort loading favorites.xml" + err.Message);
    //        }
    //    }

    //    public void Save()
    //    {
    //        var doc = XmlTool.CreateDocument("Favorites");
    //        foreach (var item in Items)
    //        {
    //            item.SaveToXml(doc.DocumentElement.AddChild("Favorite"));
    //        }
    //        doc.Save(FavoriteFile);
    //    }

    //    public static void OnFinish()
    //    {
    //        Instance.Save();
    //    }

    //    public static void OnStart()
    //    {
    //        Instance.Load();
    //    }

    //    public void Delete(IFavorite favorite)
    //    {
    //        Items.Remove(favorite);
    //    }

    //    public static void NotifyChanged()
    //    {
    //        Instance.Save();
    //        HFavorites.CallChanged();
    //    }

    //    public void Add(IFavorite favorite)
    //    {
    //        Items.Add(favorite);
    //    }

    //    public FavoriteGroup GroupByName(string name)
    //    {
    //        return Groups.Find(g => g.Name == name);
    //    }

    //    public void MoveOrCopy(IFavorite rel, IFavorite moved, IFavorite newCopy, int d, bool deleteold)
    //    {
    //        int idx = Items.IndexOf(rel);
    //        if (idx < 0) Items.Insert(0, newCopy);
    //        else if (idx + d >= Items.Count) Items.Add(newCopy);
    //        else Items.Insert(idx + d, newCopy);

    //        if (deleteold)
    //        {
    //            Items.Remove(moved);
    //        }
    //    }

    //    public IFavorite FindFavorite(string group, string name)
    //    {
    //        return Items.FirstOrDefault(f => f.Group == group && f.Name == name);
    //    }
    //}

    //public class FavoriteGroup
    //{
    //    Favorites m_favorites;

    //    public readonly string Name;
    //    public readonly string Title;
    //    public readonly Bitmap Image;

    //    public FavoriteGroup(Favorites favorites, string name, string title, Bitmap image)
    //    {
    //        m_favorites = favorites;
    //        Name = name;
    //        Title = title;
    //        Image = image;
    //    }

    //    public override string ToString()
    //    {
    //        return Texts.Get(Title);
    //    }

    //    public List<IFavorite> GetItems()
    //    {
    //        return m_favorites.GetItemsForGroup(Name);
    //    }
    //}

    public class FavoriteHolder
    {
        public string File;
        public string Name;
        [XmlElem]
        public int Position { get; set; }
        public IFavorite Favorite;
        public string Group;

        public FavoriteHolder(string file, string group)
        {
            File = file;
            Group = group;
            Name = Path.GetFileNameWithoutExtension(file);
            if (System.IO.File.Exists(File))
            {
                var doc = new XmlDocument();
                doc.Load(file);
                this.LoadPropertiesCore(doc.DocumentElement);
                Favorite = (IFavorite)FavoriteAddonType.Instance.LoadAddon(doc.DocumentElement);
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public FavoriteHolder Clone()
        {
            var res = (FavoriteHolder)MemberwiseClone();
            res.Favorite = Favorite.Clone();
            return res;
        }

        public void Save()
        {
            string dir = Path.GetDirectoryName(File);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            var doc = XmlTool.CreateDocument("Favorite");
            this.SavePropertiesCore(doc.DocumentElement);
            Favorite.SaveToXml(doc.DocumentElement);
            doc.Save(File);
        }

        public void ChangeGroup(string group)
        {
            Group = group;
            File = Path.Combine(Path.Combine(Core.FavoritesDirectory, Group), Name + ".fav");
        }
    }

    //public class FavoriteGroup
    //{
    //    public List<FavoriteHolder> Items = new List<FavoriteHolder>();
    //    public string Name { get; private set; }
    //    public string Title { get; private set; }

    //    public FavoriteGroup(string name, string title)
    //    {
    //        Name = name;
    //        Title = title;
    //        Refresh();
    //    }

    //    public void Refresh()
    //    {
    //        foreach(
    //    }
    //}

    public class FavoriteGroup
    {
        public readonly string Name;
        public readonly string Title;

        public FavoriteGroup(string name, string title)
        {
            Name = name;
            Title = title;
        }

        public override string ToString()
        {
            return Texts.Get(Title);
        }

        public List<FavoriteHolder> GetItems()
        {
            return Favorites.LoadGroup(Name);
        }
    }

    public static class Favorites
    {
        public static List<FavoriteGroup> Groups = new List<FavoriteGroup>();

        static Favorites()
        {
            Groups.Add(new FavoriteGroup("toolbar", "s_toolbar"));
            Groups.Add(new FavoriteGroup("menu", "s_menu"));
        }

        public static List<FavoriteHolder> LoadGroup(string group)
        {
            var res = new List<FavoriteHolder>();
            string dir = Path.Combine(Core.FavoritesDirectory, group);
            if (!Directory.Exists(dir)) return res;
            foreach (string fn in Directory.GetFiles(dir))
            {
                try
                {
                    res.Add(new FavoriteHolder(fn, group));
                }
                catch
                {
                    continue;
                }
            }
            res.SortByKey(f => f.Position);
            return res;
        }

        public static void AddLast(FavoriteHolder fav)
        {
            var items = LoadGroup(fav.Group);
            if (items.Count > 0) fav.Position = items[items.Count - 1].Position + 1;
            fav.Save();
        }

        public static void AddFirst(FavoriteHolder fav)
        {
            var items = LoadGroup(fav.Group);
            if (items.Count > 0) fav.Position = items[0].Position - 1;
            fav.Save();
        }

        public static void Move(FavoriteHolder rel, FavoriteHolder moved, FavoriteHolder newCopy, int d)
        {
            var items = LoadGroup(rel.Group);

            int idx = items.IndexOfIf(h => String.Compare(h.Name, rel.Name, true) == 0);
            int movidx = items.IndexOfIf(h => String.Compare(h.Name, moved.Name, true) == 0);
            if (idx < 0)
            {
                items.RemoveAt(movidx);
                items.Insert(0, newCopy);
            }
            else if (idx + d >= items.Count)
            {
                items.RemoveAt(movidx);
                items.Add(newCopy);
            }
            else
            {
                items.RemoveAt(movidx);
                if (idx >= movidx) idx--;
                items.Insert(idx + d, newCopy);
            }

            SaveGroup(items);
        }

        private static void SaveGroup(List<FavoriteHolder> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Position = i;
                items[i].Save();
            }
        }

        public static FavoriteGroup GroupByName(string name)
        {
            return Groups.Find(g => g.Name == name);
        }

        public static void NotifyChanged()
        {
            HFavorites.CallChanged();
        }

        public static void OnFinish()
        {
        }

        public static void OnStart()
        {
        }
    }
}
