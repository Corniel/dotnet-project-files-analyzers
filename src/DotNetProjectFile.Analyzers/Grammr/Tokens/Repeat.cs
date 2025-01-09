using DotNetProjectFile.Collections;
using Grammr;
using Grammr.Text;

internal sealed class Repeat(Tokens tokens, int minOccurs, int maxOccurs) : Tokens
{
    private readonly Tokens Tokens = tokens;
    private readonly int MinOccurs = minOccurs;
    private readonly int MaxOccurs = maxOccurs;

    /// <inheritdoc />
    [Pure]
    public override ResultCollection Tokenize(TokenStream stream)
    {
        var final = ResultCollection.Empty;
        var currs = ResultCollection.Empty;
        var nodes = AppendOnlyList<Grammr.Syntax.TreeNode>.Empty;
        var update = stream;

        var occurs = 0;

        if (MinOccurs == 0)
        {
            final = final.Add(Result.Successful(null, update));
        }

        foreach (var result in Tokens.Tokenize(update))
        {
            if (result.Success)
            {
                nodes = nodes.Add(result.Node);
                currs = currs.Add(result);
            }

            // Add success to final for * and +.
            // Always add failure.
            final = final.Add(result);
        }

        while (++occurs <= MaxOccurs && currs.Any())
        {
            var nexts = ResultCollection.Empty;

            foreach (var curr in currs)
            {
                foreach (var result in Tokens.Tokenize(curr.Stream))
                {
                    if (result.Success)
                    {
                        if (result.Node is { } node)
                        {
                            nodes = nodes.Add(node);
                        }

                        var merged = Result.Successful(nodes.Count == 1 ? nodes[0] : new Grammr.Syntax.Node(nodes), result.Stream);
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
