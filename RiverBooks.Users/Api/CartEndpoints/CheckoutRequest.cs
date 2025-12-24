namespace RiverBooks.Users.Api.CartEndpoints;

public record CheckoutRequest(
    Guid ShippingAddressId,
    Guid BillingAddressId
);