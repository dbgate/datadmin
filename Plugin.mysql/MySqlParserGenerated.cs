using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DatAdmin;
namespace Plugin.mysql
{
    
    public class RuleCreateView_Arguments
    {
        public SymbolPosition CreatePos;
        public bool IsOrReplace;
        public MySqlCreateView.OrReplaceModeType OrReplaceMode;
        public SymbolPosition OrPos;
        public SymbolPosition ReplacePos;
        public bool IsAlgorithm;
        public MySqlCreateView.AlgorithmModeType AlgorithmMode;
        public SymbolPosition AlgorithmPos;
        public SymbolPosition TermPos_1;
        public MySqlCreateView.ModeType_1 Mode_2;
        public SymbolPosition UndefinedPos;
        public SymbolPosition MergePos;
        public SymbolPosition TemptablePos;
        public bool IsDefiner;
        public MySqlCreateView.DefinerModeType DefinerMode;
        public SymbolPosition DefinerPos;
        public SymbolPosition TermPos_3;
        public MySqlCreateView.ModeType_2 Mode_4;
        public SqlNodeUser UserNode;
        public SymbolPosition TermPos_5;
        public bool IsSqlSecurity;
        public MySqlCreateView.SqlSecurityModeType SqlSecurityMode;
        public SymbolPosition SqlPos;
        public SymbolPosition SecurityPos;
        public MySqlCreateView.ModeType_3 Mode_6;
        public SymbolPosition DefinerPos1;
        public SymbolPosition InvokerPos;
        public SymbolPosition ViewPos;
        public SqlNameExpression NameNode;
        public bool IsItem_7;
        public MySqlCreateView.ModeType_4 Mode_8;
        public SqlExpression ExprsInBracketNode;
        public SymbolPosition AsPos;
        public MySqlCreateView.ModeType_5 Mode_9;
        public SymbolPosition TermPos_10;
        public SqlSelect SelectStatementNode;
        public SymbolPosition TermPos_11;
        public SqlSelect SelectStatementNode1;
        public bool IsWithCheck;
        public MySqlCreateView.WithCheckModeType WithCheckMode;
        public SymbolPosition WithPos;
        public bool IsItem_12;
        public MySqlCreateView.ModeType_6 Mode_13;
        public SymbolPosition CascadedPos;
        public SymbolPosition LocalPos;
        public SymbolPosition CheckPos;
        public SymbolPosition OptionPos;
    }
    public partial class MySqlCreateView
    {
        public SymbolPosition CreatePos;
        public bool IsOrReplace;
        public MySqlCreateView.OrReplaceModeType OrReplaceMode;
        public SymbolPosition OrPos;
        public SymbolPosition ReplacePos;
        public bool IsAlgorithm;
        public MySqlCreateView.AlgorithmModeType AlgorithmMode;
        public SymbolPosition AlgorithmPos;
        public SymbolPosition TermPos_1;
        public MySqlCreateView.ModeType_1 Mode_2;
        public SymbolPosition UndefinedPos;
        public SymbolPosition MergePos;
        public SymbolPosition TemptablePos;
        public bool IsDefiner;
        public MySqlCreateView.DefinerModeType DefinerMode;
        public SymbolPosition DefinerPos;
        public SymbolPosition TermPos_3;
        public MySqlCreateView.ModeType_2 Mode_4;
        public SqlNodeUser UserNode;
        public SymbolPosition TermPos_5;
        public bool IsSqlSecurity;
        public MySqlCreateView.SqlSecurityModeType SqlSecurityMode;
        public SymbolPosition SqlPos;
        public SymbolPosition SecurityPos;
        public MySqlCreateView.ModeType_3 Mode_6;
        public SymbolPosition DefinerPos1;
        public SymbolPosition InvokerPos;
        public SymbolPosition ViewPos;
        public SqlNameExpression NameNode;
        public bool IsItem_7;
        public MySqlCreateView.ModeType_4 Mode_8;
        public SqlExpression ExprsInBracketNode;
        public SymbolPosition AsPos;
        public MySqlCreateView.ModeType_5 Mode_9;
        public SymbolPosition TermPos_10;
        public SqlSelect SelectStatementNode;
        public SymbolPosition TermPos_11;
        public SqlSelect SelectStatementNode1;
        public bool IsWithCheck;
        public MySqlCreateView.WithCheckModeType WithCheckMode;
        public SymbolPosition WithPos;
        public bool IsItem_12;
        public MySqlCreateView.ModeType_6 Mode_13;
        public SymbolPosition CascadedPos;
        public SymbolPosition LocalPos;
        public SymbolPosition CheckPos;
        public SymbolPosition OptionPos;
        public enum OrReplaceModeType { OrReplace }
        public enum ModeType_1 { Undefined, Merge, Temptable }
        public enum AlgorithmModeType { Algorithm }
        public enum ModeType_2 { Element_1, Element_2 }
        public enum DefinerModeType { Definer }
        public enum ModeType_3 { Definer, Invoker }
        public enum SqlSecurityModeType { SqlSecurity }
        public enum ModeType_4 { Element_1 }
        public enum ModeType_5 { Element_1, Element_2 }
        public enum ModeType_6 { Cascaded, Local }
        public enum WithCheckModeType { WithCheckOption }
        public override void GenerateSql(ISqlDumper dmp)
        {
            dmp.Put("&s%:k", CreatePos, "CREATE");
            if (IsOrReplace)
            {
                switch (OrReplaceMode)
                {
                    case OrReplaceModeType.OrReplace:
                        dmp.Put("&s%:k", OrPos, "OR");
                        dmp.Put("&s%:k", ReplacePos, "REPLACE");
                        break;
                }
            }
            dmp.Put("&3n&3>");
            if (IsAlgorithm)
            {
                switch (AlgorithmMode)
                {
                    case AlgorithmModeType.Algorithm:
                        dmp.Put("&s%:k", AlgorithmPos, "ALGORITHM");
                        dmp.Put("&s%:k", TermPos_1, "=");
                        switch (Mode_2)
                        {
                            case ModeType_1.Undefined:
                                dmp.Put("&s%:k", UndefinedPos, "UNDEFINED");
                                break;
                            case ModeType_1.Merge:
                                dmp.Put("&s%:k", MergePos, "MERGE");
                                break;
                            case ModeType_1.Temptable:
                                dmp.Put("&s%:k", TemptablePos, "TEMPTABLE");
                                break;
                        }
                        break;
                }
            }
            dmp.Put("&3n");
            if (IsDefiner)
            {
                switch (DefinerMode)
                {
                    case DefinerModeType.Definer:
                        dmp.Put("&s%:k", DefinerPos, "DEFINER");
                        dmp.Put("&s%:k", TermPos_3, "=");
                        switch (Mode_4)
                        {
                            case ModeType_2.Element_1:
                                UserNode.GenerateSql(dmp);
                                break;
                            case ModeType_2.Element_2:
                                dmp.Put("&s%:k", TermPos_5, "CURRENT_USER");
                                break;
                        }
                        break;
                }
            }
            dmp.Put("&3n");
            if (IsSqlSecurity)
            {
                switch (SqlSecurityMode)
                {
                    case SqlSecurityModeType.SqlSecurity:
                        dmp.Put("&s%:k", SqlPos, "SQL");
                        dmp.Put("&s%:k", SecurityPos, "SECURITY");
                        switch (Mode_6)
                        {
                            case ModeType_3.Definer:
                                dmp.Put("&s%:k", DefinerPos1, "DEFINER");
                                break;
                            case ModeType_3.Invoker:
                                dmp.Put("&s%:k", InvokerPos, "INVOKER");
                                break;
                        }
                        break;
                }
            }
            dmp.Put("&3n&3<");
            dmp.Put("&s%:k", ViewPos, "VIEW");
            NameNode.GenerateSql(dmp);
            if (IsItem_7)
            {
                switch (Mode_8)
                {
                    case ModeType_4.Element_1:
                        ExprsInBracketNode.GenerateSql(dmp);
                        break;
                }
            }
            dmp.Put("&5n");
            dmp.Put("&s%:k", AsPos, "AS");
            dmp.Put("&5n");
            switch (Mode_9)
            {
                case ModeType_5.Element_1:
                    dmp.Put("&s%:k", TermPos_10, "(");
                    SelectStatementNode.GenerateSql(dmp);
                    dmp.Put("&s%:k", TermPos_11, ")");
                    break;
                case ModeType_5.Element_2:
                    SelectStatementNode1.GenerateSql(dmp);
                    break;
            }
            dmp.Put("&5n");
            if (IsWithCheck)
            {
                switch (WithCheckMode)
                {
                    case WithCheckModeType.WithCheckOption:
                        dmp.Put("&s%:k", WithPos, "WITH");
                        if (IsItem_12)
                        {
                            switch (Mode_13)
                            {
                                case ModeType_6.Cascaded:
                                    dmp.Put("&s%:k", CascadedPos, "CASCADED");
                                    break;
                                case ModeType_6.Local:
                                    dmp.Put("&s%:k", LocalPos, "LOCAL");
                                    break;
                            }
                        }
                        dmp.Put("&s%:k", CheckPos, "CHECK");
                        dmp.Put("&s%:k", OptionPos, "OPTION");
                        break;
                }
            }
        }
        public override void EnumSymbols(ISymbolEnumerator en)
        {
            en.EnumSymbol(CreatePos, this);
            en.EnumSymbol(OrPos, this);
            en.EnumSymbol(ReplacePos, this);
            en.EnumSymbol(AlgorithmPos, this);
            en.EnumSymbol(TermPos_1, this);
            en.EnumSymbol(UndefinedPos, this);
            en.EnumSymbol(MergePos, this);
            en.EnumSymbol(TemptablePos, this);
            en.EnumSymbol(DefinerPos, this);
            en.EnumSymbol(TermPos_3, this);
            UserNode.EnumSymbols(en);
            en.EnumSymbol(TermPos_5, this);
            en.EnumSymbol(SqlPos, this);
            en.EnumSymbol(SecurityPos, this);
            en.EnumSymbol(DefinerPos1, this);
            en.EnumSymbol(InvokerPos, this);
            en.EnumSymbol(ViewPos, this);
            NameNode.EnumSymbols(en);
            ExprsInBracketNode.EnumSymbols(en);
            en.EnumSymbol(AsPos, this);
            en.EnumSymbol(TermPos_10, this);
            SelectStatementNode.EnumSymbols(en);
            en.EnumSymbol(TermPos_11, this);
            SelectStatementNode1.EnumSymbols(en);
            en.EnumSymbol(WithPos, this);
            en.EnumSymbol(CascadedPos, this);
            en.EnumSymbol(LocalPos, this);
            en.EnumSymbol(CheckPos, this);
            en.EnumSymbol(OptionPos, this);
        }
    }
    
