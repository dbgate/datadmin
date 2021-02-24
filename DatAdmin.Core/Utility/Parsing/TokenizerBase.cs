using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    public abstract class TokenizerBase : ITokenizer
    {
        protected TextReader m_reader;
        protected int m_currentChar;
        protected int m_nextChar;
        // number of characters read from input stream
        protected int m_position = 0;
        protected int m_currentSeparatorStart;
        protected int m_currentStart;
        protected int m_currentStop;
        protected bool m_isStarted;
        TokenType m_current;
        protected string m_currentData;
        protected object m_currentSpecData;
        protected List<string> m_multiSymbolOperators = new List<string>();
        protected HashSetEx<char> m_symbols = new HashSetEx<char>();
        protected char m_stringEscape = '\\';
        public const int HISTORY_SIZE = 5; // LR(HISTORY_SIZE) parser is created
        protected HashSetEx<string> m_reservedWords = new HashSetEx<string>();
        protected char m_quoteIdentBegin = '\0';
        protected char m_quoteIdentEnd = '\0';
        protected bool m_checkIdentCase = false;

        protected char m_specIdentStart = '\0';

        protected bool m_supportsStringSingle = false;
        protected bool m_supportsStringDouble = false;

        protected class HistoryItem
        {
            internal int Start, Stop, SeparatorStart;
            internal TokenType Token;
            internal string Data;
            internal object SpecData;
        }

        protected List<HistoryItem> m_history = new List<HistoryItem>();
        protected int m_historyPosition = 0;

        public TokenizerBase(TextReader reader, IStringSliceProvider sliceProvider)
        {
            m_reader = reader;
            Current = TokenType.None;
            SliceProvider = sliceProvider;
            m_currentChar = m_reader.Read(); m_position++;
            m_nextChar = m_reader.Read(); m_position++;
            m_symbols = GetSymbols();
        }

        protected virtual HashSetEx<char> GetSymbols() { return new HashSetEx<char>('*', '(', ')', ',', '=', '.'); }
        protected char CurrentCh { get { return (char)m_currentChar; } }
        protected char NextCh { get { return (char)m_nextChar; } }
        protected int CurrentChPos
        {
            get
            {
                if (m_currentChar < 0)
                {
                    return m_position - 1;
                }
                return m_position - 2;
            }
        }

        public IStringSliceProvider SliceProvider { get; private set; }

        protected void GoToNextChar()
        {
            if (m_currentChar < 0) throw this.CreateParseError("DAE-00295 Unexpected EOF");
            m_currentChar = m_nextChar;
            m_nextChar = m_reader.Read(); m_position++;
        }

        private bool CharEof { get { return m_currentChar < 0; } }

        private void SkipSpaces()
        {
            while (!CharEof && Char.IsWhiteSpace(CurrentCh)) GoToNextChar();
        }

        public TokenType Current
        {
            get
            {
                WantStarted();
                return m_current;
            }
            protected set
            {
                m_current = value;
            }
        }
        public string CurrentData
        {
            get
            {
                WantStarted();
                return m_currentData;
            }
            protected set
            {
                m_currentData = value;
            }
        }
        public object CurrentSpecData
        {
            get
            {
                WantStarted();
                return m_currentSpecData;
            }
            protected set
            {
                m_currentSpecData = value;
            }
        }
        //public char CurrentSymbol { get; private set; }

        public SymbolPosition Position
        {
            get
            {
                WantStarted();
                return new SymbolPosition
                {
                    SeparatorStart = m_currentSeparatorStart,
                    Start = m_currentStart,
                    Stop = m_currentStop,
                    Original = SliceProvider,
                };
            }
        }

        private void WantStarted()
        {
            if (m_isStarted) return;
            m_isStarted = true;
            NextToken();
        }

        protected void ParseString(TokenType token, char edge)
        {
            StringBuilder sb = new StringBuilder();
            GoToNextChar();
            for (; ; )
            {
                // zdvojeni uvozovek u retezcu
                if (m_stringEscape == edge)
                {
                    if (CurrentCh == edge && NextCh == edge)
                    {
                        GoToNextChar();
                        GoToNextChar();
                        sb.Append(edge);
                    }
                    else if (CurrentCh == edge)
                    {
                        // konec retezce
                        GoToNextChar();
                        break;
                    }
                    else
                    {
                        sb.Append(CurrentCh);
                        GoToNextChar();
                    }
                }
                else
                {
                    if (CurrentCh == m_stringEscape)
                    {
                        GoToNextChar();
                        sb.Append(CurrentCh);
                        GoToNextChar();
                    }
                    else if (CurrentCh == edge)
                    {
                        // konec retezce
                        GoToNextChar();
                        break;
                    }
                    else
                    {
                        sb.Append(CurrentCh);
                        GoToNextChar();
                    }
                }
            }
            CurrentData = sb.ToString();
            Current = token;
        }

        protected void ParseQuotedIdent()
        {
            StringBuilder sb = new StringBuilder();
            GoToNextChar();
            while (CurrentCh != m_quoteIdentEnd)
            {
                sb.Append(CurrentCh);
                GoToNextChar();
            }
            GoToNextChar();
            CurrentData = sb.ToString();
            Current = TokenType.QuotedIdent;
        }

        protected void ParseIdentOrKeyword(string prefix, TokenType token, bool testReserved)
        {
            StringBuilder sb = new StringBuilder(prefix);
            while (Char.IsLetterOrDigit(CurrentCh) || CurrentCh == '_')
            {
                sb.Append(CurrentCh);
                GoToNextChar();
            }
            CurrentData = sb.ToString();
            if (testReserved && m_reservedWords.Contains(CurrentData.ToUpper())) Current = TokenType.Reserved;
            else Current = token;
        }

        protected void ParseNumber()
        {
            StringBuilder sb = new StringBuilder();
            while (Char.IsDigit(CurrentCh))
            {
                sb.Append(CurrentCh);
                GoToNextChar();
            }
            CurrentData = sb.ToString();
            Current = TokenType.Number;
        }

        public bool IsEof
        {
            get
            {
                WantStarted();
                return Current == TokenType.Eof;
            }
        }

        public void NextToken()
        {
            if (m_historyPosition < m_history.Count)
            {
                // get token from history
                var it = m_history[m_historyPosition];
                Current = it.Token;
                CurrentData = it.Data;
                CurrentSpecData = it.SpecData;
                m_currentStart = it.Start;
                m_currentStop = it.Stop;
                m_currentSeparatorStart = it.SeparatorStart;
                m_historyPosition++;
                return;
            }
            if (Current != TokenType.None)
            {
                // add current token to history
                var it = new HistoryItem();
                it.Token = Current;
                it.Data = CurrentData;
                it.SpecData = CurrentSpecData;
                it.Start = m_currentStart;
                it.Stop = m_currentStop;
                it.SeparatorStart = m_currentSeparatorStart;
                m_history.Add(it);
                m_historyPosition++;
                //if (m_history.Count > HISTORY_SIZE)
                //{
                //    // roll history, if it is too big
                //    m_history.RemoveAt(0);
                //    m_historyPosition--;
                //}
            }
            m_currentSeparatorStart = CurrentChPos;
            m_currentData = null;
            m_currentSpecData = null;
            SkipSpaces();
            m_currentStart = CurrentChPos;
            if (CharEof)
            {
                Current = TokenType.Eof;
                return;
            }
            if (ParseSpecialToken())
            {
                m_currentStop = CurrentChPos;
                return;
            }
            if (CurrentCh == '\'' && m_supportsStringSingle)
            {
                ParseString(TokenType.StringSingle, '\'');
                m_currentStop = CurrentChPos;
                return;
            }
            if (CurrentCh == '\"' && m_supportsStringDouble)
            {
                ParseString(TokenType.StringDouble, '\"');
                m_currentStop = CurrentChPos;
                return;
            }
            if (CurrentCh == m_quoteIdentBegin)
            {
                ParseQuotedIdent();
                m_currentStop = CurrentChPos;
                return;
            }
            if (Char.IsLetter(CurrentCh) || CurrentCh == '_')
            {
                if (m_checkIdentCase)
                {
                    if (Char.IsUpper(CurrentCh)) ParseIdentOrKeyword("", TokenType.UpperCaseIdent, true);
                    else ParseIdentOrKeyword("", TokenType.LowerCaseIdent, true);
                }
                else
                {
                    ParseIdentOrKeyword("", TokenType.IdentOrKeyword, true);
                }
                m_currentStop = CurrentChPos;
                return;
            }
            if (CurrentCh == m_specIdentStart)
            {
                GoToNextChar();
                ParseIdentOrKeyword("" + m_specIdentStart, TokenType.SpecIdent, false);
                m_currentStop = CurrentChPos;
                return;
            }
            if (Char.IsNumber(CurrentCh))
            {
                ParseNumber();
                m_currentStop = CurrentChPos;
                return;
            }
            if (m_symbols.Contains(CurrentCh))
            {
                string optext = "";
                optext += CurrentCh;
                GoToNextChar();
                while (m_multiSymbolOperators.Contains(optext + CurrentCh))
                {
                    optext += CurrentCh;
                    GoToNextChar();
                }
                CurrentData = optext.ToString();
                m_currentStop = CurrentChPos;
                Current = TokenType.Symbol;
                return;
            }
            throw this.CreateParseError("DAE-00296 Unexpected character:" + CurrentCh);
        }

        protected virtual bool ParseSpecialToken()
        {
            return false;
        }

        public void GoBack(int steps)
        {
            if (steps > m_historyPosition) throw new InternalError("DAE-00054 SqlTokenizer: too small history buffer");
            m_historyPosition -= steps;
        }

        public int MarkPosition()
        {
            return m_historyPosition;
        }

        public void GoToMark(int mark)
        {
            m_historyPosition = mark;
        }

        public void SetMultiSymbolOperators(IEnumerable<string> operators)
        {
            m_multiSymbolOperators = new List<string>(operators);
            foreach (var op in operators)
            {
                foreach (var c in op) m_symbols.Add(c);
            }
        }

    }
}
