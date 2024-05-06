using OrderService.Core.Events;
using OrderService.EventsContracts;
using OrderService.Infrastructure.PubSub;

namespace OrderService.Core.Ports;

public class MessageBroker(IBrokerHttpClient brokerHttpClient) : IMessageBroker
{
    public async Task PublishOrderSubmittedMessage(OrderSubmittedEvent message)
    {
        await brokerHttpClient.PublishAsync(MapToIntegrationEvent(message), "order-submitted");
    }

    private static OrderSubmittedIntegrationEvent MapToIntegrationEvent(OrderSubmittedEvent message)
    {
        return new OrderSubmittedIntegrationEvent();
    }
}