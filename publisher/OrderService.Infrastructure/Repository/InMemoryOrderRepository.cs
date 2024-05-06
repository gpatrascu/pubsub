using System.Collections.Concurrent;

public class InMemoryOrderRepository(EventPublisher eventPublisher) : IOrderRepository
{
    private readonly IDictionary<string, Order> orders = new ConcurrentDictionary<string, Order>();

    public Order GetOrderById(string orderId)
    {
        orders.TryGetValue(orderId, out var order);
        return order;
    }

    public void Add(Order order)
    {
        orders[order.Id] = order;
    }

    public async Task CommitAndSendEvents(IList<IDomainEvent> domainEvents)
    {
        await Commit();
        foreach (var domainEvent in domainEvents)
        {
            await eventPublisher.PublishDomainEvent(domainEvent);
        }
    }

    public Task Commit()
    {
        return Task.CompletedTask;
        // nothing in this implementation. In a real-world scenario, this would persist the changes to a database.
    }
}