using DotNetProjectFile.Analyzers.Ini;

namespace Rules.INI.INI_syntax;

public class Reports
{
    [Test]
    public void errors() => new IniSyntaxAnalyzer()
        .ForProject("IniSyntaxErrors.cs")
        .HasIssues(
            new Issue("Proj4000", "extraneous input 'Comment' expecting ASSIGN.").WithSpan(10, 08, 10, 14),
#if Is_Windows
            new Issue("Proj4000", @"missing TEXT at '\r\n'.").WithSpan(10, 26, 10, 27),
#else
            new Issue("Proj4000", @"missing TEXT at '\n'.").WithSpan(10, 26, 10, 26),
#endif
            new Issue("Proj4000", "extraneous input ']' expecting {TEXT, ASSIGN, '[', NL, COMMENT, WS}.").WithSpan(12, 01, 12, 01));
}

public class Guards
{
    [TestCase("CompliantCSharp.cs")]
    [TestCase("CompliantCSharpPackage.cs")]
    public void Projects_without_issues(string project) => new IniSyntaxAnalyzer()
        .ForProject(project)
        .HasNoIssues();
}

