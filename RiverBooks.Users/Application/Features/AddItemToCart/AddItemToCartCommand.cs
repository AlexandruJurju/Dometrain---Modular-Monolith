using Ardalis.Result;
using MediatR;

namespace RiverBooks.Users.Application.Features.AddItemToCart;

public record AddItemToCartCommand(
    Guid BookId,
    int Quantity,
    string EmailAddress
) : IRequest<Result>;