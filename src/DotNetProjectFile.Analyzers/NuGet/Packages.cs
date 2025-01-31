namespace DotNetProjectFile.NuGet;

public sealed class Packages : IReadOnlyCollection<Package>
{
    public static readonly BuildExtension coverlet_collector = new("coverlet.collector");
    public static readonly BuildExtension coverlet_msbuild = new("coverlet.msbuild");
    public static readonly BuildExtension Microsoft_NET_Test_Sdk = new("Microsoft.NET.Test.Sdk");
    public static readonly BuildExtension Microsoft_Sbom_Targets = new("Microsoft.Sbom.Targets");
    public static readonly BuildExtension TUnit = new("TUnit");

    public static readonly Packages All = new(

        // Generic analyzers
        new Analyzer("AspNetCoreAnalyzers"),
        new Analyzer("AsyncFixer"),
        new Analyzer("AsyncUsageAnalyzers"),
        new Analyzer("CSharpGuidelinesAnalyzer"),
        new Analyzer("ConfigureAwaitChecker.Analyzer"),
        new Analyzer("DotNetAnalyzers.BannedApiAnalyzer"),
        new Analyzer("DotNetAnalyzers.DocumentationAnalyzers"),
        new Analyzer("DotNetAnalyzers.DocumentationAnalyzers.Unstable"),
        new Analyzer("Gu.Analyzers"),
        new Analyzer("DotNetProjectFile.Analyzers"),
        new Analyzer("IDisposableAnalyzers"),
        new Analyzer("Lombiq.Analyzers"),
        new Analyzer("Lombiq.Analyzers.NetFx"),
        new Analyzer("Lombiq.Analyzers.Orchard1"),
        new Analyzer("Lombiq.Analyzers.OrchardCore"),
        new Analyzer("Lombiq.Analyzers.PowerShell"),
        new Analyzer("Lombiq.Analyzers.VisualStudioExtension"),
        new Analyzer("Meziantou.Analyzer"),
        new Analyzer("Microsoft.CodeAnalysis.BannedApiAnalyzer"),
        new Analyzer("Microsoft.CodeAnalysis.BinSkim"),
        new Analyzer("Microsoft.CodeAnalysis.CSharp.CodeStyle", language: LanguageNames.CSharp),
        new Analyzer("Microsoft.CodeAnalysis.FxCopAnalyzers"),
        new Analyzer("Microsoft.CodeAnalysis.NetAnalyzers"),
        new Analyzer("Microsoft.CodeAnalysis.PerformanceSensitiveAnalyzers"),
        new Analyzer("Microsoft.CodeAnalysis.PublicApiAnalyzers"),
        new Analyzer("Microsoft.CodeAnalysis.VersionCheckAnalyzer"),
        new Analyzer("Microsoft.CodeAnalysis.VisualBasic.CodeStyle", language: LanguageNames.VisualBasic),
        new Analyzer("PropertyChangedAnalyzers"),
        new Analyzer("DotNetAnalyzers.PublicApiAnalyzer"),
        new Analyzer("DotNetAnalyzers.PublicApiAnalyzer.Unstable"),
        new Analyzer("Qowaiv.Analyzers.CSharp"),
        new Analyzer("ReflectionAnalyzers"),
        new Analyzer("Roslynator.CodeAnalysis.Analyzers"),
        new Analyzer("SecurityCodeScan"),
        new Analyzer("SecurityCodeScan.VS2017"),
        new Analyzer("SecurityCodeScan.VS2019"),
        new Analyzer("SonarAnalyzer.CSharp", language: LanguageNames.CSharp),
        new Analyzer("SonarAnalyzer.VisualBasic", language: LanguageNames.VisualBasic),
        new Analyzer("StyleCop.Analyzers"),
        new Analyzer("StyleCop.Analyzers.Unstable"),
        new Analyzer("Text.Analyzers"),
        new Analyzer("TSqlAnalyzer"),
        new Analyzer("WpfAnalyzers"),

        // Specific analyzers
        new Analyzer("Ardalis.ApiEndpoints.CodeAnalyzers", "Ardalis.ApiEndpoints"),
        new Analyzer("FakeItEasy.Analyzer.CSharp", "FakeItEasy", LanguageNames.CSharp),
        new Analyzer("FakeItEasy.Analyzer.VisualBasic", "FakeItEasy", LanguageNames.VisualBasic),
        new Analyzer("FluentAssertions.Analyzers", "FluentAssertions"),
        new Analyzer("Libplanet.Analyzers", "Libplanet"),
        new Analyzer("Lucene.Net.Analysis.Common", "Lucene.Net"),
        new Analyzer("MassTransit.Analyzers", "MassTransit"),
        new Analyzer("MessagePackAnalyzer", "MessagePack"),
        new Analyzer("MessagePipe.Analyzer", "MessagePipe"),
        new Analyzer("Microsoft.AspNetCore.Components.Analyzers", "Microsoft.AspNetCore"),
        new Analyzer("Microsoft.Azure.Functions.Analyzers", "Microsoft.Azure.Functions"),
        new Analyzer("Microsoft.CodeAnalysis.Analyzers", "Microsoft.CodeAnalysis"),
        new Analyzer("Microsoft.EntityFrameworkCore.Analyzers", "Microsoft.EntityFrameworkCore"),
        new Analyzer("Microsoft.ServiceHub.Analyzers", "Microsoft.ServiceHub"),
        new Analyzer("MongoDB.Analyzer", "MongoDB"),
        new Analyzer("Moq.Analyzers", "Moq"),
        new Analyzer("NSubstitute.Analyzers.CSharp", "NSubstitute", LanguageNames.CSharp),
        new Analyzer("NSubstitute.Analyzers.VisualBasic", "NSubstitute", LanguageNames.VisualBasic),
        new Analyzer("NUnit.Analyzers", "NUnit"),
        new Analyzer("RuntimeContracts.Analyzer", "RuntimeContracts"),
        new Analyzer("SerilogAnalyzer", "Serilog"),
        new Analyzer("xunit.analyzers", "xunit"),
        new Analyzer("ZeroFormatter.Analyzer", "ZeroFormatter"),

        // Source generators
        new SourceGenerator("Mediator.SourceGenerator"),
        new SourceGenerator("Meziantou.Polyfill"),
        new SourceGenerator("Microsoft.CodeAnalysis.ResxSourceGenerator"),
        new SourceGenerator("OneOf.SourceGenerator"),
        new SourceGenerator("Polyfill"),
        new SourceGenerator("PolySharp"),
        new SourceGenerator("Qowaiv.Diagnostics.Contracts"),
        new SourceGenerator("PropertyChanged.SourceGenerator"),
        new SourceGenerator("ReactiveMarbles.ObservableEvents.SourceGenerator"),
        new SourceGenerator("Realm.SourceGenerator"),
        new SourceGenerator("Refitter.SourceGenerator"),
        new SourceGenerator("RestEase.SourceGenerator"),
        new SourceGenerator("Riok.Mapperly"),
        new SourceGenerator("Svg.SourceGenerator.Skia"),
        new SourceGenerator("T4.SourceGenerator"),
        new SourceGenerator("Thinktecture.Runtime.Extensions.SourceGenerator"),
        new SourceGenerator("Tmds.DBus.SourceGenerator"),
        new SourceGenerator("UnitGenerator"),
        new SourceGenerator("Vogen"),

        // Build extensions
        coverlet_collector,
        coverlet_msbuild,
        new BuildExtension("DotNetProjectFile.Analyzers.Sdk"),
        new BuildExtension("Grpc.Tools"),
        new BuildExtension("JetBrains.Annotations"),
        new BuildExtension("JetBrains.ExternalAnnotations"),
        new BuildExtension("Microsoft.DotNet.ApiCompat"),
        Microsoft_NET_Test_Sdk,
        Microsoft_Sbom_Targets,
        new BuildExtension("Microsoft.SourceLink.Gitea"),
        new BuildExtension("Microsoft.SourceLink.GitLab"),
        new BuildExtension("Microsoft.SourceLink.GitHub"),
        new BuildExtension("Microsoft.SourceLink.GitWeb"),
        new BuildExtension("Microsoft.SourceLink.AzureRepos.Git"),
        new BuildExtension("Microsoft.SourceLink.Bitbucket.Git"),
        new BuildExtension("Microsoft.SourceLink.AzureDevOpsServer.Git"),
        new BuildExtension("MinVer"),
        new BuildExtension("NUnit3TestAdapter"),
        new BuildExtension("Nullable"),
        TUnit,
        new BuildExtension("xunit.runner.visualstudio"));

    private readonly Dictionary<string, Package> items;

    private Packages(params Package[] packages)
        => items = packages.ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

    public int Count => items.Count;

    public Package? TryGet(string? name)
        => name is { Length: > 0 } && items.TryGetValue(name, out var package)
            ? package
            : null;

    public IEnumerator<Package> GetEnumerator() => items.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
