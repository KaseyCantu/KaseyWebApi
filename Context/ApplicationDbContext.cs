using KaseyWebApi.DataModel;
using Microsoft.EntityFrameworkCore;

namespace KaseyWebApi.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Link>? Links { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region LinkSeed

        modelBuilder.Entity<Link>().HasData(
            new Link
            {
                Id = Guid.NewGuid().ToString(),
                LinkUrl = "https://google.com",
                Description = "Search Engine",
                Topic = "Learning",
                CreatedAt = DateTime.Now.ToString()
            });

        #endregion
    }
}