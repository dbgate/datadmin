using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class TreeSelectForm : FormEx
    {
        public TreeSelectForm()
        {
            InitializeComponent();

        }

        event EventHandler<CheckSelectedOkEventArgs> SelectedOk;

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (SelectedOk != null)
            {
                var ev = new CheckSelectedOkEventArgs();
                SelectedOk(this, ev);
                if (!ev.SelectedOk) return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        public static IDatabaseSource SelectDatabase()
        {
            TreeSelectForm win = new TreeSelectForm();
            win.daTreeView1.TreeBehaviour.ShowFilter = node => node.IsDatabaseNodeOrParent();
            win.daTreeView1.TreeBehaviour.AllowDoubleClickNodeHandling = false;
            win.daTreeView1.RootPath = "data:";
            win.SelectedOk += new EventHandler<CheckSelectedOkEventArgs>(win_SelectedOk);
            if (win.ShowDialogEx() == DialogResult.OK)
            {
                var node = win.daTreeView1.Selected as IDatabaseTreeNode;
                if (node != null) return node.DatabaseConnection;
            }
            return null;
        }

        static void win_SelectedOk(object sender, CheckSelectedOkEventArgs e)
        {
            var win = sender as TreeSelectForm;
            var node = win.daTreeView1.Selected as IDatabaseTreeNode;
            if (node == null || node.DatabaseConnection == null)
            {
                StdDialog.ShowError("s_please_select_database");
                return;
            }
        }

        public static IPhysicalConnection SelectQueryableConnection()
        {
            TreeSelectForm win = new TreeSelectForm();
            win.daTreeView1.TreeBehaviour.ShowFilter = node => node.IsQueryableNodeOrParent();
            win.daTreeView1.RootPath = "data:";
            if (win.ShowDialogEx() == DialogResult.OK)
            {
                return win.daTreeView1.Selected.GetConnection();
            }
            return null;
        }

        private void daTreeView1_TreeDoubleClick(object sender, MouseEventArgs e)
        {
            btnOk_Click(sender, e);
        }
    }

    public class CheckSelectedOkEventArgs : EventArgs
    {
        public bool SelectedOk = true;
    }
}
