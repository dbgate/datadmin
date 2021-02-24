using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace SqlParserGenerator
{
    public class GramParser : ParserBase
    {
        new GramTokenizer m_tokenizer;
        public GramParser(GramTokenizer tokenizer)
            : base(tokenizer)
        {
            m_tokenizer = tokenizer;
            m_tokenizer.SetMultiSymbolOperators(new string[] { ":=" });
        }

        private string ParseIdent()
        {
            return SkipToken(TokenType.LowerCaseIdent, "rule name expected");
        }

        public Rule ParseRule()
        {
            string name = ParseIdent();
            SkipSymbol(":=");

            SpecNode call = ParseSpecNode("call");
            if (call != null)
            {
                var res = new CallRule();
                res.Name = name;
                res.FuncName = call.Args[0];
                res.TypeName = call.Args[1];
                SpecNode args = ParseSpecNode("args");
                if (args != null) res.CallArgs = args.Args;
                SkipSymbol(";");
                return res;
            }

            var cres = new ChainRule();
            SpecNode spec_cls = ParseSpecNode("class");
            if (spec_cls != null) cres.ClassName = spec_cls.Args[0];
            cres.Name = name;
            cres.Chain = ParseRuleChain();
            SkipSymbol(";");
            return cres;
        }

        private SpecNode ParseSpecNode()
        {
            var res = new SpecNode();
            res.Func = SkipToken().Substring(1);
            if (IsSymbol("("))
            {
                bool was = false;
                NextToken();
                while (!IsSymbol(")"))
                {
                    if (was) SkipSymbol(",");
                    if (Current == TokenType.LowerCaseIdent || Current == TokenType.UpperCaseIdent || Current == TokenType.StringSingle)
                    {
                        res.Args.Add(SkipToken());
                    }
                    else
                    {
                        throw CreateParseError("DAE-00356 identifier expected");
                    }
                    was = true;
                }
                SkipSymbol(")");
            }
            return res;
        }

        public RuleCollection ParseFile()
        {
            RuleCollection res = new RuleCollection();
            while (!m_tokenizer.IsEof)
            {
                res.Rules.Add(ParseRule());
            }
            return res;
        }

        private RuleChain ParseRuleChain()
        {
            RuleChain res = new RuleChain();
            while (!IsSymbol(";") && !IsSymbol("}") && !IsSymbol("]") && !IsSymbol("|"))
            {
                res.Items.Add(ParseRuleItem());
            }
            return res;
        }

        private RuleItem ParseRuleItemCore()
        {
            if (Current == TokenType.LowerCaseIdent)
            {
                return new RefRuleItem { RefName = SkipToken() };
            }
            else if (IsSymbol("["))
            {
                NextToken();
                return ParseCompoudRuleItem("]", false);
            }
            else if (IsSymbol("{"))
            {
                NextToken();
                return ParseCompoudRuleItem("}", true);
            }
            else if (Current == TokenType.StringDouble)
            {
                return new FormatInstructionRuleItem { Format = SkipToken() };
            }
            else if (Current == TokenType.UpperCaseIdent || Current == TokenType.Symbol || Current == TokenType.StringSingle)
            {
                return new TerminalRuleItem { Symbol = SkipToken() };
            }
            else
            {
                throw this.CreateParseError("DAE-00357 rule item expected");
            }
        }

        private RuleItem ParseRuleItem()
        {
            SpecNode nodeid = ParseSpecNode("nodeid");
            RuleItem res = ParseRuleItemCore();
            if (nodeid != null) res.CodeId = nodeid.Args[0];
            return res;
        }

        private RuleItem ParseCompoudRuleItem(string endsym, bool mandatory)
        {
            CompoudRuleItem res = new CompoudRuleItem();
            var idspec = ParseSpecNode("groupid");
            if (idspec != null) res.CodeId = idspec.Args[0];
            res.Mandatory = mandatory;
            res.Chains.Add(ParseRuleChain());
            while (IsSymbol("|"))
            {
                SkipSymbol("|");
                res.Chains.Add(ParseRuleChain());
            }
            SkipSymbol(endsym);
            return res;
        }

        public SpecNode ParseSpecNode(string funcname)
        {
            if (Current == TokenType.SpecIdent && CurrentData.Substring(1) == funcname)
            {
                return ParseSpecNode();
            }
            return null;
        }
    }
}
