using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;

namespace BenchmarkTests;

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