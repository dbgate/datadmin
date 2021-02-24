using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class FilePlaceCommandLineFrame : UserControl
    {
        FilePlaceCommandLine m_place;
        static string m_lastApplication, m_lastArguments;

        public FilePlaceCommandLineFrame(FilePlaceCommandLine place)
        {
            InitializeComponent();
            m_place = place;
            tbxApplication.Text = place.Application;
            tbxArguments.Text = place.Arguments;
            Disposed += new EventHandler(FilePlaceCommandLineFrame_Disposed);
            MainWindow.Instance.RunInMainWindow(LoadLastValue);
        }

        private void LoadLastValue()
        {
            if (String.IsNullOrEmpty(tbxApplication.Text) && m_lastApplication != null)
            {
                tbxApplication.Text = m_lastApplication;
            }
            if (String.IsNullOrEmpty(tbxArguments.Text) && m_lastArguments != null)
            {
                tbxArguments.Text = m_lastArguments;
            }
        }

        void FilePlaceCommandLineFrame_Disposed(object sender, EventArgs e)
        {
            m_lastApplication = tbxApplication.Text;
            m_lastArguments = tbxArguments.Text;
        }

        private void btnBrowseApp_Click(object sender, EventArgs e)
        {
            openFileDialogExe.Filter = Texts.Replace("{s_applications (*.exe)}|*.exe");
            openFileDialogExe.FileName = tbxApplication.Text;
            if (openFileDialogExe.ShowDialogEx() == DialogResult.OK)
            {
                tbxApplication.Text = openFileDialogExe.FileName;
            }
        }
    }
}
