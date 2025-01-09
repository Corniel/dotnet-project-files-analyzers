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
    public sealed override ResultCollection Tokenize(TokenStream stream)
    {
        var len = Match(stream.Remaining);
        if (len > 0)
        {
            var add = stream.Add(len, Kind);
            var token = new SourceSpanToken(add[^1].SourceSpan, Kind);
            var node = new Syntax.Token(token);
            return ResultCollection.Empty.Add(Result.Successful(node, add));
        }
        else
        {
            return ResultCollection.Empty.Add(Result.NoMatch(stream, $"Expected {Kind}."));
        }
    }

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
