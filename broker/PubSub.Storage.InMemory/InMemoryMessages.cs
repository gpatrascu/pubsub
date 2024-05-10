using System.Collections.Concurrent;
using PubSub.Api;
using PubSub.Domain;

namespace PubSub.Storage.InMemory;

public class InMemoryMessages : IMessageStorage
{
    private readonly IDictionary<string, List<PubSubMessage>>
        messages = new ConcurrentDictionary<string, List<PubSubMessage>>();

    public async Task AddMessage(string topic, PubSubMessage message)
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
            if (messages.ContainsKey(topic) == false)
            {
                await Task.Delay(1000, cancellationToken);
                continue;
            }
            
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