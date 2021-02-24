using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DatAdmin
{
    public abstract class FavoriteBase : AddonBase, IFavorite
    {
        protected FavoriteBase()
        {
        }

        public override AddonType AddonType
        {
            get { return FavoriteAddonType.Instance; }
        }

        #region IFavorite Members

        public abstract System.Drawing.Bitmap Image { get; }

        public abstract void Open();

        public abstract string Description { get; }

        #endregion

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            xml.SetAttribute("type", XmlTool.GetRegisterType(this));
        }

        public virtual void GetWidgets(List<IWidget> res)
        {
            res.Add(new FavoriteInfoWidget());
        }

        public virtual void DisplayProps(Action<string, string> display)
        {
            display("s_description", Description);
        }
    }
}
