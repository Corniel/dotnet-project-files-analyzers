using Grammr.Text;

namespace Grammr;

[DebuggerDisplay("Ch = {Ch}, Kind = {Kind}")]
internal sealed class Char(char ch, string? kind) : Token(kind)
{
    private readonly char Ch = ch;

    /// <inheritdoc />
    [Pure]
    public override int Match(SourceSpan source)
        => source.HasValue && source[0] == Ch ? 1 : 0;
}