    public class RuleUser_Arguments
    {
        public SqlQuotedStringExpression QstringNode;
        public SymbolPosition TermPos_1;
        public SqlQuotedStringExpression QstringNode1;
    }
    public class SqlNodeUser : SqlNode
    {
        public SqlQuotedStringExpression QstringNode;
        public SymbolPosition TermPos_1;
        public SqlQuotedStringExpression QstringNode1;
        public override void GenerateSql(ISqlDumper dmp)
        {
            QstringNode.GenerateSql(dmp);
            dmp.Put("&s%:k", TermPos_1, "@");
            QstringNode1.GenerateSql(dmp);
        }
        public override void EnumSymbols(ISymbolEnumerator en)
        {
            QstringNode.EnumSymbols(en);
            en.EnumSymbol(TermPos_1, this);
            QstringNode1.EnumSymbols(en);
        }
    }
    
    public class RuleSelectPreOption_Arguments
    {
        public bool IsItem_1;
        public SqlNodeSelectPreOption.ModeType_1 Mode_2;
        public SymbolPosition AllPos;
        public SymbolPosition DistinctPos;
        public SymbolPosition DistinctrowPos;
        public bool IsItem_3;
        public SqlNodeSelectPreOption.ModeType_2 Mode_4;
        public SymbolPosition TermPos_5;
        public bool IsItem_6;
        public SqlNodeSelectPreOption.ModeType_3 Mode_7;
        public SymbolPosition TermPos_8;
        public bool IsItem_9;
        public SqlNodeSelectPreOption.ModeType_4 Mode_10;
        public SymbolPosition TermPos_11;
        public bool IsItem_12;
        public SqlNodeSelectPreOption.ModeType_5 Mode_13;
        public SymbolPosition TermPos_14;
        public bool IsItem_15;
        public SqlNodeSelectPreOption.ModeType_6 Mode_16;
        public SymbolPosition TermPos_17;
        public bool IsItem_18;
        public SqlNodeSelectPreOption.ModeType_7 Mode_19;
        public SymbolPosition TermPos_20;
        public SymbolPosition TermPos_21;
        public bool IsItem_22;
        public SqlNodeSelectPreOption.ModeType_8 Mode_23;
        public SymbolPosition TermPos_24;
    }
    public class SqlNodeSelectPreOption : SqlNode
    {
        public bool IsItem_1;
        public SqlNodeSelectPreOption.ModeType_1 Mode_2;
        public SymbolPosition AllPos;
        public SymbolPosition DistinctPos;
        public SymbolPosition DistinctrowPos;
        public bool IsItem_3;
        public SqlNodeSelectPreOption.ModeType_2 Mode_4;
        public SymbolPosition TermPos_5;
        public bool IsItem_6;
        public SqlNodeSelectPreOption.ModeType_3 Mode_7;
        public SymbolPosition TermPos_8;
        public bool IsItem_9;
        public SqlNodeSelectPreOption.ModeType_4 Mode_10;
        public SymbolPosition TermPos_11;
        public bool IsItem_12;
        public SqlNodeSelectPreOption.ModeType_5 Mode_13;
        public SymbolPosition TermPos_14;
        public bool IsItem_15;
        public SqlNodeSelectPreOption.ModeType_6 Mode_16;
        public SymbolPosition TermPos_17;
        public bool IsItem_18;
        public SqlNodeSelectPreOption.ModeType_7 Mode_19;
        public SymbolPosition TermPos_20;
        public SymbolPosition TermPos_21;
        public bool IsItem_22;
        public SqlNodeSelectPreOption.ModeType_8 Mode_23;
        public SymbolPosition TermPos_24;
        public enum ModeType_1 { All, Distinct, Distinctrow }
        public enum ModeType_2 { Element_1 }
        public enum ModeType_3 { Element_1 }
        public enum ModeType_4 { Element_1 }
        public enum ModeType_5 { Element_1 }
        public enum ModeType_6 { Element_1 }
        public enum ModeType_7 { Element_1, Element_2 }
        public enum ModeType_8 { Element_1 }
        public override void GenerateSql(ISqlDumper dmp)
        {
            if (IsItem_1)
            {
                switch (Mode_2)
                {
                    case ModeType_1.All:
                        dmp.Put("&s%:k", AllPos, "ALL");
                        break;
                    case ModeType_1.Distinct:
                        dmp.Put("&s%:k", DistinctPos, "DISTINCT");
                        break;
                    case ModeType_1.Distinctrow:
                        dmp.Put("&s%:k", DistinctrowPos, "DISTINCTROW");
                        break;
                }
            }
            if (IsItem_3)
            {
                switch (Mode_4)
                {
                    case ModeType_2.Element_1:
                        dmp.Put("&s%:k", TermPos_5, "HIGH_PRIORITY");
                        break;
                }
            }
            if (IsItem_6)
            {
                switch (Mode_7)
                {
                    case ModeType_3.Element_1:
                        dmp.Put("&s%:k", TermPos_8, "STRAIGHT_JOIN");
                        break;
                }
            }
            if (IsItem_9)
            {
                switch (Mode_10)
                {
                    case ModeType_4.Element_1:
                        dmp.Put("&s%:k", TermPos_11, "SQL_SMALL_RESULT");
                        break;
                }
            }
            if (IsItem_12)
            {
                switch (Mode_13)
                {
                    case ModeType_5.Element_1:
                        dmp.Put("&s%:k", TermPos_14, "SQL_BIG_RESULT");
                        break;
                }
            }
            if (IsItem_15)
            {
                switch (Mode_16)
                {
                    case ModeType_6.Element_1:
                        dmp.Put("&s%:k", TermPos_17, "SQL_BUFFER_RESULT");
                        break;
                }
            }
            if (IsItem_18)
            {
                switch (Mode_19)
                {
                    case ModeType_7.Element_1:
                        dmp.Put("&s%:k", TermPos_20, "SQL_CACHE");
                        break;
                    case ModeType_7.Element_2:
                        dmp.Put("&s%:k", TermPos_21, "SQL_NO_CACHE");
                        break;
                }
            }
            if (IsItem_22)
            {
                switch (Mode_23)
                {
                    case ModeType_8.Element_1:
                        dmp.Put("&s%:k", TermPos_24, "SQL_CALC_FOUND_ROWS");
                        break;
                }
            }
        }
        public override void EnumSymbols(ISymbolEnumerator en)
        {
            en.EnumSymbol(AllPos, this);
            en.EnumSymbol(DistinctPos, this);
            en.EnumSymbol(DistinctrowPos, this);
            en.EnumSymbol(TermPos_5, this);
            en.EnumSymbol(TermPos_8, this);
            en.EnumSymbol(TermPos_11, this);
            en.EnumSymbol(TermPos_14, this);
            en.EnumSymbol(TermPos_17, this);
            en.EnumSymbol(TermPos_20, this);
            en.EnumSymbol(TermPos_21, this);
            en.EnumSymbol(TermPos_24, this);
        }
    }
    public partial class MySqlParser
    {
        public bool ParseItem_2 (RuleCreateView_Arguments args)
        {
            if (IsTerminal("CREATE"))
            {
                args.CreatePos = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseItem_5 (RuleCreateView_Arguments args)
        {
            if (IsTerminal("OR"))
            {
                args.OrPos = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseItem_6 (RuleCreateView_Arguments args)
        {
            if (IsTerminal("REPLACE"))
            {
                args.ReplacePos = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseChain_4(RuleCreateView_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_5(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_6(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_3 (RuleCreateView_Arguments args)
        {
            if (ParseChain_4(args))
            {
                args.OrReplaceMode = MySqlCreateView.OrReplaceModeType.OrReplace;
                args.IsOrReplace = true;
                return true;
            }
            args.IsOrReplace = false;
            return true;
        }
        public bool ParseItem_7 (RuleCreateView_Arguments args)
        {
            return true;
        }
        public bool ParseItem_10 (RuleCreateView_Arguments args)
        {
            if (IsTerminal("ALGORITHM"))
            {
                args.AlgorithmPos = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseItem_11 (RuleCreateView_Arguments args)
        {
            if (IsTerminal("="))
            {
                args.TermPos_1 = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseItem_14 (RuleCreateView_Arguments args)
        {
            if (IsTerminal("UNDEFINED"))
            {
                args.UndefinedPos = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseChain_13(RuleCreateView_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_14(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_16 (RuleCreateView_Arguments args)
        {
            if (IsTerminal("MERGE"))
            {
                args.MergePos = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseChain_15(RuleCreateView_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_16(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_18 (RuleCreateView_Arguments args)
        {
            if (IsTerminal("TEMPTABLE"))
            {
                args.TemptablePos = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseChain_17(RuleCreateView_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_18(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_12 (RuleCreateView_Arguments args)
        {
            if (ParseChain_13(args))
            {
                args.Mode_2 = MySqlCreateView.ModeType_1.Undefined;
                return true;
            }
            if (ParseChain_15(args))
            {
                args.Mode_2 = MySqlCreateView.ModeType_1.Merge;
                return true;
            }
            if (ParseChain_17(args))
            {
                args.Mode_2 = MySqlCreateView.ModeType_1.Temptable;
                return true;
            }
            return false;
        }
        public bool ParseChain_9(RuleCreateView_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_10(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_11(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_12(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_8 (RuleCreateView_Arguments args)
        {
            if (ParseChain_9(args))
            {
                args.AlgorithmMode = MySqlCreateView.AlgorithmModeType.Algorithm;
                args.IsAlgorithm = true;
                return true;
            }
            args.IsAlgorithm = false;
            return true;
        }
        public bool ParseItem_19 (RuleCreateView_Arguments args)
        {
            return true;
        }
        public bool ParseItem_22 (RuleCreateView_Arguments args)
        {
            if (IsTerminal("DEFINER"))
            {
                args.DefinerPos = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseItem_23 (RuleCreateView_Arguments args)
        {
            if (IsTerminal("="))
            {
                args.TermPos_3 = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseItem_26 (RuleCreateView_Arguments args)
        {
            SqlNodeUser res = ParseRuleUser();
            if (res == null) return false;
            args.UserNode = res;
            return true;
        }
        public bool ParseChain_25(RuleCreateView_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_26(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_28 (RuleCreateView_Arguments args)
        {
            if (IsTerminal("CURRENT_USER"))
            {
                args.TermPos_5 = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseChain_27(RuleCreateView_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_28(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_24 (RuleCreateView_Arguments args)
        {
            if (ParseChain_25(args))
            {
                args.Mode_4 = MySqlCreateView.ModeType_2.Element_1;
                return true;
            }
            if (ParseChain_27(args))
            {
                args.Mode_4 = MySqlCreateView.ModeType_2.Element_2;
                return true;
            }
            return false;
        }
        public bool ParseChain_21(RuleCreateView_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_22(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_23(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_24(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_20 (RuleCreateView_Arguments args)
        {
            if (ParseChain_21(args))
            {
                args.DefinerMode = MySqlCreateView.DefinerModeType.Definer;
                args.IsDefiner = true;
                return true;
            }
            args.IsDefiner = false;
            return true;
        }
        public bool ParseItem_29 (RuleCreateView_Arguments args)
        {
            return true;
        }
        public bool ParseItem_32 (RuleCreateView_Arguments args)
        {
            if (IsTerminal("SQL"))
            {
                args.SqlPos = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseItem_33 (RuleCreateView_Arguments args)
        {
            if (IsTerminal("SECURITY"))
            {
                args.SecurityPos = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseItem_36 (RuleCreateView_Arguments args)
        {
            if (IsTerminal("DEFINER"))
            {
                args.DefinerPos1 = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseChain_35(RuleCreateView_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_36(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_38 (RuleCreateView_Arguments args)
        {
            if (IsTerminal("INVOKER"))
            {
                args.InvokerPos = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseChain_37(RuleCreateView_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_38(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_34 (RuleCreateView_Arguments args)
        {
            if (ParseChain_35(args))
            {
                args.Mode_6 = MySqlCreateView.ModeType_3.Definer;
                return true;
            }
            if (ParseChain_37(args))
            {
                args.Mode_6 = MySqlCreateView.ModeType_3.Invoker;
                return true;
            }
            return false;
        }
        public bool ParseChain_31(RuleCreateView_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_32(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_33(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_34(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_30 (RuleCreateView_Arguments args)
        {
            if (ParseChain_31(args))
            {
                args.SqlSecurityMode = MySqlCreateView.SqlSecurityModeType.SqlSecurity;
                args.IsSqlSecurity = true;
                return true;
            }
            args.IsSqlSecurity = false;
            return true;
        }
        public bool ParseItem_39 (RuleCreateView_Arguments args)
        {
            return true;
        }
        public bool ParseItem_40 (RuleCreateView_Arguments args)
        {
            if (IsTerminal("VIEW"))
            {
                args.ViewPos = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseItem_41 (RuleCreateView_Arguments args)
        {
            SqlNameExpression node = ParseName();
            if (node == null) return false;
            args.NameNode = node;
            return true;
        }
        public bool ParseItem_44 (RuleCreateView_Arguments args)
        {
            SqlExpression node = ParseExprInBracket();
            if (node == null) return false;
            args.ExprsInBracketNode = node;
            return true;
        }
        public bool ParseChain_43(RuleCreateView_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_44(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_42 (RuleCreateView_Arguments args)
        {
            if (ParseChain_43(args))
            {
                args.Mode_8 = MySqlCreateView.ModeType_4.Element_1;
                args.IsItem_7 = true;
                return true;
            }
            args.IsItem_7 = false;
            return true;
        }
        public bool ParseItem_45 (RuleCreateView_Arguments args)
        {
            return true;
        }
        public bool ParseItem_46 (RuleCreateView_Arguments args)
        {
            if (IsTerminal("AS"))
            {
                args.AsPos = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseItem_47 (RuleCreateView_Arguments args)
        {
            return true;
        }
        public bool ParseItem_50 (RuleCreateView_Arguments args)
        {
            if (IsTerminal("("))
            {
                args.TermPos_10 = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseItem_51 (RuleCreateView_Arguments args)
        {
            SqlSelect node = ParseSelect();
            if (node == null) return false;
            args.SelectStatementNode = node;
            return true;
        }
        public bool ParseItem_52 (RuleCreateView_Arguments args)
        {
            if (IsTerminal(")"))
            {
                args.TermPos_11 = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseChain_49(RuleCreateView_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_50(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_51(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_52(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_54 (RuleCreateView_Arguments args)
        {
            SqlSelect node = ParseSelect();
            if (node == null) return false;
            args.SelectStatementNode1 = node;
            return true;
        }
        public bool ParseChain_53(RuleCreateView_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_54(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_48 (RuleCreateView_Arguments args)
        {
            if (ParseChain_49(args))
            {
                args.Mode_9 = MySqlCreateView.ModeType_5.Element_1;
                return true;
            }
            if (ParseChain_53(args))
            {
                args.Mode_9 = MySqlCreateView.ModeType_5.Element_2;
                return true;
            }
            return false;
        }
        public bool ParseItem_55 (RuleCreateView_Arguments args)
        {
            return true;
        }
        public bool ParseItem_58 (RuleCreateView_Arguments args)
        {
            if (IsTerminal("WITH"))
            {
                args.WithPos = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseItem_61 (RuleCreateView_Arguments args)
        {
            if (IsTerminal("CASCADED"))
            {
                args.CascadedPos = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseChain_60(RuleCreateView_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_61(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_63 (RuleCreateView_Arguments args)
        {
            if (IsTerminal("LOCAL"))
            {
                args.LocalPos = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseChain_62(RuleCreateView_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_63(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_59 (RuleCreateView_Arguments args)
        {
            if (ParseChain_60(args))
            {
                args.Mode_13 = MySqlCreateView.ModeType_6.Cascaded;
                args.IsItem_12 = true;
                return true;
            }
            if (ParseChain_62(args))
            {
                args.Mode_13 = MySqlCreateView.ModeType_6.Local;
                args.IsItem_12 = true;
                return true;
            }
            args.IsItem_12 = false;
            return true;
        }
        public bool ParseItem_64 (RuleCreateView_Arguments args)
        {
            if (IsTerminal("CHECK"))
            {
                args.CheckPos = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseItem_65 (RuleCreateView_Arguments args)
        {
            if (IsTerminal("OPTION"))
            {
                args.OptionPos = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseChain_57(RuleCreateView_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_58(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_59(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_64(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_65(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_56 (RuleCreateView_Arguments args)
        {
            if (ParseChain_57(args))
            {
                args.WithCheckMode = MySqlCreateView.WithCheckModeType.WithCheckOption;
                args.IsWithCheck = true;
                return true;
            }
            args.IsWithCheck = false;
            return true;
        }
        public bool ParseChain_1(RuleCreateView_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_2(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_3(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_7(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_8(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_19(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_20(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_29(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_30(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_39(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_40(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_41(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_42(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_45(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_46(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_47(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_48(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_55(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_56(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public MySqlCreateView ParseRuleCreateView ()
        {
            var args = new RuleCreateView_Arguments();
            bool ok = ParseChain_1(args);
            if (ok)
            {
                var res = new MySqlCreateView();
                res.CreatePos = args.CreatePos;
                res.IsOrReplace = args.IsOrReplace;
                res.OrReplaceMode = args.OrReplaceMode;
                res.OrPos = args.OrPos;
                res.ReplacePos = args.ReplacePos;
                res.IsAlgorithm = args.IsAlgorithm;
                res.AlgorithmMode = args.AlgorithmMode;
                res.AlgorithmPos = args.AlgorithmPos;
                res.TermPos_1 = args.TermPos_1;
                res.Mode_2 = args.Mode_2;
                res.UndefinedPos = args.UndefinedPos;
                res.MergePos = args.MergePos;
                res.TemptablePos = args.TemptablePos;
                res.IsDefiner = args.IsDefiner;
                res.DefinerMode = args.DefinerMode;
                res.DefinerPos = args.DefinerPos;
                res.TermPos_3 = args.TermPos_3;
                res.Mode_4 = args.Mode_4;
                res.UserNode = args.UserNode;
                if (res.UserNode != null) res.UserNode.Parent = res;
                res.TermPos_5 = args.TermPos_5;
                res.IsSqlSecurity = args.IsSqlSecurity;
                res.SqlSecurityMode = args.SqlSecurityMode;
                res.SqlPos = args.SqlPos;
                res.SecurityPos = args.SecurityPos;
                res.Mode_6 = args.Mode_6;
                res.DefinerPos1 = args.DefinerPos1;
                res.InvokerPos = args.InvokerPos;
                res.ViewPos = args.ViewPos;
                res.NameNode = args.NameNode;
                if (res.NameNode != null) res.NameNode.Parent = res;
                res.IsItem_7 = args.IsItem_7;
                res.Mode_8 = args.Mode_8;
                res.ExprsInBracketNode = args.ExprsInBracketNode;
                if (res.ExprsInBracketNode != null) res.ExprsInBracketNode.Parent = res;
                res.AsPos = args.AsPos;
                res.Mode_9 = args.Mode_9;
                res.TermPos_10 = args.TermPos_10;
                res.SelectStatementNode = args.SelectStatementNode;
                if (res.SelectStatementNode != null) res.SelectStatementNode.Parent = res;
                res.TermPos_11 = args.TermPos_11;
                res.SelectStatementNode1 = args.SelectStatementNode1;
                if (res.SelectStatementNode1 != null) res.SelectStatementNode1.Parent = res;
                res.IsWithCheck = args.IsWithCheck;
                res.WithCheckMode = args.WithCheckMode;
                res.WithPos = args.WithPos;
                res.IsItem_12 = args.IsItem_12;
                res.Mode_13 = args.Mode_13;
                res.CascadedPos = args.CascadedPos;
                res.LocalPos = args.LocalPos;
                res.CheckPos = args.CheckPos;
                res.OptionPos = args.OptionPos;
                return res;
            }
            return null;
        }
        public bool ParseItem_67 (RuleUser_Arguments args)
        {
            SqlQuotedStringExpression node = ParseQuotedString(TokenType.QuotedIdent, TokenType.StringSingle, TokenType.StringDouble);
            if (node == null) return false;
            args.QstringNode = node;
            return true;
        }
        public bool ParseItem_68 (RuleUser_Arguments args)
        {
            if (IsTerminal("@"))
            {
                args.TermPos_1 = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseItem_69 (RuleUser_Arguments args)
        {
            SqlQuotedStringExpression node = ParseQuotedString(TokenType.QuotedIdent, TokenType.StringSingle, TokenType.StringDouble);
            if (node == null) return false;
            args.QstringNode1 = node;
            return true;
        }
        public bool ParseChain_66(RuleUser_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_67(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_68(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_69(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public SqlNodeUser ParseRuleUser ()
        {
            var args = new RuleUser_Arguments();
            bool ok = ParseChain_66(args);
            if (ok)
            {
                var res = new SqlNodeUser();
                res.QstringNode = args.QstringNode;
                if (res.QstringNode != null) res.QstringNode.Parent = res;
                res.TermPos_1 = args.TermPos_1;
                res.QstringNode1 = args.QstringNode1;
                if (res.QstringNode1 != null) res.QstringNode1.Parent = res;
                return res;
            }
            return null;
        }
        public bool ParseItem_73 (RuleSelectPreOption_Arguments args)
        {
            if (IsTerminal("ALL"))
            {
                args.AllPos = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseChain_72(RuleSelectPreOption_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_73(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_75 (RuleSelectPreOption_Arguments args)
        {
            if (IsTerminal("DISTINCT"))
            {
                args.DistinctPos = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseChain_74(RuleSelectPreOption_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_75(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_77 (RuleSelectPreOption_Arguments args)
        {
            if (IsTerminal("DISTINCTROW"))
            {
                args.DistinctrowPos = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseChain_76(RuleSelectPreOption_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_77(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_71 (RuleSelectPreOption_Arguments args)
        {
            if (ParseChain_72(args))
            {
                args.Mode_2 = SqlNodeSelectPreOption.ModeType_1.All;
                args.IsItem_1 = true;
                return true;
            }
            if (ParseChain_74(args))
            {
                args.Mode_2 = SqlNodeSelectPreOption.ModeType_1.Distinct;
                args.IsItem_1 = true;
                return true;
            }
            if (ParseChain_76(args))
            {
                args.Mode_2 = SqlNodeSelectPreOption.ModeType_1.Distinctrow;
                args.IsItem_1 = true;
                return true;
            }
            args.IsItem_1 = false;
            return true;
        }
        public bool ParseItem_80 (RuleSelectPreOption_Arguments args)
        {
            if (IsTerminal("HIGH_PRIORITY"))
            {
                args.TermPos_5 = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseChain_79(RuleSelectPreOption_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_80(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_78 (RuleSelectPreOption_Arguments args)
        {
            if (ParseChain_79(args))
            {
                args.Mode_4 = SqlNodeSelectPreOption.ModeType_2.Element_1;
                args.IsItem_3 = true;
                return true;
            }
            args.IsItem_3 = false;
            return true;
        }
        public bool ParseItem_83 (RuleSelectPreOption_Arguments args)
        {
            if (IsTerminal("STRAIGHT_JOIN"))
            {
                args.TermPos_8 = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseChain_82(RuleSelectPreOption_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_83(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_81 (RuleSelectPreOption_Arguments args)
        {
            if (ParseChain_82(args))
            {
                args.Mode_7 = SqlNodeSelectPreOption.ModeType_3.Element_1;
                args.IsItem_6 = true;
                return true;
            }
            args.IsItem_6 = false;
            return true;
        }
        public bool ParseItem_86 (RuleSelectPreOption_Arguments args)
        {
            if (IsTerminal("SQL_SMALL_RESULT"))
            {
                args.TermPos_11 = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseChain_85(RuleSelectPreOption_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_86(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_84 (RuleSelectPreOption_Arguments args)
        {
            if (ParseChain_85(args))
            {
                args.Mode_10 = SqlNodeSelectPreOption.ModeType_4.Element_1;
                args.IsItem_9 = true;
                return true;
            }
            args.IsItem_9 = false;
            return true;
        }
        public bool ParseItem_89 (RuleSelectPreOption_Arguments args)
        {
            if (IsTerminal("SQL_BIG_RESULT"))
            {
                args.TermPos_14 = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseChain_88(RuleSelectPreOption_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_89(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_87 (RuleSelectPreOption_Arguments args)
        {
            if (ParseChain_88(args))
            {
                args.Mode_13 = SqlNodeSelectPreOption.ModeType_5.Element_1;
                args.IsItem_12 = true;
                return true;
            }
            args.IsItem_12 = false;
            return true;
        }
        public bool ParseItem_92 (RuleSelectPreOption_Arguments args)
        {
            if (IsTerminal("SQL_BUFFER_RESULT"))
            {
                args.TermPos_17 = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseChain_91(RuleSelectPreOption_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_92(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_90 (RuleSelectPreOption_Arguments args)
        {
            if (ParseChain_91(args))
            {
                args.Mode_16 = SqlNodeSelectPreOption.ModeType_6.Element_1;
                args.IsItem_15 = true;
                return true;
            }
            args.IsItem_15 = false;
            return true;
        }
        public bool ParseItem_95 (RuleSelectPreOption_Arguments args)
        {
            if (IsTerminal("SQL_CACHE"))
            {
                args.TermPos_20 = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseChain_94(RuleSelectPreOption_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_95(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_97 (RuleSelectPreOption_Arguments args)
        {
            if (IsTerminal("SQL_NO_CACHE"))
            {
                args.TermPos_21 = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseChain_96(RuleSelectPreOption_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_97(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_93 (RuleSelectPreOption_Arguments args)
        {
            if (ParseChain_94(args))
            {
                args.Mode_19 = SqlNodeSelectPreOption.ModeType_7.Element_1;
                args.IsItem_18 = true;
                return true;
            }
            if (ParseChain_96(args))
            {
                args.Mode_19 = SqlNodeSelectPreOption.ModeType_7.Element_2;
                args.IsItem_18 = true;
                return true;
            }
            args.IsItem_18 = false;
            return true;
        }
        public bool ParseItem_100 (RuleSelectPreOption_Arguments args)
        {
            if (IsTerminal("SQL_CALC_FOUND_ROWS"))
            {
                args.TermPos_24 = CurrentOriginal;
                NextToken();
                return true;
            }
            return false;
        }
        public bool ParseChain_99(RuleSelectPreOption_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_100(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public bool ParseItem_98 (RuleSelectPreOption_Arguments args)
        {
            if (ParseChain_99(args))
            {
                args.Mode_23 = SqlNodeSelectPreOption.ModeType_8.Element_1;
                args.IsItem_22 = true;
                return true;
            }
            args.IsItem_22 = false;
            return true;
        }
        public bool ParseChain_70(RuleSelectPreOption_Arguments args)
        {
            var beginMark = MarkPosition();
            if (!ParseItem_71(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_78(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_81(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_84(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_87(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_90(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_93(args))
            {
                GoToMark(beginMark);
                return false;
            }
            if (!ParseItem_98(args))
            {
                GoToMark(beginMark);
                return false;
            }
            return true;
        }
        public SqlNodeSelectPreOption ParseRuleSelectPreOption ()
        {
            var args = new RuleSelectPreOption_Arguments();
            bool ok = ParseChain_70(args);
            if (ok)
            {
                var res = new SqlNodeSelectPreOption();
                res.IsItem_1 = args.IsItem_1;
                res.Mode_2 = args.Mode_2;
                res.AllPos = args.AllPos;
                res.DistinctPos = args.DistinctPos;
                res.DistinctrowPos = args.DistinctrowPos;
                res.IsItem_3 = args.IsItem_3;
                res.Mode_4 = args.Mode_4;
                res.TermPos_5 = args.TermPos_5;
                res.IsItem_6 = args.IsItem_6;
                res.Mode_7 = args.Mode_7;
                res.TermPos_8 = args.TermPos_8;
                res.IsItem_9 = args.IsItem_9;
                res.Mode_10 = args.Mode_10;
                res.TermPos_11 = args.TermPos_11;
                res.IsItem_12 = args.IsItem_12;
                res.Mode_13 = args.Mode_13;
                res.TermPos_14 = args.TermPos_14;
                res.IsItem_15 = args.IsItem_15;
                res.Mode_16 = args.Mode_16;
                res.TermPos_17 = args.TermPos_17;
                res.IsItem_18 = args.IsItem_18;
                res.Mode_19 = args.Mode_19;
                res.TermPos_20 = args.TermPos_20;
                res.TermPos_21 = args.TermPos_21;
                res.IsItem_22 = args.IsItem_22;
                res.Mode_23 = args.Mode_23;
                res.TermPos_24 = args.TermPos_24;
                return res;
            }
            return null;
        }
    }
}
