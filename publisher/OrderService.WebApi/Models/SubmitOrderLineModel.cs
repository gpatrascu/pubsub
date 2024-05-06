namespace OrderService.WebApi.Models;

public record SubmitOrderLineModel
{
    public string ProductId { get; set; }
    public int Quantity { get; set; }
}