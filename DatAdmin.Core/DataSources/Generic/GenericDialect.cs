using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace DatAdmin
{
    [Dialect(Title = "Generic SQL", Name = "genericsql")]
    public class GenericDialect : DialectBase
    {
        DbProviderFactory m_factory;
        DbCommandBuilder m_builder;

        public static readonly ISqlDialect Instance = new GenericDialect();

        public GenericDialect()
        {
        }

        public GenericDialect(DbProviderFactory factory)
        {
            m_factory = factory;
            m_builder = factory.CreateCommandBuilder();
        }

        public override string QuoteIdentifier(string ident)
        {
            if (m_builder == null) return ident;
            try
            {
                return m_builder.QuoteIdentifier(ident);
            }
            catch (Exception)
            {
                return ident;
            }
        }

        public override SqlDialectCaps DialectCaps
        {
            get
            {
                var res = base.DialectCaps;
                res.MultiCommand = false;
                res.RangeSelect = false;
                res.LimitSelect = false;
                return res;
            }
        }

        public override DatabaseAnalyser CreateAnalyser()
        {
            return new GenericAnalyser();
        }

        public override ISqlDialect CloneDialect()
        {
            var res = new GenericDialect();
            res.m_builder = this.m_builder;
            res.m_factory = this.m_factory;
            return res;
        }

        public override string DisplayName
        {
            get { return "Generic SQL"; }
        }
    }

    [DialectAutoDetector(Name = "generic")]
    public class GenericSqlDialectDetector : DialectDetectorBase
    {
        public override ISqlDialect DetectDialect(string displayName)
        {
            if (displayName.ToLower().StartsWith("generic")) return new GenericDialect();
            return null;
        }
    }
}
