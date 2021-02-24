using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public class DataGridViewEx : DataGridView
    {
        public DataGridViewEx()
        {
            DoubleBuffered = true;
        }

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            base.OnColumnAdded(e);
            var cb = e.Column as DataGridViewComboBoxColumn;
            if (cb != null)
            {
                cb.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {
                return base.ProcessDialogKey(keyData);
            }
            catch (Exception err)
            {
                Logging.Warning(err.ToString());
                return true;
            }
        }

        protected override void OnEditingControlShowing(DataGridViewEditingControlShowingEventArgs e)
        {
            base.OnEditingControlShowing(e);
            var cb = e.Control as ComboBox;
            if (cb != null)
            {
                cb.Tag = CurrentCell;
                cb.DropDownStyle = ComboBoxStyle.DropDown;
                cb.Leave += new EventHandler(cb_Leave);
            }
        }

        void cb_Leave(object sender, EventArgs e)
        {
            var cb = sender as ComboBox;
            if (cb == null) return;
            for (int i = 0; i < cb.Items.Count; i++)
            {
                if (String.Compare(cb.Items[i].ToString(), cb.Text, true) == 0)
                {
                    cb.SelectedIndex = i;
                    CommitEdit(DataGridViewDataErrorContexts.Commit);
                    break;
                }
            }
        }
    }
}
