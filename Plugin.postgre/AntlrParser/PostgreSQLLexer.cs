// $ANTLR 3.2 Sep 23, 2009 12:02:23 ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g 2012-11-12 20:29:24

// The variable 'variable' is assigned but its value is never used.
#pragma warning disable 168, 219
// Unreachable code detected.
#pragma warning disable 162


using System;
using Antlr.Runtime;
using IList 		= System.Collections.IList;
using ArrayList 	= System.Collections.ArrayList;
using Stack 		= Antlr.Runtime.Collections.StackList;


public partial class PostgreSQLLexer : Lexer {
    public const int DOLLAR = 58;
    public const int T_STRING = 72;
    public const int T_IDENT = 14;
    public const int EQUALS = 32;
    public const int T_INTEGER = 73;
    public const int DOUBLE_AMPERSAND = 44;
    public const int EOF = -1;
    public const int NOT_EQUALS = 35;
    public const int T_COMMENT = 78;
    public const int LPAREN = 10;
    public const int ASTERISK = 50;
    public const int GREATER_OR_EQ = 40;
    public const int DOUBLE_PIPE = 46;
    public const int AT = 57;
    public const int INDEX = 6;
    public const int RPAREN = 12;
    public const int TILDA = 49;
    public const int CREATE = 4;
    public const int SLASH = 51;
    public const int GREATER = 39;
    public const int EXCLAMATION = 55;
    public const int USING = 9;
    public const int COMMA = 11;
    public const int LESS = 37;
    public const int PLUS = 47;
    public const int PIPE = 45;
    public const int DOT = 13;
    public const int LESS_OR_EQ = 38;
    public const int D = 23;
    public const int E = 18;
    public const int F = 61;
    public const int ARROW_UP = 59;
    public const int UNIQUE = 5;
    public const int G = 31;
    public const int A = 19;
    public const int EQUALS2 = 34;
    public const int B = 60;
    public const int PERCENT = 52;
    public const int C = 16;
    public const int LINE_COMMENT = 79;
    public const int L = 28;
    public const int NOT_EQUALS2 = 36;
    public const int SAFEEQUALS = 33;
    public const int M = 65;
    public const int N = 22;
    public const int O = 27;
    public const int T_BINARYNUM = 77;
    public const int H = 62;
    public const int I = 21;
    public const int J = 63;
    public const int K = 64;
    public const int U = 25;
    public const int T_FLOAT = 75;
    public const int AMPERSAND = 43;
    public const int T = 20;
    public const int ON = 8;
    public const int W = 68;
    public const int WHITESPACE = 80;
    public const int V = 67;
    public const int T_NSTRING = 71;
    public const int Q = 26;
    public const int P = 66;
    public const int S = 30;
    public const int R = 17;
    public const int MINUS = 48;
    public const int SEMI = 53;
    public const int Y = 29;
    public const int X = 24;
    public const int Z = 69;
    public const int T_QUOTED_IDENT = 15;
    public const int COLON = 56;
    public const int SHIFT_LEFT = 41;
    public const int FLOAT_EXP = 74;
    public const int SHIFT_RIGHT = 42;
    public const int QUESTION = 54;
    public const int CONCURRENTLY = 7;
    public const int T_BLOB = 76;
    public const int ID_START = 70;

    // delegates
    // delegators

    public PostgreSQLLexer() 
    {
		InitializeCyclicDFAs();
    }
    public PostgreSQLLexer(ICharStream input)
		: this(input, null) {
    }
    public PostgreSQLLexer(ICharStream input, RecognizerSharedState state)
		: base(input, state) {
		InitializeCyclicDFAs(); 

    }
    
    override public string GrammarFileName
    {
    	get { return "..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g";} 
    }

