using Antlr4;

namespace DotNetProjectFile.Ini;

public sealed class ValueSyntax(IniParser.ValueContext context, AbstractSyntaxTree tree) : IniSyntax(context, tree)
{
}
