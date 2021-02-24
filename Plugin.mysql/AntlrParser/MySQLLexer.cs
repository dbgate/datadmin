// $ANTLR 3.2 Sep 23, 2009 12:02:23 ..\\Plugin.mysql\\AntlrParser\\MySQL.g 2012-11-12 20:29:20

// The variable 'variable' is assigned but its value is never used.
#pragma warning disable 168, 219
// Unreachable code detected.
#pragma warning disable 162


using System;
using Antlr.Runtime;
using IList 		= System.Collections.IList;
using ArrayList 	= System.Collections.ArrayList;
using Stack 		= Antlr.Runtime.Collections.StackList;


public partial class MySQLLexer : Lexer {
    public const int GEOMETRYCOLLECTION = 100;
    public const int CASCADED = 137;
    public const int ROW = 292;
    public const int INSERT_METHOD = 110;
    public const int MOD = 195;
    public const int FIXED = 43;
    public const int T_IDENT = 31;
    public const int NOT = 33;
    public const int MULTILINESTRING = 98;
    public const int F_INC = 5;
    public const int EXCEPT = 267;
    public const int FOREIGN = 22;
    public const int EOF = -1;
    public const int ACTION = 58;
    public const int CHARACTER = 61;
    public const int TYPE = 102;
    public const int RPAREN = 13;
    public const int CREATE = 8;
    public const int EXCLAMATION = 188;
    public const int USING = 23;
    public const int BEGIN = 255;
    public const int REGEXP = 210;
    public const int AUTO_INCREMENT = 39;
    public const int ANALYZE = 251;
    public const int MEDIUMINT = 66;
    public const int CONFLICT = 259;
    public const int ZEROFILL = 60;
    public const int ATTACH = 252;
    public const int LESS_OR_EQ = 204;
    public const int TINYBLOB = 84;
    public const int VIRTUAL = 300;
    public const int D = 224;
    public const int E = 225;
    public const int F = 226;
    public const int G = 227;
    public const int BLOB = 85;
    public const int A = 221;
    public const int B = 222;
    public const int C = 223;
    public const int ASC = 25;
    public const int L = 232;
    public const int M = 233;
    public const int N = 234;
    public const int T_BINARYNUM = 125;
    public const int KEY = 20;
    public const int O = 235;
    public const int TRANSACTION = 296;
    public const int H = 228;
    public const int TEMP = 294;
    public const int I = 229;
    public const int ELSE = 184;
    public const int J = 230;
    public const int K = 231;
    public const int U = 241;
    public const int T = 240;
    public const int MEDIUMTEXT = 90;
    public const int W = 243;
    public const int WHITESPACE = 306;
    public const int DEFINER = 131;
    public const int DELAY_KEY_WRITE = 109;
    public const int V = 242;
    public const int INT = 67;
    public const int Q = 237;
    public const int P = 236;
    public const int S = 239;
    public const int R = 238;
    public const int ROLLBACK = 291;
    public const int FAIL = 271;
    public const int RESTRICT = 54;
    public const int Y = 245;
    public const int X = 244;
    public const int TABLESPACE = 121;
    public const int Z = 246;
    public const int REAL = 70;
    public const int GROUP = 163;
    public const int INTERSECT = 279;
    public const int PLAN = 284;
    public const int MIN_ROWS = 114;
    public const int END = 185;
    public const int SQL_CACHE = 158;
    public const int AVG_ROW_LENGTH = 103;
    public const int MAX_ROWS = 113;
    public const int CONSTRAINT = 14;
    public const int SQL_SMALL_RESULT = 155;
    public const int DYNAMIC = 44;
    public const int RENAME = 290;
    public const int SQL_BUFFER_RESULT = 157;
    public const int ALTER = 250;
    public const int OPTION = 139;
    public const int FORCE = 177;
    public const int POINT = 94;
    public const int CURRENT_USER = 178;
    public const int LONGBLOB = 87;
    public const int ISNULL = 281;
    public const int TABLE = 10;
    public const int VARCHAR = 81;
    public const int MULTIPOINT = 97;
    public const int FLOAT = 72;
    public const int COMPACT = 120;
    public const int NOTNULL = 282;
    public const int CHARSET = 104;
    public const int DOUBLE_AMPERSAND = 213;
    public const int MULTIPOLYGON = 99;
    public const int NOT_EQUALS = 207;
    public const int T_COMMENT = 304;
    public const int LPAREN = 11;
    public const int ASTERISK = 166;
    public const int GREATER_OR_EQ = 202;
    public const int DOUBLE_PIPE = 215;
    public const int STORAGE = 45;
    public const int AT = 179;
    public const int TIME = 76;
    public const int AS = 136;
    public const int SLASH = 192;
    public const int THEN = 183;
    public const int GEOMETRY = 93;
    public const int OFFSET = 144;
    public const int REPLACE = 129;
    public const int LEFT = 168;
    public const int COLUMN = 257;
    public const int PIPE = 200;
    public const int PLUS = 196;
    public const int EXISTS = 269;
    public const int MODE = 149;
    public const int LIKE = 209;
    public const int ADD = 248;
    public const int COLLATE = 62;
    public const int INTEGER = 68;
    public const int OUTER = 169;
    public const int BY = 142;
    public const int DEFERRABLE = 261;
    public const int LONGTEXT = 91;
    public const int SQL_NO_CACHE = 159;
    public const int TO = 295;
    public const int AMPERSAND = 199;
    public const int SET = 56;
    public const int RIGHT = 173;
    public const int HAVING = 165;
    public const int MINUS = 189;
    public const int TEXT = 89;
    public const int IGNORE = 176;
    public const int SEMI = 217;
    public const int UNION = 122;
    public const int COLON = 219;
    public const int FLOAT_EXP = 303;
    public const int F_NL = 4;
    public const int COMMIT = 258;
    public const int UNSIGNED = 59;
    public const int ENUM = 92;
    public const int DATABASE = 260;
    public const int VACUUM = 298;
    public const int DECIMAL = 73;
    public const int DROP = 264;
    public const int DETACH = 263;
    public const int BIGINT = 69;
    public const int WHEN = 182;
    public const int T_BLOB = 126;
    public const int SQL_BIG_RESULT = 156;
    public const int DIRECTORY = 108;
    public const int BIT = 63;
    public const int NATURAL = 174;
    public const int DIV = 194;
    public const int BETWEEN = 211;
    public const int FIRST = 111;
    public const int CAST = 256;
    public const int T_STRING = 41;
    public const int POLYGON = 96;
    public const int CURRENT_TIME = 301;
    public const int TRIGGER = 297;
    public const int SQL_CALC_FOUND_ROWS = 160;
    public const int CASE = 181;
    public const int CHAR = 80;
    public const int EQUALS = 28;
    public const int T_INTEGER = 24;
    public const int CASCADE = 55;
    public const int RELEASE = 289;
    public const int EXPLAIN = 270;
    public const int CONNECTION = 106;
    public const int SQL = 132;
    public const int GREATER = 203;
    public const int FULL = 50;
    public const int INVOKER = 134;
    public const int INSERT = 277;
    public const int ESCAPE = 266;
    public const int MEDIUMBLOB = 86;
    public const int SAVEPOINT = 293;
    public const int LESS = 205;
    public const int TINYINT = 64;
    public const int DOUBLE = 71;
    public const int SHARE = 148;
    public const int RAISE = 287;
    public const int EACH = 265;
    public const int LAST = 112;
    public const int COMMENT = 40;
    public const int ABORT = 247;
    public const int SELECT = 150;
    public const int INTO = 280;
    public const int CHECKSUM = 105;
    public const int UNIQUE = 18;
    public const int GLOB = 272;
    public const int VIEW = 135;
    public const int LINE_COMMENT = 305;
    public const int SAFEEQUALS = 201;
    public const int SPATIAL = 17;
    public const int ROLLUP = 164;
    public const int NULL = 34;
    public const int NO = 57;
    public const int T_FLOAT = 124;
    public const int TINYTEXT = 88;
    public const int ON = 36;
    public const int T_NSTRING = 123;
    public const int MATCH = 49;
    public const int PRIMARY = 21;
    public const int OJ = 167;
    public const int DELETE = 53;
    public const int LOCAL = 138;
    public const int NUMERIC = 74;
    public const int STRAIGHT_JOIN = 154;
    public const int OF = 283;
    public const int PASSWORD = 116;
    public const int COLUMN_FORMAT = 42;
    public const int SHIFT_LEFT = 197;
    public const int SHIFT_RIGHT = 198;
    public const int ALGORITHM = 130;
    public const int ENGINE = 101;
    public const int OR = 128;
    public const int QUERY = 286;
    public const int CHECK = 15;
    public const int MEMORY = 47;
    public const int USE = 175;
    public const int FROM = 161;
    public const int KEY_BLOCK_SIZE = 27;
    public const int DISTINCT = 151;
    public const int PARSER = 30;
    public const int TEMPORARY = 9;
    public const int CURRENT_DATE = 180;
    public const int TIMESTAMP = 77;
    public const int SIMPLE = 52;
    public const int DISTINCTROW = 152;
    public const int DOLLAR = 220;
    public const int COLUMN_DEF = 7;
    public const int WHERE = 162;
    public const int INNER = 171;
    public const int COMPRESSED = 118;
    public const int UTC_TIMESTAMP = 186;
    public const int YEAR = 79;
    public const int ORDER = 141;
    public const int LIMIT = 143;
    public const int PRAGMA = 285;
    public const int UPDATE = 37;
    public const int FOR = 145;
    public const int DEFERRED = 262;
    public const int EXCLUSIVE = 268;
    public const int AND = 212;
    public const int CROSS = 172;
    public const int INTERVAL = 187;
    public const int LOCK = 146;
    public const int IF = 273;
    public const int INDEX = 19;
    public const int TILDA = 190;
    public const int LINESTRING = 95;
    public const int IN = 147;
    public const int COMMA = 12;
    public const int REFERENCES = 48;
    public const int IS = 208;
    public const int HIGH_PRIORITY = 153;
    public const int ALL = 140;
    public const int REDUNDANT = 119;
    public const int FULLTEXT = 16;
    public const int PACK_KEYS = 115;
    public const int DOT = 127;
    public const int CURRENT_TIMESTAMP = 38;
    public const int SECURITY = 133;
    public const int PARTIAL = 51;
    public const int WITH = 29;
    public const int INITIALLY = 276;
    public const int REINDEX = 288;
    public const int ARROW_UP = 191;
    public const int XOR = 214;
    public const int EQUALS2 = 216;
    public const int VARBINARY = 83;
    public const int PERCENT = 193;
    public const int AUTOINCREMENT = 253;
    public const int ROW_FORMAT = 117;
    public const int NOT_EQUALS2 = 206;
    public const int DATETIME = 78;
    public const int DEFAULT = 35;
    public const int F_DEC = 6;
    public const int VALUES = 299;
    public const int BEFORE = 254;
    public const int AFTER = 249;
    public const int INSTEAD = 278;
    public const int JOIN = 170;
    public const int T_QUOTED_IDENT = 32;
    public const int INDEXED = 275;
    public const int QUESTION = 218;
    public const int DISK = 46;
    public const int IMMEDIATE = 274;
    public const int DESC = 26;
    public const int BINARY = 82;
    public const int DATE = 75;
    public const int ID_START = 302;
    public const int DATA = 107;
    public const int SMALLINT = 65;

    // delegates
    // delegators

    public MySQLLexer() 
    {
		InitializeCyclicDFAs();
    }
    public MySQLLexer(ICharStream input)
		: this(input, null) {
    }
    public MySQLLexer(ICharStream input, RecognizerSharedState state)
		: base(input, state) {
		InitializeCyclicDFAs(); 

    }
    
    override public string GrammarFileName
    {
    	get { return "..\\Plugin.mysql\\AntlrParser\\MySQL.g";} 
    }

