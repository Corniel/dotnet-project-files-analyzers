using Grammr.Text;
using Microsoft.CodeAnalysis.Text;

namespace Grammr.Syntax;

public sealed class Node(IReadOnlyList<TreeNode> children) : TreeNode
{
    /// <inheritdoc />
    public override IReadOnlyList<TreeNode> Children { get; } = children;

    /// <summary>Gets the source span of the node.</summary>
    public override SourceSpan SourceSpan
    {
        get
        {
            if (Children.Count == 0) return default;

            var start = Children[0].SourceSpan.Start;
            var end = Children[^1].SourceSpan.End;

            return new(Children[0].SourceSpan.SourceText, new TextSpan(start, end - start));
        }
    }
}
