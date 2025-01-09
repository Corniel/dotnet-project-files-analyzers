using Grammr.Text;

namespace Grammr;

[DebuggerDisplay("{DebuggerDisplay}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public readonly struct Result : IReadOnlyList<SourceSpanToken>, IEquatable<Result>
{
    private Result(
        Syntax.Node? node,
        ImmutableArray<SourceSpanToken> tokens,
        SourceSpan remaining,
        bool success,
        string? message)
    {
        Node = node;
        Tokens = tokens;
        Remaining = remaining;
        Success = success;
        Message = message;
    }

    public Syntax.Node? Node { get; }

    public ImmutableArray<SourceSpanToken> Tokens { get; }

    /// <summary>The remaining source span to parse.</summary>
    public SourceSpan Remaining { get; }

    /// <summary>Indicates if the parsing was successful.</summary>
    public bool Success { get; }

    /// <summary>The (optional) error message.</summary>>
    public string? Message { get; }

    /// <inheritdoc />
    public int Count => Tokens.Length;

    /// <inheritdoc />
    public SourceSpanToken this[int index] => Tokens[index];

    /// <inheritdoc />
    [Pure]
    public override bool Equals(object? obj) => obj is Result other && Equals(other);

    /// <inheritdoc />
    [Pure]
    public bool Equals(Result other)
        => Success == other.Success
        && Remaining.Length == other.Remaining.Length
        && Tokens.Length == other.Tokens.Length;

    /// <inheritdoc />
    [Pure]
    public override int GetHashCode()
    {
        var hash = Success.GetHashCode();
        hash ^= (Message ?? string.Empty).GetHashCode();
        hash ^= hash * 17 + Remaining.Length;

        foreach (var token in Tokens)
        {
            hash *= 17;
            hash ^= token.GetHashCode();
        }
        return hash;
    }

    /// <inheritdoc />
    [Pure]
    public IEnumerator<SourceSpanToken> GetEnumerator() => ((IEnumerable<SourceSpanToken>)Tokens).GetEnumerator();

    /// <inheritdoc />
    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override string ToString() => DebuggerDisplay;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay => Success
        ? $"Tokens = {Tokens.Length} {(Tokens.Any() ? $" ({Format(Tokens[^1].Text)})" : "")}, Remaining = {Remaining.Length} ({RemainingText()})"
        : $"Failure = {Message}, Remaining = {Remaining.Length} ({RemainingText()})";

    private string RemainingText() => Format(Remaining.Length >= 16
        ? Remaining.Text[0..10] + "..."
        : Remaining.Text);

    private static string Format(string str) => str.Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t");

    [Pure]
    public static Result Successful(Syntax.Node? node, SourceSpan remainder, params IEnumerable<SourceSpanToken> tokens)
        => new(node, [.. tokens], remainder, true, null);

    [Pure]
    public static Result NoMatch(SourceSpan remainder, string message)
        => new(null, [], remainder, false, message);

    public Result Merge(Result other) => (Success, other.Success) switch
    {
        (true, true) => Successful(other.Node, other.Remaining, [.. Tokens, .. other.Tokens]),
        (true, false) => other,
        _ => throw new InvalidOperationException("Can not merge on unsuccessful results."),
    };
}
