using Grammr;
using Grammr.Text;
using Microsoft.CodeAnalysis.Text;

namespace Benchmarks;

public class IniParsing
{
    private static readonly string root = string.Join("/", Enumerable.Repeat("..", 7)) + "/Files/";

    private readonly List<SourceText> Sources = [];
    
    public IniParsing()
    {
        string[] files = [ "ini-0027-lines.ini", "ini-0036-lines.ini", "ini-1220-lines.ini" ];
        foreach(var file in files)
        {
            using var stream = new FileStream(root + file, FileMode.Open, FileAccess.Read);
            Sources.Add(SourceText.From(stream));
        }
    }

    [Params(0, 1, 2)]
    public int Index { get; set; }

    [Benchmark]
    public Grammr.Syntax.Node HomeGrown() => IniGrammar.file.Parse(TokenStream.From(Sources[Index])).Root;

    [Benchmark]
    public DotNetProjectFile.Ini.IniFileSyntax ANTLR4() => DotNetProjectFile.Ini.IniFileSyntax.Parse(Sources[Index]);
}

file sealed class IniGrammar : Grammar
{
    public static readonly Token EndOfLine = eol();
    public static readonly Token WhiteSpace = match(IsWhiteSpace);
    public static readonly Token Comment = regex(@"[#;][^\r]*");
    public static readonly Token HeaderStart = ch('[');
    public static readonly Token HeaderEnd = ch(']');
    public static readonly Token HeaderText = regex(@"[^[\]]*");
    public static readonly Token Colon = ch(':');
    public static readonly Token Equal = ch('=');
    public static readonly Token Key = regex(@"[^\s=:#;]+");
    public static readonly Token Value = regex(@"[^\s#;]+");

    public static readonly Parser end = EndOfLine | eof;
    public static readonly Parser ws = WhiteSpace.Option;

    public static readonly Parser ws_line = ws & EndOfLine;

    public static readonly Parser header_line =
        ws
        & HeaderStart
        & HeaderText
        & HeaderEnd
        & ws
        & Comment.Option
        & end;

    public static readonly Parser comment_line = ws & Comment & end;

    public static readonly Parser kvp_line = 
        ws
        & Key
        & ws
        & (Equal | Colon)
        & ws
        & Value
        & ws
        & Comment.Option
        & end;

    public static readonly Parser line =
        ws_line
        | kvp_line
        | header_line
        | comment_line;

    public static readonly Parser file = line.Star.Greedy;

    private static bool IsWhiteSpace(char ch) => ch is ' ' or '\t';
}
