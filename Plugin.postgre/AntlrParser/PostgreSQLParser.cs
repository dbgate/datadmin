// $ANTLR 3.2 Sep 23, 2009 12:02:23 ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g 2012-11-12 20:29:23

// The variable 'variable' is assigned but its value is never used.
#pragma warning disable 168, 219
// Unreachable code detected.
#pragma warning disable 162


    using System.Globalization;
    using DatAdmin;
    using Plugin.postgre;


using System;
using Antlr.Runtime;
using IList 		= System.Collections.IList;
using ArrayList 	= System.Collections.ArrayList;
using Stack 		= Antlr.Runtime.Collections.StackList;



using Antlr.Runtime.Tree;

public partial class PostgreSQLParser : Parser
{
    public static readonly string[] tokenNames = new string[] 
	{
        "<invalid>", 
		"<EOR>", 
		"<DOWN>", 
		"<UP>", 
		"CREATE", 
		"UNIQUE", 
		"INDEX", 
		"CONCURRENTLY", 
		"ON", 
		"USING", 
		"LPAREN", 
		"COMMA", 
		"RPAREN", 
		"DOT", 
		"T_IDENT", 
		"T_QUOTED_IDENT", 
		"C", 
		"R", 
		"E", 
		"A", 
		"T", 
		"I", 
		"N", 
		"D", 
		"X", 
		"U", 
		"Q", 
		"O", 
		"L", 
		"Y", 
		"S", 
		"G", 
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
		"QUESTION", 
		"EXCLAMATION", 
		"COLON", 
		"AT", 
		"DOLLAR", 
		"ARROW_UP", 
		"B", 
		"F", 
		"H", 
		"J", 
		"K", 
		"M", 
		"P", 
		"V", 
		"W", 
		"Z", 
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

    public const int DOLLAR = 58;
    public const int T_STRING = 72;
    public const int T_IDENT = 14;
    public const int EQUALS = 32;
    public const int T_INTEGER = 73;
    public const int DOUBLE_AMPERSAND = 44;
    public const int EOF = -1;
    public const int NOT_EQUALS = 35;
    public const int T_COMMENT = 78;
    public const int ASTERISK = 50;
    public const int LPAREN = 10;
    public const int GREATER_OR_EQ = 40;
    public const int AT = 57;
    public const int DOUBLE_PIPE = 46;
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
    public const int M = 65;
    public const int SAFEEQUALS = 33;
    public const int NOT_EQUALS2 = 36;
    public const int N = 22;
    public const int T_BINARYNUM = 77;
    public const int O = 27;
    public const int H = 62;
    public const int I = 21;
    public const int J = 63;
    public const int K = 64;
    public const int T_FLOAT = 75;
    public const int U = 25;
    public const int ON = 8;
    public const int T = 20;
    public const int AMPERSAND = 43;
    public const int WHITESPACE = 80;
    public const int W = 68;
    public const int T_NSTRING = 71;
    public const int V = 67;
    public const int Q = 26;
    public const int P = 66;
    public const int S = 30;
    public const int MINUS = 48;
    public const int R = 17;
    public const int SEMI = 53;
    public const int Y = 29;
    public const int X = 24;
    public const int Z = 69;
    public const int COLON = 56;
    public const int T_QUOTED_IDENT = 15;
    public const int FLOAT_EXP = 74;
    public const int SHIFT_LEFT = 41;
    public const int SHIFT_RIGHT = 42;
    public const int QUESTION = 54;
    public const int CONCURRENTLY = 7;
    public const int T_BLOB = 76;
    public const int ID_START = 70;

    // delegates
    // delegators



        public PostgreSQLParser(ITokenStream input)
    		: this(input, new RecognizerSharedState()) {
        }

        public PostgreSQLParser(ITokenStream input, RecognizerSharedState state)
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
		get { return PostgreSQLParser.tokenNames; }
    }

    override public string GrammarFileName {
		get { return "..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g"; }
    }


