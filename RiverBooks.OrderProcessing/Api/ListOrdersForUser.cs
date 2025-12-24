using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using RiverBooks.OrderProcessing.Application.Features.ListOrdersForUser;

namespace RiverBooks.OrderProcessing.Api;

public class ListOrdersForUser(
    IMediator mediator
) : EndpointWithoutRequest<ListOrderForUserResponse>
{
    public override void Configure()
    {
        Get("/orders");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var emailAddress = User.FindFirstValue("EmailAddress");

        var query = new ListOrdersForUserQuery(emailAddress!);

        var result = await mediator.Send(query, ct);

        if (result.Status == ResultStatus.NotFound)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(new ListOrderForUserResponse(result.Value), ct);
    }
}