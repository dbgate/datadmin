using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;

namespace DatAdmin
{
    public class DatabaseDropDown : ComboBox
    {
        //public event ConnectionNeededEventHandler ConnectionNeeded;

        public event EventHandler CurrentConnectionNeeded;

        string alldbs = "(" + Texts.Get("s_all_databases") + ")";
        string defdb = "(" + Texts.Get("s_default_database") + ")";

        IStoredConnection m_connection;

        public IStoredConnection Connection
        {
            get { return m_connection; }
            set
            {
                m_connection = value;

                Items.Clear();
                if (m_connection != null)
                {
                    Enabled = m_connection.SupportsDatabaseSelect;
                    Items.Add(alldbs);
                    Items.Add(defdb);
                    if (Enabled)
                    {
                        switch (m_connection.DatabaseMode)
                        {
                            case ConnectionDatabaseMode.All:
                                Text = alldbs; 
                                break;
                            case ConnectionDatabaseMode.Default:
                                Text = defdb;
                                break;
                            case ConnectionDatabaseMode.Explicit:
                                Text = m_connection.ExplicitDatabaseName;
                                break;
                        }
                    }
                    else
                    {
                        Text = "N/A";
                    }
                }
            }
        }

        protected override void OnDropDown(EventArgs e)
        {
            base.OnDropDown(e);

            try
            {
                if (Connection != null)
                {
                    if (CurrentConnectionNeeded != null) CurrentConnectionNeeded(this, EventArgs.Empty);
                    //var args=new ConnectionNeededEventArgs();
                    //ConnectionNeeded(this, args);
                    using (DbConnection conn = Connection.CreateSystemConnection())
                    {
                        try
                        {
                            conn.Open();
                        }
                        catch (Exception err)
                        {
                            throw new ConnectionFailedError("DAE-00159", err);
                        }
                        Items.Clear();
                        Items.Add(alldbs);
                        Items.Add(defdb);
                        List<string> dbs = new List<string>();
                        try
                        {
                            dbs = conn.GetDatabaseNames();
                        }
                        catch
                        {
                        }
                        foreach (string db in dbs)
                        {
                            Items.Add(db);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Errors.Report(err);
            }
        }

        public void SaveToConnection()
        {
            if (m_connection == null) return;
            if (m_connection.SupportsDatabaseSelect)
            {
                if (Text == alldbs)
                {
                    m_connection.ExplicitDatabaseName = null;
                    m_connection.DatabaseMode = ConnectionDatabaseMode.All;
                }
                else if (Text == defdb)
                {
                    m_connection.ExplicitDatabaseName = null;
                    m_connection.DatabaseMode = ConnectionDatabaseMode.Default;
                }
                else
                {
                    m_connection.ExplicitDatabaseName = Text;
                    m_connection.DatabaseMode = ConnectionDatabaseMode.Explicit;
                }
            }
        }
    }

    //public class ConnectionNeededEventArgs : EventArgs
    //{
    //    public DbConnection Connection;
    //}
    //public delegate void ConnectionNeededEventHandler(object sender, ConnectionNeededEventArgs e);
}
