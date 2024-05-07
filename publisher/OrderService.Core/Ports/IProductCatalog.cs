namespace OrderService.Core.Ports;

public interface IProductCatalog
{
    public Task<IList<Product>> GetProducts(IList<string> productIds);
}