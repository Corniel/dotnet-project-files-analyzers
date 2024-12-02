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
        var currs = ResultCollection<Tokenizer.Result>.Empty;

        foreach (var result in Sequances[0].Tokenize(source))
        {
            currs = currs.Add(result);
        }

        foreach (var sequance in Sequances[1..])
        {
            var nexts = ResultCollection<Tokenizer.Result>.Empty;

            foreach (var curr in currs)
            {
                if (curr.Success)
                {
                    foreach (var result in sequance.Tokenize(curr.Remaining))
                    {
                        nexts = nexts.Add(curr.Merge(result));
                    }
                }
                else
                {
                    nexts = nexts.Add(curr);
                }
            }
            currs = nexts;
        }

        return currs;
    }
}
