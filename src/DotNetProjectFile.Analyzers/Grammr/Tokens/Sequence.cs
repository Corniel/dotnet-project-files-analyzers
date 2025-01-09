using Grammr.Text;

namespace Grammr;

internal sealed class Sequence(ImmutableArray<Tokens> sequances) : Tokens
{
    private readonly ImmutableArray<Tokens> Sequances = sequances
        .SelectMany(p => p is Sequence s ? s.Sequances : [p])
        .ToImmutableArray();

    /// <inheritdoc />
    [Pure]
    public override ResultCollection Tokenize(SourceSpan source)
    {
        var final = ResultCollection.Empty;
        var currs = ResultCollection.Empty;
        var nodes = ImmutableArray<Syntax.TreeNode>.Empty;

        foreach (var result in Sequances[0].Tokenize(source))
        {
            if (result.Success)
            {
                if (result.Node is { } node)
                {
                    nodes = nodes.Add(node);
                }
                currs = currs.Add(result);
            }
            else
            {
                final = final.Add(result);
            }
        }

        foreach (var sequance in Sequances[1..])
        {
            var nexts = ResultCollection.Empty;

            foreach (var curr in currs)
            {
                foreach (var result in sequance.Tokenize(curr.Remaining))
                {
                    if (result.Success)
                    {
                        if (result.Node is { } node)
                        {
                            nodes = nodes.Add(node);
                        }
                        var merged = Result.Successful(nodes.Length == 1 ? nodes[0] : new Syntax.Node(nodes), result.Remaining);
                        nexts = nexts.Add(merged);
                    }
                    else
                    {
                        final = final.Add(result);
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
