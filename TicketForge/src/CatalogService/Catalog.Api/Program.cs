using Catalog.Api.Services;
using Catalog.Application;
using Catalog.Application.Queries;
using Catalog.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
// Database Configuration
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings")
);
// DB DI
builder.Services.AddSingleton<CatalogContext>();

builder.Services.AddSingleton<ICatalogContext, CatalogContext>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllEventsQuery).Assembly));

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<CatalogGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
