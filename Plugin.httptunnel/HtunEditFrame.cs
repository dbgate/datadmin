using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;
using System.IO;

namespace Plugin.httptunnel
{
    public partial class HtunEditFrame : UserControl
    {
        HtunDriver m_pars;
        bool m_loaded;

        public HtunEditFrame(HtunDriver pars)
        {
            m_pars = pars;
            InitializeComponent();

            cbxEncodingStyle.Items.Add(Texts.Get("s_default"));
            cbxEncodingStyle.Items.Add(Texts.Get("s_database"));
            cbxEncodingStyle.Items.Add(Texts.Get("s_explicit"));

            foreach (var i in EncodingTypeConverter.EncodingItems)
            {
                cbxEncoding.Items.Add(i);
            }

            cbxEncoding.SelectedIndex = EncodingTypeConverter.GetEncodingIndex(m_pars.RealEncoding);

            tbxUrl.Text = m_pars.Url;
            tbxHttpLogin.Text = m_pars.HttpLogin;
            tbxHttpPassword.Text = m_pars.HttpPassword;
            cbxEncodingStyle.SelectedIndex = (int)m_pars.EncodingStyle;
            m_loaded = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(Core.FilesDirectory);
        }

        private void tbxUrl_TextChanged(object sender, EventArgs e)
        {
            if (!m_loaded) return;
            m_pars.Url = tbxUrl.Text;
        }

        private void tbxHttpLogin_TextChanged(object sender, EventArgs e)
        {
            if (!m_loaded) return;
            m_pars.HttpLogin = tbxHttpLogin.Text;
        }

        private void tbxHttpPassword_TextChanged(object sender, EventArgs e)
        {
            if (!m_loaded) return;
            m_pars.HttpPassword = tbxHttpPassword.Text;
        }

        private void EncodingDataChanged(object sender, EventArgs e)
        {
            if (!m_loaded) return;
            m_pars.EncodingStyle = (EncodingStyle)cbxEncodingStyle.SelectedIndex;
            if (cbxEncoding.SelectedIndex >= 0) m_pars.RealEncoding = Encoding.GetEncoding(((EncodingItem)cbxEncoding.Items[cbxEncoding.SelectedIndex]).WebName);
        }
    }
}
