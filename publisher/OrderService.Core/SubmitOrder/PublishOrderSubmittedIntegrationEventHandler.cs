using OrderService.Core.Ports;

namespace OrderService.Core.SubmitOrder;

public class PublishOrderSubmittedIntegrationEventHandler(IMessageBroker messageBroker) 
    : IEventHandler<OrderSubmittedEvent>
{
    public Task Handle(OrderSubmittedEvent @event)
    {
        return messageBroker.PublishOrderSubmittedToOtherServices(@event);
    }
}