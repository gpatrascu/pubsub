namespace OrderService.EventsContracts;

public record OrderSubmittedIntegrationEvent
{
    public string OrderId { get; init; }
    public string CustomerId { get; set; }
    public List<OrderLine> OrderLines { get; set; }
}