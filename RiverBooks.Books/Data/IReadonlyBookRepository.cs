using RiverBooks.Books.Domain;

namespace RiverBooks.Books.Data;

public interface IReadonlyBookRepository
{
    Task<Book?> GetByIdAsync(Guid id);
    Task<List<Book>> GetAllAsync();
}