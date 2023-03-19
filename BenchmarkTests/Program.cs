using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace BenchmarkTests;

[MemoryDiagnoser]
public class Program
{
    static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<ProcessBenchmark>();
    }
}

public class ProcessBenchmark
{
    [Benchmark]
    public List<User> GetAllUsers()
    {
        AppDbContext appDbContext = new();
        Repository<User> repository = new(appDbContext);
        
        return repository.GetAllAsync(x => true).GetAwaiter().GetResult().ToList();
    }
}