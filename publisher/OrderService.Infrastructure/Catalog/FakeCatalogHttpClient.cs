using Bogus;

namespace OrderService.Infrastructure.Catalog;

public class FakeCatalogHttpClient : ICatalogHttpClient
{
    private readonly Faker<CatalogProductModel> _faker = new Faker<CatalogProductModel>()
        .RuleFor(p => p.Name, f => f.Commerce.ProductName())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Currency, f => f.Finance.Currency().Code)
        .RuleFor(p => p.Amount, f => f.Finance.Amount());

    public IList<CatalogProductModel> GetProducts(IList<string> productIds)
    {
        return productIds.Select(Generate).ToList();
    }

    private CatalogProductModel Generate(string productId)
    {
        var generate = _faker.Generate();
        generate.ProductId = productId;
        return generate;
    }
}