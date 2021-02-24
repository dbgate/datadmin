using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.IO;

namespace Plugin.sshtunnel
{
    public class SshResultSet : ITunnelResultSet
    {
        //StreamReader m_reader;
        StunConnection m_conn;
        string m_curLine;
        int[] m_colWidths = new int[0];
        int[] m_colOffsets = new int[0];
        string[] m_fieldNames = new string[0];
        int m_lineLength;

        public SshResultSet(StunConnection conn)
        {
            m_conn = conn;
            //m_reader = reader;
            NextLine();
            while (!m_curLine.StartsWith("+-") && !m_curLine.StartsWith("ERROR") && !m_curLine.StartsWith("mysql>")) NextLine();
            if (m_curLine.StartsWith("ERROR")) throw new SshTunnelQueryError(m_curLine);
            if (m_curLine.StartsWith("mysql>")) return;
            var colwis = new List<int>();
            var colofs = new List<int>();
            int actw = 0;
            while (!m_curLine.Trim().EndsWith("+"))
            {
                m_curLine = m_curLine.Trim() + m_conn.ReadLine();
            }
            for (int i = 1; i < m_curLine.Length; i++)
            {
                if (m_curLine[i] == '+')
                {
                    colwis.Add(actw);
                    actw = 0;
                }
                else
                {
                    if (actw == 0) colofs.Add(i);
                    actw++;
                }
            }
            colofs.RemoveAt(colofs.Count - 1);
            m_colWidths = colwis.ToArray();
            m_colOffsets = colofs.ToArray();
            m_lineLength = m_curLine.Length;

            NextLine();
            // read field names
            string fs = m_curLine.Trim();
            if (fs.StartsWith("|")) fs = fs.Substring(1);
            if (fs.EndsWith("|")) fs = fs.Substring(0, fs.Length - 1);
            m_fieldNames = fs.Split('|');
            for (int i = 0; i < m_fieldNames.Length; i++) m_fieldNames[i] = m_fieldNames[i].Trim();

            //m_fieldNames = new string[m_colOffsets.Length];
            //for (int i = 0; i < m_colOffsets.Length; i++)
            //{
            //    if (m_colOffsets[i] + m_colWidths[i] >= m_curLine.Length)
            //    {
            //        m_fieldNames[i] = m_curLine.Substring(m_colOffsets[i]).Trim();
            //        if (m_fieldNames[i].EndsWith("|")) m_fieldNames[i] = m_fieldNames[i].Substring(0, m_fieldNames[i].Length - 1).Trim();
            //    }
            //    else
            //    {
            //        m_fieldNames[i] = m_curLine.Substring(m_colOffsets[i], m_colWidths[i]).Trim();
            //    }
            //}

            NextLine(); // skip fields line
            if (m_curLine.Length < m_lineLength) m_conn.ReadLineFixedLenghtAndWait(m_lineLength - m_curLine.Length - 1);
            NextLine(); // skip +----- line
        }

        private void NextLine()
        {
            m_curLine = m_conn.ReadLineAndWait();
        }

        #region ITunnelResultSet Members

        public object[] FetchRow()
        {
            if (m_curLine.StartsWith("+-") || m_curLine.StartsWith("mysql>")) return null;
            object[] res = new object[m_colOffsets.Length];
            string line = m_curLine;
            if (line.Length < m_lineLength) line += "\n" + m_conn.ReadLineFixedLenghtAndWait(m_lineLength - line.Length - 1);
            for (int i = 0; i < m_colOffsets.Length; i++)
            {
                string field;
                if (m_colOffsets[i] + m_colWidths[i] >= line.Length)
                {
                    field = line.Substring(m_colOffsets[i]);
                    while (field.Length < m_colWidths[i])
                    {
                        field += "\n";
                    }
                }
                else
                {
                    field = line.Substring(m_colOffsets[i], m_colWidths[i]);
                }
                field = field.Trim();
                if (field == "NULL") field = null;
                res[i] = field;
            }
            NextLine();
            return res;
        }

        public ITableStructure GetStructure()
        {
            var res = new TableStructure();
            foreach (string fld in m_fieldNames)
            {
                var col = new ColumnStructure();
                col.ColumnName = fld;
                col.DataType = new DbTypeString();
                res._Columns.Add(col);
            }
            return res;
        }

        public void Close()
        {
        }

        public int RecordsAffected
        {
            get { return 0; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            m_conn.UnlockReader();
        }

        #endregion
    }
}
