using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.Features.AddItemToCart;

namespace RiverBooks.Users.CartEndpoints;

public class AddItem(
    IMediator mediator
) : Endpoint<AddCartItemRequest>
{
    public override void Configure()
    {
        Post("/api/cart");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(AddCartItemRequest req, CancellationToken ct)
    {
        var emailAddress = User.FindFirstValue("EmailAddress");

        var command = new AddItemToCartCommand(req.BookId, req.Quantity, emailAddress!);

        var result = await mediator.Send(command, ct);

        if (result.Status == ResultStatus.Unauthorized)
        {
            await Send.UnauthorizedAsync(ct);
        }

        await Send.OkAsync(cancellation: ct);
    }
}