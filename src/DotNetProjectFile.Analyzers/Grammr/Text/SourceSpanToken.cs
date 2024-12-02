using Microsoft.CodeAnalysis.Text;

namespace Grammr.Text;

/// <summary>Represents a syntax token.</summary>
[DebuggerDisplay("{DebuggerDisplay}")]
public readonly struct SourceSpanToken(SourceSpan sourceSpan, string? kind = null)
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

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay
        => Kind is { Length: > 0 }
        ? $"{Span} {Text}, {Kind}"
        : $"{Span} {Text}";
}
