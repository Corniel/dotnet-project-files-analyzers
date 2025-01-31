#pragma warning disable SA1118 // Parameter should not span multiple lines: readability for descriptions.

namespace DotNetProjectFile;

public static partial class Rule
{
    public static DiagnosticDescriptor ProjectFileCouldNotBeLocated => New(
        id: 0001,
        title: "MS Build project file could not be located",
        message: "The project file '{0}' could not be located.",
        description: "In order to make these rules work, the project file should be located.",
        tags: ["Configuration"],
        category: Category.Configuration);

    public static DiagnosticDescriptor UpdateLegacyProjects => New(
        id: 0002,
        title: "Upgrade legacy MS Build project files",
        message: "Upgrade legacy MS Build project file.",
        description: "MS Build legacy projects are not supported.",
        tags: ["project file", "legacy", "obsolete"],
        category: Category.Obsolete);

    public static DiagnosticDescriptor DefineUsingsExplicit => New(
        id: 0003,
        title: "Define usings explicit",
        message: "Define usings explicit.",
        description:
            "The included namespaces should be clear. To reduce the statements " +
            "per file, consider global using statements.",
        tags: ["Configuration", "usings", "global"],
        category: Category.Clarity);

    public static DiagnosticDescriptor RunNuGetSecurityAudit => New(
        id: 0004,
        title: "Run NuGet security audits automatically",
        message: "Run NuGet security audits automatically.",
        description:
            "To reduce security issues, NuGets security audit should be run on " +
            "every build automatically.",
        tags: ["security", "NuGet", "vulnerability"],
        category: Category.Security);

    public static DiagnosticDescriptor DefinePackageReferenceAssetsAsAttributes => New(
        id: 0005,
        title: "Define package reference assets as attributes",
        message: "Define package reference assets of '{0}' as attributes.",
        description:
            "To reduce security issues, NuGets security audit should be run on " +
            "every build automatically.",
        tags: ["security", "NuGet", "vulnerability"],
        category: Category.Clarity);

    public static DiagnosticDescriptor AddAdditionalFile => New(
        id: 0006,
        title: "Add additional files to improve static code analysis",
        message: "Add '{0}' to the additional files.",
        description:
            "By adding additional non-compiling files, those files become " +
            "available in the analyzer context too.",
        tags: ["code analysis"],
        category: Category.CodeQuality);

    public static DiagnosticDescriptor RemoveEmptyNodes => New(
        id: 0007,
        title: "Remove empty nodes",
        message: "Remove empty {0} node.",
        description: "Empty nodes only add noise, as they contain no information.",
        tags: ["noise"],
        category: Category.Noise);

    public static DiagnosticDescriptor RemoveFolderNodes => New(
        id: 0008,
        title: "Remove folder nodes",
        message: "Remove folder node '{0}'.",
        description:
            "Folders nodes only add noise. They are leftovers of directories " +
            "created in the IDE, without adding an actual file to it.",
        tags: ["noise"],
        category: Category.Noise);

    public static DiagnosticDescriptor DefineSingleTargetFramework => New(
        id: 0009,
        title: "Use the <TargetFramework> node for a single target framework",
        message: "Use the <TargetFramework> node instead.",
        description:
            "To prevent confusion, only use the <TargetFrameworks> node when " +
            "there are multiple target frameworks.",
        tags: ["target framework", "confusion"],
        category: Category.Clarity);

    public static DiagnosticDescriptor DefineOutputType => New(
        id: 0010,
        title: "Define the project output type explicitly",
        message: "Define the <OutputType> node explicitly.",
        description:
            "To prevent confusion, explicitly define the OutputType " +
            "as 'Library', 'Exe', 'WinExe' or 'Module'.",
        tags: ["Configuration", "confusion"],
        category: Category.Clarity);

    public static DiagnosticDescriptor DefinePropertiesOnce => New(
        id: 0011,
        title: "Define properties once",
        message: "Property <{0}> has been already defined.",
        description: "MS Build will only select one value of a property.",
        tags: ["Configuration", "confusion"],
        category: Category.Clarity);

    public static DiagnosticDescriptor ReassignPropertiesWithDifferentValue => New(
        id: 0012,
        title: "Reassign properties with a different value",
        message: "Property <{0}> has been previously defined with the same value.",
        description: "Reassigning a property with the same value is considered noise.",
        tags: ["Configuration", "confusion"],
        category: Category.Clarity);

    public static DiagnosticDescriptor IncludePackageReferencesOnce => New(
        id: 0013,
        title: "Include package references only once",
        message: "Package '{0}' is already referenced.",
        description: "Including package references twice is considered noise.",
        tags: ["Configuration", "confusion"],
        category: Category.Clarity);

    public static DiagnosticDescriptor IncludeProjectReferencesOnce => New(
        id: 0014,
        title: "Include project references only once",
        message: "Project '{0}' is already referenced.",
        description: "Including project references twice is considered noise.",
        tags: ["Configuration", "confusion"],
        category: Category.Clarity);

    public static DiagnosticDescriptor OrderPackageReferencesAlphabetically => New(
        id: 0015,
        title: "Order package references alphabetically",
        message: "Package '{0}' is not ordered alphabetically and should appear before '{1}'.",
        description: "Not ordering package references alphabetically is considered noise.",
        tags: ["Configuration", "confusion"],
        category: Category.Clarity);

