namespace OrderService.EventsContracts;

public record OrderLine
{
    public string ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Amount { get; set; }
    public string ProductName { get; set; }
}
