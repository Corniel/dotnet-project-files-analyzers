using Grammr.Text;

namespace Grammr;

public static class Tokenizer
{
    public readonly record struct Result(
        ImmutableArray<SourceSpanToken> Tokens,
        SourceSpan Remaining,
        bool Success,
        string? Message) : GrammarResult
    {
        [Pure]
        public static Result Successful(SourceSpan remainder, params IEnumerable<SourceSpanToken> tokens)
            => new([.. tokens], remainder, true, null);

        [Pure]
        public static Result NoMatch(SourceSpan remainder, string message)
            => new([], remainder, false, message);
    }

    public static Result Tokenize(SourceSpan source, params IEnumerable<Tokens> matchers)
    {
        

        return default;
    }


    //private readonly ImmutableArray<Tokens> Tokens = tokens;

    //private readonly Token Invalid = new RegularExpression(".+", "?");

    ///// <inheritdoc />
    //public int Count => Tokens.Length;

    ///// <summary>Tokenizes the source span.</summary>
    //[Pure]
    //public Slice<SourceSpanToken> Tokenize(SourceSpan source)
    //{
    //    var all = new List<SourceSpanToken>();

    //    var current = source;

    //    while (current.HasValue)
    //    {
    //        ////var next = Next(current, all) ?? Invalid.Next(current, all)!.Value;

    //        //all.Add(next);
    //        //current = current.Skip(next.Length);
    //    }
    //    return new(0, all.Count, all.ToImmutableArray());
    //}

    ////[Pure]
    ////public SourceSpanToken? Next(SourceSpan source, IReadOnlyList<SourceSpanToken> done) => Tokens
    ////   .Select(t => t.Next(source, done))
    ////   .FirstOrDefault(t => t.HasValue);
}
