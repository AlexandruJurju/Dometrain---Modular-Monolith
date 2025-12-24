var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var booksDb = postgres.AddDatabase("books-db");
var usersDb = postgres.AddDatabase("users-db");
var orderProcessingDb= postgres.AddDatabase("order-processing-db");

builder.AddProject<Projects.RiverBooks_Web>("riverbooks-web")
    .WithReference(usersDb)
    .WaitFor(usersDb)
    .WithReference(orderProcessingDb)
    .WaitFor(orderProcessingDb)
    .WithReference(booksDb)
    .WaitFor(booksDb);

builder.Build().Run();
