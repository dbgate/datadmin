using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.IO;

namespace DatAdmin
{
    public class SqlTypeMappingItem
    {
        string m_sqlType;
        DbType m_databaseType;

        public DbType DatabaseType
        {
            get { return m_databaseType; }
            set { m_databaseType = value; }
        }
        public string SqlType
        {
            get { return m_sqlType; }
            set { m_sqlType = value; }
        }
        public override string ToString()
        {
            return String.Format("{0} <=> {1}", m_databaseType, m_sqlType);
        }
    }

    public class SqlDialectDefinition
    {
        List<SqlTypeMappingItem> m_typeMapping = new List<SqlTypeMappingItem>();
        string m_quotePrefix = "";
        string m_quoteSuffix = "";

        [Editor(typeof(CollectionEditor), typeof(UITypeEditor))]
        public List<SqlTypeMappingItem> TypeMapping
        {
            get { return m_typeMapping; }
            set { m_typeMapping = value; }
        }
        public string QuotePrefix
        {
            get { return m_quotePrefix; }
            set { m_quotePrefix = value; }
        }
        public string QuoteSuffix
        {
            get { return m_quoteSuffix; }
            set { m_quoteSuffix = value; }
        }

        private void AddMapping(DbType dbtype, string sqltype)
        {
            SqlTypeMappingItem item = new SqlTypeMappingItem();
            item.DatabaseType = dbtype;
            item.SqlType = sqltype;
            m_typeMapping.Add(item);
        }

        public void FillDefault()
        {
            AddMapping(DbType.AnsiString, "VARCHAR(50)");
            AddMapping(DbType.AnsiStringFixedLength, "CHAR(50)");
            AddMapping(DbType.Binary, "BLOB");
            AddMapping(DbType.Boolean, "INT");
            AddMapping(DbType.Byte, "INT");
            AddMapping(DbType.Currency, "INT");
            AddMapping(DbType.Date, "DATE");
            AddMapping(DbType.DateTime, "DATETIME");
            AddMapping(DbType.Decimal, "DECIMAL");
            AddMapping(DbType.Double, "NUMERIC(10,10)");
            AddMapping(DbType.Guid, "VARCHAR(50)");
            AddMapping(DbType.Int16, "INT");
            AddMapping(DbType.Int32, "INT");
            AddMapping(DbType.Int64, "INT");
            AddMapping(DbType.Object, "INT");
            AddMapping(DbType.Single, "NUMERIC(10,10)");
            AddMapping(DbType.String, "VARCHAR(50)");
            AddMapping(DbType.StringFixedLength, "CHAR(50)");
            AddMapping(DbType.Time, "TIME");
            AddMapping(DbType.UInt16, "INT");
            AddMapping(DbType.UInt32, "INT");
            AddMapping(DbType.UInt64, "INT");
            AddMapping(DbType.VarNumeric, "NUMERIC(10,10)");
            AddMapping(DbType.Xml, "TEXT");
        }
    }

    //public class XmlSqlDialect : ISqlDialect
    //{
    //    Dictionary<DbType, string> m_dbtypeToSqltype = new Dictionary<DbType, string>();
    //    SqlDialectDefinition m_sqldef;

    //    public XmlSqlDialect(SqlDialectDefinition sqldef)
    //    {
    //        m_sqldef = sqldef;
    //        foreach (SqlTypeMappingItem map in sqldef.TypeMapping) m_dbtypeToSqltype[map.DatabaseType] = map.SqlType;
    //    }

    //    #region ISqlDialect Members

    //    public string QuoteIdentifier(string ident)
    //    {
    //        return m_sqldef.QuotePrefix + ident + m_sqldef.QuoteSuffix;
    //    }

    //    public string GetSqlType(Type type)
    //    {
    //        return m_dbtypeToSqltype[TypeTool.GetProviderType(type)];
    //    }

    //    #endregion
    //}

    //[CreateFactoryItem]
    //public class DialectCreateFactory : ICreateFactoryItem
    //{
    //    #region ICreateFactoryItem Members

    //    public string Title
    //    {
    //        get { return Texts.Get("s_sql_dialect"); }
    //    }

    //    public string Group
    //    {
    //        get { return Texts.Get("s_common"); }
    //    }

    //    public System.Drawing.Bitmap Bitmap
    //    {
    //        get { return null; }
    //    }

    //    public bool Create(ITreeNode parent, string name)
    //    {
    //        SqlDialectDefinition dialdef = new SqlDialectDefinition();
    //        dialdef.FillDefault();
    //        XmlTool.SerializeObject(Path.Combine(parent.FileSystemPath, name + ".adx"), dialdef);
    //        return true;
    //    }

    //    #endregion
    //}
}
