using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class FilePlaceFileSystemFrame : UserControl
    {
        FilePlaceFileSystem m_place;
        static Dictionary<string, string> m_lastUsedFileNames = new Dictionary<string, string>();

        public FilePlaceFileSystemFrame(FilePlaceFileSystem place)
        {
            InitializeComponent();
            m_place = place;
            tbxFileName.Text = place.FilePath;
            Disposed += new EventHandler(FilePlaceFileSystemFrame_Disposed);
            MainWindow.Instance.RunInMainWindow(LoadLastValue);
        }

        private void LoadLastValue()
        {
            if (String.IsNullOrEmpty(tbxFileName.Text) && m_place != null && m_place.ContainerInfo != null && m_lastUsedFileNames.ContainsKey(m_place.ContainerInfo.FileExtension))
            {
                tbxFileName.Text = m_lastUsedFileNames[m_place.ContainerInfo.FileExtension];
            }
        }

        void FilePlaceFileSystemFrame_Disposed(object sender, EventArgs e)
        {
            if (m_place != null && m_place.ContainerInfo != null && !String.IsNullOrEmpty(tbxFileName.Text))
            {
                m_lastUsedFileNames[m_place.ContainerInfo.FileExtension] = tbxFileName.Text;
            }
        }

        private void tbxFileName_TextChanged(object sender, EventArgs e)
        {
            m_place.FilePath = tbxFileName.Text;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FileDialog dlg = null;
            if (m_place.ContainerInfo.DirectionIsSave) dlg = saveFileDialog1;
            else dlg = openFileDialog1;
            dlg.Filter = m_place.ContainerInfo.Filter;
            dlg.FileName = tbxFileName.Text;
            if (dlg.ShowDialogEx() == DialogResult.OK)
            {
                tbxFileName.Text = dlg.FileName;
            }
        }
    }
}
