using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class FavoriteExtension
    {
        public static IFavorite Clone(this IFavorite fav)
        {
            var doc = XmlTool.CreateDocument("Favorite");
            fav.SaveToXml(doc.DocumentElement);
            return (IFavorite)FavoriteAddonType.Instance.LoadAddon(doc.DocumentElement);
        }
    }
}
