using FastEndpoints;

namespace RiverBooks.Books.Endpoints;

public class GetBookByIdEndpoint(
    IBookService bookService
) : EndpointWithoutRequest<BookDto>
{
    public override void Configure()
    {
        Get("api/books/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");

        var book = await bookService.GetBookByIdAsync(id);

        if (book is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(book, ct);
    }
}