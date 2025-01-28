using Antlr4.Runtime.Misc;
using DotNetProjectFile.Collections;

namespace System.Collections.Generic;

internal static class AntlrListExtensions
{
    [Pure]
    public static Slice<T> Slice<T>(this IReadOnlyList<T> list , Interval interval)
        => list  is Slice<T> slice
        ? slice.Skip(interval.a).Take(interval.Length)
        : new(interval.a, interval.Length, list );
}
