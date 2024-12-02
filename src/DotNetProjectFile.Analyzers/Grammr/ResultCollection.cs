namespace Grammr;

[DebuggerTypeProxy(typeof(CollectionDebugView))]
[DebuggerDisplay("Count = {Count}")]
public readonly struct ResultCollection<TResult> : IReadOnlyCollection<TResult>
    where TResult : struct, GrammarResult
{
    public static readonly ResultCollection<TResult> Empty = new([]);

    private readonly TResult[] Items;

    private ResultCollection(TResult[] items) => Items = items;

    [Pure]
    public ResultCollection<TResult> Add(TResult result)
    {
        if (result.Success || Count == 0 || Items[^1].Success)
        {
            var next = new TResult[Count + 1];
            var i = 0;

            while (i < Count)
            {
                var existing = Items[i];

                if (First(existing, result))
                {
                    next[i] = existing;
                }
                else
                {
                    break;
                }
                i++;
            }

            next[i] = result;
            Array.Copy(Items, i, next, i + 1, Count - i);

            return new(next);
        }
        else
        {
            var next = new TResult[Count];
            Array.Copy(Items, next, Count - 1);

            var last = Items[^1];

            next[^1] = result.Remaining.Length < last.Remaining.Length
                ? result
                : last;

            return new(next);
        }

        static bool First(TResult l, TResult r) => (l.Success, r.Success) switch
        {
            (true, false) => true,
            (false, true) => false,
            _ => r.Remaining.Length < l.Remaining.Length,
        };
    }

    /// <inheritdoc />
    public int Count => Items.Length;

    /// <inheritdoc />
    [Pure]
    public IEnumerator<TResult> GetEnumerator() => ((IReadOnlyCollection<TResult>)Items).GetEnumerator();

    /// <inheritdoc />
    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
