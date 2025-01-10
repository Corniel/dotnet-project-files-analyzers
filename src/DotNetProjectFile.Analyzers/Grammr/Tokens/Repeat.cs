using DotNetProjectFile.Collections;
using Grammr.Text;

namespace Grammr;

internal sealed class Repeat(Tokens tokens, int minOccurs, int maxOccurs) : Tokens
{
    private readonly Tokens Tokens = tokens;
    private readonly int MinOccurs = minOccurs;
    private readonly int MaxOccurs = maxOccurs;

    public override ResultQueue Tokenize(TokenStream stream, ResultQueue queue)
    {
        if (MinOccurs == 0)
        {
            queue.Match(stream, null);
        }

        var temp = new ResultQueue();
        var currs = new ResultQueue().Match(stream, null);
        var nexts = new ResultQueue();
        var nodes = AppendOnlyList<Syntax.TreeNode>.Empty;
        var occurs = 0;

        while (occurs < MaxOccurs && currs.Any())
        {
            foreach (var curr in currs.DequeueAll())
            {
                var temps = Tokens.Tokenize(curr.Stream, temp.Clear());

                if (occurs < MinOccurs)
                {
                    queue.NoMatch(temps.Failure.Stream, temp.Failure.Message);
                }

                foreach (var next in temps.DequeueAll())
                {
                    nodes = nodes.Add(next.Node);
                    var node = Select(nodes);
                    nexts.Match(next.Stream, node);

                    // It is enough.
                    if (occurs >= MinOccurs)
                    {
                        queue.Match(next.Stream, node);
                    }
                }
            }

            (currs, nexts) = (nexts, currs);
            occurs++;
        }
        return queue;
    }
}
