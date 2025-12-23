namespace RiverBooks.Books.Endpoints;

public class ListBooksResponse
{
    public required List<BookDto> Books { get; set; }
}