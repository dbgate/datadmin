using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.versiondb
{
    [DbModelTransform(Name = "filter_by_type", Title = "s_filter_by_type")]
    public class FilterByTypeTransform : DbModelTransformBase, ICustomPropertyPage
    {
        [XmlElem]
        public bool RemoveSelected { get; set; }

        [XmlCollection(typeof(string))]
        public List<string> SelectedItems { get; set; }

        public FilterByTypeTransform()
        {
            SelectedItems = new List<string>();
        }

        #region ICustomPropertyPage Members

        public System.Windows.Forms.Control CreatePropertyPage()
        {
            return new FilterByTypeTransformFrame(this);
        }

        #endregion

        public override void RunTransform(DatabaseStructure model)
        {
            ProcessItem("table", model.Tables.Clear);
            foreach (string type in model.SpecificObjects.Keys)
            {
                ProcessItem(type, ((SpecificObjectCollection)model.SpecificObjects[type]).Clear);
            }
        }

        private void ProcessItem(string item, Action action)
        {
            if (RemoveSelected == SelectedItems.Contains(item)) action();
        }
    }
}
