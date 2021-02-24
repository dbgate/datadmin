using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class FilePlaceSendEmailFrame : UserControl
    {
        FilePlaceSendEmail m_place;

        static string m_lastSubject = "";
        static string m_lastBody = "";
        static string m_lastTo = "";
        static string m_lastAttachment = "";

        public FilePlaceSendEmailFrame(FilePlaceSendEmail place)
        {
            InitializeComponent();

            m_place = place;

            emailToFrame1.EmailTo = m_place.To;
            tbxSubject.NameTemplate = m_place.Subject;
            tbxBody.Text = m_place.Body;
            tbxAttachment.Text = m_place.AttachmentName;

            Disposed += new EventHandler(FilePlaceSendEmailFrame_Disposed);
            MainWindow.Instance.RunInMainWindow(LoadLastValue);
        }

        private void LoadLastValue()
        {
            if (emailToFrame1.EmailTo.IsEmpty()) emailToFrame1.EmailTo = m_lastTo;
            if (tbxSubject.NameTemplate.IsEmpty()) tbxSubject.NameTemplate = m_lastSubject;
            if (tbxAttachment.Text.IsEmpty()) tbxAttachment.Text = m_lastAttachment;
            if (tbxBody.Text.IsEmpty()) tbxBody.Text = m_lastBody;
        }

        void FilePlaceSendEmailFrame_Disposed(object sender, EventArgs e)
        {
            if (!emailToFrame1.EmailTo.IsEmpty()) m_lastTo = emailToFrame1.EmailTo;
            if (!tbxSubject.NameTemplate.IsEmpty()) m_lastSubject = tbxSubject.NameTemplate;
            if (!tbxAttachment.Text.IsEmpty()) m_lastAttachment = tbxAttachment.Text;
            if (!tbxBody.Text.IsEmpty()) m_lastBody = tbxBody.Text;
        }

        private void tbxTo_TextChanged(object sender, EventArgs e)
        {
            m_place.To = emailToFrame1.EmailTo;
        }

        private void tbxBody_TextChanged(object sender, EventArgs e)
        {
            m_place.Body = tbxBody.Text;
        }

        private void tbxAttachment_TextChanged(object sender, EventArgs e)
        {
            m_place.AttachmentName = tbxAttachment.Text;
        }

        private void tbxSubject_NameTemplateChanged(object sender, EventArgs e)
        {
            m_place.Subject = tbxSubject.NameTemplate;
        }
    }
}
