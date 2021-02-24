using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Globalization;

namespace DatAdmin
{
    //public class TunnelCommandBuilder : DbCommandBuilder
    //{
    //    ISqlDialect m_dialect;
    //    public TunnelCommandBuilder(ISqlDialect dialect)
    //    {
    //        m_dialect = dialect;
    //        QuotePrefix = new string(new char[] { dialect.QuoteIdentBegin });
    //        QuoteSuffix = new string(new char[] { dialect.QuoteIdentEnd });
    //    }

    //    protected override void ApplyParameterInfo(DbParameter parameter, System.Data.DataRow row, System.Data.StatementType statementType, bool whereClause)
    //    {
    //        parameter.DbType = (DbType)row[SchemaTableColumn.ProviderType];
    //    }

    //    protected override string GetParameterName(string parameterName)
    //    {
    //        return String.Format(CultureInfo.InvariantCulture, "@{0}", parameterName);
    //    }

    //    protected override string GetParameterName(int parameterOrdinal)
    //    {
    //        return String.Format(CultureInfo.InvariantCulture, "@param{0}", parameterOrdinal);
    //    }

    //    protected override string GetParameterPlaceholder(int parameterOrdinal)
    //    {
    //        return GetParameterName(parameterOrdinal);
    //    }

    //    protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
    //    {
    //    }

    //    protected override DataTable GetSchemaTable(DbCommand sourceCommand)
    //    {
    //        using (IDataReader reader = sourceCommand.ExecuteReader(CommandBehavior.KeyInfo | CommandBehavior.SchemaOnly))
    //        {
    //            return reader.GetSchemaTable();
    //        }
    //    }

    //    public override string QuoteIdentifier(string unquotedIdentifier)
    //    {
    //        return m_dialect.QuoteIdentifier(unquotedIdentifier);
    //    }
    //}
}
