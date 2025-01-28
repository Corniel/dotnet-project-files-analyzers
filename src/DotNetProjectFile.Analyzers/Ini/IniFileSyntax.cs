using Antlr4;
using Antlr4.Runtime;
using Microsoft.CodeAnalysis.Text;

namespace DotNetProjectFile.Ini;

public sealed class IniFileSyntax(
	IReadOnlyList<SectionSyntax> sections,
	IniParser.FileContext context,
    AbstractSyntaxTree tree) : IniSyntax(context, tree)
{
	public IReadOnlyList<SectionSyntax> Sections { get; } = sections;

    public static IniFileSyntax Parse(SourceText source)
    {
        var stream = new AntlrInputStream(source.ToString());
        var lexer = new IniLexer(stream);
        var tokens = new CommonTokenStream(lexer);
        var parser = new IniParser(tokens);
        var listner = new RoslynErrorListner();

        lexer.RemoveErrorListeners();
        parser.RemoveErrorListeners();

        parser.AddErrorListener(listner);

        var visitor = new IniFileVisitor(new(source, tokens, parser.Vocabulary, listner.Errors));

        return (IniFileSyntax)visitor.Visit(parser.file());
    }
}
