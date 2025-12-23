using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RiverBooks.Books.Data;

namespace RiverBooks.Books;

public static class BookServiceExtensions
{
    public static void AddBookServices(this WebApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<BookDbContext>("books-db");
        builder.Services.AddScoped<IBookRepository, EfBookRepository>();
        builder.Services.AddScoped<IBookService, BookService>();
    }
}