using Grammr.Text;

namespace Grammr;

[DebuggerDisplay("EOF")]
internal sealed class EndOfFile : Tokens
{
    public override ResultCollection Tokenize(SourceSpan source)
        => ResultCollection.Empty.Add(source.Length == 0
            ? Result.Successful(null, source)
            : Result.NoMatch(source, "Expected EndOfFile."));
}
