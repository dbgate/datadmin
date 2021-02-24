using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class FilePlaceFileSystemTemplateFrame : UserControl
    {
        FilePlaceFileSystemTemplate m_place;
        static Dictionary<string, string> m_lastUsedFolders = new Dictionary<string, string>();
        static Dictionary<string, string> m_lastUsedTemplates = new Dictionary<string, string>();

        public FilePlaceFileSystemTemplateFrame(FilePlaceFileSystemTemplate place)
        {
            InitializeComponent();
            m_place = place;

            tbxFolder.Text = m_place.Folder;
            nameTemplateFrame1.NameTemplate = m_place.FileTemplate;

            Disposed += new EventHandler(FilePlaceFileSystemTemplateFrame_Disposed);

            MainWindow.Instance.RunInMainWindow(LoadLastValue);
        }

        private void LoadLastValue()
        {
            if (String.IsNullOrEmpty(tbxFolder.Text) && m_place != null && m_place.ContainerInfo != null && m_lastUsedFolders.ContainsKey(m_place.ContainerInfo.FileExtension))
            {
                tbxFolder.Text = m_lastUsedFolders[m_place.ContainerInfo.FileExtension];
            }
            if (String.IsNullOrEmpty(nameTemplateFrame1.NameTemplate) && m_place != null && m_place.ContainerInfo != null && m_lastUsedTemplates.ContainsKey(m_place.ContainerInfo.FileExtension))
            {
                nameTemplateFrame1.NameTemplate = m_lastUsedTemplates[m_place.ContainerInfo.FileExtension];
            }
        }

        void FilePlaceFileSystemTemplateFrame_Disposed(object sender, EventArgs e)
        {
            if (m_place != null && m_place.ContainerInfo != null && !String.IsNullOrEmpty(tbxFolder.Text))
            {
                m_lastUsedFolders[m_place.ContainerInfo.FileExtension] = tbxFolder.Text;
            }
            if (m_place != null && m_place.ContainerInfo != null && !String.IsNullOrEmpty(nameTemplateFrame1.NameTemplate))
            {
                m_lastUsedTemplates[m_place.ContainerInfo.FileExtension] = nameTemplateFrame1.NameTemplate;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = tbxFolder.Text;
            if (folderBrowserDialog1.ShowDialogEx() == DialogResult.OK)
            {
                tbxFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void tbxFolder_TextChanged(object sender, EventArgs e)
        {
            m_place.Folder = tbxFolder.Text;
        }

        private void nameTemplateFrame1_ChangedNameTemplate(object sender, EventArgs e)
        {
            m_place.FileTemplate = nameTemplateFrame1.NameTemplate;
        }
    }
}
