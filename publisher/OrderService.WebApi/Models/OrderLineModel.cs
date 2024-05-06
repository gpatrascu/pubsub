namespace OrderService.WebApi.Models;

public record OrderLineModel
{
    public string ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
}