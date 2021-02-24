using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using DatAdmin;
using System.IO;
using System.Reflection;

namespace Plugin.genericsql
{
    public partial class GenericConnWizard : Form
    {
        DbProviderFactory m_factory;
        DataTable m_factoryClasses;
        DbConnectionStringBuilder m_builder;
        string m_factoryName;
        string m_driverName;
        GenericSqlStoredConnection m_conn;

        public GenericConnWizard()
        {
            InitializeComponent();
            Translating.TranslateControl(this);
            //wizard1.Pages.Remove(wpselectdb);
        }

        private void wpprovider_CloseFromNext(object sender, Gui.Wizard.PageEventArgs e)
        {
            try
            {
                if (cbxProvider.SelectedIndex >= 0)
                {
                    m_factoryName = cbxProvider.SelectedItem.ToString();
                    m_driverName = null;
                }
                if (cbxDriver.SelectedIndex >= 0)
                {
                    m_driverName = ((DbDriverDefinition)cbxDriver.SelectedItem).InvariantName;
                    m_factoryName = null;
                }

                if (m_factoryName != null)
                {
                    m_factory = DbProviderFactories.GetFactory(m_factoryName);

                }
                if (m_driverName != null)
                {
                    m_factory = DbDriverManager.Instance.CreateFactory(m_driverName);
                }
            }
            catch (Exception err)
            {
                StdDialog.ShowError(String.Format(
                    "{0}:\n{1}", Texts.Get("s_cannot_create_provider"), err.Message));
                e.Page = wpprovider;
                return;
            }
        }

        private void wpconnprops_ShowFromNext(object sender, EventArgs e)
        {
            try
            {
                m_builder = m_factory.CreateConnectionStringBuilder();
                ctlProperties.SelectedObject = m_builder;
            }
            catch (Exception err)
            {
                Errors.Report(err);
            }
        }

        private void wizard1_Load(object sender, EventArgs e)
        {
            m_factoryClasses = DbProviderFactories.GetFactoryClasses();
            foreach (DataRow row in m_factoryClasses.Rows)
            {
                cbxProvider.Items.Add(row["InvariantName"]);
            }
            foreach (DbDriverDefinition driver in DbDriverManager.Instance.Drivers)
            {
                cbxDriver.Items.Add(driver);
            }
        }

        private void wpconnprops_CloseFromNext(object sender, Gui.Wizard.PageEventArgs e)
        {
            string conns = m_builder.ConnectionString;
            DbConnection conn = m_factory.CreateConnection();
            conn.ConnectionString = conns;
            Action cb = conn.Open;
            IAsyncResult res = cb.BeginInvoke(null, null);
            try
            {
                Async.WaitFor(res);
                cb.EndInvoke(res);
            }
            catch (Exception err)
            {
                Errors.Report(err);
                e.Page = wpconnprops;
                return;
            }
        }

        private void wpfinish_CloseFromNext(object sender, Gui.Wizard.PageEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void lbprovider_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxProvider.SelectedIndex >= 0) cbxDriver.SelectedIndex = -1;
        }

        private void lbdriver_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDriver.SelectedIndex >= 0) cbxProvider.SelectedIndex = -1;
        }

        private void wizard1_OnTranslate(object sender, Gui.Wizard.TranslateTextEventArgs e)
        {
            e.TranslatedText = Texts.Get(e.OriginalText);
        }

        private void wpselectdb_ShowFromNext(object sender, EventArgs e)
        {
            m_conn = new GenericSqlStoredConnection();
            m_conn.ConnectionString = m_builder.ConnectionString;
            m_conn.FactoryName = m_factoryName;
            m_conn.DriverName = m_driverName;

            commonConnectionEditFrame1.Connection = m_conn;
        }

        private void wpselectdb_CloseFromNext(object sender, Gui.Wizard.PageEventArgs e)
        {
            commonConnectionEditFrame1.SaveConnection();
        }

        public GenericSqlStoredConnection GetConnection() { return m_conn; }
    }

    [CreateFactoryItem(Name = "generic_connection")]
    public class GenericConnectionCreateWizard : CreateFactoryItemBase
    {
        public override string Title
        {
            get { return "s_generic_database_connection"; }
        }

        public override string Name
        {
            get { return "generic_connection"; }
        }

        public override string InfoText
        {
            get { return "s_file_desc_genericdb"; }
        }

        public override string Group
        {
            get { return "s_generic_connections"; }
        }

        public override string GroupName
        {
            get { return "generic_connections"; }
        }

        public override System.Drawing.Bitmap Bitmap
        {
            get { return CoreIcons.img_database; }
        }

        public override bool Create(ITreeNode parent, string name)
        {
            GenericConnWizard wiz = new GenericConnWizard();
            wiz.ShowDialogEx();
            if (wiz.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                GenericSqlStoredConnection con = wiz.GetConnection();
                con.FileName = Path.Combine(parent.FileSystemPath, name + ".con");
                con.Save();
                return true;
            }
            return false;
        }
    }
}
