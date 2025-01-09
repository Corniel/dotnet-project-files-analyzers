namespace DotNetProjectFile.Collections;

[DebuggerTypeProxy(typeof(CollectionDebugView))]
[DebuggerDisplay("Count = {Count}, Offset = {Offset}")]
public readonly struct Slice<T>(int start, int size, IReadOnlyList<T> list) : IReadOnlyList<T>
{
    public Slice(SliceSpan span, IReadOnlyList<T> list) : this(span.Start, span.Size, list) { }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly IReadOnlyList<T> List = list;

    public int Count { get; } = size;

    public T this[int index] => List[index + Offset];

    private int Offset { get; } = start;

    /// <inheritdoc cref="Enumerable.Skip{TSource}(IEnumerable{TSource}, int)" />
    [Pure]
    public Slice<T> Skip(int count) => new(Offset + count, Count - count, List);

    /// <inheritdoc cref="Enumerable.Take{TSource}(IEnumerable{TSource}, int)" />
    [Pure]
    public Slice<T> Take(int count) => new(Offset, count, List);

    /// <inheritdoc />
    [Pure]
    public IEnumerator<T> GetEnumerator() => List.Skip(Offset).Take(Count).GetEnumerator();

    /// <inheritdoc />
    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
