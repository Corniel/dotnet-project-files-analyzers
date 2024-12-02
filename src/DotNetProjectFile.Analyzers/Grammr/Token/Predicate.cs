using Grammr.Text;

namespace Grammr;

[DebuggerDisplay("Condition = {Condition}, Kind = {Kind}")]
internal sealed class Predicate(Predicate<char> condition, string? kind) : Token(kind)
{
    private readonly Predicate<char> Condition = condition;

    /// <inheritdoc />
    [Pure]
    public override int Match(SourceSpan source)
    {
        var length = 0;

        while (length < source.Length && Condition(source[length]))
        {
            length++;
        }
        return length;
    }
}
