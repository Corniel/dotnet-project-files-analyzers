using Grammr.Text;
using Specs.TestTools;

namespace Grammr.ResultQueue_specs;

public class Adds
{
    [Test]
    public void anything_to_empty()
    {
        var queue = New();
        queue.Match(Successful(7).Stream, null);
        queue.Should().BeEquivalentTo([new { Remaining = new { Length = 3 } }]);
    }

    [TestCase(5, 6, 7)]
    [TestCase(5, 7, 6)]
    [TestCase(6, 5, 7)]
    [TestCase(6, 7, 5)]
    [TestCase(7, 5, 6)]
    [TestCase(7, 6, 5)]
    public void with_priority(int one, int two, int tre)
    {
        var queue = New();
        queue.Match(Successful(one).Stream, null);
        queue.Match(Successful(two).Stream, null);
        queue.Match(Successful(tre).Stream, null);

        queue.Should().BeEquivalentTo(
        [
            new { Remaining = new { Length = 3 } },
            new { Remaining = new { Length = 4 } },
            new { Remaining = new { Length = 5 } },
        ]);
    }

    internal static ResultQueue New() => new();

    internal static Result Successful(int length) => Result.Successful(null, TokenStream.New(Source.Text("0123456789")).Add(length, null));
}