    public static DiagnosticDescriptor OrderProjectReferencesAlphabetically => New(
        id: 0016,
        title: "Order project references alphabetically",
        message: "Project '{0}' is not ordered alphabetically and should appear before '{1}'.",
        description: "Not ordering project references alphabetically is considered noise.",
        tags: ["Configuration", "confusion"],
        category: Category.Clarity);

    public static DiagnosticDescriptor StaticAliasUsingNotSupported => New(
        id: 0017,
        title: "Can't create alias for static using directive",
        message: "Using directive for '{0}' can not be both an alias and static.",
        description: "Using directives can not be both static and an alias.",
        tags: ["Bug", "Code Generation"],
        category: Category.Bug,
        severity: DiagnosticSeverity.Error);

    public static DiagnosticDescriptor OrderUsingDirectivesByType => New(
        id: 0018,
        title: "Order using directives by type",
        message: "{0} directive for '{1}' should appear before {2} directive for '{3}'.",
        description: "Not ordering using directives by type is considered noise.",
        tags: ["Configuration", "confusion"],
        category: Category.Clarity);

    public static DiagnosticDescriptor OrderUsingDirectivesAlphabetically => New(
        id: 0019,
        title: "Order using directives alphabetically",
        message: "{0} directive '{1}' is not ordered alphabetically and should appear before '{2}'.",
        description: "Not ordering using directives alphabetically is considered noise.",
        tags: ["Configuration", "confusion"],
        category: Category.Clarity);

    public static DiagnosticDescriptor ItemGroupShouldBeUniform => New(
        id: 0020,
        title: "Item group should only contain nodes of a single type",
        message: "<ItemGroup> should only contain nodes of a single type.",
        description: "Mixing nodes of different types in a single <ItemGroup> is considered noise.",
        tags: ["Configuration", "confusion"],
        category: Category.Clarity);

    public static DiagnosticDescriptor BuildActionsShouldHaveSingleTask => New(
        id: 0021,
        title: "Build actions should have a single task",
        message: "The <{0}> defines multiple tasks.",
        description:
            "For readability, a build action should define only one task.",
        tags: ["Readability"],
        category: Category.Clarity);

    public static DiagnosticDescriptor BuildActionIncludeShouldExist => New(
        id: 0022,
        title: "Build action includes should exist",
        message: "The Include '{0}' of <{1}> does not {2}.",
        description:
            "Build action include statements that do not include any file, are most " +
            "likely a left over, or a bug.",
        tags: [],
        category: Category.Noise);

    public static DiagnosticDescriptor UseForwardSlashesInPaths => New(
        id: 0023,
        title: "Use forward slashes in paths",
        message: "<{0} {1}> contains backward slashes.",
        description:
            "The use of forward slashes is preferred as they work both for UNIX and" +
            "Windows. This is not true for backward slashes.",
        tags: ["UNIX", "Windows", "IO"],
        category: Category.Reliability);

    public static DiagnosticDescriptor OrderPackageVersionsAlphabetically => New(
        id: 0024,
        title: "Order package versions alphabetically",
        message: "Package version for '{0}' is not ordered alphabetically and should appear before '{1}'.",
        description: "Not ordering package versions alphabetically is considered noise.",
        tags: ["Configuration", "confusion"],
        category: Category.Clarity);

    public static DiagnosticDescriptor MigrateFromRulesetToEditorConfigFile => New(
        id: 0025,
        title: "Migrate from ruleset file to .editorconfig file",
        message: "Migrate ruleset '{0}' to an .editorconfig file.",
        description:
            "XML based ruleset files are defacto deprecated. Ruleset can be " +
            "automatically converted using Microsoft.CodeAnalysis.RulesetToEditorconfigConverter.",
        tags: ["Configuration", "readability"],
        category: Category.Obsolete);

    public static DiagnosticDescriptor RemoveIncludeAssetsWhenRedundant => New(
        id: 0026,
        title: "Remove IncludeAssets when redundant",
        message: "Remove {0} as it is redundant when all assets are private.",
        description: "When all assets are private, none of them will be included.",
        tags: ["redundant"],
        category: Category.Noise);

    public static DiagnosticDescriptor OverrideTargetFrameworksWithTargetFrameworks => New(
        id: 0027,
        title: "Override <TargetFrameworks> with <TargetFrameworks>",
        message: "This <TargetFramework> will be ignored due to the earlier use of <TargetFrameworks>.",
        description:
            "The <TargetFrameworks> node precedes <TargetFramework>. Hence, " +
            "once the first has been used, the use of the latter has no effect.",
        tags: ["TFM"],
        category: Category.Bug);

    public static DiagnosticDescriptor DefineConditionsOnLevel1 => New(
        id: 0028,
        title: "Define conditions on level 1",
        message: "Move the condition to the parent <{0}>.",
        description:
            "Both to keep lines short, and to group configuration that is bound " +
            "to the some constraints, it best to define condtions on level 1.",
        tags: ["conditions"],
        category: Category.Reliability);

    public static DiagnosticDescriptor UseInCSharpContextOnly => New(
        id: 0029,
        title: "Use C# specific properties only when applicable",
        message: "The property <{0}> is only applicable when using C# and can therefor be removed.",
        description:
            "Properties only applicable to C# are noise when none of " +
            "the involved targets is a C# target.",
        tags: ["noise"],
        category: Category.Noise);

    public static DiagnosticDescriptor UseInVBContextOnly => New(
        id: 0030,
        title: "Use VB.NET specific properties only when applicable",
        message: "The property <{0}> is only applicable when using VB.NET and can therefor be removed.",
        description:
            "Properties only applicable to VB.NET are noise when none of " +
            "the involved targets is a VB.NET target.",
        tags: ["noise"],
        category: Category.Noise);

