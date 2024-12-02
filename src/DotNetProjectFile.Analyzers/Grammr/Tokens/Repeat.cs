using Grammr;
using Grammr.Text;

internal sealed class Repeat(Tokens tokens, int minOccurs, int maxOccurs) : Tokens
{
    private readonly Tokens Tokens = tokens;
    private readonly int MinOccurs = minOccurs;
    private readonly int MaxOccurs = maxOccurs;

    /// <inheritdoc />
    [Pure]
    public override ResultCollection<Tokenizer.Result> Tokenize(SourceSpan source)
    {
        var results = ResultCollection<Tokenizer.Result>.Empty;

        throw new NotImplementedException();
    }

    //[Pure]
    //public override int? Match(Slice<SourceSpanToken> tokens)
    //{
    //    var length = 0;

    //    var curr = tokens;

    //    var repeat = 0;

    //    while (curr.Count != 0 && repeat < MaxOccurs)
    //    {
    //        if (Tokens.Match(curr) is not { } len)
    //        {
    //            break;
    //        }
    //        else
    //        {
    //            repeat++;
    //            length += len;
    //            curr = curr.Skip(len);
    //        }
    //    }

    //    return repeat < MinOccurs
    //        ? null
    //        : length;
    //}
}
