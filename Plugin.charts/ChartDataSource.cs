using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.charts
{
    //public abstract class ChartDataSourceBase : IChartDataSource
    //{
    //    public virtual IPhysicalConnection Connection { get { return null; } }

    //    #region IChartDataSource Members

    //    public abstract ITableStructure GetStructure();
    //    public abstract IEnumerable<IBedRecord> GetRows();

    //    public virtual IEnumerable<IBedRecord> GetAggregatedRows(string agrcol, AggregateSelector agrsel, AggregateDataColumn[] data)
    //    {
    //        throw new NotImplementedError("");
    //    }

    //    #endregion
    //}

    //public class FixedChartDataSource : ChartDataSourceBase
    //{
    //    InMemoryTable m_table;
    //    public FixedChartDataSource(InMemoryTable table)
    //    {
    //        m_table = table;
    //    }
    //    public override ITableStructure GetStructure()
    //    {
    //        return m_table.Structure;
    //    }
    //    public override IEnumerable<IBedRecord> GetRows()
    //    {
    //        foreach (var row in m_table.Rows)
    //        {
    //            yield return row;
    //        }
    //    }
    //}

    //public class QueryChartDataSource : ChartDataSourceBase
    //{
    //    IDatabaseSource m_db;
    //    string m_query;
    //    TableStructure m_table;

    //    public QueryChartDataSource(IDatabaseSource db, string query)
    //    {
    //        m_db = db;
    //        m_query = query;
    //    }

    //    public override IPhysicalConnection Connection
    //    {
    //        get { return m_db.Connection; }
    //    }

    //    public override ITableStructure GetStructure()
    //    {
    //        if (m_table == null)
    //        {
    //            using (var cmd = Connection.SystemConnection.CreateCommand())
    //            {
    //                cmd.CommandText = m_query;
    //                using (var reader = cmd.ExecuteReader(System.Data.CommandBehavior.SchemaOnly))
    //                {
    //                    m_table = reader.GetTableStructure(m_db.Dialect);
    //                }
    //            }
    //        }
    //        return m_table;
    //    }

    //    public override IEnumerable<IBedRecord> GetRows()
    //    {
    //        using (var cmd = Connection.SystemConnection.CreateCommand())
    //        {
    //            cmd.CommandText = m_query;

    //            using (IBedReader reader = m_db.GetAnyDDA().AdaptReader(cmd.ExecuteReader()))
    //            {
    //                while (reader.Read())
    //                {
    //                    yield return reader;
    //                }
    //            }
    //        }
    //    }
    //}
}
