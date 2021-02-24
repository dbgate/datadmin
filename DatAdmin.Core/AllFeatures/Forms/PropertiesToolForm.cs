using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class PropertiesToolForm : FormEx
    {
        public PropertiesToolForm()
        {
            InitializeComponent();
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (OnChanged != null) OnChanged(this, EventArgs.Empty);
        }

        public event EventHandler OnChanged;

        public object SelectedObject
        {
            get { return ctlProperties.SelectedObject; }
            set { ctlProperties.SelectedObject = value; }
        }
    }
}
