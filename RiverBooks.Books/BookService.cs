using RiverBooks.Books.Domain;

namespace RiverBooks.Books;

internal class BookService(
    IBookRepository bookRepository
) : IBookService
{
    public async Task<List<BookDto>> ListBooksAsync()
    {
        var books = (await bookRepository.GetAllAsync())
            .Select(book => new BookDto(book.Id, book.Title, book.Author, book.Price))
            .ToList();

        return books;
    }

    public async Task<BookDto?> GetBookByIdAsync(Guid id)
    {
        var book = await bookRepository.GetByIdAsync(id);

        return book is not null ? new BookDto(book.Id, book.Title, book.Author, book.Price) : null;
    }

    public async Task CreateBookAsync(BookDto newBook)
    {
        var book = new Book(newBook.Id, newBook.Title, newBook.Author, newBook.Price);

        await bookRepository.AddAsync(book);
        await bookRepository.SaveChangesAsync();
    }

    public async Task DeleteBookAsync(Guid id)
    {
        var book = await bookRepository.GetByIdAsync(id);

        if (book is not null)
        {
            await bookRepository.DeleteAsync(book);
            await bookRepository.SaveChangesAsync();
        }

        throw new NotImplementedException();
    }

    public async Task UpdateBookPriceAsync(Guid id, decimal newPrice)
    {
        var book = await bookRepository.GetByIdAsync(id);

        if (book is not null)
        {
            book.UpdatePrice(newPrice);
            await bookRepository.SaveChangesAsync();
        }

        throw new NotImplementedException();
    }
}