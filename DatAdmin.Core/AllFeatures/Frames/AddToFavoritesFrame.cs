using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DatAdmin
{
    public partial class AddToFavoritesFrame : UserControl
    {
        IFavorite m_favorite;

        public AddToFavoritesFrame()
        {
            InitializeComponent();

            foreach (var grp in Favorites.Groups)
            {
                cbxCategory.Items.Add(grp);
            }
            cbxCategory.SelectedIndex = 0;
        }

        //public void SaveToFavorite(IFavorite favorite)
        //{
        //    if (!tbxName.Text.IsEmpty()) favorite.Name = tbxName.Text;
        //    favorite.Group = ((FavoriteGroup)cbxCategory.SelectedItem).Name;
        //}

        //public void AddFavorite(IFavorite favorite)
        //{
        //    SaveToFavorite(favorite);
        //    Favorites.Instance.Add(favorite);
        //    Favorites.NotifyChanged();
        //}

        public string FavoriteName
        {
            get { return tbxName.Text; }
            set { tbxName.Text = value; }
        }

        public IFavorite Favorite
        {
            get { return m_favorite; }
            set { m_favorite = value; }
        }

        //public void LoadFrom(IFavorite favorite)
        //{
        //    tbxName.Text = favorite.Name;
        //    if (!String.IsNullOrEmpty(favorite.Group))
        //    {
        //        var grp = Favorites.Instance.Groups.Find(g => g.Name == favorite.Name);
        //        if (grp != null) cbxCategory.SelectedIndex = cbxCategory.Items.IndexOf(grp);
        //    }
        //}

        public FavoriteHolder GetHolder()
        {
            string group=((FavoriteGroup)cbxCategory.SelectedItem).Name;
            string fn = Path.Combine(Path.Combine(Core.FavoritesDirectory, group), FavoriteName + ".fav");
            var res = new FavoriteHolder(fn, group);
            res.Favorite = m_favorite;
            return res;
        }
    }
}
