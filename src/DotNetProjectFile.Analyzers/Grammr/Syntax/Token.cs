using Grammr.Text;

namespace Grammr.Syntax;

public sealed class Token : Node
{
    public Token(SourceSpanToken token)
    {
        SourceSpan = token.SourceSpan;
        Tokens = [token];
    }

    /// <inheritdoc />
    public override SourceSpan SourceSpan { get; }

    /// <inheritdoc />
    public override IReadOnlyCollection<SourceSpanToken> Tokens { get; }

    /// <inheritdoc />
    public override int Count => 1;
}
