namespace OrderService.WebApi.Models;

public class SubmitOrderModel
{
    public List<OrderLineModel> OrderLines { get; set; } = [];
    public string CustomerId { get; set; }
}