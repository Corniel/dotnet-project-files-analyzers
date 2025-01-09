using Grammr.Text;

namespace Grammr;

[DebuggerTypeProxy(typeof(CollectionDebugView))]
public readonly struct Result : IEquatable<Result>
{
    private Result(
        Syntax.TreeNode? node,
        SourceSpan remaining,
        bool success,
        string? message)
    {
        Node = node;
        Remaining = remaining;
        Success = success;
        Message = message;
    }

    public Syntax.TreeNode? Node { get; }

    public IReadOnlyCollection<SourceSpanToken> Tokens => Node?.Tokens ?? [];

    /// <summary>The remaining source span to parse.</summary>
    public SourceSpan Remaining { get; }

    /// <summary>Indicates if the parsing was successful.</summary>
    public bool Success { get; }

    /// <summary>The (optional) error message.</summary>>
    public string? Message { get; }

    /// <inheritdoc />
    [Pure]
    public override bool Equals(object? obj) => obj is Result other && Equals(other);

    /// <inheritdoc />
    [Pure]
    public bool Equals(Result other)
        => Success == other.Success
        && Remaining.Length == other.Remaining.Length
        && Node == other.Node;

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

    private static string Format(string str) => str.Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t");

    [Pure]
    public static Result Successful(Syntax.TreeNode? node, SourceSpan remainder)
        => new(node, remainder, true, null);

    [Pure]
    public static Result NoMatch(SourceSpan remainder, string message)
        => new(null, remainder, false, message);
}
