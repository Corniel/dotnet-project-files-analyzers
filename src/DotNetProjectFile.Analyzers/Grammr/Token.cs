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
    public override Tokens Option => new Optional(this);

    /// <inheritdoc />
    public override ResultQueue Tokenize(TokenStream stream, ResultQueue queue)
    {
        var len = Match(stream.Remaining);
        if (len > 0)
        {
            var add = stream.Add(len, Kind);
            var span = new SourceSpanToken(add[stream.Count].SourceSpan, Kind);
            var node = new Syntax.Token(span);
            return queue.Match(add, node);
        }
        else
        {
            return queue.NoMatch(stream, $"Expected {Kind}.");
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

    private sealed class Optional(Token token) : Tokens
    {
        public override ResultQueue Tokenize(TokenStream stream, ResultQueue queue)
        {
            var len = token.Match(stream.Remaining);
            if (len > 0)
            {
                var add = stream.Add(len, token.Kind);
                var span = new SourceSpanToken(add[stream.Count].SourceSpan, token.Kind);
                var node = new Syntax.Token(span);
                return queue.Match(add, node);
            }
            else
            {
                return queue.Match(stream, null);
            }
        }
    }
}
