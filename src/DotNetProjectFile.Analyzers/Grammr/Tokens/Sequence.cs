using Grammr.Text;

namespace Grammr;

internal sealed class Sequence(ImmutableArray<Tokens> sequances) : Tokens
{
    private readonly ImmutableArray<Tokens> Sequances = sequances
        .SelectMany(p => p is Sequence s ? s.Sequances : [p])
        .ToImmutableArray();

    /// <inheritdoc />
    [Pure]
    public override ResultCollection<Tokenizer.Result> Tokenize(SourceSpan source)
    {
        var results = ResultCollection<Tokenizer.Result>.Empty;

        foreach (var result in Sequances[0].Tokenize(source))
        {
            results = results.Add(result);
        }

        foreach (var sequance in Sequances[1..])
        {
            foreach (var src in results.Where(r => r.Success).Select(r => r.Remaining))
            {
                foreach (var result in sequance.Tokenize(src))
                {
                    results = results.Add(result);
                }
            }
        }

        return results;
    }
}
