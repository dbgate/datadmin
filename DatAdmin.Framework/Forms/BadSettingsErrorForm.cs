using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class BadSettingsErrorForm : FrameworkFormEx
    {
        string m_cfgpath;
        public BadSettingsErrorForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        public static void Run(BadSettingsError error)
        {
            Run(error.Message, error.ConfigPath);
        }

        public static void Run(string message, string cfgpath)
        {
            BadSettingsErrorForm win = new BadSettingsErrorForm();
            win.tbxError.Text = message;
            win.m_cfgpath = cfgpath;
            win.ShowDialogEx();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
            Framework.Instance.ShowOptions(m_cfgpath);
        }
    }
}
