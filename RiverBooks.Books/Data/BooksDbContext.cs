using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RiverBooks.Books.Domain;

namespace RiverBooks.Books.Data;

public class BooksDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    public BooksDbContext(DbContextOptions<BooksDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Books");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>()
            .HavePrecision(18, 6);
    }
}