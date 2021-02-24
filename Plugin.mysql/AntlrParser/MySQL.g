grammar MySQL;

options {
	language=CSharp2;
	output=AST;
	superClass=AntlrParser;
}

tokens {
    F_NL;
    F_INC;
    F_DEC;
    COLUMN_DEF;
}

@header {
    using System.Globalization;
    using DatAdmin;
    using Plugin.mysql;
}

//command	:	create;
//create 	:	create_table;

create_table[TableStructure table, bool loadName]:
		CREATE TEMPORARY? TABLE name=tbl_name {if (loadName) table.FullName=new NameWithSchema($name.text);}
        LPAREN create_definition[table] ( COMMA create_definition[table])*  RPAREN
        table_options?;

create_view_0:
    create_view;

create_definition[TableStructure table]:
    name=col_name
        {var col = new ColumnStructure(); table._Columns.Add(col); col.ColumnName=UnquoteName($name.text);}
    column_definition[col] -> ^(COLUMN_DEF col_name column_definition)
  | primary_key[table]
  | index_definition[table]
  | foreign_key[table]
  | (CONSTRAINT id?)? CHECK LPAREN expr RPAREN // not used by MySQL=>ignore
  //| (INDEX|KEY) index_name? index_type? index_cols index_option*
  //| (CONSTRAINT id?)? UNIQUE (INDEX|KEY)?
  //    index_name[null]? index_type[null]? index_cols[null] index_option[null]*
  //| (FULLTEXT|SPATIAL) (INDEX|KEY)? index_name? index_cols index_option*
  ;

index_definition[TableStructure table]:
    {var index=new IndexConstraint();table._Constraints.Add(index);}
    (CONSTRAINT id?)? (FULLTEXT|SPATIAL)? ((UNIQUE {index.IsUnique=true;} (INDEX|KEY)?) | (INDEX|KEY))
    (name=index_name {index.Name=UnquoteName($name.text);} )?
    index_type[index]? index_cols[index] index_option[index]*;

primary_key[TableStructure table]:
 (CONSTRAINT id?)?   // not used by MySQL=>ignore
    PRIMARY KEY {var pk=new PrimaryKey();table._Constraints.Add(pk);}
    index_type[pk]? index_cols[pk] index_option[pk]*;

foreign_key[TableStructure table]:
    {var fk=new ForeignKey(); table._Constraints.Add(fk);}
    (CONSTRAINT (name=id {fk.Name=UnquoteName($name.text);} )?)?
    FOREIGN KEY index_name? index_cols[fk] reference_definition[fk];

index_type[Constraint cnt]: USING id {cnt.MySet("index_type", $id.text);};

index_cols[ColumnsConstraint cnt]: LPAREN index_col[cnt] (COMMA index_col[cnt])* RPAREN;
index_col[ColumnsConstraint cnt]: name=col_name
    {var col=new ColumnReference(UnquoteName($name.text));cnt.Columns.Add(col);}
    (LPAREN subpart=T_INTEGER RPAREN {col.MySet("sub_part", $subpart.text);} )?
    (ASC | DESC)?;

fk_cols[ForeignKey fk]: LPAREN fk_col[fk] (COMMA fk_col[fk])* RPAREN;
fk_col[ForeignKey fk]: name=col_name
    {var col=new ColumnReference(UnquoteName($name.text));fk.PrimaryKeyColumns.Add(col);}
    (LPAREN subpart=T_INTEGER RPAREN {col.MySet("sub_part", $subpart.text);} )?
    (ASC | DESC)?;

index_option[Constraint cnt]:
    KEY_BLOCK_SIZE EQUALS? T_INTEGER
  | index_type[cnt]
  | WITH PARSER id;

id: T_IDENT | T_QUOTED_IDENT;
col_name:	id;
tbl_name:	id;
index_name:	id;
charset_name:   id;
collation_name:   id;

column_definition[ColumnStructure col]:
    data_type[col] {col.IsNullable=true;}
      ((NOT NULL {col.IsNullable=false;}) | (NULL {col.IsNullable=true;}))?
      (DEFAULT default_value_expr[col])?
      (ON UPDATE CURRENT_TIMESTAMP)?
      (AUTO_INCREMENT {col.DataType.SetAutoincrement(true);} )?
      (UNIQUE KEY? | PRIMARY? KEY)?
      (COMMENT com=T_STRING { col.Comment=StringValue($com.text); } )?
      (COLUMN_FORMAT (FIXED|DYNAMIC|DEFAULT))?
      (STORAGE (DISK|MEMORY|DEFAULT))?
      reference_definition[new ForeignKey()]? // reference definition is parsed but ignored
      ;

