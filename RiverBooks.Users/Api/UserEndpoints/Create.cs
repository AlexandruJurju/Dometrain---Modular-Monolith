using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using RiverBooks.Users.Domain;

namespace RiverBooks.Users.Api.UserEndpoints;

public class Create(
    UserManager<ApplicationUser> userManager
) : Endpoint<CreateUserRequest>
{
    public override void Configure()
    {
        Post("/api/users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
    {
        var newUser = new ApplicationUser
        {
            Email = req.Email,
            UserName = req.Email
        };

        await userManager.CreateAsync(newUser, req.Password);

        await Send.OkAsync(cancellation: ct);
    }
}