grammar Ini;

// Rules
file        : line ( NL+ line )* NL* EOF ;

line        : key ASSIGN value      #KeyValuePair
            | header_full           #SectionHeader
            ;

key         : TEXT;
value       : TEXT;
header_full : HEADER_L header_text HEADER_R;
header_text : ~']'+;

// Lexer
TEXT        : ([A-Za-z0-9] | '/' | '\\' | '*' | '.' | ',' | '@' | '-' | '_' | '{' | '}' )+;
ASSIGN      : '=' | ':';
HEADER_L    : '[';
HEADER_R    : ']';

// Trivia
NL      : '\r'? '\n';
COMMENT : ( '#' | ';' ) ~[\r\n]* -> channel(HIDDEN);
WS      : ( ' ' | '\t' )+        -> channel(HIDDEN);