reference_definition[ForeignKey fk]:
    REFERENCES dstname=tbl_name {fk.PrimaryKeyTable=new NameWithSchema(UnquoteName($dstname.text));}
      fk_cols[fk]
      (MATCH FULL | MATCH PARTIAL | MATCH SIMPLE)?
      (ON DELETE ondel=reference_option {fk.OnDeleteAction = $ondel.action;} )?
      (ON UPDATE onupd=reference_option {fk.OnUpdateAction = $onupd.action;})?;

reference_option returns [ForeignKeyAction action]:
    RESTRICT {$action = ForeignKeyAction.Restrict;}
  | CASCADE {$action = ForeignKeyAction.Cascade;}
  | SET NULL {$action = ForeignKeyAction.SetNull;}
  | NO ACTION {$action = ForeignKeyAction.NoAction;}
;

number_flags[MySqlTypeNumber tp]:
    (UNSIGNED {tp.Unsigned=true;})? (ZEROFILL {tp.Zerofill=true;})?;

charset_collate[ColumnStructure col]:
      (CHARACTER SET chset=id {col.CharacterSet=$chset.text;} )?
      (COLLATE coll=id {col.Collation=$coll.text;} )?;

data_type[ColumnStructure col]	:
    BIT {var tp=new MySqlTypeBit();} int_length[tp]? {col.DataType=tp.ToGenericType();;}
  | TINYINT {var tp=new MySqlTypeTinyInt();} int_length[tp]? number_flags[tp] {col.DataType=tp.ToGenericType();}
  | SMALLINT {var tp=new MySqlTypeSmallInt();} int_length[tp]? number_flags[tp] {col.DataType=tp.ToGenericType();}
  | MEDIUMINT {var tp=new MySqlTypeMediumInt();} int_length[tp]? number_flags[tp] {col.DataType=tp.ToGenericType();}
  | INT {var tp=new MySqlTypeInt();}  int_length[tp]? number_flags[tp] {col.DataType=tp.ToGenericType();}
  | INTEGER {var tp=new MySqlTypeInt();} int_length[tp]? number_flags[tp] {col.DataType=tp.ToGenericType();}
  | BIGINT {var tp=new MySqlTypeBigInt();} int_length[tp]?  number_flags[tp] {col.DataType=tp.ToGenericType();}
  | REAL {var tp=new MySqlTypeDouble();} real_length[tp]? number_flags[tp] {col.DataType=tp.ToGenericType();}
  | DOUBLE {var tp=new MySqlTypeDouble();} real_length[tp]? number_flags[tp] {col.DataType=tp.ToGenericType();}
  | FLOAT {var tp=new MySqlTypeFloat();} real_length[tp]? number_flags[tp] {col.DataType=tp.ToGenericType();}
  | DECIMAL {var tp=new MySqlTypeDecimal();} real_length[tp]? number_flags[tp] {col.DataType=tp.ToGenericType();}
  | NUMERIC {var tp=new MySqlTypeNumeric();}  real_length[tp]? number_flags[tp] {col.DataType=tp.ToGenericType();}
  | DATE {var tp=new MySqlTypeDate();} int_length[tp]? {col.DataType=tp.ToGenericType();}
  | TIME {var tp=new MySqlTypeTime();} int_length[tp]? {col.DataType=tp.ToGenericType();}
  | TIMESTAMP {var tp=new MySqlTypeTimestamp();} int_length[tp]? {col.DataType=tp.ToGenericType();}
  | DATETIME {var tp=new MySqlTypeDatetime();} int_length[tp]? {col.DataType=tp.ToGenericType();}
  | YEAR {var tp=new MySqlTypeYear();} int_length[tp]? {col.DataType=tp.ToGenericType();}
  | CHAR {var tp=new MySqlTypeChar();} char_length[tp] charset_collate[col] {col.DataType=tp.ToGenericType();}
  | VARCHAR {var tp=new MySqlTypeVarChar();} char_length[tp] charset_collate[col] {col.DataType=tp.ToGenericType();}
  | BINARY {var tp=new MySqlTypeBinary();} char_length[tp]? {col.DataType=tp.ToGenericType();}
  | VARBINARY {var tp=new MySqlTypeVarBinary();} char_length[tp]? {col.DataType=tp.ToGenericType();}
  | TINYBLOB {var tp=new MySqlTypeTinyBlob();col.DataType=tp.ToGenericType();}
  | BLOB {var tp=new MySqlTypeBlob();col.DataType=tp.ToGenericType();}
  | MEDIUMBLOB {var tp=new MySqlTypeMediumBlob();col.DataType=tp.ToGenericType();}
  | LONGBLOB {var tp=new MySqlTypeLongBlob();col.DataType=tp.ToGenericType();}
  | TINYTEXT {var tp=new MySqlTypeTinyText();}  BINARY? charset_collate[col] {col.DataType=tp.ToGenericType();}
  | TEXT {var tp=new MySqlTypeText();}  BINARY? charset_collate[col] {col.DataType=tp.ToGenericType();}
  | MEDIUMTEXT {var tp=new MySqlTypeMediumText();}  BINARY? charset_collate[col] {col.DataType=tp.ToGenericType();}
  | LONGTEXT {var tp=new MySqlTypeLongText();} BINARY? charset_collate[col] {col.DataType=tp.ToGenericType();}
  | ENUM {var tp=new MySqlTypeEnum();}  enumset_values[tp] charset_collate[col] {col.DataType=tp.ToGenericType();}
  | SET {var tp=new MySqlTypeSet();}  enumset_values[tp] charset_collate[col] {col.DataType=tp.ToGenericType();}
  | GEOMETRY {var tp=new MySqlTypeGeometry();col.DataType=tp.ToGenericType();}
  | POINT {var tp=new MySqlTypePoint();col.DataType=tp.ToGenericType();}
  | LINESTRING {var tp=new MySqlTypeLineString();col.DataType=tp.ToGenericType();}
  | POLYGON {var tp=new MySqlTypePolygon();col.DataType=tp.ToGenericType();}
  | MULTIPOINT {var tp=new MySqlTypeMultiPoint();col.DataType=tp.ToGenericType();}
  | MULTILINESTRING {var tp=new MySqlTypeMultiLineString();col.DataType=tp.ToGenericType();}
  | MULTIPOLYGON {var tp=new MySqlTypeMultiPolygon();col.DataType=tp.ToGenericType();}
  | GEOMETRYCOLLECTION {var tp=new MySqlTypeGeometryCollection();col.DataType=tp.ToGenericType();}
