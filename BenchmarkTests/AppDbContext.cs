using Bogus;
using Microsoft.EntityFrameworkCore;

namespace BenchmarkTests
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=benchmark_tests;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                var fakeUser = new Faker<User>("en")
               .RuleFor(u => u.Id, f => f.Random.Guid())
               .RuleFor(u => u.FirstName, f => f.Person.FirstName)
               .RuleFor(u => u.LastName, f => f.Person.LastName)
               .RuleFor(u => u.UserName, f => f.Person.UserName)
               .RuleFor(u => u.Avatar, f => f.Person.Avatar)
               .RuleFor(u => u.Email, f => f.Person.Email)
               .RuleFor(u => u.DateOfBirth, f => f.Person.DateOfBirth)
               .RuleFor(u => u.Street, f => f.Person.Address.Street)
               .RuleFor(u => u.Suite, f => f.Person.Address.Suite)
               .RuleFor(u => u.City, f => f.Person.Address.City)
               .RuleFor(u => u.Phone, f => f.Person.Phone)
               .RuleFor(u => u.CompanyName, f => f.Person.Company.Name);

                var users = fakeUser.Generate(10_000);

                builder.HasData(users);
            });
        }
    }
}
