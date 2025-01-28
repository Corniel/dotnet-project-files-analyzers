using Antlr4.Runtime;
using Microsoft.CodeAnalysis.Text;

namespace Antlr4;

[DebuggerDisplay("Name = {Name}, Text = {Text}")]
public class AntlrSyntax(ParserRuleContext context, AbstractSyntaxTree tree)
{
    /// <summary>The underlying abstract syntax tree.</summary>
    public AbstractSyntaxTree SyntaxTree { get; } = tree;

    protected ParserRuleContext Context { get; } = context;

    /// <inheritdoc cref="RuleContext.GetText()"/>>
    public string Text => Context.GetText();

    /// <summary>Gets the text span of the syntax.</summary>
    public TextSpan TextSpan => new TextSpan(Tokens[0].TextSpan.Start, Tokens[^1].TextSpan.End - Tokens[0].TextSpan.Start);

    /// <summary>Gets the text span of the syntax.</summary>
    public LinePositionSpan LineSpan => SyntaxTree.LineSpan(TextSpan);

    /// <summary>Gets the stream tokens.</summary>
    public IReadOnlyList<StreamToken> Tokens => SyntaxTree.Tokens(Context.SourceInterval);

    private string Name => GetType().Name;

    // TODO: location via SyntaxTree
    [Pure]
    public Location GetLocation() => SyntaxTree.GetLocation(this);
}
