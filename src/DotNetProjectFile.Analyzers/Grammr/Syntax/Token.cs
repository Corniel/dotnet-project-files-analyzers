using Grammr.Text;

namespace Grammr.Syntax;

public sealed class Token(SourceSpanToken token) : TreeNode
{
    /// <inheritdoc />
    public override SourceSpan SourceSpan { get; } = token.SourceSpan;

    /// <inheritdoc />
    public override IReadOnlyList<TreeNode> Children => [];

    /// <inheritdoc />
    public override IReadOnlyCollection<SourceSpanToken> Tokens { get; } = [token];
}
