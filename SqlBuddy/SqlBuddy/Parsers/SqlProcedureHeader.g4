grammar SqlProcedureHeader;
/*
@parser::members
{
    protected const int EOF = Eof;
}
 
@lexer::members
{
    protected const int EOF = Eof;
    protected const int HIDDEN = Hidden;
}
*/

/*
 * Parser Rules
 */

compileUnit
    : procedureHeader
    ;

procedureHeader
    : (CREATE | ALTER) PROCEDURE procedureName procedureParams? procedureOptions? AS
    ;

procedureName
    : (dbIdentifier DOT)? dbIdentifier
    ;

procedureParams
    : OPEN_PARENS CLOSE_PARENS
    | OPEN_PARENS procedureParam (COMMA procedureParam)* CLOSE_PARENS
    | procedureParam (COMMA procedureParam)*
    ;

procedureParam
    : paramIdentifier AS? paramType (ASSIGNMENT defaultLiteral)? OUTPUT?     # procedureParamNode
    ;

paramIdentifier
    : PARAM_IDENTIFIER
    ;

procedureOptions
    : WITH EXECUTE AS (CALLER | ownerLiteral)
    | WITH RECOMPILE
    ;

dbIdentifier
    : DB_IDENTIFIER
    ;

ownerLiteral
    : STRING_LITERAL
    ;

defaultLiteral
    : NULL
    | TRUE
    | FALSE
    | MINUS? INTEGER_LITERAL
    | MINUS? REAL_LITERAL
    | STRING_LITERAL
    ;

keyword 
    : CREATE 
    | ALTER 
    | PROCEDURE 
    | AS 
    | MAX 
    | OUTPUT 
    | WITH 
    | EXECUTE 
    | CALLER 
    | RECOMPILE
    ;

paramType
    : paramTypeName WHITESPACE* paramConstrains?
    ;

paramConstrains
    : OPEN_PARENS precision (COMMA scale)? CLOSE_PARENS
    | OPEN_PARENS MAX CLOSE_PARENS
    ;

precision
    : INTEGER_LITERAL+
    ;

scale
    : INTEGER_LITERAL+
    ;

paramTypeName
    : BIGINT
    | BINARY
    | BIT
    | CHAR
    | DECIMAL
    | NUMERIC
    | FLOAT
    | IMAGE
    | INTEGER
    | MONEY
    | NCHAR
    | NTEXT
    | NVARCHAR
    | REAL
    | UNIQUEIDENTIFIER
    | SMALLDATETIME
    | SMALLINT
    | SMALLMONEY
    | TEXT
    | TIMESTAMP
    | TINYINT
    | VARBINARY
    | VARCHAR
    | VARIANT
    | XML
    | DATETIME
    | SYSNAME
    | CURSOR
    ;
    /*
paramTypeName
    : BIGINT
    | BINARYSIZE
    | BINARY
    | BIT
    | CHAR
    | DECIMAL
    | NUMERIC
    | FLOAT
    | IMAGE
    | INTEGER
    | MONEY
    | NCHARSIZE
    | NCHAR
    | NCHARMAX
    | NTEXT
    | NVARCHAR
    | NVARCHARMAX
    | REAL
    | UNIQUEIDENTIFIER
    | SMALLDATETIME
    | SMALLINT
    | SMALLMONEY
    | TEXT
    | TIMESTAMP
    | TINYINT
    | VARBINARY
    | VARBINARYMAX
    | VARCHAR
    | VARCHARMAX
    | VARIANT
    | XML
    | DATETIME
    | SYSNAME
    | CURSOR
    ;
    */
/*
 * Lexer Rules
 */

BYTE_ORDER_MARK: '\u00EF\u00BB\u00BF';

// A.1. Documentation Comments
/*
SINGLE_LINE_DOC_COMMENT 
  : ('----' Input_character*) -> channel(HIDDEN)
  ;
DELIMITED_DOC_COMMENT 
  : ('/**' Delimited_comment_section* Asterisks '/') -> channel(HIDDEN) 
  ;
*/

