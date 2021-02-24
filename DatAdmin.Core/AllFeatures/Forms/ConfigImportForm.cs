using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DatAdmin
{
    public partial class ConfigImportForm : FormEx
    {
        IVirtualFileSystem m_srcFs;
        IVirtualFileSystem m_dstFs;
        List<ConfigFileNode> m_conflicts = new List<ConfigFileNode>();

        public ConfigImportForm(IVirtualFileSystem fs)
        {
            InitializeComponent();
            m_srcFs = fs;
            m_dstFs = AppDataDiskFileSystem.Instance;
            configSelectionFrame1.FileSystem = fs;
            DetectConflicts(configSelectionFrame1.Root);
            RenameAll();
            configSelectionFrame1.CheckAll();
        }

        private void DetectConflicts(ConfigNode parent)
        {
            foreach (var node in parent.Children)
            {
                if (node.VirtualPath is IVirtualFolder)
                {
                    DetectConflicts(node);
                }
                else
                {
                    if (m_dstFs.GetPath(node.VirtualPath).Exists())
                    {
                        int idx = dataGridViewEx1.Rows.Add(VirtualFileExtension.NormalizePath(node.VirtualPath.FullPath), Path.GetFileNameWithoutExtension(node.VirtualPath.Name), node.Selected);
                        dataGridViewEx1.Rows[idx].Tag = node;
                        node.Tag = dataGridViewEx1.Rows[idx];
                        node.SelectedChanged += new EventHandler(node_SelectedChanged);
                        m_conflicts.Add((ConfigFileNode)node);
                    }
                }
            }
        }

        void node_SelectedChanged(object sender, EventArgs e)
        {
            var node = (ConfigNode)sender;
            ((DataGridViewRow)node.Tag).Cells[2].Value = node.Selected;
        }

        public static bool Run(IVirtualFileSystem fs)
        {
            using (var win = new ConfigImportForm(fs))
            {
                return win.ShowDialogEx() == DialogResult.OK;
            }
        }

        public static bool Run(string file)
        {
            using (var zip = new ZipFileSystem(file))
            {
                return Run(zip);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var repls = new List<Tuple<string, string>>();
            configSelectionFrame1.Root.GetReplacePaths(repls);
            configSelectionFrame1.Root.CopyCheckedTo(m_dstFs, true, repls);
            Close();

            // refresh all what we can refresh...
            HTree.CallRefreshRoot();
            HFavorites.CallChanged();

            StdDialog.ShowInfo("s_config_imported");

            //if (MessageBox.Show("s_conf_imported_restart_recomended_restart_now", "DatAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            //{
            //    MainWindow.Instance.CloseMainWindow();
            //    Core.ExecuteAfterFinalize = Core.FullExeName;
            //}
        }

        private void dataGridViewEx1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dataGridViewEx1.Rows[e.RowIndex];
            var node = (ConfigNode)row.Tag;
            bool newsel = (bool)row.Cells[2].Value;
            if (newsel != node.Selected) node.Select(newsel);
            if (row.Cells[1].Value.ToString() != Path.GetFileNameWithoutExtension(node.VirtualPath.FullPath))
            {
                ((ConfigFileNode)node).NewName = row.Cells[1].Value.ToString();
            }
            else
            {
                ((ConfigFileNode)node).NewName = null;
            }
        }

        private void btnSkipAll_Click(object sender, EventArgs e)
        {
            foreach (var node in m_conflicts)
            {
                node.Select(false);
            }
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            foreach (var node in m_conflicts)
            {
                node.Select(true);
                node.NewName = null;
                var row = (DataGridViewRow)node.Tag;
                row.Cells[1].Value = Path.GetFileNameWithoutExtension(node.VirtualPath.FullPath);
            }
        }

        private void RenameAll()
        {
            foreach (var node in m_conflicts)
            {
                var row = (DataGridViewRow)node.Tag;
                string newname;
                for (int i = 2; ; i++)
                {
                    string noext = Path.ChangeExtension(node.VirtualPath.FullPath, null);
                    newname = Path.GetFileNameWithoutExtension(node.VirtualPath.FullPath) + "_" + i.ToString();
                    string newpath = noext + "_" + i.ToString() + node.VirtualPath.FullPath.Substring(noext.Length);
                    if (!m_dstFs.GetFile(newpath).Exists()) break;
                }
                row.Cells[1].Value = newname;
                node.NewName = newname;
            }
        }

        private void btnRenameAll_Click(object sender, EventArgs e)
        {
            RenameAll();
        }
    }

    [FileHandler(Name = "dca")]
    public class ConfigArchiveHandler : FileHandlerBase
    {
        public override string Extension
        {
            get { return "dca"; }
        }

        public override string Description
        {
            get { return Texts.Get("s_datadmin_configuration_archive"); }
        }

        public override void OpenAction()
        {
            ConfigImportForm.Run(m_file.DataDiskPath);
        }

        public override FileHandlerCaps Caps
        {
            get
            {
                return new FileHandlerCaps
                {
                    AllFlags = false,
                    OpenAction = true
                };
            }
        }
    }
}
