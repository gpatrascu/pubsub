using PubSub.Api;
using PubSub.Domain;

public record PublishNewMessageCommand(string Topic, PubSubMessage Message);

public class PublishNewMessageCommandHandler(IMessageStorage messageStorage)
{
    public void Handle(PublishNewMessageCommand publishNewMessageCommand)
    {
        messageStorage.AddMessage(publishNewMessageCommand.Topic, publishNewMessageCommand.Message);
    }
}