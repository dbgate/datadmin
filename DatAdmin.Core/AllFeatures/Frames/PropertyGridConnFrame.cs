using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class PropertyGridConnFrame : ConnectionEditFrame
    {
        public PropertyGridConnFrame()
        {
            InitializeComponent();
        }

        public PropertyGridConnFrame(object conn)
        {
            InitializeComponent();
            SelectedObject = conn;
        }

        public override void SaveConnection()
        {
            var so = SelectedObject as IExplicitSaveableObject;
            if (so != null) so.ExplicitSave();
        }

        public object SelectedObject
        {
            get { return propertyFrame1.SelectedObject; }
            set { propertyFrame1.SelectedObject = value; }
        }
    }
}
