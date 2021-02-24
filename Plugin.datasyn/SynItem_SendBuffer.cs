using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Linq;
using System.IO;

namespace Plugin.datasyn
{
    partial class SynItem
    {
        List<string[]> m_updates = new List<string[]>();
        List<string[]> m_inserts = new List<string[]>();
        List<string[]> m_deletes = new List<string[]>();
        List<string[]> m_equals = new List<string[]>();
        public const int MAXBUFSIZE = 200;

        private int m_inserted = 0, m_updated = 0, m_deleted = 0, m_equal = 0;

        private void SendBufferIfBig()
        {
            if (m_updates.Count > MAXBUFSIZE) SendUpdateBuffer();
            if (m_deletes.Count > MAXBUFSIZE) SendDeleteBuffer();
            if (m_inserts.Count > MAXBUFSIZE) SendInsertBuffer();
            if (m_equals.Count > MAXBUFSIZE) SendEqualBuffer();
        }

        private void SendBuffer()
        {
            SendUpdateBuffer();
            SendDeleteBuffer();
            SendInsertBuffer();
            SendEqualBuffer();
        }

        private void PutCondition(ISqlDumper dmp, string[] keycols, string[] key)
        {
            dmp.Put("(");
            for (int i = 0; i < keycols.Length; i++)
            {
                if (i > 0) dmp.Put(" and ");
                if (String.Compare(key[i], "(NULL)", true) == 0)
                {
                    dmp.Put("%i ^is ^null", keycols[i]);
                }
                else
                {
                    dmp.Put("%i=%v", keycols[i], key[i]);
                }
            }
            dmp.Put(")");
        }

        private void PutConditionList(ISqlDumper dmp, IEnumerable<string[]> keys)
        {
            bool was = false;
            foreach (string[] key in keys)
            {
                if (was) dmp.Put(" or ");
                PutCondition(dmp, m_srcInfo.KeyCols, key);
                was = true;
            }
        }

        private string CreateQuery(IEnumerable<string[]> keys, IDatabaseSource dbconn, IDataSynAdapter sada, SynSourceInfo info)
        {
            var sw = new StringWriter();
            var dmp = dbconn.Dialect.CreateDumper(sw);
            m_srcInfo.Query.GenerateSql(dmp, sada, info, SynQueryType.SelectAll, d => { d.Put(" where "); PutConditionList(d, keys); });
            //dmp.Put("select %,i, %,i from (%s) sub where ", m_srcInfo.KeyCols, m_srcInfo.DataCols, m_srcInfo.ReadQuery);

            //PutConditionList(dmp, keys);
            return sw.ToString();
        }

        PolySqlHolder CreateHolderDumper()
        {
            var so = new PolyStringSqOutputStream();
            var dmp = m_syn.m_target.Dialect.CreateDumper(so, new SqlFormatProperties());
            return new PolySqlHolder
            {
                Dumper = dmp,
                Scripts = so.Lines,
            };
        }

        private List<string> DoCreateUpdateQuery(string askquery)
        {
            // buffer for update command
            var dh = CreateHolderDumper();
            var dmp = dh.Dumper;

            using (var cmd = m_syn.m_source.Connection.SystemConnection.CreateCommand())
            {
                cmd.CommandText = askquery;

                using (IBedReader reader = m_syn.m_source.GetAnyDDA().AdaptReader(cmd.ExecuteReader()))
                {
                    while (reader.Read())
                    {
                        IBedRecord rec = reader;
                        foreach (var recada in m_outputAdapters)
                        {
                            rec = recada.AdaptRecord(rec, m_syn.Progress);
                        }

                        dmp.Put("update %f set ", m_item.Target.Table);
                        for (int i = 0; i < m_dstInfo.DataCols.Length; i++)
                        {
                            rec.ReadValue(i + m_dstInfo.KeyCols.Length);
                            if (i > 0) dmp.Put(", ");
                            dmp.Put("%i=%v", m_dstInfo.DataCols[i], rec);
                        }
                        dmp.Put(" where ");
                        for (int i = 0; i < m_dstInfo.KeyCols.Length; i++)
                        {
                            if (i > 0) dmp.Put(" and ");
                            rec.ReadValue(i);
                            if (rec.GetFieldType() == TypeStorage.Null)
                            {
                                dmp.Put("%i ^is ^null", m_dstInfo.KeyCols[i]);
                            }
                            else
                            {
                                dmp.Put("%i=%v", m_dstInfo.KeyCols[i], rec);
                            }
                        }
                        dmp.EndCommand();
                    }
                }
            }
            return dh.Scripts;
        }