    public static DiagnosticDescriptor AdoptPreferredCasing => New(
        id: 0031,
        title: "Adopt preferred casing of nodes",
        message: "The node <{0}> has a different casing than the preferred one <{1}>.",
        description:
            "MS Build is (mostly) case insensitive. To prevent issues, however, " +
            "it is preferred to use the same casing consistently.",
        tags: ["clarity", "casing"],
        category: Category.Clarity);

    public static DiagnosticDescriptor MigrateAwayFromBinaryFormatter => New(
        id: 0032,
        title: "Migrate away from BinaryFormatter",
        message: "Migrate away from BinaryFormatter and do not enable <{0}>.",
        description:
            "Microsoft strongly recommends that you investigate options to stop " +
            "using BinaryFormatter due to the associated security risks.",
        tags: ["Bug", "Security", "Vulnerability"],
        category: Category.Security);

    public static DiagnosticDescriptor ProjectReferenceIncludeShouldExist => New(
        id: 0033,
        title: "Project reference includes should exist",
        message: "The Include '{0}' of <{1}> does not exist.",
        description:
            "Project reference include statements that do not include any file, are most likely bugs.",
        tags: ["dependencies", "dependency"],
        category: Category.Bug);

    public static DiagnosticDescriptor DefineIsPackable => New(
        id: 0200,
        title: "Define the project packability explicitly",
        message: "Define the <IsPackable> node explicitly.",
        description:
            "To prevent confusion, explicitly define the " +
            "<IsPackable> node.",
        tags: ["Configuration", "confusion"],
        category: Category.Clarity);

    public static DiagnosticDescriptor DefineVersion => New(
        id: 0201,
        title: "Define the project version explicitly",
        message: "Define the <Version> or <VersionPrefix> node explicitly or define the <IsPackable> node with value 'false'.",
        description:
            "To ensure the creation of well-formed packages, " +
            "explicitly define the <Version> or <VersionPrefix> node or " +
            "disable package generation by defining the " +
            "<IsPackable> node with value 'false'.",
        tags: ["Configuration", "NuGet", "package"],
        category: Category.Reliability);

    public static DiagnosticDescriptor DefineDescription => New(
        id: 0202,
        title: "Define the project description explicitly",
        message: "Define the <Description> or <PackageDescription> node explicitly or define the <IsPackable> node with value 'false'.",
        description:
            "To ensure the creation of well-formed packages, explicitly define " +
            "the <Description> or <PackageDescription> node or disable package " +
            "generation by defining the <IsPackable> node with value 'false'.",
        tags: ["Configuration", "NuGet", "package"],
        category: Category.Configuration);

    public static DiagnosticDescriptor DefineAuthors => New(
        id: 0203,
        title: "Define the project authors explicitly",
        message: "Define the <Authors> node explicitly or define the <IsPackable> node with value 'false'.",
        description:
            "To ensure the creation of well-formed packages, " +
            "explicitly define the <Authors> node or " +
            "disable package generation by defining the " +
            "<IsPackable> node with value 'false'.",
        tags: ["Configuration", "NuGet", "package"],
        category: Category.Configuration);

    public static DiagnosticDescriptor DefineTags => New(
        id: 0204,
        title: "Define the project tags explicitly",
        message: "Define the <PackageTags> node explicitly or define the <IsPackable> node with value 'false'.",
        description:
            "To ensure the creation of well-formed packages, " +
            "explicitly define the <PackageTags> node or " +
            "disable package generation by defining the " +
            "<IsPackable> node with value 'false'.",
        tags: ["Configuration", "NuGet", "package"],
        category: Category.Configuration);

    public static DiagnosticDescriptor DefineRepositoryUrl => New(
        id: 0205,
        title: "Define the project repository URL explicitly",
        message: "Define the <RepositoryUrl> node explicitly or define the <IsPackable> node with value 'false'.",
        description:
            "To ensure the creation of well-formed packages, " +
            "explicitly define the <RepositoryUrl> node or " +
            "disable package generation by defining the " +
            "<IsPackable> node with value 'false'.",
        tags: ["Configuration", "NuGet", "package"],
        category: Category.Configuration);

    public static DiagnosticDescriptor DefineUrl => New(
        id: 0206,
        title: "Define the project URL explicitly",
        message: "Define the <PackageProjectUrl> node explicitly or define the <IsPackable> node with value 'false'.",
        description:
            "To ensure the creation of well-formed packages, " +
            "explicitly define the <PackageProjectUrl> node or " +
            "disable package generation by defining the " +
            "<IsPackable> node with value 'false'.",
        tags: ["Configuration", "NuGet", "package"],
        category: Category.Configuration);

    public static DiagnosticDescriptor DefineCopyright => New(
        id: 0207,
        title: "Define the project copyright explicitly",
        message: "Define the <Copyright> node explicitly or define the <IsPackable> node with value 'false'.",
        description:
            "To ensure the creation of well-formed packages, " +
            "explicitly define the <Copyright> node or " +
            "disable package generation by defining the " +
            "<IsPackable> node with value 'false'.",
        tags: ["Configuration", "NuGet", "package"],
        category: Category.Configuration);

