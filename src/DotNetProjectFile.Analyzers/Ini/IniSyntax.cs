using Antlr4;
using Antlr4.Runtime;

namespace DotNetProjectFile.Ini;

public class IniSyntax(ParserRuleContext context, AbstractSyntaxTree tree) : AntlrSyntax(context, tree)
{
}
