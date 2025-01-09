using Grammr.Text;

namespace Grammr;

[DebuggerDisplay("EOF")]
internal sealed class EndOfFile : Tokens
{
    public override ResultCollection Tokenize(TokenStream stream)
        => ResultCollection.Empty.Add(stream.Remaining.Length == 0
            ? Result.Successful(null, stream)
            : Result.NoMatch(stream, "Expected EndOfFile."));
}
