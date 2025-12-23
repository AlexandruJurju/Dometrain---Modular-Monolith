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

    public override async Task HandleAsync(CancellationToken ct)
    {
        var books = await bookService.ListBooksAsync();

        await Send.OkAsync(
            new ListBooksResponse { Books = books },
            cancellation: ct
        );
    }
}