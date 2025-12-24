using Microsoft.EntityFrameworkCore;
using RiverBooks.OrderProcessing.Domain;

namespace RiverBooks.OrderProcessing.Infrastructure;

public class EfOrderRepository(
    OrdersDbContext dbContext
) : IOrderRepository
{
    public async Task<List<Order>> GetAllAsync()
    {
        return await dbContext.Orders
            .Include(o => o.OrderItems)
            .ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await dbContext.Orders
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await dbContext
            .SaveChangesAsync();
    }
}