namespace DotNetProjectFile.Analyzers.Ini;

[DiagnosticAnalyzer(LanguageNames.CSharp, LanguageNames.VisualBasic)]
public sealed class IniSyntaxAnalyzer() : IniFileAnalyzer(
    Rule.Ini.SyntaxError,
    Rule.Ini.InvalidHeader,
    Rule.Ini.InvalidKeyValuePair)
{
    protected override void Register(IniFileAnalysisContext context)
    {
        foreach (var error in context.File.Syntax.SyntaxTree.Errors)
        {
            var diagnostic = Diagnostic.Create(Rule.Ini.SyntaxError, context.File.Syntax.SyntaxTree.GetLocation(error), error.Message);
            context.ReportDiagnostic(diagnostic);
        }
    }
}
