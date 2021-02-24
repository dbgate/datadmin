using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;

namespace DatAdmin
{
    public partial class ConnWizardForm : FormEx
    {
        IStoredConnection m_conn;
        ConnectionEditFrame m_frame;
        CommonConnectionEditFrame m_common;
        TunnelConfigFrame m_tunCfg;
        int m_origWidth, m_origHeight;

        public ConnWizardForm(IStoredConnection conn)
        {
            InitializeComponent();
            m_conn = conn;

            int top = ClientSize.Height - (btnOk.Top + btnOk.Height);
            int left = ClientSize.Width - (btnOk.Left + btnOk.Width);
            int wi = ClientSize.Width - 2 * left;

            var tun = m_conn as ITunellableStoredConnection;

            var parent = Controls;
            TabControl pg = null;
            if (tun != null)
            {
                pg = new TabControl();
                var pg1 = new TabPage { Text = Texts.Get("s_connection") };
                var pg2 = new TabPage { Text = Texts.Get("s_tunnelling") };
                pg.TabPages.Add(pg1);
                pg.TabPages.Add(pg2);
                pg.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                Controls.Add(pg);

                pg.Top = top;
                pg.Width = wi;
                pg.Left = left;

                m_tunCfg = new TunnelConfigFrame();
                m_tunCfg.AllowDirectConnection = tun.AllowDirectConnection;
                m_tunCfg.SelectedDriver = tun.TunnelDriver;
                m_tunCfg.SelectedDriverChanged += new EventHandler(m_tunCfg_SelectedDriverChanged);
                pg2.Controls.Add(m_tunCfg);
                m_tunCfg.Dock = DockStyle.Fill;

                parent = pg1.Controls;
                top = 10;
                left = 10;
                wi = pg.Width - 20;

                pg.SelectedIndexChanged += pg_TabIndexChanged;
            }

            m_frame = m_conn.CreateEditor();
            parent.Add(m_frame);
            m_frame.Left = left;
            m_frame.Top = top;
            m_frame.Width = wi;

            m_common = new CommonConnectionEditFrame(conn);
            parent.Add(m_common);
            m_common.Left = left;
            m_common.Top = m_frame.Height + top;
            m_common.Width = wi;

            m_common.LinkToSpecific(m_frame);

            Height = top + Height + m_frame.Height + m_common.Height + (tun != null ? 50 : 0);
            if (pg != null) pg.Height = m_frame.Height + m_common.Height + 50;

            Text = Texts.Get(m_conn.ConnectionTypeTitle);
            Translating.TranslateControl(this);
            btnAdvanced.Enabled = conn.ConnectionSettings != null;
            btnCtxhelp.Enabled = m_conn.HelpTopic != null;
            Usage["contype"] = conn.GetType().FullName;

            m_origHeight = Height;
            m_origWidth = Width;
        }

        //public override string UsageEventName
        //{
        //    get { return "conn_wizard"; }
        //}

        void m_tunCfg_SelectedDriverChanged(object sender, EventArgs e)
        {
            ((ITunellableStoredConnection)m_conn).TunnelDriver = m_tunCfg.SelectedDriver;
        }

        void pg_TabIndexChanged(object sender, EventArgs e)
        {
            var pg = (TabControl)sender;
            if (pg.SelectedIndex == 0)
            {
                Width = m_origWidth;
                Height = m_origHeight;
            }
            else
            {
                Width = 450;
                Height = 500;
            }
        }

        private void btok_Click(object sender, EventArgs e)
        {
            m_frame.SaveConnection();
            m_common.SaveConnection();

            try
            {
                DbConnection conn = m_conn.CreateSystemConnection();
                try
                {
                    Async.InvokeFromGui(conn.Open);
                    Async.InvokeFromGui(conn.Close);
                }
                catch (Exception err)
                {
                    throw new ConnectionFailedError("DAE-00162", err);
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception err)
            {
                Errors.Report(err);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btcancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        public static bool Edit(IStoredConnection conn)
        {
            ConnWizardForm win = new ConnWizardForm(conn);
            if (win.ShowDialogEx() == DialogResult.OK)
            {
                return true;
            }
            return false;
        }

        private void btadvanced_Click(object sender, EventArgs e)
        {
            SettingsForm.Run(m_conn.ConnectionSettings, SettingsTargets.Connection);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Core.ShowHelp(m_conn.HelpTopic);
        }
    }

    public abstract class ConnectionCreateWizard : CreateFactoryItemBase
    {
        string m_name;
        string m_title;
        string m_infoText;

        public ConnectionCreateWizard(string name, string title, string infoText)
        {
            m_name = name;
            m_title = title;
            m_infoText = infoText;
        }

        public override string InfoText
        {
            get { return m_infoText; }
        }

        public override string Title
        {
            get { return m_title; }
        }

        public override string Name
        {
            get { return m_name; }
        }

        public override string Group
        {
            get { return "s_connections"; }
        }

        public override string GroupName
        {
            get { return "connections"; }
        }

        public override System.Drawing.Bitmap Bitmap
        {
            get { return CoreIcons.img_database; }
        }

        protected virtual Form CreateWizard(IStoredConnection conn)
        {
            return new ConnWizardForm(conn);
        }

        public override bool Create(ITreeNode parent, string name)
        {
            IStoredConnection conn = CreateStoredConnection();
            conn.FileName = System.IO.Path.Combine(parent.FileSystemPath, name + ".con");
            Form wiz = CreateWizard(conn);
            wiz.ShowDialogEx();
            if (wiz.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                conn.Save();
                return true;
            }
            return false;
        }

        public abstract IStoredConnection CreateStoredConnection();
    }

    public abstract class GenericConnectionCreateWizard : ConnectionCreateWizard
    {
        public GenericConnectionCreateWizard(string name, string title, string infoText)
            : base(name, title, infoText)
        {
        }

        public override string Group
        {
            get { return "s_generic_connections"; }
        }

        public override string GroupName
        {
            get { return "generic_connections"; }
        }
    }

}
