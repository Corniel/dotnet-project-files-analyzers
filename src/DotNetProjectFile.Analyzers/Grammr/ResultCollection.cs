namespace Grammr;

[DebuggerTypeProxy(typeof(CollectionDebugView))]
[DebuggerDisplay("Count = {Count}")]
public readonly struct ResultCollection<TResult> : IReadOnlyList<TResult>
    where TResult : struct, GrammarResult
{
    public static readonly ResultCollection<TResult> Empty = new([]);

    private ResultCollection(TResult[] items) => Items = items;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly TResult[] Items;

    /// <inheritdoc />
    public int Count => Items.Length;

    public bool Success => Items.Length != 0 && Items[0].Success;

    /// <inheritdoc />
    public TResult this[int index] => Items[index];

    [Pure]
    public ResultCollection<TResult> Add(TResult result)
    {
        // If empty always add.
        if (Count == 0)
        {
            return new([result]);
        }
        var first = Comparer.Compare(result, Items[0]);

        return (first, result.Success, Success) switch
        {
            // Failure is not better.
            (>= 0, false, _) => this,

            // Best is not successful, and result is better.
            (< 0, _, false) => new([result, .. Items.Skip(1)]),

            // Add result to existing cases.
            _ => Sort([result, .. Items]),
        };
    }

    private static ResultCollection<TResult> Sort(TResult[] items)
    {
        Array.Sort(items, Comparer);
        return new(items);
    }

    /// <inheritdoc />
    [Pure]
    public IEnumerator<TResult> GetEnumerator() => ((IReadOnlyCollection<TResult>)Items).GetEnumerator();

    /// <inheritdoc />
    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private static readonly Sorter Comparer = new();

    private sealed class Sorter : IComparer<TResult>
    {
        public int Compare(TResult x, TResult y)
        {
            var compare = x.Remaining.Length.CompareTo(y.Remaining.Length);

            if (compare == 0)
            {
                // with the same length, failure first.
                compare = x.Success.CompareTo(y.Success);
            }
            return compare;
        }
    }
}
