using DotNetProjectFile.Parsing;
using DotNetProjectFile.Syntax;

namespace DotNetProjectFile.Ini;

[DebuggerDisplay("{DebuggerDisplay}")]
public sealed record IniFileSyntax : IniSyntax
{
    public SyntaxNodeCollection<SectionSyntax> Sections => new(this);

    public override IEnumerable<Diagnostic> GetDiagnostics() =>
    [
        .. Tokens
            .Where(t => t.Kind == TokenKind.UnparsableToken)
            .Select(t => Diagnostic.Create(Rule.Ini.Invalid, SyntaxTree.GetLocation(t.LinePositionSpan))),

        .. Children.SelectMany(c => c.GetDiagnostics()),
    ];

    internal static IniFileSyntax New(Parser parser)
        => Root(parser, init: false) with
        {
            Span = new(0, parser.Tokens.Count),
        };

    internal static IniFileSyntax Root(Parser parser, bool init = true)
        => parser.Syntax as IniFileSyntax
        ?? (init
            ? new() { Children = [new SectionSyntax()] }
            : new());

    public static IniFileSyntax Parse(Syntax.SyntaxTree tree)
    {
        try
        {
            return IniGrammarBak.file
                .Parse(tree.SourceText)
                .Resolve<IniFileSyntax>(tree);
        }
        catch (Unparsable)
        {
            return Unparsable.Syntax(new IniFileSyntax(), tree);
        }
    }
}
