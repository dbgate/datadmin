using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class NewTableChooserFrame : UserControl
    {
        public event EventHandler ChangedProperties;

        public NewTableChooserFrame()
        {
            InitializeComponent();
            daTreeView1.TreeBehaviour.ShowFilter = node => node.IsDatabaseNodeOrParent();
            daTreeView1.RootPath = "data:";
        }

        private void daTreeView1_ActiveNodeChange(object sender, EventArgs e)
        {
            if (ChangedProperties != null)
            {
                ChangedProperties(this, e);
                tableFromTabularDataFrame1.SetDatabase(Database);
            }
        }

        public IDatabaseSource Database
        {
            get
            {
                var dbnode = daTreeView1.Selected as IDatabaseTreeNode;
                if (dbnode != null) return dbnode.DatabaseConnection;
                return null;
            }
        }

        private void tableFromTabularDataFrame1_ChangedProperties(object sender, EventArgs e)
        {
            if (ChangedProperties != null) ChangedProperties(this, e);
        }

        public NameWithSchema FullName { get { return tableFromTabularDataFrame1.FullName; } }
    }
}
