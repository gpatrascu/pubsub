using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderService.Infrastructure.PubSub;
using OrderService.IntegrationTests;

namespace PubSub.Api.Tests;

public class TestWebApplicationFactory
    : WebApplicationFactory<Program>
{
    public static readonly TestWebApplicationFactory Instance = new();

    private TestWebApplicationFactory()
    {
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddSingleton<IBrokerHttpClient>(FakeBrokerHttpClient.Instance);
        });
        return base.CreateHost(builder);
    }
}