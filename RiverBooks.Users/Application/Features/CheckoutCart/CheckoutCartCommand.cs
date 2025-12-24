using Ardalis.Result;
using MediatR;

namespace RiverBooks.Users.Application.Features.CheckoutCart;

public record CheckoutCartCommand(
    string EmailAddress,
    Guid ShippingAddressId,
    Guid BillingAddressId
) : IRequest<Result<Guid>>;