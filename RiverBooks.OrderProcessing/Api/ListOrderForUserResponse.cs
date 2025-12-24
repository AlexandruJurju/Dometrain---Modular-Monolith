namespace RiverBooks.OrderProcessing.Api;

public record ListOrderForUserResponse(
    List<OrderSummary> Orders
);