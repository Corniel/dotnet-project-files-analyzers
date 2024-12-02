using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Grammr;

/// <summary>Represents a base to build grammar with.</summary>
[Inheritable]
public class Grammar
{
    public static readonly Tokens eof = new EndOfFile();

    /// <summary>End of Line.</summary>
    public static Token eol([CallerMemberName] string? kind = null) => new EndOfLine(kind);

    /// <summary>Matches if the remaining source starts with the specified character.</summary>
    /// <param name="ch">
    /// The expected character.
    /// </param>
    /// <param name="kind">
    /// The (optional) token kind.
    /// </param>
    /// <returns>
    /// A new grammar.
    /// </returns>
    public static Token ch(char ch, [CallerMemberName] string? kind = null) => new Char(ch, kind);

    /// <summary>Matches if the remaining source matches the predicate.</summary>
    /// <param name="predicate">
    /// The predicate to match.
    /// </param>
    /// <param name="kind">
    /// The (optional) token kind.
    /// </param>
    /// <returns>
    /// A new grammar.
    /// </returns>
    [Pure]
    public static Token match(Predicate<char> predicate, [CallerMemberName] string? kind = default) => new Predicate(predicate, kind);

    /// <summary>Matches if the remaining source starts with the specified string.</summary>
    /// <param name="str">
    /// The expected string.
    /// </param>
    /// <param name="kind">
    /// The (optional) token kind.
    /// </param>
    /// <returns>
    /// A new grammar.
    /// </returns>
    public static Token str(string str, [CallerMemberName] string? kind = null) => new String(str, kind);

    /// <inheritdoc cref="regex(Regex, string?)"/>
    [Pure]
    public static Token regex([StringSyntax(StringSyntaxAttribute.Regex)] string pattern, [CallerMemberName] string? kind = default)
        => new RegularExpression(pattern, kind);

    /// <summary>Matches if the remaining source starts with the specified pattern.</summary>
    /// <param name="pattern">
    /// The expected pattern.
    /// </param>
    /// <param name="kind">
    /// The (optional) token kind.
    /// </param>
    /// <returns>
    /// A new grammar.
    /// </returns>
    [Pure]
    public static Token regex(Regex pattern, [CallerMemberName] string? kind = default) => new RegularExpression(pattern, kind);
}
