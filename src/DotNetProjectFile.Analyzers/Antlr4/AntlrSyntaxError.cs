using Antlr4.Runtime;
using Microsoft.CodeAnalysis.Text;

namespace Antlr4;

public sealed record AntlrSyntaxError
{
    public required IRecognizer Recognizer { get; init; }
    public required string Message { get; init; }
    public required TextSpan TextSpan { get; init; }
    public required RecognitionException? Exceptìon { get; init; }
}