    public class create_index_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "create_index"
    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:14:1: create_index[IndexConstraint index] : CREATE ( UNIQUE )? INDEX ( CONCURRENTLY )? name= index_name ON table_name ( USING id )? LPAREN index_column[index] ( COMMA index_column[index] )* RPAREN ;
    public PostgreSQLParser.create_index_return create_index(IndexConstraint index) // throws RecognitionException [1]
    {   
        PostgreSQLParser.create_index_return retval = new PostgreSQLParser.create_index_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken CREATE1 = null;
        IToken UNIQUE2 = null;
        IToken INDEX3 = null;
        IToken CONCURRENTLY4 = null;
        IToken ON5 = null;
        IToken USING7 = null;
        IToken LPAREN9 = null;
        IToken COMMA11 = null;
        IToken RPAREN13 = null;
        PostgreSQLParser.index_name_return name = default(PostgreSQLParser.index_name_return);

        PostgreSQLParser.table_name_return table_name6 = default(PostgreSQLParser.table_name_return);

        PostgreSQLParser.id_return id8 = default(PostgreSQLParser.id_return);

        PostgreSQLParser.index_column_return index_column10 = default(PostgreSQLParser.index_column_return);

        PostgreSQLParser.index_column_return index_column12 = default(PostgreSQLParser.index_column_return);


        object CREATE1_tree=null;
        object UNIQUE2_tree=null;
        object INDEX3_tree=null;
        object CONCURRENTLY4_tree=null;
        object ON5_tree=null;
        object USING7_tree=null;
        object LPAREN9_tree=null;
        object COMMA11_tree=null;
        object RPAREN13_tree=null;

        try 
    	{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:14:36: ( CREATE ( UNIQUE )? INDEX ( CONCURRENTLY )? name= index_name ON table_name ( USING id )? LPAREN index_column[index] ( COMMA index_column[index] )* RPAREN )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:15:5: CREATE ( UNIQUE )? INDEX ( CONCURRENTLY )? name= index_name ON table_name ( USING id )? LPAREN index_column[index] ( COMMA index_column[index] )* RPAREN
            {
            	root_0 = (object)adaptor.GetNilNode();

            	CREATE1=(IToken)Match(input,CREATE,FOLLOW_CREATE_in_create_index37); 
            		CREATE1_tree = (object)adaptor.Create(CREATE1);
            		adaptor.AddChild(root_0, CREATE1_tree);

            	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:16:5: ( UNIQUE )?
            	int alt1 = 2;
            	int LA1_0 = input.LA(1);

            	if ( (LA1_0 == UNIQUE) )
            	{
            	    alt1 = 1;
            	}
            	switch (alt1) 
            	{
            	    case 1 :
            	        // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:16:6: UNIQUE
            	        {
            	        	UNIQUE2=(IToken)Match(input,UNIQUE,FOLLOW_UNIQUE_in_create_index44); 
            	        		UNIQUE2_tree = (object)adaptor.Create(UNIQUE2);
            	        		adaptor.AddChild(root_0, UNIQUE2_tree);

            	        	index.IsUnique=true;

            	        }
            	        break;

            	}

            	INDEX3=(IToken)Match(input,INDEX,FOLLOW_INDEX_in_create_index54); 
            		INDEX3_tree = (object)adaptor.Create(INDEX3);
            		adaptor.AddChild(root_0, INDEX3_tree);

            	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:18:5: ( CONCURRENTLY )?
            	int alt2 = 2;
            	int LA2_0 = input.LA(1);

            	if ( (LA2_0 == CONCURRENTLY) )
            	{
            	    alt2 = 1;
            	}
            	switch (alt2) 
            	{
            	    case 1 :
            	        // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:18:5: CONCURRENTLY
            	        {
            	        	CONCURRENTLY4=(IToken)Match(input,CONCURRENTLY,FOLLOW_CONCURRENTLY_in_create_index60); 
            	        		CONCURRENTLY4_tree = (object)adaptor.Create(CONCURRENTLY4);
            	        		adaptor.AddChild(root_0, CONCURRENTLY4_tree);


            	        }
            	        break;

            	}

            	PushFollow(FOLLOW_index_name_in_create_index70);
            	name = index_name();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, name.Tree);
            	index.Name=UnquoteName(((name != null) ? input.ToString((IToken)(name.Start),(IToken)(name.Stop)) : null));
            	ON5=(IToken)Match(input,ON,FOLLOW_ON_in_create_index79); 
            		ON5_tree = (object)adaptor.Create(ON5);
            		adaptor.AddChild(root_0, ON5_tree);

            	PushFollow(FOLLOW_table_name_in_create_index81);
            	table_name6 = table_name();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, table_name6.Tree);
            	index.SetDummyTable(((table_name6 != null) ? table_name6.result : default(NameWithSchema)));
            	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:21:5: ( USING id )?
            	int alt3 = 2;
            	int LA3_0 = input.LA(1);

            	if ( (LA3_0 == USING) )
            	{
            	    alt3 = 1;
            	}
            	switch (alt3) 
            	{
            	    case 1 :
            	        // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:21:6: USING id
            	        {
            	        	USING7=(IToken)Match(input,USING,FOLLOW_USING_in_create_index90); 
            	        		USING7_tree = (object)adaptor.Create(USING7);
            	        		adaptor.AddChild(root_0, USING7_tree);

            	        	PushFollow(FOLLOW_id_in_create_index92);
            	        	id8 = id();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, id8.Tree);

            	        }
            	        break;

            	}

