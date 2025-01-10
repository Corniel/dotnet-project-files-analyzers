using Grammr.Text;

namespace Grammr;

public readonly struct Result : IEquatable<Result>, IComparable<Result>
{
    private Result(
        Syntax.TreeNode? node,
        TokenStream stream,
        bool success,
        string? message)
    {
        Node = node;
        Stream = stream;
        Success = success;
        Message = message;
    }

    public Syntax.TreeNode? Node { get; }

    public TokenStream Stream { get; }

    /// <summary>The remaining source span to parse.</summary>
    public SourceSpan Remaining => Stream.Remaining;

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
        hash ^= Stream.GetHashCode();
        return hash;
    }

    /// <inheritdoc />
    [Pure]
    public int CompareTo(Result other) => other.Stream.Length.CompareTo(Stream.Length);

    [Pure]
    public static Result Successful(Syntax.TreeNode? node, TokenStream stream)
        => new(node, stream, true, null);

    [Pure]
    public static Result Match(TokenStream stream, Syntax.TreeNode? node = null)
      => new(node, stream, true, null);

    [Pure]
    public static Result NoMatch(TokenStream stream, string message)
        => new(null, stream, false, message);
}
