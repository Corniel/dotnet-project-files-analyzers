using Grammr.Text;

namespace Grammr;

public abstract class Parser
{
    public readonly record struct Result<T>(
        T? Value,
        ImmutableArray<SourceSpanToken> Tokens,
        SourceSpan Source,
        SourceSpan Remaining,
        bool Success = true) : GrammarResult;
}
