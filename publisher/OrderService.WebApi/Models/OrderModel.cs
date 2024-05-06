namespace OrderService.WebApi.Models;

public class OrderModel
{
    public string Id { get; set; }
    public List<OrderLineModel> OrderLines { get; set; } = [];
    public string CustomerId { get; set; }
}