using FastEndpoints;

namespace RiverBooks.Books.Endpoints;

public class CreateBookEndpoint(
    IBookService bookService
) : Endpoint<CreateBookRequest, Guid>
{
    public override void Configure()
    {
        Post("/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateBookRequest req, CancellationToken ct)
    {
        var newBookDto = new BookDto(
            req.Id ?? Guid.NewGuid(),
            req.Title,
            req.Author,
            req.Price
        );

        await bookService.CreateBookAsync(newBookDto);

        await Send.CreatedAtAsync<GetBookByIdEndpoint>(new { newBookDto.Id }, newBookDto.Id, cancellation: ct);
    }
}