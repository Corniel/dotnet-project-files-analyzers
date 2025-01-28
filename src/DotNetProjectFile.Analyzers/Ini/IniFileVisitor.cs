using Antlr4;
using System.Runtime.CompilerServices;

namespace DotNetProjectFile.Ini;

public sealed class IniFileVisitor(AbstractSyntaxTree tree) : IniBaseVisitor<IniSyntax>
{
    private readonly AbstractSyntaxTree Tree = tree;

    public override IniSyntax VisitFile([NotNull] IniParser.FileContext context)
    {
        HeaderSyntax? header = null;
        var sections = new List<SectionSyntax>();
        var pairs = new List<KeyValuePairSyntax>();

        foreach (var child in context.children.Select(Visit))
        {
            if (child is KeyValuePairSyntax pair)
            {
                pairs.Add(pair);
            }
            else if (child is HeaderSyntax next)
            {
                if (pairs.Any() || header is { })
                {
                    sections.Add(new(header, pairs.ToArray()));
                    pairs.Clear();
                }
                header = next;
            }
        }
        if (pairs.Any())
        {
            sections.Add(new(header, pairs.ToArray()));
        }

        return new IniFileSyntax(sections, context, Tree);
    }

    public override IniSyntax VisitKeyValuePair([NotNull] IniParser.KeyValuePairContext context)
    {
        var key = new KeySyntax(context.key(), Tree);
        var value = new ValueSyntax(context.value(), Tree);

        return new KeyValuePairSyntax(key, value, context, Tree);
    }
    
    public override IniSyntax VisitSectionHeader([NotNull] IniParser.SectionHeaderContext context)
        => new HeaderSyntax(context, Tree);
}

