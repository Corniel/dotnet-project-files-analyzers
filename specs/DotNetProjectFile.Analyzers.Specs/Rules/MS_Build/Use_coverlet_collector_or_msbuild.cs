namespace Rules.MS_Build.Use_coverlet_collector_or_msbuild;

public class Reports
{
    [Test]
    public void on_both_packages_included() => new UseCoverletCollectorOrMsBuild()
        .ForInlineCsproj(@"
<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include=""coverlet.collector"" Version=""6.0.0"" PrivateAssets=""all"" />
    <PackageReference Include=""coverlet.msbuild"" Version=""6.0.0"" PrivateAssets=""all"" />
  </ItemGroup>
</Project>")
        .HasIssue(
           Issue.WRN("Proj1102", "Choose either coverlet.collector or coverlet.msbuild.")
            .WithSpan(5, 04, 5, 89));
}

public class Guards
{
    [TestCase]
    public void Project_with_collector() => new UseCoverletCollectorOrMsBuild()
        .ForInlineCsproj(@"
<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include=""coverlet.collector"" Version=""6.0.0"" PrivateAssets=""all"" />
  </ItemGroup>
</Project>")
        .HasNoIssues();

    [TestCase]
    public void Project_with_msbuild() => new UseCoverletCollectorOrMsBuild()
    .ForInlineCsproj(@"
<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include=""coverlet.msbuild"" Version=""6.0.0"" PrivateAssets=""all"" />
  </ItemGroup>
</Project>")
    .HasNoIssues();

    [TestCase("CompliantCSharp.cs")]
    [TestCase("CompliantCSharpPackage.cs")]
    public void Projects_with_analyzers(string project) => new UseCoverletCollectorOrMsBuild()
        .ForProject(project)
        .HasNoIssues();
}