;

int_length[MySqlTypeBase tp] : LPAREN len=T_INTEGER {tp.SetLength($len.text);} RPAREN;
char_length[MySqlTypeBase tp] : LPAREN len=T_INTEGER {tp.SetLength($len.text);} RPAREN;
real_length[MySqlTypeBase tp] :	LPAREN len=T_INTEGER {tp.SetLength($len.text);} (COMMA dec=T_INTEGER {tp.SetDecimals($dec.text);} )? RPAREN;

enumset_values[MySqlTypeEnumSet tp] : LPAREN enumset_value[tp] (COMMA enumset_value[tp] )* RPAREN;

enumset_value[MySqlTypeEnumSet tp]:
	id1=id {tp.Values.Add($id.text);} | s=T_STRING {tp.Values.Add(StringValue($s.text));};

table_options: table_option ( COMMA? table_option)*;

table_option:
    ENGINE EQUALS? ( id | MEMORY )
  | TYPE EQUALS? id
  | AUTO_INCREMENT EQUALS? T_INTEGER
  | AVG_ROW_LENGTH EQUALS? value
  | DEFAULT? (CHARACTER SET|CHARSET) EQUALS? charset_name
  | CHECKSUM EQUALS? T_INTEGER
  | DEFAULT? COLLATE EQUALS? collation_name
  | COMMENT EQUALS? T_STRING
  | CONNECTION EQUALS? T_STRING
  | DATA DIRECTORY EQUALS? T_STRING
  | DELAY_KEY_WRITE EQUALS? T_INTEGER
  | INDEX DIRECTORY EQUALS? T_STRING
  | INSERT_METHOD EQUALS? ( NO | FIRST | LAST )
  | KEY_BLOCK_SIZE EQUALS? T_INTEGER
  | MAX_ROWS EQUALS? T_INTEGER
  | MIN_ROWS EQUALS? T_INTEGER
  | PACK_KEYS EQUALS? (T_INTEGER | DEFAULT)
  | PASSWORD EQUALS? T_STRING
  | ROW_FORMAT EQUALS? (DEFAULT|DYNAMIC|FIXED|COMPRESSED|REDUNDANT|COMPACT)
  | TABLESPACE id (STORAGE (DISK|MEMORY|DEFAULT))?
  | UNION EQUALS? LPAREN tbl_name (COMMA tbl_name)* RPAREN;

