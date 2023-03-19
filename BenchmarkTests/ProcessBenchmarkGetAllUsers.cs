using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;

namespace BenchmarkTests;

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
