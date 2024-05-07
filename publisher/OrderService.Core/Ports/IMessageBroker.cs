using OrderService.Core.SubmitOrder;

namespace OrderService.Core.Ports;

public interface IMessageBroker
{
    public Task PublishOrderSubmittedToOtherServices(OrderSubmittedEvent message);
}