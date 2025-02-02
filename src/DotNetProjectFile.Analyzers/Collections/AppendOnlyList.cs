using System.Runtime.CompilerServices;

namespace DotNetProjectFile.Collections;

[DebuggerTypeProxy(typeof(CollectionDebugView))]
[DebuggerDisplay("Count = {Count}, Capacity = {Buffer.Length}")]
public readonly struct AppendOnlyList<T> : IReadOnlyList<T>
{
    public static readonly AppendOnlyList<T> Empty = new(0, []);

    /// <remarks>Internal from .NET.</remarks>
    private const int MaxCapacity = 0X7FFFFFC7;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly T[] Buffer;

    /// <summary>Initializes a new instance of the <see cref="AppendOnlyList{T}"/> struct.</summary>
    private AppendOnlyList(int count, T[] buffer)
    {
        Count = count;
        Buffer = buffer;
    }

    /// <inheritdoc />
    public int Count { get; }

    /// <inheritdoc />
    public T this[int index] => Buffer[index];

    /// <summary>Creates a new <see cref="AppendOnlyList{T}"/> with the added item(s).</summary>
    /// <param name="item">
    /// The item(s) to add.
    /// </param>
    /// <remarks>
    /// Null, and null items are ignored.
    /// </remarks>
    [Pure]
    public AppendOnlyList<T> Add(T? item)
    {
        if (item is null)
        {
            return this;
        }
        else
        {
            return AddSingle(item);
        }
    }

    /// <summary>Adds multiple elements to the collection.</summary>
    /// <remarks>
    /// Adding the first item is done like adding a single item: by doing so, a
    /// lock is only required for that first item, because whatever the outcome,
    /// the remaining items can always be added without checking the lock.
    /// </remarks>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AppendOnlyList<T> AddRange(params IEnumerable<T> items)
    {
        var append = this;

        var iterator = items.GetEnumerator();

        while (iterator.MoveNext())
        {
            if (iterator.Current is { } element)
            {
                append = append.AddSingle(element);
#pragma warning disable S1227 // break statements should not be used except for switch cases
                break; // results in the best performance.
#pragma warning restore S1227 // break statements should not be used except for switch cases
            }
        }

        while (iterator.MoveNext())
        {
            if (iterator.Current is { } element)
            {
                append = append.Ensure().Append(element);
            }
        }
        return append;
    }

    /// <inheritdoc cref="ICollection.CopyTo(Array, int)" />
    public void CopyTo(object[] array, int arrayIndex)
    {
        if (Count > 0)
        {
            Array.Copy(Buffer, 0, array, arrayIndex, Count);
        }
    }

    /// <summary>Returns a specified range of contiguous elements from the collection.</summary>
    [Pure]
    public Enumerator<T> Take(int count) => new(Buffer, Math.Min(count, Count));

    /// <summary>
    /// Bypasses a specified number of elements in the collection and then returns the remaining elements.
    /// </summary>
    [Pure]
    public Enumerator<T> Skip(int count) => new(Buffer, count, Count);

    /// <summary>Adds a single item to the collection.</summary>
    /// <remarks>
    /// If the parent collection has not been extended yet, the array buffer is
    /// shared with the new collection (if it still fits), otherwise the buffer
    /// is copied first.
    /// </remarks>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private AppendOnlyList<T> AddSingle(T element)
    {
        var append = Ensure();

        if (append.Buffer != Buffer)
        {
            return append.Append(element);
        }
        else
        {
            lock (Buffer)
            {
                if (Buffer[Count] is null)
                {
                    return Append(element);
                }
            }
            return Copy(Buffer.Length).Append(element);
        }
    }

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private AppendOnlyList<T> Append(T element)
    {
        Buffer[Count] = element;
        return new(Count + 1, Buffer);
    }

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private AppendOnlyList<T> Ensure()
    {
        if (Count < Buffer.Length && Count != 0)
        {
            return this;
        }
        int capacity = Buffer.Length == 0 ? 8 : 2 * Buffer.Length;

        // Allow the list to grow to maximum possible capacity (~2G elements) before encountering overflow.
        // Note that this check works even when _items.Length overflowed thanks to the (uint) cast
        if ((uint)capacity > MaxCapacity) capacity = MaxCapacity;
        return Copy(capacity);
    }

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private AppendOnlyList<T> Copy(int capacity)
    {
        var copy = new T[capacity];
        Array.Copy(Buffer, copy, Count);
        return new(Count, copy);
    }

    /// <inheritdoc cref="IEnumerable{T}.GetEnumerator()" />
    [Pure]
    public Enumerator<T> GetEnumerator() => new(Buffer, Count);

    /// <inheritdoc />
    [Pure]
    IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
