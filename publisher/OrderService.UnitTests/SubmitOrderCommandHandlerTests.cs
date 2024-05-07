using Moq;
using OrderService.Core.Ports;
using OrderService.Core.SubmitOrder;

namespace OrderService.UnitTests;

public class SubmitOrderCommandHandlerTests
{
    private readonly IOrderRepository repository = Mock.Of<IOrderRepository>();
    private readonly IProductCatalog productCatalog = Mock.Of<IProductCatalog>();

    private SubmitOrderCommandHandler CreateSut()
    {
        return new SubmitOrderCommandHandler(repository, productCatalog);
    }

    public class When_order_is_submitted : SubmitOrderCommandHandlerTests
    {
        [Fact]
        public async Task Should_add_data_into_repository()
        {
            //to be implemented
        }
        [Fact]
        public async Task Should_commit_and_send_events()
        {
            //to be implemented
        }
        [Fact]
        public async Task Should_enhance_order_with_product_catalog_data()
        {
            string productId = Guid.NewGuid().ToString();
            var productIds = new List<string> { productId };
            var product = new Product(productId, "Product name", new Money("usd", 10.1m));
            Mock.Get(productCatalog)
                .Setup(catalog => catalog.GetProducts(productIds))
                .ReturnsAsync(new List<Product> { product });
            
            var sut = CreateSut();
            var order = await sut.Handle(CreateSubmitOrderCommand(productId));
            
            Assert.Equal(product, order.OrderLines.First().Product);
        }

        private static SubmitOrderCommand CreateSubmitOrderCommand(string productId)
        {
            return new SubmitOrderCommand()
            {
                OrderLines =
                [
                    new OrderLineSubmitted()
                    {
                        ProductId = productId,
                        Quantity = 1
                    }
                ]
            };
        }
    }
}