        private List<string> DoCreateInsertQuery(string askquery)
        {
            var dh = CreateHolderDumper();
            var dmp = dh.Dumper;
            var autoinccol = m_dstInfo.Model.FindAutoIncrementColumn();
            bool idins = autoinccol != null &&
                (m_dstInfo.KeyCols.Contains(autoinccol.ColumnName) || m_dstInfo.DataCols.Contains(autoinccol.ColumnName));
            if (idins) dmp.AllowIdentityInsert(m_item.Target.Table, true);

            using (var cmd = m_syn.m_source.Connection.SystemConnection.CreateCommand())
            {
                cmd.CommandText = askquery;

                using (IBedReader reader = m_syn.m_source.GetAnyDDA().AdaptReader(cmd.ExecuteReader()))
                {
                    while (reader.Read())
                    {
                        IBedRecord rec = reader;
                        foreach (var recada in m_outputAdapters)
                        {
                            rec = recada.AdaptRecord(rec, m_syn.Progress);
                        }
                        dmp.Put("insert into %f (%,i %s %,i) values (%,v)",
                            m_item.Target.Table,
                            m_dstInfo.KeyCols, 
                            m_dstInfo.DataCols.Length > 0 ? "," : "",
                            m_dstInfo.DataCols,
                            rec
                            );
                        dmp.EndCommand();
                    }
                }
            }

            if (idins) dmp.AllowIdentityInsert(m_item.Target.Table, false);
            return dh.Scripts;
        }

        private void CallSendScript(List<string> script, DataSynOperation operation)
        {
            if (m_syn.m_outBuffer != null)
            {
                SaveScript(script);
            }
            else
            {
                m_syn.m_target.Connection.Invoke2(DoSendScript, script, operation);
            }
        }


        private void SendUpdateBuffer()
        {
            if (m_updates.Count == 0) return;

            if (m_options.Update == true && WantSynchronize)
            {
                string query = CreateQuery(m_updates, m_syn.m_source, m_srcSada, m_srcInfo);
                var updquery = (List<string>)m_syn.m_source.Connection.InvokeR1<List<string>, string>(DoCreateUpdateQuery, query);
                CallSendScript(updquery, DataSynOperation.Update);
            }

            if (m_gridFills[(int)SynTableData.Modified] != null)
            {
                FillUpdateGrid();
            }

            m_updated += m_updates.Count;
            m_updates = new List<string[]>();
        }

        private IEnumerable<IBedRecord> EnumUpgradeGridRows(GridTable srcgrid, GridTable dstgrid, ITableStructure ts)
        {
            var ensrc = srcgrid.EnumRows(new TableDataSetProperties()).GetEnumerator();
            var endst = dstgrid.EnumRows(new TableDataSetProperties()).GetEnumerator();
            var holder = new BedValueHolder();
            for (; ; )
            {
                if (!ensrc.MoveNext()) break;
                if (!endst.MoveNext()) break;
                var newrec = new ArrayDataRecord(ts);

                for (int i = 0; i < m_srcInfo.KeyCols.Length; i++)
                {
                    newrec.SeekValue(i);
                    ensrc.Current.ReadValue(i);
                    ensrc.Current.WriteTo(newrec);
                }

                for (int i = 0; i < m_srcInfo.DataCols.Length; i++)
                {
                    newrec.SeekValue(m_srcInfo.KeyCols.Length + i);
                    ensrc.Current.ReadValue(m_srcInfo.KeyCols.Length + i);
                    ensrc.Current.WriteTo(newrec);
                }

                for (int i = 0; i < m_dstInfo.DataCols.Length; i++)
                {
                    newrec.SeekValue(m_srcInfo.KeyCols.Length + m_srcInfo.DataCols.Length + i);
                    endst.Current.ReadValue(m_srcInfo.KeyCols.Length + i);
                    endst.Current.WriteTo(newrec);
                }

                yield return newrec;
            }
        }

        private void FillUpdateGrid()
        {
            var srcgrid = new GridTable(m_srcInfo.GetTableStructure(SynQueryType.SelectAll), "");
            var dstgrid = new GridTable(m_dstInfo.GetTableStructure(SynQueryType.SelectAll), "");
            try
            {
                m_gridSourceConn.Connection.Invoke(() => { FillGridTable(m_updates, srcgrid, m_gridSourceConn, m_srcSada, m_srcInfo); });
                m_gridTargetConn.Connection.Invoke(() => { FillGridTable(m_updates, dstgrid, m_gridTargetConn, m_dstSada, m_dstInfo); });

                var upgrid = m_gridFills[(int)SynTableData.Modified];
                upgrid.Fill(EnumUpgradeGridRows(srcgrid, dstgrid, upgrid.GetStructure(null)));
            }
            finally
            {
                srcgrid.CloseView();
                dstgrid.CloseView();
            }
        }

