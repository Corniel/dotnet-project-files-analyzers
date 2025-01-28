using DotNetProjectFile.Parsing;

namespace DotNetProjectFile.Syntax;

internal static class SyntaxNodeResolver
{
    [Pure]
    public static TSyntax Resolve<TSyntax>(this Parser parser, SyntaxTree tree) where TSyntax : SyntaxNode
    {
        var syntax = parser.Syntax;
        syntax.SetParent(new RootSyntax(tree.With(parser.Tokens)));
        return syntax as TSyntax
            ?? throw new InvalidOperationException($"Unexpected syntax of type {syntax.GetType().FullName}.");
    }
}
