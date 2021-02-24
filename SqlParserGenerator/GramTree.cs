using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DatAdmin;

namespace SqlParserGenerator
{
    public class GramNode
    {
    }

    public abstract class RuleItem : GramNode
    {
        public string ParseFuncName;
        public string CodeId = null;

        public virtual void Compile(RuleCompiler cmp)
        {
            ParseFuncName = cmp.AllocFuncName("ParseItem");
        }
        public abstract void WriteParserMembers(CSharpWriter csw, RuleCompiler cmp);
        public abstract void WriteGenSqlBody(CSharpWriter csw, RuleCompiler cmp);
        public virtual void WriteSqlNodeMembers(CSharpWriter csw, RuleCompiler cmp) { }
        protected void WriteParseMethodHeader(CSharpWriter csw, RuleCompiler cmp)
        {
            csw.MethodEx("public", "bool", ParseFuncName, cmp.Rule.ArgsClassName + " args");
        }

        public virtual string SuggestCodeId()
        {
            return CodeId ?? "";
        }
    }

    public class TerminalRuleItem : RuleItem
    {
        public string Symbol; // SQL keyword or symbol
        public string TerminalPosName;

        //public bool IsSymbol{get{return !Char.IsLetter( Symbol[0]);}}

        public override void Compile(RuleCompiler cmp)
        {
            base.Compile(cmp);
            if (Symbol.ToCharArray().All(Char.IsLetter))
            {
                TerminalPosName = cmp.AllocVar(Symbol.Capitalize() + "Pos", "TermPos", "SymbolPosition", VariableTypeGroup.SymbolPos);
            }
            else
            {
                TerminalPosName = cmp.AllocVar(null, "TermPos", "SymbolPosition", VariableTypeGroup.SymbolPos);
            }
        }

        public override string SuggestCodeId()
        {
            if (CodeId != null) return CodeId;
            if (Symbol.ToCharArray().All(Char.IsLetter))
            {
                return Symbol.Capitalize();
            }
            return base.SuggestCodeId();
        }

        public override void WriteParserMembers(CSharpWriter csw, RuleCompiler cmp)
        {
            WriteParseMethodHeader(csw, cmp);
            csw.Begin("if (IsTerminal(\"{0}\"))", Symbol);
            csw.WriteLine("args.{0} = CurrentOriginal;", TerminalPosName);
            csw.WriteLine("NextToken();");
            csw.WriteLine("return true;");
            csw.End();
            csw.WriteLine("return false;");
            csw.End(); // method
        }

        public override void WriteGenSqlBody(CSharpWriter csw, RuleCompiler cmp)
        {
            csw.WriteLine("dmp.Put(\"&s%:k\", {0}, \"{1}\");", TerminalPosName, Symbol);
        }
    }

    public class FormatInstructionRuleItem : RuleItem
    {
        public string Format;

        public override void WriteGenSqlBody(CSharpWriter csw, RuleCompiler cmp)
        {
            csw.WriteLine("dmp.Put(\"{0}\");", Format);
        }

        public override void WriteParserMembers(CSharpWriter csw, RuleCompiler cmp)
        {
            WriteParseMethodHeader(csw, cmp);
            csw.WriteLine("return true;");
            csw.End(); // method
        }
    }

    public class CompoudRuleItem : RuleItem
    {
        public List<RuleChain> Chains = new List<RuleChain>();
        public bool Mandatory;

        public string IsVariable;
        public string CodeId;

        public string EnumTypeName;
        public string EnumVarName;
        public List<string> EnumElems = new List<string>();

