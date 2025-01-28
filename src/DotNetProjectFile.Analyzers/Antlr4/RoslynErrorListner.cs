using Antlr4.Runtime;
using Microsoft.CodeAnalysis.Text;
using System.IO;

namespace Antlr4;

public sealed class RoslynErrorListner : IAntlrErrorListener<IToken>
{
    public void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    {
        Errors.Add(new()
        {
            Recognizer = recognizer,
            Message = msg,
            TextSpan = new(offendingSymbol.StartIndex, offendingSymbol.StopIndex - offendingSymbol.StartIndex),
            Exceptìon = e,
        });
    }

    public List<AntlrSyntaxError> Errors { get; } = [];
}
