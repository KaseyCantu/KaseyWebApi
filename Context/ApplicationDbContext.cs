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
        // Employee DB schema
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeId");
            entity.Property(e => e.NationalIdNumber).HasMaxLength(25).IsUnicode(false);
            entity.Property(e => e.EmployeeName).HasMaxLength(100).IsUnicode(false).IsRequired();
            entity.Property(e => e.LoginId).HasMaxLength(256).IsUnicode(false);
            entity.Property(e => e.JobTitle).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.BirthDate).IsUnicode(false).IsRequired();
            entity.Property(e => e.MaritalStatus).HasMaxLength(1).IsUnicode(false);
            entity.Property(e => e.Gender).HasMaxLength(1).IsUnicode(false);
            entity.Property(e => e.HireDate).IsUnicode(false);
            entity.Property(e => e.VacationHours).IsUnicode(false);
            entity.Property(e => e.SickLeaveHours).IsUnicode(false);
            entity.Property(e => e.RowGuid).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.ModifiedDate).IsUnicode(false);
        });

        // Link DB schema
        modelBuilder.Entity<Link>(entity =>
        {
            entity.Property(l => l.LinkId).HasColumnName("LinkId");
            entity.Property(l => l.LinkUrl).IsUnicode(false).IsRequired();
            entity.Property(l => l.Description).HasMaxLength(250).IsUnicode(false).IsRequired();
            entity.Property(l => l.Topic).IsUnicode(false).IsRequired();
            entity.Property(l => l.CreatedAt).IsUnicode(false);
        });

        // UserInfo DB schema
        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.Property(e => e.UserId).HasColumnName("UserId");
            entity.Property(e => e.DisplayName).HasMaxLength(60).IsUnicode(false);
            entity.Property(e => e.UserName).HasMaxLength(30).IsUnicode(false).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(50).IsUnicode(false).IsRequired();
            entity.Property(e => e.Password).HasMaxLength(20).IsUnicode(false).IsRequired();
            entity.Property(e => e.CreatedDate).IsUnicode(false);
        });

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
                    LinkId = Guid.NewGuid().ToString(),
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