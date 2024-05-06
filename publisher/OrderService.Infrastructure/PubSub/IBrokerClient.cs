namespace OrderService.Infrastructure.PubSub;

public interface IBrokerHttpClient
{
    public Task PublishAsync<T>(T message, string topicName);
}