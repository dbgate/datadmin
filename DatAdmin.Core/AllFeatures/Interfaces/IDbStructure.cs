using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Xml;
using System.ComponentModel;

/**
 * Definition of DatAdmin database structure model
 * all objects have back pointers to their parents
 * cross-references to other objects are named
 * when renaming object, all these references must be renamed
 */

namespace DatAdmin
{
    public interface IAbstractObjectStructure
    {
        Dictionary<string, string> SpecificData { get; }
        string Comment { get; set; }
        string GroupId { get; }
        void AddAllObjects(List<IAbstractObjectStructure> res);
        FullDatabaseRelatedName GetName();

        //bool IsFreezed { get; }
        //IAbstractObjectStructure FreezeUntyped();
        //IAbstractObjectStructure CloneUntyped();
    }

    public interface IAttachableObjectStructure : IAbstractObjectStructure
    {
        IAbstractObjectStructure Parent { get; }
    }

    public interface ITableObjectStructure : IAttachableObjectStructure
    {
        ITableStructure Table { get; }
    }

    public interface IDatabaseObjectStructure : IAttachableObjectStructure
    {
        IDatabaseStructure Database { get; }
    }

    public interface IColumnStructure : ITableObjectStructure
    {
        int ColumnOrder { get;}
        string ColumnName { get;}
        DbTypeBase DataType { get;}
        bool IsNullable { get;}
        SqlExpression DefaultValue { get;}
        /// <summary>
        /// specific data for column in form dialect.specific_value, eg. default values 
        /// if no portable expressino is used
        /// </summary>
        string Collation { get; }
        string CharacterSet { get; }
        NameWithSchema Domain { get; }
    }

    public interface IGenericCollection<Key, Value> : IEnumerable<Value>
    {
        Value this[int index] { get; }
        Value this[Key name] { get; }
        int Count { get; }
        int GetIndex(Key name);
    }

    public interface IColumnCollection : IGenericCollection<string, IColumnStructure> { }
    public interface IConstraintCollection : IGenericCollection<string, IConstraint> { }
    //public interface IForeignKeyCollection : IGenericCollection<string, IForeignKey> { }

    public enum ConstraintType { PrimaryKey, ForeignKey, Unique, Index, Check }

    public static class ConstraintTypeExtension
    {
        public static string GetSqlName(this ConstraintType type)
        {
            switch (type)
            {
                case ConstraintType.ForeignKey: return "FOREIGN KEY";
                case ConstraintType.Check: return "CHECK";
                case ConstraintType.Index: return "INDEX";
                case ConstraintType.PrimaryKey: return "PRIMARY KEY";
                case ConstraintType.Unique: return "UNIQUE";
            }
            throw new InternalError("DAE-00016 Unknown constraint type:" + type.ToString());
        }
        public static string GetIdentifier(this ConstraintType type)
        {
            return type.GetSqlName().ToLower().Replace(" ", "_");
        }
    }

    public interface IConstraint : ITableObjectStructure 
    {
        string Name { get;}
        //NameWithSchema Table { get;}
        ConstraintType Type { get; }
        //string SqlTypeName { get;}
    }

    public interface IColumnReference
    {
        string ColumnName { get; }
        Dictionary<string, string> SpecificData { get; }
    }

    public interface IColumnsConstraint : IConstraint
    {
        List<IColumnReference> Columns { get; }
        //List<string> Columns { get;}
    }

    public interface IPrimaryKey : IColumnsConstraint
    {
    }

    public enum ForeignKeyAction { 
        [Description("s_none")] Undefined,
        [Description("NO ACTION")] NoAction, 
        [Description("CASCADE")] Cascade, 
        [Description("RESTRICT")] Restrict, 
        [Description("SET NULL")] SetNull
    };

    public interface IForeignKey : IColumnsConstraint
    {
        NameWithSchema PrimaryKeyTable { get;}
        /// <summary>
        /// can be null, than primary key of ForeignTable is assumed
        /// </summary>
        IList<IColumnReference> PrimaryKeyColumns { get; }
        ForeignKeyAction OnDeleteAction { get; }
        ForeignKeyAction OnUpdateAction { get; }
    }

    public interface IUnique : IColumnsConstraint
    {
    }

    public interface ICheck : IConstraint
    {
        string Expression { get;}
    }

    public interface IIndex : IColumnsConstraint
    {
        bool IsUnique { get;}
    }

