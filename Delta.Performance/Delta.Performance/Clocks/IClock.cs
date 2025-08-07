namespace Delta.Performance.Clocks
{
    // Adapted from https://github.com/dotnet/BenchmarkDotNet/blob/master/src/BenchmarkDotNet.Core/Horology/IClock.cs
    internal interface IClock
    {
        Frequency Frequency { get; }
        long GetTimestamp();
    }
}
