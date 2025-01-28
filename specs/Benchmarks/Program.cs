using BenchmarkDotNet.Running;

namespace Benchmarks;

public static class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<IniParsing>();
    }

    public static void All()
    {
        BenchmarkRunner.Run<GitIgnoreFile>();
        BenchmarkRunner.Run<Globs>();
        BenchmarkRunner.Run<IniParsing>();
        BenchmarkRunner.Run<RunAll>();
    }
}