find_deps[DepsCollector dc]
    : find_dep_item[dc]*;
find_dep_item[DepsCollector dc]
    : keyword | operator_no_dot | T_NSTRING | T_STRING | T_INTEGER | T_FLOAT | T_BINARYNUM | T_BLOB |
        name1=id { var name=new DepsName();name.AddComponent(UnquoteName($name1.text)); }
        (DOT DOT? name2=id { name.AddComponent(UnquoteName($name2.text));} )*
        {dc.AddName(name); } ;

create_view:
		CREATE (OR REPLACE)? f_inc
        (f_nl ALGORITHM EQUALS id)?
        (f_nl DEFINER EQUALS user)?
        (f_nl SQL SECURITY (DEFINER|INVOKER))?
        f_dec f_nl VIEW id ( LPAREN id (COMMA id)* RPAREN)?
        AS f_nl (select_statement | LPAREN select_statement RPAREN)
        (WITH (CASCADED | LOCAL)? CHECK OPTION)?;

// SELECT

f_nl: -> F_NL;
f_dec: -> F_DEC;
f_inc: -> F_INC;

select_statement:
    select_statement_component
    (f_nl UNION ALL? f_nl select_statement_component)*

    (f_nl ORDER BY selectby_expr (COMMA selectby_expr)*)?
    (f_nl LIMIT ((T_INTEGER COMMA)? INTEGER | INTEGER OFFSET INTEGER ))?
    (f_nl FOR UPDATE | LOCK IN SHARE MODE)?
    ;

select_statement_component:
        SELECT
        (ALL | DISTINCT | DISTINCTROW)?
        HIGH_PRIORITY?
        STRAIGHT_JOIN?
        SQL_SMALL_RESULT? SQL_BIG_RESULT? SQL_BUFFER_RESULT?
        (SQL_CACHE | SQL_NO_CACHE)? SQL_CALC_FOUND_ROWS?
    f_inc f_nl select_expr (COMMA f_nl select_expr)* f_dec f_nl
    (f_nl FROM table_references)?
    (f_nl WHERE condition)?
    (f_nl GROUP BY selectby_expr (COMMA selectby_expr)* (WITH ROLLUP)?)?
    (f_nl HAVING condition)?
    ;

selectby_expr: expr (ASC|DESC)?;

select_expr:
    expr (AS? id)?
    | id DOT ASTERISK
    | ASTERISK
;

table_references:
    f_inc table_reference (COMMA table_reference)* f_dec
    ;


table_reference:
    table_factor join_table*;

table_factor:
    id (AS? id)? index_hint?
    | table_subquery AS? id
    | LPAREN table_references RPAREN
    | OJ table_factor LEFT OUTER JOIN table_factor ON condition
;

join_table:
    f_nl (
        INNER | CROSS | LEFT OUTER? | RIGHT OUTER?
        | NATURAL ((LEFT|RIGHT) OUTER?)?
    )?
    (JOIN|STRAIGHT_JOIN) table_factor join_condition?
;

//table_reference:
//    id (AS? id)? index_hint?
//    | table_subquery AS? id
//    | LPAREN table_references RPAREN
//    | OJ table_reference LEFT OUTER JOIN table_reference ON condition
//;


//table_reference options {backtrack=true;memoize=true;}:
//    table_factor | join_table
//    ;

//table_factor options {backtrack=true;memoize=true;}:
//    id (AS? id)? index_hint?
//  | table_subquery AS? id
//  | LPAREN table_references RPAREN
//  | OJ table_reference LEFT OUTER JOIN table_reference ON condition
//;

//join_table options {backtrack=true;memoize=true;}: table_factor join_table_2;

//join_table_2 options {backtrack=true;memoize=true;}:
//    (INNER | CROSS)? JOIN table_factor join_condition?
//    | STRAIGHT_JOIN table_factor (ON condition)?
//    | (LEFT|RIGHT) OUTER? JOIN table_reference join_condition
//    | NATURAL ((LEFT|RIGHT) OUTER?)? JOIN table_factor
//;

join_condition:
    ON condition
  | USING LPAREN id (COMMA id)* RPAREN
  ;

