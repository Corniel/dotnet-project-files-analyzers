﻿namespace DotNetProjectFile.Analyzers.MsBuild;

[DiagnosticAnalyzer(LanguageNames.CSharp, LanguageNames.VisualBasic)]
public sealed class PackageReferencesShouldBeStable() : MsBuildProjectFileAnalyzer(Rule.PackageReferencesShouldBeStable)
{
    protected override void Register(ProjectFileAnalysisContext context)
    {
        foreach (var package in context.Project.ItemGroups
            .SelectMany(i => i.PackageReferences)
            .Where(IsUnstable))
        {
            context.ReportDiagnostic(Descriptor, package, package.IncludeOrUpdate, package.VersionOrVersionOverride);
        }

        foreach (var package in context.Project.ItemGroups
            .SelectMany(i => i.PackageVersions)
            .Where(IsUnstable))
        {
            context.ReportDiagnostic(Descriptor, package, package.IncludeOrUpdate, package.Version);
        }
    }

    private static bool IsUnstable(PackageReference package)
        => package.VersionOrVersionOverride is { Length: > 0 } version
        && version.Contains('-')
        && !"ALL".Equals(package.PrivateAssets, StringComparison.OrdinalIgnoreCase);

    private static bool IsUnstable(PackageVersion package)
        => package.Version is { Length: > 0 } version
        && version.Contains('-');
}
