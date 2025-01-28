using DotNetProjectFile.Ini;

namespace DotNetProjectFile.Analyzers.EditorConfig;

[DiagnosticAnalyzer(LanguageNames.CSharp, LanguageNames.VisualBasic)]
public sealed class HeaderMustBeGlob() : IniFileAnalyzer(Rule.Ini.HeaderMustBeGlob)
{
    protected override void Register(IniFileAnalysisContext context)
    {
        if (!context.File.Path.Name.IsMatch(".editorconfig")) return;

        foreach (var header in context.File.Syntax.Sections
            .Select(s => s.Header)
            .OfType<HeaderSyntax>()
            .Where(h => h.HeaderText is { Length: > 0 } && Glob.TryParse(h.HeaderText) is null))
        {
            var span = header.Tokens[1];
            context.ReportDiagnostic(Descriptor, span.GetLocation(), header.HeaderText);
        }
    }
}
