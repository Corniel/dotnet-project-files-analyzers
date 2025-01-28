using Antlr4;

namespace DotNetProjectFile.Ini;

public sealed class HeaderSyntax(
    IniParser.SectionHeaderContext context,
    AbstractSyntaxTree tree) : IniSyntax(context, tree)
{
    public string HeaderText => Tokens[1].Text;
}
