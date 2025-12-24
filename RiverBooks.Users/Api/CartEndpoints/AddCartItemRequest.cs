namespace RiverBooks.Users.Api.CartEndpoints;

public record AddCartItemRequest(
    Guid BookId,
    int Quantity
);