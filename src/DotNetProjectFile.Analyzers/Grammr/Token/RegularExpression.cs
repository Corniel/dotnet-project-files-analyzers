using Grammr.Text;
using System.Text.RegularExpressions;

namespace Grammr;

[DebuggerDisplay("Pattern = {Pattern}, Kind = {Kind}")]
internal sealed class RegularExpression(Regex pattern, string? kind) : Token(kind)
{
    public RegularExpression(string pattern, string? kind) : this(Regex(pattern), kind) { }

    private readonly Regex Pattern = pattern;

    /// <inheritdoc />
    [Pure]
    public override int Match(SourceSpan source) => Pattern.Match(source.ToString()).Length;

    private static Regex Regex(string regex) => regex[0] == '^'
         ? new(regex, Options, Timeout)
         : new('^' + regex, Options, Timeout);

    private static readonly RegexOptions Options = RegexOptions.CultureInvariant;

    private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(10);
}
