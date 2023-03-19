using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

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

public class ProcessBenchmarkGetAllUsers
{
    [Benchmark]
    public List<User> GetAllUsers()
    {
        AppDbContext appDbContext = new();
        Repository<User> repository = new(appDbContext);

        return repository.GetAllAsync(x => true).GetAwaiter().GetResult().ToList();
    }

    [Benchmark]
    public List<User> GetAllUsersV1()
    {
        AppDbContext appDbContext = new();

        return appDbContext.Users.ToList();
    }

    [Benchmark]
    public List<User> GetAllUsersV2()
    {
        AppDbContext appDbContext = new();

        return appDbContext.Users.FromSqlRaw("SELECT * FROM Users").ToList();
    }

    [Benchmark]
    public List<User> GetAllUsersV3()
    {
        AppDbContext appDbContext = new();

        return appDbContext.Users.Select(x => new
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            UserName = x.UserName,
            Avatar = x.Avatar,
            Email = x.Email,
            DateOfBirth = x.DateOfBirth,
            Street = x.Street,
            Suite = x.Suite,
            City = x.City,
            Phone = x.Phone,
            CompanyName = x.CompanyName
        }).ToList().Select(y => new User
        {
            Id = y.Id,
            FirstName = y.FirstName,
            LastName = y.LastName,
            UserName = y.UserName,
            Avatar = y.Avatar,
            Email = y.Email,
            DateOfBirth = y.DateOfBirth,
            Street = y.Street,
            Suite = y.Suite,
            City = y.City,
            Phone = y.Phone,
            CompanyName = y.CompanyName
        }).ToList();
    }

    [Benchmark]
    public List<User> GetAllUsersV4()
    {
        AppDbContext appDbContext = new();
        Repository<User> repository = new(appDbContext);

        return repository.GetAll(x => true).ToList();
    }
}

public class ProcessBenchmarkGetSingle
{
    private const string UserId = "01395d63-2c1f-207b-04d5-749a0fccc02f";

    [Benchmark]
    public User GetSingleAsync()
    {
        AppDbContext appDbContext = new();
        Repository<User> repository = new(appDbContext);

        return repository.GetSingleAsync(x => x.Id == Guid.Parse(UserId)).GetAwaiter().GetResult();
    }

    [Benchmark]
    public User GetSingle()
    {
        AppDbContext appDbContext = new();
        Repository<User> repository = new(appDbContext);

        return repository.GetSingle(x => x.Id == Guid.Parse(UserId));
    }

    [Benchmark]
    public User GetSingleV1()
    {
        AppDbContext appDbContext = new();

        return appDbContext.Users.Where(x=> x.Id == Guid.Parse(UserId)).FirstOrDefault()!;
    }

    [Benchmark]
    public User GetSingleV2()
    {
        AppDbContext appDbContext = new();

        return appDbContext.Users.FromSqlRaw($"SELECT * FROM Users WHERE Id = '{UserId}'").FirstOrDefault()!;
    }

    [Benchmark]
    public User GetSingleV2_1()
    {
        AppDbContext appDbContext = new();

        return appDbContext.Users.FromSqlRaw($"SELECT Id, FirstName, LastName, UserName, Avatar, Email, DateOfBirth, Street, Suite, City, Phone, CompanyName FROM Users WHERE Id = '{UserId}'").FirstOrDefault()!;
    }

    [Benchmark]
    public User GetSingleV2_2()
    {
        AppDbContext appDbContext = new();

        return appDbContext.Users.FromSqlRaw($"SELECT * FROM Users WHERE Id = '{UserId}'")
            .Select(x => new
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserName = x.UserName,
                Avatar = x.Avatar,
                Email = x.Email,
                DateOfBirth = x.DateOfBirth,
                Street = x.Street,
                Suite = x.Suite,
                City = x.City,
                Phone = x.Phone,
                CompanyName = x.CompanyName
            })
            .Select(y => new User
            {
                Id = y.Id,
                FirstName = y.FirstName,
                LastName = y.LastName,
                UserName = y.UserName,
                Avatar = y.Avatar,
                Email = y.Email,
                DateOfBirth = y.DateOfBirth,
                Street = y.Street,
                Suite = y.Suite,
                City = y.City,
                Phone = y.Phone,
                CompanyName = y.CompanyName
            })
            .FirstOrDefault()!;
    }

    [Benchmark]
    public User GetSingleV3()
    {
        AppDbContext appDbContext = new();

        return appDbContext.Users.FirstOrDefault(x => x.Id == Guid.Parse(UserId))!;
    }

    [Benchmark]
    public User GetSingleV4()
    {
        AppDbContext appDbContext = new();

        return appDbContext.Users
            .Where(x => x.Id == Guid.Parse(UserId))
            .Select(x => new
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            UserName = x.UserName,
            Avatar = x.Avatar,
            Email = x.Email,
            DateOfBirth = x.DateOfBirth,
            Street = x.Street,
            Suite = x.Suite,
            City = x.City,
            Phone = x.Phone,
            CompanyName = x.CompanyName
        }).Select(y => new User
        {
            Id = y.Id,
            FirstName = y.FirstName,
            LastName = y.LastName,
            UserName = y.UserName,
            Avatar = y.Avatar,
            Email = y.Email,
            DateOfBirth = y.DateOfBirth,
            Street = y.Street,
            Suite = y.Suite,
            City = y.City,
            Phone = y.Phone,
            CompanyName = y.CompanyName
        }).FirstOrDefault()!;
    }

    [Benchmark]
    public User GetSingleV5()
    {
        AppDbContext appDbContext = new();

        return appDbContext.Users
            .Select(x => new
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserName = x.UserName,
                Avatar = x.Avatar,
                Email = x.Email,
                DateOfBirth = x.DateOfBirth,
                Street = x.Street,
                Suite = x.Suite,
                City = x.City,
                Phone = x.Phone,
                CompanyName = x.CompanyName
            }).Select(y => new User
            {
                Id = y.Id,
                FirstName = y.FirstName,
                LastName = y.LastName,
                UserName = y.UserName,
                Avatar = y.Avatar,
                Email = y.Email,
                DateOfBirth = y.DateOfBirth,
                Street = y.Street,
                Suite = y.Suite,
                City = y.City,
                Phone = y.Phone,
                CompanyName = y.CompanyName
            }).FirstOrDefault(x => x.Id == Guid.Parse(UserId))!;
    }
}