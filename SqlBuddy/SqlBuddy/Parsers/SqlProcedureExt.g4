grammar SqlProcedureExt;

/*
 * Parser Rules
 */

compileUnit
    :	extBlocks
    ;

extBlocks
    :   extBlock*
    ;

extBlock
    :   OPEN_EXTBLOCK flags CLOSE_EXTBLOCK
    ;

flags
    : FLAGS (PUBLIC | PRIVATE)?
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

FLAGS
    :   'flags:'
    ;

PUBLIC
    :   'public'
    ;

PRIVATE
    :   'private'
    ;

OPEN_EXTBLOCK
    :   '/*?'
    ;

CLOSE_EXTBLOCK
    :   '?*/'
    ;