    public static DiagnosticDescriptor DefineReleaseNotes => New(
        id: 0208,
        title: "Define the project release notes explicitly",
        message: "Define the <PackageReleaseNotes> node explicitly or define the <IsPackable> node with value 'false'.",
        description:
            "To ensure the creation of well-formed packages, " +
            "explicitly define the <PackageReleaseNotes> node or " +
            "disable package generation by defining the " +
            "<IsPackable> node with value 'false'.",
        tags: ["Configuration", "NuGet", "package"],
        category: Category.Configuration);

    public static DiagnosticDescriptor DefineReadmeFile => New(
        id: 0209,
        title: "Define the project readme file explicitly",
        message: "Define the <PackageReadmeFile> node explicitly or define the <IsPackable> node with value 'false'.",
        description:
            "To ensure the creation of well-formed packages, " +
            "explicitly define the <PackageReadmeFile> node or " +
            "disable package generation by defining the " +
            "<IsPackable> node with value 'false'.",
        tags: ["Configuration", "NuGet", "package"],
        category: Category.Configuration);

    public static DiagnosticDescriptor DefineLicense => New(
        id: 0210,
        title: "Define the project license expression explicitly",
        message: "Define the <PackageLicenseExpression> or <PackageLicenseFile> node explicitly or define the <IsPackable> node with value 'false'.",
        description:
            "To ensure the creation of well-formed packages " +
            "and for maximum compatibility with external tools, " +
            "explicitly define the <PackageLicenseExpression> " +
            "or <PackageLicenseFile>` node. Alternatively " +
            "disable package generation by defining the " +
            "<IsPackable> node with value 'false'.",
        tags: ["Configuration", "NuGet", "package"],
        category: Category.Configuration);

    public static DiagnosticDescriptor AvoidLicenseUrl => New(
        id: 0211,
        title: "Avoid using deprecated license definition",
        message: "Replace deprecated <PackageLicenseUrl> with <PackageLicenseExpression> or <PackageLicenseFile> node.",
        description:
            "The <PackageLicenseUrl> node has been deprecated " +
            "and should be replaced by either the " +
            "<PackageLicenseExpression> or the <PackageLicenseFile> node.",
        tags: ["Configuration", "NuGet", "package"],
        category: Category.Obsolete);

    public static DiagnosticDescriptor DefineIcon => New(
        id: 0212,
        title: "Define the project icon file explicitly",
        message: "Define the <PackageIcon> node explicitly or define the <IsPackable> node with value 'false'.",
        description:
            "To ensure the creation of well-formed packages " +
            "and for maximum compatibility with external tools, " +
            "explicitly define the <PackageIcon> node or " +
            "disable package generation by defining the " +
            "<IsPackable> node with value 'false'.",
        tags: ["Configuration", "NuGet", "package"],
        category: Category.Configuration);

    public static DiagnosticDescriptor DefineIconUrl => New(
        id: 0213,
        title: "Define the project icon URL explicitly",
        message: "Define the <PackageIconUrl> node explicitly or define the <IsPackable> node with value 'false'.",
        description:
            "To ensure the creation of well-formed packages " +
            "and for maximum compatibility with external tools, " +
            "explicitly define the deprecated <PackageIconUrl> node or " +
            "disable package generation by defining the " +
            "<IsPackable> node with value 'false'.",
        tags: ["Configuration", "NuGet", "package"],
        category: Category.Configuration);

    public static DiagnosticDescriptor DefinePackageId => New(
        id: 0214,
        title: "Define the NuGet project ID explicitly",
        message: "Define the <PackageId> node explicitly or define the <IsPackable> node with value 'false'.",
        description:
            "To ensure the creation of well-formed packages, " +
            "explicitly define the <PackageId> node or " +
            "disable package generation by defining the " +
            "<IsPackable> node with value 'false'.",
        tags: ["Configuration", "NuGet", "package"],
        category: Category.Configuration);

    public static DiagnosticDescriptor ProvideCompliantPackageIcon => New(
        id: 0215,
        title: "Provide a compliant NuGet package icon",
        message: "The package icon '{0}' {1}.",
        description:
            "To ensure the creation of well-formed packages, use an image that is " +
            "128x128 and has a transparent background(PNG) for the best viewing results.",
        tags: ["Configuration", "NuGet", "package", "image", "PNG"],
        category: Category.Configuration);

    public static DiagnosticDescriptor DefineProductName => New(
        id: 0216,
        title: "Define the project name explicitly",
        message: "Define the <ProductName> node explicitly or define the <IsPackable> node with value 'false'.",
        description:
            "To ensure the creation of well-formed packages, " +
            "explicitly define the <ProductName> node or " +
            "disable package generation by defining the " +
            "<IsPackable> node with value 'false'.",
        tags: ["Configuration", "NuGet", "package"],
        category: Category.Configuration);

    public static DiagnosticDescriptor EnablePackageValidation => New(
        id: 0240,
        title: "Enable package validation",
        message: "Define the <EnablePackageValidation> node with value 'true' or define the <IsPackable> node with value 'false' or define the <DevelopmentDependency> node with value 'false'.",
        description:
            "To ensure the (backwards) compatibility " +
            "of the API surface of your package, it is advised " +
            "to enable package validation by defining the " +
            "<EnablePackageValidation> node with value 'true'.",
        tags: ["Configuration", "package", "compatibility"],
        category: Category.Reliability,
        severity: DiagnosticSeverity.Warning,
        isEnabled: true);

