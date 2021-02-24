// $ANTLR 3.2 Sep 23, 2009 12:02:23 ..\\Plugin.oracle\\AntlrParser\\Oracle.g 2012-11-12 20:29:25

// The variable 'variable' is assigned but its value is never used.
#pragma warning disable 168, 219
// Unreachable code detected.
#pragma warning disable 162


    using System.Globalization;
    using DatAdmin;
    using Plugin.oracle;


using System;
using Antlr.Runtime;
using IList 		= System.Collections.IList;
using ArrayList 	= System.Collections.ArrayList;
using Stack 		= Antlr.Runtime.Collections.StackList;



using Antlr.Runtime.Tree;

public partial class OracleParser : Parser
{
    public static readonly string[] tokenNames = new string[] 
	{
        "<invalid>", 
		"<EOR>", 
		"<DOWN>", 
		"<UP>", 
		"T_IDENT", 
		"T_QUOTED_IDENT", 
		"EQUALS", 
		"SAFEEQUALS", 
		"EQUALS2", 
		"NOT_EQUALS", 
		"NOT_EQUALS2", 
		"LESS", 
		"LESS_OR_EQ", 
		"GREATER", 
		"GREATER_OR_EQ", 
		"SHIFT_LEFT", 
		"SHIFT_RIGHT", 
		"AMPERSAND", 
		"DOUBLE_AMPERSAND", 
		"PIPE", 
		"DOUBLE_PIPE", 
		"PLUS", 
		"MINUS", 
		"TILDA", 
		"ASTERISK", 
		"SLASH", 
		"PERCENT", 
		"SEMI", 
		"DOT", 
		"COMMA", 
		"LPAREN", 
		"RPAREN", 
		"QUESTION", 
		"EXCLAMATION", 
		"COLON", 
		"AT", 
		"DOLLAR", 
		"ARROW_UP", 
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
		"ADD", 
		"ALTER", 
		"AND", 
		"AS", 
		"ASC", 
		"BEGIN", 
		"BETWEEN", 
		"BY", 
		"CASCADE", 
		"CASE", 
		"CAST", 
		"CHECK", 
		"COLLATE", 
		"COLUMN", 
		"COMMIT", 
		"CONFLICT", 
		"CONSTRAINT", 
		"CREATE", 
		"CROSS", 
		"CURRENT_TIME", 
		"CURRENT_DATE", 
		"CURRENT_TIMESTAMP", 
		"UTC_TIMESTAMP", 
		"DATABASE", 
		"DEFAULT", 
		"DELETE", 
		"DESC", 
		"DISTINCT", 
		"DROP", 
		"ELSE", 
		"END", 
		"ESCAPE", 
		"EXCEPT", 
		"EXCLUSIVE", 
		"EXISTS", 
		"EXPLAIN", 
		"FOR", 
		"FOREIGN", 
		"FROM", 
		"GROUP", 
		"HAVING", 
		"IF", 
		"IN", 
		"INDEX", 
		"INNER", 
		"INSERT", 
		"INTERSECT", 
		"INTO", 
		"IS", 
		"JOIN", 
		"KEY", 
		"LEFT", 
		"LIKE", 
		"NOT", 
		"NULL", 
		"OF", 
		"ON", 
		"OR", 
		"ORDER", 
		"OUTER", 
		"PRIMARY", 
		"REFERENCES", 
		"ROLLBACK", 
		"SELECT", 
		"SET", 
		"TABLE", 
		"TEMPORARY", 
		"TEMP", 
		"THEN", 
		"TO", 
		"TRANSACTION", 
		"TRIGGER", 
		"UNION", 
		"UNIQUE", 
		"UPDATE", 
		"VALUES", 
		"VIEW", 
		"WHEN", 
		"WHERE", 
		"WITH", 
		"PARSER", 
		"XOR", 
		"ID_START", 
		"T_NSTRING", 
		"T_STRING", 
		"T_INTEGER", 
		"FLOAT_EXP", 
		"T_FLOAT", 
		"T_BLOB", 
		"T_BINARYNUM", 
		"T_COMMENT", 
		"LINE_COMMENT", 
		"WHITESPACE"
    };