index_hint:
    USE (INDEX|KEY) (FOR JOIN)? LPAREN id (COMMA id)* RPAREN
  | IGNORE (INDEX|KEY) (FOR JOIN)? LPAREN id (COMMA id)* RPAREN
  | FORCE (INDEX|KEY) (FOR JOIN)? LPAREN id (COMMA id)* RPAREN
  ;

table_subquery:
    LPAREN select_statement RPAREN;

user:
    CURRENT_USER ( LPAREN RPAREN )? |
    id AT id
;
default_value_expr[ColumnStructure col]:
    sval=T_STRING {col.DefaultValue = new ConstStringSqlExpression(StringValue($sval.text)); }
  | ival=T_INTEGER {col.DefaultValue = new ConstNumberSqlExpression(Int32.Parse($ival.text, CultureInfo.InvariantCulture)); }
  | ival=T_BINARYNUM {col.DefaultValue = new ConstNumberSqlExpression(ParseBinNum($ival.text)); }
  | fval=T_FLOAT {col.DefaultValue = new ConstNumberSqlExpression(Double.Parse($fval.text, CultureInfo.InvariantCulture)); }
  | NULL {col.DefaultValue = null; } // default value NULL is not remembered... new NullSqlExpression();
  | CURRENT_TIMESTAMP {col.DefaultValue = new SpecialConstantSqlExpression(SqlSpecialConstant.Current_Timestamp); }
  | CURRENT_DATE {col.DefaultValue = new SpecialConstantSqlExpression(SqlSpecialConstant.Current_Date); }
  ;

case_expr: case_expr1 | case_expr2 ;
case_expr1: CASE value
    (WHEN value THEN expr)+
    (ELSE expr)?
    END CASE;
case_expr2: CASE
    (WHEN condition THEN expr)+
    (ELSE expr)?
    END CASE;

value: T_INTEGER | T_STRING | NULL | T_FLOAT | T_BLOB | CURRENT_TIMESTAMP | UTC_TIMESTAMP;

expr: condition;

arg_list: LPAREN DISTINCT (ASTERISK | expr) RPAREN | LPAREN expr (COMMA expr) * RPAREN | LPAREN RPAREN;
func_call: (id|sysname) (DOT id)? arg_list;
expr_elem:
    id (DOT id)?
    | func_call
    | value
    | LPAREN expr RPAREN
    | LPAREN select_statement RPAREN
    | INTERVAL T_INTEGER (id | sysname)
;
exclamation_expr: EXCLAMATION? expr_elem;
unary_expr: (MINUS|TILDA)? exclamation_expr;
pow_expr: unary_expr (ARROW_UP unary_expr)?;
mul_expr: pow_expr ((ASTERISK | SLASH | PERCENT | DIV | MOD) pow_expr)*;
add_expr: mul_expr ( (PLUS | MINUS) mul_expr)*;
shift_expr: add_expr ((SHIFT_LEFT | SHIFT_RIGHT) add_expr)*;
bitand_expr: shift_expr (AMPERSAND shift_expr)*;
bitor_expr: bitand_expr (PIPE bitand_expr)*;
rel_expr: bitor_expr ( (EQUALS | SAFEEQUALS | GREATER_OR_EQ | GREATER | LESS_OR_EQ | LESS
    | NOT_EQUALS2 | NOT_EQUALS | IS | LIKE | REGEXP | IN) bitor_expr)*;
between_expr: rel_expr (BETWEEN rel_expr AND rel_expr)?;
casegroup_expr: between_expr | case_expr;
not_expr : NOT? casegroup_expr;
and_expr: not_expr ( (AND | DOUBLE_AMPERSAND) not_expr)*;
xor_expr: and_expr ( XOR and_expr)*;
or_expr : xor_expr ( (OR | DOUBLE_PIPE) xor_expr )*;
condition : or_expr;

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

operator_no_dot :
    EQUALS | SAFEEQUALS | EQUALS2 | NOT_EQUALS | NOT_EQUALS2 | LESS | LESS_OR_EQ
    | GREATER | GREATER_OR_EQ | SHIFT_LEFT | SHIFT_RIGHT | AMPERSAND | DOUBLE_AMPERSAND
    | PIPE | DOUBLE_PIPE | PLUS | MINUS | TILDA | ASTERISK | SLASH | PERCENT | SEMI
    | COMMA | LPAREN | RPAREN | QUESTION | EXCLAMATION | COLON | AT | DOLLAR
    | ARROW_UP;

any_operator: DOT | operator_no_dot;

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


