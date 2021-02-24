using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public enum TokenType
    {
        None, Eof, StringSingle, StringDouble, QuotedIdent,
        SpecIdent, IdentOrKeyword, Reserved, Number, Symbol,
        LowerCaseIdent, UpperCaseIdent
    }

    public interface IStringSliceProvider
    {
        string GetStringSlice(int start, int stop);
    }

    public class SymbolPosition
    {
        public IStringSliceProvider Original;
        public int SeparatorStart;
        public int Start;
        public int Stop;
        //public int Line;
        //public int Column;

        public static SymbolPosition WholeString(string expr)
        {
            return new SymbolPosition
            {
                Original = new StringSliceProvider(expr),
                SeparatorStart = 0,
                Start = 0,
                Stop = expr.Length,
            };
        }

        public string GetOriginalToken()
        {
            return Original.GetStringSlice(Start, Stop);
        }

        public string GetOriginalSeparator()
        {
            return Original.GetStringSlice(SeparatorStart, Start);
        }
    }

    public interface ITokenizer
    {
        bool IsEof { get; }
        void NextToken();
        TokenType Current { get; }
        string CurrentData { get; }
        object CurrentSpecData { get; }
        SymbolPosition Position { get; }
        IStringSliceProvider SliceProvider { get; }
        void SetMultiSymbolOperators(IEnumerable<string> operators);

        void GoBack(int steps);
        int MarkPosition();
        void GoToMark(int mark);
    }

    public interface ISqlTokenizer : ITokenizer
    {
        void SetSqlReservedWords(IEnumerable<string> words);
    }

    public interface ISqlParser
    {
        ISqlTokenizer Tokernizer { get; }
        SqlExpression ParseExpression();
        SqlCommand ParseCommand();
        TableStructure ReadCreateTable();
        OperatorGroup[] GetOperatorTable();
        bool AllowOperators { get; set; }
        bool AllowSpecialConstantReplacement { get; set; }
    }
}
