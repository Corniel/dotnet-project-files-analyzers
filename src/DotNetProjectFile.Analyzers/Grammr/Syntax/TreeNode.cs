using Grammr.Text;

namespace Grammr.Syntax;

public abstract class TreeNode
{
    /// <summary>Gets the source span of the node.</summary>
    public abstract SourceSpan SourceSpan { get; }

    /// <summary>Gets the children of the node.</summary>
    public abstract IReadOnlyList<TreeNode> Children { get; }

    /// <summary>Gets the (child) tokens.</summary>
    public virtual IReadOnlyCollection<SourceSpanToken> Tokens => [.. Children.SelectMany(c => c.Tokens)];
}
