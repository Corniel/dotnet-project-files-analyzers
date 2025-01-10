using Grammr.Text;
using Specs.TestTools;

namespace Grammr.Token_specs;

public class Tokenizes : Grammar
{
    private static readonly Token EndOfLine = eol();
    private static readonly Token C = ch('C');
    private static readonly Token ABC = str("ABC");
    private static readonly Token WhiteSpace = match(char.IsWhiteSpace);
    private static readonly Token Digits = regex("[0-9]+");

    [Test]
    public void chars()
    {
        var results = C.Tokenize(TokenStream.New(Source.Text("CDE")));

        results.Should().BeEquivalentTo([
            new
            {
                Stream = new []{ new { Text = "C", Kind = "C" } },
                Success = true,
                Remaining = new { Text = "DE" },
            }]);
    }

    [Test]
    public void strings()
    {
        var results = ABC.Tokenize(TokenStream.New(Source.Text("ABCDEF")));

        results.Should().BeEquivalentTo([
            new
            {
                Stream = new []{ new { Text = "ABC", Kind = "ABC" } },
                Success = true,
                Remaining = new { Text = "DEF" },
            }]);
    }

    [Test]
    public void predications()
    {
        var results = WhiteSpace.Tokenize(TokenStream.New(Source.Text(" \t\r\nABC")));

        results.Should().BeEquivalentTo([
            new
            {
                Stream = new []{ new { Text = " \t\r\n", Kind = "WhiteSpace" } },
                Success = true,
                Remaining = new { Text = "ABC" },
            }]);
    }

    [Test]
    public void regexes()
    {
        var results = Digits.Tokenize(TokenStream.New(Source.Text("42d")));

        results.Should().BeEquivalentTo([
            new
            {
                Stream = new []{ new { Text = "42", Kind = "Digits" } },
                Success = true,
                Remaining = new { Text = "d" },
            }]);
    }

    [TestCase("\n")]
    [TestCase("\r\n")]
    public void end_of_lines(string start)
    {
        var results = EndOfLine.Tokenize(TokenStream.New(Source.Text($"{start}Hello")));

        results.Should().BeEquivalentTo([
            new
            {
                Stream = new []{ new { Text = start, Kind = "EndOfLine" } },
                Success = true,
                Remaining = new { Text = "Hello" },
            }]);
    }
}

public class Does_not_match : Grammar
{
    private static readonly Token C = ch('C');
    private static readonly Token ABC = str("ABC");
    private static readonly Token WhiteSpace = match(char.IsWhiteSpace);
    private static readonly Token Digits = regex("[0-9]+");

    [Test]
    public void chars()
    {
        var results = C.Tokenize(TokenStream.New(Source.Text("ABCDE")));

        results.Outcome.Should().BeEquivalentTo(
            new
            {
                Stream = Array.Empty<object>(),
                Success = false,
                Remaining = new { Text = "ABCDE" },
                Message = "Expected C.",
            });
    }

    [Test]
    public void strings()
    {
        var results = ABC.Tokenize(TokenStream.New(Source.Text("BCDEF")));

        results.Outcome.Should().BeEquivalentTo(
            new
            {
                Stream = Array.Empty<object>(),
                Success = false,
                Remaining = new { Text = "BCDEF" },
                Message = "Expected ABC.",
            });
    }

    [Test]
    public void predications()
    {
        var results = WhiteSpace.Tokenize(TokenStream.New(Source.Text("ABC\nCDE")));

        results.Outcome.Should().BeEquivalentTo(
            new
            {
                Stream = Array.Empty<object>(),
                Success = false,
                Remaining = new { Text = "ABC\nCDE" },
                Message = "Expected WhiteSpace.",
            });
    }

    [Test]
    public void regexes()
    {
        var results = Digits.Tokenize(TokenStream.New(Source.Text("Test42d")));

        results.Outcome.Should().BeEquivalentTo(
            new
            {

                Stream = Array.Empty<object>(),
                Success = false,
                Remaining = new { Text = "Test42d" },
                Message = "Expected Digits.",
            });
    }
}