//B.1.1 Line Terminators
NEW_LINE 
  : ('\u000D' //'<Carriage Return Character (U+000D)>'
  | '\u000A' //'<Line Feed Character (U+000A)>'
  | '\u000D' '\u000A' //'<Carriage Return Character (U+000D) Followed By Line Feed Character (U+000A)>'
  | '\u0085' //<Next Line Character (U+0085)>'
  | '\u2028' //'<Line Separator Character (U+2028)>'
  | '\u2029' //'<Paragraph Separator Character (U+2029)>'
  ) -> channel(HIDDEN)
  ;

//B.1.2 Comments
SINGLE_LINE_COMMENT 
  : ('--' Input_character* NEW_LINE?) -> channel(HIDDEN)
  ;

fragment Input_characters
  : Input_character+
  ;
fragment Input_character 
  : ~([\u000D\u000A\u0085\u2028\u2029]) //'<Any Unicode Character Except A NEW_LINE_CHARACTER>'
  ;
fragment NEW_LINE_CHARACTER 
  : '\u000D' //'<Carriage Return Character (U+000D)>'
  | '\u000A' //'<Line Feed Character (U+000A)>'
  | '\u0085' //'<Next Line Character (U+0085)>'
  | '\u2028' //'<Line Separator Character (U+2028)>'
  | '\u2029' //'<Paragraph Separator Character (U+2029)>'
  ;

DELIMITED_COMMENT 
  : ('/*' Delimited_comment_section* Asterisks '/') -> channel(HIDDEN)
  ;
fragment Delimited_comment_section 
  : '/'
  | Asterisks? Not_slash_or_asterisk
  ;
fragment Asterisks 
  : '*'+
  ;
//'<Any Unicode Character Except / Or *>'
fragment Not_slash_or_asterisk 
  : ~( '/' | '*' )
  ;
  
//B.1.3 White Space
WHITESPACE 
  : (Whitespace_characters) -> channel(HIDDEN)
  ;

fragment Whitespace_characters 
  : Whitespace_character+
  ;

fragment Whitespace_character 
  : UNICODE_CLASS_ZS //'<Any Character With Unicode Class Zs>'
  | '\r'
  | '\n'
  | '\u0009' //'<Horizontal Tab Character (U+0009)>'
  | '\u000B' //'<Vertical Tab Character (U+000B)>'
  | '\u000C' //'<Form Feed Character (U+000C)>'
  ;
  
//B.1.5 Unicode Character Escape Sequences
fragment Unicode_escape_sequence 
  : '\\u' HEX_DIGIT HEX_DIGIT HEX_DIGIT HEX_DIGIT
  | '\\U' HEX_DIGIT HEX_DIGIT HEX_DIGIT HEX_DIGIT HEX_DIGIT HEX_DIGIT HEX_DIGIT HEX_DIGIT
  ;

//B.1.7 Keywords
CREATE 
    : C R E A T E
    ;

ALTER 
    : A L T E R
    ;

PROCEDURE 
    : P R O C E D U R E
    | P R O C
    ;

AS 
    : A S
    ;

WITH
    : W I T H
    ;

EXECUTE 
    : E X E C U T E
    ;

CALLER
    : C A L L E R
    ;

MAX
    : M A X
    ;

NULL
    : N U L L
    ;

OUTPUT
    : O U T P U T
    | O U T
    ;

RECOMPILE
    : R E C O M P I L E
    ;

TRUE
    : T R U E
    ;

FALSE
    : F A L S E
    ;


// Types
/*
DECIMAL_INTEGER
    : Decimal_digit_character
    ;

NEGATIVE_DECIMAL_INTEGER
    : '-' Decimal_digit_character+
    ;

REAL_LITERAL_1
    : DECIMAL_INTEGER* '.' DECIMAL_INTEGER+;

REAL_LITERAL_2
    : DECIMAL_INTEGER+;

REAL_LITERAL
    : REAL_LITERAL_1 
    | REAL_LITERAL_2;

NEGATIVE_REAL_LITERAL_1
    : '-' DECIMAL_INTEGER+ '.' DECIMAL_INTEGER+;

NEGATIVE_REAL_LITERAL_2
    : '-' DECIMAL_INTEGER+;

NEGATIVE_REAL_LITERAL
    : NEGATIVE_REAL_LITERAL_1 
    | NEGATIVE_REAL_LITERAL_2;
*/

  /*
LITERAL
    : STRING_LITERAL
    | INTEGER_LITERAL+
    | REAL_LITERAL
    ;
    */
