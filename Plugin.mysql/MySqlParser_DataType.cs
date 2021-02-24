using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.mysql
{
    partial class MySqlParser
    {
        protected override DbTypeBase ReadDataType()
        {
            return ReadMySqlDataType().ToGenericType();
        }

        private MySqlTypeNumber ReadNumberType(MySqlTypeNumber res)
        {
            if (IsSymbol("(")) ReadExprInBracket();
            ReadUnsignedZerofill(res);
            return res;
        }

        private MySqlTypeNumber ReadRealNumberType(MySqlTypeNumber res)
        {
            NextToken();
            if (SkipTokenIf("("))
            {
                string prec = SkipToken();
                string scale = "0";
                if (SkipTokenIf(","))
                {
                    scale = SkipToken();
                }
                SkipSymbol(")");
                var num = res as MySqlTypeNumericBase;
                if (num != null)
                {
                    num.Length = Int32.Parse(prec);
                    num.Decimals = Int32.Parse(scale);
                }
            }
            ReadUnsignedZerofill(res);
            return res;
        }

        private MySqlTypeBase ReadCharType(MySqlTypeCharacter res)
        {
            if (IsSymbol("("))
            {
                res.Length = Int32.Parse(ReadExprInBracket());
            }
            return res;
        }

        private MySqlTypeBase ReadTextType(MySqlTypeTextBase res)
        {
            SkipTokenIf("binary");
            return res;
        }

        private MySqlTypeBase ReadNumSet(MySqlTypeEnumSet res)
        {
            SkipSymbol("(");
            bool was = false;
            while (!IsSymbol(")"))
            {
                if (was) SkipSymbol(",");
                res.Values.Add(SkipToken());
                was = true;
            }
            SkipSymbol(")");
            return res;
        }

        protected MySqlTypeBase ReadMySqlDataType()
        {
            if (SkipTokenIf("bit"))
            {
                var res = new MySqlTypeBit();
                if (IsSymbol("(")) ReadExprInBracket();
                return res;
            }
            if (SkipTokenIf("tinyint")) return ReadNumberType(new MySqlTypeTinyInt());
            if (SkipTokenIf("smallint")) return ReadNumberType(new MySqlTypeSmallInt());
            if (SkipTokenIf("mediumint")) return ReadNumberType(new MySqlTypeMediumInt());
            if (SkipTokenIf("int")) return ReadNumberType(new MySqlTypeInt());
            if (SkipTokenIf("integer")) return ReadNumberType(new MySqlTypeInt());
            if (SkipTokenIf("bigint")) return ReadNumberType(new MySqlTypeBigInt());

            if (SkipTokenIf("real")) return ReadNumberType(new MySqlTypeDouble());
            if (SkipMultiIf("double", "precision")) return ReadNumberType(new MySqlTypeDouble());
            if (SkipTokenIf("double")) return ReadNumberType(new MySqlTypeDouble());
            if (SkipTokenIf("float")) return ReadNumberType(new MySqlTypeFloat());
            if (SkipTokenIf("decimal")) return ReadNumberType(new MySqlTypeDecimal());
            if (SkipTokenIf("numeric")) return ReadNumberType(new MySqlTypeNumeric());

            if (SkipTokenIf("date")) return new MySqlTypeDate();
            if (SkipTokenIf("time")) return new MySqlTypeTime();
            if (SkipTokenIf("timestamp")) return new MySqlTypeTimestamp();
            if (SkipTokenIf("datetime")) return new MySqlTypeDatetime();
            if (SkipTokenIf("year")) return new MySqlTypeYear();

            if (SkipTokenIf("char")) return ReadCharType(new MySqlTypeChar());
            if (SkipTokenIf("varchar")) return ReadCharType(new MySqlTypeVarChar());
            if (SkipTokenIf("binary")) return ReadCharType(new MySqlTypeBinary());
            if (SkipTokenIf("varbinary")) return ReadCharType(new MySqlTypeVarBinary());

            if (SkipTokenIf("tinyblob")) return new MySqlTypeTinyBlob();
            if (SkipTokenIf("blob")) return new MySqlTypeBlob();
            if (SkipTokenIf("mediumblob")) return new MySqlTypeMediumBlob();
            if (SkipTokenIf("longblob")) return new MySqlTypeLongBlob();

            if (SkipTokenIf("tinytext")) return ReadTextType(new MySqlTypeTinyText());
            if (SkipTokenIf("text")) return ReadTextType(new MySqlTypeText());
            if (SkipTokenIf("mediumtext")) return ReadTextType(new MySqlTypeMediumText());
            if (SkipTokenIf("longtext")) return ReadTextType(new MySqlTypeLongText());

            if (SkipTokenIf("enum")) return ReadNumSet(new MySqlTypeEnum());
            if (SkipTokenIf("set")) return ReadNumSet(new MySqlTypeSet());
            return null;
        }

        private void ReadUnsignedZerofill(MySqlTypeNumber type)
        {
            bool any = true;
            while (any)
            {
                any = false;
                if (SkipTokenIf("unsigned"))
                {
                    type.Unsigned = true;
                    any = true;
                }
                if (SkipTokenIf("zerofill"))
                {
                    type.Zerofill = true;
                    any = true;
                }
            }
        }
    }
}
