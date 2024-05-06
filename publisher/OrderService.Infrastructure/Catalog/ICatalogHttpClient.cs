namespace OrderService.Infrastructure.Catalog;

public interface ICatalogHttpClient
{
    public IList<CatalogProductModel> GetProducts(IList<string> productIds);
}