    public static DiagnosticDescriptor DefinePackageValidationBaselineVersion => New(
        id: 0241,
        title: "Enable package baseline validation",
        message: "Define the <PackageValidationBaselineVersion> node with a previously released stable version.",
        description:
            "To ensure the backwards compatibility " +
            "of the API surface of your package, it is advised " +
            "to enable package baseline validation by defining the " +
            "<PackageValidationBaselineVersion> node with the version " +
            "of the previous stable release.",
        tags: ["Configuration", "package", "compatibility"],
        category: Category.Reliability,
        severity: DiagnosticSeverity.Warning,
        isEnabled: true);

    public static DiagnosticDescriptor GeneratePackageOnBuildConditionally => New(
        id: 0242,
        title: "Generate NuGet packages conditionally",
        message: "Add a condition to <GeneratePackageOnBuild>.",
        description:
            "To ensure that packages are not interdependently shipped in DEBUG " +
            "(or other) mode, a conditional statement should be defined.",
        tags: ["Configuration", "package", "compatibility"],
        category: Category.Bug,
        severity: DiagnosticSeverity.Warning,
        isEnabled: true);

    public static DiagnosticDescriptor GenerateSbom => New(
        id: 0243,
        title: "Generate software bill of materials",
        message: "{0} or define the <IsPackable> node with value 'false'.",
        description:
           "To be compliant with USA legislation, a software bill of materials " +
           "should be included with a shipped package.",
        tags: ["compliance", "package", "legislation"],
        category: Category.CodeQuality,
        severity: DiagnosticSeverity.Warning,
        isEnabled: true);

    public static DiagnosticDescriptor GenerateDocumentationFile => New(
        id: 0244,
        title: "Generate documentation file",
        message: "Define the <GenerateDocumentationFile> node with value 'true' or define the <DocumentationFile> node with a valid file path or define the <IsPackable> node with value 'false'.",
        description:
           "In order for code documentation to be visible for package consumers " +
           "it is important that the documentation is generated.",
        tags: ["Configuration", "package"],
        category: Category.Clarity,
        severity: DiagnosticSeverity.Warning,
        isEnabled: true);

    public static DiagnosticDescriptor DontMixVersionAndVersionPrefixOrVersionSuffix => New(
        id: 0245,
        title: "Don't mix Version and VersionPrefix/VersionSuffix",
        message: "Remove the <Version> node or remove the {0}.",
        description:
            "Version node overrides VersionPrefix and VersionSuffix nodes " +
            ", therefore you should either remove the Version node " +
            "(if you want to use the VersionPrefix and VersionSuffix nodes) or " +
            "you should remove the VersionPrefix and VersionSuffix nodes " +
            "(if you want to use Version node).",
        tags: ["Configuration", "NuGet", "package"],
        category: Category.Configuration,
        severity: DiagnosticSeverity.Warning,
        isEnabled: true);

    public static DiagnosticDescriptor DefineVersionPrefixIfVersionSuffixIsDefined => New(
        id: 0246,
        title: "Define VersionPrefix if VersionSuffix is defined",
        message: "Define the <VersionPrefix> node or remove the <VersionSuffix> node.",
        description:
            "VersionSuffix indicates the desire to use the VersionPrefix and VersionSuffix system " +
            ", but when only defining the VersionSuffix node, the default value of VersionPrefix (1.0.0) " +
            "is used, which is most likely an error.",
        tags: ["Configuration", "NuGet", "package"],
        category: Category.Configuration,
        severity: DiagnosticSeverity.Warning,
        isEnabled: true);

    public static DiagnosticDescriptor EnableStrictModeForPackageBaselineValidation => New(
        id: 0247,
        title: "Enable strict mode for package baseline validation",
        message: "Define the <EnableStrictModeForBaselineValidation> node with value 'true' or remove the <PackageValidationBaselineVersion> node or remove the <EnablePackageValidation> node with value 'true'.",
        description:
            "When ensuring backwards compatibility of the API surface " +
            "of your package, it is advised to do this in strict mode. " +
            "This helps preventing any unintentional API changes.",
        tags: ["Configuration", "package", "compatibility"],
        category: Category.Reliability,
        severity: DiagnosticSeverity.Warning,
        isEnabled: true);

    public static DiagnosticDescriptor EnableStrictModeForPackageRuntimeCompatibilityValidation => New(
        id: 0248,
        title: "Enable strict mode for package runtime compatibility validation",
        message: "Define the <EnableStrictModeForCompatibleTfms> node with value 'true' or remove the <EnableStrictModeForCompatibleTfms> node with value 'false' or remove the <EnablePackageValidation> node with value 'true'.",
        description:
            "When building your package for multiple runtimes it " +
            "is advised to enable the strict mode of the runtime " +
            "compatibility validation.",
        tags: ["Configuration", "package", "compatibility"],
        category: Category.Reliability,
        severity: DiagnosticSeverity.Warning,
        isEnabled: true);

    public static DiagnosticDescriptor EnableStrictModeForPackageFrameworkCompatibilityValidation => New(
        id: 0249,
        title: "Enable strict mode for package framework compatibility validation",
        message: "Define the <EnableStrictModeForCompatibleFrameworksInPackage> node with value 'true' or remove the <EnablePackageValidation> node with value 'true'.",
        description:
            "When building your package for multiple runtimes it " +
            "is advised to enable the strict mode of the framework " +
            "compatibility validation.",
        tags: ["Configuration", "package", "compatibility"],
        category: Category.Reliability,
        severity: DiagnosticSeverity.Warning,
    isEnabled: true);

