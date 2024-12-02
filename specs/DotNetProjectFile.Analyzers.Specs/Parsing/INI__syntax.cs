
using DotNetProjectFile.Ini;
using Specs.TestTools;
using System.IO;

namespace Parsing.INI__syntax;

public class Tokenizes
{
    [Test]
    public void header_with_space()
    {
        var span = Source.Span("[My Header] \r\n");

        var tokens = INI_grammar.file.Tokenize(span);

        ((object)tokens[0]).Should().BeEquivalentTo(new
        {
            Success = true,
            Remaining = new { Length = 0 },
            Tokens = new { Length = 5 },
        });
    }

    [Test]
    public void dot_editorconfig()
    {
        using var file = new FileInfo("../../../../../.editorconfig").OpenText();
        var span = Source.Span(file.ReadToEnd());

        var tokens = INI_grammar.file.Tokenize(span);
    }

    [Test]
    public void with_garbage()
    {
        var span = Source.Span(@"global = false
some_key = value
invalid line
indenting = \t
[]"
            );

            var tokens = INI_grammar.file.Tokenize(span);
    }
}
