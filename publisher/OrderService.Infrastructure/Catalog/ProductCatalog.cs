using OrderService.Infrastructure.Catalog;

namespace OrderService.Core.Ports;

public class ProductCatalog(ICatalogHttpClient catalogHttpClient) : IProductCatalog
{
    public async Task<IList<Product>> GetProducts(IList<string> productIds)
    {
        var catalogProductModels = await catalogHttpClient.GetProducts(productIds);
        return catalogProductModels.Select(p => 
                new Product(p.ProductId, p.Name, new Money(p.Currency, p.Amount)))
            .ToList();
    }
}