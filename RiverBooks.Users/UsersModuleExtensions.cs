using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace RiverBooks.Users;

public static class UsersModuleExtensions
{
    public static void AddUsersServices(
        this WebApplicationBuilder builder,
        ILogger logger,
        List<Assembly> mediatrAssemblies)
    {
        builder.AddNpgsqlDbContext<UsersDbContext>("users-db");

        builder.Services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<UsersDbContext>();
        builder.Services.AddScoped<IApplicationUserRepository, EfApplicationUserRepository>();

        mediatrAssemblies.Add(typeof(UsersModuleExtensions).Assembly);

        logger.Information("{Module} module services have been registered", "Users");
    }
}