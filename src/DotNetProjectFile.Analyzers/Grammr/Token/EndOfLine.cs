using Grammr.Text;

namespace Grammr;

[DebuggerDisplay("EOL, Kind = {Kind}")]
internal sealed class EndOfLine(string? kind) : Token(kind)
{
    /// <inheritdoc />
    [Pure]
    public override int Match(SourceSpan source) => source switch
    {
        _ when source.StartsWith("\r\n").HasValue => 2,
        _ when source.HasValue && source[0] == '\n' => 1,
        _ => 0,
    };
}
