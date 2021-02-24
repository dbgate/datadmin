using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DatAdmin
{
    public partial class JobCommandEditorFrame : ContentFrame
    {
        JobConnection m_jobconn;
        JobCommand m_command;
        bool m_modified;

        public JobCommandEditorFrame(JobConnection jobconn, JobCommand command)
        {
            InitializeComponent();
            m_jobconn = jobconn;
            m_command = command;
            propertyFrame1.SelectedObject = m_command;
        }

        bool IsModified()
        {
            return m_modified;
        }

        public override bool AllowClose()
        {
            if (IsModified())
            {
                DialogResult dr = MessageBox.Show(Texts.Get("s_file_modified_save$file", "file", m_jobconn.m_file), VersionInfo.ProgramTitle, MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Yes)
                {
                    return Save();
                }
                if (dr == DialogResult.No) return true;
                return false;
            }
            return true;
        }
        public override bool SupportsSave { get { return true; } }
        public override string PageTitle
        {
            get
            {
                return Path.GetFileNameWithoutExtension(m_jobconn.m_file);
            }
        }

        public override bool Save()
        {
            m_jobconn.SaveCommand(m_command);
            return true;
        }

        public override Bitmap Image
        {
            get
            {
                return CoreIcons.job;
            }
        }
    }
}
