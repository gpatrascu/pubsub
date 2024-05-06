using OrderService.Core.Events;

namespace OrderService.Core.Ports;

public interface IMessageBroker
{
    public Task PublishOrderSubmittedMessage(OrderSubmittedEvent message);
}