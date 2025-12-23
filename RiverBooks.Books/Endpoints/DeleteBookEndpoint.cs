using FastEndpoints;

namespace RiverBooks.Books.Endpoints;

public class DeleteBookEndpoint(
    IBookService bookService
) : Endpoint<DeleteBookRequest>
{
    public override void Configure()
    {
        Delete("/books/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteBookRequest req, CancellationToken ct)
    {
        await bookService.DeleteBookAsync(req.Id);

        await Send.NoContentAsync(ct);
    }
}