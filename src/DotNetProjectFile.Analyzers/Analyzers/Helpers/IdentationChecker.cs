﻿using DotNetProjectFile.Xml;
using Microsoft.CodeAnalysis.Text;

namespace DotNetProjectFile.Analyzers.Helpers;

internal sealed class IdentationChecker<TContext>(char ch, int repeat, DiagnosticDescriptor descriptor)
    where TContext : DiagnosticReporter
{
    private readonly char Char = ch;
    private readonly int Repeat = repeat;
    private readonly DiagnosticDescriptor Descriptor = descriptor;

    public void Walk(XmlAnalysisNode node, SourceText text, TContext context)
    {
        Report(node, text, context, true);
        Report(node, text, context, false);

        foreach (var child in node.Children())
        {
            Walk(child, text, context);
        }
    }

    private void Report(XmlAnalysisNode node, SourceText text, TContext context, bool start)
    {
        var checkSelfClosingEnd = !start && node.Positions.IsSelfClosing;
        var startAndEndOnSameLine = !start && node.Positions.StartElement.End.Line == node.Positions.EndElement.Start.Line;

        if (checkSelfClosingEnd || startAndEndOnSameLine) { return; }

        var element = start ? node.Positions.StartElement : node.Positions.EndElement;

        if (!ProperlyIndented(element))
        {
            var name = start ? node.LocalName : '/' + node.LocalName;
            context.ReportDiagnostic(Descriptor, element, name);
        }

        bool ProperlyIndented(LinePositionSpan element)
        {
            var width = Repeat * node.Depth;

            if (width != element.Start.Character)
            {
                return false;
            }
            else if (width == 0)
            {
                return true;
            }
            else
            {
                var span = new LinePositionSpan(new(element.Start.Line, 0), new(element.Start.Line, width));
                var indent = text.ToString(text.Lines.GetTextSpan(span));
                return indent.All(ch => ch == Char);
            }
        }
    }
}
