using PubSub.Api;
using PubSub.Domain;

public record GetSubscriptionQuery(string Topic, string SubscriptionId);

public class GetSubscriptionsMessagesQueryHandler(
    ISubscriptionsStorage subscriptionsStorage,
    IMessageStorage messageStorage)
{
    public async IAsyncEnumerable<PubSubMessage> Handle(GetSubscriptionQuery getSubscriptionQuery,
        CancellationToken cancellationToken)
    {
        var subscription =
            subscriptionsStorage.GetSubscription(new SubscriptionIdentifier(getSubscriptionQuery.Topic,
                getSubscriptionQuery.SubscriptionId));

        await foreach (var pubSubMessage in
                       messageStorage.StreamMessages(getSubscriptionQuery.Topic, subscription.OffSet, cancellationToken))
        {
            subscription.OffSet++;
            yield return pubSubMessage;
        }
    }
}