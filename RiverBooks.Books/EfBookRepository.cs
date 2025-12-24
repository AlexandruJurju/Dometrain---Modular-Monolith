using Microsoft.EntityFrameworkCore;
using RiverBooks.Books.Data;
using RiverBooks.Books.Domain;

namespace RiverBooks.Books;

public class EfBookRepository(
    BooksDbContext dbContext
) : IBookRepository
{
    public async Task<Book?> GetByIdAsync(Guid id)
    {
        return await dbContext.Books.FirstOrDefaultAsync(book => book.Id == id);
    }

    public async Task<List<Book>> GetAllAsync()
    {
        var books = await dbContext.Books.ToListAsync();
        return books;
    }

    public async Task AddAsync(Book book)
    {
        await dbContext.Books.AddAsync(book);
    }

    public Task DeleteAsync(Book book)
    {
        dbContext.Remove(book);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}