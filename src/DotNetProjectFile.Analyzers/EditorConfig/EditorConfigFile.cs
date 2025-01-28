using DotNetProjectFile.Ini;
using Microsoft.CodeAnalysis.Text;

namespace DotNetProjectFile.EditorConfig;

public sealed class EditorConfigFile(IniFileSyntax syntax) : ProjectFile
{
    /// <summary>INI syntax.</summary>
    public IniFileSyntax Syntax { get; } = syntax;

    /// <inheritdoc />
    public IOFile Path => default; // Syntax.SyntaxTree.Path;

    /// <inheritdoc />
    public SourceText Text => null;//  Syntax.SyntaxTree.SourceText;

    /// <inheritdoc />
    public WarningPragmas WarningPragmas => WarningPragmas.None;

    public bool IsRoot
        => Syntax.Sections.FirstOrDefault() is { } section
        && section.Header is null
        //&& section.Kvps
        //    .Where(kvp => kvp.Key.IsMatch("root"))
        //    .Select(kvp => kvp.Value.IsMatch("true"))
        //    .LastOrDefault()
        ;
}
