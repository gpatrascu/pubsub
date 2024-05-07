namespace OrderService.Core.SubmitOrder;

public class SubmitOrderCommandHandler(IOrderRepository repository)
{
    public async Task<Order> Handle(SubmitOrderCommand submitOrderCommand)
    {
        var order = new Order(submitOrderCommand.CustomerId);
        order.AddLineItems(submitOrderCommand.OrderLines);
        order.Submit();
        
        repository.Add(order);
        await repository.CommitAndSendEvents(order.Events);
        return order;
    }
}