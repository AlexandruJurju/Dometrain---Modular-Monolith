using RiverBooks.Books.Domain;

namespace RiverBooks.Books.Data;

public interface IBookRepository : IReadonlyBookRepository
{
    Task AddAsync(Book book);
    Task DeleteAsync(Book book);
    Task SaveChangesAsync();
}