    public const int CAST = 74;
    public const int T_STRING = 148;
    public const int TRIGGER = 135;
    public const int CURRENT_TIME = 83;
    public const int CASE = 73;
    public const int EQUALS = 6;
    public const int T_IDENT = 4;
    public const int NOT = 117;
    public const int T_INTEGER = 149;
    public const int EXCEPT = 96;
    public const int CASCADE = 72;
    public const int FOREIGN = 101;
    public const int EOF = -1;
    public const int EXPLAIN = 99;
    public const int RPAREN = 31;
    public const int CREATE = 81;
    public const int GREATER = 13;
    public const int INSERT = 109;
    public const int ESCAPE = 95;
    public const int EXCLAMATION = 33;
    public const int BEGIN = 69;
    public const int LESS = 11;
    public const int CONFLICT = 79;
    public const int SELECT = 127;
    public const int LESS_OR_EQ = 12;
    public const int INTO = 111;
    public const int D = 41;
    public const int E = 42;
    public const int UNIQUE = 137;
    public const int F = 43;
    public const int G = 44;
    public const int A = 38;
    public const int VIEW = 140;
    public const int B = 39;
    public const int C = 40;
    public const int ASC = 68;
    public const int LINE_COMMENT = 155;
    public const int L = 49;
    public const int SAFEEQUALS = 7;
    public const int M = 50;
    public const int N = 51;
    public const int T_BINARYNUM = 153;
    public const int TRANSACTION = 134;
    public const int KEY = 114;
    public const int O = 52;
    public const int TEMP = 131;
    public const int H = 45;
    public const int NULL = 118;
    public const int I = 46;
    public const int ELSE = 93;
    public const int J = 47;
    public const int K = 48;
    public const int T_FLOAT = 151;
    public const int U = 58;
    public const int ON = 120;
    public const int T = 57;
    public const int WHITESPACE = 156;
    public const int W = 60;
    public const int T_NSTRING = 147;
    public const int V = 59;
    public const int PRIMARY = 124;
    public const int Q = 54;
    public const int P = 53;
    public const int DELETE = 89;
    public const int S = 56;
    public const int R = 55;
    public const int ROLLBACK = 126;
    public const int OF = 119;
    public const int Y = 62;
    public const int X = 61;
    public const int Z = 63;
    public const int SHIFT_LEFT = 15;
    public const int INTERSECT = 110;
    public const int GROUP = 103;
    public const int SHIFT_RIGHT = 16;
    public const int OR = 121;
    public const int CHECK = 75;
    public const int FROM = 102;
    public const int END = 94;
    public const int PARSER = 144;
    public const int TEMPORARY = 130;
    public const int CONSTRAINT = 80;
    public const int DISTINCT = 91;
    public const int CURRENT_DATE = 84;
    public const int DOLLAR = 36;
    public const int WHERE = 142;
    public const int ALTER = 65;
    public const int INNER = 108;
    public const int UTC_TIMESTAMP = 86;
    public const int ORDER = 122;
    public const int UPDATE = 138;
    public const int TABLE = 129;
    public const int FOR = 100;
    public const int EXCLUSIVE = 97;
    public const int AND = 66;
    public const int DOUBLE_AMPERSAND = 18;
    public const int NOT_EQUALS = 9;
    public const int CROSS = 82;
    public const int T_COMMENT = 154;
    public const int LPAREN = 30;
    public const int ASTERISK = 24;
    public const int IF = 105;
    public const int GREATER_OR_EQ = 14;
    public const int AT = 35;
    public const int DOUBLE_PIPE = 20;
    public const int INDEX = 107;
    public const int AS = 67;
    public const int TILDA = 23;
    public const int SLASH = 25;
    public const int THEN = 132;
    public const int IN = 106;
    public const int REFERENCES = 125;
    public const int COMMA = 29;
    public const int IS = 112;
    public const int LEFT = 115;
    public const int COLUMN = 77;
    public const int PLUS = 21;
    public const int PIPE = 19;
    public const int EXISTS = 98;
    public const int DOT = 28;
    public const int CURRENT_TIMESTAMP = 85;
    public const int WITH = 143;
    public const int LIKE = 116;
    public const int ADD = 64;
    public const int COLLATE = 76;
    public const int OUTER = 123;
    public const int ARROW_UP = 37;
    public const int BY = 71;
    public const int XOR = 145;
    public const int EQUALS2 = 8;
    public const int PERCENT = 26;
    public const int TO = 133;
    public const int NOT_EQUALS2 = 10;
    public const int DEFAULT = 88;
    public const int VALUES = 139;
    public const int AMPERSAND = 17;
    public const int SET = 128;
    public const int HAVING = 104;
    public const int MINUS = 22;
    public const int SEMI = 27;
    public const int JOIN = 113;
    public const int UNION = 136;
    public const int COLON = 34;
    public const int T_QUOTED_IDENT = 5;
    public const int FLOAT_EXP = 150;
    public const int COMMIT = 78;
    public const int QUESTION = 32;
    public const int DATABASE = 87;
    public const int DROP = 92;
    public const int WHEN = 141;
    public const int T_BLOB = 152;
    public const int DESC = 90;
    public const int ID_START = 146;
    public const int BETWEEN = 70;

    // delegates
    // delegators



