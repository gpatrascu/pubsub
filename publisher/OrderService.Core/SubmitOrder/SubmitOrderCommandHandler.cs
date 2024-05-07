using OrderService.Core.Ports;

namespace OrderService.Core.SubmitOrder;

public class SubmitOrderCommandHandler(IOrderRepository repository, IProductCatalog productCatalog)
{
    public async Task<Order> Handle(SubmitOrderCommand submitOrderCommand)
    {
        var order = new Order(submitOrderCommand.CustomerId);
        var productsFromCatalog = await GetProductsFromCatalog(submitOrderCommand);
        order.AddLineItems(submitOrderCommand.OrderLines, productsFromCatalog);
            
        order.Submit();
        
        repository.Add(order);
        await repository.CommitAndSendEvents(order.Events);
        return order;
    }

    private async Task<IList<Product>> GetProductsFromCatalog(SubmitOrderCommand submitOrderCommand)
    {
        return await productCatalog.GetProducts(submitOrderCommand.OrderLines.Select(ol => ol.ProductId).ToList());
    }
}