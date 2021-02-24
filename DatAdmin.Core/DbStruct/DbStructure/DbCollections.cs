using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace DatAdmin
{
    public abstract class GenericCollection<Key, Value> : ListProxy<Value>, IGenericCollection<Key, Value>
        where Value : IAttachableObjectStructure
    {
        IAbstractObjectStructure m_parent;

        public GenericCollection(IAbstractObjectStructure parent)
        {
            m_parent = parent;
        }

        #region IGenericCollection<Key,Value> Members

        public Value this[Key name]
        {
            get
            {
                int index = GetIndex(name);
                if (index < 0) throw new InternalError("DAE-00035 Object " + name.ToString() + " not found");
                return this[index];
            }
        }

        public virtual int GetIndex(Key name)
        {
            int index = 0;
            foreach (var item in this)
            {
                if (ExtractKey(item).Equals(name)) return index;
                index++;
            }
            index = 0;
            foreach (var item in this)
            {
                if (String.Compare(ExtractKey(item).ToString(), name.ToString(), true) == 0) return index;
                index++;
            }
            return -1;
        }

        #endregion

        public override void Add(Value value)
        {
            base.Add(value);
            ((AttachableObjectStructure)(object)value).Parent = m_parent;
        }

        public override bool Remove(Value value)
        {
            if (base.Remove(value))
            {
                ((AttachableObjectStructure)(object)value).Parent = null;
                return true;
            }
            return false;
        }

        public override void RemoveAt(int index)
        {
            var value = this[index];
            base.RemoveAt(index);
            ((AttachableObjectStructure)(object)value).Parent = null;
        }

        public override Value this[int index]
        {
            get { return base[index]; }
            set
            {
                ((AttachableObjectStructure)(object)base[index]).Parent = null;
                base[index] = value;
                ((AttachableObjectStructure)(object)value).Parent = m_parent;
            }
        }

        public override void Clear()
        {
            foreach (var item in this) ((AttachableObjectStructure)(object)item).Parent = null;
            base.Clear();
        }

        protected abstract Key ExtractKey(Value value);

        //protected abstract GenericCollection<Key, Value> ExtractCollection(IAbstractObjectStructure parent);

        public void AddAllObjects(List<IAbstractObjectStructure> res)
        {
            foreach (var obj in this) ((IAbstractObjectStructure)(object)obj).AddAllObjects(res);
        }
    }

    public class ColumnCollection : GenericCollection<string, IColumnStructure>, IColumnCollection
    {
        public ColumnCollection(ITableStructure parent) : base(parent) { }
        protected override string ExtractKey(IColumnStructure value)
        {
            return value.ColumnName;
        }
        //protected override GenericCollection<string, IColumnStructure> ExtractCollection(IAbstractObjectStructure parent)
        //{
        //    return ((TableStructure)parent).Columns;
        //}
    }

    public class ConstraintCollection : GenericCollection<string, IConstraint>, IConstraintCollection
    {
        public ConstraintCollection(ITableStructure parent) : base(parent) { }
        protected override string ExtractKey(IConstraint value)
        {
            return value.Name;
        }
        //protected override GenericCollection<string, IConstraint> ExtractCollection(IAbstractObjectStructure parent)
        //{
        //    return ((TableStructure)parent).Constraints;
        //}
    }

    //public class ForeignKeyCollection : GenericCollection<string, IForeignKey>, IForeignKeyCollection
    //{
    //    public ForeignKeyCollection(ITableStructure parent) : base(parent) { }
    //    protected override string ExtractKey(IForeignKey value)
    //    {
    //        return value.Name;
    //    }
    //    //protected override GenericCollection<string, IForeignKey> ExtractCollection(IAbstractObjectStructure parent)
    //    //{
    //    //    return ((TableStructure)parent).ReferencedFrom;
    //    //}
    //}

    public abstract class GenericCollection_NameWithSchema<Value> : GenericCollection<NameWithSchema, Value>
        where Value : IAttachableObjectStructure
    {
        public GenericCollection_NameWithSchema(IAbstractObjectStructure parent) : base(parent) { }
        public override int GetIndex(NameWithSchema name)
        {
            int res = base.GetIndex(name);
            if (res < 0 && name.Schema == null)
            {
                // ignore schema, if we are searching NULL schema
                int index = 0;
                foreach (var item in this)
                {
                    if (ExtractKey(item).Name == name.Name) return index;
                    index++;
                }
            }
            return res;
        }
    }

    public class TableCollection : GenericCollection_NameWithSchema<ITableStructure>, ITableCollection
    {
        public TableCollection(IDatabaseStructure parent) : base(parent) { }
        protected override NameWithSchema ExtractKey(ITableStructure value)
        {
            return value.FullName;
        }
    }

    public class SpecificObjectCollection : GenericCollection_NameWithSchema<ISpecificObjectStructure>, ISpecificObjectCollection
    {
        public SpecificObjectCollection(IDatabaseStructure parent) : base(parent) { }
        protected override NameWithSchema ExtractKey(ISpecificObjectStructure value)
        {
            return value.ObjectName;
        }
    }

    public class SchemaCollection : GenericCollection<string, ISchemaStructure>, ISchemaCollection
    {
        public SchemaCollection(IDatabaseStructure parent) : base(parent) { }
        protected override string ExtractKey(ISchemaStructure value)
        {
            return value.SchemaName;
        }
    }

    public class DomainCollection : GenericCollection_NameWithSchema<IDomainStructure>, IDomainCollection
    {
        public DomainCollection(IDatabaseStructure parent) : base(parent) { }
        protected override NameWithSchema ExtractKey(IDomainStructure value)
        {
            return value.FullName;
        }
    }

    public class CollationCollection : GenericCollection<string, ICollationStructure>, ICollationCollection
    {
        public CollationCollection(IDatabaseStructure parent) : base(parent) { }
        protected override string ExtractKey(ICollationStructure value)
        {
            return value.Name;
        }
    }

    public class CharacterSetCollection : GenericCollection<string, ICharacterSetStructure>, ICharacterSetCollection
    {
        public CharacterSetCollection(IDatabaseStructure parent) : base(parent) { }
        protected override string ExtractKey(ICharacterSetStructure value)
        {
            return value.Name;
        }
    }
}
