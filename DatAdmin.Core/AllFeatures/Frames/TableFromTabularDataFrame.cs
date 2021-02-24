using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class TableFromTabularDataFrame : UserControl
    {
        public event EventHandler ChangedProperties;

        public TableFromTabularDataFrame()
        {
            InitializeComponent();
            SetDatabase(null);
        }

        public NameWithSchema FullName
        {
            get
            {
                return new NameWithSchema(cbxSchema.Text, tbxName.Text);
            }
        }

        private void AnyChanged(object sender, EventArgs e)
        {
            if (ChangedProperties != null) ChangedProperties(this, EventArgs.Empty);
        }

        public void SetDatabase(IDatabaseSource db)
        {
            cbxSchema.Items.Clear();
            if (db == null)
            {
                cbxSchema.Enabled = false;
            }
            else
            {
                cbxSchema.Enabled = db.DatabaseCaps.MultipleSchema;
                if (db.Connection.IsOpened)
                {
                    foreach (string item in db.InvokeLoadSchemata())
                    {
                        cbxSchema.Items.Add(item);
                    }
                }
            }
        }
    }
}
