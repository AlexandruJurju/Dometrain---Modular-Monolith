using RiverBooks.Users.Domain;

namespace RiverBooks.Users.Infrastructure;

public interface IApplicationUserRepository
{
    Task<ApplicationUser?> GetUserWithCartByEmailAsync(string email, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}