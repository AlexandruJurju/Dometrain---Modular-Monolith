using Ardalis.Result;
using MediatR;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Infrastructure;

namespace RiverBooks.Users.Application.Features.AddItemToCart;

public class AddItemToCartCommandHandler(
    IApplicationUserRepository userRepository
) : IRequestHandler<AddItemToCartCommand, Result>
{
    public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserWithCartByEmailAsync(request.EmailAddress, cancellationToken);

        if (user == null)
        {
            return Result.Unauthorized();
        }

        var newCartItem = new CartItem(request.BookId, "DESCRIPTION", request.Quantity, 1.00m);

        user.AddItemToCart(newCartItem);

        await userRepository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}