    // $ANTLR start "EQUALS"
    public void mEQUALS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = EQUALS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:377:7: ( '=' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:377:16: '='
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:378:11: ( '<=>' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:378:16: '<=>'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:379:8: ( '==' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:379:16: '=='
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:380:11: ( '!=' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:380:16: '!='
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:381:12: ( '<>' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:381:16: '<>'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:382:5: ( '<' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:382:16: '<'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:383:11: ( '<=' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:383:16: '<='
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:384:8: ( '>' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:384:16: '>'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:385:14: ( '>=' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:385:16: '>='
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:386:11: ( '<<' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:386:16: '<<'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:387:12: ( '>>' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:387:16: '>>'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:388:10: ( '&' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:388:16: '&'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:389:17: ( '&&' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:389:19: '&&'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:390:5: ( '|' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:390:16: '|'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:391:12: ( '||' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:391:16: '||'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:392:5: ( '+' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:392:16: '+'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:393:6: ( '-' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:393:16: '-'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:394:6: ( '~' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:394:16: '~'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:395:9: ( '*' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:395:16: '*'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:396:6: ( '/' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:396:16: '/'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:397:8: ( '%' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:397:16: '%'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:398:5: ( ';' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:398:16: ';'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:399:4: ( '.' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:399:16: '.'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:400:6: ( ',' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:400:16: ','
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:401:7: ( '(' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:401:16: '('
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:402:7: ( ')' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:402:16: ')'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:403:9: ( '?' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:403:16: '?'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:404:12: ( '!' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:404:16: '!'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:405:6: ( ':' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:405:16: ':'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:406:3: ( '@' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:406:16: '@'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:407:7: ( '$' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:407:16: '$'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:408:9: ( '^' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:408:16: '^'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:420:11: ( ( 'a' | 'A' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:420:12: ( 'a' | 'A' )
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:421:11: ( ( 'b' | 'B' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:421:12: ( 'b' | 'B' )
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:422:11: ( ( 'c' | 'C' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:422:12: ( 'c' | 'C' )
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:423:11: ( ( 'd' | 'D' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:423:12: ( 'd' | 'D' )
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:424:11: ( ( 'e' | 'E' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:424:12: ( 'e' | 'E' )
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:425:11: ( ( 'f' | 'F' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:425:12: ( 'f' | 'F' )
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:426:11: ( ( 'g' | 'G' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:426:12: ( 'g' | 'G' )
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:427:11: ( ( 'h' | 'H' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:427:12: ( 'h' | 'H' )
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:428:11: ( ( 'i' | 'I' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:428:12: ( 'i' | 'I' )
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:429:11: ( ( 'j' | 'J' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:429:12: ( 'j' | 'J' )
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:430:11: ( ( 'k' | 'K' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:430:12: ( 'k' | 'K' )
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:431:11: ( ( 'l' | 'L' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:431:12: ( 'l' | 'L' )
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:432:11: ( ( 'm' | 'M' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:432:12: ( 'm' | 'M' )
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:433:11: ( ( 'n' | 'N' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:433:12: ( 'n' | 'N' )
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:434:11: ( ( 'o' | 'O' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:434:12: ( 'o' | 'O' )
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:435:11: ( ( 'p' | 'P' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:435:12: ( 'p' | 'P' )
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:436:11: ( ( 'q' | 'Q' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:436:12: ( 'q' | 'Q' )
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:437:11: ( ( 'r' | 'R' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:437:12: ( 'r' | 'R' )
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:438:11: ( ( 's' | 'S' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:438:12: ( 's' | 'S' )
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:439:11: ( ( 't' | 'T' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:439:12: ( 't' | 'T' )
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:440:11: ( ( 'u' | 'U' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:440:12: ( 'u' | 'U' )
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:441:11: ( ( 'v' | 'V' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:441:12: ( 'v' | 'V' )
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:442:11: ( ( 'w' | 'W' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:442:12: ( 'w' | 'W' )
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:443:11: ( ( 'x' | 'X' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:443:12: ( 'x' | 'X' )
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:444:11: ( ( 'y' | 'Y' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:444:12: ( 'y' | 'Y' )
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:445:11: ( ( 'z' | 'Z' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:445:12: ( 'z' | 'Z' )
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

    // $ANTLR start "ABORT"
    public void mABORT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ABORT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:448:7: ( A B O R T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:448:9: A B O R T
            {
            	mA(); 
            	mB(); 
            	mO(); 
            	mR(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ABORT"

    // $ANTLR start "ADD"
    public void mADD() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ADD;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:449:5: ( A D D )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:449:7: A D D
            {
            	mA(); 
            	mD(); 
            	mD(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ADD"

    // $ANTLR start "AFTER"
    public void mAFTER() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = AFTER;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:450:7: ( A F T E R )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:450:9: A F T E R
            {
            	mA(); 
            	mF(); 
            	mT(); 
            	mE(); 
            	mR(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "AFTER"

    // $ANTLR start "ALL"
    public void mALL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ALL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:451:5: ( A L L )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:451:7: A L L
            {
            	mA(); 
            	mL(); 
            	mL(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ALL"

    // $ANTLR start "ALTER"
    public void mALTER() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ALTER;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:452:7: ( A L T E R )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:452:9: A L T E R
            {
            	mA(); 
            	mL(); 
            	mT(); 
            	mE(); 
            	mR(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ALTER"

    // $ANTLR start "ANALYZE"
    public void mANALYZE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ANALYZE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:453:9: ( A N A L Y Z E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:453:11: A N A L Y Z E
            {
            	mA(); 
            	mN(); 
            	mA(); 
            	mL(); 
            	mY(); 
            	mZ(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ANALYZE"

    // $ANTLR start "AND"
    public void mAND() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = AND;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:454:5: ( A N D )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:454:7: A N D
            {
            	mA(); 
            	mN(); 
            	mD(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "AND"

    // $ANTLR start "AS"
    public void mAS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = AS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:455:4: ( A S )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:455:6: A S
            {
            	mA(); 
            	mS(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "AS"

    // $ANTLR start "ASC"
    public void mASC() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ASC;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:456:5: ( A S C )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:456:7: A S C
            {
            	mA(); 
            	mS(); 
            	mC(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ASC"

    // $ANTLR start "ATTACH"
    public void mATTACH() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ATTACH;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:457:8: ( A T T A C H )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:457:10: A T T A C H
            {
            	mA(); 
            	mT(); 
            	mT(); 
            	mA(); 
            	mC(); 
            	mH(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ATTACH"

    // $ANTLR start "AUTOINCREMENT"
    public void mAUTOINCREMENT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = AUTOINCREMENT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:458:15: ( A U T O I N C R E M E N T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:458:17: A U T O I N C R E M E N T
            {
            	mA(); 
            	mU(); 
            	mT(); 
            	mO(); 
            	mI(); 
            	mN(); 
            	mC(); 
            	mR(); 
            	mE(); 
            	mM(); 
            	mE(); 
            	mN(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "AUTOINCREMENT"

    // $ANTLR start "BEFORE"
    public void mBEFORE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = BEFORE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:459:8: ( B E F O R E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:459:10: B E F O R E
            {
            	mB(); 
            	mE(); 
            	mF(); 
            	mO(); 
            	mR(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "BEFORE"

    // $ANTLR start "BEGIN"
    public void mBEGIN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = BEGIN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:460:7: ( B E G I N )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:460:9: B E G I N
            {
            	mB(); 
            	mE(); 
            	mG(); 
            	mI(); 
            	mN(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "BEGIN"

    // $ANTLR start "BETWEEN"
    public void mBETWEEN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = BETWEEN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:461:9: ( B E T W E E N )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:461:11: B E T W E E N
            {
            	mB(); 
            	mE(); 
            	mT(); 
            	mW(); 
            	mE(); 
            	mE(); 
            	mN(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "BETWEEN"

    // $ANTLR start "BY"
    public void mBY() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = BY;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:462:4: ( B Y )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:462:6: B Y
            {
            	mB(); 
            	mY(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "BY"

    // $ANTLR start "CASCADE"
    public void mCASCADE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CASCADE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:463:9: ( C A S C A D E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:463:11: C A S C A D E
            {
            	mC(); 
            	mA(); 
            	mS(); 
            	mC(); 
            	mA(); 
            	mD(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CASCADE"

    // $ANTLR start "CASE"
    public void mCASE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CASE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:464:6: ( C A S E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:464:8: C A S E
            {
            	mC(); 
            	mA(); 
            	mS(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CASE"

    // $ANTLR start "CAST"
    public void mCAST() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CAST;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:465:6: ( C A S T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:465:8: C A S T
            {
            	mC(); 
            	mA(); 
            	mS(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CAST"

    // $ANTLR start "CHECK"
    public void mCHECK() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CHECK;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:466:7: ( C H E C K )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:466:9: C H E C K
            {
            	mC(); 
            	mH(); 
            	mE(); 
            	mC(); 
            	mK(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CHECK"

    // $ANTLR start "COLLATE"
    public void mCOLLATE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = COLLATE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:467:9: ( C O L L A T E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:467:11: C O L L A T E
            {
            	mC(); 
            	mO(); 
            	mL(); 
            	mL(); 
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
    // $ANTLR end "COLLATE"

    // $ANTLR start "COLUMN"
    public void mCOLUMN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = COLUMN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:468:8: ( C O L U M N )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:468:10: C O L U M N
            {
            	mC(); 
            	mO(); 
            	mL(); 
            	mU(); 
            	mM(); 
            	mN(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "COLUMN"

    // $ANTLR start "COMMIT"
    public void mCOMMIT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = COMMIT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:469:8: ( C O M M I T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:469:10: C O M M I T
            {
            	mC(); 
            	mO(); 
            	mM(); 
            	mM(); 
            	mI(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "COMMIT"

    // $ANTLR start "CONFLICT"
    public void mCONFLICT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CONFLICT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:470:10: ( C O N F L I C T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:470:12: C O N F L I C T
            {
            	mC(); 
            	mO(); 
            	mN(); 
            	mF(); 
            	mL(); 
            	mI(); 
            	mC(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CONFLICT"

    // $ANTLR start "CONSTRAINT"
    public void mCONSTRAINT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CONSTRAINT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:471:12: ( C O N S T R A I N T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:471:14: C O N S T R A I N T
            {
            	mC(); 
            	mO(); 
            	mN(); 
            	mS(); 
            	mT(); 
            	mR(); 
            	mA(); 
            	mI(); 
            	mN(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CONSTRAINT"

    // $ANTLR start "CREATE"
    public void mCREATE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CREATE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:472:8: ( C R E A T E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:472:10: C R E A T E
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

    // $ANTLR start "CROSS"
    public void mCROSS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CROSS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:473:7: ( C R O S S )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:473:9: C R O S S
            {
            	mC(); 
            	mR(); 
            	mO(); 
            	mS(); 
            	mS(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CROSS"

    // $ANTLR start "DATABASE"
    public void mDATABASE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DATABASE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:474:10: ( D A T A B A S E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:474:12: D A T A B A S E
            {
            	mD(); 
            	mA(); 
            	mT(); 
            	mA(); 
            	mB(); 
            	mA(); 
            	mS(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DATABASE"

    // $ANTLR start "DEFAULT"
    public void mDEFAULT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DEFAULT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:475:9: ( D E F A U L T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:475:11: D E F A U L T
            {
            	mD(); 
            	mE(); 
            	mF(); 
            	mA(); 
            	mU(); 
            	mL(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DEFAULT"

    // $ANTLR start "DEFERRABLE"
    public void mDEFERRABLE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DEFERRABLE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:476:12: ( D E F E R R A B L E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:476:14: D E F E R R A B L E
            {
            	mD(); 
            	mE(); 
            	mF(); 
            	mE(); 
            	mR(); 
            	mR(); 
            	mA(); 
            	mB(); 
            	mL(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DEFERRABLE"

    // $ANTLR start "DEFERRED"
    public void mDEFERRED() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DEFERRED;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:477:10: ( D E F E R R E D )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:477:12: D E F E R R E D
            {
            	mD(); 
            	mE(); 
            	mF(); 
            	mE(); 
            	mR(); 
            	mR(); 
            	mE(); 
            	mD(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DEFERRED"

    // $ANTLR start "DELETE"
    public void mDELETE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DELETE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:478:8: ( D E L E T E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:478:10: D E L E T E
            {
            	mD(); 
            	mE(); 
            	mL(); 
            	mE(); 
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
    // $ANTLR end "DELETE"

    // $ANTLR start "DESC"
    public void mDESC() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DESC;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:479:6: ( D E S C )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:479:8: D E S C
            {
            	mD(); 
            	mE(); 
            	mS(); 
            	mC(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DESC"

    // $ANTLR start "DETACH"
    public void mDETACH() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DETACH;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:480:8: ( D E T A C H )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:480:10: D E T A C H
            {
            	mD(); 
            	mE(); 
            	mT(); 
            	mA(); 
            	mC(); 
            	mH(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DETACH"

    // $ANTLR start "DISTINCT"
    public void mDISTINCT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DISTINCT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:481:10: ( D I S T I N C T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:481:12: D I S T I N C T
            {
            	mD(); 
            	mI(); 
            	mS(); 
            	mT(); 
            	mI(); 
            	mN(); 
            	mC(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DISTINCT"

    // $ANTLR start "DROP"
    public void mDROP() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DROP;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:482:6: ( D R O P )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:482:8: D R O P
            {
            	mD(); 
            	mR(); 
            	mO(); 
            	mP(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DROP"

    // $ANTLR start "EACH"
    public void mEACH() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = EACH;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:483:6: ( E A C H )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:483:8: E A C H
            {
            	mE(); 
            	mA(); 
            	mC(); 
            	mH(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "EACH"

    // $ANTLR start "ELSE"
    public void mELSE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ELSE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:484:6: ( E L S E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:484:8: E L S E
            {
            	mE(); 
            	mL(); 
            	mS(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ELSE"

    // $ANTLR start "END"
    public void mEND() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = END;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:485:5: ( E N D )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:485:7: E N D
            {
            	mE(); 
            	mN(); 
            	mD(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "END"

    // $ANTLR start "ESCAPE"
    public void mESCAPE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ESCAPE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:486:8: ( E S C A P E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:486:10: E S C A P E
            {
            	mE(); 
            	mS(); 
            	mC(); 
            	mA(); 
            	mP(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ESCAPE"

    // $ANTLR start "EXCEPT"
    public void mEXCEPT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = EXCEPT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:487:8: ( E X C E P T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:487:10: E X C E P T
            {
            	mE(); 
            	mX(); 
            	mC(); 
            	mE(); 
            	mP(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "EXCEPT"

    // $ANTLR start "EXCLUSIVE"
    public void mEXCLUSIVE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = EXCLUSIVE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:488:11: ( E X C L U S I V E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:488:13: E X C L U S I V E
            {
            	mE(); 
            	mX(); 
            	mC(); 
            	mL(); 
            	mU(); 
            	mS(); 
            	mI(); 
            	mV(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "EXCLUSIVE"

    // $ANTLR start "EXISTS"
    public void mEXISTS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = EXISTS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:489:8: ( E X I S T S )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:489:10: E X I S T S
            {
            	mE(); 
            	mX(); 
            	mI(); 
            	mS(); 
            	mT(); 
            	mS(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "EXISTS"

    // $ANTLR start "EXPLAIN"
    public void mEXPLAIN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = EXPLAIN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:490:9: ( E X P L A I N )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:490:11: E X P L A I N
            {
            	mE(); 
            	mX(); 
            	mP(); 
            	mL(); 
            	mA(); 
            	mI(); 
            	mN(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "EXPLAIN"

    // $ANTLR start "FAIL"
    public void mFAIL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = FAIL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:491:6: ( F A I L )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:491:8: F A I L
            {
            	mF(); 
            	mA(); 
            	mI(); 
            	mL(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "FAIL"

    // $ANTLR start "FOR"
    public void mFOR() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = FOR;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:492:5: ( F O R )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:492:7: F O R
            {
            	mF(); 
            	mO(); 
            	mR(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "FOR"

    // $ANTLR start "FOREIGN"
    public void mFOREIGN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = FOREIGN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:493:9: ( F O R E I G N )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:493:11: F O R E I G N
            {
            	mF(); 
            	mO(); 
            	mR(); 
            	mE(); 
            	mI(); 
            	mG(); 
            	mN(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "FOREIGN"

    // $ANTLR start "FROM"
    public void mFROM() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = FROM;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:494:6: ( F R O M )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:494:8: F R O M
            {
            	mF(); 
            	mR(); 
            	mO(); 
            	mM(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "FROM"

    // $ANTLR start "GLOB"
    public void mGLOB() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = GLOB;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:495:6: ( G L O B )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:495:8: G L O B
            {
            	mG(); 
            	mL(); 
            	mO(); 
            	mB(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "GLOB"

    // $ANTLR start "GROUP"
    public void mGROUP() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = GROUP;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:496:7: ( G R O U P )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:496:9: G R O U P
            {
            	mG(); 
            	mR(); 
            	mO(); 
            	mU(); 
            	mP(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "GROUP"

    // $ANTLR start "HAVING"
    public void mHAVING() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = HAVING;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:497:8: ( H A V I N G )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:497:10: H A V I N G
            {
            	mH(); 
            	mA(); 
            	mV(); 
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
    // $ANTLR end "HAVING"

    // $ANTLR start "IF"
    public void mIF() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = IF;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:498:4: ( I F )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:498:6: I F
            {
            	mI(); 
            	mF(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "IF"

    // $ANTLR start "IGNORE"
    public void mIGNORE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = IGNORE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:499:8: ( I G N O R E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:499:10: I G N O R E
            {
            	mI(); 
            	mG(); 
            	mN(); 
            	mO(); 
            	mR(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "IGNORE"

    // $ANTLR start "IMMEDIATE"
    public void mIMMEDIATE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = IMMEDIATE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:500:11: ( I M M E D I A T E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:500:13: I M M E D I A T E
            {
            	mI(); 
            	mM(); 
            	mM(); 
            	mE(); 
            	mD(); 
            	mI(); 
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
    // $ANTLR end "IMMEDIATE"

    // $ANTLR start "IN"
    public void mIN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = IN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:501:4: ( I N )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:501:6: I N
            {
            	mI(); 
            	mN(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "IN"

    // $ANTLR start "INDEX"
    public void mINDEX() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = INDEX;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:502:7: ( I N D E X )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:502:9: I N D E X
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

    // $ANTLR start "INDEXED"
    public void mINDEXED() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = INDEXED;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:503:9: ( I N D E X E D )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:503:11: I N D E X E D
            {
            	mI(); 
            	mN(); 
            	mD(); 
            	mE(); 
            	mX(); 
            	mE(); 
            	mD(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "INDEXED"

    // $ANTLR start "INITIALLY"
    public void mINITIALLY() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = INITIALLY;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:504:11: ( I N I T I A L L Y )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:504:13: I N I T I A L L Y
            {
            	mI(); 
            	mN(); 
            	mI(); 
            	mT(); 
            	mI(); 
            	mA(); 
            	mL(); 
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
    // $ANTLR end "INITIALLY"

    // $ANTLR start "INNER"
    public void mINNER() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = INNER;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:505:7: ( I N N E R )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:505:9: I N N E R
            {
            	mI(); 
            	mN(); 
            	mN(); 
            	mE(); 
            	mR(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "INNER"

    // $ANTLR start "INSERT"
    public void mINSERT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = INSERT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:506:8: ( I N S E R T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:506:10: I N S E R T
            {
            	mI(); 
            	mN(); 
            	mS(); 
            	mE(); 
            	mR(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "INSERT"

    // $ANTLR start "INSTEAD"
    public void mINSTEAD() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = INSTEAD;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:507:9: ( I N S T E A D )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:507:11: I N S T E A D
            {
            	mI(); 
            	mN(); 
            	mS(); 
            	mT(); 
            	mE(); 
            	mA(); 
            	mD(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "INSTEAD"

    // $ANTLR start "INTERSECT"
    public void mINTERSECT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = INTERSECT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:508:11: ( I N T E R S E C T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:508:13: I N T E R S E C T
            {
            	mI(); 
            	mN(); 
            	mT(); 
            	mE(); 
            	mR(); 
            	mS(); 
            	mE(); 
            	mC(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "INTERSECT"

    // $ANTLR start "INTO"
    public void mINTO() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = INTO;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:509:6: ( I N T O )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:509:8: I N T O
            {
            	mI(); 
            	mN(); 
            	mT(); 
            	mO(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "INTO"

    // $ANTLR start "IS"
    public void mIS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = IS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:510:4: ( I S )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:510:6: I S
            {
            	mI(); 
            	mS(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "IS"

    // $ANTLR start "ISNULL"
    public void mISNULL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ISNULL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:511:8: ( I S N U L L )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:511:10: I S N U L L
            {
            	mI(); 
            	mS(); 
            	mN(); 
            	mU(); 
            	mL(); 
            	mL(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ISNULL"

    // $ANTLR start "JOIN"
    public void mJOIN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = JOIN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:512:6: ( J O I N )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:512:8: J O I N
            {
            	mJ(); 
            	mO(); 
            	mI(); 
            	mN(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "JOIN"

    // $ANTLR start "KEY"
    public void mKEY() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = KEY;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:513:5: ( K E Y )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:513:7: K E Y
            {
            	mK(); 
            	mE(); 
            	mY(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "KEY"

    // $ANTLR start "LEFT"
    public void mLEFT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LEFT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:514:6: ( L E F T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:514:8: L E F T
            {
            	mL(); 
            	mE(); 
            	mF(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LEFT"

    // $ANTLR start "LIKE"
    public void mLIKE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LIKE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:515:6: ( L I K E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:515:8: L I K E
            {
            	mL(); 
            	mI(); 
            	mK(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LIKE"

    // $ANTLR start "LIMIT"
    public void mLIMIT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LIMIT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:516:7: ( L I M I T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:516:9: L I M I T
            {
            	mL(); 
            	mI(); 
            	mM(); 
            	mI(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LIMIT"

    // $ANTLR start "MATCH"
    public void mMATCH() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = MATCH;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:517:7: ( M A T C H )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:517:9: M A T C H
            {
            	mM(); 
            	mA(); 
            	mT(); 
            	mC(); 
            	mH(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "MATCH"

    // $ANTLR start "NATURAL"
    public void mNATURAL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = NATURAL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:518:9: ( N A T U R A L )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:518:11: N A T U R A L
            {
            	mN(); 
            	mA(); 
            	mT(); 
            	mU(); 
            	mR(); 
            	mA(); 
            	mL(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "NATURAL"

    // $ANTLR start "NOT"
    public void mNOT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = NOT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:519:5: ( N O T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:519:7: N O T
            {
            	mN(); 
            	mO(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "NOT"

    // $ANTLR start "NOTNULL"
    public void mNOTNULL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = NOTNULL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:520:9: ( N O T N U L L )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:520:11: N O T N U L L
            {
            	mN(); 
            	mO(); 
            	mT(); 
            	mN(); 
            	mU(); 
            	mL(); 
            	mL(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "NOTNULL"

    // $ANTLR start "NULL"
    public void mNULL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = NULL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:521:6: ( N U L L )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:521:8: N U L L
            {
            	mN(); 
            	mU(); 
            	mL(); 
            	mL(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "NULL"

    // $ANTLR start "OF"
    public void mOF() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = OF;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:522:4: ( O F )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:522:6: O F
            {
            	mO(); 
            	mF(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "OF"

    // $ANTLR start "OFFSET"
    public void mOFFSET() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = OFFSET;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:523:8: ( O F F S E T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:523:10: O F F S E T
            {
            	mO(); 
            	mF(); 
            	mF(); 
            	mS(); 
            	mE(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "OFFSET"

    // $ANTLR start "ON"
    public void mON() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ON;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:524:4: ( O N )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:524:6: O N
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

    // $ANTLR start "OR"
    public void mOR() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = OR;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:525:4: ( O R )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:525:6: O R
            {
            	mO(); 
            	mR(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "OR"

    // $ANTLR start "ORDER"
    public void mORDER() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ORDER;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:526:7: ( O R D E R )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:526:9: O R D E R
            {
            	mO(); 
            	mR(); 
            	mD(); 
            	mE(); 
            	mR(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ORDER"

    // $ANTLR start "OUTER"
    public void mOUTER() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = OUTER;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:527:7: ( O U T E R )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:527:9: O U T E R
            {
            	mO(); 
            	mU(); 
            	mT(); 
            	mE(); 
            	mR(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "OUTER"

    // $ANTLR start "PLAN"
    public void mPLAN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = PLAN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:528:6: ( P L A N )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:528:8: P L A N
            {
            	mP(); 
            	mL(); 
            	mA(); 
            	mN(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "PLAN"

    // $ANTLR start "PRAGMA"
    public void mPRAGMA() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = PRAGMA;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:529:8: ( P R A G M A )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:529:10: P R A G M A
            {
            	mP(); 
            	mR(); 
            	mA(); 
            	mG(); 
            	mM(); 
            	mA(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "PRAGMA"

    // $ANTLR start "PRIMARY"
    public void mPRIMARY() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = PRIMARY;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:530:9: ( P R I M A R Y )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:530:11: P R I M A R Y
            {
            	mP(); 
            	mR(); 
            	mI(); 
            	mM(); 
            	mA(); 
            	mR(); 
            	mY(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "PRIMARY"

    // $ANTLR start "QUERY"
    public void mQUERY() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = QUERY;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:531:7: ( Q U E R Y )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:531:9: Q U E R Y
            {
            	mQ(); 
            	mU(); 
            	mE(); 
            	mR(); 
            	mY(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "QUERY"

    // $ANTLR start "RAISE"
    public void mRAISE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = RAISE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:532:7: ( R A I S E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:532:9: R A I S E
            {
            	mR(); 
            	mA(); 
            	mI(); 
            	mS(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "RAISE"

    // $ANTLR start "REFERENCES"
    public void mREFERENCES() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = REFERENCES;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:533:12: ( R E F E R E N C E S )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:533:14: R E F E R E N C E S
            {
            	mR(); 
            	mE(); 
            	mF(); 
            	mE(); 
            	mR(); 
            	mE(); 
            	mN(); 
            	mC(); 
            	mE(); 
            	mS(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "REFERENCES"

    // $ANTLR start "REGEXP"
    public void mREGEXP() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = REGEXP;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:534:8: ( R E G E X P )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:534:10: R E G E X P
            {
            	mR(); 
            	mE(); 
            	mG(); 
            	mE(); 
            	mX(); 
            	mP(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "REGEXP"

    // $ANTLR start "REINDEX"
    public void mREINDEX() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = REINDEX;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:535:9: ( R E I N D E X )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:535:11: R E I N D E X
            {
            	mR(); 
            	mE(); 
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
    // $ANTLR end "REINDEX"

    // $ANTLR start "RELEASE"
    public void mRELEASE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = RELEASE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:536:9: ( R E L E A S E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:536:11: R E L E A S E
            {
            	mR(); 
            	mE(); 
            	mL(); 
            	mE(); 
            	mA(); 
            	mS(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "RELEASE"

    // $ANTLR start "RENAME"
    public void mRENAME() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = RENAME;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:537:8: ( R E N A M E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:537:10: R E N A M E
            {
            	mR(); 
            	mE(); 
            	mN(); 
            	mA(); 
            	mM(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "RENAME"

    // $ANTLR start "REPLACE"
    public void mREPLACE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = REPLACE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:538:9: ( R E P L A C E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:538:11: R E P L A C E
            {
            	mR(); 
            	mE(); 
            	mP(); 
            	mL(); 
            	mA(); 
            	mC(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "REPLACE"

    // $ANTLR start "RESTRICT"
    public void mRESTRICT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = RESTRICT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:539:10: ( R E S T R I C T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:539:12: R E S T R I C T
            {
            	mR(); 
            	mE(); 
            	mS(); 
            	mT(); 
            	mR(); 
            	mI(); 
            	mC(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "RESTRICT"

    // $ANTLR start "ROLLBACK"
    public void mROLLBACK() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ROLLBACK;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:540:10: ( R O L L B A C K )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:540:12: R O L L B A C K
            {
            	mR(); 
            	mO(); 
            	mL(); 
            	mL(); 
            	mB(); 
            	mA(); 
            	mC(); 
            	mK(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ROLLBACK"

    // $ANTLR start "ROW"
    public void mROW() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ROW;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:541:5: ( R O W )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:541:7: R O W
            {
            	mR(); 
            	mO(); 
            	mW(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ROW"

    // $ANTLR start "SAVEPOINT"
    public void mSAVEPOINT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SAVEPOINT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:542:11: ( S A V E P O I N T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:542:13: S A V E P O I N T
            {
            	mS(); 
            	mA(); 
            	mV(); 
            	mE(); 
            	mP(); 
            	mO(); 
            	mI(); 
            	mN(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SAVEPOINT"

    // $ANTLR start "SELECT"
    public void mSELECT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SELECT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:543:8: ( S E L E C T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:543:10: S E L E C T
            {
            	mS(); 
            	mE(); 
            	mL(); 
            	mE(); 
            	mC(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SELECT"

    // $ANTLR start "SET"
    public void mSET() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SET;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:544:5: ( S E T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:544:7: S E T
            {
            	mS(); 
            	mE(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SET"

    // $ANTLR start "TABLE"
    public void mTABLE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = TABLE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:545:7: ( T A B L E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:545:9: T A B L E
            {
            	mT(); 
            	mA(); 
            	mB(); 
            	mL(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "TABLE"

    // $ANTLR start "TEMPORARY"
    public void mTEMPORARY() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = TEMPORARY;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:546:11: ( T E M P O R A R Y )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:546:13: T E M P O R A R Y
            {
            	mT(); 
            	mE(); 
            	mM(); 
            	mP(); 
            	mO(); 
            	mR(); 
            	mA(); 
            	mR(); 
            	mY(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "TEMPORARY"

    // $ANTLR start "TEMP"
    public void mTEMP() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = TEMP;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:547:6: ( T E M P )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:547:8: T E M P
            {
            	mT(); 
            	mE(); 
            	mM(); 
            	mP(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "TEMP"

    // $ANTLR start "THEN"
    public void mTHEN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = THEN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:548:6: ( T H E N )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:548:8: T H E N
            {
            	mT(); 
            	mH(); 
            	mE(); 
            	mN(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "THEN"

    // $ANTLR start "TO"
    public void mTO() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = TO;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:549:4: ( T O )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:549:6: T O
            {
            	mT(); 
            	mO(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "TO"

    // $ANTLR start "TRANSACTION"
    public void mTRANSACTION() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = TRANSACTION;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:550:13: ( T R A N S A C T I O N )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:550:15: T R A N S A C T I O N
            {
            	mT(); 
            	mR(); 
            	mA(); 
            	mN(); 
            	mS(); 
            	mA(); 
            	mC(); 
            	mT(); 
            	mI(); 
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
    // $ANTLR end "TRANSACTION"

    // $ANTLR start "TRIGGER"
    public void mTRIGGER() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = TRIGGER;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:551:9: ( T R I G G E R )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:551:11: T R I G G E R
            {
            	mT(); 
            	mR(); 
            	mI(); 
            	mG(); 
            	mG(); 
            	mE(); 
            	mR(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "TRIGGER"

    // $ANTLR start "UNION"
    public void mUNION() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = UNION;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:552:7: ( U N I O N )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:552:9: U N I O N
            {
            	mU(); 
            	mN(); 
            	mI(); 
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
    // $ANTLR end "UNION"

    // $ANTLR start "UNIQUE"
    public void mUNIQUE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = UNIQUE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:553:8: ( U N I Q U E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:553:10: U N I Q U E
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

    // $ANTLR start "UPDATE"
    public void mUPDATE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = UPDATE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:554:8: ( U P D A T E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:554:10: U P D A T E
            {
            	mU(); 
            	mP(); 
            	mD(); 
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
    // $ANTLR end "UPDATE"

    // $ANTLR start "USING"
    public void mUSING() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = USING;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:555:7: ( U S I N G )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:555:9: U S I N G
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

    // $ANTLR start "VACUUM"
    public void mVACUUM() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = VACUUM;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:556:8: ( V A C U U M )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:556:10: V A C U U M
            {
            	mV(); 
            	mA(); 
            	mC(); 
            	mU(); 
            	mU(); 
            	mM(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "VACUUM"

    // $ANTLR start "VALUES"
    public void mVALUES() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = VALUES;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:557:8: ( V A L U E S )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:557:10: V A L U E S
            {
            	mV(); 
            	mA(); 
            	mL(); 
            	mU(); 
            	mE(); 
            	mS(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "VALUES"

    // $ANTLR start "VIEW"
    public void mVIEW() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = VIEW;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:558:6: ( V I E W )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:558:8: V I E W
            {
            	mV(); 
            	mI(); 
            	mE(); 
            	mW(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "VIEW"

    // $ANTLR start "VIRTUAL"
    public void mVIRTUAL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = VIRTUAL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:559:9: ( V I R T U A L )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:559:11: V I R T U A L
            {
            	mV(); 
            	mI(); 
            	mR(); 
            	mT(); 
            	mU(); 
            	mA(); 
            	mL(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "VIRTUAL"

    // $ANTLR start "WHEN"
    public void mWHEN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = WHEN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:560:6: ( W H E N )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:560:8: W H E N
            {
            	mW(); 
            	mH(); 
            	mE(); 
            	mN(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "WHEN"

    // $ANTLR start "WHERE"
    public void mWHERE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = WHERE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:561:7: ( W H E R E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:561:9: W H E R E
            {
            	mW(); 
            	mH(); 
            	mE(); 
            	mR(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "WHERE"

    // $ANTLR start "DIV"
    public void mDIV() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DIV;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:562:5: ( D I V )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:562:7: D I V
            {
            	mD(); 
            	mI(); 
            	mV(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DIV"

    // $ANTLR start "MOD"
    public void mMOD() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = MOD;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:563:5: ( M O D )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:563:7: M O D
            {
            	mM(); 
            	mO(); 
            	mD(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "MOD"

    // $ANTLR start "FULLTEXT"
    public void mFULLTEXT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = FULLTEXT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:564:10: ( F U L L T E X T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:564:12: F U L L T E X T
            {
            	mF(); 
            	mU(); 
            	mL(); 
            	mL(); 
            	mT(); 
            	mE(); 
            	mX(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "FULLTEXT"

    // $ANTLR start "SPATIAL"
    public void mSPATIAL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SPATIAL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:565:9: ( S P A T I A L )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:565:11: S P A T I A L
            {
            	mS(); 
            	mP(); 
            	mA(); 
            	mT(); 
            	mI(); 
            	mA(); 
            	mL(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SPATIAL"

    // $ANTLR start "KEY_BLOCK_SIZE"
    public void mKEY_BLOCK_SIZE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = KEY_BLOCK_SIZE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:566:16: ( K E Y '_' B L O C K '_' S I Z E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:566:18: K E Y '_' B L O C K '_' S I Z E
            {
            	mK(); 
            	mE(); 
            	mY(); 
            	Match('_'); 
            	mB(); 
            	mL(); 
            	mO(); 
            	mC(); 
            	mK(); 
            	Match('_'); 
            	mS(); 
            	mI(); 
            	mZ(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "KEY_BLOCK_SIZE"

    // $ANTLR start "WITH"
    public void mWITH() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = WITH;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:567:6: ( W I T H )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:567:8: W I T H
            {
            	mW(); 
            	mI(); 
            	mT(); 
            	mH(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "WITH"

    // $ANTLR start "PARSER"
    public void mPARSER() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = PARSER;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:568:8: ( P A R S E R )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:568:10: P A R S E R
            {
            	mP(); 
            	mA(); 
            	mR(); 
            	mS(); 
            	mE(); 
            	mR(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "PARSER"

    // $ANTLR start "XOR"
    public void mXOR() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = XOR;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:569:5: ( X O R )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:569:7: X O R
            {
            	mX(); 
            	mO(); 
            	mR(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "XOR"

    // $ANTLR start "AUTO_INCREMENT"
    public void mAUTO_INCREMENT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = AUTO_INCREMENT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:570:16: ( A U T O '_' I N C R E M E N T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:570:18: A U T O '_' I N C R E M E N T
            {
            	mA(); 
            	mU(); 
            	mT(); 
            	mO(); 
            	Match('_'); 
            	mI(); 
            	mN(); 
            	mC(); 
            	mR(); 
            	mE(); 
            	mM(); 
            	mE(); 
            	mN(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "AUTO_INCREMENT"

    // $ANTLR start "COLUMN_FORMAT"
    public void mCOLUMN_FORMAT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = COLUMN_FORMAT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:571:15: ( C O L U M N '_' F O R M A T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:571:17: C O L U M N '_' F O R M A T
            {
            	mC(); 
            	mO(); 
            	mL(); 
            	mU(); 
            	mM(); 
            	mN(); 
            	Match('_'); 
            	mF(); 
            	mO(); 
            	mR(); 
            	mM(); 
            	mA(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "COLUMN_FORMAT"

    // $ANTLR start "FIXED"
    public void mFIXED() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = FIXED;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:572:7: ( F I X E D )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:572:9: F I X E D
            {
            	mF(); 
            	mI(); 
            	mX(); 
            	mE(); 
            	mD(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "FIXED"

    // $ANTLR start "DYNAMIC"
    public void mDYNAMIC() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DYNAMIC;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:573:9: ( D Y N A M I C )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:573:11: D Y N A M I C
            {
            	mD(); 
            	mY(); 
            	mN(); 
            	mA(); 
            	mM(); 
            	mI(); 
            	mC(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DYNAMIC"

    // $ANTLR start "STORAGE"
    public void mSTORAGE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = STORAGE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:574:9: ( S T O R A G E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:574:11: S T O R A G E
            {
            	mS(); 
            	mT(); 
            	mO(); 
            	mR(); 
            	mA(); 
            	mG(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "STORAGE"

    // $ANTLR start "DISK"
    public void mDISK() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DISK;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:575:6: ( D I S K )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:575:8: D I S K
            {
            	mD(); 
            	mI(); 
            	mS(); 
            	mK(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DISK"

    // $ANTLR start "MEMORY"
    public void mMEMORY() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = MEMORY;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:576:8: ( M E M O R Y )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:576:10: M E M O R Y
            {
            	mM(); 
            	mE(); 
            	mM(); 
            	mO(); 
            	mR(); 
            	mY(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "MEMORY"

    // $ANTLR start "FULL"
    public void mFULL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = FULL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:577:6: ( F U L L )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:577:8: F U L L
            {
            	mF(); 
            	mU(); 
            	mL(); 
            	mL(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "FULL"

    // $ANTLR start "PARTIAL"
    public void mPARTIAL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = PARTIAL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:578:9: ( P A R T I A L )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:578:11: P A R T I A L
            {
            	mP(); 
            	mA(); 
            	mR(); 
            	mT(); 
            	mI(); 
            	mA(); 
            	mL(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "PARTIAL"

    // $ANTLR start "SIMPLE"
    public void mSIMPLE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SIMPLE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:579:8: ( S I M P L E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:579:10: S I M P L E
            {
            	mS(); 
            	mI(); 
            	mM(); 
            	mP(); 
            	mL(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SIMPLE"

    // $ANTLR start "NO"
    public void mNO() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = NO;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:580:4: ( N O )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:580:6: N O
            {
            	mN(); 
            	mO(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "NO"

    // $ANTLR start "ACTION"
    public void mACTION() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ACTION;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:581:8: ( A C T I O N )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:581:10: A C T I O N
            {
            	mA(); 
            	mC(); 
            	mT(); 
            	mI(); 
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
    // $ANTLR end "ACTION"

    // $ANTLR start "ENGINE"
    public void mENGINE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ENGINE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:582:8: ( E N G I N E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:582:10: E N G I N E
            {
            	mE(); 
            	mN(); 
            	mG(); 
            	mI(); 
            	mN(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ENGINE"

    // $ANTLR start "AVG_ROW_LENGTH"
    public void mAVG_ROW_LENGTH() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = AVG_ROW_LENGTH;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:583:16: ( A V G '_' R O W '_' L E N G T H )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:583:18: A V G '_' R O W '_' L E N G T H
            {
            	mA(); 
            	mV(); 
            	mG(); 
            	Match('_'); 
            	mR(); 
            	mO(); 
            	mW(); 
            	Match('_'); 
            	mL(); 
            	mE(); 
            	mN(); 
            	mG(); 
            	mT(); 
            	mH(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "AVG_ROW_LENGTH"

    // $ANTLR start "CHECKSUM"
    public void mCHECKSUM() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CHECKSUM;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:584:10: ( C H E C K S U M )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:584:12: C H E C K S U M
            {
            	mC(); 
            	mH(); 
            	mE(); 
            	mC(); 
            	mK(); 
            	mS(); 
            	mU(); 
            	mM(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CHECKSUM"

    // $ANTLR start "DATA"
    public void mDATA() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DATA;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:585:6: ( D A T A )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:585:8: D A T A
            {
            	mD(); 
            	mA(); 
            	mT(); 
            	mA(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DATA"

    // $ANTLR start "DIRECTORY"
    public void mDIRECTORY() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DIRECTORY;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:586:11: ( D I R E C T O R Y )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:586:13: D I R E C T O R Y
            {
            	mD(); 
            	mI(); 
            	mR(); 
            	mE(); 
            	mC(); 
            	mT(); 
            	mO(); 
            	mR(); 
            	mY(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DIRECTORY"

    // $ANTLR start "DELAY_KEY_WRITE"
    public void mDELAY_KEY_WRITE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DELAY_KEY_WRITE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:587:17: ( D E L A Y '_' K E Y '_' W R I T E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:587:19: D E L A Y '_' K E Y '_' W R I T E
            {
            	mD(); 
            	mE(); 
            	mL(); 
            	mA(); 
            	mY(); 
            	Match('_'); 
            	mK(); 
            	mE(); 
            	mY(); 
            	Match('_'); 
            	mW(); 
            	mR(); 
            	mI(); 
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
    // $ANTLR end "DELAY_KEY_WRITE"

    // $ANTLR start "INSERT_METHOD"
    public void mINSERT_METHOD() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = INSERT_METHOD;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:588:15: ( I N S E R T '_' M E T H O D )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:588:17: I N S E R T '_' M E T H O D
            {
            	mI(); 
            	mN(); 
            	mS(); 
            	mE(); 
            	mR(); 
            	mT(); 
            	Match('_'); 
            	mM(); 
            	mE(); 
            	mT(); 
            	mH(); 
            	mO(); 
            	mD(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "INSERT_METHOD"

    // $ANTLR start "FIRST"
    public void mFIRST() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = FIRST;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:589:7: ( F I R S T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:589:9: F I R S T
            {
            	mF(); 
            	mI(); 
            	mR(); 
            	mS(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "FIRST"

    // $ANTLR start "LAST"
    public void mLAST() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LAST;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:590:6: ( L A S T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:590:8: L A S T
            {
            	mL(); 
            	mA(); 
            	mS(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LAST"

    // $ANTLR start "MAX_ROWS"
    public void mMAX_ROWS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = MAX_ROWS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:591:10: ( M A X '_' R O W S )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:591:12: M A X '_' R O W S
            {
            	mM(); 
            	mA(); 
            	mX(); 
            	Match('_'); 
            	mR(); 
            	mO(); 
            	mW(); 
            	mS(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "MAX_ROWS"

    // $ANTLR start "MIN_ROWS"
    public void mMIN_ROWS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = MIN_ROWS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:592:10: ( M I N '_' R O W S )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:592:12: M I N '_' R O W S
            {
            	mM(); 
            	mI(); 
            	mN(); 
            	Match('_'); 
            	mR(); 
            	mO(); 
            	mW(); 
            	mS(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "MIN_ROWS"

    // $ANTLR start "PACK_KEYS"
    public void mPACK_KEYS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = PACK_KEYS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:593:11: ( P A C K '_' K E Y S )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:593:13: P A C K '_' K E Y S
            {
            	mP(); 
            	mA(); 
            	mC(); 
            	mK(); 
            	Match('_'); 
            	mK(); 
            	mE(); 
            	mY(); 
            	mS(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "PACK_KEYS"

    // $ANTLR start "PASSWORD"
    public void mPASSWORD() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = PASSWORD;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:594:10: ( P A S S W O R D )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:594:12: P A S S W O R D
            {
            	mP(); 
            	mA(); 
            	mS(); 
            	mS(); 
            	mW(); 
            	mO(); 
            	mR(); 
            	mD(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "PASSWORD"

    // $ANTLR start "ROW_FORMAT"
    public void mROW_FORMAT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ROW_FORMAT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:595:12: ( R O W '_' F O R M A T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:595:14: R O W '_' F O R M A T
            {
            	mR(); 
            	mO(); 
            	mW(); 
            	Match('_'); 
            	mF(); 
            	mO(); 
            	mR(); 
            	mM(); 
            	mA(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ROW_FORMAT"

    // $ANTLR start "COMPRESSED"
    public void mCOMPRESSED() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = COMPRESSED;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:596:12: ( C O M P R E S S E D )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:596:14: C O M P R E S S E D
            {
            	mC(); 
            	mO(); 
            	mM(); 
            	mP(); 
            	mR(); 
            	mE(); 
            	mS(); 
            	mS(); 
            	mE(); 
            	mD(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "COMPRESSED"

    // $ANTLR start "REDUNDANT"
    public void mREDUNDANT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = REDUNDANT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:597:11: ( R E D U N D A N T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:597:13: R E D U N D A N T
            {
            	mR(); 
            	mE(); 
            	mD(); 
            	mU(); 
            	mN(); 
            	mD(); 
            	mA(); 
            	mN(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "REDUNDANT"

    // $ANTLR start "COMPACT"
    public void mCOMPACT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = COMPACT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:598:9: ( C O M P A C T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:598:11: C O M P A C T
            {
            	mC(); 
            	mO(); 
            	mM(); 
            	mP(); 
            	mA(); 
            	mC(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "COMPACT"

    // $ANTLR start "TABLESPACE"
    public void mTABLESPACE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = TABLESPACE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:599:12: ( T A B L E S P A C E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:599:14: T A B L E S P A C E
            {
            	mT(); 
            	mA(); 
            	mB(); 
            	mL(); 
            	mE(); 
            	mS(); 
            	mP(); 
            	mA(); 
            	mC(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "TABLESPACE"

    // $ANTLR start "CHARSET"
    public void mCHARSET() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CHARSET;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:600:9: ( C H A R S E T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:600:11: C H A R S E T
            {
            	mC(); 
            	mH(); 
            	mA(); 
            	mR(); 
            	mS(); 
            	mE(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CHARSET"

    // $ANTLR start "BIT"
    public void mBIT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = BIT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:601:5: ( B I T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:601:7: B I T
            {
            	mB(); 
            	mI(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "BIT"

    // $ANTLR start "TINYINT"
    public void mTINYINT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = TINYINT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:602:9: ( T I N Y I N T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:602:11: T I N Y I N T
            {
            	mT(); 
            	mI(); 
            	mN(); 
            	mY(); 
            	mI(); 
            	mN(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "TINYINT"

    // $ANTLR start "SMALLINT"
    public void mSMALLINT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SMALLINT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:603:10: ( S M A L L I N T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:603:12: S M A L L I N T
            {
            	mS(); 
            	mM(); 
            	mA(); 
            	mL(); 
            	mL(); 
            	mI(); 
            	mN(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SMALLINT"

    // $ANTLR start "MEDIUMINT"
    public void mMEDIUMINT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = MEDIUMINT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:604:11: ( M E D I U M I N T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:604:13: M E D I U M I N T
            {
            	mM(); 
            	mE(); 
            	mD(); 
            	mI(); 
            	mU(); 
            	mM(); 
            	mI(); 
            	mN(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "MEDIUMINT"

    // $ANTLR start "INT"
    public void mINT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = INT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:605:5: ( I N T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:605:7: I N T
            {
            	mI(); 
            	mN(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "INT"

    // $ANTLR start "INTEGER"
    public void mINTEGER() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = INTEGER;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:606:9: ( I N T E G E R )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:606:11: I N T E G E R
            {
            	mI(); 
            	mN(); 
            	mT(); 
            	mE(); 
            	mG(); 
            	mE(); 
            	mR(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "INTEGER"

    // $ANTLR start "BIGINT"
    public void mBIGINT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = BIGINT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:607:8: ( B I G I N T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:607:10: B I G I N T
            {
            	mB(); 
            	mI(); 
            	mG(); 
            	mI(); 
            	mN(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "BIGINT"

    // $ANTLR start "REAL"
    public void mREAL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = REAL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:608:6: ( R E A L )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:608:8: R E A L
            {
            	mR(); 
            	mE(); 
            	mA(); 
            	mL(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "REAL"

    // $ANTLR start "DOUBLE"
    public void mDOUBLE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DOUBLE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:609:8: ( D O U B L E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:609:10: D O U B L E
            {
            	mD(); 
            	mO(); 
            	mU(); 
            	mB(); 
            	mL(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DOUBLE"

    // $ANTLR start "FLOAT"
    public void mFLOAT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = FLOAT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:610:7: ( F L O A T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:610:9: F L O A T
            {
            	mF(); 
            	mL(); 
            	mO(); 
            	mA(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "FLOAT"

    // $ANTLR start "DECIMAL"
    public void mDECIMAL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DECIMAL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:611:9: ( D E C I M A L )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:611:11: D E C I M A L
            {
            	mD(); 
            	mE(); 
            	mC(); 
            	mI(); 
            	mM(); 
            	mA(); 
            	mL(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DECIMAL"

    // $ANTLR start "NUMERIC"
    public void mNUMERIC() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = NUMERIC;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:612:9: ( N U M E R I C )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:612:11: N U M E R I C
            {
            	mN(); 
            	mU(); 
            	mM(); 
            	mE(); 
            	mR(); 
            	mI(); 
            	mC(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "NUMERIC"

    // $ANTLR start "DATE"
    public void mDATE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DATE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:613:6: ( D A T E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:613:8: D A T E
            {
            	mD(); 
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
    // $ANTLR end "DATE"

    // $ANTLR start "TIME"
    public void mTIME() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = TIME;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:614:6: ( T I M E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:614:8: T I M E
            {
            	mT(); 
            	mI(); 
            	mM(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "TIME"

    // $ANTLR start "TIMESTAMP"
    public void mTIMESTAMP() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = TIMESTAMP;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:615:11: ( T I M E S T A M P )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:615:13: T I M E S T A M P
            {
            	mT(); 
            	mI(); 
            	mM(); 
            	mE(); 
            	mS(); 
            	mT(); 
            	mA(); 
            	mM(); 
            	mP(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "TIMESTAMP"

    // $ANTLR start "DATETIME"
    public void mDATETIME() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DATETIME;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:616:10: ( D A T E T I M E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:616:12: D A T E T I M E
            {
            	mD(); 
            	mA(); 
            	mT(); 
            	mE(); 
            	mT(); 
            	mI(); 
            	mM(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DATETIME"

    // $ANTLR start "CHAR"
    public void mCHAR() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CHAR;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:617:6: ( C H A R )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:617:8: C H A R
            {
            	mC(); 
            	mH(); 
            	mA(); 
            	mR(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CHAR"

    // $ANTLR start "VARCHAR"
    public void mVARCHAR() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = VARCHAR;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:618:9: ( V A R C H A R )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:618:11: V A R C H A R
            {
            	mV(); 
            	mA(); 
            	mR(); 
            	mC(); 
            	mH(); 
            	mA(); 
            	mR(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "VARCHAR"

    // $ANTLR start "BINARY"
    public void mBINARY() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = BINARY;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:619:8: ( B I N A R Y )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:619:10: B I N A R Y
            {
            	mB(); 
            	mI(); 
            	mN(); 
            	mA(); 
            	mR(); 
            	mY(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "BINARY"

    // $ANTLR start "VARBINARY"
    public void mVARBINARY() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = VARBINARY;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:620:11: ( V A R B I N A R Y )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:620:13: V A R B I N A R Y
            {
            	mV(); 
            	mA(); 
            	mR(); 
            	mB(); 
            	mI(); 
            	mN(); 
            	mA(); 
            	mR(); 
            	mY(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "VARBINARY"

    // $ANTLR start "TINYBLOB"
    public void mTINYBLOB() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = TINYBLOB;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:621:10: ( T I N Y B L O B )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:621:12: T I N Y B L O B
            {
            	mT(); 
            	mI(); 
            	mN(); 
            	mY(); 
            	mB(); 
            	mL(); 
            	mO(); 
            	mB(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "TINYBLOB"

    // $ANTLR start "BLOB"
    public void mBLOB() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = BLOB;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:622:6: ( B L O B )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:622:8: B L O B
            {
            	mB(); 
            	mL(); 
            	mO(); 
            	mB(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "BLOB"

    // $ANTLR start "MEDIUMBLOB"
    public void mMEDIUMBLOB() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = MEDIUMBLOB;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:623:12: ( M E D I U M B L O B )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:623:14: M E D I U M B L O B
            {
            	mM(); 
            	mE(); 
            	mD(); 
            	mI(); 
            	mU(); 
            	mM(); 
            	mB(); 
            	mL(); 
            	mO(); 
            	mB(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "MEDIUMBLOB"

    // $ANTLR start "LONGBLOB"
    public void mLONGBLOB() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LONGBLOB;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:624:10: ( L O N G B L O B )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:624:12: L O N G B L O B
            {
            	mL(); 
            	mO(); 
            	mN(); 
            	mG(); 
            	mB(); 
            	mL(); 
            	mO(); 
            	mB(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LONGBLOB"

    // $ANTLR start "TINYTEXT"
    public void mTINYTEXT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = TINYTEXT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:625:10: ( T I N Y T E X T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:625:12: T I N Y T E X T
            {
            	mT(); 
            	mI(); 
            	mN(); 
            	mY(); 
            	mT(); 
            	mE(); 
            	mX(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "TINYTEXT"

    // $ANTLR start "TEXT"
    public void mTEXT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = TEXT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:626:6: ( T E X T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:626:8: T E X T
            {
            	mT(); 
            	mE(); 
            	mX(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "TEXT"

    // $ANTLR start "MEDIUMTEXT"
    public void mMEDIUMTEXT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = MEDIUMTEXT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:627:12: ( M E D I U M T E X T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:627:14: M E D I U M T E X T
            {
            	mM(); 
            	mE(); 
            	mD(); 
            	mI(); 
            	mU(); 
            	mM(); 
            	mT(); 
            	mE(); 
            	mX(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "MEDIUMTEXT"

    // $ANTLR start "LONGTEXT"
    public void mLONGTEXT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LONGTEXT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:628:10: ( L O N G T E X T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:628:12: L O N G T E X T
            {
            	mL(); 
            	mO(); 
            	mN(); 
            	mG(); 
            	mT(); 
            	mE(); 
            	mX(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LONGTEXT"

    // $ANTLR start "ENUM"
    public void mENUM() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ENUM;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:629:6: ( E N U M )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:629:8: E N U M
            {
            	mE(); 
            	mN(); 
            	mU(); 
            	mM(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ENUM"

    // $ANTLR start "CHARACTER"
    public void mCHARACTER() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CHARACTER;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:630:11: ( C H A R A C T E R )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:630:13: C H A R A C T E R
            {
            	mC(); 
            	mH(); 
            	mA(); 
            	mR(); 
            	mA(); 
            	mC(); 
            	mT(); 
            	mE(); 
            	mR(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CHARACTER"

    // $ANTLR start "UNSIGNED"
    public void mUNSIGNED() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = UNSIGNED;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:631:10: ( U N S I G N E D )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:631:12: U N S I G N E D
            {
            	mU(); 
            	mN(); 
            	mS(); 
            	mI(); 
            	mG(); 
            	mN(); 
            	mE(); 
            	mD(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "UNSIGNED"

    // $ANTLR start "ZEROFILL"
    public void mZEROFILL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ZEROFILL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:632:10: ( Z E R O F I L L )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:632:12: Z E R O F I L L
            {
            	mZ(); 
            	mE(); 
            	mR(); 
            	mO(); 
            	mF(); 
            	mI(); 
            	mL(); 
            	mL(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ZEROFILL"

    // $ANTLR start "CONNECTION"
    public void mCONNECTION() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CONNECTION;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:633:12: ( C O N N E C T I O N )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:633:14: C O N N E C T I O N
            {
            	mC(); 
            	mO(); 
            	mN(); 
            	mN(); 
            	mE(); 
            	mC(); 
            	mT(); 
            	mI(); 
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
    // $ANTLR end "CONNECTION"

    // $ANTLR start "COMMENT"
    public void mCOMMENT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = COMMENT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:634:9: ( C O M M E N T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:634:11: C O M M E N T
            {
            	mC(); 
            	mO(); 
            	mM(); 
            	mM(); 
            	mE(); 
            	mN(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "COMMENT"

    // $ANTLR start "GEOMETRY"
    public void mGEOMETRY() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = GEOMETRY;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:635:10: ( G E O M E T R Y )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:635:12: G E O M E T R Y
            {
            	mG(); 
            	mE(); 
            	mO(); 
            	mM(); 
            	mE(); 
            	mT(); 
            	mR(); 
            	mY(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "GEOMETRY"

    // $ANTLR start "POINT"
    public void mPOINT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = POINT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:636:7: ( P O I N T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:636:9: P O I N T
            {
            	mP(); 
            	mO(); 
            	mI(); 
            	mN(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "POINT"

    // $ANTLR start "LINESTRING"
    public void mLINESTRING() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LINESTRING;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:637:12: ( L I N E S T R I N G )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:637:14: L I N E S T R I N G
            {
            	mL(); 
            	mI(); 
            	mN(); 
            	mE(); 
            	mS(); 
            	mT(); 
            	mR(); 
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
    // $ANTLR end "LINESTRING"

    // $ANTLR start "POLYGON"
    public void mPOLYGON() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = POLYGON;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:638:9: ( P O L Y G O N )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:638:11: P O L Y G O N
            {
            	mP(); 
            	mO(); 
            	mL(); 
            	mY(); 
            	mG(); 
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
    // $ANTLR end "POLYGON"

    // $ANTLR start "MULTIPOINT"
    public void mMULTIPOINT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = MULTIPOINT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:639:12: ( M U L T I P O I N T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:639:14: M U L T I P O I N T
            {
            	mM(); 
            	mU(); 
            	mL(); 
            	mT(); 
            	mI(); 
            	mP(); 
            	mO(); 
            	mI(); 
            	mN(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "MULTIPOINT"

    // $ANTLR start "MULTILINESTRING"
    public void mMULTILINESTRING() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = MULTILINESTRING;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:640:17: ( M U L T I L I N E S T R I N G )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:640:19: M U L T I L I N E S T R I N G
            {
            	mM(); 
            	mU(); 
            	mL(); 
            	mT(); 
            	mI(); 
            	mL(); 
            	mI(); 
            	mN(); 
            	mE(); 
            	mS(); 
            	mT(); 
            	mR(); 
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
    // $ANTLR end "MULTILINESTRING"

    // $ANTLR start "MULTIPOLYGON"
    public void mMULTIPOLYGON() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = MULTIPOLYGON;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:641:14: ( M U L T I P O L Y G O N )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:641:16: M U L T I P O L Y G O N
            {
            	mM(); 
            	mU(); 
            	mL(); 
            	mT(); 
            	mI(); 
            	mP(); 
            	mO(); 
            	mL(); 
            	mY(); 
            	mG(); 
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
    // $ANTLR end "MULTIPOLYGON"

    // $ANTLR start "GEOMETRYCOLLECTION"
    public void mGEOMETRYCOLLECTION() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = GEOMETRYCOLLECTION;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:642:20: ( G E O M E T R Y C O L L E C T I O N )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:642:22: G E O M E T R Y C O L L E C T I O N
            {
            	mG(); 
            	mE(); 
            	mO(); 
            	mM(); 
            	mE(); 
            	mT(); 
            	mR(); 
            	mY(); 
            	mC(); 
            	mO(); 
            	mL(); 
            	mL(); 
            	mE(); 
            	mC(); 
            	mT(); 
            	mI(); 
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
    // $ANTLR end "GEOMETRYCOLLECTION"

    // $ANTLR start "TYPE"
    public void mTYPE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = TYPE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:643:6: ( T Y P E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:643:8: T Y P E
            {
            	mT(); 
            	mY(); 
            	mP(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "TYPE"

    // $ANTLR start "RIGHT"
    public void mRIGHT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = RIGHT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:644:7: ( R I G H T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:644:9: R I G H T
            {
            	mR(); 
            	mI(); 
            	mG(); 
            	mH(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "RIGHT"

    // $ANTLR start "ALGORITHM"
    public void mALGORITHM() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ALGORITHM;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:645:11: ( A L G O R I T H M )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:645:13: A L G O R I T H M
            {
            	mA(); 
            	mL(); 
            	mG(); 
            	mO(); 
            	mR(); 
            	mI(); 
            	mT(); 
            	mH(); 
            	mM(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ALGORITHM"

    // $ANTLR start "DEFINER"
    public void mDEFINER() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DEFINER;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:646:9: ( D E F I N E R )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:646:11: D E F I N E R
            {
            	mD(); 
            	mE(); 
            	mF(); 
            	mI(); 
            	mN(); 
            	mE(); 
            	mR(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DEFINER"

    // $ANTLR start "INVOKER"
    public void mINVOKER() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = INVOKER;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:647:9: ( I N V O K E R )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:647:11: I N V O K E R
            {
            	mI(); 
            	mN(); 
            	mV(); 
            	mO(); 
            	mK(); 
            	mE(); 
            	mR(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "INVOKER"

    // $ANTLR start "SQL"
    public void mSQL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SQL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:648:5: ( S Q L )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:648:7: S Q L
            {
            	mS(); 
            	mQ(); 
            	mL(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SQL"

    // $ANTLR start "SECURITY"
    public void mSECURITY() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SECURITY;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:649:10: ( S E C U R I T Y )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:649:12: S E C U R I T Y
            {
            	mS(); 
            	mE(); 
            	mC(); 
            	mU(); 
            	mR(); 
            	mI(); 
            	mT(); 
            	mY(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SECURITY"

    // $ANTLR start "CASCADED"
    public void mCASCADED() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CASCADED;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:650:10: ( C A S C A D E D )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:650:12: C A S C A D E D
            {
            	mC(); 
            	mA(); 
            	mS(); 
            	mC(); 
            	mA(); 
            	mD(); 
            	mE(); 
            	mD(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CASCADED"

    // $ANTLR start "LOCAL"
    public void mLOCAL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LOCAL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:651:7: ( L O C A L )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:651:9: L O C A L
            {
            	mL(); 
            	mO(); 
            	mC(); 
            	mA(); 
            	mL(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LOCAL"

    // $ANTLR start "OPTION"
    public void mOPTION() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = OPTION;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:652:8: ( O P T I O N )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:652:10: O P T I O N
            {
            	mO(); 
            	mP(); 
            	mT(); 
            	mI(); 
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
    // $ANTLR end "OPTION"

    // $ANTLR start "DISTINCTROW"
    public void mDISTINCTROW() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DISTINCTROW;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:653:13: ( D I S T I N C T R O W )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:653:15: D I S T I N C T R O W
            {
            	mD(); 
            	mI(); 
            	mS(); 
            	mT(); 
            	mI(); 
            	mN(); 
            	mC(); 
            	mT(); 
            	mR(); 
            	mO(); 
            	mW(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DISTINCTROW"

    // $ANTLR start "HIGH_PRIORITY"
    public void mHIGH_PRIORITY() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = HIGH_PRIORITY;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:654:15: ( H I G H '_' P R I O R I T Y )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:654:17: H I G H '_' P R I O R I T Y
            {
            	mH(); 
            	mI(); 
            	mG(); 
            	mH(); 
            	Match('_'); 
            	mP(); 
            	mR(); 
            	mI(); 
            	mO(); 
            	mR(); 
            	mI(); 
            	mT(); 
            	mY(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "HIGH_PRIORITY"

    // $ANTLR start "STRAIGHT_JOIN"
    public void mSTRAIGHT_JOIN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = STRAIGHT_JOIN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:655:15: ( S T R A I G H T '_' J O I N )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:655:17: S T R A I G H T '_' J O I N
            {
            	mS(); 
            	mT(); 
            	mR(); 
            	mA(); 
            	mI(); 
            	mG(); 
            	mH(); 
            	mT(); 
            	Match('_'); 
            	mJ(); 
            	mO(); 
            	mI(); 
            	mN(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "STRAIGHT_JOIN"

    // $ANTLR start "SQL_SMALL_RESULT"
    public void mSQL_SMALL_RESULT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SQL_SMALL_RESULT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:656:18: ( S Q L '_' S M A L L '_' R E S U L T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:656:20: S Q L '_' S M A L L '_' R E S U L T
            {
            	mS(); 
            	mQ(); 
            	mL(); 
            	Match('_'); 
            	mS(); 
            	mM(); 
            	mA(); 
            	mL(); 
            	mL(); 
            	Match('_'); 
            	mR(); 
            	mE(); 
            	mS(); 
            	mU(); 
            	mL(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SQL_SMALL_RESULT"

    // $ANTLR start "SQL_BIG_RESULT"
    public void mSQL_BIG_RESULT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SQL_BIG_RESULT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:657:16: ( S Q L '_' B I G '_' R E S U L T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:657:18: S Q L '_' B I G '_' R E S U L T
            {
            	mS(); 
            	mQ(); 
            	mL(); 
            	Match('_'); 
            	mB(); 
            	mI(); 
            	mG(); 
            	Match('_'); 
            	mR(); 
            	mE(); 
            	mS(); 
            	mU(); 
            	mL(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SQL_BIG_RESULT"

    // $ANTLR start "SQL_BUFFER_RESULT"
    public void mSQL_BUFFER_RESULT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SQL_BUFFER_RESULT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:658:19: ( S Q L '_' B U F F E R '_' R E S U L T )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:658:21: S Q L '_' B U F F E R '_' R E S U L T
            {
            	mS(); 
            	mQ(); 
            	mL(); 
            	Match('_'); 
            	mB(); 
            	mU(); 
            	mF(); 
            	mF(); 
            	mE(); 
            	mR(); 
            	Match('_'); 
            	mR(); 
            	mE(); 
            	mS(); 
            	mU(); 
            	mL(); 
            	mT(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SQL_BUFFER_RESULT"

    // $ANTLR start "SQL_CACHE"
    public void mSQL_CACHE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SQL_CACHE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:659:11: ( S Q L '_' C A C H E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:659:13: S Q L '_' C A C H E
            {
            	mS(); 
            	mQ(); 
            	mL(); 
            	Match('_'); 
            	mC(); 
            	mA(); 
            	mC(); 
            	mH(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SQL_CACHE"

    // $ANTLR start "SQL_NO_CACHE"
    public void mSQL_NO_CACHE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SQL_NO_CACHE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:660:14: ( S Q L '_' N O '_' C A C H E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:660:16: S Q L '_' N O '_' C A C H E
            {
            	mS(); 
            	mQ(); 
            	mL(); 
            	Match('_'); 
            	mN(); 
            	mO(); 
            	Match('_'); 
            	mC(); 
            	mA(); 
            	mC(); 
            	mH(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SQL_NO_CACHE"

    // $ANTLR start "SQL_CALC_FOUND_ROWS"
    public void mSQL_CALC_FOUND_ROWS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SQL_CALC_FOUND_ROWS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:661:21: ( S Q L '_' C A L C '_' F O U N D '_' R O W S )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:661:23: S Q L '_' C A L C '_' F O U N D '_' R O W S
            {
            	mS(); 
            	mQ(); 
            	mL(); 
            	Match('_'); 
            	mC(); 
            	mA(); 
            	mL(); 
            	mC(); 
            	Match('_'); 
            	mF(); 
            	mO(); 
            	mU(); 
            	mN(); 
            	mD(); 
            	Match('_'); 
            	mR(); 
            	mO(); 
            	mW(); 
            	mS(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SQL_CALC_FOUND_ROWS"

    // $ANTLR start "ROLLUP"
    public void mROLLUP() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ROLLUP;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:662:8: ( R O L L U P )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:662:10: R O L L U P
            {
            	mR(); 
            	mO(); 
            	mL(); 
            	mL(); 
            	mU(); 
            	mP(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ROLLUP"

    // $ANTLR start "SHARE"
    public void mSHARE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SHARE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:663:7: ( S H A R E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:663:9: S H A R E
            {
            	mS(); 
            	mH(); 
            	mA(); 
            	mR(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SHARE"

    // $ANTLR start "LOCK"
    public void mLOCK() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LOCK;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:664:6: ( L O C K )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:664:8: L O C K
            {
            	mL(); 
            	mO(); 
            	mC(); 
            	mK(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LOCK"

    // $ANTLR start "MODE"
    public void mMODE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = MODE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:665:6: ( M O D E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:665:8: M O D E
            {
            	mM(); 
            	mO(); 
            	mD(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "MODE"

    // $ANTLR start "FORCE"
    public void mFORCE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = FORCE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:666:7: ( F O R C E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:666:9: F O R C E
            {
            	mF(); 
            	mO(); 
            	mR(); 
            	mC(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "FORCE"

    // $ANTLR start "OJ"
    public void mOJ() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = OJ;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:667:4: ( O J )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:667:6: O J
            {
            	mO(); 
            	mJ(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "OJ"

    // $ANTLR start "USE"
    public void mUSE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = USE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:668:5: ( U S E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:668:7: U S E
            {
            	mU(); 
            	mS(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "USE"

    // $ANTLR start "INTERVAL"
    public void mINTERVAL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = INTERVAL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:669:10: ( I N T E R V A L )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:669:12: I N T E R V A L
            {
            	mI(); 
            	mN(); 
            	mT(); 
            	mE(); 
            	mR(); 
            	mV(); 
            	mA(); 
            	mL(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "INTERVAL"

    // $ANTLR start "CURRENT_TIME"
    public void mCURRENT_TIME() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CURRENT_TIME;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:671:14: ( C U R R E N T '_' T I M E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:671:16: C U R R E N T '_' T I M E
            {
            	mC(); 
            	mU(); 
            	mR(); 
            	mR(); 
            	mE(); 
            	mN(); 
            	mT(); 
            	Match('_'); 
            	mT(); 
            	mI(); 
            	mM(); 
            	mE(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CURRENT_TIME"

    // $ANTLR start "CURRENT_DATE"
    public void mCURRENT_DATE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CURRENT_DATE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:672:14: ( C U R R E N T '_' D A T E )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:672:16: C U R R E N T '_' D A T E
            {
            	mC(); 
            	mU(); 
            	mR(); 
            	mR(); 
            	mE(); 
            	mN(); 
            	mT(); 
            	Match('_'); 
            	mD(); 
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
    // $ANTLR end "CURRENT_DATE"

    // $ANTLR start "CURRENT_TIMESTAMP"
    public void mCURRENT_TIMESTAMP() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CURRENT_TIMESTAMP;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:673:19: ( C U R R E N T '_' T I M E S T A M P )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:673:21: C U R R E N T '_' T I M E S T A M P
            {
            	mC(); 
            	mU(); 
            	mR(); 
            	mR(); 
            	mE(); 
            	mN(); 
            	mT(); 
            	Match('_'); 
            	mT(); 
            	mI(); 
            	mM(); 
            	mE(); 
            	mS(); 
            	mT(); 
            	mA(); 
            	mM(); 
            	mP(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CURRENT_TIMESTAMP"

    // $ANTLR start "UTC_TIMESTAMP"
    public void mUTC_TIMESTAMP() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = UTC_TIMESTAMP;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:674:15: ( U T C '_' T I M E S T A M P )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:674:17: U T C '_' T I M E S T A M P
            {
            	mU(); 
            	mT(); 
            	mC(); 
            	Match('_'); 
            	mT(); 
            	mI(); 
            	mM(); 
            	mE(); 
            	mS(); 
            	mT(); 
            	mA(); 
            	mM(); 
            	mP(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "UTC_TIMESTAMP"

    // $ANTLR start "CURRENT_USER"
    public void mCURRENT_USER() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CURRENT_USER;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:675:14: ( C U R R E N T '_' U S E R )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:675:16: C U R R E N T '_' U S E R
            {
            	mC(); 
            	mU(); 
            	mR(); 
            	mR(); 
            	mE(); 
            	mN(); 
            	mT(); 
            	Match('_'); 
            	mU(); 
            	mS(); 
            	mE(); 
            	mR(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CURRENT_USER"

    // $ANTLR start "YEAR"
    public void mYEAR() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = YEAR;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:676:6: ( Y E A R )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:676:8: Y E A R
            {
            	mY(); 
            	mE(); 
            	mA(); 
            	mR(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "YEAR"

    // $ANTLR start "ID_START"
    public void mID_START() // throws RecognitionException [2]
    {
    		try
    		{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:678:18: ( ( 'a' .. 'z' | 'A' .. 'Z' | '_' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:678:20: ( 'a' .. 'z' | 'A' .. 'Z' | '_' )
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:679:8: ( ( ID_START ( ID_START | '0' .. '9' )* ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:679:10: ( ID_START ( ID_START | '0' .. '9' )* )
            {
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:679:10: ( ID_START ( ID_START | '0' .. '9' )* )
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:679:11: ID_START ( ID_START | '0' .. '9' )*
            	{
            		mID_START(); 
            		// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:679:20: ( ID_START | '0' .. '9' )*
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
            				    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:680:15: ( '`' ( options {greedy=false; } : . )* '`' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:680:17: '`' ( options {greedy=false; } : . )* '`'
            {
            	Match('`'); 
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:680:21: ( options {greedy=false; } : . )*
            	do 
            	{
            	    int alt2 = 2;
            	    int LA2_0 = input.LA(1);

            	    if ( (LA2_0 == '`') )
            	    {
            	        alt2 = 2;
            	    }
            	    else if ( ((LA2_0 >= '\u0000' && LA2_0 <= '_') || (LA2_0 >= 'a' && LA2_0 <= '\uFFFF')) )
            	    {
            	        alt2 = 1;
            	    }


            	    switch (alt2) 
            		{
            			case 1 :
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:680:49: .
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

            	Match('`'); 

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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:688:10: ( ( 'N' | 'n' ) ( '\\'' ( options {greedy=true; } : ~ ( '\\'' | '\\r' | '\\n' ) | '\\'' '\\'' )* '\\'' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:689:2: ( 'N' | 'n' ) ( '\\'' ( options {greedy=true; } : ~ ( '\\'' | '\\r' | '\\n' ) | '\\'' '\\'' )* '\\'' )
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

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:690:4: ( '\\'' ( options {greedy=true; } : ~ ( '\\'' | '\\r' | '\\n' ) | '\\'' '\\'' )* '\\'' )
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:690:5: '\\'' ( options {greedy=true; } : ~ ( '\\'' | '\\r' | '\\n' ) | '\\'' '\\'' )* '\\''
            	{
            		Match('\''); 
            		// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:691:5: ( options {greedy=true; } : ~ ( '\\'' | '\\r' | '\\n' ) | '\\'' '\\'' )*
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
            				    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:692:31: ~ ( '\\'' | '\\r' | '\\n' )
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
            				    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:692:56: '\\'' '\\''
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:696:9: ( ( '\\'' ( options {greedy=true; } : ~ ( '\\'' | '\\r' | '\\n' ) | '\\'' '\\'' )* '\\'' ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:697:4: ( '\\'' ( options {greedy=true; } : ~ ( '\\'' | '\\r' | '\\n' ) | '\\'' '\\'' )* '\\'' )
            {
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:697:4: ( '\\'' ( options {greedy=true; } : ~ ( '\\'' | '\\r' | '\\n' ) | '\\'' '\\'' )* '\\'' )
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:697:5: '\\'' ( options {greedy=true; } : ~ ( '\\'' | '\\r' | '\\n' ) | '\\'' '\\'' )* '\\''
            	{
            		Match('\''); 
            		// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:698:5: ( options {greedy=true; } : ~ ( '\\'' | '\\r' | '\\n' ) | '\\'' '\\'' )*
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
            				    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:699:31: ~ ( '\\'' | '\\r' | '\\n' )
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
            				    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:699:56: '\\'' '\\''
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:703:10: ( ( '0' .. '9' )+ )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:703:12: ( '0' .. '9' )+
            {
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:703:12: ( '0' .. '9' )+
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
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:703:13: '0' .. '9'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:704:20: ( ( 'e' | 'E' ) ( '+' | '-' )? ( '0' .. '9' )+ )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:704:22: ( 'e' | 'E' ) ( '+' | '-' )? ( '0' .. '9' )+
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

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:704:32: ( '+' | '-' )?
            	int alt6 = 2;
            	int LA6_0 = input.LA(1);

            	if ( (LA6_0 == '+' || LA6_0 == '-') )
            	{
            	    alt6 = 1;
            	}
            	switch (alt6) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:
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

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:704:43: ( '0' .. '9' )+
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
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:704:44: '0' .. '9'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:706:5: ( ( '0' .. '9' )+ '.' ( '0' .. '9' )* ( FLOAT_EXP )? | '.' ( '0' .. '9' )+ ( FLOAT_EXP )? | ( '0' .. '9' )+ FLOAT_EXP )
            int alt14 = 3;
            alt14 = dfa14.Predict(input);
            switch (alt14) 
            {
                case 1 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:706:9: ( '0' .. '9' )+ '.' ( '0' .. '9' )* ( FLOAT_EXP )?
                    {
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:706:9: ( '0' .. '9' )+
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
                    			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:706:10: '0' .. '9'
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
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:706:25: ( '0' .. '9' )*
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
                    			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:706:26: '0' .. '9'
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

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:706:37: ( FLOAT_EXP )?
                    	int alt10 = 2;
                    	int LA10_0 = input.LA(1);

                    	if ( (LA10_0 == 'E' || LA10_0 == 'e') )
                    	{
                    	    alt10 = 1;
                    	}
                    	switch (alt10) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:706:37: FLOAT_EXP
                    	        {
                    	        	mFLOAT_EXP(); 

                    	        }
                    	        break;

                    	}


                    }
                    break;
                case 2 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:707:9: '.' ( '0' .. '9' )+ ( FLOAT_EXP )?
                    {
                    	Match('.'); 
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:707:13: ( '0' .. '9' )+
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
                    			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:707:14: '0' .. '9'
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

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:707:25: ( FLOAT_EXP )?
                    	int alt12 = 2;
                    	int LA12_0 = input.LA(1);

                    	if ( (LA12_0 == 'E' || LA12_0 == 'e') )
                    	{
                    	    alt12 = 1;
                    	}
                    	switch (alt12) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:707:25: FLOAT_EXP
                    	        {
                    	        	mFLOAT_EXP(); 

                    	        }
                    	        break;

                    	}


                    }
                    break;
                case 3 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:708:9: ( '0' .. '9' )+ FLOAT_EXP
                    {
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:708:9: ( '0' .. '9' )+
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
                    			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:708:10: '0' .. '9'
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:710:7: ( ( 'x' | 'X' ) '\\'' ( '0' .. '9' | 'a' .. 'f' | 'A' .. 'F' )+ '\\'' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:710:9: ( 'x' | 'X' ) '\\'' ( '0' .. '9' | 'a' .. 'f' | 'A' .. 'F' )+ '\\''
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
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:710:24: ( '0' .. '9' | 'a' .. 'f' | 'A' .. 'F' )+
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
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:712:13: ( ( '0' 'b' ( '0' | '1' )+ ) | 'b' '\\'' ( '0' | '1' )+ '\\'' )
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
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:712:15: ( '0' 'b' ( '0' | '1' )+ )
                    {
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:712:15: ( '0' 'b' ( '0' | '1' )+ )
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:712:16: '0' 'b' ( '0' | '1' )+
                    	{
                    		Match('0'); 
                    		Match('b'); 
                    		// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:712:24: ( '0' | '1' )+
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
                    				    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:
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
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:712:41: 'b' '\\'' ( '0' | '1' )+ '\\''
                    {
                    	Match('b'); 
                    	Match('\''); 
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:712:50: ( '0' | '1' )+
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
                    			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:714:19: ( '/*' ( options {greedy=false; } : . )* '*/' )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:714:21: '/*' ( options {greedy=false; } : . )* '*/'
            {
            	Match("/*"); 

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:714:26: ( options {greedy=false; } : . )*
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
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:714:54: .
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:715:22: ( '--' (~ ( '\\n' | '\\r' ) )* ( ( '\\r' )? '\\n' | EOF ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:715:24: '--' (~ ( '\\n' | '\\r' ) )* ( ( '\\r' )? '\\n' | EOF )
            {
            	Match("--"); 

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:715:29: (~ ( '\\n' | '\\r' ) )*
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
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:715:29: ~ ( '\\n' | '\\r' )
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

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:715:43: ( ( '\\r' )? '\\n' | EOF )
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
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:715:44: ( '\\r' )? '\\n'
            	        {
            	        	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:715:44: ( '\\r' )?
            	        	int alt21 = 2;
            	        	int LA21_0 = input.LA(1);

            	        	if ( (LA21_0 == '\r') )
            	        	{
            	        	    alt21 = 1;
            	        	}
            	        	switch (alt21) 
            	        	{
            	        	    case 1 :
            	        	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:715:44: '\\r'
            	        	        {
            	        	        	Match('\r'); 

            	        	        }
            	        	        break;

            	        	}

            	        	Match('\n'); 

            	        }
            	        break;
            	    case 2 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:715:55: EOF
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
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:717:11: ( ( ' ' | '\\r' | '\\t' | '\\u000C' | '\\n' | T_COMMENT | LINE_COMMENT ) )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:717:13: ( ' ' | '\\r' | '\\t' | '\\u000C' | '\\n' | T_COMMENT | LINE_COMMENT )
            {
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:717:13: ( ' ' | '\\r' | '\\t' | '\\u000C' | '\\n' | T_COMMENT | LINE_COMMENT )
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
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:717:14: ' '
            	        {
            	        	Match(' '); 

            	        }
            	        break;
            	    case 2 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:717:18: '\\r'
            	        {
            	        	Match('\r'); 

            	        }
            	        break;
            	    case 3 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:717:23: '\\t'
            	        {
            	        	Match('\t'); 

            	        }
            	        break;
            	    case 4 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:717:28: '\\u000C'
            	        {
            	        	Match('\f'); 

            	        }
            	        break;
            	    case 5 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:717:37: '\\n'
            	        {
            	        	Match('\n'); 

            	        }
            	        break;
            	    case 6 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:717:42: T_COMMENT
            	        {
            	        	mT_COMMENT(); 

            	        }
            	        break;
            	    case 7 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:717:52: LINE_COMMENT
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
        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:8: ( EQUALS | SAFEEQUALS | EQUALS2 | NOT_EQUALS | NOT_EQUALS2 | LESS | LESS_OR_EQ | GREATER | GREATER_OR_EQ | SHIFT_LEFT | SHIFT_RIGHT | AMPERSAND | DOUBLE_AMPERSAND | PIPE | DOUBLE_PIPE | PLUS | MINUS | TILDA | ASTERISK | SLASH | PERCENT | SEMI | DOT | COMMA | LPAREN | RPAREN | QUESTION | EXCLAMATION | COLON | AT | DOLLAR | ARROW_UP | ABORT | ADD | AFTER | ALL | ALTER | ANALYZE | AND | AS | ASC | ATTACH | AUTOINCREMENT | BEFORE | BEGIN | BETWEEN | BY | CASCADE | CASE | CAST | CHECK | COLLATE | COLUMN | COMMIT | CONFLICT | CONSTRAINT | CREATE | CROSS | DATABASE | DEFAULT | DEFERRABLE | DEFERRED | DELETE | DESC | DETACH | DISTINCT | DROP | EACH | ELSE | END | ESCAPE | EXCEPT | EXCLUSIVE | EXISTS | EXPLAIN | FAIL | FOR | FOREIGN | FROM | GLOB | GROUP | HAVING | IF | IGNORE | IMMEDIATE | IN | INDEX | INDEXED | INITIALLY | INNER | INSERT | INSTEAD | INTERSECT | INTO | IS | ISNULL | JOIN | KEY | LEFT | LIKE | LIMIT | MATCH | NATURAL | NOT | NOTNULL | NULL | OF | OFFSET | ON | OR | ORDER | OUTER | PLAN | PRAGMA | PRIMARY | QUERY | RAISE | REFERENCES | REGEXP | REINDEX | RELEASE | RENAME | REPLACE | RESTRICT | ROLLBACK | ROW | SAVEPOINT | SELECT | SET | TABLE | TEMPORARY | TEMP | THEN | TO | TRANSACTION | TRIGGER | UNION | UNIQUE | UPDATE | USING | VACUUM | VALUES | VIEW | VIRTUAL | WHEN | WHERE | DIV | MOD | FULLTEXT | SPATIAL | KEY_BLOCK_SIZE | WITH | PARSER | XOR | AUTO_INCREMENT | COLUMN_FORMAT | FIXED | DYNAMIC | STORAGE | DISK | MEMORY | FULL | PARTIAL | SIMPLE | NO | ACTION | ENGINE | AVG_ROW_LENGTH | CHECKSUM | DATA | DIRECTORY | DELAY_KEY_WRITE | INSERT_METHOD | FIRST | LAST | MAX_ROWS | MIN_ROWS | PACK_KEYS | PASSWORD | ROW_FORMAT | COMPRESSED | REDUNDANT | COMPACT | TABLESPACE | CHARSET | BIT | TINYINT | SMALLINT | MEDIUMINT | INT | INTEGER | BIGINT | REAL | DOUBLE | FLOAT | DECIMAL | NUMERIC | DATE | TIME | TIMESTAMP | DATETIME | CHAR | VARCHAR | BINARY | VARBINARY | TINYBLOB | BLOB | MEDIUMBLOB | LONGBLOB | TINYTEXT | TEXT | MEDIUMTEXT | LONGTEXT | ENUM | CHARACTER | UNSIGNED | ZEROFILL | CONNECTION | COMMENT | GEOMETRY | POINT | LINESTRING | POLYGON | MULTIPOINT | MULTILINESTRING | MULTIPOLYGON | GEOMETRYCOLLECTION | TYPE | RIGHT | ALGORITHM | DEFINER | INVOKER | SQL | SECURITY | CASCADED | LOCAL | OPTION | DISTINCTROW | HIGH_PRIORITY | STRAIGHT_JOIN | SQL_SMALL_RESULT | SQL_BIG_RESULT | SQL_BUFFER_RESULT | SQL_CACHE | SQL_NO_CACHE | SQL_CALC_FOUND_ROWS | ROLLUP | SHARE | LOCK | MODE | FORCE | OJ | USE | INTERVAL | CURRENT_TIME | CURRENT_DATE | CURRENT_TIMESTAMP | UTC_TIMESTAMP | CURRENT_USER | YEAR | T_IDENT | T_QUOTED_IDENT | T_NSTRING | T_STRING | T_INTEGER | T_FLOAT | T_BLOB | T_BINARYNUM | WHITESPACE )
        int alt24 = 269;
        alt24 = dfa24.Predict(input);
        switch (alt24) 
        {
            case 1 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:10: EQUALS
                {
                	mEQUALS(); 

                }
                break;
            case 2 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:17: SAFEEQUALS
                {
                	mSAFEEQUALS(); 

                }
                break;
            case 3 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:28: EQUALS2
                {
                	mEQUALS2(); 

                }
                break;
            case 4 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:36: NOT_EQUALS
                {
                	mNOT_EQUALS(); 

                }
                break;
            case 5 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:47: NOT_EQUALS2
                {
                	mNOT_EQUALS2(); 

                }
                break;
            case 6 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:59: LESS
                {
                	mLESS(); 

                }
                break;
            case 7 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:64: LESS_OR_EQ
                {
                	mLESS_OR_EQ(); 

                }
                break;
            case 8 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:75: GREATER
                {
                	mGREATER(); 

                }
                break;
            case 9 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:83: GREATER_OR_EQ
                {
                	mGREATER_OR_EQ(); 

                }
                break;
            case 10 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:97: SHIFT_LEFT
                {
                	mSHIFT_LEFT(); 

                }
                break;
            case 11 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:108: SHIFT_RIGHT
                {
                	mSHIFT_RIGHT(); 

                }
                break;
            case 12 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:120: AMPERSAND
                {
                	mAMPERSAND(); 

                }
                break;
            case 13 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:130: DOUBLE_AMPERSAND
                {
                	mDOUBLE_AMPERSAND(); 

                }
                break;
            case 14 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:147: PIPE
                {
                	mPIPE(); 

                }
                break;
            case 15 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:152: DOUBLE_PIPE
                {
                	mDOUBLE_PIPE(); 

                }
                break;
            case 16 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:164: PLUS
                {
                	mPLUS(); 

                }
                break;
            case 17 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:169: MINUS
                {
                	mMINUS(); 

                }
                break;
            case 18 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:175: TILDA
                {
                	mTILDA(); 

                }
                break;
            case 19 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:181: ASTERISK
                {
                	mASTERISK(); 

                }
                break;
            case 20 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:190: SLASH
                {
                	mSLASH(); 

                }
                break;
            case 21 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:196: PERCENT
                {
                	mPERCENT(); 

                }
                break;
            case 22 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:204: SEMI
                {
                	mSEMI(); 

                }
                break;
            case 23 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:209: DOT
                {
                	mDOT(); 

                }
                break;
            case 24 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:213: COMMA
                {
                	mCOMMA(); 

                }
                break;
            case 25 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:219: LPAREN
                {
                	mLPAREN(); 

                }
                break;
            case 26 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:226: RPAREN
                {
                	mRPAREN(); 

                }
                break;
            case 27 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:233: QUESTION
                {
                	mQUESTION(); 

                }
                break;
            case 28 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:242: EXCLAMATION
                {
                	mEXCLAMATION(); 

                }
                break;
            case 29 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:254: COLON
                {
                	mCOLON(); 

                }
                break;
            case 30 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:260: AT
                {
                	mAT(); 

                }
                break;
            case 31 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:263: DOLLAR
                {
                	mDOLLAR(); 

                }
                break;
            case 32 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:270: ARROW_UP
                {
                	mARROW_UP(); 

                }
                break;
            case 33 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:279: ABORT
                {
                	mABORT(); 

                }
                break;
            case 34 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:285: ADD
                {
                	mADD(); 

                }
                break;
            case 35 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:289: AFTER
                {
                	mAFTER(); 

                }
                break;
            case 36 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:295: ALL
                {
                	mALL(); 

                }
                break;
            case 37 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:299: ALTER
                {
                	mALTER(); 

                }
                break;
            case 38 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:305: ANALYZE
                {
                	mANALYZE(); 

                }
                break;
            case 39 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:313: AND
                {
                	mAND(); 

                }
                break;
            case 40 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:317: AS
                {
                	mAS(); 

                }
                break;
            case 41 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:320: ASC
                {
                	mASC(); 

                }
                break;
            case 42 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:324: ATTACH
                {
                	mATTACH(); 

                }
                break;
            case 43 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:331: AUTOINCREMENT
                {
                	mAUTOINCREMENT(); 

                }
                break;
            case 44 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:345: BEFORE
                {
                	mBEFORE(); 

                }
                break;
            case 45 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:352: BEGIN
                {
                	mBEGIN(); 

                }
                break;
            case 46 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:358: BETWEEN
                {
                	mBETWEEN(); 

                }
                break;
            case 47 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:366: BY
                {
                	mBY(); 

                }
                break;
            case 48 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:369: CASCADE
                {
                	mCASCADE(); 

                }
                break;
            case 49 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:377: CASE
                {
                	mCASE(); 

                }
                break;
            case 50 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:382: CAST
                {
                	mCAST(); 

                }
                break;
            case 51 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:387: CHECK
                {
                	mCHECK(); 

                }
                break;
            case 52 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:393: COLLATE
                {
                	mCOLLATE(); 

                }
                break;
            case 53 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:401: COLUMN
                {
                	mCOLUMN(); 

                }
                break;
            case 54 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:408: COMMIT
                {
                	mCOMMIT(); 

                }
                break;
            case 55 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:415: CONFLICT
                {
                	mCONFLICT(); 

                }
                break;
            case 56 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:424: CONSTRAINT
                {
                	mCONSTRAINT(); 

                }
                break;
            case 57 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:435: CREATE
                {
                	mCREATE(); 

                }
                break;
            case 58 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:442: CROSS
                {
                	mCROSS(); 

                }
                break;
            case 59 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:448: DATABASE
                {
                	mDATABASE(); 

                }
                break;
            case 60 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:457: DEFAULT
                {
                	mDEFAULT(); 

                }
                break;
            case 61 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:465: DEFERRABLE
                {
                	mDEFERRABLE(); 

                }
                break;
            case 62 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:476: DEFERRED
                {
                	mDEFERRED(); 

                }
                break;
            case 63 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:485: DELETE
                {
                	mDELETE(); 

                }
                break;
            case 64 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:492: DESC
                {
                	mDESC(); 

                }
                break;
            case 65 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:497: DETACH
                {
                	mDETACH(); 

                }
                break;
            case 66 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:504: DISTINCT
                {
                	mDISTINCT(); 

                }
                break;
            case 67 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:513: DROP
                {
                	mDROP(); 

                }
                break;
            case 68 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:518: EACH
                {
                	mEACH(); 

                }
                break;
            case 69 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:523: ELSE
                {
                	mELSE(); 

                }
                break;
            case 70 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:528: END
                {
                	mEND(); 

                }
                break;
            case 71 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:532: ESCAPE
                {
                	mESCAPE(); 

                }
                break;
            case 72 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:539: EXCEPT
                {
                	mEXCEPT(); 

                }
                break;
            case 73 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:546: EXCLUSIVE
                {
                	mEXCLUSIVE(); 

                }
                break;
            case 74 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:556: EXISTS
                {
                	mEXISTS(); 

                }
                break;
            case 75 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:563: EXPLAIN
                {
                	mEXPLAIN(); 

                }
                break;
            case 76 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:571: FAIL
                {
                	mFAIL(); 

                }
                break;
            case 77 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:576: FOR
                {
                	mFOR(); 

                }
                break;
            case 78 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:580: FOREIGN
                {
                	mFOREIGN(); 

                }
                break;
            case 79 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:588: FROM
                {
                	mFROM(); 

                }
                break;
            case 80 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:593: GLOB
                {
                	mGLOB(); 

                }
                break;
            case 81 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:598: GROUP
                {
                	mGROUP(); 

                }
                break;
            case 82 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:604: HAVING
                {
                	mHAVING(); 

                }
                break;
            case 83 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:611: IF
                {
                	mIF(); 

                }
                break;
            case 84 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:614: IGNORE
                {
                	mIGNORE(); 

                }
                break;
            case 85 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:621: IMMEDIATE
                {
                	mIMMEDIATE(); 

                }
                break;
            case 86 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:631: IN
                {
                	mIN(); 

                }
                break;
            case 87 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:634: INDEX
                {
                	mINDEX(); 

                }
                break;
            case 88 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:640: INDEXED
                {
                	mINDEXED(); 

                }
                break;
            case 89 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:648: INITIALLY
                {
                	mINITIALLY(); 

                }
                break;
            case 90 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:658: INNER
                {
                	mINNER(); 

                }
                break;
            case 91 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:664: INSERT
                {
                	mINSERT(); 

                }
                break;
            case 92 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:671: INSTEAD
                {
                	mINSTEAD(); 

                }
                break;
            case 93 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:679: INTERSECT
                {
                	mINTERSECT(); 

                }
                break;
            case 94 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:689: INTO
                {
                	mINTO(); 

                }
                break;
            case 95 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:694: IS
                {
                	mIS(); 

                }
                break;
            case 96 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:697: ISNULL
                {
                	mISNULL(); 

                }
                break;
            case 97 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:704: JOIN
                {
                	mJOIN(); 

                }
                break;
            case 98 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:709: KEY
                {
                	mKEY(); 

                }
                break;
            case 99 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:713: LEFT
                {
                	mLEFT(); 

                }
                break;
            case 100 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:718: LIKE
                {
                	mLIKE(); 

                }
                break;
            case 101 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:723: LIMIT
                {
                	mLIMIT(); 

                }
                break;
            case 102 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:729: MATCH
                {
                	mMATCH(); 

                }
                break;
            case 103 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:735: NATURAL
                {
                	mNATURAL(); 

                }
                break;
            case 104 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:743: NOT
                {
                	mNOT(); 

                }
                break;
            case 105 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:747: NOTNULL
                {
                	mNOTNULL(); 

                }
                break;
            case 106 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:755: NULL
                {
                	mNULL(); 

                }
                break;
            case 107 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:760: OF
                {
                	mOF(); 

                }
                break;
            case 108 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:763: OFFSET
                {
                	mOFFSET(); 

                }
                break;
            case 109 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:770: ON
                {
                	mON(); 

                }
                break;
            case 110 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:773: OR
                {
                	mOR(); 

                }
                break;
            case 111 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:776: ORDER
                {
                	mORDER(); 

                }
                break;
            case 112 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:782: OUTER
                {
                	mOUTER(); 

                }
                break;
            case 113 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:788: PLAN
                {
                	mPLAN(); 

                }
                break;
            case 114 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:793: PRAGMA
                {
                	mPRAGMA(); 

                }
                break;
            case 115 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:800: PRIMARY
                {
                	mPRIMARY(); 

                }
                break;
            case 116 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:808: QUERY
                {
                	mQUERY(); 

                }
                break;
            case 117 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:814: RAISE
                {
                	mRAISE(); 

                }
                break;
            case 118 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:820: REFERENCES
                {
                	mREFERENCES(); 

                }
                break;
            case 119 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:831: REGEXP
                {
                	mREGEXP(); 

                }
                break;
            case 120 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:838: REINDEX
                {
                	mREINDEX(); 

                }
                break;
            case 121 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:846: RELEASE
                {
                	mRELEASE(); 

                }
                break;
            case 122 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:854: RENAME
                {
                	mRENAME(); 

                }
                break;
            case 123 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:861: REPLACE
                {
                	mREPLACE(); 

                }
                break;
            case 124 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:869: RESTRICT
                {
                	mRESTRICT(); 

                }
                break;
            case 125 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:878: ROLLBACK
                {
                	mROLLBACK(); 

                }
                break;
            case 126 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:887: ROW
                {
                	mROW(); 

                }
                break;
            case 127 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:891: SAVEPOINT
                {
                	mSAVEPOINT(); 

                }
                break;
            case 128 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:901: SELECT
                {
                	mSELECT(); 

                }
                break;
            case 129 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:908: SET
                {
                	mSET(); 

                }
                break;
            case 130 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:912: TABLE
                {
                	mTABLE(); 

                }
                break;
            case 131 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:918: TEMPORARY
                {
                	mTEMPORARY(); 

                }
                break;
            case 132 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:928: TEMP
                {
                	mTEMP(); 

                }
                break;
            case 133 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:933: THEN
                {
                	mTHEN(); 

                }
                break;
            case 134 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:938: TO
                {
                	mTO(); 

                }
                break;
            case 135 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:941: TRANSACTION
                {
                	mTRANSACTION(); 

                }
                break;
            case 136 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:953: TRIGGER
                {
                	mTRIGGER(); 

                }
                break;
            case 137 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:961: UNION
                {
                	mUNION(); 

                }
                break;
            case 138 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:967: UNIQUE
                {
                	mUNIQUE(); 

                }
                break;
            case 139 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:974: UPDATE
                {
                	mUPDATE(); 

                }
                break;
            case 140 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:981: USING
                {
                	mUSING(); 

                }
                break;
            case 141 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:987: VACUUM
                {
                	mVACUUM(); 

                }
                break;
            case 142 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:994: VALUES
                {
                	mVALUES(); 

                }
                break;
            case 143 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1001: VIEW
                {
                	mVIEW(); 

                }
                break;
            case 144 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1006: VIRTUAL
                {
                	mVIRTUAL(); 

                }
                break;
            case 145 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1014: WHEN
                {
                	mWHEN(); 

                }
                break;
            case 146 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1019: WHERE
                {
                	mWHERE(); 

                }
                break;
            case 147 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1025: DIV
                {
                	mDIV(); 

                }
                break;
            case 148 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1029: MOD
                {
                	mMOD(); 

                }
                break;
            case 149 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1033: FULLTEXT
                {
                	mFULLTEXT(); 

                }
                break;
            case 150 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1042: SPATIAL
                {
                	mSPATIAL(); 

                }
                break;
            case 151 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1050: KEY_BLOCK_SIZE
                {
                	mKEY_BLOCK_SIZE(); 

                }
                break;
            case 152 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1065: WITH
                {
                	mWITH(); 

                }
                break;
            case 153 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1070: PARSER
                {
                	mPARSER(); 

                }
                break;
            case 154 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1077: XOR
                {
                	mXOR(); 

                }
                break;
            case 155 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1081: AUTO_INCREMENT
                {
                	mAUTO_INCREMENT(); 

                }
                break;
            case 156 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1096: COLUMN_FORMAT
                {
                	mCOLUMN_FORMAT(); 

                }
                break;
            case 157 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1110: FIXED
                {
                	mFIXED(); 

                }
                break;
            case 158 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1116: DYNAMIC
                {
                	mDYNAMIC(); 

                }
                break;
            case 159 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1124: STORAGE
                {
                	mSTORAGE(); 

                }
                break;
            case 160 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1132: DISK
                {
                	mDISK(); 

                }
                break;
            case 161 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1137: MEMORY
                {
                	mMEMORY(); 

                }
                break;
            case 162 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1144: FULL
                {
                	mFULL(); 

                }
                break;
            case 163 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1149: PARTIAL
                {
                	mPARTIAL(); 

                }
                break;
            case 164 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1157: SIMPLE
                {
                	mSIMPLE(); 

                }
                break;
            case 165 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1164: NO
                {
                	mNO(); 

                }
                break;
            case 166 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1167: ACTION
                {
                	mACTION(); 

                }
                break;
            case 167 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1174: ENGINE
                {
                	mENGINE(); 

                }
                break;
            case 168 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1181: AVG_ROW_LENGTH
                {
                	mAVG_ROW_LENGTH(); 

                }
                break;
            case 169 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1196: CHECKSUM
                {
                	mCHECKSUM(); 

                }
                break;
            case 170 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1205: DATA
                {
                	mDATA(); 

                }
                break;
            case 171 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1210: DIRECTORY
                {
                	mDIRECTORY(); 

                }
                break;
            case 172 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1220: DELAY_KEY_WRITE
                {
                	mDELAY_KEY_WRITE(); 

                }
                break;
            case 173 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1236: INSERT_METHOD
                {
                	mINSERT_METHOD(); 

                }
                break;
            case 174 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1250: FIRST
                {
                	mFIRST(); 

                }
                break;
            case 175 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1256: LAST
                {
                	mLAST(); 

                }
                break;
            case 176 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1261: MAX_ROWS
                {
                	mMAX_ROWS(); 

                }
                break;
            case 177 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1270: MIN_ROWS
                {
                	mMIN_ROWS(); 

                }
                break;
            case 178 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1279: PACK_KEYS
                {
                	mPACK_KEYS(); 

                }
                break;
            case 179 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1289: PASSWORD
                {
                	mPASSWORD(); 

                }
                break;
            case 180 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1298: ROW_FORMAT
                {
                	mROW_FORMAT(); 

                }
                break;
            case 181 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1309: COMPRESSED
                {
                	mCOMPRESSED(); 

                }
                break;
            case 182 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1320: REDUNDANT
                {
                	mREDUNDANT(); 

                }
                break;
            case 183 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1330: COMPACT
                {
                	mCOMPACT(); 

                }
                break;
            case 184 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1338: TABLESPACE
                {
                	mTABLESPACE(); 

                }
                break;
            case 185 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1349: CHARSET
                {
                	mCHARSET(); 

                }
                break;
            case 186 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1357: BIT
                {
                	mBIT(); 

                }
                break;
            case 187 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1361: TINYINT
                {
                	mTINYINT(); 

                }
                break;
            case 188 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1369: SMALLINT
                {
                	mSMALLINT(); 

                }
                break;
            case 189 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1378: MEDIUMINT
                {
                	mMEDIUMINT(); 

                }
                break;
            case 190 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1388: INT
                {
                	mINT(); 

                }
                break;
            case 191 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1392: INTEGER
                {
                	mINTEGER(); 

                }
                break;
            case 192 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1400: BIGINT
                {
                	mBIGINT(); 

                }
                break;
            case 193 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1407: REAL
                {
                	mREAL(); 

                }
                break;
            case 194 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1412: DOUBLE
                {
                	mDOUBLE(); 

                }
                break;
            case 195 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1419: FLOAT
                {
                	mFLOAT(); 

                }
                break;
            case 196 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1425: DECIMAL
                {
                	mDECIMAL(); 

                }
                break;
            case 197 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1433: NUMERIC
                {
                	mNUMERIC(); 

                }
                break;
            case 198 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1441: DATE
                {
                	mDATE(); 

                }
                break;
            case 199 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1446: TIME
                {
                	mTIME(); 

                }
                break;
            case 200 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1451: TIMESTAMP
                {
                	mTIMESTAMP(); 

                }
                break;
            case 201 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1461: DATETIME
                {
                	mDATETIME(); 

                }
                break;
            case 202 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1470: CHAR
                {
                	mCHAR(); 

                }
                break;
            case 203 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1475: VARCHAR
                {
                	mVARCHAR(); 

                }
                break;
            case 204 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1483: BINARY
                {
                	mBINARY(); 

                }
                break;
            case 205 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1490: VARBINARY
                {
                	mVARBINARY(); 

                }
                break;
            case 206 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1500: TINYBLOB
                {
                	mTINYBLOB(); 

                }
                break;
            case 207 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1509: BLOB
                {
                	mBLOB(); 

                }
                break;
            case 208 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1514: MEDIUMBLOB
                {
                	mMEDIUMBLOB(); 

                }
                break;
            case 209 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1525: LONGBLOB
                {
                	mLONGBLOB(); 

                }
                break;
            case 210 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1534: TINYTEXT
                {
                	mTINYTEXT(); 

                }
                break;
            case 211 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1543: TEXT
                {
                	mTEXT(); 

                }
                break;
            case 212 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1548: MEDIUMTEXT
                {
                	mMEDIUMTEXT(); 

                }
                break;
            case 213 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1559: LONGTEXT
                {
                	mLONGTEXT(); 

                }
                break;
            case 214 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1568: ENUM
                {
                	mENUM(); 

                }
                break;
            case 215 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1573: CHARACTER
                {
                	mCHARACTER(); 

                }
                break;
            case 216 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1583: UNSIGNED
                {
                	mUNSIGNED(); 

                }
                break;
            case 217 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1592: ZEROFILL
                {
                	mZEROFILL(); 

                }
                break;
            case 218 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1601: CONNECTION
                {
                	mCONNECTION(); 

                }
                break;
            case 219 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1612: COMMENT
                {
                	mCOMMENT(); 

                }
                break;
            case 220 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1620: GEOMETRY
                {
                	mGEOMETRY(); 

                }
                break;
            case 221 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1629: POINT
                {
                	mPOINT(); 

                }
                break;
            case 222 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1635: LINESTRING
                {
                	mLINESTRING(); 

                }
                break;
            case 223 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1646: POLYGON
                {
                	mPOLYGON(); 

                }
                break;
            case 224 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1654: MULTIPOINT
                {
                	mMULTIPOINT(); 

                }
                break;
            case 225 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1665: MULTILINESTRING
                {
                	mMULTILINESTRING(); 

                }
                break;
            case 226 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1681: MULTIPOLYGON
                {
                	mMULTIPOLYGON(); 

                }
                break;
            case 227 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1694: GEOMETRYCOLLECTION
                {
                	mGEOMETRYCOLLECTION(); 

                }
                break;
            case 228 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1713: TYPE
                {
                	mTYPE(); 

                }
                break;
            case 229 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1718: RIGHT
                {
                	mRIGHT(); 

                }
                break;
            case 230 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1724: ALGORITHM
                {
                	mALGORITHM(); 

                }
                break;
            case 231 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1734: DEFINER
                {
                	mDEFINER(); 

                }
                break;
            case 232 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1742: INVOKER
                {
                	mINVOKER(); 

                }
                break;
            case 233 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1750: SQL
                {
                	mSQL(); 

                }
                break;
            case 234 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1754: SECURITY
                {
                	mSECURITY(); 

                }
                break;
            case 235 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1763: CASCADED
                {
                	mCASCADED(); 

                }
                break;
            case 236 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1772: LOCAL
                {
                	mLOCAL(); 

                }
                break;
            case 237 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1778: OPTION
                {
                	mOPTION(); 

                }
                break;
            case 238 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1785: DISTINCTROW
                {
                	mDISTINCTROW(); 

                }
                break;
            case 239 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1797: HIGH_PRIORITY
                {
                	mHIGH_PRIORITY(); 

                }
                break;
            case 240 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1811: STRAIGHT_JOIN
                {
                	mSTRAIGHT_JOIN(); 

                }
                break;
            case 241 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1825: SQL_SMALL_RESULT
                {
                	mSQL_SMALL_RESULT(); 

                }
                break;
            case 242 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1842: SQL_BIG_RESULT
                {
                	mSQL_BIG_RESULT(); 

                }
                break;
            case 243 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1857: SQL_BUFFER_RESULT
                {
                	mSQL_BUFFER_RESULT(); 

                }
                break;
            case 244 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1875: SQL_CACHE
                {
                	mSQL_CACHE(); 

                }
                break;
            case 245 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1885: SQL_NO_CACHE
                {
                	mSQL_NO_CACHE(); 

                }
                break;
            case 246 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1898: SQL_CALC_FOUND_ROWS
                {
                	mSQL_CALC_FOUND_ROWS(); 

                }
                break;
            case 247 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1918: ROLLUP
                {
                	mROLLUP(); 

                }
                break;
            case 248 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1925: SHARE
                {
                	mSHARE(); 

                }
                break;
            case 249 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1931: LOCK
                {
                	mLOCK(); 

                }
                break;
            case 250 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1936: MODE
                {
                	mMODE(); 

                }
                break;
            case 251 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1941: FORCE
                {
                	mFORCE(); 

                }
                break;
            case 252 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1947: OJ
                {
                	mOJ(); 

                }
                break;
            case 253 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1950: USE
                {
                	mUSE(); 

                }
                break;
            case 254 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1954: INTERVAL
                {
                	mINTERVAL(); 

                }
                break;
            case 255 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1963: CURRENT_TIME
                {
                	mCURRENT_TIME(); 

                }
                break;
            case 256 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1976: CURRENT_DATE
                {
                	mCURRENT_DATE(); 

                }
                break;
            case 257 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:1989: CURRENT_TIMESTAMP
                {
                	mCURRENT_TIMESTAMP(); 

                }
                break;
            case 258 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:2007: UTC_TIMESTAMP
                {
                	mUTC_TIMESTAMP(); 

                }
                break;
            case 259 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:2021: CURRENT_USER
                {
                	mCURRENT_USER(); 

                }
                break;
            case 260 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:2034: YEAR
                {
                	mYEAR(); 

                }
                break;
            case 261 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:2039: T_IDENT
                {
                	mT_IDENT(); 

                }
                break;
            case 262 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:2047: T_QUOTED_IDENT
                {
                	mT_QUOTED_IDENT(); 

                }
                break;
            case 263 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:2062: T_NSTRING
                {
                	mT_NSTRING(); 

                }
                break;
            case 264 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:2072: T_STRING
                {
                	mT_STRING(); 

                }
                break;
            case 265 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:2081: T_INTEGER
                {
                	mT_INTEGER(); 

                }
                break;
            case 266 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:2091: T_FLOAT
                {
                	mT_FLOAT(); 

                }
                break;
            case 267 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:2099: T_BLOB
                {
                	mT_BLOB(); 

                }
                break;
            case 268 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:2106: T_BINARYNUM
                {
                	mT_BINARYNUM(); 

                }
                break;
            case 269 :
                // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:1:2118: WHITESPACE
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
        "\x02\uffff\x01\x02\x01\x03\x01\x01";
    const string DFA14_specialS =
        "\x05\uffff}>";
    static readonly string[] DFA14_transitionS = {
            "\x01\x02\x01\uffff\x0a\x01",
            "\x01\x04\x01\uffff\x0a\x01\x0b\uffff\x01\x03\x1f\uffff\x01"+
            "\x03",
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
            get { return "705:1: T_FLOAT : ( ( '0' .. '9' )+ '.' ( '0' .. '9' )* ( FLOAT_EXP )? | '.' ( '0' .. '9' )+ ( FLOAT_EXP )? | ( '0' .. '9' )+ FLOAT_EXP );"; }
        }

    }

    const string DFA24_eotS =
        "\x01\uffff\x01\x39\x01\x3d\x01\x3f\x01\x42\x01\x44\x01\x46\x01"+
        "\uffff\x01\x47\x02\uffff\x01\x48\x02\uffff\x01\x49\x08\uffff\x1a"+
        "\x31\x03\uffff\x02\u00b3\x01\x31\x03\uffff\x01\u00b5\x10\uffff\x03"+
        "\x31\x01\u00bb\x06\x31\x01\uffff\x01\u00c4\x1e\x31\x01\u00f6\x01"+
        "\x31\x01\u00f9\x01\u0100\x0c\x31\x01\u0112\x01\uffff\x03\x31\x01"+
        "\u0118\x01\u011a\x01\u011b\x01\x31\x01\u011e\x14\x31\x01\u0144\x0b"+
        "\x31\x01\uffff\x03\x31\x03\uffff\x01\x31\x01\u015b\x03\x31\x01\uffff"+
        "\x01\u015f\x02\x31\x01\u0162\x01\x31\x01\u0164\x02\x31\x01\uffff"+
        "\x05\x31\x01\u016c\x0b\x31\x01\u017e\x0d\x31\x01\u0191\x05\x31\x01"+
        "\u0198\x0b\x31\x01\uffff\x02\x31\x01\uffff\x01\u01a8\x05\x31\x01"+
        "\uffff\x02\x31\x01\u01b3\x0b\x31\x01\u01c1\x02\x31\x01\uffff\x01"+
        "\u01c5\x04\x31\x01\uffff\x01\x31\x02\uffff\x02\x31\x01\uffff\x0a"+
        "\x31\x01\u01d9\x0d\x31\x01\u01e8\x02\x31\x01\u01eb\x09\x31\x01\uffff"+
        "\x05\x31\x01\u01fb\x0c\x31\x01\u020b\x03\x31\x01\uffff\x03\x31\x01"+
        "\uffff\x02\x31\x01\uffff\x01\x31\x01\uffff\x05\x31\x01\u021b\x01"+
        "\x31\x01\uffff\x0b\x31\x01\u022a\x02\x31\x01\u022f\x01\u0230\x01"+
        "\x31\x01\uffff\x01\u0232\x03\x31\x01\u0236\x05\x31\x01\u023c\x01"+
        "\u023e\x01\u0240\x03\x31\x01\u0244\x01\x31\x01\uffff\x04\x31\x01"+
        "\u024a\x01\u024b\x01\uffff\x03\x31\x01\u024f\x01\u0251\x02\x31\x01"+
        "\u0254\x01\x31\x01\u0256\x05\x31\x01\uffff\x01\x31\x01\u025e\x07"+
        "\x31\x01\u0266\x01\uffff\x03\x31\x01\u026b\x01\u026c\x02\x31\x01"+
        "\u026f\x01\u0270\x04\x31\x01\uffff\x01\u0275\x02\x31\x01\uffff\x01"+
        "\x31\x01\u0279\x0e\x31\x01\u0288\x02\x31\x01\uffff\x02\x31\x01\u028e"+
        "\x0b\x31\x01\uffff\x02\x31\x01\uffff\x06\x31\x01\u02a5\x01\x31\x01"+
        "\u02a7\x01\x31\x01\u02ac\x01\u02ae\x01\u02af\x02\x31\x01\uffff\x07"+
        "\x31\x01\u02b9\x04\x31\x01\u02be\x01\u02bf\x01\x31\x01\uffff\x01"+
        "\x31\x01\u02c2\x01\x31\x01\u02c4\x01\u02c5\x06\x31\x01\u02cc\x02"+
        "\x31\x01\u02cf\x01\uffff\x0c\x31\x01\u02dc\x01\x31\x01\uffff\x02"+
        "\x31\x01\u02e0\x01\x31\x02\uffff\x01\x31\x01\uffff\x03\x31\x01\uffff"+
        "\x05\x31\x01\uffff\x01\x31\x01\uffff\x01\x31\x01\uffff\x03\x31\x01"+
        "\uffff\x05\x31\x02\uffff\x01\x31\x01\u02f7\x01\u02f8\x01\uffff\x01"+
        "\x31\x01\uffff\x01\u02fa\x01\u02fb\x01\uffff\x01\x31\x01\uffff\x01"+
        "\u02fd\x06\x31\x01\uffff\x01\x31\x01\u0306\x03\x31\x01\u030a\x01"+
        "\x31\x01\uffff\x03\x31\x01\u0310\x02\uffff\x01\x31\x01\u0312\x02"+
        "\uffff\x04\x31\x01\uffff\x01\u0318\x02\x31\x01\uffff\x03\x31\x01"+
        "\u031e\x01\x31\x01\u0320\x02\x31\x01\u0323\x05\x31\x01\uffff\x01"+
        "\u0329\x03\x31\x01\u032d\x01\uffff\x08\x31\x01\u0336\x0a\x31\x01"+
        "\u0342\x02\x31\x01\uffff\x01\u0345\x01\uffff\x04\x31\x01\uffff\x01"+
        "\x31\x02\uffff\x02\x31\x01\u034e\x03\x31\x01\u0352\x02\x31\x01\uffff"+
        "\x04\x31\x02\uffff\x01\u0359\x01\x31\x01\uffff\x01\x31\x02\uffff"+
        "\x02\x31\x01\u035e\x01\u035f\x02\x31\x01\uffff\x01\u0362\x01\x31"+
        "\x01\uffff\x01\u0364\x01\u0365\x03\x31\x01\u0369\x03\x31\x01\u036d"+
        "\x02\x31\x01\uffff\x01\u0371\x02\x31\x01\uffff\x04\x31\x01\u0378"+
        "\x01\x31\x01\u037a\x07\x31\x01\u0383\x01\u0384\x01\u0385\x02\x31"+
        "\x01\u0388\x01\u0389\x01\x31\x02\uffff\x01\x31\x02\uffff\x01\x31"+
        "\x01\uffff\x01\x31\x01\u038e\x01\u038f\x05\x31\x01\uffff\x01\u0395"+
        "\x02\x31\x01\uffff\x01\x31\x01\u039a\x03\x31\x01\uffff\x01\x31\x01"+
        "\uffff\x04\x31\x01\u03a5\x01\uffff\x04\x31\x01\u03aa\x01\uffff\x01"+
        "\u03ab\x01\uffff\x01\u03ac\x01\x31\x01\uffff\x01\x31\x01\u03af\x03"+
        "\x31\x01\uffff\x01\x31\x01\u03b4\x01\x31\x01\uffff\x01\x31\x01\u03b7"+
        "\x04\x31\x01\u03bc\x01\x31\x01\uffff\x01\x31\x01\u03bf\x09\x31\x01"+
        "\uffff\x01\u03ca\x01\x31\x01\uffff\x08\x31\x01\uffff\x01\u03d4\x02"+
        "\x31\x01\uffff\x01\u03d7\x03\x31\x01\u03db\x01\u03dc\x01\uffff\x04"+
        "\x31\x02\uffff\x01\u03e1\x01\x31\x01\uffff\x01\u03e3\x02\uffff\x01"+
        "\u03e4\x01\x31\x01\u03e6\x01\uffff\x03\x31\x01\uffff\x01\x31\x01"+
        "\u03eb\x01\x31\x01\uffff\x01\u03ed\x02\x31\x01\u03f0\x02\x31\x01"+
        "\uffff\x01\x31\x01\uffff\x02\x31\x01\u03f7\x01\u03f8\x01\u03f9\x02"+
        "\x31\x01\u03fc\x03\uffff\x01\u03fd\x01\x31\x02\uffff\x01\u03ff\x03"+
        "\x31\x02\uffff\x03\x31\x01\u0406\x01\u0407\x01\uffff\x01\x31\x01"+
        "\u0409\x01\x31\x01\u040b\x01\uffff\x0a\x31\x01\uffff\x01\x31\x01"+
        "\u0418\x01\u0419\x01\u041a\x03\uffff\x01\u041b\x01\u041c\x01\uffff"+
        "\x01\u041d\x03\x31\x01\uffff\x01\x31\x01\u0422\x01\uffff\x01\x31"+
        "\x01\u0424\x01\x31\x01\u0426\x01\uffff\x02\x31\x01\uffff\x01\x31"+
        "\x01\u042a\x06\x31\x01\u0431\x01\x31\x01\uffff\x04\x31\x01\u0437"+
        "\x02\x31\x01\u043a\x01\x31\x01\uffff\x02\x31\x01\uffff\x01\u043e"+
        "\x01\u043f\x01\x31\x02\uffff\x04\x31\x01\uffff\x01\x31\x02\uffff"+
        "\x01\x31\x01\uffff\x01\x31\x01\u0448\x02\x31\x01\uffff\x01\x31\x01"+
        "\uffff\x01\x31\x01\u044f\x01\uffff\x01\u0450\x01\x31\x01\u0452\x02"+
        "\x31\x01\u0456\x03\uffff\x01\u0457\x01\u0458\x02\uffff\x01\x31\x01"+
        "\uffff\x01\u045a\x01\u045b\x02\x31\x01\u045f\x01\x31\x02\uffff\x01"+
        "\x31\x01\uffff\x01\x31\x01\uffff\x01\x31\x01\u0464\x01\u0465\x04"+
        "\x31\x01\u046a\x03\x31\x01\u046e\x06\uffff\x01\x31\x01\u0470\x01"+
        "\u0471\x01\x31\x01\uffff\x01\x31\x01\uffff\x01\u0474\x01\uffff\x01"+
        "\x31\x01\u0476\x01\x31\x01\uffff\x06\x31\x01\uffff\x01\x31\x01\u047f"+
        "\x02\x31\x01\u0482\x01\uffff\x01\u0483\x01\x31\x01\uffff\x02\x31"+
        "\x01\u0487\x02\uffff\x01\x31\x01\u0489\x01\u048a\x05\x31\x01\uffff"+
        "\x05\x31\x01\u0495\x02\uffff\x01\u0496\x01\uffff\x03\x31\x03\uffff"+
        "\x01\u049a\x02\uffff\x02\x31\x01\u049d\x01\uffff\x01\u049e\x01\x31"+
        "\x01\u04a0\x01\x31\x02\uffff\x04\x31\x01\uffff\x01\u04a6\x02\x31"+
        "\x01\uffff\x01\u04a9\x02\uffff\x01\x31\x01\u04ab\x01\uffff\x01\x31"+
        "\x01\uffff\x05\x31\x01\u04b2\x01\x31\x01\u04b4\x01\uffff\x01\x31"+
        "\x01\u04b6\x02\uffff\x01\u04b7\x02\x31\x01\uffff\x01\u04ba\x02\uffff"+
        "\x03\x31\x01\u04be\x01\u04bf\x01\u04c0\x04\x31\x02\uffff\x02\x31"+
        "\x01\u04c7\x01\uffff\x02\x31\x02\uffff\x01\x31\x01\uffff\x01\x31"+
        "\x01\u04cc\x01\u04cd\x02\x31\x01\uffff\x01\u04d0\x01\u04d1\x01\uffff"+
        "\x01\u04d2\x01\uffff\x01\u04d3\x05\x31\x01\uffff\x01\x31\x01\uffff"+
        "\x01\u04da\x02\uffff\x02\x31\x01\uffff\x03\x31\x03\uffff\x04\x31"+
        "\x01\u04e4\x01\x31\x01\uffff\x04\x31\x02\uffff\x02\x31\x04\uffff"+
        "\x06\x31\x01\uffff\x01\u04f2\x05\x31\x01\u04f8\x01\u04f9\x01\u04fb"+
        "\x01\uffff\x05\x31\x01\u0501\x06\x31\x01\u0508\x01\uffff\x01\x31"+
        "\x01\u050a\x02\x31\x01\u050d\x02\uffff\x01\x31\x01\uffff\x02\x31"+
        "\x01\u0511\x01\u0512\x01\x31\x01\uffff\x01\x31\x01\u0515\x04\x31"+
        "\x01\uffff\x01\u051a\x01\uffff\x01\u051b\x01\u051c\x01\uffff\x03"+
        "\x31\x02\uffff\x01\u0520\x01\x31\x01\uffff\x01\u0522\x03\x31\x03"+
        "\uffff\x01\x31\x01\u0527\x01\x31\x01\uffff\x01\u0529\x01\uffff\x04"+
        "\x31\x01\uffff\x01\x31\x01\uffff\x01\x31\x01\u0530\x01\x31\x01\u0532"+
        "\x01\x31\x01\u0534\x01\uffff\x01\x31\x01\uffff\x01\u0536\x01\uffff"+
        "\x01\x31\x01\uffff\x01\u0538\x01\uffff";
    const string DFA24_eofS =
        "\u0539\uffff";
    const string DFA24_minS =
        "\x01\x09\x01\x3d\x01\x3c\x02\x3d\x01\x26\x01\x7c\x01\uffff\x01"+
        "\x2d\x02\uffff\x01\x2a\x02\uffff\x01\x30\x08\uffff\x01\x42\x01\x27"+
        "\x04\x41\x01\x45\x01\x41\x01\x46\x01\x4f\x01\x45\x02\x41\x01\x27"+
        "\x01\x46\x01\x41\x01\x55\x03\x41\x01\x4e\x01\x41\x01\x48\x01\x27"+
        "\x02\x45\x03\uffff\x02\x2e\x01\x45\x03\uffff\x01\x3e\x10\uffff\x01"+
        "\x47\x02\x54\x01\x30\x02\x54\x01\x44\x01\x41\x01\x47\x01\x4f\x01"+
        "\uffff\x01\x30\x01\x46\x01\x4f\x01\x47\x01\x4c\x01\x52\x01\x45\x01"+
        "\x41\x01\x53\x01\x52\x01\x43\x01\x54\x01\x4f\x01\x4e\x01\x55\x01"+
        "\x43\x01\x44\x02\x43\x01\x53\x01\x52\x01\x4f\x01\x4c\x01\x4f\x01"+
        "\x52\x01\x49\x03\x4f\x01\x47\x01\x56\x01\x30\x01\x4d\x02\x30\x01"+
        "\x4e\x01\x49\x01\x59\x01\x43\x01\x4b\x01\x53\x01\x46\x01\x4c\x01"+
        "\x4e\x02\x44\x01\x54\x01\x30\x01\uffff\x01\x4c\x02\x54\x03\x30\x01"+
        "\x54\x01\x30\x01\x41\x01\x49\x01\x43\x01\x41\x01\x45\x01\x4c\x01"+
        "\x47\x01\x41\x01\x49\x01\x43\x01\x4f\x01\x4c\x01\x41\x01\x56\x01"+
        "\x41\x01\x4d\x01\x41\x01\x50\x01\x42\x01\x4d\x01\x30\x01\x4d\x01"+
        "\x45\x01\x41\x01\x45\x01\x44\x01\x43\x01\x49\x01\x45\x01\x43\x01"+
        "\x54\x01\x45\x01\uffff\x02\x52\x01\x41\x03\uffff\x01\x4f\x01\x30"+
        "\x02\x45\x01\x4f\x01\uffff\x01\x30\x01\x41\x01\x49\x01\x30\x01\x4c"+
        "\x01\x30\x01\x5f\x01\x52\x01\uffff\x01\x4f\x01\x57\x01\x49\x01\x42"+
        "\x01\x41\x01\x30\x01\x49\x01\x4d\x01\x46\x01\x4c\x01\x52\x01\x53"+
        "\x01\x41\x01\x52\x02\x43\x01\x45\x01\x30\x01\x4b\x01\x41\x01\x43"+
        "\x02\x41\x01\x49\x01\x41\x01\x50\x01\x41\x01\x42\x01\x41\x01\x4d"+
        "\x01\x49\x01\x30\x01\x4c\x01\x45\x01\x53\x01\x48\x01\x45\x01\x30"+
        "\x01\x41\x01\x4c\x01\x4d\x01\x45\x01\x53\x01\x4c\x01\x4d\x01\x42"+
        "\x01\x55\x01\x48\x01\x49\x01\uffff\x01\x55\x01\x45\x01\uffff\x01"+
        "\x30\x01\x4f\x02\x45\x01\x54\x01\x45\x01\uffff\x01\x4f\x01\x4e\x01"+
        "\x30\x01\x47\x01\x41\x02\x45\x01\x49\x03\x54\x01\x5f\x01\x49\x01"+
        "\x4f\x01\x30\x01\x43\x01\x5f\x01\uffff\x01\x30\x01\x4c\x01\x45\x01"+
        "\x55\x01\x49\x01\uffff\x01\x45\x02\uffff\x01\x53\x01\x45\x01\uffff"+
        "\x01\x47\x01\x4d\x01\x4e\x01\x59\x01\x53\x01\x4b\x01\x53\x01\x4e"+
        "\x01\x52\x01\x4c\x01\x30\x01\x48\x01\x4c\x01\x4e\x01\x45\x01\x55"+
        "\x01\x45\x01\x54\x01\x4c\x01\x41\x01\x45\x01\x53\x01\x55\x01\x45"+
        "\x01\x30\x01\x41\x01\x52\x01\x30\x01\x54\x01\x45\x01\x52\x01\x50"+
        "\x01\x4c\x01\x45\x01\x4c\x01\x45\x01\x59\x01\uffff\x01\x50\x01\x54"+
        "\x01\x4e\x01\x47\x01\x4e\x01\x30\x01\x4e\x01\x41\x01\x5f\x01\x49"+
        "\x01\x4f\x01\x54\x01\x57\x01\x42\x02\x55\x01\x48\x01\x4e\x01\x30"+
        "\x01\x4f\x02\x52\x01\uffff\x02\x52\x01\x49\x01\uffff\x01\x43\x01"+
        "\x4f\x01\uffff\x01\x59\x01\uffff\x01\x52\x01\x54\x01\x52\x01\x45"+
        "\x01\x4e\x01\x30\x01\x52\x01\uffff\x01\x4e\x01\x41\x01\x45\x01\x54"+
        "\x01\x4c\x01\x45\x01\x4d\x01\x41\x01\x45\x01\x53\x01\x54\x01\x30"+
        "\x01\x4b\x01\x41\x02\x30\x01\x43\x01\uffff\x01\x30\x01\x49\x01\x54"+
        "\x01\x59\x01\x30\x01\x43\x01\x52\x01\x55\x01\x4e\x01\x4d\x03\x30"+
        "\x01\x4d\x01\x4c\x01\x50\x01\x30\x01\x4e\x01\uffff\x01\x41\x01\x55"+
        "\x01\x50\x01\x54\x02\x30\x01\uffff\x01\x49\x01\x45\x01\x54\x02\x30"+
        "\x01\x44\x01\x54\x01\x30\x01\x45\x01\x30\x01\x50\x01\x5f\x01\x4e"+
        "\x01\x4c\x01\x44\x01\uffff\x01\x47\x01\x30\x01\x4b\x02\x52\x01\x45"+
        "\x01\x49\x01\x58\x01\x52\x01\x30\x01\uffff\x02\x42\x01\x4c\x02\x30"+
        "\x01\x53\x01\x54\x02\x30\x01\x49\x01\x52\x01\x55\x01\x52\x01\uffff"+
        "\x01\x30\x01\x48\x01\x52\x01\uffff\x01\x55\x01\x30\x02\x52\x01\x4f"+
        "\x01\x52\x01\x45\x01\x52\x01\x4d\x01\x41\x01\x54\x01\x47\x01\x45"+
        "\x01\x49\x01\x5f\x01\x57\x01\x30\x01\x59\x01\x42\x01\uffff\x01\x46"+
        "\x01\x54\x01\x30\x01\x44\x01\x58\x01\x4e\x01\x41\x01\x52\x01\x41"+
        "\x01\x4d\x01\x52\x01\x45\x01\x52\x01\x43\x01\uffff\x01\x49\x01\x41"+
        "\x01\uffff\x01\x42\x01\x49\x01\x50\x01\x45\x02\x4c\x01\x30\x01\x45"+
        "\x01\x30\x01\x42\x03\x30\x01\x47\x01\x53\x01\uffff\x01\x47\x02\x54"+
        "\x01\x47\x01\x4e\x02\x55\x01\x30\x01\x48\x01\x49\x01\x45\x01\x55"+
        "\x02\x30\x01\x45\x01\uffff\x01\x46\x01\x30\x01\x49\x02\x30\x01\x4e"+
        "\x01\x49\x01\x48\x01\x4e\x01\x5a\x01\x4f\x01\x30\x02\x45\x01\x30"+
        "\x01\uffff\x01\x59\x01\x54\x01\x43\x01\x45\x01\x4e\x01\x54\x01\x52"+
        "\x01\x49\x01\x43\x01\x4e\x01\x54\x01\x4e\x01\x30\x01\x45\x01\uffff"+
        "\x01\x45\x01\x43\x01\x30\x01\x44\x02\uffff\x01\x54\x01\uffff\x01"+
        "\x4e\x01\x45\x01\x5f\x01\uffff\x01\x48\x01\x52\x01\x4c\x01\x45\x01"+
        "\x41\x01\uffff\x01\x49\x01\uffff\x01\x41\x01\uffff\x01\x49\x02\x45"+
        "\x01\uffff\x01\x45\x01\x49\x01\x53\x01\x54\x01\x53\x02\uffff\x01"+
        "\x47\x02\x30\x01\uffff\x01\x45\x01\uffff\x02\x30\x01\uffff\x01\x54"+
        "\x01\uffff\x01\x30\x01\x50\x01\x47\x01\x4c\x01\x49\x01\x53\x01\x45"+
        "\x01\uffff\x01\x45\x01\x30\x01\x54\x02\x41\x01\x30\x01\x45\x01\uffff"+
        "\x01\x4c\x01\x45\x01\x4c\x01\x30\x02\uffff\x01\x54\x01\x30\x02\uffff"+
        "\x01\x4c\x01\x4f\x01\x4d\x01\x59\x01\uffff\x01\x30\x01\x4f\x01\x4c"+
        "\x01\uffff\x01\x49\x01\x41\x01\x4e\x01\x30\x01\x54\x01\x30\x01\x41"+
        "\x01\x52\x01\x30\x01\x4f\x01\x52\x01\x41\x01\x4b\x01\x4f\x01\uffff"+
        "\x01\x30\x01\x41\x01\x50\x01\x4f\x01\x30\x01\uffff\x01\x45\x01\x50"+
        "\x01\x44\x01\x53\x01\x49\x01\x43\x02\x45\x01\x30\x01\x49\x01\x54"+
        "\x02\x47\x01\x49\x01\x4d\x01\x41\x01\x4f\x01\x41\x01\x4f\x01\x30"+
        "\x01\x45\x01\x49\x01\uffff\x01\x30\x01\uffff\x01\x54\x01\x4c\x01"+
        "\x4e\x01\x45\x01\uffff\x01\x52\x02\uffff\x01\x45\x01\x41\x01\x30"+
        "\x01\x45\x01\x49\x01\x4e\x01\x30\x01\x45\x01\x41\x01\uffff\x01\x41"+
        "\x01\x4e\x01\x53\x01\x4d\x02\uffff\x01\x30\x01\x49\x01\uffff\x01"+
        "\x54\x02\uffff\x01\x43\x01\x4e\x02\x30\x01\x45\x01\x57\x01\uffff"+
        "\x01\x30\x01\x4e\x01\uffff\x02\x30\x01\x54\x01\x53\x01\x54\x01\x30"+
        "\x01\x41\x01\x43\x01\x54\x01\x30\x01\x45\x01\x54\x01\uffff\x01\x30"+
        "\x02\x54\x01\uffff\x01\x55\x01\x45\x01\x4f\x01\x43\x01\x30\x01\x4b"+
        "\x01\x30\x01\x41\x01\x54\x01\x52\x01\x4c\x01\x4d\x01\x53\x01\x43"+
        "\x03\x30\x01\x4e\x01\x49\x02\x30\x01\x4e\x02\uffff\x01\x58\x02\uffff"+
        "\x01\x52\x01\uffff\x01\x52\x02\x30\x02\x41\x01\x45\x02\x52\x01\uffff"+
        "\x01\x30\x01\x44\x01\x4c\x01\uffff\x01\x44\x01\x30\x01\x4f\x01\x58"+
        "\x01\x4f\x01\uffff\x01\x52\x01\uffff\x01\x4f\x01\x49\x01\x57\x01"+
        "\x42\x01\x30\x01\uffff\x01\x57\x01\x4c\x01\x43\x01\x4c\x01\x30\x01"+
        "\uffff\x01\x30\x01\uffff\x01\x30\x01\x59\x01\uffff\x01\x4e\x01\x30"+
        "\x01\x4c\x01\x45\x01\x52\x01\uffff\x01\x43\x01\x30\x01\x52\x01\uffff"+
        "\x01\x58\x01\x30\x01\x41\x01\x45\x01\x43\x01\x45\x01\x30\x01\x4e"+
        "\x01\uffff\x01\x54\x01\x30\x01\x48\x01\x45\x01\x47\x01\x46\x01\x41"+
        "\x01\x43\x01\x5f\x01\x4c\x01\x49\x01\uffff\x01\x30\x01\x4e\x01\uffff"+
        "\x01\x50\x01\x41\x01\x4f\x01\x54\x01\x58\x01\x41\x01\x52\x01\x43"+
        "\x01\uffff\x01\x30\x01\x4d\x01\x45\x01\uffff\x01\x30\x01\x4c\x01"+
        "\x52\x01\x41\x02\x30\x01\uffff\x01\x4c\x01\x48\x01\x52\x01\x43\x02"+
        "\uffff\x01\x30\x01\x5f\x01\uffff\x01\x30\x02\uffff\x01\x30\x01\x53"+
        "\x01\x30\x01\uffff\x01\x49\x01\x54\x01\x49\x01\uffff\x01\x46\x01"+
        "\x30\x01\x5f\x01\uffff\x01\x30\x01\x45\x01\x4d\x01\x30\x01\x52\x01"+
        "\x54\x01\uffff\x01\x45\x01\uffff\x01\x42\x01\x44\x03\x30\x02\x45"+
        "\x01\x30\x03\uffff\x01\x30\x01\x56\x02\uffff\x01\x30\x01\x54\x01"+
        "\x59\x01\x49\x02\uffff\x01\x54\x01\x4c\x01\x43\x02\x30\x01\uffff"+
        "\x01\x4d\x01\x30\x01\x4c\x01\x30\x01\uffff\x01\x43\x01\x54\x01\x42"+
        "\x02\x49\x01\x4e\x01\x53\x01\x4e\x01\x4c\x01\x45\x01\uffff\x01\x53"+
        "\x03\x30\x03\uffff\x02\x30\x01\uffff\x01\x30\x01\x59\x01\x44\x01"+
        "\x4b\x01\uffff\x01\x4d\x01\x30\x01\uffff\x01\x4e\x01\x30\x01\x54"+
        "\x01\x30\x01\uffff\x01\x43\x01\x59\x01\uffff\x01\x54\x01\x30\x01"+
        "\x5f\x01\x46\x01\x4c\x01\x43\x01\x48\x01\x43\x01\x30\x01\x4e\x01"+
        "\uffff\x01\x54\x01\x41\x01\x4d\x01\x42\x01\x30\x01\x54\x01\x52\x01"+
        "\x30\x01\x54\x01\uffff\x01\x45\x01\x44\x01\uffff\x02\x30\x01\x52"+
        "\x02\uffff\x01\x4c\x01\x4d\x01\x45\x01\x52\x01\uffff\x01\x4c\x02"+
        "\uffff\x01\x45\x01\uffff\x01\x4e\x01\x30\x02\x4f\x01\uffff\x01\x44"+
        "\x01\uffff\x01\x52\x01\x30\x01\uffff\x01\x30\x01\x59\x01\x30\x01"+
        "\x59\x01\x4c\x01\x30\x03\uffff\x02\x30\x02\uffff\x01\x45\x01\uffff"+
        "\x02\x30\x01\x4f\x01\x45\x01\x30\x01\x54\x02\uffff\x01\x45\x01\uffff"+
        "\x01\x59\x01\uffff\x01\x4b\x02\x30\x02\x4e\x01\x59\x01\x45\x01\x30"+
        "\x01\x54\x01\x4f\x01\x58\x01\x30\x06\uffff\x01\x53\x02\x30\x01\x41"+
        "\x01\uffff\x01\x54\x01\uffff\x01\x30\x01\uffff\x01\x45\x01\x30\x01"+
        "\x5f\x01\uffff\x01\x52\x01\x45\x01\x4c\x01\x5f\x01\x45\x01\x41\x01"+
        "\uffff\x01\x54\x01\x30\x01\x43\x01\x50\x01\x30\x01\uffff\x01\x30"+
        "\x01\x59\x01\uffff\x01\x49\x01\x53\x01\x30\x02\uffff\x01\x59\x02"+
        "\x30\x01\x4d\x02\x45\x01\x44\x01\x54\x01\uffff\x01\x4e\x01\x52\x01"+
        "\x41\x01\x49\x01\x53\x01\x30\x02\uffff\x01\x30\x01\uffff\x01\x4f"+
        "\x01\x5f\x01\x45\x03\uffff\x01\x30\x02\uffff\x01\x4f\x01\x52\x01"+
        "\x30\x01\uffff\x01\x30\x01\x54\x01\x30\x01\x5f\x02\uffff\x01\x47"+
        "\x01\x54\x01\x47\x01\x53\x01\uffff\x01\x30\x01\x42\x01\x54\x01\uffff"+
        "\x01\x30\x02\uffff\x01\x54\x01\x30\x01\uffff\x01\x53\x01\uffff\x01"+
        "\x4a\x01\x45\x01\x52\x01\x5f\x01\x46\x01\x30\x01\x43\x01\x30\x01"+
        "\uffff\x01\x45\x01\x30\x02\uffff\x01\x30\x01\x4f\x01\x54\x01\uffff"+
        "\x01\x30\x02\uffff\x01\x45\x01\x4d\x01\x4e\x03\x30\x01\x4d\x01\x54"+
        "\x01\x4d\x01\x45\x02\uffff\x02\x57\x01\x30\x01\uffff\x01\x4c\x01"+
        "\x49\x02\uffff\x01\x48\x01\uffff\x01\x53\x02\x30\x01\x4f\x01\x54"+
        "\x01\uffff\x02\x30\x01\uffff\x01\x30\x01\uffff\x01\x30\x01\x4f\x01"+
        "\x53\x01\x5f\x01\x52\x01\x4f\x01\uffff\x01\x48\x01\uffff\x01\x30"+
        "\x02\uffff\x01\x4e\x01\x41\x01\uffff\x01\x4e\x01\x45\x01\x47\x03"+
        "\uffff\x01\x41\x02\x45\x01\x52\x01\x30\x01\x52\x01\uffff\x01\x4c"+
        "\x01\x54\x01\x4f\x01\x49\x02\uffff\x01\x4e\x01\x52\x04\uffff\x01"+
        "\x49\x01\x55\x01\x52\x01\x45\x01\x55\x01\x45\x01\uffff\x01\x30\x01"+
        "\x4d\x01\x54\x01\x4e\x02\x54\x03\x30\x01\uffff\x01\x49\x01\x45\x01"+
        "\x59\x01\x44\x01\x5a\x01\x30\x01\x49\x01\x4e\x01\x4c\x01\x45\x01"+
        "\x53\x01\x4e\x01\x30\x01\uffff\x01\x50\x01\x30\x01\x54\x01\x48\x01"+
        "\x30\x02\uffff\x01\x54\x01\uffff\x01\x54\x01\x43\x02\x30\x01\x45"+
        "\x01\uffff\x01\x4e\x01\x30\x01\x54\x01\x53\x01\x55\x01\x44\x01\uffff"+
        "\x01\x30\x01\uffff\x02\x30\x01\uffff\x01\x41\x01\x45\x01\x54\x02"+
        "\uffff\x01\x30\x01\x47\x01\uffff\x01\x30\x01\x55\x01\x4c\x01\x5f"+
        "\x03\uffff\x01\x4d\x01\x30\x01\x49\x01\uffff\x01\x30\x01\uffff\x01"+
        "\x4c\x01\x54\x01\x52\x01\x50\x01\uffff\x01\x4f\x01\uffff\x01\x54"+
        "\x01\x30\x01\x4f\x01\x30\x01\x4e\x01\x30\x01\uffff\x01\x57\x01\uffff"+
        "\x01\x30\x01\uffff\x01\x53\x01\uffff\x01\x30\x01\uffff";
    const string DFA24_maxS =
        "\x01\x7e\x01\x3d\x01\x3e\x01\x3d\x01\x3e\x01\x26\x01\x7c\x01\uffff"+
        "\x01\x2d\x02\uffff\x01\x2a\x02\uffff\x01\x39\x08\uffff\x01\x76\x01"+
        "\x79\x01\x75\x01\x79\x01\x78\x01\x75\x01\x72\x01\x69\x01\x73\x01"+
        "\x6f\x01\x65\x01\x6f\x03\x75\x01\x72\x01\x75\x01\x6f\x01\x74\x01"+
        "\x79\x01\x74\x02\x69\x01\x6f\x02\x65\x03\uffff\x02\x65\x01\x79\x03"+
        "\uffff\x01\x3e\x10\uffff\x03\x74\x01\x7a\x02\x74\x02\x64\x01\x67"+
        "\x01\x6f\x01\uffff\x01\x7a\x01\x74\x01\x6f\x01\x74\x01\x6e\x01\x72"+
        "\x01\x6f\x01\x65\x01\x73\x01\x76\x02\x74\x01\x6f\x01\x6e\x01\x75"+
        "\x01\x63\x01\x75\x01\x70\x01\x63\x01\x73\x01\x72\x01\x6f\x01\x6c"+
        "\x01\x6f\x01\x78\x01\x69\x03\x6f\x01\x67\x01\x76\x01\x7a\x01\x6d"+
        "\x02\x7a\x01\x6e\x01\x69\x01\x79\x02\x6e\x01\x73\x01\x66\x01\x6c"+
        "\x01\x6e\x01\x6d\x01\x64\x01\x78\x01\x7a\x01\uffff\x01\x6d\x02\x74"+
        "\x03\x7a\x01\x74\x01\x7a\x01\x69\x01\x6c\x01\x73\x01\x61\x01\x65"+
        "\x01\x77\x01\x67\x01\x73\x01\x69\x01\x74\x01\x72\x01\x6c\x01\x61"+
        "\x01\x76\x01\x61\x01\x6d\x01\x61\x01\x70\x01\x62\x01\x6e\x01\x7a"+
        "\x01\x78\x01\x65\x02\x69\x01\x64\x01\x63\x01\x73\x02\x72\x01\x74"+
        "\x01\x65\x01\uffff\x02\x72\x01\x61\x03\uffff\x01\x6f\x01\x7a\x02"+
        "\x65\x01\x6f\x01\uffff\x01\x7a\x01\x61\x01\x69\x01\x7a\x01\x6c\x01"+
        "\x7a\x01\x5f\x01\x72\x01\uffff\x01\x6f\x01\x77\x01\x69\x01\x62\x01"+
        "\x61\x01\x7a\x01\x69\x01\x70\x01\x73\x01\x75\x01\x72\x01\x73\x01"+
        "\x61\x01\x72\x01\x63\x01\x74\x01\x65\x01\x7a\x01\x74\x01\x65\x01"+
        "\x63\x01\x61\x02\x69\x01\x65\x01\x70\x01\x61\x01\x62\x01\x61\x01"+
        "\x6d\x01\x69\x01\x7a\x02\x6c\x01\x73\x01\x68\x01\x65\x01\x7a\x01"+
        "\x61\x01\x6c\x01\x6d\x01\x65\x01\x73\x01\x6c\x01\x6d\x01\x62\x01"+
        "\x75\x01\x68\x01\x69\x01\uffff\x01\x75\x01\x65\x01\uffff\x01\x7a"+
        "\x01\x6f\x01\x65\x02\x74\x01\x65\x01\uffff\x01\x6f\x01\x6e\x01\x7a"+
        "\x01\x67\x01\x6b\x02\x65\x01\x69\x03\x74\x01\x5f\x01\x69\x01\x6f"+
        "\x01\x7a\x01\x63\x01\x5f\x01\uffff\x01\x7a\x01\x6c\x01\x65\x01\x75"+
        "\x01\x69\x01\uffff\x01\x65\x02\uffff\x01\x73\x01\x65\x01\uffff\x01"+
        "\x67\x01\x6d\x01\x6e\x01\x79\x01\x74\x01\x6b\x01\x73\x01\x6e\x01"+
        "\x72\x01\x6c\x01\x7a\x01\x68\x01\x6c\x01\x6e\x01\x65\x01\x75\x01"+
        "\x65\x01\x74\x01\x6c\x01\x61\x01\x65\x01\x73\x01\x75\x01\x65\x01"+
        "\x7a\x01\x61\x01\x72\x01\x7a\x01\x74\x01\x65\x01\x72\x01\x70\x01"+
        "\x6c\x01\x65\x01\x6c\x01\x65\x01\x79\x01\uffff\x01\x70\x01\x74\x01"+
        "\x6e\x01\x67\x01\x6e\x01\x7a\x01\x6e\x01\x61\x01\x5f\x01\x69\x01"+
        "\x71\x01\x74\x01\x77\x01\x63\x02\x75\x01\x68\x01\x72\x01\x7a\x01"+
        "\x6f\x02\x72\x01\uffff\x02\x72\x01\x69\x01\uffff\x01\x63\x01\x6f"+
        "\x01\uffff\x01\x79\x01\uffff\x01\x72\x01\x74\x01\x72\x01\x65\x01"+
        "\x6e\x01\x7a\x01\x72\x01\uffff\x01\x6e\x01\x72\x01\x69\x01\x74\x01"+
        "\x6c\x01\x65\x01\x6d\x01\x61\x01\x65\x01\x73\x01\x74\x01\x7a\x01"+
        "\x6b\x01\x61\x02\x7a\x01\x63\x01\uffff\x01\x7a\x01\x69\x01\x74\x01"+
        "\x79\x01\x7a\x01\x63\x01\x72\x01\x75\x01\x6e\x01\x6d\x03\x7a\x01"+
        "\x6d\x01\x6c\x01\x70\x01\x7a\x01\x6e\x01\uffff\x01\x61\x01\x75\x01"+
        "\x70\x01\x74\x02\x7a\x01\uffff\x01\x69\x01\x65\x01\x74\x02\x7a\x01"+
        "\x64\x01\x74\x01\x7a\x01\x65\x01\x7a\x01\x70\x01\x5f\x01\x6e\x01"+
        "\x6c\x01\x64\x01\uffff\x01\x72\x01\x7a\x01\x6b\x02\x72\x01\x65\x01"+
        "\x69\x01\x78\x01\x72\x01\x7a\x01\uffff\x01\x62\x01\x74\x01\x6c\x02"+
        "\x7a\x01\x73\x01\x74\x02\x7a\x01\x69\x01\x72\x01\x75\x01\x72\x01"+
        "\uffff\x01\x7a\x01\x68\x01\x72\x01\uffff\x01\x75\x01\x7a\x02\x72"+
        "\x01\x6f\x01\x72\x01\x65\x01\x72\x01\x6d\x01\x61\x01\x74\x01\x67"+
        "\x01\x65\x01\x69\x01\x5f\x01\x77\x01\x7a\x01\x79\x01\x75\x01\uffff"+
        "\x01\x66\x01\x74\x01\x7a\x01\x64\x01\x78\x01\x6e\x01\x61\x01\x72"+
        "\x01\x61\x01\x6d\x01\x72\x01\x65\x01\x72\x01\x63\x01\uffff\x01\x69"+
        "\x01\x61\x01\uffff\x01\x73\x01\x69\x01\x70\x01\x65\x02\x6c\x01\x7a"+
        "\x01\x65\x01\x7a\x01\x74\x03\x7a\x01\x67\x01\x73\x01\uffff\x01\x67"+
        "\x02\x74\x01\x67\x01\x6e\x02\x75\x01\x7a\x01\x68\x01\x69\x01\x65"+
        "\x01\x75\x02\x7a\x01\x65\x01\uffff\x01\x66\x01\x7a\x01\x69\x02\x7a"+
        "\x01\x6e\x01\x69\x01\x68\x01\x6e\x01\x7a\x01\x6f\x01\x7a\x02\x65"+
        "\x01\x7a\x01\uffff\x01\x79\x01\x74\x01\x63\x01\x65\x01\x6e\x01\x74"+
        "\x01\x72\x01\x69\x01\x63\x01\x6e\x01\x74\x01\x6e\x01\x7a\x01\x65"+
        "\x01\uffff\x01\x65\x01\x63\x01\x7a\x01\x64\x02\uffff\x01\x74\x01"+
        "\uffff\x01\x6e\x01\x65\x01\x5f\x01\uffff\x01\x68\x01\x72\x01\x6c"+
        "\x01\x65\x01\x61\x01\uffff\x01\x69\x01\uffff\x01\x61\x01\uffff\x01"+
        "\x69\x02\x65\x01\uffff\x01\x65\x01\x69\x01\x73\x01\x74\x01\x73\x02"+
        "\uffff\x01\x67\x02\x7a\x01\uffff\x01\x65\x01\uffff\x02\x7a\x01\uffff"+
        "\x01\x74\x01\uffff\x01\x7a\x01\x70\x01\x67\x01\x6c\x01\x69\x01\x76"+
        "\x01\x65\x01\uffff\x01\x65\x01\x7a\x01\x74\x02\x61\x01\x7a\x01\x65"+
        "\x01\uffff\x01\x6c\x01\x65\x01\x6c\x01\x7a\x02\uffff\x01\x74\x01"+
        "\x7a\x02\uffff\x01\x70\x01\x6f\x01\x6d\x01\x79\x01\uffff\x01\x7a"+
        "\x01\x6f\x01\x6c\x01\uffff\x01\x69\x01\x61\x01\x6e\x01\x7a\x01\x74"+
        "\x01\x7a\x01\x61\x01\x72\x01\x7a\x01\x6f\x01\x72\x01\x61\x01\x6b"+
        "\x01\x6f\x01\uffff\x01\x7a\x01\x61\x01\x70\x01\x6f\x01\x7a\x01\uffff"+
        "\x01\x65\x01\x70\x01\x64\x01\x73\x01\x69\x01\x63\x02\x65\x01\x7a"+
        "\x01\x69\x01\x74\x02\x67\x01\x75\x01\x6d\x01\x61\x01\x6f\x01\x61"+
        "\x01\x6f\x01\x7a\x01\x65\x01\x69\x01\uffff\x01\x7a\x01\uffff\x01"+
        "\x74\x01\x6c\x01\x6e\x01\x65\x01\uffff\x01\x72\x02\uffff\x01\x65"+
        "\x01\x61\x01\x7a\x01\x65\x01\x69\x01\x6e\x01\x7a\x01\x65\x01\x61"+
        "\x01\uffff\x01\x61\x01\x6e\x01\x73\x01\x6d\x02\uffff\x01\x7a\x01"+
        "\x69\x01\uffff\x01\x74\x02\uffff\x01\x63\x01\x6e\x02\x7a\x01\x65"+
        "\x01\x77\x01\uffff\x01\x7a\x01\x6e\x01\uffff\x02\x7a\x01\x74\x01"+
        "\x73\x01\x74\x01\x7a\x01\x61\x01\x63\x01\x74\x01\x7a\x01\x65\x01"+
        "\x74\x01\uffff\x01\x7a\x02\x74\x01\uffff\x01\x75\x01\x65\x01\x6f"+
        "\x01\x63\x01\x7a\x01\x6b\x01\x7a\x01\x65\x01\x74\x01\x72\x01\x6c"+
        "\x01\x6d\x01\x73\x01\x63\x03\x7a\x01\x6e\x01\x69\x02\x7a\x01\x6e"+
        "\x02\uffff\x01\x78\x02\uffff\x01\x72\x01\uffff\x01\x72\x02\x7a\x02"+
        "\x61\x01\x65\x02\x72\x01\uffff\x01\x7a\x01\x64\x01\x6c\x01\uffff"+
        "\x01\x64\x01\x7a\x01\x6f\x01\x78\x01\x6f\x01\uffff\x01\x72\x01\uffff"+
        "\x01\x6f\x01\x69\x01\x77\x01\x74\x01\x7a\x01\uffff\x01\x77\x01\x6c"+
        "\x01\x63\x01\x6c\x01\x7a\x01\uffff\x01\x7a\x01\uffff\x01\x7a\x01"+
        "\x79\x01\uffff\x01\x6e\x01\x7a\x01\x6c\x01\x65\x01\x72\x01\uffff"+
        "\x01\x63\x01\x7a\x01\x72\x01\uffff\x01\x78\x01\x7a\x01\x61\x01\x65"+
        "\x01\x63\x01\x65\x01\x7a\x01\x6e\x01\uffff\x01\x74\x01\x7a\x01\x68"+
        "\x01\x65\x01\x67\x01\x66\x01\x61\x01\x6c\x01\x5f\x01\x6c\x01\x69"+
        "\x01\uffff\x01\x7a\x01\x6e\x01\uffff\x01\x70\x01\x61\x01\x6f\x01"+
        "\x74\x01\x78\x01\x61\x01\x72\x01\x63\x01\uffff\x01\x7a\x01\x6d\x01"+
        "\x65\x01\uffff\x01\x7a\x01\x6c\x01\x72\x01\x61\x02\x7a\x01\uffff"+
        "\x01\x6c\x01\x68\x01\x72\x01\x63\x02\uffff\x01\x7a\x01\x5f\x01\uffff"+
        "\x01\x7a\x02\uffff\x01\x7a\x01\x73\x01\x7a\x01\uffff\x01\x69\x01"+
        "\x74\x01\x69\x01\uffff\x01\x66\x01\x7a\x01\x5f\x01\uffff\x01\x7a"+
        "\x01\x65\x01\x6d\x01\x7a\x01\x72\x01\x74\x01\uffff\x01\x65\x01\uffff"+
        "\x01\x62\x01\x64\x03\x7a\x02\x65\x01\x7a\x03\uffff\x01\x7a\x01\x76"+
        "\x02\uffff\x01\x7a\x01\x74\x01\x79\x01\x69\x02\uffff\x01\x74\x01"+
        "\x6c\x01\x63\x02\x7a\x01\uffff\x01\x6d\x01\x7a\x01\x6c\x01\x7a\x01"+
        "\uffff\x01\x63\x01\x74\x01\x62\x01\x69\x01\x6c\x01\x6e\x01\x73\x01"+
        "\x6e\x01\x6c\x01\x65\x01\uffff\x01\x73\x03\x7a\x03\uffff\x02\x7a"+
        "\x01\uffff\x01\x7a\x01\x79\x01\x64\x01\x6b\x01\uffff\x01\x6d\x01"+
        "\x7a\x01\uffff\x01\x6e\x01\x7a\x01\x74\x01\x7a\x01\uffff\x01\x63"+
        "\x01\x79\x01\uffff\x01\x74\x01\x7a\x01\x5f\x01\x66\x01\x6c\x01\x63"+
        "\x01\x68\x01\x63\x01\x7a\x01\x6e\x01\uffff\x01\x74\x01\x61\x01\x6d"+
        "\x01\x62\x01\x7a\x01\x74\x01\x72\x01\x7a\x01\x74\x01\uffff\x01\x65"+
        "\x01\x64\x01\uffff\x02\x7a\x01\x72\x02\uffff\x01\x6c\x01\x6d\x01"+
        "\x65\x01\x72\x01\uffff\x01\x6c\x02\uffff\x01\x65\x01\uffff\x01\x6e"+
        "\x01\x7a\x02\x6f\x01\uffff\x01\x75\x01\uffff\x01\x72\x01\x7a\x01"+
        "\uffff\x01\x7a\x01\x79\x01\x7a\x01\x79\x01\x6c\x01\x7a\x03\uffff"+
        "\x02\x7a\x02\uffff\x01\x65\x01\uffff\x02\x7a\x01\x6f\x01\x65\x01"+
        "\x7a\x01\x74\x02\uffff\x01\x65\x01\uffff\x01\x79\x01\uffff\x01\x6b"+
        "\x02\x7a\x02\x6e\x01\x79\x01\x65\x01\x7a\x01\x74\x01\x6f\x01\x78"+
        "\x01\x7a\x06\uffff\x01\x73\x02\x7a\x01\x61\x01\uffff\x01\x74\x01"+
        "\uffff\x01\x7a\x01\uffff\x01\x65\x01\x7a\x01\x5f\x01\uffff\x01\x72"+
        "\x01\x65\x01\x6c\x01\x5f\x01\x65\x01\x61\x01\uffff\x01\x74\x01\x7a"+
        "\x01\x63\x01\x70\x01\x7a\x01\uffff\x01\x7a\x01\x79\x01\uffff\x01"+
        "\x69\x01\x73\x01\x7a\x02\uffff\x01\x79\x02\x7a\x01\x6d\x02\x65\x01"+
        "\x64\x01\x74\x01\uffff\x01\x6e\x01\x72\x01\x61\x01\x69\x01\x73\x01"+
        "\x7a\x02\uffff\x01\x7a\x01\uffff\x01\x6f\x01\x5f\x01\x65\x03\uffff"+
        "\x01\x7a\x02\uffff\x01\x6f\x01\x72\x01\x7a\x01\uffff\x01\x7a\x01"+
        "\x74\x01\x7a\x01\x5f\x02\uffff\x01\x67\x01\x74\x01\x67\x01\x73\x01"+
        "\uffff\x01\x7a\x01\x62\x01\x74\x01\uffff\x01\x7a\x02\uffff\x01\x74"+
        "\x01\x7a\x01\uffff\x01\x73\x01\uffff\x01\x6a\x01\x65\x01\x72\x01"+
        "\x5f\x01\x66\x01\x7a\x01\x63\x01\x7a\x01\uffff\x01\x65\x01\x7a\x02"+
        "\uffff\x01\x7a\x01\x6f\x01\x74\x01\uffff\x01\x7a\x02\uffff\x01\x65"+
        "\x01\x6d\x01\x6e\x03\x7a\x01\x6d\x01\x74\x01\x6d\x01\x65\x02\uffff"+
        "\x02\x77\x01\x7a\x01\uffff\x01\x6c\x01\x69\x02\uffff\x01\x68\x01"+
        "\uffff\x01\x73\x02\x7a\x01\x6f\x01\x74\x01\uffff\x02\x7a\x01\uffff"+
        "\x01\x7a\x01\uffff\x01\x7a\x01\x6f\x01\x73\x01\x5f\x01\x72\x01\x6f"+
        "\x01\uffff\x01\x68\x01\uffff\x01\x7a\x02\uffff\x01\x6e\x01\x61\x01"+
        "\uffff\x01\x6e\x01\x65\x01\x67\x03\uffff\x01\x61\x02\x65\x01\x72"+
        "\x01\x7a\x01\x72\x01\uffff\x01\x6c\x01\x74\x01\x6f\x01\x69\x02\uffff"+
        "\x01\x6e\x01\x72\x04\uffff\x01\x69\x01\x75\x01\x72\x01\x65\x01\x75"+
        "\x01\x65\x01\uffff\x01\x7a\x01\x6d\x01\x74\x01\x6e\x02\x74\x03\x7a"+
        "\x01\uffff\x01\x69\x01\x65\x01\x79\x01\x64\x02\x7a\x01\x69\x01\x6e"+
        "\x01\x6c\x01\x65\x01\x73\x01\x6e\x01\x7a\x01\uffff\x01\x70\x01\x7a"+
        "\x01\x74\x01\x68\x01\x7a\x02\uffff\x01\x74\x01\uffff\x01\x74\x01"+
        "\x63\x02\x7a\x01\x65\x01\uffff\x01\x6e\x01\x7a\x01\x74\x01\x73\x01"+
        "\x75\x01\x64\x01\uffff\x01\x7a\x01\uffff\x02\x7a\x01\uffff\x01\x61"+
        "\x01\x65\x01\x74\x02\uffff\x01\x7a\x01\x67\x01\uffff\x01\x7a\x01"+
        "\x75\x01\x6c\x01\x5f\x03\uffff\x01\x6d\x01\x7a\x01\x69\x01\uffff"+
        "\x01\x7a\x01\uffff\x01\x6c\x01\x74\x01\x72\x01\x70\x01\uffff\x01"+
        "\x6f\x01\uffff\x01\x74\x01\x7a\x01\x6f\x01\x7a\x01\x6e\x01\x7a\x01"+
        "\uffff\x01\x77\x01\uffff\x01\x7a\x01\uffff\x01\x73\x01\uffff\x01"+
        "\x7a\x01\uffff";
    const string DFA24_acceptS =
        "\x07\uffff\x01\x10\x01\uffff\x01\x12\x01\x13\x01\uffff\x01\x15"+
        "\x01\x16\x01\uffff\x01\x18\x01\x19\x01\x1a\x01\x1b\x01\x1d\x01\x1e"+
        "\x01\x1f\x01\x20\x1a\uffff\x01\u0105\x01\u0106\x01\u0108\x03\uffff"+
        "\x01\u010d\x01\x03\x01\x01\x01\uffff\x01\x05\x01\x0a\x01\x06\x01"+
        "\x04\x01\x1c\x01\x09\x01\x0b\x01\x08\x01\x0d\x01\x0c\x01\x0f\x01"+
        "\x0e\x01\x11\x01\x14\x01\x17\x01\u010a\x0a\uffff\x01\u010c\x30\uffff"+
        "\x01\u0107\x28\uffff\x01\u010b\x03\uffff\x01\u0109\x01\x02\x01\x07"+
        "\x05\uffff\x01\x28\x08\uffff\x01\x2f\x31\uffff\x01\x5f\x02\uffff"+
        "\x01\x56\x06\uffff\x01\x53\x11\uffff\x01\u00a5\x05\uffff\x01\x6e"+
        "\x01\uffff\x01\x6d\x01\x6b\x02\uffff\x01\u00fc\x25\uffff\x01\u0086"+
        "\x16\uffff\x01\x24\x03\uffff\x01\x29\x02\uffff\x01\x22\x01\uffff"+
        "\x01\x27\x07\uffff\x01\u00ba\x11\uffff\x01\u0093\x12\uffff\x01\x46"+
        "\x06\uffff\x01\x4d\x0f\uffff\x01\u00be\x0a\uffff\x01\x62\x0d\uffff"+
        "\x01\u0094\x03\uffff\x01\x68\x13\uffff\x01\x7e\x0e\uffff\x01\u0081"+
        "\x02\uffff\x01\u00e9\x0f\uffff\x01\u00fd\x0f\uffff\x01\u009a\x0f"+
        "\uffff\x01\u00cf\x0e\uffff\x01\u00ca\x04\uffff\x01\x31\x01\x32\x01"+
        "\uffff\x01\u00a0\x03\uffff\x01\x40\x05\uffff\x01\u00c6\x01\uffff"+
        "\x01\u00aa\x01\uffff\x01\x43\x03\uffff\x01\u00d6\x05\uffff\x01\x44"+
        "\x01\x45\x03\uffff\x01\u00a2\x01\uffff\x01\x4f\x02\uffff\x01\x4c"+
        "\x01\uffff\x01\x50\x07\uffff\x01\x5e\x07\uffff\x01\x61\x04\uffff"+
        "\x01\u00f9\x01\x64\x02\uffff\x01\u00af\x01\x63\x04\uffff\x01\u00fa"+
        "\x03\uffff\x01\x6a\x0e\uffff\x01\x71\x05\uffff\x01\u00c1\x16\uffff"+
        "\x01\u00e4\x01\uffff\x01\u00c7\x04\uffff\x01\u0084\x01\uffff\x01"+
        "\u00d3\x01\u0085\x09\uffff\x01\u008f\x04\uffff\x01\u0098\x01\u0091"+
        "\x02\uffff\x01\u0104\x01\uffff\x01\x25\x01\x23\x06\uffff\x01\x21"+
        "\x02\uffff\x01\x2d\x0c\uffff\x01\x3a\x03\uffff\x01\x33\x16\uffff"+
        "\x01\u00fb\x01\u00c3\x01\uffff\x01\u009d\x01\u00ae\x01\uffff\x01"+
        "\x51\x08\uffff\x01\x5a\x03\uffff\x01\x57\x05\uffff\x01\u00ec\x01"+
        "\uffff\x01\x65\x05\uffff\x01\x66\x05\uffff\x01\x6f\x01\uffff\x01"+
        "\x70\x02\uffff\x01\u00dd\x05\uffff\x01\x74\x03\uffff\x01\u00e5\x08"+
        "\uffff\x01\x75\x0b\uffff\x01\u00f8\x02\uffff\x01\u0082\x08\uffff"+
        "\x01\u008c\x03\uffff\x01\u0089\x06\uffff\x01\u0092\x04\uffff\x01"+
        "\x2a\x01\u00a6\x02\uffff\x01\x2c\x01\uffff\x01\u00cc\x01\u00c0\x03"+
        "\uffff\x01\x36\x03\uffff\x01\x35\x03\uffff\x01\x39\x06\uffff\x01"+
        "\x3f\x01\uffff\x01\x41\x08\uffff\x01\u00c2\x01\x47\x01\u00a7\x02"+
        "\uffff\x01\x48\x01\x4a\x04\uffff\x01\x52\x01\x60\x05\uffff\x01\x5b"+
        "\x04\uffff\x01\x54\x0a\uffff\x01\u00a1\x04\uffff\x01\u00ed\x01\x6c"+
        "\x01\x72\x02\uffff\x01\u0099\x04\uffff\x01\u00f7\x02\uffff\x01\x77"+
        "\x04\uffff\x01\x7a\x02\uffff\x01\u0080\x0a\uffff\x01\u00a4\x09\uffff"+
        "\x01\u008b\x02\uffff\x01\u008a\x03\uffff\x01\u008e\x01\u008d\x04"+
        "\uffff\x01\x26\x01\uffff\x01\x2e\x01\u00b7\x01\uffff\x01\u00db\x04"+
        "\uffff\x01\x34\x01\uffff\x01\u00b9\x02\uffff\x01\x30\x06\uffff\x01"+
        "\x3c\x01\u00e7\x01\u00c4\x02\uffff\x01\u009e\x01\x4b\x01\uffff\x01"+
        "\x4e\x06\uffff\x01\u00bf\x01\u00e8\x01\uffff\x01\x5c\x01\uffff\x01"+
        "\x58\x0c\uffff\x01\x69\x01\u00c5\x01\x67\x01\x73\x01\u00df\x01\u00a3"+
        "\x04\uffff\x01\x78\x01\uffff\x01\x79\x01\uffff\x01\x7b\x03\uffff"+
        "\x01\u009f\x06\uffff\x01\u0096\x05\uffff\x01\u00bb\x02\uffff\x01"+
        "\u0088\x03\uffff\x01\u0090\x01\u00cb\x08\uffff\x01\x37\x06\uffff"+
        "\x01\u00a9\x01\u00eb\x01\uffff\x01\x42\x03\uffff\x01\x3e\x01\u00c9"+
        "\x01\x3b\x01\uffff\x01\u0095\x01\u00dc\x03\uffff\x01\u00fe\x04\uffff"+
        "\x01\u00d5\x01\u00d1\x04\uffff\x01\u00b1\x03\uffff\x01\u00b0\x01"+
        "\uffff\x01\u00b3\x01\x7d\x02\uffff\x01\x7c\x01\uffff\x01\u00ea\x08"+
        "\uffff\x01\u00bc\x02\uffff\x01\u00ce\x01\u00d2\x03\uffff\x01\u00d8"+
        "\x01\uffff\x01\u00d9\x01\u00e6\x0a\uffff\x01\u00d7\x01\u00ab\x03"+
        "\uffff\x01\x49\x02\uffff\x01\x55\x01\x5d\x01\uffff\x01\x59\x05\uffff"+
        "\x01\u00bd\x02\uffff\x01\u00b2\x01\uffff\x01\u00b6\x06\uffff\x01"+
        "\u00f4\x01\uffff\x01\x7f\x01\uffff\x01\u00c8\x01\u0083\x02\uffff"+
        "\x01\u00cd\x03\uffff\x01\u00b5\x01\x38\x01\u00da\x06\uffff\x01\x3d"+
        "\x04\uffff\x01\u00de\x01\u00e0\x02\uffff\x01\u00d0\x01\u00d4\x01"+
        "\u00b4\x01\x76\x06\uffff\x01\u00b8\x09\uffff\x01\u00ee\x0d\uffff"+
        "\x01\u0087\x05\uffff\x01\u0100\x01\u00ff\x01\uffff\x01\u0103\x05"+
        "\uffff\x01\u00e2\x06\uffff\x01\u00f5\x01\uffff\x01\x2b\x02\uffff"+
        "\x01\u009c\x03\uffff\x01\u00ef\x01\u00ad\x02\uffff\x01\u00f0\x04"+
        "\uffff\x01\u0102\x01\u009b\x01\u00a8\x03\uffff\x01\u0097\x01\uffff"+
        "\x01\u00f2\x04\uffff\x01\u00ac\x01\uffff\x01\u00e1\x06\uffff\x01"+
        "\u00f1\x01\uffff\x01\u0101\x01\uffff\x01\u00f3\x01\uffff\x01\u00e3"+
        "\x01\uffff\x01\u00f6";
    const string DFA24_specialS =
        "\u0539\uffff}>";
    static readonly string[] DFA24_transitionS = {
            "\x02\x37\x01\uffff\x02\x37\x12\uffff\x01\x37\x01\x03\x02\uffff"+
            "\x01\x15\x01\x0c\x01\x05\x01\x33\x01\x10\x01\x11\x01\x0a\x01"+
            "\x07\x01\x0f\x01\x08\x01\x0e\x01\x0b\x01\x34\x09\x35\x01\x13"+
            "\x01\x0d\x01\x02\x01\x01\x01\x04\x01\x12\x01\x14\x01\x17\x01"+
            "\x36\x01\x19\x01\x1a\x01\x1b\x01\x1c\x01\x1d\x01\x1e\x01\x1f"+
            "\x01\x20\x01\x21\x01\x22\x01\x23\x01\x24\x01\x25\x01\x26\x01"+
            "\x27\x01\x28\x01\x29\x01\x2a\x01\x2b\x01\x2c\x01\x2d\x01\x2e"+
            "\x01\x30\x01\x2f\x03\uffff\x01\x16\x01\x31\x01\x32\x01\x17\x01"+
            "\x18\x01\x19\x01\x1a\x01\x1b\x01\x1c\x01\x1d\x01\x1e\x01\x1f"+
            "\x01\x20\x01\x21\x01\x22\x01\x23\x01\x24\x01\x25\x01\x26\x01"+
            "\x27\x01\x28\x01\x29\x01\x2a\x01\x2b\x01\x2c\x01\x2d\x01\x2e"+
            "\x01\x30\x01\x2f\x01\uffff\x01\x06\x01\uffff\x01\x09",
            "\x01\x38",
            "\x01\x3c\x01\x3a\x01\x3b",
            "\x01\x3e",
            "\x01\x40\x01\x41",
            "\x01\x43",
            "\x01\x45",
            "",
            "\x01\x37",
            "",
            "",
            "\x01\x37",
            "",
            "",
            "\x0a\x4a",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "\x01\x54\x01\x50\x01\x51\x01\uffff\x01\x4c\x05\uffff\x01\x4b"+
            "\x01\uffff\x01\x52\x04\uffff\x01\x4e\x01\x4f\x01\x4d\x01\x53"+
            "\x0b\uffff\x01\x54\x01\x50\x01\x51\x01\uffff\x01\x4c\x05\uffff"+
            "\x01\x4b\x01\uffff\x01\x52\x04\uffff\x01\x4e\x01\x4f\x01\x4d"+
            "\x01\x53",
            "\x01\x55\x1d\uffff\x01\x57\x03\uffff\x01\x59\x02\uffff\x01"+
            "\x58\x0c\uffff\x01\x56\x0b\uffff\x01\x57\x03\uffff\x01\x59\x02"+
            "\uffff\x01\x58\x0c\uffff\x01\x56",
            "\x01\x5e\x06\uffff\x01\x5d\x06\uffff\x01\x5a\x02\uffff\x01"+
            "\x5c\x02\uffff\x01\x5b\x0b\uffff\x01\x5e\x06\uffff\x01\x5d\x06"+
            "\uffff\x01\x5a\x02\uffff\x01\x5c\x02\uffff\x01\x5b",
            "\x01\x61\x03\uffff\x01\x60\x03\uffff\x01\x5f\x05\uffff\x01"+
            "\x64\x02\uffff\x01\x62\x06\uffff\x01\x63\x07\uffff\x01\x61\x03"+
            "\uffff\x01\x60\x03\uffff\x01\x5f\x05\uffff\x01\x64\x02\uffff"+
            "\x01\x62\x06\uffff\x01\x63",
            "\x01\x68\x0a\uffff\x01\x69\x01\uffff\x01\x66\x04\uffff\x01"+
            "\x65\x04\uffff\x01\x67\x08\uffff\x01\x68\x0a\uffff\x01\x69\x01"+
            "\uffff\x01\x66\x04\uffff\x01\x65\x04\uffff\x01\x67",
            "\x01\x6f\x07\uffff\x01\x6e\x02\uffff\x01\x6b\x02\uffff\x01"+
            "\x6a\x02\uffff\x01\x6d\x02\uffff\x01\x6c\x0b\uffff\x01\x6f\x07"+
            "\uffff\x01\x6e\x02\uffff\x01\x6b\x02\uffff\x01\x6a\x02\uffff"+
            "\x01\x6d\x02\uffff\x01\x6c",
            "\x01\x70\x06\uffff\x01\x71\x05\uffff\x01\x72\x12\uffff\x01"+
            "\x70\x06\uffff\x01\x71\x05\uffff\x01\x72",
            "\x01\x74\x07\uffff\x01\x73\x17\uffff\x01\x74\x07\uffff\x01"+
            "\x73",
            "\x01\x78\x01\x79\x05\uffff\x01\x76\x01\x77\x04\uffff\x01\x75"+
            "\x12\uffff\x01\x78\x01\x79\x05\uffff\x01\x76\x01\x77\x04\uffff"+
            "\x01\x75",
            "\x01\x7a\x1f\uffff\x01\x7a",
            "\x01\x7b\x1f\uffff\x01\x7b",
            "\x01\x7e\x03\uffff\x01\x7f\x03\uffff\x01\x7d\x05\uffff\x01"+
            "\x7c\x11\uffff\x01\x7e\x03\uffff\x01\x7f\x03\uffff\x01\x7d\x05"+
            "\uffff\x01\x7c",
            "\x01\u0084\x03\uffff\x01\u0082\x03\uffff\x01\u0081\x05\uffff"+
            "\x01\u0083\x05\uffff\x01\u0080\x0b\uffff\x01\u0084\x03\uffff"+
            "\x01\u0082\x03\uffff\x01\u0081\x05\uffff\x01\u0083\x05\uffff"+
            "\x01\u0080",
            "\x01\u0086\x19\uffff\x01\u0088\x0d\uffff\x01\u0085\x05\uffff"+
            "\x01\u0087\x0b\uffff\x01\u0088\x0d\uffff\x01\u0085\x05\uffff"+
            "\x01\u0087",
            "\x01\u008c\x03\uffff\x01\u008e\x03\uffff\x01\u008b\x01\uffff"+
            "\x01\u0089\x01\uffff\x01\u008a\x02\uffff\x01\u008d\x10\uffff"+
            "\x01\u008c\x03\uffff\x01\u008e\x03\uffff\x01\u008b\x01\uffff"+
            "\x01\u0089\x01\uffff\x01\u008a\x02\uffff\x01\u008d",
            "\x01\u0091\x0a\uffff\x01\u0092\x02\uffff\x01\u0090\x02\uffff"+
            "\x01\u008f\x0e\uffff\x01\u0091\x0a\uffff\x01\u0092\x02\uffff"+
            "\x01\u0090\x02\uffff\x01\u008f",
            "\x01\u0093\x1f\uffff\x01\u0093",
            "\x01\u0097\x03\uffff\x01\u0096\x03\uffff\x01\u0095\x05\uffff"+
            "\x01\u0094\x11\uffff\x01\u0097\x03\uffff\x01\u0096\x03\uffff"+
            "\x01\u0095\x05\uffff\x01\u0094",
            "\x01\u009c\x03\uffff\x01\u0098\x02\uffff\x01\u009d\x01\u009e"+
            "\x03\uffff\x01\u009f\x02\uffff\x01\u009b\x01\u009a\x02\uffff"+
            "\x01\u0099\x0c\uffff\x01\u009c\x03\uffff\x01\u0098\x02\uffff"+
            "\x01\u009d\x01\u009e\x03\uffff\x01\u009f\x02\uffff\x01\u009b"+
            "\x01\u009a\x02\uffff\x01\u0099",
            "\x01\u00a1\x03\uffff\x01\u00a4\x02\uffff\x01\u00a5\x01\u00a2"+
            "\x05\uffff\x01\u00a3\x02\uffff\x01\u00a6\x06\uffff\x01\u00a0"+
            "\x07\uffff\x01\u00a1\x03\uffff\x01\u00a4\x02\uffff\x01\u00a5"+
            "\x01\u00a2\x05\uffff\x01\u00a3\x02\uffff\x01\u00a6\x06\uffff"+
            "\x01\u00a0",
            "\x01\u00aa\x01\uffff\x01\u00a8\x02\uffff\x01\u00a7\x01\u00a9"+
            "\x19\uffff\x01\u00aa\x01\uffff\x01\u00a8\x02\uffff\x01\u00a7"+
            "\x01\u00a9",
            "\x01\u00ac\x07\uffff\x01\u00ab\x17\uffff\x01\u00ac\x07\uffff"+
            "\x01\u00ab",
            "\x01\u00ae\x01\u00ad\x1e\uffff\x01\u00ae\x01\u00ad",
            "\x01\u00af\x27\uffff\x01\u00b0\x1f\uffff\x01\u00b0",
            "\x01\u00b1\x1f\uffff\x01\u00b1",
            "\x01\u00b2\x1f\uffff\x01\u00b2",
            "",
            "",
            "",
            "\x01\x4a\x01\uffff\x0a\x35\x0b\uffff\x01\x4a\x1c\uffff\x01"+
            "\x55\x02\uffff\x01\x4a",
            "\x01\x4a\x01\uffff\x0a\x35\x0b\uffff\x01\x4a\x1f\uffff\x01"+
            "\x4a",
            "\x01\x57\x03\uffff\x01\x59\x02\uffff\x01\x58\x0c\uffff\x01"+
            "\x56\x0b\uffff\x01\x57\x03\uffff\x01\x59\x02\uffff\x01\x58\x0c"+
            "\uffff\x01\x56",
            "",
            "",
            "",
            "\x01\u00b4",
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
            "\x01\u00b6\x04\uffff\x01\u00b7\x07\uffff\x01\u00b8\x12\uffff"+
            "\x01\u00b6\x04\uffff\x01\u00b7\x07\uffff\x01\u00b8",
            "\x01\u00b9\x1f\uffff\x01\u00b9",
            "\x01\u00ba\x1f\uffff\x01\u00ba",
            "\x0a\x31\x07\uffff\x02\x31\x01\u00bc\x17\x31\x04\uffff\x01"+
            "\x31\x01\uffff\x02\x31\x01\u00bc\x17\x31",
            "\x01\u00bd\x1f\uffff\x01\u00bd",
            "\x01\u00be\x1f\uffff\x01\u00be",
            "\x01\u00bf\x1f\uffff\x01\u00bf",
            "\x01\u00c0\x02\uffff\x01\u00c1\x1c\uffff\x01\u00c0\x02\uffff"+
            "\x01\u00c1",
            "\x01\u00c2\x1f\uffff\x01\u00c2",
            "\x01\u00c3\x1f\uffff\x01\u00c3",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u00c5\x01\u00c7\x0c\uffff\x01\u00c6\x11\uffff\x01\u00c5"+
            "\x01\u00c7\x0c\uffff\x01\u00c6",
            "\x01\u00c8\x1f\uffff\x01\u00c8",
            "\x01\u00cb\x06\uffff\x01\u00c9\x05\uffff\x01\u00ca\x12\uffff"+
            "\x01\u00cb\x06\uffff\x01\u00c9\x05\uffff\x01\u00ca",
            "\x01\u00ce\x01\u00cc\x01\u00cd\x1d\uffff\x01\u00ce\x01\u00cc"+
            "\x01\u00cd",
            "\x01\u00cf\x1f\uffff\x01\u00cf",
            "\x01\u00d1\x09\uffff\x01\u00d0\x15\uffff\x01\u00d1\x09\uffff"+
            "\x01\u00d0",
            "\x01\u00d2\x03\uffff\x01\u00d3\x1b\uffff\x01\u00d2\x03\uffff"+
            "\x01\u00d3",
            "\x01\u00d4\x1f\uffff\x01\u00d4",
            "\x01\u00d5\x01\u00d7\x02\uffff\x01\u00d6\x1b\uffff\x01\u00d5"+
            "\x01\u00d7\x02\uffff\x01\u00d6",
            "\x01\u00dc\x02\uffff\x01\u00db\x05\uffff\x01\u00d8\x06\uffff"+
            "\x01\u00d9\x01\u00da\x0e\uffff\x01\u00dc\x02\uffff\x01\u00db"+
            "\x05\uffff\x01\u00d8\x06\uffff\x01\u00d9\x01\u00da",
            "\x01\u00dd\x1f\uffff\x01\u00dd",
            "\x01\u00de\x1f\uffff\x01\u00de",
            "\x01\u00df\x1f\uffff\x01\u00df",
            "\x01\u00e0\x1f\uffff\x01\u00e0",
            "\x01\u00e1\x1f\uffff\x01\u00e1",
            "\x01\u00e4\x02\uffff\x01\u00e3\x0d\uffff\x01\u00e2\x0e\uffff"+
            "\x01\u00e4\x02\uffff\x01\u00e3\x0d\uffff\x01\u00e2",
            "\x01\u00e6\x05\uffff\x01\u00e7\x06\uffff\x01\u00e5\x12\uffff"+
            "\x01\u00e6\x05\uffff\x01\u00e7\x06\uffff\x01\u00e5",
            "\x01\u00e8\x1f\uffff\x01\u00e8",
            "\x01\u00e9\x1f\uffff\x01\u00e9",
            "\x01\u00ea\x1f\uffff\x01\u00ea",
            "\x01\u00eb\x1f\uffff\x01\u00eb",
            "\x01\u00ec\x1f\uffff\x01\u00ec",
            "\x01\u00ed\x1f\uffff\x01\u00ed",
            "\x01\u00ef\x05\uffff\x01\u00ee\x19\uffff\x01\u00ef\x05\uffff"+
            "\x01\u00ee",
            "\x01\u00f0\x1f\uffff\x01\u00f0",
            "\x01\u00f1\x1f\uffff\x01\u00f1",
            "\x01\u00f2\x1f\uffff\x01\u00f2",
            "\x01\u00f3\x1f\uffff\x01\u00f3",
            "\x01\u00f4\x1f\uffff\x01\u00f4",
            "\x01\u00f5\x1f\uffff\x01\u00f5",
            "\x0a\x31\x07\uffff\x0d\x31\x01\u00f7\x0c\x31\x04\uffff\x01"+
            "\x31\x01\uffff\x0d\x31\x01\u00f7\x0c\x31",
            "\x01\u00f8\x1f\uffff\x01\u00f8",
            "\x0a\x31\x07\uffff\x03\x31\x01\u00ff\x04\x31\x01\u00fe\x04"+
            "\x31\x01\u00fc\x04\x31\x01\u00fd\x01\u00fa\x01\x31\x01\u00fb"+
            "\x04\x31\x04\uffff\x01\x31\x01\uffff\x03\x31\x01\u00ff\x04\x31"+
            "\x01\u00fe\x04\x31\x01\u00fc\x04\x31\x01\u00fd\x01\u00fa\x01"+
            "\x31\x01\u00fb\x04\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0101\x1f\uffff\x01\u0101",
            "\x01\u0102\x1f\uffff\x01\u0102",
            "\x01\u0103\x1f\uffff\x01\u0103",
            "\x01\u0105\x0a\uffff\x01\u0104\x14\uffff\x01\u0105\x0a\uffff"+
            "\x01\u0104",
            "\x01\u0106\x01\uffff\x01\u0108\x01\u0107\x1c\uffff\x01\u0106"+
            "\x01\uffff\x01\u0108\x01\u0107",
            "\x01\u0109\x1f\uffff\x01\u0109",
            "\x01\u010a\x1f\uffff\x01\u010a",
            "\x01\u010b\x1f\uffff\x01\u010b",
            "\x01\u010c\x1f\uffff\x01\u010c",
            "\x01\u010d\x08\uffff\x01\u010e\x16\uffff\x01\u010d\x08\uffff"+
            "\x01\u010e",
            "\x01\u010f\x1f\uffff\x01\u010f",
            "\x01\u0110\x03\uffff\x01\u0111\x1b\uffff\x01\u0110\x03\uffff"+
            "\x01\u0111",
            "\x0a\x31\x07\uffff\x13\x31\x01\u0113\x06\x31\x04\uffff\x01"+
            "\x31\x01\uffff\x13\x31\x01\u0113\x06\x31",
            "",
            "\x01\u0114\x01\u0115\x1e\uffff\x01\u0114\x01\u0115",
            "\x01\u0116\x1f\uffff\x01\u0116",
            "\x01\u0117\x1f\uffff\x01\u0117",
            "\x0a\x31\x07\uffff\x03\x31\x01\u0119\x16\x31\x04\uffff\x01"+
            "\x31\x01\uffff\x03\x31\x01\u0119\x16\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x05\x31\x01\u011c\x14\x31\x04\uffff\x01"+
            "\x31\x01\uffff\x05\x31\x01\u011c\x14\x31",
            "\x01\u011d\x1f\uffff\x01\u011d",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u011f\x07\uffff\x01\u0120\x17\uffff\x01\u011f\x07\uffff"+
            "\x01\u0120",
            "\x01\u0121\x02\uffff\x01\u0122\x1c\uffff\x01\u0121\x02\uffff"+
            "\x01\u0122",
            "\x01\u0124\x0e\uffff\x01\u0123\x01\u0125\x0f\uffff\x01\u0124"+
            "\x0e\uffff\x01\u0123\x01\u0125",
            "\x01\u0126\x1f\uffff\x01\u0126",
            "\x01\u0127\x1f\uffff\x01\u0127",
            "\x01\u0128\x0a\uffff\x01\u0129\x14\uffff\x01\u0128\x0a\uffff"+
            "\x01\u0129",
            "\x01\u012a\x1f\uffff\x01\u012a",
            "\x01\u012b\x02\uffff\x01\u012e\x01\uffff\x01\u0133\x01\u012d"+
            "\x01\uffff\x01\u012c\x02\uffff\x01\u012f\x01\uffff\x01\u0132"+
            "\x01\uffff\x01\u0131\x02\uffff\x01\u0130\x0d\uffff\x01\u012b"+
            "\x02\uffff\x01\u012e\x01\uffff\x01\u0133\x01\u012d\x01\uffff"+
            "\x01\u012c\x02\uffff\x01\u012f\x01\uffff\x01\u0132\x01\uffff"+
            "\x01\u0131\x02\uffff\x01\u0130",
            "\x01\u0134\x1f\uffff\x01\u0134",
            "\x01\u0135\x08\uffff\x01\u0136\x07\uffff\x01\u0137\x0e\uffff"+
            "\x01\u0135\x08\uffff\x01\u0136\x07\uffff\x01\u0137",
            "\x01\u0139\x02\uffff\x01\u0138\x1c\uffff\x01\u0139\x02\uffff"+
            "\x01\u0138",
            "\x01\u013a\x1f\uffff\x01\u013a",
            "\x01\u013b\x1f\uffff\x01\u013b",
            "\x01\u013c\x1f\uffff\x01\u013c",
            "\x01\u013d\x1f\uffff\x01\u013d",
            "\x01\u013e\x1f\uffff\x01\u013e",
            "\x01\u013f\x1f\uffff\x01\u013f",
            "\x01\u0140\x1f\uffff\x01\u0140",
            "\x01\u0141\x1f\uffff\x01\u0141",
            "\x01\u0142\x01\u0143\x1e\uffff\x01\u0142\x01\u0143",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0145\x0a\uffff\x01\u0146\x14\uffff\x01\u0145\x0a\uffff"+
            "\x01\u0146",
            "\x01\u0147\x1f\uffff\x01\u0147",
            "\x01\u0149\x07\uffff\x01\u0148\x17\uffff\x01\u0149\x07\uffff"+
            "\x01\u0148",
            "\x01\u014a\x03\uffff\x01\u014b\x1b\uffff\x01\u014a\x03\uffff"+
            "\x01\u014b",
            "\x01\u014c\x1f\uffff\x01\u014c",
            "\x01\u014d\x1f\uffff\x01\u014d",
            "\x01\u014f\x09\uffff\x01\u014e\x15\uffff\x01\u014f\x09\uffff"+
            "\x01\u014e",
            "\x01\u0151\x0c\uffff\x01\u0150\x12\uffff\x01\u0151\x0c\uffff"+
            "\x01\u0150",
            "\x01\u0154\x08\uffff\x01\u0153\x05\uffff\x01\u0152\x10\uffff"+
            "\x01\u0154\x08\uffff\x01\u0153\x05\uffff\x01\u0152",
            "\x01\u0155\x1f\uffff\x01\u0155",
            "\x01\u0156\x1f\uffff\x01\u0156",
            "",
            "\x01\u0157\x1f\uffff\x01\u0157",
            "\x01\u0158\x1f\uffff\x01\u0158",
            "\x01\u0159\x1f\uffff\x01\u0159",
            "",
            "",
            "",
            "\x01\u015a\x1f\uffff\x01\u015a",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u015c\x1f\uffff\x01\u015c",
            "\x01\u015d\x1f\uffff\x01\u015d",
            "\x01\u015e\x1f\uffff\x01\u015e",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0160\x1f\uffff\x01\u0160",
            "\x01\u0161\x1f\uffff\x01\u0161",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0163\x1f\uffff\x01\u0163",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0165",
            "\x01\u0166\x1f\uffff\x01\u0166",
            "",
            "\x01\u0167\x1f\uffff\x01\u0167",
            "\x01\u0168\x1f\uffff\x01\u0168",
            "\x01\u0169\x1f\uffff\x01\u0169",
            "\x01\u016a\x1f\uffff\x01\u016a",
            "\x01\u016b\x1f\uffff\x01\u016b",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u016d\x1f\uffff\x01\u016d",
            "\x01\u016f\x02\uffff\x01\u016e\x1c\uffff\x01\u016f\x02\uffff"+
            "\x01\u016e",
            "\x01\u0171\x07\uffff\x01\u0172\x04\uffff\x01\u0170\x12\uffff"+
            "\x01\u0171\x07\uffff\x01\u0172\x04\uffff\x01\u0170",
            "\x01\u0174\x08\uffff\x01\u0173\x16\uffff\x01\u0174\x08\uffff"+
            "\x01\u0173",
            "\x01\u0175\x1f\uffff\x01\u0175",
            "\x01\u0176\x1f\uffff\x01\u0176",
            "\x01\u0177\x1f\uffff\x01\u0177",
            "\x01\u0178\x1f\uffff\x01\u0178",
            "\x01\u0179\x1f\uffff\x01\u0179",
            "\x01\u017a\x01\uffff\x01\u017b\x0e\uffff\x01\u017c\x0e\uffff"+
            "\x01\u017a\x01\uffff\x01\u017b\x0e\uffff\x01\u017c",
            "\x01\u017d\x1f\uffff\x01\u017d",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u017f\x08\uffff\x01\u0180\x16\uffff\x01\u017f\x08\uffff"+
            "\x01\u0180",
            "\x01\u0182\x03\uffff\x01\u0181\x1b\uffff\x01\u0182\x03\uffff"+
            "\x01\u0181",
            "\x01\u0183\x1f\uffff\x01\u0183",
            "\x01\u0184\x1f\uffff\x01\u0184",
            "\x01\u0186\x03\uffff\x01\u0185\x03\uffff\x01\u0187\x17\uffff"+
            "\x01\u0186\x03\uffff\x01\u0185\x03\uffff\x01\u0187",
            "\x01\u0188\x1f\uffff\x01\u0188",
            "\x01\u018a\x03\uffff\x01\u0189\x1b\uffff\x01\u018a\x03\uffff"+
            "\x01\u0189",
            "\x01\u018b\x1f\uffff\x01\u018b",
            "\x01\u018c\x1f\uffff\x01\u018c",
            "\x01\u018d\x1f\uffff\x01\u018d",
            "\x01\u018e\x1f\uffff\x01\u018e",
            "\x01\u018f\x1f\uffff\x01\u018f",
            "\x01\u0190\x1f\uffff\x01\u0190",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0192\x1f\uffff\x01\u0192",
            "\x01\u0194\x06\uffff\x01\u0193\x18\uffff\x01\u0194\x06\uffff"+
            "\x01\u0193",
            "\x01\u0195\x1f\uffff\x01\u0195",
            "\x01\u0196\x1f\uffff\x01\u0196",
            "\x01\u0197\x1f\uffff\x01\u0197",
            "\x0a\x31\x07\uffff\x02\x31\x01\u019a\x01\x31\x01\u0199\x15"+
            "\x31\x04\uffff\x01\x31\x01\uffff\x02\x31\x01\u019a\x01\x31\x01"+
            "\u0199\x15\x31",
            "\x01\u019b\x1f\uffff\x01\u019b",
            "\x01\u019c\x1f\uffff\x01\u019c",
            "\x01\u019d\x1f\uffff\x01\u019d",
            "\x01\u019e\x1f\uffff\x01\u019e",
            "\x01\u019f\x1f\uffff\x01\u019f",
            "\x01\u01a0\x1f\uffff\x01\u01a0",
            "\x01\u01a1\x1f\uffff\x01\u01a1",
            "\x01\u01a2\x1f\uffff\x01\u01a2",
            "\x01\u01a3\x1f\uffff\x01\u01a3",
            "\x01\u01a4\x1f\uffff\x01\u01a4",
            "\x01\u01a5\x1f\uffff\x01\u01a5",
            "",
            "\x01\u01a6\x1f\uffff\x01\u01a6",
            "\x01\u01a7\x1f\uffff\x01\u01a7",
            "",
            "\x0a\x31\x07\uffff\x04\x31\x01\u01a9\x09\x31\x01\u01aa\x0b"+
            "\x31\x04\uffff\x01\x31\x01\uffff\x04\x31\x01\u01a9\x09\x31\x01"+
            "\u01aa\x0b\x31",
            "\x01\u01ab\x1f\uffff\x01\u01ab",
            "\x01\u01ac\x1f\uffff\x01\u01ac",
            "\x01\u01ad\x0e\uffff\x01\u01ae\x10\uffff\x01\u01ad\x0e\uffff"+
            "\x01\u01ae",
            "\x01\u01af\x1f\uffff\x01\u01af",
            "\x01\u01b0\x1f\uffff\x01\u01b0",
            "",
            "\x01\u01b1\x1f\uffff\x01\u01b1",
            "\x01\u01b2\x1f\uffff\x01\u01b2",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\u01b4\x01\uffff\x1a"+
            "\x31",
            "\x01\u01b5\x1f\uffff\x01\u01b5",
            "\x01\u01b6\x09\uffff\x01\u01b7\x15\uffff\x01\u01b6\x09\uffff"+
            "\x01\u01b7",
            "\x01\u01b8\x1f\uffff\x01\u01b8",
            "\x01\u01b9\x1f\uffff\x01\u01b9",
            "\x01\u01ba\x1f\uffff\x01\u01ba",
            "\x01\u01bb\x1f\uffff\x01\u01bb",
            "\x01\u01bc\x1f\uffff\x01\u01bc",
            "\x01\u01bd\x1f\uffff\x01\u01bd",
            "\x01\u01be",
            "\x01\u01bf\x1f\uffff\x01\u01bf",
            "\x01\u01c0\x1f\uffff\x01\u01c0",
            "\x0a\x31\x07\uffff\x04\x31\x01\u01c2\x15\x31\x04\uffff\x01"+
            "\x31\x01\uffff\x04\x31\x01\u01c2\x15\x31",
            "\x01\u01c3\x1f\uffff\x01\u01c3",
            "\x01\u01c4",
            "",
            "\x0a\x31\x07\uffff\x0d\x31\x01\u01c6\x0c\x31\x04\uffff\x01"+
            "\x31\x01\uffff\x0d\x31\x01\u01c6\x0c\x31",
            "\x01\u01c7\x1f\uffff\x01\u01c7",
            "\x01\u01c8\x1f\uffff\x01\u01c8",
            "\x01\u01c9\x1f\uffff\x01\u01c9",
            "\x01\u01ca\x1f\uffff\x01\u01ca",
            "",
            "\x01\u01cb\x1f\uffff\x01\u01cb",
            "",
            "",
            "\x01\u01cc\x1f\uffff\x01\u01cc",
            "\x01\u01cd\x1f\uffff\x01\u01cd",
            "",
            "\x01\u01ce\x1f\uffff\x01\u01ce",
            "\x01\u01cf\x1f\uffff\x01\u01cf",
            "\x01\u01d0\x1f\uffff\x01\u01d0",
            "\x01\u01d1\x1f\uffff\x01\u01d1",
            "\x01\u01d2\x01\u01d3\x1e\uffff\x01\u01d2\x01\u01d3",
            "\x01\u01d4\x1f\uffff\x01\u01d4",
            "\x01\u01d5\x1f\uffff\x01\u01d5",
            "\x01\u01d6\x1f\uffff\x01\u01d6",
            "\x01\u01d7\x1f\uffff\x01\u01d7",
            "\x01\u01d8\x1f\uffff\x01\u01d8",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\u01da\x01\uffff\x1a"+
            "\x31",
            "\x01\u01db\x1f\uffff\x01\u01db",
            "\x01\u01dc\x1f\uffff\x01\u01dc",
            "\x01\u01dd\x1f\uffff\x01\u01dd",
            "\x01\u01de\x1f\uffff\x01\u01de",
            "\x01\u01df\x1f\uffff\x01\u01df",
            "\x01\u01e0\x1f\uffff\x01\u01e0",
            "\x01\u01e1\x1f\uffff\x01\u01e1",
            "\x01\u01e2\x1f\uffff\x01\u01e2",
            "\x01\u01e3\x1f\uffff\x01\u01e3",
            "\x01\u01e4\x1f\uffff\x01\u01e4",
            "\x01\u01e5\x1f\uffff\x01\u01e5",
            "\x01\u01e6\x1f\uffff\x01\u01e6",
            "\x01\u01e7\x1f\uffff\x01\u01e7",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u01e9\x1f\uffff\x01\u01e9",
            "\x01\u01ea\x1f\uffff\x01\u01ea",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\u01ec\x01\uffff\x1a"+
            "\x31",
            "\x01\u01ed\x1f\uffff\x01\u01ed",
            "\x01\u01ee\x1f\uffff\x01\u01ee",
            "\x01\u01ef\x1f\uffff\x01\u01ef",
            "\x01\u01f0\x1f\uffff\x01\u01f0",
            "\x01\u01f1\x1f\uffff\x01\u01f1",
            "\x01\u01f2\x1f\uffff\x01\u01f2",
            "\x01\u01f3\x1f\uffff\x01\u01f3",
            "\x01\u01f4\x1f\uffff\x01\u01f4",
            "\x01\u01f5\x1f\uffff\x01\u01f5",
            "",
            "\x01\u01f6\x1f\uffff\x01\u01f6",
            "\x01\u01f7\x1f\uffff\x01\u01f7",
            "\x01\u01f8\x1f\uffff\x01\u01f8",
            "\x01\u01f9\x1f\uffff\x01\u01f9",
            "\x01\u01fa\x1f\uffff\x01\u01fa",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u01fc\x1f\uffff\x01\u01fc",
            "\x01\u01fd\x1f\uffff\x01\u01fd",
            "\x01\u01fe",
            "\x01\u01ff\x1f\uffff\x01\u01ff",
            "\x01\u0200\x01\uffff\x01\u0201\x1d\uffff\x01\u0200\x01\uffff"+
            "\x01\u0201",
            "\x01\u0202\x1f\uffff\x01\u0202",
            "\x01\u0203\x1f\uffff\x01\u0203",
            "\x01\u0205\x01\u0204\x1e\uffff\x01\u0205\x01\u0204",
            "\x01\u0206\x1f\uffff\x01\u0206",
            "\x01\u0207\x1f\uffff\x01\u0207",
            "\x01\u0208\x1f\uffff\x01\u0208",
            "\x01\u0209\x03\uffff\x01\u020a\x1b\uffff\x01\u0209\x03\uffff"+
            "\x01\u020a",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u020c\x1f\uffff\x01\u020c",
            "\x01\u020d\x1f\uffff\x01\u020d",
            "\x01\u020e\x1f\uffff\x01\u020e",
            "",
            "\x01\u020f\x1f\uffff\x01\u020f",
            "\x01\u0210\x1f\uffff\x01\u0210",
            "\x01\u0211\x15\uffff\x01\u0212\x09\uffff\x01\u0211",
            "",
            "\x01\u0213\x1f\uffff\x01\u0213",
            "\x01\u0214\x1f\uffff\x01\u0214",
            "",
            "\x01\u0215\x1f\uffff\x01\u0215",
            "",
            "\x01\u0216\x1f\uffff\x01\u0216",
            "\x01\u0217\x1f\uffff\x01\u0217",
            "\x01\u0218\x1f\uffff\x01\u0218",
            "\x01\u0219\x1f\uffff\x01\u0219",
            "\x01\u021a\x1f\uffff\x01\u021a",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u021c\x1f\uffff\x01\u021c",
            "",
            "\x01\u021d\x1f\uffff\x01\u021d",
            "\x01\u021e\x10\uffff\x01\u021f\x0e\uffff\x01\u021e\x10\uffff"+
            "\x01\u021f",
            "\x01\u0220\x03\uffff\x01\u0221\x1b\uffff\x01\u0220\x03\uffff"+
            "\x01\u0221",
            "\x01\u0222\x1f\uffff\x01\u0222",
            "\x01\u0223\x1f\uffff\x01\u0223",
            "\x01\u0224\x1f\uffff\x01\u0224",
            "\x01\u0225\x1f\uffff\x01\u0225",
            "\x01\u0226\x1f\uffff\x01\u0226",
            "\x01\u0227\x1f\uffff\x01\u0227",
            "\x01\u0228\x1f\uffff\x01\u0228",
            "\x01\u0229\x1f\uffff\x01\u0229",
            "\x0a\x31\x07\uffff\x01\u022c\x11\x31\x01\u022b\x07\x31\x04"+
            "\uffff\x01\x31\x01\uffff\x01\u022c\x11\x31\x01\u022b\x07\x31",
            "\x01\u022d\x1f\uffff\x01\u022d",
            "\x01\u022e\x1f\uffff\x01\u022e",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0231\x1f\uffff\x01\u0231",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0233\x1f\uffff\x01\u0233",
            "\x01\u0234\x1f\uffff\x01\u0234",
            "\x01\u0235\x1f\uffff\x01\u0235",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0237\x1f\uffff\x01\u0237",
            "\x01\u0238\x1f\uffff\x01\u0238",
            "\x01\u0239\x1f\uffff\x01\u0239",
            "\x01\u023a\x1f\uffff\x01\u023a",
            "\x01\u023b\x1f\uffff\x01\u023b",
            "\x0a\x31\x07\uffff\x13\x31\x01\u023d\x06\x31\x04\uffff\x01"+
            "\x31\x01\uffff\x13\x31\x01\u023d\x06\x31",
            "\x0a\x31\x07\uffff\x01\x31\x01\u023f\x18\x31\x04\uffff\x01"+
            "\x31\x01\uffff\x01\x31\x01\u023f\x18\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0241\x1f\uffff\x01\u0241",
            "\x01\u0242\x1f\uffff\x01\u0242",
            "\x01\u0243\x1f\uffff\x01\u0243",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0245\x1f\uffff\x01\u0245",
            "",
            "\x01\u0246\x1f\uffff\x01\u0246",
            "\x01\u0247\x1f\uffff\x01\u0247",
            "\x01\u0248\x1f\uffff\x01\u0248",
            "\x01\u0249\x1f\uffff\x01\u0249",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x01\u024c\x1f\uffff\x01\u024c",
            "\x01\u024d\x1f\uffff\x01\u024d",
            "\x01\u024e\x1f\uffff\x01\u024e",
            "\x0a\x31\x07\uffff\x13\x31\x01\u0250\x06\x31\x04\uffff\x01"+
            "\x31\x01\uffff\x13\x31\x01\u0250\x06\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0252\x1f\uffff\x01\u0252",
            "\x01\u0253\x1f\uffff\x01\u0253",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0255\x1f\uffff\x01\u0255",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0257\x1f\uffff\x01\u0257",
            "\x01\u0258",
            "\x01\u0259\x1f\uffff\x01\u0259",
            "\x01\u025a\x1f\uffff\x01\u025a",
            "\x01\u025b\x1f\uffff\x01\u025b",
            "",
            "\x01\u025d\x0a\uffff\x01\u025c\x14\uffff\x01\u025d\x0a\uffff"+
            "\x01\u025c",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u025f\x1f\uffff\x01\u025f",
            "\x01\u0260\x1f\uffff\x01\u0260",
            "\x01\u0261\x1f\uffff\x01\u0261",
            "\x01\u0262\x1f\uffff\x01\u0262",
            "\x01\u0263\x1f\uffff\x01\u0263",
            "\x01\u0264\x1f\uffff\x01\u0264",
            "\x01\u0265\x1f\uffff\x01\u0265",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x01\u0267\x1f\uffff\x01\u0267",
            "\x01\u0269\x11\uffff\x01\u0268\x0d\uffff\x01\u0269\x11\uffff"+
            "\x01\u0268",
            "\x01\u026a\x1f\uffff\x01\u026a",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u026d\x1f\uffff\x01\u026d",
            "\x01\u026e\x1f\uffff\x01\u026e",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0271\x1f\uffff\x01\u0271",
            "\x01\u0272\x1f\uffff\x01\u0272",
            "\x01\u0273\x1f\uffff\x01\u0273",
            "\x01\u0274\x1f\uffff\x01\u0274",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0276\x1f\uffff\x01\u0276",
            "\x01\u0277\x1f\uffff\x01\u0277",
            "",
            "\x01\u0278\x1f\uffff\x01\u0278",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u027a\x1f\uffff\x01\u027a",
            "\x01\u027b\x1f\uffff\x01\u027b",
            "\x01\u027c\x1f\uffff\x01\u027c",
            "\x01\u027d\x1f\uffff\x01\u027d",
            "\x01\u027e\x1f\uffff\x01\u027e",
            "\x01\u027f\x1f\uffff\x01\u027f",
            "\x01\u0280\x1f\uffff\x01\u0280",
            "\x01\u0281\x1f\uffff\x01\u0281",
            "\x01\u0282\x1f\uffff\x01\u0282",
            "\x01\u0283\x1f\uffff\x01\u0283",
            "\x01\u0284\x1f\uffff\x01\u0284",
            "\x01\u0285\x1f\uffff\x01\u0285",
            "\x01\u0286",
            "\x01\u0287\x1f\uffff\x01\u0287",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0289\x1f\uffff\x01\u0289",
            "\x01\u028a\x12\uffff\x01\u028b\x0c\uffff\x01\u028a\x12\uffff"+
            "\x01\u028b",
            "",
            "\x01\u028c\x1f\uffff\x01\u028c",
            "\x01\u028d\x1f\uffff\x01\u028d",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u028f\x1f\uffff\x01\u028f",
            "\x01\u0290\x1f\uffff\x01\u0290",
            "\x01\u0291\x1f\uffff\x01\u0291",
            "\x01\u0292\x1f\uffff\x01\u0292",
            "\x01\u0293\x1f\uffff\x01\u0293",
            "\x01\u0294\x1f\uffff\x01\u0294",
            "\x01\u0295\x1f\uffff\x01\u0295",
            "\x01\u0296\x1f\uffff\x01\u0296",
            "\x01\u0297\x1f\uffff\x01\u0297",
            "\x01\u0298\x1f\uffff\x01\u0298",
            "\x01\u0299\x1f\uffff\x01\u0299",
            "",
            "\x01\u029a\x1f\uffff\x01\u029a",
            "\x01\u029b\x1f\uffff\x01\u029b",
            "",
            "\x01\u029c\x01\u029e\x0a\uffff\x01\u029f\x04\uffff\x01\u029d"+
            "\x0e\uffff\x01\u029c\x01\u029e\x0a\uffff\x01\u029f\x04\uffff"+
            "\x01\u029d",
            "\x01\u02a0\x1f\uffff\x01\u02a0",
            "\x01\u02a1\x1f\uffff\x01\u02a1",
            "\x01\u02a2\x1f\uffff\x01\u02a2",
            "\x01\u02a3\x1f\uffff\x01\u02a3",
            "\x01\u02a4\x1f\uffff\x01\u02a4",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u02a6\x1f\uffff\x01\u02a6",
            "\x0a\x31\x07\uffff\x12\x31\x01\u02a8\x07\x31\x04\uffff\x01"+
            "\x31\x01\uffff\x12\x31\x01\u02a8\x07\x31",
            "\x01\u02a9\x06\uffff\x01\u02aa\x0a\uffff\x01\u02ab\x0d\uffff"+
            "\x01\u02a9\x06\uffff\x01\u02aa\x0a\uffff\x01\u02ab",
            "\x0a\x31\x07\uffff\x0e\x31\x01\u02ad\x0b\x31\x04\uffff\x01"+
            "\x31\x01\uffff\x0e\x31\x01\u02ad\x0b\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u02b0\x1f\uffff\x01\u02b0",
            "\x01\u02b1\x1f\uffff\x01\u02b1",
            "",
            "\x01\u02b2\x1f\uffff\x01\u02b2",
            "\x01\u02b3\x1f\uffff\x01\u02b3",
            "\x01\u02b4\x1f\uffff\x01\u02b4",
            "\x01\u02b5\x1f\uffff\x01\u02b5",
            "\x01\u02b6\x1f\uffff\x01\u02b6",
            "\x01\u02b7\x1f\uffff\x01\u02b7",
            "\x01\u02b8\x1f\uffff\x01\u02b8",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u02ba\x1f\uffff\x01\u02ba",
            "\x01\u02bb\x1f\uffff\x01\u02bb",
            "\x01\u02bc\x1f\uffff\x01\u02bc",
            "\x01\u02bd\x1f\uffff\x01\u02bd",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u02c0\x1f\uffff\x01\u02c0",
            "",
            "\x01\u02c1\x1f\uffff\x01\u02c1",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u02c3\x1f\uffff\x01\u02c3",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u02c6\x1f\uffff\x01\u02c6",
            "\x01\u02c7\x1f\uffff\x01\u02c7",
            "\x01\u02c8\x1f\uffff\x01\u02c8",
            "\x01\u02c9\x1f\uffff\x01\u02c9",
            "\x01\u02ca\x1f\uffff\x01\u02ca",
            "\x01\u02cb\x1f\uffff\x01\u02cb",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u02cd\x1f\uffff\x01\u02cd",
            "\x01\u02ce\x1f\uffff\x01\u02ce",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x01\u02d0\x1f\uffff\x01\u02d0",
            "\x01\u02d1\x1f\uffff\x01\u02d1",
            "\x01\u02d2\x1f\uffff\x01\u02d2",
            "\x01\u02d3\x1f\uffff\x01\u02d3",
            "\x01\u02d4\x1f\uffff\x01\u02d4",
            "\x01\u02d5\x1f\uffff\x01\u02d5",
            "\x01\u02d6\x1f\uffff\x01\u02d6",
            "\x01\u02d7\x1f\uffff\x01\u02d7",
            "\x01\u02d8\x1f\uffff\x01\u02d8",
            "\x01\u02d9\x1f\uffff\x01\u02d9",
            "\x01\u02da\x1f\uffff\x01\u02da",
            "\x01\u02db\x1f\uffff\x01\u02db",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u02dd\x1f\uffff\x01\u02dd",
            "",
            "\x01\u02de\x1f\uffff\x01\u02de",
            "\x01\u02df\x1f\uffff\x01\u02df",
            "\x0a\x31\x07\uffff\x12\x31\x01\u02e1\x07\x31\x04\uffff\x01"+
            "\x31\x01\uffff\x12\x31\x01\u02e1\x07\x31",
            "\x01\u02e2\x1f\uffff\x01\u02e2",
            "",
            "",
            "\x01\u02e3\x1f\uffff\x01\u02e3",
            "",
            "\x01\u02e4\x1f\uffff\x01\u02e4",
            "\x01\u02e5\x1f\uffff\x01\u02e5",
            "\x01\u02e6",
            "",
            "\x01\u02e7\x1f\uffff\x01\u02e7",
            "\x01\u02e8\x1f\uffff\x01\u02e8",
            "\x01\u02e9\x1f\uffff\x01\u02e9",
            "\x01\u02ea\x1f\uffff\x01\u02ea",
            "\x01\u02eb\x1f\uffff\x01\u02eb",
            "",
            "\x01\u02ec\x1f\uffff\x01\u02ec",
            "",
            "\x01\u02ed\x1f\uffff\x01\u02ed",
            "",
            "\x01\u02ee\x1f\uffff\x01\u02ee",
            "\x01\u02ef\x1f\uffff\x01\u02ef",
            "\x01\u02f0\x1f\uffff\x01\u02f0",
            "",
            "\x01\u02f1\x1f\uffff\x01\u02f1",
            "\x01\u02f2\x1f\uffff\x01\u02f2",
            "\x01\u02f3\x1f\uffff\x01\u02f3",
            "\x01\u02f4\x1f\uffff\x01\u02f4",
            "\x01\u02f5\x1f\uffff\x01\u02f5",
            "",
            "",
            "\x01\u02f6\x1f\uffff\x01\u02f6",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x01\u02f9\x1f\uffff\x01\u02f9",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x01\u02fc\x1f\uffff\x01\u02fc",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u02fe\x1f\uffff\x01\u02fe",
            "\x01\u02ff\x1f\uffff\x01\u02ff",
            "\x01\u0300\x1f\uffff\x01\u0300",
            "\x01\u0301\x1f\uffff\x01\u0301",
            "\x01\u0303\x02\uffff\x01\u0302\x1c\uffff\x01\u0303\x02\uffff"+
            "\x01\u0302",
            "\x01\u0304\x1f\uffff\x01\u0304",
            "",
            "\x01\u0305\x1f\uffff\x01\u0305",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0307\x1f\uffff\x01\u0307",
            "\x01\u0308\x1f\uffff\x01\u0308",
            "\x01\u0309\x1f\uffff\x01\u0309",
            "\x0a\x31\x07\uffff\x04\x31\x01\u030b\x15\x31\x04\uffff\x01"+
            "\x31\x01\uffff\x04\x31\x01\u030b\x15\x31",
            "\x01\u030c\x1f\uffff\x01\u030c",
            "",
            "\x01\u030d\x1f\uffff\x01\u030d",
            "\x01\u030e\x1f\uffff\x01\u030e",
            "\x01\u030f\x1f\uffff\x01\u030f",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "",
            "\x01\u0311\x1f\uffff\x01\u0311",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "",
            "\x01\u0314\x03\uffff\x01\u0313\x1b\uffff\x01\u0314\x03\uffff"+
            "\x01\u0313",
            "\x01\u0315\x1f\uffff\x01\u0315",
            "\x01\u0316\x1f\uffff\x01\u0316",
            "\x01\u0317\x1f\uffff\x01\u0317",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0319\x1f\uffff\x01\u0319",
            "\x01\u031a\x1f\uffff\x01\u031a",
            "",
            "\x01\u031b\x1f\uffff\x01\u031b",
            "\x01\u031c\x1f\uffff\x01\u031c",
            "\x01\u031d\x1f\uffff\x01\u031d",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u031f\x1f\uffff\x01\u031f",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0321\x1f\uffff\x01\u0321",
            "\x01\u0322\x1f\uffff\x01\u0322",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0324\x1f\uffff\x01\u0324",
            "\x01\u0325\x1f\uffff\x01\u0325",
            "\x01\u0326\x1f\uffff\x01\u0326",
            "\x01\u0327\x1f\uffff\x01\u0327",
            "\x01\u0328\x1f\uffff\x01\u0328",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u032a\x1f\uffff\x01\u032a",
            "\x01\u032b\x1f\uffff\x01\u032b",
            "\x01\u032c\x1f\uffff\x01\u032c",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x01\u032e\x1f\uffff\x01\u032e",
            "\x01\u032f\x1f\uffff\x01\u032f",
            "\x01\u0330\x1f\uffff\x01\u0330",
            "\x01\u0331\x1f\uffff\x01\u0331",
            "\x01\u0332\x1f\uffff\x01\u0332",
            "\x01\u0333\x1f\uffff\x01\u0333",
            "\x01\u0334\x1f\uffff\x01\u0334",
            "\x01\u0335\x1f\uffff\x01\u0335",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0337\x1f\uffff\x01\u0337",
            "\x01\u0338\x1f\uffff\x01\u0338",
            "\x01\u0339\x1f\uffff\x01\u0339",
            "\x01\u033a\x1f\uffff\x01\u033a",
            "\x01\u033b\x0b\uffff\x01\u033c\x13\uffff\x01\u033b\x0b\uffff"+
            "\x01\u033c",
            "\x01\u033d\x1f\uffff\x01\u033d",
            "\x01\u033e\x1f\uffff\x01\u033e",
            "\x01\u033f\x1f\uffff\x01\u033f",
            "\x01\u0340\x1f\uffff\x01\u0340",
            "\x01\u0341\x1f\uffff\x01\u0341",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0343\x1f\uffff\x01\u0343",
            "\x01\u0344\x1f\uffff\x01\u0344",
            "",
            "\x0a\x31\x07\uffff\x12\x31\x01\u0346\x07\x31\x04\uffff\x01"+
            "\x31\x01\uffff\x12\x31\x01\u0346\x07\x31",
            "",
            "\x01\u0347\x1f\uffff\x01\u0347",
            "\x01\u0348\x1f\uffff\x01\u0348",
            "\x01\u0349\x1f\uffff\x01\u0349",
            "\x01\u034a\x1f\uffff\x01\u034a",
            "",
            "\x01\u034b\x1f\uffff\x01\u034b",
            "",
            "",
            "\x01\u034c\x1f\uffff\x01\u034c",
            "\x01\u034d\x1f\uffff\x01\u034d",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u034f\x1f\uffff\x01\u034f",
            "\x01\u0350\x1f\uffff\x01\u0350",
            "\x01\u0351\x1f\uffff\x01\u0351",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0353\x1f\uffff\x01\u0353",
            "\x01\u0354\x1f\uffff\x01\u0354",
            "",
            "\x01\u0355\x1f\uffff\x01\u0355",
            "\x01\u0356\x1f\uffff\x01\u0356",
            "\x01\u0357\x1f\uffff\x01\u0357",
            "\x01\u0358\x1f\uffff\x01\u0358",
            "",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u035a\x1f\uffff\x01\u035a",
            "",
            "\x01\u035b\x1f\uffff\x01\u035b",
            "",
            "",
            "\x01\u035c\x1f\uffff\x01\u035c",
            "\x01\u035d\x1f\uffff\x01\u035d",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0360\x1f\uffff\x01\u0360",
            "\x01\u0361\x1f\uffff\x01\u0361",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0363\x1f\uffff\x01\u0363",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0366\x1f\uffff\x01\u0366",
            "\x01\u0367\x1f\uffff\x01\u0367",
            "\x01\u0368\x1f\uffff\x01\u0368",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u036a\x1f\uffff\x01\u036a",
            "\x01\u036b\x1f\uffff\x01\u036b",
            "\x01\u036c\x1f\uffff\x01\u036c",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\u036e\x01\uffff\x1a"+
            "\x31",
            "\x01\u036f\x1f\uffff\x01\u036f",
            "\x01\u0370\x1f\uffff\x01\u0370",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0372\x1f\uffff\x01\u0372",
            "\x01\u0373\x1f\uffff\x01\u0373",
            "",
            "\x01\u0374\x1f\uffff\x01\u0374",
            "\x01\u0375\x1f\uffff\x01\u0375",
            "\x01\u0376\x1f\uffff\x01\u0376",
            "\x01\u0377\x1f\uffff\x01\u0377",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0379\x1f\uffff\x01\u0379",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u037b\x03\uffff\x01\u037c\x1b\uffff\x01\u037b\x03\uffff"+
            "\x01\u037c",
            "\x01\u037d\x1f\uffff\x01\u037d",
            "\x01\u037e\x1f\uffff\x01\u037e",
            "\x01\u037f\x1f\uffff\x01\u037f",
            "\x01\u0380\x1f\uffff\x01\u0380",
            "\x01\u0381\x1f\uffff\x01\u0381",
            "\x01\u0382\x1f\uffff\x01\u0382",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0386\x1f\uffff\x01\u0386",
            "\x01\u0387\x1f\uffff\x01\u0387",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u038a\x1f\uffff\x01\u038a",
            "",
            "",
            "\x01\u038b\x1f\uffff\x01\u038b",
            "",
            "",
            "\x01\u038c\x1f\uffff\x01\u038c",
            "",
            "\x01\u038d\x1f\uffff\x01\u038d",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0390\x1f\uffff\x01\u0390",
            "\x01\u0391\x1f\uffff\x01\u0391",
            "\x01\u0392\x1f\uffff\x01\u0392",
            "\x01\u0393\x1f\uffff\x01\u0393",
            "\x01\u0394\x1f\uffff\x01\u0394",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\u0396\x01\uffff\x1a"+
            "\x31",
            "\x01\u0397\x1f\uffff\x01\u0397",
            "\x01\u0398\x1f\uffff\x01\u0398",
            "",
            "\x01\u0399\x1f\uffff\x01\u0399",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u039b\x1f\uffff\x01\u039b",
            "\x01\u039c\x1f\uffff\x01\u039c",
            "\x01\u039d\x1f\uffff\x01\u039d",
            "",
            "\x01\u039e\x1f\uffff\x01\u039e",
            "",
            "\x01\u039f\x1f\uffff\x01\u039f",
            "\x01\u03a0\x1f\uffff\x01\u03a0",
            "\x01\u03a1\x1f\uffff\x01\u03a1",
            "\x01\u03a3\x06\uffff\x01\u03a2\x0a\uffff\x01\u03a4\x0d\uffff"+
            "\x01\u03a3\x06\uffff\x01\u03a2\x0a\uffff\x01\u03a4",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x01\u03a6\x1f\uffff\x01\u03a6",
            "\x01\u03a7\x1f\uffff\x01\u03a7",
            "\x01\u03a8\x1f\uffff\x01\u03a8",
            "\x01\u03a9\x1f\uffff\x01\u03a9",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u03ad\x1f\uffff\x01\u03ad",
            "",
            "\x01\u03ae\x1f\uffff\x01\u03ae",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u03b0\x1f\uffff\x01\u03b0",
            "\x01\u03b1\x1f\uffff\x01\u03b1",
            "\x01\u03b2\x1f\uffff\x01\u03b2",
            "",
            "\x01\u03b3\x1f\uffff\x01\u03b3",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u03b5\x1f\uffff\x01\u03b5",
            "",
            "\x01\u03b6\x1f\uffff\x01\u03b6",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u03b8\x1f\uffff\x01\u03b8",
            "\x01\u03b9\x1f\uffff\x01\u03b9",
            "\x01\u03ba\x1f\uffff\x01\u03ba",
            "\x01\u03bb\x1f\uffff\x01\u03bb",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u03bd\x1f\uffff\x01\u03bd",
            "",
            "\x01\u03be\x1f\uffff\x01\u03be",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u03c0\x1f\uffff\x01\u03c0",
            "\x01\u03c1\x1f\uffff\x01\u03c1",
            "\x01\u03c2\x1f\uffff\x01\u03c2",
            "\x01\u03c3\x1f\uffff\x01\u03c3",
            "\x01\u03c4\x1f\uffff\x01\u03c4",
            "\x01\u03c6\x08\uffff\x01\u03c5\x16\uffff\x01\u03c6\x08\uffff"+
            "\x01\u03c5",
            "\x01\u03c7",
            "\x01\u03c8\x1f\uffff\x01\u03c8",
            "\x01\u03c9\x1f\uffff\x01\u03c9",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u03cb\x1f\uffff\x01\u03cb",
            "",
            "\x01\u03cc\x1f\uffff\x01\u03cc",
            "\x01\u03cd\x1f\uffff\x01\u03cd",
            "\x01\u03ce\x1f\uffff\x01\u03ce",
            "\x01\u03cf\x1f\uffff\x01\u03cf",
            "\x01\u03d0\x1f\uffff\x01\u03d0",
            "\x01\u03d1\x1f\uffff\x01\u03d1",
            "\x01\u03d2\x1f\uffff\x01\u03d2",
            "\x01\u03d3\x1f\uffff\x01\u03d3",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u03d5\x1f\uffff\x01\u03d5",
            "\x01\u03d6\x1f\uffff\x01\u03d6",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u03d8\x1f\uffff\x01\u03d8",
            "\x01\u03d9\x1f\uffff\x01\u03d9",
            "\x01\u03da\x1f\uffff\x01\u03da",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x01\u03dd\x1f\uffff\x01\u03dd",
            "\x01\u03de\x1f\uffff\x01\u03de",
            "\x01\u03df\x1f\uffff\x01\u03df",
            "\x01\u03e0\x1f\uffff\x01\u03e0",
            "",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u03e2",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u03e5\x1f\uffff\x01\u03e5",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x01\u03e7\x1f\uffff\x01\u03e7",
            "\x01\u03e8\x1f\uffff\x01\u03e8",
            "\x01\u03e9\x1f\uffff\x01\u03e9",
            "",
            "\x01\u03ea\x1f\uffff\x01\u03ea",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u03ec",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u03ee\x1f\uffff\x01\u03ee",
            "\x01\u03ef\x1f\uffff\x01\u03ef",
            "\x0a\x31\x07\uffff\x03\x31\x01\u03f1\x16\x31\x04\uffff\x01"+
            "\x31\x01\uffff\x03\x31\x01\u03f1\x16\x31",
            "\x01\u03f2\x1f\uffff\x01\u03f2",
            "\x01\u03f3\x1f\uffff\x01\u03f3",
            "",
            "\x01\u03f4\x1f\uffff\x01\u03f4",
            "",
            "\x01\u03f5\x1f\uffff\x01\u03f5",
            "\x01\u03f6\x1f\uffff\x01\u03f6",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u03fa\x1f\uffff\x01\u03fa",
            "\x01\u03fb\x1f\uffff\x01\u03fb",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u03fe\x1f\uffff\x01\u03fe",
            "",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0400\x1f\uffff\x01\u0400",
            "\x01\u0401\x1f\uffff\x01\u0401",
            "\x01\u0402\x1f\uffff\x01\u0402",
            "",
            "",
            "\x01\u0403\x1f\uffff\x01\u0403",
            "\x01\u0404\x1f\uffff\x01\u0404",
            "\x01\u0405\x1f\uffff\x01\u0405",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x01\u0408\x1f\uffff\x01\u0408",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u040a\x1f\uffff\x01\u040a",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x01\u040c\x1f\uffff\x01\u040c",
            "\x01\u040d\x1f\uffff\x01\u040d",
            "\x01\u040e\x1f\uffff\x01\u040e",
            "\x01\u040f\x1f\uffff\x01\u040f",
            "\x01\u0410\x02\uffff\x01\u0411\x1c\uffff\x01\u0410\x02\uffff"+
            "\x01\u0411",
            "\x01\u0412\x1f\uffff\x01\u0412",
            "\x01\u0413\x1f\uffff\x01\u0413",
            "\x01\u0414\x1f\uffff\x01\u0414",
            "\x01\u0415\x1f\uffff\x01\u0415",
            "\x01\u0416\x1f\uffff\x01\u0416",
            "",
            "\x01\u0417\x1f\uffff\x01\u0417",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u041e\x1f\uffff\x01\u041e",
            "\x01\u041f\x1f\uffff\x01\u041f",
            "\x01\u0420\x1f\uffff\x01\u0420",
            "",
            "\x01\u0421\x1f\uffff\x01\u0421",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x01\u0423\x1f\uffff\x01\u0423",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0425\x1f\uffff\x01\u0425",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x01\u0427\x1f\uffff\x01\u0427",
            "\x01\u0428\x1f\uffff\x01\u0428",
            "",
            "\x01\u0429\x1f\uffff\x01\u0429",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u042b",
            "\x01\u042c\x1f\uffff\x01\u042c",
            "\x01\u042d\x1f\uffff\x01\u042d",
            "\x01\u042e\x1f\uffff\x01\u042e",
            "\x01\u042f\x1f\uffff\x01\u042f",
            "\x01\u0430\x1f\uffff\x01\u0430",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0432\x1f\uffff\x01\u0432",
            "",
            "\x01\u0433\x1f\uffff\x01\u0433",
            "\x01\u0434\x1f\uffff\x01\u0434",
            "\x01\u0435\x1f\uffff\x01\u0435",
            "\x01\u0436\x1f\uffff\x01\u0436",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0438\x1f\uffff\x01\u0438",
            "\x01\u0439\x1f\uffff\x01\u0439",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u043b\x1f\uffff\x01\u043b",
            "",
            "\x01\u043c\x1f\uffff\x01\u043c",
            "\x01\u043d\x1f\uffff\x01\u043d",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0440\x1f\uffff\x01\u0440",
            "",
            "",
            "\x01\u0441\x1f\uffff\x01\u0441",
            "\x01\u0442\x1f\uffff\x01\u0442",
            "\x01\u0443\x1f\uffff\x01\u0443",
            "\x01\u0444\x1f\uffff\x01\u0444",
            "",
            "\x01\u0445\x1f\uffff\x01\u0445",
            "",
            "",
            "\x01\u0446\x1f\uffff\x01\u0446",
            "",
            "\x01\u0447\x1f\uffff\x01\u0447",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0449\x1f\uffff\x01\u0449",
            "\x01\u044a\x1f\uffff\x01\u044a",
            "",
            "\x01\u044b\x0f\uffff\x01\u044c\x01\u044d\x0e\uffff\x01\u044b"+
            "\x0f\uffff\x01\u044c\x01\u044d",
            "",
            "\x01\u044e\x1f\uffff\x01\u044e",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0451\x1f\uffff\x01\u0451",
            "\x0a\x31\x07\uffff\x11\x31\x01\u0453\x08\x31\x04\uffff\x01"+
            "\x31\x01\uffff\x11\x31\x01\u0453\x08\x31",
            "\x01\u0454\x1f\uffff\x01\u0454",
            "\x01\u0455\x1f\uffff\x01\u0455",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "",
            "\x01\u0459\x1f\uffff\x01\u0459",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x02\x31\x01\u045c\x17\x31\x04\uffff\x01"+
            "\x31\x01\uffff\x02\x31\x01\u045c\x17\x31",
            "\x01\u045d\x1f\uffff\x01\u045d",
            "\x01\u045e\x1f\uffff\x01\u045e",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0460\x1f\uffff\x01\u0460",
            "",
            "",
            "\x01\u0461\x1f\uffff\x01\u0461",
            "",
            "\x01\u0462\x1f\uffff\x01\u0462",
            "",
            "\x01\u0463\x1f\uffff\x01\u0463",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0466\x1f\uffff\x01\u0466",
            "\x01\u0467\x1f\uffff\x01\u0467",
            "\x01\u0468\x1f\uffff\x01\u0468",
            "\x01\u0469\x1f\uffff\x01\u0469",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u046b\x1f\uffff\x01\u046b",
            "\x01\u046c\x1f\uffff\x01\u046c",
            "\x01\u046d\x1f\uffff\x01\u046d",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "",
            "",
            "",
            "",
            "",
            "\x01\u046f\x1f\uffff\x01\u046f",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0472\x1f\uffff\x01\u0472",
            "",
            "\x01\u0473\x1f\uffff\x01\u0473",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x01\u0475\x1f\uffff\x01\u0475",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0477",
            "",
            "\x01\u0478\x1f\uffff\x01\u0478",
            "\x01\u0479\x1f\uffff\x01\u0479",
            "\x01\u047a\x1f\uffff\x01\u047a",
            "\x01\u047b",
            "\x01\u047c\x1f\uffff\x01\u047c",
            "\x01\u047d\x1f\uffff\x01\u047d",
            "",
            "\x01\u047e\x1f\uffff\x01\u047e",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0480\x1f\uffff\x01\u0480",
            "\x01\u0481\x1f\uffff\x01\u0481",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0484\x1f\uffff\x01\u0484",
            "",
            "\x01\u0485\x1f\uffff\x01\u0485",
            "\x01\u0486\x1f\uffff\x01\u0486",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "",
            "\x01\u0488\x1f\uffff\x01\u0488",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u048b\x1f\uffff\x01\u048b",
            "\x01\u048c\x1f\uffff\x01\u048c",
            "\x01\u048d\x1f\uffff\x01\u048d",
            "\x01\u048e\x1f\uffff\x01\u048e",
            "\x01\u048f\x1f\uffff\x01\u048f",
            "",
            "\x01\u0490\x1f\uffff\x01\u0490",
            "\x01\u0491\x1f\uffff\x01\u0491",
            "\x01\u0492\x1f\uffff\x01\u0492",
            "\x01\u0493\x1f\uffff\x01\u0493",
            "\x01\u0494\x1f\uffff\x01\u0494",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x01\u0497\x1f\uffff\x01\u0497",
            "\x01\u0498",
            "\x01\u0499\x1f\uffff\x01\u0499",
            "",
            "",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "",
            "\x01\u049b\x1f\uffff\x01\u049b",
            "\x01\u049c\x1f\uffff\x01\u049c",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u049f\x1f\uffff\x01\u049f",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u04a1",
            "",
            "",
            "\x01\u04a2\x1f\uffff\x01\u04a2",
            "\x01\u04a3\x1f\uffff\x01\u04a3",
            "\x01\u04a4\x1f\uffff\x01\u04a4",
            "\x01\u04a5\x1f\uffff\x01\u04a5",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u04a7\x1f\uffff\x01\u04a7",
            "\x01\u04a8\x1f\uffff\x01\u04a8",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "",
            "\x01\u04aa\x1f\uffff\x01\u04aa",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x01\u04ac\x1f\uffff\x01\u04ac",
            "",
            "\x01\u04ad\x1f\uffff\x01\u04ad",
            "\x01\u04ae\x1f\uffff\x01\u04ae",
            "\x01\u04af\x1f\uffff\x01\u04af",
            "\x01\u04b0",
            "\x01\u04b1\x1f\uffff\x01\u04b1",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u04b3\x1f\uffff\x01\u04b3",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x01\u04b5\x1f\uffff\x01\u04b5",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u04b8\x1f\uffff\x01\u04b8",
            "\x01\u04b9\x1f\uffff\x01\u04b9",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "",
            "\x01\u04bb\x1f\uffff\x01\u04bb",
            "\x01\u04bc\x1f\uffff\x01\u04bc",
            "\x01\u04bd\x1f\uffff\x01\u04bd",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u04c1\x1f\uffff\x01\u04c1",
            "\x01\u04c2\x1f\uffff\x01\u04c2",
            "\x01\u04c3\x1f\uffff\x01\u04c3",
            "\x01\u04c4\x1f\uffff\x01\u04c4",
            "",
            "",
            "\x01\u04c5\x1f\uffff\x01\u04c5",
            "\x01\u04c6\x1f\uffff\x01\u04c6",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x01\u04c8\x1f\uffff\x01\u04c8",
            "\x01\u04c9\x1f\uffff\x01\u04c9",
            "",
            "",
            "\x01\u04ca\x1f\uffff\x01\u04ca",
            "",
            "\x01\u04cb\x1f\uffff\x01\u04cb",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u04ce\x1f\uffff\x01\u04ce",
            "\x01\u04cf\x1f\uffff\x01\u04cf",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u04d4\x1f\uffff\x01\u04d4",
            "\x01\u04d5\x1f\uffff\x01\u04d5",
            "\x01\u04d6",
            "\x01\u04d7\x1f\uffff\x01\u04d7",
            "\x01\u04d8\x1f\uffff\x01\u04d8",
            "",
            "\x01\u04d9\x1f\uffff\x01\u04d9",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "",
            "\x01\u04db\x1f\uffff\x01\u04db",
            "\x01\u04dc\x1f\uffff\x01\u04dc",
            "",
            "\x01\u04dd\x1f\uffff\x01\u04dd",
            "\x01\u04de\x1f\uffff\x01\u04de",
            "\x01\u04df\x1f\uffff\x01\u04df",
            "",
            "",
            "",
            "\x01\u04e0\x1f\uffff\x01\u04e0",
            "\x01\u04e1\x1f\uffff\x01\u04e1",
            "\x01\u04e2\x1f\uffff\x01\u04e2",
            "\x01\u04e3\x1f\uffff\x01\u04e3",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u04e5\x1f\uffff\x01\u04e5",
            "",
            "\x01\u04e6\x1f\uffff\x01\u04e6",
            "\x01\u04e7\x1f\uffff\x01\u04e7",
            "\x01\u04e8\x1f\uffff\x01\u04e8",
            "\x01\u04e9\x1f\uffff\x01\u04e9",
            "",
            "",
            "\x01\u04ea\x1f\uffff\x01\u04ea",
            "\x01\u04eb\x1f\uffff\x01\u04eb",
            "",
            "",
            "",
            "",
            "\x01\u04ec\x1f\uffff\x01\u04ec",
            "\x01\u04ed\x1f\uffff\x01\u04ed",
            "\x01\u04ee\x1f\uffff\x01\u04ee",
            "\x01\u04ef\x1f\uffff\x01\u04ef",
            "\x01\u04f0\x1f\uffff\x01\u04f0",
            "\x01\u04f1\x1f\uffff\x01\u04f1",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u04f3\x1f\uffff\x01\u04f3",
            "\x01\u04f4\x1f\uffff\x01\u04f4",
            "\x01\u04f5\x1f\uffff\x01\u04f5",
            "\x01\u04f6\x1f\uffff\x01\u04f6",
            "\x01\u04f7\x1f\uffff\x01\u04f7",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x12\x31\x01\u04fa\x07\x31\x04\uffff\x01"+
            "\x31\x01\uffff\x12\x31\x01\u04fa\x07\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x01\u04fc\x1f\uffff\x01\u04fc",
            "\x01\u04fd\x1f\uffff\x01\u04fd",
            "\x01\u04fe\x1f\uffff\x01\u04fe",
            "\x01\u04ff\x1f\uffff\x01\u04ff",
            "\x01\u0500\x1f\uffff\x01\u0500",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0502\x1f\uffff\x01\u0502",
            "\x01\u0503\x1f\uffff\x01\u0503",
            "\x01\u0504\x1f\uffff\x01\u0504",
            "\x01\u0505\x1f\uffff\x01\u0505",
            "\x01\u0506\x1f\uffff\x01\u0506",
            "\x01\u0507\x1f\uffff\x01\u0507",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x01\u0509\x1f\uffff\x01\u0509",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u050b\x1f\uffff\x01\u050b",
            "\x01\u050c\x1f\uffff\x01\u050c",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "",
            "\x01\u050e\x1f\uffff\x01\u050e",
            "",
            "\x01\u050f\x1f\uffff\x01\u050f",
            "\x01\u0510\x1f\uffff\x01\u0510",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0513\x1f\uffff\x01\u0513",
            "",
            "\x01\u0514\x1f\uffff\x01\u0514",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0516\x1f\uffff\x01\u0516",
            "\x01\u0517\x1f\uffff\x01\u0517",
            "\x01\u0518\x1f\uffff\x01\u0518",
            "\x01\u0519\x1f\uffff\x01\u0519",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x01\u051d\x1f\uffff\x01\u051d",
            "\x01\u051e\x1f\uffff\x01\u051e",
            "\x01\u051f\x1f\uffff\x01\u051f",
            "",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0521\x1f\uffff\x01\u0521",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0523\x1f\uffff\x01\u0523",
            "\x01\u0524\x1f\uffff\x01\u0524",
            "\x01\u0525",
            "",
            "",
            "",
            "\x01\u0526\x1f\uffff\x01\u0526",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0528\x1f\uffff\x01\u0528",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x01\u052a\x1f\uffff\x01\u052a",
            "\x01\u052b\x1f\uffff\x01\u052b",
            "\x01\u052c\x1f\uffff\x01\u052c",
            "\x01\u052d\x1f\uffff\x01\u052d",
            "",
            "\x01\u052e\x1f\uffff\x01\u052e",
            "",
            "\x01\u052f\x1f\uffff\x01\u052f",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0531\x1f\uffff\x01\u0531",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "\x01\u0533\x1f\uffff\x01\u0533",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x01\u0535\x1f\uffff\x01\u0535",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
            "",
            "\x01\u0537\x1f\uffff\x01\u0537",
            "",
            "\x0a\x31\x07\uffff\x1a\x31\x04\uffff\x01\x31\x01\uffff\x1a"+
            "\x31",
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
            get { return "1:1: Tokens : ( EQUALS | SAFEEQUALS | EQUALS2 | NOT_EQUALS | NOT_EQUALS2 | LESS | LESS_OR_EQ | GREATER | GREATER_OR_EQ | SHIFT_LEFT | SHIFT_RIGHT | AMPERSAND | DOUBLE_AMPERSAND | PIPE | DOUBLE_PIPE | PLUS | MINUS | TILDA | ASTERISK | SLASH | PERCENT | SEMI | DOT | COMMA | LPAREN | RPAREN | QUESTION | EXCLAMATION | COLON | AT | DOLLAR | ARROW_UP | ABORT | ADD | AFTER | ALL | ALTER | ANALYZE | AND | AS | ASC | ATTACH | AUTOINCREMENT | BEFORE | BEGIN | BETWEEN | BY | CASCADE | CASE | CAST | CHECK | COLLATE | COLUMN | COMMIT | CONFLICT | CONSTRAINT | CREATE | CROSS | DATABASE | DEFAULT | DEFERRABLE | DEFERRED | DELETE | DESC | DETACH | DISTINCT | DROP | EACH | ELSE | END | ESCAPE | EXCEPT | EXCLUSIVE | EXISTS | EXPLAIN | FAIL | FOR | FOREIGN | FROM | GLOB | GROUP | HAVING | IF | IGNORE | IMMEDIATE | IN | INDEX | INDEXED | INITIALLY | INNER | INSERT | INSTEAD | INTERSECT | INTO | IS | ISNULL | JOIN | KEY | LEFT | LIKE | LIMIT | MATCH | NATURAL | NOT | NOTNULL | NULL | OF | OFFSET | ON | OR | ORDER | OUTER | PLAN | PRAGMA | PRIMARY | QUERY | RAISE | REFERENCES | REGEXP | REINDEX | RELEASE | RENAME | REPLACE | RESTRICT | ROLLBACK | ROW | SAVEPOINT | SELECT | SET | TABLE | TEMPORARY | TEMP | THEN | TO | TRANSACTION | TRIGGER | UNION | UNIQUE | UPDATE | USING | VACUUM | VALUES | VIEW | VIRTUAL | WHEN | WHERE | DIV | MOD | FULLTEXT | SPATIAL | KEY_BLOCK_SIZE | WITH | PARSER | XOR | AUTO_INCREMENT | COLUMN_FORMAT | FIXED | DYNAMIC | STORAGE | DISK | MEMORY | FULL | PARTIAL | SIMPLE | NO | ACTION | ENGINE | AVG_ROW_LENGTH | CHECKSUM | DATA | DIRECTORY | DELAY_KEY_WRITE | INSERT_METHOD | FIRST | LAST | MAX_ROWS | MIN_ROWS | PACK_KEYS | PASSWORD | ROW_FORMAT | COMPRESSED | REDUNDANT | COMPACT | TABLESPACE | CHARSET | BIT | TINYINT | SMALLINT | MEDIUMINT | INT | INTEGER | BIGINT | REAL | DOUBLE | FLOAT | DECIMAL | NUMERIC | DATE | TIME | TIMESTAMP | DATETIME | CHAR | VARCHAR | BINARY | VARBINARY | TINYBLOB | BLOB | MEDIUMBLOB | LONGBLOB | TINYTEXT | TEXT | MEDIUMTEXT | LONGTEXT | ENUM | CHARACTER | UNSIGNED | ZEROFILL | CONNECTION | COMMENT | GEOMETRY | POINT | LINESTRING | POLYGON | MULTIPOINT | MULTILINESTRING | MULTIPOLYGON | GEOMETRYCOLLECTION | TYPE | RIGHT | ALGORITHM | DEFINER | INVOKER | SQL | SECURITY | CASCADED | LOCAL | OPTION | DISTINCTROW | HIGH_PRIORITY | STRAIGHT_JOIN | SQL_SMALL_RESULT | SQL_BIG_RESULT | SQL_BUFFER_RESULT | SQL_CACHE | SQL_NO_CACHE | SQL_CALC_FOUND_ROWS | ROLLUP | SHARE | LOCK | MODE | FORCE | OJ | USE | INTERVAL | CURRENT_TIME | CURRENT_DATE | CURRENT_TIMESTAMP | UTC_TIMESTAMP | CURRENT_USER | YEAR | T_IDENT | T_QUOTED_IDENT | T_NSTRING | T_STRING | T_INTEGER | T_FLOAT | T_BLOB | T_BINARYNUM | WHITESPACE );"; }
        }

    }

 
    
}
