using Ardalis.Result;
using MediatR;
using RiverBooks.OrderProcessing.Api;
using RiverBooks.OrderProcessing.Infrastructure;

namespace RiverBooks.OrderProcessing.Application.Features.ListOrdersForUser;

public class ListOrdersForUserQueryHandler(
    IOrderRepository orderRepository
) : IRequestHandler<ListOrdersForUserQuery, Result<List<OrderSummary>>>
{
    public async Task<Result<List<OrderSummary>>> Handle(ListOrdersForUserQuery request, CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetAllAsync();

        var summaries = orders
            .Select(order => new OrderSummary(
                order.UserId,
                order.Id,
                order.DateCreated,
                null,
                order.Total))
            .ToList();

        return summaries;
    }
}