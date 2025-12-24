using FastEndpoints;

namespace RiverBooks.Books.Endpoints;

public class DeleteBookEndpoint(
    IBookService bookService
) : EndpointWithoutRequest<DeleteBookRequest>
{
    public override void Configure()
    {
        Delete("api/books/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");

        await bookService.DeleteBookAsync(id);

        await Send.NoContentAsync(ct);
    }
}