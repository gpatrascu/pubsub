public class SubmitOrderCommandHandler(IOrderRepository repository)
{
    public async Task<Order> Handle(SubmitOrderCommand submitOrderCommand)
    {
        var order = new Order();
        order.Submit();
        
        repository.Add(order);
        await repository.CommitAndSendEvents(order.Events);
        return order;
    }
}