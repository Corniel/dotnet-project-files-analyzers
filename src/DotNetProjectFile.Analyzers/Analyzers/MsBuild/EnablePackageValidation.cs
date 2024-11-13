namespace DotNetProjectFile.Analyzers.MsBuild;

[DiagnosticAnalyzer(LanguageNames.CSharp, LanguageNames.VisualBasic)]
public sealed class EnablePackageValidation() : MsBuildProjectFileAnalyzer(Rule.EnablePackageValidation)
{
    public override IReadOnlyCollection<ProjectFileType> ApplicableTo => ProjectFileTypes.ProjectFile;

    protected override void Register(ProjectFileAnalysisContext context)
    {
        var project = context.File;
        if (project.IsPackable() && !project.IsDevelopmentDependency() && !project.PackageValidationEnabled())
        {
            context.ReportDiagnostic(Descriptor, context.File);
        }
    }
}
