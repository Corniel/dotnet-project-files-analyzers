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
        var final = ResultCollection<Tokenizer.Result>.Empty;
        var currs = ResultCollection<Tokenizer.Result>.Empty;

        foreach (var result in Sequances[0].Tokenize(source))
        {
            if (result.Success)
            {
                currs = currs.Add(result);
            }
            else
            {
                final = final.Add(result);
            }
        }

        foreach (var sequance in Sequances[1..])
        {
            var nexts = ResultCollection<Tokenizer.Result>.Empty;

            foreach (var curr in currs)
            {
                foreach (var result in sequance.Tokenize(curr.Remaining))
                {
                    var merged = curr.Merge(result);
                    if (merged.Success)
                    {
                        nexts = nexts.Add(merged);
                    }
                    else
                    {
                        final = final.Add(merged);
                    }
                }
            }
            currs = nexts;
        }

        foreach (var curr in currs)
        {
            final = final.Add(curr);
        }

        return final;
    }
}
