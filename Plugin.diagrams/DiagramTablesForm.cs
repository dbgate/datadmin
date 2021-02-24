using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.diagrams
{
    public partial class DiagramTablesForm : FormEx
    {
        DiagramEditFrame m_frame;
        public DiagramTablesForm(DiagramEditFrame frame)
        {
            InitializeComponent();
            m_frame = frame;
            RefreshTables();
        }

        private void RefreshTables()
        {
            int lastUsed = lbxUsed.SelectedIndex;
            lbxUsed.Items.Clear();
            foreach (NameWithSchema table in m_frame.TableNames)
            {
                lbxUsed.Items.Add(table);
            }
            int lastAvailable = lbxAvailable.SelectedIndex;
            lbxAvailable.Items.Clear();
            foreach (NameWithSchema table in m_frame.Database.InvokeLoadFullTableNames(true).Sorted())
            {
                if (lbxUsed.Items.IndexOf(table) >= 0) continue;
                lbxAvailable.Items.Add(table);
            }
            if (lastUsed < lbxUsed.Items.Count) lbxUsed.SelectedIndex = lastUsed;
            if (lastAvailable < lbxAvailable.Items.Count) lbxAvailable.SelectedIndex = lastAvailable;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach(NameWithSchema table in lbxUsed.SelectedItems)
            {
                m_frame.RemoveTable(table);
            }
            RefreshTables();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            foreach (NameWithSchema table in lbxAvailable.SelectedItems)
            {
                m_frame.AddTable(table);
            }
            RefreshTables();
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Texts.Get("s_really_remove_all_tables"), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (NameWithSchema table in lbxUsed.Items)
                {
                    m_frame.RemoveTable(table);
                }
                RefreshTables();
            }
        }

        private void btnAddAll_Click(object sender, EventArgs e)
        {
            using (WaitContext wc = new WaitContext())
            {
                foreach (NameWithSchema table in lbxAvailable.Items)
                {
                    m_frame.AddTable(table);
                }
                RefreshTables();
            }
        }
    }
}