ABORT : A B O R T;
ADD : A D D;
AFTER : A F T E R;
ALL : A L L;
ALTER : A L T E R;
ANALYZE : A N A L Y Z E;
AND : A N D;
AS : A S;
ASC : A S C;
ATTACH : A T T A C H;
AUTOINCREMENT : A U T O I N C R E M E N T;
BEFORE : B E F O R E;
BEGIN : B E G I N;
BETWEEN : B E T W E E N;
BY : B Y;
CASCADE : C A S C A D E;
CASE : C A S E;
CAST : C A S T;
CHECK : C H E C K;
COLLATE : C O L L A T E;
COLUMN : C O L U M N;
COMMIT : C O M M I T;
CONFLICT : C O N F L I C T;
CONSTRAINT : C O N S T R A I N T;
CREATE : C R E A T E;
CROSS : C R O S S;
DATABASE : D A T A B A S E;
DEFAULT : D E F A U L T;
DEFERRABLE : D E F E R R A B L E;
DEFERRED : D E F E R R E D;
DELETE : D E L E T E;
DESC : D E S C;
DETACH : D E T A C H;
DISTINCT : D I S T I N C T;
DROP : D R O P;
EACH : E A C H;
ELSE : E L S E;
END : E N D;
ESCAPE : E S C A P E;
EXCEPT : E X C E P T;
EXCLUSIVE : E X C L U S I V E;
EXISTS : E X I S T S;
EXPLAIN : E X P L A I N;
FAIL : F A I L;
FOR : F O R;
FOREIGN : F O R E I G N;
FROM : F R O M;
GLOB : G L O B;
GROUP : G R O U P;
HAVING : H A V I N G;
IF : I F;
IGNORE : I G N O R E;
IMMEDIATE : I M M E D I A T E;
IN : I N;
INDEX : I N D E X;
INDEXED : I N D E X E D;
INITIALLY : I N I T I A L L Y;
INNER : I N N E R;
INSERT : I N S E R T;
INSTEAD : I N S T E A D;
INTERSECT : I N T E R S E C T;
INTO : I N T O;
IS : I S;
ISNULL : I S N U L L;
JOIN : J O I N;
KEY : K E Y;
LEFT : L E F T;
LIKE : L I K E;
LIMIT : L I M I T;
MATCH : M A T C H;
NATURAL : N A T U R A L;
NOT : N O T;
NOTNULL : N O T N U L L;
NULL : N U L L;
OF : O F;
OFFSET : O F F S E T;
ON : O N;
OR : O R;
ORDER : O R D E R;
OUTER : O U T E R;
PLAN : P L A N;
PRAGMA : P R A G M A;
PRIMARY : P R I M A R Y;
QUERY : Q U E R Y;
RAISE : R A I S E;
REFERENCES : R E F E R E N C E S;
REGEXP : R E G E X P;
REINDEX : R E I N D E X;
RELEASE : R E L E A S E;
RENAME : R E N A M E;
REPLACE : R E P L A C E;
RESTRICT : R E S T R I C T;
ROLLBACK : R O L L B A C K;
ROW : R O W;
SAVEPOINT : S A V E P O I N T;
SELECT : S E L E C T;
SET : S E T;
TABLE : T A B L E;
TEMPORARY : T E M P O R A R Y;
TEMP : T E M P;
THEN : T H E N;
TO : T O;
TRANSACTION : T R A N S A C T I O N;
TRIGGER : T R I G G E R;
UNION : U N I O N;
UNIQUE : U N I Q U E;
UPDATE : U P D A T E;
USING : U S I N G;
VACUUM : V A C U U M;
VALUES : V A L U E S;
VIEW : V I E W;
VIRTUAL : V I R T U A L;
WHEN : W H E N;
WHERE : W H E R E;
DIV : D I V;
MOD : M O D;
FULLTEXT : F U L L T E X T;
SPATIAL : S P A T I A L;
KEY_BLOCK_SIZE : K E Y '_' B L O C K '_' S I Z E;
WITH : W I T H;
PARSER : P A R S E R;
XOR : X O R;
AUTO_INCREMENT : A U T O '_' I N C R E M E N T;
COLUMN_FORMAT : C O L U M N '_' F O R M A T;
FIXED : F I X E D;
DYNAMIC : D Y N A M I C;
STORAGE : S T O R A G E;
DISK : D I S K;
MEMORY : M E M O R Y;
FULL : F U L L;
PARTIAL : P A R T I A L;
SIMPLE : S I M P L E;
NO : N O;
ACTION : A C T I O N;
ENGINE : E N G I N E;
AVG_ROW_LENGTH : A V G '_' R O W '_' L E N G T H;
CHECKSUM : C H E C K S U M;
DATA : D A T A;
DIRECTORY : D I R E C T O R Y;
DELAY_KEY_WRITE : D E L A Y '_' K E Y '_' W R I T E;
INSERT_METHOD : I N S E R T '_' M E T H O D;
FIRST : F I R S T;
LAST : L A S T;
MAX_ROWS : M A X '_' R O W S;
MIN_ROWS : M I N '_' R O W S;
PACK_KEYS : P A C K '_' K E Y S;
PASSWORD : P A S S W O R D;
ROW_FORMAT : R O W '_' F O R M A T;
COMPRESSED : C O M P R E S S E D;
REDUNDANT : R E D U N D A N T;
COMPACT : C O M P A C T;
TABLESPACE : T A B L E S P A C E;
CHARSET : C H A R S E T;
BIT : B I T;
TINYINT : T I N Y I N T;
SMALLINT : S M A L L I N T;
MEDIUMINT : M E D I U M I N T;
INT : I N T;
INTEGER : I N T E G E R;
BIGINT : B I G I N T;
REAL : R E A L;
DOUBLE : D O U B L E;
FLOAT : F L O A T;
DECIMAL : D E C I M A L;
NUMERIC : N U M E R I C;
DATE : D A T E;
TIME : T I M E;
TIMESTAMP : T I M E S T A M P;
DATETIME : D A T E T I M E;
CHAR : C H A R;
VARCHAR : V A R C H A R;
BINARY : B I N A R Y;
VARBINARY : V A R B I N A R Y;
TINYBLOB : T I N Y B L O B;
BLOB : B L O B;
MEDIUMBLOB : M E D I U M B L O B;
LONGBLOB : L O N G B L O B;
TINYTEXT : T I N Y T E X T;
TEXT : T E X T;
MEDIUMTEXT : M E D I U M T E X T;
LONGTEXT : L O N G T E X T;
ENUM : E N U M;
CHARACTER : C H A R A C T E R;
UNSIGNED : U N S I G N E D;
ZEROFILL : Z E R O F I L L;
CONNECTION : C O N N E C T I O N;
COMMENT : C O M M E N T;
GEOMETRY : G E O M E T R Y;
POINT : P O I N T;
LINESTRING : L I N E S T R I N G;
POLYGON : P O L Y G O N;
MULTIPOINT : M U L T I P O I N T;
MULTILINESTRING : M U L T I L I N E S T R I N G;
MULTIPOLYGON : M U L T I P O L Y G O N;
GEOMETRYCOLLECTION : G E O M E T R Y C O L L E C T I O N;
TYPE : T Y P E;
RIGHT : R I G H T;
ALGORITHM : A L G O R I T H M;
DEFINER : D E F I N E R;
INVOKER : I N V O K E R;
SQL : S Q L;
SECURITY : S E C U R I T Y;
CASCADED : C A S C A D E D;
LOCAL : L O C A L;
OPTION : O P T I O N;
DISTINCTROW : D I S T I N C T R O W;
HIGH_PRIORITY : H I G H '_' P R I O R I T Y;
STRAIGHT_JOIN : S T R A I G H T '_' J O I N;
SQL_SMALL_RESULT : S Q L '_' S M A L L '_' R E S U L T;
SQL_BIG_RESULT : S Q L '_' B I G '_' R E S U L T;
SQL_BUFFER_RESULT : S Q L '_' B U F F E R '_' R E S U L T;
SQL_CACHE : S Q L '_' C A C H E;
SQL_NO_CACHE : S Q L '_' N O '_' C A C H E;
SQL_CALC_FOUND_ROWS : S Q L '_' C A L C '_' F O U N D '_' R O W S;
ROLLUP : R O L L U P;
SHARE : S H A R E;
LOCK : L O C K;
MODE : M O D E;
FORCE : F O R C E;
OJ : O J;
USE : U S E;
INTERVAL : I N T E R V A L;