BIGINT
    : B I G I N T
    | OPEN_BRACKET B I G I N T CLOSE_BRACKET
    ;

BINARY
    : B I N A R Y
    | OPEN_BRACKET B I N A R Y CLOSE_BRACKET
    ;
    /*
BINARYSIZE
    : B I N A R Y WHITESPACE* OPEN_PARENS INTEGER_LITERAL+ CLOSE_PARENS
    | OPEN_BRACKET B I N A R Y CLOSE_BRACKET WHITESPACE* OPEN_PARENS INTEGER_LITERAL+ CLOSE_PARENS
    ;
    */
BIT
    : B I T
    | OPEN_BRACKET B I T CLOSE_BRACKET
    ;

CHAR
    : C H A R
    | OPEN_BRACKET C H A R CLOSE_BRACKET
    ;
DECIMAL
    : D E C I M A L
    | OPEN_BRACKET D E C I M A L CLOSE_BRACKET
    ;
    /*
DECIMAL
    : D E C I M A L WHITESPACE* OPEN_PARENS INTEGER_LITERAL+ ',' INTEGER_LITERAL+ CLOSE_PARENS
    | OPEN_BRACKET D E C I M A L CLOSE_BRACKET WHITESPACE* OPEN_PARENS INTEGER_LITERAL+ ',' INTEGER_LITERAL+ CLOSE_PARENS
    ;
    */
NUMERIC 
    : N U M E R I C
    | OPEN_BRACKET N U M E R I C CLOSE_BRACKET
    ;
    /*
NUMERIC 
    : N U M E R I C WHITESPACE* OPEN_PARENS INTEGER_LITERAL+ ',' INTEGER_LITERAL+ CLOSE_PARENS
    | OPEN_BRACKET N U M E R I C CLOSE_BRACKET WHITESPACE* OPEN_PARENS INTEGER_LITERAL+ ',' INTEGER_LITERAL+ CLOSE_PARENS
    ;
    */
FLOAT
    : F L O A T
    | OPEN_BRACKET F L O A T CLOSE_BRACKET
    ;

IMAGE
    : I M A G E
    | OPEN_BRACKET I M A G E CLOSE_BRACKET
    ;

INTEGER
    : I N T E G E R
    | OPEN_BRACKET I N T E G E R CLOSE_BRACKET
    | I N T
    | OPEN_BRACKET I N T CLOSE_BRACKET
    ;

MONEY
    : M O N E Y
    | OPEN_BRACKET M O N E Y CLOSE_BRACKET
    ;

NCHAR
    : N C H A R
    | OPEN_BRACKET N C H A R CLOSE_BRACKET
    ;
    /*
NCHARMAX
    : N C H A R WHITESPACE* OPEN_PARENS MAX CLOSE_PARENS
    | OPEN_BRACKET N C H A R CLOSE_BRACKET WHITESPACE* OPEN_PARENS MAX CLOSE_PARENS
    ;

NCHARSIZE 
    : N C H A R WHITESPACE* OPEN_PARENS INTEGER_LITERAL+ CLOSE_PARENS
    | OPEN_BRACKET N C H A R CLOSE_BRACKET WHITESPACE* OPEN_PARENS INTEGER_LITERAL+ CLOSE_PARENS
    ;
    */
NTEXT
    : N T E X T
    | OPEN_BRACKET N T E X T CLOSE_BRACKET
    ;

NVARCHAR
    : N V A R C H A R
    | OPEN_BRACKET N V A R C H A R CLOSE_BRACKET
    ;
    /*
NVARCHAR
    : N V A R C H A R WHITESPACE* OPEN_PARENS INTEGER_LITERAL+ CLOSE_PARENS
    | OPEN_BRACKET N V A R C H A R CLOSE_BRACKET WHITESPACE* OPEN_PARENS INTEGER_LITERAL+ CLOSE_PARENS
    ;

NVARCHARMAX 
    : N V A R C H A R WHITESPACE* OPEN_PARENS MAX CLOSE_PARENS
    | OPEN_BRACKET N V A R C H A R CLOSE_BRACKET WHITESPACE* OPEN_PARENS MAX CLOSE_PARENS
    ;
    */
