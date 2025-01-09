using DotNetProjectFile.Parsing;
using static DotNetProjectFile.Ini.TokenKind;

namespace DotNetProjectFile.Ini;

/// <summary>
/// Represents the grammer for an INI file.
/// </summary>
/// <remarks>
/// Unparsable is separated from the happy flow for performance reasons.
/// </remarks>
internal sealed class IniGrammarBak : Grammar
{
    public static readonly Grammar eol = eof | str("\r\n", EoLToken) | ch('\n', EoLToken);

    public static readonly Grammar ws = match(IsWhitespace, WhitespaceToken).Option;

    public static readonly Grammar ws_only = line(@"^\s*$", WhitespaceToken);

    public static readonly Grammar garbage = line(".+", UnparsableToken);

    public static readonly Grammar comment =
        ws
        & (ch('#', CommentDelimiterToken) | ch(';', CommentDelimiterToken))
        & line(".*", CommentToken);

    public static readonly Grammar header_start = ws & Header.start;

    public static readonly Grammar header =
        (header_start
        & Header.text
        & Header.end
        & ws
        & comment.Option
        & eol)
        + HeaderSyntax.New;

    public static readonly Grammar kvp =
        (ws
        & INI.key + KeySyntax.New
        & ws
        & INI.assign
        & ws
        & INI.value + ValueSyntax.New
        & ws
        & comment.Option)
        + KeyValuePairSyntax.New;

    public static readonly Grammar kvp_line =
        ws_only
        | kvp
        | comment
        | Invalid.kvp;

    public static readonly Grammar single_line =
        ~header_start
        & (kvp_line | garbage)
        & eol;

    public static readonly Grammar section = ((header | Invalid.header) & single_line.Star) + SectionSyntax.New;

    public static readonly Grammar file = (single_line.Star & section.Star) + IniFileSyntax.New;

    /// <summary>Header tokens.</summary>
    private sealed class Header : Grammar
    {
        public static Grammar start = ch('[', HeaderStartToken);
        public static Grammar text = line(@"[^[\]]+", HeaderTextToken);
        public static Grammar end = ch(']', HeaderEndToken);
    }

    /// <summary>INI tokens.</summary>
    private sealed class INI : Grammar
    {
        public static readonly Grammar key = line(@"[^\s:=#;[\]]+", KeyToken);
        public static readonly Grammar assign = ch('=', EqualsToken) | ch(':', ColonToken);
        public static readonly Grammar value = line(@"[^=:^\s#;][^\s#;]*", ValueToken);
    }

    private sealed class Invalid : Grammar
    {
        public static readonly Grammar header =
           (header_start
           & Header.start.Star
           & Header.text.Option
           & Header.end.Star
           & ws
           & comment.Option
           & eol)
           + HeaderSyntax.New;

        public static readonly Grammar kvp =
           (ws
           & (INI.key & ws).Star
           & (INI.assign & ws).Star
           & INI.value.Star
           & ws
           & comment.Option)
           + KeyValuePairSyntax.Invalid;
    }

    private static bool IsWhitespace(char ch) => ch == ' ' || ch == '\t';
}

public static class TokenKind
{
    public const string ValueToken = nameof(ValueToken);
    public const string KeyToken = nameof(KeyToken);
    public const string WhitespaceToken = nameof(WhitespaceToken);
    public const string CommentDelimiterToken = nameof(CommentDelimiterToken);
    public const string CommentToken = nameof(CommentToken);
    public const string EoLToken = nameof(EoLToken);
    public const string HeaderTextToken = nameof(HeaderTextToken);
    public const string HeaderStartToken = nameof(HeaderStartToken);
    public const string HeaderEndToken = nameof(HeaderEndToken);
    public const string EqualsToken = nameof(EqualsToken);
    public const string ColonToken = nameof(ColonToken);
    public const string UnparsableToken = nameof(UnparsableToken);
}
