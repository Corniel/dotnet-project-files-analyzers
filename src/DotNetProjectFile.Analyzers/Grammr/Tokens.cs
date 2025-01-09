using Grammr.Text;

namespace Grammr;

/// <summary>Represents a sequence of tokens.</summary>
public abstract class Tokens
{
    /// <summary>Tokenizes the source span.</summary>
    [Pure]
    public abstract ResultCollection Tokenize(SourceSpan source);

    /// <summary>Creates a switch of tokens to choose one of.</summary>
    public static Tokens operator |(Tokens l, Tokens r) => new Switch([l, r]);

    /// <summary>Creates a required sequence of tokens.</summary>
    public static Tokens operator &(Tokens l, Tokens r) => new Sequence([l, r]);

#if NETSTANDARD2_0
    internal Tokens this[Range range] => new Repeat(this, range.Start.Value, range.End.Value);

    public Tokens Repeat(int min, int max = int.MaxValue) => new Repeat(this, min, max);
#else
    public Tokens this[Range range] => new Repeat(this, range.Start.Value, range.End.Value);
#endif

    /// <summary>The grammar may or may not match.</summary>
    public Tokens Option => new Repeat(this, 0, 1);

    /// <summary>This grammar may match multiple times.</summary>
    public Tokens Star => new Repeat(this, 0, int.MaxValue);

    /// <summary>This grammar may match multiple times, but at least once.</summary>
    public Tokens Plus => new Repeat(this, 1, int.MaxValue);
}
