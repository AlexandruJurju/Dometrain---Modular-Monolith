using FastEndpoints;

namespace RiverBooks.Books.Endpoints;

public class GetBookByIdEndpoint(
    IBookService bookService
) : Endpoint<GetBookByIdRequest, BookDto>
{
    public override void Configure()
    {
        Get("/books/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetBookByIdRequest req, CancellationToken ct)
    {
        var book = await bookService.GetBookByIdAsync(req.Id);

        if (book is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(book, ct);
    }
}