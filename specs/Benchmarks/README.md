# Benchmarks
To monitor performance.

## Get Diagnostics
Comparison of the build times, with and without running the .NET project file
analyzers.

| Project        |     Build |    Build+ | Analysis |
|----------------|----------:|----------:|---------:|
| CompliantCS    |  0.267 ms |  7.859 ms | 7.592 ms |
| CompliantVB    |  0.440 ms |  7.806 ms | 7.366 ms |
| .NET analyzers | 73.621 ms | 73.621 ms | 0.586 ms |

# Parsing INI files
|     File |        Mean |        Speed |
|---------:|------------:|-------------:|
|   27 LoC |    35.71 us |   1,3 µs/LoC |
|   36 LoC |    36.30 us |   1,0 µs/LoC |
| 1220 LoC | 5,692.83 us |   4,7 µs/LoC |

| Method | Index | Mean          | Error         | StdDev        |
|------- |------ |--------------:|--------------:|--------------:|
| Parse  | 0     |      93.69 us |      1.809 us |      3.121 us |
| Parse  | 1     |     104.40 us |      1.943 us |      3.025 us |
| Parse  | 2     | 560,238.32 us | 10,998.185 us | 22,957.309 us |


## Parsing Globs
The purpose was never speed, 
| Method               | Expression           | Mean      |
|--------------------- |--------------------- |----------:|
| DotNetProjectFile_IO | [Dd]ebug             |  81.95 ns |
| GlobExpressions_Glob | [Dd]ebug             | 215.51 ns |
| DotNet_Globbing      | [Dd]ebug             | 189.20 ns |
| DotNetProjectFile_IO | *.{cs,vb,ts}         | 242.29 ns |
| GlobExpressions_Glob | *.{cs,vb,ts}         | 311.40 ns |
| DotNet_Globbing      | *.{cs,vb,ts}         | 236.58 ns |
| DotNetProjectFile_IO | *multiple xml files* | 339.46 ns |
| GlobExpressions_Glob | *multiple xml files* | 362.53 ns |
| DotNet_Globbing      | *multiple xml files* | 385.11 ns |
| DotNetProjectFile_IO | *.*                  |  69.31 ns |
| GlobExpressions_Glob | *.*                  | 134.49 ns |
| DotNet_Globbing      | *.*                  | 207.82 ns |

### Parsing .gitignore files
|    File |      Mean |      Speed |
|--------:|----------:|-----------:|
|  50 LoC |  25.29 us | 506 ns/LoC |
| 231 LoC | 107.77 us | 467 ns/LoC |
