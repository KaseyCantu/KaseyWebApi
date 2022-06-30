using KaseyWebApi.DataModel;
using Microsoft.EntityFrameworkCore;

namespace KaseyWebApi.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Link> Links { get; set; }

    public DbSet<UserInfo> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region EmployeeSeed

        modelBuilder
            .Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeId = 9,
                    NationalIdNumber = "8989898989898989",
                    EmployeeName = "Kasey Cantu",
                    LoginId = "shipengine-kasey",
                    JobTitle = "Software Engieer",
                    BirthDate = new DateTime(1993, 6, 22).ToUniversalTime(),
                    MaritalStatus = "M",
                    Gender = "M",
                    HireDate = new DateTime(2018, 8, 9).ToUniversalTime(),
                    VacationHours = 500,
                    SickLeaveHours = 300,
                    RowGuid = Guid.NewGuid(),
                    ModifiedDate = new DateTime(2022, 5, 27).ToUniversalTime()
                });

        #endregion

        #region LinkSeed

        modelBuilder
            .Entity<Link>().HasData(
                new Link
                {
                    Id = Guid.NewGuid().ToString(),
                    LinkUrl = "https://google.com",
                    Description = "Search Engine",
                    Topic = "Learning",
                    CreatedAt = DateTime.Now.ToString()
                });

        #endregion

        #region UserSeed

        modelBuilder
            .Entity<UserInfo>().HasData(
                new UserInfo
                {
                    UserId = 7,
                    DisplayName = "KPC",
                    UserName = "KaseyPCantu",
                    Email = "kaseypaulcantu@gmail.com",
                    Password = "password321",
                    CreatedDate = DateTime.UtcNow
                });

        #endregion
    }
}