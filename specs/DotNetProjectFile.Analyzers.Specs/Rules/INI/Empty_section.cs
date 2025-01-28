using DotNetProjectFile.Analyzers.Ini;

namespace Rules.INI.Empty_section;

public class Reports
{
    [Test]
    public void empty_sections() => new EmptySection()
        .ForProject("IniFileWithEmptySection.cs")
        .HasIssue(Issue.WRN("Proj4010", "Section [empty.*] is empty.").WithSpan(09, 00, 09, 08).WithPath(".editorconfig"));
}

public class Guards
{
    [TestCase("CompliantCSharp.cs")]
    [TestCase("CompliantCSharpPackage.cs")]
    public void Projects_without_issues(string project) => new EmptySection()
        .ForProject(project)
        .HasNoIssues();
}

