using Ardalis.Result;
using MediatR;

namespace RiverBooks.Users.Features.AddItemToCart;

public record AddItemToCartCommand(
    Guid BookId,
    int Quantity,
    string EmailAddress
) : IRequest<Result>;