using Antlr4.Runtime;
using Microsoft.CodeAnalysis.Text;

namespace DotNetProjectFile.Ini;

public sealed class IniFile : ProjectFile
{
	public required IniFileSyntax Syntax { get; init; }

    /// <inheritdoc />
    public required IOFile Path { get; init; }

    /// <inheritdoc />
    public required SourceText Text { get; init; }

    /// <inheritdoc />
    public WarningPragmas WarningPragmas => WarningPragmas.None;

    public static IniFile Load(IOFile path)
	{
        using var stream = path.OpenRead();
        var source = SourceText.From(stream);

		var file = new IniFile()
		{
            Path = path,
			Syntax = IniFileSyntax.Parse(source),
            Text = source,
		};

        file.Syntax.SyntaxTree.SetPath(path);

        return file;
	}
}
