using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public class OperatorDef
    {
        public bool IsSymbol;
        public bool AcceptTokenType(TokenType type)
        {
            if (IsSymbol) return type == TokenType.Symbol;
            return type == TokenType.IdentOrKeyword || type == TokenType.Reserved;
        }
        public string[] Tokens;
    }

    public abstract class OperatorGroup
    {
        public OperatorGroup HigherPriority;

        public abstract SqlExpression ParseFunc(SqlParser parser);

        public virtual IEnumerable<OperatorDef> EnumOperators()
        {
            yield break;
        }
    }

    public abstract class OperatorListOperatorGroup : OperatorGroup
    {
        public OperatorDef[] Operators;
        public OperatorListOperatorGroup(params OperatorDef[] operators)
        {
            Operators = operators;
        }
        public OperatorListOperatorGroup(params string[] operators)
        {
            List<OperatorDef> ops = new List<OperatorDef>();
            foreach (var op in operators)
            {
                OperatorDef od = new OperatorDef();
                od.Tokens = op.Split(' ');
                od.IsSymbol = !Char.IsLetter(op[0]);
                ops.Add(od);
            }
            Operators = ops.ToArray();
        }
        public override IEnumerable<OperatorDef> EnumOperators()
        {
            return Operators;
        }
    }

    public class BinaryOperatorGroup : OperatorListOperatorGroup
    {
        public BinaryOperatorGroup(params OperatorDef[] operators)
            : base(operators)
        {
        }
        public BinaryOperatorGroup(params string[] operators)
            : base(operators)
        {
        }
        public override SqlExpression ParseFunc(SqlParser parser)
        {
            return parser.ParseBinaryExpr(this);
        }
    }

    public class UnaryPrefixOperatorGroup : OperatorListOperatorGroup
    {
        public UnaryPrefixOperatorGroup(params OperatorDef[] operators)
            : base(operators)
        {
        }
        public UnaryPrefixOperatorGroup(params string[] operators)
            : base(operators)
        {
        }

        public override SqlExpression ParseFunc(SqlParser parser)
        {
            return parser.ParseUnaryPrefixExpr(this);
        }
    }

    [Flags]
    public enum ComplexOperatorType
    {
        Case = 1,
        Between = 2
    }

    public class ComplexOperatorGroup : OperatorGroup
    {
        public ComplexOperatorType Type;
        public ComplexOperatorGroup(ComplexOperatorType type)
        {
            Type = type;
        }
        public override SqlExpression ParseFunc(SqlParser parser)
        {
            return parser.ParseComplexExpr(this);
        }
    }
}
