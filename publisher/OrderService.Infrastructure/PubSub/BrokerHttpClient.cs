namespace OrderService.Infrastructure.PubSub;

public class BrokerHttpClient(HttpClient httpClient) : IBrokerHttpClient
{
    public Task PublishAsync<T>(T message, string topicName)
    {
        throw new NotImplementedException();
    }
}