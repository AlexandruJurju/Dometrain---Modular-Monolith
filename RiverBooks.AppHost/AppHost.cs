var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var books = postgres.AddDatabase("books-db");

builder.AddProject<Projects.RiverBooks_Web>("riverbooks-web")
    .WithReference(books)
    .WaitFor(books);

builder.Build().Run();
