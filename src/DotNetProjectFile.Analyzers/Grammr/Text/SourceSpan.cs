using Microsoft.CodeAnalysis.Text;
using System.Text.RegularExpressions;

namespace Grammr.Text;

/// <summary>Represents a span of a source text.</summary>
/// <param name="sourceText">
/// The full source text.
/// </param>
/// <param name="textSpan">
/// The (selected) span of the source text.
/// </param>
public readonly struct SourceSpan(SourceText sourceText, TextSpan textSpan) : IEquatable<SourceSpan>
{
    /// <summary>Initializes a new instance of the <see cref="SourceSpan"/> struct.</summary>
    /// <param name="sourceText">
    /// The full source text.
    /// </param>
    public SourceSpan(SourceText sourceText) : this(sourceText, new(0, sourceText.Length)) { }

    /// <summary>A match on source span.</summary>
    /// <param name="sourceSpan">
    /// The source span to match.
    /// </param>
    /// <returns>
    /// A text span indicating the match result.
    /// </returns>
    public delegate TextSpan? Match(SourceSpan sourceSpan);

    /// <remarks>Syntactic sugar that allows the use of NoMatch over null.</remarks>
    private static readonly TextSpan? NoMatch = null;

    /// <summary>The underlying source text.</summary>
    public readonly SourceText SourceText = sourceText;

    /// <summary>The (selected) span.</summary>
    public readonly TextSpan Span = textSpan;

    /// <summary>The (selected) source text.</summary>
    public string Text => SourceText.ToString(Span);

    /// <summary>The First char in the span.</summary>
    public char First => this[0];

    /// <summary>Gets the n^th char of the source text.</summary>
    public char this[int index] => SourceText[Start + index];

    /// <summary>The start position.</summary>
    public int Start => Span.Start;

    /// <summary>The length of the span.</summary>
    public int Length => Span.Length;

    /// <summary>Indicates if the span is empty.</summary>
    public bool IsEmpty => Span.IsEmpty;

    /// <summary>Indicates if the span is not empty.</summary>
    public bool HasValue => !Span.IsEmpty;

    /// <summary>Trims the source span.</summary>
    /// <param name="span">
    /// The span to trim to.
    /// </param>
    /// <returns>
    /// A trimmed source span.
    /// </returns>
    public SourceSpan Trim(TextSpan span) => new(SourceText, span);

    /// <summary>Takes the number of specified characters from the start of this source span.</summary>
    [Pure]
    public SourceSpan Take(int count) => new(SourceText, new(Start, count));

    /// <summary>Trims the source span from the left.</summary>
    /// <param name="left">
    /// The number of characters to trim.
    /// </param>
    /// <returns>
    /// A trimmed source span.
    /// </returns>
    [Pure]
    public SourceSpan Skip(int left) => new(SourceText, new(Start + left, Length - left));

    /// <summary>Matches until the end of line.</summary>
    /// <returns>
    /// The matching text span (can have a length of zero).
    /// </returns>
    [Pure]
    public TextSpan Line()
    {
        var line = SourceText.Lines.GetLineFromPosition(Start);
        var span = line.Span;
        var text = new TextSpan(Start, span.Length - (Start - span.Start));
        return text;
    }

    /// <summary>Matches the regular expression.</summary>
    /// <param name="regex">
    /// The regular expression to match.
    /// </param>
    /// <returns>
    /// Null if no match, otherwise the matching text span.
    /// </returns>
    [Pure]
    public TextSpan? Line(Regex regex) => Matches(regex, Line());

    /// <summary>Indicates that the text span starts with the specified character.</summary>
    /// <param name="ch">
    /// The character to match.
    /// </param>
    /// <returns>
    /// Null if no match, otherwise the matching text span.
    /// </returns>
    [Pure]
    public TextSpan? StartsWith(char ch)
    {
        var result = !Span.IsEmpty && SourceText[Span.Start] == ch
            ? new TextSpan(Span.Start, 1)
            : NoMatch;

        SourceSpanLogger.Log(SourceText, Span, result, () => $"StartsWith('{ch}')");

        return result;
    }

    /// <summary>Indicates that the text span starts with the specified string.</summary>
    /// <param name="str">
    /// The string to match.
    /// </param>
    /// <returns>
    /// Null if no match, otherwise the matching text span.
    /// </returns>
    [Pure]
    public TextSpan? StartsWith(string str)
    {
        if (Span.Length >= str.Length)
        {
            var pos = Span.Start;

            for (var i = 0; i < str.Length; i++)
            {
                if (SourceText[pos++] != str[i])
                {
                    SourceSpanLogger.Log(SourceText, Span, NoMatch, () => $"StartsWith(\"{str}\")");
                    return NoMatch;
                }
            }
            var result = new TextSpan(Span.Start, str.Length);
            SourceSpanLogger.Log(SourceText, Span, result, () => $"StartsWith(\"{str}\")");
            return result;
        }
        else
        {
            SourceSpanLogger.Log(SourceText, Span, NoMatch, () => $"StartsWith(\"{str}\")");
            return NoMatch;
        }
    }

    /// <summary>Matches the predicate.</summary>
    /// <param name="match">
    /// The required match.
    /// </param>
    /// <returns>
    /// Null if no match, otherwise the matching text span.
    /// </returns>
    [Pure]
    public TextSpan? Matches(Predicate<char> match)
    {
        if (IsEmpty)
        {
            SourceSpanLogger.Log(SourceText, Span, NoMatch, match);
            return NoMatch;
        }

        var len = -1;
        var i = Start;

        while (++len < Length)
        {
            if (!match(SourceText[i++]))
            {
                var result = (len == 0)
                    ? NoMatch
                    : new(Span.Start, len);

                SourceSpanLogger.Log(SourceText, Span, result, match);

                return result;
            }
        }

        SourceSpanLogger.Log(SourceText, Span, Span, match);

        return Span;
    }

    /// <summary>Matches the regular expression.</summary>
    /// <param name="regex">
    /// The regular expression to match.
    /// </param>
    /// <returns>
    /// Null if no match, otherwise the matching text span.
    /// </returns>
    [Pure]
    public TextSpan? Regex(Regex regex) => Matches(regex, Span);

    [Pure]
    private TextSpan? Matches(Regex regex, TextSpan span)
    {
        var text = SourceText.ToString(span);
        var match = regex.Match(text);

        if (match.Index != 0)
        {
            throw new InvalidPattern($"Pattern '{regex}' did match from the start.");
        }

        var result = match.Success
            ? new(Span.Start, match.Length)
            : NoMatch;

        SourceSpanLogger.Log(SourceText, span, result, () => $"Regex({regex})");

        return result;
    }

    /// <inheritdoc />
    [Pure]
    public override string ToString() => Text;

    /// <inheritdoc />
    [Pure]
    public override bool Equals(object obj) => obj is SourceSpan other && Equals(other);

    /// <inheritdoc />
    [Pure]
    public bool Equals(SourceSpan other)
        => Span == other.Span
        && SourceText == other.SourceText;

    /// <inheritdoc />
    [Pure]
    public override int GetHashCode() => Span.GetHashCode();

    /// <summary>Returns true if left and right are equal.</summary>
    /// <param name="left">
    /// Left operator.
    /// </param>
    /// <param name="right">
    /// Right operator.
    /// </param>
    public static bool operator ==(SourceSpan left, SourceSpan right) => left.Equals(right);

    /// <summary>Returns true if left and right are different.</summary>
    /// <param name="left">
    /// Left operator.
    /// </param>
    /// <param name="right">
    /// Right operator.
    /// </param>
    public static bool operator !=(SourceSpan left, SourceSpan right) => !(left == right);

    public static SourceSpan operator ++(SourceSpan span) => span.Skip(1);
}