        public override void Compile(RuleCompiler cmp)
        {
            base.Compile(cmp);
            if (!Mandatory)
            {
                if (CodeId != null) IsVariable = cmp.AllocVar("Is" + CodeId, null, "bool", VariableTypeGroup.Bool);
                else IsVariable = cmp.AllocVar(null, "IsItem", "bool", VariableTypeGroup.Bool);
            }
            if (CodeId != null) EnumTypeName = cmp.AllocLocalType(CodeId + "ModeType", null);
            else EnumTypeName = cmp.AllocLocalType(null, "ModeType");
            if (CodeId != null) EnumVarName = cmp.AllocVar(CodeId + "Mode", null, cmp.Rule.GetResultType() + "." + EnumTypeName, VariableTypeGroup.ModeEnum);
            else EnumVarName = cmp.AllocVar(null, "Mode", cmp.Rule.GetResultType() + "." + EnumTypeName, VariableTypeGroup.ModeEnum);
            foreach (var chain in Chains)
            {
                string sug = chain.SuggestCodeId();
                if (sug == "" || EnumElems.Contains(sug)) sug = String.Format("Element_{0}", EnumElems.Count + 1);
                EnumElems.Add(sug);
            }
            foreach (var chain in Chains) chain.Compile(cmp);
        }

        public override void WriteParserMembers(CSharpWriter csw, RuleCompiler cmp)
        {
            foreach (var chain in Chains) chain.WriteParserMembers(csw, cmp);
            WriteParseMethodHeader(csw, cmp);
            int chindex = 0;
            foreach (var chain in Chains)
            {
                csw.Begin("if ({0}(args))", chain.ParseFuncName);
                csw.WriteLine("args.{0} = {1}.{2}.{3};", EnumVarName, cmp.Rule.GetResultType(), EnumTypeName, EnumElems[chindex]);
                if (!Mandatory) csw.WriteLine("args.{0} = true;", IsVariable);
                csw.WriteLine("return true;");
                csw.End();
                chindex++;
            }
            if (Mandatory)
            {
                csw.WriteLine("return false;");
            }
            else
            {
                csw.WriteLine("args.{0} = false;", IsVariable);
                csw.WriteLine("return true;");
            }
            csw.End(); // method
        }

        public override void WriteSqlNodeMembers(CSharpWriter csw, RuleCompiler cmp)
        {
            base.WriteSqlNodeMembers(csw, cmp);
            foreach (var chain in Chains) chain.WriteSqlNodeMembers(csw, cmp);
            csw.WriteLine("public enum {0} {{ {1} }}", EnumTypeName, EnumElems.CreateDelimitedText(", "));
        }

        public override void WriteGenSqlBody(CSharpWriter csw, RuleCompiler cmp)
        {
            if (!Mandatory) csw.Begin("if ({0})", IsVariable);
            csw.Begin("switch ({0})", EnumVarName);
            for (int i = 0; i < Chains.Count; i++)
            {
                csw.WriteLine("case {0}.{1}:", EnumTypeName, EnumElems[i]);
                csw.Inc();
                Chains[i].WriteGenSqlBody(csw, cmp);
                csw.WriteLine("break;");
                csw.Dec();
            }
            csw.End(); // switch
            if (!Mandatory) csw.End(); // if ($IsVariable)
        }
    }

    public class RefRuleItem : RuleItem
    {
        public string RefName;
        public string MemberNodeName;

        public override void Compile(RuleCompiler cmp)
        {
            base.Compile(cmp);
            Rule target = cmp.Rules.FindRule(RefName);
            MemberNodeName = cmp.AllocVar(target.CodeName + "Node", "SubNode", target.GetResultType(), VariableTypeGroup.SubNode);
        }

        public override void WriteParserMembers(CSharpWriter csw, RuleCompiler cmp)
        {
            WriteParseMethodHeader(csw, cmp);
            Rule target = cmp.Rules.FindRule(RefName);
            target.GenRuleTestBody(csw, cmp, MemberNodeName);
            csw.End();
        }

        public override void WriteGenSqlBody(CSharpWriter csw, RuleCompiler cmp)
        {
            csw.WriteLine("{0}.GenerateSql(dmp);", MemberNodeName);
        }
    }

    public class RuleChain : GramNode
    {
        public List<RuleItem> Items = new List<RuleItem>();
        public string ParseFuncName;

        internal void Compile(RuleCompiler cmp)
        {
            ParseFuncName = cmp.AllocFuncName("ParseChain");
            foreach (var item in Items) item.Compile(cmp);
        }

