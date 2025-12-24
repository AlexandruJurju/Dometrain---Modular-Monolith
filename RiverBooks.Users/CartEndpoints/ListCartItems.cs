using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.Features.ListCartItems;

namespace RiverBooks.Users.CartEndpoints;

public class ListCartItems(
    IMediator mediator
) : EndpointWithoutRequest<CartResponse>
{
    public override void Configure()
    {
        Get("/api/cart");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var emailAddress = User.FindFirstValue("EmailAddress");

        var query = new ListCartItemsQuery(emailAddress!);

        var result = await mediator.Send(query, ct);

        if (result.Status == ResultStatus.Unauthorized)
        {
            await Send.UnauthorizedAsync(ct);
            return;
        }

        var response = new CartResponse(result.Value);

        await Send.OkAsync(response, ct);
    }
}