namespace RiverBooks.Users.Api.CartEndpoints;

public record CartResponse(
    List<CartItemDto> CartItems
);