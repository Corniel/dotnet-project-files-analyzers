using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Grammr;
using Microsoft.CodeAnalysis.Text;
using System;

namespace Antlr4;

public class AbstractSyntaxTree(SourceText sourceText, ITokenStream stream, IVocabulary vocabulary, IReadOnlyList<AntlrSyntaxError> errors)
{
    public IOFile Path => path;

    private readonly ITokenStream Stream = stream;

    private readonly SourceText SourceText = sourceText;

    private readonly IVocabulary Vocabulary = vocabulary;

    public IReadOnlyList<AntlrSyntaxError> Errors { get; } = errors;

    /// <summary>Gets the location of the token.</summary>
    [Pure]
    public Location GetLocation(StreamToken token)
        => Location.Create(Path.ToString(), token.TextSpan, token.LineSpan);

    /// <summary>Gets the location of the syntax.</summary>
    [Pure]
    public Location GetLocation(AntlrSyntax syntax)
        => Location.Create(Path.ToString(), syntax.TextSpan, syntax.LineSpan);

    /// <summary>Gets the location of the syntax.</summary>
    [Pure]
    public Location GetLocation(AntlrSyntaxError error)
        => Location.Create(Path.ToString(), error.TextSpan, LineSpan(error.TextSpan));

    public IReadOnlyList<StreamToken> Tokens(Interval interval)
    {
        if (tokens is null || tokens.Count != Stream.Size)
        {
            tokens = Enumerable.Range(0, Stream.Size)
                .Select(i => Stream.Get(i))
                .Select(t => new StreamToken(t, LineSpan(t), Vocabulary.GetDisplayName(t.Type), this))
                .ToArray();
        }

        return tokens.Slice(interval);
    }

    internal LinePositionSpan LineSpan(IToken token) 
        => token.Type == -1 // EOF
        ? SourceText.Lines.GetLinePositionSpan(new(token.StartIndex, 0))
        : SourceText.Lines.GetLinePositionSpan(new(token.StartIndex, token.StopIndex - token.StartIndex));

    internal LinePositionSpan LineSpan(TextSpan span)
        => SourceText.Lines.GetLinePositionSpan(span);

    internal void SetPath(IOFile path)
        => this.path = this.path.Equals(default)
        ? path
        : throw new InvalidOperationException("Path already set.");

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private IOFile path;

    private IReadOnlyList<StreamToken>? tokens;

    
}
