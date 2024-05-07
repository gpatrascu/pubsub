using System.Net.Http.Json;
using System.Text.Json;

namespace OrderService.Infrastructure.PubSub;

public class BrokerHttpClient(HttpClient httpClient) : IBrokerHttpClient
{
    public async Task PublishAsync<T>(T message, string topicName)
    {
        var serializedMessage = JsonSerializer.Serialize(message);
        var postAsJsonAsync = await httpClient.PostAsJsonAsync($"/topics/{topicName}/messages",
            new
            {
                Metadata = new Dictionary<string, string>()
                {
                    {"contract", typeof(T).Name}
                },
                Payload = serializedMessage
            });
        postAsJsonAsync.EnsureSuccessStatusCode();
    }
}