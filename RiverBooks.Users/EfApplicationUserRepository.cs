using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Users;

public class EfApplicationUserRepository(
    UsersDbContext dbContext
) : IApplicationUserRepository
{
    public async Task<ApplicationUser?> GetUserWithCartByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await dbContext.ApplicationUsers
            .Include(x => x.CartItems)
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

        return user;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}