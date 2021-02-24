// $ANTLR 3.2 Sep 23, 2009 12:02:23 ..\\Plugin.mysql\\AntlrParser\\MySQL.g 2012-11-12 20:29:18

// The variable 'variable' is assigned but its value is never used.
#pragma warning disable 168, 219
// Unreachable code detected.
#pragma warning disable 162


    using System.Globalization;
    using DatAdmin;
    using Plugin.mysql;


using System;
using Antlr.Runtime;
using IList 		= System.Collections.IList;
using ArrayList 	= System.Collections.ArrayList;
using Stack 		= Antlr.Runtime.Collections.StackList;



using Antlr.Runtime.Tree;

public partial class MySQLParser : AntlrParser
{
    public static readonly string[] tokenNames = new string[] 
	{
        "<invalid>", 
		"<EOR>", 
		"<DOWN>", 
		"<UP>", 
		"F_NL", 
		"F_INC", 
		"F_DEC", 
		"COLUMN_DEF", 
		"CREATE", 
		"TEMPORARY", 
		"TABLE", 
		"LPAREN", 
		"COMMA", 
		"RPAREN", 
		"CONSTRAINT", 
		"CHECK", 
		"FULLTEXT", 
		"SPATIAL", 
		"UNIQUE", 
		"INDEX", 
		"KEY", 
		"PRIMARY", 
		"FOREIGN", 
		"USING", 
		"T_INTEGER", 
		"ASC", 
		"DESC", 
		"KEY_BLOCK_SIZE", 
		"EQUALS", 
		"WITH", 
		"PARSER", 
		"T_IDENT", 
		"T_QUOTED_IDENT", 
		"NOT", 
		"NULL", 
		"DEFAULT", 
		"ON", 
		"UPDATE", 
		"CURRENT_TIMESTAMP", 
		"AUTO_INCREMENT", 
		"COMMENT", 
		"T_STRING", 
		"COLUMN_FORMAT", 
		"FIXED", 
		"DYNAMIC", 
		"STORAGE", 
		"DISK", 
		"MEMORY", 
		"REFERENCES", 
		"MATCH", 
		"FULL", 
		"PARTIAL", 
		"SIMPLE", 
		"DELETE", 
		"RESTRICT", 
		"CASCADE", 
		"SET", 
		"NO", 
		"ACTION", 
		"UNSIGNED", 
		"ZEROFILL", 
		"CHARACTER", 
		"COLLATE", 
		"BIT", 
		"TINYINT", 
		"SMALLINT", 
		"MEDIUMINT", 
		"INT", 
		"INTEGER", 
		"BIGINT", 
		"REAL", 
		"DOUBLE", 
		"FLOAT", 
		"DECIMAL", 
		"NUMERIC", 
		"DATE", 
		"TIME", 
		"TIMESTAMP", 
		"DATETIME", 
		"YEAR", 
		"CHAR", 
		"VARCHAR", 
		"BINARY", 
		"VARBINARY", 
		"TINYBLOB", 
		"BLOB", 
		"MEDIUMBLOB", 
		"LONGBLOB", 
		"TINYTEXT", 
		"TEXT", 
		"MEDIUMTEXT", 
		"LONGTEXT", 
		"ENUM", 
		"GEOMETRY", 
		"POINT", 
		"LINESTRING", 
		"POLYGON", 
		"MULTIPOINT", 
		"MULTILINESTRING", 
		"MULTIPOLYGON", 
		"GEOMETRYCOLLECTION", 
		"ENGINE", 
		"TYPE", 
		"AVG_ROW_LENGTH", 
		"CHARSET", 
		"CHECKSUM", 
		"CONNECTION", 
		"DATA", 
		"DIRECTORY", 
		"DELAY_KEY_WRITE", 
		"INSERT_METHOD", 
		"FIRST", 
		"LAST", 
		"MAX_ROWS", 
		"MIN_ROWS", 
		"PACK_KEYS", 
		"PASSWORD", 
		"ROW_FORMAT", 
		"COMPRESSED", 
		"REDUNDANT", 
		"COMPACT", 
		"TABLESPACE", 
		"UNION", 
		"T_NSTRING", 
		"T_FLOAT", 
		"T_BINARYNUM", 
		"T_BLOB", 
		"DOT", 
		"OR", 
		"REPLACE", 
		"ALGORITHM", 
		"DEFINER", 
		"SQL", 
		"SECURITY", 
		"INVOKER", 
		"VIEW", 
		"AS", 
		"CASCADED", 
		"LOCAL", 
		"OPTION", 
		"ALL", 
		"ORDER", 
		"BY", 
		"LIMIT", 
		"OFFSET", 
		"FOR", 
		"LOCK", 
		"IN", 
		"SHARE", 
		"MODE", 
		"SELECT", 
		"DISTINCT", 
		"DISTINCTROW", 
		"HIGH_PRIORITY", 
		"STRAIGHT_JOIN", 
		"SQL_SMALL_RESULT", 
		"SQL_BIG_RESULT", 
		"SQL_BUFFER_RESULT", 
		"SQL_CACHE", 
		"SQL_NO_CACHE", 
		"SQL_CALC_FOUND_ROWS", 
		"FROM", 
		"WHERE", 
		"GROUP", 
		"ROLLUP", 
		"HAVING", 
		"ASTERISK", 
		"OJ", 
		"LEFT", 
		"OUTER", 
		"JOIN", 
		"INNER", 
		"CROSS", 
		"RIGHT", 
		"NATURAL", 
		"USE", 
		"IGNORE", 
		"FORCE", 
		"CURRENT_USER", 
		"AT", 
		"CURRENT_DATE", 
		"CASE", 
		"WHEN", 
		"THEN", 
		"ELSE", 
		"END", 
		"UTC_TIMESTAMP", 
		"INTERVAL", 
		"EXCLAMATION", 
		"MINUS", 
		"TILDA", 
		"ARROW_UP", 
		"SLASH", 
		"PERCENT", 
		"DIV", 
		"MOD", 
		"PLUS", 
		"SHIFT_LEFT", 
		"SHIFT_RIGHT", 
		"AMPERSAND", 
		"PIPE", 
		"SAFEEQUALS", 
		"GREATER_OR_EQ", 
		"GREATER", 
		"LESS_OR_EQ", 
		"LESS", 
		"NOT_EQUALS2", 
		"NOT_EQUALS", 
		"IS", 
		"LIKE", 
		"REGEXP", 
		"BETWEEN", 
		"AND", 
		"DOUBLE_AMPERSAND", 
		"XOR", 
		"DOUBLE_PIPE", 
		"EQUALS2", 
		"SEMI", 
		"QUESTION", 
		"COLON", 
		"DOLLAR", 
		"A", 
		"B", 
		"C", 
		"D", 
		"E", 
		"F", 
		"G", 
		"H", 
		"I", 
		"J", 
		"K", 
		"L", 
		"M", 
		"N", 
		"O", 
		"P", 
		"Q", 
		"R", 
		"S", 
		"T", 
		"U", 
		"V", 
		"W", 
		"X", 
		"Y", 
		"Z", 
		"ABORT", 
		"ADD", 
		"AFTER", 
		"ALTER", 
		"ANALYZE", 
		"ATTACH", 
		"AUTOINCREMENT", 
		"BEFORE", 
		"BEGIN", 
		"CAST", 
		"COLUMN", 
		"COMMIT", 
		"CONFLICT", 
		"DATABASE", 
		"DEFERRABLE", 
		"DEFERRED", 
		"DETACH", 
		"DROP", 
		"EACH", 
		"ESCAPE", 
		"EXCEPT", 
		"EXCLUSIVE", 
		"EXISTS", 
		"EXPLAIN", 
		"FAIL", 
		"GLOB", 
		"IF", 
		"IMMEDIATE", 
		"INDEXED", 
		"INITIALLY", 
		"INSERT", 
		"INSTEAD", 
		"INTERSECT", 
		"INTO", 
		"ISNULL", 
		"NOTNULL", 
		"OF", 
		"PLAN", 
		"PRAGMA", 
		"QUERY", 
		"RAISE", 
		"REINDEX", 
		"RELEASE", 
		"RENAME", 
		"ROLLBACK", 
		"ROW", 
		"SAVEPOINT", 
		"TEMP", 
		"TO", 
		"TRANSACTION", 
		"TRIGGER", 
		"VACUUM", 
		"VALUES", 
		"VIRTUAL", 
		"CURRENT_TIME", 
		"ID_START", 
		"FLOAT_EXP", 
		"T_COMMENT", 
		"LINE_COMMENT", 
		"WHITESPACE"
    };

    public const int GEOMETRYCOLLECTION = 100;
    public const int CASCADED = 137;
    public const int ROW = 292;
    public const int INSERT_METHOD = 110;
    public const int MOD = 195;
    public const int FIXED = 43;
    public const int T_IDENT = 31;
    public const int NOT = 33;
    public const int MULTILINESTRING = 98;
    public const int EXCEPT = 267;
    public const int F_INC = 5;
    public const int EOF = -1;
    public const int FOREIGN = 22;
    public const int CHARACTER = 61;
    public const int ACTION = 58;
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
    public const int TRANSACTION = 296;
    public const int O = 235;
    public const int KEY = 20;
    public const int T_BINARYNUM = 125;
    public const int TEMP = 294;
    public const int H = 228;
    public const int I = 229;
    public const int J = 230;
    public const int ELSE = 184;
    public const int K = 231;
    public const int U = 241;
    public const int T = 240;
    public const int WHITESPACE = 306;
    public const int W = 243;
    public const int MEDIUMTEXT = 90;
    public const int V = 242;
    public const int DELAY_KEY_WRITE = 109;
    public const int DEFINER = 131;
    public const int Q = 237;
    public const int INT = 67;
    public const int P = 236;
    public const int S = 239;
    public const int R = 238;
    public const int ROLLBACK = 291;
    public const int FAIL = 271;
    public const int Y = 245;
    public const int RESTRICT = 54;
    public const int X = 244;
    public const int Z = 246;
    public const int TABLESPACE = 121;
    public const int REAL = 70;
    public const int INTERSECT = 279;
    public const int GROUP = 163;
    public const int PLAN = 284;
    public const int MIN_ROWS = 114;
    public const int AVG_ROW_LENGTH = 103;
    public const int SQL_CACHE = 158;
    public const int END = 185;
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
    public const int ABORT = 247;
    public const int COMMENT = 40;
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
    public const int TINYTEXT = 88;
    public const int T_FLOAT = 124;
    public const int ON = 36;
    public const int MATCH = 49;
    public const int T_NSTRING = 123;
    public const int PRIMARY = 21;
    public const int DELETE = 53;
    public const int OJ = 167;
    public const int NUMERIC = 74;
    public const int LOCAL = 138;
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
    public const int TEMPORARY = 9;
    public const int PARSER = 30;
    public const int DISTINCT = 151;
    public const int TIMESTAMP = 77;
    public const int CURRENT_DATE = 180;
    public const int SIMPLE = 52;
    public const int DISTINCTROW = 152;
    public const int DOLLAR = 220;
    public const int COLUMN_DEF = 7;
    public const int WHERE = 162;
    public const int INNER = 171;
    public const int COMPRESSED = 118;
    public const int YEAR = 79;
    public const int UTC_TIMESTAMP = 186;
    public const int ORDER = 141;
    public const int LIMIT = 143;
    public const int PRAGMA = 285;
    public const int UPDATE = 37;
    public const int DEFERRED = 262;
    public const int FOR = 145;
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
    public const int REFERENCES = 48;
    public const int COMMA = 12;
    public const int IS = 208;
    public const int REDUNDANT = 119;
    public const int ALL = 140;
    public const int HIGH_PRIORITY = 153;
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
    public const int PERCENT = 193;
    public const int VARBINARY = 83;
    public const int AUTOINCREMENT = 253;
    public const int NOT_EQUALS2 = 206;
    public const int ROW_FORMAT = 117;
    public const int DATETIME = 78;
    public const int DEFAULT = 35;
    public const int VALUES = 299;
    public const int F_DEC = 6;
    public const int BEFORE = 254;
    public const int AFTER = 249;
    public const int INSTEAD = 278;
    public const int JOIN = 170;
    public const int INDEXED = 275;
    public const int T_QUOTED_IDENT = 32;
    public const int QUESTION = 218;
    public const int DISK = 46;
    public const int IMMEDIATE = 274;
    public const int BINARY = 82;
    public const int DESC = 26;
    public const int DATE = 75;
    public const int ID_START = 302;
    public const int DATA = 107;
    public const int SMALLINT = 65;

    // delegates
    // delegators



        public MySQLParser(ITokenStream input)
    		: this(input, new RecognizerSharedState()) {
        }

        public MySQLParser(ITokenStream input, RecognizerSharedState state)
    		: base(input, state) {
            InitializeCyclicDFAs();

             
        }
        
    protected ITreeAdaptor adaptor = new CommonTreeAdaptor();

    public ITreeAdaptor TreeAdaptor
    {
        get { return this.adaptor; }
        set {
    	this.adaptor = value;
    	}
    }

    override public string[] TokenNames {
		get { return MySQLParser.tokenNames; }
    }

    override public string GrammarFileName {
		get { return "..\\Plugin.mysql\\AntlrParser\\MySQL.g"; }
    }


    public class create_table_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "create_table"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:25:1: create_table[TableStructure table, bool loadName] : CREATE ( TEMPORARY )? TABLE name= tbl_name LPAREN create_definition[table] ( COMMA create_definition[table] )* RPAREN ( table_options )? ;
    public MySQLParser.create_table_return create_table(TableStructure table, bool loadName) // throws RecognitionException [1]
    {   
        MySQLParser.create_table_return retval = new MySQLParser.create_table_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken CREATE1 = null;
        IToken TEMPORARY2 = null;
        IToken TABLE3 = null;
        IToken LPAREN4 = null;
        IToken COMMA6 = null;
        IToken RPAREN8 = null;
        MySQLParser.tbl_name_return name = default(MySQLParser.tbl_name_return);

        MySQLParser.create_definition_return create_definition5 = default(MySQLParser.create_definition_return);

        MySQLParser.create_definition_return create_definition7 = default(MySQLParser.create_definition_return);

        MySQLParser.table_options_return table_options9 = default(MySQLParser.table_options_return);


        object CREATE1_tree=null;
        object TEMPORARY2_tree=null;
        object TABLE3_tree=null;
        object LPAREN4_tree=null;
        object COMMA6_tree=null;
        object RPAREN8_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:25:50: ( CREATE ( TEMPORARY )? TABLE name= tbl_name LPAREN create_definition[table] ( COMMA create_definition[table] )* RPAREN ( table_options )? )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:26:3: CREATE ( TEMPORARY )? TABLE name= tbl_name LPAREN create_definition[table] ( COMMA create_definition[table] )* RPAREN ( table_options )?
            {
            	root_0 = (object)adaptor.GetNilNode();

            	CREATE1=(IToken)Match(input,CREATE,FOLLOW_CREATE_in_create_table77); 
            		CREATE1_tree = (object)adaptor.Create(CREATE1);
            		adaptor.AddChild(root_0, CREATE1_tree);

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:26:10: ( TEMPORARY )?
            	int alt1 = 2;
            	int LA1_0 = input.LA(1);

            	if ( (LA1_0 == TEMPORARY) )
            	{
            	    alt1 = 1;
            	}
            	switch (alt1) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:26:10: TEMPORARY
            	        {
            	        	TEMPORARY2=(IToken)Match(input,TEMPORARY,FOLLOW_TEMPORARY_in_create_table79); 
            	        		TEMPORARY2_tree = (object)adaptor.Create(TEMPORARY2);
            	        		adaptor.AddChild(root_0, TEMPORARY2_tree);


            	        }
            	        break;

            	}

            	TABLE3=(IToken)Match(input,TABLE,FOLLOW_TABLE_in_create_table82); 
            		TABLE3_tree = (object)adaptor.Create(TABLE3);
            		adaptor.AddChild(root_0, TABLE3_tree);

            	PushFollow(FOLLOW_tbl_name_in_create_table86);
            	name = tbl_name();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, name.Tree);
            	if (loadName) table.FullName=new NameWithSchema(((name != null) ? input.ToString((IToken)(name.Start),(IToken)(name.Stop)) : null));
            	LPAREN4=(IToken)Match(input,LPAREN,FOLLOW_LPAREN_in_create_table98); 
            		LPAREN4_tree = (object)adaptor.Create(LPAREN4);
            		adaptor.AddChild(root_0, LPAREN4_tree);

            	PushFollow(FOLLOW_create_definition_in_create_table100);
            	create_definition5 = create_definition(table);
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, create_definition5.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:27:41: ( COMMA create_definition[table] )*
            	do 
            	{
            	    int alt2 = 2;
            	    int LA2_0 = input.LA(1);

            	    if ( (LA2_0 == COMMA) )
            	    {
            	        alt2 = 1;
            	    }


            	    switch (alt2) 
            		{
            			case 1 :
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:27:43: COMMA create_definition[table]
            			    {
            			    	COMMA6=(IToken)Match(input,COMMA,FOLLOW_COMMA_in_create_table105); 
            			    		COMMA6_tree = (object)adaptor.Create(COMMA6);
            			    		adaptor.AddChild(root_0, COMMA6_tree);

            			    	PushFollow(FOLLOW_create_definition_in_create_table107);
            			    	create_definition7 = create_definition(table);
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, create_definition7.Tree);

            			    }
            			    break;

            			default:
            			    goto loop2;
            	    }
            	} while (true);

            	loop2:
            		;	// Stops C# compiler whining that label 'loop2' has no statements

            	RPAREN8=(IToken)Match(input,RPAREN,FOLLOW_RPAREN_in_create_table113); 
            		RPAREN8_tree = (object)adaptor.Create(RPAREN8);
            		adaptor.AddChild(root_0, RPAREN8_tree);

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:28:9: ( table_options )?
            	int alt3 = 2;
            	int LA3_0 = input.LA(1);

            	if ( (LA3_0 == INDEX || LA3_0 == KEY_BLOCK_SIZE || LA3_0 == DEFAULT || (LA3_0 >= AUTO_INCREMENT && LA3_0 <= COMMENT) || (LA3_0 >= CHARACTER && LA3_0 <= COLLATE) || (LA3_0 >= ENGINE && LA3_0 <= DATA) || (LA3_0 >= DELAY_KEY_WRITE && LA3_0 <= INSERT_METHOD) || (LA3_0 >= MAX_ROWS && LA3_0 <= ROW_FORMAT) || (LA3_0 >= TABLESPACE && LA3_0 <= UNION)) )
            	{
            	    alt3 = 1;
            	}
            	switch (alt3) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:28:9: table_options
            	        {
            	        	PushFollow(FOLLOW_table_options_in_create_table123);
            	        	table_options9 = table_options();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, table_options9.Tree);

            	        }
            	        break;

            	}


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "create_table"

    public class create_view_0_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "create_view_0"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:30:1: create_view_0 : create_view ;
    public MySQLParser.create_view_0_return create_view_0() // throws RecognitionException [1]
    {   
        MySQLParser.create_view_0_return retval = new MySQLParser.create_view_0_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        MySQLParser.create_view_return create_view10 = default(MySQLParser.create_view_return);



        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:30:14: ( create_view )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:31:5: create_view
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_create_view_in_create_view_0135);
            	create_view10 = create_view();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, create_view10.Tree);

            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "create_view_0"

    public class create_definition_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "create_definition"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:33:1: create_definition[TableStructure table] : (name= col_name column_definition[col] -> ^( COLUMN_DEF col_name column_definition ) | primary_key[table] | index_definition[table] | foreign_key[table] | ( CONSTRAINT ( id )? )? CHECK LPAREN expr RPAREN );
    public MySQLParser.create_definition_return create_definition(TableStructure table) // throws RecognitionException [1]
    {   
        MySQLParser.create_definition_return retval = new MySQLParser.create_definition_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken CONSTRAINT15 = null;
        IToken CHECK17 = null;
        IToken LPAREN18 = null;
        IToken RPAREN20 = null;
        MySQLParser.col_name_return name = default(MySQLParser.col_name_return);

        MySQLParser.column_definition_return column_definition11 = default(MySQLParser.column_definition_return);

        MySQLParser.primary_key_return primary_key12 = default(MySQLParser.primary_key_return);

        MySQLParser.index_definition_return index_definition13 = default(MySQLParser.index_definition_return);

        MySQLParser.foreign_key_return foreign_key14 = default(MySQLParser.foreign_key_return);

        MySQLParser.id_return id16 = default(MySQLParser.id_return);

        MySQLParser.expr_return expr19 = default(MySQLParser.expr_return);


        object CONSTRAINT15_tree=null;
        object CHECK17_tree=null;
        object LPAREN18_tree=null;
        object RPAREN20_tree=null;
        RewriteRuleSubtreeStream stream_col_name = new RewriteRuleSubtreeStream(adaptor,"rule col_name");
        RewriteRuleSubtreeStream stream_column_definition = new RewriteRuleSubtreeStream(adaptor,"rule column_definition");
        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:33:40: (name= col_name column_definition[col] -> ^( COLUMN_DEF col_name column_definition ) | primary_key[table] | index_definition[table] | foreign_key[table] | ( CONSTRAINT ( id )? )? CHECK LPAREN expr RPAREN )
            int alt6 = 5;
            switch ( input.LA(1) ) 
            {
            case T_IDENT:
            case T_QUOTED_IDENT:
            	{
                alt6 = 1;
                }
                break;
            case CONSTRAINT:
            	{
                switch ( input.LA(2) ) 
                {
                case T_IDENT:
                case T_QUOTED_IDENT:
                	{
                    switch ( input.LA(3) ) 
                    {
                    case CHECK:
                    	{
                        alt6 = 5;
                        }
                        break;
                    case FOREIGN:
                    	{
                        alt6 = 4;
                        }
                        break;
                    case PRIMARY:
                    	{
                        alt6 = 2;
                        }
                        break;
                    case FULLTEXT:
                    case SPATIAL:
                    case UNIQUE:
                    case INDEX:
                    case KEY:
                    	{
                        alt6 = 3;
                        }
                        break;
                    	default:
                    	    NoViableAltException nvae_d6s7 =
                    	        new NoViableAltException("", 6, 7, input);

                    	    throw nvae_d6s7;
                    }

                    }
                    break;
                case PRIMARY:
                	{
                    alt6 = 2;
                    }
                    break;
                case FOREIGN:
                	{
                    alt6 = 4;
                    }
                    break;
                case FULLTEXT:
                case SPATIAL:
                case UNIQUE:
                case INDEX:
                case KEY:
                	{
                    alt6 = 3;
                    }
                    break;
                case CHECK:
                	{
                    alt6 = 5;
                    }
                    break;
                	default:
                	    NoViableAltException nvae_d6s2 =
                	        new NoViableAltException("", 6, 2, input);

                	    throw nvae_d6s2;
                }

                }
                break;
            case PRIMARY:
            	{
                alt6 = 2;
                }
                break;
            case FULLTEXT:
            case SPATIAL:
            case UNIQUE:
            case INDEX:
            case KEY:
            	{
                alt6 = 3;
                }
                break;
            case FOREIGN:
            	{
                alt6 = 4;
                }
                break;
            case CHECK:
            	{
                alt6 = 5;
                }
                break;
            	default:
            	    NoViableAltException nvae_d6s0 =
            	        new NoViableAltException("", 6, 0, input);

            	    throw nvae_d6s0;
            }

            switch (alt6) 
            {
                case 1 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:34:5: name= col_name column_definition[col]
                    {
                    	PushFollow(FOLLOW_col_name_in_create_definition149);
                    	name = col_name();
                    	state.followingStackPointer--;

                    	stream_col_name.Add(name.Tree);
                    	var col = new ColumnStructure(); table._Columns.Add(col); col.ColumnName=UnquoteName(((name != null) ? input.ToString((IToken)(name.Start),(IToken)(name.Stop)) : null));
                    	PushFollow(FOLLOW_column_definition_in_create_definition165);
                    	column_definition11 = column_definition(col);
                    	state.followingStackPointer--;

                    	stream_column_definition.Add(column_definition11.Tree);


                    	// AST REWRITE
                    	// elements:          column_definition, col_name
                    	// token labels:      
                    	// rule labels:       retval
                    	// token list labels: 
                    	// rule list labels:  
                    	// wildcard labels: 
                    	retval.Tree = root_0;
                    	RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval!=null ? retval.Tree : null);

                    	root_0 = (object)adaptor.GetNilNode();
                    	// 36:28: -> ^( COLUMN_DEF col_name column_definition )
                    	{
                    	    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:36:31: ^( COLUMN_DEF col_name column_definition )
                    	    {
                    	    object root_1 = (object)adaptor.GetNilNode();
                    	    root_1 = (object)adaptor.BecomeRoot((object)adaptor.Create(COLUMN_DEF, "COLUMN_DEF"), root_1);

                    	    adaptor.AddChild(root_1, stream_col_name.NextTree());
                    	    adaptor.AddChild(root_1, stream_column_definition.NextTree());

                    	    adaptor.AddChild(root_0, root_1);
                    	    }

                    	}

                    	retval.Tree = root_0;retval.Tree = root_0;
                    }
                    break;
                case 2 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:37:5: primary_key[table]
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_primary_key_in_create_definition182);
                    	primary_key12 = primary_key(table);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, primary_key12.Tree);

                    }
                    break;
                case 3 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:38:5: index_definition[table]
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_index_definition_in_create_definition189);
                    	index_definition13 = index_definition(table);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, index_definition13.Tree);

                    }
                    break;
                case 4 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:39:5: foreign_key[table]
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_foreign_key_in_create_definition196);
                    	foreign_key14 = foreign_key(table);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, foreign_key14.Tree);

                    }
                    break;
                case 5 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:40:5: ( CONSTRAINT ( id )? )? CHECK LPAREN expr RPAREN
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:40:5: ( CONSTRAINT ( id )? )?
                    	int alt5 = 2;
                    	int LA5_0 = input.LA(1);

                    	if ( (LA5_0 == CONSTRAINT) )
                    	{
                    	    alt5 = 1;
                    	}
                    	switch (alt5) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:40:6: CONSTRAINT ( id )?
                    	        {
                    	        	CONSTRAINT15=(IToken)Match(input,CONSTRAINT,FOLLOW_CONSTRAINT_in_create_definition204); 
                    	        		CONSTRAINT15_tree = (object)adaptor.Create(CONSTRAINT15);
                    	        		adaptor.AddChild(root_0, CONSTRAINT15_tree);

                    	        	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:40:17: ( id )?
                    	        	int alt4 = 2;
                    	        	int LA4_0 = input.LA(1);

                    	        	if ( ((LA4_0 >= T_IDENT && LA4_0 <= T_QUOTED_IDENT)) )
                    	        	{
                    	        	    alt4 = 1;
                    	        	}
                    	        	switch (alt4) 
                    	        	{
                    	        	    case 1 :
                    	        	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:40:17: id
                    	        	        {
                    	        	        	PushFollow(FOLLOW_id_in_create_definition206);
                    	        	        	id16 = id();
                    	        	        	state.followingStackPointer--;

                    	        	        	adaptor.AddChild(root_0, id16.Tree);

                    	        	        }
                    	        	        break;

                    	        	}


                    	        }
                    	        break;

                    	}

                    	CHECK17=(IToken)Match(input,CHECK,FOLLOW_CHECK_in_create_definition211); 
                    		CHECK17_tree = (object)adaptor.Create(CHECK17);
                    		adaptor.AddChild(root_0, CHECK17_tree);

                    	LPAREN18=(IToken)Match(input,LPAREN,FOLLOW_LPAREN_in_create_definition213); 
                    		LPAREN18_tree = (object)adaptor.Create(LPAREN18);
                    		adaptor.AddChild(root_0, LPAREN18_tree);

                    	PushFollow(FOLLOW_expr_in_create_definition215);
                    	expr19 = expr();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, expr19.Tree);
                    	RPAREN20=(IToken)Match(input,RPAREN,FOLLOW_RPAREN_in_create_definition217); 
                    		RPAREN20_tree = (object)adaptor.Create(RPAREN20);
                    		adaptor.AddChild(root_0, RPAREN20_tree);


                    }
                    break;

            }
            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "create_definition"

    public class index_definition_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "index_definition"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:47:1: index_definition[TableStructure table] : ( CONSTRAINT ( id )? )? ( FULLTEXT | SPATIAL )? ( ( UNIQUE ( INDEX | KEY )? ) | ( INDEX | KEY ) ) (name= index_name )? ( index_type[index] )? index_cols[index] ( index_option[index] )* ;
    public MySQLParser.index_definition_return index_definition(TableStructure table) // throws RecognitionException [1]
    {   
        MySQLParser.index_definition_return retval = new MySQLParser.index_definition_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken CONSTRAINT21 = null;
        IToken set23 = null;
        IToken UNIQUE24 = null;
        IToken set25 = null;
        IToken set26 = null;
        MySQLParser.index_name_return name = default(MySQLParser.index_name_return);

        MySQLParser.id_return id22 = default(MySQLParser.id_return);

        MySQLParser.index_type_return index_type27 = default(MySQLParser.index_type_return);

        MySQLParser.index_cols_return index_cols28 = default(MySQLParser.index_cols_return);

        MySQLParser.index_option_return index_option29 = default(MySQLParser.index_option_return);


        object CONSTRAINT21_tree=null;
        object set23_tree=null;
        object UNIQUE24_tree=null;
        object set25_tree=null;
        object set26_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:47:39: ( ( CONSTRAINT ( id )? )? ( FULLTEXT | SPATIAL )? ( ( UNIQUE ( INDEX | KEY )? ) | ( INDEX | KEY ) ) (name= index_name )? ( index_type[index] )? index_cols[index] ( index_option[index] )* )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:48:5: ( CONSTRAINT ( id )? )? ( FULLTEXT | SPATIAL )? ( ( UNIQUE ( INDEX | KEY )? ) | ( INDEX | KEY ) ) (name= index_name )? ( index_type[index] )? index_cols[index] ( index_option[index] )*
            {
            	root_0 = (object)adaptor.GetNilNode();

            	var index=new IndexConstraint();table._Constraints.Add(index);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:49:5: ( CONSTRAINT ( id )? )?
            	int alt8 = 2;
            	int LA8_0 = input.LA(1);

            	if ( (LA8_0 == CONSTRAINT) )
            	{
            	    alt8 = 1;
            	}
            	switch (alt8) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:49:6: CONSTRAINT ( id )?
            	        {
            	        	CONSTRAINT21=(IToken)Match(input,CONSTRAINT,FOLLOW_CONSTRAINT_in_index_definition252); 
            	        		CONSTRAINT21_tree = (object)adaptor.Create(CONSTRAINT21);
            	        		adaptor.AddChild(root_0, CONSTRAINT21_tree);

            	        	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:49:17: ( id )?
            	        	int alt7 = 2;
            	        	int LA7_0 = input.LA(1);

            	        	if ( ((LA7_0 >= T_IDENT && LA7_0 <= T_QUOTED_IDENT)) )
            	        	{
            	        	    alt7 = 1;
            	        	}
            	        	switch (alt7) 
            	        	{
            	        	    case 1 :
            	        	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:49:17: id
            	        	        {
            	        	        	PushFollow(FOLLOW_id_in_index_definition254);
            	        	        	id22 = id();
            	        	        	state.followingStackPointer--;

            	        	        	adaptor.AddChild(root_0, id22.Tree);

            	        	        }
            	        	        break;

            	        	}


            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:49:23: ( FULLTEXT | SPATIAL )?
            	int alt9 = 2;
            	int LA9_0 = input.LA(1);

            	if ( ((LA9_0 >= FULLTEXT && LA9_0 <= SPATIAL)) )
            	{
            	    alt9 = 1;
            	}
            	switch (alt9) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:
            	        {
            	        	set23 = (IToken)input.LT(1);
            	        	if ( (input.LA(1) >= FULLTEXT && input.LA(1) <= SPATIAL) ) 
            	        	{
            	        	    input.Consume();
            	        	    adaptor.AddChild(root_0, (object)adaptor.Create(set23));
            	        	    state.errorRecovery = false;
            	        	}
            	        	else 
            	        	{
            	        	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	        	    throw mse;
            	        	}


            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:49:43: ( ( UNIQUE ( INDEX | KEY )? ) | ( INDEX | KEY ) )
            	int alt11 = 2;
            	int LA11_0 = input.LA(1);

            	if ( (LA11_0 == UNIQUE) )
            	{
            	    alt11 = 1;
            	}
            	else if ( ((LA11_0 >= INDEX && LA11_0 <= KEY)) )
            	{
            	    alt11 = 2;
            	}
            	else 
            	{
            	    NoViableAltException nvae_d11s0 =
            	        new NoViableAltException("", 11, 0, input);

            	    throw nvae_d11s0;
            	}
            	switch (alt11) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:49:44: ( UNIQUE ( INDEX | KEY )? )
            	        {
            	        	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:49:44: ( UNIQUE ( INDEX | KEY )? )
            	        	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:49:45: UNIQUE ( INDEX | KEY )?
            	        	{
            	        		UNIQUE24=(IToken)Match(input,UNIQUE,FOLLOW_UNIQUE_in_index_definition268); 
            	        			UNIQUE24_tree = (object)adaptor.Create(UNIQUE24);
            	        			adaptor.AddChild(root_0, UNIQUE24_tree);

            	        		index.IsUnique=true;
            	        		// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:49:75: ( INDEX | KEY )?
            	        		int alt10 = 2;
            	        		int LA10_0 = input.LA(1);

            	        		if ( ((LA10_0 >= INDEX && LA10_0 <= KEY)) )
            	        		{
            	        		    alt10 = 1;
            	        		}
            	        		switch (alt10) 
            	        		{
            	        		    case 1 :
            	        		        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:
            	        		        {
            	        		        	set25 = (IToken)input.LT(1);
            	        		        	if ( (input.LA(1) >= INDEX && input.LA(1) <= KEY) ) 
            	        		        	{
            	        		        	    input.Consume();
            	        		        	    adaptor.AddChild(root_0, (object)adaptor.Create(set25));
            	        		        	    state.errorRecovery = false;
            	        		        	}
            	        		        	else 
            	        		        	{
            	        		        	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	        		        	    throw mse;
            	        		        	}


            	        		        }
            	        		        break;

            	        		}


            	        	}


            	        }
            	        break;
            	    case 2 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:49:91: ( INDEX | KEY )
            	        {
            	        	set26 = (IToken)input.LT(1);
            	        	if ( (input.LA(1) >= INDEX && input.LA(1) <= KEY) ) 
            	        	{
            	        	    input.Consume();
            	        	    adaptor.AddChild(root_0, (object)adaptor.Create(set26));
            	        	    state.errorRecovery = false;
            	        	}
            	        	else 
            	        	{
            	        	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	        	    throw mse;
            	        	}


            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:50:5: (name= index_name )?
            	int alt12 = 2;
            	int LA12_0 = input.LA(1);

            	if ( ((LA12_0 >= T_IDENT && LA12_0 <= T_QUOTED_IDENT)) )
            	{
            	    alt12 = 1;
            	}
            	switch (alt12) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:50:6: name= index_name
            	        {
            	        	PushFollow(FOLLOW_index_name_in_index_definition296);
            	        	name = index_name();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, name.Tree);
            	        	index.Name=UnquoteName(((name != null) ? input.ToString((IToken)(name.Start),(IToken)(name.Stop)) : null));

            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:51:5: ( index_type[index] )?
            	int alt13 = 2;
            	int LA13_0 = input.LA(1);

            	if ( (LA13_0 == USING) )
            	{
            	    alt13 = 1;
            	}
            	switch (alt13) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:51:5: index_type[index]
            	        {
            	        	PushFollow(FOLLOW_index_type_in_index_definition307);
            	        	index_type27 = index_type(index);
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, index_type27.Tree);

            	        }
            	        break;

            	}

            	PushFollow(FOLLOW_index_cols_in_index_definition311);
            	index_cols28 = index_cols(index);
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, index_cols28.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:51:42: ( index_option[index] )*
            	do 
            	{
            	    int alt14 = 2;
            	    int LA14_0 = input.LA(1);

            	    if ( (LA14_0 == USING || LA14_0 == KEY_BLOCK_SIZE || LA14_0 == WITH) )
            	    {
            	        alt14 = 1;
            	    }


            	    switch (alt14) 
            		{
            			case 1 :
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:51:42: index_option[index]
            			    {
            			    	PushFollow(FOLLOW_index_option_in_index_definition314);
            			    	index_option29 = index_option(index);
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, index_option29.Tree);

            			    }
            			    break;

            			default:
            			    goto loop14;
            	    }
            	} while (true);

            	loop14:
            		;	// Stops C# compiler whining that label 'loop14' has no statements


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "index_definition"

    public class primary_key_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "primary_key"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:53:1: primary_key[TableStructure table] : ( CONSTRAINT ( id )? )? PRIMARY KEY ( index_type[pk] )? index_cols[pk] ( index_option[pk] )* ;
    public MySQLParser.primary_key_return primary_key(TableStructure table) // throws RecognitionException [1]
    {   
        MySQLParser.primary_key_return retval = new MySQLParser.primary_key_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken CONSTRAINT30 = null;
        IToken PRIMARY32 = null;
        IToken KEY33 = null;
        MySQLParser.id_return id31 = default(MySQLParser.id_return);

        MySQLParser.index_type_return index_type34 = default(MySQLParser.index_type_return);

        MySQLParser.index_cols_return index_cols35 = default(MySQLParser.index_cols_return);

        MySQLParser.index_option_return index_option36 = default(MySQLParser.index_option_return);


        object CONSTRAINT30_tree=null;
        object PRIMARY32_tree=null;
        object KEY33_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:53:34: ( ( CONSTRAINT ( id )? )? PRIMARY KEY ( index_type[pk] )? index_cols[pk] ( index_option[pk] )* )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:54:2: ( CONSTRAINT ( id )? )? PRIMARY KEY ( index_type[pk] )? index_cols[pk] ( index_option[pk] )*
            {
            	root_0 = (object)adaptor.GetNilNode();

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:54:2: ( CONSTRAINT ( id )? )?
            	int alt16 = 2;
            	int LA16_0 = input.LA(1);

            	if ( (LA16_0 == CONSTRAINT) )
            	{
            	    alt16 = 1;
            	}
            	switch (alt16) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:54:3: CONSTRAINT ( id )?
            	        {
            	        	CONSTRAINT30=(IToken)Match(input,CONSTRAINT,FOLLOW_CONSTRAINT_in_primary_key326); 
            	        		CONSTRAINT30_tree = (object)adaptor.Create(CONSTRAINT30);
            	        		adaptor.AddChild(root_0, CONSTRAINT30_tree);

            	        	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:54:14: ( id )?
            	        	int alt15 = 2;
            	        	int LA15_0 = input.LA(1);

            	        	if ( ((LA15_0 >= T_IDENT && LA15_0 <= T_QUOTED_IDENT)) )
            	        	{
            	        	    alt15 = 1;
            	        	}
            	        	switch (alt15) 
            	        	{
            	        	    case 1 :
            	        	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:54:14: id
            	        	        {
            	        	        	PushFollow(FOLLOW_id_in_primary_key328);
            	        	        	id31 = id();
            	        	        	state.followingStackPointer--;

            	        	        	adaptor.AddChild(root_0, id31.Tree);

            	        	        }
            	        	        break;

            	        	}


            	        }
            	        break;

            	}

            	PRIMARY32=(IToken)Match(input,PRIMARY,FOLLOW_PRIMARY_in_primary_key340); 
            		PRIMARY32_tree = (object)adaptor.Create(PRIMARY32);
            		adaptor.AddChild(root_0, PRIMARY32_tree);

            	KEY33=(IToken)Match(input,KEY,FOLLOW_KEY_in_primary_key342); 
            		KEY33_tree = (object)adaptor.Create(KEY33);
            		adaptor.AddChild(root_0, KEY33_tree);

            	var pk=new PrimaryKey();table._Constraints.Add(pk);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:56:5: ( index_type[pk] )?
            	int alt17 = 2;
            	int LA17_0 = input.LA(1);

            	if ( (LA17_0 == USING) )
            	{
            	    alt17 = 1;
            	}
            	switch (alt17) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:56:5: index_type[pk]
            	        {
            	        	PushFollow(FOLLOW_index_type_in_primary_key350);
            	        	index_type34 = index_type(pk);
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, index_type34.Tree);

            	        }
            	        break;

            	}

            	PushFollow(FOLLOW_index_cols_in_primary_key354);
            	index_cols35 = index_cols(pk);
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, index_cols35.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:56:36: ( index_option[pk] )*
            	do 
            	{
            	    int alt18 = 2;
            	    int LA18_0 = input.LA(1);

            	    if ( (LA18_0 == USING || LA18_0 == KEY_BLOCK_SIZE || LA18_0 == WITH) )
            	    {
            	        alt18 = 1;
            	    }


            	    switch (alt18) 
            		{
            			case 1 :
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:56:36: index_option[pk]
            			    {
            			    	PushFollow(FOLLOW_index_option_in_primary_key357);
            			    	index_option36 = index_option(pk);
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, index_option36.Tree);

            			    }
            			    break;

            			default:
            			    goto loop18;
            	    }
            	} while (true);

            	loop18:
            		;	// Stops C# compiler whining that label 'loop18' has no statements


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "primary_key"

    public class foreign_key_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "foreign_key"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:58:1: foreign_key[TableStructure table] : ( CONSTRAINT (name= id )? )? FOREIGN KEY ( index_name )? index_cols[fk] reference_definition[fk] ;
    public MySQLParser.foreign_key_return foreign_key(TableStructure table) // throws RecognitionException [1]
    {   
        MySQLParser.foreign_key_return retval = new MySQLParser.foreign_key_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken CONSTRAINT37 = null;
        IToken FOREIGN38 = null;
        IToken KEY39 = null;
        MySQLParser.id_return name = default(MySQLParser.id_return);

        MySQLParser.index_name_return index_name40 = default(MySQLParser.index_name_return);

        MySQLParser.index_cols_return index_cols41 = default(MySQLParser.index_cols_return);

        MySQLParser.reference_definition_return reference_definition42 = default(MySQLParser.reference_definition_return);


        object CONSTRAINT37_tree=null;
        object FOREIGN38_tree=null;
        object KEY39_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:58:34: ( ( CONSTRAINT (name= id )? )? FOREIGN KEY ( index_name )? index_cols[fk] reference_definition[fk] )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:59:5: ( CONSTRAINT (name= id )? )? FOREIGN KEY ( index_name )? index_cols[fk] reference_definition[fk]
            {
            	root_0 = (object)adaptor.GetNilNode();

            	var fk=new ForeignKey(); table._Constraints.Add(fk);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:60:5: ( CONSTRAINT (name= id )? )?
            	int alt20 = 2;
            	int LA20_0 = input.LA(1);

            	if ( (LA20_0 == CONSTRAINT) )
            	{
            	    alt20 = 1;
            	}
            	switch (alt20) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:60:6: CONSTRAINT (name= id )?
            	        {
            	        	CONSTRAINT37=(IToken)Match(input,CONSTRAINT,FOLLOW_CONSTRAINT_in_foreign_key378); 
            	        		CONSTRAINT37_tree = (object)adaptor.Create(CONSTRAINT37);
            	        		adaptor.AddChild(root_0, CONSTRAINT37_tree);

            	        	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:60:17: (name= id )?
            	        	int alt19 = 2;
            	        	int LA19_0 = input.LA(1);

            	        	if ( ((LA19_0 >= T_IDENT && LA19_0 <= T_QUOTED_IDENT)) )
            	        	{
            	        	    alt19 = 1;
            	        	}
            	        	switch (alt19) 
            	        	{
            	        	    case 1 :
            	        	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:60:18: name= id
            	        	        {
            	        	        	PushFollow(FOLLOW_id_in_foreign_key383);
            	        	        	name = id();
            	        	        	state.followingStackPointer--;

            	        	        	adaptor.AddChild(root_0, name.Tree);
            	        	        	fk.Name=UnquoteName(((name != null) ? input.ToString((IToken)(name.Start),(IToken)(name.Stop)) : null));

            	        	        }
            	        	        break;

            	        	}


            	        }
            	        break;

            	}

            	FOREIGN38=(IToken)Match(input,FOREIGN,FOLLOW_FOREIGN_in_foreign_key396); 
            		FOREIGN38_tree = (object)adaptor.Create(FOREIGN38);
            		adaptor.AddChild(root_0, FOREIGN38_tree);

            	KEY39=(IToken)Match(input,KEY,FOLLOW_KEY_in_foreign_key398); 
            		KEY39_tree = (object)adaptor.Create(KEY39);
            		adaptor.AddChild(root_0, KEY39_tree);

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:61:17: ( index_name )?
            	int alt21 = 2;
            	int LA21_0 = input.LA(1);

            	if ( ((LA21_0 >= T_IDENT && LA21_0 <= T_QUOTED_IDENT)) )
            	{
            	    alt21 = 1;
            	}
            	switch (alt21) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:61:17: index_name
            	        {
            	        	PushFollow(FOLLOW_index_name_in_foreign_key400);
            	        	index_name40 = index_name();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, index_name40.Tree);

            	        }
            	        break;

            	}

            	PushFollow(FOLLOW_index_cols_in_foreign_key403);
            	index_cols41 = index_cols(fk);
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, index_cols41.Tree);
            	PushFollow(FOLLOW_reference_definition_in_foreign_key406);
            	reference_definition42 = reference_definition(fk);
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, reference_definition42.Tree);

            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "foreign_key"

    public class index_type_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "index_type"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:63:1: index_type[Constraint cnt] : USING id ;
    public MySQLParser.index_type_return index_type(Constraint cnt) // throws RecognitionException [1]
    {   
        MySQLParser.index_type_return retval = new MySQLParser.index_type_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken USING43 = null;
        MySQLParser.id_return id44 = default(MySQLParser.id_return);


        object USING43_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:63:27: ( USING id )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:63:29: USING id
            {
            	root_0 = (object)adaptor.GetNilNode();

            	USING43=(IToken)Match(input,USING,FOLLOW_USING_in_index_type415); 
            		USING43_tree = (object)adaptor.Create(USING43);
            		adaptor.AddChild(root_0, USING43_tree);

            	PushFollow(FOLLOW_id_in_index_type417);
            	id44 = id();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, id44.Tree);
            	cnt.MySet("index_type", ((id44 != null) ? input.ToString((IToken)(id44.Start),(IToken)(id44.Stop)) : null));

            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "index_type"

    public class index_cols_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "index_cols"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:65:1: index_cols[ColumnsConstraint cnt] : LPAREN index_col[cnt] ( COMMA index_col[cnt] )* RPAREN ;
    public MySQLParser.index_cols_return index_cols(ColumnsConstraint cnt) // throws RecognitionException [1]
    {   
        MySQLParser.index_cols_return retval = new MySQLParser.index_cols_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken LPAREN45 = null;
        IToken COMMA47 = null;
        IToken RPAREN49 = null;
        MySQLParser.index_col_return index_col46 = default(MySQLParser.index_col_return);

        MySQLParser.index_col_return index_col48 = default(MySQLParser.index_col_return);


        object LPAREN45_tree=null;
        object COMMA47_tree=null;
        object RPAREN49_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:65:34: ( LPAREN index_col[cnt] ( COMMA index_col[cnt] )* RPAREN )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:65:36: LPAREN index_col[cnt] ( COMMA index_col[cnt] )* RPAREN
            {
            	root_0 = (object)adaptor.GetNilNode();

            	LPAREN45=(IToken)Match(input,LPAREN,FOLLOW_LPAREN_in_index_cols427); 
            		LPAREN45_tree = (object)adaptor.Create(LPAREN45);
            		adaptor.AddChild(root_0, LPAREN45_tree);

            	PushFollow(FOLLOW_index_col_in_index_cols429);
            	index_col46 = index_col(cnt);
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, index_col46.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:65:58: ( COMMA index_col[cnt] )*
            	do 
            	{
            	    int alt22 = 2;
            	    int LA22_0 = input.LA(1);

            	    if ( (LA22_0 == COMMA) )
            	    {
            	        alt22 = 1;
            	    }


            	    switch (alt22) 
            		{
            			case 1 :
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:65:59: COMMA index_col[cnt]
            			    {
            			    	COMMA47=(IToken)Match(input,COMMA,FOLLOW_COMMA_in_index_cols433); 
            			    		COMMA47_tree = (object)adaptor.Create(COMMA47);
            			    		adaptor.AddChild(root_0, COMMA47_tree);

            			    	PushFollow(FOLLOW_index_col_in_index_cols435);
            			    	index_col48 = index_col(cnt);
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, index_col48.Tree);

            			    }
            			    break;

            			default:
            			    goto loop22;
            	    }
            	} while (true);

            	loop22:
            		;	// Stops C# compiler whining that label 'loop22' has no statements

            	RPAREN49=(IToken)Match(input,RPAREN,FOLLOW_RPAREN_in_index_cols440); 
            		RPAREN49_tree = (object)adaptor.Create(RPAREN49);
            		adaptor.AddChild(root_0, RPAREN49_tree);


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "index_cols"

    public class index_col_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "index_col"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:66:1: index_col[ColumnsConstraint cnt] : name= col_name ( LPAREN subpart= T_INTEGER RPAREN )? ( ASC | DESC )? ;
    public MySQLParser.index_col_return index_col(ColumnsConstraint cnt) // throws RecognitionException [1]
    {   
        MySQLParser.index_col_return retval = new MySQLParser.index_col_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken subpart = null;
        IToken LPAREN50 = null;
        IToken RPAREN51 = null;
        IToken set52 = null;
        MySQLParser.col_name_return name = default(MySQLParser.col_name_return);


        object subpart_tree=null;
        object LPAREN50_tree=null;
        object RPAREN51_tree=null;
        object set52_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:66:33: (name= col_name ( LPAREN subpart= T_INTEGER RPAREN )? ( ASC | DESC )? )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:66:35: name= col_name ( LPAREN subpart= T_INTEGER RPAREN )? ( ASC | DESC )?
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_col_name_in_index_col449);
            	name = col_name();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, name.Tree);
            	var col=new ColumnReference(UnquoteName(((name != null) ? input.ToString((IToken)(name.Start),(IToken)(name.Stop)) : null)));cnt.Columns.Add(col);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:68:5: ( LPAREN subpart= T_INTEGER RPAREN )?
            	int alt23 = 2;
            	int LA23_0 = input.LA(1);

            	if ( (LA23_0 == LPAREN) )
            	{
            	    alt23 = 1;
            	}
            	switch (alt23) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:68:6: LPAREN subpart= T_INTEGER RPAREN
            	        {
            	        	LPAREN50=(IToken)Match(input,LPAREN,FOLLOW_LPAREN_in_index_col462); 
            	        		LPAREN50_tree = (object)adaptor.Create(LPAREN50);
            	        		adaptor.AddChild(root_0, LPAREN50_tree);

            	        	subpart=(IToken)Match(input,T_INTEGER,FOLLOW_T_INTEGER_in_index_col466); 
            	        		subpart_tree = (object)adaptor.Create(subpart);
            	        		adaptor.AddChild(root_0, subpart_tree);

            	        	RPAREN51=(IToken)Match(input,RPAREN,FOLLOW_RPAREN_in_index_col468); 
            	        		RPAREN51_tree = (object)adaptor.Create(RPAREN51);
            	        		adaptor.AddChild(root_0, RPAREN51_tree);

            	        	col.MySet("sub_part", ((subpart != null) ? subpart.Text : null));

            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:69:5: ( ASC | DESC )?
            	int alt24 = 2;
            	int LA24_0 = input.LA(1);

            	if ( ((LA24_0 >= ASC && LA24_0 <= DESC)) )
            	{
            	    alt24 = 1;
            	}
            	switch (alt24) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:
            	        {
            	        	set52 = (IToken)input.LT(1);
            	        	if ( (input.LA(1) >= ASC && input.LA(1) <= DESC) ) 
            	        	{
            	        	    input.Consume();
            	        	    adaptor.AddChild(root_0, (object)adaptor.Create(set52));
            	        	    state.errorRecovery = false;
            	        	}
            	        	else 
            	        	{
            	        	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	        	    throw mse;
            	        	}


            	        }
            	        break;

            	}


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "index_col"

    public class fk_cols_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "fk_cols"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:71:1: fk_cols[ForeignKey fk] : LPAREN fk_col[fk] ( COMMA fk_col[fk] )* RPAREN ;
    public MySQLParser.fk_cols_return fk_cols(ForeignKey fk) // throws RecognitionException [1]
    {   
        MySQLParser.fk_cols_return retval = new MySQLParser.fk_cols_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken LPAREN53 = null;
        IToken COMMA55 = null;
        IToken RPAREN57 = null;
        MySQLParser.fk_col_return fk_col54 = default(MySQLParser.fk_col_return);

        MySQLParser.fk_col_return fk_col56 = default(MySQLParser.fk_col_return);


        object LPAREN53_tree=null;
        object COMMA55_tree=null;
        object RPAREN57_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:71:23: ( LPAREN fk_col[fk] ( COMMA fk_col[fk] )* RPAREN )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:71:25: LPAREN fk_col[fk] ( COMMA fk_col[fk] )* RPAREN
            {
            	root_0 = (object)adaptor.GetNilNode();

            	LPAREN53=(IToken)Match(input,LPAREN,FOLLOW_LPAREN_in_fk_cols494); 
            		LPAREN53_tree = (object)adaptor.Create(LPAREN53);
            		adaptor.AddChild(root_0, LPAREN53_tree);

            	PushFollow(FOLLOW_fk_col_in_fk_cols496);
            	fk_col54 = fk_col(fk);
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, fk_col54.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:71:43: ( COMMA fk_col[fk] )*
            	do 
            	{
            	    int alt25 = 2;
            	    int LA25_0 = input.LA(1);

            	    if ( (LA25_0 == COMMA) )
            	    {
            	        alt25 = 1;
            	    }


            	    switch (alt25) 
            		{
            			case 1 :
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:71:44: COMMA fk_col[fk]
            			    {
            			    	COMMA55=(IToken)Match(input,COMMA,FOLLOW_COMMA_in_fk_cols500); 
            			    		COMMA55_tree = (object)adaptor.Create(COMMA55);
            			    		adaptor.AddChild(root_0, COMMA55_tree);

            			    	PushFollow(FOLLOW_fk_col_in_fk_cols502);
            			    	fk_col56 = fk_col(fk);
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, fk_col56.Tree);

            			    }
            			    break;

            			default:
            			    goto loop25;
            	    }
            	} while (true);

            	loop25:
            		;	// Stops C# compiler whining that label 'loop25' has no statements

            	RPAREN57=(IToken)Match(input,RPAREN,FOLLOW_RPAREN_in_fk_cols507); 
            		RPAREN57_tree = (object)adaptor.Create(RPAREN57);
            		adaptor.AddChild(root_0, RPAREN57_tree);


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "fk_cols"

    public class fk_col_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "fk_col"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:72:1: fk_col[ForeignKey fk] : name= col_name ( LPAREN subpart= T_INTEGER RPAREN )? ( ASC | DESC )? ;
    public MySQLParser.fk_col_return fk_col(ForeignKey fk) // throws RecognitionException [1]
    {   
        MySQLParser.fk_col_return retval = new MySQLParser.fk_col_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken subpart = null;
        IToken LPAREN58 = null;
        IToken RPAREN59 = null;
        IToken set60 = null;
        MySQLParser.col_name_return name = default(MySQLParser.col_name_return);


        object subpart_tree=null;
        object LPAREN58_tree=null;
        object RPAREN59_tree=null;
        object set60_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:72:22: (name= col_name ( LPAREN subpart= T_INTEGER RPAREN )? ( ASC | DESC )? )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:72:24: name= col_name ( LPAREN subpart= T_INTEGER RPAREN )? ( ASC | DESC )?
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_col_name_in_fk_col516);
            	name = col_name();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, name.Tree);
            	var col=new ColumnReference(UnquoteName(((name != null) ? input.ToString((IToken)(name.Start),(IToken)(name.Stop)) : null)));fk.PrimaryKeyColumns.Add(col);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:74:5: ( LPAREN subpart= T_INTEGER RPAREN )?
            	int alt26 = 2;
            	int LA26_0 = input.LA(1);

            	if ( (LA26_0 == LPAREN) )
            	{
            	    alt26 = 1;
            	}
            	switch (alt26) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:74:6: LPAREN subpart= T_INTEGER RPAREN
            	        {
            	        	LPAREN58=(IToken)Match(input,LPAREN,FOLLOW_LPAREN_in_fk_col529); 
            	        		LPAREN58_tree = (object)adaptor.Create(LPAREN58);
            	        		adaptor.AddChild(root_0, LPAREN58_tree);

            	        	subpart=(IToken)Match(input,T_INTEGER,FOLLOW_T_INTEGER_in_fk_col533); 
            	        		subpart_tree = (object)adaptor.Create(subpart);
            	        		adaptor.AddChild(root_0, subpart_tree);

            	        	RPAREN59=(IToken)Match(input,RPAREN,FOLLOW_RPAREN_in_fk_col535); 
            	        		RPAREN59_tree = (object)adaptor.Create(RPAREN59);
            	        		adaptor.AddChild(root_0, RPAREN59_tree);

            	        	col.MySet("sub_part", ((subpart != null) ? subpart.Text : null));

            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:75:5: ( ASC | DESC )?
            	int alt27 = 2;
            	int LA27_0 = input.LA(1);

            	if ( ((LA27_0 >= ASC && LA27_0 <= DESC)) )
            	{
            	    alt27 = 1;
            	}
            	switch (alt27) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:
            	        {
            	        	set60 = (IToken)input.LT(1);
            	        	if ( (input.LA(1) >= ASC && input.LA(1) <= DESC) ) 
            	        	{
            	        	    input.Consume();
            	        	    adaptor.AddChild(root_0, (object)adaptor.Create(set60));
            	        	    state.errorRecovery = false;
            	        	}
            	        	else 
            	        	{
            	        	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	        	    throw mse;
            	        	}


            	        }
            	        break;

            	}


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "fk_col"

    public class index_option_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "index_option"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:77:1: index_option[Constraint cnt] : ( KEY_BLOCK_SIZE ( EQUALS )? T_INTEGER | index_type[cnt] | WITH PARSER id );
    public MySQLParser.index_option_return index_option(Constraint cnt) // throws RecognitionException [1]
    {   
        MySQLParser.index_option_return retval = new MySQLParser.index_option_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken KEY_BLOCK_SIZE61 = null;
        IToken EQUALS62 = null;
        IToken T_INTEGER63 = null;
        IToken WITH65 = null;
        IToken PARSER66 = null;
        MySQLParser.index_type_return index_type64 = default(MySQLParser.index_type_return);

        MySQLParser.id_return id67 = default(MySQLParser.id_return);


        object KEY_BLOCK_SIZE61_tree=null;
        object EQUALS62_tree=null;
        object T_INTEGER63_tree=null;
        object WITH65_tree=null;
        object PARSER66_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:77:29: ( KEY_BLOCK_SIZE ( EQUALS )? T_INTEGER | index_type[cnt] | WITH PARSER id )
            int alt29 = 3;
            switch ( input.LA(1) ) 
            {
            case KEY_BLOCK_SIZE:
            	{
                alt29 = 1;
                }
                break;
            case USING:
            	{
                alt29 = 2;
                }
                break;
            case WITH:
            	{
                alt29 = 3;
                }
                break;
            	default:
            	    NoViableAltException nvae_d29s0 =
            	        new NoViableAltException("", 29, 0, input);

            	    throw nvae_d29s0;
            }

            switch (alt29) 
            {
                case 1 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:78:5: KEY_BLOCK_SIZE ( EQUALS )? T_INTEGER
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	KEY_BLOCK_SIZE61=(IToken)Match(input,KEY_BLOCK_SIZE,FOLLOW_KEY_BLOCK_SIZE_in_index_option565); 
                    		KEY_BLOCK_SIZE61_tree = (object)adaptor.Create(KEY_BLOCK_SIZE61);
                    		adaptor.AddChild(root_0, KEY_BLOCK_SIZE61_tree);

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:78:20: ( EQUALS )?
                    	int alt28 = 2;
                    	int LA28_0 = input.LA(1);

                    	if ( (LA28_0 == EQUALS) )
                    	{
                    	    alt28 = 1;
                    	}
                    	switch (alt28) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:78:20: EQUALS
                    	        {
                    	        	EQUALS62=(IToken)Match(input,EQUALS,FOLLOW_EQUALS_in_index_option567); 
                    	        		EQUALS62_tree = (object)adaptor.Create(EQUALS62);
                    	        		adaptor.AddChild(root_0, EQUALS62_tree);


                    	        }
                    	        break;

                    	}

                    	T_INTEGER63=(IToken)Match(input,T_INTEGER,FOLLOW_T_INTEGER_in_index_option570); 
                    		T_INTEGER63_tree = (object)adaptor.Create(T_INTEGER63);
                    		adaptor.AddChild(root_0, T_INTEGER63_tree);


                    }
                    break;
                case 2 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:79:5: index_type[cnt]
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_index_type_in_index_option576);
                    	index_type64 = index_type(cnt);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, index_type64.Tree);

                    }
                    break;
                case 3 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:80:5: WITH PARSER id
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	WITH65=(IToken)Match(input,WITH,FOLLOW_WITH_in_index_option583); 
                    		WITH65_tree = (object)adaptor.Create(WITH65);
                    		adaptor.AddChild(root_0, WITH65_tree);

                    	PARSER66=(IToken)Match(input,PARSER,FOLLOW_PARSER_in_index_option585); 
                    		PARSER66_tree = (object)adaptor.Create(PARSER66);
                    		adaptor.AddChild(root_0, PARSER66_tree);

                    	PushFollow(FOLLOW_id_in_index_option587);
                    	id67 = id();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, id67.Tree);

                    }
                    break;

            }
            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "index_option"

    public class id_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "id"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:82:1: id : ( T_IDENT | T_QUOTED_IDENT );
    public MySQLParser.id_return id() // throws RecognitionException [1]
    {   
        MySQLParser.id_return retval = new MySQLParser.id_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken set68 = null;

        object set68_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:82:3: ( T_IDENT | T_QUOTED_IDENT )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:
            {
            	root_0 = (object)adaptor.GetNilNode();

            	set68 = (IToken)input.LT(1);
            	if ( (input.LA(1) >= T_IDENT && input.LA(1) <= T_QUOTED_IDENT) ) 
            	{
            	    input.Consume();
            	    adaptor.AddChild(root_0, (object)adaptor.Create(set68));
            	    state.errorRecovery = false;
            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    throw mse;
            	}


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "id"

    public class col_name_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "col_name"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:83:1: col_name : id ;
    public MySQLParser.col_name_return col_name() // throws RecognitionException [1]
    {   
        MySQLParser.col_name_return retval = new MySQLParser.col_name_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        MySQLParser.id_return id69 = default(MySQLParser.id_return);



        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:83:9: ( id )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:83:11: id
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_id_in_col_name604);
            	id69 = id();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, id69.Tree);

            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "col_name"

    public class tbl_name_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "tbl_name"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:84:1: tbl_name : id ;
    public MySQLParser.tbl_name_return tbl_name() // throws RecognitionException [1]
    {   
        MySQLParser.tbl_name_return retval = new MySQLParser.tbl_name_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        MySQLParser.id_return id70 = default(MySQLParser.id_return);



        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:84:9: ( id )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:84:11: id
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_id_in_tbl_name610);
            	id70 = id();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, id70.Tree);

            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "tbl_name"

    public class index_name_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "index_name"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:85:1: index_name : id ;
    public MySQLParser.index_name_return index_name() // throws RecognitionException [1]
    {   
        MySQLParser.index_name_return retval = new MySQLParser.index_name_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        MySQLParser.id_return id71 = default(MySQLParser.id_return);



        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:85:11: ( id )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:85:13: id
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_id_in_index_name616);
            	id71 = id();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, id71.Tree);

            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "index_name"

    public class charset_name_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "charset_name"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:86:1: charset_name : id ;
    public MySQLParser.charset_name_return charset_name() // throws RecognitionException [1]
    {   
        MySQLParser.charset_name_return retval = new MySQLParser.charset_name_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        MySQLParser.id_return id72 = default(MySQLParser.id_return);



        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:86:13: ( id )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:86:17: id
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_id_in_charset_name624);
            	id72 = id();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, id72.Tree);

            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "charset_name"

    public class collation_name_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "collation_name"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:87:1: collation_name : id ;
    public MySQLParser.collation_name_return collation_name() // throws RecognitionException [1]
    {   
        MySQLParser.collation_name_return retval = new MySQLParser.collation_name_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        MySQLParser.id_return id73 = default(MySQLParser.id_return);



        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:87:15: ( id )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:87:19: id
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_id_in_collation_name632);
            	id73 = id();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, id73.Tree);

            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "collation_name"

    public class column_definition_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "column_definition"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:89:1: column_definition[ColumnStructure col] : data_type[col] ( ( NOT NULL ) | ( NULL ) )? ( DEFAULT default_value_expr[col] )? ( ON UPDATE CURRENT_TIMESTAMP )? ( AUTO_INCREMENT )? ( UNIQUE ( KEY )? | ( PRIMARY )? KEY )? ( COMMENT com= T_STRING )? ( COLUMN_FORMAT ( FIXED | DYNAMIC | DEFAULT ) )? ( STORAGE ( DISK | MEMORY | DEFAULT ) )? ( reference_definition[new ForeignKey()] )? ;
    public MySQLParser.column_definition_return column_definition(ColumnStructure col) // throws RecognitionException [1]
    {   
        MySQLParser.column_definition_return retval = new MySQLParser.column_definition_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken com = null;
        IToken NOT75 = null;
        IToken NULL76 = null;
        IToken NULL77 = null;
        IToken DEFAULT78 = null;
        IToken ON80 = null;
        IToken UPDATE81 = null;
        IToken CURRENT_TIMESTAMP82 = null;
        IToken AUTO_INCREMENT83 = null;
        IToken UNIQUE84 = null;
        IToken KEY85 = null;
        IToken PRIMARY86 = null;
        IToken KEY87 = null;
        IToken COMMENT88 = null;
        IToken COLUMN_FORMAT89 = null;
        IToken set90 = null;
        IToken STORAGE91 = null;
        IToken set92 = null;
        MySQLParser.data_type_return data_type74 = default(MySQLParser.data_type_return);

        MySQLParser.default_value_expr_return default_value_expr79 = default(MySQLParser.default_value_expr_return);

        MySQLParser.reference_definition_return reference_definition93 = default(MySQLParser.reference_definition_return);


        object com_tree=null;
        object NOT75_tree=null;
        object NULL76_tree=null;
        object NULL77_tree=null;
        object DEFAULT78_tree=null;
        object ON80_tree=null;
        object UPDATE81_tree=null;
        object CURRENT_TIMESTAMP82_tree=null;
        object AUTO_INCREMENT83_tree=null;
        object UNIQUE84_tree=null;
        object KEY85_tree=null;
        object PRIMARY86_tree=null;
        object KEY87_tree=null;
        object COMMENT88_tree=null;
        object COLUMN_FORMAT89_tree=null;
        object set90_tree=null;
        object STORAGE91_tree=null;
        object set92_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:89:39: ( data_type[col] ( ( NOT NULL ) | ( NULL ) )? ( DEFAULT default_value_expr[col] )? ( ON UPDATE CURRENT_TIMESTAMP )? ( AUTO_INCREMENT )? ( UNIQUE ( KEY )? | ( PRIMARY )? KEY )? ( COMMENT com= T_STRING )? ( COLUMN_FORMAT ( FIXED | DYNAMIC | DEFAULT ) )? ( STORAGE ( DISK | MEMORY | DEFAULT ) )? ( reference_definition[new ForeignKey()] )? )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:90:5: data_type[col] ( ( NOT NULL ) | ( NULL ) )? ( DEFAULT default_value_expr[col] )? ( ON UPDATE CURRENT_TIMESTAMP )? ( AUTO_INCREMENT )? ( UNIQUE ( KEY )? | ( PRIMARY )? KEY )? ( COMMENT com= T_STRING )? ( COLUMN_FORMAT ( FIXED | DYNAMIC | DEFAULT ) )? ( STORAGE ( DISK | MEMORY | DEFAULT ) )? ( reference_definition[new ForeignKey()] )?
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_data_type_in_column_definition644);
            	data_type74 = data_type(col);
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, data_type74.Tree);
            	col.IsNullable=true;
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:91:7: ( ( NOT NULL ) | ( NULL ) )?
            	int alt30 = 3;
            	int LA30_0 = input.LA(1);

            	if ( (LA30_0 == NOT) )
            	{
            	    alt30 = 1;
            	}
            	else if ( (LA30_0 == NULL) )
            	{
            	    alt30 = 2;
            	}
            	switch (alt30) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:91:8: ( NOT NULL )
            	        {
            	        	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:91:8: ( NOT NULL )
            	        	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:91:9: NOT NULL
            	        	{
            	        		NOT75=(IToken)Match(input,NOT,FOLLOW_NOT_in_column_definition657); 
            	        			NOT75_tree = (object)adaptor.Create(NOT75);
            	        			adaptor.AddChild(root_0, NOT75_tree);

            	        		NULL76=(IToken)Match(input,NULL,FOLLOW_NULL_in_column_definition659); 
            	        			NULL76_tree = (object)adaptor.Create(NULL76);
            	        			adaptor.AddChild(root_0, NULL76_tree);

            	        		col.IsNullable=false;

            	        	}


            	        }
            	        break;
            	    case 2 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:91:45: ( NULL )
            	        {
            	        	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:91:45: ( NULL )
            	        	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:91:46: NULL
            	        	{
            	        		NULL77=(IToken)Match(input,NULL,FOLLOW_NULL_in_column_definition667); 
            	        			NULL77_tree = (object)adaptor.Create(NULL77);
            	        			adaptor.AddChild(root_0, NULL77_tree);

            	        		col.IsNullable=true;

            	        	}


            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:92:7: ( DEFAULT default_value_expr[col] )?
            	int alt31 = 2;
            	int LA31_0 = input.LA(1);

            	if ( (LA31_0 == DEFAULT) )
            	{
            	    alt31 = 1;
            	}
            	switch (alt31) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:92:8: DEFAULT default_value_expr[col]
            	        {
            	        	DEFAULT78=(IToken)Match(input,DEFAULT,FOLLOW_DEFAULT_in_column_definition681); 
            	        		DEFAULT78_tree = (object)adaptor.Create(DEFAULT78);
            	        		adaptor.AddChild(root_0, DEFAULT78_tree);

            	        	PushFollow(FOLLOW_default_value_expr_in_column_definition683);
            	        	default_value_expr79 = default_value_expr(col);
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, default_value_expr79.Tree);

            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:93:7: ( ON UPDATE CURRENT_TIMESTAMP )?
            	int alt32 = 2;
            	int LA32_0 = input.LA(1);

            	if ( (LA32_0 == ON) )
            	{
            	    alt32 = 1;
            	}
            	switch (alt32) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:93:8: ON UPDATE CURRENT_TIMESTAMP
            	        {
            	        	ON80=(IToken)Match(input,ON,FOLLOW_ON_in_column_definition695); 
            	        		ON80_tree = (object)adaptor.Create(ON80);
            	        		adaptor.AddChild(root_0, ON80_tree);

            	        	UPDATE81=(IToken)Match(input,UPDATE,FOLLOW_UPDATE_in_column_definition697); 
            	        		UPDATE81_tree = (object)adaptor.Create(UPDATE81);
            	        		adaptor.AddChild(root_0, UPDATE81_tree);

            	        	CURRENT_TIMESTAMP82=(IToken)Match(input,CURRENT_TIMESTAMP,FOLLOW_CURRENT_TIMESTAMP_in_column_definition699); 
            	        		CURRENT_TIMESTAMP82_tree = (object)adaptor.Create(CURRENT_TIMESTAMP82);
            	        		adaptor.AddChild(root_0, CURRENT_TIMESTAMP82_tree);


            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:94:7: ( AUTO_INCREMENT )?
            	int alt33 = 2;
            	int LA33_0 = input.LA(1);

            	if ( (LA33_0 == AUTO_INCREMENT) )
            	{
            	    alt33 = 1;
            	}
            	switch (alt33) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:94:8: AUTO_INCREMENT
            	        {
            	        	AUTO_INCREMENT83=(IToken)Match(input,AUTO_INCREMENT,FOLLOW_AUTO_INCREMENT_in_column_definition710); 
            	        		AUTO_INCREMENT83_tree = (object)adaptor.Create(AUTO_INCREMENT83);
            	        		adaptor.AddChild(root_0, AUTO_INCREMENT83_tree);

            	        	col.DataType.SetAutoincrement(true);

            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:95:7: ( UNIQUE ( KEY )? | ( PRIMARY )? KEY )?
            	int alt36 = 3;
            	int LA36_0 = input.LA(1);

            	if ( (LA36_0 == UNIQUE) )
            	{
            	    alt36 = 1;
            	}
            	else if ( ((LA36_0 >= KEY && LA36_0 <= PRIMARY)) )
            	{
            	    alt36 = 2;
            	}
            	switch (alt36) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:95:8: UNIQUE ( KEY )?
            	        {
            	        	UNIQUE84=(IToken)Match(input,UNIQUE,FOLLOW_UNIQUE_in_column_definition724); 
            	        		UNIQUE84_tree = (object)adaptor.Create(UNIQUE84);
            	        		adaptor.AddChild(root_0, UNIQUE84_tree);

            	        	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:95:15: ( KEY )?
            	        	int alt34 = 2;
            	        	int LA34_0 = input.LA(1);

            	        	if ( (LA34_0 == KEY) )
            	        	{
            	        	    alt34 = 1;
            	        	}
            	        	switch (alt34) 
            	        	{
            	        	    case 1 :
            	        	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:95:15: KEY
            	        	        {
            	        	        	KEY85=(IToken)Match(input,KEY,FOLLOW_KEY_in_column_definition726); 
            	        	        		KEY85_tree = (object)adaptor.Create(KEY85);
            	        	        		adaptor.AddChild(root_0, KEY85_tree);


            	        	        }
            	        	        break;

            	        	}


            	        }
            	        break;
            	    case 2 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:95:22: ( PRIMARY )? KEY
            	        {
            	        	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:95:22: ( PRIMARY )?
            	        	int alt35 = 2;
            	        	int LA35_0 = input.LA(1);

            	        	if ( (LA35_0 == PRIMARY) )
            	        	{
            	        	    alt35 = 1;
            	        	}
            	        	switch (alt35) 
            	        	{
            	        	    case 1 :
            	        	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:95:22: PRIMARY
            	        	        {
            	        	        	PRIMARY86=(IToken)Match(input,PRIMARY,FOLLOW_PRIMARY_in_column_definition731); 
            	        	        		PRIMARY86_tree = (object)adaptor.Create(PRIMARY86);
            	        	        		adaptor.AddChild(root_0, PRIMARY86_tree);


            	        	        }
            	        	        break;

            	        	}

            	        	KEY87=(IToken)Match(input,KEY,FOLLOW_KEY_in_column_definition734); 
            	        		KEY87_tree = (object)adaptor.Create(KEY87);
            	        		adaptor.AddChild(root_0, KEY87_tree);


            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:96:7: ( COMMENT com= T_STRING )?
            	int alt37 = 2;
            	int LA37_0 = input.LA(1);

            	if ( (LA37_0 == COMMENT) )
            	{
            	    alt37 = 1;
            	}
            	switch (alt37) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:96:8: COMMENT com= T_STRING
            	        {
            	        	COMMENT88=(IToken)Match(input,COMMENT,FOLLOW_COMMENT_in_column_definition745); 
            	        		COMMENT88_tree = (object)adaptor.Create(COMMENT88);
            	        		adaptor.AddChild(root_0, COMMENT88_tree);

            	        	com=(IToken)Match(input,T_STRING,FOLLOW_T_STRING_in_column_definition749); 
            	        		com_tree = (object)adaptor.Create(com);
            	        		adaptor.AddChild(root_0, com_tree);

            	        	 col.Comment=StringValue(((com != null) ? com.Text : null)); 

            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:97:7: ( COLUMN_FORMAT ( FIXED | DYNAMIC | DEFAULT ) )?
            	int alt38 = 2;
            	int LA38_0 = input.LA(1);

            	if ( (LA38_0 == COLUMN_FORMAT) )
            	{
            	    alt38 = 1;
            	}
            	switch (alt38) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:97:8: COLUMN_FORMAT ( FIXED | DYNAMIC | DEFAULT )
            	        {
            	        	COLUMN_FORMAT89=(IToken)Match(input,COLUMN_FORMAT,FOLLOW_COLUMN_FORMAT_in_column_definition763); 
            	        		COLUMN_FORMAT89_tree = (object)adaptor.Create(COLUMN_FORMAT89);
            	        		adaptor.AddChild(root_0, COLUMN_FORMAT89_tree);

            	        	set90 = (IToken)input.LT(1);
            	        	if ( input.LA(1) == DEFAULT || (input.LA(1) >= FIXED && input.LA(1) <= DYNAMIC) ) 
            	        	{
            	        	    input.Consume();
            	        	    adaptor.AddChild(root_0, (object)adaptor.Create(set90));
            	        	    state.errorRecovery = false;
            	        	}
            	        	else 
            	        	{
            	        	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	        	    throw mse;
            	        	}


            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:98:7: ( STORAGE ( DISK | MEMORY | DEFAULT ) )?
            	int alt39 = 2;
            	int LA39_0 = input.LA(1);

            	if ( (LA39_0 == STORAGE) )
            	{
            	    alt39 = 1;
            	}
            	switch (alt39) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:98:8: STORAGE ( DISK | MEMORY | DEFAULT )
            	        {
            	        	STORAGE91=(IToken)Match(input,STORAGE,FOLLOW_STORAGE_in_column_definition782); 
            	        		STORAGE91_tree = (object)adaptor.Create(STORAGE91);
            	        		adaptor.AddChild(root_0, STORAGE91_tree);

            	        	set92 = (IToken)input.LT(1);
            	        	if ( input.LA(1) == DEFAULT || (input.LA(1) >= DISK && input.LA(1) <= MEMORY) ) 
            	        	{
            	        	    input.Consume();
            	        	    adaptor.AddChild(root_0, (object)adaptor.Create(set92));
            	        	    state.errorRecovery = false;
            	        	}
            	        	else 
            	        	{
            	        	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	        	    throw mse;
            	        	}


            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:99:7: ( reference_definition[new ForeignKey()] )?
            	int alt40 = 2;
            	int LA40_0 = input.LA(1);

            	if ( (LA40_0 == REFERENCES) )
            	{
            	    alt40 = 1;
            	}
            	switch (alt40) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:99:7: reference_definition[new ForeignKey()]
            	        {
            	        	PushFollow(FOLLOW_reference_definition_in_column_definition800);
            	        	reference_definition93 = reference_definition(new ForeignKey());
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, reference_definition93.Tree);

            	        }
            	        break;

            	}


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "column_definition"

    public class reference_definition_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "reference_definition"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:102:1: reference_definition[ForeignKey fk] : REFERENCES dstname= tbl_name fk_cols[fk] ( MATCH FULL | MATCH PARTIAL | MATCH SIMPLE )? ( ON DELETE ondel= reference_option )? ( ON UPDATE onupd= reference_option )? ;
    public MySQLParser.reference_definition_return reference_definition(ForeignKey fk) // throws RecognitionException [1]
    {   
        MySQLParser.reference_definition_return retval = new MySQLParser.reference_definition_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken REFERENCES94 = null;
        IToken MATCH96 = null;
        IToken FULL97 = null;
        IToken MATCH98 = null;
        IToken PARTIAL99 = null;
        IToken MATCH100 = null;
        IToken SIMPLE101 = null;
        IToken ON102 = null;
        IToken DELETE103 = null;
        IToken ON104 = null;
        IToken UPDATE105 = null;
        MySQLParser.tbl_name_return dstname = default(MySQLParser.tbl_name_return);

        MySQLParser.reference_option_return ondel = default(MySQLParser.reference_option_return);

        MySQLParser.reference_option_return onupd = default(MySQLParser.reference_option_return);

        MySQLParser.fk_cols_return fk_cols95 = default(MySQLParser.fk_cols_return);


        object REFERENCES94_tree=null;
        object MATCH96_tree=null;
        object FULL97_tree=null;
        object MATCH98_tree=null;
        object PARTIAL99_tree=null;
        object MATCH100_tree=null;
        object SIMPLE101_tree=null;
        object ON102_tree=null;
        object DELETE103_tree=null;
        object ON104_tree=null;
        object UPDATE105_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:102:36: ( REFERENCES dstname= tbl_name fk_cols[fk] ( MATCH FULL | MATCH PARTIAL | MATCH SIMPLE )? ( ON DELETE ondel= reference_option )? ( ON UPDATE onupd= reference_option )? )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:103:5: REFERENCES dstname= tbl_name fk_cols[fk] ( MATCH FULL | MATCH PARTIAL | MATCH SIMPLE )? ( ON DELETE ondel= reference_option )? ( ON UPDATE onupd= reference_option )?
            {
            	root_0 = (object)adaptor.GetNilNode();

            	REFERENCES94=(IToken)Match(input,REFERENCES,FOLLOW_REFERENCES_in_reference_definition822); 
            		REFERENCES94_tree = (object)adaptor.Create(REFERENCES94);
            		adaptor.AddChild(root_0, REFERENCES94_tree);

            	PushFollow(FOLLOW_tbl_name_in_reference_definition826);
            	dstname = tbl_name();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, dstname.Tree);
            	fk.PrimaryKeyTable=new NameWithSchema(UnquoteName(((dstname != null) ? input.ToString((IToken)(dstname.Start),(IToken)(dstname.Stop)) : null)));
            	PushFollow(FOLLOW_fk_cols_in_reference_definition836);
            	fk_cols95 = fk_cols(fk);
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, fk_cols95.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:105:7: ( MATCH FULL | MATCH PARTIAL | MATCH SIMPLE )?
            	int alt41 = 4;
            	int LA41_0 = input.LA(1);

            	if ( (LA41_0 == MATCH) )
            	{
            	    switch ( input.LA(2) ) 
            	    {
            	        case FULL:
            	        	{
            	            alt41 = 1;
            	            }
            	            break;
            	        case PARTIAL:
            	        	{
            	            alt41 = 2;
            	            }
            	            break;
            	        case SIMPLE:
            	        	{
            	            alt41 = 3;
            	            }
            	            break;
            	    }

            	}
            	switch (alt41) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:105:8: MATCH FULL
            	        {
            	        	MATCH96=(IToken)Match(input,MATCH,FOLLOW_MATCH_in_reference_definition846); 
            	        		MATCH96_tree = (object)adaptor.Create(MATCH96);
            	        		adaptor.AddChild(root_0, MATCH96_tree);

            	        	FULL97=(IToken)Match(input,FULL,FOLLOW_FULL_in_reference_definition848); 
            	        		FULL97_tree = (object)adaptor.Create(FULL97);
            	        		adaptor.AddChild(root_0, FULL97_tree);


            	        }
            	        break;
            	    case 2 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:105:21: MATCH PARTIAL
            	        {
            	        	MATCH98=(IToken)Match(input,MATCH,FOLLOW_MATCH_in_reference_definition852); 
            	        		MATCH98_tree = (object)adaptor.Create(MATCH98);
            	        		adaptor.AddChild(root_0, MATCH98_tree);

            	        	PARTIAL99=(IToken)Match(input,PARTIAL,FOLLOW_PARTIAL_in_reference_definition854); 
            	        		PARTIAL99_tree = (object)adaptor.Create(PARTIAL99);
            	        		adaptor.AddChild(root_0, PARTIAL99_tree);


            	        }
            	        break;
            	    case 3 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:105:37: MATCH SIMPLE
            	        {
            	        	MATCH100=(IToken)Match(input,MATCH,FOLLOW_MATCH_in_reference_definition858); 
            	        		MATCH100_tree = (object)adaptor.Create(MATCH100);
            	        		adaptor.AddChild(root_0, MATCH100_tree);

            	        	SIMPLE101=(IToken)Match(input,SIMPLE,FOLLOW_SIMPLE_in_reference_definition860); 
            	        		SIMPLE101_tree = (object)adaptor.Create(SIMPLE101);
            	        		adaptor.AddChild(root_0, SIMPLE101_tree);


            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:106:7: ( ON DELETE ondel= reference_option )?
            	int alt42 = 2;
            	int LA42_0 = input.LA(1);

            	if ( (LA42_0 == ON) )
            	{
            	    int LA42_1 = input.LA(2);

            	    if ( (LA42_1 == DELETE) )
            	    {
            	        alt42 = 1;
            	    }
            	}
            	switch (alt42) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:106:8: ON DELETE ondel= reference_option
            	        {
            	        	ON102=(IToken)Match(input,ON,FOLLOW_ON_in_reference_definition871); 
            	        		ON102_tree = (object)adaptor.Create(ON102);
            	        		adaptor.AddChild(root_0, ON102_tree);

            	        	DELETE103=(IToken)Match(input,DELETE,FOLLOW_DELETE_in_reference_definition873); 
            	        		DELETE103_tree = (object)adaptor.Create(DELETE103);
            	        		adaptor.AddChild(root_0, DELETE103_tree);

            	        	PushFollow(FOLLOW_reference_option_in_reference_definition877);
            	        	ondel = reference_option();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, ondel.Tree);
            	        	fk.OnDeleteAction = ((ondel != null) ? ondel.action : default(ForeignKeyAction));

            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:107:7: ( ON UPDATE onupd= reference_option )?
            	int alt43 = 2;
            	int LA43_0 = input.LA(1);

            	if ( (LA43_0 == ON) )
            	{
            	    alt43 = 1;
            	}
            	switch (alt43) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:107:8: ON UPDATE onupd= reference_option
            	        {
            	        	ON104=(IToken)Match(input,ON,FOLLOW_ON_in_reference_definition891); 
            	        		ON104_tree = (object)adaptor.Create(ON104);
            	        		adaptor.AddChild(root_0, ON104_tree);

            	        	UPDATE105=(IToken)Match(input,UPDATE,FOLLOW_UPDATE_in_reference_definition893); 
            	        		UPDATE105_tree = (object)adaptor.Create(UPDATE105);
            	        		adaptor.AddChild(root_0, UPDATE105_tree);

            	        	PushFollow(FOLLOW_reference_option_in_reference_definition897);
            	        	onupd = reference_option();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, onupd.Tree);
            	        	fk.OnUpdateAction = ((onupd != null) ? onupd.action : default(ForeignKeyAction));

            	        }
            	        break;

            	}


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "reference_definition"

    public class reference_option_return : ParserRuleReturnScope
    {
        public ForeignKeyAction action;
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "reference_option"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:109:1: reference_option returns [ForeignKeyAction action] : ( RESTRICT | CASCADE | SET NULL | NO ACTION );
    public MySQLParser.reference_option_return reference_option() // throws RecognitionException [1]
    {   
        MySQLParser.reference_option_return retval = new MySQLParser.reference_option_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken RESTRICT106 = null;
        IToken CASCADE107 = null;
        IToken SET108 = null;
        IToken NULL109 = null;
        IToken NO110 = null;
        IToken ACTION111 = null;

        object RESTRICT106_tree=null;
        object CASCADE107_tree=null;
        object SET108_tree=null;
        object NULL109_tree=null;
        object NO110_tree=null;
        object ACTION111_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:109:51: ( RESTRICT | CASCADE | SET NULL | NO ACTION )
            int alt44 = 4;
            switch ( input.LA(1) ) 
            {
            case RESTRICT:
            	{
                alt44 = 1;
                }
                break;
            case CASCADE:
            	{
                alt44 = 2;
                }
                break;
            case SET:
            	{
                alt44 = 3;
                }
                break;
            case NO:
            	{
                alt44 = 4;
                }
                break;
            	default:
            	    NoViableAltException nvae_d44s0 =
            	        new NoViableAltException("", 44, 0, input);

            	    throw nvae_d44s0;
            }

            switch (alt44) 
            {
                case 1 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:110:5: RESTRICT
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	RESTRICT106=(IToken)Match(input,RESTRICT,FOLLOW_RESTRICT_in_reference_option916); 
                    		RESTRICT106_tree = (object)adaptor.Create(RESTRICT106);
                    		adaptor.AddChild(root_0, RESTRICT106_tree);

                    	retval.action =  ForeignKeyAction.Restrict;

                    }
                    break;
                case 2 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:111:5: CASCADE
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	CASCADE107=(IToken)Match(input,CASCADE,FOLLOW_CASCADE_in_reference_option924); 
                    		CASCADE107_tree = (object)adaptor.Create(CASCADE107);
                    		adaptor.AddChild(root_0, CASCADE107_tree);

                    	retval.action =  ForeignKeyAction.Cascade;

                    }
                    break;
                case 3 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:112:5: SET NULL
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	SET108=(IToken)Match(input,SET,FOLLOW_SET_in_reference_option932); 
                    		SET108_tree = (object)adaptor.Create(SET108);
                    		adaptor.AddChild(root_0, SET108_tree);

                    	NULL109=(IToken)Match(input,NULL,FOLLOW_NULL_in_reference_option934); 
                    		NULL109_tree = (object)adaptor.Create(NULL109);
                    		adaptor.AddChild(root_0, NULL109_tree);

                    	retval.action =  ForeignKeyAction.SetNull;

                    }
                    break;
                case 4 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:113:5: NO ACTION
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	NO110=(IToken)Match(input,NO,FOLLOW_NO_in_reference_option942); 
                    		NO110_tree = (object)adaptor.Create(NO110);
                    		adaptor.AddChild(root_0, NO110_tree);

                    	ACTION111=(IToken)Match(input,ACTION,FOLLOW_ACTION_in_reference_option944); 
                    		ACTION111_tree = (object)adaptor.Create(ACTION111);
                    		adaptor.AddChild(root_0, ACTION111_tree);

                    	retval.action =  ForeignKeyAction.NoAction;

                    }
                    break;

            }
            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "reference_option"

    public class number_flags_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "number_flags"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:116:1: number_flags[MySqlTypeNumber tp] : ( UNSIGNED )? ( ZEROFILL )? ;
    public MySQLParser.number_flags_return number_flags(MySqlTypeNumber tp) // throws RecognitionException [1]
    {   
        MySQLParser.number_flags_return retval = new MySQLParser.number_flags_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken UNSIGNED112 = null;
        IToken ZEROFILL113 = null;

        object UNSIGNED112_tree=null;
        object ZEROFILL113_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:116:33: ( ( UNSIGNED )? ( ZEROFILL )? )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:117:5: ( UNSIGNED )? ( ZEROFILL )?
            {
            	root_0 = (object)adaptor.GetNilNode();

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:117:5: ( UNSIGNED )?
            	int alt45 = 2;
            	int LA45_0 = input.LA(1);

            	if ( (LA45_0 == UNSIGNED) )
            	{
            	    alt45 = 1;
            	}
            	switch (alt45) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:117:6: UNSIGNED
            	        {
            	        	UNSIGNED112=(IToken)Match(input,UNSIGNED,FOLLOW_UNSIGNED_in_number_flags960); 
            	        		UNSIGNED112_tree = (object)adaptor.Create(UNSIGNED112);
            	        		adaptor.AddChild(root_0, UNSIGNED112_tree);

            	        	tp.Unsigned=true;

            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:117:37: ( ZEROFILL )?
            	int alt46 = 2;
            	int LA46_0 = input.LA(1);

            	if ( (LA46_0 == ZEROFILL) )
            	{
            	    alt46 = 1;
            	}
            	switch (alt46) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:117:38: ZEROFILL
            	        {
            	        	ZEROFILL113=(IToken)Match(input,ZEROFILL,FOLLOW_ZEROFILL_in_number_flags967); 
            	        		ZEROFILL113_tree = (object)adaptor.Create(ZEROFILL113);
            	        		adaptor.AddChild(root_0, ZEROFILL113_tree);

            	        	tp.Zerofill=true;

            	        }
            	        break;

            	}


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "number_flags"

    public class charset_collate_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "charset_collate"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:119:1: charset_collate[ColumnStructure col] : ( CHARACTER SET chset= id )? ( COLLATE coll= id )? ;
    public MySQLParser.charset_collate_return charset_collate(ColumnStructure col) // throws RecognitionException [1]
    {   
        MySQLParser.charset_collate_return retval = new MySQLParser.charset_collate_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken CHARACTER114 = null;
        IToken SET115 = null;
        IToken COLLATE116 = null;
        MySQLParser.id_return chset = default(MySQLParser.id_return);

        MySQLParser.id_return coll = default(MySQLParser.id_return);


        object CHARACTER114_tree=null;
        object SET115_tree=null;
        object COLLATE116_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:119:37: ( ( CHARACTER SET chset= id )? ( COLLATE coll= id )? )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:120:7: ( CHARACTER SET chset= id )? ( COLLATE coll= id )?
            {
            	root_0 = (object)adaptor.GetNilNode();

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:120:7: ( CHARACTER SET chset= id )?
            	int alt47 = 2;
            	int LA47_0 = input.LA(1);

            	if ( (LA47_0 == CHARACTER) )
            	{
            	    alt47 = 1;
            	}
            	switch (alt47) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:120:8: CHARACTER SET chset= id
            	        {
            	        	CHARACTER114=(IToken)Match(input,CHARACTER,FOLLOW_CHARACTER_in_charset_collate986); 
            	        		CHARACTER114_tree = (object)adaptor.Create(CHARACTER114);
            	        		adaptor.AddChild(root_0, CHARACTER114_tree);

            	        	SET115=(IToken)Match(input,SET,FOLLOW_SET_in_charset_collate988); 
            	        		SET115_tree = (object)adaptor.Create(SET115);
            	        		adaptor.AddChild(root_0, SET115_tree);

            	        	PushFollow(FOLLOW_id_in_charset_collate992);
            	        	chset = id();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, chset.Tree);
            	        	col.CharacterSet=((chset != null) ? input.ToString((IToken)(chset.Start),(IToken)(chset.Stop)) : null);

            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:121:7: ( COLLATE coll= id )?
            	int alt48 = 2;
            	int LA48_0 = input.LA(1);

            	if ( (LA48_0 == COLLATE) )
            	{
            	    alt48 = 1;
            	}
            	switch (alt48) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:121:8: COLLATE coll= id
            	        {
            	        	COLLATE116=(IToken)Match(input,COLLATE,FOLLOW_COLLATE_in_charset_collate1006); 
            	        		COLLATE116_tree = (object)adaptor.Create(COLLATE116);
            	        		adaptor.AddChild(root_0, COLLATE116_tree);

            	        	PushFollow(FOLLOW_id_in_charset_collate1010);
            	        	coll = id();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, coll.Tree);
            	        	col.Collation=((coll != null) ? input.ToString((IToken)(coll.Start),(IToken)(coll.Stop)) : null);

            	        }
            	        break;

            	}


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "charset_collate"

    public class data_type_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "data_type"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:123:1: data_type[ColumnStructure col] : ( BIT ( int_length[tp] )? | TINYINT ( int_length[tp] )? number_flags[tp] | SMALLINT ( int_length[tp] )? number_flags[tp] | MEDIUMINT ( int_length[tp] )? number_flags[tp] | INT ( int_length[tp] )? number_flags[tp] | INTEGER ( int_length[tp] )? number_flags[tp] | BIGINT ( int_length[tp] )? number_flags[tp] | REAL ( real_length[tp] )? number_flags[tp] | DOUBLE ( real_length[tp] )? number_flags[tp] | FLOAT ( real_length[tp] )? number_flags[tp] | DECIMAL ( real_length[tp] )? number_flags[tp] | NUMERIC ( real_length[tp] )? number_flags[tp] | DATE ( int_length[tp] )? | TIME ( int_length[tp] )? | TIMESTAMP ( int_length[tp] )? | DATETIME ( int_length[tp] )? | YEAR ( int_length[tp] )? | CHAR char_length[tp] charset_collate[col] | VARCHAR char_length[tp] charset_collate[col] | BINARY ( char_length[tp] )? | VARBINARY ( char_length[tp] )? | TINYBLOB | BLOB | MEDIUMBLOB | LONGBLOB | TINYTEXT ( BINARY )? charset_collate[col] | TEXT ( BINARY )? charset_collate[col] | MEDIUMTEXT ( BINARY )? charset_collate[col] | LONGTEXT ( BINARY )? charset_collate[col] | ENUM enumset_values[tp] charset_collate[col] | SET enumset_values[tp] charset_collate[col] | GEOMETRY | POINT | LINESTRING | POLYGON | MULTIPOINT | MULTILINESTRING | MULTIPOLYGON | GEOMETRYCOLLECTION );
    public MySQLParser.data_type_return data_type(ColumnStructure col) // throws RecognitionException [1]
    {   
        MySQLParser.data_type_return retval = new MySQLParser.data_type_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken BIT117 = null;
        IToken TINYINT119 = null;
        IToken SMALLINT122 = null;
        IToken MEDIUMINT125 = null;
        IToken INT128 = null;
        IToken INTEGER131 = null;
        IToken BIGINT134 = null;
        IToken REAL137 = null;
        IToken DOUBLE140 = null;
        IToken FLOAT143 = null;
        IToken DECIMAL146 = null;
        IToken NUMERIC149 = null;
        IToken DATE152 = null;
        IToken TIME154 = null;
        IToken TIMESTAMP156 = null;
        IToken DATETIME158 = null;
        IToken YEAR160 = null;
        IToken CHAR162 = null;
        IToken VARCHAR165 = null;
        IToken BINARY168 = null;
        IToken VARBINARY170 = null;
        IToken TINYBLOB172 = null;
        IToken BLOB173 = null;
        IToken MEDIUMBLOB174 = null;
        IToken LONGBLOB175 = null;
        IToken TINYTEXT176 = null;
        IToken BINARY177 = null;
        IToken TEXT179 = null;
        IToken BINARY180 = null;
        IToken MEDIUMTEXT182 = null;
        IToken BINARY183 = null;
        IToken LONGTEXT185 = null;
        IToken BINARY186 = null;
        IToken ENUM188 = null;
        IToken SET191 = null;
        IToken GEOMETRY194 = null;
        IToken POINT195 = null;
        IToken LINESTRING196 = null;
        IToken POLYGON197 = null;
        IToken MULTIPOINT198 = null;
        IToken MULTILINESTRING199 = null;
        IToken MULTIPOLYGON200 = null;
        IToken GEOMETRYCOLLECTION201 = null;
        MySQLParser.int_length_return int_length118 = default(MySQLParser.int_length_return);

        MySQLParser.int_length_return int_length120 = default(MySQLParser.int_length_return);

        MySQLParser.number_flags_return number_flags121 = default(MySQLParser.number_flags_return);

        MySQLParser.int_length_return int_length123 = default(MySQLParser.int_length_return);

        MySQLParser.number_flags_return number_flags124 = default(MySQLParser.number_flags_return);

        MySQLParser.int_length_return int_length126 = default(MySQLParser.int_length_return);

        MySQLParser.number_flags_return number_flags127 = default(MySQLParser.number_flags_return);

        MySQLParser.int_length_return int_length129 = default(MySQLParser.int_length_return);

        MySQLParser.number_flags_return number_flags130 = default(MySQLParser.number_flags_return);

        MySQLParser.int_length_return int_length132 = default(MySQLParser.int_length_return);

        MySQLParser.number_flags_return number_flags133 = default(MySQLParser.number_flags_return);

        MySQLParser.int_length_return int_length135 = default(MySQLParser.int_length_return);

        MySQLParser.number_flags_return number_flags136 = default(MySQLParser.number_flags_return);

        MySQLParser.real_length_return real_length138 = default(MySQLParser.real_length_return);

        MySQLParser.number_flags_return number_flags139 = default(MySQLParser.number_flags_return);

        MySQLParser.real_length_return real_length141 = default(MySQLParser.real_length_return);

        MySQLParser.number_flags_return number_flags142 = default(MySQLParser.number_flags_return);

        MySQLParser.real_length_return real_length144 = default(MySQLParser.real_length_return);

        MySQLParser.number_flags_return number_flags145 = default(MySQLParser.number_flags_return);

        MySQLParser.real_length_return real_length147 = default(MySQLParser.real_length_return);

        MySQLParser.number_flags_return number_flags148 = default(MySQLParser.number_flags_return);

        MySQLParser.real_length_return real_length150 = default(MySQLParser.real_length_return);

        MySQLParser.number_flags_return number_flags151 = default(MySQLParser.number_flags_return);

        MySQLParser.int_length_return int_length153 = default(MySQLParser.int_length_return);

        MySQLParser.int_length_return int_length155 = default(MySQLParser.int_length_return);

        MySQLParser.int_length_return int_length157 = default(MySQLParser.int_length_return);

        MySQLParser.int_length_return int_length159 = default(MySQLParser.int_length_return);

        MySQLParser.int_length_return int_length161 = default(MySQLParser.int_length_return);

        MySQLParser.char_length_return char_length163 = default(MySQLParser.char_length_return);

        MySQLParser.charset_collate_return charset_collate164 = default(MySQLParser.charset_collate_return);

        MySQLParser.char_length_return char_length166 = default(MySQLParser.char_length_return);

        MySQLParser.charset_collate_return charset_collate167 = default(MySQLParser.charset_collate_return);

        MySQLParser.char_length_return char_length169 = default(MySQLParser.char_length_return);

        MySQLParser.char_length_return char_length171 = default(MySQLParser.char_length_return);

        MySQLParser.charset_collate_return charset_collate178 = default(MySQLParser.charset_collate_return);

        MySQLParser.charset_collate_return charset_collate181 = default(MySQLParser.charset_collate_return);

        MySQLParser.charset_collate_return charset_collate184 = default(MySQLParser.charset_collate_return);

        MySQLParser.charset_collate_return charset_collate187 = default(MySQLParser.charset_collate_return);

        MySQLParser.enumset_values_return enumset_values189 = default(MySQLParser.enumset_values_return);

        MySQLParser.charset_collate_return charset_collate190 = default(MySQLParser.charset_collate_return);

        MySQLParser.enumset_values_return enumset_values192 = default(MySQLParser.enumset_values_return);

        MySQLParser.charset_collate_return charset_collate193 = default(MySQLParser.charset_collate_return);


        object BIT117_tree=null;
        object TINYINT119_tree=null;
        object SMALLINT122_tree=null;
        object MEDIUMINT125_tree=null;
        object INT128_tree=null;
        object INTEGER131_tree=null;
        object BIGINT134_tree=null;
        object REAL137_tree=null;
        object DOUBLE140_tree=null;
        object FLOAT143_tree=null;
        object DECIMAL146_tree=null;
        object NUMERIC149_tree=null;
        object DATE152_tree=null;
        object TIME154_tree=null;
        object TIMESTAMP156_tree=null;
        object DATETIME158_tree=null;
        object YEAR160_tree=null;
        object CHAR162_tree=null;
        object VARCHAR165_tree=null;
        object BINARY168_tree=null;
        object VARBINARY170_tree=null;
        object TINYBLOB172_tree=null;
        object BLOB173_tree=null;
        object MEDIUMBLOB174_tree=null;
        object LONGBLOB175_tree=null;
        object TINYTEXT176_tree=null;
        object BINARY177_tree=null;
        object TEXT179_tree=null;
        object BINARY180_tree=null;
        object MEDIUMTEXT182_tree=null;
        object BINARY183_tree=null;
        object LONGTEXT185_tree=null;
        object BINARY186_tree=null;
        object ENUM188_tree=null;
        object SET191_tree=null;
        object GEOMETRY194_tree=null;
        object POINT195_tree=null;
        object LINESTRING196_tree=null;
        object POLYGON197_tree=null;
        object MULTIPOINT198_tree=null;
        object MULTILINESTRING199_tree=null;
        object MULTIPOLYGON200_tree=null;
        object GEOMETRYCOLLECTION201_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:123:32: ( BIT ( int_length[tp] )? | TINYINT ( int_length[tp] )? number_flags[tp] | SMALLINT ( int_length[tp] )? number_flags[tp] | MEDIUMINT ( int_length[tp] )? number_flags[tp] | INT ( int_length[tp] )? number_flags[tp] | INTEGER ( int_length[tp] )? number_flags[tp] | BIGINT ( int_length[tp] )? number_flags[tp] | REAL ( real_length[tp] )? number_flags[tp] | DOUBLE ( real_length[tp] )? number_flags[tp] | FLOAT ( real_length[tp] )? number_flags[tp] | DECIMAL ( real_length[tp] )? number_flags[tp] | NUMERIC ( real_length[tp] )? number_flags[tp] | DATE ( int_length[tp] )? | TIME ( int_length[tp] )? | TIMESTAMP ( int_length[tp] )? | DATETIME ( int_length[tp] )? | YEAR ( int_length[tp] )? | CHAR char_length[tp] charset_collate[col] | VARCHAR char_length[tp] charset_collate[col] | BINARY ( char_length[tp] )? | VARBINARY ( char_length[tp] )? | TINYBLOB | BLOB | MEDIUMBLOB | LONGBLOB | TINYTEXT ( BINARY )? charset_collate[col] | TEXT ( BINARY )? charset_collate[col] | MEDIUMTEXT ( BINARY )? charset_collate[col] | LONGTEXT ( BINARY )? charset_collate[col] | ENUM enumset_values[tp] charset_collate[col] | SET enumset_values[tp] charset_collate[col] | GEOMETRY | POINT | LINESTRING | POLYGON | MULTIPOINT | MULTILINESTRING | MULTIPOLYGON | GEOMETRYCOLLECTION )
            int alt72 = 39;
            switch ( input.LA(1) ) 
            {
            case BIT:
            	{
                alt72 = 1;
                }
                break;
            case TINYINT:
            	{
                alt72 = 2;
                }
                break;
            case SMALLINT:
            	{
                alt72 = 3;
                }
                break;
            case MEDIUMINT:
            	{
                alt72 = 4;
                }
                break;
            case INT:
            	{
                alt72 = 5;
                }
                break;
            case INTEGER:
            	{
                alt72 = 6;
                }
                break;
            case BIGINT:
            	{
                alt72 = 7;
                }
                break;
            case REAL:
            	{
                alt72 = 8;
                }
                break;
            case DOUBLE:
            	{
                alt72 = 9;
                }
                break;
            case FLOAT:
            	{
                alt72 = 10;
                }
                break;
            case DECIMAL:
            	{
                alt72 = 11;
                }
                break;
            case NUMERIC:
            	{
                alt72 = 12;
                }
                break;
            case DATE:
            	{
                alt72 = 13;
                }
                break;
            case TIME:
            	{
                alt72 = 14;
                }
                break;
            case TIMESTAMP:
            	{
                alt72 = 15;
                }
                break;
            case DATETIME:
            	{
                alt72 = 16;
                }
                break;
            case YEAR:
            	{
                alt72 = 17;
                }
                break;
            case CHAR:
            	{
                alt72 = 18;
                }
                break;
            case VARCHAR:
            	{
                alt72 = 19;
                }
                break;
            case BINARY:
            	{
                alt72 = 20;
                }
                break;
            case VARBINARY:
            	{
                alt72 = 21;
                }
                break;
            case TINYBLOB:
            	{
                alt72 = 22;
                }
                break;
            case BLOB:
            	{
                alt72 = 23;
                }
                break;
            case MEDIUMBLOB:
            	{
                alt72 = 24;
                }
                break;
            case LONGBLOB:
            	{
                alt72 = 25;
                }
                break;
            case TINYTEXT:
            	{
                alt72 = 26;
                }
                break;
            case TEXT:
            	{
                alt72 = 27;
                }
                break;
            case MEDIUMTEXT:
            	{
                alt72 = 28;
                }
                break;
            case LONGTEXT:
            	{
                alt72 = 29;
                }
                break;
            case ENUM:
            	{
                alt72 = 30;
                }
                break;
            case SET:
            	{
                alt72 = 31;
                }
                break;
            case GEOMETRY:
            	{
                alt72 = 32;
                }
                break;
            case POINT:
            	{
                alt72 = 33;
                }
                break;
            case LINESTRING:
            	{
                alt72 = 34;
                }
                break;
            case POLYGON:
            	{
                alt72 = 35;
                }
                break;
            case MULTIPOINT:
            	{
                alt72 = 36;
                }
                break;
            case MULTILINESTRING:
            	{
                alt72 = 37;
                }
                break;
            case MULTIPOLYGON:
            	{
                alt72 = 38;
                }
                break;
            case GEOMETRYCOLLECTION:
            	{
                alt72 = 39;
                }
                break;
            	default:
            	    NoViableAltException nvae_d72s0 =
            	        new NoViableAltException("", 72, 0, input);

            	    throw nvae_d72s0;
            }

            switch (alt72) 
            {
                case 1 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:124:5: BIT ( int_length[tp] )?
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	BIT117=(IToken)Match(input,BIT,FOLLOW_BIT_in_data_type1028); 
                    		BIT117_tree = (object)adaptor.Create(BIT117);
                    		adaptor.AddChild(root_0, BIT117_tree);

                    	var tp=new MySqlTypeBit();
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:124:38: ( int_length[tp] )?
                    	int alt49 = 2;
                    	int LA49_0 = input.LA(1);

                    	if ( (LA49_0 == LPAREN) )
                    	{
                    	    alt49 = 1;
                    	}
                    	switch (alt49) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:124:38: int_length[tp]
                    	        {
                    	        	PushFollow(FOLLOW_int_length_in_data_type1032);
                    	        	int_length118 = int_length(tp);
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, int_length118.Tree);

                    	        }
                    	        break;

                    	}

                    	col.DataType=tp.ToGenericType();;

                    }
                    break;
                case 2 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:125:5: TINYINT ( int_length[tp] )? number_flags[tp]
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	TINYINT119=(IToken)Match(input,TINYINT,FOLLOW_TINYINT_in_data_type1042); 
                    		TINYINT119_tree = (object)adaptor.Create(TINYINT119);
                    		adaptor.AddChild(root_0, TINYINT119_tree);

                    	var tp=new MySqlTypeTinyInt();
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:125:46: ( int_length[tp] )?
                    	int alt50 = 2;
                    	int LA50_0 = input.LA(1);

                    	if ( (LA50_0 == LPAREN) )
                    	{
                    	    alt50 = 1;
                    	}
                    	switch (alt50) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:125:46: int_length[tp]
                    	        {
                    	        	PushFollow(FOLLOW_int_length_in_data_type1046);
                    	        	int_length120 = int_length(tp);
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, int_length120.Tree);

                    	        }
                    	        break;

                    	}

                    	PushFollow(FOLLOW_number_flags_in_data_type1050);
                    	number_flags121 = number_flags(tp);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, number_flags121.Tree);
                    	col.DataType=tp.ToGenericType();

                    }
                    break;
                case 3 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:126:5: SMALLINT ( int_length[tp] )? number_flags[tp]
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	SMALLINT122=(IToken)Match(input,SMALLINT,FOLLOW_SMALLINT_in_data_type1059); 
                    		SMALLINT122_tree = (object)adaptor.Create(SMALLINT122);
                    		adaptor.AddChild(root_0, SMALLINT122_tree);

                    	var tp=new MySqlTypeSmallInt();
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:126:48: ( int_length[tp] )?
                    	int alt51 = 2;
                    	int LA51_0 = input.LA(1);

                    	if ( (LA51_0 == LPAREN) )
                    	{
                    	    alt51 = 1;
                    	}
                    	switch (alt51) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:126:48: int_length[tp]
                    	        {
                    	        	PushFollow(FOLLOW_int_length_in_data_type1063);
                    	        	int_length123 = int_length(tp);
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, int_length123.Tree);

                    	        }
                    	        break;

                    	}

                    	PushFollow(FOLLOW_number_flags_in_data_type1067);
                    	number_flags124 = number_flags(tp);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, number_flags124.Tree);
                    	col.DataType=tp.ToGenericType();

                    }
                    break;
                case 4 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:127:5: MEDIUMINT ( int_length[tp] )? number_flags[tp]
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	MEDIUMINT125=(IToken)Match(input,MEDIUMINT,FOLLOW_MEDIUMINT_in_data_type1076); 
                    		MEDIUMINT125_tree = (object)adaptor.Create(MEDIUMINT125);
                    		adaptor.AddChild(root_0, MEDIUMINT125_tree);

                    	var tp=new MySqlTypeMediumInt();
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:127:50: ( int_length[tp] )?
                    	int alt52 = 2;
                    	int LA52_0 = input.LA(1);

                    	if ( (LA52_0 == LPAREN) )
                    	{
                    	    alt52 = 1;
                    	}
                    	switch (alt52) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:127:50: int_length[tp]
                    	        {
                    	        	PushFollow(FOLLOW_int_length_in_data_type1080);
                    	        	int_length126 = int_length(tp);
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, int_length126.Tree);

                    	        }
                    	        break;

                    	}

                    	PushFollow(FOLLOW_number_flags_in_data_type1084);
                    	number_flags127 = number_flags(tp);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, number_flags127.Tree);
                    	col.DataType=tp.ToGenericType();

                    }
                    break;
                case 5 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:128:5: INT ( int_length[tp] )? number_flags[tp]
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	INT128=(IToken)Match(input,INT,FOLLOW_INT_in_data_type1093); 
                    		INT128_tree = (object)adaptor.Create(INT128);
                    		adaptor.AddChild(root_0, INT128_tree);

                    	var tp=new MySqlTypeInt();
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:128:39: ( int_length[tp] )?
                    	int alt53 = 2;
                    	int LA53_0 = input.LA(1);

                    	if ( (LA53_0 == LPAREN) )
                    	{
                    	    alt53 = 1;
                    	}
                    	switch (alt53) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:128:39: int_length[tp]
                    	        {
                    	        	PushFollow(FOLLOW_int_length_in_data_type1098);
                    	        	int_length129 = int_length(tp);
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, int_length129.Tree);

                    	        }
                    	        break;

                    	}

                    	PushFollow(FOLLOW_number_flags_in_data_type1102);
                    	number_flags130 = number_flags(tp);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, number_flags130.Tree);
                    	col.DataType=tp.ToGenericType();

                    }
                    break;
                case 6 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:129:5: INTEGER ( int_length[tp] )? number_flags[tp]
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	INTEGER131=(IToken)Match(input,INTEGER,FOLLOW_INTEGER_in_data_type1111); 
                    		INTEGER131_tree = (object)adaptor.Create(INTEGER131);
                    		adaptor.AddChild(root_0, INTEGER131_tree);

                    	var tp=new MySqlTypeInt();
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:129:42: ( int_length[tp] )?
                    	int alt54 = 2;
                    	int LA54_0 = input.LA(1);

                    	if ( (LA54_0 == LPAREN) )
                    	{
                    	    alt54 = 1;
                    	}
                    	switch (alt54) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:129:42: int_length[tp]
                    	        {
                    	        	PushFollow(FOLLOW_int_length_in_data_type1115);
                    	        	int_length132 = int_length(tp);
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, int_length132.Tree);

                    	        }
                    	        break;

                    	}

                    	PushFollow(FOLLOW_number_flags_in_data_type1119);
                    	number_flags133 = number_flags(tp);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, number_flags133.Tree);
                    	col.DataType=tp.ToGenericType();

                    }
                    break;
                case 7 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:130:5: BIGINT ( int_length[tp] )? number_flags[tp]
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	BIGINT134=(IToken)Match(input,BIGINT,FOLLOW_BIGINT_in_data_type1128); 
                    		BIGINT134_tree = (object)adaptor.Create(BIGINT134);
                    		adaptor.AddChild(root_0, BIGINT134_tree);

                    	var tp=new MySqlTypeBigInt();
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:130:44: ( int_length[tp] )?
                    	int alt55 = 2;
                    	int LA55_0 = input.LA(1);

                    	if ( (LA55_0 == LPAREN) )
                    	{
                    	    alt55 = 1;
                    	}
                    	switch (alt55) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:130:44: int_length[tp]
                    	        {
                    	        	PushFollow(FOLLOW_int_length_in_data_type1132);
                    	        	int_length135 = int_length(tp);
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, int_length135.Tree);

                    	        }
                    	        break;

                    	}

                    	PushFollow(FOLLOW_number_flags_in_data_type1137);
                    	number_flags136 = number_flags(tp);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, number_flags136.Tree);
                    	col.DataType=tp.ToGenericType();

                    }
                    break;
                case 8 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:131:5: REAL ( real_length[tp] )? number_flags[tp]
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	REAL137=(IToken)Match(input,REAL,FOLLOW_REAL_in_data_type1146); 
                    		REAL137_tree = (object)adaptor.Create(REAL137);
                    		adaptor.AddChild(root_0, REAL137_tree);

                    	var tp=new MySqlTypeDouble();
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:131:42: ( real_length[tp] )?
                    	int alt56 = 2;
                    	int LA56_0 = input.LA(1);

                    	if ( (LA56_0 == LPAREN) )
                    	{
                    	    alt56 = 1;
                    	}
                    	switch (alt56) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:131:42: real_length[tp]
                    	        {
                    	        	PushFollow(FOLLOW_real_length_in_data_type1150);
                    	        	real_length138 = real_length(tp);
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, real_length138.Tree);

                    	        }
                    	        break;

                    	}

                    	PushFollow(FOLLOW_number_flags_in_data_type1154);
                    	number_flags139 = number_flags(tp);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, number_flags139.Tree);
                    	col.DataType=tp.ToGenericType();

                    }
                    break;
                case 9 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:132:5: DOUBLE ( real_length[tp] )? number_flags[tp]
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	DOUBLE140=(IToken)Match(input,DOUBLE,FOLLOW_DOUBLE_in_data_type1163); 
                    		DOUBLE140_tree = (object)adaptor.Create(DOUBLE140);
                    		adaptor.AddChild(root_0, DOUBLE140_tree);

                    	var tp=new MySqlTypeDouble();
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:132:44: ( real_length[tp] )?
                    	int alt57 = 2;
                    	int LA57_0 = input.LA(1);

                    	if ( (LA57_0 == LPAREN) )
                    	{
                    	    alt57 = 1;
                    	}
                    	switch (alt57) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:132:44: real_length[tp]
                    	        {
                    	        	PushFollow(FOLLOW_real_length_in_data_type1167);
                    	        	real_length141 = real_length(tp);
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, real_length141.Tree);

                    	        }
                    	        break;

                    	}

                    	PushFollow(FOLLOW_number_flags_in_data_type1171);
                    	number_flags142 = number_flags(tp);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, number_flags142.Tree);
                    	col.DataType=tp.ToGenericType();

                    }
                    break;
                case 10 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:133:5: FLOAT ( real_length[tp] )? number_flags[tp]
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	FLOAT143=(IToken)Match(input,FLOAT,FOLLOW_FLOAT_in_data_type1180); 
                    		FLOAT143_tree = (object)adaptor.Create(FLOAT143);
                    		adaptor.AddChild(root_0, FLOAT143_tree);

                    	var tp=new MySqlTypeFloat();
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:133:42: ( real_length[tp] )?
                    	int alt58 = 2;
                    	int LA58_0 = input.LA(1);

                    	if ( (LA58_0 == LPAREN) )
                    	{
                    	    alt58 = 1;
                    	}
                    	switch (alt58) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:133:42: real_length[tp]
                    	        {
                    	        	PushFollow(FOLLOW_real_length_in_data_type1184);
                    	        	real_length144 = real_length(tp);
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, real_length144.Tree);

                    	        }
                    	        break;

                    	}

                    	PushFollow(FOLLOW_number_flags_in_data_type1188);
                    	number_flags145 = number_flags(tp);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, number_flags145.Tree);
                    	col.DataType=tp.ToGenericType();

                    }
                    break;
                case 11 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:134:5: DECIMAL ( real_length[tp] )? number_flags[tp]
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	DECIMAL146=(IToken)Match(input,DECIMAL,FOLLOW_DECIMAL_in_data_type1197); 
                    		DECIMAL146_tree = (object)adaptor.Create(DECIMAL146);
                    		adaptor.AddChild(root_0, DECIMAL146_tree);

                    	var tp=new MySqlTypeDecimal();
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:134:46: ( real_length[tp] )?
                    	int alt59 = 2;
                    	int LA59_0 = input.LA(1);

                    	if ( (LA59_0 == LPAREN) )
                    	{
                    	    alt59 = 1;
                    	}
                    	switch (alt59) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:134:46: real_length[tp]
                    	        {
                    	        	PushFollow(FOLLOW_real_length_in_data_type1201);
                    	        	real_length147 = real_length(tp);
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, real_length147.Tree);

                    	        }
                    	        break;

                    	}

                    	PushFollow(FOLLOW_number_flags_in_data_type1205);
                    	number_flags148 = number_flags(tp);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, number_flags148.Tree);
                    	col.DataType=tp.ToGenericType();

                    }
                    break;
                case 12 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:135:5: NUMERIC ( real_length[tp] )? number_flags[tp]
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	NUMERIC149=(IToken)Match(input,NUMERIC,FOLLOW_NUMERIC_in_data_type1214); 
                    		NUMERIC149_tree = (object)adaptor.Create(NUMERIC149);
                    		adaptor.AddChild(root_0, NUMERIC149_tree);

                    	var tp=new MySqlTypeNumeric();
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:135:47: ( real_length[tp] )?
                    	int alt60 = 2;
                    	int LA60_0 = input.LA(1);

                    	if ( (LA60_0 == LPAREN) )
                    	{
                    	    alt60 = 1;
                    	}
                    	switch (alt60) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:135:47: real_length[tp]
                    	        {
                    	        	PushFollow(FOLLOW_real_length_in_data_type1219);
                    	        	real_length150 = real_length(tp);
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, real_length150.Tree);

                    	        }
                    	        break;

                    	}

                    	PushFollow(FOLLOW_number_flags_in_data_type1223);
                    	number_flags151 = number_flags(tp);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, number_flags151.Tree);
                    	col.DataType=tp.ToGenericType();

                    }
                    break;
                case 13 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:136:5: DATE ( int_length[tp] )?
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	DATE152=(IToken)Match(input,DATE,FOLLOW_DATE_in_data_type1232); 
                    		DATE152_tree = (object)adaptor.Create(DATE152);
                    		adaptor.AddChild(root_0, DATE152_tree);

                    	var tp=new MySqlTypeDate();
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:136:40: ( int_length[tp] )?
                    	int alt61 = 2;
                    	int LA61_0 = input.LA(1);

                    	if ( (LA61_0 == LPAREN) )
                    	{
                    	    alt61 = 1;
                    	}
                    	switch (alt61) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:136:40: int_length[tp]
                    	        {
                    	        	PushFollow(FOLLOW_int_length_in_data_type1236);
                    	        	int_length153 = int_length(tp);
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, int_length153.Tree);

                    	        }
                    	        break;

                    	}

                    	col.DataType=tp.ToGenericType();

                    }
                    break;
                case 14 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:137:5: TIME ( int_length[tp] )?
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	TIME154=(IToken)Match(input,TIME,FOLLOW_TIME_in_data_type1246); 
                    		TIME154_tree = (object)adaptor.Create(TIME154);
                    		adaptor.AddChild(root_0, TIME154_tree);

                    	var tp=new MySqlTypeTime();
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:137:40: ( int_length[tp] )?
                    	int alt62 = 2;
                    	int LA62_0 = input.LA(1);

                    	if ( (LA62_0 == LPAREN) )
                    	{
                    	    alt62 = 1;
                    	}
                    	switch (alt62) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:137:40: int_length[tp]
                    	        {
                    	        	PushFollow(FOLLOW_int_length_in_data_type1250);
                    	        	int_length155 = int_length(tp);
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, int_length155.Tree);

                    	        }
                    	        break;

                    	}

                    	col.DataType=tp.ToGenericType();

                    }
                    break;
                case 15 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:138:5: TIMESTAMP ( int_length[tp] )?
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	TIMESTAMP156=(IToken)Match(input,TIMESTAMP,FOLLOW_TIMESTAMP_in_data_type1260); 
                    		TIMESTAMP156_tree = (object)adaptor.Create(TIMESTAMP156);
                    		adaptor.AddChild(root_0, TIMESTAMP156_tree);

                    	var tp=new MySqlTypeTimestamp();
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:138:50: ( int_length[tp] )?
                    	int alt63 = 2;
                    	int LA63_0 = input.LA(1);

                    	if ( (LA63_0 == LPAREN) )
                    	{
                    	    alt63 = 1;
                    	}
                    	switch (alt63) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:138:50: int_length[tp]
                    	        {
                    	        	PushFollow(FOLLOW_int_length_in_data_type1264);
                    	        	int_length157 = int_length(tp);
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, int_length157.Tree);

                    	        }
                    	        break;

                    	}

                    	col.DataType=tp.ToGenericType();

                    }
                    break;
                case 16 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:139:5: DATETIME ( int_length[tp] )?
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	DATETIME158=(IToken)Match(input,DATETIME,FOLLOW_DATETIME_in_data_type1274); 
                    		DATETIME158_tree = (object)adaptor.Create(DATETIME158);
                    		adaptor.AddChild(root_0, DATETIME158_tree);

                    	var tp=new MySqlTypeDatetime();
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:139:48: ( int_length[tp] )?
                    	int alt64 = 2;
                    	int LA64_0 = input.LA(1);

                    	if ( (LA64_0 == LPAREN) )
                    	{
                    	    alt64 = 1;
                    	}
                    	switch (alt64) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:139:48: int_length[tp]
                    	        {
                    	        	PushFollow(FOLLOW_int_length_in_data_type1278);
                    	        	int_length159 = int_length(tp);
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, int_length159.Tree);

                    	        }
                    	        break;

                    	}

                    	col.DataType=tp.ToGenericType();

                    }
                    break;
                case 17 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:140:5: YEAR ( int_length[tp] )?
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	YEAR160=(IToken)Match(input,YEAR,FOLLOW_YEAR_in_data_type1288); 
                    		YEAR160_tree = (object)adaptor.Create(YEAR160);
                    		adaptor.AddChild(root_0, YEAR160_tree);

                    	var tp=new MySqlTypeYear();
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:140:40: ( int_length[tp] )?
                    	int alt65 = 2;
                    	int LA65_0 = input.LA(1);

                    	if ( (LA65_0 == LPAREN) )
                    	{
                    	    alt65 = 1;
                    	}
                    	switch (alt65) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:140:40: int_length[tp]
                    	        {
                    	        	PushFollow(FOLLOW_int_length_in_data_type1292);
                    	        	int_length161 = int_length(tp);
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, int_length161.Tree);

                    	        }
                    	        break;

                    	}

                    	col.DataType=tp.ToGenericType();

                    }
                    break;
                case 18 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:141:5: CHAR char_length[tp] charset_collate[col]
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	CHAR162=(IToken)Match(input,CHAR,FOLLOW_CHAR_in_data_type1302); 
                    		CHAR162_tree = (object)adaptor.Create(CHAR162);
                    		adaptor.AddChild(root_0, CHAR162_tree);

                    	var tp=new MySqlTypeChar();
                    	PushFollow(FOLLOW_char_length_in_data_type1306);
                    	char_length163 = char_length(tp);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, char_length163.Tree);
                    	PushFollow(FOLLOW_charset_collate_in_data_type1309);
                    	charset_collate164 = charset_collate(col);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, charset_collate164.Tree);
                    	col.DataType=tp.ToGenericType();

                    }
                    break;
                case 19 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:142:5: VARCHAR char_length[tp] charset_collate[col]
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	VARCHAR165=(IToken)Match(input,VARCHAR,FOLLOW_VARCHAR_in_data_type1318); 
                    		VARCHAR165_tree = (object)adaptor.Create(VARCHAR165);
                    		adaptor.AddChild(root_0, VARCHAR165_tree);

                    	var tp=new MySqlTypeVarChar();
                    	PushFollow(FOLLOW_char_length_in_data_type1322);
                    	char_length166 = char_length(tp);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, char_length166.Tree);
                    	PushFollow(FOLLOW_charset_collate_in_data_type1325);
                    	charset_collate167 = charset_collate(col);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, charset_collate167.Tree);
                    	col.DataType=tp.ToGenericType();

                    }
                    break;
                case 20 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:143:5: BINARY ( char_length[tp] )?
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	BINARY168=(IToken)Match(input,BINARY,FOLLOW_BINARY_in_data_type1334); 
                    		BINARY168_tree = (object)adaptor.Create(BINARY168);
                    		adaptor.AddChild(root_0, BINARY168_tree);

                    	var tp=new MySqlTypeBinary();
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:143:44: ( char_length[tp] )?
                    	int alt66 = 2;
                    	int LA66_0 = input.LA(1);

                    	if ( (LA66_0 == LPAREN) )
                    	{
                    	    alt66 = 1;
                    	}
                    	switch (alt66) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:143:44: char_length[tp]
                    	        {
                    	        	PushFollow(FOLLOW_char_length_in_data_type1338);
                    	        	char_length169 = char_length(tp);
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, char_length169.Tree);

                    	        }
                    	        break;

                    	}

                    	col.DataType=tp.ToGenericType();

                    }
                    break;
                case 21 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:144:5: VARBINARY ( char_length[tp] )?
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	VARBINARY170=(IToken)Match(input,VARBINARY,FOLLOW_VARBINARY_in_data_type1348); 
                    		VARBINARY170_tree = (object)adaptor.Create(VARBINARY170);
                    		adaptor.AddChild(root_0, VARBINARY170_tree);

                    	var tp=new MySqlTypeVarBinary();
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:144:50: ( char_length[tp] )?
                    	int alt67 = 2;
                    	int LA67_0 = input.LA(1);

                    	if ( (LA67_0 == LPAREN) )
                    	{
                    	    alt67 = 1;
                    	}
                    	switch (alt67) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:144:50: char_length[tp]
                    	        {
                    	        	PushFollow(FOLLOW_char_length_in_data_type1352);
                    	        	char_length171 = char_length(tp);
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, char_length171.Tree);

                    	        }
                    	        break;

                    	}

                    	col.DataType=tp.ToGenericType();

                    }
                    break;
                case 22 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:145:5: TINYBLOB
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	TINYBLOB172=(IToken)Match(input,TINYBLOB,FOLLOW_TINYBLOB_in_data_type1362); 
                    		TINYBLOB172_tree = (object)adaptor.Create(TINYBLOB172);
                    		adaptor.AddChild(root_0, TINYBLOB172_tree);

                    	var tp=new MySqlTypeTinyBlob();col.DataType=tp.ToGenericType();

                    }
                    break;
                case 23 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:146:5: BLOB
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	BLOB173=(IToken)Match(input,BLOB,FOLLOW_BLOB_in_data_type1370); 
                    		BLOB173_tree = (object)adaptor.Create(BLOB173);
                    		adaptor.AddChild(root_0, BLOB173_tree);

                    	var tp=new MySqlTypeBlob();col.DataType=tp.ToGenericType();

                    }
                    break;
                case 24 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:147:5: MEDIUMBLOB
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	MEDIUMBLOB174=(IToken)Match(input,MEDIUMBLOB,FOLLOW_MEDIUMBLOB_in_data_type1378); 
                    		MEDIUMBLOB174_tree = (object)adaptor.Create(MEDIUMBLOB174);
                    		adaptor.AddChild(root_0, MEDIUMBLOB174_tree);

                    	var tp=new MySqlTypeMediumBlob();col.DataType=tp.ToGenericType();

                    }
                    break;
                case 25 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:148:5: LONGBLOB
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	LONGBLOB175=(IToken)Match(input,LONGBLOB,FOLLOW_LONGBLOB_in_data_type1386); 
                    		LONGBLOB175_tree = (object)adaptor.Create(LONGBLOB175);
                    		adaptor.AddChild(root_0, LONGBLOB175_tree);

                    	var tp=new MySqlTypeLongBlob();col.DataType=tp.ToGenericType();

                    }
                    break;
                case 26 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:149:5: TINYTEXT ( BINARY )? charset_collate[col]
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	TINYTEXT176=(IToken)Match(input,TINYTEXT,FOLLOW_TINYTEXT_in_data_type1394); 
                    		TINYTEXT176_tree = (object)adaptor.Create(TINYTEXT176);
                    		adaptor.AddChild(root_0, TINYTEXT176_tree);

                    	var tp=new MySqlTypeTinyText();
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:149:49: ( BINARY )?
                    	int alt68 = 2;
                    	int LA68_0 = input.LA(1);

                    	if ( (LA68_0 == BINARY) )
                    	{
                    	    alt68 = 1;
                    	}
                    	switch (alt68) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:149:49: BINARY
                    	        {
                    	        	BINARY177=(IToken)Match(input,BINARY,FOLLOW_BINARY_in_data_type1399); 
                    	        		BINARY177_tree = (object)adaptor.Create(BINARY177);
                    	        		adaptor.AddChild(root_0, BINARY177_tree);


                    	        }
                    	        break;

                    	}

                    	PushFollow(FOLLOW_charset_collate_in_data_type1402);
                    	charset_collate178 = charset_collate(col);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, charset_collate178.Tree);
                    	col.DataType=tp.ToGenericType();

                    }
                    break;
                case 27 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:150:5: TEXT ( BINARY )? charset_collate[col]
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	TEXT179=(IToken)Match(input,TEXT,FOLLOW_TEXT_in_data_type1411); 
                    		TEXT179_tree = (object)adaptor.Create(TEXT179);
                    		adaptor.AddChild(root_0, TEXT179_tree);

                    	var tp=new MySqlTypeText();
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:150:41: ( BINARY )?
                    	int alt69 = 2;
                    	int LA69_0 = input.LA(1);

                    	if ( (LA69_0 == BINARY) )
                    	{
                    	    alt69 = 1;
                    	}
                    	switch (alt69) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:150:41: BINARY
                    	        {
                    	        	BINARY180=(IToken)Match(input,BINARY,FOLLOW_BINARY_in_data_type1416); 
                    	        		BINARY180_tree = (object)adaptor.Create(BINARY180);
                    	        		adaptor.AddChild(root_0, BINARY180_tree);


                    	        }
                    	        break;

                    	}

                    	PushFollow(FOLLOW_charset_collate_in_data_type1419);
                    	charset_collate181 = charset_collate(col);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, charset_collate181.Tree);
                    	col.DataType=tp.ToGenericType();

                    }
                    break;
                case 28 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:151:5: MEDIUMTEXT ( BINARY )? charset_collate[col]
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	MEDIUMTEXT182=(IToken)Match(input,MEDIUMTEXT,FOLLOW_MEDIUMTEXT_in_data_type1428); 
                    		MEDIUMTEXT182_tree = (object)adaptor.Create(MEDIUMTEXT182);
                    		adaptor.AddChild(root_0, MEDIUMTEXT182_tree);

                    	var tp=new MySqlTypeMediumText();
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:151:53: ( BINARY )?
                    	int alt70 = 2;
                    	int LA70_0 = input.LA(1);

                    	if ( (LA70_0 == BINARY) )
                    	{
                    	    alt70 = 1;
                    	}
                    	switch (alt70) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:151:53: BINARY
                    	        {
                    	        	BINARY183=(IToken)Match(input,BINARY,FOLLOW_BINARY_in_data_type1433); 
                    	        		BINARY183_tree = (object)adaptor.Create(BINARY183);
                    	        		adaptor.AddChild(root_0, BINARY183_tree);


                    	        }
                    	        break;

                    	}

                    	PushFollow(FOLLOW_charset_collate_in_data_type1436);
                    	charset_collate184 = charset_collate(col);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, charset_collate184.Tree);
                    	col.DataType=tp.ToGenericType();

                    }
                    break;
                case 29 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:152:5: LONGTEXT ( BINARY )? charset_collate[col]
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	LONGTEXT185=(IToken)Match(input,LONGTEXT,FOLLOW_LONGTEXT_in_data_type1445); 
                    		LONGTEXT185_tree = (object)adaptor.Create(LONGTEXT185);
                    		adaptor.AddChild(root_0, LONGTEXT185_tree);

                    	var tp=new MySqlTypeLongText();
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:152:48: ( BINARY )?
                    	int alt71 = 2;
                    	int LA71_0 = input.LA(1);

                    	if ( (LA71_0 == BINARY) )
                    	{
                    	    alt71 = 1;
                    	}
                    	switch (alt71) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:152:48: BINARY
                    	        {
                    	        	BINARY186=(IToken)Match(input,BINARY,FOLLOW_BINARY_in_data_type1449); 
                    	        		BINARY186_tree = (object)adaptor.Create(BINARY186);
                    	        		adaptor.AddChild(root_0, BINARY186_tree);


                    	        }
                    	        break;

                    	}

                    	PushFollow(FOLLOW_charset_collate_in_data_type1452);
                    	charset_collate187 = charset_collate(col);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, charset_collate187.Tree);
                    	col.DataType=tp.ToGenericType();

                    }
                    break;
                case 30 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:153:5: ENUM enumset_values[tp] charset_collate[col]
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	ENUM188=(IToken)Match(input,ENUM,FOLLOW_ENUM_in_data_type1461); 
                    		ENUM188_tree = (object)adaptor.Create(ENUM188);
                    		adaptor.AddChild(root_0, ENUM188_tree);

                    	var tp=new MySqlTypeEnum();
                    	PushFollow(FOLLOW_enumset_values_in_data_type1466);
                    	enumset_values189 = enumset_values(tp);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, enumset_values189.Tree);
                    	PushFollow(FOLLOW_charset_collate_in_data_type1469);
                    	charset_collate190 = charset_collate(col);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, charset_collate190.Tree);
                    	col.DataType=tp.ToGenericType();

                    }
                    break;
                case 31 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:154:5: SET enumset_values[tp] charset_collate[col]
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	SET191=(IToken)Match(input,SET,FOLLOW_SET_in_data_type1478); 
                    		SET191_tree = (object)adaptor.Create(SET191);
                    		adaptor.AddChild(root_0, SET191_tree);

                    	var tp=new MySqlTypeSet();
                    	PushFollow(FOLLOW_enumset_values_in_data_type1483);
                    	enumset_values192 = enumset_values(tp);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, enumset_values192.Tree);
                    	PushFollow(FOLLOW_charset_collate_in_data_type1486);
                    	charset_collate193 = charset_collate(col);
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, charset_collate193.Tree);
                    	col.DataType=tp.ToGenericType();

                    }
                    break;
                case 32 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:155:5: GEOMETRY
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	GEOMETRY194=(IToken)Match(input,GEOMETRY,FOLLOW_GEOMETRY_in_data_type1495); 
                    		GEOMETRY194_tree = (object)adaptor.Create(GEOMETRY194);
                    		adaptor.AddChild(root_0, GEOMETRY194_tree);

                    	var tp=new MySqlTypeGeometry();col.DataType=tp.ToGenericType();

                    }
                    break;
                case 33 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:156:5: POINT
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	POINT195=(IToken)Match(input,POINT,FOLLOW_POINT_in_data_type1503); 
                    		POINT195_tree = (object)adaptor.Create(POINT195);
                    		adaptor.AddChild(root_0, POINT195_tree);

                    	var tp=new MySqlTypePoint();col.DataType=tp.ToGenericType();

                    }
                    break;
                case 34 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:157:5: LINESTRING
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	LINESTRING196=(IToken)Match(input,LINESTRING,FOLLOW_LINESTRING_in_data_type1511); 
                    		LINESTRING196_tree = (object)adaptor.Create(LINESTRING196);
                    		adaptor.AddChild(root_0, LINESTRING196_tree);

                    	var tp=new MySqlTypeLineString();col.DataType=tp.ToGenericType();

                    }
                    break;
                case 35 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:158:5: POLYGON
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	POLYGON197=(IToken)Match(input,POLYGON,FOLLOW_POLYGON_in_data_type1519); 
                    		POLYGON197_tree = (object)adaptor.Create(POLYGON197);
                    		adaptor.AddChild(root_0, POLYGON197_tree);

                    	var tp=new MySqlTypePolygon();col.DataType=tp.ToGenericType();

                    }
                    break;
                case 36 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:159:5: MULTIPOINT
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	MULTIPOINT198=(IToken)Match(input,MULTIPOINT,FOLLOW_MULTIPOINT_in_data_type1527); 
                    		MULTIPOINT198_tree = (object)adaptor.Create(MULTIPOINT198);
                    		adaptor.AddChild(root_0, MULTIPOINT198_tree);

                    	var tp=new MySqlTypeMultiPoint();col.DataType=tp.ToGenericType();

                    }
                    break;
                case 37 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:160:5: MULTILINESTRING
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	MULTILINESTRING199=(IToken)Match(input,MULTILINESTRING,FOLLOW_MULTILINESTRING_in_data_type1535); 
                    		MULTILINESTRING199_tree = (object)adaptor.Create(MULTILINESTRING199);
                    		adaptor.AddChild(root_0, MULTILINESTRING199_tree);

                    	var tp=new MySqlTypeMultiLineString();col.DataType=tp.ToGenericType();

                    }
                    break;
                case 38 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:161:5: MULTIPOLYGON
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	MULTIPOLYGON200=(IToken)Match(input,MULTIPOLYGON,FOLLOW_MULTIPOLYGON_in_data_type1543); 
                    		MULTIPOLYGON200_tree = (object)adaptor.Create(MULTIPOLYGON200);
                    		adaptor.AddChild(root_0, MULTIPOLYGON200_tree);

                    	var tp=new MySqlTypeMultiPolygon();col.DataType=tp.ToGenericType();

                    }
                    break;
                case 39 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:162:5: GEOMETRYCOLLECTION
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	GEOMETRYCOLLECTION201=(IToken)Match(input,GEOMETRYCOLLECTION,FOLLOW_GEOMETRYCOLLECTION_in_data_type1551); 
                    		GEOMETRYCOLLECTION201_tree = (object)adaptor.Create(GEOMETRYCOLLECTION201);
                    		adaptor.AddChild(root_0, GEOMETRYCOLLECTION201_tree);

                    	var tp=new MySqlTypeGeometryCollection();col.DataType=tp.ToGenericType();

                    }
                    break;

            }
            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "data_type"

    public class int_length_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "int_length"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:165:1: int_length[MySqlTypeBase tp] : LPAREN len= T_INTEGER RPAREN ;
    public MySQLParser.int_length_return int_length(MySqlTypeBase tp) // throws RecognitionException [1]
    {   
        MySQLParser.int_length_return retval = new MySQLParser.int_length_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken len = null;
        IToken LPAREN202 = null;
        IToken RPAREN203 = null;

        object len_tree=null;
        object LPAREN202_tree=null;
        object RPAREN203_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:165:30: ( LPAREN len= T_INTEGER RPAREN )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:165:32: LPAREN len= T_INTEGER RPAREN
            {
            	root_0 = (object)adaptor.GetNilNode();

            	LPAREN202=(IToken)Match(input,LPAREN,FOLLOW_LPAREN_in_int_length1563); 
            		LPAREN202_tree = (object)adaptor.Create(LPAREN202);
            		adaptor.AddChild(root_0, LPAREN202_tree);

            	len=(IToken)Match(input,T_INTEGER,FOLLOW_T_INTEGER_in_int_length1567); 
            		len_tree = (object)adaptor.Create(len);
            		adaptor.AddChild(root_0, len_tree);

            	tp.SetLength(((len != null) ? len.Text : null));
            	RPAREN203=(IToken)Match(input,RPAREN,FOLLOW_RPAREN_in_int_length1571); 
            		RPAREN203_tree = (object)adaptor.Create(RPAREN203);
            		adaptor.AddChild(root_0, RPAREN203_tree);


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "int_length"

    public class char_length_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "char_length"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:166:1: char_length[MySqlTypeBase tp] : LPAREN len= T_INTEGER RPAREN ;
    public MySQLParser.char_length_return char_length(MySqlTypeBase tp) // throws RecognitionException [1]
    {   
        MySQLParser.char_length_return retval = new MySQLParser.char_length_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken len = null;
        IToken LPAREN204 = null;
        IToken RPAREN205 = null;

        object len_tree=null;
        object LPAREN204_tree=null;
        object RPAREN205_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:166:31: ( LPAREN len= T_INTEGER RPAREN )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:166:33: LPAREN len= T_INTEGER RPAREN
            {
            	root_0 = (object)adaptor.GetNilNode();

            	LPAREN204=(IToken)Match(input,LPAREN,FOLLOW_LPAREN_in_char_length1579); 
            		LPAREN204_tree = (object)adaptor.Create(LPAREN204);
            		adaptor.AddChild(root_0, LPAREN204_tree);

            	len=(IToken)Match(input,T_INTEGER,FOLLOW_T_INTEGER_in_char_length1583); 
            		len_tree = (object)adaptor.Create(len);
            		adaptor.AddChild(root_0, len_tree);

            	tp.SetLength(((len != null) ? len.Text : null));
            	RPAREN205=(IToken)Match(input,RPAREN,FOLLOW_RPAREN_in_char_length1587); 
            		RPAREN205_tree = (object)adaptor.Create(RPAREN205);
            		adaptor.AddChild(root_0, RPAREN205_tree);


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "char_length"

    public class real_length_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "real_length"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:167:1: real_length[MySqlTypeBase tp] : LPAREN len= T_INTEGER ( COMMA dec= T_INTEGER )? RPAREN ;
    public MySQLParser.real_length_return real_length(MySqlTypeBase tp) // throws RecognitionException [1]
    {   
        MySQLParser.real_length_return retval = new MySQLParser.real_length_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken len = null;
        IToken dec = null;
        IToken LPAREN206 = null;
        IToken COMMA207 = null;
        IToken RPAREN208 = null;

        object len_tree=null;
        object dec_tree=null;
        object LPAREN206_tree=null;
        object COMMA207_tree=null;
        object RPAREN208_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:167:31: ( LPAREN len= T_INTEGER ( COMMA dec= T_INTEGER )? RPAREN )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:167:33: LPAREN len= T_INTEGER ( COMMA dec= T_INTEGER )? RPAREN
            {
            	root_0 = (object)adaptor.GetNilNode();

            	LPAREN206=(IToken)Match(input,LPAREN,FOLLOW_LPAREN_in_real_length1595); 
            		LPAREN206_tree = (object)adaptor.Create(LPAREN206);
            		adaptor.AddChild(root_0, LPAREN206_tree);

            	len=(IToken)Match(input,T_INTEGER,FOLLOW_T_INTEGER_in_real_length1599); 
            		len_tree = (object)adaptor.Create(len);
            		adaptor.AddChild(root_0, len_tree);

            	tp.SetLength(((len != null) ? len.Text : null));
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:167:81: ( COMMA dec= T_INTEGER )?
            	int alt73 = 2;
            	int LA73_0 = input.LA(1);

            	if ( (LA73_0 == COMMA) )
            	{
            	    alt73 = 1;
            	}
            	switch (alt73) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:167:82: COMMA dec= T_INTEGER
            	        {
            	        	COMMA207=(IToken)Match(input,COMMA,FOLLOW_COMMA_in_real_length1604); 
            	        		COMMA207_tree = (object)adaptor.Create(COMMA207);
            	        		adaptor.AddChild(root_0, COMMA207_tree);

            	        	dec=(IToken)Match(input,T_INTEGER,FOLLOW_T_INTEGER_in_real_length1608); 
            	        		dec_tree = (object)adaptor.Create(dec);
            	        		adaptor.AddChild(root_0, dec_tree);

            	        	tp.SetDecimals(((dec != null) ? dec.Text : null));

            	        }
            	        break;

            	}

            	RPAREN208=(IToken)Match(input,RPAREN,FOLLOW_RPAREN_in_real_length1615); 
            		RPAREN208_tree = (object)adaptor.Create(RPAREN208);
            		adaptor.AddChild(root_0, RPAREN208_tree);


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "real_length"

    public class enumset_values_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "enumset_values"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:169:1: enumset_values[MySqlTypeEnumSet tp] : LPAREN enumset_value[tp] ( COMMA enumset_value[tp] )* RPAREN ;
    public MySQLParser.enumset_values_return enumset_values(MySqlTypeEnumSet tp) // throws RecognitionException [1]
    {   
        MySQLParser.enumset_values_return retval = new MySQLParser.enumset_values_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken LPAREN209 = null;
        IToken COMMA211 = null;
        IToken RPAREN213 = null;
        MySQLParser.enumset_value_return enumset_value210 = default(MySQLParser.enumset_value_return);

        MySQLParser.enumset_value_return enumset_value212 = default(MySQLParser.enumset_value_return);


        object LPAREN209_tree=null;
        object COMMA211_tree=null;
        object RPAREN213_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:169:37: ( LPAREN enumset_value[tp] ( COMMA enumset_value[tp] )* RPAREN )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:169:39: LPAREN enumset_value[tp] ( COMMA enumset_value[tp] )* RPAREN
            {
            	root_0 = (object)adaptor.GetNilNode();

            	LPAREN209=(IToken)Match(input,LPAREN,FOLLOW_LPAREN_in_enumset_values1624); 
            		LPAREN209_tree = (object)adaptor.Create(LPAREN209);
            		adaptor.AddChild(root_0, LPAREN209_tree);

            	PushFollow(FOLLOW_enumset_value_in_enumset_values1626);
            	enumset_value210 = enumset_value(tp);
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, enumset_value210.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:169:64: ( COMMA enumset_value[tp] )*
            	do 
            	{
            	    int alt74 = 2;
            	    int LA74_0 = input.LA(1);

            	    if ( (LA74_0 == COMMA) )
            	    {
            	        alt74 = 1;
            	    }


            	    switch (alt74) 
            		{
            			case 1 :
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:169:65: COMMA enumset_value[tp]
            			    {
            			    	COMMA211=(IToken)Match(input,COMMA,FOLLOW_COMMA_in_enumset_values1630); 
            			    		COMMA211_tree = (object)adaptor.Create(COMMA211);
            			    		adaptor.AddChild(root_0, COMMA211_tree);

            			    	PushFollow(FOLLOW_enumset_value_in_enumset_values1632);
            			    	enumset_value212 = enumset_value(tp);
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, enumset_value212.Tree);

            			    }
            			    break;

            			default:
            			    goto loop74;
            	    }
            	} while (true);

            	loop74:
            		;	// Stops C# compiler whining that label 'loop74' has no statements

            	RPAREN213=(IToken)Match(input,RPAREN,FOLLOW_RPAREN_in_enumset_values1638); 
            		RPAREN213_tree = (object)adaptor.Create(RPAREN213);
            		adaptor.AddChild(root_0, RPAREN213_tree);


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "enumset_values"

    public class enumset_value_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "enumset_value"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:171:1: enumset_value[MySqlTypeEnumSet tp] : (id1= id | s= T_STRING );
    public MySQLParser.enumset_value_return enumset_value(MySqlTypeEnumSet tp) // throws RecognitionException [1]
    {   
        MySQLParser.enumset_value_return retval = new MySQLParser.enumset_value_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken s = null;
        MySQLParser.id_return id1 = default(MySQLParser.id_return);


        object s_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:171:35: (id1= id | s= T_STRING )
            int alt75 = 2;
            int LA75_0 = input.LA(1);

            if ( ((LA75_0 >= T_IDENT && LA75_0 <= T_QUOTED_IDENT)) )
            {
                alt75 = 1;
            }
            else if ( (LA75_0 == T_STRING) )
            {
                alt75 = 2;
            }
            else 
            {
                NoViableAltException nvae_d75s0 =
                    new NoViableAltException("", 75, 0, input);

                throw nvae_d75s0;
            }
            switch (alt75) 
            {
                case 1 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:172:2: id1= id
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_id_in_enumset_value1649);
                    	id1 = id();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, id1.Tree);
                    	tp.Values.Add(((id1 != null) ? input.ToString((IToken)(id1.Start),(IToken)(id1.Stop)) : null));

                    }
                    break;
                case 2 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:172:38: s= T_STRING
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	s=(IToken)Match(input,T_STRING,FOLLOW_T_STRING_in_enumset_value1657); 
                    		s_tree = (object)adaptor.Create(s);
                    		adaptor.AddChild(root_0, s_tree);

                    	tp.Values.Add(StringValue(((s != null) ? s.Text : null)));

                    }
                    break;

            }
            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "enumset_value"

    public class table_options_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "table_options"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:174:1: table_options : table_option ( ( COMMA )? table_option )* ;
    public MySQLParser.table_options_return table_options() // throws RecognitionException [1]
    {   
        MySQLParser.table_options_return retval = new MySQLParser.table_options_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken COMMA215 = null;
        MySQLParser.table_option_return table_option214 = default(MySQLParser.table_option_return);

        MySQLParser.table_option_return table_option216 = default(MySQLParser.table_option_return);


        object COMMA215_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:174:14: ( table_option ( ( COMMA )? table_option )* )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:174:16: table_option ( ( COMMA )? table_option )*
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_table_option_in_table_options1666);
            	table_option214 = table_option();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, table_option214.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:174:29: ( ( COMMA )? table_option )*
            	do 
            	{
            	    int alt77 = 2;
            	    int LA77_0 = input.LA(1);

            	    if ( (LA77_0 == COMMA || LA77_0 == INDEX || LA77_0 == KEY_BLOCK_SIZE || LA77_0 == DEFAULT || (LA77_0 >= AUTO_INCREMENT && LA77_0 <= COMMENT) || (LA77_0 >= CHARACTER && LA77_0 <= COLLATE) || (LA77_0 >= ENGINE && LA77_0 <= DATA) || (LA77_0 >= DELAY_KEY_WRITE && LA77_0 <= INSERT_METHOD) || (LA77_0 >= MAX_ROWS && LA77_0 <= ROW_FORMAT) || (LA77_0 >= TABLESPACE && LA77_0 <= UNION)) )
            	    {
            	        alt77 = 1;
            	    }


            	    switch (alt77) 
            		{
            			case 1 :
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:174:31: ( COMMA )? table_option
            			    {
            			    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:174:31: ( COMMA )?
            			    	int alt76 = 2;
            			    	int LA76_0 = input.LA(1);

            			    	if ( (LA76_0 == COMMA) )
            			    	{
            			    	    alt76 = 1;
            			    	}
            			    	switch (alt76) 
            			    	{
            			    	    case 1 :
            			    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:174:31: COMMA
            			    	        {
            			    	        	COMMA215=(IToken)Match(input,COMMA,FOLLOW_COMMA_in_table_options1670); 
            			    	        		COMMA215_tree = (object)adaptor.Create(COMMA215);
            			    	        		adaptor.AddChild(root_0, COMMA215_tree);


            			    	        }
            			    	        break;

            			    	}

            			    	PushFollow(FOLLOW_table_option_in_table_options1673);
            			    	table_option216 = table_option();
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, table_option216.Tree);

            			    }
            			    break;

            			default:
            			    goto loop77;
            	    }
            	} while (true);

            	loop77:
            		;	// Stops C# compiler whining that label 'loop77' has no statements


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "table_options"

    public class table_option_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "table_option"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:176:1: table_option : ( ENGINE ( EQUALS )? ( id | MEMORY ) | TYPE ( EQUALS )? id | AUTO_INCREMENT ( EQUALS )? T_INTEGER | AVG_ROW_LENGTH ( EQUALS )? value | ( DEFAULT )? ( CHARACTER SET | CHARSET ) ( EQUALS )? charset_name | CHECKSUM ( EQUALS )? T_INTEGER | ( DEFAULT )? COLLATE ( EQUALS )? collation_name | COMMENT ( EQUALS )? T_STRING | CONNECTION ( EQUALS )? T_STRING | DATA DIRECTORY ( EQUALS )? T_STRING | DELAY_KEY_WRITE ( EQUALS )? T_INTEGER | INDEX DIRECTORY ( EQUALS )? T_STRING | INSERT_METHOD ( EQUALS )? ( NO | FIRST | LAST ) | KEY_BLOCK_SIZE ( EQUALS )? T_INTEGER | MAX_ROWS ( EQUALS )? T_INTEGER | MIN_ROWS ( EQUALS )? T_INTEGER | PACK_KEYS ( EQUALS )? ( T_INTEGER | DEFAULT ) | PASSWORD ( EQUALS )? T_STRING | ROW_FORMAT ( EQUALS )? ( DEFAULT | DYNAMIC | FIXED | COMPRESSED | REDUNDANT | COMPACT ) | TABLESPACE id ( STORAGE ( DISK | MEMORY | DEFAULT ) )? | UNION ( EQUALS )? LPAREN tbl_name ( COMMA tbl_name )* RPAREN );
    public MySQLParser.table_option_return table_option() // throws RecognitionException [1]
    {   
        MySQLParser.table_option_return retval = new MySQLParser.table_option_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken ENGINE217 = null;
        IToken EQUALS218 = null;
        IToken MEMORY220 = null;
        IToken TYPE221 = null;
        IToken EQUALS222 = null;
        IToken AUTO_INCREMENT224 = null;
        IToken EQUALS225 = null;
        IToken T_INTEGER226 = null;
        IToken AVG_ROW_LENGTH227 = null;
        IToken EQUALS228 = null;
        IToken DEFAULT230 = null;
        IToken CHARACTER231 = null;
        IToken SET232 = null;
        IToken CHARSET233 = null;
        IToken EQUALS234 = null;
        IToken CHECKSUM236 = null;
        IToken EQUALS237 = null;
        IToken T_INTEGER238 = null;
        IToken DEFAULT239 = null;
        IToken COLLATE240 = null;
        IToken EQUALS241 = null;
        IToken COMMENT243 = null;
        IToken EQUALS244 = null;
        IToken T_STRING245 = null;
        IToken CONNECTION246 = null;
        IToken EQUALS247 = null;
        IToken T_STRING248 = null;
        IToken DATA249 = null;
        IToken DIRECTORY250 = null;
        IToken EQUALS251 = null;
        IToken T_STRING252 = null;
        IToken DELAY_KEY_WRITE253 = null;
        IToken EQUALS254 = null;
        IToken T_INTEGER255 = null;
        IToken INDEX256 = null;
        IToken DIRECTORY257 = null;
        IToken EQUALS258 = null;
        IToken T_STRING259 = null;
        IToken INSERT_METHOD260 = null;
        IToken EQUALS261 = null;
        IToken set262 = null;
        IToken KEY_BLOCK_SIZE263 = null;
        IToken EQUALS264 = null;
        IToken T_INTEGER265 = null;
        IToken MAX_ROWS266 = null;
        IToken EQUALS267 = null;
        IToken T_INTEGER268 = null;
        IToken MIN_ROWS269 = null;
        IToken EQUALS270 = null;
        IToken T_INTEGER271 = null;
        IToken PACK_KEYS272 = null;
        IToken EQUALS273 = null;
        IToken set274 = null;
        IToken PASSWORD275 = null;
        IToken EQUALS276 = null;
        IToken T_STRING277 = null;
        IToken ROW_FORMAT278 = null;
        IToken EQUALS279 = null;
        IToken set280 = null;
        IToken TABLESPACE281 = null;
        IToken STORAGE283 = null;
        IToken set284 = null;
        IToken UNION285 = null;
        IToken EQUALS286 = null;
        IToken LPAREN287 = null;
        IToken COMMA289 = null;
        IToken RPAREN291 = null;
        MySQLParser.id_return id219 = default(MySQLParser.id_return);

        MySQLParser.id_return id223 = default(MySQLParser.id_return);

        MySQLParser.value_return value229 = default(MySQLParser.value_return);

        MySQLParser.charset_name_return charset_name235 = default(MySQLParser.charset_name_return);

        MySQLParser.collation_name_return collation_name242 = default(MySQLParser.collation_name_return);

        MySQLParser.id_return id282 = default(MySQLParser.id_return);

        MySQLParser.tbl_name_return tbl_name288 = default(MySQLParser.tbl_name_return);

        MySQLParser.tbl_name_return tbl_name290 = default(MySQLParser.tbl_name_return);


        object ENGINE217_tree=null;
        object EQUALS218_tree=null;
        object MEMORY220_tree=null;
        object TYPE221_tree=null;
        object EQUALS222_tree=null;
        object AUTO_INCREMENT224_tree=null;
        object EQUALS225_tree=null;
        object T_INTEGER226_tree=null;
        object AVG_ROW_LENGTH227_tree=null;
        object EQUALS228_tree=null;
        object DEFAULT230_tree=null;
        object CHARACTER231_tree=null;
        object SET232_tree=null;
        object CHARSET233_tree=null;
        object EQUALS234_tree=null;
        object CHECKSUM236_tree=null;
        object EQUALS237_tree=null;
        object T_INTEGER238_tree=null;
        object DEFAULT239_tree=null;
        object COLLATE240_tree=null;
        object EQUALS241_tree=null;
        object COMMENT243_tree=null;
        object EQUALS244_tree=null;
        object T_STRING245_tree=null;
        object CONNECTION246_tree=null;
        object EQUALS247_tree=null;
        object T_STRING248_tree=null;
        object DATA249_tree=null;
        object DIRECTORY250_tree=null;
        object EQUALS251_tree=null;
        object T_STRING252_tree=null;
        object DELAY_KEY_WRITE253_tree=null;
        object EQUALS254_tree=null;
        object T_INTEGER255_tree=null;
        object INDEX256_tree=null;
        object DIRECTORY257_tree=null;
        object EQUALS258_tree=null;
        object T_STRING259_tree=null;
        object INSERT_METHOD260_tree=null;
        object EQUALS261_tree=null;
        object set262_tree=null;
        object KEY_BLOCK_SIZE263_tree=null;
        object EQUALS264_tree=null;
        object T_INTEGER265_tree=null;
        object MAX_ROWS266_tree=null;
        object EQUALS267_tree=null;
        object T_INTEGER268_tree=null;
        object MIN_ROWS269_tree=null;
        object EQUALS270_tree=null;
        object T_INTEGER271_tree=null;
        object PACK_KEYS272_tree=null;
        object EQUALS273_tree=null;
        object set274_tree=null;
        object PASSWORD275_tree=null;
        object EQUALS276_tree=null;
        object T_STRING277_tree=null;
        object ROW_FORMAT278_tree=null;
        object EQUALS279_tree=null;
        object set280_tree=null;
        object TABLESPACE281_tree=null;
        object STORAGE283_tree=null;
        object set284_tree=null;
        object UNION285_tree=null;
        object EQUALS286_tree=null;
        object LPAREN287_tree=null;
        object COMMA289_tree=null;
        object RPAREN291_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:176:13: ( ENGINE ( EQUALS )? ( id | MEMORY ) | TYPE ( EQUALS )? id | AUTO_INCREMENT ( EQUALS )? T_INTEGER | AVG_ROW_LENGTH ( EQUALS )? value | ( DEFAULT )? ( CHARACTER SET | CHARSET ) ( EQUALS )? charset_name | CHECKSUM ( EQUALS )? T_INTEGER | ( DEFAULT )? COLLATE ( EQUALS )? collation_name | COMMENT ( EQUALS )? T_STRING | CONNECTION ( EQUALS )? T_STRING | DATA DIRECTORY ( EQUALS )? T_STRING | DELAY_KEY_WRITE ( EQUALS )? T_INTEGER | INDEX DIRECTORY ( EQUALS )? T_STRING | INSERT_METHOD ( EQUALS )? ( NO | FIRST | LAST ) | KEY_BLOCK_SIZE ( EQUALS )? T_INTEGER | MAX_ROWS ( EQUALS )? T_INTEGER | MIN_ROWS ( EQUALS )? T_INTEGER | PACK_KEYS ( EQUALS )? ( T_INTEGER | DEFAULT ) | PASSWORD ( EQUALS )? T_STRING | ROW_FORMAT ( EQUALS )? ( DEFAULT | DYNAMIC | FIXED | COMPRESSED | REDUNDANT | COMPACT ) | TABLESPACE id ( STORAGE ( DISK | MEMORY | DEFAULT ) )? | UNION ( EQUALS )? LPAREN tbl_name ( COMMA tbl_name )* RPAREN )
            int alt104 = 21;
            alt104 = dfa104.Predict(input);
            switch (alt104) 
            {
                case 1 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:177:5: ENGINE ( EQUALS )? ( id | MEMORY )
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	ENGINE217=(IToken)Match(input,ENGINE,FOLLOW_ENGINE_in_table_option1686); 
                    		ENGINE217_tree = (object)adaptor.Create(ENGINE217);
                    		adaptor.AddChild(root_0, ENGINE217_tree);

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:177:12: ( EQUALS )?
                    	int alt78 = 2;
                    	int LA78_0 = input.LA(1);

                    	if ( (LA78_0 == EQUALS) )
                    	{
                    	    alt78 = 1;
                    	}
                    	switch (alt78) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:177:12: EQUALS
                    	        {
                    	        	EQUALS218=(IToken)Match(input,EQUALS,FOLLOW_EQUALS_in_table_option1688); 
                    	        		EQUALS218_tree = (object)adaptor.Create(EQUALS218);
                    	        		adaptor.AddChild(root_0, EQUALS218_tree);


                    	        }
                    	        break;

                    	}

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:177:20: ( id | MEMORY )
                    	int alt79 = 2;
                    	int LA79_0 = input.LA(1);

                    	if ( ((LA79_0 >= T_IDENT && LA79_0 <= T_QUOTED_IDENT)) )
                    	{
                    	    alt79 = 1;
                    	}
                    	else if ( (LA79_0 == MEMORY) )
                    	{
                    	    alt79 = 2;
                    	}
                    	else 
                    	{
                    	    NoViableAltException nvae_d79s0 =
                    	        new NoViableAltException("", 79, 0, input);

                    	    throw nvae_d79s0;
                    	}
                    	switch (alt79) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:177:22: id
                    	        {
                    	        	PushFollow(FOLLOW_id_in_table_option1693);
                    	        	id219 = id();
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, id219.Tree);

                    	        }
                    	        break;
                    	    case 2 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:177:27: MEMORY
                    	        {
                    	        	MEMORY220=(IToken)Match(input,MEMORY,FOLLOW_MEMORY_in_table_option1697); 
                    	        		MEMORY220_tree = (object)adaptor.Create(MEMORY220);
                    	        		adaptor.AddChild(root_0, MEMORY220_tree);


                    	        }
                    	        break;

                    	}


                    }
                    break;
                case 2 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:178:5: TYPE ( EQUALS )? id
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	TYPE221=(IToken)Match(input,TYPE,FOLLOW_TYPE_in_table_option1705); 
                    		TYPE221_tree = (object)adaptor.Create(TYPE221);
                    		adaptor.AddChild(root_0, TYPE221_tree);

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:178:10: ( EQUALS )?
                    	int alt80 = 2;
                    	int LA80_0 = input.LA(1);

                    	if ( (LA80_0 == EQUALS) )
                    	{
                    	    alt80 = 1;
                    	}
                    	switch (alt80) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:178:10: EQUALS
                    	        {
                    	        	EQUALS222=(IToken)Match(input,EQUALS,FOLLOW_EQUALS_in_table_option1707); 
                    	        		EQUALS222_tree = (object)adaptor.Create(EQUALS222);
                    	        		adaptor.AddChild(root_0, EQUALS222_tree);


                    	        }
                    	        break;

                    	}

                    	PushFollow(FOLLOW_id_in_table_option1710);
                    	id223 = id();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, id223.Tree);

                    }
                    break;
                case 3 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:179:5: AUTO_INCREMENT ( EQUALS )? T_INTEGER
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	AUTO_INCREMENT224=(IToken)Match(input,AUTO_INCREMENT,FOLLOW_AUTO_INCREMENT_in_table_option1716); 
                    		AUTO_INCREMENT224_tree = (object)adaptor.Create(AUTO_INCREMENT224);
                    		adaptor.AddChild(root_0, AUTO_INCREMENT224_tree);

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:179:20: ( EQUALS )?
                    	int alt81 = 2;
                    	int LA81_0 = input.LA(1);

                    	if ( (LA81_0 == EQUALS) )
                    	{
                    	    alt81 = 1;
                    	}
                    	switch (alt81) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:179:20: EQUALS
                    	        {
                    	        	EQUALS225=(IToken)Match(input,EQUALS,FOLLOW_EQUALS_in_table_option1718); 
                    	        		EQUALS225_tree = (object)adaptor.Create(EQUALS225);
                    	        		adaptor.AddChild(root_0, EQUALS225_tree);


                    	        }
                    	        break;

                    	}

                    	T_INTEGER226=(IToken)Match(input,T_INTEGER,FOLLOW_T_INTEGER_in_table_option1721); 
                    		T_INTEGER226_tree = (object)adaptor.Create(T_INTEGER226);
                    		adaptor.AddChild(root_0, T_INTEGER226_tree);


                    }
                    break;
                case 4 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:180:5: AVG_ROW_LENGTH ( EQUALS )? value
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	AVG_ROW_LENGTH227=(IToken)Match(input,AVG_ROW_LENGTH,FOLLOW_AVG_ROW_LENGTH_in_table_option1727); 
                    		AVG_ROW_LENGTH227_tree = (object)adaptor.Create(AVG_ROW_LENGTH227);
                    		adaptor.AddChild(root_0, AVG_ROW_LENGTH227_tree);

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:180:20: ( EQUALS )?
                    	int alt82 = 2;
                    	int LA82_0 = input.LA(1);

                    	if ( (LA82_0 == EQUALS) )
                    	{
                    	    alt82 = 1;
                    	}
                    	switch (alt82) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:180:20: EQUALS
                    	        {
                    	        	EQUALS228=(IToken)Match(input,EQUALS,FOLLOW_EQUALS_in_table_option1729); 
                    	        		EQUALS228_tree = (object)adaptor.Create(EQUALS228);
                    	        		adaptor.AddChild(root_0, EQUALS228_tree);


                    	        }
                    	        break;

                    	}

                    	PushFollow(FOLLOW_value_in_table_option1732);
                    	value229 = value();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, value229.Tree);

                    }
                    break;
                case 5 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:181:5: ( DEFAULT )? ( CHARACTER SET | CHARSET ) ( EQUALS )? charset_name
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:181:5: ( DEFAULT )?
                    	int alt83 = 2;
                    	int LA83_0 = input.LA(1);

                    	if ( (LA83_0 == DEFAULT) )
                    	{
                    	    alt83 = 1;
                    	}
                    	switch (alt83) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:181:5: DEFAULT
                    	        {
                    	        	DEFAULT230=(IToken)Match(input,DEFAULT,FOLLOW_DEFAULT_in_table_option1738); 
                    	        		DEFAULT230_tree = (object)adaptor.Create(DEFAULT230);
                    	        		adaptor.AddChild(root_0, DEFAULT230_tree);


                    	        }
                    	        break;

                    	}

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:181:14: ( CHARACTER SET | CHARSET )
                    	int alt84 = 2;
                    	int LA84_0 = input.LA(1);

                    	if ( (LA84_0 == CHARACTER) )
                    	{
                    	    alt84 = 1;
                    	}
                    	else if ( (LA84_0 == CHARSET) )
                    	{
                    	    alt84 = 2;
                    	}
                    	else 
                    	{
                    	    NoViableAltException nvae_d84s0 =
                    	        new NoViableAltException("", 84, 0, input);

                    	    throw nvae_d84s0;
                    	}
                    	switch (alt84) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:181:15: CHARACTER SET
                    	        {
                    	        	CHARACTER231=(IToken)Match(input,CHARACTER,FOLLOW_CHARACTER_in_table_option1742); 
                    	        		CHARACTER231_tree = (object)adaptor.Create(CHARACTER231);
                    	        		adaptor.AddChild(root_0, CHARACTER231_tree);

                    	        	SET232=(IToken)Match(input,SET,FOLLOW_SET_in_table_option1744); 
                    	        		SET232_tree = (object)adaptor.Create(SET232);
                    	        		adaptor.AddChild(root_0, SET232_tree);


                    	        }
                    	        break;
                    	    case 2 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:181:29: CHARSET
                    	        {
                    	        	CHARSET233=(IToken)Match(input,CHARSET,FOLLOW_CHARSET_in_table_option1746); 
                    	        		CHARSET233_tree = (object)adaptor.Create(CHARSET233);
                    	        		adaptor.AddChild(root_0, CHARSET233_tree);


                    	        }
                    	        break;

                    	}

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:181:38: ( EQUALS )?
                    	int alt85 = 2;
                    	int LA85_0 = input.LA(1);

                    	if ( (LA85_0 == EQUALS) )
                    	{
                    	    alt85 = 1;
                    	}
                    	switch (alt85) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:181:38: EQUALS
                    	        {
                    	        	EQUALS234=(IToken)Match(input,EQUALS,FOLLOW_EQUALS_in_table_option1749); 
                    	        		EQUALS234_tree = (object)adaptor.Create(EQUALS234);
                    	        		adaptor.AddChild(root_0, EQUALS234_tree);


                    	        }
                    	        break;

                    	}

                    	PushFollow(FOLLOW_charset_name_in_table_option1752);
                    	charset_name235 = charset_name();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, charset_name235.Tree);

                    }
                    break;
                case 6 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:182:5: CHECKSUM ( EQUALS )? T_INTEGER
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	CHECKSUM236=(IToken)Match(input,CHECKSUM,FOLLOW_CHECKSUM_in_table_option1758); 
                    		CHECKSUM236_tree = (object)adaptor.Create(CHECKSUM236);
                    		adaptor.AddChild(root_0, CHECKSUM236_tree);

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:182:14: ( EQUALS )?
                    	int alt86 = 2;
                    	int LA86_0 = input.LA(1);

                    	if ( (LA86_0 == EQUALS) )
                    	{
                    	    alt86 = 1;
                    	}
                    	switch (alt86) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:182:14: EQUALS
                    	        {
                    	        	EQUALS237=(IToken)Match(input,EQUALS,FOLLOW_EQUALS_in_table_option1760); 
                    	        		EQUALS237_tree = (object)adaptor.Create(EQUALS237);
                    	        		adaptor.AddChild(root_0, EQUALS237_tree);


                    	        }
                    	        break;

                    	}

                    	T_INTEGER238=(IToken)Match(input,T_INTEGER,FOLLOW_T_INTEGER_in_table_option1763); 
                    		T_INTEGER238_tree = (object)adaptor.Create(T_INTEGER238);
                    		adaptor.AddChild(root_0, T_INTEGER238_tree);


                    }
                    break;
                case 7 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:183:5: ( DEFAULT )? COLLATE ( EQUALS )? collation_name
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:183:5: ( DEFAULT )?
                    	int alt87 = 2;
                    	int LA87_0 = input.LA(1);

                    	if ( (LA87_0 == DEFAULT) )
                    	{
                    	    alt87 = 1;
                    	}
                    	switch (alt87) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:183:5: DEFAULT
                    	        {
                    	        	DEFAULT239=(IToken)Match(input,DEFAULT,FOLLOW_DEFAULT_in_table_option1769); 
                    	        		DEFAULT239_tree = (object)adaptor.Create(DEFAULT239);
                    	        		adaptor.AddChild(root_0, DEFAULT239_tree);


                    	        }
                    	        break;

                    	}

                    	COLLATE240=(IToken)Match(input,COLLATE,FOLLOW_COLLATE_in_table_option1772); 
                    		COLLATE240_tree = (object)adaptor.Create(COLLATE240);
                    		adaptor.AddChild(root_0, COLLATE240_tree);

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:183:22: ( EQUALS )?
                    	int alt88 = 2;
                    	int LA88_0 = input.LA(1);

                    	if ( (LA88_0 == EQUALS) )
                    	{
                    	    alt88 = 1;
                    	}
                    	switch (alt88) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:183:22: EQUALS
                    	        {
                    	        	EQUALS241=(IToken)Match(input,EQUALS,FOLLOW_EQUALS_in_table_option1774); 
                    	        		EQUALS241_tree = (object)adaptor.Create(EQUALS241);
                    	        		adaptor.AddChild(root_0, EQUALS241_tree);


                    	        }
                    	        break;

                    	}

                    	PushFollow(FOLLOW_collation_name_in_table_option1777);
                    	collation_name242 = collation_name();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, collation_name242.Tree);

                    }
                    break;
                case 8 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:184:5: COMMENT ( EQUALS )? T_STRING
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	COMMENT243=(IToken)Match(input,COMMENT,FOLLOW_COMMENT_in_table_option1783); 
                    		COMMENT243_tree = (object)adaptor.Create(COMMENT243);
                    		adaptor.AddChild(root_0, COMMENT243_tree);

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:184:13: ( EQUALS )?
                    	int alt89 = 2;
                    	int LA89_0 = input.LA(1);

                    	if ( (LA89_0 == EQUALS) )
                    	{
                    	    alt89 = 1;
                    	}
                    	switch (alt89) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:184:13: EQUALS
                    	        {
                    	        	EQUALS244=(IToken)Match(input,EQUALS,FOLLOW_EQUALS_in_table_option1785); 
                    	        		EQUALS244_tree = (object)adaptor.Create(EQUALS244);
                    	        		adaptor.AddChild(root_0, EQUALS244_tree);


                    	        }
                    	        break;

                    	}

                    	T_STRING245=(IToken)Match(input,T_STRING,FOLLOW_T_STRING_in_table_option1788); 
                    		T_STRING245_tree = (object)adaptor.Create(T_STRING245);
                    		adaptor.AddChild(root_0, T_STRING245_tree);


                    }
                    break;
                case 9 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:185:5: CONNECTION ( EQUALS )? T_STRING
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	CONNECTION246=(IToken)Match(input,CONNECTION,FOLLOW_CONNECTION_in_table_option1794); 
                    		CONNECTION246_tree = (object)adaptor.Create(CONNECTION246);
                    		adaptor.AddChild(root_0, CONNECTION246_tree);

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:185:16: ( EQUALS )?
                    	int alt90 = 2;
                    	int LA90_0 = input.LA(1);

                    	if ( (LA90_0 == EQUALS) )
                    	{
                    	    alt90 = 1;
                    	}
                    	switch (alt90) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:185:16: EQUALS
                    	        {
                    	        	EQUALS247=(IToken)Match(input,EQUALS,FOLLOW_EQUALS_in_table_option1796); 
                    	        		EQUALS247_tree = (object)adaptor.Create(EQUALS247);
                    	        		adaptor.AddChild(root_0, EQUALS247_tree);


                    	        }
                    	        break;

                    	}

                    	T_STRING248=(IToken)Match(input,T_STRING,FOLLOW_T_STRING_in_table_option1799); 
                    		T_STRING248_tree = (object)adaptor.Create(T_STRING248);
                    		adaptor.AddChild(root_0, T_STRING248_tree);


                    }
                    break;
                case 10 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:186:5: DATA DIRECTORY ( EQUALS )? T_STRING
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	DATA249=(IToken)Match(input,DATA,FOLLOW_DATA_in_table_option1805); 
                    		DATA249_tree = (object)adaptor.Create(DATA249);
                    		adaptor.AddChild(root_0, DATA249_tree);

                    	DIRECTORY250=(IToken)Match(input,DIRECTORY,FOLLOW_DIRECTORY_in_table_option1807); 
                    		DIRECTORY250_tree = (object)adaptor.Create(DIRECTORY250);
                    		adaptor.AddChild(root_0, DIRECTORY250_tree);

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:186:20: ( EQUALS )?
                    	int alt91 = 2;
                    	int LA91_0 = input.LA(1);

                    	if ( (LA91_0 == EQUALS) )
                    	{
                    	    alt91 = 1;
                    	}
                    	switch (alt91) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:186:20: EQUALS
                    	        {
                    	        	EQUALS251=(IToken)Match(input,EQUALS,FOLLOW_EQUALS_in_table_option1809); 
                    	        		EQUALS251_tree = (object)adaptor.Create(EQUALS251);
                    	        		adaptor.AddChild(root_0, EQUALS251_tree);


                    	        }
                    	        break;

                    	}

                    	T_STRING252=(IToken)Match(input,T_STRING,FOLLOW_T_STRING_in_table_option1812); 
                    		T_STRING252_tree = (object)adaptor.Create(T_STRING252);
                    		adaptor.AddChild(root_0, T_STRING252_tree);


                    }
                    break;
                case 11 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:187:5: DELAY_KEY_WRITE ( EQUALS )? T_INTEGER
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	DELAY_KEY_WRITE253=(IToken)Match(input,DELAY_KEY_WRITE,FOLLOW_DELAY_KEY_WRITE_in_table_option1818); 
                    		DELAY_KEY_WRITE253_tree = (object)adaptor.Create(DELAY_KEY_WRITE253);
                    		adaptor.AddChild(root_0, DELAY_KEY_WRITE253_tree);

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:187:21: ( EQUALS )?
                    	int alt92 = 2;
                    	int LA92_0 = input.LA(1);

                    	if ( (LA92_0 == EQUALS) )
                    	{
                    	    alt92 = 1;
                    	}
                    	switch (alt92) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:187:21: EQUALS
                    	        {
                    	        	EQUALS254=(IToken)Match(input,EQUALS,FOLLOW_EQUALS_in_table_option1820); 
                    	        		EQUALS254_tree = (object)adaptor.Create(EQUALS254);
                    	        		adaptor.AddChild(root_0, EQUALS254_tree);


                    	        }
                    	        break;

                    	}

                    	T_INTEGER255=(IToken)Match(input,T_INTEGER,FOLLOW_T_INTEGER_in_table_option1823); 
                    		T_INTEGER255_tree = (object)adaptor.Create(T_INTEGER255);
                    		adaptor.AddChild(root_0, T_INTEGER255_tree);


                    }
                    break;
                case 12 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:188:5: INDEX DIRECTORY ( EQUALS )? T_STRING
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	INDEX256=(IToken)Match(input,INDEX,FOLLOW_INDEX_in_table_option1829); 
                    		INDEX256_tree = (object)adaptor.Create(INDEX256);
                    		adaptor.AddChild(root_0, INDEX256_tree);

                    	DIRECTORY257=(IToken)Match(input,DIRECTORY,FOLLOW_DIRECTORY_in_table_option1831); 
                    		DIRECTORY257_tree = (object)adaptor.Create(DIRECTORY257);
                    		adaptor.AddChild(root_0, DIRECTORY257_tree);

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:188:21: ( EQUALS )?
                    	int alt93 = 2;
                    	int LA93_0 = input.LA(1);

                    	if ( (LA93_0 == EQUALS) )
                    	{
                    	    alt93 = 1;
                    	}
                    	switch (alt93) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:188:21: EQUALS
                    	        {
                    	        	EQUALS258=(IToken)Match(input,EQUALS,FOLLOW_EQUALS_in_table_option1833); 
                    	        		EQUALS258_tree = (object)adaptor.Create(EQUALS258);
                    	        		adaptor.AddChild(root_0, EQUALS258_tree);


                    	        }
                    	        break;

                    	}

                    	T_STRING259=(IToken)Match(input,T_STRING,FOLLOW_T_STRING_in_table_option1836); 
                    		T_STRING259_tree = (object)adaptor.Create(T_STRING259);
                    		adaptor.AddChild(root_0, T_STRING259_tree);


                    }
                    break;
                case 13 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:189:5: INSERT_METHOD ( EQUALS )? ( NO | FIRST | LAST )
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	INSERT_METHOD260=(IToken)Match(input,INSERT_METHOD,FOLLOW_INSERT_METHOD_in_table_option1842); 
                    		INSERT_METHOD260_tree = (object)adaptor.Create(INSERT_METHOD260);
                    		adaptor.AddChild(root_0, INSERT_METHOD260_tree);

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:189:19: ( EQUALS )?
                    	int alt94 = 2;
                    	int LA94_0 = input.LA(1);

                    	if ( (LA94_0 == EQUALS) )
                    	{
                    	    alt94 = 1;
                    	}
                    	switch (alt94) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:189:19: EQUALS
                    	        {
                    	        	EQUALS261=(IToken)Match(input,EQUALS,FOLLOW_EQUALS_in_table_option1844); 
                    	        		EQUALS261_tree = (object)adaptor.Create(EQUALS261);
                    	        		adaptor.AddChild(root_0, EQUALS261_tree);


                    	        }
                    	        break;

                    	}

                    	set262 = (IToken)input.LT(1);
                    	if ( input.LA(1) == NO || (input.LA(1) >= FIRST && input.LA(1) <= LAST) ) 
                    	{
                    	    input.Consume();
                    	    adaptor.AddChild(root_0, (object)adaptor.Create(set262));
                    	    state.errorRecovery = false;
                    	}
                    	else 
                    	{
                    	    MismatchedSetException mse = new MismatchedSetException(null,input);
                    	    throw mse;
                    	}


                    }
                    break;
                case 14 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:190:5: KEY_BLOCK_SIZE ( EQUALS )? T_INTEGER
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	KEY_BLOCK_SIZE263=(IToken)Match(input,KEY_BLOCK_SIZE,FOLLOW_KEY_BLOCK_SIZE_in_table_option1865); 
                    		KEY_BLOCK_SIZE263_tree = (object)adaptor.Create(KEY_BLOCK_SIZE263);
                    		adaptor.AddChild(root_0, KEY_BLOCK_SIZE263_tree);

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:190:20: ( EQUALS )?
                    	int alt95 = 2;
                    	int LA95_0 = input.LA(1);

                    	if ( (LA95_0 == EQUALS) )
                    	{
                    	    alt95 = 1;
                    	}
                    	switch (alt95) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:190:20: EQUALS
                    	        {
                    	        	EQUALS264=(IToken)Match(input,EQUALS,FOLLOW_EQUALS_in_table_option1867); 
                    	        		EQUALS264_tree = (object)adaptor.Create(EQUALS264);
                    	        		adaptor.AddChild(root_0, EQUALS264_tree);


                    	        }
                    	        break;

                    	}

                    	T_INTEGER265=(IToken)Match(input,T_INTEGER,FOLLOW_T_INTEGER_in_table_option1870); 
                    		T_INTEGER265_tree = (object)adaptor.Create(T_INTEGER265);
                    		adaptor.AddChild(root_0, T_INTEGER265_tree);


                    }
                    break;
                case 15 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:191:5: MAX_ROWS ( EQUALS )? T_INTEGER
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	MAX_ROWS266=(IToken)Match(input,MAX_ROWS,FOLLOW_MAX_ROWS_in_table_option1876); 
                    		MAX_ROWS266_tree = (object)adaptor.Create(MAX_ROWS266);
                    		adaptor.AddChild(root_0, MAX_ROWS266_tree);

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:191:14: ( EQUALS )?
                    	int alt96 = 2;
                    	int LA96_0 = input.LA(1);

                    	if ( (LA96_0 == EQUALS) )
                    	{
                    	    alt96 = 1;
                    	}
                    	switch (alt96) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:191:14: EQUALS
                    	        {
                    	        	EQUALS267=(IToken)Match(input,EQUALS,FOLLOW_EQUALS_in_table_option1878); 
                    	        		EQUALS267_tree = (object)adaptor.Create(EQUALS267);
                    	        		adaptor.AddChild(root_0, EQUALS267_tree);


                    	        }
                    	        break;

                    	}

                    	T_INTEGER268=(IToken)Match(input,T_INTEGER,FOLLOW_T_INTEGER_in_table_option1881); 
                    		T_INTEGER268_tree = (object)adaptor.Create(T_INTEGER268);
                    		adaptor.AddChild(root_0, T_INTEGER268_tree);


                    }
                    break;
                case 16 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:192:5: MIN_ROWS ( EQUALS )? T_INTEGER
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	MIN_ROWS269=(IToken)Match(input,MIN_ROWS,FOLLOW_MIN_ROWS_in_table_option1887); 
                    		MIN_ROWS269_tree = (object)adaptor.Create(MIN_ROWS269);
                    		adaptor.AddChild(root_0, MIN_ROWS269_tree);

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:192:14: ( EQUALS )?
                    	int alt97 = 2;
                    	int LA97_0 = input.LA(1);

                    	if ( (LA97_0 == EQUALS) )
                    	{
                    	    alt97 = 1;
                    	}
                    	switch (alt97) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:192:14: EQUALS
                    	        {
                    	        	EQUALS270=(IToken)Match(input,EQUALS,FOLLOW_EQUALS_in_table_option1889); 
                    	        		EQUALS270_tree = (object)adaptor.Create(EQUALS270);
                    	        		adaptor.AddChild(root_0, EQUALS270_tree);


                    	        }
                    	        break;

                    	}

                    	T_INTEGER271=(IToken)Match(input,T_INTEGER,FOLLOW_T_INTEGER_in_table_option1892); 
                    		T_INTEGER271_tree = (object)adaptor.Create(T_INTEGER271);
                    		adaptor.AddChild(root_0, T_INTEGER271_tree);


                    }
                    break;
                case 17 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:193:5: PACK_KEYS ( EQUALS )? ( T_INTEGER | DEFAULT )
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PACK_KEYS272=(IToken)Match(input,PACK_KEYS,FOLLOW_PACK_KEYS_in_table_option1898); 
                    		PACK_KEYS272_tree = (object)adaptor.Create(PACK_KEYS272);
                    		adaptor.AddChild(root_0, PACK_KEYS272_tree);

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:193:15: ( EQUALS )?
                    	int alt98 = 2;
                    	int LA98_0 = input.LA(1);

                    	if ( (LA98_0 == EQUALS) )
                    	{
                    	    alt98 = 1;
                    	}
                    	switch (alt98) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:193:15: EQUALS
                    	        {
                    	        	EQUALS273=(IToken)Match(input,EQUALS,FOLLOW_EQUALS_in_table_option1900); 
                    	        		EQUALS273_tree = (object)adaptor.Create(EQUALS273);
                    	        		adaptor.AddChild(root_0, EQUALS273_tree);


                    	        }
                    	        break;

                    	}

                    	set274 = (IToken)input.LT(1);
                    	if ( input.LA(1) == T_INTEGER || input.LA(1) == DEFAULT ) 
                    	{
                    	    input.Consume();
                    	    adaptor.AddChild(root_0, (object)adaptor.Create(set274));
                    	    state.errorRecovery = false;
                    	}
                    	else 
                    	{
                    	    MismatchedSetException mse = new MismatchedSetException(null,input);
                    	    throw mse;
                    	}


                    }
                    break;
                case 18 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:194:5: PASSWORD ( EQUALS )? T_STRING
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PASSWORD275=(IToken)Match(input,PASSWORD,FOLLOW_PASSWORD_in_table_option1915); 
                    		PASSWORD275_tree = (object)adaptor.Create(PASSWORD275);
                    		adaptor.AddChild(root_0, PASSWORD275_tree);

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:194:14: ( EQUALS )?
                    	int alt99 = 2;
                    	int LA99_0 = input.LA(1);

                    	if ( (LA99_0 == EQUALS) )
                    	{
                    	    alt99 = 1;
                    	}
                    	switch (alt99) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:194:14: EQUALS
                    	        {
                    	        	EQUALS276=(IToken)Match(input,EQUALS,FOLLOW_EQUALS_in_table_option1917); 
                    	        		EQUALS276_tree = (object)adaptor.Create(EQUALS276);
                    	        		adaptor.AddChild(root_0, EQUALS276_tree);


                    	        }
                    	        break;

                    	}

                    	T_STRING277=(IToken)Match(input,T_STRING,FOLLOW_T_STRING_in_table_option1920); 
                    		T_STRING277_tree = (object)adaptor.Create(T_STRING277);
                    		adaptor.AddChild(root_0, T_STRING277_tree);


                    }
                    break;
                case 19 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:195:5: ROW_FORMAT ( EQUALS )? ( DEFAULT | DYNAMIC | FIXED | COMPRESSED | REDUNDANT | COMPACT )
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	ROW_FORMAT278=(IToken)Match(input,ROW_FORMAT,FOLLOW_ROW_FORMAT_in_table_option1926); 
                    		ROW_FORMAT278_tree = (object)adaptor.Create(ROW_FORMAT278);
                    		adaptor.AddChild(root_0, ROW_FORMAT278_tree);

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:195:16: ( EQUALS )?
                    	int alt100 = 2;
                    	int LA100_0 = input.LA(1);

                    	if ( (LA100_0 == EQUALS) )
                    	{
                    	    alt100 = 1;
                    	}
                    	switch (alt100) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:195:16: EQUALS
                    	        {
                    	        	EQUALS279=(IToken)Match(input,EQUALS,FOLLOW_EQUALS_in_table_option1928); 
                    	        		EQUALS279_tree = (object)adaptor.Create(EQUALS279);
                    	        		adaptor.AddChild(root_0, EQUALS279_tree);


                    	        }
                    	        break;

                    	}

                    	set280 = (IToken)input.LT(1);
                    	if ( input.LA(1) == DEFAULT || (input.LA(1) >= FIXED && input.LA(1) <= DYNAMIC) || (input.LA(1) >= COMPRESSED && input.LA(1) <= COMPACT) ) 
                    	{
                    	    input.Consume();
                    	    adaptor.AddChild(root_0, (object)adaptor.Create(set280));
                    	    state.errorRecovery = false;
                    	}
                    	else 
                    	{
                    	    MismatchedSetException mse = new MismatchedSetException(null,input);
                    	    throw mse;
                    	}


                    }
                    break;
                case 20 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:196:5: TABLESPACE id ( STORAGE ( DISK | MEMORY | DEFAULT ) )?
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	TABLESPACE281=(IToken)Match(input,TABLESPACE,FOLLOW_TABLESPACE_in_table_option1949); 
                    		TABLESPACE281_tree = (object)adaptor.Create(TABLESPACE281);
                    		adaptor.AddChild(root_0, TABLESPACE281_tree);

                    	PushFollow(FOLLOW_id_in_table_option1951);
                    	id282 = id();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, id282.Tree);
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:196:19: ( STORAGE ( DISK | MEMORY | DEFAULT ) )?
                    	int alt101 = 2;
                    	int LA101_0 = input.LA(1);

                    	if ( (LA101_0 == STORAGE) )
                    	{
                    	    alt101 = 1;
                    	}
                    	switch (alt101) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:196:20: STORAGE ( DISK | MEMORY | DEFAULT )
                    	        {
                    	        	STORAGE283=(IToken)Match(input,STORAGE,FOLLOW_STORAGE_in_table_option1954); 
                    	        		STORAGE283_tree = (object)adaptor.Create(STORAGE283);
                    	        		adaptor.AddChild(root_0, STORAGE283_tree);

                    	        	set284 = (IToken)input.LT(1);
                    	        	if ( input.LA(1) == DEFAULT || (input.LA(1) >= DISK && input.LA(1) <= MEMORY) ) 
                    	        	{
                    	        	    input.Consume();
                    	        	    adaptor.AddChild(root_0, (object)adaptor.Create(set284));
                    	        	    state.errorRecovery = false;
                    	        	}
                    	        	else 
                    	        	{
                    	        	    MismatchedSetException mse = new MismatchedSetException(null,input);
                    	        	    throw mse;
                    	        	}


                    	        }
                    	        break;

                    	}


                    }
                    break;
                case 21 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:197:5: UNION ( EQUALS )? LPAREN tbl_name ( COMMA tbl_name )* RPAREN
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	UNION285=(IToken)Match(input,UNION,FOLLOW_UNION_in_table_option1970); 
                    		UNION285_tree = (object)adaptor.Create(UNION285);
                    		adaptor.AddChild(root_0, UNION285_tree);

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:197:11: ( EQUALS )?
                    	int alt102 = 2;
                    	int LA102_0 = input.LA(1);

                    	if ( (LA102_0 == EQUALS) )
                    	{
                    	    alt102 = 1;
                    	}
                    	switch (alt102) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:197:11: EQUALS
                    	        {
                    	        	EQUALS286=(IToken)Match(input,EQUALS,FOLLOW_EQUALS_in_table_option1972); 
                    	        		EQUALS286_tree = (object)adaptor.Create(EQUALS286);
                    	        		adaptor.AddChild(root_0, EQUALS286_tree);


                    	        }
                    	        break;

                    	}

                    	LPAREN287=(IToken)Match(input,LPAREN,FOLLOW_LPAREN_in_table_option1975); 
                    		LPAREN287_tree = (object)adaptor.Create(LPAREN287);
                    		adaptor.AddChild(root_0, LPAREN287_tree);

                    	PushFollow(FOLLOW_tbl_name_in_table_option1977);
                    	tbl_name288 = tbl_name();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, tbl_name288.Tree);
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:197:35: ( COMMA tbl_name )*
                    	do 
                    	{
                    	    int alt103 = 2;
                    	    int LA103_0 = input.LA(1);

                    	    if ( (LA103_0 == COMMA) )
                    	    {
                    	        alt103 = 1;
                    	    }


                    	    switch (alt103) 
                    		{
                    			case 1 :
                    			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:197:36: COMMA tbl_name
                    			    {
                    			    	COMMA289=(IToken)Match(input,COMMA,FOLLOW_COMMA_in_table_option1980); 
                    			    		COMMA289_tree = (object)adaptor.Create(COMMA289);
                    			    		adaptor.AddChild(root_0, COMMA289_tree);

                    			    	PushFollow(FOLLOW_tbl_name_in_table_option1982);
                    			    	tbl_name290 = tbl_name();
                    			    	state.followingStackPointer--;

                    			    	adaptor.AddChild(root_0, tbl_name290.Tree);

                    			    }
                    			    break;

                    			default:
                    			    goto loop103;
                    	    }
                    	} while (true);

                    	loop103:
                    		;	// Stops C# compiler whining that label 'loop103' has no statements

                    	RPAREN291=(IToken)Match(input,RPAREN,FOLLOW_RPAREN_in_table_option1986); 
                    		RPAREN291_tree = (object)adaptor.Create(RPAREN291);
                    		adaptor.AddChild(root_0, RPAREN291_tree);


                    }
                    break;

            }
            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "table_option"

    public class find_deps_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "find_deps"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:199:1: find_deps[DepsCollector dc] : ( find_dep_item[dc] )* ;
    public MySQLParser.find_deps_return find_deps(DepsCollector dc) // throws RecognitionException [1]
    {   
        MySQLParser.find_deps_return retval = new MySQLParser.find_deps_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        MySQLParser.find_dep_item_return find_dep_item292 = default(MySQLParser.find_dep_item_return);



        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:200:5: ( ( find_dep_item[dc] )* )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:200:7: ( find_dep_item[dc] )*
            {
            	root_0 = (object)adaptor.GetNilNode();

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:200:7: ( find_dep_item[dc] )*
            	do 
            	{
            	    int alt105 = 2;
            	    int LA105_0 = input.LA(1);

            	    if ( ((LA105_0 >= CREATE && LA105_0 <= UPDATE) || (LA105_0 >= AUTO_INCREMENT && LA105_0 <= DATETIME) || (LA105_0 >= CHAR && LA105_0 <= T_BLOB) || (LA105_0 >= OR && LA105_0 <= FORCE) || LA105_0 == AT || (LA105_0 >= CASE && LA105_0 <= END) || (LA105_0 >= INTERVAL && LA105_0 <= DOLLAR) || (LA105_0 >= ABORT && LA105_0 <= VIRTUAL)) )
            	    {
            	        alt105 = 1;
            	    }


            	    switch (alt105) 
            		{
            			case 1 :
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:200:7: find_dep_item[dc]
            			    {
            			    	PushFollow(FOLLOW_find_dep_item_in_find_deps1999);
            			    	find_dep_item292 = find_dep_item(dc);
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, find_dep_item292.Tree);

            			    }
            			    break;

            			default:
            			    goto loop105;
            	    }
            	} while (true);

            	loop105:
            		;	// Stops C# compiler whining that label 'loop105' has no statements


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "find_deps"

    public class find_dep_item_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "find_dep_item"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:201:1: find_dep_item[DepsCollector dc] : ( keyword | operator_no_dot | T_NSTRING | T_STRING | T_INTEGER | T_FLOAT | T_BINARYNUM | T_BLOB | name1= id ( DOT ( DOT )? name2= id )* );
    public MySQLParser.find_dep_item_return find_dep_item(DepsCollector dc) // throws RecognitionException [1]
    {   
        MySQLParser.find_dep_item_return retval = new MySQLParser.find_dep_item_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken T_NSTRING295 = null;
        IToken T_STRING296 = null;
        IToken T_INTEGER297 = null;
        IToken T_FLOAT298 = null;
        IToken T_BINARYNUM299 = null;
        IToken T_BLOB300 = null;
        IToken DOT301 = null;
        IToken DOT302 = null;
        MySQLParser.id_return name1 = default(MySQLParser.id_return);

        MySQLParser.id_return name2 = default(MySQLParser.id_return);

        MySQLParser.keyword_return keyword293 = default(MySQLParser.keyword_return);

        MySQLParser.operator_no_dot_return operator_no_dot294 = default(MySQLParser.operator_no_dot_return);


        object T_NSTRING295_tree=null;
        object T_STRING296_tree=null;
        object T_INTEGER297_tree=null;
        object T_FLOAT298_tree=null;
        object T_BINARYNUM299_tree=null;
        object T_BLOB300_tree=null;
        object DOT301_tree=null;
        object DOT302_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:202:5: ( keyword | operator_no_dot | T_NSTRING | T_STRING | T_INTEGER | T_FLOAT | T_BINARYNUM | T_BLOB | name1= id ( DOT ( DOT )? name2= id )* )
            int alt108 = 9;
            switch ( input.LA(1) ) 
            {
            case CREATE:
            case TEMPORARY:
            case TABLE:
            case CONSTRAINT:
            case CHECK:
            case FULLTEXT:
            case SPATIAL:
            case UNIQUE:
            case INDEX:
            case KEY:
            case PRIMARY:
            case FOREIGN:
            case USING:
            case ASC:
            case DESC:
            case KEY_BLOCK_SIZE:
            case WITH:
            case PARSER:
            case NOT:
            case NULL:
            case DEFAULT:
            case ON:
            case UPDATE:
            case AUTO_INCREMENT:
            case COMMENT:
            case COLUMN_FORMAT:
            case FIXED:
            case DYNAMIC:
            case STORAGE:
            case DISK:
            case MEMORY:
            case REFERENCES:
            case MATCH:
            case FULL:
            case PARTIAL:
            case SIMPLE:
            case DELETE:
            case RESTRICT:
            case CASCADE:
            case SET:
            case NO:
            case ACTION:
            case UNSIGNED:
            case ZEROFILL:
            case CHARACTER:
            case COLLATE:
            case BIT:
            case TINYINT:
            case SMALLINT:
            case MEDIUMINT:
            case INT:
            case INTEGER:
            case BIGINT:
            case REAL:
            case DOUBLE:
            case FLOAT:
            case DECIMAL:
            case NUMERIC:
            case DATE:
            case TIME:
            case TIMESTAMP:
            case DATETIME:
            case CHAR:
            case VARCHAR:
            case BINARY:
            case VARBINARY:
            case TINYBLOB:
            case BLOB:
            case MEDIUMBLOB:
            case LONGBLOB:
            case TINYTEXT:
            case TEXT:
            case MEDIUMTEXT:
            case LONGTEXT:
            case ENUM:
            case GEOMETRY:
            case POINT:
            case LINESTRING:
            case POLYGON:
            case MULTIPOINT:
            case MULTILINESTRING:
            case MULTIPOLYGON:
            case GEOMETRYCOLLECTION:
            case ENGINE:
            case TYPE:
            case AVG_ROW_LENGTH:
            case CHARSET:
            case CHECKSUM:
            case CONNECTION:
            case DATA:
            case DIRECTORY:
            case DELAY_KEY_WRITE:
            case INSERT_METHOD:
            case FIRST:
            case LAST:
            case MAX_ROWS:
            case MIN_ROWS:
            case PACK_KEYS:
            case PASSWORD:
            case ROW_FORMAT:
            case COMPRESSED:
            case REDUNDANT:
            case COMPACT:
            case TABLESPACE:
            case UNION:
            case OR:
            case REPLACE:
            case ALGORITHM:
            case DEFINER:
            case SQL:
            case SECURITY:
            case INVOKER:
            case VIEW:
            case AS:
            case CASCADED:
            case LOCAL:
            case OPTION:
            case ALL:
            case ORDER:
            case BY:
            case LIMIT:
            case OFFSET:
            case FOR:
            case LOCK:
            case IN:
            case SHARE:
            case MODE:
            case SELECT:
            case DISTINCT:
            case DISTINCTROW:
            case HIGH_PRIORITY:
            case STRAIGHT_JOIN:
            case SQL_SMALL_RESULT:
            case SQL_BIG_RESULT:
            case SQL_BUFFER_RESULT:
            case SQL_CACHE:
            case SQL_NO_CACHE:
            case SQL_CALC_FOUND_ROWS:
            case FROM:
            case WHERE:
            case GROUP:
            case ROLLUP:
            case HAVING:
            case OJ:
            case LEFT:
            case OUTER:
            case JOIN:
            case INNER:
            case CROSS:
            case RIGHT:
            case NATURAL:
            case USE:
            case IGNORE:
            case FORCE:
            case CASE:
            case WHEN:
            case THEN:
            case ELSE:
            case END:
            case INTERVAL:
            case DIV:
            case MOD:
            case IS:
            case LIKE:
            case REGEXP:
            case BETWEEN:
            case AND:
            case XOR:
            case ABORT:
            case ADD:
            case AFTER:
            case ALTER:
            case ANALYZE:
            case ATTACH:
            case AUTOINCREMENT:
            case BEFORE:
            case BEGIN:
            case CAST:
            case COLUMN:
            case COMMIT:
            case CONFLICT:
            case DATABASE:
            case DEFERRABLE:
            case DEFERRED:
            case DETACH:
            case DROP:
            case EACH:
            case ESCAPE:
            case EXCEPT:
            case EXCLUSIVE:
            case EXISTS:
            case EXPLAIN:
            case FAIL:
            case GLOB:
            case IF:
            case IMMEDIATE:
            case INDEXED:
            case INITIALLY:
            case INSERT:
            case INSTEAD:
            case INTERSECT:
            case INTO:
            case ISNULL:
            case NOTNULL:
            case OF:
            case PLAN:
            case PRAGMA:
            case QUERY:
            case RAISE:
            case REINDEX:
            case RELEASE:
            case RENAME:
            case ROLLBACK:
            case ROW:
            case SAVEPOINT:
            case TEMP:
            case TO:
            case TRANSACTION:
            case TRIGGER:
            case VACUUM:
            case VALUES:
            case VIRTUAL:
            	{
                alt108 = 1;
                }
                break;
            case LPAREN:
            case COMMA:
            case RPAREN:
            case EQUALS:
            case ASTERISK:
            case AT:
            case EXCLAMATION:
            case MINUS:
            case TILDA:
            case ARROW_UP:
            case SLASH:
            case PERCENT:
            case PLUS:
            case SHIFT_LEFT:
            case SHIFT_RIGHT:
            case AMPERSAND:
            case PIPE:
            case SAFEEQUALS:
            case GREATER_OR_EQ:
            case GREATER:
            case LESS_OR_EQ:
            case LESS:
            case NOT_EQUALS2:
            case NOT_EQUALS:
            case DOUBLE_AMPERSAND:
            case DOUBLE_PIPE:
            case EQUALS2:
            case SEMI:
            case QUESTION:
            case COLON:
            case DOLLAR:
            	{
                alt108 = 2;
                }
                break;
            case T_NSTRING:
            	{
                alt108 = 3;
                }
                break;
            case T_STRING:
            	{
                alt108 = 4;
                }
                break;
            case T_INTEGER:
            	{
                alt108 = 5;
                }
                break;
            case T_FLOAT:
            	{
                alt108 = 6;
                }
                break;
            case T_BINARYNUM:
            	{
                alt108 = 7;
                }
                break;
            case T_BLOB:
            	{
                alt108 = 8;
                }
                break;
            case T_IDENT:
            case T_QUOTED_IDENT:
            	{
                alt108 = 9;
                }
                break;
            	default:
            	    NoViableAltException nvae_d108s0 =
            	        new NoViableAltException("", 108, 0, input);

            	    throw nvae_d108s0;
            }

            switch (alt108) 
            {
                case 1 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:202:7: keyword
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_keyword_in_find_dep_item2013);
                    	keyword293 = keyword();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, keyword293.Tree);

                    }
                    break;
                case 2 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:202:17: operator_no_dot
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_operator_no_dot_in_find_dep_item2017);
                    	operator_no_dot294 = operator_no_dot();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, operator_no_dot294.Tree);

                    }
                    break;
                case 3 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:202:35: T_NSTRING
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	T_NSTRING295=(IToken)Match(input,T_NSTRING,FOLLOW_T_NSTRING_in_find_dep_item2021); 
                    		T_NSTRING295_tree = (object)adaptor.Create(T_NSTRING295);
                    		adaptor.AddChild(root_0, T_NSTRING295_tree);


                    }
                    break;
                case 4 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:202:47: T_STRING
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	T_STRING296=(IToken)Match(input,T_STRING,FOLLOW_T_STRING_in_find_dep_item2025); 
                    		T_STRING296_tree = (object)adaptor.Create(T_STRING296);
                    		adaptor.AddChild(root_0, T_STRING296_tree);


                    }
                    break;
                case 5 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:202:58: T_INTEGER
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	T_INTEGER297=(IToken)Match(input,T_INTEGER,FOLLOW_T_INTEGER_in_find_dep_item2029); 
                    		T_INTEGER297_tree = (object)adaptor.Create(T_INTEGER297);
                    		adaptor.AddChild(root_0, T_INTEGER297_tree);


                    }
                    break;
                case 6 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:202:70: T_FLOAT
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	T_FLOAT298=(IToken)Match(input,T_FLOAT,FOLLOW_T_FLOAT_in_find_dep_item2033); 
                    		T_FLOAT298_tree = (object)adaptor.Create(T_FLOAT298);
                    		adaptor.AddChild(root_0, T_FLOAT298_tree);


                    }
                    break;
                case 7 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:202:80: T_BINARYNUM
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	T_BINARYNUM299=(IToken)Match(input,T_BINARYNUM,FOLLOW_T_BINARYNUM_in_find_dep_item2037); 
                    		T_BINARYNUM299_tree = (object)adaptor.Create(T_BINARYNUM299);
                    		adaptor.AddChild(root_0, T_BINARYNUM299_tree);


                    }
                    break;
                case 8 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:202:94: T_BLOB
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	T_BLOB300=(IToken)Match(input,T_BLOB,FOLLOW_T_BLOB_in_find_dep_item2041); 
                    		T_BLOB300_tree = (object)adaptor.Create(T_BLOB300);
                    		adaptor.AddChild(root_0, T_BLOB300_tree);


                    }
                    break;
                case 9 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:203:9: name1= id ( DOT ( DOT )? name2= id )*
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_id_in_find_dep_item2055);
                    	name1 = id();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, name1.Tree);
                    	 var name=new DepsName();name.AddComponent(UnquoteName(((name1 != null) ? input.ToString((IToken)(name1.Start),(IToken)(name1.Stop)) : null))); 
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:204:9: ( DOT ( DOT )? name2= id )*
                    	do 
                    	{
                    	    int alt107 = 2;
                    	    int LA107_0 = input.LA(1);

                    	    if ( (LA107_0 == DOT) )
                    	    {
                    	        alt107 = 1;
                    	    }


                    	    switch (alt107) 
                    		{
                    			case 1 :
                    			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:204:10: DOT ( DOT )? name2= id
                    			    {
                    			    	DOT301=(IToken)Match(input,DOT,FOLLOW_DOT_in_find_dep_item2068); 
                    			    		DOT301_tree = (object)adaptor.Create(DOT301);
                    			    		adaptor.AddChild(root_0, DOT301_tree);

                    			    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:204:14: ( DOT )?
                    			    	int alt106 = 2;
                    			    	int LA106_0 = input.LA(1);

                    			    	if ( (LA106_0 == DOT) )
                    			    	{
                    			    	    alt106 = 1;
                    			    	}
                    			    	switch (alt106) 
                    			    	{
                    			    	    case 1 :
                    			    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:204:14: DOT
                    			    	        {
                    			    	        	DOT302=(IToken)Match(input,DOT,FOLLOW_DOT_in_find_dep_item2070); 
                    			    	        		DOT302_tree = (object)adaptor.Create(DOT302);
                    			    	        		adaptor.AddChild(root_0, DOT302_tree);


                    			    	        }
                    			    	        break;

                    			    	}

                    			    	PushFollow(FOLLOW_id_in_find_dep_item2075);
                    			    	name2 = id();
                    			    	state.followingStackPointer--;

                    			    	adaptor.AddChild(root_0, name2.Tree);
                    			    	 name.AddComponent(UnquoteName(((name2 != null) ? input.ToString((IToken)(name2.Start),(IToken)(name2.Stop)) : null)));

                    			    }
                    			    break;

                    			default:
                    			    goto loop107;
                    	    }
                    	} while (true);

                    	loop107:
                    		;	// Stops C# compiler whining that label 'loop107' has no statements

                    	dc.AddName(name); 

                    }
                    break;

            }
            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "find_dep_item"

    public class create_view_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "create_view"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:207:1: create_view : CREATE ( OR REPLACE )? f_inc ( f_nl ALGORITHM EQUALS id )? ( f_nl DEFINER EQUALS user )? ( f_nl SQL SECURITY ( DEFINER | INVOKER ) )? f_dec f_nl VIEW id ( LPAREN id ( COMMA id )* RPAREN )? AS f_nl ( select_statement | LPAREN select_statement RPAREN ) ( WITH ( CASCADED | LOCAL )? CHECK OPTION )? ;
    public MySQLParser.create_view_return create_view() // throws RecognitionException [1]
    {   
        MySQLParser.create_view_return retval = new MySQLParser.create_view_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken CREATE303 = null;
        IToken OR304 = null;
        IToken REPLACE305 = null;
        IToken ALGORITHM308 = null;
        IToken EQUALS309 = null;
        IToken DEFINER312 = null;
        IToken EQUALS313 = null;
        IToken SQL316 = null;
        IToken SECURITY317 = null;
        IToken set318 = null;
        IToken VIEW321 = null;
        IToken LPAREN323 = null;
        IToken COMMA325 = null;
        IToken RPAREN327 = null;
        IToken AS328 = null;
        IToken LPAREN331 = null;
        IToken RPAREN333 = null;
        IToken WITH334 = null;
        IToken set335 = null;
        IToken CHECK336 = null;
        IToken OPTION337 = null;
        MySQLParser.f_inc_return f_inc306 = default(MySQLParser.f_inc_return);

        MySQLParser.f_nl_return f_nl307 = default(MySQLParser.f_nl_return);

        MySQLParser.id_return id310 = default(MySQLParser.id_return);

        MySQLParser.f_nl_return f_nl311 = default(MySQLParser.f_nl_return);

        MySQLParser.user_return user314 = default(MySQLParser.user_return);

        MySQLParser.f_nl_return f_nl315 = default(MySQLParser.f_nl_return);

        MySQLParser.f_dec_return f_dec319 = default(MySQLParser.f_dec_return);

        MySQLParser.f_nl_return f_nl320 = default(MySQLParser.f_nl_return);

        MySQLParser.id_return id322 = default(MySQLParser.id_return);

        MySQLParser.id_return id324 = default(MySQLParser.id_return);

        MySQLParser.id_return id326 = default(MySQLParser.id_return);

        MySQLParser.f_nl_return f_nl329 = default(MySQLParser.f_nl_return);

        MySQLParser.select_statement_return select_statement330 = default(MySQLParser.select_statement_return);

        MySQLParser.select_statement_return select_statement332 = default(MySQLParser.select_statement_return);


        object CREATE303_tree=null;
        object OR304_tree=null;
        object REPLACE305_tree=null;
        object ALGORITHM308_tree=null;
        object EQUALS309_tree=null;
        object DEFINER312_tree=null;
        object EQUALS313_tree=null;
        object SQL316_tree=null;
        object SECURITY317_tree=null;
        object set318_tree=null;
        object VIEW321_tree=null;
        object LPAREN323_tree=null;
        object COMMA325_tree=null;
        object RPAREN327_tree=null;
        object AS328_tree=null;
        object LPAREN331_tree=null;
        object RPAREN333_tree=null;
        object WITH334_tree=null;
        object set335_tree=null;
        object CHECK336_tree=null;
        object OPTION337_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:207:12: ( CREATE ( OR REPLACE )? f_inc ( f_nl ALGORITHM EQUALS id )? ( f_nl DEFINER EQUALS user )? ( f_nl SQL SECURITY ( DEFINER | INVOKER ) )? f_dec f_nl VIEW id ( LPAREN id ( COMMA id )* RPAREN )? AS f_nl ( select_statement | LPAREN select_statement RPAREN ) ( WITH ( CASCADED | LOCAL )? CHECK OPTION )? )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:208:3: CREATE ( OR REPLACE )? f_inc ( f_nl ALGORITHM EQUALS id )? ( f_nl DEFINER EQUALS user )? ( f_nl SQL SECURITY ( DEFINER | INVOKER ) )? f_dec f_nl VIEW id ( LPAREN id ( COMMA id )* RPAREN )? AS f_nl ( select_statement | LPAREN select_statement RPAREN ) ( WITH ( CASCADED | LOCAL )? CHECK OPTION )?
            {
            	root_0 = (object)adaptor.GetNilNode();

            	CREATE303=(IToken)Match(input,CREATE,FOLLOW_CREATE_in_create_view2100); 
            		CREATE303_tree = (object)adaptor.Create(CREATE303);
            		adaptor.AddChild(root_0, CREATE303_tree);

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:208:10: ( OR REPLACE )?
            	int alt109 = 2;
            	int LA109_0 = input.LA(1);

            	if ( (LA109_0 == OR) )
            	{
            	    alt109 = 1;
            	}
            	switch (alt109) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:208:11: OR REPLACE
            	        {
            	        	OR304=(IToken)Match(input,OR,FOLLOW_OR_in_create_view2103); 
            	        		OR304_tree = (object)adaptor.Create(OR304);
            	        		adaptor.AddChild(root_0, OR304_tree);

            	        	REPLACE305=(IToken)Match(input,REPLACE,FOLLOW_REPLACE_in_create_view2105); 
            	        		REPLACE305_tree = (object)adaptor.Create(REPLACE305);
            	        		adaptor.AddChild(root_0, REPLACE305_tree);


            	        }
            	        break;

            	}

            	PushFollow(FOLLOW_f_inc_in_create_view2109);
            	f_inc306 = f_inc();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, f_inc306.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:209:9: ( f_nl ALGORITHM EQUALS id )?
            	int alt110 = 2;
            	int LA110_0 = input.LA(1);

            	if ( (LA110_0 == ALGORITHM) )
            	{
            	    alt110 = 1;
            	}
            	switch (alt110) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:209:10: f_nl ALGORITHM EQUALS id
            	        {
            	        	PushFollow(FOLLOW_f_nl_in_create_view2120);
            	        	f_nl307 = f_nl();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, f_nl307.Tree);
            	        	ALGORITHM308=(IToken)Match(input,ALGORITHM,FOLLOW_ALGORITHM_in_create_view2122); 
            	        		ALGORITHM308_tree = (object)adaptor.Create(ALGORITHM308);
            	        		adaptor.AddChild(root_0, ALGORITHM308_tree);

            	        	EQUALS309=(IToken)Match(input,EQUALS,FOLLOW_EQUALS_in_create_view2124); 
            	        		EQUALS309_tree = (object)adaptor.Create(EQUALS309);
            	        		adaptor.AddChild(root_0, EQUALS309_tree);

            	        	PushFollow(FOLLOW_id_in_create_view2126);
            	        	id310 = id();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, id310.Tree);

            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:210:9: ( f_nl DEFINER EQUALS user )?
            	int alt111 = 2;
            	int LA111_0 = input.LA(1);

            	if ( (LA111_0 == DEFINER) )
            	{
            	    alt111 = 1;
            	}
            	switch (alt111) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:210:10: f_nl DEFINER EQUALS user
            	        {
            	        	PushFollow(FOLLOW_f_nl_in_create_view2139);
            	        	f_nl311 = f_nl();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, f_nl311.Tree);
            	        	DEFINER312=(IToken)Match(input,DEFINER,FOLLOW_DEFINER_in_create_view2141); 
            	        		DEFINER312_tree = (object)adaptor.Create(DEFINER312);
            	        		adaptor.AddChild(root_0, DEFINER312_tree);

            	        	EQUALS313=(IToken)Match(input,EQUALS,FOLLOW_EQUALS_in_create_view2143); 
            	        		EQUALS313_tree = (object)adaptor.Create(EQUALS313);
            	        		adaptor.AddChild(root_0, EQUALS313_tree);

            	        	PushFollow(FOLLOW_user_in_create_view2145);
            	        	user314 = user();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, user314.Tree);

            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:211:9: ( f_nl SQL SECURITY ( DEFINER | INVOKER ) )?
            	int alt112 = 2;
            	int LA112_0 = input.LA(1);

            	if ( (LA112_0 == SQL) )
            	{
            	    alt112 = 1;
            	}
            	switch (alt112) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:211:10: f_nl SQL SECURITY ( DEFINER | INVOKER )
            	        {
            	        	PushFollow(FOLLOW_f_nl_in_create_view2158);
            	        	f_nl315 = f_nl();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, f_nl315.Tree);
            	        	SQL316=(IToken)Match(input,SQL,FOLLOW_SQL_in_create_view2160); 
            	        		SQL316_tree = (object)adaptor.Create(SQL316);
            	        		adaptor.AddChild(root_0, SQL316_tree);

            	        	SECURITY317=(IToken)Match(input,SECURITY,FOLLOW_SECURITY_in_create_view2162); 
            	        		SECURITY317_tree = (object)adaptor.Create(SECURITY317);
            	        		adaptor.AddChild(root_0, SECURITY317_tree);

            	        	set318 = (IToken)input.LT(1);
            	        	if ( input.LA(1) == DEFINER || input.LA(1) == INVOKER ) 
            	        	{
            	        	    input.Consume();
            	        	    adaptor.AddChild(root_0, (object)adaptor.Create(set318));
            	        	    state.errorRecovery = false;
            	        	}
            	        	else 
            	        	{
            	        	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	        	    throw mse;
            	        	}


            	        }
            	        break;

            	}

            	PushFollow(FOLLOW_f_dec_in_create_view2180);
            	f_dec319 = f_dec();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, f_dec319.Tree);
            	PushFollow(FOLLOW_f_nl_in_create_view2182);
            	f_nl320 = f_nl();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, f_nl320.Tree);
            	VIEW321=(IToken)Match(input,VIEW,FOLLOW_VIEW_in_create_view2184); 
            		VIEW321_tree = (object)adaptor.Create(VIEW321);
            		adaptor.AddChild(root_0, VIEW321_tree);

            	PushFollow(FOLLOW_id_in_create_view2186);
            	id322 = id();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, id322.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:212:28: ( LPAREN id ( COMMA id )* RPAREN )?
            	int alt114 = 2;
            	int LA114_0 = input.LA(1);

            	if ( (LA114_0 == LPAREN) )
            	{
            	    alt114 = 1;
            	}
            	switch (alt114) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:212:30: LPAREN id ( COMMA id )* RPAREN
            	        {
            	        	LPAREN323=(IToken)Match(input,LPAREN,FOLLOW_LPAREN_in_create_view2190); 
            	        		LPAREN323_tree = (object)adaptor.Create(LPAREN323);
            	        		adaptor.AddChild(root_0, LPAREN323_tree);

            	        	PushFollow(FOLLOW_id_in_create_view2192);
            	        	id324 = id();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, id324.Tree);
            	        	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:212:40: ( COMMA id )*
            	        	do 
            	        	{
            	        	    int alt113 = 2;
            	        	    int LA113_0 = input.LA(1);

            	        	    if ( (LA113_0 == COMMA) )
            	        	    {
            	        	        alt113 = 1;
            	        	    }


            	        	    switch (alt113) 
            	        		{
            	        			case 1 :
            	        			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:212:41: COMMA id
            	        			    {
            	        			    	COMMA325=(IToken)Match(input,COMMA,FOLLOW_COMMA_in_create_view2195); 
            	        			    		COMMA325_tree = (object)adaptor.Create(COMMA325);
            	        			    		adaptor.AddChild(root_0, COMMA325_tree);

            	        			    	PushFollow(FOLLOW_id_in_create_view2197);
            	        			    	id326 = id();
            	        			    	state.followingStackPointer--;

            	        			    	adaptor.AddChild(root_0, id326.Tree);

            	        			    }
            	        			    break;

            	        			default:
            	        			    goto loop113;
            	        	    }
            	        	} while (true);

            	        	loop113:
            	        		;	// Stops C# compiler whining that label 'loop113' has no statements

            	        	RPAREN327=(IToken)Match(input,RPAREN,FOLLOW_RPAREN_in_create_view2201); 
            	        		RPAREN327_tree = (object)adaptor.Create(RPAREN327);
            	        		adaptor.AddChild(root_0, RPAREN327_tree);


            	        }
            	        break;

            	}

            	AS328=(IToken)Match(input,AS,FOLLOW_AS_in_create_view2213); 
            		AS328_tree = (object)adaptor.Create(AS328);
            		adaptor.AddChild(root_0, AS328_tree);

            	PushFollow(FOLLOW_f_nl_in_create_view2215);
            	f_nl329 = f_nl();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, f_nl329.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:213:17: ( select_statement | LPAREN select_statement RPAREN )
            	int alt115 = 2;
            	int LA115_0 = input.LA(1);

            	if ( (LA115_0 == SELECT) )
            	{
            	    alt115 = 1;
            	}
            	else if ( (LA115_0 == LPAREN) )
            	{
            	    alt115 = 2;
            	}
            	else 
            	{
            	    NoViableAltException nvae_d115s0 =
            	        new NoViableAltException("", 115, 0, input);

            	    throw nvae_d115s0;
            	}
            	switch (alt115) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:213:18: select_statement
            	        {
            	        	PushFollow(FOLLOW_select_statement_in_create_view2218);
            	        	select_statement330 = select_statement();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, select_statement330.Tree);

            	        }
            	        break;
            	    case 2 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:213:37: LPAREN select_statement RPAREN
            	        {
            	        	LPAREN331=(IToken)Match(input,LPAREN,FOLLOW_LPAREN_in_create_view2222); 
            	        		LPAREN331_tree = (object)adaptor.Create(LPAREN331);
            	        		adaptor.AddChild(root_0, LPAREN331_tree);

            	        	PushFollow(FOLLOW_select_statement_in_create_view2224);
            	        	select_statement332 = select_statement();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, select_statement332.Tree);
            	        	RPAREN333=(IToken)Match(input,RPAREN,FOLLOW_RPAREN_in_create_view2226); 
            	        		RPAREN333_tree = (object)adaptor.Create(RPAREN333);
            	        		adaptor.AddChild(root_0, RPAREN333_tree);


            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:214:9: ( WITH ( CASCADED | LOCAL )? CHECK OPTION )?
            	int alt117 = 2;
            	int LA117_0 = input.LA(1);

            	if ( (LA117_0 == WITH) )
            	{
            	    alt117 = 1;
            	}
            	switch (alt117) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:214:10: WITH ( CASCADED | LOCAL )? CHECK OPTION
            	        {
            	        	WITH334=(IToken)Match(input,WITH,FOLLOW_WITH_in_create_view2238); 
            	        		WITH334_tree = (object)adaptor.Create(WITH334);
            	        		adaptor.AddChild(root_0, WITH334_tree);

            	        	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:214:15: ( CASCADED | LOCAL )?
            	        	int alt116 = 2;
            	        	int LA116_0 = input.LA(1);

            	        	if ( ((LA116_0 >= CASCADED && LA116_0 <= LOCAL)) )
            	        	{
            	        	    alt116 = 1;
            	        	}
            	        	switch (alt116) 
            	        	{
            	        	    case 1 :
            	        	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:
            	        	        {
            	        	        	set335 = (IToken)input.LT(1);
            	        	        	if ( (input.LA(1) >= CASCADED && input.LA(1) <= LOCAL) ) 
            	        	        	{
            	        	        	    input.Consume();
            	        	        	    adaptor.AddChild(root_0, (object)adaptor.Create(set335));
            	        	        	    state.errorRecovery = false;
            	        	        	}
            	        	        	else 
            	        	        	{
            	        	        	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	        	        	    throw mse;
            	        	        	}


            	        	        }
            	        	        break;

            	        	}

            	        	CHECK336=(IToken)Match(input,CHECK,FOLLOW_CHECK_in_create_view2249); 
            	        		CHECK336_tree = (object)adaptor.Create(CHECK336);
            	        		adaptor.AddChild(root_0, CHECK336_tree);

            	        	OPTION337=(IToken)Match(input,OPTION,FOLLOW_OPTION_in_create_view2251); 
            	        		OPTION337_tree = (object)adaptor.Create(OPTION337);
            	        		adaptor.AddChild(root_0, OPTION337_tree);


            	        }
            	        break;

            	}


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "create_view"

    public class f_nl_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "f_nl"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:218:1: f_nl : -> F_NL ;
    public MySQLParser.f_nl_return f_nl() // throws RecognitionException [1]
    {   
        MySQLParser.f_nl_return retval = new MySQLParser.f_nl_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:218:5: ( -> F_NL )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:218:7: 
            {

            	// AST REWRITE
            	// elements:          
            	// token labels:      
            	// rule labels:       retval
            	// token list labels: 
            	// rule list labels:  
            	// wildcard labels: 
            	retval.Tree = root_0;
            	RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval!=null ? retval.Tree : null);

            	root_0 = (object)adaptor.GetNilNode();
            	// 218:7: -> F_NL
            	{
            	    adaptor.AddChild(root_0, (object)adaptor.Create(F_NL, "F_NL"));

            	}

            	retval.Tree = root_0;retval.Tree = root_0;
            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "f_nl"

    public class f_dec_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "f_dec"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:219:1: f_dec : -> F_DEC ;
    public MySQLParser.f_dec_return f_dec() // throws RecognitionException [1]
    {   
        MySQLParser.f_dec_return retval = new MySQLParser.f_dec_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:219:6: ( -> F_DEC )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:219:8: 
            {

            	// AST REWRITE
            	// elements:          
            	// token labels:      
            	// rule labels:       retval
            	// token list labels: 
            	// rule list labels:  
            	// wildcard labels: 
            	retval.Tree = root_0;
            	RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval!=null ? retval.Tree : null);

            	root_0 = (object)adaptor.GetNilNode();
            	// 219:8: -> F_DEC
            	{
            	    adaptor.AddChild(root_0, (object)adaptor.Create(F_DEC, "F_DEC"));

            	}

            	retval.Tree = root_0;retval.Tree = root_0;
            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "f_dec"

    public class f_inc_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "f_inc"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:220:1: f_inc : -> F_INC ;
    public MySQLParser.f_inc_return f_inc() // throws RecognitionException [1]
    {   
        MySQLParser.f_inc_return retval = new MySQLParser.f_inc_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:220:6: ( -> F_INC )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:220:8: 
            {

            	// AST REWRITE
            	// elements:          
            	// token labels:      
            	// rule labels:       retval
            	// token list labels: 
            	// rule list labels:  
            	// wildcard labels: 
            	retval.Tree = root_0;
            	RewriteRuleSubtreeStream stream_retval = new RewriteRuleSubtreeStream(adaptor, "rule retval", retval!=null ? retval.Tree : null);

            	root_0 = (object)adaptor.GetNilNode();
            	// 220:8: -> F_INC
            	{
            	    adaptor.AddChild(root_0, (object)adaptor.Create(F_INC, "F_INC"));

            	}

            	retval.Tree = root_0;retval.Tree = root_0;
            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "f_inc"

    public class select_statement_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "select_statement"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:222:1: select_statement : select_statement_component ( f_nl UNION ( ALL )? f_nl select_statement_component )* ( f_nl ORDER BY selectby_expr ( COMMA selectby_expr )* )? ( f_nl LIMIT ( ( T_INTEGER COMMA )? INTEGER | INTEGER OFFSET INTEGER ) )? ( f_nl FOR UPDATE | LOCK IN SHARE MODE )? ;
    public MySQLParser.select_statement_return select_statement() // throws RecognitionException [1]
    {   
        MySQLParser.select_statement_return retval = new MySQLParser.select_statement_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken UNION340 = null;
        IToken ALL341 = null;
        IToken ORDER345 = null;
        IToken BY346 = null;
        IToken COMMA348 = null;
        IToken LIMIT351 = null;
        IToken T_INTEGER352 = null;
        IToken COMMA353 = null;
        IToken INTEGER354 = null;
        IToken INTEGER355 = null;
        IToken OFFSET356 = null;
        IToken INTEGER357 = null;
        IToken FOR359 = null;
        IToken UPDATE360 = null;
        IToken LOCK361 = null;
        IToken IN362 = null;
        IToken SHARE363 = null;
        IToken MODE364 = null;
        MySQLParser.select_statement_component_return select_statement_component338 = default(MySQLParser.select_statement_component_return);

        MySQLParser.f_nl_return f_nl339 = default(MySQLParser.f_nl_return);

        MySQLParser.f_nl_return f_nl342 = default(MySQLParser.f_nl_return);

        MySQLParser.select_statement_component_return select_statement_component343 = default(MySQLParser.select_statement_component_return);

        MySQLParser.f_nl_return f_nl344 = default(MySQLParser.f_nl_return);

        MySQLParser.selectby_expr_return selectby_expr347 = default(MySQLParser.selectby_expr_return);

        MySQLParser.selectby_expr_return selectby_expr349 = default(MySQLParser.selectby_expr_return);

        MySQLParser.f_nl_return f_nl350 = default(MySQLParser.f_nl_return);

        MySQLParser.f_nl_return f_nl358 = default(MySQLParser.f_nl_return);


        object UNION340_tree=null;
        object ALL341_tree=null;
        object ORDER345_tree=null;
        object BY346_tree=null;
        object COMMA348_tree=null;
        object LIMIT351_tree=null;
        object T_INTEGER352_tree=null;
        object COMMA353_tree=null;
        object INTEGER354_tree=null;
        object INTEGER355_tree=null;
        object OFFSET356_tree=null;
        object INTEGER357_tree=null;
        object FOR359_tree=null;
        object UPDATE360_tree=null;
        object LOCK361_tree=null;
        object IN362_tree=null;
        object SHARE363_tree=null;
        object MODE364_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:222:17: ( select_statement_component ( f_nl UNION ( ALL )? f_nl select_statement_component )* ( f_nl ORDER BY selectby_expr ( COMMA selectby_expr )* )? ( f_nl LIMIT ( ( T_INTEGER COMMA )? INTEGER | INTEGER OFFSET INTEGER ) )? ( f_nl FOR UPDATE | LOCK IN SHARE MODE )? )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:223:5: select_statement_component ( f_nl UNION ( ALL )? f_nl select_statement_component )* ( f_nl ORDER BY selectby_expr ( COMMA selectby_expr )* )? ( f_nl LIMIT ( ( T_INTEGER COMMA )? INTEGER | INTEGER OFFSET INTEGER ) )? ( f_nl FOR UPDATE | LOCK IN SHARE MODE )?
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_select_statement_component_in_select_statement2291);
            	select_statement_component338 = select_statement_component();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, select_statement_component338.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:224:5: ( f_nl UNION ( ALL )? f_nl select_statement_component )*
            	do 
            	{
            	    int alt119 = 2;
            	    int LA119_0 = input.LA(1);

            	    if ( (LA119_0 == UNION) )
            	    {
            	        alt119 = 1;
            	    }


            	    switch (alt119) 
            		{
            			case 1 :
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:224:6: f_nl UNION ( ALL )? f_nl select_statement_component
            			    {
            			    	PushFollow(FOLLOW_f_nl_in_select_statement2298);
            			    	f_nl339 = f_nl();
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, f_nl339.Tree);
            			    	UNION340=(IToken)Match(input,UNION,FOLLOW_UNION_in_select_statement2300); 
            			    		UNION340_tree = (object)adaptor.Create(UNION340);
            			    		adaptor.AddChild(root_0, UNION340_tree);

            			    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:224:17: ( ALL )?
            			    	int alt118 = 2;
            			    	int LA118_0 = input.LA(1);

            			    	if ( (LA118_0 == ALL) )
            			    	{
            			    	    alt118 = 1;
            			    	}
            			    	switch (alt118) 
            			    	{
            			    	    case 1 :
            			    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:224:17: ALL
            			    	        {
            			    	        	ALL341=(IToken)Match(input,ALL,FOLLOW_ALL_in_select_statement2302); 
            			    	        		ALL341_tree = (object)adaptor.Create(ALL341);
            			    	        		adaptor.AddChild(root_0, ALL341_tree);


            			    	        }
            			    	        break;

            			    	}

            			    	PushFollow(FOLLOW_f_nl_in_select_statement2305);
            			    	f_nl342 = f_nl();
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, f_nl342.Tree);
            			    	PushFollow(FOLLOW_select_statement_component_in_select_statement2307);
            			    	select_statement_component343 = select_statement_component();
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, select_statement_component343.Tree);

            			    }
            			    break;

            			default:
            			    goto loop119;
            	    }
            	} while (true);

            	loop119:
            		;	// Stops C# compiler whining that label 'loop119' has no statements

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:226:5: ( f_nl ORDER BY selectby_expr ( COMMA selectby_expr )* )?
            	int alt121 = 2;
            	int LA121_0 = input.LA(1);

            	if ( (LA121_0 == ORDER) )
            	{
            	    alt121 = 1;
            	}
            	switch (alt121) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:226:6: f_nl ORDER BY selectby_expr ( COMMA selectby_expr )*
            	        {
            	        	PushFollow(FOLLOW_f_nl_in_select_statement2317);
            	        	f_nl344 = f_nl();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, f_nl344.Tree);
            	        	ORDER345=(IToken)Match(input,ORDER,FOLLOW_ORDER_in_select_statement2319); 
            	        		ORDER345_tree = (object)adaptor.Create(ORDER345);
            	        		adaptor.AddChild(root_0, ORDER345_tree);

            	        	BY346=(IToken)Match(input,BY,FOLLOW_BY_in_select_statement2321); 
            	        		BY346_tree = (object)adaptor.Create(BY346);
            	        		adaptor.AddChild(root_0, BY346_tree);

            	        	PushFollow(FOLLOW_selectby_expr_in_select_statement2323);
            	        	selectby_expr347 = selectby_expr();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, selectby_expr347.Tree);
            	        	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:226:34: ( COMMA selectby_expr )*
            	        	do 
            	        	{
            	        	    int alt120 = 2;
            	        	    int LA120_0 = input.LA(1);

            	        	    if ( (LA120_0 == COMMA) )
            	        	    {
            	        	        alt120 = 1;
            	        	    }


            	        	    switch (alt120) 
            	        		{
            	        			case 1 :
            	        			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:226:35: COMMA selectby_expr
            	        			    {
            	        			    	COMMA348=(IToken)Match(input,COMMA,FOLLOW_COMMA_in_select_statement2326); 
            	        			    		COMMA348_tree = (object)adaptor.Create(COMMA348);
            	        			    		adaptor.AddChild(root_0, COMMA348_tree);

            	        			    	PushFollow(FOLLOW_selectby_expr_in_select_statement2328);
            	        			    	selectby_expr349 = selectby_expr();
            	        			    	state.followingStackPointer--;

            	        			    	adaptor.AddChild(root_0, selectby_expr349.Tree);

            	        			    }
            	        			    break;

            	        			default:
            	        			    goto loop120;
            	        	    }
            	        	} while (true);

            	        	loop120:
            	        		;	// Stops C# compiler whining that label 'loop120' has no statements


            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:227:5: ( f_nl LIMIT ( ( T_INTEGER COMMA )? INTEGER | INTEGER OFFSET INTEGER ) )?
            	int alt124 = 2;
            	int LA124_0 = input.LA(1);

            	if ( (LA124_0 == LIMIT) )
            	{
            	    alt124 = 1;
            	}
            	switch (alt124) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:227:6: f_nl LIMIT ( ( T_INTEGER COMMA )? INTEGER | INTEGER OFFSET INTEGER )
            	        {
            	        	PushFollow(FOLLOW_f_nl_in_select_statement2339);
            	        	f_nl350 = f_nl();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, f_nl350.Tree);
            	        	LIMIT351=(IToken)Match(input,LIMIT,FOLLOW_LIMIT_in_select_statement2341); 
            	        		LIMIT351_tree = (object)adaptor.Create(LIMIT351);
            	        		adaptor.AddChild(root_0, LIMIT351_tree);

            	        	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:227:17: ( ( T_INTEGER COMMA )? INTEGER | INTEGER OFFSET INTEGER )
            	        	int alt123 = 2;
            	        	int LA123_0 = input.LA(1);

            	        	if ( (LA123_0 == T_INTEGER) )
            	        	{
            	        	    alt123 = 1;
            	        	}
            	        	else if ( (LA123_0 == INTEGER) )
            	        	{
            	        	    int LA123_2 = input.LA(2);

            	        	    if ( (LA123_2 == OFFSET) )
            	        	    {
            	        	        alt123 = 2;
            	        	    }
            	        	    else if ( (LA123_2 == EOF || LA123_2 == RPAREN || LA123_2 == WITH || (LA123_2 >= FOR && LA123_2 <= LOCK)) )
            	        	    {
            	        	        alt123 = 1;
            	        	    }
            	        	    else 
            	        	    {
            	        	        NoViableAltException nvae_d123s2 =
            	        	            new NoViableAltException("", 123, 2, input);

            	        	        throw nvae_d123s2;
            	        	    }
            	        	}
            	        	else 
            	        	{
            	        	    NoViableAltException nvae_d123s0 =
            	        	        new NoViableAltException("", 123, 0, input);

            	        	    throw nvae_d123s0;
            	        	}
            	        	switch (alt123) 
            	        	{
            	        	    case 1 :
            	        	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:227:18: ( T_INTEGER COMMA )? INTEGER
            	        	        {
            	        	        	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:227:18: ( T_INTEGER COMMA )?
            	        	        	int alt122 = 2;
            	        	        	int LA122_0 = input.LA(1);

            	        	        	if ( (LA122_0 == T_INTEGER) )
            	        	        	{
            	        	        	    alt122 = 1;
            	        	        	}
            	        	        	switch (alt122) 
            	        	        	{
            	        	        	    case 1 :
            	        	        	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:227:19: T_INTEGER COMMA
            	        	        	        {
            	        	        	        	T_INTEGER352=(IToken)Match(input,T_INTEGER,FOLLOW_T_INTEGER_in_select_statement2345); 
            	        	        	        		T_INTEGER352_tree = (object)adaptor.Create(T_INTEGER352);
            	        	        	        		adaptor.AddChild(root_0, T_INTEGER352_tree);

            	        	        	        	COMMA353=(IToken)Match(input,COMMA,FOLLOW_COMMA_in_select_statement2347); 
            	        	        	        		COMMA353_tree = (object)adaptor.Create(COMMA353);
            	        	        	        		adaptor.AddChild(root_0, COMMA353_tree);


            	        	        	        }
            	        	        	        break;

            	        	        	}

            	        	        	INTEGER354=(IToken)Match(input,INTEGER,FOLLOW_INTEGER_in_select_statement2351); 
            	        	        		INTEGER354_tree = (object)adaptor.Create(INTEGER354);
            	        	        		adaptor.AddChild(root_0, INTEGER354_tree);


            	        	        }
            	        	        break;
            	        	    case 2 :
            	        	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:227:47: INTEGER OFFSET INTEGER
            	        	        {
            	        	        	INTEGER355=(IToken)Match(input,INTEGER,FOLLOW_INTEGER_in_select_statement2355); 
            	        	        		INTEGER355_tree = (object)adaptor.Create(INTEGER355);
            	        	        		adaptor.AddChild(root_0, INTEGER355_tree);

            	        	        	OFFSET356=(IToken)Match(input,OFFSET,FOLLOW_OFFSET_in_select_statement2357); 
            	        	        		OFFSET356_tree = (object)adaptor.Create(OFFSET356);
            	        	        		adaptor.AddChild(root_0, OFFSET356_tree);

            	        	        	INTEGER357=(IToken)Match(input,INTEGER,FOLLOW_INTEGER_in_select_statement2359); 
            	        	        		INTEGER357_tree = (object)adaptor.Create(INTEGER357);
            	        	        		adaptor.AddChild(root_0, INTEGER357_tree);


            	        	        }
            	        	        break;

            	        	}


            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:228:5: ( f_nl FOR UPDATE | LOCK IN SHARE MODE )?
            	int alt125 = 3;
            	int LA125_0 = input.LA(1);

            	if ( (LA125_0 == FOR) )
            	{
            	    alt125 = 1;
            	}
            	else if ( (LA125_0 == LOCK) )
            	{
            	    alt125 = 2;
            	}
            	switch (alt125) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:228:6: f_nl FOR UPDATE
            	        {
            	        	PushFollow(FOLLOW_f_nl_in_select_statement2370);
            	        	f_nl358 = f_nl();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, f_nl358.Tree);
            	        	FOR359=(IToken)Match(input,FOR,FOLLOW_FOR_in_select_statement2372); 
            	        		FOR359_tree = (object)adaptor.Create(FOR359);
            	        		adaptor.AddChild(root_0, FOR359_tree);

            	        	UPDATE360=(IToken)Match(input,UPDATE,FOLLOW_UPDATE_in_select_statement2374); 
            	        		UPDATE360_tree = (object)adaptor.Create(UPDATE360);
            	        		adaptor.AddChild(root_0, UPDATE360_tree);


            	        }
            	        break;
            	    case 2 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:228:24: LOCK IN SHARE MODE
            	        {
            	        	LOCK361=(IToken)Match(input,LOCK,FOLLOW_LOCK_in_select_statement2378); 
            	        		LOCK361_tree = (object)adaptor.Create(LOCK361);
            	        		adaptor.AddChild(root_0, LOCK361_tree);

            	        	IN362=(IToken)Match(input,IN,FOLLOW_IN_in_select_statement2380); 
            	        		IN362_tree = (object)adaptor.Create(IN362);
            	        		adaptor.AddChild(root_0, IN362_tree);

            	        	SHARE363=(IToken)Match(input,SHARE,FOLLOW_SHARE_in_select_statement2382); 
            	        		SHARE363_tree = (object)adaptor.Create(SHARE363);
            	        		adaptor.AddChild(root_0, SHARE363_tree);

            	        	MODE364=(IToken)Match(input,MODE,FOLLOW_MODE_in_select_statement2384); 
            	        		MODE364_tree = (object)adaptor.Create(MODE364);
            	        		adaptor.AddChild(root_0, MODE364_tree);


            	        }
            	        break;

            	}


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "select_statement"

    public class select_statement_component_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "select_statement_component"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:231:1: select_statement_component : SELECT ( ALL | DISTINCT | DISTINCTROW )? ( HIGH_PRIORITY )? ( STRAIGHT_JOIN )? ( SQL_SMALL_RESULT )? ( SQL_BIG_RESULT )? ( SQL_BUFFER_RESULT )? ( SQL_CACHE | SQL_NO_CACHE )? ( SQL_CALC_FOUND_ROWS )? f_inc f_nl select_expr ( COMMA f_nl select_expr )* f_dec f_nl ( f_nl FROM table_references )? ( f_nl WHERE condition )? ( f_nl GROUP BY selectby_expr ( COMMA selectby_expr )* ( WITH ROLLUP )? )? ( f_nl HAVING condition )? ;
    public MySQLParser.select_statement_component_return select_statement_component() // throws RecognitionException [1]
    {   
        MySQLParser.select_statement_component_return retval = new MySQLParser.select_statement_component_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken SELECT365 = null;
        IToken set366 = null;
        IToken HIGH_PRIORITY367 = null;
        IToken STRAIGHT_JOIN368 = null;
        IToken SQL_SMALL_RESULT369 = null;
        IToken SQL_BIG_RESULT370 = null;
        IToken SQL_BUFFER_RESULT371 = null;
        IToken set372 = null;
        IToken SQL_CALC_FOUND_ROWS373 = null;
        IToken COMMA377 = null;
        IToken FROM383 = null;
        IToken WHERE386 = null;
        IToken GROUP389 = null;
        IToken BY390 = null;
        IToken COMMA392 = null;
        IToken WITH394 = null;
        IToken ROLLUP395 = null;
        IToken HAVING397 = null;
        MySQLParser.f_inc_return f_inc374 = default(MySQLParser.f_inc_return);

        MySQLParser.f_nl_return f_nl375 = default(MySQLParser.f_nl_return);

        MySQLParser.select_expr_return select_expr376 = default(MySQLParser.select_expr_return);

        MySQLParser.f_nl_return f_nl378 = default(MySQLParser.f_nl_return);

        MySQLParser.select_expr_return select_expr379 = default(MySQLParser.select_expr_return);

        MySQLParser.f_dec_return f_dec380 = default(MySQLParser.f_dec_return);

        MySQLParser.f_nl_return f_nl381 = default(MySQLParser.f_nl_return);

        MySQLParser.f_nl_return f_nl382 = default(MySQLParser.f_nl_return);

        MySQLParser.table_references_return table_references384 = default(MySQLParser.table_references_return);

        MySQLParser.f_nl_return f_nl385 = default(MySQLParser.f_nl_return);

        MySQLParser.condition_return condition387 = default(MySQLParser.condition_return);

        MySQLParser.f_nl_return f_nl388 = default(MySQLParser.f_nl_return);

        MySQLParser.selectby_expr_return selectby_expr391 = default(MySQLParser.selectby_expr_return);

        MySQLParser.selectby_expr_return selectby_expr393 = default(MySQLParser.selectby_expr_return);

        MySQLParser.f_nl_return f_nl396 = default(MySQLParser.f_nl_return);

        MySQLParser.condition_return condition398 = default(MySQLParser.condition_return);


        object SELECT365_tree=null;
        object set366_tree=null;
        object HIGH_PRIORITY367_tree=null;
        object STRAIGHT_JOIN368_tree=null;
        object SQL_SMALL_RESULT369_tree=null;
        object SQL_BIG_RESULT370_tree=null;
        object SQL_BUFFER_RESULT371_tree=null;
        object set372_tree=null;
        object SQL_CALC_FOUND_ROWS373_tree=null;
        object COMMA377_tree=null;
        object FROM383_tree=null;
        object WHERE386_tree=null;
        object GROUP389_tree=null;
        object BY390_tree=null;
        object COMMA392_tree=null;
        object WITH394_tree=null;
        object ROLLUP395_tree=null;
        object HAVING397_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:231:27: ( SELECT ( ALL | DISTINCT | DISTINCTROW )? ( HIGH_PRIORITY )? ( STRAIGHT_JOIN )? ( SQL_SMALL_RESULT )? ( SQL_BIG_RESULT )? ( SQL_BUFFER_RESULT )? ( SQL_CACHE | SQL_NO_CACHE )? ( SQL_CALC_FOUND_ROWS )? f_inc f_nl select_expr ( COMMA f_nl select_expr )* f_dec f_nl ( f_nl FROM table_references )? ( f_nl WHERE condition )? ( f_nl GROUP BY selectby_expr ( COMMA selectby_expr )* ( WITH ROLLUP )? )? ( f_nl HAVING condition )? )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:232:9: SELECT ( ALL | DISTINCT | DISTINCTROW )? ( HIGH_PRIORITY )? ( STRAIGHT_JOIN )? ( SQL_SMALL_RESULT )? ( SQL_BIG_RESULT )? ( SQL_BUFFER_RESULT )? ( SQL_CACHE | SQL_NO_CACHE )? ( SQL_CALC_FOUND_ROWS )? f_inc f_nl select_expr ( COMMA f_nl select_expr )* f_dec f_nl ( f_nl FROM table_references )? ( f_nl WHERE condition )? ( f_nl GROUP BY selectby_expr ( COMMA selectby_expr )* ( WITH ROLLUP )? )? ( f_nl HAVING condition )?
            {
            	root_0 = (object)adaptor.GetNilNode();

            	SELECT365=(IToken)Match(input,SELECT,FOLLOW_SELECT_in_select_statement_component2406); 
            		SELECT365_tree = (object)adaptor.Create(SELECT365);
            		adaptor.AddChild(root_0, SELECT365_tree);

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:233:9: ( ALL | DISTINCT | DISTINCTROW )?
            	int alt126 = 2;
            	int LA126_0 = input.LA(1);

            	if ( (LA126_0 == ALL || (LA126_0 >= DISTINCT && LA126_0 <= DISTINCTROW)) )
            	{
            	    alt126 = 1;
            	}
            	switch (alt126) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:
            	        {
            	        	set366 = (IToken)input.LT(1);
            	        	if ( input.LA(1) == ALL || (input.LA(1) >= DISTINCT && input.LA(1) <= DISTINCTROW) ) 
            	        	{
            	        	    input.Consume();
            	        	    adaptor.AddChild(root_0, (object)adaptor.Create(set366));
            	        	    state.errorRecovery = false;
            	        	}
            	        	else 
            	        	{
            	        	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	        	    throw mse;
            	        	}


            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:234:9: ( HIGH_PRIORITY )?
            	int alt127 = 2;
            	int LA127_0 = input.LA(1);

            	if ( (LA127_0 == HIGH_PRIORITY) )
            	{
            	    alt127 = 1;
            	}
            	switch (alt127) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:234:9: HIGH_PRIORITY
            	        {
            	        	HIGH_PRIORITY367=(IToken)Match(input,HIGH_PRIORITY,FOLLOW_HIGH_PRIORITY_in_select_statement_component2437); 
            	        		HIGH_PRIORITY367_tree = (object)adaptor.Create(HIGH_PRIORITY367);
            	        		adaptor.AddChild(root_0, HIGH_PRIORITY367_tree);


            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:235:9: ( STRAIGHT_JOIN )?
            	int alt128 = 2;
            	int LA128_0 = input.LA(1);

            	if ( (LA128_0 == STRAIGHT_JOIN) )
            	{
            	    alt128 = 1;
            	}
            	switch (alt128) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:235:9: STRAIGHT_JOIN
            	        {
            	        	STRAIGHT_JOIN368=(IToken)Match(input,STRAIGHT_JOIN,FOLLOW_STRAIGHT_JOIN_in_select_statement_component2448); 
            	        		STRAIGHT_JOIN368_tree = (object)adaptor.Create(STRAIGHT_JOIN368);
            	        		adaptor.AddChild(root_0, STRAIGHT_JOIN368_tree);


            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:236:9: ( SQL_SMALL_RESULT )?
            	int alt129 = 2;
            	int LA129_0 = input.LA(1);

            	if ( (LA129_0 == SQL_SMALL_RESULT) )
            	{
            	    alt129 = 1;
            	}
            	switch (alt129) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:236:9: SQL_SMALL_RESULT
            	        {
            	        	SQL_SMALL_RESULT369=(IToken)Match(input,SQL_SMALL_RESULT,FOLLOW_SQL_SMALL_RESULT_in_select_statement_component2459); 
            	        		SQL_SMALL_RESULT369_tree = (object)adaptor.Create(SQL_SMALL_RESULT369);
            	        		adaptor.AddChild(root_0, SQL_SMALL_RESULT369_tree);


            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:236:27: ( SQL_BIG_RESULT )?
            	int alt130 = 2;
            	int LA130_0 = input.LA(1);

            	if ( (LA130_0 == SQL_BIG_RESULT) )
            	{
            	    alt130 = 1;
            	}
            	switch (alt130) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:236:27: SQL_BIG_RESULT
            	        {
            	        	SQL_BIG_RESULT370=(IToken)Match(input,SQL_BIG_RESULT,FOLLOW_SQL_BIG_RESULT_in_select_statement_component2462); 
            	        		SQL_BIG_RESULT370_tree = (object)adaptor.Create(SQL_BIG_RESULT370);
            	        		adaptor.AddChild(root_0, SQL_BIG_RESULT370_tree);


            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:236:43: ( SQL_BUFFER_RESULT )?
            	int alt131 = 2;
            	int LA131_0 = input.LA(1);

            	if ( (LA131_0 == SQL_BUFFER_RESULT) )
            	{
            	    alt131 = 1;
            	}
            	switch (alt131) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:236:43: SQL_BUFFER_RESULT
            	        {
            	        	SQL_BUFFER_RESULT371=(IToken)Match(input,SQL_BUFFER_RESULT,FOLLOW_SQL_BUFFER_RESULT_in_select_statement_component2465); 
            	        		SQL_BUFFER_RESULT371_tree = (object)adaptor.Create(SQL_BUFFER_RESULT371);
            	        		adaptor.AddChild(root_0, SQL_BUFFER_RESULT371_tree);


            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:237:9: ( SQL_CACHE | SQL_NO_CACHE )?
            	int alt132 = 2;
            	int LA132_0 = input.LA(1);

            	if ( ((LA132_0 >= SQL_CACHE && LA132_0 <= SQL_NO_CACHE)) )
            	{
            	    alt132 = 1;
            	}
            	switch (alt132) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:
            	        {
            	        	set372 = (IToken)input.LT(1);
            	        	if ( (input.LA(1) >= SQL_CACHE && input.LA(1) <= SQL_NO_CACHE) ) 
            	        	{
            	        	    input.Consume();
            	        	    adaptor.AddChild(root_0, (object)adaptor.Create(set372));
            	        	    state.errorRecovery = false;
            	        	}
            	        	else 
            	        	{
            	        	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	        	    throw mse;
            	        	}


            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:237:37: ( SQL_CALC_FOUND_ROWS )?
            	int alt133 = 2;
            	int LA133_0 = input.LA(1);

            	if ( (LA133_0 == SQL_CALC_FOUND_ROWS) )
            	{
            	    alt133 = 1;
            	}
            	switch (alt133) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:237:37: SQL_CALC_FOUND_ROWS
            	        {
            	        	SQL_CALC_FOUND_ROWS373=(IToken)Match(input,SQL_CALC_FOUND_ROWS,FOLLOW_SQL_CALC_FOUND_ROWS_in_select_statement_component2485); 
            	        		SQL_CALC_FOUND_ROWS373_tree = (object)adaptor.Create(SQL_CALC_FOUND_ROWS373);
            	        		adaptor.AddChild(root_0, SQL_CALC_FOUND_ROWS373_tree);


            	        }
            	        break;

            	}

            	PushFollow(FOLLOW_f_inc_in_select_statement_component2492);
            	f_inc374 = f_inc();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, f_inc374.Tree);
            	PushFollow(FOLLOW_f_nl_in_select_statement_component2494);
            	f_nl375 = f_nl();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, f_nl375.Tree);
            	PushFollow(FOLLOW_select_expr_in_select_statement_component2496);
            	select_expr376 = select_expr();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, select_expr376.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:238:28: ( COMMA f_nl select_expr )*
            	do 
            	{
            	    int alt134 = 2;
            	    int LA134_0 = input.LA(1);

            	    if ( (LA134_0 == COMMA) )
            	    {
            	        alt134 = 1;
            	    }


            	    switch (alt134) 
            		{
            			case 1 :
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:238:29: COMMA f_nl select_expr
            			    {
            			    	COMMA377=(IToken)Match(input,COMMA,FOLLOW_COMMA_in_select_statement_component2499); 
            			    		COMMA377_tree = (object)adaptor.Create(COMMA377);
            			    		adaptor.AddChild(root_0, COMMA377_tree);

            			    	PushFollow(FOLLOW_f_nl_in_select_statement_component2501);
            			    	f_nl378 = f_nl();
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, f_nl378.Tree);
            			    	PushFollow(FOLLOW_select_expr_in_select_statement_component2503);
            			    	select_expr379 = select_expr();
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, select_expr379.Tree);

            			    }
            			    break;

            			default:
            			    goto loop134;
            	    }
            	} while (true);

            	loop134:
            		;	// Stops C# compiler whining that label 'loop134' has no statements

            	PushFollow(FOLLOW_f_dec_in_select_statement_component2507);
            	f_dec380 = f_dec();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, f_dec380.Tree);
            	PushFollow(FOLLOW_f_nl_in_select_statement_component2509);
            	f_nl381 = f_nl();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, f_nl381.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:239:5: ( f_nl FROM table_references )?
            	int alt135 = 2;
            	int LA135_0 = input.LA(1);

            	if ( (LA135_0 == FROM) )
            	{
            	    alt135 = 1;
            	}
            	switch (alt135) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:239:6: f_nl FROM table_references
            	        {
            	        	PushFollow(FOLLOW_f_nl_in_select_statement_component2516);
            	        	f_nl382 = f_nl();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, f_nl382.Tree);
            	        	FROM383=(IToken)Match(input,FROM,FOLLOW_FROM_in_select_statement_component2518); 
            	        		FROM383_tree = (object)adaptor.Create(FROM383);
            	        		adaptor.AddChild(root_0, FROM383_tree);

            	        	PushFollow(FOLLOW_table_references_in_select_statement_component2520);
            	        	table_references384 = table_references();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, table_references384.Tree);

            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:240:5: ( f_nl WHERE condition )?
            	int alt136 = 2;
            	int LA136_0 = input.LA(1);

            	if ( (LA136_0 == WHERE) )
            	{
            	    alt136 = 1;
            	}
            	switch (alt136) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:240:6: f_nl WHERE condition
            	        {
            	        	PushFollow(FOLLOW_f_nl_in_select_statement_component2529);
            	        	f_nl385 = f_nl();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, f_nl385.Tree);
            	        	WHERE386=(IToken)Match(input,WHERE,FOLLOW_WHERE_in_select_statement_component2531); 
            	        		WHERE386_tree = (object)adaptor.Create(WHERE386);
            	        		adaptor.AddChild(root_0, WHERE386_tree);

            	        	PushFollow(FOLLOW_condition_in_select_statement_component2533);
            	        	condition387 = condition();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, condition387.Tree);

            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:241:5: ( f_nl GROUP BY selectby_expr ( COMMA selectby_expr )* ( WITH ROLLUP )? )?
            	int alt139 = 2;
            	int LA139_0 = input.LA(1);

            	if ( (LA139_0 == GROUP) )
            	{
            	    alt139 = 1;
            	}
            	switch (alt139) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:241:6: f_nl GROUP BY selectby_expr ( COMMA selectby_expr )* ( WITH ROLLUP )?
            	        {
            	        	PushFollow(FOLLOW_f_nl_in_select_statement_component2542);
            	        	f_nl388 = f_nl();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, f_nl388.Tree);
            	        	GROUP389=(IToken)Match(input,GROUP,FOLLOW_GROUP_in_select_statement_component2544); 
            	        		GROUP389_tree = (object)adaptor.Create(GROUP389);
            	        		adaptor.AddChild(root_0, GROUP389_tree);

            	        	BY390=(IToken)Match(input,BY,FOLLOW_BY_in_select_statement_component2546); 
            	        		BY390_tree = (object)adaptor.Create(BY390);
            	        		adaptor.AddChild(root_0, BY390_tree);

            	        	PushFollow(FOLLOW_selectby_expr_in_select_statement_component2548);
            	        	selectby_expr391 = selectby_expr();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, selectby_expr391.Tree);
            	        	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:241:34: ( COMMA selectby_expr )*
            	        	do 
            	        	{
            	        	    int alt137 = 2;
            	        	    int LA137_0 = input.LA(1);

            	        	    if ( (LA137_0 == COMMA) )
            	        	    {
            	        	        alt137 = 1;
            	        	    }


            	        	    switch (alt137) 
            	        		{
            	        			case 1 :
            	        			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:241:35: COMMA selectby_expr
            	        			    {
            	        			    	COMMA392=(IToken)Match(input,COMMA,FOLLOW_COMMA_in_select_statement_component2551); 
            	        			    		COMMA392_tree = (object)adaptor.Create(COMMA392);
            	        			    		adaptor.AddChild(root_0, COMMA392_tree);

            	        			    	PushFollow(FOLLOW_selectby_expr_in_select_statement_component2553);
            	        			    	selectby_expr393 = selectby_expr();
            	        			    	state.followingStackPointer--;

            	        			    	adaptor.AddChild(root_0, selectby_expr393.Tree);

            	        			    }
            	        			    break;

            	        			default:
            	        			    goto loop137;
            	        	    }
            	        	} while (true);

            	        	loop137:
            	        		;	// Stops C# compiler whining that label 'loop137' has no statements

            	        	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:241:57: ( WITH ROLLUP )?
            	        	int alt138 = 2;
            	        	int LA138_0 = input.LA(1);

            	        	if ( (LA138_0 == WITH) )
            	        	{
            	        	    int LA138_1 = input.LA(2);

            	        	    if ( (LA138_1 == ROLLUP) )
            	        	    {
            	        	        alt138 = 1;
            	        	    }
            	        	}
            	        	switch (alt138) 
            	        	{
            	        	    case 1 :
            	        	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:241:58: WITH ROLLUP
            	        	        {
            	        	        	WITH394=(IToken)Match(input,WITH,FOLLOW_WITH_in_select_statement_component2558); 
            	        	        		WITH394_tree = (object)adaptor.Create(WITH394);
            	        	        		adaptor.AddChild(root_0, WITH394_tree);

            	        	        	ROLLUP395=(IToken)Match(input,ROLLUP,FOLLOW_ROLLUP_in_select_statement_component2560); 
            	        	        		ROLLUP395_tree = (object)adaptor.Create(ROLLUP395);
            	        	        		adaptor.AddChild(root_0, ROLLUP395_tree);


            	        	        }
            	        	        break;

            	        	}


            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:242:5: ( f_nl HAVING condition )?
            	int alt140 = 2;
            	int LA140_0 = input.LA(1);

            	if ( (LA140_0 == HAVING) )
            	{
            	    alt140 = 1;
            	}
            	switch (alt140) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:242:6: f_nl HAVING condition
            	        {
            	        	PushFollow(FOLLOW_f_nl_in_select_statement_component2571);
            	        	f_nl396 = f_nl();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, f_nl396.Tree);
            	        	HAVING397=(IToken)Match(input,HAVING,FOLLOW_HAVING_in_select_statement_component2573); 
            	        		HAVING397_tree = (object)adaptor.Create(HAVING397);
            	        		adaptor.AddChild(root_0, HAVING397_tree);

            	        	PushFollow(FOLLOW_condition_in_select_statement_component2575);
            	        	condition398 = condition();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, condition398.Tree);

            	        }
            	        break;

            	}


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "select_statement_component"

    public class selectby_expr_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "selectby_expr"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:245:1: selectby_expr : expr ( ASC | DESC )? ;
    public MySQLParser.selectby_expr_return selectby_expr() // throws RecognitionException [1]
    {   
        MySQLParser.selectby_expr_return retval = new MySQLParser.selectby_expr_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken set400 = null;
        MySQLParser.expr_return expr399 = default(MySQLParser.expr_return);


        object set400_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:245:14: ( expr ( ASC | DESC )? )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:245:16: expr ( ASC | DESC )?
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_expr_in_selectby_expr2589);
            	expr399 = expr();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, expr399.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:245:21: ( ASC | DESC )?
            	int alt141 = 2;
            	int LA141_0 = input.LA(1);

            	if ( ((LA141_0 >= ASC && LA141_0 <= DESC)) )
            	{
            	    alt141 = 1;
            	}
            	switch (alt141) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:
            	        {
            	        	set400 = (IToken)input.LT(1);
            	        	if ( (input.LA(1) >= ASC && input.LA(1) <= DESC) ) 
            	        	{
            	        	    input.Consume();
            	        	    adaptor.AddChild(root_0, (object)adaptor.Create(set400));
            	        	    state.errorRecovery = false;
            	        	}
            	        	else 
            	        	{
            	        	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	        	    throw mse;
            	        	}


            	        }
            	        break;

            	}


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "selectby_expr"

    public class select_expr_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "select_expr"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:247:1: select_expr : ( expr ( ( AS )? id )? | id DOT ASTERISK | ASTERISK );
    public MySQLParser.select_expr_return select_expr() // throws RecognitionException [1]
    {   
        MySQLParser.select_expr_return retval = new MySQLParser.select_expr_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken AS402 = null;
        IToken DOT405 = null;
        IToken ASTERISK406 = null;
        IToken ASTERISK407 = null;
        MySQLParser.expr_return expr401 = default(MySQLParser.expr_return);

        MySQLParser.id_return id403 = default(MySQLParser.id_return);

        MySQLParser.id_return id404 = default(MySQLParser.id_return);


        object AS402_tree=null;
        object DOT405_tree=null;
        object ASTERISK406_tree=null;
        object ASTERISK407_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:247:12: ( expr ( ( AS )? id )? | id DOT ASTERISK | ASTERISK )
            int alt144 = 3;
            switch ( input.LA(1) ) 
            {
            case LPAREN:
            case T_INTEGER:
            case NOT:
            case NULL:
            case CURRENT_TIMESTAMP:
            case T_STRING:
            case YEAR:
            case T_FLOAT:
            case T_BLOB:
            case CURRENT_USER:
            case CURRENT_DATE:
            case CASE:
            case UTC_TIMESTAMP:
            case INTERVAL:
            case EXCLAMATION:
            case MINUS:
            case TILDA:
            case CURRENT_TIME:
            	{
                alt144 = 1;
                }
                break;
            case T_IDENT:
            case T_QUOTED_IDENT:
            	{
                int LA144_2 = input.LA(2);

                if ( (LA144_2 == DOT) )
                {
                    int LA144_4 = input.LA(3);

                    if ( (LA144_4 == ASTERISK) )
                    {
                        alt144 = 2;
                    }
                    else if ( ((LA144_4 >= T_IDENT && LA144_4 <= T_QUOTED_IDENT)) )
                    {
                        alt144 = 1;
                    }
                    else 
                    {
                        NoViableAltException nvae_d144s4 =
                            new NoViableAltException("", 144, 4, input);

                        throw nvae_d144s4;
                    }
                }
                else if ( (LA144_2 == EOF || (LA144_2 >= LPAREN && LA144_2 <= RPAREN) || (LA144_2 >= EQUALS && LA144_2 <= WITH) || (LA144_2 >= T_IDENT && LA144_2 <= T_QUOTED_IDENT) || LA144_2 == UNION || LA144_2 == OR || LA144_2 == AS || LA144_2 == ORDER || LA144_2 == LIMIT || (LA144_2 >= FOR && LA144_2 <= IN) || (LA144_2 >= FROM && LA144_2 <= GROUP) || (LA144_2 >= HAVING && LA144_2 <= ASTERISK) || LA144_2 == MINUS || (LA144_2 >= ARROW_UP && LA144_2 <= DOUBLE_PIPE)) )
                {
                    alt144 = 1;
                }
                else 
                {
                    NoViableAltException nvae_d144s2 =
                        new NoViableAltException("", 144, 2, input);

                    throw nvae_d144s2;
                }
                }
                break;
            case ASTERISK:
            	{
                alt144 = 3;
                }
                break;
            	default:
            	    NoViableAltException nvae_d144s0 =
            	        new NoViableAltException("", 144, 0, input);

            	    throw nvae_d144s0;
            }

            switch (alt144) 
            {
                case 1 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:248:5: expr ( ( AS )? id )?
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_expr_in_select_expr2607);
                    	expr401 = expr();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, expr401.Tree);
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:248:10: ( ( AS )? id )?
                    	int alt143 = 2;
                    	int LA143_0 = input.LA(1);

                    	if ( ((LA143_0 >= T_IDENT && LA143_0 <= T_QUOTED_IDENT) || LA143_0 == AS) )
                    	{
                    	    alt143 = 1;
                    	}
                    	switch (alt143) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:248:11: ( AS )? id
                    	        {
                    	        	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:248:11: ( AS )?
                    	        	int alt142 = 2;
                    	        	int LA142_0 = input.LA(1);

                    	        	if ( (LA142_0 == AS) )
                    	        	{
                    	        	    alt142 = 1;
                    	        	}
                    	        	switch (alt142) 
                    	        	{
                    	        	    case 1 :
                    	        	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:248:11: AS
                    	        	        {
                    	        	        	AS402=(IToken)Match(input,AS,FOLLOW_AS_in_select_expr2610); 
                    	        	        		AS402_tree = (object)adaptor.Create(AS402);
                    	        	        		adaptor.AddChild(root_0, AS402_tree);


                    	        	        }
                    	        	        break;

                    	        	}

                    	        	PushFollow(FOLLOW_id_in_select_expr2613);
                    	        	id403 = id();
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, id403.Tree);

                    	        }
                    	        break;

                    	}


                    }
                    break;
                case 2 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:249:7: id DOT ASTERISK
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_id_in_select_expr2623);
                    	id404 = id();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, id404.Tree);
                    	DOT405=(IToken)Match(input,DOT,FOLLOW_DOT_in_select_expr2625); 
                    		DOT405_tree = (object)adaptor.Create(DOT405);
                    		adaptor.AddChild(root_0, DOT405_tree);

                    	ASTERISK406=(IToken)Match(input,ASTERISK,FOLLOW_ASTERISK_in_select_expr2627); 
                    		ASTERISK406_tree = (object)adaptor.Create(ASTERISK406);
                    		adaptor.AddChild(root_0, ASTERISK406_tree);


                    }
                    break;
                case 3 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:250:7: ASTERISK
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	ASTERISK407=(IToken)Match(input,ASTERISK,FOLLOW_ASTERISK_in_select_expr2635); 
                    		ASTERISK407_tree = (object)adaptor.Create(ASTERISK407);
                    		adaptor.AddChild(root_0, ASTERISK407_tree);


                    }
                    break;

            }
            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "select_expr"

    public class table_references_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "table_references"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:253:1: table_references : f_inc table_reference ( COMMA table_reference )* f_dec ;
    public MySQLParser.table_references_return table_references() // throws RecognitionException [1]
    {   
        MySQLParser.table_references_return retval = new MySQLParser.table_references_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken COMMA410 = null;
        MySQLParser.f_inc_return f_inc408 = default(MySQLParser.f_inc_return);

        MySQLParser.table_reference_return table_reference409 = default(MySQLParser.table_reference_return);

        MySQLParser.table_reference_return table_reference411 = default(MySQLParser.table_reference_return);

        MySQLParser.f_dec_return f_dec412 = default(MySQLParser.f_dec_return);


        object COMMA410_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:253:17: ( f_inc table_reference ( COMMA table_reference )* f_dec )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:254:5: f_inc table_reference ( COMMA table_reference )* f_dec
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_f_inc_in_table_references2647);
            	f_inc408 = f_inc();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, f_inc408.Tree);
            	PushFollow(FOLLOW_table_reference_in_table_references2649);
            	table_reference409 = table_reference();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, table_reference409.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:254:27: ( COMMA table_reference )*
            	do 
            	{
            	    int alt145 = 2;
            	    int LA145_0 = input.LA(1);

            	    if ( (LA145_0 == COMMA) )
            	    {
            	        alt145 = 1;
            	    }


            	    switch (alt145) 
            		{
            			case 1 :
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:254:28: COMMA table_reference
            			    {
            			    	COMMA410=(IToken)Match(input,COMMA,FOLLOW_COMMA_in_table_references2652); 
            			    		COMMA410_tree = (object)adaptor.Create(COMMA410);
            			    		adaptor.AddChild(root_0, COMMA410_tree);

            			    	PushFollow(FOLLOW_table_reference_in_table_references2654);
            			    	table_reference411 = table_reference();
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, table_reference411.Tree);

            			    }
            			    break;

            			default:
            			    goto loop145;
            	    }
            	} while (true);

            	loop145:
            		;	// Stops C# compiler whining that label 'loop145' has no statements

            	PushFollow(FOLLOW_f_dec_in_table_references2658);
            	f_dec412 = f_dec();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, f_dec412.Tree);

            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "table_references"

    public class table_reference_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "table_reference"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:258:1: table_reference : table_factor ( join_table )* ;
    public MySQLParser.table_reference_return table_reference() // throws RecognitionException [1]
    {   
        MySQLParser.table_reference_return retval = new MySQLParser.table_reference_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        MySQLParser.table_factor_return table_factor413 = default(MySQLParser.table_factor_return);

        MySQLParser.join_table_return join_table414 = default(MySQLParser.join_table_return);



        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:258:16: ( table_factor ( join_table )* )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:259:5: table_factor ( join_table )*
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_table_factor_in_table_reference2675);
            	table_factor413 = table_factor();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, table_factor413.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:259:18: ( join_table )*
            	do 
            	{
            	    int alt146 = 2;
            	    int LA146_0 = input.LA(1);

            	    if ( (LA146_0 == STRAIGHT_JOIN || LA146_0 == LEFT || (LA146_0 >= JOIN && LA146_0 <= NATURAL)) )
            	    {
            	        alt146 = 1;
            	    }


            	    switch (alt146) 
            		{
            			case 1 :
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:259:18: join_table
            			    {
            			    	PushFollow(FOLLOW_join_table_in_table_reference2677);
            			    	join_table414 = join_table();
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, join_table414.Tree);

            			    }
            			    break;

            			default:
            			    goto loop146;
            	    }
            	} while (true);

            	loop146:
            		;	// Stops C# compiler whining that label 'loop146' has no statements


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "table_reference"

    public class table_factor_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "table_factor"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:261:1: table_factor : ( id ( ( AS )? id )? ( index_hint )? | table_subquery ( AS )? id | LPAREN table_references RPAREN | OJ table_factor LEFT OUTER JOIN table_factor ON condition );
    public MySQLParser.table_factor_return table_factor() // throws RecognitionException [1]
    {   
        MySQLParser.table_factor_return retval = new MySQLParser.table_factor_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken AS416 = null;
        IToken AS420 = null;
        IToken LPAREN422 = null;
        IToken RPAREN424 = null;
        IToken OJ425 = null;
        IToken LEFT427 = null;
        IToken OUTER428 = null;
        IToken JOIN429 = null;
        IToken ON431 = null;
        MySQLParser.id_return id415 = default(MySQLParser.id_return);

        MySQLParser.id_return id417 = default(MySQLParser.id_return);

        MySQLParser.index_hint_return index_hint418 = default(MySQLParser.index_hint_return);

        MySQLParser.table_subquery_return table_subquery419 = default(MySQLParser.table_subquery_return);

        MySQLParser.id_return id421 = default(MySQLParser.id_return);

        MySQLParser.table_references_return table_references423 = default(MySQLParser.table_references_return);

        MySQLParser.table_factor_return table_factor426 = default(MySQLParser.table_factor_return);

        MySQLParser.table_factor_return table_factor430 = default(MySQLParser.table_factor_return);

        MySQLParser.condition_return condition432 = default(MySQLParser.condition_return);


        object AS416_tree=null;
        object AS420_tree=null;
        object LPAREN422_tree=null;
        object RPAREN424_tree=null;
        object OJ425_tree=null;
        object LEFT427_tree=null;
        object OUTER428_tree=null;
        object JOIN429_tree=null;
        object ON431_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:261:13: ( id ( ( AS )? id )? ( index_hint )? | table_subquery ( AS )? id | LPAREN table_references RPAREN | OJ table_factor LEFT OUTER JOIN table_factor ON condition )
            int alt151 = 4;
            switch ( input.LA(1) ) 
            {
            case T_IDENT:
            case T_QUOTED_IDENT:
            	{
                alt151 = 1;
                }
                break;
            case LPAREN:
            	{
                int LA151_2 = input.LA(2);

                if ( (LA151_2 == SELECT) )
                {
                    alt151 = 2;
                }
                else if ( (LA151_2 == LPAREN || (LA151_2 >= T_IDENT && LA151_2 <= T_QUOTED_IDENT) || LA151_2 == OJ) )
                {
                    alt151 = 3;
                }
                else 
                {
                    NoViableAltException nvae_d151s2 =
                        new NoViableAltException("", 151, 2, input);

                    throw nvae_d151s2;
                }
                }
                break;
            case OJ:
            	{
                alt151 = 4;
                }
                break;
            	default:
            	    NoViableAltException nvae_d151s0 =
            	        new NoViableAltException("", 151, 0, input);

            	    throw nvae_d151s0;
            }

            switch (alt151) 
            {
                case 1 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:262:5: id ( ( AS )? id )? ( index_hint )?
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_id_in_table_factor2689);
                    	id415 = id();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, id415.Tree);
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:262:8: ( ( AS )? id )?
                    	int alt148 = 2;
                    	int LA148_0 = input.LA(1);

                    	if ( ((LA148_0 >= T_IDENT && LA148_0 <= T_QUOTED_IDENT) || LA148_0 == AS) )
                    	{
                    	    alt148 = 1;
                    	}
                    	switch (alt148) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:262:9: ( AS )? id
                    	        {
                    	        	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:262:9: ( AS )?
                    	        	int alt147 = 2;
                    	        	int LA147_0 = input.LA(1);

                    	        	if ( (LA147_0 == AS) )
                    	        	{
                    	        	    alt147 = 1;
                    	        	}
                    	        	switch (alt147) 
                    	        	{
                    	        	    case 1 :
                    	        	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:262:9: AS
                    	        	        {
                    	        	        	AS416=(IToken)Match(input,AS,FOLLOW_AS_in_table_factor2692); 
                    	        	        		AS416_tree = (object)adaptor.Create(AS416);
                    	        	        		adaptor.AddChild(root_0, AS416_tree);


                    	        	        }
                    	        	        break;

                    	        	}

                    	        	PushFollow(FOLLOW_id_in_table_factor2695);
                    	        	id417 = id();
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, id417.Tree);

                    	        }
                    	        break;

                    	}

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:262:18: ( index_hint )?
                    	int alt149 = 2;
                    	int LA149_0 = input.LA(1);

                    	if ( ((LA149_0 >= USE && LA149_0 <= FORCE)) )
                    	{
                    	    alt149 = 1;
                    	}
                    	switch (alt149) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:262:18: index_hint
                    	        {
                    	        	PushFollow(FOLLOW_index_hint_in_table_factor2699);
                    	        	index_hint418 = index_hint();
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, index_hint418.Tree);

                    	        }
                    	        break;

                    	}


                    }
                    break;
                case 2 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:263:7: table_subquery ( AS )? id
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_table_subquery_in_table_factor2708);
                    	table_subquery419 = table_subquery();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, table_subquery419.Tree);
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:263:22: ( AS )?
                    	int alt150 = 2;
                    	int LA150_0 = input.LA(1);

                    	if ( (LA150_0 == AS) )
                    	{
                    	    alt150 = 1;
                    	}
                    	switch (alt150) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:263:22: AS
                    	        {
                    	        	AS420=(IToken)Match(input,AS,FOLLOW_AS_in_table_factor2710); 
                    	        		AS420_tree = (object)adaptor.Create(AS420);
                    	        		adaptor.AddChild(root_0, AS420_tree);


                    	        }
                    	        break;

                    	}

                    	PushFollow(FOLLOW_id_in_table_factor2713);
                    	id421 = id();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, id421.Tree);

                    }
                    break;
                case 3 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:264:7: LPAREN table_references RPAREN
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	LPAREN422=(IToken)Match(input,LPAREN,FOLLOW_LPAREN_in_table_factor2721); 
                    		LPAREN422_tree = (object)adaptor.Create(LPAREN422);
                    		adaptor.AddChild(root_0, LPAREN422_tree);

                    	PushFollow(FOLLOW_table_references_in_table_factor2723);
                    	table_references423 = table_references();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, table_references423.Tree);
                    	RPAREN424=(IToken)Match(input,RPAREN,FOLLOW_RPAREN_in_table_factor2725); 
                    		RPAREN424_tree = (object)adaptor.Create(RPAREN424);
                    		adaptor.AddChild(root_0, RPAREN424_tree);


                    }
                    break;
                case 4 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:265:7: OJ table_factor LEFT OUTER JOIN table_factor ON condition
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	OJ425=(IToken)Match(input,OJ,FOLLOW_OJ_in_table_factor2733); 
                    		OJ425_tree = (object)adaptor.Create(OJ425);
                    		adaptor.AddChild(root_0, OJ425_tree);

                    	PushFollow(FOLLOW_table_factor_in_table_factor2735);
                    	table_factor426 = table_factor();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, table_factor426.Tree);
                    	LEFT427=(IToken)Match(input,LEFT,FOLLOW_LEFT_in_table_factor2737); 
                    		LEFT427_tree = (object)adaptor.Create(LEFT427);
                    		adaptor.AddChild(root_0, LEFT427_tree);

                    	OUTER428=(IToken)Match(input,OUTER,FOLLOW_OUTER_in_table_factor2739); 
                    		OUTER428_tree = (object)adaptor.Create(OUTER428);
                    		adaptor.AddChild(root_0, OUTER428_tree);

                    	JOIN429=(IToken)Match(input,JOIN,FOLLOW_JOIN_in_table_factor2741); 
                    		JOIN429_tree = (object)adaptor.Create(JOIN429);
                    		adaptor.AddChild(root_0, JOIN429_tree);

                    	PushFollow(FOLLOW_table_factor_in_table_factor2743);
                    	table_factor430 = table_factor();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, table_factor430.Tree);
                    	ON431=(IToken)Match(input,ON,FOLLOW_ON_in_table_factor2745); 
                    		ON431_tree = (object)adaptor.Create(ON431);
                    		adaptor.AddChild(root_0, ON431_tree);

                    	PushFollow(FOLLOW_condition_in_table_factor2747);
                    	condition432 = condition();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, condition432.Tree);

                    }
                    break;

            }
            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "table_factor"

    public class join_table_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "join_table"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:268:1: join_table : f_nl ( INNER | CROSS | LEFT ( OUTER )? | RIGHT ( OUTER )? | NATURAL ( ( LEFT | RIGHT ) ( OUTER )? )? )? ( JOIN | STRAIGHT_JOIN ) table_factor ( join_condition )? ;
    public MySQLParser.join_table_return join_table() // throws RecognitionException [1]
    {   
        MySQLParser.join_table_return retval = new MySQLParser.join_table_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken INNER434 = null;
        IToken CROSS435 = null;
        IToken LEFT436 = null;
        IToken OUTER437 = null;
        IToken RIGHT438 = null;
        IToken OUTER439 = null;
        IToken NATURAL440 = null;
        IToken set441 = null;
        IToken OUTER442 = null;
        IToken set443 = null;
        MySQLParser.f_nl_return f_nl433 = default(MySQLParser.f_nl_return);

        MySQLParser.table_factor_return table_factor444 = default(MySQLParser.table_factor_return);

        MySQLParser.join_condition_return join_condition445 = default(MySQLParser.join_condition_return);


        object INNER434_tree=null;
        object CROSS435_tree=null;
        object LEFT436_tree=null;
        object OUTER437_tree=null;
        object RIGHT438_tree=null;
        object OUTER439_tree=null;
        object NATURAL440_tree=null;
        object set441_tree=null;
        object OUTER442_tree=null;
        object set443_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:268:11: ( f_nl ( INNER | CROSS | LEFT ( OUTER )? | RIGHT ( OUTER )? | NATURAL ( ( LEFT | RIGHT ) ( OUTER )? )? )? ( JOIN | STRAIGHT_JOIN ) table_factor ( join_condition )? )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:269:5: f_nl ( INNER | CROSS | LEFT ( OUTER )? | RIGHT ( OUTER )? | NATURAL ( ( LEFT | RIGHT ) ( OUTER )? )? )? ( JOIN | STRAIGHT_JOIN ) table_factor ( join_condition )?
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_f_nl_in_join_table2759);
            	f_nl433 = f_nl();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, f_nl433.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:269:10: ( INNER | CROSS | LEFT ( OUTER )? | RIGHT ( OUTER )? | NATURAL ( ( LEFT | RIGHT ) ( OUTER )? )? )?
            	int alt156 = 6;
            	switch ( input.LA(1) ) 
            	{
            	    case INNER:
            	    	{
            	        alt156 = 1;
            	        }
            	        break;
            	    case CROSS:
            	    	{
            	        alt156 = 2;
            	        }
            	        break;
            	    case LEFT:
            	    	{
            	        alt156 = 3;
            	        }
            	        break;
            	    case RIGHT:
            	    	{
            	        alt156 = 4;
            	        }
            	        break;
            	    case NATURAL:
            	    	{
            	        alt156 = 5;
            	        }
            	        break;
            	}

            	switch (alt156) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:270:9: INNER
            	        {
            	        	INNER434=(IToken)Match(input,INNER,FOLLOW_INNER_in_join_table2771); 
            	        		INNER434_tree = (object)adaptor.Create(INNER434);
            	        		adaptor.AddChild(root_0, INNER434_tree);


            	        }
            	        break;
            	    case 2 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:270:17: CROSS
            	        {
            	        	CROSS435=(IToken)Match(input,CROSS,FOLLOW_CROSS_in_join_table2775); 
            	        		CROSS435_tree = (object)adaptor.Create(CROSS435);
            	        		adaptor.AddChild(root_0, CROSS435_tree);


            	        }
            	        break;
            	    case 3 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:270:25: LEFT ( OUTER )?
            	        {
            	        	LEFT436=(IToken)Match(input,LEFT,FOLLOW_LEFT_in_join_table2779); 
            	        		LEFT436_tree = (object)adaptor.Create(LEFT436);
            	        		adaptor.AddChild(root_0, LEFT436_tree);

            	        	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:270:30: ( OUTER )?
            	        	int alt152 = 2;
            	        	int LA152_0 = input.LA(1);

            	        	if ( (LA152_0 == OUTER) )
            	        	{
            	        	    alt152 = 1;
            	        	}
            	        	switch (alt152) 
            	        	{
            	        	    case 1 :
            	        	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:270:30: OUTER
            	        	        {
            	        	        	OUTER437=(IToken)Match(input,OUTER,FOLLOW_OUTER_in_join_table2781); 
            	        	        		OUTER437_tree = (object)adaptor.Create(OUTER437);
            	        	        		adaptor.AddChild(root_0, OUTER437_tree);


            	        	        }
            	        	        break;

            	        	}


            	        }
            	        break;
            	    case 4 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:270:39: RIGHT ( OUTER )?
            	        {
            	        	RIGHT438=(IToken)Match(input,RIGHT,FOLLOW_RIGHT_in_join_table2786); 
            	        		RIGHT438_tree = (object)adaptor.Create(RIGHT438);
            	        		adaptor.AddChild(root_0, RIGHT438_tree);

            	        	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:270:45: ( OUTER )?
            	        	int alt153 = 2;
            	        	int LA153_0 = input.LA(1);

            	        	if ( (LA153_0 == OUTER) )
            	        	{
            	        	    alt153 = 1;
            	        	}
            	        	switch (alt153) 
            	        	{
            	        	    case 1 :
            	        	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:270:45: OUTER
            	        	        {
            	        	        	OUTER439=(IToken)Match(input,OUTER,FOLLOW_OUTER_in_join_table2788); 
            	        	        		OUTER439_tree = (object)adaptor.Create(OUTER439);
            	        	        		adaptor.AddChild(root_0, OUTER439_tree);


            	        	        }
            	        	        break;

            	        	}


            	        }
            	        break;
            	    case 5 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:271:11: NATURAL ( ( LEFT | RIGHT ) ( OUTER )? )?
            	        {
            	        	NATURAL440=(IToken)Match(input,NATURAL,FOLLOW_NATURAL_in_join_table2801); 
            	        		NATURAL440_tree = (object)adaptor.Create(NATURAL440);
            	        		adaptor.AddChild(root_0, NATURAL440_tree);

            	        	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:271:19: ( ( LEFT | RIGHT ) ( OUTER )? )?
            	        	int alt155 = 2;
            	        	int LA155_0 = input.LA(1);

            	        	if ( (LA155_0 == LEFT || LA155_0 == RIGHT) )
            	        	{
            	        	    alt155 = 1;
            	        	}
            	        	switch (alt155) 
            	        	{
            	        	    case 1 :
            	        	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:271:20: ( LEFT | RIGHT ) ( OUTER )?
            	        	        {
            	        	        	set441 = (IToken)input.LT(1);
            	        	        	if ( input.LA(1) == LEFT || input.LA(1) == RIGHT ) 
            	        	        	{
            	        	        	    input.Consume();
            	        	        	    adaptor.AddChild(root_0, (object)adaptor.Create(set441));
            	        	        	    state.errorRecovery = false;
            	        	        	}
            	        	        	else 
            	        	        	{
            	        	        	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	        	        	    throw mse;
            	        	        	}

            	        	        	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:271:33: ( OUTER )?
            	        	        	int alt154 = 2;
            	        	        	int LA154_0 = input.LA(1);

            	        	        	if ( (LA154_0 == OUTER) )
            	        	        	{
            	        	        	    alt154 = 1;
            	        	        	}
            	        	        	switch (alt154) 
            	        	        	{
            	        	        	    case 1 :
            	        	        	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:271:33: OUTER
            	        	        	        {
            	        	        	        	OUTER442=(IToken)Match(input,OUTER,FOLLOW_OUTER_in_join_table2810); 
            	        	        	        		OUTER442_tree = (object)adaptor.Create(OUTER442);
            	        	        	        		adaptor.AddChild(root_0, OUTER442_tree);


            	        	        	        }
            	        	        	        break;

            	        	        	}


            	        	        }
            	        	        break;

            	        	}


            	        }
            	        break;

            	}

            	set443 = (IToken)input.LT(1);
            	if ( input.LA(1) == STRAIGHT_JOIN || input.LA(1) == JOIN ) 
            	{
            	    input.Consume();
            	    adaptor.AddChild(root_0, (object)adaptor.Create(set443));
            	    state.errorRecovery = false;
            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    throw mse;
            	}

            	PushFollow(FOLLOW_table_factor_in_join_table2832);
            	table_factor444 = table_factor();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, table_factor444.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:273:39: ( join_condition )?
            	int alt157 = 2;
            	int LA157_0 = input.LA(1);

            	if ( (LA157_0 == USING || LA157_0 == ON) )
            	{
            	    alt157 = 1;
            	}
            	switch (alt157) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:273:39: join_condition
            	        {
            	        	PushFollow(FOLLOW_join_condition_in_join_table2834);
            	        	join_condition445 = join_condition();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, join_condition445.Tree);

            	        }
            	        break;

            	}


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "join_table"

    public class join_condition_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "join_condition"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:304:1: join_condition : ( ON condition | USING LPAREN id ( COMMA id )* RPAREN );
    public MySQLParser.join_condition_return join_condition() // throws RecognitionException [1]
    {   
        MySQLParser.join_condition_return retval = new MySQLParser.join_condition_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken ON446 = null;
        IToken USING448 = null;
        IToken LPAREN449 = null;
        IToken COMMA451 = null;
        IToken RPAREN453 = null;
        MySQLParser.condition_return condition447 = default(MySQLParser.condition_return);

        MySQLParser.id_return id450 = default(MySQLParser.id_return);

        MySQLParser.id_return id452 = default(MySQLParser.id_return);


        object ON446_tree=null;
        object USING448_tree=null;
        object LPAREN449_tree=null;
        object COMMA451_tree=null;
        object RPAREN453_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:304:15: ( ON condition | USING LPAREN id ( COMMA id )* RPAREN )
            int alt159 = 2;
            int LA159_0 = input.LA(1);

            if ( (LA159_0 == ON) )
            {
                alt159 = 1;
            }
            else if ( (LA159_0 == USING) )
            {
                alt159 = 2;
            }
            else 
            {
                NoViableAltException nvae_d159s0 =
                    new NoViableAltException("", 159, 0, input);

                throw nvae_d159s0;
            }
            switch (alt159) 
            {
                case 1 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:305:5: ON condition
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	ON446=(IToken)Match(input,ON,FOLLOW_ON_in_join_condition2875); 
                    		ON446_tree = (object)adaptor.Create(ON446);
                    		adaptor.AddChild(root_0, ON446_tree);

                    	PushFollow(FOLLOW_condition_in_join_condition2877);
                    	condition447 = condition();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, condition447.Tree);

                    }
                    break;
                case 2 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:306:5: USING LPAREN id ( COMMA id )* RPAREN
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	USING448=(IToken)Match(input,USING,FOLLOW_USING_in_join_condition2883); 
                    		USING448_tree = (object)adaptor.Create(USING448);
                    		adaptor.AddChild(root_0, USING448_tree);

                    	LPAREN449=(IToken)Match(input,LPAREN,FOLLOW_LPAREN_in_join_condition2885); 
                    		LPAREN449_tree = (object)adaptor.Create(LPAREN449);
                    		adaptor.AddChild(root_0, LPAREN449_tree);

                    	PushFollow(FOLLOW_id_in_join_condition2887);
                    	id450 = id();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, id450.Tree);
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:306:21: ( COMMA id )*
                    	do 
                    	{
                    	    int alt158 = 2;
                    	    int LA158_0 = input.LA(1);

                    	    if ( (LA158_0 == COMMA) )
                    	    {
                    	        alt158 = 1;
                    	    }


                    	    switch (alt158) 
                    		{
                    			case 1 :
                    			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:306:22: COMMA id
                    			    {
                    			    	COMMA451=(IToken)Match(input,COMMA,FOLLOW_COMMA_in_join_condition2890); 
                    			    		COMMA451_tree = (object)adaptor.Create(COMMA451);
                    			    		adaptor.AddChild(root_0, COMMA451_tree);

                    			    	PushFollow(FOLLOW_id_in_join_condition2892);
                    			    	id452 = id();
                    			    	state.followingStackPointer--;

                    			    	adaptor.AddChild(root_0, id452.Tree);

                    			    }
                    			    break;

                    			default:
                    			    goto loop158;
                    	    }
                    	} while (true);

                    	loop158:
                    		;	// Stops C# compiler whining that label 'loop158' has no statements

                    	RPAREN453=(IToken)Match(input,RPAREN,FOLLOW_RPAREN_in_join_condition2896); 
                    		RPAREN453_tree = (object)adaptor.Create(RPAREN453);
                    		adaptor.AddChild(root_0, RPAREN453_tree);


                    }
                    break;

            }
            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "join_condition"

    public class index_hint_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "index_hint"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:309:1: index_hint : ( USE ( INDEX | KEY ) ( FOR JOIN )? LPAREN id ( COMMA id )* RPAREN | IGNORE ( INDEX | KEY ) ( FOR JOIN )? LPAREN id ( COMMA id )* RPAREN | FORCE ( INDEX | KEY ) ( FOR JOIN )? LPAREN id ( COMMA id )* RPAREN );
    public MySQLParser.index_hint_return index_hint() // throws RecognitionException [1]
    {   
        MySQLParser.index_hint_return retval = new MySQLParser.index_hint_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken USE454 = null;
        IToken set455 = null;
        IToken FOR456 = null;
        IToken JOIN457 = null;
        IToken LPAREN458 = null;
        IToken COMMA460 = null;
        IToken RPAREN462 = null;
        IToken IGNORE463 = null;
        IToken set464 = null;
        IToken FOR465 = null;
        IToken JOIN466 = null;
        IToken LPAREN467 = null;
        IToken COMMA469 = null;
        IToken RPAREN471 = null;
        IToken FORCE472 = null;
        IToken set473 = null;
        IToken FOR474 = null;
        IToken JOIN475 = null;
        IToken LPAREN476 = null;
        IToken COMMA478 = null;
        IToken RPAREN480 = null;
        MySQLParser.id_return id459 = default(MySQLParser.id_return);

        MySQLParser.id_return id461 = default(MySQLParser.id_return);

        MySQLParser.id_return id468 = default(MySQLParser.id_return);

        MySQLParser.id_return id470 = default(MySQLParser.id_return);

        MySQLParser.id_return id477 = default(MySQLParser.id_return);

        MySQLParser.id_return id479 = default(MySQLParser.id_return);


        object USE454_tree=null;
        object set455_tree=null;
        object FOR456_tree=null;
        object JOIN457_tree=null;
        object LPAREN458_tree=null;
        object COMMA460_tree=null;
        object RPAREN462_tree=null;
        object IGNORE463_tree=null;
        object set464_tree=null;
        object FOR465_tree=null;
        object JOIN466_tree=null;
        object LPAREN467_tree=null;
        object COMMA469_tree=null;
        object RPAREN471_tree=null;
        object FORCE472_tree=null;
        object set473_tree=null;
        object FOR474_tree=null;
        object JOIN475_tree=null;
        object LPAREN476_tree=null;
        object COMMA478_tree=null;
        object RPAREN480_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:309:11: ( USE ( INDEX | KEY ) ( FOR JOIN )? LPAREN id ( COMMA id )* RPAREN | IGNORE ( INDEX | KEY ) ( FOR JOIN )? LPAREN id ( COMMA id )* RPAREN | FORCE ( INDEX | KEY ) ( FOR JOIN )? LPAREN id ( COMMA id )* RPAREN )
            int alt166 = 3;
            switch ( input.LA(1) ) 
            {
            case USE:
            	{
                alt166 = 1;
                }
                break;
            case IGNORE:
            	{
                alt166 = 2;
                }
                break;
            case FORCE:
            	{
                alt166 = 3;
                }
                break;
            	default:
            	    NoViableAltException nvae_d166s0 =
            	        new NoViableAltException("", 166, 0, input);

            	    throw nvae_d166s0;
            }

            switch (alt166) 
            {
                case 1 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:310:5: USE ( INDEX | KEY ) ( FOR JOIN )? LPAREN id ( COMMA id )* RPAREN
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	USE454=(IToken)Match(input,USE,FOLLOW_USE_in_index_hint2910); 
                    		USE454_tree = (object)adaptor.Create(USE454);
                    		adaptor.AddChild(root_0, USE454_tree);

                    	set455 = (IToken)input.LT(1);
                    	if ( (input.LA(1) >= INDEX && input.LA(1) <= KEY) ) 
                    	{
                    	    input.Consume();
                    	    adaptor.AddChild(root_0, (object)adaptor.Create(set455));
                    	    state.errorRecovery = false;
                    	}
                    	else 
                    	{
                    	    MismatchedSetException mse = new MismatchedSetException(null,input);
                    	    throw mse;
                    	}

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:310:21: ( FOR JOIN )?
                    	int alt160 = 2;
                    	int LA160_0 = input.LA(1);

                    	if ( (LA160_0 == FOR) )
                    	{
                    	    alt160 = 1;
                    	}
                    	switch (alt160) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:310:22: FOR JOIN
                    	        {
                    	        	FOR456=(IToken)Match(input,FOR,FOLLOW_FOR_in_index_hint2919); 
                    	        		FOR456_tree = (object)adaptor.Create(FOR456);
                    	        		adaptor.AddChild(root_0, FOR456_tree);

                    	        	JOIN457=(IToken)Match(input,JOIN,FOLLOW_JOIN_in_index_hint2921); 
                    	        		JOIN457_tree = (object)adaptor.Create(JOIN457);
                    	        		adaptor.AddChild(root_0, JOIN457_tree);


                    	        }
                    	        break;

                    	}

                    	LPAREN458=(IToken)Match(input,LPAREN,FOLLOW_LPAREN_in_index_hint2925); 
                    		LPAREN458_tree = (object)adaptor.Create(LPAREN458);
                    		adaptor.AddChild(root_0, LPAREN458_tree);

                    	PushFollow(FOLLOW_id_in_index_hint2927);
                    	id459 = id();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, id459.Tree);
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:310:43: ( COMMA id )*
                    	do 
                    	{
                    	    int alt161 = 2;
                    	    int LA161_0 = input.LA(1);

                    	    if ( (LA161_0 == COMMA) )
                    	    {
                    	        alt161 = 1;
                    	    }


                    	    switch (alt161) 
                    		{
                    			case 1 :
                    			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:310:44: COMMA id
                    			    {
                    			    	COMMA460=(IToken)Match(input,COMMA,FOLLOW_COMMA_in_index_hint2930); 
                    			    		COMMA460_tree = (object)adaptor.Create(COMMA460);
                    			    		adaptor.AddChild(root_0, COMMA460_tree);

                    			    	PushFollow(FOLLOW_id_in_index_hint2932);
                    			    	id461 = id();
                    			    	state.followingStackPointer--;

                    			    	adaptor.AddChild(root_0, id461.Tree);

                    			    }
                    			    break;

                    			default:
                    			    goto loop161;
                    	    }
                    	} while (true);

                    	loop161:
                    		;	// Stops C# compiler whining that label 'loop161' has no statements

                    	RPAREN462=(IToken)Match(input,RPAREN,FOLLOW_RPAREN_in_index_hint2936); 
                    		RPAREN462_tree = (object)adaptor.Create(RPAREN462);
                    		adaptor.AddChild(root_0, RPAREN462_tree);


                    }
                    break;
                case 2 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:311:5: IGNORE ( INDEX | KEY ) ( FOR JOIN )? LPAREN id ( COMMA id )* RPAREN
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	IGNORE463=(IToken)Match(input,IGNORE,FOLLOW_IGNORE_in_index_hint2942); 
                    		IGNORE463_tree = (object)adaptor.Create(IGNORE463);
                    		adaptor.AddChild(root_0, IGNORE463_tree);

                    	set464 = (IToken)input.LT(1);
                    	if ( (input.LA(1) >= INDEX && input.LA(1) <= KEY) ) 
                    	{
                    	    input.Consume();
                    	    adaptor.AddChild(root_0, (object)adaptor.Create(set464));
                    	    state.errorRecovery = false;
                    	}
                    	else 
                    	{
                    	    MismatchedSetException mse = new MismatchedSetException(null,input);
                    	    throw mse;
                    	}

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:311:24: ( FOR JOIN )?
                    	int alt162 = 2;
                    	int LA162_0 = input.LA(1);

                    	if ( (LA162_0 == FOR) )
                    	{
                    	    alt162 = 1;
                    	}
                    	switch (alt162) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:311:25: FOR JOIN
                    	        {
                    	        	FOR465=(IToken)Match(input,FOR,FOLLOW_FOR_in_index_hint2951); 
                    	        		FOR465_tree = (object)adaptor.Create(FOR465);
                    	        		adaptor.AddChild(root_0, FOR465_tree);

                    	        	JOIN466=(IToken)Match(input,JOIN,FOLLOW_JOIN_in_index_hint2953); 
                    	        		JOIN466_tree = (object)adaptor.Create(JOIN466);
                    	        		adaptor.AddChild(root_0, JOIN466_tree);


                    	        }
                    	        break;

                    	}

                    	LPAREN467=(IToken)Match(input,LPAREN,FOLLOW_LPAREN_in_index_hint2957); 
                    		LPAREN467_tree = (object)adaptor.Create(LPAREN467);
                    		adaptor.AddChild(root_0, LPAREN467_tree);

                    	PushFollow(FOLLOW_id_in_index_hint2959);
                    	id468 = id();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, id468.Tree);
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:311:46: ( COMMA id )*
                    	do 
                    	{
                    	    int alt163 = 2;
                    	    int LA163_0 = input.LA(1);

                    	    if ( (LA163_0 == COMMA) )
                    	    {
                    	        alt163 = 1;
                    	    }


                    	    switch (alt163) 
                    		{
                    			case 1 :
                    			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:311:47: COMMA id
                    			    {
                    			    	COMMA469=(IToken)Match(input,COMMA,FOLLOW_COMMA_in_index_hint2962); 
                    			    		COMMA469_tree = (object)adaptor.Create(COMMA469);
                    			    		adaptor.AddChild(root_0, COMMA469_tree);

                    			    	PushFollow(FOLLOW_id_in_index_hint2964);
                    			    	id470 = id();
                    			    	state.followingStackPointer--;

                    			    	adaptor.AddChild(root_0, id470.Tree);

                    			    }
                    			    break;

                    			default:
                    			    goto loop163;
                    	    }
                    	} while (true);

                    	loop163:
                    		;	// Stops C# compiler whining that label 'loop163' has no statements

                    	RPAREN471=(IToken)Match(input,RPAREN,FOLLOW_RPAREN_in_index_hint2968); 
                    		RPAREN471_tree = (object)adaptor.Create(RPAREN471);
                    		adaptor.AddChild(root_0, RPAREN471_tree);


                    }
                    break;
                case 3 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:312:5: FORCE ( INDEX | KEY ) ( FOR JOIN )? LPAREN id ( COMMA id )* RPAREN
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	FORCE472=(IToken)Match(input,FORCE,FOLLOW_FORCE_in_index_hint2974); 
                    		FORCE472_tree = (object)adaptor.Create(FORCE472);
                    		adaptor.AddChild(root_0, FORCE472_tree);

                    	set473 = (IToken)input.LT(1);
                    	if ( (input.LA(1) >= INDEX && input.LA(1) <= KEY) ) 
                    	{
                    	    input.Consume();
                    	    adaptor.AddChild(root_0, (object)adaptor.Create(set473));
                    	    state.errorRecovery = false;
                    	}
                    	else 
                    	{
                    	    MismatchedSetException mse = new MismatchedSetException(null,input);
                    	    throw mse;
                    	}

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:312:23: ( FOR JOIN )?
                    	int alt164 = 2;
                    	int LA164_0 = input.LA(1);

                    	if ( (LA164_0 == FOR) )
                    	{
                    	    alt164 = 1;
                    	}
                    	switch (alt164) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:312:24: FOR JOIN
                    	        {
                    	        	FOR474=(IToken)Match(input,FOR,FOLLOW_FOR_in_index_hint2983); 
                    	        		FOR474_tree = (object)adaptor.Create(FOR474);
                    	        		adaptor.AddChild(root_0, FOR474_tree);

                    	        	JOIN475=(IToken)Match(input,JOIN,FOLLOW_JOIN_in_index_hint2985); 
                    	        		JOIN475_tree = (object)adaptor.Create(JOIN475);
                    	        		adaptor.AddChild(root_0, JOIN475_tree);


                    	        }
                    	        break;

                    	}

                    	LPAREN476=(IToken)Match(input,LPAREN,FOLLOW_LPAREN_in_index_hint2989); 
                    		LPAREN476_tree = (object)adaptor.Create(LPAREN476);
                    		adaptor.AddChild(root_0, LPAREN476_tree);

                    	PushFollow(FOLLOW_id_in_index_hint2991);
                    	id477 = id();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, id477.Tree);
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:312:45: ( COMMA id )*
                    	do 
                    	{
                    	    int alt165 = 2;
                    	    int LA165_0 = input.LA(1);

                    	    if ( (LA165_0 == COMMA) )
                    	    {
                    	        alt165 = 1;
                    	    }


                    	    switch (alt165) 
                    		{
                    			case 1 :
                    			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:312:46: COMMA id
                    			    {
                    			    	COMMA478=(IToken)Match(input,COMMA,FOLLOW_COMMA_in_index_hint2994); 
                    			    		COMMA478_tree = (object)adaptor.Create(COMMA478);
                    			    		adaptor.AddChild(root_0, COMMA478_tree);

                    			    	PushFollow(FOLLOW_id_in_index_hint2996);
                    			    	id479 = id();
                    			    	state.followingStackPointer--;

                    			    	adaptor.AddChild(root_0, id479.Tree);

                    			    }
                    			    break;

                    			default:
                    			    goto loop165;
                    	    }
                    	} while (true);

                    	loop165:
                    		;	// Stops C# compiler whining that label 'loop165' has no statements

                    	RPAREN480=(IToken)Match(input,RPAREN,FOLLOW_RPAREN_in_index_hint3000); 
                    		RPAREN480_tree = (object)adaptor.Create(RPAREN480);
                    		adaptor.AddChild(root_0, RPAREN480_tree);


                    }
                    break;

            }
            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "index_hint"

    public class table_subquery_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "table_subquery"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:315:1: table_subquery : LPAREN select_statement RPAREN ;
    public MySQLParser.table_subquery_return table_subquery() // throws RecognitionException [1]
    {   
        MySQLParser.table_subquery_return retval = new MySQLParser.table_subquery_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken LPAREN481 = null;
        IToken RPAREN483 = null;
        MySQLParser.select_statement_return select_statement482 = default(MySQLParser.select_statement_return);


        object LPAREN481_tree=null;
        object RPAREN483_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:315:15: ( LPAREN select_statement RPAREN )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:316:5: LPAREN select_statement RPAREN
            {
            	root_0 = (object)adaptor.GetNilNode();

            	LPAREN481=(IToken)Match(input,LPAREN,FOLLOW_LPAREN_in_table_subquery3014); 
            		LPAREN481_tree = (object)adaptor.Create(LPAREN481);
            		adaptor.AddChild(root_0, LPAREN481_tree);

            	PushFollow(FOLLOW_select_statement_in_table_subquery3016);
            	select_statement482 = select_statement();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, select_statement482.Tree);
            	RPAREN483=(IToken)Match(input,RPAREN,FOLLOW_RPAREN_in_table_subquery3018); 
            		RPAREN483_tree = (object)adaptor.Create(RPAREN483);
            		adaptor.AddChild(root_0, RPAREN483_tree);


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "table_subquery"

    public class user_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "user"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:318:1: user : ( CURRENT_USER ( LPAREN RPAREN )? | id AT id );
    public MySQLParser.user_return user() // throws RecognitionException [1]
    {   
        MySQLParser.user_return retval = new MySQLParser.user_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken CURRENT_USER484 = null;
        IToken LPAREN485 = null;
        IToken RPAREN486 = null;
        IToken AT488 = null;
        MySQLParser.id_return id487 = default(MySQLParser.id_return);

        MySQLParser.id_return id489 = default(MySQLParser.id_return);


        object CURRENT_USER484_tree=null;
        object LPAREN485_tree=null;
        object RPAREN486_tree=null;
        object AT488_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:318:5: ( CURRENT_USER ( LPAREN RPAREN )? | id AT id )
            int alt168 = 2;
            int LA168_0 = input.LA(1);

            if ( (LA168_0 == CURRENT_USER) )
            {
                alt168 = 1;
            }
            else if ( ((LA168_0 >= T_IDENT && LA168_0 <= T_QUOTED_IDENT)) )
            {
                alt168 = 2;
            }
            else 
            {
                NoViableAltException nvae_d168s0 =
                    new NoViableAltException("", 168, 0, input);

                throw nvae_d168s0;
            }
            switch (alt168) 
            {
                case 1 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:319:5: CURRENT_USER ( LPAREN RPAREN )?
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	CURRENT_USER484=(IToken)Match(input,CURRENT_USER,FOLLOW_CURRENT_USER_in_user3029); 
                    		CURRENT_USER484_tree = (object)adaptor.Create(CURRENT_USER484);
                    		adaptor.AddChild(root_0, CURRENT_USER484_tree);

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:319:18: ( LPAREN RPAREN )?
                    	int alt167 = 2;
                    	int LA167_0 = input.LA(1);

                    	if ( (LA167_0 == LPAREN) )
                    	{
                    	    alt167 = 1;
                    	}
                    	switch (alt167) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:319:20: LPAREN RPAREN
                    	        {
                    	        	LPAREN485=(IToken)Match(input,LPAREN,FOLLOW_LPAREN_in_user3033); 
                    	        		LPAREN485_tree = (object)adaptor.Create(LPAREN485);
                    	        		adaptor.AddChild(root_0, LPAREN485_tree);

                    	        	RPAREN486=(IToken)Match(input,RPAREN,FOLLOW_RPAREN_in_user3035); 
                    	        		RPAREN486_tree = (object)adaptor.Create(RPAREN486);
                    	        		adaptor.AddChild(root_0, RPAREN486_tree);


                    	        }
                    	        break;

                    	}


                    }
                    break;
                case 2 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:320:5: id AT id
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_id_in_user3046);
                    	id487 = id();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, id487.Tree);
                    	AT488=(IToken)Match(input,AT,FOLLOW_AT_in_user3048); 
                    		AT488_tree = (object)adaptor.Create(AT488);
                    		adaptor.AddChild(root_0, AT488_tree);

                    	PushFollow(FOLLOW_id_in_user3050);
                    	id489 = id();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, id489.Tree);

                    }
                    break;

            }
            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "user"

    public class default_value_expr_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "default_value_expr"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:322:1: default_value_expr[ColumnStructure col] : (sval= T_STRING | ival= T_INTEGER | ival= T_BINARYNUM | fval= T_FLOAT | NULL | CURRENT_TIMESTAMP | CURRENT_DATE );
    public MySQLParser.default_value_expr_return default_value_expr(ColumnStructure col) // throws RecognitionException [1]
    {   
        MySQLParser.default_value_expr_return retval = new MySQLParser.default_value_expr_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken sval = null;
        IToken ival = null;
        IToken fval = null;
        IToken NULL490 = null;
        IToken CURRENT_TIMESTAMP491 = null;
        IToken CURRENT_DATE492 = null;

        object sval_tree=null;
        object ival_tree=null;
        object fval_tree=null;
        object NULL490_tree=null;
        object CURRENT_TIMESTAMP491_tree=null;
        object CURRENT_DATE492_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:322:40: (sval= T_STRING | ival= T_INTEGER | ival= T_BINARYNUM | fval= T_FLOAT | NULL | CURRENT_TIMESTAMP | CURRENT_DATE )
            int alt169 = 7;
            switch ( input.LA(1) ) 
            {
            case T_STRING:
            	{
                alt169 = 1;
                }
                break;
            case T_INTEGER:
            	{
                alt169 = 2;
                }
                break;
            case T_BINARYNUM:
            	{
                alt169 = 3;
                }
                break;
            case T_FLOAT:
            	{
                alt169 = 4;
                }
                break;
            case NULL:
            	{
                alt169 = 5;
                }
                break;
            case CURRENT_TIMESTAMP:
            	{
                alt169 = 6;
                }
                break;
            case CURRENT_DATE:
            	{
                alt169 = 7;
                }
                break;
            	default:
            	    NoViableAltException nvae_d169s0 =
            	        new NoViableAltException("", 169, 0, input);

            	    throw nvae_d169s0;
            }

            switch (alt169) 
            {
                case 1 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:323:5: sval= T_STRING
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	sval=(IToken)Match(input,T_STRING,FOLLOW_T_STRING_in_default_value_expr3064); 
                    		sval_tree = (object)adaptor.Create(sval);
                    		adaptor.AddChild(root_0, sval_tree);

                    	col.DefaultValue = new ConstStringSqlExpression(StringValue(((sval != null) ? sval.Text : null))); 

                    }
                    break;
                case 2 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:324:5: ival= T_INTEGER
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	ival=(IToken)Match(input,T_INTEGER,FOLLOW_T_INTEGER_in_default_value_expr3074); 
                    		ival_tree = (object)adaptor.Create(ival);
                    		adaptor.AddChild(root_0, ival_tree);

                    	col.DefaultValue = new ConstNumberSqlExpression(Int32.Parse(((ival != null) ? ival.Text : null), CultureInfo.InvariantCulture)); 

                    }
                    break;
                case 3 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:325:5: ival= T_BINARYNUM
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	ival=(IToken)Match(input,T_BINARYNUM,FOLLOW_T_BINARYNUM_in_default_value_expr3084); 
                    		ival_tree = (object)adaptor.Create(ival);
                    		adaptor.AddChild(root_0, ival_tree);

                    	col.DefaultValue = new ConstNumberSqlExpression(ParseBinNum(((ival != null) ? ival.Text : null))); 

                    }
                    break;
                case 4 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:326:5: fval= T_FLOAT
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	fval=(IToken)Match(input,T_FLOAT,FOLLOW_T_FLOAT_in_default_value_expr3094); 
                    		fval_tree = (object)adaptor.Create(fval);
                    		adaptor.AddChild(root_0, fval_tree);

                    	col.DefaultValue = new ConstNumberSqlExpression(Double.Parse(((fval != null) ? fval.Text : null), CultureInfo.InvariantCulture)); 

                    }
                    break;
                case 5 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:327:5: NULL
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	NULL490=(IToken)Match(input,NULL,FOLLOW_NULL_in_default_value_expr3102); 
                    		NULL490_tree = (object)adaptor.Create(NULL490);
                    		adaptor.AddChild(root_0, NULL490_tree);

                    	col.DefaultValue = null; 

                    }
                    break;
                case 6 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:328:5: CURRENT_TIMESTAMP
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	CURRENT_TIMESTAMP491=(IToken)Match(input,CURRENT_TIMESTAMP,FOLLOW_CURRENT_TIMESTAMP_in_default_value_expr3111); 
                    		CURRENT_TIMESTAMP491_tree = (object)adaptor.Create(CURRENT_TIMESTAMP491);
                    		adaptor.AddChild(root_0, CURRENT_TIMESTAMP491_tree);

                    	col.DefaultValue = new SpecialConstantSqlExpression(SqlSpecialConstant.Current_Timestamp); 

                    }
                    break;
                case 7 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:329:5: CURRENT_DATE
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	CURRENT_DATE492=(IToken)Match(input,CURRENT_DATE,FOLLOW_CURRENT_DATE_in_default_value_expr3119); 
                    		CURRENT_DATE492_tree = (object)adaptor.Create(CURRENT_DATE492);
                    		adaptor.AddChild(root_0, CURRENT_DATE492_tree);

                    	col.DefaultValue = new SpecialConstantSqlExpression(SqlSpecialConstant.Current_Date); 

                    }
                    break;

            }
            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "default_value_expr"

    public class case_expr_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "case_expr"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:332:1: case_expr : ( case_expr1 | case_expr2 );
    public MySQLParser.case_expr_return case_expr() // throws RecognitionException [1]
    {   
        MySQLParser.case_expr_return retval = new MySQLParser.case_expr_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        MySQLParser.case_expr1_return case_expr1493 = default(MySQLParser.case_expr1_return);

        MySQLParser.case_expr2_return case_expr2494 = default(MySQLParser.case_expr2_return);



        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:332:10: ( case_expr1 | case_expr2 )
            int alt170 = 2;
            int LA170_0 = input.LA(1);

            if ( (LA170_0 == CASE) )
            {
                int LA170_1 = input.LA(2);

                if ( (LA170_1 == WHEN) )
                {
                    alt170 = 2;
                }
                else if ( (LA170_1 == T_INTEGER || LA170_1 == NULL || LA170_1 == CURRENT_TIMESTAMP || LA170_1 == T_STRING || LA170_1 == T_FLOAT || LA170_1 == T_BLOB || LA170_1 == UTC_TIMESTAMP) )
                {
                    alt170 = 1;
                }
                else 
                {
                    NoViableAltException nvae_d170s1 =
                        new NoViableAltException("", 170, 1, input);

                    throw nvae_d170s1;
                }
            }
            else 
            {
                NoViableAltException nvae_d170s0 =
                    new NoViableAltException("", 170, 0, input);

                throw nvae_d170s0;
            }
            switch (alt170) 
            {
                case 1 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:332:12: case_expr1
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_case_expr1_in_case_expr3131);
                    	case_expr1493 = case_expr1();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, case_expr1493.Tree);

                    }
                    break;
                case 2 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:332:25: case_expr2
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_case_expr2_in_case_expr3135);
                    	case_expr2494 = case_expr2();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, case_expr2494.Tree);

                    }
                    break;

            }
            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "case_expr"

    public class case_expr1_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "case_expr1"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:333:1: case_expr1 : CASE value ( WHEN value THEN expr )+ ( ELSE expr )? END CASE ;
    public MySQLParser.case_expr1_return case_expr1() // throws RecognitionException [1]
    {   
        MySQLParser.case_expr1_return retval = new MySQLParser.case_expr1_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken CASE495 = null;
        IToken WHEN497 = null;
        IToken THEN499 = null;
        IToken ELSE501 = null;
        IToken END503 = null;
        IToken CASE504 = null;
        MySQLParser.value_return value496 = default(MySQLParser.value_return);

        MySQLParser.value_return value498 = default(MySQLParser.value_return);

        MySQLParser.expr_return expr500 = default(MySQLParser.expr_return);

        MySQLParser.expr_return expr502 = default(MySQLParser.expr_return);


        object CASE495_tree=null;
        object WHEN497_tree=null;
        object THEN499_tree=null;
        object ELSE501_tree=null;
        object END503_tree=null;
        object CASE504_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:333:11: ( CASE value ( WHEN value THEN expr )+ ( ELSE expr )? END CASE )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:333:13: CASE value ( WHEN value THEN expr )+ ( ELSE expr )? END CASE
            {
            	root_0 = (object)adaptor.GetNilNode();

            	CASE495=(IToken)Match(input,CASE,FOLLOW_CASE_in_case_expr13142); 
            		CASE495_tree = (object)adaptor.Create(CASE495);
            		adaptor.AddChild(root_0, CASE495_tree);

            	PushFollow(FOLLOW_value_in_case_expr13144);
            	value496 = value();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, value496.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:334:5: ( WHEN value THEN expr )+
            	int cnt171 = 0;
            	do 
            	{
            	    int alt171 = 2;
            	    int LA171_0 = input.LA(1);

            	    if ( (LA171_0 == WHEN) )
            	    {
            	        alt171 = 1;
            	    }


            	    switch (alt171) 
            		{
            			case 1 :
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:334:6: WHEN value THEN expr
            			    {
            			    	WHEN497=(IToken)Match(input,WHEN,FOLLOW_WHEN_in_case_expr13151); 
            			    		WHEN497_tree = (object)adaptor.Create(WHEN497);
            			    		adaptor.AddChild(root_0, WHEN497_tree);

            			    	PushFollow(FOLLOW_value_in_case_expr13153);
            			    	value498 = value();
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, value498.Tree);
            			    	THEN499=(IToken)Match(input,THEN,FOLLOW_THEN_in_case_expr13155); 
            			    		THEN499_tree = (object)adaptor.Create(THEN499);
            			    		adaptor.AddChild(root_0, THEN499_tree);

            			    	PushFollow(FOLLOW_expr_in_case_expr13157);
            			    	expr500 = expr();
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, expr500.Tree);

            			    }
            			    break;

            			default:
            			    if ( cnt171 >= 1 ) goto loop171;
            		            EarlyExitException eee171 =
            		                new EarlyExitException(171, input);
            		            throw eee171;
            	    }
            	    cnt171++;
            	} while (true);

            	loop171:
            		;	// Stops C# compiler whining that label 'loop171' has no statements

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:335:5: ( ELSE expr )?
            	int alt172 = 2;
            	int LA172_0 = input.LA(1);

            	if ( (LA172_0 == ELSE) )
            	{
            	    alt172 = 1;
            	}
            	switch (alt172) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:335:6: ELSE expr
            	        {
            	        	ELSE501=(IToken)Match(input,ELSE,FOLLOW_ELSE_in_case_expr13166); 
            	        		ELSE501_tree = (object)adaptor.Create(ELSE501);
            	        		adaptor.AddChild(root_0, ELSE501_tree);

            	        	PushFollow(FOLLOW_expr_in_case_expr13168);
            	        	expr502 = expr();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, expr502.Tree);

            	        }
            	        break;

            	}

            	END503=(IToken)Match(input,END,FOLLOW_END_in_case_expr13176); 
            		END503_tree = (object)adaptor.Create(END503);
            		adaptor.AddChild(root_0, END503_tree);

            	CASE504=(IToken)Match(input,CASE,FOLLOW_CASE_in_case_expr13178); 
            		CASE504_tree = (object)adaptor.Create(CASE504);
            		adaptor.AddChild(root_0, CASE504_tree);


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "case_expr1"

    public class case_expr2_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "case_expr2"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:337:1: case_expr2 : CASE ( WHEN condition THEN expr )+ ( ELSE expr )? END CASE ;
    public MySQLParser.case_expr2_return case_expr2() // throws RecognitionException [1]
    {   
        MySQLParser.case_expr2_return retval = new MySQLParser.case_expr2_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken CASE505 = null;
        IToken WHEN506 = null;
        IToken THEN508 = null;
        IToken ELSE510 = null;
        IToken END512 = null;
        IToken CASE513 = null;
        MySQLParser.condition_return condition507 = default(MySQLParser.condition_return);

        MySQLParser.expr_return expr509 = default(MySQLParser.expr_return);

        MySQLParser.expr_return expr511 = default(MySQLParser.expr_return);


        object CASE505_tree=null;
        object WHEN506_tree=null;
        object THEN508_tree=null;
        object ELSE510_tree=null;
        object END512_tree=null;
        object CASE513_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:337:11: ( CASE ( WHEN condition THEN expr )+ ( ELSE expr )? END CASE )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:337:13: CASE ( WHEN condition THEN expr )+ ( ELSE expr )? END CASE
            {
            	root_0 = (object)adaptor.GetNilNode();

            	CASE505=(IToken)Match(input,CASE,FOLLOW_CASE_in_case_expr23184); 
            		CASE505_tree = (object)adaptor.Create(CASE505);
            		adaptor.AddChild(root_0, CASE505_tree);

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:338:5: ( WHEN condition THEN expr )+
            	int cnt173 = 0;
            	do 
            	{
            	    int alt173 = 2;
            	    int LA173_0 = input.LA(1);

            	    if ( (LA173_0 == WHEN) )
            	    {
            	        alt173 = 1;
            	    }


            	    switch (alt173) 
            		{
            			case 1 :
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:338:6: WHEN condition THEN expr
            			    {
            			    	WHEN506=(IToken)Match(input,WHEN,FOLLOW_WHEN_in_case_expr23191); 
            			    		WHEN506_tree = (object)adaptor.Create(WHEN506);
            			    		adaptor.AddChild(root_0, WHEN506_tree);

            			    	PushFollow(FOLLOW_condition_in_case_expr23193);
            			    	condition507 = condition();
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, condition507.Tree);
            			    	THEN508=(IToken)Match(input,THEN,FOLLOW_THEN_in_case_expr23195); 
            			    		THEN508_tree = (object)adaptor.Create(THEN508);
            			    		adaptor.AddChild(root_0, THEN508_tree);

            			    	PushFollow(FOLLOW_expr_in_case_expr23197);
            			    	expr509 = expr();
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, expr509.Tree);

            			    }
            			    break;

            			default:
            			    if ( cnt173 >= 1 ) goto loop173;
            		            EarlyExitException eee173 =
            		                new EarlyExitException(173, input);
            		            throw eee173;
            	    }
            	    cnt173++;
            	} while (true);

            	loop173:
            		;	// Stops C# compiler whining that label 'loop173' has no statements

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:339:5: ( ELSE expr )?
            	int alt174 = 2;
            	int LA174_0 = input.LA(1);

            	if ( (LA174_0 == ELSE) )
            	{
            	    alt174 = 1;
            	}
            	switch (alt174) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:339:6: ELSE expr
            	        {
            	        	ELSE510=(IToken)Match(input,ELSE,FOLLOW_ELSE_in_case_expr23206); 
            	        		ELSE510_tree = (object)adaptor.Create(ELSE510);
            	        		adaptor.AddChild(root_0, ELSE510_tree);

            	        	PushFollow(FOLLOW_expr_in_case_expr23208);
            	        	expr511 = expr();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, expr511.Tree);

            	        }
            	        break;

            	}

            	END512=(IToken)Match(input,END,FOLLOW_END_in_case_expr23216); 
            		END512_tree = (object)adaptor.Create(END512);
            		adaptor.AddChild(root_0, END512_tree);

            	CASE513=(IToken)Match(input,CASE,FOLLOW_CASE_in_case_expr23218); 
            		CASE513_tree = (object)adaptor.Create(CASE513);
            		adaptor.AddChild(root_0, CASE513_tree);


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "case_expr2"

    public class value_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "value"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:342:1: value : ( T_INTEGER | T_STRING | NULL | T_FLOAT | T_BLOB | CURRENT_TIMESTAMP | UTC_TIMESTAMP );
    public MySQLParser.value_return value() // throws RecognitionException [1]
    {   
        MySQLParser.value_return retval = new MySQLParser.value_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken set514 = null;

        object set514_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:342:6: ( T_INTEGER | T_STRING | NULL | T_FLOAT | T_BLOB | CURRENT_TIMESTAMP | UTC_TIMESTAMP )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:
            {
            	root_0 = (object)adaptor.GetNilNode();

            	set514 = (IToken)input.LT(1);
            	if ( input.LA(1) == T_INTEGER || input.LA(1) == NULL || input.LA(1) == CURRENT_TIMESTAMP || input.LA(1) == T_STRING || input.LA(1) == T_FLOAT || input.LA(1) == T_BLOB || input.LA(1) == UTC_TIMESTAMP ) 
            	{
            	    input.Consume();
            	    adaptor.AddChild(root_0, (object)adaptor.Create(set514));
            	    state.errorRecovery = false;
            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    throw mse;
            	}


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "value"

    public class expr_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "expr"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:344:1: expr : condition ;
    public MySQLParser.expr_return expr() // throws RecognitionException [1]
    {   
        MySQLParser.expr_return retval = new MySQLParser.expr_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        MySQLParser.condition_return condition515 = default(MySQLParser.condition_return);



        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:344:5: ( condition )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:344:7: condition
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_condition_in_expr3256);
            	condition515 = condition();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, condition515.Tree);

            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "expr"

    public class arg_list_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "arg_list"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:346:1: arg_list : ( LPAREN DISTINCT ( ASTERISK | expr ) RPAREN | LPAREN expr ( COMMA expr )* RPAREN | LPAREN RPAREN );
    public MySQLParser.arg_list_return arg_list() // throws RecognitionException [1]
    {   
        MySQLParser.arg_list_return retval = new MySQLParser.arg_list_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken LPAREN516 = null;
        IToken DISTINCT517 = null;
        IToken ASTERISK518 = null;
        IToken RPAREN520 = null;
        IToken LPAREN521 = null;
        IToken COMMA523 = null;
        IToken RPAREN525 = null;
        IToken LPAREN526 = null;
        IToken RPAREN527 = null;
        MySQLParser.expr_return expr519 = default(MySQLParser.expr_return);

        MySQLParser.expr_return expr522 = default(MySQLParser.expr_return);

        MySQLParser.expr_return expr524 = default(MySQLParser.expr_return);


        object LPAREN516_tree=null;
        object DISTINCT517_tree=null;
        object ASTERISK518_tree=null;
        object RPAREN520_tree=null;
        object LPAREN521_tree=null;
        object COMMA523_tree=null;
        object RPAREN525_tree=null;
        object LPAREN526_tree=null;
        object RPAREN527_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:346:9: ( LPAREN DISTINCT ( ASTERISK | expr ) RPAREN | LPAREN expr ( COMMA expr )* RPAREN | LPAREN RPAREN )
            int alt177 = 3;
            int LA177_0 = input.LA(1);

            if ( (LA177_0 == LPAREN) )
            {
                switch ( input.LA(2) ) 
                {
                case DISTINCT:
                	{
                    alt177 = 1;
                    }
                    break;
                case RPAREN:
                	{
                    alt177 = 3;
                    }
                    break;
                case LPAREN:
                case T_INTEGER:
                case T_IDENT:
                case T_QUOTED_IDENT:
                case NOT:
                case NULL:
                case CURRENT_TIMESTAMP:
                case T_STRING:
                case YEAR:
                case T_FLOAT:
                case T_BLOB:
                case CURRENT_USER:
                case CURRENT_DATE:
                case CASE:
                case UTC_TIMESTAMP:
                case INTERVAL:
                case EXCLAMATION:
                case MINUS:
                case TILDA:
                case CURRENT_TIME:
                	{
                    alt177 = 2;
                    }
                    break;
                	default:
                	    NoViableAltException nvae_d177s1 =
                	        new NoViableAltException("", 177, 1, input);

                	    throw nvae_d177s1;
                }

            }
            else 
            {
                NoViableAltException nvae_d177s0 =
                    new NoViableAltException("", 177, 0, input);

                throw nvae_d177s0;
            }
            switch (alt177) 
            {
                case 1 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:346:11: LPAREN DISTINCT ( ASTERISK | expr ) RPAREN
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	LPAREN516=(IToken)Match(input,LPAREN,FOLLOW_LPAREN_in_arg_list3263); 
                    		LPAREN516_tree = (object)adaptor.Create(LPAREN516);
                    		adaptor.AddChild(root_0, LPAREN516_tree);

                    	DISTINCT517=(IToken)Match(input,DISTINCT,FOLLOW_DISTINCT_in_arg_list3265); 
                    		DISTINCT517_tree = (object)adaptor.Create(DISTINCT517);
                    		adaptor.AddChild(root_0, DISTINCT517_tree);

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:346:27: ( ASTERISK | expr )
                    	int alt175 = 2;
                    	int LA175_0 = input.LA(1);

                    	if ( (LA175_0 == ASTERISK) )
                    	{
                    	    alt175 = 1;
                    	}
                    	else if ( (LA175_0 == LPAREN || LA175_0 == T_INTEGER || (LA175_0 >= T_IDENT && LA175_0 <= NULL) || LA175_0 == CURRENT_TIMESTAMP || LA175_0 == T_STRING || LA175_0 == YEAR || LA175_0 == T_FLOAT || LA175_0 == T_BLOB || LA175_0 == CURRENT_USER || (LA175_0 >= CURRENT_DATE && LA175_0 <= CASE) || (LA175_0 >= UTC_TIMESTAMP && LA175_0 <= TILDA) || LA175_0 == CURRENT_TIME) )
                    	{
                    	    alt175 = 2;
                    	}
                    	else 
                    	{
                    	    NoViableAltException nvae_d175s0 =
                    	        new NoViableAltException("", 175, 0, input);

                    	    throw nvae_d175s0;
                    	}
                    	switch (alt175) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:346:28: ASTERISK
                    	        {
                    	        	ASTERISK518=(IToken)Match(input,ASTERISK,FOLLOW_ASTERISK_in_arg_list3268); 
                    	        		ASTERISK518_tree = (object)adaptor.Create(ASTERISK518);
                    	        		adaptor.AddChild(root_0, ASTERISK518_tree);


                    	        }
                    	        break;
                    	    case 2 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:346:39: expr
                    	        {
                    	        	PushFollow(FOLLOW_expr_in_arg_list3272);
                    	        	expr519 = expr();
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, expr519.Tree);

                    	        }
                    	        break;

                    	}

                    	RPAREN520=(IToken)Match(input,RPAREN,FOLLOW_RPAREN_in_arg_list3275); 
                    		RPAREN520_tree = (object)adaptor.Create(RPAREN520);
                    		adaptor.AddChild(root_0, RPAREN520_tree);


                    }
                    break;
                case 2 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:346:54: LPAREN expr ( COMMA expr )* RPAREN
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	LPAREN521=(IToken)Match(input,LPAREN,FOLLOW_LPAREN_in_arg_list3279); 
                    		LPAREN521_tree = (object)adaptor.Create(LPAREN521);
                    		adaptor.AddChild(root_0, LPAREN521_tree);

                    	PushFollow(FOLLOW_expr_in_arg_list3281);
                    	expr522 = expr();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, expr522.Tree);
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:346:66: ( COMMA expr )*
                    	do 
                    	{
                    	    int alt176 = 2;
                    	    int LA176_0 = input.LA(1);

                    	    if ( (LA176_0 == COMMA) )
                    	    {
                    	        alt176 = 1;
                    	    }


                    	    switch (alt176) 
                    		{
                    			case 1 :
                    			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:346:67: COMMA expr
                    			    {
                    			    	COMMA523=(IToken)Match(input,COMMA,FOLLOW_COMMA_in_arg_list3284); 
                    			    		COMMA523_tree = (object)adaptor.Create(COMMA523);
                    			    		adaptor.AddChild(root_0, COMMA523_tree);

                    			    	PushFollow(FOLLOW_expr_in_arg_list3286);
                    			    	expr524 = expr();
                    			    	state.followingStackPointer--;

                    			    	adaptor.AddChild(root_0, expr524.Tree);

                    			    }
                    			    break;

                    			default:
                    			    goto loop176;
                    	    }
                    	} while (true);

                    	loop176:
                    		;	// Stops C# compiler whining that label 'loop176' has no statements

                    	RPAREN525=(IToken)Match(input,RPAREN,FOLLOW_RPAREN_in_arg_list3291); 
                    		RPAREN525_tree = (object)adaptor.Create(RPAREN525);
                    		adaptor.AddChild(root_0, RPAREN525_tree);


                    }
                    break;
                case 3 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:346:90: LPAREN RPAREN
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	LPAREN526=(IToken)Match(input,LPAREN,FOLLOW_LPAREN_in_arg_list3295); 
                    		LPAREN526_tree = (object)adaptor.Create(LPAREN526);
                    		adaptor.AddChild(root_0, LPAREN526_tree);

                    	RPAREN527=(IToken)Match(input,RPAREN,FOLLOW_RPAREN_in_arg_list3297); 
                    		RPAREN527_tree = (object)adaptor.Create(RPAREN527);
                    		adaptor.AddChild(root_0, RPAREN527_tree);


                    }
                    break;

            }
            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "arg_list"

    public class func_call_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "func_call"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:347:1: func_call : ( id | sysname ) ( DOT id )? arg_list ;
    public MySQLParser.func_call_return func_call() // throws RecognitionException [1]
    {   
        MySQLParser.func_call_return retval = new MySQLParser.func_call_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken DOT530 = null;
        MySQLParser.id_return id528 = default(MySQLParser.id_return);

        MySQLParser.sysname_return sysname529 = default(MySQLParser.sysname_return);

        MySQLParser.id_return id531 = default(MySQLParser.id_return);

        MySQLParser.arg_list_return arg_list532 = default(MySQLParser.arg_list_return);


        object DOT530_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:347:10: ( ( id | sysname ) ( DOT id )? arg_list )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:347:12: ( id | sysname ) ( DOT id )? arg_list
            {
            	root_0 = (object)adaptor.GetNilNode();

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:347:12: ( id | sysname )
            	int alt178 = 2;
            	int LA178_0 = input.LA(1);

            	if ( ((LA178_0 >= T_IDENT && LA178_0 <= T_QUOTED_IDENT)) )
            	{
            	    alt178 = 1;
            	}
            	else if ( (LA178_0 == CURRENT_TIMESTAMP || LA178_0 == YEAR || LA178_0 == CURRENT_USER || LA178_0 == CURRENT_DATE || LA178_0 == UTC_TIMESTAMP || LA178_0 == CURRENT_TIME) )
            	{
            	    alt178 = 2;
            	}
            	else 
            	{
            	    NoViableAltException nvae_d178s0 =
            	        new NoViableAltException("", 178, 0, input);

            	    throw nvae_d178s0;
            	}
            	switch (alt178) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:347:13: id
            	        {
            	        	PushFollow(FOLLOW_id_in_func_call3304);
            	        	id528 = id();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, id528.Tree);

            	        }
            	        break;
            	    case 2 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:347:16: sysname
            	        {
            	        	PushFollow(FOLLOW_sysname_in_func_call3306);
            	        	sysname529 = sysname();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, sysname529.Tree);

            	        }
            	        break;

            	}

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:347:25: ( DOT id )?
            	int alt179 = 2;
            	int LA179_0 = input.LA(1);

            	if ( (LA179_0 == DOT) )
            	{
            	    alt179 = 1;
            	}
            	switch (alt179) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:347:26: DOT id
            	        {
            	        	DOT530=(IToken)Match(input,DOT,FOLLOW_DOT_in_func_call3310); 
            	        		DOT530_tree = (object)adaptor.Create(DOT530);
            	        		adaptor.AddChild(root_0, DOT530_tree);

            	        	PushFollow(FOLLOW_id_in_func_call3312);
            	        	id531 = id();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, id531.Tree);

            	        }
            	        break;

            	}

            	PushFollow(FOLLOW_arg_list_in_func_call3316);
            	arg_list532 = arg_list();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, arg_list532.Tree);

            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "func_call"

    public class expr_elem_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "expr_elem"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:348:1: expr_elem : ( id ( DOT id )? | func_call | value | LPAREN expr RPAREN | LPAREN select_statement RPAREN | INTERVAL T_INTEGER ( id | sysname ) );
    public MySQLParser.expr_elem_return expr_elem() // throws RecognitionException [1]
    {   
        MySQLParser.expr_elem_return retval = new MySQLParser.expr_elem_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken DOT534 = null;
        IToken LPAREN538 = null;
        IToken RPAREN540 = null;
        IToken LPAREN541 = null;
        IToken RPAREN543 = null;
        IToken INTERVAL544 = null;
        IToken T_INTEGER545 = null;
        MySQLParser.id_return id533 = default(MySQLParser.id_return);

        MySQLParser.id_return id535 = default(MySQLParser.id_return);

        MySQLParser.func_call_return func_call536 = default(MySQLParser.func_call_return);

        MySQLParser.value_return value537 = default(MySQLParser.value_return);

        MySQLParser.expr_return expr539 = default(MySQLParser.expr_return);

        MySQLParser.select_statement_return select_statement542 = default(MySQLParser.select_statement_return);

        MySQLParser.id_return id546 = default(MySQLParser.id_return);

        MySQLParser.sysname_return sysname547 = default(MySQLParser.sysname_return);


        object DOT534_tree=null;
        object LPAREN538_tree=null;
        object RPAREN540_tree=null;
        object LPAREN541_tree=null;
        object RPAREN543_tree=null;
        object INTERVAL544_tree=null;
        object T_INTEGER545_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:348:10: ( id ( DOT id )? | func_call | value | LPAREN expr RPAREN | LPAREN select_statement RPAREN | INTERVAL T_INTEGER ( id | sysname ) )
            int alt182 = 6;
            alt182 = dfa182.Predict(input);
            switch (alt182) 
            {
                case 1 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:349:5: id ( DOT id )?
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_id_in_expr_elem3326);
                    	id533 = id();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, id533.Tree);
                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:349:8: ( DOT id )?
                    	int alt180 = 2;
                    	int LA180_0 = input.LA(1);

                    	if ( (LA180_0 == DOT) )
                    	{
                    	    alt180 = 1;
                    	}
                    	switch (alt180) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:349:9: DOT id
                    	        {
                    	        	DOT534=(IToken)Match(input,DOT,FOLLOW_DOT_in_expr_elem3329); 
                    	        		DOT534_tree = (object)adaptor.Create(DOT534);
                    	        		adaptor.AddChild(root_0, DOT534_tree);

                    	        	PushFollow(FOLLOW_id_in_expr_elem3331);
                    	        	id535 = id();
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, id535.Tree);

                    	        }
                    	        break;

                    	}


                    }
                    break;
                case 2 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:350:7: func_call
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_func_call_in_expr_elem3341);
                    	func_call536 = func_call();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, func_call536.Tree);

                    }
                    break;
                case 3 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:351:7: value
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_value_in_expr_elem3349);
                    	value537 = value();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, value537.Tree);

                    }
                    break;
                case 4 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:352:7: LPAREN expr RPAREN
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	LPAREN538=(IToken)Match(input,LPAREN,FOLLOW_LPAREN_in_expr_elem3357); 
                    		LPAREN538_tree = (object)adaptor.Create(LPAREN538);
                    		adaptor.AddChild(root_0, LPAREN538_tree);

                    	PushFollow(FOLLOW_expr_in_expr_elem3359);
                    	expr539 = expr();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, expr539.Tree);
                    	RPAREN540=(IToken)Match(input,RPAREN,FOLLOW_RPAREN_in_expr_elem3361); 
                    		RPAREN540_tree = (object)adaptor.Create(RPAREN540);
                    		adaptor.AddChild(root_0, RPAREN540_tree);


                    }
                    break;
                case 5 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:353:7: LPAREN select_statement RPAREN
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	LPAREN541=(IToken)Match(input,LPAREN,FOLLOW_LPAREN_in_expr_elem3369); 
                    		LPAREN541_tree = (object)adaptor.Create(LPAREN541);
                    		adaptor.AddChild(root_0, LPAREN541_tree);

                    	PushFollow(FOLLOW_select_statement_in_expr_elem3371);
                    	select_statement542 = select_statement();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, select_statement542.Tree);
                    	RPAREN543=(IToken)Match(input,RPAREN,FOLLOW_RPAREN_in_expr_elem3373); 
                    		RPAREN543_tree = (object)adaptor.Create(RPAREN543);
                    		adaptor.AddChild(root_0, RPAREN543_tree);


                    }
                    break;
                case 6 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:354:7: INTERVAL T_INTEGER ( id | sysname )
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	INTERVAL544=(IToken)Match(input,INTERVAL,FOLLOW_INTERVAL_in_expr_elem3381); 
                    		INTERVAL544_tree = (object)adaptor.Create(INTERVAL544);
                    		adaptor.AddChild(root_0, INTERVAL544_tree);

                    	T_INTEGER545=(IToken)Match(input,T_INTEGER,FOLLOW_T_INTEGER_in_expr_elem3383); 
                    		T_INTEGER545_tree = (object)adaptor.Create(T_INTEGER545);
                    		adaptor.AddChild(root_0, T_INTEGER545_tree);

                    	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:354:26: ( id | sysname )
                    	int alt181 = 2;
                    	int LA181_0 = input.LA(1);

                    	if ( ((LA181_0 >= T_IDENT && LA181_0 <= T_QUOTED_IDENT)) )
                    	{
                    	    alt181 = 1;
                    	}
                    	else if ( (LA181_0 == CURRENT_TIMESTAMP || LA181_0 == YEAR || LA181_0 == CURRENT_USER || LA181_0 == CURRENT_DATE || LA181_0 == UTC_TIMESTAMP || LA181_0 == CURRENT_TIME) )
                    	{
                    	    alt181 = 2;
                    	}
                    	else 
                    	{
                    	    NoViableAltException nvae_d181s0 =
                    	        new NoViableAltException("", 181, 0, input);

                    	    throw nvae_d181s0;
                    	}
                    	switch (alt181) 
                    	{
                    	    case 1 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:354:27: id
                    	        {
                    	        	PushFollow(FOLLOW_id_in_expr_elem3386);
                    	        	id546 = id();
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, id546.Tree);

                    	        }
                    	        break;
                    	    case 2 :
                    	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:354:32: sysname
                    	        {
                    	        	PushFollow(FOLLOW_sysname_in_expr_elem3390);
                    	        	sysname547 = sysname();
                    	        	state.followingStackPointer--;

                    	        	adaptor.AddChild(root_0, sysname547.Tree);

                    	        }
                    	        break;

                    	}


                    }
                    break;

            }
            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "expr_elem"

    public class exclamation_expr_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "exclamation_expr"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:356:1: exclamation_expr : ( EXCLAMATION )? expr_elem ;
    public MySQLParser.exclamation_expr_return exclamation_expr() // throws RecognitionException [1]
    {   
        MySQLParser.exclamation_expr_return retval = new MySQLParser.exclamation_expr_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken EXCLAMATION548 = null;
        MySQLParser.expr_elem_return expr_elem549 = default(MySQLParser.expr_elem_return);


        object EXCLAMATION548_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:356:17: ( ( EXCLAMATION )? expr_elem )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:356:19: ( EXCLAMATION )? expr_elem
            {
            	root_0 = (object)adaptor.GetNilNode();

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:356:19: ( EXCLAMATION )?
            	int alt183 = 2;
            	int LA183_0 = input.LA(1);

            	if ( (LA183_0 == EXCLAMATION) )
            	{
            	    alt183 = 1;
            	}
            	switch (alt183) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:356:19: EXCLAMATION
            	        {
            	        	EXCLAMATION548=(IToken)Match(input,EXCLAMATION,FOLLOW_EXCLAMATION_in_exclamation_expr3398); 
            	        		EXCLAMATION548_tree = (object)adaptor.Create(EXCLAMATION548);
            	        		adaptor.AddChild(root_0, EXCLAMATION548_tree);


            	        }
            	        break;

            	}

            	PushFollow(FOLLOW_expr_elem_in_exclamation_expr3401);
            	expr_elem549 = expr_elem();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, expr_elem549.Tree);

            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "exclamation_expr"

    public class unary_expr_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "unary_expr"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:357:1: unary_expr : ( MINUS | TILDA )? exclamation_expr ;
    public MySQLParser.unary_expr_return unary_expr() // throws RecognitionException [1]
    {   
        MySQLParser.unary_expr_return retval = new MySQLParser.unary_expr_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken set550 = null;
        MySQLParser.exclamation_expr_return exclamation_expr551 = default(MySQLParser.exclamation_expr_return);


        object set550_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:357:11: ( ( MINUS | TILDA )? exclamation_expr )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:357:13: ( MINUS | TILDA )? exclamation_expr
            {
            	root_0 = (object)adaptor.GetNilNode();

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:357:13: ( MINUS | TILDA )?
            	int alt184 = 2;
            	int LA184_0 = input.LA(1);

            	if ( ((LA184_0 >= MINUS && LA184_0 <= TILDA)) )
            	{
            	    alt184 = 1;
            	}
            	switch (alt184) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:
            	        {
            	        	set550 = (IToken)input.LT(1);
            	        	if ( (input.LA(1) >= MINUS && input.LA(1) <= TILDA) ) 
            	        	{
            	        	    input.Consume();
            	        	    adaptor.AddChild(root_0, (object)adaptor.Create(set550));
            	        	    state.errorRecovery = false;
            	        	}
            	        	else 
            	        	{
            	        	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	        	    throw mse;
            	        	}


            	        }
            	        break;

            	}

            	PushFollow(FOLLOW_exclamation_expr_in_unary_expr3414);
            	exclamation_expr551 = exclamation_expr();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, exclamation_expr551.Tree);

            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "unary_expr"

    public class pow_expr_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "pow_expr"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:358:1: pow_expr : unary_expr ( ARROW_UP unary_expr )? ;
    public MySQLParser.pow_expr_return pow_expr() // throws RecognitionException [1]
    {   
        MySQLParser.pow_expr_return retval = new MySQLParser.pow_expr_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken ARROW_UP553 = null;
        MySQLParser.unary_expr_return unary_expr552 = default(MySQLParser.unary_expr_return);

        MySQLParser.unary_expr_return unary_expr554 = default(MySQLParser.unary_expr_return);


        object ARROW_UP553_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:358:9: ( unary_expr ( ARROW_UP unary_expr )? )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:358:11: unary_expr ( ARROW_UP unary_expr )?
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_unary_expr_in_pow_expr3420);
            	unary_expr552 = unary_expr();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, unary_expr552.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:358:22: ( ARROW_UP unary_expr )?
            	int alt185 = 2;
            	int LA185_0 = input.LA(1);

            	if ( (LA185_0 == ARROW_UP) )
            	{
            	    alt185 = 1;
            	}
            	switch (alt185) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:358:23: ARROW_UP unary_expr
            	        {
            	        	ARROW_UP553=(IToken)Match(input,ARROW_UP,FOLLOW_ARROW_UP_in_pow_expr3423); 
            	        		ARROW_UP553_tree = (object)adaptor.Create(ARROW_UP553);
            	        		adaptor.AddChild(root_0, ARROW_UP553_tree);

            	        	PushFollow(FOLLOW_unary_expr_in_pow_expr3425);
            	        	unary_expr554 = unary_expr();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, unary_expr554.Tree);

            	        }
            	        break;

            	}


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "pow_expr"

    public class mul_expr_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "mul_expr"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:359:1: mul_expr : pow_expr ( ( ASTERISK | SLASH | PERCENT | DIV | MOD ) pow_expr )* ;
    public MySQLParser.mul_expr_return mul_expr() // throws RecognitionException [1]
    {   
        MySQLParser.mul_expr_return retval = new MySQLParser.mul_expr_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken set556 = null;
        MySQLParser.pow_expr_return pow_expr555 = default(MySQLParser.pow_expr_return);

        MySQLParser.pow_expr_return pow_expr557 = default(MySQLParser.pow_expr_return);


        object set556_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:359:9: ( pow_expr ( ( ASTERISK | SLASH | PERCENT | DIV | MOD ) pow_expr )* )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:359:11: pow_expr ( ( ASTERISK | SLASH | PERCENT | DIV | MOD ) pow_expr )*
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_pow_expr_in_mul_expr3433);
            	pow_expr555 = pow_expr();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, pow_expr555.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:359:20: ( ( ASTERISK | SLASH | PERCENT | DIV | MOD ) pow_expr )*
            	do 
            	{
            	    int alt186 = 2;
            	    int LA186_0 = input.LA(1);

            	    if ( (LA186_0 == ASTERISK || (LA186_0 >= SLASH && LA186_0 <= MOD)) )
            	    {
            	        alt186 = 1;
            	    }


            	    switch (alt186) 
            		{
            			case 1 :
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:359:21: ( ASTERISK | SLASH | PERCENT | DIV | MOD ) pow_expr
            			    {
            			    	set556 = (IToken)input.LT(1);
            			    	if ( input.LA(1) == ASTERISK || (input.LA(1) >= SLASH && input.LA(1) <= MOD) ) 
            			    	{
            			    	    input.Consume();
            			    	    adaptor.AddChild(root_0, (object)adaptor.Create(set556));
            			    	    state.errorRecovery = false;
            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    throw mse;
            			    	}

            			    	PushFollow(FOLLOW_pow_expr_in_mul_expr3456);
            			    	pow_expr557 = pow_expr();
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, pow_expr557.Tree);

            			    }
            			    break;

            			default:
            			    goto loop186;
            	    }
            	} while (true);

            	loop186:
            		;	// Stops C# compiler whining that label 'loop186' has no statements


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "mul_expr"

    public class add_expr_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "add_expr"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:360:1: add_expr : mul_expr ( ( PLUS | MINUS ) mul_expr )* ;
    public MySQLParser.add_expr_return add_expr() // throws RecognitionException [1]
    {   
        MySQLParser.add_expr_return retval = new MySQLParser.add_expr_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken set559 = null;
        MySQLParser.mul_expr_return mul_expr558 = default(MySQLParser.mul_expr_return);

        MySQLParser.mul_expr_return mul_expr560 = default(MySQLParser.mul_expr_return);


        object set559_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:360:9: ( mul_expr ( ( PLUS | MINUS ) mul_expr )* )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:360:11: mul_expr ( ( PLUS | MINUS ) mul_expr )*
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_mul_expr_in_add_expr3464);
            	mul_expr558 = mul_expr();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, mul_expr558.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:360:20: ( ( PLUS | MINUS ) mul_expr )*
            	do 
            	{
            	    int alt187 = 2;
            	    int LA187_0 = input.LA(1);

            	    if ( (LA187_0 == MINUS || LA187_0 == PLUS) )
            	    {
            	        alt187 = 1;
            	    }


            	    switch (alt187) 
            		{
            			case 1 :
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:360:22: ( PLUS | MINUS ) mul_expr
            			    {
            			    	set559 = (IToken)input.LT(1);
            			    	if ( input.LA(1) == MINUS || input.LA(1) == PLUS ) 
            			    	{
            			    	    input.Consume();
            			    	    adaptor.AddChild(root_0, (object)adaptor.Create(set559));
            			    	    state.errorRecovery = false;
            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    throw mse;
            			    	}

            			    	PushFollow(FOLLOW_mul_expr_in_add_expr3476);
            			    	mul_expr560 = mul_expr();
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, mul_expr560.Tree);

            			    }
            			    break;

            			default:
            			    goto loop187;
            	    }
            	} while (true);

            	loop187:
            		;	// Stops C# compiler whining that label 'loop187' has no statements


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "add_expr"

    public class shift_expr_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "shift_expr"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:361:1: shift_expr : add_expr ( ( SHIFT_LEFT | SHIFT_RIGHT ) add_expr )* ;
    public MySQLParser.shift_expr_return shift_expr() // throws RecognitionException [1]
    {   
        MySQLParser.shift_expr_return retval = new MySQLParser.shift_expr_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken set562 = null;
        MySQLParser.add_expr_return add_expr561 = default(MySQLParser.add_expr_return);

        MySQLParser.add_expr_return add_expr563 = default(MySQLParser.add_expr_return);


        object set562_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:361:11: ( add_expr ( ( SHIFT_LEFT | SHIFT_RIGHT ) add_expr )* )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:361:13: add_expr ( ( SHIFT_LEFT | SHIFT_RIGHT ) add_expr )*
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_add_expr_in_shift_expr3484);
            	add_expr561 = add_expr();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, add_expr561.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:361:22: ( ( SHIFT_LEFT | SHIFT_RIGHT ) add_expr )*
            	do 
            	{
            	    int alt188 = 2;
            	    int LA188_0 = input.LA(1);

            	    if ( ((LA188_0 >= SHIFT_LEFT && LA188_0 <= SHIFT_RIGHT)) )
            	    {
            	        alt188 = 1;
            	    }


            	    switch (alt188) 
            		{
            			case 1 :
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:361:23: ( SHIFT_LEFT | SHIFT_RIGHT ) add_expr
            			    {
            			    	set562 = (IToken)input.LT(1);
            			    	if ( (input.LA(1) >= SHIFT_LEFT && input.LA(1) <= SHIFT_RIGHT) ) 
            			    	{
            			    	    input.Consume();
            			    	    adaptor.AddChild(root_0, (object)adaptor.Create(set562));
            			    	    state.errorRecovery = false;
            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    throw mse;
            			    	}

            			    	PushFollow(FOLLOW_add_expr_in_shift_expr3495);
            			    	add_expr563 = add_expr();
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, add_expr563.Tree);

            			    }
            			    break;

            			default:
            			    goto loop188;
            	    }
            	} while (true);

            	loop188:
            		;	// Stops C# compiler whining that label 'loop188' has no statements


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "shift_expr"

    public class bitand_expr_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "bitand_expr"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:362:1: bitand_expr : shift_expr ( AMPERSAND shift_expr )* ;
    public MySQLParser.bitand_expr_return bitand_expr() // throws RecognitionException [1]
    {   
        MySQLParser.bitand_expr_return retval = new MySQLParser.bitand_expr_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken AMPERSAND565 = null;
        MySQLParser.shift_expr_return shift_expr564 = default(MySQLParser.shift_expr_return);

        MySQLParser.shift_expr_return shift_expr566 = default(MySQLParser.shift_expr_return);


        object AMPERSAND565_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:362:12: ( shift_expr ( AMPERSAND shift_expr )* )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:362:14: shift_expr ( AMPERSAND shift_expr )*
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_shift_expr_in_bitand_expr3503);
            	shift_expr564 = shift_expr();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, shift_expr564.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:362:25: ( AMPERSAND shift_expr )*
            	do 
            	{
            	    int alt189 = 2;
            	    int LA189_0 = input.LA(1);

            	    if ( (LA189_0 == AMPERSAND) )
            	    {
            	        alt189 = 1;
            	    }


            	    switch (alt189) 
            		{
            			case 1 :
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:362:26: AMPERSAND shift_expr
            			    {
            			    	AMPERSAND565=(IToken)Match(input,AMPERSAND,FOLLOW_AMPERSAND_in_bitand_expr3506); 
            			    		AMPERSAND565_tree = (object)adaptor.Create(AMPERSAND565);
            			    		adaptor.AddChild(root_0, AMPERSAND565_tree);

            			    	PushFollow(FOLLOW_shift_expr_in_bitand_expr3508);
            			    	shift_expr566 = shift_expr();
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, shift_expr566.Tree);

            			    }
            			    break;

            			default:
            			    goto loop189;
            	    }
            	} while (true);

            	loop189:
            		;	// Stops C# compiler whining that label 'loop189' has no statements


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "bitand_expr"

    public class bitor_expr_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "bitor_expr"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:363:1: bitor_expr : bitand_expr ( PIPE bitand_expr )* ;
    public MySQLParser.bitor_expr_return bitor_expr() // throws RecognitionException [1]
    {   
        MySQLParser.bitor_expr_return retval = new MySQLParser.bitor_expr_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken PIPE568 = null;
        MySQLParser.bitand_expr_return bitand_expr567 = default(MySQLParser.bitand_expr_return);

        MySQLParser.bitand_expr_return bitand_expr569 = default(MySQLParser.bitand_expr_return);


        object PIPE568_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:363:11: ( bitand_expr ( PIPE bitand_expr )* )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:363:13: bitand_expr ( PIPE bitand_expr )*
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_bitand_expr_in_bitor_expr3516);
            	bitand_expr567 = bitand_expr();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, bitand_expr567.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:363:25: ( PIPE bitand_expr )*
            	do 
            	{
            	    int alt190 = 2;
            	    int LA190_0 = input.LA(1);

            	    if ( (LA190_0 == PIPE) )
            	    {
            	        alt190 = 1;
            	    }


            	    switch (alt190) 
            		{
            			case 1 :
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:363:26: PIPE bitand_expr
            			    {
            			    	PIPE568=(IToken)Match(input,PIPE,FOLLOW_PIPE_in_bitor_expr3519); 
            			    		PIPE568_tree = (object)adaptor.Create(PIPE568);
            			    		adaptor.AddChild(root_0, PIPE568_tree);

            			    	PushFollow(FOLLOW_bitand_expr_in_bitor_expr3521);
            			    	bitand_expr569 = bitand_expr();
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, bitand_expr569.Tree);

            			    }
            			    break;

            			default:
            			    goto loop190;
            	    }
            	} while (true);

            	loop190:
            		;	// Stops C# compiler whining that label 'loop190' has no statements


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "bitor_expr"

    public class rel_expr_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "rel_expr"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:364:1: rel_expr : bitor_expr ( ( EQUALS | SAFEEQUALS | GREATER_OR_EQ | GREATER | LESS_OR_EQ | LESS | NOT_EQUALS2 | NOT_EQUALS | IS | LIKE | REGEXP | IN ) bitor_expr )* ;
    public MySQLParser.rel_expr_return rel_expr() // throws RecognitionException [1]
    {   
        MySQLParser.rel_expr_return retval = new MySQLParser.rel_expr_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken set571 = null;
        MySQLParser.bitor_expr_return bitor_expr570 = default(MySQLParser.bitor_expr_return);

        MySQLParser.bitor_expr_return bitor_expr572 = default(MySQLParser.bitor_expr_return);


        object set571_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:364:9: ( bitor_expr ( ( EQUALS | SAFEEQUALS | GREATER_OR_EQ | GREATER | LESS_OR_EQ | LESS | NOT_EQUALS2 | NOT_EQUALS | IS | LIKE | REGEXP | IN ) bitor_expr )* )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:364:11: bitor_expr ( ( EQUALS | SAFEEQUALS | GREATER_OR_EQ | GREATER | LESS_OR_EQ | LESS | NOT_EQUALS2 | NOT_EQUALS | IS | LIKE | REGEXP | IN ) bitor_expr )*
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_bitor_expr_in_rel_expr3529);
            	bitor_expr570 = bitor_expr();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, bitor_expr570.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:364:22: ( ( EQUALS | SAFEEQUALS | GREATER_OR_EQ | GREATER | LESS_OR_EQ | LESS | NOT_EQUALS2 | NOT_EQUALS | IS | LIKE | REGEXP | IN ) bitor_expr )*
            	do 
            	{
            	    int alt191 = 2;
            	    int LA191_0 = input.LA(1);

            	    if ( (LA191_0 == EQUALS || LA191_0 == IN || (LA191_0 >= SAFEEQUALS && LA191_0 <= REGEXP)) )
            	    {
            	        alt191 = 1;
            	    }


            	    switch (alt191) 
            		{
            			case 1 :
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:364:24: ( EQUALS | SAFEEQUALS | GREATER_OR_EQ | GREATER | LESS_OR_EQ | LESS | NOT_EQUALS2 | NOT_EQUALS | IS | LIKE | REGEXP | IN ) bitor_expr
            			    {
            			    	set571 = (IToken)input.LT(1);
            			    	if ( input.LA(1) == EQUALS || input.LA(1) == IN || (input.LA(1) >= SAFEEQUALS && input.LA(1) <= REGEXP) ) 
            			    	{
            			    	    input.Consume();
            			    	    adaptor.AddChild(root_0, (object)adaptor.Create(set571));
            			    	    state.errorRecovery = false;
            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    throw mse;
            			    	}

            			    	PushFollow(FOLLOW_bitor_expr_in_rel_expr3585);
            			    	bitor_expr572 = bitor_expr();
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, bitor_expr572.Tree);

            			    }
            			    break;

            			default:
            			    goto loop191;
            	    }
            	} while (true);

            	loop191:
            		;	// Stops C# compiler whining that label 'loop191' has no statements


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "rel_expr"

    public class between_expr_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "between_expr"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:366:1: between_expr : rel_expr ( BETWEEN rel_expr AND rel_expr )? ;
    public MySQLParser.between_expr_return between_expr() // throws RecognitionException [1]
    {   
        MySQLParser.between_expr_return retval = new MySQLParser.between_expr_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken BETWEEN574 = null;
        IToken AND576 = null;
        MySQLParser.rel_expr_return rel_expr573 = default(MySQLParser.rel_expr_return);

        MySQLParser.rel_expr_return rel_expr575 = default(MySQLParser.rel_expr_return);

        MySQLParser.rel_expr_return rel_expr577 = default(MySQLParser.rel_expr_return);


        object BETWEEN574_tree=null;
        object AND576_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:366:13: ( rel_expr ( BETWEEN rel_expr AND rel_expr )? )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:366:15: rel_expr ( BETWEEN rel_expr AND rel_expr )?
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_rel_expr_in_between_expr3593);
            	rel_expr573 = rel_expr();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, rel_expr573.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:366:24: ( BETWEEN rel_expr AND rel_expr )?
            	int alt192 = 2;
            	int LA192_0 = input.LA(1);

            	if ( (LA192_0 == BETWEEN) )
            	{
            	    alt192 = 1;
            	}
            	switch (alt192) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:366:25: BETWEEN rel_expr AND rel_expr
            	        {
            	        	BETWEEN574=(IToken)Match(input,BETWEEN,FOLLOW_BETWEEN_in_between_expr3596); 
            	        		BETWEEN574_tree = (object)adaptor.Create(BETWEEN574);
            	        		adaptor.AddChild(root_0, BETWEEN574_tree);

            	        	PushFollow(FOLLOW_rel_expr_in_between_expr3598);
            	        	rel_expr575 = rel_expr();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, rel_expr575.Tree);
            	        	AND576=(IToken)Match(input,AND,FOLLOW_AND_in_between_expr3600); 
            	        		AND576_tree = (object)adaptor.Create(AND576);
            	        		adaptor.AddChild(root_0, AND576_tree);

            	        	PushFollow(FOLLOW_rel_expr_in_between_expr3602);
            	        	rel_expr577 = rel_expr();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, rel_expr577.Tree);

            	        }
            	        break;

            	}


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "between_expr"

    public class casegroup_expr_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "casegroup_expr"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:367:1: casegroup_expr : ( between_expr | case_expr );
    public MySQLParser.casegroup_expr_return casegroup_expr() // throws RecognitionException [1]
    {   
        MySQLParser.casegroup_expr_return retval = new MySQLParser.casegroup_expr_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        MySQLParser.between_expr_return between_expr578 = default(MySQLParser.between_expr_return);

        MySQLParser.case_expr_return case_expr579 = default(MySQLParser.case_expr_return);



        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:367:15: ( between_expr | case_expr )
            int alt193 = 2;
            int LA193_0 = input.LA(1);

            if ( (LA193_0 == LPAREN || LA193_0 == T_INTEGER || (LA193_0 >= T_IDENT && LA193_0 <= T_QUOTED_IDENT) || LA193_0 == NULL || LA193_0 == CURRENT_TIMESTAMP || LA193_0 == T_STRING || LA193_0 == YEAR || LA193_0 == T_FLOAT || LA193_0 == T_BLOB || LA193_0 == CURRENT_USER || LA193_0 == CURRENT_DATE || (LA193_0 >= UTC_TIMESTAMP && LA193_0 <= TILDA) || LA193_0 == CURRENT_TIME) )
            {
                alt193 = 1;
            }
            else if ( (LA193_0 == CASE) )
            {
                alt193 = 2;
            }
            else 
            {
                NoViableAltException nvae_d193s0 =
                    new NoViableAltException("", 193, 0, input);

                throw nvae_d193s0;
            }
            switch (alt193) 
            {
                case 1 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:367:17: between_expr
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_between_expr_in_casegroup_expr3610);
                    	between_expr578 = between_expr();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, between_expr578.Tree);

                    }
                    break;
                case 2 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:367:32: case_expr
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_case_expr_in_casegroup_expr3614);
                    	case_expr579 = case_expr();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, case_expr579.Tree);

                    }
                    break;

            }
            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "casegroup_expr"

    public class not_expr_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "not_expr"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:368:1: not_expr : ( NOT )? casegroup_expr ;
    public MySQLParser.not_expr_return not_expr() // throws RecognitionException [1]
    {   
        MySQLParser.not_expr_return retval = new MySQLParser.not_expr_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken NOT580 = null;
        MySQLParser.casegroup_expr_return casegroup_expr581 = default(MySQLParser.casegroup_expr_return);


        object NOT580_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:368:10: ( ( NOT )? casegroup_expr )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:368:12: ( NOT )? casegroup_expr
            {
            	root_0 = (object)adaptor.GetNilNode();

            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:368:12: ( NOT )?
            	int alt194 = 2;
            	int LA194_0 = input.LA(1);

            	if ( (LA194_0 == NOT) )
            	{
            	    alt194 = 1;
            	}
            	switch (alt194) 
            	{
            	    case 1 :
            	        // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:368:12: NOT
            	        {
            	        	NOT580=(IToken)Match(input,NOT,FOLLOW_NOT_in_not_expr3621); 
            	        		NOT580_tree = (object)adaptor.Create(NOT580);
            	        		adaptor.AddChild(root_0, NOT580_tree);


            	        }
            	        break;

            	}

            	PushFollow(FOLLOW_casegroup_expr_in_not_expr3624);
            	casegroup_expr581 = casegroup_expr();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, casegroup_expr581.Tree);

            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "not_expr"

    public class and_expr_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "and_expr"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:369:1: and_expr : not_expr ( ( AND | DOUBLE_AMPERSAND ) not_expr )* ;
    public MySQLParser.and_expr_return and_expr() // throws RecognitionException [1]
    {   
        MySQLParser.and_expr_return retval = new MySQLParser.and_expr_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken set583 = null;
        MySQLParser.not_expr_return not_expr582 = default(MySQLParser.not_expr_return);

        MySQLParser.not_expr_return not_expr584 = default(MySQLParser.not_expr_return);


        object set583_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:369:9: ( not_expr ( ( AND | DOUBLE_AMPERSAND ) not_expr )* )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:369:11: not_expr ( ( AND | DOUBLE_AMPERSAND ) not_expr )*
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_not_expr_in_and_expr3630);
            	not_expr582 = not_expr();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, not_expr582.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:369:20: ( ( AND | DOUBLE_AMPERSAND ) not_expr )*
            	do 
            	{
            	    int alt195 = 2;
            	    int LA195_0 = input.LA(1);

            	    if ( ((LA195_0 >= AND && LA195_0 <= DOUBLE_AMPERSAND)) )
            	    {
            	        alt195 = 1;
            	    }


            	    switch (alt195) 
            		{
            			case 1 :
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:369:22: ( AND | DOUBLE_AMPERSAND ) not_expr
            			    {
            			    	set583 = (IToken)input.LT(1);
            			    	if ( (input.LA(1) >= AND && input.LA(1) <= DOUBLE_AMPERSAND) ) 
            			    	{
            			    	    input.Consume();
            			    	    adaptor.AddChild(root_0, (object)adaptor.Create(set583));
            			    	    state.errorRecovery = false;
            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    throw mse;
            			    	}

            			    	PushFollow(FOLLOW_not_expr_in_and_expr3642);
            			    	not_expr584 = not_expr();
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, not_expr584.Tree);

            			    }
            			    break;

            			default:
            			    goto loop195;
            	    }
            	} while (true);

            	loop195:
            		;	// Stops C# compiler whining that label 'loop195' has no statements


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "and_expr"

    public class xor_expr_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "xor_expr"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:370:1: xor_expr : and_expr ( XOR and_expr )* ;
    public MySQLParser.xor_expr_return xor_expr() // throws RecognitionException [1]
    {   
        MySQLParser.xor_expr_return retval = new MySQLParser.xor_expr_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken XOR586 = null;
        MySQLParser.and_expr_return and_expr585 = default(MySQLParser.and_expr_return);

        MySQLParser.and_expr_return and_expr587 = default(MySQLParser.and_expr_return);


        object XOR586_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:370:9: ( and_expr ( XOR and_expr )* )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:370:11: and_expr ( XOR and_expr )*
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_and_expr_in_xor_expr3650);
            	and_expr585 = and_expr();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, and_expr585.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:370:20: ( XOR and_expr )*
            	do 
            	{
            	    int alt196 = 2;
            	    int LA196_0 = input.LA(1);

            	    if ( (LA196_0 == XOR) )
            	    {
            	        alt196 = 1;
            	    }


            	    switch (alt196) 
            		{
            			case 1 :
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:370:22: XOR and_expr
            			    {
            			    	XOR586=(IToken)Match(input,XOR,FOLLOW_XOR_in_xor_expr3654); 
            			    		XOR586_tree = (object)adaptor.Create(XOR586);
            			    		adaptor.AddChild(root_0, XOR586_tree);

            			    	PushFollow(FOLLOW_and_expr_in_xor_expr3656);
            			    	and_expr587 = and_expr();
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, and_expr587.Tree);

            			    }
            			    break;

            			default:
            			    goto loop196;
            	    }
            	} while (true);

            	loop196:
            		;	// Stops C# compiler whining that label 'loop196' has no statements


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "xor_expr"

    public class or_expr_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "or_expr"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:371:1: or_expr : xor_expr ( ( OR | DOUBLE_PIPE ) xor_expr )* ;
    public MySQLParser.or_expr_return or_expr() // throws RecognitionException [1]
    {   
        MySQLParser.or_expr_return retval = new MySQLParser.or_expr_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken set589 = null;
        MySQLParser.xor_expr_return xor_expr588 = default(MySQLParser.xor_expr_return);

        MySQLParser.xor_expr_return xor_expr590 = default(MySQLParser.xor_expr_return);


        object set589_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:371:9: ( xor_expr ( ( OR | DOUBLE_PIPE ) xor_expr )* )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:371:11: xor_expr ( ( OR | DOUBLE_PIPE ) xor_expr )*
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_xor_expr_in_or_expr3665);
            	xor_expr588 = xor_expr();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, xor_expr588.Tree);
            	// ..\\Plugin.mysql\\AntlrParser\\MySQL.g:371:20: ( ( OR | DOUBLE_PIPE ) xor_expr )*
            	do 
            	{
            	    int alt197 = 2;
            	    int LA197_0 = input.LA(1);

            	    if ( (LA197_0 == OR || LA197_0 == DOUBLE_PIPE) )
            	    {
            	        alt197 = 1;
            	    }


            	    switch (alt197) 
            		{
            			case 1 :
            			    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:371:22: ( OR | DOUBLE_PIPE ) xor_expr
            			    {
            			    	set589 = (IToken)input.LT(1);
            			    	if ( input.LA(1) == OR || input.LA(1) == DOUBLE_PIPE ) 
            			    	{
            			    	    input.Consume();
            			    	    adaptor.AddChild(root_0, (object)adaptor.Create(set589));
            			    	    state.errorRecovery = false;
            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    throw mse;
            			    	}

            			    	PushFollow(FOLLOW_xor_expr_in_or_expr3677);
            			    	xor_expr590 = xor_expr();
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, xor_expr590.Tree);

            			    }
            			    break;

            			default:
            			    goto loop197;
            	    }
            	} while (true);

            	loop197:
            		;	// Stops C# compiler whining that label 'loop197' has no statements


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "or_expr"

    public class condition_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "condition"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:372:1: condition : or_expr ;
    public MySQLParser.condition_return condition() // throws RecognitionException [1]
    {   
        MySQLParser.condition_return retval = new MySQLParser.condition_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        MySQLParser.or_expr_return or_expr591 = default(MySQLParser.or_expr_return);



        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:372:11: ( or_expr )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:372:13: or_expr
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_or_expr_in_condition3687);
            	or_expr591 = or_expr();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, or_expr591.Tree);

            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "condition"

    public class operator_no_dot_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "operator_no_dot"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:410:1: operator_no_dot : ( EQUALS | SAFEEQUALS | EQUALS2 | NOT_EQUALS | NOT_EQUALS2 | LESS | LESS_OR_EQ | GREATER | GREATER_OR_EQ | SHIFT_LEFT | SHIFT_RIGHT | AMPERSAND | DOUBLE_AMPERSAND | PIPE | DOUBLE_PIPE | PLUS | MINUS | TILDA | ASTERISK | SLASH | PERCENT | SEMI | COMMA | LPAREN | RPAREN | QUESTION | EXCLAMATION | COLON | AT | DOLLAR | ARROW_UP );
    public MySQLParser.operator_no_dot_return operator_no_dot() // throws RecognitionException [1]
    {   
        MySQLParser.operator_no_dot_return retval = new MySQLParser.operator_no_dot_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken set592 = null;

        object set592_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:410:17: ( EQUALS | SAFEEQUALS | EQUALS2 | NOT_EQUALS | NOT_EQUALS2 | LESS | LESS_OR_EQ | GREATER | GREATER_OR_EQ | SHIFT_LEFT | SHIFT_RIGHT | AMPERSAND | DOUBLE_AMPERSAND | PIPE | DOUBLE_PIPE | PLUS | MINUS | TILDA | ASTERISK | SLASH | PERCENT | SEMI | COMMA | LPAREN | RPAREN | QUESTION | EXCLAMATION | COLON | AT | DOLLAR | ARROW_UP )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:
            {
            	root_0 = (object)adaptor.GetNilNode();

            	set592 = (IToken)input.LT(1);
            	if ( (input.LA(1) >= LPAREN && input.LA(1) <= RPAREN) || input.LA(1) == EQUALS || input.LA(1) == ASTERISK || input.LA(1) == AT || (input.LA(1) >= EXCLAMATION && input.LA(1) <= PERCENT) || (input.LA(1) >= PLUS && input.LA(1) <= NOT_EQUALS) || input.LA(1) == DOUBLE_AMPERSAND || (input.LA(1) >= DOUBLE_PIPE && input.LA(1) <= DOLLAR) ) 
            	{
            	    input.Consume();
            	    adaptor.AddChild(root_0, (object)adaptor.Create(set592));
            	    state.errorRecovery = false;
            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    throw mse;
            	}


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "operator_no_dot"

    public class any_operator_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "any_operator"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:417:1: any_operator : ( DOT | operator_no_dot );
    public MySQLParser.any_operator_return any_operator() // throws RecognitionException [1]
    {   
        MySQLParser.any_operator_return retval = new MySQLParser.any_operator_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken DOT593 = null;
        MySQLParser.operator_no_dot_return operator_no_dot594 = default(MySQLParser.operator_no_dot_return);


        object DOT593_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:417:13: ( DOT | operator_no_dot )
            int alt198 = 2;
            int LA198_0 = input.LA(1);

            if ( (LA198_0 == DOT) )
            {
                alt198 = 1;
            }
            else if ( ((LA198_0 >= LPAREN && LA198_0 <= RPAREN) || LA198_0 == EQUALS || LA198_0 == ASTERISK || LA198_0 == AT || (LA198_0 >= EXCLAMATION && LA198_0 <= PERCENT) || (LA198_0 >= PLUS && LA198_0 <= NOT_EQUALS) || LA198_0 == DOUBLE_AMPERSAND || (LA198_0 >= DOUBLE_PIPE && LA198_0 <= DOLLAR)) )
            {
                alt198 = 2;
            }
            else 
            {
                NoViableAltException nvae_d198s0 =
                    new NoViableAltException("", 198, 0, input);

                throw nvae_d198s0;
            }
            switch (alt198) 
            {
                case 1 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:417:15: DOT
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	DOT593=(IToken)Match(input,DOT,FOLLOW_DOT_in_any_operator4220); 
                    		DOT593_tree = (object)adaptor.Create(DOT593);
                    		adaptor.AddChild(root_0, DOT593_tree);


                    }
                    break;
                case 2 :
                    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:417:21: operator_no_dot
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_operator_no_dot_in_any_operator4224);
                    	operator_no_dot594 = operator_no_dot();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, operator_no_dot594.Tree);

                    }
                    break;

            }
            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "any_operator"

    public class keyword_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "keyword"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:719:1: keyword : ( ABORT | ADD | AFTER | ALL | ALTER | ANALYZE | AND | AS | ASC | ATTACH | AUTOINCREMENT | BEFORE | BEGIN | BETWEEN | BY | CASCADE | CASE | CAST | CHECK | COLLATE | COLUMN | COMMIT | CONFLICT | CONSTRAINT | CREATE | CROSS | DATABASE | DEFAULT | DEFERRABLE | DEFERRED | DELETE | DESC | DETACH | DISTINCT | DROP | EACH | ELSE | END | ESCAPE | EXCEPT | EXCLUSIVE | EXISTS | EXPLAIN | FAIL | FOR | FOREIGN | FROM | GLOB | GROUP | HAVING | IF | IGNORE | IMMEDIATE | IN | INDEX | INDEXED | INITIALLY | INNER | INSERT | INSTEAD | INTERSECT | INTO | IS | ISNULL | JOIN | KEY | LEFT | LIKE | LIMIT | MATCH | NATURAL | NOT | NOTNULL | NULL | OF | OFFSET | ON | OR | ORDER | OUTER | PLAN | PRAGMA | PRIMARY | QUERY | RAISE | REFERENCES | REGEXP | REINDEX | RELEASE | RENAME | REPLACE | RESTRICT | ROLLBACK | ROW | SAVEPOINT | SELECT | SET | TABLE | TEMPORARY | TEMP | THEN | TO | TRANSACTION | TRIGGER | UNION | UNIQUE | UPDATE | USING | VACUUM | VALUES | VIEW | VIRTUAL | WHEN | WHERE | DIV | MOD | FULLTEXT | SPATIAL | KEY_BLOCK_SIZE | WITH | PARSER | XOR | AUTO_INCREMENT | COLUMN_FORMAT | FIXED | DYNAMIC | STORAGE | DISK | MEMORY | FULL | PARTIAL | SIMPLE | NO | ACTION | ENGINE | AVG_ROW_LENGTH | CHECKSUM | DATA | DIRECTORY | DELAY_KEY_WRITE | INSERT_METHOD | FIRST | LAST | MAX_ROWS | MIN_ROWS | PACK_KEYS | PASSWORD | ROW_FORMAT | COMPRESSED | REDUNDANT | COMPACT | TABLESPACE | CHARSET | BIT | TINYINT | SMALLINT | MEDIUMINT | INT | INTEGER | BIGINT | REAL | DOUBLE | FLOAT | DECIMAL | NUMERIC | DATE | TIME | TIMESTAMP | DATETIME | CHAR | VARCHAR | BINARY | VARBINARY | TINYBLOB | BLOB | MEDIUMBLOB | LONGBLOB | TINYTEXT | TEXT | MEDIUMTEXT | LONGTEXT | ENUM | CHARACTER | UNSIGNED | ZEROFILL | CONNECTION | COMMENT | GEOMETRY | POINT | LINESTRING | POLYGON | MULTIPOINT | MULTILINESTRING | MULTIPOLYGON | GEOMETRYCOLLECTION | TYPE | RIGHT | ALGORITHM | DEFINER | INVOKER | SQL | SECURITY | CASCADED | LOCAL | OPTION | DISTINCTROW | HIGH_PRIORITY | STRAIGHT_JOIN | SQL_SMALL_RESULT | SQL_BIG_RESULT | SQL_BUFFER_RESULT | SQL_CACHE | SQL_NO_CACHE | SQL_CALC_FOUND_ROWS | ROLLUP | SHARE | LOCK | MODE | FORCE | OJ | USE | INTERVAL );
    public MySQLParser.keyword_return keyword() // throws RecognitionException [1]
    {   
        MySQLParser.keyword_return retval = new MySQLParser.keyword_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken set595 = null;

        object set595_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:719:9: ( ABORT | ADD | AFTER | ALL | ALTER | ANALYZE | AND | AS | ASC | ATTACH | AUTOINCREMENT | BEFORE | BEGIN | BETWEEN | BY | CASCADE | CASE | CAST | CHECK | COLLATE | COLUMN | COMMIT | CONFLICT | CONSTRAINT | CREATE | CROSS | DATABASE | DEFAULT | DEFERRABLE | DEFERRED | DELETE | DESC | DETACH | DISTINCT | DROP | EACH | ELSE | END | ESCAPE | EXCEPT | EXCLUSIVE | EXISTS | EXPLAIN | FAIL | FOR | FOREIGN | FROM | GLOB | GROUP | HAVING | IF | IGNORE | IMMEDIATE | IN | INDEX | INDEXED | INITIALLY | INNER | INSERT | INSTEAD | INTERSECT | INTO | IS | ISNULL | JOIN | KEY | LEFT | LIKE | LIMIT | MATCH | NATURAL | NOT | NOTNULL | NULL | OF | OFFSET | ON | OR | ORDER | OUTER | PLAN | PRAGMA | PRIMARY | QUERY | RAISE | REFERENCES | REGEXP | REINDEX | RELEASE | RENAME | REPLACE | RESTRICT | ROLLBACK | ROW | SAVEPOINT | SELECT | SET | TABLE | TEMPORARY | TEMP | THEN | TO | TRANSACTION | TRIGGER | UNION | UNIQUE | UPDATE | USING | VACUUM | VALUES | VIEW | VIRTUAL | WHEN | WHERE | DIV | MOD | FULLTEXT | SPATIAL | KEY_BLOCK_SIZE | WITH | PARSER | XOR | AUTO_INCREMENT | COLUMN_FORMAT | FIXED | DYNAMIC | STORAGE | DISK | MEMORY | FULL | PARTIAL | SIMPLE | NO | ACTION | ENGINE | AVG_ROW_LENGTH | CHECKSUM | DATA | DIRECTORY | DELAY_KEY_WRITE | INSERT_METHOD | FIRST | LAST | MAX_ROWS | MIN_ROWS | PACK_KEYS | PASSWORD | ROW_FORMAT | COMPRESSED | REDUNDANT | COMPACT | TABLESPACE | CHARSET | BIT | TINYINT | SMALLINT | MEDIUMINT | INT | INTEGER | BIGINT | REAL | DOUBLE | FLOAT | DECIMAL | NUMERIC | DATE | TIME | TIMESTAMP | DATETIME | CHAR | VARCHAR | BINARY | VARBINARY | TINYBLOB | BLOB | MEDIUMBLOB | LONGBLOB | TINYTEXT | TEXT | MEDIUMTEXT | LONGTEXT | ENUM | CHARACTER | UNSIGNED | ZEROFILL | CONNECTION | COMMENT | GEOMETRY | POINT | LINESTRING | POLYGON | MULTIPOINT | MULTILINESTRING | MULTIPOLYGON | GEOMETRYCOLLECTION | TYPE | RIGHT | ALGORITHM | DEFINER | INVOKER | SQL | SECURITY | CASCADED | LOCAL | OPTION | DISTINCTROW | HIGH_PRIORITY | STRAIGHT_JOIN | SQL_SMALL_RESULT | SQL_BIG_RESULT | SQL_BUFFER_RESULT | SQL_CACHE | SQL_NO_CACHE | SQL_CALC_FOUND_ROWS | ROLLUP | SHARE | LOCK | MODE | FORCE | OJ | USE | INTERVAL )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:
            {
            	root_0 = (object)adaptor.GetNilNode();

            	set595 = (IToken)input.LT(1);
            	if ( (input.LA(1) >= CREATE && input.LA(1) <= TABLE) || (input.LA(1) >= CONSTRAINT && input.LA(1) <= USING) || (input.LA(1) >= ASC && input.LA(1) <= KEY_BLOCK_SIZE) || (input.LA(1) >= WITH && input.LA(1) <= PARSER) || (input.LA(1) >= NOT && input.LA(1) <= UPDATE) || (input.LA(1) >= AUTO_INCREMENT && input.LA(1) <= COMMENT) || (input.LA(1) >= COLUMN_FORMAT && input.LA(1) <= DATETIME) || (input.LA(1) >= CHAR && input.LA(1) <= UNION) || (input.LA(1) >= OR && input.LA(1) <= HAVING) || (input.LA(1) >= OJ && input.LA(1) <= FORCE) || (input.LA(1) >= CASE && input.LA(1) <= END) || input.LA(1) == INTERVAL || (input.LA(1) >= DIV && input.LA(1) <= MOD) || (input.LA(1) >= IS && input.LA(1) <= AND) || input.LA(1) == XOR || (input.LA(1) >= ABORT && input.LA(1) <= VIRTUAL) ) 
            	{
            	    input.Consume();
            	    adaptor.AddChild(root_0, (object)adaptor.Create(set595));
            	    state.errorRecovery = false;
            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    throw mse;
            	}


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "keyword"

    public class sysname_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "sysname"
    // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:722:1: sysname : ( CURRENT_TIME | CURRENT_DATE | CURRENT_TIMESTAMP | UTC_TIMESTAMP | CURRENT_USER | YEAR );
    public MySQLParser.sysname_return sysname() // throws RecognitionException [1]
    {   
        MySQLParser.sysname_return retval = new MySQLParser.sysname_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken set596 = null;

        object set596_tree=null;

        try 
    	{
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:722:9: ( CURRENT_TIME | CURRENT_DATE | CURRENT_TIMESTAMP | UTC_TIMESTAMP | CURRENT_USER | YEAR )
            // ..\\Plugin.mysql\\AntlrParser\\MySQL.g:
            {
            	root_0 = (object)adaptor.GetNilNode();

            	set596 = (IToken)input.LT(1);
            	if ( input.LA(1) == CURRENT_TIMESTAMP || input.LA(1) == YEAR || input.LA(1) == CURRENT_USER || input.LA(1) == CURRENT_DATE || input.LA(1) == UTC_TIMESTAMP || input.LA(1) == CURRENT_TIME ) 
            	{
            	    input.Consume();
            	    adaptor.AddChild(root_0, (object)adaptor.Create(set596));
            	    state.errorRecovery = false;
            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    throw mse;
            	}


            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "sysname"

    // Delegated rules


   	protected DFA104 dfa104;
   	protected DFA182 dfa182;
	private void InitializeCyclicDFAs()
	{
    	this.dfa104 = new DFA104(this);
    	this.dfa182 = new DFA182(this);
	}

    const string DFA104_eotS =
        "\x17\uffff";
    const string DFA104_eofS =
        "\x17\uffff";
    const string DFA104_minS =
        "\x01\x13\x04\uffff\x01\x3d\x11\uffff";
    const string DFA104_maxS =
        "\x01\x7a\x04\uffff\x01\x68\x11\uffff";
    const string DFA104_acceptS =
        "\x01\uffff\x01\x01\x01\x02\x01\x03\x01\x04\x01\uffff\x01\x05\x01"+
        "\x06\x01\x07\x01\x08\x01\x09\x01\x0a\x01\x0b\x01\x0c\x01\x0d\x01"+
        "\x0e\x01\x0f\x01\x10\x01\x11\x01\x12\x01\x13\x01\x14\x01\x15";
    const string DFA104_specialS =
        "\x17\uffff}>";
    static readonly string[] DFA104_transitionS = {
            "\x01\x0d\x07\uffff\x01\x0f\x07\uffff\x01\x05\x03\uffff\x01"+
            "\x03\x01\x09\x14\uffff\x01\x06\x01\x08\x26\uffff\x01\x01\x01"+
            "\x02\x01\x04\x01\x06\x01\x07\x01\x0a\x01\x0b\x01\uffff\x01\x0c"+
            "\x01\x0e\x02\uffff\x01\x10\x01\x11\x01\x12\x01\x13\x01\x14\x03"+
            "\uffff\x01\x15\x01\x16",
            "",
            "",
            "",
            "",
            "\x01\x06\x01\x08\x29\uffff\x01\x06",
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
            ""
    };

    static readonly short[] DFA104_eot = DFA.UnpackEncodedString(DFA104_eotS);
    static readonly short[] DFA104_eof = DFA.UnpackEncodedString(DFA104_eofS);
    static readonly char[] DFA104_min = DFA.UnpackEncodedStringToUnsignedChars(DFA104_minS);
    static readonly char[] DFA104_max = DFA.UnpackEncodedStringToUnsignedChars(DFA104_maxS);
    static readonly short[] DFA104_accept = DFA.UnpackEncodedString(DFA104_acceptS);
    static readonly short[] DFA104_special = DFA.UnpackEncodedString(DFA104_specialS);
    static readonly short[][] DFA104_transition = DFA.UnpackEncodedStringArray(DFA104_transitionS);

    protected class DFA104 : DFA
    {
        public DFA104(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 104;
            this.eot = DFA104_eot;
            this.eof = DFA104_eof;
            this.min = DFA104_min;
            this.max = DFA104_max;
            this.accept = DFA104_accept;
            this.special = DFA104_special;
            this.transition = DFA104_transition;

        }

        override public string Description
        {
            get { return "176:1: table_option : ( ENGINE ( EQUALS )? ( id | MEMORY ) | TYPE ( EQUALS )? id | AUTO_INCREMENT ( EQUALS )? T_INTEGER | AVG_ROW_LENGTH ( EQUALS )? value | ( DEFAULT )? ( CHARACTER SET | CHARSET ) ( EQUALS )? charset_name | CHECKSUM ( EQUALS )? T_INTEGER | ( DEFAULT )? COLLATE ( EQUALS )? collation_name | COMMENT ( EQUALS )? T_STRING | CONNECTION ( EQUALS )? T_STRING | DATA DIRECTORY ( EQUALS )? T_STRING | DELAY_KEY_WRITE ( EQUALS )? T_INTEGER | INDEX DIRECTORY ( EQUALS )? T_STRING | INSERT_METHOD ( EQUALS )? ( NO | FIRST | LAST ) | KEY_BLOCK_SIZE ( EQUALS )? T_INTEGER | MAX_ROWS ( EQUALS )? T_INTEGER | MIN_ROWS ( EQUALS )? T_INTEGER | PACK_KEYS ( EQUALS )? ( T_INTEGER | DEFAULT ) | PASSWORD ( EQUALS )? T_STRING | ROW_FORMAT ( EQUALS )? ( DEFAULT | DYNAMIC | FIXED | COMPRESSED | REDUNDANT | COMPACT ) | TABLESPACE id ( STORAGE ( DISK | MEMORY | DEFAULT ) )? | UNION ( EQUALS )? LPAREN tbl_name ( COMMA tbl_name )* RPAREN );"; }
        }

    }

    const string DFA182_eotS =
        "\x0c\uffff";
    const string DFA182_eofS =
        "\x01\uffff\x01\x08\x01\x04\x08\uffff\x01\x08";
    const string DFA182_minS =
        "\x03\x0b\x02\uffff\x01\x0b\x01\uffff\x01\x1f\x03\uffff\x01\x0b";
    const string DFA182_maxS =
        "\x01\u012d\x02\u00d7\x02\uffff\x01\u012d\x01\uffff\x01\x20\x03"+
        "\uffff\x01\u00d7";
    const string DFA182_acceptS =
        "\x03\uffff\x01\x02\x01\x03\x01\uffff\x01\x06\x01\uffff\x01\x01"+
        "\x01\x04\x01\x05\x01\uffff";
    const string DFA182_specialS =
        "\x0c\uffff}>";
    static readonly string[] DFA182_transitionS = {
            "\x01\x05\x0c\uffff\x01\x04\x06\uffff\x02\x01\x01\uffff\x01"+
            "\x04\x03\uffff\x01\x02\x02\uffff\x01\x04\x25\uffff\x01\x03\x2c"+
            "\uffff\x01\x04\x01\uffff\x01\x04\x33\uffff\x01\x03\x01\uffff"+
            "\x01\x03\x05\uffff\x01\x02\x01\x06\x71\uffff\x01\x03",
            "\x01\x03\x02\x08\x09\uffff\x01\x08\x01\uffff\x02\x08\x01\uffff"+
            "\x02\x08\x01\uffff\x02\x08\x03\uffff\x01\x08\x55\uffff\x01\x08"+
            "\x04\uffff\x01\x07\x01\x08\x07\uffff\x01\x08\x04\uffff\x01\x08"+
            "\x01\uffff\x01\x08\x01\uffff\x03\x08\x06\uffff\x01\x08\x06\uffff"+
            "\x03\x08\x01\uffff\x02\x08\x01\uffff\x01\x08\x01\uffff\x05\x08"+
            "\x07\uffff\x04\x08\x03\uffff\x01\x08\x01\uffff\x19\x08",
            "\x01\x03\x02\x04\x09\uffff\x01\x04\x01\uffff\x02\x04\x01\uffff"+
            "\x02\x04\x01\uffff\x02\x04\x03\uffff\x01\x04\x55\uffff\x01\x04"+
            "\x04\uffff\x01\x03\x01\x04\x07\uffff\x01\x04\x04\uffff\x01\x04"+
            "\x01\uffff\x01\x04\x01\uffff\x03\x04\x06\uffff\x01\x04\x06\uffff"+
            "\x03\x04\x01\uffff\x02\x04\x01\uffff\x01\x04\x01\uffff\x05\x04"+
            "\x07\uffff\x04\x04\x03\uffff\x01\x04\x01\uffff\x19\x04",
            "",
            "",
            "\x01\x09\x0c\uffff\x01\x09\x06\uffff\x04\x09\x03\uffff\x01"+
            "\x09\x02\uffff\x01\x09\x25\uffff\x01\x09\x2c\uffff\x01\x09\x01"+
            "\uffff\x01\x09\x17\uffff\x01\x0a\x1b\uffff\x01\x09\x01\uffff"+
            "\x02\x09\x04\uffff\x05\x09\x6e\uffff\x01\x09",
            "",
            "\x02\x0b",
            "",
            "",
            "",
            "\x01\x03\x02\x08\x09\uffff\x01\x08\x01\uffff\x02\x08\x01\uffff"+
            "\x02\x08\x01\uffff\x02\x08\x03\uffff\x01\x08\x55\uffff\x01\x08"+
            "\x05\uffff\x01\x08\x07\uffff\x01\x08\x04\uffff\x01\x08\x01\uffff"+
            "\x01\x08\x01\uffff\x03\x08\x06\uffff\x01\x08\x06\uffff\x03\x08"+
            "\x01\uffff\x02\x08\x01\uffff\x01\x08\x01\uffff\x05\x08\x07\uffff"+
            "\x04\x08\x03\uffff\x01\x08\x01\uffff\x19\x08"
    };

    static readonly short[] DFA182_eot = DFA.UnpackEncodedString(DFA182_eotS);
    static readonly short[] DFA182_eof = DFA.UnpackEncodedString(DFA182_eofS);
    static readonly char[] DFA182_min = DFA.UnpackEncodedStringToUnsignedChars(DFA182_minS);
    static readonly char[] DFA182_max = DFA.UnpackEncodedStringToUnsignedChars(DFA182_maxS);
    static readonly short[] DFA182_accept = DFA.UnpackEncodedString(DFA182_acceptS);
    static readonly short[] DFA182_special = DFA.UnpackEncodedString(DFA182_specialS);
    static readonly short[][] DFA182_transition = DFA.UnpackEncodedStringArray(DFA182_transitionS);

    protected class DFA182 : DFA
    {
        public DFA182(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 182;
            this.eot = DFA182_eot;
            this.eof = DFA182_eof;
            this.min = DFA182_min;
            this.max = DFA182_max;
            this.accept = DFA182_accept;
            this.special = DFA182_special;
            this.transition = DFA182_transition;

        }

        override public string Description
        {
            get { return "348:1: expr_elem : ( id ( DOT id )? | func_call | value | LPAREN expr RPAREN | LPAREN select_statement RPAREN | INTERVAL T_INTEGER ( id | sysname ) );"; }
        }

    }

 

    public static readonly BitSet FOLLOW_CREATE_in_create_table77 = new BitSet(new ulong[]{0x0000000000000600UL});
    public static readonly BitSet FOLLOW_TEMPORARY_in_create_table79 = new BitSet(new ulong[]{0x0000000000000400UL});
    public static readonly BitSet FOLLOW_TABLE_in_create_table82 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_tbl_name_in_create_table86 = new BitSet(new ulong[]{0x0000000000000800UL});
    public static readonly BitSet FOLLOW_LPAREN_in_create_table98 = new BitSet(new ulong[]{0x00000001807FC000UL});
    public static readonly BitSet FOLLOW_create_definition_in_create_table100 = new BitSet(new ulong[]{0x0000000000003000UL});
    public static readonly BitSet FOLLOW_COMMA_in_create_table105 = new BitSet(new ulong[]{0x00000001807FC000UL});
    public static readonly BitSet FOLLOW_create_definition_in_create_table107 = new BitSet(new ulong[]{0x0000000000003000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_create_table113 = new BitSet(new ulong[]{0x6000018808080002UL,0x063E6FE000000000UL});
    public static readonly BitSet FOLLOW_table_options_in_create_table123 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_create_view_in_create_view_0135 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_col_name_in_create_definition149 = new BitSet(new ulong[]{0x8100000000000000UL,0x0000001FFFFFFFFFUL});
    public static readonly BitSet FOLLOW_column_definition_in_create_definition165 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_primary_key_in_create_definition182 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_index_definition_in_create_definition189 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_foreign_key_in_create_definition196 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_CONSTRAINT_in_create_definition204 = new BitSet(new ulong[]{0x0000000180008000UL});
    public static readonly BitSet FOLLOW_id_in_create_definition206 = new BitSet(new ulong[]{0x0000000000008000UL});
    public static readonly BitSet FOLLOW_CHECK_in_create_definition211 = new BitSet(new ulong[]{0x0000000000000800UL});
    public static readonly BitSet FOLLOW_LPAREN_in_create_definition213 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C34000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_expr_in_create_definition215 = new BitSet(new ulong[]{0x0000000000002000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_create_definition217 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_CONSTRAINT_in_index_definition252 = new BitSet(new ulong[]{0x00000001801F0000UL});
    public static readonly BitSet FOLLOW_id_in_index_definition254 = new BitSet(new ulong[]{0x00000000001F0000UL});
    public static readonly BitSet FOLLOW_set_in_index_definition259 = new BitSet(new ulong[]{0x00000000001C0000UL});
    public static readonly BitSet FOLLOW_UNIQUE_in_index_definition268 = new BitSet(new ulong[]{0x0000000180980800UL});
    public static readonly BitSet FOLLOW_set_in_index_definition272 = new BitSet(new ulong[]{0x0000000180980800UL});
    public static readonly BitSet FOLLOW_set_in_index_definition282 = new BitSet(new ulong[]{0x0000000180980800UL});
    public static readonly BitSet FOLLOW_index_name_in_index_definition296 = new BitSet(new ulong[]{0x0000000180980800UL});
    public static readonly BitSet FOLLOW_index_type_in_index_definition307 = new BitSet(new ulong[]{0x0000000180980800UL});
    public static readonly BitSet FOLLOW_index_cols_in_index_definition311 = new BitSet(new ulong[]{0x0000000028800002UL});
    public static readonly BitSet FOLLOW_index_option_in_index_definition314 = new BitSet(new ulong[]{0x0000000028800002UL});
    public static readonly BitSet FOLLOW_CONSTRAINT_in_primary_key326 = new BitSet(new ulong[]{0x0000000180200000UL});
    public static readonly BitSet FOLLOW_id_in_primary_key328 = new BitSet(new ulong[]{0x0000000000200000UL});
    public static readonly BitSet FOLLOW_PRIMARY_in_primary_key340 = new BitSet(new ulong[]{0x0000000000100000UL});
    public static readonly BitSet FOLLOW_KEY_in_primary_key342 = new BitSet(new ulong[]{0x0000000180980800UL});
    public static readonly BitSet FOLLOW_index_type_in_primary_key350 = new BitSet(new ulong[]{0x0000000180980800UL});
    public static readonly BitSet FOLLOW_index_cols_in_primary_key354 = new BitSet(new ulong[]{0x0000000028800002UL});
    public static readonly BitSet FOLLOW_index_option_in_primary_key357 = new BitSet(new ulong[]{0x0000000028800002UL});
    public static readonly BitSet FOLLOW_CONSTRAINT_in_foreign_key378 = new BitSet(new ulong[]{0x0000000180400000UL});
    public static readonly BitSet FOLLOW_id_in_foreign_key383 = new BitSet(new ulong[]{0x0000000000400000UL});
    public static readonly BitSet FOLLOW_FOREIGN_in_foreign_key396 = new BitSet(new ulong[]{0x0000000000100000UL});
    public static readonly BitSet FOLLOW_KEY_in_foreign_key398 = new BitSet(new ulong[]{0x0000000180980800UL});
    public static readonly BitSet FOLLOW_index_name_in_foreign_key400 = new BitSet(new ulong[]{0x0000000180980800UL});
    public static readonly BitSet FOLLOW_index_cols_in_foreign_key403 = new BitSet(new ulong[]{0x0001000000000000UL});
    public static readonly BitSet FOLLOW_reference_definition_in_foreign_key406 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_USING_in_index_type415 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_id_in_index_type417 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_LPAREN_in_index_cols427 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_index_col_in_index_cols429 = new BitSet(new ulong[]{0x0000000000003000UL});
    public static readonly BitSet FOLLOW_COMMA_in_index_cols433 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_index_col_in_index_cols435 = new BitSet(new ulong[]{0x0000000000003000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_index_cols440 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_col_name_in_index_col449 = new BitSet(new ulong[]{0x0000000006000802UL});
    public static readonly BitSet FOLLOW_LPAREN_in_index_col462 = new BitSet(new ulong[]{0x0000000001000000UL});
    public static readonly BitSet FOLLOW_T_INTEGER_in_index_col466 = new BitSet(new ulong[]{0x0000000000002000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_index_col468 = new BitSet(new ulong[]{0x0000000006000002UL});
    public static readonly BitSet FOLLOW_set_in_index_col479 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_LPAREN_in_fk_cols494 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_fk_col_in_fk_cols496 = new BitSet(new ulong[]{0x0000000000003000UL});
    public static readonly BitSet FOLLOW_COMMA_in_fk_cols500 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_fk_col_in_fk_cols502 = new BitSet(new ulong[]{0x0000000000003000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_fk_cols507 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_col_name_in_fk_col516 = new BitSet(new ulong[]{0x0000000006000802UL});
    public static readonly BitSet FOLLOW_LPAREN_in_fk_col529 = new BitSet(new ulong[]{0x0000000001000000UL});
    public static readonly BitSet FOLLOW_T_INTEGER_in_fk_col533 = new BitSet(new ulong[]{0x0000000000002000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_fk_col535 = new BitSet(new ulong[]{0x0000000006000002UL});
    public static readonly BitSet FOLLOW_set_in_fk_col546 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_KEY_BLOCK_SIZE_in_index_option565 = new BitSet(new ulong[]{0x0000000011000000UL});
    public static readonly BitSet FOLLOW_EQUALS_in_index_option567 = new BitSet(new ulong[]{0x0000000001000000UL});
    public static readonly BitSet FOLLOW_T_INTEGER_in_index_option570 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_index_type_in_index_option576 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_WITH_in_index_option583 = new BitSet(new ulong[]{0x0000000040000000UL});
    public static readonly BitSet FOLLOW_PARSER_in_index_option585 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_id_in_index_option587 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_set_in_id0 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_id_in_col_name604 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_id_in_tbl_name610 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_id_in_index_name616 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_id_in_charset_name624 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_id_in_collation_name632 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_data_type_in_column_definition644 = new BitSet(new ulong[]{0x0001259E00340002UL});
    public static readonly BitSet FOLLOW_NOT_in_column_definition657 = new BitSet(new ulong[]{0x0000000400000000UL});
    public static readonly BitSet FOLLOW_NULL_in_column_definition659 = new BitSet(new ulong[]{0x0001259800340002UL});
    public static readonly BitSet FOLLOW_NULL_in_column_definition667 = new BitSet(new ulong[]{0x0001259800340002UL});
    public static readonly BitSet FOLLOW_DEFAULT_in_column_definition681 = new BitSet(new ulong[]{0x0000024401000000UL,0x3000000000000000UL,0x0010000000000000UL});
    public static readonly BitSet FOLLOW_default_value_expr_in_column_definition683 = new BitSet(new ulong[]{0x0001259000340002UL});
    public static readonly BitSet FOLLOW_ON_in_column_definition695 = new BitSet(new ulong[]{0x0000002000000000UL});
    public static readonly BitSet FOLLOW_UPDATE_in_column_definition697 = new BitSet(new ulong[]{0x0000004000000000UL});
    public static readonly BitSet FOLLOW_CURRENT_TIMESTAMP_in_column_definition699 = new BitSet(new ulong[]{0x0001258000340002UL});
    public static readonly BitSet FOLLOW_AUTO_INCREMENT_in_column_definition710 = new BitSet(new ulong[]{0x0001250000340002UL});
    public static readonly BitSet FOLLOW_UNIQUE_in_column_definition724 = new BitSet(new ulong[]{0x0001250000100002UL});
    public static readonly BitSet FOLLOW_KEY_in_column_definition726 = new BitSet(new ulong[]{0x0001250000000002UL});
    public static readonly BitSet FOLLOW_PRIMARY_in_column_definition731 = new BitSet(new ulong[]{0x0000000000100000UL});
    public static readonly BitSet FOLLOW_KEY_in_column_definition734 = new BitSet(new ulong[]{0x0001250000000002UL});
    public static readonly BitSet FOLLOW_COMMENT_in_column_definition745 = new BitSet(new ulong[]{0x0000020000000000UL});
    public static readonly BitSet FOLLOW_T_STRING_in_column_definition749 = new BitSet(new ulong[]{0x0001240000000002UL});
    public static readonly BitSet FOLLOW_COLUMN_FORMAT_in_column_definition763 = new BitSet(new ulong[]{0x0000180800000000UL});
    public static readonly BitSet FOLLOW_set_in_column_definition765 = new BitSet(new ulong[]{0x0001200000000002UL});
    public static readonly BitSet FOLLOW_STORAGE_in_column_definition782 = new BitSet(new ulong[]{0x0000C00800000000UL});
    public static readonly BitSet FOLLOW_set_in_column_definition784 = new BitSet(new ulong[]{0x0001000000000002UL});
    public static readonly BitSet FOLLOW_reference_definition_in_column_definition800 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_REFERENCES_in_reference_definition822 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_tbl_name_in_reference_definition826 = new BitSet(new ulong[]{0x0000000000000800UL});
    public static readonly BitSet FOLLOW_fk_cols_in_reference_definition836 = new BitSet(new ulong[]{0x0002001000000002UL});
    public static readonly BitSet FOLLOW_MATCH_in_reference_definition846 = new BitSet(new ulong[]{0x0004000000000000UL});
    public static readonly BitSet FOLLOW_FULL_in_reference_definition848 = new BitSet(new ulong[]{0x0000001000000002UL});
    public static readonly BitSet FOLLOW_MATCH_in_reference_definition852 = new BitSet(new ulong[]{0x0008000000000000UL});
    public static readonly BitSet FOLLOW_PARTIAL_in_reference_definition854 = new BitSet(new ulong[]{0x0000001000000002UL});
    public static readonly BitSet FOLLOW_MATCH_in_reference_definition858 = new BitSet(new ulong[]{0x0010000000000000UL});
    public static readonly BitSet FOLLOW_SIMPLE_in_reference_definition860 = new BitSet(new ulong[]{0x0000001000000002UL});
    public static readonly BitSet FOLLOW_ON_in_reference_definition871 = new BitSet(new ulong[]{0x0020000000000000UL});
    public static readonly BitSet FOLLOW_DELETE_in_reference_definition873 = new BitSet(new ulong[]{0x03C0000000000000UL});
    public static readonly BitSet FOLLOW_reference_option_in_reference_definition877 = new BitSet(new ulong[]{0x0000001000000002UL});
    public static readonly BitSet FOLLOW_ON_in_reference_definition891 = new BitSet(new ulong[]{0x0000002000000000UL});
    public static readonly BitSet FOLLOW_UPDATE_in_reference_definition893 = new BitSet(new ulong[]{0x03C0000000000000UL});
    public static readonly BitSet FOLLOW_reference_option_in_reference_definition897 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_RESTRICT_in_reference_option916 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_CASCADE_in_reference_option924 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_SET_in_reference_option932 = new BitSet(new ulong[]{0x0000000400000000UL});
    public static readonly BitSet FOLLOW_NULL_in_reference_option934 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_NO_in_reference_option942 = new BitSet(new ulong[]{0x0400000000000000UL});
    public static readonly BitSet FOLLOW_ACTION_in_reference_option944 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_UNSIGNED_in_number_flags960 = new BitSet(new ulong[]{0x1000000000000002UL});
    public static readonly BitSet FOLLOW_ZEROFILL_in_number_flags967 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_CHARACTER_in_charset_collate986 = new BitSet(new ulong[]{0x0100000000000000UL});
    public static readonly BitSet FOLLOW_SET_in_charset_collate988 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_id_in_charset_collate992 = new BitSet(new ulong[]{0x4000000000000002UL});
    public static readonly BitSet FOLLOW_COLLATE_in_charset_collate1006 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_id_in_charset_collate1010 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_BIT_in_data_type1028 = new BitSet(new ulong[]{0x0000000000000802UL});
    public static readonly BitSet FOLLOW_int_length_in_data_type1032 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_TINYINT_in_data_type1042 = new BitSet(new ulong[]{0x1800000000000800UL});
    public static readonly BitSet FOLLOW_int_length_in_data_type1046 = new BitSet(new ulong[]{0x1800000000000000UL});
    public static readonly BitSet FOLLOW_number_flags_in_data_type1050 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_SMALLINT_in_data_type1059 = new BitSet(new ulong[]{0x1800000000000800UL});
    public static readonly BitSet FOLLOW_int_length_in_data_type1063 = new BitSet(new ulong[]{0x1800000000000000UL});
    public static readonly BitSet FOLLOW_number_flags_in_data_type1067 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_MEDIUMINT_in_data_type1076 = new BitSet(new ulong[]{0x1800000000000800UL});
    public static readonly BitSet FOLLOW_int_length_in_data_type1080 = new BitSet(new ulong[]{0x1800000000000000UL});
    public static readonly BitSet FOLLOW_number_flags_in_data_type1084 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_INT_in_data_type1093 = new BitSet(new ulong[]{0x1800000000000800UL});
    public static readonly BitSet FOLLOW_int_length_in_data_type1098 = new BitSet(new ulong[]{0x1800000000000000UL});
    public static readonly BitSet FOLLOW_number_flags_in_data_type1102 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_INTEGER_in_data_type1111 = new BitSet(new ulong[]{0x1800000000000800UL});
    public static readonly BitSet FOLLOW_int_length_in_data_type1115 = new BitSet(new ulong[]{0x1800000000000000UL});
    public static readonly BitSet FOLLOW_number_flags_in_data_type1119 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_BIGINT_in_data_type1128 = new BitSet(new ulong[]{0x1800000000000800UL});
    public static readonly BitSet FOLLOW_int_length_in_data_type1132 = new BitSet(new ulong[]{0x1800000000000000UL});
    public static readonly BitSet FOLLOW_number_flags_in_data_type1137 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_REAL_in_data_type1146 = new BitSet(new ulong[]{0x1800000000000800UL});
    public static readonly BitSet FOLLOW_real_length_in_data_type1150 = new BitSet(new ulong[]{0x1800000000000000UL});
    public static readonly BitSet FOLLOW_number_flags_in_data_type1154 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_DOUBLE_in_data_type1163 = new BitSet(new ulong[]{0x1800000000000800UL});
    public static readonly BitSet FOLLOW_real_length_in_data_type1167 = new BitSet(new ulong[]{0x1800000000000000UL});
    public static readonly BitSet FOLLOW_number_flags_in_data_type1171 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_FLOAT_in_data_type1180 = new BitSet(new ulong[]{0x1800000000000800UL});
    public static readonly BitSet FOLLOW_real_length_in_data_type1184 = new BitSet(new ulong[]{0x1800000000000000UL});
    public static readonly BitSet FOLLOW_number_flags_in_data_type1188 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_DECIMAL_in_data_type1197 = new BitSet(new ulong[]{0x1800000000000800UL});
    public static readonly BitSet FOLLOW_real_length_in_data_type1201 = new BitSet(new ulong[]{0x1800000000000000UL});
    public static readonly BitSet FOLLOW_number_flags_in_data_type1205 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_NUMERIC_in_data_type1214 = new BitSet(new ulong[]{0x1800000000000800UL});
    public static readonly BitSet FOLLOW_real_length_in_data_type1219 = new BitSet(new ulong[]{0x1800000000000000UL});
    public static readonly BitSet FOLLOW_number_flags_in_data_type1223 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_DATE_in_data_type1232 = new BitSet(new ulong[]{0x0000000000000802UL});
    public static readonly BitSet FOLLOW_int_length_in_data_type1236 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_TIME_in_data_type1246 = new BitSet(new ulong[]{0x0000000000000802UL});
    public static readonly BitSet FOLLOW_int_length_in_data_type1250 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_TIMESTAMP_in_data_type1260 = new BitSet(new ulong[]{0x0000000000000802UL});
    public static readonly BitSet FOLLOW_int_length_in_data_type1264 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_DATETIME_in_data_type1274 = new BitSet(new ulong[]{0x0000000000000802UL});
    public static readonly BitSet FOLLOW_int_length_in_data_type1278 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_YEAR_in_data_type1288 = new BitSet(new ulong[]{0x0000000000000802UL});
    public static readonly BitSet FOLLOW_int_length_in_data_type1292 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_CHAR_in_data_type1302 = new BitSet(new ulong[]{0x0000000000000800UL});
    public static readonly BitSet FOLLOW_char_length_in_data_type1306 = new BitSet(new ulong[]{0x6000000000000000UL});
    public static readonly BitSet FOLLOW_charset_collate_in_data_type1309 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_VARCHAR_in_data_type1318 = new BitSet(new ulong[]{0x0000000000000800UL});
    public static readonly BitSet FOLLOW_char_length_in_data_type1322 = new BitSet(new ulong[]{0x6000000000000000UL});
    public static readonly BitSet FOLLOW_charset_collate_in_data_type1325 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_BINARY_in_data_type1334 = new BitSet(new ulong[]{0x0000000000000802UL});
    public static readonly BitSet FOLLOW_char_length_in_data_type1338 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_VARBINARY_in_data_type1348 = new BitSet(new ulong[]{0x0000000000000802UL});
    public static readonly BitSet FOLLOW_char_length_in_data_type1352 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_TINYBLOB_in_data_type1362 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_BLOB_in_data_type1370 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_MEDIUMBLOB_in_data_type1378 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_LONGBLOB_in_data_type1386 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_TINYTEXT_in_data_type1394 = new BitSet(new ulong[]{0x6000000000000000UL,0x0000000000040000UL});
    public static readonly BitSet FOLLOW_BINARY_in_data_type1399 = new BitSet(new ulong[]{0x6000000000000000UL});
    public static readonly BitSet FOLLOW_charset_collate_in_data_type1402 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_TEXT_in_data_type1411 = new BitSet(new ulong[]{0x6000000000000000UL,0x0000000000040000UL});
    public static readonly BitSet FOLLOW_BINARY_in_data_type1416 = new BitSet(new ulong[]{0x6000000000000000UL});
    public static readonly BitSet FOLLOW_charset_collate_in_data_type1419 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_MEDIUMTEXT_in_data_type1428 = new BitSet(new ulong[]{0x6000000000000000UL,0x0000000000040000UL});
    public static readonly BitSet FOLLOW_BINARY_in_data_type1433 = new BitSet(new ulong[]{0x6000000000000000UL});
    public static readonly BitSet FOLLOW_charset_collate_in_data_type1436 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_LONGTEXT_in_data_type1445 = new BitSet(new ulong[]{0x6000000000000000UL,0x0000000000040000UL});
    public static readonly BitSet FOLLOW_BINARY_in_data_type1449 = new BitSet(new ulong[]{0x6000000000000000UL});
    public static readonly BitSet FOLLOW_charset_collate_in_data_type1452 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_ENUM_in_data_type1461 = new BitSet(new ulong[]{0x0000000000000800UL});
    public static readonly BitSet FOLLOW_enumset_values_in_data_type1466 = new BitSet(new ulong[]{0x6000000000000000UL});
    public static readonly BitSet FOLLOW_charset_collate_in_data_type1469 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_SET_in_data_type1478 = new BitSet(new ulong[]{0x0000000000000800UL});
    public static readonly BitSet FOLLOW_enumset_values_in_data_type1483 = new BitSet(new ulong[]{0x6000000000000000UL});
    public static readonly BitSet FOLLOW_charset_collate_in_data_type1486 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_GEOMETRY_in_data_type1495 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_POINT_in_data_type1503 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_LINESTRING_in_data_type1511 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_POLYGON_in_data_type1519 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_MULTIPOINT_in_data_type1527 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_MULTILINESTRING_in_data_type1535 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_MULTIPOLYGON_in_data_type1543 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_GEOMETRYCOLLECTION_in_data_type1551 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_LPAREN_in_int_length1563 = new BitSet(new ulong[]{0x0000000001000000UL});
    public static readonly BitSet FOLLOW_T_INTEGER_in_int_length1567 = new BitSet(new ulong[]{0x0000000000002000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_int_length1571 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_LPAREN_in_char_length1579 = new BitSet(new ulong[]{0x0000000001000000UL});
    public static readonly BitSet FOLLOW_T_INTEGER_in_char_length1583 = new BitSet(new ulong[]{0x0000000000002000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_char_length1587 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_LPAREN_in_real_length1595 = new BitSet(new ulong[]{0x0000000001000000UL});
    public static readonly BitSet FOLLOW_T_INTEGER_in_real_length1599 = new BitSet(new ulong[]{0x0000000000003000UL});
    public static readonly BitSet FOLLOW_COMMA_in_real_length1604 = new BitSet(new ulong[]{0x0000000001000000UL});
    public static readonly BitSet FOLLOW_T_INTEGER_in_real_length1608 = new BitSet(new ulong[]{0x0000000000002000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_real_length1615 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_LPAREN_in_enumset_values1624 = new BitSet(new ulong[]{0x0000020180000000UL});
    public static readonly BitSet FOLLOW_enumset_value_in_enumset_values1626 = new BitSet(new ulong[]{0x0000000000003000UL});
    public static readonly BitSet FOLLOW_COMMA_in_enumset_values1630 = new BitSet(new ulong[]{0x0000020180000000UL});
    public static readonly BitSet FOLLOW_enumset_value_in_enumset_values1632 = new BitSet(new ulong[]{0x0000000000003000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_enumset_values1638 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_id_in_enumset_value1649 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_T_STRING_in_enumset_value1657 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_table_option_in_table_options1666 = new BitSet(new ulong[]{0x6000018808081002UL,0x063E6FE000000000UL});
    public static readonly BitSet FOLLOW_COMMA_in_table_options1670 = new BitSet(new ulong[]{0x6000018808080000UL,0x063E6FE000000000UL});
    public static readonly BitSet FOLLOW_table_option_in_table_options1673 = new BitSet(new ulong[]{0x6000018808081002UL,0x063E6FE000000000UL});
    public static readonly BitSet FOLLOW_ENGINE_in_table_option1686 = new BitSet(new ulong[]{0x0000800190000000UL});
    public static readonly BitSet FOLLOW_EQUALS_in_table_option1688 = new BitSet(new ulong[]{0x0000800180000000UL});
    public static readonly BitSet FOLLOW_id_in_table_option1693 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_MEMORY_in_table_option1697 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_TYPE_in_table_option1705 = new BitSet(new ulong[]{0x0000000190000000UL});
    public static readonly BitSet FOLLOW_EQUALS_in_table_option1707 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_id_in_table_option1710 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_AUTO_INCREMENT_in_table_option1716 = new BitSet(new ulong[]{0x0000000011000000UL});
    public static readonly BitSet FOLLOW_EQUALS_in_table_option1718 = new BitSet(new ulong[]{0x0000000001000000UL});
    public static readonly BitSet FOLLOW_T_INTEGER_in_table_option1721 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_AVG_ROW_LENGTH_in_table_option1727 = new BitSet(new ulong[]{0x0000024411000000UL,0x5000000000000000UL,0x0400000000000000UL});
    public static readonly BitSet FOLLOW_EQUALS_in_table_option1729 = new BitSet(new ulong[]{0x0000024401000000UL,0x5000000000000000UL,0x0400000000000000UL});
    public static readonly BitSet FOLLOW_value_in_table_option1732 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_DEFAULT_in_table_option1738 = new BitSet(new ulong[]{0x2000000000000000UL,0x0000010000000000UL});
    public static readonly BitSet FOLLOW_CHARACTER_in_table_option1742 = new BitSet(new ulong[]{0x0100000000000000UL});
    public static readonly BitSet FOLLOW_SET_in_table_option1744 = new BitSet(new ulong[]{0x0000000190000000UL});
    public static readonly BitSet FOLLOW_CHARSET_in_table_option1746 = new BitSet(new ulong[]{0x0000000190000000UL});
    public static readonly BitSet FOLLOW_EQUALS_in_table_option1749 = new BitSet(new ulong[]{0x0000000190000000UL});
    public static readonly BitSet FOLLOW_charset_name_in_table_option1752 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_CHECKSUM_in_table_option1758 = new BitSet(new ulong[]{0x0000000011000000UL});
    public static readonly BitSet FOLLOW_EQUALS_in_table_option1760 = new BitSet(new ulong[]{0x0000000001000000UL});
    public static readonly BitSet FOLLOW_T_INTEGER_in_table_option1763 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_DEFAULT_in_table_option1769 = new BitSet(new ulong[]{0x4000000000000000UL});
    public static readonly BitSet FOLLOW_COLLATE_in_table_option1772 = new BitSet(new ulong[]{0x0000000190000000UL});
    public static readonly BitSet FOLLOW_EQUALS_in_table_option1774 = new BitSet(new ulong[]{0x0000000190000000UL});
    public static readonly BitSet FOLLOW_collation_name_in_table_option1777 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_COMMENT_in_table_option1783 = new BitSet(new ulong[]{0x0000020010000000UL});
    public static readonly BitSet FOLLOW_EQUALS_in_table_option1785 = new BitSet(new ulong[]{0x0000020000000000UL});
    public static readonly BitSet FOLLOW_T_STRING_in_table_option1788 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_CONNECTION_in_table_option1794 = new BitSet(new ulong[]{0x0000020010000000UL});
    public static readonly BitSet FOLLOW_EQUALS_in_table_option1796 = new BitSet(new ulong[]{0x0000020000000000UL});
    public static readonly BitSet FOLLOW_T_STRING_in_table_option1799 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_DATA_in_table_option1805 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000100000000000UL});
    public static readonly BitSet FOLLOW_DIRECTORY_in_table_option1807 = new BitSet(new ulong[]{0x0000020010000000UL});
    public static readonly BitSet FOLLOW_EQUALS_in_table_option1809 = new BitSet(new ulong[]{0x0000020000000000UL});
    public static readonly BitSet FOLLOW_T_STRING_in_table_option1812 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_DELAY_KEY_WRITE_in_table_option1818 = new BitSet(new ulong[]{0x0000000011000000UL});
    public static readonly BitSet FOLLOW_EQUALS_in_table_option1820 = new BitSet(new ulong[]{0x0000000001000000UL});
    public static readonly BitSet FOLLOW_T_INTEGER_in_table_option1823 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_INDEX_in_table_option1829 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000100000000000UL});
    public static readonly BitSet FOLLOW_DIRECTORY_in_table_option1831 = new BitSet(new ulong[]{0x0000020010000000UL});
    public static readonly BitSet FOLLOW_EQUALS_in_table_option1833 = new BitSet(new ulong[]{0x0000020000000000UL});
    public static readonly BitSet FOLLOW_T_STRING_in_table_option1836 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_INSERT_METHOD_in_table_option1842 = new BitSet(new ulong[]{0x0200000010000000UL,0x0001800000000000UL});
    public static readonly BitSet FOLLOW_EQUALS_in_table_option1844 = new BitSet(new ulong[]{0x0200000000000000UL,0x0001800000000000UL});
    public static readonly BitSet FOLLOW_set_in_table_option1847 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_KEY_BLOCK_SIZE_in_table_option1865 = new BitSet(new ulong[]{0x0000000011000000UL});
    public static readonly BitSet FOLLOW_EQUALS_in_table_option1867 = new BitSet(new ulong[]{0x0000000001000000UL});
    public static readonly BitSet FOLLOW_T_INTEGER_in_table_option1870 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_MAX_ROWS_in_table_option1876 = new BitSet(new ulong[]{0x0000000011000000UL});
    public static readonly BitSet FOLLOW_EQUALS_in_table_option1878 = new BitSet(new ulong[]{0x0000000001000000UL});
    public static readonly BitSet FOLLOW_T_INTEGER_in_table_option1881 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_MIN_ROWS_in_table_option1887 = new BitSet(new ulong[]{0x0000000011000000UL});
    public static readonly BitSet FOLLOW_EQUALS_in_table_option1889 = new BitSet(new ulong[]{0x0000000001000000UL});
    public static readonly BitSet FOLLOW_T_INTEGER_in_table_option1892 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_PACK_KEYS_in_table_option1898 = new BitSet(new ulong[]{0x0000000811000000UL});
    public static readonly BitSet FOLLOW_EQUALS_in_table_option1900 = new BitSet(new ulong[]{0x0000000801000000UL});
    public static readonly BitSet FOLLOW_set_in_table_option1903 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_PASSWORD_in_table_option1915 = new BitSet(new ulong[]{0x0000020010000000UL});
    public static readonly BitSet FOLLOW_EQUALS_in_table_option1917 = new BitSet(new ulong[]{0x0000020000000000UL});
    public static readonly BitSet FOLLOW_T_STRING_in_table_option1920 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_ROW_FORMAT_in_table_option1926 = new BitSet(new ulong[]{0x0000180810000000UL,0x01C0000000000000UL});
    public static readonly BitSet FOLLOW_EQUALS_in_table_option1928 = new BitSet(new ulong[]{0x0000180800000000UL,0x01C0000000000000UL});
    public static readonly BitSet FOLLOW_set_in_table_option1931 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_TABLESPACE_in_table_option1949 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_id_in_table_option1951 = new BitSet(new ulong[]{0x0000200000000002UL});
    public static readonly BitSet FOLLOW_STORAGE_in_table_option1954 = new BitSet(new ulong[]{0x0000C00800000000UL});
    public static readonly BitSet FOLLOW_set_in_table_option1956 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_UNION_in_table_option1970 = new BitSet(new ulong[]{0x0000000010000800UL});
    public static readonly BitSet FOLLOW_EQUALS_in_table_option1972 = new BitSet(new ulong[]{0x0000000000000800UL});
    public static readonly BitSet FOLLOW_LPAREN_in_table_option1975 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_tbl_name_in_table_option1977 = new BitSet(new ulong[]{0x0000000000003000UL});
    public static readonly BitSet FOLLOW_COMMA_in_table_option1980 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_tbl_name_in_table_option1982 = new BitSet(new ulong[]{0x0000000000003000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_table_option1986 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_find_dep_item_in_find_deps1999 = new BitSet(new ulong[]{0xFFFFFFBFFFFFFF02UL,0x7FFFFFFFFFFF7FFFUL,0xFBEBFFFFFFFFFFFFUL,0xFF8000001FFFFFFFUL,0x00001FFFFFFFFFFFUL});
    public static readonly BitSet FOLLOW_keyword_in_find_dep_item2013 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_operator_no_dot_in_find_dep_item2017 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_T_NSTRING_in_find_dep_item2021 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_T_STRING_in_find_dep_item2025 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_T_INTEGER_in_find_dep_item2029 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_T_FLOAT_in_find_dep_item2033 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_T_BINARYNUM_in_find_dep_item2037 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_T_BLOB_in_find_dep_item2041 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_id_in_find_dep_item2055 = new BitSet(new ulong[]{0x0000000000000002UL,0x8000000000000000UL});
    public static readonly BitSet FOLLOW_DOT_in_find_dep_item2068 = new BitSet(new ulong[]{0x0000000180000000UL,0x8000000000000000UL});
    public static readonly BitSet FOLLOW_DOT_in_find_dep_item2070 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_id_in_find_dep_item2075 = new BitSet(new ulong[]{0x0000000000000002UL,0x8000000000000000UL});
    public static readonly BitSet FOLLOW_CREATE_in_create_view2100 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x000000000000009DUL});
    public static readonly BitSet FOLLOW_OR_in_create_view2103 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000000002UL});
    public static readonly BitSet FOLLOW_REPLACE_in_create_view2105 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x000000000000009CUL});
    public static readonly BitSet FOLLOW_f_inc_in_create_view2109 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x000000000000009CUL});
    public static readonly BitSet FOLLOW_f_nl_in_create_view2120 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000000004UL});
    public static readonly BitSet FOLLOW_ALGORITHM_in_create_view2122 = new BitSet(new ulong[]{0x0000000010000000UL});
    public static readonly BitSet FOLLOW_EQUALS_in_create_view2124 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_id_in_create_view2126 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000000098UL});
    public static readonly BitSet FOLLOW_f_nl_in_create_view2139 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000000008UL});
    public static readonly BitSet FOLLOW_DEFINER_in_create_view2141 = new BitSet(new ulong[]{0x0000000010000000UL});
    public static readonly BitSet FOLLOW_EQUALS_in_create_view2143 = new BitSet(new ulong[]{0x0000000180000000UL,0x0000000000000000UL,0x0004000000000000UL});
    public static readonly BitSet FOLLOW_user_in_create_view2145 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000000090UL});
    public static readonly BitSet FOLLOW_f_nl_in_create_view2158 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000000010UL});
    public static readonly BitSet FOLLOW_SQL_in_create_view2160 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000000020UL});
    public static readonly BitSet FOLLOW_SECURITY_in_create_view2162 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000000048UL});
    public static readonly BitSet FOLLOW_set_in_create_view2164 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000000080UL});
    public static readonly BitSet FOLLOW_f_dec_in_create_view2180 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000000080UL});
    public static readonly BitSet FOLLOW_f_nl_in_create_view2182 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000000080UL});
    public static readonly BitSet FOLLOW_VIEW_in_create_view2184 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_id_in_create_view2186 = new BitSet(new ulong[]{0x0000000000000800UL,0x0000000000000000UL,0x0000000000000100UL});
    public static readonly BitSet FOLLOW_LPAREN_in_create_view2190 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_id_in_create_view2192 = new BitSet(new ulong[]{0x0000000000003000UL});
    public static readonly BitSet FOLLOW_COMMA_in_create_view2195 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_id_in_create_view2197 = new BitSet(new ulong[]{0x0000000000003000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_create_view2201 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000000100UL});
    public static readonly BitSet FOLLOW_AS_in_create_view2213 = new BitSet(new ulong[]{0x0000000000000800UL,0x0000000000000000UL,0x0000000000400000UL});
    public static readonly BitSet FOLLOW_f_nl_in_create_view2215 = new BitSet(new ulong[]{0x0000000000000800UL,0x0000000000000000UL,0x0000000000400000UL});
    public static readonly BitSet FOLLOW_select_statement_in_create_view2218 = new BitSet(new ulong[]{0x0000000020000002UL});
    public static readonly BitSet FOLLOW_LPAREN_in_create_view2222 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000400000UL});
    public static readonly BitSet FOLLOW_select_statement_in_create_view2224 = new BitSet(new ulong[]{0x0000000000002000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_create_view2226 = new BitSet(new ulong[]{0x0000000020000002UL});
    public static readonly BitSet FOLLOW_WITH_in_create_view2238 = new BitSet(new ulong[]{0x0000000000008000UL,0x0000000000000000UL,0x0000000000000600UL});
    public static readonly BitSet FOLLOW_set_in_create_view2240 = new BitSet(new ulong[]{0x0000000000008000UL});
    public static readonly BitSet FOLLOW_CHECK_in_create_view2249 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000000800UL});
    public static readonly BitSet FOLLOW_OPTION_in_create_view2251 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_select_statement_component_in_select_statement2291 = new BitSet(new ulong[]{0x0000000000000002UL,0x0400000000000000UL,0x000000000006A000UL});
    public static readonly BitSet FOLLOW_f_nl_in_select_statement2298 = new BitSet(new ulong[]{0x0000000000000000UL,0x0400000000000000UL});
    public static readonly BitSet FOLLOW_UNION_in_select_statement2300 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000401000UL});
    public static readonly BitSet FOLLOW_ALL_in_select_statement2302 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000400000UL});
    public static readonly BitSet FOLLOW_f_nl_in_select_statement2305 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000400000UL});
    public static readonly BitSet FOLLOW_select_statement_component_in_select_statement2307 = new BitSet(new ulong[]{0x0000000000000002UL,0x0400000000000000UL,0x000000000006A000UL});
    public static readonly BitSet FOLLOW_f_nl_in_select_statement2317 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000002000UL});
    public static readonly BitSet FOLLOW_ORDER_in_select_statement2319 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000004000UL});
    public static readonly BitSet FOLLOW_BY_in_select_statement2321 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C34000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_selectby_expr_in_select_statement2323 = new BitSet(new ulong[]{0x0000000000001002UL,0x0000000000000000UL,0x0000000000068000UL});
    public static readonly BitSet FOLLOW_COMMA_in_select_statement2326 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C34000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_selectby_expr_in_select_statement2328 = new BitSet(new ulong[]{0x0000000000001002UL,0x0000000000000000UL,0x0000000000068000UL});
    public static readonly BitSet FOLLOW_f_nl_in_select_statement2339 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000008000UL});
    public static readonly BitSet FOLLOW_LIMIT_in_select_statement2341 = new BitSet(new ulong[]{0x0000000001000000UL,0x0000000000000010UL});
    public static readonly BitSet FOLLOW_T_INTEGER_in_select_statement2345 = new BitSet(new ulong[]{0x0000000000001000UL});
    public static readonly BitSet FOLLOW_COMMA_in_select_statement2347 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000010UL});
    public static readonly BitSet FOLLOW_INTEGER_in_select_statement2351 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x0000000000060000UL});
    public static readonly BitSet FOLLOW_INTEGER_in_select_statement2355 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000010000UL});
    public static readonly BitSet FOLLOW_OFFSET_in_select_statement2357 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000010UL});
    public static readonly BitSet FOLLOW_INTEGER_in_select_statement2359 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x0000000000060000UL});
    public static readonly BitSet FOLLOW_f_nl_in_select_statement2370 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000020000UL});
    public static readonly BitSet FOLLOW_FOR_in_select_statement2372 = new BitSet(new ulong[]{0x0000002000000000UL});
    public static readonly BitSet FOLLOW_UPDATE_in_select_statement2374 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_LOCK_in_select_statement2378 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000080000UL});
    public static readonly BitSet FOLLOW_IN_in_select_statement2380 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000100000UL});
    public static readonly BitSet FOLLOW_SHARE_in_select_statement2382 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000200000UL});
    public static readonly BitSet FOLLOW_MODE_in_select_statement2384 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_SELECT_in_select_statement_component2406 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C340041FF801000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_set_in_select_statement_component2416 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C340041FF801000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_HIGH_PRIORITY_in_select_statement_component2437 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C340041FF801000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_STRAIGHT_JOIN_in_select_statement_component2448 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C340041FF801000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_SQL_SMALL_RESULT_in_select_statement_component2459 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C340041FF801000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_SQL_BIG_RESULT_in_select_statement_component2462 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C340041FF801000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_SQL_BUFFER_RESULT_in_select_statement_component2465 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C340041FF801000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_set_in_select_statement_component2476 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C340041FF801000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_SQL_CALC_FOUND_ROWS_in_select_statement_component2485 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C340041FF801000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_f_inc_in_select_statement_component2492 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C340041FF801000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_f_nl_in_select_statement_component2494 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C340041FF801000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_select_expr_in_select_statement_component2496 = new BitSet(new ulong[]{0x0000000000001000UL,0x0000000000000000UL,0x0000002E00000000UL});
    public static readonly BitSet FOLLOW_COMMA_in_select_statement_component2499 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C340041FF801000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_f_nl_in_select_statement_component2501 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C340041FF801000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_select_expr_in_select_statement_component2503 = new BitSet(new ulong[]{0x0000000000001000UL,0x0000000000000000UL,0x0000002E00000000UL});
    public static readonly BitSet FOLLOW_f_dec_in_select_statement_component2507 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000002E00000000UL});
    public static readonly BitSet FOLLOW_f_nl_in_select_statement_component2509 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x0000002E00000000UL});
    public static readonly BitSet FOLLOW_f_nl_in_select_statement_component2516 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000200000000UL});
    public static readonly BitSet FOLLOW_FROM_in_select_statement_component2518 = new BitSet(new ulong[]{0x0000000180000800UL,0x0000000000000000UL,0x0000008000000000UL});
    public static readonly BitSet FOLLOW_table_references_in_select_statement_component2520 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x0000002C00000000UL});
    public static readonly BitSet FOLLOW_f_nl_in_select_statement_component2529 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000400000000UL});
    public static readonly BitSet FOLLOW_WHERE_in_select_statement_component2531 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C34000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_condition_in_select_statement_component2533 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x0000002800000000UL});
    public static readonly BitSet FOLLOW_f_nl_in_select_statement_component2542 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000800000000UL});
    public static readonly BitSet FOLLOW_GROUP_in_select_statement_component2544 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000004000UL});
    public static readonly BitSet FOLLOW_BY_in_select_statement_component2546 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C34000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_selectby_expr_in_select_statement_component2548 = new BitSet(new ulong[]{0x0000000020001002UL,0x0000000000000000UL,0x0000002000000000UL});
    public static readonly BitSet FOLLOW_COMMA_in_select_statement_component2551 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C34000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_selectby_expr_in_select_statement_component2553 = new BitSet(new ulong[]{0x0000000020001002UL,0x0000000000000000UL,0x0000002000000000UL});
    public static readonly BitSet FOLLOW_WITH_in_select_statement_component2558 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000001000000000UL});
    public static readonly BitSet FOLLOW_ROLLUP_in_select_statement_component2560 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x0000002000000000UL});
    public static readonly BitSet FOLLOW_f_nl_in_select_statement_component2571 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000002000000000UL});
    public static readonly BitSet FOLLOW_HAVING_in_select_statement_component2573 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C34000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_condition_in_select_statement_component2575 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_expr_in_selectby_expr2589 = new BitSet(new ulong[]{0x0000000006000002UL});
    public static readonly BitSet FOLLOW_set_in_selectby_expr2591 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_expr_in_select_expr2607 = new BitSet(new ulong[]{0x0000000180000002UL,0x0000000000000000UL,0x0000000000000100UL});
    public static readonly BitSet FOLLOW_AS_in_select_expr2610 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_id_in_select_expr2613 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_id_in_select_expr2623 = new BitSet(new ulong[]{0x0000000000000000UL,0x8000000000000000UL});
    public static readonly BitSet FOLLOW_DOT_in_select_expr2625 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000004000000000UL});
    public static readonly BitSet FOLLOW_ASTERISK_in_select_expr2627 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_ASTERISK_in_select_expr2635 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_f_inc_in_table_references2647 = new BitSet(new ulong[]{0x0000000180000800UL,0x0000000000000000UL,0x0000008000000000UL});
    public static readonly BitSet FOLLOW_table_reference_in_table_references2649 = new BitSet(new ulong[]{0x0000000000001000UL});
    public static readonly BitSet FOLLOW_COMMA_in_table_references2652 = new BitSet(new ulong[]{0x0000000180000800UL,0x0000000000000000UL,0x0000008000000000UL});
    public static readonly BitSet FOLLOW_table_reference_in_table_references2654 = new BitSet(new ulong[]{0x0000000000001000UL});
    public static readonly BitSet FOLLOW_f_dec_in_table_references2658 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_table_factor_in_table_reference2675 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x00007D0004000000UL});
    public static readonly BitSet FOLLOW_join_table_in_table_reference2677 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x00007D0004000000UL});
    public static readonly BitSet FOLLOW_id_in_table_factor2689 = new BitSet(new ulong[]{0x0000000180000002UL,0x0000000000000000UL,0x0003800000000100UL});
    public static readonly BitSet FOLLOW_AS_in_table_factor2692 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_id_in_table_factor2695 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x0003800000000000UL});
    public static readonly BitSet FOLLOW_index_hint_in_table_factor2699 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_table_subquery_in_table_factor2708 = new BitSet(new ulong[]{0x0000000180000000UL,0x0000000000000000UL,0x0000000000000100UL});
    public static readonly BitSet FOLLOW_AS_in_table_factor2710 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_id_in_table_factor2713 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_LPAREN_in_table_factor2721 = new BitSet(new ulong[]{0x0000000180000800UL,0x0000000000000000UL,0x0000008000000000UL});
    public static readonly BitSet FOLLOW_table_references_in_table_factor2723 = new BitSet(new ulong[]{0x0000000000002000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_table_factor2725 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_OJ_in_table_factor2733 = new BitSet(new ulong[]{0x0000000180000800UL,0x0000000000000000UL,0x0000008000000000UL});
    public static readonly BitSet FOLLOW_table_factor_in_table_factor2735 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000010000000000UL});
    public static readonly BitSet FOLLOW_LEFT_in_table_factor2737 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000020000000000UL});
    public static readonly BitSet FOLLOW_OUTER_in_table_factor2739 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000040000000000UL});
    public static readonly BitSet FOLLOW_JOIN_in_table_factor2741 = new BitSet(new ulong[]{0x0000000180000800UL,0x0000000000000000UL,0x0000008000000000UL});
    public static readonly BitSet FOLLOW_table_factor_in_table_factor2743 = new BitSet(new ulong[]{0x0000001000000000UL});
    public static readonly BitSet FOLLOW_ON_in_table_factor2745 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C34000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_condition_in_table_factor2747 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_f_nl_in_join_table2759 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x00007D0004000000UL});
    public static readonly BitSet FOLLOW_INNER_in_join_table2771 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000040004000000UL});
    public static readonly BitSet FOLLOW_CROSS_in_join_table2775 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000040004000000UL});
    public static readonly BitSet FOLLOW_LEFT_in_join_table2779 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000060004000000UL});
    public static readonly BitSet FOLLOW_OUTER_in_join_table2781 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000040004000000UL});
    public static readonly BitSet FOLLOW_RIGHT_in_join_table2786 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000060004000000UL});
    public static readonly BitSet FOLLOW_OUTER_in_join_table2788 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000040004000000UL});
    public static readonly BitSet FOLLOW_NATURAL_in_join_table2801 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000250004000000UL});
    public static readonly BitSet FOLLOW_set_in_join_table2804 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000060004000000UL});
    public static readonly BitSet FOLLOW_OUTER_in_join_table2810 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000040004000000UL});
    public static readonly BitSet FOLLOW_set_in_join_table2826 = new BitSet(new ulong[]{0x0000000180000800UL,0x0000000000000000UL,0x0000008000000000UL});
    public static readonly BitSet FOLLOW_table_factor_in_join_table2832 = new BitSet(new ulong[]{0x0000001000800002UL});
    public static readonly BitSet FOLLOW_join_condition_in_join_table2834 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_ON_in_join_condition2875 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C34000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_condition_in_join_condition2877 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_USING_in_join_condition2883 = new BitSet(new ulong[]{0x0000000000000800UL});
    public static readonly BitSet FOLLOW_LPAREN_in_join_condition2885 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_id_in_join_condition2887 = new BitSet(new ulong[]{0x0000000000003000UL});
    public static readonly BitSet FOLLOW_COMMA_in_join_condition2890 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_id_in_join_condition2892 = new BitSet(new ulong[]{0x0000000000003000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_join_condition2896 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_USE_in_index_hint2910 = new BitSet(new ulong[]{0x0000000000180000UL});
    public static readonly BitSet FOLLOW_set_in_index_hint2912 = new BitSet(new ulong[]{0x0000000000000800UL,0x0000000000000000UL,0x0000000000020000UL});
    public static readonly BitSet FOLLOW_FOR_in_index_hint2919 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000040000000000UL});
    public static readonly BitSet FOLLOW_JOIN_in_index_hint2921 = new BitSet(new ulong[]{0x0000000000000800UL});
    public static readonly BitSet FOLLOW_LPAREN_in_index_hint2925 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_id_in_index_hint2927 = new BitSet(new ulong[]{0x0000000000003000UL});
    public static readonly BitSet FOLLOW_COMMA_in_index_hint2930 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_id_in_index_hint2932 = new BitSet(new ulong[]{0x0000000000003000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_index_hint2936 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IGNORE_in_index_hint2942 = new BitSet(new ulong[]{0x0000000000180000UL});
    public static readonly BitSet FOLLOW_set_in_index_hint2944 = new BitSet(new ulong[]{0x0000000000000800UL,0x0000000000000000UL,0x0000000000020000UL});
    public static readonly BitSet FOLLOW_FOR_in_index_hint2951 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000040000000000UL});
    public static readonly BitSet FOLLOW_JOIN_in_index_hint2953 = new BitSet(new ulong[]{0x0000000000000800UL});
    public static readonly BitSet FOLLOW_LPAREN_in_index_hint2957 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_id_in_index_hint2959 = new BitSet(new ulong[]{0x0000000000003000UL});
    public static readonly BitSet FOLLOW_COMMA_in_index_hint2962 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_id_in_index_hint2964 = new BitSet(new ulong[]{0x0000000000003000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_index_hint2968 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_FORCE_in_index_hint2974 = new BitSet(new ulong[]{0x0000000000180000UL});
    public static readonly BitSet FOLLOW_set_in_index_hint2976 = new BitSet(new ulong[]{0x0000000000000800UL,0x0000000000000000UL,0x0000000000020000UL});
    public static readonly BitSet FOLLOW_FOR_in_index_hint2983 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000040000000000UL});
    public static readonly BitSet FOLLOW_JOIN_in_index_hint2985 = new BitSet(new ulong[]{0x0000000000000800UL});
    public static readonly BitSet FOLLOW_LPAREN_in_index_hint2989 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_id_in_index_hint2991 = new BitSet(new ulong[]{0x0000000000003000UL});
    public static readonly BitSet FOLLOW_COMMA_in_index_hint2994 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_id_in_index_hint2996 = new BitSet(new ulong[]{0x0000000000003000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_index_hint3000 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_LPAREN_in_table_subquery3014 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000400000UL});
    public static readonly BitSet FOLLOW_select_statement_in_table_subquery3016 = new BitSet(new ulong[]{0x0000000000002000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_table_subquery3018 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_CURRENT_USER_in_user3029 = new BitSet(new ulong[]{0x0000000000000802UL});
    public static readonly BitSet FOLLOW_LPAREN_in_user3033 = new BitSet(new ulong[]{0x0000000000002000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_user3035 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_id_in_user3046 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0008000000000000UL});
    public static readonly BitSet FOLLOW_AT_in_user3048 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_id_in_user3050 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_T_STRING_in_default_value_expr3064 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_T_INTEGER_in_default_value_expr3074 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_T_BINARYNUM_in_default_value_expr3084 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_T_FLOAT_in_default_value_expr3094 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_NULL_in_default_value_expr3102 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_CURRENT_TIMESTAMP_in_default_value_expr3111 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_CURRENT_DATE_in_default_value_expr3119 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_case_expr1_in_case_expr3131 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_case_expr2_in_case_expr3135 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_CASE_in_case_expr13142 = new BitSet(new ulong[]{0x0000024401000000UL,0x5000000000000000UL,0x0400000000000000UL});
    public static readonly BitSet FOLLOW_value_in_case_expr13144 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0040000000000000UL});
    public static readonly BitSet FOLLOW_WHEN_in_case_expr13151 = new BitSet(new ulong[]{0x0000024401000000UL,0x5000000000000000UL,0x0400000000000000UL});
    public static readonly BitSet FOLLOW_value_in_case_expr13153 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0080000000000000UL});
    public static readonly BitSet FOLLOW_THEN_in_case_expr13155 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C34000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_expr_in_case_expr13157 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0340000000000000UL});
    public static readonly BitSet FOLLOW_ELSE_in_case_expr13166 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C34000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_expr_in_case_expr13168 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0200000000000000UL});
    public static readonly BitSet FOLLOW_END_in_case_expr13176 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0020000000000000UL});
    public static readonly BitSet FOLLOW_CASE_in_case_expr13178 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_CASE_in_case_expr23184 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0040000000000000UL});
    public static readonly BitSet FOLLOW_WHEN_in_case_expr23191 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C34000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_condition_in_case_expr23193 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0080000000000000UL});
    public static readonly BitSet FOLLOW_THEN_in_case_expr23195 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C34000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_expr_in_case_expr23197 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0340000000000000UL});
    public static readonly BitSet FOLLOW_ELSE_in_case_expr23206 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C34000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_expr_in_case_expr23208 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0200000000000000UL});
    public static readonly BitSet FOLLOW_END_in_case_expr23216 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0020000000000000UL});
    public static readonly BitSet FOLLOW_CASE_in_case_expr23218 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_set_in_value0 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_condition_in_expr3256 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_LPAREN_in_arg_list3263 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000800000UL});
    public static readonly BitSet FOLLOW_DISTINCT_in_arg_list3265 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C34004000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_ASTERISK_in_arg_list3268 = new BitSet(new ulong[]{0x0000000000002000UL});
    public static readonly BitSet FOLLOW_expr_in_arg_list3272 = new BitSet(new ulong[]{0x0000000000002000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_arg_list3275 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_LPAREN_in_arg_list3279 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C34000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_expr_in_arg_list3281 = new BitSet(new ulong[]{0x0000000000003000UL});
    public static readonly BitSet FOLLOW_COMMA_in_arg_list3284 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C34000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_expr_in_arg_list3286 = new BitSet(new ulong[]{0x0000000000003000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_arg_list3291 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_LPAREN_in_arg_list3295 = new BitSet(new ulong[]{0x0000000000002000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_arg_list3297 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_id_in_func_call3304 = new BitSet(new ulong[]{0x0000000000000800UL,0x8000000000000000UL});
    public static readonly BitSet FOLLOW_sysname_in_func_call3306 = new BitSet(new ulong[]{0x0000000000000800UL,0x8000000000000000UL});
    public static readonly BitSet FOLLOW_DOT_in_func_call3310 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_id_in_func_call3312 = new BitSet(new ulong[]{0x0000000000000800UL,0x8000000000000000UL});
    public static readonly BitSet FOLLOW_arg_list_in_func_call3316 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_id_in_expr_elem3326 = new BitSet(new ulong[]{0x0000000000000002UL,0x8000000000000000UL});
    public static readonly BitSet FOLLOW_DOT_in_expr_elem3329 = new BitSet(new ulong[]{0x0000000180000000UL});
    public static readonly BitSet FOLLOW_id_in_expr_elem3331 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_func_call_in_expr_elem3341 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_value_in_expr_elem3349 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_LPAREN_in_expr_elem3357 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C34000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_expr_in_expr_elem3359 = new BitSet(new ulong[]{0x0000000000002000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_expr_elem3361 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_LPAREN_in_expr_elem3369 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000400000UL});
    public static readonly BitSet FOLLOW_select_statement_in_expr_elem3371 = new BitSet(new ulong[]{0x0000000000002000UL});
    public static readonly BitSet FOLLOW_RPAREN_in_expr_elem3373 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_INTERVAL_in_expr_elem3381 = new BitSet(new ulong[]{0x0000000001000000UL});
    public static readonly BitSet FOLLOW_T_INTEGER_in_expr_elem3383 = new BitSet(new ulong[]{0x0000004180000000UL,0x0000000000008000UL,0x0414000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_id_in_expr_elem3386 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_sysname_in_expr_elem3390 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_EXCLAMATION_in_exclamation_expr3398 = new BitSet(new ulong[]{0x0000024581000800UL,0x5000000000008000UL,0x7C14000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_expr_elem_in_exclamation_expr3401 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_set_in_unary_expr3407 = new BitSet(new ulong[]{0x0000024581000800UL,0x5000000000008000UL,0x7C14000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_exclamation_expr_in_unary_expr3414 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_unary_expr_in_pow_expr3420 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x8000000000000000UL});
    public static readonly BitSet FOLLOW_ARROW_UP_in_pow_expr3423 = new BitSet(new ulong[]{0x0000024581000800UL,0x5000000000008000UL,0x7C14000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_unary_expr_in_pow_expr3425 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_pow_expr_in_mul_expr3433 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x0000004000000000UL,0x000000000000000FUL});
    public static readonly BitSet FOLLOW_set_in_mul_expr3436 = new BitSet(new ulong[]{0x0000024581000800UL,0x5000000000008000UL,0x7C14000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_pow_expr_in_mul_expr3456 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x0000004000000000UL,0x000000000000000FUL});
    public static readonly BitSet FOLLOW_mul_expr_in_add_expr3464 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x2000000000000000UL,0x0000000000000010UL});
    public static readonly BitSet FOLLOW_set_in_add_expr3468 = new BitSet(new ulong[]{0x0000024581000800UL,0x5000000000008000UL,0x7C14000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_mul_expr_in_add_expr3476 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x2000000000000000UL,0x0000000000000010UL});
    public static readonly BitSet FOLLOW_add_expr_in_shift_expr3484 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x0000000000000000UL,0x0000000000000060UL});
    public static readonly BitSet FOLLOW_set_in_shift_expr3487 = new BitSet(new ulong[]{0x0000024581000800UL,0x5000000000008000UL,0x7C14000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_add_expr_in_shift_expr3495 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x0000000000000000UL,0x0000000000000060UL});
    public static readonly BitSet FOLLOW_shift_expr_in_bitand_expr3503 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x0000000000000000UL,0x0000000000000080UL});
    public static readonly BitSet FOLLOW_AMPERSAND_in_bitand_expr3506 = new BitSet(new ulong[]{0x0000024581000800UL,0x5000000000008000UL,0x7C14000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_shift_expr_in_bitand_expr3508 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x0000000000000000UL,0x0000000000000080UL});
    public static readonly BitSet FOLLOW_bitand_expr_in_bitor_expr3516 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x0000000000000000UL,0x0000000000000100UL});
    public static readonly BitSet FOLLOW_PIPE_in_bitor_expr3519 = new BitSet(new ulong[]{0x0000024581000800UL,0x5000000000008000UL,0x7C14000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_bitand_expr_in_bitor_expr3521 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x0000000000000000UL,0x0000000000000100UL});
    public static readonly BitSet FOLLOW_bitor_expr_in_rel_expr3529 = new BitSet(new ulong[]{0x0000000010000002UL,0x0000000000000000UL,0x0000000000080000UL,0x000000000007FE00UL});
    public static readonly BitSet FOLLOW_set_in_rel_expr3533 = new BitSet(new ulong[]{0x0000024581000800UL,0x5000000000008000UL,0x7C14000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_bitor_expr_in_rel_expr3585 = new BitSet(new ulong[]{0x0000000010000002UL,0x0000000000000000UL,0x0000000000080000UL,0x000000000007FE00UL});
    public static readonly BitSet FOLLOW_rel_expr_in_between_expr3593 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x0000000000000000UL,0x0000000000080000UL});
    public static readonly BitSet FOLLOW_BETWEEN_in_between_expr3596 = new BitSet(new ulong[]{0x0000024581000800UL,0x5000000000008000UL,0x7C14000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_rel_expr_in_between_expr3598 = new BitSet(new ulong[]{0x0000000000000000UL,0x0000000000000000UL,0x0000000000000000UL,0x0000000000100000UL});
    public static readonly BitSet FOLLOW_AND_in_between_expr3600 = new BitSet(new ulong[]{0x0000024581000800UL,0x5000000000008000UL,0x7C14000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_rel_expr_in_between_expr3602 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_between_expr_in_casegroup_expr3610 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_case_expr_in_casegroup_expr3614 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_NOT_in_not_expr3621 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C34000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_casegroup_expr_in_not_expr3624 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_not_expr_in_and_expr3630 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x0000000000000000UL,0x0000000000300000UL});
    public static readonly BitSet FOLLOW_set_in_and_expr3634 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C34000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_not_expr_in_and_expr3642 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x0000000000000000UL,0x0000000000300000UL});
    public static readonly BitSet FOLLOW_and_expr_in_xor_expr3650 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x0000000000000000UL,0x0000000000400000UL});
    public static readonly BitSet FOLLOW_XOR_in_xor_expr3654 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C34000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_and_expr_in_xor_expr3656 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x0000000000000000UL,0x0000000000400000UL});
    public static readonly BitSet FOLLOW_xor_expr_in_or_expr3665 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x0000000000000001UL,0x0000000000800000UL});
    public static readonly BitSet FOLLOW_set_in_or_expr3669 = new BitSet(new ulong[]{0x0000024781000800UL,0x5000000000008000UL,0x7C34000000000000UL,0x0000000000000000UL,0x0000200000000000UL});
    public static readonly BitSet FOLLOW_xor_expr_in_or_expr3677 = new BitSet(new ulong[]{0x0000000000000002UL,0x0000000000000000UL,0x0000000000000001UL,0x0000000000800000UL});
    public static readonly BitSet FOLLOW_or_expr_in_condition3687 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_set_in_operator_no_dot0 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_DOT_in_any_operator4220 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_operator_no_dot_in_any_operator4224 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_set_in_keyword0 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_set_in_sysname0 = new BitSet(new ulong[]{0x0000000000000002UL});

}
