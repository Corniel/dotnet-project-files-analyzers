using Antlr4.Runtime;
using Microsoft.CodeAnalysis.Text;

namespace Antlr4;

[DebuggerDisplay("{Text}, Kind = {Kind}")]
public readonly struct StreamToken(IToken token, LinePositionSpan lineSpan, string kind, AbstractSyntaxTree tree)
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly AbstractSyntaxTree SyntaxTree = tree;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly IToken Token = token;

    /// <inheritdoc cref="IToken.Text" />
    public string Text => Token.Text;

    public string Kind { get; } = kind;

    /// <inheritdoc cref="IToken.Type" />
    public int Type => Token.Type;

    /// <summary>Gets the text span of the token.</summary>
    public TextSpan TextSpan => new(Token.StartIndex, Token.StopIndex - Token.StartIndex);

    /// <summary>Gets the text span of the token.</summary>
    public LinePositionSpan LineSpan { get; } = lineSpan;

    /// <summary>Gets the location of the token.</summary>
    [Pure]
    public Location GetLocation() => SyntaxTree.GetLocation(this);
}
