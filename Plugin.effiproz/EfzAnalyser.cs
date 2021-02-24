using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Data;

namespace Plugin.effiproz
{
    public class EfzAnalyser : DatabaseAnalyser
    {
        DataTable m_indexCols;

        protected override bool IsColumnNullable(IDataRecord row)
        {
            return (bool)row["IS_NULLABLE"];
        }

        protected override DbTypeBase AnalyseType(IDataRecord row, IPhysicalConnection conn, bool isdomain)
        {
            EfzTypeBase res = MakeSpecificEfzType(row);
            return res.ToGenericType();
        }

        private EfzTypeBase MakeSpecificEfzType(IDataRecord row)
        {
            string[] cols = row.GetFieldNames();
            string dt;
            if (cols.IndexOfEx("TYPE_NAME") >= 0) dt = row["TYPE_NAME"].ToString().ToUpper();
            else dt = row["DATA_TYPE"].ToString().ToUpper();
            int size = 0, digits = 0;
            if (cols.IndexOfEx("COLUMN_SIZE") >= 0) Int32.TryParse(row["COLUMN_SIZE"].ToString(), out size);
            if (cols.IndexOfEx("DECIMAL_DIGITS") >= 0) Int32.TryParse(row["DECIMAL_DIGITS"].ToString(), out digits);

            bool autoinc = false;
            int incstart = 1;
            int incdiff = 1;
            if (cols.IndexOfEx("IS_AUTOINCREMENT") >= 0) autoinc = (bool)row["IS_AUTOINCREMENT"];
            if (cols.IndexOfEx("IDENTITY_START") >= 0) Int32.TryParse(row["IDENTITY_START"].SafeToString() ?? "", out incstart);
            if (cols.IndexOfEx("IDENTITY_INCREMENT") >= 0) Int32.TryParse(row["IDENTITY_INCREMENT"].SafeToString() ?? "", out incdiff);
            var res = MakeSpecificEfzType(dt, size, digits);
            var ai = res as IEfzAutoIncrement;
            if (ai != null)
            {
                ai.IsIdentity = autoinc;
                ai.IdentitySeed = incstart;
                ai.IdentityIncrement = incdiff;
            }
            return res;
        }

        private EfzTypeBase MakeSpecificEfzType(string dt, int size, int digits)
        {
            switch (dt)
            {
                case "BOOLEAN":
                case "BIT":
                    return new EfzTypeBoolean();
                case "TINYINT":
                    return new EfzTypeTinyInt();
                case "SMALLINT":
                    return new EfzTypeSmallInt();
                case "INT":
                case "INTEGER":
                    return new EfzTypeInt();
                case "BIGINT":
                    return new EfzTypeBigInt();
                case "CHAR":
                case "NCHAR":
                case "CHARACTER":
                case "VARCHAR_IGNORECASE":
                    {
                        var res = new EfzTypeChar();
                        res.Length = size;
                        return res;
                    }
                case "VARCHAR":
                case "NVARCHAR":
                case "CHARACTER VARYING":
                    {
                        var res = new EfzTypeVarChar();
                        res.Length = size;
                        return res;
                    }
                case "NVARCHAR2":
                case "VARCHAR2":
                    {
                        var res = new EfzTypeVarChar2();
                        res.Length = size;
                        return res;
                    }
                case "BINARY":
                    {
                        var res = new EfzTypeBinary();
                        res.Length = size;
                        return res;
                    }
                case "VARBINARY":
                    {
                        var res = new EfzTypeVarBinary();
                        res.Length = size;
                        return res;
                    }
                case "DECIMAL":
                case "NUMERIC":
                case "NUMBER":
                    {
                        var res = new EfzTypeNumber();
                        res.Precision = size;
                        res.Scale = digits;
                        return res;
                    }
                case "TIMESTAMP":
                case "TIME":
                    {
                        var res = new EfzTypeTimestamp();
                        return res;
                    }
                case "DATETIME":
                case "DATE":
                    {
                        var res = new EfzTypeDate();
                        return res;
                    }
                case "TIMESTAMP WITH TIMEZONE":
                    {
                        var res = new EfzTypeTimestampTz();
                        return res;
                    }
                case "UNIQUEIDENTIFIER":
                    {
                        var res = new EfzTypeUniqueIdentifier();
                        return res;
                    }
                case "DOUBLE":
                    {
                        var res = new EfzTypeDouble();
                        return res;
                    }
                case "CLOB":
                    {
                        var res = new EfzTypeClob();
                        res.MaxBytes = size;
                        return res;
                    }
                case "BLOB":
                    {
                        var res = new EfzTypeBlob();
                        res.MaxBytes = size;
                        return res;
                    }
            }
            ReportUnknownType(dt);
            return new EfzTypeGeneric { Sql = dt };
        }

        public override bool SkipConstraint(IConstraint cnt)
        {
            if (cnt is ICheck && cnt.Name != null && cnt.Name.StartsWith("SYS_CT_")) return true;
            return base.SkipConstraint(cnt);
        }

        protected override void LoadIndexes(TableStructure table)
        {
            base.LoadIndexes(table);
            if (m_indexCols == null) m_indexCols = GetDbConn().GetSchema("IndexColumns");

            TableAnalyser tadidx = new TableAnalyser();
            foreach (DataRow row in m_indexCols.Rows)
            {
                TableAnalyser.Col col = new TableAnalyser.Col();
                col.keytype = "INDEX";
                col.keyname = row["INDEX_NAME"].ToString();
                col.tblname = row["TABLE_NAME"].ToString();
                col.tblschema = row["TABLE_SCHEM"].ToString();
                col.ordinal = row["ORDINAL_POSITION"].ToString();
                col.colname = row["COLUMN_NAME"].ToString();
                col.keyisunique = (bool)row["UNIQUE_INDEX"];
                if (col.keyname.StartsWith("SYS_IDX")) continue;
                if (col.tblschema.StartsWith("SYSTEM_")) continue;
                tadidx.cols.Add(col);
            }

            tadidx.SaveConstraints(table, this);
        }

        protected override void LoadSpecificObjects()
        {
            base.LoadSpecificObjects();

            LoadSpecificObjectListAndDetail("trigger", "SELECT * FROM INFORMATION_SCHEMA.TRIGGERS", LoadTrigger);
            LoadSpecificObjectListAndDetail("procedure", "SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE='PROCEDURE'", LoadRoutine);
            LoadSpecificObjectListAndDetail("function", "SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE='FUNCTION'", LoadRoutine);
        }

        private void LoadTrigger(DataRow row, SpecificObjectStructure obj)
        {
            obj.ObjectName = new NameWithSchema(row["TRIGGER_SCHEMA"].ToString(), row["TRIGGER_NAME"].ToString());
            obj.RelatedTable = new NameWithSchema(row["EVENT_OBJECT_SCHEMA"].ToString(), row["EVENT_OBJECT_TABLE"].ToString());
            obj.CreateSql = row["DEFINITION"].ToString();
        }

        private void LoadRoutine(DataRow row, SpecificObjectStructure obj)
        {
            obj.ObjectName = new NameWithSchema(row["ROUTINE_SCHEMA"].ToString(), row["ROUTINE_NAME"].ToString());
            obj.CreateSql = row["ROUTINE_DEFINITION"].ToString();
        }
    }
}
