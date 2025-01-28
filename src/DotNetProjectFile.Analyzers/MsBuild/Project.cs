using DotNetProjectFile.Ini;
using Microsoft.CodeAnalysis.Text;

namespace DotNetProjectFile.MsBuild;

public sealed partial class Project : Node, ProjectFile
{
    private Project(IOFile path, SourceText text, ProjectFiles projectFiles, AdditionalText? additionalText)
        : this(path, text, XDocument.Parse(text.ToString(), LoadOptions), projectFiles, additionalText)
    {
    }

    private Project(IOFile path, SourceText text, XDocument document, ProjectFiles projectFiles, AdditionalText? additionalText)
        : base(document.Root, null, null)
    {
        Path = path;
        Text = text;
        ProjectFiles = projectFiles;
        AdditionalText = additionalText;
        WarningPragmas = WarningPragmas.New(this);
    }

    public MsBuildProject? DirectoryBuildProps => Path.Directory
        .AncestorsAndSelf()
        .Select(dir => ProjectFiles.MsBuildProject(dir.File("Directory.Build.props")))
        .OfType<MsBuildProject>()
        .FirstOrDefault();

    public MsBuildProject? DirectoryBuildTargets => Path.Directory
       .AncestorsAndSelf()
       .Select(dir => ProjectFiles.MsBuildProject(dir.File("Directory.Build.targets")))
       .OfType<MsBuildProject>()
       .FirstOrDefault();

    public MsBuildProject? DirectoryPackagesProps => Path.Directory
        .AncestorsAndSelf()
        .Select(dir => ProjectFiles.MsBuildProject(dir.File("Directory.Packages.props")))
        .OfType<MsBuildProject>()
        .FirstOrDefault();

    public AdditionalText? AdditionalText { get; }

    public bool IsLegacy => Element.Name.NamespaceName is { Length: > 0 };

    public string? Sdk => Attribute();

    public IOFile Path { get; }

    public ProjectFileType FileType => Path switch
    {
        _ when Path.Name.IsMatch(".net.csproj") => ProjectFileType.SDK,
        _ when Path.Extension.IsMatch(".csproj")
            || Path.Extension.IsMatch(".vbproj") => ProjectFileType.ProjectFile,
        _ when Path.Name.IsMatch("Directory.Build.props")
            || Path.Name.IsMatch("Directory.Build.targets") => ProjectFileType.DirectoryBuild,
        _ when Path.Name.IsMatch("Directory.Packages.props") => ProjectFileType.DirectoryPackages,
        _ => ProjectFileType.Props,
    };

    public SourceText Text { get; }

    internal ProjectFiles ProjectFiles { get; }

    public Nodes<Import> Imports => new(Children);

    public Nodes<PropertyGroup> PropertyGroups => new(DescendantsAndSelf());

    public Nodes<ItemGroup> ItemGroups => new(DescendantsAndSelf());

    public WarningPragmas WarningPragmas { get; }

    public IEnumerable<IniFile> EditorConfigs() => [];
        //=> Path.Directory.AncestorsAndSelf()
        //.Select(dir => ProjectFiles.IniFile(dir.File(".editorconfig")))
        //.OfType<IniFileSyntax>()
        //.Select(ini => new IniFile(ini))
        //.TakeUntil(config => config.IsRoot);

    /// <summary>Loops through all imports and self.</summary>
    /// <remarks>
    /// Should only be used to register project files. In other cases
    /// <see cref="Walk()" /> and <see cref="WalkBackward()"/> should be used.
    /// </remarks>
    public IEnumerable<Project> ImportsAndSelf()
        => Walk().OfType<Project>().Distinct();

    /// <summary>Gets Directory.Build.targets, self, Directory.Packages.props, and Directory.Build.props).</summary>
    /// <remarks>
    /// If the *.props are null, or higher in the type hierarchy they are skipped.
    /// </remarks>
    private IEnumerable<Project> SelfAndDirectoryProps()
    {
        if (DirectoryBuildTargets is { } targets && FileType < ProjectFileType.DirectoryBuild)
        {
            yield return targets;
        }

        yield return this;

        if (DirectoryPackagesProps is { } pack && FileType < ProjectFileType.DirectoryPackages)
        {
            yield return pack;
        }
        if (DirectoryBuildProps is { } props && FileType < ProjectFileType.DirectoryBuild)
        {
            yield return props;
        }
    }

    public static Project Load(IOFile file, ProjectFiles projects)
    {
        using var reader = file.OpenText();
        return new(
            path: file,
            text: SourceText.From(reader.ReadToEnd()),
            projectFiles: projects,
            additionalText: null);
    }

    public static Project Load(AdditionalText text, ProjectFiles projects)
        => new(
            path: IOFile.Parse(text.Path),
            text: text.GetText()!,
            projectFiles: projects,
            additionalText: text);

    private static readonly LoadOptions LoadOptions = LoadOptions.PreserveWhitespace | LoadOptions.SetLineInfo;
}
