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
}
