# Comparing various ways to compute min/max

I've stumbled upon this code [here](https://github.com/buybackoff/1brc/blob/59b42ec3493a50a4f9e3c3ab1e470f98bff8c26a/1brc/Summary.cs#L41) and wondered whether it was indeed faster than other 'simpler' options:

```csharp
[MethodImpl(MethodImplOptions.AggressiveInlining)]
static int GetMin(int a, int b)
{
    int delta = a - b;
    return b + (delta & (delta >> 31));
}

[MethodImpl(MethodImplOptions.AggressiveInlining)]
static int GetMax(int a, int b)
{
    int delta = a - b;
    return a - (delta & (delta >> 31));
}
```

Two other 'simpler' implementations would be:

```csharp
internal static class Baseline
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Min(int x, int y) => Math.Min(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Max(int x, int y) => Math.Max(x, y);
}

internal static class Comparison
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Min(int x, int y) => x < y ? x : y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Max(int x, int y) => x > y ? x : y;
}
```

Here is what [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet) has to say on my machine:

```plaintext
BenchmarkDotNet v0.13.12, Windows 10 (10.0.19045.3803/22H2/2022Update)
11th Gen Intel Core i7-11800H 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.100
  [Host]        : .NET 6.0.25 (6.0.2523.51912), X64 RyuJIT AVX2
  .NET 6.0      : .NET 6.0.25 (6.0.2523.51912), X64 RyuJIT AVX2
  .NET 8.0      : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  NativeAOT 8.0 : .NET 8.0.0, X64 NativeAOT AVX2
```

| Method        | Job           | Runtime       | N          | Mean       | Error    | StdDev   | Ratio | RatioSD |
|-------------- |-------------- |-------------- |----------- |-----------:|---------:|---------:|------:|--------:|
| RunBaseline   | .NET 6.0      | .NET 6.0      | 1000000000 |   924.0 ms |  8.26 ms |  7.73 ms |  1.00 |    0.00 |
| RunBaseline   | .NET 8.0      | .NET 8.0      | 1000000000 |   981.2 ms | 19.34 ms | 47.81 ms |  0.99 |    0.04 |
| RunBaseline   | NativeAOT 8.0 | NativeAOT 8.0 | 1000000000 |   895.8 ms | 17.64 ms | 19.61 ms |  0.97 |    0.02 |
|               |               |               |            |            |          |          |       |         |
| RunComparison | .NET 6.0      | .NET 6.0      | 1000000000 |   860.5 ms |  9.61 ms |  8.52 ms |  1.00 |    0.00 |
| RunComparison | .NET 8.0      | .NET 8.0      | 1000000000 |   966.4 ms | 18.08 ms | 16.91 ms |  1.12 |    0.02 |
| RunComparison | NativeAOT 8.0 | NativeAOT 8.0 | 1000000000 |   908.6 ms | 18.13 ms | 16.96 ms |  1.06 |    0.01 |
|               |               |               |            |            |          |          |       |         |
| RunOptimized  | .NET 6.0      | .NET 6.0      | 1000000000 | 1,193.5 ms |  3.25 ms |  2.54 ms |  1.00 |    0.00 |
| RunOptimized  | .NET 8.0      | .NET 8.0      | 1000000000 | 1,252.6 ms | 16.15 ms | 14.32 ms |  1.05 |    0.01 |
| RunOptimized  | NativeAOT 8.0 | NativeAOT 8.0 | 1000000000 | 1,197.8 ms |  7.48 ms |  5.84 ms |  1.00 |    0.00 |

The winner seems to sbe my `Comparison` implementation and the weird `Optimized` implementation is clearly slower!

> Btw, I don't know why .NET 6 > .NET 8 and even beats .NET 8 AOT (except for the `Math.Min/Max`case`)...
>
> I should try .NET 6 AOT too.
