using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace PubSub.Api.Tests;

public class TestWebApplicationFactory
    : WebApplicationFactory<Program>
{
    public static readonly TestWebApplicationFactory Instance = new TestWebApplicationFactory();

    private TestWebApplicationFactory()
    {
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services => { });

        return base.CreateHost(builder);
    }
}