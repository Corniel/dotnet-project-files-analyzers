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
        var temp = new ResultQueue();

        foreach (var option in Options)
        {
            var results = option.Tokenize(stream, temp.Clear());
            queue.NoMatch(results.Failure.Stream, results.Failure.Message);

            foreach (var result in results.DequeueAll())
            {
                queue.Enqueue(result);
            }
        }
        return queue;
    }
}
