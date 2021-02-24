using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DatAdmin
{
    public partial class AddToFavoriteForm : FormEx
    {
        public AddToFavoriteForm()
        {
            InitializeComponent();
        }

        public static bool RunLoop(Form win, AddToFavoritesFrame frame, Action updateFavorite)
        {
            for (; ; )
            {
                if (win.ShowDialogEx() == DialogResult.OK)
                {
                    if (updateFavorite != null) updateFavorite();
                    var fh = frame.GetHolder();
                    if (File.Exists(fh.File))
                    {
                        var res = MessageBox.Show(Texts.Get("s_favorite_allready_exists_overwrite"), VersionInfo.ProgramTitle, MessageBoxButtons.YesNoCancel);
                        if (res == DialogResult.Yes)
                        {
                            File.Delete(fh.File);
                        }
                        else if (res == DialogResult.No)
                        {
                            return false;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    Favorites.AddLast(fh);
                    Favorites.NotifyChanged();
                    return true;
                }
                return false;
            }
        }

        public static bool Run(IFavorite favorite, string defname)
        {
            var win = new AddToFavoriteForm();
            win.addToFavoritesFrame1.FavoriteName = defname;
            win.addToFavoritesFrame1.Favorite = favorite;
            return RunLoop(win, win.addToFavoritesFrame1, null);
        }
    }
}
