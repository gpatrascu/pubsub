using PubSub.Api;

public record DeleteTopicCommand(string Topic);

public class DeleteTopicHandler(IMessageStorage messageStorage)
{
    public void Handle(DeleteTopicCommand deleteTopicCommand)
    {
        messageStorage.RemoveTopic(deleteTopicCommand.Topic);
    }
}