namespace DotNetProjectFile.Collections;

/// <summary>Enumerator to iterate all elements in <see cref="AppendOnlyList{T}"/>.</summary>
public struct Enumerator<T> : IEnumerator<T>, IEnumerable<T>
{
    private readonly T[] Array;
    private readonly int End;
    private int Index;

    /// <summary>Initializes a new instance of the <see cref="Enumerator{T}"/> struct.</summary>
    internal Enumerator(T[]? array, int start, int end)
    {
        Array = array ?? [];
        End = end;
        Index = start - 1;
    }

    /// <summary>Initializes a new instance of the <see cref="Enumerator{T}"/> struct.</summary>
    internal Enumerator(T[] array, int count) : this(array, 0, count) { }

    /// <inheritdoc />
    public readonly T Current => Array[Index];

    /// <inheritdoc />
    readonly object? IEnumerator.Current => Current;

    /// <inheritdoc />
    [Pure]
    public bool MoveNext() => ++Index < End;

    /// <inheritdoc />
    [ExcludeFromCodeCoverage/* Required for backward comparability. */]
    public void Reset() => throw new NotSupportedException();

    /// <inheritdoc />
    readonly void IDisposable.Dispose() { /* Nothing to dispose. */ }

    /// <inheritdoc />
    [Pure]
    public IEnumerator<T> GetEnumerator() => this;

    /// <inheritdoc />
    [Pure]
    [ExcludeFromCodeCoverage/* Required for backward comparability. */]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
