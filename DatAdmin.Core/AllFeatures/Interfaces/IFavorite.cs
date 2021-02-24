using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DatAdmin
{
    public interface IFavorite : IAddonInstance
    {
        Bitmap Image { get; }
        void Open();
        void GetWidgets(List<IWidget> res);
        void DisplayProps(Action<string, string> display);
        string Description { get; }
    }

    public interface IFavoriteWithSql : IFavorite
    {
        string LoadSql();
        ISqlDialect GetDialect();
    }

    public class FavoriteAttribute : RegisterAttribute { }

    [AddonType]
    public class FavoriteAddonType : AddonType
    {
        public override string Name
        {
            get { return "favorite"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(IFavorite); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(FavoriteAttribute); }
        }

        public static readonly FavoriteAddonType Instance = new FavoriteAddonType();
    }
}