        public OracleParser(ITokenStream input)
    		: this(input, new RecognizerSharedState()) {
        }

        public OracleParser(ITokenStream input, RecognizerSharedState state)
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
		get { return OracleParser.tokenNames; }
    }

    override public string GrammarFileName {
		get { return "..\\Plugin.oracle\\AntlrParser\\Oracle.g"; }
    }


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
    // ..\\Plugin.oracle\\AntlrParser\\Oracle.g:14:1: id : ( T_IDENT | T_QUOTED_IDENT );
    public OracleParser.id_return id() // throws RecognitionException [1]
    {   
        OracleParser.id_return retval = new OracleParser.id_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken set1 = null;

        object set1_tree=null;

        try 
    	{
            // ..\\Plugin.oracle\\AntlrParser\\Oracle.g:14:3: ( T_IDENT | T_QUOTED_IDENT )
            // ..\\Plugin.oracle\\AntlrParser\\Oracle.g:
            {
            	root_0 = (object)adaptor.GetNilNode();

            	set1 = (IToken)input.LT(1);
            	if ( (input.LA(1) >= T_IDENT && input.LA(1) <= T_QUOTED_IDENT) ) 
            	{
            	    input.Consume();
            	    adaptor.AddChild(root_0, (object)adaptor.Create(set1));
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
    // ..\\Plugin.oracle\\AntlrParser\\Oracle.g:52:1: operator_no_dot : ( EQUALS | SAFEEQUALS | EQUALS2 | NOT_EQUALS | NOT_EQUALS2 | LESS | LESS_OR_EQ | GREATER | GREATER_OR_EQ | SHIFT_LEFT | SHIFT_RIGHT | AMPERSAND | DOUBLE_AMPERSAND | PIPE | DOUBLE_PIPE | PLUS | MINUS | TILDA | ASTERISK | SLASH | PERCENT | SEMI | COMMA | LPAREN | RPAREN | QUESTION | EXCLAMATION | COLON | AT | DOLLAR | ARROW_UP );
    public OracleParser.operator_no_dot_return operator_no_dot() // throws RecognitionException [1]
    {   
        OracleParser.operator_no_dot_return retval = new OracleParser.operator_no_dot_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken set2 = null;

        object set2_tree=null;

        try 
    	{
            // ..\\Plugin.oracle\\AntlrParser\\Oracle.g:52:17: ( EQUALS | SAFEEQUALS | EQUALS2 | NOT_EQUALS | NOT_EQUALS2 | LESS | LESS_OR_EQ | GREATER | GREATER_OR_EQ | SHIFT_LEFT | SHIFT_RIGHT | AMPERSAND | DOUBLE_AMPERSAND | PIPE | DOUBLE_PIPE | PLUS | MINUS | TILDA | ASTERISK | SLASH | PERCENT | SEMI | COMMA | LPAREN | RPAREN | QUESTION | EXCLAMATION | COLON | AT | DOLLAR | ARROW_UP )
            // ..\\Plugin.oracle\\AntlrParser\\Oracle.g:
            {
            	root_0 = (object)adaptor.GetNilNode();

            	set2 = (IToken)input.LT(1);
            	if ( (input.LA(1) >= EQUALS && input.LA(1) <= SEMI) || (input.LA(1) >= COMMA && input.LA(1) <= ARROW_UP) ) 
            	{
            	    input.Consume();
            	    adaptor.AddChild(root_0, (object)adaptor.Create(set2));
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
    // ..\\Plugin.oracle\\AntlrParser\\Oracle.g:59:1: any_operator : ( DOT | operator_no_dot );
    public OracleParser.any_operator_return any_operator() // throws RecognitionException [1]
    {   
        OracleParser.any_operator_return retval = new OracleParser.any_operator_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken DOT3 = null;
        OracleParser.operator_no_dot_return operator_no_dot4 = default(OracleParser.operator_no_dot_return);


        object DOT3_tree=null;

        try 
    	{
            // ..\\Plugin.oracle\\AntlrParser\\Oracle.g:59:13: ( DOT | operator_no_dot )
            int alt1 = 2;
            int LA1_0 = input.LA(1);

            if ( (LA1_0 == DOT) )
            {
                alt1 = 1;
            }
            else if ( ((LA1_0 >= EQUALS && LA1_0 <= SEMI) || (LA1_0 >= COMMA && LA1_0 <= ARROW_UP)) )
            {
                alt1 = 2;
            }
            else 
            {
                NoViableAltException nvae_d1s0 =
                    new NoViableAltException("", 1, 0, input);

                throw nvae_d1s0;
            }
            switch (alt1) 
            {
                case 1 :
                    // ..\\Plugin.oracle\\AntlrParser\\Oracle.g:59:15: DOT
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	DOT3=(IToken)Match(input,DOT,FOLLOW_DOT_in_any_operator569); 
                    		DOT3_tree = (object)adaptor.Create(DOT3);
                    		adaptor.AddChild(root_0, DOT3_tree);


                    }
                    break;
                case 2 :
                    // ..\\Plugin.oracle\\AntlrParser\\Oracle.g:59:21: operator_no_dot
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_operator_no_dot_in_any_operator573);
                    	operator_no_dot4 = operator_no_dot();
                    	state.followingStackPointer--;

                    	adaptor.AddChild(root_0, operator_no_dot4.Tree);

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
    // ..\\Plugin.oracle\\AntlrParser\\Oracle.g:208:1: keyword : ( ADD | ALTER | AND | AS | ASC | BEGIN | BETWEEN | BY | CASCADE | CASE | CAST | CHECK | COLLATE | COLUMN | COMMIT | CONFLICT | CONSTRAINT | CREATE | CROSS | CURRENT_TIME | CURRENT_DATE | CURRENT_TIMESTAMP | UTC_TIMESTAMP | DATABASE | DEFAULT | DELETE | DESC | DISTINCT | DROP | ELSE | END | ESCAPE | EXCEPT | EXCLUSIVE | EXISTS | EXPLAIN | FOR | FOREIGN | FROM | GROUP | HAVING | IF | IN | INDEX | INNER | INSERT | INTERSECT | INTO | IS | JOIN | KEY | LEFT | LIKE | NOT | NULL | OF | ON | OR | ORDER | OUTER | PRIMARY | REFERENCES | ROLLBACK | SELECT | SET | TABLE | TEMPORARY | TEMP | THEN | TO | TRANSACTION | TRIGGER | UNION | UNIQUE | UPDATE | VALUES | VIEW | WHEN | WHERE | WITH | PARSER | XOR );
    public OracleParser.keyword_return keyword() // throws RecognitionException [1]
    {   
        OracleParser.keyword_return retval = new OracleParser.keyword_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken set5 = null;

        object set5_tree=null;

        try 
    	{
            // ..\\Plugin.oracle\\AntlrParser\\Oracle.g:208:9: ( ADD | ALTER | AND | AS | ASC | BEGIN | BETWEEN | BY | CASCADE | CASE | CAST | CHECK | COLLATE | COLUMN | COMMIT | CONFLICT | CONSTRAINT | CREATE | CROSS | CURRENT_TIME | CURRENT_DATE | CURRENT_TIMESTAMP | UTC_TIMESTAMP | DATABASE | DEFAULT | DELETE | DESC | DISTINCT | DROP | ELSE | END | ESCAPE | EXCEPT | EXCLUSIVE | EXISTS | EXPLAIN | FOR | FOREIGN | FROM | GROUP | HAVING | IF | IN | INDEX | INNER | INSERT | INTERSECT | INTO | IS | JOIN | KEY | LEFT | LIKE | NOT | NULL | OF | ON | OR | ORDER | OUTER | PRIMARY | REFERENCES | ROLLBACK | SELECT | SET | TABLE | TEMPORARY | TEMP | THEN | TO | TRANSACTION | TRIGGER | UNION | UNIQUE | UPDATE | VALUES | VIEW | WHEN | WHERE | WITH | PARSER | XOR )
            // ..\\Plugin.oracle\\AntlrParser\\Oracle.g:
            {
            	root_0 = (object)adaptor.GetNilNode();

            	set5 = (IToken)input.LT(1);
            	if ( (input.LA(1) >= ADD && input.LA(1) <= XOR) ) 
            	{
            	    input.Consume();
            	    adaptor.AddChild(root_0, (object)adaptor.Create(set5));
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
    // ..\\Plugin.oracle\\AntlrParser\\Oracle.g:211:1: sysname : ;
    public OracleParser.sysname_return sysname() // throws RecognitionException [1]
    {   
        OracleParser.sysname_return retval = new OracleParser.sysname_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        try 
    	{
            // ..\\Plugin.oracle\\AntlrParser\\Oracle.g:211:9: ()
            // ..\\Plugin.oracle\\AntlrParser\\Oracle.g:213:1: 
            {
            	root_0 = (object)adaptor.GetNilNode();

            }

            retval.Stop = input.LT(-1);

            	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);
        }
        finally 
    	{
        }
        return retval;
    }
    // $ANTLR end "sysname"

    // Delegated rules


	private void InitializeCyclicDFAs()
	{
	}

 

    public static readonly BitSet FOLLOW_set_in_id0 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_set_in_operator_no_dot0 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_DOT_in_any_operator569 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_operator_no_dot_in_any_operator573 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_set_in_keyword0 = new BitSet(new ulong[]{0x0000000000000002UL});

}
