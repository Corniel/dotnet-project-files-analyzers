using Grammr.Text;

namespace Grammr;

internal sealed class Switch(ImmutableArray<Tokens> options) : Tokens
{
    private readonly ImmutableArray<Tokens> Options = options
        .SelectMany(p => p is Switch s ? s.Options : [p])
        .ToImmutableArray();

    /// <inheritdoc />
    public override ResultQueue Tokenize(TokenStream stream, ResultQueue queue)
    {
        foreach (var option in Options)
        {
            foreach (var result in option.Tokenize(stream))
            {
                queue.Enqueue(result);
            }
        }
        return queue;
    }

    /// <inheritdoc />
    [Pure]
    public override ResultCollection Tokenize(TokenStream stream)
    {
        var results = ResultCollection.Empty;

        foreach (var option in Options)
        {
            foreach (var result in option.Tokenize(stream))
            {
                results = results.Add(result);
            }
        }
        return results;
    }
}
