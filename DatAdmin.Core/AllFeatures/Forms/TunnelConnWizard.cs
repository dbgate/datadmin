using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DatAdmin
{
    public partial class TunnelConnWizard : FormEx
    {
        GenericTunnelStoredConnection m_conn;
        ITunnelDriver m_driver;

        public TunnelConnWizard(GenericTunnelStoredConnection conn)
        {
            InitializeComponent();
            m_conn = conn;
            tunnelCommonFrame1.Connection = m_conn;
        }

        public IStoredConnection Connection
        {
            get { return m_conn; }
        }

        public IStoredConnection GetConnection()
        {
            Save();
            return m_conn;
        }

        private void Save()
        {
            tunnelCommonFrame1.SaveConnection();
        }

        private void wpgHttp_CloseFromNext(object sender, Gui.Wizard.PageEventArgs e)
        {
            try
            {
                var conn = (TunnelConnection)GetConnection().CreateSystemConnection();
                using (var wc = new WaitContext())
                {
                    conn.PingTunnel();
                }
            }
            catch (Exception err)
            {
                StdDialog.ShowError(err.Message);
                e.Page = wpgTunnel;
            }
        }

        private void pgdbconn_CloseFromNext(object sender, Gui.Wizard.PageEventArgs e)
        {
            try
            {
                var conn = (TunnelConnection)GetConnection().CreateSystemConnection();
                using (var wc = new WaitContext())
                {
                    conn.PingDb();
                }
            }
            catch (Exception err)
            {
                StdDialog.ShowError(err.Message);
                e.Page = pgdbconn;
            }
        }

        private void databaseDropDown1_CurrentConnectionNeeded(object sender, EventArgs e)
        {
            Save();
        }

        private void wizWizard_OnTranslate(object sender, Gui.Wizard.TranslateTextEventArgs e)
        {
            e.TranslatedText = Texts.Get(e.OriginalText);
        }

        private void addonSelectFrame1_ChangedSelectedObject(object sender, EventArgs e)
        {
            m_conn.TunnelDriver = addonSelectFrame1.SelectedObject as ITunnelDriver;
        }
    }
}