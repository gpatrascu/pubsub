using OrderService.Core.Ports;
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

    public void AddLineItems(List<OrderLineSubmitted> orderLines, IList<Product> productsFromCatalog)
    {
        var dictionary = productsFromCatalog.ToDictionary(product => product.Id);
        foreach (var orderLine in orderLines)
        {
            if (dictionary.TryGetValue(orderLine.ProductId, out var product))
            {
                OrderLines.Add(new OrderLine(product, orderLine.Quantity));
            }
        }
    }
}