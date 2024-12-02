#pragma warning disable S4050 // Operators should be overloaded consistently
// Operators are used here for clean syntax.

using DotNetProjectFile.Parsing.Internal;
using DotNetProjectFile.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace DotNetProjectFile.Parsing;

/// <summary>A way of defining formal grammar.</summary>
public partial class Grammar
{
    /// <summary>Initializes a new instance of the <see cref="Grammar"/> class.</summary>
    /// <remarks>
    /// This class should not be instantiated.
    /// </remarks>
    protected Grammar() { }

    /// <summary>Parse the source text.</summary>
    [Pure]
    public Parser Parse(SourceText sourceText)
    {
        var parser = Match(Parser.New(new(sourceText)));
        return parser.State == Matching.EoF
            ? parser
            : Parser.NoMatch;
    }

    /// <summary>Matches the grammar on the current state of the parser.</summary>
    /// <param name="parser">
    /// The (current state of the) parser.
    /// </param>
    /// <returns>
    /// An updated parser.
    /// </returns>
    [Pure]
    public virtual Parser Match(Parser parser) => throw new NotImplementedException();

    /// <summary>This grammar may or may not match.</summary>
    [Pure]
    public Grammar Option => new Internal.Repeat(this, 0, 1);

    /// <summary>This grammar must not match.</summary>
    [Pure]
    public Grammar Not => new Internal.Repeat(this, 0, 0);

    /// <summary>This grammar may match multiple times.</summary>
    [Pure]
    public Grammar Star => Repeat(0);

    /// <summary>This grammar may match multiple times, but at least once.</summary>
    [Pure]
    public Grammar Plus => Repeat(1);

    /// <summary>This grammar may match multiple times.</summary>
    /// <param name="min">
    /// The minimum number of matches.
    /// </param>
    /// <param name="max">
    /// The maximum number of matches.
    /// </param>
    /// <returns>
    /// A new grammar.
    /// </returns>
    [Pure]
    public Grammar Repeat(int min, int max = int.MaxValue) => new Internal.Repeat(this, min, max);

    /// <summary>The grammar must not match.</summary>
    public static Grammar operator ~(Grammar grammar) => grammar.Not;

    /// <summary>The left or the right grammar should match.</summary>
    public static Grammar operator |(Grammar l, Grammar r) => new Or(l, r);

    /// <summary>The left grammar must be followed by the right grammar.</summary>
    public static Grammar operator &(Grammar l, Grammar r) => new And(l, r);

    /// <summary>On a matching grammar, an syntax node is added to the context.</summary>
    public static Grammar operator +(Grammar grammar, CreateSyntaxNode create) => new AddSyntaxNode(grammar, create);
}