REAL
    : R E A L
    | OPEN_BRACKET R E A L CLOSE_BRACKET
    ;

UNIQUEIDENTIFIER
    : U N I Q U E I D E N T I F I E R
    | OPEN_BRACKET U N I Q U E I D E N T I F I E R CLOSE_BRACKET
    ;

SMALLDATETIME
    : S M A L L D A T E T I M E
    | OPEN_BRACKET S M A L L D A T E T I M E CLOSE_BRACKET
    ;

SMALLINT
    : S M A L L I N T
    | OPEN_BRACKET S M A L L I N T CLOSE_BRACKET
    ;

SMALLMONEY
    : S M A L L M O N E Y
    | OPEN_BRACKET S M A L L M O N E Y CLOSE_BRACKET
    ;

TEXT
    : T E X T
    | OPEN_BRACKET T E X T CLOSE_BRACKET
    ;

TIMESTAMP
    : T I M E S T A M P
    | OPEN_BRACKET T I M E S T A M P CLOSE_BRACKET
    ;

TINYINT
    : T I N Y I N T
    | OPEN_BRACKET T I N Y I N T CLOSE_BRACKET
    ;

VARBINARY
    : V A R B I N A R Y
    | OPEN_BRACKET V A R B I N A R Y CLOSE_BRACKET
    ;
/*
VARBINARY
    : V A R B I N A R Y WHITESPACE* OPEN_PARENS INTEGER_LITERAL+ CLOSE_PARENS
    | OPEN_BRACKET V A R B I N A R Y CLOSE_BRACKET WHITESPACE* OPEN_PARENS INTEGER_LITERAL+ CLOSE_PARENS
    ;

VARBINARYMAX
    : V A R B I N A R Y WHITESPACE* OPEN_PARENS MAX CLOSE_PARENS
    | OPEN_BRACKET V A R B I N A R Y CLOSE_BRACKET WHITESPACE* OPEN_PARENS MAX CLOSE_PARENS
    ;
    */
VARCHAR
    : V A R C H A R
    | OPEN_BRACKET V A R C H A R CLOSE_BRACKET
    ;
    /*
VARCHAR
    : V A R C H A R WHITESPACE* OPEN_PARENS INTEGER_LITERAL+ CLOSE_PARENS
    | OPEN_BRACKET V A R C H A R CLOSE_BRACKET WHITESPACE* OPEN_PARENS INTEGER_LITERAL+ CLOSE_PARENS
    ;

VARCHARMAX
    : V A R C H A R WHITESPACE* OPEN_PARENS MAX CLOSE_PARENS
    | OPEN_BRACKET V A R C H A R CLOSE_BRACKET WHITESPACE* OPEN_PARENS MAX CLOSE_PARENS
    ;
    */
VARIANT
    : V A R I A N T
    | OPEN_BRACKET V A R I A N T CLOSE_BRACKET
    ;

XML
    : X M L
    | OPEN_BRACKET X M L CLOSE_BRACKET
    ;

DATETIME
    : D A T E T I M E
    | OPEN_BRACKET D A T E T I M E CLOSE_BRACKET
    ;

SYSNAME
    : S Y S N A M E
    | OPEN_BRACKET S Y S N A M E CLOSE_BRACKET
    ;

CURSOR
    : C U R S O R WHITESPACE+ V A R Y I N G
    | OPEN_BRACKET C U R S O R WHITESPACE+ V A R Y I N G CLOSE_BRACKET;


//B.1.6 Identifiers
// must be defined after all keywords so the first branch (Available_identifier) does not match keywords 
PARAM_IDENTIFIER
    : PLUSHKA IDENTIFIER
    ;

DB_IDENTIFIER
    : IDENTIFIER
    | OPEN_BRACKET IDENTIFIER CLOSE_BRACKET
    ;

IDENTIFIER
  : Available_identifier
  | Identifier_or_keyword
  ;

