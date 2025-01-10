using BenchmarkDotNet.Running;

namespace Benchmarks;

public static class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<IniFile>();

        //var b = new IniFile() { Index = 2 };
        //b.Parse();
    }

    public static void All()
    {
        BenchmarkRunner.Run<IniFile>();
        BenchmarkRunner.Run<GitIgnoreFile>();
        BenchmarkRunner.Run<Globs>();
        BenchmarkRunner.Run<RunAll>();
    }
}
