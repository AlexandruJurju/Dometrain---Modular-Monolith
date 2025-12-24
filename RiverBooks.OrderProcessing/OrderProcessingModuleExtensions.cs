using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RiverBooks.OrderProcessing.Infrastructure;
using Serilog;

namespace RiverBooks.OrderProcessing;

public static class OrderProcessingModuleExtensions
{
    public static void AddOrderProcessingServices(
        this WebApplicationBuilder builder,
        ILogger logger,
        List<Assembly> mediatrAssemblies)
    {
        builder.AddNpgsqlDbContext<OrdersDbContext>("order-processing-db");
        builder.Services.AddScoped<IOrderRepository, EfOrderRepository>();

        mediatrAssemblies.Add(typeof(OrderProcessingModuleExtensions).Assembly);

        logger.Information("{Module} module services have been registered", "OrderProcessing");
    }
}