grammar SqlProcedureExt;

/*
 * Parser Rules
 */

compileUnit
    : extBlocks
    ;

extBlocks
    : ANY* extBlock* (ANY* extBlock*) ANY*
    ;

extBlock
    : OPEN_EXTBLOCK flags (description | CLOSE_EXTBLOCK)
    | OPEN_EXTBLOCK field (description | CLOSE_EXTBLOCK)
    ;

flags
    : FLAGS (PUBLIC | PRIVATE)?
    ;

field
    : FIELD PARAM_IDENTIFIER? (NOT? NULL)?
    ;

description
    : OPEN_DESCR ANY* CLOSE_EXTBLOCK
    ;

/*
 * Lexer Rules
 */

NEW_LINE 
  : ('\u000D' //'<Carriage Return Character (U+000D)>'
  | '\u000A' //'<Line Feed Character (U+000A)>'
  | '\u000D' '\u000A' //'<Carriage Return Character (U+000D) Followed By Line Feed Character (U+000A)>'
  | '\u0085' //<Next Line Character (U+0085)>'
  | '\u2028' //'<Line Separator Character (U+2028)>'
  | '\u2029' //'<Paragraph Separator Character (U+2029)>'
  ) -> channel(HIDDEN)
  ;

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

PLUSHKA : '@';

OPEN_DESCR
    : D S C ':'
    | D E S C ':'
    | D E S C R ':'
    | D E S C R I P T I O N ':'
    | '--'
    ;

FLAGS
    : F L A G S ':'
    ;

PUBLIC
    : P U B L I C
    ;

PRIVATE
    : P R I V A T E
    ;

FIELD
    : F L D ':'
    | F I E L D ':'
    ;

NOT
    : N O T
    ;

NULL
    : N U L L
    ;

OPEN_EXTBLOCK
    : '/*?'
    ;

CLOSE_EXTBLOCK
    : '?*/'
    ;

PARAM_IDENTIFIER
    : PLUSHKA IDENTIFIER
    ;

IDENTIFIER
    : Available_identifier
    | Identifier_or_keyword
    ;

ANY   
    : .
    ;

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