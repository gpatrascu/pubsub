using System.Collections.Concurrent;

namespace PubSub.Api;

public interface IMessageRepository
{
    void AddMessage(string topic, PubSubMessage message);
}

public class MessageRepository : IMessageRepository
{
    private readonly IDictionary<string, List<PubSubMessage>>
        messages = new ConcurrentDictionary<string, List<PubSubMessage>>();
    public void AddMessage(string topic, PubSubMessage message)
    {
        if (messages.TryGetValue(topic, out var topicMessages))
        {
            topicMessages.Add(message);
        }
        else
        {
            messages[topic] = [message];
        }
    }
}