using OrderService.Core.Events;
using OrderService.Core.Ports;

namespace OrderService.Core;

public class PublishOrderSubmittedIntegrationEventHandler(IMessageBroker messageBroker) 
    : IEventHandler<OrderSubmittedEvent>
{
    public Task Handle(OrderSubmittedEvent @event)
    {
        return messageBroker.PublishOrderSubmittedMessage(@event);
    }
}