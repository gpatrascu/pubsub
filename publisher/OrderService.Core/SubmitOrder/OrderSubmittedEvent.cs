namespace OrderService.Core.SubmitOrder;

public record OrderSubmittedEvent : IDomainEvent
{
    public string OrderId { get; set; }
    public string CustomerId { get; set; }
    public List<OrderLine> OrderLines { get; set; }
}