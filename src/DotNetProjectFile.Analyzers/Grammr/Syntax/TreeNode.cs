using Grammr.Text;
using Microsoft.CodeAnalysis.Text;

namespace Grammr.Syntax;

public abstract class TreeNode
{
    /// <summary>Gets the source span of the node.</summary>
    public abstract SourceSpan SourceSpan { get; }

    /// <summary>Gets the text span of the node.</summary>
    public TextSpan TextSpan => SourceSpan.Span;

    /// <summary>Gets the children of the node.</summary>
    public abstract IReadOnlyList<TreeNode> Children { get; }

    /// <summary>Gets the (child) tokens.</summary>
    public virtual IReadOnlyCollection<SourceSpanToken> Tokens => [.. Children.SelectMany(c => c.Tokens)];
}
