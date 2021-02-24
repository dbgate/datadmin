using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.IO;
using MySql.Data.MySqlClient;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Plugin.mysql
{
    public class MySqlDumpLoader : DumpLoaderBase
    {
        public MySqlDumpLoader(ISqlDialect dialect)
            : base(dialect)
        {
        }

        public override void Run(TextReader reader)
        {
            int maxpacket = Int32.Parse(Connection.ExecuteScalar("select @@max_allowed_packet").ToString());
            int maxdatalen = maxpacket / 3 * 2;
            if (Cfg != null && Cfg.MaxPacketSize > 0 && Cfg.MaxPacketSize < maxdatalen) maxdatalen = Cfg.MaxPacketSize;
            var splitter = m_dialect.CreateQuerySplitter();
            splitter.AllowGoSeparator = false;
            splitter.StripComments = true;

            // mysql splitter strips line comments, /* */ comments remains (they cannot be stripped, unless they contains ! )
            var setre = new Regex(@"((\/\*.*\*\/)?\s?)*(\/\*\!\d+)?\s*set\s+", RegexOptions.IgnoreCase);

            var scripter = Connection as IScriptAcceptConnection;
            var stateless = Connection as IStatelessConnection;
            string originit = "";
            if (stateless != null) originit = stateless.SessionInitScript;

            try
            {
                if (scripter != null && scripter.SendScriptSupported)
                {
                    // HTTP tunnel - SendScript + handle StateLessConnection only here (collect init script)
                    bool collectInit = stateless != null;
                    var pacdata = new List<string>();
                    var init = new List<string>();
                    int paclen = 0;
                    foreach (var item in splitter.Run(reader))
                    {
                        if (collectInit) 
                        {
                            if (setre.Match(item.Data).Success)
                            {
                                init.Add(item.Data);
                                continue;
                            }
                            else
                            {
                                collectInit = false;
                                if (!stateless.SessionInitScript.IsEmpty()) init.Insert(0, stateless.SessionInitScript);
                                stateless.SessionInitScript = init.CreateDelimitedText("||");
                            }
                        }
                        if (item.Data.TrimStart().StartsWith("DELIMITER ", StringComparison.InvariantCultureIgnoreCase)) continue;
                        if (pacdata.Count >= 64 || paclen >= maxdatalen)
                        {
                            SendPacket(scripter, pacdata);
                            pacdata.Clear();
                            paclen = 0;
                        }
                        pacdata.Add(item.Data);
                        paclen += item.Data.Length;
                        ShowProgress();
                    }
                    if (pacdata.Count > 0)
                    {
                        SendPacket(scripter, pacdata);
                        pacdata.Clear();
                    }
                }
                else
                {
                    var pacdata = new StringBuilder();
                    foreach (var item in splitter.Run(reader))
                    {
                        ShowProgress();
                        if (item.Data.TrimStart().StartsWith("DELIMITER ", StringComparison.InvariantCultureIgnoreCase)) continue;

                        if (item.Delimiter != ";" || (pacdata.Length + item.Length > maxdatalen && pacdata.Length > 0))
                        {
                            SendPacket(pacdata.ToString());
                            pacdata = new StringBuilder();
                        }
                        if (item.Delimiter != ";")
                        {
                            SendPacket(item.Data);
                        }
                        else
                        {
                            pacdata.Append(item.Data);
                            pacdata.Append(";\n");
                        }
                    }
                    if (pacdata.Length > 0)
                    {
                        SendPacket(pacdata.ToString());
                    }
                }
            }
            finally
            {
                if (stateless != null) stateless.SessionInitScript = originit;
            }
            ReportFinish();
        }

        private void SendPacket(IScriptAcceptConnection scripter, List<string> pacdata)
        {
            try
            {
                scripter.SendScript(pacdata);
                OkInc();
            }
            catch (Exception err)
            {
                ReportFail(pacdata[0], err);
            }
        }

        private void SendPacket(string query)
        {
            try
            {
                Connection.ExecuteNonQuery(query, m_dialect, Transaction, null);
                OkInc();
            }
            catch (Exception err)
            {
                ReportFail(query, err);
            }
        }

        private void RunText(Stream fr)
        {
            using (StreamReader sr = new StreamReader(fr, TextEncoding))
            {
                Run(sr);
            }
        }

        private void RunBinary(Stream fr)
        {
            if (Connection is MySqlConnection)
            {
                var myconn = (MySqlConnection)Connection;
                string conns = myconn.ConnectionString;
                MySqlConnectionStringBuilder msb = new MySqlConnectionStringBuilder(conns);
                if (!msb.PersistSecurityInfo)
                {
                    RunText(fr);
                    return;
                }

                // import dump, reopen database in latin1 encoding for preserve BLOB data
                msb.CharacterSet = "latin1";
                msb.AllowUserVariables = true;
                string olddb = myconn.Database;
                Connection.Close();
                Connection.ConnectionString = msb.ConnectionString;
                Connection.Open();
                Connection.SafeChangeDatabase(olddb);

                try
                {
                    using (var sr = new StreamReader(fr, Latin1))
                    {
                        Run(sr);
                    }
                }
                finally
                {
                    Connection.Close();
                    Connection.ConnectionString = conns;
                    Connection.Open();
                    Connection.SafeChangeDatabase(olddb);
                }
            }
            else if (Connection is IBinaryCommandConnection)
            {
                var bin = (IBinaryCommandConnection)Connection;
                var oldenc = bin.BinaryCommandEncoding;
                bin.BinaryCommandEncoding = Latin1;
                using (var sr = new StreamReader(fr, Latin1))
                {
                    Run(sr);
                }
                bin.BinaryCommandEncoding = oldenc;
            }
            else
            {
                RunText(fr);
            }
        }

        public override void Run(Stream fr)
        {
            InitProgress(fr);
            if (LoadAsBinary) RunBinary(fr);
            else RunText(fr);
        }
        Encoding Latin1 { get { return Encoding.GetEncoding("iso-8859-1"); } }
        MysqlDatabaseLoader Cfg
        {
            get { return Config as MysqlDatabaseLoader; }
        }
        bool LoadAsBinary
        {
            get { return Cfg != null && Cfg.LoadAsBinary; }
        }
        Encoding TextEncoding
        {
            get
            {
                if (Cfg != null) return Cfg.GetEncoding();
                return Encoding.UTF8;
            }
        }
    }

    [DatabaseLoader(Name = "mysql_dumploader", SupportsDirectUse = false)]
    public class MysqlDatabaseLoader : SqlDumpLoaderBase
    {
        public override string GetTitle()
        {
            return "DatAdmin MySQL Dump Loader";
        }

        bool m_loadAsBinary = true;
        [TypeConverter(typeof(YesNoTypeConverter))]
        [XmlElem]
        public bool LoadAsBinary
        {
            get { return m_loadAsBinary; }
            set { m_loadAsBinary = value; }
        }

        Encoding m_encoding = System.Text.Encoding.UTF8;
        [DatAdmin.DisplayName("s_encoding")]
        [TypeConverter(typeof(EncodingTypeConverter))]
        [XmlElem]
        public string Encoding
        {
            get { return m_encoding.WebName; }
            set { m_encoding = System.Text.Encoding.GetEncoding(value); }
        }

        public Encoding GetEncoding() { return m_encoding; }

        int m_maxPacketSize = 64 * 1024;
        [XmlElem]
        public int MaxPacketSize
        {
            get { return m_maxPacketSize; }
            set { m_maxPacketSize = value; }
        }

    }
}
