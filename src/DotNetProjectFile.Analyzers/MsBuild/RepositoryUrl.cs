namespace DotNetProjectFile.MsBuild;

public sealed class RepositoryUrl(XElement element, Node parent, MsBuildProject project)
    : Node<string>(element, parent, project) { }
