using DotNetProjectFile.Ini;
using System.IO;
using SyntaxTree = DotNetProjectFile.Syntax.SyntaxTree;

namespace Parsing.INI_syntax;

public class Parses
{
    [Test]
    public void header_with_space()
    {
        var syntax = Parse.Syntax("[My Header]");

        syntax.Sections.Should().HaveCount(1);
        syntax.Sections[0].Header.Should().BeEquivalentTo(new { Text = "My Header" });
    }

    [Test]
    public void header_with_one_kvp()
    {
        var syntax = Parse.Syntax(@"
[MyHeader]
mykey = 3.14");

        syntax.Sections.Should().HaveCount(1);
        syntax.Sections[0].Header.Should().BeEquivalentTo(new { Text = "MyHeader" });
        syntax.Sections[0].Kvps.Should().BeEquivalentTo([new { Key = "mykey", Value = "3.14" }]);
    }

    [Test]
    public void dot_editorconfig()
    {
        using var file = new FileStream("../../../../../.editorconfig", FileMode.Open, FileAccess.Read);
        var tree = SyntaxTree.Load(file);
        var syntax = IniFileSyntax.Parse(tree);

        syntax.Should().BeOfType<IniFileSyntax>();

        syntax.Tokens.Should().NotContain(t => t.Kind == TokenKind.UnparsableToken);
    }

    [Test]
    public void with_garbage()
    {
        var syntax = Parse.Syntax(@"global = false
some_key = value
invalid line
indenting = \t
[]"
        );

        var kvps = syntax.Sections[0].Kvps.ToArray();

        kvps.Should().BeEquivalentTo(new Dictionary<string, string>()
        {
            ["global"] = "false",
            ["some_key"] = "value",
            ["indenting"] = "\\t",
        });
    }
}


public class Parses_with_errors
{
    public class Header
    {
        [Test]
        public void no_text()
        {
            var syntax = Parse.Syntax("[]");

            syntax.Sections[0].Header!.GetDiagnostics().Should().HaveIssue(
                Issue.ERR("Proj4001", "] is unexpected.").WithSpan(0, 1, 0, 2));
        }

        [Test]
        public void multi_open()
        {
            var syntax = Parse.Syntax("[[header]");

            syntax.Sections[0].Header!.GetDiagnostics().Should().HaveIssue(
                Issue.ERR("Proj4001", "[ is unexpected.").WithSpan(0, 1, 0, 2));
        }

        [Test]
        public void no_closing()
        {
            var syntax = Parse.Syntax("[header\n");

            syntax.Sections[0].Header!.GetDiagnostics().Should().HaveIssue(
                Issue.ERR("Proj4001", "] is expected.").WithSpan(0, 7, 1, 0));
        }

        [Test]
        public void multi_closing()
        {
            var syntax = Parse.Syntax("[header]]");

            syntax.Sections[0].Header!.GetDiagnostics().Should().HaveIssue(
                Issue.ERR("Proj4001", "] is unexpected.").WithSpan(0, 8, 0, 9));
        }
    }

    public class Key_value_pair
    {
        [Test]
        public void no_key()
        {
            var syntax = Parse.Syntax("= value \n");

            syntax.GetDiagnostics().Should().HaveIssue(
                Issue.ERR("Proj4002", "= is unexpected.").WithSpan(0, 0, 0, 1));
        }

        [Test]
        public void no_value()
        {
            var syntax = Parse.Syntax("key1 = \n");

            syntax.GetDiagnostics().Should().HaveIssue(
                Issue.ERR("Proj4002", "Value is missing.").WithSpan(0, 6, 0, 7));
        }

        [Test]
        public void no_assign()
        {
            var syntax = Parse.Syntax("key1 value1\n");

            syntax.GetDiagnostics().Should().HaveIssue(
                Issue.ERR("Proj4002", "= or : is expected.").WithSpan(0, 5, 0, 11));
        }

        [Test]
        public void  multipe_assign()
        {
            var syntax = Parse.Syntax("key1 = : value1\n");

            syntax.GetDiagnostics().Should().HaveIssue(
                Issue.ERR("Proj4002", ": is unexpected.").WithSpan(0, 7, 0, 8));
        }

        [Test]
        public void Rubbish()
        {
            var syntax = Parse.Syntax("KeyWith Comment = #Comment");

            syntax.GetDiagnostics().Should().HaveIssue(
                Issue.ERR("Proj4002", "= or : is expected.").WithSpan(0, 8, 0, 15));
        }
    }

    public class File_with_issue
    {
        [Test]
        public void issue_251()
        {
            var syntax = Parse.Syntax(new FileInfo("../../../Parsing/Examples/bug_00251.ini"));
            syntax.GetDiagnostics().Should().HaveCount(1);
        }
    }

    public class Non_INI
    {
        [Test]
        public void XML()
        {
            var syntax = Parse.Syntax(@"
<Project>
  <PropertyGroup>
    <TargetFramework>9.0</TargetFramework>
  </PropertyGroup>
</Project>");

            syntax.GetDiagnostics().Should().HaveCount(5);
        }
    }
}

file static class Parse
{
    public static IniFileSyntax Syntax(FileInfo file)
    {
        using var stream = file.OpenRead();

        var tree = SyntaxTree.Load(stream);
        var synstax = IniFileSyntax.Parse(tree);
        synstax.Should().NotBeNull();
        return synstax!;

    }

    public static IniFileSyntax Syntax(string text)
    {
        var tree = SyntaxTree.Parse(text);
        var synstax = IniFileSyntax.Parse(tree);
        synstax.Should().NotBeNull();
        return synstax!;
    }
}
