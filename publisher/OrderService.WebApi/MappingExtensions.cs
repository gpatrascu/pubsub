using OrderService.WebApi.Models;

public static class MappingExtensions
{
    public static SubmitOrderCommand ToCommand(this SubmitOrderModel submitOrderModel)
    {
        return new SubmitOrderCommand
        {
            CustomerId = submitOrderModel.CustomerId,
            OrderLines = submitOrderModel.OrderLines.Select(ol => new OrderLineSubmitted()
            {
                ProductId = ol.ProductId,
                Quantity = ol.Quantity
            }).ToList()
        };
    }
    
    public static OrderModel ToModel(this Order order)
    {
        return new OrderModel
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            OrderLines = order.OrderLines.Select(ol => new OrderLineModel
            {
                ProductId = ol.Product.Id,
                Quantity = ol.Quantity
            }).ToList()
        };
    }
}