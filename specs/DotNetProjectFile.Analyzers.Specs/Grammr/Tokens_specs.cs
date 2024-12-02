using Specs.TestTools;

namespace Grammr.Tokens_specs;

public class Tokenizes : Grammar
{
    private static readonly Token A = ch('A');
    private static readonly Token B = ch('B');
    private static readonly Token C = ch('C');

    public static readonly Tokens ABC = A & B & C;
    public static readonly Tokens Letter = A | B | C;
    public static readonly Tokens As = A.Star;
    public static readonly Tokens Bs = B.Repeat(2);

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

    public class Repeates
    {
        [Test]
        public void multiple()
        {
            var results = As.Tokenize(Source.Span("AAABCDE"));

            object[] expected = [
                new
                {
                    Tokens = new[]{ new { Text = "A", Kind = "A" }, new { Text = "A", Kind = "A" }, new { Text = "A", Kind = "A" } },
                    Success = true,
                    Remaining = new { Text = "BCDE" },
                },
                new
                {
                    Tokens = new[]{ new { Text = "A", Kind = "A" }, new { Text = "A", Kind = "A" } },
                    Success = true,
                    Remaining = new { Text = "ABCDE" },
                },
                new
                {
                    Tokens = new[]{ new { Text = "A", Kind = "A" } },
                    Success = true,
                    Remaining = new { Text = "AABCDE" },
                },
                new
                {
                    Tokens = Array.Empty<object>(),
                    Success = true,
                    Remaining = new { Text = "AAABCDE" },
                }];

            results.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void not_enough()
        {
            var results = Bs.Tokenize(Source.Span("BCDE"));

            results.Should().BeEquivalentTo([
                new
                {
                    Tokens = Array.Empty<object>(),
                    Success = false,
                    Remaining = new { Text = "CDE" },
                    Message = "Expected B."
                }]);
        }
    }
}
