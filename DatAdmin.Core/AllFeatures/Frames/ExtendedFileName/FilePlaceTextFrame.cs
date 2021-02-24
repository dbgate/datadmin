using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class FilePlaceTextFrame : UserControl
    {
        FilePlaceText m_place;
        static string m_lastValue = "";

        public FilePlaceTextFrame(FilePlaceText place)
        {
            InitializeComponent();
            m_place = place;
            tbxMemo.Text = m_place.Text;
            Disposed += new EventHandler(FilePlaceTextFrame_Disposed);
            MainWindow.Instance.RunInMainWindow(LoadLastValue);
        }

        private void LoadLastValue()
        {
            if (String.IsNullOrEmpty(tbxMemo.Text)) tbxMemo.Text = m_lastValue;
        }

        void FilePlaceTextFrame_Disposed(object sender, EventArgs e)
        {
            if (!tbxMemo.Text.IsEmpty()) m_lastValue = tbxMemo.Text;
        }

        private void tbxMemo_TextChanged(object sender, EventArgs e)
        {
            m_place.Text = tbxMemo.Text;
        }
    }
}
