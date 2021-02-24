using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;
using System.Collections;

namespace DatAdmin
{
    public static class DialectExtension
    {
        // not very efective, better is to direct call DDA.GetSqlLiteral
        public static string GetSqlLiteral(this ISqlDialect dialect, object value)
        {
            var hld = new BedValueHolder();
            var dda = dialect.CreateDataAdapter();
            hld.ReadFrom(value);
            return dda.GetSqlLiteral(hld);
            //if (value == DBNull.Value || value == null) return "NULL";
            //if (value.GetType().IsNumberType()) return dialect.EscapeNumber(value);
            //if (value is byte[]) return dialect.EscapeBinary((byte[])value);
            //if (value is DateTime) return dialect.EscapeDateTime((DateTime)value);
            //if (value is bool) return dialect.EscapeLogical((bool)value);
            //return dialect.QuoteString(Convert.ToString(value, CultureInfo.InvariantCulture));
        }

        public static string QuoteFullName(this ISqlDialect dialect, NameWithSchema name)
        {
            if (name.Schema != null)
            {
                if (name.Schema.ToUpper() == "INFORMATION_SCHEMA") return String.Format("{0}.{1}", name.Schema, name.Name);
                if (dialect.DialectCaps.MultipleSchema)
                {
                    return String.Format("{0}.{1}", dialect.QuoteIdentifier(name.Schema), dialect.QuoteIdentifier(name.Name));
                }
            }
            return dialect.QuoteIdentifier(name.Name);
        }

        public static bool IsSystemTable(this ISqlDialect dialect, NameWithSchema table)
        {
            return dialect.IsSystemTable(table.Schema, table.Name);
        }

        public static bool IsSystemObject(this ISqlDialect dialect, string objtype, NameWithSchema name)
        {
            if (name == null) return false;
            return dialect.IsSystemObject(objtype, name.Schema, name.Name);
        }

        public static ISqlDumper CreateDumper(this ISqlDialect dialect, TextWriter tw)
        {
            SqlOutputStream sqlo = new SqlOutputStream(dialect, tw, SqlFormatProperties.Default);
            return dialect.CreateDumper(sqlo, SqlFormatProperties.Default);
        }

        public static ISqlDumper CreateDumper(this ISqlDialect dialect, TextWriter tw, SqlFormatProperties props)
        {
            SqlOutputStream sqlo = new SqlOutputStream(dialect, tw, props);
            return dialect.CreateDumper(sqlo, props);
        }

        public static string GenerateScript(this ISqlDialect dialect, Action<ISqlDumper> script)
        {
            return dialect.GenerateScript(script, SqlFormatProperties.Default);
        }

        public static string GenerateScript(this ISqlDialect dialect, Action<ISqlDumper> script, SqlFormatProperties props)
        {
            StringWriter sw = new StringWriter();
            ISqlDumper dmp;
            if (dialect != null)
            {
                dmp = dialect.CreateDumper(sw, props);
            }
            else
            {
                dialect = new GenericDialect();
                dmp = new InfoSqlDumper(new SqlOutputStream(dialect, sw, props), dialect, props);
            }
            script(dmp);
            return sw.ToString();
        }

        public static List<string> GetDefinedSpecificObjects(this ISqlDialect dialect)
        {
            List<string> res = new List<string>();
            foreach (var so in dialect.GetSpecificTypes()) res.Add(so.ObjectType);
            return res;
        }

        public static ISpecificType GenericTypeToSpecific(this ISqlDialect dialect, DbTypeBase type)
        {
            return dialect.GenericTypeToSpecific(type, null, null);
        }

        public static ISqlParser CreateParser(this ISqlDialect dialect, string data)
        {
            return dialect.CreateParser(dialect.CreateTokenizer(data));
        }

        public static ISqlTokenizer CreateTokenizer(this ISqlDialect dialect, string data)
        {
            return dialect.CreateTokenizer(new StringReader(data), new StringSliceProvider(data));
        }

        public static bool SameDialect(this ISqlDialect a, ISqlDialect b)
        {
            if (a == null || b == null)
            {
                return a == null && b == null;
            }
            return a.DialectName == b.DialectName;
        }

        public static void FillInfo(this ISqlDialect dialect, IDictionary data)
        {
            if (dialect == null) return;
            data["dialect_name"] = dialect.DialectName;
            data["dialect_version"] = dialect.Version.ToString();
            data["dialect"] = dialect.ToString();
            data["dialect_class"] = dialect.GetType().FullName;
        }

        public static NameWithSchema MigrateFullName(this ISqlDialect dialect, NameWithSchema name)
        {
            if (name == null) return null;
            if (dialect == null) return name;
            return new NameWithSchema(dialect.MigrateName(name.Schema), dialect.MigrateName(name.Name));
        }

        public static void EscapeString(this ISqlDialect dialect, string value, StringBuilder sb)
        {
            char esc = dialect.StringEscapeChar;
            foreach (var ch in value)
            {
                if (!dialect.SuportedUnicodeCharacter(ch)) continue;

                if (ch == '\'' || ch == esc)
                {
                    sb.Append(esc);
                    sb.Append(ch);
                }
                else
                {
                    sb.Append(ch);
                }
            }
        }

        public static string EscapeString(this ISqlDialect dialect, string value)
        {
            var sb = new StringBuilder();
            dialect.EscapeString(value, sb);
            return sb.ToString();
        }

        public static ISpecificObjectType GetSpecificObjectType(this ISqlDialect dialect, string type)
        {
            foreach (var tp in dialect.GetSpecificTypes())
            {
                if (tp.ObjectType == type) return tp;
            }
            return null;
        }

        public static string GetSqlLiteral(this IDialectDataAdapter dda, IBedValueReader reader)
        {
            return dda.GetSqlLiteral(reader, null);
        }

        public static string GetSqlLiteral(this IDialectDataAdapter dda, object value)
        {
            return dda.GetSqlLiteral(value, null);
        }

        internal static void ChangedDialectSettings()
        {
            lock (m_migrationProfileCache) m_migrationProfileCache.Clear();
        }
        static Dictionary<string, IMigrationProfile> m_migrationProfileCache = new Dictionary<string, IMigrationProfile>();
        public static IMigrationProfile GetDefaultMigrationProfile(this ISqlDialect dialect)
        {
            lock (m_migrationProfileCache)
            {
                var res = m_migrationProfileCache.Get(dialect.DialectName, null);
                if (res == null)
                {
                    res = dialect.CreateMigrationProfile();
                    m_migrationProfileCache[dialect.DialectName] = res;
                }
                return res;
            }
        }
    }

    [PluginHandler]
    public class DialectExtensionPluginHandler : PluginHandlerBase
    {
        public override void OnLoadedAllPlugins()
        {
            GlobalSettings.OnChange += DialectExtension.ChangedDialectSettings;
        }
    }
}
