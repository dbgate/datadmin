using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ObjectFilterAttribute : RegisterAttribute
    {
    }
    //[AttributeUsage(AttributeTargets.Class)]
    //public class ObjectFilterItemAttribute : RegisterAttribute
    //{
    //}


    public abstract class ObjectFilterBase : AddonBase, ICustomPropertyPage
    {
        //[XmlSubElem]
        //public TAddonCollection<ObjectFilterItemBase> Items { get; set; }

        public ObjectFilterBase()
        {
        }

        //public Control GetEditor()
        //{
        //    throw new NotImplementedException();
        //}

        public bool Accept(Dictionary<string,string> props)
        {
            var items = GetItems();
            foreach (var item in items)
            {
                if (!item.Enabled) continue;
                string val = props.Get(item.PropertyName);
                if (val != null && !item.Accept(val)) return false;
            }
            return true;
        }

        public virtual void GetItems(List<ObjectFilterItemBase> items) { }
        public List<ObjectFilterItemBase> GetItems()
        {
            var items = new List<ObjectFilterItemBase>();
            GetItems(items);
            return items;
        }

        public override AddonType AddonType
        {
            get { return ObjectFilterAddonType.Instance; }
        }

        #region ICustomPropertyPage Members

        public Control CreatePropertyPage()
        {
            return new ObjectFilterFrame(this);
        }

        #endregion
    }

    public abstract class ObjectFilterItemBase
    {
        public string PropertyName { get; set; }
        public string PropertyTitle { get; set; }
        [XmlElem]
        public bool Enabled { get; set; }
        public abstract Control CreateEditor();
        public abstract bool Accept(string value);

        //public override AddonType AddonType
        //{
        //    get { return ObjectFilterItemAddonType.Instance; }
        //}
    }

    [AddonType]
    public class ObjectFilterAddonType : AddonType
    {
        public override string Name
        {
            get { return "objectfilter"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(ObjectFilterBase); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(ObjectFilterAttribute); }
        }

        public static readonly ObjectFilterAddonType Instance = new ObjectFilterAddonType();
    }

    //[AddonType]
    //public class ObjectFilterItemAddonType : AddonType
    //{
    //    public override string Name
    //    {
    //        get { return "objectfilteritem"; }
    //    }

    //    public override Type InterfaceType
    //    {
    //        get { return typeof(ObjectFilterItemBase); }
    //    }

    //    public override Type RegisterAttributeType
    //    {
    //        get { return typeof(ObjectFilterItemAttribute); }
    //    }

    //    public static readonly ObjectFilterItemAddonType Instance = new ObjectFilterItemAddonType();
    //}
}
