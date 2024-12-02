using DotNetProjectFile.Collections;
using DotNetProjectFile.Syntax;
using Grammr.Text;
using Microsoft.CodeAnalysis.Text;
using SyntaxNode = DotNetProjectFile.Syntax.SyntaxNode;

namespace DotNetProjectFile.Parsing;

/// <summary>A syntax parser.</summary>
[DebuggerDisplay("Tokens: {Tokens.Count}, {SourceSpan.Span}, Text = {SourceSpan.Text}")]
public readonly struct Parser
{
    /// <summary>Initializes a new instance of the <see cref="Parser"/> struct.</summary>
    private Parser(SourceSpan sourceSpan, ParseBuffer tokens, int cursor, Matching state, SyntaxNode syntax)
    {
        SourceSpan = sourceSpan;
        Tokens = tokens;
        Cursor = cursor;
        State = state;
        Syntax = syntax;
    }

    /// <summary>The tokenized tokens so far.</summary>
    public readonly ParseBuffer Tokens;

    public readonly int Cursor;

    /// <summary>The (remaining) source span to tokenize.</summary>
    public readonly SourceSpan SourceSpan;

    /// <summary>The matching state.</summary>
    public readonly Matching State;

    /// <summary>The syntax.</summary>
    public readonly SyntaxNode Syntax;

    public SliceSpan Span => new(Cursor, Tokens.Count - Cursor);

    /// <summary>Tries to apply a <see cref="SourceSpan.Match"/>.</summary>
    /// <param name="match">
    /// The match to apply.
    /// </param>
    /// <param name="kind">
    /// The token kind to add, if the match was successful.
    /// </param>
    /// <returns>
    /// A new parser with an updated state.
    /// </returns>
    [Pure]
    public Parser Match(SourceSpan.Match match, string? kind = null) => match(SourceSpan) switch
    {
        null => NoMatch,
        var span when span.Value.Length == 0 => this,
        var span => New(span.Value, kind),
    };

    [Pure]
    public Parser WithSyntax(SyntaxNode syntax) => new(SourceSpan, Tokens, Tokens.Count, State, syntax);

    /// <summary>Creates a new state.</summary>
    /// <param name="span">
    /// The matching span.
    /// </param>
    /// <param name="kind">
    /// The kind of the token to add.
    /// </param>
    /// <returns>
    /// A new parser with an updated state.
    /// </returns>
    [Pure]
    private Parser New(TextSpan span, string? kind = null)
    {
        var token = new SourceSpanToken(SourceSpan.Trim(span), kind);
        var trimmed = SourceSpan.Skip(span.Length);
        return new(trimmed, Tokens.Add(token), Cursor, trimmed.IsEmpty ? Matching.EoF : Matching.Match, Syntax);
    }

    /// <summary>Creates a new state.</summary>
    /// <param name="sourceSpan">
    /// The source span to tokenize.
    /// </param>
    /// <returns>
    /// A new parser with an updated state.
    /// </returns>
    [Pure]
    public static Parser New(SourceSpan sourceSpan)
        => new(sourceSpan, Empty, 0, sourceSpan.Length == 0 ? Matching.EoF : Matching.Match, RootSyntax);

    private static readonly ParseBuffer Empty = ParseBuffer.Empty;

    /// <summary>The result of <see cref="Matching.NoMatch"/>.</summary>
    public static readonly Parser NoMatch = new(default, Empty, 0, Matching.NoMatch, null!);

    private static readonly RootSyntax RootSyntax = new();
}
