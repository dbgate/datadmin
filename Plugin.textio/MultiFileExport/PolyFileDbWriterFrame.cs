using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.textio
{
    public partial class PolyFileDbWriterFrame : UserControl
    {
        PolyFileDbWriter m_writer;
        bool m_created;
        bool m_loading;

        public PolyFileDbWriterFrame(PolyFileDbWriter writer)
        {
            InitializeComponent();
            m_writer = writer;

            cbxFileType.Items.Add(new FileWriterComboItem { Frame = new CodeWriterFrame(this, TextGeneratorLanguage.Template) });
            cbxFileType.Items.Add(new FileWriterComboItem { Frame = new CodeWriterFrame(this, TextGeneratorLanguage.Python) });
            cbxFileType.Items.Add(new FileWriterComboItem { Frame = new BlobWriterFrame(this) });
            cbxFileType.Items.Add(new FileWriterComboItem { Frame = new DataStoreWriterFrame(this) });
            m_created = true;

            foreach (var file in writer.Files) lbxFiles.Items.Add(file);

            UpdateEnabling();
            Translating.TranslateControl(this);
        }

        private void UpdateEnabling()
        {
            cbxFileType.Enabled = tbxQuery.Enabled = tbxFileName.Enabled = CurFile != null;
        }

        public WriterFileInfo CurFile
        {
            get
            {
                if (lbxFiles.SelectedIndex >= 0) return (WriterFileInfo)lbxFiles.Items[lbxFiles.SelectedIndex];
                return null;
            }
        }

        public FileWriterFrame FileTypeFrame
        {
            get
            {
                if (CurFile == null) return null;
                if (cbxFileType.SelectedIndex < 0) return null;
                var item = (FileWriterComboItem)cbxFileType.Items[cbxFileType.SelectedIndex];
                return item.Frame;
            }
        }

        private void cbxFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_created) return;
            if (CurFile == null) return;
            if (!m_loading) CurFile.Content = ((FileWriterComboItem)cbxFileType.SelectedItem).Frame.CreateContent();
            if (!panel2.Controls.Contains(FileTypeFrame))
            {
                panel2.Controls.Add(FileTypeFrame);
                FileTypeFrame.Dock = DockStyle.Fill;
            }
            foreach (Control c in panel2.Controls) c.Hide();
            FileTypeFrame.Show();
        }

        private void tbxFileName_TextChanged(object sender, EventArgs e)
        {
            if (m_loading) return;
            Save();
            lbxFiles.ReloadItemNames();
        }

        public void Save()
        {
            if (m_loading) return;
            m_writer.Files.Clear();
            foreach (WriterFileInfo file in lbxFiles.Items) m_writer.Files.Add(file);
            if (CurFile != null)
            {
                CurFile.Sql = tbxQuery.Text;
                CurFile.FileNameTemplate = tbxFileName.Text;
            }
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            lbxFiles.Items.Add(new WriterFileInfo());
            lbxFiles.SelectedIndex = lbxFiles.Items.Count - 1;
            Save();
            UpdateEnabling();
        }

        private void btnRemoveFile_Click(object sender, EventArgs e)
        {
            lbxFiles.RemoveSelected();
            Save();
            UpdateEnabling();
        }

        private void lbxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurFile != null)
            {
                m_loading = true;
                tbxFileName.Text = CurFile.FileNameTemplate;
                tbxQuery.Text = CurFile.Sql;
                if (CurFile.Content is TemplateFileContent) cbxFileType.SelectedIndex = 0;
                else if (CurFile.Content is PythonFileContent) cbxFileType.SelectedIndex = 1;
                else if (CurFile.Content is BlobFileContent) cbxFileType.SelectedIndex = 2;
                else if (CurFile.Content is DataStoreFileContent) cbxFileType.SelectedIndex = 3;
                if (FileTypeFrame != null) FileTypeFrame.Content = CurFile.Content;
                m_loading = false;
            }
            else
            {
                cbxFileType.SelectedIndex = -1;
            }
            UpdateEnabling();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Core.ShowHelp("multiple-files-export");
        }
    }

    public class FileWriterComboItem
    {
        public FileWriterFrame Frame;

        public override string ToString()
        {
            return Texts.Get(Frame.ComboTitle());
        }
    }
}
