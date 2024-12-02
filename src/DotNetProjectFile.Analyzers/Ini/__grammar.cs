using Grammr;

namespace DotNetProjectFile.Ini;

public sealed class INI_grammar : Grammar
{
    public static readonly Token EndOfLine = eol();
    public static readonly Token WhiteSpace = match(IsWhiteSpace);
    public static readonly Token Comment = regex(@"[#;][^\r]*");
    public static readonly Token HeaderStart = ch('[');
    public static readonly Token HeaderEnd = ch(']');
    public static readonly Token HeaderText = regex(@"[^[\]]*");
    public static readonly Token Colon = ch(':');
    public static readonly Token Equal = ch('=');
    public static readonly Token Word = regex("[\\w.-]+");

    public static readonly Tokens ws = WhiteSpace.Option;

    public static readonly Tokens ws_line = ws & EndOfLine;

    public static readonly Tokens header_line =
        ws
        & HeaderStart
        & HeaderText
        & HeaderEnd
        & ws
        & Comment.Option
        & EndOfLine.Option;

    public static readonly Tokens comment_line = ws & Comment & EndOfLine.Option;

    public static readonly Tokens kvp_line =
        ws
        & Word
        & ws
        & (Equal | Colon)
        & ws
        & Word
        & ws
        & Colon.Option
        & EndOfLine.Option;

    public static readonly Tokens line =
        ws_line
        | kvp_line
        | header_line
        | comment_line;

    public static readonly Tokens file = line.Star;

    private static bool IsWhiteSpace(char ch) => ch == ' ' || ch == '\t';
}
