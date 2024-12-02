using Grammr.Text;
using Microsoft.CodeAnalysis.Text;

namespace DotNetProjectFile.IO.Globbing;

internal static class GlobParser
{
    public static Segement? TryParse(string str)
        => TryParse(new SourceSpan(SourceText.From(str)));

    private static Segement? TryParse(SourceSpan span)
    {
        var group = new List<Segement>();

        while (!span.IsEmpty)
        {
            if (span.First == '?')
            {
                group.Add(Segement.AnyChar);
                span++;
            }
            else if (span.Matches(c => c == '*') is { } wildcards)
            {
                group.Add(wildcards.Length == 1 ? Segement.Wildcard : Segement.RecursiveWildcard);
                span = span.Skip(wildcards.Length);
            }
            else if (span.Option(out var option) is { } sp)
            {
                group.Add(option!);
                span = span.Skip(sp.Length);
            }
            else if (span.Sequence() is { } sequence)
            {
                var text = span.SourceText.ToString(sequence);
                group.Add(text[0] == '!' ? new NotSequence(text[1..]) : new Sequence(text));
                span = span.Skip(sequence.Length + 2);
            }
            else if (span.Literal() is { } literal)
            {
                group.Add(new Literal(span.SourceText.ToString(literal)));
                span = span.Skip(literal.Length);
            }
            else
            {
                // unparsable
                return null;
            }
        }
        return group.Count == 1
            ? group[0]
            : Segement.Group(group);
    }

    private static TextSpan? Sequence(this SourceSpan span)
    {
        if (span.StartsWith('[') is { })
        {
            span++;

            if (span.Matches(c => c != ']' && c != '[') is { } match &&
                span.Skip(match.Length).StartsWith(']') is { })
            {
                return match;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    private static TextSpan? Option(this SourceSpan span, out Option? option)
    {
        option = null;
        return span.First == '{'
            ? OptionBody(span, ref option)
            : null;
    }

    private static TextSpan? OptionBody(this SourceSpan span, ref Option? option)
    {
        var sp = span;

        sp++;

        var options = new List<Segement>();

        while (sp.Length != 0)
        {
            if (sp.First == ',')
            {
                // unexpected comma.
                if (options.Count == 0)
                {
                    return null;
                }
                sp++;
            }

            // closing.
            else if (sp.First == '}')
            {
                return OptionClosing(span, ref option, sp, options);
            }

            // comma is missing.
            else if (options.Count != 0)
            {
                return null;
            }

            if (sp.First == '{' && sp.Option(out var nested) is { } nest)
            {
                options.Add(nested!);
                sp = sp.Skip(nest.Length);
            }
            else if (sp.Matches(c => c != ',' && c != '}') is { } next
                && TryParse(sp.Trim(next)) is { } sub)
            {
                options.Add(sub);
                sp = sp.Skip(next.Length);
            }
            else
            {
                return null;
            }
        }
        return null;
    }

    private static TextSpan? OptionClosing(SourceSpan span, ref Option? option, SourceSpan sp, List<Segement> options)
    {
        if (options.Count != 0)
        {
            option = new Option(options);
            return new(span.Start, sp.Start - span.Start + 1);
        }
        else
        {
            return null;
        }
    }

    private static TextSpan? Literal(this SourceSpan span)
        => span.Matches(c => c != '?' && c != '*' && c != '[' && c != '{' && c != '}' && c != ',');
}