            	LPAREN9=(IToken)Match(input,LPAREN,FOLLOW_LPAREN_in_create_index100); 
            		LPAREN9_tree = (object)adaptor.Create(LPAREN9);
            		adaptor.AddChild(root_0, LPAREN9_tree);

            	PushFollow(FOLLOW_index_column_in_create_index102);
            	index_column10 = index_column(index);
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, index_column10.Tree);
            	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:22:32: ( COMMA index_column[index] )*
            	do 
            	{
            	    int alt4 = 2;
            	    int LA4_0 = input.LA(1);

            	    if ( (LA4_0 == COMMA) )
            	    {
            	        alt4 = 1;
            	    }


            	    switch (alt4) 
            		{
            			case 1 :
            			    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:22:33: COMMA index_column[index]
            			    {
            			    	COMMA11=(IToken)Match(input,COMMA,FOLLOW_COMMA_in_create_index106); 
            			    		COMMA11_tree = (object)adaptor.Create(COMMA11);
            			    		adaptor.AddChild(root_0, COMMA11_tree);

            			    	PushFollow(FOLLOW_index_column_in_create_index108);
            			    	index_column12 = index_column(index);
            			    	state.followingStackPointer--;

            			    	adaptor.AddChild(root_0, index_column12.Tree);

            			    }
            			    break;

            			default:
            			    goto loop4;
            	    }
            	} while (true);

            	loop4:
            		;	// Stops C# compiler whining that label 'loop4' has no statements

            	RPAREN13=(IToken)Match(input,RPAREN,FOLLOW_RPAREN_in_create_index113); 
            		RPAREN13_tree = (object)adaptor.Create(RPAREN13);
            		adaptor.AddChild(root_0, RPAREN13_tree);


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
    // $ANTLR end "create_index"

    public class index_column_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "index_column"
    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:24:1: index_column[IndexConstraint index] : name= id (opsclass= id )? ;
    public PostgreSQLParser.index_column_return index_column(IndexConstraint index) // throws RecognitionException [1]
    {   
        PostgreSQLParser.index_column_return retval = new PostgreSQLParser.index_column_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        PostgreSQLParser.id_return name = default(PostgreSQLParser.id_return);

        PostgreSQLParser.id_return opsclass = default(PostgreSQLParser.id_return);



        try 
    	{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:24:36: (name= id (opsclass= id )? )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:25:5: name= id (opsclass= id )?
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_id_in_index_column127);
            	name = id();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, name.Tree);
            	 index.Columns.Add(new ColumnReference(UnquoteName(((name != null) ? input.ToString((IToken)(name.Start),(IToken)(name.Stop)) : null)))); 
            	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:26:13: (opsclass= id )?
            	int alt5 = 2;
            	int LA5_0 = input.LA(1);

            	if ( ((LA5_0 >= T_IDENT && LA5_0 <= T_QUOTED_IDENT)) )
            	{
            	    alt5 = 1;
            	}
            	switch (alt5) 
            	{
            	    case 1 :
            	        // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:26:13: opsclass= id
            	        {
            	        	PushFollow(FOLLOW_id_in_index_column137);
            	        	opsclass = id();
            	        	state.followingStackPointer--;

            	        	adaptor.AddChild(root_0, opsclass.Tree);

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
    // $ANTLR end "index_column"

    public class table_name_return : ParserRuleReturnScope
    {
        public NameWithSchema result;
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "table_name"
    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:28:1: table_name returns [NameWithSchema result] : qualified_name ;
    public PostgreSQLParser.table_name_return table_name() // throws RecognitionException [1]
    {   
        PostgreSQLParser.table_name_return retval = new PostgreSQLParser.table_name_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        PostgreSQLParser.qualified_name_return qualified_name14 = default(PostgreSQLParser.qualified_name_return);



        try 
    	{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:29:2: ( qualified_name )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:29:4: qualified_name
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_qualified_name_in_table_name150);
            	qualified_name14 = qualified_name();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, qualified_name14.Tree);
            	retval.result =  ((qualified_name14 != null) ? qualified_name14.result : default(NameWithSchema)); 

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
    // $ANTLR end "table_name"

    public class qualified_name_return : ParserRuleReturnScope
    {
        public NameWithSchema result;
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "qualified_name"
    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:30:1: qualified_name returns [NameWithSchema result] : ( (schema= id DOT name= id ) | (name= id ) );
    public PostgreSQLParser.qualified_name_return qualified_name() // throws RecognitionException [1]
    {   
        PostgreSQLParser.qualified_name_return retval = new PostgreSQLParser.qualified_name_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken DOT15 = null;
        PostgreSQLParser.id_return schema = default(PostgreSQLParser.id_return);

        PostgreSQLParser.id_return name = default(PostgreSQLParser.id_return);


        object DOT15_tree=null;

        try 
    	{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:31:2: ( (schema= id DOT name= id ) | (name= id ) )
            int alt6 = 2;
            int LA6_0 = input.LA(1);

            if ( ((LA6_0 >= T_IDENT && LA6_0 <= T_QUOTED_IDENT)) )
            {
                int LA6_1 = input.LA(2);

                if ( ((LA6_1 >= USING && LA6_1 <= LPAREN)) )
                {
                    alt6 = 2;
                }
                else if ( (LA6_1 == DOT) )
                {
                    alt6 = 1;
                }
                else 
                {
                    NoViableAltException nvae_d6s1 =
                        new NoViableAltException("", 6, 1, input);

                    throw nvae_d6s1;
                }
            }
            else 
            {
                NoViableAltException nvae_d6s0 =
                    new NoViableAltException("", 6, 0, input);

                throw nvae_d6s0;
            }
            switch (alt6) 
            {
                case 1 :
                    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:31:4: (schema= id DOT name= id )
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:31:4: (schema= id DOT name= id )
                    	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:31:5: schema= id DOT name= id
                    	{
                    		PushFollow(FOLLOW_id_in_qualified_name166);
                    		schema = id();
                    		state.followingStackPointer--;

                    		adaptor.AddChild(root_0, schema.Tree);
                    		DOT15=(IToken)Match(input,DOT,FOLLOW_DOT_in_qualified_name168); 
                    			DOT15_tree = (object)adaptor.Create(DOT15);
                    			adaptor.AddChild(root_0, DOT15_tree);

                    		PushFollow(FOLLOW_id_in_qualified_name172);
                    		name = id();
                    		state.followingStackPointer--;

                    		adaptor.AddChild(root_0, name.Tree);
                    		retval.result = new NameWithSchema(UnquoteName(((schema != null) ? input.ToString((IToken)(schema.Start),(IToken)(schema.Stop)) : null)), UnquoteName(((name != null) ? input.ToString((IToken)(name.Start),(IToken)(name.Stop)) : null))); 

                    	}


                    }
                    break;
                case 2 :
                    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:32:6: (name= id )
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:32:6: (name= id )
                    	// ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:32:7: name= id
                    	{
                    		PushFollow(FOLLOW_id_in_qualified_name188);
                    		name = id();
                    		state.followingStackPointer--;

                    		adaptor.AddChild(root_0, name.Tree);
                    		retval.result = new NameWithSchema(UnquoteName(((name != null) ? input.ToString((IToken)(name.Start),(IToken)(name.Stop)) : null))); 

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
    // $ANTLR end "qualified_name"

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
    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:33:1: index_name : id ;
    public PostgreSQLParser.index_name_return index_name() // throws RecognitionException [1]
    {   
        PostgreSQLParser.index_name_return retval = new PostgreSQLParser.index_name_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        PostgreSQLParser.id_return id16 = default(PostgreSQLParser.id_return);



        try 
    	{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:33:12: ( id )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:33:14: id
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_id_in_index_name199);
            	id16 = id();
            	state.followingStackPointer--;

            	adaptor.AddChild(root_0, id16.Tree);

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
    // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:34:1: id : ( T_IDENT | T_QUOTED_IDENT );
    public PostgreSQLParser.id_return id() // throws RecognitionException [1]
    {   
        PostgreSQLParser.id_return retval = new PostgreSQLParser.id_return();
        retval.Start = input.LT(1);

        object root_0 = null;

        IToken set17 = null;

        object set17_tree=null;

        try 
    	{
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:34:3: ( T_IDENT | T_QUOTED_IDENT )
            // ..\\Plugin.postgre\\AntlrParser\\PostgreSQL.g:
            {
            	root_0 = (object)adaptor.GetNilNode();

            	set17 = (IToken)input.LT(1);
            	if ( (input.LA(1) >= T_IDENT && input.LA(1) <= T_QUOTED_IDENT) ) 
            	{
            	    input.Consume();
            	    adaptor.AddChild(root_0, (object)adaptor.Create(set17));
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

    // Delegated rules


	private void InitializeCyclicDFAs()
	{
	}

 

    public static readonly BitSet FOLLOW_CREATE_in_create_index37 = new BitSet(new ulong[]{0x0000000000000060UL});
    public static readonly BitSet FOLLOW_UNIQUE_in_create_index44 = new BitSet(new ulong[]{0x0000000000000040UL});
    public static readonly BitSet FOLLOW_INDEX_in_create_index54 = new BitSet(new ulong[]{0x000000000000C080UL});
    public static readonly BitSet FOLLOW_CONCURRENTLY_in_create_index60 = new BitSet(new ulong[]{0x000000000000C080UL});
    public static readonly BitSet FOLLOW_index_name_in_create_index70 = new BitSet(new ulong[]{0x0000000000000100UL});
    public static readonly BitSet FOLLOW_ON_in_create_index79 = new BitSet(new ulong[]{0x000000000000C080UL});
    public static readonly BitSet FOLLOW_table_name_in_create_index81 = new BitSet(new ulong[]{0x0000000000000600UL});
    public static readonly BitSet FOLLOW_USING_in_create_index90 = new BitSet(new ulong[]{0x000000000000C080UL});
    public static readonly BitSet FOLLOW_id_in_create_index92 = new BitSet(new ulong[]{0x0000000000000400UL});
    public static readonly BitSet FOLLOW_LPAREN_in_create_index100 = new BitSet(new ulong[]{0x000000000000C080UL});
    public static readonly BitSet FOLLOW_index_column_in_create_index102 = new BitSet(new ulong[]{0x0000000000001800UL});
    public static readonly BitSet FOLLOW_COMMA_in_create_index106 = new BitSet(new ulong[]{0x000000000000C080UL});
    public static readonly BitSet FOLLOW_index_column_in_create_index108 = new BitSet(new ulong[]{0x0000000000001800UL});
    public static readonly BitSet FOLLOW_RPAREN_in_create_index113 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_id_in_index_column127 = new BitSet(new ulong[]{0x000000000000C082UL});
    public static readonly BitSet FOLLOW_id_in_index_column137 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_qualified_name_in_table_name150 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_id_in_qualified_name166 = new BitSet(new ulong[]{0x0000000000002000UL});
    public static readonly BitSet FOLLOW_DOT_in_qualified_name168 = new BitSet(new ulong[]{0x000000000000C080UL});
    public static readonly BitSet FOLLOW_id_in_qualified_name172 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_id_in_qualified_name188 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_id_in_index_name199 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_set_in_id0 = new BitSet(new ulong[]{0x0000000000000002UL});

}
