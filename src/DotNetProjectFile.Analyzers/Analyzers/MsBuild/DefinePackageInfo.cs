﻿using System.Collections.Immutable;

namespace DotNetProjectFile.Analyzers.MsBuild;

[DiagnosticAnalyzer(LanguageNames.CSharp, LanguageNames.VisualBasic)]
public sealed class DefinePackageInfo() : MsBuildProjectFileAnalyzer(
    Rule.DefineVersion,
    Rule.DefineDescription,
    Rule.DefineAuthors,
    Rule.DefineTags,
    Rule.DefineRepositoryUrl,
    Rule.DefineUrl,
    Rule.DefineCopyright,
    Rule.DefineReleaseNotes,
    Rule.DefineReadmeFile,
    Rule.DefineLicense,
    Rule.DefineIcon,
    Rule.DefineIconUrl,
    Rule.DefinePackageId)
{
    protected override IReadOnlyCollection<ProjectFileType> ApplicableTo => ProjectFileTypes.ProjectFile;

    protected override void Register(ProjectFileAnalysisContext context)
    {
        if (!context.Project.IsPackable() || context.Project.IsTestProject()) return;

        var available = context.Project.Walk().Select(n => n.GetType()).ToImmutableHashSet();

        Analyze(context, available, Rule.DefineVersion, typeof(DotNetProjectFile.MsBuild.Version));
        Analyze(context, available, Rule.DefineDescription, typeof(Description), typeof(PackageDescription));
        Analyze(context, available, Rule.DefineAuthors, typeof(Authors));
        Analyze(context, available, Rule.DefineTags, typeof(PackageTags));
        Analyze(context, available, Rule.DefineRepositoryUrl, typeof(RepositoryUrl));
        Analyze(context, available, Rule.DefineUrl, typeof(PackageProjectUrl));
        Analyze(context, available, Rule.DefineCopyright, typeof(Copyright));
        Analyze(context, available, Rule.DefineReleaseNotes, typeof(PackageReleaseNotes));
        Analyze(context, available, Rule.DefineReadmeFile, typeof(PackageReadmeFile));
        Analyze(context, available, Rule.DefineIcon, typeof(PackageIcon));
        Analyze(context, available, Rule.DefineIconUrl, typeof(PackageIconUrl));
        Analyze(context, available, Rule.DefinePackageId, typeof(PackageId));
        Analyze(context, available, Rule.DefineLicense, typeof(PackageLicenseFile), typeof(PackageLicenseExpression));
    }

    private static void Analyze(
        ProjectFileAnalysisContext context,
        ImmutableHashSet<Type> available,
        DiagnosticDescriptor descriptor,
        params Type[] required)
    {
        if (!required.Exists(available.Contains))
        {
            context.ReportDiagnostic(descriptor, context.Project);
        }
    }
}
