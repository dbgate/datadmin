using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.mysql
{
    public partial class MySqlParser : SqlParser
    {
        public MySqlParser(ISqlTokenizer tokenizer, ISqlDialect dialect)
            : base(tokenizer, dialect)
        {
        }
        protected override SqlSpecialConstant GetFunctionAsConstant(string function)
        {
            switch (function.ToLower())
            {
                case "now": return SqlSpecialConstant.Current_Timestamp;
                case "current_timestamp": return SqlSpecialConstant.Current_Timestamp;
                case "utc_timestamp": return SqlSpecialConstant.Utc_Timestamp;
            }
            return SqlSpecialConstant.None;
        }

        protected override SqlSpecialConstant GetSymbolAsConstant(string data)
        {
            switch (data.ToLower())
            {
                case "current_timestamp": return SqlSpecialConstant.Current_Timestamp;
                case "utc_timestamp": return SqlSpecialConstant.Utc_Timestamp;
            }
            return SqlSpecialConstant.None;
        }

        protected override SqlCommand ParseSpecificCommand()
        {
            return ParseRuleCreateView();
        }

        //public SqlNode ParseUser()
        //{
        //    if (Current == TokenType.StringSingle)
        //    {
                
        //    }
        //    return null;
        //}
    }

    //public class MySqlUserNode : SqlNode
    //{
    //    public SymbolPosition UserPos;
    //    public SymbolPosition SepPos;
    //    public SymbolPosition HostPos;

    //    public 
    //}

    public class MySqlEqualityTestProps : SqlEqualityTestProps
    {
    }

    public partial class MySqlCreateView : SqlCreateView
    {
        public override SqlSelect SelectStatement { get { return SelectStatementNode ?? SelectStatementNode1; } }

        public bool EqualTo(MySqlCreateView v2, MySqlEqualityTestProps props)
        {
            if (!NameNode.EqualTo(v2.NameNode, props)) return false;
            if (!SelectStatement.EqualTo(v2.SelectStatement, props)) return false;
            return true;
        }
    }
}
