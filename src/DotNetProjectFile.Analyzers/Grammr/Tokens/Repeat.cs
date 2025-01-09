using Grammr;
using Grammr.Text;

internal sealed class Repeat(Tokens tokens, int minOccurs, int maxOccurs) : Tokens
{
    private readonly Tokens Tokens = tokens;
    private readonly int MinOccurs = minOccurs;
    private readonly int MaxOccurs = maxOccurs;

    /// <inheritdoc />
    [Pure]
    public override ResultCollection Tokenize(SourceSpan source)
    {
        var final = ResultCollection.Empty;
        var currs = ResultCollection.Empty;

        var occurs = 0;

        if (MinOccurs == 0)
        {
            final = final.Add(Result.Successful(source));
        }

        foreach (var result in Tokens.Tokenize(source))
        {
            if (result.Success)
            {
                currs = currs.Add(result);
            }

            // Add success to final for * and +.
            // Always add failure.
                
            {
                final = final.Add(result);
            }
        }

        while (++occurs <= MaxOccurs && currs.Any())
        {
            var nexts = ResultCollection.Empty;

            foreach (var curr in currs)
            {
                foreach (var result in Tokens.Tokenize(curr.Remaining))
                {
                    if (result.Success)
                    {
                        var merged = curr.Merge(result);
                        nexts = nexts.Add(merged);
                        if (occurs > MinOccurs)
                        {
                            final = final.Add(merged);
                        }
                    }

                    // only add failures when not enough matches.
                    else if (occurs < MinOccurs)
                    {
                        final = final.Add(result);
                    }
                }
            }
            currs = nexts;
        }

        return final;
    }
}
