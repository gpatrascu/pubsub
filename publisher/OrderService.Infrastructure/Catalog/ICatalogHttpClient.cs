namespace OrderService.Infrastructure.Catalog;

public interface ICatalogHttpClient
{
    public Task<IList<CatalogProductModel>> GetProducts(IList<string> productIds);
}