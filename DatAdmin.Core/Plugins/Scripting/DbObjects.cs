using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin.Scripting
{
    /// <summary>
    /// definition of column
    /// </summary>
    public class Column
    {
        IColumnStructure m_col;
        ISqlDialect m_dialect;

        public Column(IColumnStructure col, ISqlDialect dialect)
        {
            m_col = col;
            m_dialect = dialect;
        }
        /// <summary>
        /// column name
        /// </summary>
        public string name
        {
            get { return m_col.ColumnName; }
        }
        /// <summary>
        /// quoted column name
        /// </summary>
        public string q_name
        {
            get { return m_dialect.QuoteIdentifier(m_col.ColumnName); }
        }
        /// <summary>
        /// SQL name of type (eg. "VARCHAR(50)")
        /// </summary>
        public string sql_name
        {
            get { return m_dialect.GenericTypeToSpecific(m_col.DataType).ToString(); }
        }
    }

    /// <summary>
    /// definition of constraint
    /// </summary>
    public class Constraint
    {
        IConstraint m_cnt;
        ISqlDialect m_dialect;

        public Constraint(IConstraint cnt, ISqlDialect dialect)
        {
            m_cnt = cnt;
            m_dialect = dialect;
        }
        /// <summary>
        /// constraint name
        /// </summary>
        public string name
        {
            get { return m_cnt.Name; }
        }
        /// <summary>
        /// quoted constraint name
        /// </summary>
        public string q_name
        {
            get { return m_dialect.QuoteIdentifier(m_cnt.Name); }
        }

        public string sql_type
        {
            get { return m_cnt.Type.GetSqlName(); }
        }
    }

    public class ColumnCollection : IEnumerable<Column>
    {
        ITableStructure m_table;
        ISqlDialect m_dialect;
        List<Column> m_cols = new List<Column>();

        public ColumnCollection(ITableStructure table, ISqlDialect dialect)
        {
            m_table = table;
            m_dialect = dialect;
            foreach (IColumnStructure col in m_table.Columns)
            {
                m_cols.Add(new Column(col, dialect));
            }
        }

        /// <summary>
        /// return scount of columns
        /// </summary>
        public int count { get { return m_cols.Count; } }
        /// <summary>
        /// returns column by index
        /// </summary>
        /// <param name="index">zero-based index of column</param>
        /// <returns></returns>
        public Column this[int index]
        {
            get { return m_cols[index]; }
        }
        /// <summary>
        /// returns column by name
        /// </summary>
        /// <param name="name">column's name</param>
        /// <returns></returns>
        public Column this[string name]
        {
            get { return m_cols[m_table.Columns.GetIndex(name)]; }
        }

        #region IEnumerable<Column> Members

        public IEnumerator<Column> GetEnumerator()
        {
            return m_cols.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return m_cols.GetEnumerator();
        }

        #endregion
    }
    /// <summary>
    /// Represents database table.
    /// </summary>
    public class Table
    {
        ITableSource m_table;
        ColumnCollection m_columns;
        ISqlDialect m_dialect;
        public Table(ITableSource table)
        {
            m_table = table;
            if (table.Connection != null) m_dialect = table.Connection.Dialect;
        }

        /// <summary>
        /// name of table
        /// </summary>
        public string name { get { return m_table.FullName.Name; } }
        /// <summary>
        /// quoted name of table, eg. on MySql enclosed in these `quotes`
        /// </summary>
        public string q_name { get { return m_dialect.QuoteIdentifier(m_table.FullName.Name); } }

        public string schema { get { return m_table.FullName.Schema; } }

        public string q_schema { get { return m_dialect.QuoteIdentifier(m_table.FullName.Schema); } }

        public string fullname { get { return m_table.FullName.ToString(); } }

        public string q_fullname { get { return DialectExtension.QuoteFullName(m_dialect, m_table.FullName); } }

        public string sql_create
        {
            get
            {
                StringWriter sw = new StringWriter();
                ISqlDumper dmp = m_dialect.CreateDumper(sw);
                dmp.CreateTable(m_table.InvokeLoadStructure(TableStructureMembers.AllNoRefs));
                return sw.ToString();
            }
        }

        /// <summary>
        /// collection of columns
        /// </summary>
        public ColumnCollection columns
        {
            get
            {
                if (m_columns == null) m_columns = new ColumnCollection(m_table.InvokeLoadStructure(TableStructureMembers.Columns), m_dialect);
                return m_columns;
            }
        }
    }

    public class Database
    {
        IDatabaseSource m_db;
        string m_dbname;
        ISqlDialect m_dialect;
        public Database(IDatabaseSource db, string dbname)
        {
            m_db = db;
            //if (dbname == null)
            //{
            //    try
            //    {
            //        if (m_dialect != null) m_dbname = m_dialect.GetCurrentDatabase(m_db.Connection);
            //        else m_dbname = m_db.Connection.SystemConnection.Database;
            //    }
            //    catch (Exception)
            //    {
            //        m_dbname = null;
            //    }
            //}
            m_dbname = dbname;
        }
        public Database(IDatabaseSource db)
            : this(db, null)
        {
        }

        private ISqlDialect GetDialect()
        {
            if (m_dialect == null) m_dialect = m_db.Dialect;
            return m_dialect;
        }

        /// <summary>
        /// name of table
        /// </summary>
        public string name { get { return m_dbname; } }
        /// <summary>
        /// quoted name of table, eg. on MySql enclosed in these `quotes`
        /// </summary>
        public string q_name { get { return GetDialect().QuoteIdentifier(m_dbname); } }
    }
}
