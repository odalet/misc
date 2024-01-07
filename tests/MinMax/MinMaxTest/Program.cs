#define RUN_BENCHMARK

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace MinMaxTest;

internal sealed class Program
{
#if RUN_BENCHMARK
    private static void Main()
    {
        var summary = BenchmarkRunner.Run<Benchmark>();
        _ = summary;
    }
#else
    private enum Implementation
    {
        Baseline,
        Comparison,
        Optimized
    }

    private const int count = 1_000_000_000;

    private static void Main()
    {
        var data = GenerateData(count);
        var baselineResult = Measure(data, Implementation.Baseline);
        var comparisonResult = Measure(data, Implementation.Comparison);
        var optimizedResult = Measure(data, Implementation.Optimized);

        Dump(baselineResult, Implementation.Baseline);
        Dump(comparisonResult, Implementation.Comparison);
        Dump(optimizedResult, Implementation.Optimized);
    }

    private static void Dump((TimeSpan duration, int min, int max) r, Implementation impl) =>
        Console.WriteLine($"{impl,-15}{r.duration,-20} {r.min,-20} {r.max,-20}");

    private static (TimeSpan duration, int min, int max) Measure(int[] data, Implementation impl)
    {
        var sw = new Stopwatch();
        sw.Start();

        var min = int.MaxValue;
        var max = int.MinValue;

        switch (impl)
        {
            case Implementation.Baseline:
                for (var i = 0; i < data.Length; i++)
                {
                    min = Baseline.Min(min, data[i]);
                    max = Baseline.Max(max, data[i]);
                }
                break;
            case Implementation.Comparison:
                for (var i = 0; i < data.Length; i++)
                {
                    min = Comparison.Min(min, data[i]);
                    max = Comparison.Max(max, data[i]);
                }
                break;
            case Implementation.Optimized:
                for (var i = 0; i < data.Length; i++)
                {
                    min = Optimized.Min(min, data[i]);
                    max = Optimized.Max(max, data[i]);
                }
                break;
            default:
                throw new ArgumentException($"Unsupported implementation: {impl}");
        }

        return (sw.Elapsed, min, max);
    }

    private static int[] GenerateData(int count)
    {
        var list = new List<int>();

        var rnd = new Random();
        for (var i = 0; i < count; i++)
            list.Add(rnd.Next(int.MinValue / 2, int.MaxValue / 2));

        return list.ToArray();
    }
#endif
}

[SimpleJob(RuntimeMoniker.Net60, baseline: true)]
[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.NativeAot80)]
public class Benchmark
{
    private int[] data = Array.Empty<int>();
    private int min = int.MaxValue;
    private int max = int.MinValue;

    [Params(1000 * 1000 * 1000)]
    public int N;

    [GlobalSetup]
    public void Setup()
    {
        var list = new List<int>();

        var rnd = new Random();
        for (var i = 0; i < N; i++)
            list.Add(rnd.Next(int.MinValue / 2, int.MaxValue / 2));

        data = list.ToArray();
    }

    [Benchmark]
    public void RunBaseline()
    {
        for (var i = 0; i < data.Length; i++)
        {
            min = Baseline.Min(min, data[i]);
            max = Baseline.Max(max, data[i]);
        }
    }

    [Benchmark]
    public void RunComparison()
    {
        for (var i = 0; i < data.Length; i++)
        {
            min = Comparison.Min(min, data[i]);
            max = Comparison.Max(max, data[i]);
        }
    }

    [Benchmark]
    public void RunOptimized()
    {
        for (var i = 0; i < data.Length; i++)
        {
            min = Optimized.Min(min, data[i]);
            max = Optimized.Max(max, data[i]);
        }
    }
}

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

// See https://github.com/buybackoff/1brc/blob/59b42ec3493a50a4f9e3c3ab1e470f98bff8c26a/1brc/Summary.cs#L41
internal static class Optimized
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Min(int x, int y)
    {
        var delta = x - y;
        return y + (delta & (delta >> 31));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Max(int x, int y)
    {
        var delta = x - y;
        return x - (delta & (delta >> 31));
    }
}