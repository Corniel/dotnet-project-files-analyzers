using DotNetProjectFile.Collections;
using Microsoft.CodeAnalysis.Text;

namespace Grammr.Text;

/// <summary>Represents an append-only token stream.</summary>
[DebuggerTypeProxy(typeof(CollectionDebugView))]
[DebuggerDisplay("Count = {Count}, Remaining = {Remaining}")]
public readonly struct TokenStream : IReadOnlyList<SourceSpanToken>, IEquatable<TokenStream>
{
    private readonly AppendOnlyList<Info> Items;
    private readonly SourceText SourceText;

    public static readonly TokenStream None = new(AppendOnlyList<Info>.Empty, SourceText.From(string.Empty));

    [Pure]
    public static TokenStream New(SourceText sourceText) => new(AppendOnlyList<Info>.Empty, sourceText);

    private TokenStream(AppendOnlyList<Info> items, SourceText sourceText)
    {
        Items = items;
        SourceText = sourceText;
    }

    /// <inheritdoc />
    public SourceSpanToken this[int index]
    {
        get
        {
            var info = Items[index];
            return new SourceSpanToken(new(SourceText, info.TextSpan), info.Kind);
        }
    }

    /// <inheritdoc />
    public int Count => Items.Count;

    public int Length => Count == 0 ? 0 : Items[^1].TextSpan.End;

    public SourceSpan Remaining
    {
        get
        {
            if (Count == 0) return new SourceSpan(SourceText);

            var end = Length;
            return new SourceSpan(SourceText, new(end, SourceText.Length - end));
        }
    }

    /// <summary>Gets the text of the underlying source text.</summary>
    public string Text => SourceText.ToString();

    /// <summary>Adds a new token to the stream.</summary>
    public TokenStream Add(int length, string? kind)
        => Count == 0
        ? new(Items.Add(new Info(new(0, length), kind)), SourceText)
        : new(Items.Add(new Info(new(Items[^1].TextSpan.End, length), kind)), SourceText);

    /// <inheritdoc />
    [Pure]
    public override bool Equals(object? obj) => obj is TokenStream other && Equals(other);

    /// <inheritdoc />
    [Pure]
    public bool Equals(TokenStream other)
        => Items.Count == other.Items.Count
        && Items[^1].TextSpan == other.Items[^1].TextSpan
        && Enumerable.SequenceEqual(Items, other.Items);

    /// <inheritdoc />
    [Pure]
    public override int GetHashCode()
    {
        var hash = Items.Count;
        foreach (var item in Items)
        {
            hash ^= (17 * hash) ^ item.GetHashCode();
        }
        return hash;
    }

    /// <inheritdoc />
    [Pure]
    public IEnumerator<SourceSpanToken> GetEnumerator()
    {
        var sourceText = SourceText;

        return Items
            .Select(info => new SourceSpanToken(new(sourceText, info.TextSpan), info.Kind))
            .GetEnumerator();
    }

    /// <inheritdoc />
    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private readonly record struct Info(TextSpan TextSpan, string? Kind);
}
