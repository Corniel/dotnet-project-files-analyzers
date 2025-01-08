using Microsoft.CodeAnalysis.Text;

namespace Grammr.Text;

/// <summary>Represents a syntax token.</summary>
[DebuggerDisplay("{DebuggerDisplay}")]
public readonly struct SourceSpanToken(SourceSpan sourceSpan, string? kind = null) : IEquatable<SourceSpanToken>
{
    /// <summary>The (selected) source span.</summary>
    public readonly SourceSpan SourceSpan = sourceSpan;

    /// <summary>The token kind.</summary>
    public readonly string? Kind = kind;

    /// <summary>The span of the token.</summary>
    public TextSpan Span => SourceSpan.Span;

    /// <summary>The text of the token.</summary>
    public string Text => SourceSpan.Text;

    /// <summary>The length of the token.</summary>
    public int Length => SourceSpan.Length;

    /// <summary>Gets the line position span of the token.</summary>
    public LinePositionSpan LinePositionSpan => SourceSpan.SourceText.Lines.GetLinePositionSpan(new(Span.Start, Span.End - Span.Start));

    /// <inheritdoc />
    [Pure]
    public override string ToString() => Text;

    /// <inheritdoc />
    [Pure]
    public override bool Equals(object? obj) => obj is SourceSpanToken other && Equals(other);

    /// <inheritdoc />
    [Pure]
    public bool Equals(SourceSpanToken other)
        => Kind == other.Kind
        && Span == other.Span;

    /// <inheritdoc />
    [Pure]
    public override int GetHashCode() 
        => (Kind ?? string.Empty).GetHashCode() 
        ^ Span.GetHashCode();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay
        => Kind is { Length: > 0 }
        ? $"{Span} {Text}, {Kind}"
        : $"{Span} {Text}";

    /// <summary>Returns true if left and right are equal.</summary>
    public static bool operator ==(SourceSpanToken left, SourceSpanToken right) => left.Equals(right);

    /// <summary>Returns true if left and right are different.</summary>
    public static bool operator !=(SourceSpanToken left, SourceSpanToken right) => !(left == right);
}
