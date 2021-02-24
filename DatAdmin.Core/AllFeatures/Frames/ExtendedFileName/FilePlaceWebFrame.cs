using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class FilePlaceWebFrame : UserControl
    {
        FilePlaceWeb m_place;
        static string m_lastUrl;

        public FilePlaceWebFrame(FilePlaceWeb place)
        {
            InitializeComponent();
            m_place = place;
            tbxWebAddress.Text = m_place.Url;
            Disposed += new EventHandler(FilePlaceWebFrame_Disposed);
            MainWindow.Instance.RunInMainWindow(LoadLastValue);
        }

        private void LoadLastValue()
        {
            if (String.IsNullOrEmpty(tbxWebAddress.Text) && m_lastUrl != null) tbxWebAddress.Text = m_lastUrl;
        }

        void FilePlaceWebFrame_Disposed(object sender, EventArgs e)
        {
            m_lastUrl = tbxWebAddress.Text;
        }

        private void tbxWebAddress_TextChanged(object sender, EventArgs e)
        {
            m_place.Url = tbxWebAddress.Text;
        }
    }
}
