using FastEndpoints;
using RiverBooks.Books;
using RiverBooks.ServiceDefaults;
using RiverBooks.Web;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddBookServices();
builder.Services.AddFastEndpoints();

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseFastEndpoints();

app.Run();