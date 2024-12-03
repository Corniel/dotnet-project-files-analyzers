using Grammr.Text;

namespace Grammr;

[DebuggerDisplay("EOF")]
internal sealed class EndOfFile : Tokens
{
    public override ResultCollection<Lexer.Result> Tokenize(SourceSpan source)
        => ResultCollection<Lexer.Result>.Empty.Add(source.Length == 0
            ? Lexer.Result.Successful(source, new SourceSpanToken(source, nameof(EndOfFile)))
            : Lexer.Result.NoMatch(source, "Expected EndOfFile."));
}
