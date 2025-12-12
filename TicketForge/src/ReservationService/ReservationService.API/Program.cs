using ReservationService.API.Services;
using ReservationService.Application.Features.Commands;
using ReservationService.Application.Interfaces;
using ReservationService.Application.Interfaces.RabbitMQ;
using ReservationService.Infrastructure.Messaging;
using ReservationService.Infrastructure.Persistence;
using ReservationService.Infrastructure.Proxies;
using ReservationService.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
// Add MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateReservationCommand).Assembly));

// Register Infrastructure Services
builder.Services.AddSingleton<IMessageBus, RabbitMqBus>();
builder.Services.AddScoped<IReservationRepository, InMemoryReservationRepository>();

// First, register the concrete, real service.
builder.Services.AddScoped<ReservationServiceReal>();
// injecting the real service into the proxy.
builder.Services.AddScoped<IReservationService, ReservationServiceProxy>(provider =>
{
    var realService = provider.GetRequiredService<ReservationServiceReal>();
    return new ReservationServiceProxy(realService);
});
// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ReservationerApiService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
