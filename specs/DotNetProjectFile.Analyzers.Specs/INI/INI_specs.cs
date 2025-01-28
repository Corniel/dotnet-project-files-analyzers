#pragma warning disable RS1035

using DotNetProjectFile.Ini;
using Microsoft.CodeAnalysis.Text;

namespace Grammars.INI_specs;

public class Parses
{
    private const string ASSIGN = nameof(ASSIGN);
    private const string HEADER = "TEXT";
    private const string KEY = "TEXT";
    private const string VALUE = "TEXT";
    private const string WS = nameof(WS);
    private const string NL = nameof(NL);
    private const string COMMENT = nameof(COMMENT);

    [TestCase("=")]
    [TestCase(":")]
    public void key_value_pair(string assign)
    {
        var source = SourceText.From($"root{assign}true");
        var file = IniFileSyntax.Parse(source);

        file.Tokens.Should().BeEquivalentTo(
        [
            new { Text = "root", Kind = KEY },
            new { Text = assign, Kind = ASSIGN },
            new { Text = "true", Kind = VALUE },
        ]);
    }

    [Test]
    public void key_value_pairs()
    {
        var source = SourceText.From($"root=true\nvalue=okay");
        var file = IniFileSyntax.Parse(source);

        file.Tokens.Should().BeEquivalentTo(
        [
            new { Text = "root", Kind = KEY },
            new { Text = "=", Kind = ASSIGN },
            new { Text = "true", Kind = VALUE },
            new { Text = "\n", Kind = NL },
            new { Text = "value", Kind = KEY },
            new { Text = "=", Kind = ASSIGN },
            new { Text = "okay", Kind = VALUE },
        ]);
    }

    [Test]
    public void empty_lines()
    {
        var source = SourceText.From($"root=true\n\r\n     \nvalue=okay");
        var file = IniFileSyntax.Parse(source);

        file.Tokens.Should().BeEquivalentTo(
        [
            new { Text = "root", Kind = KEY },
            new { Text = "=", Kind = ASSIGN },
            new { Text = "true", Kind = VALUE },
            new { Text = "\n", Kind = NL },
            new { Text = "\r\n", Kind = NL },
            new { Text = "     ", Kind = WS },
            new { Text = "\n", Kind = NL },
            new { Text = "value", Kind = KEY },
            new { Text = "=", Kind = ASSIGN },
            new { Text = "okay", Kind = VALUE },
        ]);
    }

    [Test]
    public void key_value_pair_with_spaces()
    {
        var source = SourceText.From("\troot  = true\r\n");
        var file = IniFileSyntax.Parse(source);

        file.Tokens.Should().BeEquivalentTo(
        [
            new { Text = "root", Kind = KEY },
            new { Text = "  ", Kind = WS },
            new { Text = "=", Kind = ASSIGN },
            new { Text = " ", Kind = WS },
            new { Text = "true", Kind = VALUE },
            new { Text = "\r\n", Kind = NL },
        ]);
    }

    [Test]
    public void header_with_key_value_pair()
    {
        var source = SourceText.From("[*.cs]\nroot=true");
        var file = IniFileSyntax.Parse(source);

        file.Tokens.Should().BeEquivalentTo(
        [
            new { Text = "[", Kind = "'['" },
            new { Text = "*.cs", Kind = HEADER },
            new { Text = "]", Kind = "']'" },

            new { Text = "\n", Kind = NL },

            new { Text = "root", Kind = KEY },
            new { Text = "=", Kind = ASSIGN },
            new { Text = "true", Kind = VALUE },
        ]);
    }

    [Test]
    public void key_value_pair_followed_by_header()
    {
        var source = SourceText.From("first=1\n[*.cs]\nroot=true");
        var file = IniFileSyntax.Parse(source);

        file.Tokens.Should().BeEquivalentTo(
        [
            new { Text = "first", Kind = KEY },
            new { Text = "=", Kind = ASSIGN },
            new { Text = "1", Kind = VALUE },

            new { Text = "\n", Kind = NL },

            new { Text = "[", Kind = "'['" },
            new { Text = "*.cs", Kind = HEADER },
            new { Text = "]", Kind = "']'" },

            new { Text = "\n", Kind = NL },

            new { Text = "root", Kind = KEY },
            new { Text = "=", Kind = ASSIGN },
            new { Text = "true", Kind = VALUE },
        ]);
    }

    [TestCase("#")]
    [TestCase(";")]
    public void key_value_pair_with_comment(string ch)
    {
        var source = SourceText.From(
            $"dotnet_diagnostic.Proj1000.severity = error {ch} Use the .NET project file analyzers.\r\n");
        var file = IniFileSyntax.Parse(source);

        file.Tokens.Should().BeEquivalentTo(
        [
            new { Text = "dotnet_diagnostic.Proj1000.severity", Kind = KEY },
            new { Text = " ", Kind = WS },
            new { Text = "=", Kind = ASSIGN },
            new { Text = " ", Kind = WS },
            new { Text = "error", Kind = VALUE },
            new { Text = " ", Kind = WS },
            new { Text = $"{ch} Use the .NET project file analyzers.", Kind = COMMENT },
            new { Text = "\r\n", Kind = NL },
        ]);
    }

    [Test]
    public void line_comment()
    {
        var source = SourceText.From(@"
# Use the .NET project file analyzers.
dotnet_diagnostic.Proj1000.severity=error");
        var file = IniFileSyntax.Parse(source);

        file.Tokens.Should().BeEquivalentTo(
        [
            new { Text = Environment.NewLine, Kind = NL },
            new { Text = "# Use the .NET project file analyzers.", Kind = COMMENT },
            new { Text = Environment.NewLine, Kind = NL },
            new { Text = "dotnet_diagnostic.Proj1000.severity", Kind = KEY },
            new { Text = "=", Kind = ASSIGN },
            new { Text = "error", Kind = VALUE },
        ]);
    }
}
