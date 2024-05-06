using PubSub.Api;
using PubSub.Domain;

public record GetTopicMessagesQuery(string Topic);

public class GetTopicMessagesHandler(IMessageStorage messageRepository)
{
    public Task<IEnumerable<PubSubMessage>> Handle(GetTopicMessagesQuery query)
    {
        return messageRepository.ReadMessages(query.Topic);
    }
}