using System.Collections.Concurrent;

public class InMemorySubscriptions : ISubscriptionsStorage
{
    private readonly IDictionary<SubscriptionIdentifier, Subscription> subscriptions =
        new ConcurrentDictionary<SubscriptionIdentifier, Subscription>();

    public Subscription GetSubscription(SubscriptionIdentifier subscriptionIdentifier)
    {
        if (!subscriptions.TryGetValue(subscriptionIdentifier, out Subscription? value))
        {
            value = new Subscription(subscriptionIdentifier);
            subscriptions[subscriptionIdentifier] = value;
        }

        return value;
    }
}