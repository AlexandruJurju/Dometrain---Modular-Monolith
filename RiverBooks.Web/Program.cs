using System.Reflection;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using RiverBooks.Books;
using RiverBooks.Books.Data;
using RiverBooks.ServiceDefaults;
using RiverBooks.Users;
using RiverBooks.Web;
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

List<Assembly> mediatRAssemblies = [typeof(Program).Assembly];
builder.AddBookServices(logger, mediatRAssemblies);
builder.AddUsersServices(logger,  mediatRAssemblies);

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies([.. mediatRAssemblies]));

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