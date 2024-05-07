using OrderService.Core.SubmitOrder;

public class Order(string customerId)
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public string CustomerId { get; } = customerId;
    public List<OrderLine> OrderLines { get; } = new();
    public List<IDomainEvent> Events { get; } = new();

    public void Submit()
    {
        Events.Add(new OrderSubmittedEvent
        {
            OrderId = Id,
            CustomerId = CustomerId,
            OrderLines = OrderLines
        });
    }

    public void AddLineItems(List<OrderLine> orderLines)
    {
        OrderLines.AddRange(orderLines);
    }
}