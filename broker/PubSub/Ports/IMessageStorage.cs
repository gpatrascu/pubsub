using PubSub.Domain;

namespace PubSub.Api;

public interface IMessageStorage
{
    Task AddMessage(string topic, PubSubMessage message);
    Task<IEnumerable<PubSubMessage>> ReadMessages(string topic, int offSet = 0);
    IAsyncEnumerable<PubSubMessage> StreamMessages(string topic, int offSet, CancellationToken cancellationToken);
    void RemoveTopic(string topic);
}