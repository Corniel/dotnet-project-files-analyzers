using Antlr4;

namespace DotNetProjectFile.Ini;

public sealed class KeySyntax(IniParser.KeyContext context, AbstractSyntaxTree tree) : IniSyntax(context, tree)
{
}