    [Flags]
    public enum TableStructureMembers
    {
        None = 0x0,
        Name = 0x1,
        ColumnNames = 0x2,
        ColumnTypes = 0x4,
        PrimaryKey = 0x8,
        ForeignKeys = 0x10,
        Checks = 0x20,
        Uniques = 0x40,
        Indexes = 0x80,
        ReferencedFrom = 0x100,
        SpecificDetails = 0x200,
        All = Name | ColumnNames | ColumnTypes | PrimaryKey | ForeignKeys | Checks |
              Uniques | Indexes | ReferencedFrom | SpecificDetails,
        AllNoRefs = All - ReferencedFrom,
        Columns = ColumnNames | ColumnTypes,
        Constraints = PrimaryKey | ForeignKeys | Checks | Uniques | Indexes | ReferencedFrom,
        ConstraintsNoIndexes = PrimaryKey | ForeignKeys | Checks | Uniques | ReferencedFrom,
        ConstraintsNoRefs = PrimaryKey | ForeignKeys | Checks | Uniques | Indexes,
        ConstraintsNoIndexesNoRefs = PrimaryKey | ForeignKeys | Checks | Uniques,
    }

    public interface IFullNamedObject
    {
        NameWithSchema FullName { get; }
    }

    public interface ITableStructure : IDatabaseObjectStructure, IFullNamedObject
    {
        TableStructureMembers FilledMembers { get; }
        IColumnCollection Columns { get;}
        IConstraintCollection Constraints { get;}
        //IForeignKeyCollection ReferencedFrom { get;}
        InMemoryTable FixedData { get; }
    }

    public class DependencyItem : IComparable
    {
        public NameWithSchema Name;
        public string ObjectType;
        public DependencyItem() { }
        public DependencyItem(DependencyItem src)
        {
            Name = src.Name;
            ObjectType = src.ObjectType;
        }
        public override bool Equals(object obj)
        {
            var item = obj as DependencyItem;
            if (item == null) return base.Equals(obj);
            return Name == item.Name && ObjectType == item.ObjectType;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return Name.GetHashCode() + ObjectType.GetHashCode();
            }
        }
        public static bool operator ==(DependencyItem a, DependencyItem b)
        {
            return Object.Equals(a, b);
        }
        public static bool operator !=(DependencyItem a, DependencyItem b)
        {
            return !Object.Equals(a, b);
        }
        public override string ToString()
        {
            return String.Format("{0}:{1}", ObjectType, Name);
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            var o = obj as DependencyItem;
            if (o != null)
            {
                int res = String.Compare(ObjectType, o.ObjectType);
                if (res != 0) return res;
                res = Name.CompareTo(o.Name);
                return res;
            }
            return 0;
        }

        #endregion
    }

    public interface ISpecificObjectStructure : IDatabaseObjectStructure
    {
        List<DependencyItem> DependsOn { get; }
        string CreateSql { get; }
        NameWithSchema ObjectName { get; }
        NameWithSchema RelatedTable { get; }
        string SpecificDialect { get; }
        string ObjectType { get; }
    }

    public interface ISchemaStructure : IDatabaseObjectStructure
    {
        string SchemaName { get; }
    }
    public interface IDomainStructure : IDatabaseObjectStructure, IFullNamedObject
    {
        //NameWithSchema FullName { get; }
        DbTypeBase DataType { get; }
        bool IsNullable { get; }
        SqlExpression DefaultValue { get; }
        string CheckExpr { get; }
        string Collation { get; }
        string CharacterSet { get; }
    }

    public interface ICollationStructure : IDatabaseObjectStructure
    {
        string Name { get; }
        string CharacterSet { get; }
    }

    public interface ICharacterSetStructure : IDatabaseObjectStructure
    {
        string Name { get; }
        string DefaultCollation { get; }
    }

    public interface ITableCollection : IGenericCollection<NameWithSchema, ITableStructure> { }
    public interface ISpecificObjectCollection : IGenericCollection<NameWithSchema, ISpecificObjectStructure> { }
    public interface ISchemaCollection : IGenericCollection<string, ISchemaStructure> { }
    public interface IDomainCollection : IGenericCollection<NameWithSchema, IDomainStructure> { }
    public interface ICollationCollection : IGenericCollection<string, ICollationStructure> { }
    public interface ICharacterSetCollection : IGenericCollection<string, ICharacterSetStructure> { }

    public interface IDatabaseStructure : IAbstractObjectStructure
    {
        ITableCollection Tables { get;}
        ITableCollection ViewAsTables { get; }
        // specific objects grouped by object type
        IDictionary<string, ISpecificObjectCollection> SpecificObjects { get; }
        ISchemaCollection Schemata { get; }
        ICollationCollection Collations { get; }
        IDomainCollection Domains { get; }
        ICharacterSetCollection CharacterSets { get; }

        ISqlDialect Dialect { get; }
        bool ForceSingleSchema { get; }
    }

    public interface INameTransformation
    {
        NameWithSchema RenameObject(NameWithSchema name, string objtype);
        string RenameConstraint(IConstraint constraint);
        string RenameColumn(NameWithSchema table, string name);
    }

    //public interface ISpecificObjectImpl
    //{
    //    NameWithSchema FullName { get; set; }
    //    string ObjectType { get; }
    //    ISpecificDbObjectStructure ToStructure();
    //}
}
