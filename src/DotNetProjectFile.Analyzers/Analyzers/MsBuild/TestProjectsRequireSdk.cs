
namespace DotNetProjectFile.Analyzers.MsBuild;

[DiagnosticAnalyzer(LanguageNames.CSharp, LanguageNames.VisualBasic)]
public sealed class TestProjectsRequireSdk() : MsBuildProjectFileAnalyzer(
    Rule.TestProjectsRequireSdk,
    Rule.UsingMicrosoftNetTestSdkImpliesTestProject)
{
    public override IReadOnlyCollection<ProjectFileType> ApplicableTo => ProjectFileTypes.ProjectFile;

    protected override void Register(ProjectFileAnalysisContext context)
    {
        var isTest = context.File.IsTestProject();
        var hasSdk = context.File
            .Walk().OfType<PackageReference>()
            .Any(r => r.Include == NuGet.Packages.Microsoft_NET_Test_Sdk.Name);
        var hasTUnit = context.File
            .Walk().OfType<PackageReference>()
            .Any(r => r.Include == NuGet.Packages.TUnit.Name);

        var hasSdkOrTUnit = hasSdk || hasTUnit;

        if (isTest && !hasSdkOrTUnit)
        {
            context.ReportDiagnostic(Rule.TestProjectsRequireSdk, context.File);
        }
        else if (!isTest && hasSdkOrTUnit)
        {
            context.ReportDiagnostic(Rule.UsingMicrosoftNetTestSdkImpliesTestProject, context.File);
        }
    }
}
