using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public interface ISqlGeneratorCommon
    {
        IProgressInfo ProgressInfo { get; set; }
        event EventHandler ChangedProperties;
    }

    public interface IAppObjectSqlGenerator : ISqlGeneratorCommon
    {
        void GenerateSql(IDatabaseSource db, FullDatabaseRelatedName objname, ISqlDumper dmp, ISqlDialect dialect);
        void GenerateSql(AppObject appobj, ISqlDumper dmp, ISqlDialect dialect);
        bool SuitableFor(AppObject appobj);
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class AppObjectSqlGeneratorAttribute : RegisterAttribute
    {
    }

    [AddonType]
    public class AppObjectSqlGeneratorAddonType : AddonType
    {
        public override string Name
        {
            get { return "appobjectsqlgenerator"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(IAppObjectSqlGenerator); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(AppObjectSqlGeneratorAttribute); }
        }

        public static readonly AppObjectSqlGeneratorAddonType Instance = new AppObjectSqlGeneratorAddonType();
    }
}
