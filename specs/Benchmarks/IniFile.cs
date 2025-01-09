using DotNetProjectFile.Ini;
using Grammr.Text;
using Microsoft.CodeAnalysis.Text;
using SyntaxTree = DotNetProjectFile.Syntax.SyntaxTree;

namespace Benchmarks;

public class IniFile
{
    private static readonly string root = string.Join("/", Enumerable.Repeat("..", 7)) + "/Files/";

    private readonly List<SourceSpan> Spans = [];
    
    public IniFile()
    {
        string[] files = [ "ini-0027-lines.ini", "ini-0036-lines.ini", "ini-1220-lines.ini" ];
        foreach(var file in files)
        {
            using var stream = new FileStream(root + file, FileMode.Open, FileAccess.Read);
            var span = SourceText.From(stream);
            Spans.Add(new(span));
        }
    }

    [Params(0, 1, 2)]
    public int Index { get; set; }

    [Benchmark]
    public Grammr.Syntax.TreeNode? Parse() => IniGrammar.file.Tokenize(Spans[Index])[0].Node;
}
