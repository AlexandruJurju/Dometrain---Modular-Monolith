using Microsoft.EntityFrameworkCore;
using RiverBooks.Books.Data;

namespace RiverBooks.Web;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using BookDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<BookDbContext>();

        dbContext.Database.Migrate();
    }
}