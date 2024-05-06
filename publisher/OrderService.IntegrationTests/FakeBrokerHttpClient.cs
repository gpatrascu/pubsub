using System.Text.Json;
using OrderService.Infrastructure.PubSub;

namespace OrderService.IntegrationTests;

public class FakeBrokerHttpClient : IBrokerHttpClient
{
    public static readonly FakeBrokerHttpClient Instance = new();
    private readonly IDictionary<string, IList<string>> topics = new Dictionary<string, IList<string>>();

    private FakeBrokerHttpClient()
    {
    }

    public Task PublishAsync<T>(T message, string topicName)
    {
        if (!topics.ContainsKey(topicName)) topics[topicName] = new List<string>();
        topics[topicName].Add(JsonSerializer.Serialize(message));
        return Task.CompletedTask;
    }

    public IList<string> GetTopicMessages(string topicName)
    {
        return topics.TryGetValue(topicName, out var messages) ? messages : new List<string>();
    }

    public void ClearMessages()
    {
        topics.Clear();
    }
}