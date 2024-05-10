using OrderService.Core.Ports;
using OrderService.Core.SubmitOrder;
using OrderService.EventsContracts;

namespace OrderService.Infrastructure.PubSub;

public class MessageBroker(IBrokerHttpClient brokerHttpClient) : IMessageBroker
{
    public async Task PublishOrderSubmittedToOtherServices(OrderSubmittedEvent message)
    {
        await brokerHttpClient.PublishAsync(MapToIntegrationEvent(message), "order-submitted");
    }

    private static OrderSubmittedIntegrationEvent MapToIntegrationEvent(OrderSubmittedEvent message)
    {
        return new OrderSubmittedIntegrationEvent
        {
            OrderId = message.OrderId,
            CustomerId = message.CustomerId,
            OrderLines = message.OrderLines.Select(ol => new EventsContracts.OrderLine
            {
                ProductId = ol.Product.Id,
                ProductName = ol.Product.Name,
                Amount = ol.Product.Price.Amount,
                Currency = ol.Product.Price.Currency,
                Quantity = ol.Quantity
            }).ToList()
        };
    }
}