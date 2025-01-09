using Grammr.Text;

namespace Grammr.Syntax;

[DebuggerDisplay("{Kind}: {SourceSpan.Text}")]
public sealed class Token(SourceSpanToken token) : TreeNode
{
    public string? Kind { get; } = token.Kind;

    /// <inheritdoc />
    public override SourceSpan SourceSpan { get; } = token.SourceSpan;

    /// <inheritdoc />
    public override IReadOnlyList<TreeNode> Children => [];

    /// <inheritdoc />
    public override IReadOnlyCollection<SourceSpanToken> Tokens { get; } = [token];
}
