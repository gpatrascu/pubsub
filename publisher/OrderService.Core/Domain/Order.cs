using OrderService.Core.Events;

public class Order
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public string CustomerId { get; }
    public IList<OrderLine> OrderLines { get; } = new List<OrderLine>();
    public IList<IDomainEvent> Events { get; } = new List<IDomainEvent>();

    public void Submit()
    {
        Events.Add(new OrderSubmittedEvent());
    }
}