using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace BenchmarkTests;

[MemoryDiagnoser]
public class Program
{
    static void Main(string[] args)
    {
        //var summary = BenchmarkRunner.Run<ProcessBenchmarkGetAllUsers>();
        var summary = BenchmarkRunner.Run<ProcessBenchmarkGetSingle>();
    }
}
