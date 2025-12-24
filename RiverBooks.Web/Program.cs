using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using RiverBooks.Books;
using RiverBooks.Books.Data;
using RiverBooks.ServiceDefaults;
using RiverBooks.Users;
using RiverBooks.Web;
using Scalar.AspNetCore;
using Serilog;

var logger = Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

logger.Information("Starting web host");

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, config) =>
    config.ReadFrom.Configuration(builder.Configuration));

builder.AddServiceDefaults();
builder.Services.AddOpenApi();

builder.Services.AddFastEndpoints()
    .AddAuthenticationJwtBearer(signing =>
        signing.SigningKey = builder.Configuration["Auth:JwtSecret"]
    )
    .AddAuthorization()
    .AddSwaggerDocument();

builder.AddBookServices(logger);
builder.AddUsersServices(logger);

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.ApplyMigrations<BooksDbContext>();
    app.ApplyMigrations<UsersDbContext>();
}

app.UseAuthentication()
    .UseAuthorization();

app.UseFastEndpoints()
    .UseSwaggerGen();

app.Run();