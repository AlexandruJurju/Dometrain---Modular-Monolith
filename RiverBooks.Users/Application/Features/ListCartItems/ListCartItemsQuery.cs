using Ardalis.Result;
using MediatR;
using RiverBooks.Users.Api.CartEndpoints;

namespace RiverBooks.Users.Application.Features.ListCartItems;

public record ListCartItemsQuery(
    string EmailAddress
) : IRequest<Result<List<CartItemDto>>>;