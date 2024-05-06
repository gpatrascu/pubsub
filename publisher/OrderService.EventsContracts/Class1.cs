namespace OrderService.EventsContracts;

public record OrderSubmittedIntegrationEvent
{
    public string OrderId { get; }
}