    public static DiagnosticDescriptor GenerateApiCompatibilitySuppressionFile => New(
        id: 0250,
        title: "Generate API compatibility suppression file",
        message: "Define the <ApiCompatGenerateSuppressionFile> node with value 'true' or remove the <EnablePackageValidation> node with value 'true'.",
        description:
            "This suppression file can be created manually, or automatically generated " +
            "by enabling the `GenerateCompatibilitySuppressionFile` property.It is advised " +
            "to enable this property in the project file to ensure that file is kept " +
            "up-to-date automatically.",
        tags: ["Configuration", "package", "compatibility"],
        category: Category.Reliability,
        severity: DiagnosticSeverity.Warning,
        isEnabled: true);

    public static DiagnosticDescriptor EnableApiCompatibilityAttributeChecks => New(
        id: 0251,
        title: "Enable API compatibility attribute checks",
        message: "Define the <ApiCompatEnableRuleAttributesMustMatch> node with value 'true' or remove the <EnablePackageValidation> node with value 'true'.",
        description:
            "When package validation is enabled, it is advised to opt-in " +
            "to the strict attribute compatibility checks.",
        tags: ["Configuration", "package", "compatibility"],
        category: Category.Reliability,
        severity: DiagnosticSeverity.Warning,
        isEnabled: true);

    public static DiagnosticDescriptor EnableApiCompatibilityParameterNameChecks => New(
        id: 0252,
        title: "Enable API compatibility parameter name checks",
        message: "Define the <ApiCompatEnableRuleCannotChangeParameterName> node with value 'true' or remove the <EnablePackageValidation> node with value 'true'.",
        description:
            "When package validation is enabled, it is advised to opt-in " +
            "to the strict parameter name compatibility checks.",
        tags: ["Configuration", "package", "compatibility"],
        category: Category.Reliability,
        severity: DiagnosticSeverity.Warning,
        isEnabled: true);

    public static DiagnosticDescriptor DefineIsPublishable => New(
        id: 0400,
        title: "Define the project publishability explicitly",
        message: "Define the <IsPublishable> node explicitly.",
        description:
            "To prevent confusion, explicitly define the " +
            "<IsPublishable> node.",
        tags: ["Configuration", "confusion"],
        category: Category.Clarity);

    public static DiagnosticDescriptor TestProjectShouldNotBePackable => New(
        id: 0450,
        title: "Test projects should not be packable",
        message: "Set <IsPackable> to false.",
        description:
            "Test projects should only be responsible for running tests. Hence " +
            "they should not be packable.",
        tags: ["Configuration", "Unit Testing"],
        category: Category.Bug);

    public static DiagnosticDescriptor TestProjectShouldNotBePublishable => New(
        id: 0451,
        title: "Test projects should not be publishable",
        message: "Set <IsPublishable> to false.",
        description:
            "Test projects should only be responsible for running tests. Hence " +
            "they should not be publishable.",
        tags: ["Configuration", "Unit Testing"],
        category: Category.Bug);

    public static DiagnosticDescriptor TestProjectsRequireSdk => New(
        id: 0452,
        title: "Test projects require Microsoft.NET.Test.Sdk",
        message: @"Include <PackageReference Include=""Microsoft.NET.Test.Sdk"" PrivateAssets =""all"" />.",
        description: "Tests in a test projects do not run properly without the Microsoft.NET.Test.Sdk being included.",
        tags: ["Unit Testing"],
        category: Category.Bug);

    public static DiagnosticDescriptor UsingMicrosoftNetTestSdkImpliesTestProject => New(
        id: 0453,
        title: "Using Microsoft.NET.Test.Sdk implies a test project",
        message: @"Set <IsTestProject> to true.",
        description: "Including the Microsoft.NET.Test.Sdk is only useful for test projects.",
        tags: ["Unit Testing"],
        category: Category.Bug);

    public static DiagnosticDescriptor AvoidGeneratePackageOnBuildWhenNotPackable => New(
        id: 0600,
        title: "Avoid generating packages on build if not packable",
        message: "Avoid defining <GeneratePackageOnBuild> node explicitly when <IsPackable> is 'false'.",
        description:
            "The <GeneratePackageOnBuild> option has no effect " +
            "when <IsPackable> the node is disabled. " +
            "Removing the <GeneratePackageOnBuild> " +
            "node will reduce noise.",
        tags: ["Configuration", "NuGet", "package"],
        category: Category.Configuration);

    public static DiagnosticDescriptor AvoidCompileItemInSdk => New(
        id: 0700,
        title: "Avoid defining <Compile> items in SDK project",
        message: "The .net.csproj SDK project should not contain <Compile> items.",
        description:
            "The .net.csproj SDK project is a placeholder for non-compiling " +
            "files. Compile items should be avoided.",
        tags: ["SDK", "NuGet", "package"],
        category: Category.SDK);

    public static DiagnosticDescriptor ConfigureCentralPackageVersionManagement => New(
        id: 0800,
        title: "Configure Central Package Management explicitly",
        message: "Define the <ManagePackageVersionsCentrally> node with the value 'true', or 'false'.",
        description:
            "In situations where you manage common dependencies for many " +
            "different projects, you can leverage Central Package Version " +
            "Management (CPM) features to do all of this from a single location.",
        tags: ["configuration", "versioning"],
        category: Category.CPM);

    public static DiagnosticDescriptor IncludeDirectoryPackagesProps => New(
        id: 0801,
        title: "Include 'Directory.Packages.props'",
        message: "The file 'Directory.Packages.props' could not be located.",
        description: "When CPM is enabled 'Directory.Packages.props' should be resolvable.",
        tags: ["configuration", "versioning"],
        category: Category.CPM);

