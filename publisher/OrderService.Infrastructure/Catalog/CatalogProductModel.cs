namespace OrderService.Infrastructure.Catalog;

public record CatalogProductModel
{
    public string ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Currency { get; set; }
    public decimal Amount { get; set; }
}