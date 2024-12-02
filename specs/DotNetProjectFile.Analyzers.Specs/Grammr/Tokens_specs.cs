using Specs.TestTools;

namespace Grammr.Tokens_specs;

public class Tokenizes : Grammar
{
    private static readonly Token A = ch('A');
    private static readonly Token B = ch('B');
    private static readonly Token C = ch('C');

    public static readonly Tokens ABC = A & B & C;
    public static readonly Tokens Letter = A | B | C;

    [Test]
    public void sequences()
    {
        var results = ABC.Tokenize(Source.Span("ABCDE"));

        results.Should().BeEquivalentTo([
            new
            {
                Tokens = new []{ new { Text = "A", Kind = "A" }, new { Text = "B", Kind = "B" }, new { Text = "C", Kind = "C" } },
                Success = true,
                Remaining = new { Text = "DE" },
            }]);
    }

    [Test]
    public void switches()
    {
        var results = Letter.Tokenize(Source.Span("ABCDE"));

        results.Should().BeEquivalentTo([
        new
        {
            Tokens = new []{ new { Text = "A", Kind = "A" } },
            Success = true,
            Remaining = new { Text = "BCDE" },
        }]);
    }
}
