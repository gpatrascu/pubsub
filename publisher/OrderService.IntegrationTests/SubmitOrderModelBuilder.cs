using OrderService.WebApi.Models;

namespace OrderService.IntegrationTests;

public class SubmitOrderModelBuilder
{
    public SubmitOrderModel Model { get; } = new SubmitOrderModel();

    public SubmitOrderModelBuilder WithCustomerId(string customerId)
    {
        Model.CustomerId = customerId;
        return this;
    }

    public SubmitOrderModelBuilder WithOrderLine(string productId, int quantity)
    {
        Model.OrderLines.Add(new OrderLineModel
        {
            ProductId = productId,
            Quantity = quantity
        });
        return this;
    }
}