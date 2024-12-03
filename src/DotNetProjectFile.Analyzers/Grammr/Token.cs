using Grammr.Text;

namespace Grammr;

public abstract class Token : Tokens
{
    protected Token(string? kind) => Kind = kind ?? GetType().Name;

    public string Kind { get; }

    /// <inheritdoc />
    [Pure]
    public sealed override ResultCollection<Lexer.Result> Tokenize(SourceSpan source) => Match(source) switch
    {
        var len when len > 0 => ResultCollection<Lexer.Result>.Empty.Add(Lexer.Result.Successful(source.Skip(len), new SourceSpanToken(source.Take(len), Kind))),
        _ => ResultCollection<Lexer.Result>.Empty.Add(Lexer.Result.NoMatch(source, $"Expected {Kind}.")),
    };

    /// <summary>
    /// Returns the length of the matched token, and zero the source does not
    /// start with a matching token.
    /// </summary>
    /// <param name="source">
    /// The source text to match on.
    /// </param>
    /// <param name="done">
    /// Tokens already processed.
    /// </param>
    [Pure]
    public abstract int Match(SourceSpan source);
}
