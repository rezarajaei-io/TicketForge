using Catalog.Api;
using Catalog.Api.Grpc;
using Catalog.Domain;
using Catalog.Infrastructure;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mongo2Go;
using MongoDB.Driver;
using Xunit;

namespace Catalog.IntegrationTest;

public class CatalogServiceTests
{
    // 1. یک کلاس داخلی برای ساختن سرور تست ما
    private class CatalogServiceApplicationFactory : WebApplicationFactory<Program>
    {
        // 2. یک نمونه استاتیک از Mongo2Go تا فقط یک بار برای تمام تست‌ها اجرا شود
        private static readonly MongoDbRunner _runner = MongoDbRunner.Start();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // 3. مهم: تنظیمات دیتابیس را در لحظه تغییر می‌دهیم
                // تا به سرور MongoDB موقت ما وصل شود، نه سرور محلی
                services.Configure<DatabaseSettings>(options =>
                {
                    options.ConnectionString = _runner.ConnectionString;
                    options.DatabaseName = "TestCatalogDb";
                    options.CollectionName = "Events";
                });
            });
        }

        // یک متد کمکی برای دسترسی به دیتابیس تست
        public IMongoCollection<Event> GetEventsCollection()
        {
            var client = new MongoClient(_runner.ConnectionString);
            var database = client.GetDatabase("TestCatalogDb");
            return database.GetCollection<Event>("Events");
        }
    }

    [Fact]
    public async Task GetAllEvents_WhenDatabaseHasEvents_ReturnsEvents()
    {
        // --- Arrange (آماده‌سازی) ---

        // یک نمونه از سرور تست خودمان را می‌سازیم
        await using var application = new CatalogServiceApplicationFactory();

        // یک رویداد تستی را مستقیماً در دیتابیس موقت خودمان درج می‌کنیم
        var eventsCollection = application.GetEventsCollection();
        var testEvent = new Event
        {
            Name = "Test Event",
            Venue = "Test Venue",
            EventDate = DateTime.UtcNow,
            AvailableTickets = 100
        };
        await eventsCollection.InsertOneAsync(testEvent);

        // یک کلاینت gRPC می‌سازیم که به سرور تست ما وصل شود
        using var channel = GrpcChannel.ForAddress(application.Server.BaseAddress, new GrpcChannelOptions
        {
            HttpClient = application.CreateClient()
        });
        var client = new CatalogService.CatalogServiceClient(channel);

        // --- Act (اجرای عملیات) ---

        // متد gRPC را صدا می‌زنیم
        var response = await client.GetAllEventsAsync(new GetAllEventsRequest());

        // --- Assert (بررسی نتایج) ---

        // بررسی می‌کنیم که پاسخ null نباشد
        Assert.NotNull(response);
        // باید دقیقاً یک رویداد در پاسخ وجود داشته باشد
        Assert.Single(response.Events);

        var returnedEvent = response.Events.First();
        // بررسی می‌کنیم که داده‌های برگشتی با داده‌های تستی ما مطابقت دارد
        Assert.Equal(testEvent.Name, returnedEvent.Name);
        Assert.Equal(testEvent.Venue, returnedEvent.Venue);
        Assert.Equal(testEvent.AvailableTickets, returnedEvent.AvailableTickets);
    }
}
