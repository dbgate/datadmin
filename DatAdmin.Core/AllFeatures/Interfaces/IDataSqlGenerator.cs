using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public class DataSqlGeneratorColumnSet
    {
        public DataSqlGeneratorColumnSet()
        {
            Columns = new List<string>();
            Mode = ModeEnum.AllColumns;
        }

        public enum ModeEnum { SelectedColumns, NoSelectedColumns, PrimaryKey, NoPkCols, AllColumns, ExplicitColumns }

        [XmlCollection(typeof(string))]
        public List<string> Columns { get; set; }

        [XmlElem]
        public ModeEnum Mode { get; set; }
    }

    public interface IDataSqlGenerator : ISqlGeneratorCommon
    {
        bool IsRowEnumerator { get; }
        void GenerateSqlRow(IBedRecord row, ISqlDumper dmp, string[] selcolumns);
        void GenerateSql(ISqlDumper dmp);

        bool NeedsWhereColumns { get; }
        bool NeedsValueColumns { get; }

        DataSqlGeneratorColumnSet WhereColumns { get; set; }
        DataSqlGeneratorColumnSet ValueColumns { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class DataSqlGeneratorAttribute : RegisterAttribute
    {
    }

    [AddonType]
    public class DataSqlGeneratorAddonType : AddonType
    {
        public override string Name
        {
            get { return "datasqlgenerator"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(IDataSqlGenerator); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(DataSqlGeneratorAttribute); }
        }

        public static readonly DataSqlGeneratorAddonType Instance = new DataSqlGeneratorAddonType();
    }
}
