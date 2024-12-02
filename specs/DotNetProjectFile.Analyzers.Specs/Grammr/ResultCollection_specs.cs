using Grammr.Text;
using Specs.TestTools;

namespace Grammr.ResultCollection_specs;

internal class Adds
{
    public static readonly SourceSpan Left1 = Source.Span("1");
    public static readonly SourceSpan Left2 = Source.Span("22");
    public static readonly SourceSpan Left3 = Source.Span("333");

    [Test]
    public void Closer_to_end_first()
    {
        var initial = ResultCollection<TestResult>.Empty.Add(new(Left2, true));
        var updated = initial.Add(new(Left1, true));

        updated.Should().BeEquivalentTo(
        [
            new { Success = true, Remaining = new { Length = 1 } },
            new { Success = true, Remaining = new { Length = 2 } },
        ]);
    }

    [Test]
    public void Further_from_end_last()
    {
        var initial = ResultCollection<TestResult>.Empty.Add(new(Left1, true));
        var updated = initial.Add(new(Left2, true));

        updated.Should().BeEquivalentTo(
        [
            new { Success = true, Remaining = new { Length = 1 } },
            new { Success = true, Remaining = new { Length = 2 } },
        ]);
    }

    [Test]
    public void Ensures_right_order()
    {
        var initial = ResultCollection<TestResult>.Empty
            .Add(new(Left1, true))
            .Add(new(Left3, true));
        var updated = initial.Add(new(Left2, true));

        updated.Should().BeEquivalentTo(
        [
            new { Success = true, Remaining = new { Length = 1 } },
            new { Success = true, Remaining = new { Length = 2 } },
            new { Success = true, Remaining = new { Length = 3 } },
        ]);
    }

    [Test]
    public void Unsuccesful_after_successfull()
    {
        var initial = ResultCollection<TestResult>.Empty.Add(new(Left2, true));
        var updated = initial.Add(new(Left1, false));

        updated.Should().BeEquivalentTo(
        [
            new { Success = true, Remaining = new { Length = 2 } },
            new { Success = false, Remaining = new { Length = 1 } },
        ]);
    }

    [Test]
    public void Updates_unsuccessful_with_beter()
    {
        var initial = ResultCollection<TestResult>.Empty
            .Add(new(Left2, true))
            .Add(new(Left2, false));

        var updated = initial.Add(new(Left1, false));

        updated.Should().BeEquivalentTo(
        [
            new { Success = true, Remaining = new { Length = 2 } },
            new { Success = false, Remaining = new { Length = 1 } },
        ]);
    }

    [Test]
    public void Keeps_beter_unsuccessful()
    {
        var initial = ResultCollection<TestResult>.Empty
            .Add(new(Left2, true))
            .Add(new(Left1, false));

        var updated = initial.Add(new(Left2, false));

        updated.Should().BeEquivalentTo(
        [
            new { Success = true, Remaining = new { Length = 2 } },
            new { Success = false, Remaining = new { Length = 1 } },
        ]);
    }
}

internal readonly record struct TestResult(SourceSpan Remaining, bool Success) : GrammarResult;