    public static DiagnosticDescriptor EnableCentralPackageManagementCentrally => New(
        id: 0802,
        title: "Enable Central Package Management centrally",
        message: "Enable <ManagePackageVersionsCentrally> in 'Directory.Packages.props' or a shared props file.",
        description: "Enabling it per project would defy the purpose of CPM.",
        tags: ["Maintainability"],
        category: Category.CPM);

    public static DiagnosticDescriptor UseVersionOverrideOnlyWithCpm => New(
        id: 0803,
        title: "Use VersionOverride only with Central Package Management enabled",
        message: "Use Version instead of VersionOverride when CPM is not enabled.",
        description:
            "When CPM is not enabled the use of <PackageReference VersionOveride /> " +
            "`has no effect, and is most likely a mistake.",
        tags: ["Maintainability"],
        category: Category.CPM);

    public static DiagnosticDescriptor UseVersionOnlyWithoutCpm => New(
        id: 0804,
        title: "Use Version only with Central Package Management not enabled",
        message: "Do not use Version when CPM is enabled.",
        description:
            "When CPM is enabled the use of <PackageReference Version /> " +
            "`has no effect, and is most likely a mistake.",
        tags: ["Maintainability"],
        category: Category.CPM);

    public static DiagnosticDescriptor DefinePackageReferenceVersion => New(
        id: 0805,
        title: "Define version for PackageReference",
        message: "Define version for '{0}' PackageReference.",
        description:
            "PackageReference nodes require a version number in order to " +
            "properly resolve the package. This can be done by either using the " +
            "Version attribute on the PackageReference node, or by using a matching " +
            "PackageVersion node.",
        tags: ["NuGet"],
        category: Category.CPM);

    public static DiagnosticDescriptor VersionOverrideShouldChangeVersion => New(
        id: 0806,
        title: "VersionOverride should change the version",
        message: "Remove VersionOverride or change it to a version different than defined by the CPM.",
        description:
            "The use of VersionOverride on a <PackageReference> is only useful " +
            "when it defines a different version then already defined by the CPM.",
        tags: ["Bug"],
        category: Category.CPM);

    public static DiagnosticDescriptor OnlyUseDirectoryPackagesPropsForCPM => New(
        id: 0807,
        title: "Only use Directory.Packages.props for Central Package Management",
        message: "As <{0}> is not about Central Package Management it should not be in Directory.Packages.props.",
        description:
            "The use of VersionOverride on a <PackageReference> is only useful " +
            "when it defines a different version then already defined by the CPM.",
        tags: ["Bug"],
        category: Category.CPM);

    public static DiagnosticDescriptor UseDotNetProjectFileAnalyzers => New(
        id: 1000,
        title: "Use the .NET project file analyzers",
        message: "Use the .NET project file analyzers.",
        description: "To improve the code quality of .NET project files.",
        tags: ["roslyn", "analyzer", "cbproj", "vbproj"],
        category: Category.CodeQuality,
        isEnabled: false);

    public static DiagnosticDescriptor UseAnalyzersForPackages => New(
        id: 1001,
        title: "Use analyzers for packages",
        message: "Use {0} to analyze {1}.",
        description:
            "Some NuGet packages come with there own/dedicated Roslyn analyzers; " +
            "they just contain rules to improve the usage of those packages. " +
            "In order to get the best out of those NuGet packages, their " +
            "analyzer(s) should be used.",
        tags: ["roslyn", "analyzer", "NuGet"],
        category: Category.CodeQuality);

    public static DiagnosticDescriptor UseDotNetAnalyzers => New(
        id: 1002,
        title: "Use Microsoft's .NET analyzers",
        message: "Use Microsoft's .NET analyzers by setting <EnableNETAnalyzers> to true.",
        description: "Improve the code quality by adding Microsoft's Roslyn analyzers.",
        tags: ["roslyn", "analyzer", "Microsoft"],
        category: Category.CodeQuality);

    public static DiagnosticDescriptor UseSonarAnalyzers => New(
        id: 1003,
        title: "Use Sonar analyzers for packages",
        message: "Add {0}.",
        description: "Improve the code quality by adding Sonar's Roslyn analyzers.",
        tags: ["roslyn", "analyzer", "Sonar"],
        category: Category.CodeQuality);

    public static DiagnosticDescriptor AvoidUsingMoq => New(
        id: 1100,
        title: "Avoid using Moq",
        message: "Do not use Moq.",
        description: "Moq has built in data harvesting that violates GDPR.",
        tags: ["GDPR", "privacy"],
        category: Category.Security,
        isEnabled: false);

    public static DiagnosticDescriptor PackageReferencesShouldBeStable => New(
        id: 1101,
        title: "Package references should have stable versions",
        message: "Use a stable version of '{0}', instead of '{1}'.",
        description: "The use of nightly builds and other pre-releases should be avoided.",
        tags: ["NuGet", "Versioning"],
        category: Category.Reliability);

    public static DiagnosticDescriptor UseCoverletCollectorOrMsBuild => New(
        id: 1102,
        title: "Use Coverlet Collector or MSBuild",
        message: "Choose either coverlet.collector or coverlet.msbuild.",
        description:
            "The packages coverlet.collector and coverlet.msbuild have the " +
            "same purpose but should not be used together.",
        tags: ["coverlet", "code", "coverage"],
        category: Category.Bug);

