using DotNetProjectFile.Ini;
using Specs.TestTools;
using System.IO;

namespace Parsing.INI_syntax;

[Explicit]
public class Tokenizes
{
    [Test]
    public void header()
    {
        var span = Source.Span("[My Header] \r\n");

        var tokens = IniGrammar.file.Tokenize(span);

        ((object)tokens[0]).Should().BeEquivalentTo(Result(true, 0, 5));
    }

    [Test]
    public void header_with_comment()
    {
        var span = Source.Span("[My Header] # comment here.\r\n");

        var tokens = IniGrammar.file.Tokenize(span);

        ((object)tokens[0]).Should().BeEquivalentTo(Result(true, 0, 6));
    }

    [Test]
    public void comment_only()
    {
        var span = Source.Span("# comment here.\t");

        var tokens = IniGrammar.file.Tokenize(span);

        ((object)tokens[0]).Should().BeEquivalentTo(Result(true, 0, 2));
    }

    [Test]
    public void key_value_pair_with_colon()
    {
        var span = Source.Span("    some.key:\t space");

        var tokens = IniGrammar.file.Tokenize(span);

        ((object)tokens[0]).Should().BeEquivalentTo(Result(true, 0, 6));
    }

    [Test]
    public void key_value_pair_with_equal()
    {
        var span = Source.Span("    some.key =\t space\n");

        var tokens = IniGrammar.file.Tokenize(span);

        ((object)tokens[0]).Should().BeEquivalentTo(Result(true, 0, 7));
    }

    [Test]
    public void value_with_special_chars()
    {
        var span = Source.Span("vsspell_ignored_words_main = File:dictionary.dic");

        var tokens = IniGrammar.file.Tokenize(span);

        ((object)tokens[0]).Should().BeEquivalentTo(Result(true, 0, 6));
    }

    [Test]
    public void key_value_pair_with_comment()
    {
        var span = Source.Span(@"dotnet_diagnostic.IDE1006.severity = none # IDE1006: Naming Styles");

        var tokens = IniGrammar.file.Tokenize(span);

        ((object)tokens[0]).Should().BeEquivalentTo(Result(true, 0, 8));
    }

    [Test]
    public void white_space_only()
    {
        var span = Source.Span("    \r\n\t\t\n");

        var tokens = IniGrammar.file.Tokenize(span);

        ((object)tokens[0]).Should().BeEquivalentTo(Result(true, 0, 4));
    }

    [Test]
    public void dot_editorconfig()
    {
        using var file = new FileInfo("../../../../../.editorconfig").OpenText();
        var span = Source.Span(file.ReadToEnd());

        var tokens = IniGrammar.file.Tokenize(span);

        var array = tokens.ToArray();
    }

    [Test]
    public void with_garbage()
    {
        var span = Source.Span(@"global = false
some_key = value
invalid line
indenting = \t
[]");

            var tokens = IniGrammar.file.Tokenize(span);
    }

    private static object Result(bool success, int remaining, int tokens) => new
    {
        Success = success,
        Remaining = new { Length = remaining },
        Tokens = new { Length = tokens },
    };
}
