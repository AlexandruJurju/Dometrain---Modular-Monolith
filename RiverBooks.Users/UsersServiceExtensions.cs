using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace RiverBooks.Users;

public static class UsersServiceExtensions
{
    public static void AddUsersServices(this WebApplicationBuilder builder, ILogger logger)
    {
        builder.AddNpgsqlDbContext<UsersDbContext>("users-db");

        builder.Services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<UsersDbContext>();

        logger.Information("{Module} module services have been registered", "Users");
    }
}