//'<An Identifier_or_keyword That Is Not A Keyword>'
// WARNING: ignores exclusion
fragment Available_identifier 
  : Identifier_or_keyword
  ;
fragment Identifier_or_keyword 
  : Identifier_start_character Identifier_part_character*
  ;
fragment Identifier_start_character 
  : Letter_character
  | '_'
  ;
fragment Identifier_part_character 
  : Letter_character
  | Decimal_digit_character
  | Connecting_character
  | Combining_character
  | Formatting_character
  ;
//'<A Unicode Character Of Classes Lu, Ll, Lt, Lm, Lo, Or Nl>'
// WARNING: ignores Unicode_escape_sequence
fragment Letter_character 
  : UNICODE_CLASS_LU
  | UNICODE_CLASS_LL
  | UNICODE_CLASS_LT
  | UNICODE_CLASS_LM
  | UNICODE_CLASS_LO
  | UNICODE_CLASS_NL
//  | '<A Unicode_escape_sequence Representing A Character Of Classes Lu, Ll, Lt, Lm, Lo, Or Nl>'
  ;
//'<A Unicode Character Of Classes Mn Or Mc>'
// WARNING: ignores Unicode_escape_sequence
fragment Combining_character 
  : UNICODE_CLASS_MN
  | UNICODE_CLASS_MC
//  | '<A Unicode_escape_sequence Representing A Character Of Classes Mn Or Mc>'
  ;
//'<A Unicode Character Of The Class Nd>'
// WARNING: ignores Unicode_escape_sequence
fragment Decimal_digit_character 
  : UNICODE_CLASS_ND
//  | '<A Unicode_escape_sequence Representing A Character Of The Class Nd>'
  ;
//'<A Unicode Character Of The Class Pc>'
// WARNING: ignores Unicode_escape_sequence
fragment Connecting_character 
  : UNICODE_CLASS_PC
//  | '<A Unicode_escape_sequence Representing A Character Of The Class Pc>'
  ;
//'<A Unicode Character Of The Class Cf>'
// WARNING: ignores Unicode_escape_sequence
fragment Formatting_character 
  : UNICODE_CLASS_CF
//  | '<A Unicode_escape_sequence Representing A Character Of The Class Cf>'
  ;

//B.1.8 Literals
INTEGER_LITERAL 
  : Decimal_integer_literal
  | Hexadecimal_integer_literal
  ;
fragment Decimal_integer_literal 
  : Decimal_digits
  ;
fragment Decimal_digits 
  : DECIMAL_DIGIT+
  ;
fragment DECIMAL_DIGIT 
  : '0'..'9'
  ;
  /*
fragment Integer_type_suffix 
  : 'U'
  | 'u'
  | 'L'
  | 'l'
  | 'UL'
  | 'Ul'
  | 'uL'
  | 'ul'
  | 'LU'
  | 'Lu'
  | 'lU'
  | 'lu'
  ;
  */
fragment Hexadecimal_integer_literal 
  : ('0x' | '0X') Hex_digits
  ;
fragment Hex_digits 
  : HEX_DIGIT+
  ;
fragment HEX_DIGIT 
  : '0'..'9'
  | 'A'..'F'
  | 'a'..'f'
  ;
// added by chw
// For the rare case where 0.ToString() etc is used.
// Explanation: 0.Equals() would be parsed as an invalid real (1. branch) causing a lexer error
LiteralAccess
  : INTEGER_LITERAL DOT IDENTIFIER       
  ;

REAL_LITERAL
  : Decimal_digits DOT Decimal_digits Exponent_part? Real_type_suffix?
  | DOT Decimal_digits Exponent_part? Real_type_suffix?
  | Decimal_digits Exponent_part Real_type_suffix?
  | Decimal_digits Real_type_suffix
  ;
fragment Exponent_part 
  : ('e' | 'E') Sign? Decimal_digits
  ;
fragment Sign 
  : '+'
  | '-'
  ;
fragment Real_type_suffix 
  : 'F'
  | 'f'
  | 'D'
  | 'd'
  | 'M'
  | 'm'
  ;