        internal void WriteParserMembers(CSharpWriter csw, RuleCompiler cmp)
        {
            foreach (RuleItem item in Items) item.WriteParserMembers(csw, cmp);
            csw.Begin("public bool {0}({1} args)", ParseFuncName, cmp.Rule.ArgsClassName);
            csw.WriteLine("var beginMark = MarkPosition();");
            foreach (RuleItem item in Items)
            {
                csw.Begin("if (!{0}(args))", item.ParseFuncName);
                csw.WriteLine("GoToMark(beginMark);");
                csw.WriteLine("return false;");
                csw.End();
            }
            csw.WriteLine("return true;");
            csw.End();
        }

        public string SuggestCodeId()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in Items) sb.Append(item.SuggestCodeId());
            return sb.ToString();
        }

        public void WriteSqlNodeMembers(CSharpWriter csw, RuleCompiler cmp)
        {
            foreach (var item in Items) item.WriteSqlNodeMembers(csw, cmp);
        }

        public void WriteGenSqlBody(CSharpWriter csw, RuleCompiler cmp)
        {
            foreach (var item in Items) item.WriteGenSqlBody(csw, cmp);
        }
    }

    public abstract class Rule : GramNode
    {
        public string Name;
        public string CodeName { get { return Name.CapitalizeUnderscored(); } }

        public virtual void GenParserMethods(CSharpWriter csw) { }
        public abstract string GetResultType();
        public abstract void GenRuleTestBody(CSharpWriter csw, RuleCompiler cmp, string member);
        public virtual void Compile(ParserCompiler pc) { }
        public virtual void GenClassDefs(CSharpWriter csw) { }
    }

    public class ChainRule : Rule
    {
        public RuleChain Chain;
        public string ArgsClassName { get { return "Rule" + Name.CapitalizeUnderscored() + "_Arguments"; } }
        public string ParseFuncName { get { return "ParseRule" + Name.CapitalizeUnderscored(); } }

        public string ClassName;
        RuleCompiler m_compiler;

        public override string GetResultType()
        {
            if (ClassName != null) return ClassName;
            return "SqlNode" + Name.CapitalizeUnderscored();
        }

        public override void Compile(ParserCompiler pc)
        {
            m_compiler = new RuleCompiler(pc, this);
            m_compiler.Compile();
        }

        public override void GenParserMethods(CSharpWriter csw)
        {
            m_compiler.GenParserMethods(csw);
        }

        public override void GenRuleTestBody(CSharpWriter csw, RuleCompiler cmp, string member)
        {
            csw.WriteLine("{0} res = {1}();", GetResultType(), ParseFuncName);
            csw.WriteLine("if (res == null) return false;");
            csw.WriteLine("args.{0} = res;", member);
            csw.WriteLine("return true;");
        }

        public override void GenClassDefs(CSharpWriter csw)
        {
            m_compiler.GenArgsClass(csw);
            m_compiler.GenNodeClass(csw);
        }
    }

    public class CallRule : Rule
    {
        public string FuncName;
        public string TypeName;
        public List<string> CallArgs = new List<string>();

        public override string GetResultType()
        {
            return TypeName;
        }

        public override void GenRuleTestBody(CSharpWriter csw, RuleCompiler cmp, string member)
        {
            csw.WriteLine("{0} node = {1}({2});", TypeName, FuncName, CallArgs.CreateDelimitedText(", "));
            csw.WriteLine("if (node == null) return false;");
            csw.WriteLine("args.{0} = node;", member);
            csw.WriteLine("return true;");
        }
    }

    public class RuleCollection : GramNode
    {
        public List<Rule> Rules = new List<Rule>();

        public void GenCode(CSharpWriter csw, string ns, string cls)
        {
            csw.Begin("namespace {0}", ns);
            foreach (var rule in Rules)
            {
                rule.GenClassDefs(csw);
            }
            csw.Begin("public partial class {0}", cls);
            foreach (var rule in Rules)
            {
                rule.GenParserMethods(csw);
            }
            csw.End();
            csw.End();
        }

        public void Compile(ParserCompiler pc)
        {
            foreach (var rule in Rules)
            {
                rule.Compile(pc);
            }
        }

        public Rule FindRule(string name)
        {
            return (from r in Rules where r.Name == name select r).First();
        }
    }

    public class SpecNode
    {
        public string Func;
        public List<string> Args = new List<string>();
    }
}
