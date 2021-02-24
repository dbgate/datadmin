grammar PostgreSQL;

options {
	language=CSharp2;
	output=AST;
}

@header {
    using System.Globalization;
    using DatAdmin;
    using Plugin.postgre;
}

create_index[IndexConstraint index]:
    CREATE
    (UNIQUE {index.IsUnique=true;})?
    INDEX
    CONCURRENTLY? 
    name=index_name {index.Name=UnquoteName($name.text);} 
    ON table_name {index.SetDummyTable($table_name.result);}
    (USING id)?
    LPAREN index_column[index] (COMMA index_column[index])* RPAREN;

index_column[IndexConstraint index]:
    name=id { index.Columns.Add(new ColumnReference(UnquoteName($name.text))); }
    opsclass=id?;

table_name returns[NameWithSchema result]
	: qualified_name {$result = $qualified_name.result; };
qualified_name returns[NameWithSchema result]
	:	(schema=id DOT name=id {$result=new NameWithSchema(UnquoteName($schema.text), UnquoteName($name.text)); } ) |
	    (name=id {$result=new NameWithSchema(UnquoteName($name.text)); } );
index_name	: id;
id: T_IDENT | T_QUOTED_IDENT;


CREATE: C R E A T E;
INDEX: I N D E X;
UNIQUE: U N I Q U E;
CONCURRENTLY : C O N C U R R E N T L Y;
USING: U S I N G;
ON: O N;

// ****************************************************************************
// LEXER definitions
// ****************************************************************************
EQUALS:        '=';
SAFEEQUALS:    '<=>';
EQUALS2:       '==';
NOT_EQUALS:    '!=';
NOT_EQUALS2:   '<>';
LESS:          '<';
LESS_OR_EQ:    '<=';
GREATER:       '>';
GREATER_OR_EQ: '>=';
SHIFT_LEFT:    '<<';
SHIFT_RIGHT:   '>>';
AMPERSAND:     '&';
DOUBLE_AMPERSAND: '&&';
PIPE:          '|';
DOUBLE_PIPE:   '||';
PLUS:          '+';
MINUS:         '-';
TILDA:         '~';
ASTERISK:      '*';
SLASH:         '/';
PERCENT:       '%';
SEMI:          ';';
DOT:           '.';
COMMA:         ',';
LPAREN:        '(';
RPAREN:        ')';
QUESTION:      '?';
EXCLAMATION:   '!';
COLON:         ':';
AT:            '@';
DOLLAR:        '$';
ARROW_UP:      '^';

// http://www.antlr.org/wiki/pages/viewpage.action?pageId=1782
fragment A:('a'|'A');
fragment B:('b'|'B');
fragment C:('c'|'C');
fragment D:('d'|'D');
fragment E:('e'|'E');
fragment F:('f'|'F');
fragment G:('g'|'G');
fragment H:('h'|'H');
fragment I:('i'|'I');
fragment J:('j'|'J');
fragment K:('k'|'K');
fragment L:('l'|'L');
fragment M:('m'|'M');
fragment N:('n'|'N');
fragment O:('o'|'O');
fragment P:('p'|'P');
fragment Q:('q'|'Q');
fragment R:('r'|'R');
fragment S:('s'|'S');
fragment T:('t'|'T');
fragment U:('u'|'U');
fragment V:('v'|'V');
fragment W:('w'|'W');
fragment X:('x'|'X');
fragment Y:('y'|'Y');
fragment Z:('z'|'Z');



fragment ID_START: ('a'..'z'|'A'..'Z'|'_');
T_IDENT: (ID_START (ID_START|'0'..'9')*);
T_QUOTED_IDENT: '"' ( options {greedy=false;} : . )* '"';

T_NSTRING: 
	('N' | 'n') 
	  ('\'' 
	  	( 
	  		  options{greedy=true;}: ~('\'' | '\r' | '\n' ) | '\'' '\'' 
	  	)* 
	  '\'' )
;
T_STRING:
	  ('\'' 
	  	( 
	  		  options{greedy=true;}: ~('\'' | '\r' | '\n' ) | '\'' '\'' 
	  	)* 
	  '\'' )
;
T_INTEGER: ('0'..'9')+;
fragment FLOAT_EXP : ('e'|'E') ('+'|'-')? ('0'..'9')+ ;
T_FLOAT
    :   ('0'..'9')+ '.' ('0'..'9')* FLOAT_EXP?
    |   '.' ('0'..'9')+ FLOAT_EXP?
    |   ('0'..'9')+ FLOAT_EXP
    ;
T_BLOB: ('x'|'X') '\'' ('0'..'9'|'a'..'f'|'A'..'F')+ '\'';

T_BINARYNUM : ('0' 'b' ('0' | '1')+ ) | 'b' '\'' ('0' | '1')+ '\'';

fragment T_COMMENT: '/*' ( options {greedy=false;} : . )* '*/';
fragment LINE_COMMENT: '--' ~('\n'|'\r')* ('\r'? '\n'|EOF);

WHITESPACE: (' '|'\r'|'\t'|'\u000C'|'\n'|T_COMMENT|LINE_COMMENT) {$channel=HIDDEN;};
