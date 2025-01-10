using DotNetProjectFile.Collections;
using Grammr.Text;

namespace Grammr;

internal sealed class Sequence(ImmutableArray<Tokens> sequances) : Tokens
{
    private readonly ImmutableArray<Tokens> Sequances = sequances
        .SelectMany(p => p is Sequence s ? s.Sequances : [p])
        .ToImmutableArray();

    /// <inheritdoc />
    public override ResultQueue Tokenize(TokenStream stream, ResultQueue queue)
    {
        var temp = new ResultQueue();
        var currs = new ResultQueue();
        var nexts = new ResultQueue().Match(stream, null);

        var nodes = AppendOnlyList<Syntax.TreeNode>.Empty;

        foreach (var sequance in Sequances)
        {
            (currs, nexts) = (nexts, currs);

            if (sequance == Sequances[^1])
            {
                nexts = queue;
            }

            foreach (var curr in currs.DequeueAll())
            {
                var temps = sequance.Tokenize(curr.Stream, temp.Clear());
                queue.NoMatch(temps.Failure.Stream, temp.Failure.Message);

                foreach (var next in temps.DequeueAll())
                {
                    nodes = nodes.Add(next.Node);
                    var node = Select(nodes);
                    nexts.Match(next.Stream, node);
                }
            }
        }
        return queue;
    }


    /// <inheritdoc />
    [Pure]
    public override ResultCollection Tokenize(TokenStream stream)
    {
        var final = ResultCollection.Empty;
        var currs = ResultCollection.Empty;
        var nodes = AppendOnlyList<Grammr.Syntax.TreeNode>.Empty;

        foreach (var result in Sequances[0].Tokenize(stream))
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
                foreach (var result in sequance.Tokenize(curr.Stream))
                {
                    if (result.Success)
                    {
                        if (result.Node is { } node)
                        {
                            nodes = nodes.Add(node);
                        }
                        var merged = Result.Successful(nodes.Count == 1 ? nodes[0] : new Syntax.Node(nodes), result.Stream);
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
