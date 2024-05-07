namespace OrderService.EventsContracts;

public record OrderLine
{
    public string ProductId { get; set; }
    public int Quantity { get; set; }
}
