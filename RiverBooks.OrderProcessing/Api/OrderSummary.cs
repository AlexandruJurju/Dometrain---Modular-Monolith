namespace RiverBooks.OrderProcessing.Api;

public record OrderSummary(
    Guid UserId,
    Guid OrderId,
    DateTimeOffset DateCreated,
    DateTimeOffset? DateShipped,
    decimal Total
);