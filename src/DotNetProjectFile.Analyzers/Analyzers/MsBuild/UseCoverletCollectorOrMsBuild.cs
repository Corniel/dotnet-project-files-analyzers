using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetProjectFile.Analyzers.MsBuild;

/// <summary>Implements  <see cref="Rule.UseCoverletCollectorOrMsBuild"/>.</summary>
[DiagnosticAnalyzer(LanguageNames.CSharp, LanguageNames.VisualBasic)]
public sealed class UseCoverletCollectorOrMsBuild()
    : MsBuildProjectFileAnalyzer(Rule.UseCoverletCollectorOrMsBuild)
{
    /// <inheritdoc />
    public override IReadOnlyCollection<ProjectFileType> ApplicableTo => ProjectFileTypes.ProjectFile;

    /// <inheritdoc />
    protected override void Register(ProjectFileAnalysisContext<MsBuildProject> context)
    {
        var collector = context.File
            .Walk()
            .OfType<PackageReference>()
            .LastOrDefault(r => r.Include == NuGet.Packages.coverlet_collector.Name);
        var msBuild = context.File
            .Walk()
            .OfType<PackageReference>()
            .LastOrDefault(r => r.Include == NuGet.Packages.coverlet_msbuild.Name);
        
        if (collector is { } && msBuild is { })
        {
            if (collector.Project == context.File)
            {
                context.ReportDiagnostic(Descriptor, collector);
            }
            else if (msBuild.Project == context.File)
            {
                context.ReportDiagnostic(Descriptor, msBuild);
            }
            else
            {
                context.ReportDiagnostic(Descriptor, context.File);
            }
        }
    }
}
