using Grammr.Text;

namespace Grammr;

[DebuggerDisplay("EOF")]
internal sealed class EndOfFile : Tokens
{
    public override ResultCollection<Tokenizer.Result> Tokenize(SourceSpan source)
        => ResultCollection<Tokenizer.Result>.Empty.Add(source.Length == 0
            ? Tokenizer.Result.Successful(source, new SourceSpanToken(source, nameof(EndOfFile)))
            : Tokenizer.Result.NoMatch(source, "Expected EndOfFile."));
}
