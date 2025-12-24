using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RiverBooks.Books.Data;
using Serilog;

namespace RiverBooks.Books;

public static class BooksServiceExtensions
{
    public static void AddBookServices(this WebApplicationBuilder builder, ILogger logger)
    {
        builder.AddNpgsqlDbContext<BooksDbContext>("books-db");
        builder.Services.AddScoped<IBookRepository, EfBookRepository>();
        builder.Services.AddScoped<IBookService, BookService>();

        logger.Information("{Module} module services have been registered", "Books");
    }
}