    public static DiagnosticDescriptor ExcludePrivateAssetDependencies => New(
        id: 1200,
        title: "Exclude private assets as project file dependency",
        message: "Mark the package reference \"{0}\" as a private asset.",
        description:
            "Private assets (such as analyzers) will not result in being a " +
            "project dependency after being compiled.",
        tags: ["private", "asset", "dependencies", "dependency"],
        category: Category.CodeQuality);

    public static DiagnosticDescriptor IndentXml => New(
        id: 1700,
        title: "Indent XML files",
        message: "The element <{0}> has not been properly indented.",
        description: "To improve readability, XML elements should be properly indented.",
        tags: ["XML", "indentation"],
        category: Category.Formatting);

    public static DiagnosticDescriptor UseCDATAForLargeTexts => New(
        id: 1701,
        title: "Use <![CDATA[ for large texts",
        message: "Add <![CDATA[ and ]]> around this text.",
        description: "To improve readability, large texts should be CDATA.",
        tags: ["XML", "CDATA"],
        category: Category.Formatting);

    public static DiagnosticDescriptor OmitXmlDeclarations => New(
        id: 1702,
        title: "Omit XML declarations",
        message: "Remove the XML declaration as it is redundant.",
        description: "The XML declaration is redundant for MS Build project files.",
        tags: ["XML", "declaration"],
        category: Category.Formatting);

    public static DiagnosticDescriptor EmbedValidResourceFiles => New(
        id: 2000,
        title: "Embed valid resource files",
        message: "Resource file {0}",
        description: "A resource file should contain valid XML.",
        tags: ["resx", "resources"],
        category: Category.Bug,
        severity: DiagnosticSeverity.Error);

    public static DiagnosticDescriptor DefineData => New(
        id: 2001,
        title: "Define data in a resource file",
        message: "Resource does not contain any data.",
        description: "A resource file without `<data>` elements is of no use.",
        tags: ["resx", "resources"],
        category: Category.Noise);

    public static DiagnosticDescriptor SortDataAlphabetically => New(
        id: 2002,
        title: "Sort resource file values alphabetically",
        message: "Resource '{0}' is not ordered alphabetically and should appear before '{1}'.",
        description:
            "To improve readability, and reduce the number of merge conflicts, " +
            "the `<data>` elements should be sorted based on the `@name` attribute.",
        tags: ["resx", "resources"],
        category: Category.Clarity);

    public static DiagnosticDescriptor AddInvariantFallbackResources => New(
        id: 2003,
        title: "Add invariant fallback resources",
        message: "Add invariant fallback resource.",
        description:
            "To ensure that localized values can be resolved, a localized resource " +
            "file must have a culture invariant alternative.",
        tags: ["resx", "resources", "invariant", "localization"],
        category: Category.Bug);

    public static DiagnosticDescriptor AddInvariantFallbackValues => New(
       id: 2004,
       title: "Add invariant fallback values",
       message: "Add invariant fallback value for '{0}'.",
       description:
           "To ensure that localized values can be resolved, a localized value " +
           "file must have a culture invariant alternative.",
       tags: ["resx", "resources", "invariant", "localization"],
       category: Category.Bug);

    public static DiagnosticDescriptor EscapeXmlNodesResourceValues => New(
      id: 2005,
      title: "Escape XML nodes of resource values",
      message: "Escape the XML node in '{0}'.",
      description: "To ensure correct handling, XML nodes within resource values should be escaped.",
      tags: ["resx", "resources", "XML", "escaping"],
      category: Category.Bug);

    public static DiagnosticDescriptor IndentResx => New(
      id: 2100,
      title: "Indent XML files",
      message: "The element <{0}> has not been properly indented.",
      description: "To improve readability, XML elements should be properly indented.",
      tags: ["XML", "indentation"],
      category: Category.Formatting);

    public static DiagnosticDescriptor OnlyUseUTF8WithoutBom => New(
        id: 3000,
        title: "Ony use UTF-8 encoding without BOM",
        message: "This file is using UTF-8 encoding with BOM.",
        description:
            "The use of BOM for UTF-8 can cause systems to malfunction. This " +
            "includes multiple web based files such as Javascript and CSS.",
        tags: ["encoding", "UTF-8"],
        category: Category.Reliability);

    public static DiagnosticDescriptor TrackToDoTags => New(
        id: 3001,
        title: @"Track uses of ""TODO"" tags",
        message: @"Complete the task associated to this ""{0}"" comment.",
        description:
            "Developers often use TODO tags to mark areas in the code where " +
            "additional work or improvements are needed but are not implemented " +
            "immediately.",
        tags: ["Code smell"],
        category: Category.CodeSmell);

#pragma warning disable S107 // Methods should not have too many parameters
    // it calls a ctor with even more arguments.
    public static DiagnosticDescriptor New(
        int id,
        string title,
        string message,
        string description,
        string[] tags,
        Category category,
        DiagnosticSeverity severity = DiagnosticSeverity.Warning,
        bool isEnabled = true)
#pragma warning restore S107 // Methods should not have too many parameters
        => new(
            id: $"Proj{id:0000}",
            title: title,
            messageFormat: message,
            customTags: tags,
            category: category.ToString(),
            defaultSeverity: category == Category.SyntaxError ? DiagnosticSeverity.Error : severity,
            isEnabledByDefault: isEnabled,
            description: description,
            helpLinkUri: $"https://dotnet-project-file-analyzers.github.io/rules/Proj{id:0000}.html");
}
