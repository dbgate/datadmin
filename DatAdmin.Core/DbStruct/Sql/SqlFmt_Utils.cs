﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DatAdmin
{
    public class ValueTypeHolder
    {
        public readonly object Value;
        public readonly DbTypeBase DbType;

        public ValueTypeHolder(object value, DbTypeBase type)
        {
            Value = value;
            DbType = type;
        }
    }

    partial class SqlDumper
    {
        protected void WriteRaw(string text) { m_stream.Write(text); }
        protected void EndCommand() { m_stream.EndCommand(); }

        protected void PutCmd(string format, params object[] args)
        {
            Put(format, args);
            EndCommand();
        }

        protected void WriteRawTest(string version, string text)
        {
            PutVersionTestBegin(version);
            WriteRaw(text);
            PutVersionTestEnd(version);
        }

        protected void PutCmdTest(string version, string format, params object[] args)
        {
            PutVersionTestBegin(version);
            Put(format, args);
            PutVersionTestEnd(version);
            EndCommand();
        }

        public virtual void PutVersionTestBegin(string version) { }
        public virtual void PutVersionTestEnd(string version) { }

        protected void Put(string format, params object[] args)
        {
            SqlDumperExtension.Put(this, format, args);
        }

        public static string Format(ISqlDialect dialect, string format, params object[] args)
        {
            return Format(dialect, SqlFormatProperties.Default, null, format, args);
        }

        private static void DataDumped(SqlFormatterState state)
        {
            if (state != null) state.WasDataOnCurrentLine = true;
        }

        private static void DumpSeparatorIfNeeded(StringBuilder sb, SqlFormatProperties props, SqlFormatterState state, SymbolPosition orig)
        {
            if (state == null) return;
            if (state.LineFeedNeeded)
            {
                DumpEoln(sb, props, state);
                state.LineFeedNeeded = false;
                state.SeparatorNeeded = false;
                state.WasDataOnCurrentLine = false;
            }
            if (state.SeparatorNeeded && state.WasDataOnCurrentLine)
            {
                if (props.IndentationLevel == SqlIndentationLevel.Original)
                {
                    if (orig != null)
                    {
                        sb.Append(orig.GetOriginalSeparator());
                    }
                    else
                    {
                        sb.Append(' ');
                    }
                }
                else
                {
                    sb.Append(' ');
                }
                state.SeparatorNeeded = false;
            }
        }

        private static void DumpEoln(StringBuilder sb, SqlFormatProperties props, SqlFormatterState state)
        {
            if (props.IndentationLevel != SqlIndentationLevel.SingleLine)
            {
                sb.Append("\n");
                if (state != null)
                {
                    for (int j = 0; j < state.IndentLevel * props.Indentation; j++) sb.Append(" ");
                }
            }
            else
            {
                sb.Append(" ");
            }
        }

        public static string Format(ISqlDialect dialect, SqlFormatProperties props, SqlFormatterState state, string format, params object[] args)
        {
            IDialectDataAdapter dda = null;
            if (state != null) dda = state.DDA;
            if (dda == null) dda = dialect.CreateDataAdapter();

            int argindex = 0;
            StringBuilder sb = new StringBuilder();
            int i = 0;
            while (i < format.Length)
            {
                char c = format[i];
                switch (c)
                {
                    case '^': // SQL keyword
                        {
                            i++;
                            SymbolPosition original = null;
                            if (format[i] == ':')
                            {
                                original = (SymbolPosition)args[argindex];
                                argindex++;
                                i++;
                            }
                            DumpSeparatorIfNeeded(sb, props, state, original);
                            while (i < format.Length && (Char.IsLetter(format, i) || format[i] == '_'))
                            {
                                sb.Append(GetCasedChar(format[i], props.SqlCommandCase));
                                i++;
                            }
                            DataDumped(state);
                        }
                        break;
                    case '&': // indentation & spacing
                        {
                            i++;
                            c = format[i];
                            i++;
                            char level = '0';
                            if (c == '1' || c == '2' || c == '3' || c == '5')
                            {
                                level = c;
                                c = format[i];
                                i++;
                            }
                            if (level != '0')
                            {
                                // indentation levels
                                if (props.IndentationLevel == SqlIndentationLevel.Original || props.IndentationLevel == SqlIndentationLevel.SingleLine)
                                {
                                    if (c == 'n' || c == 's')
                                    {
                                        if (state != null)
                                        {
                                            state.SeparatorNeeded = true;
                                        }
                                        else
                                        {
                                            sb.Append(" ");
                                        }
                                    }
                                    // when original indentation is used, don't use our separators
                                    break;
                                }
                                bool valid = (props.IndentationLevel == SqlIndentationLevel.Compact && (level == '2' || level == '5'))
                                    || (props.IndentationLevel == SqlIndentationLevel.Large && (level == '3' || level == '5'));
                                if (!valid)
                                {
                                    break; // mark is not for this indentation level
                                }
                            }
                            switch (c)
                            {
                                case '&':
                                    sb.Append("&");
                                    break;
                                case 'n':
                                    if (state == null) DumpEoln(sb, props, state);
                                    else state.LineFeedNeeded = true;
                                    break;
                                case '>':
                                    if (state != null) state.IndentLevel++;
                                    break;
                                case '<':
                                    if (state != null) state.IndentLevel--;
                                    break;
                                case 's':
                                    if (state != null) state.SeparatorNeeded = true;
                                    else sb.Append(" ");
                                    break;
                                default:
                                    throw new InternalError("DAE-00041 Unknown & formatting instruction:" + c);
                            }
                        }
                        break;
                    case '%': // format parameter
                        {
                            i++;
                            c = format[i];

                            if (c == '%')
                            {
                                sb.Append('%');
                                i++;
                            }
                            else if (c == ',') // comma separated list
                            {
                                i++;
                                c = format[i];
                                bool ok = false;
                                if (args[argindex] is IEnumerable) ok = true;
                                if (args[argindex] is IBedRecord && c == 'v') ok = true;
                                if (!ok) throw new InternalError("DAE-00042 List must be of type Enumerable");

                                bool was = false;
                                if (args[argindex] is IEnumerable)
                                {
                                    foreach (object item in (IEnumerable)args[argindex])
                                    {
                                        if (was) sb.Append(", ");
                                        WriteFormattedValue(dialect, props, sb, item, c, null, state, dda);
                                        was = true;
                                    }
                                }
                                else
                                {
                                    var rec = (IBedRecord)args[argindex];
                                    for (int x = 0; x < rec.FieldCount; x++)
                                    {
                                        if (was) sb.Append(", ");
                                        rec.ReadValue(x);
                                        sb.Append(GetSqlLiteral(props, dda, state, rec));
                                        was = true;
                                    }
                                }

                                argindex++;
                                i++;
                            }
                            else if (c == ':')
                            {
                                object orig = args[argindex];
                                argindex++;
                                i++;
                                c = format[i];
                                object arg = args[argindex];
                                argindex++;
                                i++;
                                WriteFormattedValue(dialect, props, sb, arg, c, orig, state, dda);
                            }
                            else
                            {
                                WriteFormattedValue(dialect, props, sb, args[argindex], c, null, state, dda);
                                argindex++;
                                i++;
                            }
                        }
                        break;
                    default:
                        {
                            if (Char.IsWhiteSpace(c))
                            {
                                if (state != null) state.SeparatorNeeded = false;
                            }
                            else
                            {
                                DumpSeparatorIfNeeded(sb, props, state, null);
                            }
                            sb.Append(c);
                            i++;
                        }
                        break;
                }
            }
            return sb.ToString();
        }

        protected string Format(string format, params object[] args)
        {
            return Format(Dialect, FormatProperties, FormatterState, format, args);
        }

        private static void WriteFormattedValue(ISqlDialect dialect, SqlFormatProperties props, StringBuilder sb, object val, char fmt, object orig, SqlFormatterState state, IDialectDataAdapter dda)
        {
            switch (fmt)
            {
                case 'i': // quote identifier
                    DumpSeparatorIfNeeded(sb, props, state, (SymbolPosition)orig);
                    if (val is string)
                    {
                        sb.Append(QuoteIdentifier(dialect, props, (string)val, (SymbolPosition)orig));
                    }
                    else if (val is IColumnReference)
                    {
                        sb.Append(QuoteIdentifier(dialect, props, ((IColumnReference)val).ColumnName, (SymbolPosition)orig));
                    }
                    else
                    {
                        throw new InternalError("DAE-00043 Identifier must be of type string or IColumnReference");
                    }
                    DataDumped(state);
                    break;
                case 'f': // quote full name
                    DumpSeparatorIfNeeded(sb, props, state, (SymbolPosition)orig);
                    if (val is NameWithSchema) sb.Append(QuoteFullName(dialect, props, (NameWithSchema)val));
                    else if (val is IFullNamedObject) sb.Append(QuoteFullName(dialect, props, ((IFullNamedObject)val).FullName));
                    else throw new InternalError("DAE-00044 Full name must be of type NameWithSchema or IFullNamedObject");
                    DataDumped(state);
                    break;
                case 's': // string - copy character data
                    if (val != null)
                    {
                        DumpSeparatorIfNeeded(sb, props, state, (SymbolPosition)orig);
                        sb.Append(val.ToString());
                        DataDumped(state);
                    }
                    break;
                case 'k': // keyword
                    DumpSeparatorIfNeeded(sb, props, state, (SymbolPosition)orig);
                    if (!(val is string)) throw new InternalError("DAE-00045 Identifier must be of type string");
                    if (props.SqlCommandCase == CharacterCase.Original && orig != null)
                    {
                        sb.Append(((SymbolPosition)orig).GetOriginalToken());
                    }
                    else
                    {
                        foreach (char c2 in (string)val) sb.Append(GetCasedChar(c2, props.SqlCommandCase));
                    }
                    DataDumped(state);
                    break;
                case 'K': // multi-word keyword
                    if (!(val is IEnumerable<string>)) throw new InternalError("DAE-00046 Identifier must be of type string");
                    if (orig != null)
                    {
                        foreach (var sym in ((IEnumerable<SymbolPosition>)orig))
                        {
                            DumpSeparatorIfNeeded(sb, props, state, sym);
                            sb.Append(GetCasedString(sym.GetOriginalToken(), props.SqlCommandCase));
                            if (state != null) state.SeparatorNeeded = true;
                            else sb.Append(" ");
                            DataDumped(state);
                        }
                    }
                    else
                    {
                        foreach (string s in ((IEnumerable<string>)val))
                        {
                            DumpSeparatorIfNeeded(sb, props, state, null);
                            sb.Append(GetCasedString(s, props.SqlCommandCase));
                            if (state != null) state.SeparatorNeeded = true;
                            else sb.Append(" ");
                            DataDumped(state);
                        }
                    }
                    break;
                case 'v': // value - copy character data
                    DumpSeparatorIfNeeded(sb, props, state, (SymbolPosition)orig);
                    if (props.UseOriginalValues && orig != null)
                    {
                        sb.Append(((SymbolPosition)orig).GetOriginalToken());
                    }
                    else
                    {
                        var vth = val as ValueTypeHolder;
                        if (vth != null)
                        {
                            sb.Append(GetSqlLiteralAndRead(props, dda, state, vth.Value, vth.DbType));
                        }
                        else
                        {
                            sb.Append(GetSqlLiteralAndRead(props, dda, state, val));
                        }
                    }
                    DataDumped(state);
                    break;
                case 't': // version test
                    if (val != null && !props.OmitVersionTests) sb.Append(val.ToString());
                    break;
                default:
                    throw new InternalError("DAE-00047 Unknown format character: " + fmt);
            }
        }

        public static string GetSqlLiteralAndRead(SqlFormatProperties props, IDialectDataAdapter dda, SqlFormatterState state, object val)
        {
            return GetSqlLiteralAndRead(props, dda, state, val, null);
        }

        public static string GetSqlLiteralAndRead(SqlFormatProperties props, IDialectDataAdapter dda, SqlFormatterState state, object val, DbTypeBase dsttype)
        {
            state._Holder.ReadFrom(val);
            return GetSqlLiteral(props, dda, state, state._Holder, dsttype);
        }

        public static string GetSqlLiteral(SqlFormatProperties props, IDialectDataAdapter dda, SqlFormatterState state, IBedValueReader reader)
        {
            return GetSqlLiteral(props, dda, state, reader, null);
        }

        public static string GetSqlLiteral(SqlFormatProperties props, IDialectDataAdapter dda, SqlFormatterState state, IBedValueReader reader, DbTypeBase dsttype)
        {
            if (props.BinaryStrings)
            {
                switch (reader.GetFieldType())
                {
                    case TypeStorage.String:
                        if (props.BinaryStrings)
                        {
                            return dda.GetSqlLiteral(props.RealBinaryEncoding.GetBytes(reader.GetString()), dsttype);
                        }
                        break;
                }
            }
            return dda.GetSqlLiteral(reader, dsttype);
        }

        private static bool MustBeQuoted(string s, ISqlDialect dialect)
        {
            foreach (char c in s)
            {
                bool ok = char.IsLetterOrDigit(c) || c == '_';
                if (!ok) return true;
            }
            if (dialect.PossibleKeywords.Contains(s.ToUpper())) return true;
            return false;
        }

        private static string QuoteIdentifier(ISqlDialect dialect, SqlFormatProperties props, string ident, SymbolPosition original)
        {
            switch (props.IdentifierQuoteMode)
            {
                case SqlIdentifierQuoteMode.Original:
                    if (original != null) return GetCasedString(original.GetOriginalToken(), props.IdentifierCase);
                    return dialect.QuoteIdentifier(GetCasedString(ident, props.IdentifierCase));
                    break;
                case SqlIdentifierQuoteMode.Plain:
                    if (MustBeQuoted(ident, dialect)) return dialect.QuoteIdentifier(GetCasedString(ident, props.IdentifierCase));
                    return GetCasedString(ident, props.IdentifierCase);
                    break;
                case SqlIdentifierQuoteMode.Quoted:
                    return dialect.QuoteIdentifier(GetCasedString(ident, props.IdentifierCase));
                    break;
            }
            throw new InternalError("DAE-00048 Unexpected idquote mode");
        }

        protected string QuoteIdentifier(string ident, SymbolPosition original)
        {
            return QuoteIdentifier(m_dialect, m_props, ident, original);
        }

        private static string QuoteFullName(ISqlDialect dialect, SqlFormatProperties props, NameWithSchema name)
        {
            if (props.QualifierMode == SqlQualifierMode.OmitAll || name.Schema == null)
            {
                return QuoteIdentifier(dialect, props, name.Name, null);
            }
            return QuoteIdentifier(dialect, props, name.Schema, null) + "." + QuoteIdentifier(dialect, props, name.Name, null);
        }

        protected string QuoteFullName(NameWithSchema name)
        {
            return QuoteFullName(m_dialect, m_props, name);
        }

        private static string GetCasedString(string s, CharacterCase cc)
        {
            if (s == null) return null;
            StringBuilder sb = new StringBuilder();
            foreach (char c in s) sb.Append(GetCasedChar(c, cc));
            return sb.ToString();
        }

        private static char GetCasedChar(char c, CharacterCase cc)
        {
            switch (cc)
            {
                case CharacterCase.Lower: return Char.ToLower(c);
                case CharacterCase.Upper: return Char.ToUpper(c);
            }
            return c;
        }
    }
}
