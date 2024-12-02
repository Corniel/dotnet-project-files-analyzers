using Grammr.Text;
using Specs.TestTools;

namespace Grammr.ResultCollection_specs;

public class Adds : Scenarios
{
    [TestCase(true)]
    [TestCase(false)]
    public void anything_to_empty(bool success)
    {
        var initial = Empty;
        var updated = initial.Add(new(Left3, success));
        
        updated.Should().BeEquivalentTo([new { Success = success, Remaining = new { Length = 3 } }]);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void best_as_first(bool success)
    {
        var initial = Empty.Add(new(Left2, true));
        var updated = initial.Add(new(Left1, success));

        updated.Should().BeEquivalentTo(
        [
            new { Success = success, Remaining = new { Length = 1 } },
            new { Success = true, Remaining = new { Length = 2 } },
        ]);
    }

    [Test]
    public void failure_as_first_on_same_remainder()
    {
        var initial = Empty.Add(new(Left1, true));
        var updated = initial.Add(new(Left1, false));

        updated.Should().BeEquivalentTo(
        [
            new { Success = false, Remaining = new { Length = 1 } },
            new { Success = true, Remaining = new { Length = 1 } },
        ]);
    }

    [TestCase(1, 2, 3)]
    [TestCase(2, 1, 3)]
    [TestCase(3, 1, 2)]
    public void failure_as_firstsdf_on_same_remainder(params int[] remainders)
    {
        var initial = Empty
            .Add(new(Source.Span(new string('*', remainders[0])), true))
            .Add(new(Source.Span(new string('*', remainders[1])), true));

        var updated = initial.Add(new(Source.Span(new string('*', remainders[2])), true));

        updated.Should().BeEquivalentTo(
        [
            new { Success = true, Remaining = new { Length = 1 } },
            new { Success = true, Remaining = new { Length = 2 } },
            new { Success = true, Remaining = new { Length = 3 } },
        ]);
    }
}

public class Updates : Scenarios
{
    [Test]
    public void new_failure_with_failure_smaller_remainder()
    {
        var initial = Empty.Add(new(Left2, false, "Existing"));
        var updated = initial.Add(new(Left1, false, "Updated"));

        updated.Should().BeEquivalentTo(
        [
            new { Success = false, Remaining = new { Length = 1 }, Message = "Updated" },
        ]);
    }

    [Test]
    public void new_failure_with_successful_smaller_remainder()
    {
        var initial = Empty.Add(new(Left2, false));
        var updated = initial.Add(new(Left1, true));

        updated.Should().BeEquivalentTo(
        [
            new { Success = true, Remaining = new { Length = 1 } },
        ]);
    }
}

public class Does_not_add : Scenarios
{
    [TestCase(true, "22")]
    [TestCase(true, "333")]
    [TestCase(false, "22")]
    [TestCase(false, "333")]
    public void less_far_failure(bool success, string text)
    {
        var span = Source.Span(text);

        var initial = Empty.Add(new(Left1, success));
        var updated = initial.Add(new(span, false));

        updated.Should().BeEquivalentTo(
        [
            new { Success = success, Remaining = new { Length = 1 } },
        ]);
    }

    [Test]
    public void new_failure_with_equal_remainder()
    {
        var initial = Empty.Add(new(Left1, false, "Existing"));
        var updated = initial.Add(new(Left1, false, "Updated"));

        updated.Should().BeEquivalentTo(
        [
            new { Success = false, Remaining = new { Length = 1 }, Message = "Existing" },
        ]);
    }
}

public class Scenarios
{
    internal static readonly ResultCollection<TestResult> Empty = ResultCollection<TestResult>.Empty;

    internal static readonly SourceSpan Left1 = Source.Span("1");
    internal static readonly SourceSpan Left2 = Source.Span("22");
    internal static readonly SourceSpan Left3 = Source.Span("333");
}

internal readonly record struct TestResult(SourceSpan Remaining, bool Success, string? Message = null) : GrammarResult;
