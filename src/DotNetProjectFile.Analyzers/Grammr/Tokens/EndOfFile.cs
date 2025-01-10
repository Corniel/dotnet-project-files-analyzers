using Grammr.Text;

namespace Grammr;

[DebuggerDisplay("EOF")]
internal sealed class EndOfFile : Tokens
{
    /// <inheritdoc />
    public override ResultQueue Tokenize(TokenStream stream, ResultQueue queue)
        => stream.Remaining.Length == 0
            ? queue.Match(stream.Add(0, "EOF"), null)
            : queue.NoMatch(stream, "Expected EndOfFile.");

    public override ResultCollection Tokenize(TokenStream stream)
        => ResultCollection.Empty.Add(stream.Remaining.Length == 0
            ? Result.Successful(null, stream)
            : Result.NoMatch(stream, "Expected EndOfFile."));
}
