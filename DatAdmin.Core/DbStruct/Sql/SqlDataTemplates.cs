using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DatAdmin
{
    public abstract class DataSqlGeneratorBase : AddonBase, IDataSqlGenerator
    {
        public override AddonType AddonType
        {
            get { return DataSqlGeneratorAddonType.Instance; }
        }

        #region IDataSqlGenerator Members

        public virtual void GenerateSqlRow(IBedRecord row, ISqlDumper dmp, string[] selcolumns)
        {
            throw new NotImplementedError("DAE-00106");
        }
        public virtual void GenerateSql(ISqlDumper dmp)
        {
            throw new NotImplementedError("DAE-00107");
        }

        public virtual bool NeedsWhereColumns
        {
            get { return false; }
        }

        public virtual bool IsRowEnumerator
        {
            get { return true; }
        }

        public virtual bool NeedsValueColumns
        {
            get { return false; }
        }

        public DataSqlGeneratorColumnSet WhereColumns { get; set; }

        public DataSqlGeneratorColumnSet ValueColumns { get; set; }

        #endregion

        #region ISqlGeneratorCommon Members

        public event EventHandler ChangedProperties;

        public void CallChangedProperties()
        {
            if (ChangedProperties != null) ChangedProperties(this, EventArgs.Empty);
        }

        public IProgressInfo ProgressInfo { get; set; }

        #endregion

        public override string ToString()
        {
            return XmlTool.GetRegisterAttr(this).Title;
        }

        protected static string[] GetColumns(DataSqlGeneratorColumnSet colset, ITableStructure ts, string[] selcolumns)
        {
            switch (colset.Mode)
            {
                case DataSqlGeneratorColumnSet.ModeEnum.AllColumns:
                    return ts.Columns.GetNames();
                case DataSqlGeneratorColumnSet.ModeEnum.ExplicitColumns:
                    return colset.Columns.ToArray();
                case DataSqlGeneratorColumnSet.ModeEnum.NoPkCols:
                    return ts.GetNoPkColumnNames();
                case DataSqlGeneratorColumnSet.ModeEnum.PrimaryKey:
                    return ts.GetPkColumnNames();
                case DataSqlGeneratorColumnSet.ModeEnum.SelectedColumns:
                    return selcolumns;
                case DataSqlGeneratorColumnSet.ModeEnum.NoSelectedColumns:
                    {
                        var res = new List<string>();
                        foreach (string col in ts.Columns.GetNames())
                        {
                            if (Array.IndexOf(selcolumns, col) <= 0) res.Add(col);
                        }
                        return res.ToArray();
                    }
            }
            return null;
        }

        protected void GenerateWhere(IBedRecord row, ISqlDumper dmp, string[] selcolumns)
        {
            dmp.Put(" ^where ");
            bool was = false;
            foreach (string col in GetColumns(WhereColumns, row.Structure, selcolumns))
            {
                if (was) dmp.Put(" ^and ");
                dmp.Put("%i = %v", col, row.GetValue(col));
                was = true;
            }
        }
    }

    public abstract class TableDataSqlGeneratorBase : DataSqlGeneratorBase
    {
        string m_schemaName;
        string m_tableName = "mytable";

        public string SchemaName
        {
            get { return m_schemaName; }
            set { m_schemaName = value; }
        }

        public string TableName
        {
            get { return m_tableName; }
            set { m_tableName = value; }
        }

        public NameWithSchema FullTableName
        {
            get { return new NameWithSchema(SchemaName, TableName); }
            set
            {
                SchemaName = value.Schema;
                TableName = value.Name;
            }
        }
    }

    [DataSqlGenerator(Name = "insert", Title = "INSERT")]
    public class InsertSqlGenerator : TableDataSqlGeneratorBase 
    {
        public override bool NeedsValueColumns
        {
            get { return true; }
        }

        public override void GenerateSqlRow(IBedRecord row, ISqlDumper dmp, string[] selcolumns)
        {
            string[] colnames = GetColumns(ValueColumns, row.Structure, selcolumns);
            var vals = new ValueTypeHolder[colnames.Length];
            for (int i = 0; i < colnames.Length; i++)
            {
                vals[i] = new ValueTypeHolder(row.GetValue(colnames[i]), row.Structure.Columns[colnames[i]].DataType);
            }
            dmp.PutCmd("^insert ^into %f (%,i) ^values (%,v)",
                FullTableName,
                colnames,
                vals
                );
        }
    }

    [DataSqlGenerator(Name = "update", Title = "UPDATE")]
    public class UpdateSqlGenerator : TableDataSqlGeneratorBase 
    {
        public override bool NeedsValueColumns
        {
            get { return true; }
        }

        public override bool NeedsWhereColumns
        {
            get { return true; }
        }

        public override void GenerateSqlRow(IBedRecord row, ISqlDumper dmp, string[] selcolumns)
        {
            dmp.Put("^update %f ^set ", FullTableName);
            bool was = false;
            foreach (string col in GetColumns(ValueColumns, row.Structure, selcolumns))
            {
                if (was) dmp.Put(", ");
                dmp.Put("%i = %v", col, new ValueTypeHolder(row.GetValue(col), row.Structure.Columns[col].DataType));
                was = true;
            }
            GenerateWhere(row, dmp, selcolumns);
            dmp.EndCommand();
        }
    }

    [DataSqlGenerator(Name = "delete", Title = "DELETE")]
    public class DeleteSqlGenerator : TableDataSqlGeneratorBase 
    {
        public override bool NeedsWhereColumns
        {
            get { return true; }
        }

        public override void GenerateSqlRow(IBedRecord row, ISqlDumper dmp, string[] selcolumns)
        {
            dmp.Put("^delete ^from %f ", FullTableName);
            GenerateWhere(row, dmp, selcolumns);
            dmp.EndCommand();
        }
    }
}
