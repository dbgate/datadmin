using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public abstract class NameTransformationBase : INameTransformation
    {
        #region INameTransformation Members

        public virtual NameWithSchema RenameObject(NameWithSchema name, string objtype)
        {
            return name;
        }

        public virtual string RenameConstraint(IConstraint constraint)
        {
            return constraint.Name;
        }

        public virtual string RenameColumn(NameWithSchema table, string name)
        {
            return name;
        }

        #endregion
    }

    public class SetSchemaNameTransformation : NameTransformationBase
    {
        string m_schema;
        public SetSchemaNameTransformation(string schema)
        {
            m_schema = schema;
        }

        public override NameWithSchema RenameObject(NameWithSchema name, string objtype)
        {
            return new NameWithSchema(m_schema, name.Name);
        }
    }

    public class RenameSchemaTransform : NameTransformationBase
    {
        string m_oldSchema;
        string m_newSchema;

        public RenameSchemaTransform(string oldSchema, string newSchema)
        {
            m_oldSchema = oldSchema;
            m_newSchema=newSchema;
        }
        public override NameWithSchema RenameObject(NameWithSchema name, string objtype)
        {
            if (name.Schema == m_oldSchema) return new NameWithSchema(m_newSchema, name.Name);
            return name;
        }
    }

    public abstract class StringNameTransformation : NameTransformationBase
    {
        protected abstract string ConvertString(string value);

        public override NameWithSchema RenameObject(NameWithSchema name, string objtype)
        {
            return new NameWithSchema(ConvertString(name.Schema), ConvertString(name.Name));
        }
        public override string RenameColumn(NameWithSchema table, string name)
        {
            return ConvertString(name);
        }
        public override string RenameConstraint(IConstraint constraint)
        {
            return ConvertString(constraint.Name);
        }
    }

    public class UpperCaseNameTransformation : StringNameTransformation
    {
        protected override string ConvertString(string value)
        {
            if (value != null) return value.ToUpper();
            return null;
        }
    }

    public class LowerCaseNameTransformation : StringNameTransformation
    {
        protected override string ConvertString(string value)
        {
            if (value != null) return value.ToLower();
            return null;
        }
    }

    public class NormalizeConstraintNamesNameTransformation : NameTransformationBase
    {
        public override string RenameConstraint(IConstraint constraint)
        {
            return DbObjectNameTool.ConstraintName(constraint);
        }
    }
}
