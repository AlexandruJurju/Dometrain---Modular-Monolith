using Ardalis.Result;
using MediatR;
using RiverBooks.OrderProcessing.Contracts;
using RiverBooks.Users.Infrastructure;

namespace RiverBooks.Users.Application.Features.CheckoutCart;

public class CheckoutCartCommandHandler(
    IApplicationUserRepository userRepository,
    IMediator mediator
) : IRequestHandler<CheckoutCartCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CheckoutCartCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserWithCartByEmailAsync(request.EmailAddress, cancellationToken);

        if (user is null)
        {
            return Result.Unauthorized();
        }

        var items = user.CartItems.Select(item =>
                new OrderItemDetails(item.BookId,
                    item.Quantity,
                    item.UnitPrice,
                    item.Description))
            .ToList();

        var createOrderCommand = new CreateOrderCommand(Guid.Parse(user.Id),
            request.ShippingAddressId,
            request.BillingAddressId,
            items);

        // TODO: Consider replacing with a message-based approach for perf reasons
        var result = await mediator.Send(createOrderCommand); // synchronous

        if (!result.IsSuccess)
        {
            // Change from a Result<OrderDetailsResponse> to Result<Guid>
            return result.Map(x => x.OrderId);
        }

        user.ClearCart();
        await userRepository.SaveChangesAsync();

        return Result.Success(result.Value.OrderId);
    }
}