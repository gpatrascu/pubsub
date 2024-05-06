public interface IOrderRepository
{
    Order GetOrderById(string orderId);
    void Add(Order order);
    Task CommitAndSendEvents(IList<IDomainEvent> orderEvents);
}