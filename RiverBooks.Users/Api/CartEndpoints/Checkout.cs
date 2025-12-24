using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.Application.Features.CheckoutCart;

namespace RiverBooks.Users.Api.CartEndpoints;

public class Checkout(
    IMediator mediator
) : Endpoint<CheckoutRequest, CheckoutResponse>
{
    public override void Configure()
    {
        Post("/cart/checkout");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(CheckoutRequest request, CancellationToken cancellationToken)
    {
        var emailAddress = User.FindFirstValue("EmailAddress");

        var command = new CheckoutCartCommand(emailAddress!,
            request.ShippingAddressId,
            request.BillingAddressId);

        var result = await mediator.Send(command, cancellationToken);

        if (result.Status == ResultStatus.Unauthorized)
        {
            await Send.UnauthorizedAsync(cancellationToken);
        }
        else
        {
            await Send.OkAsync(new CheckoutResponse(result.Value), cancellationToken);
        }
    }
}