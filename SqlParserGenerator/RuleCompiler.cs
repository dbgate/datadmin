using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace SqlParserGenerator
{
    public enum VariableTypeGroup { SubNode, SymbolPos, Bool, ModeEnum }

    public class VariableDef
    {
        public string Name;
        public string Type;
        public VariableTypeGroup TypeGroup;
    }

    public class RuleCompiler
    {
        public ChainRule Rule;
        public ParserCompiler PC;
        // dict: name => type
        public List<VariableDef> Vars = new List<VariableDef>();

        int lastVarName = 0;
        HashSetEx<string> UsedVars = new HashSetEx<string>();

        int lastTypeName = 0;
        HashSetEx<string> UsedTypes = new HashSetEx<string>();

        public RuleCompiler(ParserCompiler pc, ChainRule rule)
        {
            Rule = rule;
            PC = pc;
        }

        public RuleCollection Rules { get { return PC.Rules; } }

        public void Compile()
        {
            Rule.Chain.Compile(this);
        }

        public void GenParserMethods(CSharpWriter csw)
        {
            Rule.Chain.WriteParserMembers(csw, this);
            csw.Method(Rule.GetResultType(), Rule.ParseFuncName);
            csw.WriteLine("var args = new {0}();", Rule.ArgsClassName);
            csw.WriteLine("bool ok = {0}(args);", Rule.Chain.ParseFuncName);
            csw.Begin("if (ok)");
            csw.WriteLine("var res = new {0}();", Rule.GetResultType());
            foreach (var vd in Vars)
            {
                csw.WriteLine("res.{0} = args.{0};", vd.Name);
                if (vd.TypeGroup == VariableTypeGroup.SubNode)
                {
                    csw.WriteLine("if (res.{0} != null) res.{0}.Parent = res;", vd.Name);
                }
            }
            csw.WriteLine("return res;");
            csw.End(); // if (ok)
            csw.WriteLine("return null;");
            csw.End(); // method
        }

        public string AllocFuncName(string prefix)
        {
            return PC.AllocFuncName(prefix);
        }

        private static string AllocName(HashSetEx<string> usedNames, ref int lastIndex, string preffered, string prefix)
        {
            string name;
            if (preffered != null)
            {
                name = preffered;
                int idx = 1;
                while (usedNames.Contains(name)) name = String.Format("{0}{1}", name, idx++);
            }
            else
            {
                name = String.Format("{0}_{1}", prefix, ++lastIndex);
            }
            usedNames.Add(name);
            return name;
        }

        public string AllocVar(string preffered, string prefix, string type, VariableTypeGroup typegrp)
        {
            string name = AllocName(UsedVars, ref lastVarName, preffered, prefix);
            var vd = new VariableDef { Name = name, Type = type, TypeGroup = typegrp };
            Vars.Add(vd);
            return name;
        }

        public string AllocLocalType(string preffered, string prefix)
        {
            return AllocName(UsedTypes, ref lastTypeName, preffered, prefix);
        }

        private void GenVarMembers(CSharpWriter csw)
        {
            foreach (var vd in Vars)
            {
                csw.WriteLine("public {0} {1};", vd.Type, vd.Name);
            }
        }

        public void GenArgsClass(CSharpWriter csw)
        {
            csw.Class(Rule.ArgsClassName);
            GenVarMembers(csw);
            csw.End();
        }

        public void GenNodeClass(CSharpWriter csw)
        {
            if (Rule.ClassName != null) csw.Begin("public partial class {0}", Rule.ClassName);
            else csw.Begin("public class {0} : SqlNode", Rule.GetResultType());
            GenVarMembers(csw);
            Rule.Chain.WriteSqlNodeMembers(csw, this);
            csw.Begin("public override void GenerateSql(ISqlDumper dmp)");
            Rule.Chain.WriteGenSqlBody(csw, this);
            csw.End(); // GenerateSql
            csw.Begin("public override void EnumSymbols(ISymbolEnumerator en)");
            foreach (var vd in Vars)
            {
                switch (vd.TypeGroup)
                {
                    case VariableTypeGroup.SubNode:
                        csw.WriteLine("{0}.EnumSymbols(en);", vd.Name);
                        break;
                    case VariableTypeGroup.SymbolPos:
                        csw.WriteLine("en.EnumSymbol({0}, this);", vd.Name);
                        break;
                }
            }
            csw.End(); // EnumSymbols
            csw.End(); // class
        }
    }

    public class ParserCompiler
    {
        public RuleCollection Rules;
        int lastName = 0;

        public ParserCompiler(RuleCollection rules)
        {
            Rules = rules;
        }

        public string AllocFuncName(string prefix)
        {
            return String.Format("{0}_{1}", prefix, ++lastName);
        }
    }
}
