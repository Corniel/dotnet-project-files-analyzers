using Grammr.Text;

namespace Grammr;

/// <summary>Represents a single token.</summary>
public abstract class Token : Tokens
{
    /// <summary>Initializes a new instance of the <see cref="Token"/> class.</summary>
    protected Token(string? kind) => Kind = kind ?? GetType().Name;

    /// <summary>The kind of the token.</summary>
    public string Kind { get; }

    /// <inheritdoc />
    [Pure]
    public sealed override ResultCollection Tokenize(SourceSpan source) => Match(source) switch
    {
        var len when len > 0 => ResultCollection.Empty.Add(Result.Successful(source.Skip(len), new SourceSpanToken(source.Take(len), Kind))),
        _ => ResultCollection.Empty.Add(Result.NoMatch(source, $"Expected {Kind}.")),
    };

    /// <summary>
    /// Returns the length of the matched token, and zero the source does not
    /// start with a matching token.
    /// </summary>
    /// <param name="source">
    /// The source text to match on.
    /// </param>
    [Pure]
    public abstract int Match(SourceSpan source);
}
