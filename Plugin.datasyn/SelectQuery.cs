using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.datasyn
{
    public enum SynQueryType { SelectAll, SelectKeyMd5, SelectKeyNull }

    public abstract class SelectQueryBase
    {
        public List<string> SelectedColumns { get; private set; }
        //protected List<string> SelectedColumns;
        //protected List<string> Md5Columns;
        //public ITableStructure Model;

        //public void SelectColumns(string[] cols)
        //{
        //    SelectedColumns = new List<string>(cols);
        //}
        //public void SelectMd5(string[] keycols, string[] md5cols)
        //{
        //    SelectedColumns = new List<string>(keycols);
        //    Md5Columns = new List<string>(md5cols);
        //}

        protected SelectQueryBase(IEnumerable<string> cols)
        {
            SelectedColumns = new List<string>(cols);
        }

        public abstract void GenerateSql(ISqlDumper dmp, IDataSynAdapter sada, SynSourceInfo info, SynQueryType qtype, Action<ISqlDumper> putCondition);
        protected void GenerateSqlToFrom(ISqlDumper dmp, IDataSynAdapter sada, SynSourceInfo info, SynQueryType qtype)
        {
            dmp.Put("^select %,i", (IEnumerable<string>)info.KeyCols);

            if (qtype == SynQueryType.SelectKeyMd5)
            {
                var colexprs = new List<string>();
                bool was = false;
                foreach (string col in info.CompareCols)
                {
                    if (was) colexprs.Add("'(SEP#n3f9)'");
                    var type = info.Model.Columns[col].DataType;
                    colexprs.Add(sada.Coalesce(sada.GetHashableString(dmp.Dialect.QuoteIdentifier(col), type), "'(NULL#093foew)'"));
                    was = true;
                }
                if (colexprs.Count == 0) colexprs.Add("'(NONEswgg5)'");
                dmp.Put(",");
                dmp.WriteRaw(sada.Md5(sada.Concat(colexprs)));
            }
            if (qtype == SynQueryType.SelectKeyNull)
            {
                dmp.Put(",^null");
            }
            if (qtype == SynQueryType.SelectAll)
            {
                if (info.DataCols.Length > 0)
                {
                    dmp.Put(",%,i", (IEnumerable<string>)info.DataCols);
                }
            }
            dmp.Put(" from ");
        }
        protected void GenerateSqlOrder(ISqlDumper dmp, IDataSynAdapter sada, SynSourceInfo info, SynQueryType qtype)
        {
            if (qtype == SynQueryType.SelectKeyMd5 || qtype == SynQueryType.SelectKeyNull)
            {
                dmp.Put(" order by %,i", (IEnumerable<string>)info.KeyCols);
            }
        }

        public SelectQueryBase Clone()
        {
            return (SelectQueryBase)MemberwiseClone();
        }
    }

    public class SelectQuerySql : SelectQueryBase
    {
        string Sql;

        public SelectQuerySql(string sql, IEnumerable<string> columns)
            : base(columns)
        {
            Sql = sql;
        }

        public override void GenerateSql(ISqlDumper dmp, IDataSynAdapter sada, SynSourceInfo info, SynQueryType qtype, Action<ISqlDumper> putCondition)
        {
            GenerateSqlToFrom(dmp, sada, info, qtype);
            dmp.Put("(");
            dmp.WriteRaw(Sql);
            dmp.Put(") sub");
            if (putCondition != null) putCondition(dmp);
            GenerateSqlOrder(dmp, sada, info, qtype);
        }
    }

    public class SelectFromTableQuery : SelectQueryBase
    {
        NameWithSchema Table;

        public SelectFromTableQuery(NameWithSchema table, IEnumerable<string> columns)
            : base(columns)
        {
            Table = table;
        }

        public override void GenerateSql(ISqlDumper dmp, IDataSynAdapter sada, SynSourceInfo info, SynQueryType qtype, Action<ISqlDumper> putCondition)
        {
            GenerateSqlToFrom(dmp, sada, info, qtype);
            dmp.Put("%f", Table);
            if (putCondition != null) putCondition(dmp);
            GenerateSqlOrder(dmp, sada, info, qtype);
        }
    }
}
