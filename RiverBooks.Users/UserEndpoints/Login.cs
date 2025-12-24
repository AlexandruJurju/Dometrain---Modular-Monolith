using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;

namespace RiverBooks.Users.UserEndpoints;

public class Login(
    UserManager<ApplicationUser> userManager
) : Endpoint<UserLoginRequest>
{
    public override void Configure()
    {
        Post("/api/users/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UserLoginRequest req, CancellationToken ct)
    {
        var user = await userManager.FindByEmailAsync(req.Email);
        if (user == null)
        {
            await Send.UnauthorizedAsync(ct);
            return;
        }

        var loginResult = await userManager.CheckPasswordAsync(user, req.Password);
        if (!loginResult)
        {
            await Send.UnauthorizedAsync(ct);
            return;
        }

        var jwtSecret = Config["Auth:JwtSecret"]!;

        var token = JwtBearer.CreateToken(options =>
        {
            options.SigningKey = jwtSecret;
            options.ExpireAt = DateTime.UtcNow.AddHours(1);
        });

        await Send.OkAsync(token, ct);
    }
}