/*
CHARACTER_LITERAL 
  : QUOTE Character QUOTE
  ;
*/
fragment Character 
  : Single_character
  | Simple_escape_sequence
  | Hexadecimal_escape_sequence
  | Unicode_escape_sequence
  ;
fragment Single_character 
  : ~(['\\\u000D\u000A\u000D\u0085\u2028\u2029]) //'<Any Character Except \' (U+0027), \\ (U+005C), And NEW_LINE_CHARACTER>'
  ;
/*fragment Simple_escape_sequence 
  : '\\\''
  | '\\"'
  | DOUBLE_BACK_SLASH
  | '\\0'
  | '\\a'
  | '\\b'
  | '\\f'
  | '\\n'
  | '\\r'
  | '\\t'
  | '\\v'
  ;*/
fragment Simple_escape_sequence 
  : DOUBLE_BACK_SLASH
  | '\\0'
  | '\\a'
  | '\\b'
  | '\\f'
  | '\\n'
  | '\\r'
  | '\\t'
  | '\\v'
  ;
fragment Hexadecimal_escape_sequence 
  : '\\x' HEX_DIGIT
  | '\\x' HEX_DIGIT HEX_DIGIT
  | '\\x' HEX_DIGIT HEX_DIGIT HEX_DIGIT
  | '\\x' HEX_DIGIT HEX_DIGIT HEX_DIGIT HEX_DIGIT
  ;
  /*
STRING_LITERAL 
    : N_BIG? QUOTE Regular_string_literal_character* QUOTE
    ;
    */
STRING_LITERAL
    : N? '\'' (~'\'' | '\'\'')* '\''
    ;

/*
STRING_LITERAL 
  : Regular_string_literal
  | Verbatim_string_literal
  ;

fragment Regular_string_literal 
  : QUOTE Regular_string_literal_character* QUOTE
  ;
*/

fragment Regular_string_literal_character 
  : Single_regular_string_literal_character
  /*| Simple_escape_sequence*/
  /*| Hexadecimal_escape_sequence*/
  | Unicode_escape_sequence
  ;
//'<Any Character Except " (U+0022), \\ (U+005C), And NEW_LINE_CHARACTER>'
fragment Single_regular_string_literal_character 
  : ~(["\\\u000D\u000A\u000D\u0085\u2028\u2029])
  ;

/*
fragment Verbatim_string_literal 
  : '@' QUOTE Verbatim_string_literal_character* QUOTE
  ;
fragment Verbatim_string_literal_character 
  : Single_verbatim_string_literal_character
  | Quote_escape_sequence
  ;
fragment Single_verbatim_string_literal_character 
  : ~([']) //<any Character Except ">
  ;
fragment Quote_escape_sequence 
  : QUOTE QUOTE
  ;
*/


//B.1.9 Operators And Punctuators
OPEN_BRACE : '{';
CLOSE_BRACE : '}';
OPEN_BRACKET : '[';
CLOSE_BRACKET : ']';
OPEN_PARENS : '(';
CLOSE_PARENS : ')';
DOT : '.';
COMMA : ',';
COLON : ':';
SEMICOLON : ';';
PLUS : '+';
MINUS : '-';
STAR : '*';
DIV : '/';
PERCENT : '%';
AMP : '&';
BITWISE_OR : '|';
CARET : '^';
BANG : '!';
TILDE : '~';
ASSIGNMENT : '=';
LT : '<';
GT : '>';
/*
INTERR : '?';
DOUBLE_COLON : '::';
OP_COALESCING : '??';
OP_INC : '++';
OP_DEC : '--';
OP_AND : '&&';
OP_OR : '||';
OP_PTR : '->';
OP_EQ : '==';
OP_NE : '!=';
OP_LE : '<=';
OP_GE : '>=';
OP_ADD_ASSIGNMENT : '+=';
OP_SUB_ASSIGNMENT : '-=';
OP_MULT_ASSIGNMENT : '*=';
OP_DIV_ASSIGNMENT : '/=';
OP_MOD_ASSIGNMENT : '%=';
OP_AND_ASSIGNMENT : '&=';
OP_OR_ASSIGNMENT : '|=';
OP_XOR_ASSIGNMENT : '^=';
OP_LEFT_SHIFT : '<<';
OP_LEFT_SHIFT_ASSIGNMENT : '<<=';
*/
PLUSHKA : '@';

//B.1.10 Pre_processing Directives
// see above

// Custome Lexer rules
QUOTE :             '\'';
// DOUBLE_QUOTE :      '"';
BACK_SLASH :        '\\';
DOUBLE_BACK_SLASH : '\\\\';
SHARP :             '#';


//// Unicode character classes
fragment UNICODE_CLASS_ZS
  : '\u0020' // SPACE
  | '\u00A0' // NO_BREAK SPACE
  | '\u1680' // OGHAM SPACE MARK
  | '\u180E' // MONGOLIAN VOWEL SEPARATOR
  | '\u2000' // EN QUAD
  | '\u2001' // EM QUAD
  | '\u2002' // EN SPACE
  | '\u2003' // EM SPACE
  | '\u2004' // THREE_PER_EM SPACE
  | '\u2005' // FOUR_PER_EM SPACE
  | '\u2006' // SIX_PER_EM SPACE
  | '\u2008' // PUNCTUATION SPACE
  | '\u2009' // THIN SPACE
  | '\u200A' // HAIR SPACE
  | '\u202F' // NARROW NO_BREAK SPACE
  | '\u3000' // IDEOGRAPHIC SPACE
  | '\u205F' // MEDIUM MATHEMATICAL SPACE
  ;
  
fragment UNICODE_CLASS_LU
  : '\u0041'..'\u005A' // LATIN CAPITAL LETTER A_Z
  | '\u00C0'..'\u00DE' // ACCENTED CAPITAL LETTERS
//  | { isUnicodeClass_Lu($text) }?
  ;

fragment UNICODE_CLASS_LL
  : '\u0061'..'\u007A' // LATIN SMALL LETTER a_z
  ;

fragment UNICODE_CLASS_LT
  : '\u01C5' // LATIN CAPITAL LETTER D WITH SMALL LETTER Z WITH CARON
  | '\u01C8' // LATIN CAPITAL LETTER L WITH SMALL LETTER J
  | '\u01CB' // LATIN CAPITAL LETTER N WITH SMALL LETTER J
  | '\u01F2' // LATIN CAPITAL LETTER D WITH SMALL LETTER Z
  ;

fragment UNICODE_CLASS_LM
  : '\u02B0'..'\u02EE' // MODIFIER LETTERS
  ;

fragment UNICODE_CLASS_LO
  : '\u01BB' // LATIN LETTER TWO WITH STROKE
  | '\u01C0' // LATIN LETTER DENTAL CLICK
  | '\u01C1' // LATIN LETTER LATERAL CLICK
  | '\u01C2' // LATIN LETTER ALVEOLAR CLICK
  | '\u01C3' // LATIN LETTER RETROFLEX CLICK
  | '\u0294' // LATIN LETTER GLOTTAL STOP
  ;

fragment UNICODE_CLASS_NL
  : '\u16EE' // RUNIC ARLAUG SYMBOL
  | '\u16EF' // RUNIC TVIMADUR SYMBOL
  | '\u16F0' // RUNIC BELGTHOR SYMBOL
  | '\u2160' // ROMAN NUMERAL ONE
  | '\u2161' // ROMAN NUMERAL TWO
  | '\u2162' // ROMAN NUMERAL THREE
  | '\u2163' // ROMAN NUMERAL FOUR
  | '\u2164' // ROMAN NUMERAL FIVE
  | '\u2165' // ROMAN NUMERAL SIX
  | '\u2166' // ROMAN NUMERAL SEVEN
  | '\u2167' // ROMAN NUMERAL EIGHT
  | '\u2168' // ROMAN NUMERAL NINE
  | '\u2169' // ROMAN NUMERAL TEN
  | '\u216A' // ROMAN NUMERAL ELEVEN
  | '\u216B' // ROMAN NUMERAL TWELVE
  | '\u216C' // ROMAN NUMERAL FIFTY
  | '\u216D' // ROMAN NUMERAL ONE HUNDRED
  | '\u216E' // ROMAN NUMERAL FIVE HUNDRED
  | '\u216F' // ROMAN NUMERAL ONE THOUSAND
  ;

fragment UNICODE_CLASS_MN
  : '\u0300' // COMBINING GRAVE ACCENT
  | '\u0301' // COMBINING ACUTE ACCENT
  | '\u0302' // COMBINING CIRCUMFLEX ACCENT
  | '\u0303' // COMBINING TILDE
  | '\u0304' // COMBINING MACRON
  | '\u0305' // COMBINING OVERLINE
  | '\u0306' // COMBINING BREVE
  | '\u0307' // COMBINING DOT ABOVE
  | '\u0308' // COMBINING DIAERESIS
  | '\u0309' // COMBINING HOOK ABOVE
  | '\u030A' // COMBINING RING ABOVE
  | '\u030B' // COMBINING DOUBLE ACUTE ACCENT
  | '\u030C' // COMBINING CARON
  | '\u030D' // COMBINING VERTICAL LINE ABOVE
  | '\u030E' // COMBINING DOUBLE VERTICAL LINE ABOVE
  | '\u030F' // COMBINING DOUBLE GRAVE ACCENT
  | '\u0310' // COMBINING CANDRABINDU
  ;

fragment UNICODE_CLASS_MC
  : '\u0903' // DEVANAGARI SIGN VISARGA
  | '\u093E' // DEVANAGARI VOWEL SIGN AA
  | '\u093F' // DEVANAGARI VOWEL SIGN I
  | '\u0940' // DEVANAGARI VOWEL SIGN II
  | '\u0949' // DEVANAGARI VOWEL SIGN CANDRA O
  | '\u094A' // DEVANAGARI VOWEL SIGN SHORT O
  | '\u094B' // DEVANAGARI VOWEL SIGN O
  | '\u094C' // DEVANAGARI VOWEL SIGN AU
  ;

fragment UNICODE_CLASS_CF
  : '\u00AD' // SOFT HYPHEN
  | '\u0600' // ARABIC NUMBER SIGN
  | '\u0601' // ARABIC SIGN SANAH
  | '\u0602' // ARABIC FOOTNOTE MARKER
  | '\u0603' // ARABIC SIGN SAFHA
  | '\u06DD' // ARABIC END OF AYAH
  ;

fragment UNICODE_CLASS_PC
  : '\u005F' // LOW LINE
  | '\u203F' // UNDERTIE
  | '\u2040' // CHARACTER TIE
  | '\u2054' // INVERTED UNDERTIE
  | '\uFE33' // PRESENTATION FORM FOR VERTICAL LOW LINE
  | '\uFE34' // PRESENTATION FORM FOR VERTICAL WAVY LOW LINE
  | '\uFE4D' // DASHED LOW LINE
  | '\uFE4E' // CENTRELINE LOW LINE
  | '\uFE4F' // WAVY LOW LINE
  | '\uFF3F' // FULLWIDTH LOW LINE
  ;

fragment UNICODE_CLASS_ND
  : '\u0030' // DIGIT ZERO
  | '\u0031' // DIGIT ONE
  | '\u0032' // DIGIT TWO
  | '\u0033' // DIGIT THREE
  | '\u0034' // DIGIT FOUR
  | '\u0035' // DIGIT FIVE
  | '\u0036' // DIGIT SIX
  | '\u0037' // DIGIT SEVEN
  | '\u0038' // DIGIT EIGHT
  | '\u0039' // DIGIT NINE
  ;

fragment A : [aA];
fragment B : [bB];
fragment C : [cC];
fragment D : [dD];
fragment E : [eE];
fragment F : [fF];
fragment G : [gG];
fragment H : [hH];
fragment I : [iI];
fragment J : [jJ];
fragment K : [kK];
fragment L : [lL];
fragment M : [mM];
fragment N : [nN];
fragment O : [oO];
fragment P : [pP];
fragment Q : [qQ];
fragment R : [rR];
fragment S : [sS];
fragment T : [tT];
fragment U : [uU];
fragment V : [vV];
fragment W : [wW];
fragment X : [xX];
fragment Y : [yY];
fragment Z : [zZ];