        private void DoFillInsertGrid()
        {
            FillGridTable(m_inserts, m_gridFills[(int)SynTableData.OnlyInSource], m_gridSourceConn, m_srcSada, m_srcInfo);
        }

        private void DoFillDeleteGrid()
        {
            FillGridTable(m_deletes, m_gridFills[(int)SynTableData.OnlyInTarget], m_gridTargetConn, m_dstSada, m_dstInfo);
        }

        private void DoFillEqualGrid()
        {
            FillGridTable(m_equals, m_gridFills[(int)SynTableData.Equal], m_gridSourceConn, m_srcSada, m_srcInfo);
        }

        private void FillGridTable(IEnumerable<string[]> keys, GridTable grid, IDatabaseSource conn, IDataSynAdapter sada, SynSourceInfo info)
        {
            string query = CreateQuery(keys, conn, sada, info);

            using (var cmd = conn.Connection.SystemConnection.CreateCommand())
            {
                cmd.CommandText = query;

                using (IBedReader reader = conn.GetAnyDDA().AdaptReader(cmd.ExecuteReader()))
                {
                    grid.Fill(reader);
                }
            }
        }

        private void SendInsertBuffer()
        {
            if (m_inserts.Count == 0) return;

            if (m_options.Insert == true && WantSynchronize)
            {
                string query = CreateQuery(m_inserts, m_syn.m_source, m_srcSada, m_srcInfo);
                var insquery = (List<string>)m_syn.m_source.Connection.InvokeR1<List<string>, string>(DoCreateInsertQuery, query);
                CallSendScript(insquery, DataSynOperation.Insert);
            }

            if (m_gridFills[(int)SynTableData.OnlyInSource] != null)
            {
                m_gridSourceConn.Connection.Invoke(DoFillInsertGrid);
            }

            m_inserted += m_inserts.Count;
            m_inserts = new List<string[]>();
        }

        private void SendDeleteBuffer()
        {
            if (m_deletes.Count == 0) return;

            if (m_options.Delete == true && WantSynchronize)
            {
                var dh = CreateHolderDumper();
                var dmp = dh.Dumper;
                dmp.Put("delete from %f where ", m_item.Target.Table);
                PutConditionList(dmp, m_deletes);
                List<string> delquery = dh.Scripts;
                CallSendScript(delquery, DataSynOperation.Delete);
            }

            if (m_gridFills[(int)SynTableData.OnlyInTarget] != null)
            {
                m_gridTargetConn.Connection.Invoke(DoFillDeleteGrid);
            }

            m_deleted += m_deletes.Count;
            m_deletes = new List<string[]>();
        }

        private void SendEqualBuffer()
        {
            if (m_equals.Count == 0) return;

            if (m_gridFills[(int)SynTableData.Equal] != null)
            {
                m_gridSourceConn.Connection.Invoke(DoFillEqualGrid);
            }

            m_equal += m_equals.Count;
            m_equals = new List<string[]>();
        }

        private void DoSendScript(List<string> script, DataSynOperation operation)
        {
            var begin = CreateHolderDumper();
            var end = CreateHolderDumper();
            if (m_options.DisableConstraints == true)
            {
                begin.Dumper.EnableConstraints(m_item.Target.Table, false);
                end.Dumper.EnableConstraints(m_item.Target.Table, true);
            }
            m_syn.m_dstSada.SendScript(m_syn.m_target.Connection, script, begin.Scripts, end.Scripts, operation, m_syn.m_reportEnv);
        }

        private void SaveScriptCore(List<string> script)
        {
            foreach (string line in script)
            {
                m_syn.m_outBuffer.WriteLine(m_syn.m_dstDDA.FilterNotDumpableCharacters(line) + ";");
            }
        }

        private void SaveScript(List<string> script)
        {
            var begin = CreateHolderDumper();
            var end = CreateHolderDumper();
            if (m_options.DisableConstraints == true)
            {
                begin.Dumper.EnableConstraints(m_item.Target.Table, false);
                end.Dumper.EnableConstraints(m_item.Target.Table, true);
            }
            SaveScriptCore(begin.Scripts);
            SaveScriptCore(script);
            SaveScriptCore(end.Scripts);
        }

        class PolySqlHolder
        {
            internal ISqlDumper Dumper;
            internal List<string> Scripts;
        }
    }
}
