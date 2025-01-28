using DotNetProjectFile.Analyzers.EditorConfig;

namespace Rules.EditorConfig.Use_equals_for_assignment;

public class Reports
{
    [Test]
    public void colon_signs() => new UseEqualsAssign()
        .ForProject("InvalidEditorConfigHeaders.cs")
        .HasIssues(
            Issue.WRN("Proj4051", "Use = instead.").WithSpan(13, 12, 13, 12).WithPath(".editorconfig"),
            Issue.WRN("Proj4051", "Use = instead.").WithSpan(14, 11, 14, 11).WithPath(".editorconfig"));
}

public class Guards
{
    [TestCase("CompliantCSharp.cs")]
    [TestCase("CompliantCSharpPackage.cs")]
    public void Projects_without_issues(string project) => new UseEqualsAssign()
        .ForProject(project)
        .HasNoIssues();
}

