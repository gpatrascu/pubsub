using System.Collections.Concurrent;
using PubSub.Domain;

namespace PubSub.Api;

public class InMemoryMessages : IMessageStorage
{
    private readonly IDictionary<string, List<PubSubMessage>>
        messages = new ConcurrentDictionary<string, List<PubSubMessage>>();

    public void AddMessage(string topic, PubSubMessage message)
    {
        if (messages.TryGetValue(topic, out var topicMessages))
            topicMessages.Add(message);
        else
            messages[topic] = [message];
    }

    public async Task<IEnumerable<PubSubMessage>> ReadMessages(string topic, int offSet)
    {
        return messages.TryGetValue(topic, out var topicMessages) ? topicMessages.Skip(offSet) : [];
    }

    public async IAsyncEnumerable<PubSubMessage> StreamMessages(string topic, int offSet, CancellationToken cancellationToken)
    {;
        while (cancellationToken.IsCancellationRequested == false)
        {
            if (offSet < messages[topic].Count)
            {
                yield return messages[topic][offSet];
                offSet++;
            }
            else
            {
                await Task.Delay(1000, cancellationToken);    
            }
        }
    }

    public void RemoveTopic(string topic)
    {
        messages.Remove(topic);
    }
}