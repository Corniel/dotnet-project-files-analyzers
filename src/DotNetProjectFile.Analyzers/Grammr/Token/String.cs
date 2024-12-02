using Grammr.Text;

namespace Grammr;

[DebuggerDisplay("String = {Str}, Kind = {Kind}")]
internal sealed class String(string str, string? kind) : Token(kind)
{
    private readonly string Str = str;

    /// <inheritdoc />
    [Pure]
    public override int Match(SourceSpan source)
        => source.StartsWith(Str).HasValue ? Str.Length : 0;
}
