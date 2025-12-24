using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RiverBooks.Books.Data;
using Serilog;

namespace RiverBooks.Books;

public static class BooksModuleExtensions
{
    public static void AddBookServices(this WebApplicationBuilder builder, ILogger logger, List<Assembly> mediatrAssemblies)
    {
        builder.AddNpgsqlDbContext<BooksDbContext>("books-db");
        builder.Services.AddScoped<IBookRepository, EfBookRepository>();
        builder.Services.AddScoped<IBookService, BookService>();

        mediatrAssemblies.Add(typeof(BooksModuleExtensions).Assembly);

        logger.Information("{Module} module services have been registered", "Books");
    }
}