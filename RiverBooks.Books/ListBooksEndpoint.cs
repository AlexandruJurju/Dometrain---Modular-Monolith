using FastEndpoints;

namespace RiverBooks.Books;

internal class ListBooksEndpoint(
    IBookService bookService
) : EndpointWithoutRequest<ListBooksResponse>
{
    public override void Configure()
    {
        Get("/api/books");
        AllowAnonymous();
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        return Task.FromResult(new ListBooksResponse
        {
            Books = bookService.ListBooks()
        });
    }
}