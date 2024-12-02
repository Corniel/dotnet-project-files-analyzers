using Grammr.Text;

namespace Grammr;

public static class Tokenizer
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    [DebuggerTypeProxy(typeof(CollectionDebugView))]
    public readonly record struct Result(
        ImmutableArray<SourceSpanToken> Tokens,
        SourceSpan Remaining,
        bool Success,
        string? Message) : GrammarResult, IReadOnlyList<SourceSpanToken>
    {
        /// <inheritdoc />
        public int Count => Tokens.Length;

        /// <inheritdoc />
        public SourceSpanToken this[int index] => Tokens[index];

        /// <inheritdoc />
        [Pure]
        public IEnumerator<SourceSpanToken> GetEnumerator() => ((IEnumerable<SourceSpanToken>)Tokens).GetEnumerator();

        /// <inheritdoc />
        [Pure]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        [Pure]
        private string DebuggerDisplay => Success
            ? $"Tokens = {Tokens.Length}, Remaining = {Remaining.Length}"
            : $"Failure = {Message}, Remaining = {Remaining.Length}";

        [Pure]
        public static Result Successful(SourceSpan remainder, params IEnumerable<SourceSpanToken> tokens)
            => new([.. tokens], remainder, true, null);

        [Pure]
        public static Result NoMatch(SourceSpan remainder, string message)
            => new([], remainder, false, message);

        public Result Merge(Result other) => (Success, other.Success) switch
        {
            (true, true) => Successful(other.Remaining, [.. Tokens, .. other.Tokens]),
            (true, false) => other,
            _ => throw new InvalidOperationException("Can not merge on unsuccessful results."),
        };
    }
}
