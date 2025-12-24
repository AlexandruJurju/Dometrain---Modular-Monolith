using Ardalis.Result;
using MediatR;
using RiverBooks.OrderProcessing.Api;
using RiverBooks.OrderProcessing.Domain;

namespace RiverBooks.OrderProcessing.Application.Features.ListOrdersForUser;

public record ListOrdersForUserQuery(
    string EmailAddress
) : IRequest<Result<List<OrderSummary>>>;