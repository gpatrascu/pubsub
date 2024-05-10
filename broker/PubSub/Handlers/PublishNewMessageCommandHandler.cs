using PubSub.Api;
using PubSub.Domain;

public record PublishNewMessageCommand(string Topic, PubSubMessage Message);

public class PublishNewMessageCommandHandler(IMessageStorage messageStorage)
{
    public async Task Handle(PublishNewMessageCommand publishNewMessageCommand)
    {
        await messageStorage.AddMessage(publishNewMessageCommand.Topic, publishNewMessageCommand.Message);
    }
}