CURRENT_TIME : C U R R E N T '_' T I M E;
CURRENT_DATE : C U R R E N T '_' D A T E;
CURRENT_TIMESTAMP : C U R R E N T '_' T I M E S T A M P;
UTC_TIMESTAMP : U T C '_' T I M E S T A M P;
CURRENT_USER : C U R R E N T '_' U S E R;
YEAR : Y E A R;

fragment ID_START: ('a'..'z'|'A'..'Z'|'_');
T_IDENT: (ID_START (ID_START|'0'..'9')*);
T_QUOTED_IDENT: '`' ( options {greedy=false;} : . )* '`';
//TCL_ID: ID_START (ID_START|'0'..'9'|'::')* (LPAREN ( options {greedy=false;} : . )* RPAREN)?;
//fragment ESCAPE_SEQ: '\\'  ('\"'|'\''|'\\'|'n'|'r'|'0'|'t');
//T_STRING
//	: '"' ( ESCAPE_SEQ | ~('\\'|'"') )* '"'
//	| '\'' ( ESCAPE_SEQ | ~('\\'|'\'') )* '\''
//	;

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

keyword :
ABORT|ADD|AFTER|ALL|ALTER|ANALYZE|AND|AS|ASC|ATTACH|AUTOINCREMENT|BEFORE|BEGIN|BETWEEN|BY|CASCADE|CASE|CAST|CHECK|COLLATE|COLUMN|COMMIT|CONFLICT|CONSTRAINT|CREATE|CROSS|DATABASE|DEFAULT|DEFERRABLE|DEFERRED|DELETE|DESC|DETACH|DISTINCT|DROP|EACH|ELSE|END|ESCAPE|EXCEPT|EXCLUSIVE|EXISTS|EXPLAIN|FAIL|FOR|FOREIGN|FROM|GLOB|GROUP|HAVING|IF|IGNORE|IMMEDIATE|IN|INDEX|INDEXED|INITIALLY|INNER|INSERT|INSTEAD|INTERSECT|INTO|IS|ISNULL|JOIN|KEY|LEFT|LIKE|LIMIT|MATCH|NATURAL|NOT|NOTNULL|NULL|OF|OFFSET|ON|OR|ORDER|OUTER|PLAN|PRAGMA|PRIMARY|QUERY|RAISE|REFERENCES|REGEXP|REINDEX|RELEASE|RENAME|REPLACE|RESTRICT|ROLLBACK|ROW|SAVEPOINT|SELECT|SET|TABLE|TEMPORARY|TEMP|THEN|TO|TRANSACTION|TRIGGER|UNION|UNIQUE|UPDATE|USING|VACUUM|VALUES|VIEW|VIRTUAL|WHEN|WHERE|DIV|MOD|FULLTEXT|SPATIAL|KEY_BLOCK_SIZE|WITH|PARSER|XOR|AUTO_INCREMENT|COLUMN_FORMAT|FIXED|DYNAMIC|STORAGE|DISK|MEMORY|FULL|PARTIAL|SIMPLE|NO|ACTION|ENGINE|AVG_ROW_LENGTH|CHECKSUM|DATA|DIRECTORY|DELAY_KEY_WRITE|INSERT_METHOD|FIRST|LAST|MAX_ROWS|MIN_ROWS|PACK_KEYS|PASSWORD|ROW_FORMAT|COMPRESSED|REDUNDANT|COMPACT|TABLESPACE|CHARSET|BIT|TINYINT|SMALLINT|MEDIUMINT|INT|INTEGER|BIGINT|REAL|DOUBLE|FLOAT|DECIMAL|NUMERIC|DATE|TIME|TIMESTAMP|DATETIME|CHAR|VARCHAR|BINARY|VARBINARY|TINYBLOB|BLOB|MEDIUMBLOB|LONGBLOB|TINYTEXT|TEXT|MEDIUMTEXT|LONGTEXT|ENUM|CHARACTER|UNSIGNED|ZEROFILL|CONNECTION|COMMENT|GEOMETRY|POINT|LINESTRING|POLYGON|MULTIPOINT|MULTILINESTRING|MULTIPOLYGON|GEOMETRYCOLLECTION|TYPE|RIGHT|ALGORITHM|DEFINER|INVOKER|SQL|SECURITY|CASCADED|LOCAL|OPTION|DISTINCTROW|HIGH_PRIORITY|STRAIGHT_JOIN|SQL_SMALL_RESULT|SQL_BIG_RESULT|SQL_BUFFER_RESULT|SQL_CACHE|SQL_NO_CACHE|SQL_CALC_FOUND_ROWS|ROLLUP|SHARE|LOCK|MODE|FORCE|OJ|USE|INTERVAL
;
sysname :
CURRENT_TIME|CURRENT_DATE|CURRENT_TIMESTAMP|UTC_TIMESTAMP|CURRENT_USER|YEAR
;
