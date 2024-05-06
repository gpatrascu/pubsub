public class GetOrderByIdHandler(IOrderRepository orderRepository)
{
    IOrderRepository orderRepository = orderRepository;

    public Order Handle(GetOrderByIdQuery getOrderByIdQuery)
    {
        return orderRepository.GetOrderById(getOrderByIdQuery.OrderId);
    }
}