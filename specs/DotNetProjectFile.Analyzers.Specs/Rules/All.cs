using Specs.TestTools;
using System.IO;

namespace Rules.All;

public class Guards
{
    [Test]
    public void DotNetProjectFile_Analyzers_project()
    {
        var all = AvailableAnalyzers.All.ToArray();
        var context = all[0].ForProject(new FileInfo($"../../../../../src/DotNetProjectFile.Analyzers/DotNetProjectFile.Analyzers.csproj"))
            with
        {
            IgnoredDiagnostics = DiagnosticIds.Empty.AddRange(
                "CS8019", // Unnecessary using directive.
                "CS8933", // The using directive appeared previously as global using

                // Introduced by ANTLR4 code base
                "CS0103",
                "CS0246",
                "CS1061",
                "CS3021",
                "CS8600",
                "CS8601",
                "CS8602",
                "CS8603",
                "CS8604",
                "CS8618",
                "CS8619",
                "CS8625")
        };

        // Add the remaining analyzers.
        foreach (var analyzer in all[1..])
        {
            context = context.Add(analyzer);
        }
        context.HasNoIssues();
    }
}
