using Grammr.Text;

namespace Grammr;

internal sealed class Switch(ImmutableArray<Tokens> options) : Tokens
{
    private readonly ImmutableArray<Tokens> Options = options
        .SelectMany(p => p is Switch s ? s.Options : [p])
        .ToImmutableArray();

    /// <inheritdoc />
    [Pure]
    public override ResultCollection<Tokenizer.Result> Tokenize(SourceSpan source)
    {
        var results = ResultCollection<Tokenizer.Result>.Empty;

        foreach (var option in Options)
        {
            foreach (var result in option.Tokenize(source))
            {
                results = results.Add(result);
            }
        }
        return results;
    }
}