    // $ANTLR start "CREATE"
    public void mCREATE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CREATE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:37:7: ( C R E A T E )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:37:9: C R E A T E
            {
            	mC(); 
            	mR(); 
            	mE(); 
            	mA(); 
            	mT(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CREATE"

    // $ANTLR start "INDEX"
    public void mINDEX() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = INDEX;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:38:6: ( I N D E X )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:38:8: I N D E X
            {
            	mI(); 
            	mN(); 
            	mD(); 
            	mE(); 
            	mX(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "INDEX"

    // $ANTLR start "UNIQUE"
    public void mUNIQUE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = UNIQUE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:39:7: ( U N I Q U E )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:39:9: U N I Q U E
            {
            	mU(); 
            	mN(); 
            	mI(); 
            	mQ(); 
            	mU(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "UNIQUE"

    // $ANTLR start "CONCURRENTLY"
    public void mCONCURRENTLY() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CONCURRENTLY;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:40:14: ( C O N C U R R E N T L Y )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:40:16: C O N C U R R E N T L Y
            {
            	mC(); 
            	mO(); 
            	mN(); 
            	mC(); 
            	mU(); 
            	mR(); 
            	mR(); 
            	mE(); 
            	mN(); 
            	mT(); 
            	mL(); 
            	mY(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CONCURRENTLY"

    // $ANTLR start "USING"
    public void mUSING() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = USING;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:41:6: ( U S I N G )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:41:8: U S I N G
            {
            	mU(); 
            	mS(); 
            	mI(); 
            	mN(); 
            	mG(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "USING"

    // $ANTLR start "ON"
    public void mON() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ON;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:42:3: ( O N )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:42:5: O N
            {
            	mO(); 
            	mN(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ON"

    // $ANTLR start "EQUALS"
    public void mEQUALS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = EQUALS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:47:7: ( '=' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:47:16: '='
            {
            	Match('='); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "EQUALS"

    // $ANTLR start "SAFEEQUALS"
    public void mSAFEEQUALS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SAFEEQUALS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:48:11: ( '<=>' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:48:16: '<=>'
            {
            	Match("<=>"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SAFEEQUALS"

    // $ANTLR start "EQUALS2"
    public void mEQUALS2() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = EQUALS2;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:49:8: ( '==' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:49:16: '=='
            {
            	Match("=="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "EQUALS2"

    // $ANTLR start "NOT_EQUALS"
    public void mNOT_EQUALS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = NOT_EQUALS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:50:11: ( '!=' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:50:16: '!='
            {
            	Match("!="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "NOT_EQUALS"

    // $ANTLR start "NOT_EQUALS2"
    public void mNOT_EQUALS2() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = NOT_EQUALS2;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:51:12: ( '<>' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:51:16: '<>'
            {
            	Match("<>"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "NOT_EQUALS2"

    // $ANTLR start "LESS"
    public void mLESS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LESS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:52:5: ( '<' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:52:16: '<'
            {
            	Match('<'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LESS"

    // $ANTLR start "LESS_OR_EQ"
    public void mLESS_OR_EQ() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LESS_OR_EQ;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:53:11: ( '<=' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:53:16: '<='
            {
            	Match("<="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LESS_OR_EQ"

    // $ANTLR start "GREATER"
    public void mGREATER() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = GREATER;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:54:8: ( '>' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:54:16: '>'
            {
            	Match('>'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "GREATER"

    // $ANTLR start "GREATER_OR_EQ"
    public void mGREATER_OR_EQ() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = GREATER_OR_EQ;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:55:14: ( '>=' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:55:16: '>='
            {
            	Match(">="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "GREATER_OR_EQ"

    // $ANTLR start "SHIFT_LEFT"
    public void mSHIFT_LEFT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SHIFT_LEFT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:56:11: ( '<<' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:56:16: '<<'
            {
            	Match("<<"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SHIFT_LEFT"

    // $ANTLR start "SHIFT_RIGHT"
    public void mSHIFT_RIGHT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SHIFT_RIGHT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:57:12: ( '>>' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:57:16: '>>'
            {
            	Match(">>"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SHIFT_RIGHT"

    // $ANTLR start "AMPERSAND"
    public void mAMPERSAND() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = AMPERSAND;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:58:10: ( '&' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:58:16: '&'
            {
            	Match('&'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "AMPERSAND"

    // $ANTLR start "DOUBLE_AMPERSAND"
    public void mDOUBLE_AMPERSAND() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DOUBLE_AMPERSAND;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:59:17: ( '&&' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:59:19: '&&'
            {
            	Match("&&"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DOUBLE_AMPERSAND"

    // $ANTLR start "PIPE"
    public void mPIPE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = PIPE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:60:5: ( '|' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:60:16: '|'
            {
            	Match('|'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "PIPE"

    // $ANTLR start "DOUBLE_PIPE"
    public void mDOUBLE_PIPE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DOUBLE_PIPE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:61:12: ( '||' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:61:16: '||'
            {
            	Match("||"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DOUBLE_PIPE"

    // $ANTLR start "PLUS"
    public void mPLUS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = PLUS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:62:5: ( '+' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:62:16: '+'
            {
            	Match('+'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "PLUS"

    // $ANTLR start "MINUS"
    public void mMINUS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = MINUS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:63:6: ( '-' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:63:16: '-'
            {
            	Match('-'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "MINUS"

    // $ANTLR start "TILDA"
    public void mTILDA() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = TILDA;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:64:6: ( '~' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:64:16: '~'
            {
            	Match('~'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "TILDA"

    // $ANTLR start "ASTERISK"
    public void mASTERISK() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ASTERISK;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:65:9: ( '*' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:65:16: '*'
            {
            	Match('*'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ASTERISK"

    // $ANTLR start "SLASH"
    public void mSLASH() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SLASH;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:66:6: ( '/' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:66:16: '/'
            {
            	Match('/'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SLASH"

    // $ANTLR start "PERCENT"
    public void mPERCENT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = PERCENT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:67:8: ( '%' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:67:16: '%'
            {
            	Match('%'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "PERCENT"

    // $ANTLR start "SEMI"
    public void mSEMI() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SEMI;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:68:5: ( ';' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:68:16: ';'
            {
            	Match(';'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SEMI"

    // $ANTLR start "DOT"
    public void mDOT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DOT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:69:4: ( '.' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:69:16: '.'
            {
            	Match('.'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DOT"

    // $ANTLR start "COMMA"
    public void mCOMMA() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = COMMA;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:70:6: ( ',' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:70:16: ','
            {
            	Match(','); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "COMMA"

    // $ANTLR start "LPAREN"
    public void mLPAREN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LPAREN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:71:7: ( '(' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:71:16: '('
            {
            	Match('('); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LPAREN"

    // $ANTLR start "RPAREN"
    public void mRPAREN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = RPAREN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:72:7: ( ')' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:72:16: ')'
            {
            	Match(')'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "RPAREN"

    // $ANTLR start "QUESTION"
    public void mQUESTION() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = QUESTION;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:73:9: ( '?' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:73:16: '?'
            {
            	Match('?'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "QUESTION"

    // $ANTLR start "EXCLAMATION"
    public void mEXCLAMATION() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = EXCLAMATION;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:74:12: ( '!' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:74:16: '!'
            {
            	Match('!'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "EXCLAMATION"

    // $ANTLR start "COLON"
    public void mCOLON() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = COLON;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:75:6: ( ':' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:75:16: ':'
            {
            	Match(':'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "COLON"

    // $ANTLR start "AT"
    public void mAT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = AT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:76:3: ( '@' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:76:16: '@'
            {
            	Match('@'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "AT"

    // $ANTLR start "DOLLAR"
    public void mDOLLAR() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DOLLAR;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:77:7: ( '$' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:77:16: '$'
            {
            	Match('$'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DOLLAR"

    // $ANTLR start "ARROW_UP"
    public void mARROW_UP() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ARROW_UP;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:78:9: ( '^' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:78:16: '^'
            {
            	Match('^'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ARROW_UP"

    // $ANTLR start "A"
    public void mA() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:81:11: ( ( 'a' | 'A' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:81:12: ( 'a' | 'A' )
            {
            	if ( input.LA(1) == 'A' || input.LA(1) == 'a' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "A"

    // $ANTLR start "B"
    public void mB() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:82:11: ( ( 'b' | 'B' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:82:12: ( 'b' | 'B' )
            {
            	if ( input.LA(1) == 'B' || input.LA(1) == 'b' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "B"

    // $ANTLR start "C"
    public void mC() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:83:11: ( ( 'c' | 'C' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:83:12: ( 'c' | 'C' )
            {
            	if ( input.LA(1) == 'C' || input.LA(1) == 'c' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "C"

    // $ANTLR start "D"
    public void mD() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:84:11: ( ( 'd' | 'D' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:84:12: ( 'd' | 'D' )
            {
            	if ( input.LA(1) == 'D' || input.LA(1) == 'd' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "D"

    // $ANTLR start "E"
    public void mE() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:85:11: ( ( 'e' | 'E' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:85:12: ( 'e' | 'E' )
            {
            	if ( input.LA(1) == 'E' || input.LA(1) == 'e' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "E"

    // $ANTLR start "F"
    public void mF() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:86:11: ( ( 'f' | 'F' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:86:12: ( 'f' | 'F' )
            {
            	if ( input.LA(1) == 'F' || input.LA(1) == 'f' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "F"

    // $ANTLR start "G"
    public void mG() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:87:11: ( ( 'g' | 'G' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:87:12: ( 'g' | 'G' )
            {
            	if ( input.LA(1) == 'G' || input.LA(1) == 'g' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "G"

    // $ANTLR start "H"
    public void mH() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:88:11: ( ( 'h' | 'H' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:88:12: ( 'h' | 'H' )
            {
            	if ( input.LA(1) == 'H' || input.LA(1) == 'h' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "H"

    // $ANTLR start "I"
    public void mI() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:89:11: ( ( 'i' | 'I' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:89:12: ( 'i' | 'I' )
            {
            	if ( input.LA(1) == 'I' || input.LA(1) == 'i' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "I"

    // $ANTLR start "J"
    public void mJ() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:90:11: ( ( 'j' | 'J' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:90:12: ( 'j' | 'J' )
            {
            	if ( input.LA(1) == 'J' || input.LA(1) == 'j' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "J"

    // $ANTLR start "K"
    public void mK() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:91:11: ( ( 'k' | 'K' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:91:12: ( 'k' | 'K' )
            {
            	if ( input.LA(1) == 'K' || input.LA(1) == 'k' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "K"

    // $ANTLR start "L"
    public void mL() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:92:11: ( ( 'l' | 'L' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:92:12: ( 'l' | 'L' )
            {
            	if ( input.LA(1) == 'L' || input.LA(1) == 'l' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "L"

    // $ANTLR start "M"
    public void mM() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:93:11: ( ( 'm' | 'M' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:93:12: ( 'm' | 'M' )
            {
            	if ( input.LA(1) == 'M' || input.LA(1) == 'm' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "M"

    // $ANTLR start "N"
    public void mN() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:94:11: ( ( 'n' | 'N' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:94:12: ( 'n' | 'N' )
            {
            	if ( input.LA(1) == 'N' || input.LA(1) == 'n' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "N"

    // $ANTLR start "O"
    public void mO() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:95:11: ( ( 'o' | 'O' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:95:12: ( 'o' | 'O' )
            {
            	if ( input.LA(1) == 'O' || input.LA(1) == 'o' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "O"

    // $ANTLR start "P"
    public void mP() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:96:11: ( ( 'p' | 'P' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:96:12: ( 'p' | 'P' )
            {
            	if ( input.LA(1) == 'P' || input.LA(1) == 'p' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "P"

    // $ANTLR start "Q"
    public void mQ() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:97:11: ( ( 'q' | 'Q' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:97:12: ( 'q' | 'Q' )
            {
            	if ( input.LA(1) == 'Q' || input.LA(1) == 'q' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "Q"

    // $ANTLR start "R"
    public void mR() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:98:11: ( ( 'r' | 'R' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:98:12: ( 'r' | 'R' )
            {
            	if ( input.LA(1) == 'R' || input.LA(1) == 'r' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "R"

    // $ANTLR start "S"
    public void mS() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:99:11: ( ( 's' | 'S' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:99:12: ( 's' | 'S' )
            {
            	if ( input.LA(1) == 'S' || input.LA(1) == 's' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "S"

    // $ANTLR start "T"
    public void mT() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:100:11: ( ( 't' | 'T' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:100:12: ( 't' | 'T' )
            {
            	if ( input.LA(1) == 'T' || input.LA(1) == 't' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "T"

    // $ANTLR start "U"
    public void mU() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:101:11: ( ( 'u' | 'U' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:101:12: ( 'u' | 'U' )
            {
            	if ( input.LA(1) == 'U' || input.LA(1) == 'u' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "U"

    // $ANTLR start "V"
    public void mV() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:102:11: ( ( 'v' | 'V' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:102:12: ( 'v' | 'V' )
            {
            	if ( input.LA(1) == 'V' || input.LA(1) == 'v' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "V"

    // $ANTLR start "W"
    public void mW() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:103:11: ( ( 'w' | 'W' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:103:12: ( 'w' | 'W' )
            {
            	if ( input.LA(1) == 'W' || input.LA(1) == 'w' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "W"

    // $ANTLR start "X"
    public void mX() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:104:11: ( ( 'x' | 'X' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:104:12: ( 'x' | 'X' )
            {
            	if ( input.LA(1) == 'X' || input.LA(1) == 'x' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "X"

    // $ANTLR start "Y"
    public void mY() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:105:11: ( ( 'y' | 'Y' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:105:12: ( 'y' | 'Y' )
            {
            	if ( input.LA(1) == 'Y' || input.LA(1) == 'y' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "Y"

    // $ANTLR start "Z"
    public void mZ() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:106:11: ( ( 'z' | 'Z' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:106:12: ( 'z' | 'Z' )
            {
            	if ( input.LA(1) == 'Z' || input.LA(1) == 'z' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "Z"

    // $ANTLR start "ID_START"
    public void mID_START() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:110:18: ( ( 'a' .. 'z' | 'A' .. 'Z' | '_' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:110:20: ( 'a' .. 'z' | 'A' .. 'Z' | '_' )
            {
            	if ( (input.LA(1) >= 'A' && input.LA(1) <= 'Z') || input.LA(1) == '_' || (input.LA(1) >= 'a' && input.LA(1) <= 'z') ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "ID_START"

    // $ANTLR start "T_IDENT"
    public void mT_IDENT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T_IDENT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:111:8: ( ( ID_START ( ID_START | '0' .. '9' )* ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:111:10: ( ID_START ( ID_START | '0' .. '9' )* )
            {
            	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:111:10: ( ID_START ( ID_START | '0' .. '9' )* )
            	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:111:11: ID_START ( ID_START | '0' .. '9' )*
            	{
            		mID_START(); 
            		// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:111:20: ( ID_START | '0' .. '9' )*
            		do 
            		{
            		    int alt1 = 2;
            		    int LA1_0 = input.LA(1);

            		    if ( ((LA1_0 >= '0' && LA1_0 <= '9') || (LA1_0 >= 'A' && LA1_0 <= 'Z') || LA1_0 == '_' || (LA1_0 >= 'a' && LA1_0 <= 'z')) )
            		    {
            		        alt1 = 1;
            		    }


            		    switch (alt1) 
            			{
            				case 1 :
            				    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:
            				    {
            				    	if ( (input.LA(1) >= '0' && input.LA(1) <= '9') || (input.LA(1) >= 'A' && input.LA(1) <= 'Z') || input.LA(1) == '_' || (input.LA(1) >= 'a' && input.LA(1) <= 'z') ) 
            				    	{
            				    	    input.Consume();

            				    	}
            				    	else 
            				    	{
            				    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            				    	    Recover(mse);
            				    	    throw mse;}


            				    }
            				    break;

            				default:
            				    goto loop1;
            		    }
            		} while (true);

            		loop1:
            			;	// Stops C# compiler whining that label 'loop1' has no statements


            	}


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T_IDENT"

    // $ANTLR start "T_QUOTED_IDENT"
    public void mT_QUOTED_IDENT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T_QUOTED_IDENT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:112:15: ( '\"' ( options {greedy=false; } : . )* '\"' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:112:17: '\"' ( options {greedy=false; } : . )* '\"'
            {
            	Match('\"'); 
            	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:112:21: ( options {greedy=false; } : . )*
            	do 
            	{
            	    int alt2 = 2;
            	    int LA2_0 = input.LA(1);

            	    if ( (LA2_0 == '\"') )
            	    {
            	        alt2 = 2;
            	    }
            	    else if ( ((LA2_0 >= '\u0000' && LA2_0 <= '!') || (LA2_0 >= '#' && LA2_0 <= '\uFFFF')) )
            	    {
            	        alt2 = 1;
            	    }


            	    switch (alt2) 
            		{
            			case 1 :
            			    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:112:49: .
            			    {
            			    	MatchAny(); 

            			    }
            			    break;

            			default:
            			    goto loop2;
            	    }
            	} while (true);

            	loop2:
            		;	// Stops C# compiler whining that label 'loop2' has no statements

            	Match('\"'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T_QUOTED_IDENT"

    // $ANTLR start "T_NSTRING"
    public void mT_NSTRING() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T_NSTRING;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:114:10: ( ( 'N' | 'n' ) ( '\\'' ( options {greedy=true; } : ~ ( '\\'' | '\\r' | '\\n' ) | '\\'' '\\'' )* '\\'' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:115:2: ( 'N' | 'n' ) ( '\\'' ( options {greedy=true; } : ~ ( '\\'' | '\\r' | '\\n' ) | '\\'' '\\'' )* '\\'' )
            {
            	if ( input.LA(1) == 'N' || input.LA(1) == 'n' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}

            	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:116:4: ( '\\'' ( options {greedy=true; } : ~ ( '\\'' | '\\r' | '\\n' ) | '\\'' '\\'' )* '\\'' )
            	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:116:5: '\\'' ( options {greedy=true; } : ~ ( '\\'' | '\\r' | '\\n' ) | '\\'' '\\'' )* '\\''
            	{
            		Match('\''); 
            		// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:117:5: ( options {greedy=true; } : ~ ( '\\'' | '\\r' | '\\n' ) | '\\'' '\\'' )*
            		do 
            		{
            		    int alt3 = 3;
            		    int LA3_0 = input.LA(1);

            		    if ( (LA3_0 == '\'') )
            		    {
            		        int LA3_1 = input.LA(2);

            		        if ( (LA3_1 == '\'') )
            		        {
            		            alt3 = 2;
            		        }


            		    }
            		    else if ( ((LA3_0 >= '\u0000' && LA3_0 <= '\t') || (LA3_0 >= '\u000B' && LA3_0 <= '\f') || (LA3_0 >= '\u000E' && LA3_0 <= '&') || (LA3_0 >= '(' && LA3_0 <= '\uFFFF')) )
            		    {
            		        alt3 = 1;
            		    }


            		    switch (alt3) 
            			{
            				case 1 :
            				    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:118:31: ~ ( '\\'' | '\\r' | '\\n' )
            				    {
            				    	if ( (input.LA(1) >= '\u0000' && input.LA(1) <= '\t') || (input.LA(1) >= '\u000B' && input.LA(1) <= '\f') || (input.LA(1) >= '\u000E' && input.LA(1) <= '&') || (input.LA(1) >= '(' && input.LA(1) <= '\uFFFF') ) 
            				    	{
            				    	    input.Consume();

            				    	}
            				    	else 
            				    	{
            				    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            				    	    Recover(mse);
            				    	    throw mse;}


            				    }
            				    break;
            				case 2 :
            				    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:118:56: '\\'' '\\''
            				    {
            				    	Match('\''); 
            				    	Match('\''); 

            				    }
            				    break;

            				default:
            				    goto loop3;
            		    }
            		} while (true);

            		loop3:
            			;	// Stops C# compiler whining that label 'loop3' has no statements

            		Match('\''); 

            	}


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T_NSTRING"

    // $ANTLR start "T_STRING"
    public void mT_STRING() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T_STRING;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:122:9: ( ( '\\'' ( options {greedy=true; } : ~ ( '\\'' | '\\r' | '\\n' ) | '\\'' '\\'' )* '\\'' ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:123:4: ( '\\'' ( options {greedy=true; } : ~ ( '\\'' | '\\r' | '\\n' ) | '\\'' '\\'' )* '\\'' )
            {
            	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:123:4: ( '\\'' ( options {greedy=true; } : ~ ( '\\'' | '\\r' | '\\n' ) | '\\'' '\\'' )* '\\'' )
            	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:123:5: '\\'' ( options {greedy=true; } : ~ ( '\\'' | '\\r' | '\\n' ) | '\\'' '\\'' )* '\\''
            	{
            		Match('\''); 
            		// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:124:5: ( options {greedy=true; } : ~ ( '\\'' | '\\r' | '\\n' ) | '\\'' '\\'' )*
            		do 
            		{
            		    int alt4 = 3;
            		    int LA4_0 = input.LA(1);

            		    if ( (LA4_0 == '\'') )
            		    {
            		        int LA4_1 = input.LA(2);

            		        if ( (LA4_1 == '\'') )
            		        {
            		            alt4 = 2;
            		        }


            		    }
            		    else if ( ((LA4_0 >= '\u0000' && LA4_0 <= '\t') || (LA4_0 >= '\u000B' && LA4_0 <= '\f') || (LA4_0 >= '\u000E' && LA4_0 <= '&') || (LA4_0 >= '(' && LA4_0 <= '\uFFFF')) )
            		    {
            		        alt4 = 1;
            		    }


            		    switch (alt4) 
            			{
            				case 1 :
            				    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:125:31: ~ ( '\\'' | '\\r' | '\\n' )
            				    {
            				    	if ( (input.LA(1) >= '\u0000' && input.LA(1) <= '\t') || (input.LA(1) >= '\u000B' && input.LA(1) <= '\f') || (input.LA(1) >= '\u000E' && input.LA(1) <= '&') || (input.LA(1) >= '(' && input.LA(1) <= '\uFFFF') ) 
            				    	{
            				    	    input.Consume();

            				    	}
            				    	else 
            				    	{
            				    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            				    	    Recover(mse);
            				    	    throw mse;}


            				    }
            				    break;
            				case 2 :
            				    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:125:56: '\\'' '\\''
            				    {
            				    	Match('\''); 
            				    	Match('\''); 

            				    }
            				    break;

            				default:
            				    goto loop4;
            		    }
            		} while (true);

            		loop4:
            			;	// Stops C# compiler whining that label 'loop4' has no statements

            		Match('\''); 

            	}


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T_STRING"

    // $ANTLR start "T_INTEGER"
    public void mT_INTEGER() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T_INTEGER;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:129:10: ( ( '0' .. '9' )+ )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:129:12: ( '0' .. '9' )+
            {
            	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:129:12: ( '0' .. '9' )+
            	int cnt5 = 0;
            	do 
            	{
            	    int alt5 = 2;
            	    int LA5_0 = input.LA(1);

            	    if ( ((LA5_0 >= '0' && LA5_0 <= '9')) )
            	    {
            	        alt5 = 1;
            	    }


            	    switch (alt5) 
            		{
            			case 1 :
            			    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:129:13: '0' .. '9'
            			    {
            			    	MatchRange('0','9'); 

            			    }
            			    break;

            			default:
            			    if ( cnt5 >= 1 ) goto loop5;
            		            EarlyExitException eee5 =
            		                new EarlyExitException(5, input);
            		            throw eee5;
            	    }
            	    cnt5++;
            	} while (true);

            	loop5:
            		;	// Stops C# compiler whining that label 'loop5' has no statements


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T_INTEGER"

    // $ANTLR start "FLOAT_EXP"
    public void mFLOAT_EXP() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:130:20: ( ( 'e' | 'E' ) ( '+' | '-' )? ( '0' .. '9' )+ )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:130:22: ( 'e' | 'E' ) ( '+' | '-' )? ( '0' .. '9' )+
            {
            	if ( input.LA(1) == 'E' || input.LA(1) == 'e' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}

            	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:130:32: ( '+' | '-' )?
            	int alt6 = 2;
            	int LA6_0 = input.LA(1);

            	if ( (LA6_0 == '+' || LA6_0 == '-') )
            	{
            	    alt6 = 1;
            	}
            	switch (alt6) 
            	{
            	    case 1 :
            	        // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:
            	        {
            	        	if ( input.LA(1) == '+' || input.LA(1) == '-' ) 
            	        	{
            	        	    input.Consume();

            	        	}
            	        	else 
            	        	{
            	        	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	        	    Recover(mse);
            	        	    throw mse;}


            	        }
            	        break;

            	}

            	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:130:43: ( '0' .. '9' )+
            	int cnt7 = 0;
            	do 
            	{
            	    int alt7 = 2;
            	    int LA7_0 = input.LA(1);

            	    if ( ((LA7_0 >= '0' && LA7_0 <= '9')) )
            	    {
            	        alt7 = 1;
            	    }


            	    switch (alt7) 
            		{
            			case 1 :
            			    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:130:44: '0' .. '9'
            			    {
            			    	MatchRange('0','9'); 

            			    }
            			    break;

            			default:
            			    if ( cnt7 >= 1 ) goto loop7;
            		            EarlyExitException eee7 =
            		                new EarlyExitException(7, input);
            		            throw eee7;
            	    }
            	    cnt7++;
            	} while (true);

            	loop7:
            		;	// Stops C# compiler whining that label 'loop7' has no statements


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "FLOAT_EXP"

    // $ANTLR start "T_FLOAT"
    public void mT_FLOAT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T_FLOAT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:132:5: ( ( '0' .. '9' )+ '.' ( '0' .. '9' )* ( FLOAT_EXP )? | '.' ( '0' .. '9' )+ ( FLOAT_EXP )? | ( '0' .. '9' )+ FLOAT_EXP )
            int alt14 = 3;
            alt14 = dfa14.Predict(input);
            switch (alt14) 
            {
                case 1 :
                    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:132:9: ( '0' .. '9' )+ '.' ( '0' .. '9' )* ( FLOAT_EXP )?
                    {
                    	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:132:9: ( '0' .. '9' )+
                    	int cnt8 = 0;
                    	do 
                    	{
                    	    int alt8 = 2;
                    	    int LA8_0 = input.LA(1);

                    	    if ( ((LA8_0 >= '0' && LA8_0 <= '9')) )
                    	    {
                    	        alt8 = 1;
                    	    }


                    	    switch (alt8) 
                    		{
                    			case 1 :
                    			    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:132:10: '0' .. '9'
                    			    {
                    			    	MatchRange('0','9'); 

                    			    }
                    			    break;

                    			default:
                    			    if ( cnt8 >= 1 ) goto loop8;
                    		            EarlyExitException eee8 =
                    		                new EarlyExitException(8, input);
                    		            throw eee8;
                    	    }
                    	    cnt8++;
                    	} while (true);

                    	loop8:
                    		;	// Stops C# compiler whining that label 'loop8' has no statements

                    	Match('.'); 
                    	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:132:25: ( '0' .. '9' )*
                    	do 
                    	{
                    	    int alt9 = 2;
                    	    int LA9_0 = input.LA(1);

                    	    if ( ((LA9_0 >= '0' && LA9_0 <= '9')) )
                    	    {
                    	        alt9 = 1;
                    	    }


                    	    switch (alt9) 
                    		{
                    			case 1 :
                    			    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:132:26: '0' .. '9'
                    			    {
                    			    	MatchRange('0','9'); 

                    			    }
                    			    break;

                    			default:
                    			    goto loop9;
                    	    }
                    	} while (true);

                    	loop9:
                    		;	// Stops C# compiler whining that label 'loop9' has no statements

                    	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:132:37: ( FLOAT_EXP )?
                    	int alt10 = 2;
                    	int LA10_0 = input.LA(1);

                    	if ( (LA10_0 == 'E' || LA10_0 == 'e') )
                    	{
                    	    alt10 = 1;
                    	}
                    	switch (alt10) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:132:37: FLOAT_EXP
                    	        {
                    	        	mFLOAT_EXP(); 

                    	        }
                    	        break;

                    	}


                    }
                    break;
                case 2 :
                    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:133:9: '.' ( '0' .. '9' )+ ( FLOAT_EXP )?
                    {
                    	Match('.'); 
                    	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:133:13: ( '0' .. '9' )+
                    	int cnt11 = 0;
                    	do 
                    	{
                    	    int alt11 = 2;
                    	    int LA11_0 = input.LA(1);

                    	    if ( ((LA11_0 >= '0' && LA11_0 <= '9')) )
                    	    {
                    	        alt11 = 1;
                    	    }


                    	    switch (alt11) 
                    		{
                    			case 1 :
                    			    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:133:14: '0' .. '9'
                    			    {
                    			    	MatchRange('0','9'); 

                    			    }
                    			    break;

                    			default:
                    			    if ( cnt11 >= 1 ) goto loop11;
                    		            EarlyExitException eee11 =
                    		                new EarlyExitException(11, input);
                    		            throw eee11;
                    	    }
                    	    cnt11++;
                    	} while (true);

                    	loop11:
                    		;	// Stops C# compiler whining that label 'loop11' has no statements

                    	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:133:25: ( FLOAT_EXP )?
                    	int alt12 = 2;
                    	int LA12_0 = input.LA(1);

                    	if ( (LA12_0 == 'E' || LA12_0 == 'e') )
                    	{
                    	    alt12 = 1;
                    	}
                    	switch (alt12) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:133:25: FLOAT_EXP
                    	        {
                    	        	mFLOAT_EXP(); 

                    	        }
                    	        break;

                    	}


                    }
                    break;
                case 3 :
                    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:134:9: ( '0' .. '9' )+ FLOAT_EXP
                    {
                    	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:134:9: ( '0' .. '9' )+
                    	int cnt13 = 0;
                    	do 
                    	{
                    	    int alt13 = 2;
                    	    int LA13_0 = input.LA(1);

                    	    if ( ((LA13_0 >= '0' && LA13_0 <= '9')) )
                    	    {
                    	        alt13 = 1;
                    	    }


                    	    switch (alt13) 
                    		{
                    			case 1 :
                    			    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:134:10: '0' .. '9'
                    			    {
                    			    	MatchRange('0','9'); 

                    			    }
                    			    break;

                    			default:
                    			    if ( cnt13 >= 1 ) goto loop13;
                    		            EarlyExitException eee13 =
                    		                new EarlyExitException(13, input);
                    		            throw eee13;
                    	    }
                    	    cnt13++;
                    	} while (true);

                    	loop13:
                    		;	// Stops C# compiler whining that label 'loop13' has no statements

                    	mFLOAT_EXP(); 

                    }
                    break;

            }
            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T_FLOAT"

    // $ANTLR start "T_BLOB"
    public void mT_BLOB() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T_BLOB;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:136:7: ( ( 'x' | 'X' ) '\\'' ( '0' .. '9' | 'a' .. 'f' | 'A' .. 'F' )+ '\\'' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:136:9: ( 'x' | 'X' ) '\\'' ( '0' .. '9' | 'a' .. 'f' | 'A' .. 'F' )+ '\\''
            {
            	if ( input.LA(1) == 'X' || input.LA(1) == 'x' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}

            	Match('\''); 
            	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:136:24: ( '0' .. '9' | 'a' .. 'f' | 'A' .. 'F' )+
            	int cnt15 = 0;
            	do 
            	{
            	    int alt15 = 2;
            	    int LA15_0 = input.LA(1);

            	    if ( ((LA15_0 >= '0' && LA15_0 <= '9') || (LA15_0 >= 'A' && LA15_0 <= 'F') || (LA15_0 >= 'a' && LA15_0 <= 'f')) )
            	    {
            	        alt15 = 1;
            	    }


            	    switch (alt15) 
            		{
            			case 1 :
            			    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:
            			    {
            			    	if ( (input.LA(1) >= '0' && input.LA(1) <= '9') || (input.LA(1) >= 'A' && input.LA(1) <= 'F') || (input.LA(1) >= 'a' && input.LA(1) <= 'f') ) 
            			    	{
            			    	    input.Consume();

            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    Recover(mse);
            			    	    throw mse;}


            			    }
            			    break;

            			default:
            			    if ( cnt15 >= 1 ) goto loop15;
            		            EarlyExitException eee15 =
            		                new EarlyExitException(15, input);
            		            throw eee15;
            	    }
            	    cnt15++;
            	} while (true);

            	loop15:
            		;	// Stops C# compiler whining that label 'loop15' has no statements

            	Match('\''); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T_BLOB"

    // $ANTLR start "T_BINARYNUM"
    public void mT_BINARYNUM() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T_BINARYNUM;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:138:13: ( ( '0' 'b' ( '0' | '1' )+ ) | 'b' '\\'' ( '0' | '1' )+ '\\'' )
            int alt18 = 2;
            int LA18_0 = input.LA(1);

            if ( (LA18_0 == '0') )
            {
                alt18 = 1;
            }
            else if ( (LA18_0 == 'b') )
            {
                alt18 = 2;
            }
            else 
            {
                NoViableAltException nvae_d18s0 =
                    new NoViableAltException("", 18, 0, input);

                throw nvae_d18s0;
            }
            switch (alt18) 
            {
                case 1 :
                    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:138:15: ( '0' 'b' ( '0' | '1' )+ )
                    {
                    	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:138:15: ( '0' 'b' ( '0' | '1' )+ )
                    	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:138:16: '0' 'b' ( '0' | '1' )+
                    	{
                    		Match('0'); 
                    		Match('b'); 
                    		// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:138:24: ( '0' | '1' )+
                    		int cnt16 = 0;
                    		do 
                    		{
                    		    int alt16 = 2;
                    		    int LA16_0 = input.LA(1);

                    		    if ( ((LA16_0 >= '0' && LA16_0 <= '1')) )
                    		    {
                    		        alt16 = 1;
                    		    }


                    		    switch (alt16) 
                    			{
                    				case 1 :
                    				    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:
                    				    {
                    				    	if ( (input.LA(1) >= '0' && input.LA(1) <= '1') ) 
                    				    	{
                    				    	    input.Consume();

                    				    	}
                    				    	else 
                    				    	{
                    				    	    MismatchedSetException mse = new MismatchedSetException(null,input);
                    				    	    Recover(mse);
                    				    	    throw mse;}


                    				    }
                    				    break;

                    				default:
                    				    if ( cnt16 >= 1 ) goto loop16;
                    			            EarlyExitException eee16 =
                    			                new EarlyExitException(16, input);
                    			            throw eee16;
                    		    }
                    		    cnt16++;
                    		} while (true);

                    		loop16:
                    			;	// Stops C# compiler whining that label 'loop16' has no statements


                    	}


                    }
                    break;
                case 2 :
                    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:138:41: 'b' '\\'' ( '0' | '1' )+ '\\''
                    {
                    	Match('b'); 
                    	Match('\''); 
                    	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:138:50: ( '0' | '1' )+
                    	int cnt17 = 0;
                    	do 
                    	{
                    	    int alt17 = 2;
                    	    int LA17_0 = input.LA(1);

                    	    if ( ((LA17_0 >= '0' && LA17_0 <= '1')) )
                    	    {
                    	        alt17 = 1;
                    	    }


                    	    switch (alt17) 
                    		{
                    			case 1 :
                    			    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:
                    			    {
                    			    	if ( (input.LA(1) >= '0' && input.LA(1) <= '1') ) 
                    			    	{
                    			    	    input.Consume();

                    			    	}
                    			    	else 
                    			    	{
                    			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
                    			    	    Recover(mse);
                    			    	    throw mse;}


                    			    }
                    			    break;

                    			default:
                    			    if ( cnt17 >= 1 ) goto loop17;
                    		            EarlyExitException eee17 =
                    		                new EarlyExitException(17, input);
                    		            throw eee17;
                    	    }
                    	    cnt17++;
                    	} while (true);

                    	loop17:
                    		;	// Stops C# compiler whining that label 'loop17' has no statements

                    	Match('\''); 

                    }
                    break;

            }
            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T_BINARYNUM"

    // $ANTLR start "T_COMMENT"
    public void mT_COMMENT() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:140:19: ( '/*' ( options {greedy=false; } : . )* '*/' )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:140:21: '/*' ( options {greedy=false; } : . )* '*/'
            {
            	Match("/*"); 

            	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:140:26: ( options {greedy=false; } : . )*
            	do 
            	{
            	    int alt19 = 2;
            	    int LA19_0 = input.LA(1);

            	    if ( (LA19_0 == '*') )
            	    {
            	        int LA19_1 = input.LA(2);

            	        if ( (LA19_1 == '/') )
            	        {
            	            alt19 = 2;
            	        }
            	        else if ( ((LA19_1 >= '\u0000' && LA19_1 <= '.') || (LA19_1 >= '0' && LA19_1 <= '\uFFFF')) )
            	        {
            	            alt19 = 1;
            	        }


            	    }
            	    else if ( ((LA19_0 >= '\u0000' && LA19_0 <= ')') || (LA19_0 >= '+' && LA19_0 <= '\uFFFF')) )
            	    {
            	        alt19 = 1;
            	    }


            	    switch (alt19) 
            		{
            			case 1 :
            			    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:140:54: .
            			    {
            			    	MatchAny(); 

            			    }
            			    break;

            			default:
            			    goto loop19;
            	    }
            	} while (true);

            	loop19:
            		;	// Stops C# compiler whining that label 'loop19' has no statements

            	Match("*/"); 


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "T_COMMENT"

    // $ANTLR start "LINE_COMMENT"
    public void mLINE_COMMENT() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:141:22: ( '--' (~ ( '\\n' | '\\r' ) )* ( ( '\\r' )? '\\n' | EOF ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:141:24: '--' (~ ( '\\n' | '\\r' ) )* ( ( '\\r' )? '\\n' | EOF )
            {
            	Match("--"); 

            	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:141:29: (~ ( '\\n' | '\\r' ) )*
            	do 
            	{
            	    int alt20 = 2;
            	    int LA20_0 = input.LA(1);

            	    if ( ((LA20_0 >= '\u0000' && LA20_0 <= '\t') || (LA20_0 >= '\u000B' && LA20_0 <= '\f') || (LA20_0 >= '\u000E' && LA20_0 <= '\uFFFF')) )
            	    {
            	        alt20 = 1;
            	    }


            	    switch (alt20) 
            		{
            			case 1 :
            			    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:141:29: ~ ( '\\n' | '\\r' )
            			    {
            			    	if ( (input.LA(1) >= '\u0000' && input.LA(1) <= '\t') || (input.LA(1) >= '\u000B' && input.LA(1) <= '\f') || (input.LA(1) >= '\u000E' && input.LA(1) <= '\uFFFF') ) 
            			    	{
            			    	    input.Consume();

            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    Recover(mse);
            			    	    throw mse;}


            			    }
            			    break;

            			default:
            			    goto loop20;
            	    }
            	} while (true);

            	loop20:
            		;	// Stops C# compiler whining that label 'loop20' has no statements

            	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:141:43: ( ( '\\r' )? '\\n' | EOF )
            	int alt22 = 2;
            	int LA22_0 = input.LA(1);

            	if ( (LA22_0 == '\n' || LA22_0 == '\r') )
            	{
            	    alt22 = 1;
            	}
            	else 
            	{
            	    alt22 = 2;}
            	switch (alt22) 
            	{
            	    case 1 :
            	        // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:141:44: ( '\\r' )? '\\n'
            	        {
            	        	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:141:44: ( '\\r' )?
            	        	int alt21 = 2;
            	        	int LA21_0 = input.LA(1);

            	        	if ( (LA21_0 == '\r') )
            	        	{
            	        	    alt21 = 1;
            	        	}
            	        	switch (alt21) 
            	        	{
            	        	    case 1 :
            	        	        // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:141:44: '\\r'
            	        	        {
            	        	        	Match('\r'); 

            	        	        }
            	        	        break;

            	        	}

            	        	Match('\n'); 

            	        }
            	        break;
            	    case 2 :
            	        // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:141:55: EOF
            	        {
            	        	Match(EOF); 

            	        }
            	        break;

            	}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "LINE_COMMENT"

    // $ANTLR start "WHITESPACE"
    public void mWHITESPACE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = WHITESPACE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:143:11: ( ( ' ' | '\\r' | '\\t' | '\\u000C' | '\\n' | T_COMMENT | LINE_COMMENT ) )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:143:13: ( ' ' | '\\r' | '\\t' | '\\u000C' | '\\n' | T_COMMENT | LINE_COMMENT )
            {
            	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:143:13: ( ' ' | '\\r' | '\\t' | '\\u000C' | '\\n' | T_COMMENT | LINE_COMMENT )
            	int alt23 = 7;
            	switch ( input.LA(1) ) 
            	{
            	case ' ':
            		{
            	    alt23 = 1;
            	    }
            	    break;
            	case '\r':
            		{
            	    alt23 = 2;
            	    }
            	    break;
            	case '\t':
            		{
            	    alt23 = 3;
            	    }
            	    break;
            	case '\f':
            		{
            	    alt23 = 4;
            	    }
            	    break;
            	case '\n':
            		{
            	    alt23 = 5;
            	    }
            	    break;
            	case '/':
            		{
            	    alt23 = 6;
            	    }
            	    break;
            	case '-':
            		{
            	    alt23 = 7;
            	    }
            	    break;
            		default:
            		    NoViableAltException nvae_d23s0 =
            		        new NoViableAltException("", 23, 0, input);

            		    throw nvae_d23s0;
            	}

            	switch (alt23) 
            	{
            	    case 1 :
            	        // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:143:14: ' '
            	        {
            	        	Match(' '); 

            	        }
            	        break;
            	    case 2 :
            	        // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:143:18: '\\r'
            	        {
            	        	Match('\r'); 

            	        }
            	        break;
            	    case 3 :
            	        // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:143:23: '\\t'
            	        {
            	        	Match('\t'); 

            	        }
            	        break;
            	    case 4 :
            	        // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:143:28: '\\u000C'
            	        {
            	        	Match('\f'); 

            	        }
            	        break;
            	    case 5 :
            	        // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:143:37: '\\n'
            	        {
            	        	Match('\n'); 

            	        }
            	        break;
            	    case 6 :
            	        // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:143:42: T_COMMENT
            	        {
            	        	mT_COMMENT(); 

            	        }
            	        break;
            	    case 7 :
            	        // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:143:52: LINE_COMMENT
            	        {
            	        	mLINE_COMMENT(); 

            	        }
            	        break;

            	}

            	_channel=HIDDEN;

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "WHITESPACE"

    override public void mTokens() // throws RecognitionException 
    {
        // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:8: ( CREATE | INDEX | UNIQUE | CONCURRENTLY | USING | ON | EQUALS | SAFEEQUALS | EQUALS2 | NOT_EQUALS | NOT_EQUALS2 | LESS | LESS_OR_EQ | GREATER | GREATER_OR_EQ | SHIFT_LEFT | SHIFT_RIGHT | AMPERSAND | DOUBLE_AMPERSAND | PIPE | DOUBLE_PIPE | PLUS | MINUS | TILDA | ASTERISK | SLASH | PERCENT | SEMI | DOT | COMMA | LPAREN | RPAREN | QUESTION | EXCLAMATION | COLON | AT | DOLLAR | ARROW_UP | T_IDENT | T_QUOTED_IDENT | T_NSTRING | T_STRING | T_INTEGER | T_FLOAT | T_BLOB | T_BINARYNUM | WHITESPACE )
        int alt24 = 47;
        alt24 = dfa24.Predict(input);
        switch (alt24) 
        {
            case 1 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:10: CREATE
                {
                	mCREATE(); 

                }
                break;
            case 2 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:17: INDEX
                {
                	mINDEX(); 

                }
                break;
            case 3 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:23: UNIQUE
                {
                	mUNIQUE(); 

                }
                break;
            case 4 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:30: CONCURRENTLY
                {
                	mCONCURRENTLY(); 

                }
                break;
            case 5 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:43: USING
                {
                	mUSING(); 

                }
                break;
            case 6 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:49: ON
                {
                	mON(); 

                }
                break;
            case 7 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:52: EQUALS
                {
                	mEQUALS(); 

                }
                break;
            case 8 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:59: SAFEEQUALS
                {
                	mSAFEEQUALS(); 

                }
                break;
            case 9 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:70: EQUALS2
                {
                	mEQUALS2(); 

                }
                break;
            case 10 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:78: NOT_EQUALS
                {
                	mNOT_EQUALS(); 

                }
                break;
            case 11 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:89: NOT_EQUALS2
                {
                	mNOT_EQUALS2(); 

                }
                break;
            case 12 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:101: LESS
                {
                	mLESS(); 

                }
                break;
            case 13 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:106: LESS_OR_EQ
                {
                	mLESS_OR_EQ(); 

                }
                break;
            case 14 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:117: GREATER
                {
                	mGREATER(); 

                }
                break;
            case 15 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:125: GREATER_OR_EQ
                {
                	mGREATER_OR_EQ(); 

                }
                break;
            case 16 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:139: SHIFT_LEFT
                {
                	mSHIFT_LEFT(); 

                }
                break;
            case 17 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:150: SHIFT_RIGHT
                {
                	mSHIFT_RIGHT(); 

                }
                break;
            case 18 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:162: AMPERSAND
                {
                	mAMPERSAND(); 

                }
                break;
            case 19 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:172: DOUBLE_AMPERSAND
                {
                	mDOUBLE_AMPERSAND(); 

                }
                break;
            case 20 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:189: PIPE
                {
                	mPIPE(); 

                }
                break;
            case 21 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:194: DOUBLE_PIPE
                {
                	mDOUBLE_PIPE(); 

                }
                break;
            case 22 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:206: PLUS
                {
                	mPLUS(); 

                }
                break;
            case 23 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:211: MINUS
                {
                	mMINUS(); 

                }
                break;
            case 24 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:217: TILDA
                {
                	mTILDA(); 

                }
                break;
            case 25 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:223: ASTERISK
                {
                	mASTERISK(); 

                }
                break;
            case 26 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:232: SLASH
                {
                	mSLASH(); 

                }
                break;
            case 27 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:238: PERCENT
                {
                	mPERCENT(); 

                }
                break;
            case 28 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:246: SEMI
                {
                	mSEMI(); 

                }
                break;
            case 29 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:251: DOT
                {
                	mDOT(); 

                }
                break;
            case 30 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:255: COMMA
                {
                	mCOMMA(); 

                }
                break;
            case 31 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:261: LPAREN
                {
                	mLPAREN(); 

                }
                break;
            case 32 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:268: RPAREN
                {
                	mRPAREN(); 

                }
                break;
            case 33 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:275: QUESTION
                {
                	mQUESTION(); 

                }
                break;
            case 34 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:284: EXCLAMATION
                {
                	mEXCLAMATION(); 

                }
                break;
            case 35 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:296: COLON
                {
                	mCOLON(); 

                }
                break;
            case 36 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:302: AT
                {
                	mAT(); 

                }
                break;
            case 37 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:305: DOLLAR
                {
                	mDOLLAR(); 

                }
                break;
            case 38 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:312: ARROW_UP
                {
                	mARROW_UP(); 

                }
                break;
            case 39 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:321: T_IDENT
                {
                	mT_IDENT(); 

                }
                break;
            case 40 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:329: T_QUOTED_IDENT
                {
                	mT_QUOTED_IDENT(); 

                }
                break;
            case 41 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:344: T_NSTRING
                {
                	mT_NSTRING(); 

                }
                break;
            case 42 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:354: T_STRING
                {
                	mT_STRING(); 

                }
                break;
            case 43 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:363: T_INTEGER
                {
                	mT_INTEGER(); 

                }
                break;
            case 44 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:373: T_FLOAT
                {
                	mT_FLOAT(); 

                }
                break;
            case 45 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:381: T_BLOB
                {
                	mT_BLOB(); 

                }
                break;
            case 46 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:388: T_BINARYNUM
                {
                	mT_BINARYNUM(); 

                }
                break;
            case 47 :
                // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:1:400: WHITESPACE
                {
                	mWHITESPACE(); 

                }
                break;

        }

    }


    protected DFA14 dfa14;
    protected DFA24 dfa24;
	private void InitializeCyclicDFAs()
	{
	    this.dfa14 = new DFA14(this);
	    this.dfa24 = new DFA24(this);
	}

    const string DFA14_eotS =
        "\x05\uffff";
    const string DFA14_eofS =
        "\x05\uffff";
    const string DFA14_minS =
        "\x02\x2e\x03\uffff";
    const string DFA14_maxS =
        "\x01\x39\x01\x65\x03\uffff";
    const string DFA14_acceptS =
        "\x02\uffff\x01\x02\x01\x01\x01\x03";
    const string DFA14_specialS =
        "\x05\uffff}>";
    static readonly string[] DFA14_transitionS = {
            "\x01\x02\x01\uffff\x0a\x01",
            "\x01\x03\x01\uffff\x0a\x01\x0b\uffff\x01\x04\x1f\uffff\x01"+
            "\x04",
            "",
            "",
            ""
    };

    static readonly short[] DFA14_eot = DFA.UnpackEncodedString(DFA14_eotS);
    static readonly short[] DFA14_eof = DFA.UnpackEncodedString(DFA14_eofS);
    static readonly char[] DFA14_min = DFA.UnpackEncodedStringToUnsignedChars(DFA14_minS);
    static readonly char[] DFA14_max = DFA.UnpackEncodedStringToUnsignedChars(DFA14_maxS);
    static readonly short[] DFA14_accept = DFA.UnpackEncodedString(DFA14_acceptS);
    static readonly short[] DFA14_special = DFA.UnpackEncodedString(DFA14_specialS);
    static readonly short[][] DFA14_transition = DFA.UnpackEncodedStringArray(DFA14_transitionS);

    protected class DFA14 : DFA
    {
        public DFA14(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 14;
            this.eot = DFA14_eot;
            this.eof = DFA14_eof;
            this.min = DFA14_min;
            this.max = DFA14_max;
            this.accept = DFA14_accept;
            this.special = DFA14_special;
            this.transition = DFA14_transition;

        }

        override public string Description
        {
            get { return "131:1: T_FLOAT : ( ( '0' .. '9' )+ '.' ( '0' .. '9' )* ( FLOAT_EXP )? | '.' ( '0' .. '9' )+ ( FLOAT_EXP )? | ( '0' .. '9' )+ FLOAT_EXP );"; }
        }

    }

    const string DFA24_eotS =
        "\x01\uffff\x04\x22\x01\x2b\x01\x2f\x01\x31\x01\x34\x01\x36\x01"+
        "\x38\x01\uffff\x01\x39\x02\uffff\x01\x3a\x02\uffff\x01\x3b\x08\uffff"+
        "\x01\x22\x01\uffff\x01\x22\x01\uffff\x01\x40\x01\x22\x01\x40\x02"+
        "\uffff\x05\x22\x01\x46\x02\uffff\x01\x48\x14\uffff\x05\x22\x03\uffff"+
        "\x07\x22\x01\x55\x01\x22\x01\x57\x01\x58\x01\x22\x01\uffff\x01\x5a"+
        "\x02\uffff\x01\x22\x01\uffff\x04\x22\x01\x60\x01\uffff";
    const string DFA24_eofS =
        "\x61\uffff";
    const string DFA24_minS =
        "\x01\x09\x01\x4f\x03\x4e\x01\x3d\x01\x3c\x02\x3d\x01\x26\x01\x7c"+
        "\x01\uffff\x01\x2d\x02\uffff\x01\x2a\x02\uffff\x01\x30\x08\uffff"+
        "\x01\x27\x01\uffff\x01\x27\x01\uffff\x01\x2e\x01\x27\x01\x2e\x02"+
        "\uffff\x01\x45\x01\x4e\x01\x44\x02\x49\x01\x30\x02\uffff\x01\x3e"+
        "\x14\uffff\x01\x41\x01\x43\x01\x45\x01\x51\x01\x4e\x03\uffff\x01"+
        "\x54\x01\x55\x01\x58\x01\x55\x01\x47\x01\x45\x01\x52\x01\x30\x01"+
        "\x45\x02\x30\x01\x52\x01\uffff\x01\x30\x02\uffff\x01\x45\x01\uffff"+
        "\x01\x4e\x01\x54\x01\x4c\x01\x59\x01\x30\x01\uffff";
    const string DFA24_maxS =
        "\x01\x7e\x01\x72\x01\x6e\x01\x73\x01\x6e\x01\x3d\x01\x3e\x01\x3d"+
        "\x01\x3e\x01\x26\x01\x7c\x01\uffff\x01\x2d\x02\uffff\x01\x2a\x02"+
        "\uffff\x01\x39\x08\uffff\x01\x27\x01\uffff\x01\x27\x01\uffff\x01"+
        "\x65\x01\x27\x01\x65\x02\uffff\x01\x65\x01\x6e\x01\x64\x02\x69\x01"+
        "\x7a\x02\uffff\x01\x3e\x14\uffff\x01\x61\x01\x63\x01\x65\x01\x71"+
        "\x01\x6e\x03\uffff\x01\x74\x01\x75\x01\x78\x01\x75\x01\x67\x01\x65"+
        "\x01\x72\x01\x7a\x01\x65\x02\x7a\x01\x72\x01\uffff\x01\x7a\x02\uffff"+
        "\x01\x65\x01\uffff\x01\x6e\x01\x74\x01\x6c\x01\x79\x01\x7a\x01\uffff";
    const string DFA24_acceptS =
        "\x0b\uffff\x01\x16\x01\uffff\x01\x18\x01\x19\x01\uffff\x01\x1b"+
        "\x01\x1c\x01\uffff\x01\x1e\x01\x1f\x01\x20\x01\x21\x01\x23\x01\x24"+
        "\x01\x25\x01\x26\x01\uffff\x01\x28\x01\uffff\x01\x2a\x03\uffff\x01"+
        "\x27\x01\x2f\x06\uffff\x01\x09\x01\x07\x01\uffff\x01\x0b\x01\x10"+
        "\x01\x0c\x01\x0a\x01\x22\x01\x0f\x01\x11\x01\x0e\x01\x13\x01\x12"+
        "\x01\x15\x01\x14\x01\x17\x01\x1a\x01\x1d\x01\x2c\x01\x29\x01\x2d"+
        "\x01\x2e\x01\x2b\x05\uffff\x01\x06\x01\x08\x01\x0d\x0c\uffff\x01"+
        "\x02\x01\uffff\x01\x05\x01\x01\x01\uffff\x01\x03\x05\uffff\x01\x04";
    const string DFA24_specialS =
        "\x61\uffff}>";
    static readonly string[] DFA24_transitionS = {
            "\x02\x23\x01\uffff\x02\x23\x12\uffff\x01\x23\x01\x07\x01\x1c"+
            "\x01\uffff\x01\x19\x01\x10\x01\x09\x01\x1e\x01\x14\x01\x15\x01"+
            "\x0e\x01\x0b\x01\x13\x01\x0c\x01\x12\x01\x0f\x01\x1f\x09\x21"+
            "\x01\x17\x01\x11\x01\x06\x01\x05\x01\x08\x01\x16\x01\x18\x02"+
            "\x22\x01\x01\x05\x22\x01\x02\x04\x22\x01\x1b\x01\x04\x05\x22"+
            "\x01\x03\x02\x22\x01\x1d\x02\x22\x03\uffff\x01\x1a\x01\x22\x01"+
            "\uffff\x01\x22\x01\x20\x01\x01\x05\x22\x01\x02\x04\x22\x01\x1b"+
            "\x01\x04\x05\x22\x01\x03\x02\x22\x01\x1d\x02\x22\x01\uffff\x01"+
            "\x0a\x01\uffff\x01\x0d",
            "\x01\x25\x02\uffff\x01\x24\x1c\uffff\x01\x25\x02\uffff\x01"+
            "\x24",
            "\x01\x26\x1f\uffff\x01\x26",
            "\x01\x27\x04\uffff\x01\x28\x1a\uffff\x01\x27\x04\uffff\x01"+
            "\x28",
            "\x01\x29\x1f\uffff\x01\x29",
            "\x01\x2a",
            "\x01\x2e\x01\x2c\x01\x2d",
            "\x01\x30",
            "\x01\x32\x01\x33",
            "\x01\x35",
            "\x01\x37",
            "",
            "\x01\x23",
            "",
            "",
            "\x01\x23",
            "",
            "",
            "\x0a\x3c",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "\x01\x3d",
            "",
            "\x01\x3e",
            "",
            "\x01\x3c\x01\uffff\x0a\x21\x0b\uffff\x01\x3c\x1c\uffff\x01"+
            "\x3f\x02\uffff\x01\x3c",
            "\x01\x3f",
            "\x01\x3c\x01\uffff\x0a\x21\x0b\uffff\x01\x3c\x1f\uffff\x01"+
            "\x3c",
            "",
            "",
            "\x01\x41\x1f\uffff\x01\x41",
            "\x01\x42\x1f\uffff\x01\x42",
            "\x01\x43\x1f\uffff\x01\x43",
            "\x01\x44\x1f\uffff\x01\x44",
            "\x01\x45\x1f\uffff\x01\x45",
            "\x0a\x22\x07\uffff\x1a\x22\x04\uffff\x01\x22\x01\uffff\x1a"+
            "\x22",
            "",
            "",
            "\x01\x47",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "\x01\x49\x1f\uffff\x01\x49",
            "\x01\x4a\x1f\uffff\x01\x4a",
            "\x01\x4b\x1f\uffff\x01\x4b",
            "\x01\x4c\x1f\uffff\x01\x4c",
            "\x01\x4d\x1f\uffff\x01\x4d",
            "",
            "",
            "",
            "\x01\x4e\x1f\uffff\x01\x4e",
            "\x01\x4f\x1f\uffff\x01\x4f",
            "\x01\x50\x1f\uffff\x01\x50",
            "\x01\x51\x1f\uffff\x01\x51",
            "\x01\x52\x1f\uffff\x01\x52",
            "\x01\x53\x1f\uffff\x01\x53",
            "\x01\x54\x1f\uffff\x01\x54",
            "\x0a\x22\x07\uffff\x1a\x22\x04\uffff\x01\x22\x01\uffff\x1a"+
            "\x22",
            "\x01\x56\x1f\uffff\x01\x56",
            "\x0a\x22\x07\uffff\x1a\x22\x04\uffff\x01\x22\x01\uffff\x1a"+
            "\x22",
            "\x0a\x22\x07\uffff\x1a\x22\x04\uffff\x01\x22\x01\uffff\x1a"+
            "\x22",
            "\x01\x59\x1f\uffff\x01\x59",
            "",
            "\x0a\x22\x07\uffff\x1a\x22\x04\uffff\x01\x22\x01\uffff\x1a"+
            "\x22",
            "",
            "",
            "\x01\x5b\x1f\uffff\x01\x5b",
            "",
            "\x01\x5c\x1f\uffff\x01\x5c",
            "\x01\x5d\x1f\uffff\x01\x5d",
            "\x01\x5e\x1f\uffff\x01\x5e",
            "\x01\x5f\x1f\uffff\x01\x5f",
            "\x0a\x22\x07\uffff\x1a\x22\x04\uffff\x01\x22\x01\uffff\x1a"+
            "\x22",
            ""
    };

    static readonly short[] DFA24_eot = DFA.UnpackEncodedString(DFA24_eotS);
    static readonly short[] DFA24_eof = DFA.UnpackEncodedString(DFA24_eofS);
    static readonly char[] DFA24_min = DFA.UnpackEncodedStringToUnsignedChars(DFA24_minS);
    static readonly char[] DFA24_max = DFA.UnpackEncodedStringToUnsignedChars(DFA24_maxS);
    static readonly short[] DFA24_accept = DFA.UnpackEncodedString(DFA24_acceptS);
    static readonly short[] DFA24_special = DFA.UnpackEncodedString(DFA24_specialS);
    static readonly short[][] DFA24_transition = DFA.UnpackEncodedStringArray(DFA24_transitionS);

    protected class DFA24 : DFA
    {
        public DFA24(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 24;
            this.eot = DFA24_eot;
            this.eof = DFA24_eof;
            this.min = DFA24_min;
            this.max = DFA24_max;
            this.accept = DFA24_accept;
            this.special = DFA24_special;
            this.transition = DFA24_transition;

        }

        override public string Description
        {
            get { return "1:1: Tokens : ( CREATE | INDEX | UNIQUE | CONCURRENTLY | USING | ON | EQUALS | SAFEEQUALS | EQUALS2 | NOT_EQUALS | NOT_EQUALS2 | LESS | LESS_OR_EQ | GREATER | GREATER_OR_EQ | SHIFT_LEFT | SHIFT_RIGHT | AMPERSAND | DOUBLE_AMPERSAND | PIPE | DOUBLE_PIPE | PLUS | MINUS | TILDA | ASTERISK | SLASH | PERCENT | SEMI | DOT | COMMA | LPAREN | RPAREN | QUESTION | EXCLAMATION | COLON | AT | DOLLAR | ARROW_UP | T_IDENT | T_QUOTED_IDENT | T_NSTRING | T_STRING | T_INTEGER | T_FLOAT | T_BLOB | T_BINARYNUM | WHITESPACE );"; }
        }

    }

 
    
}
