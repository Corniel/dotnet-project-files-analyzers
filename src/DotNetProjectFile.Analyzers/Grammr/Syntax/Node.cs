using Grammr.Text;
using Microsoft.CodeAnalysis.Text;

namespace Grammr.Syntax;

public class Node
{
    /// <summary>Gets the children of the node.</summary>
    public ImmutableArray<Token> Children { get; init; } = [];

    /// <summary>Gets the source span of the node.</summary>
    public virtual SourceSpan SourceSpan
    {
        get
        {
            if (Children.Length == 0) return default;

            var start = Children[0].SourceSpan.Start;
            var end = Children[^1].SourceSpan.End;

            return new(Children[0].SourceSpan.SourceText, new TextSpan(start, end - start));
        }
    }

    /// <summary>Gets the token count.</summary>
    public virtual int Count => Tokens.Count;

    /// <summary>Gets the (child) tokens.</summary>
    public virtual IReadOnlyCollection<SourceSpanToken> Tokens => [.. Children.SelectMany(c => c.Tokens)];
}
