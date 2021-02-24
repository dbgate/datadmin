using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.versiondb
{
    [DbModelTransform(Name = "filter_by_name", Title = "s_filter_by_name")]
    public class FilterByNameTransform : DbModelTransformBase, ICustomPropertyPage
    {
        public FilterByNameTransform()
        {
            NameFilter = new ObjectNameFilter();
        }

        [XmlElem]
        public string ObjectType { get; set; }

        [XmlElem]
        public bool RemoveSelected { get; set; }

        [XmlSubElem]
        public ObjectNameFilter NameFilter { get; set; }

        private bool RemoveName(NameWithSchema name)
        {
            var dct = new Dictionary<string, string>();
            dct["schema"] = name.Schema;
            dct["name"] = name.Name;
            return NameFilter.Accept(dct) == RemoveSelected;
        }

        public override void RunTransform(DatabaseStructure model)
        {
            if (ObjectType == "table")
            {
                var remove = new List<NameWithSchema>();
                foreach (var tbl in model.Tables)
                {
                    if (RemoveName(tbl.FullName)) remove.Add(tbl.FullName);
                }
                foreach (var name in remove)
                {
                    model.Tables.RemoveIf(t => t.FullName == name);
                }
            }
            else
            {
                var remove = new List<NameWithSchema>();
                if (model.SpecificObjects.ContainsKey(ObjectType))
                {
                    foreach (var so in model.SpecificObjects[ObjectType])
                    {
                        if (RemoveName(so.ObjectName)) remove.Add(so.ObjectName);
                    }
                    foreach (var name in remove)
                    {
                        ((SpecificObjectCollection)model.SpecificObjects[ObjectType]).RemoveIf(s => s.ObjectName == name);
                    }
                }
            }
        }

        #region ICustomPropertyPage Members

        public System.Windows.Forms.Control CreatePropertyPage()
        {
            return new FilterByNameTransformFrame(this);
        }

        #endregion
    }

    public class ObjectNameFilter : ObjectFilterBase
    {
        public string NamePropertyName = "name", NamePropertyTitle = "s_name";

        [XmlSubElem]
        public StringObjectFilterItem SchemaFilter { get; set; }

        [XmlSubElem]
        public StringInListObjectFilterItem NameFilter { get; set; }

        public ObjectNameFilter()
        {
            SchemaFilter = new StringObjectFilterItem { PropertyName = "schema", PropertyTitle = "s_schema" };
            NameFilter = new StringInListObjectFilterItem { PropertyName = NamePropertyName, PropertyTitle = NamePropertyTitle };
        }

        public override void GetItems(List<ObjectFilterItemBase> items)
        {
            base.GetItems(items);
            items.Add(SchemaFilter);
            items.Add(NameFilter);
        }
    }
}
