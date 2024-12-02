using Grammr.Text;

namespace Grammr;

public interface GrammarResult
{
    bool Success { get; }

    SourceSpan Remaining { get; }
}
