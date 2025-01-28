using DotNetProjectFile.Analyzers.EditorConfig;

namespace Rules.EditorConfig.Header_must_be_glob;

public class Reports
{
    [Test]
    public void invalid_GLOBs() => new HeaderMustBeGlob()
        .ForProject("InvalidEditorConfigHeaders.cs")
        .HasIssue(Issue.WRN("Proj4050", "Header [*.{cs,json}}] is not a valid GLOB.").WithSpan(07, 01, 07, 12).WithPath(".editorconfig"));
}

public class Guards
{
    [TestCase("CompliantCSharp.cs")]
    [TestCase("CompliantCSharpPackage.cs")]
    public void Projects_without_issues(string project) => new HeaderMustBeGlob()
        .ForProject(project)
        .HasNoIssues();
}

