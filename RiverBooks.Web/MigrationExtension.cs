using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Web;

public static class MigrationExtensions
{
    public static void ApplyMigrations<TDb>(this IApplicationBuilder app) where TDb : DbContext
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using TDb dbContext =
            scope.ServiceProvider.GetRequiredService<TDb>();

        dbContext.Database.Migrate();
    }
}