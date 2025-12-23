using RiverBooks.Books.Domain;

namespace RiverBooks.Books;

public interface IReadonlyBookRepository
{
    Task<Book?> GetByIdAsync(Guid id);
    Task<List<Book>> GetAllAsync();
}