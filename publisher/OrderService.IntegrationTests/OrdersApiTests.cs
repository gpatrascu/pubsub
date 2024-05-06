using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using OrderService.EventsContracts;
using OrderService.WebApi.Models;
using PubSub.Api.Tests;

namespace OrderService.IntegrationTests;

public class OrdersApiTests
{
    OrdersApiTests()
    {
        FakeBrokerHttpClient.Instance.ClearMessages();
    }
    private readonly HttpClient client = TestWebApplicationFactory.Instance.CreateClient();

    public class Wnen_submit_order : OrdersApiTests
    {
        private readonly HttpResponseMessage response;

        public Wnen_submit_order()
        {
            var order = new SubmitOrderModelBuilder()
                .WithCustomerId("2323121")
                .WithOrderLine("1", 2)
                .WithOrderLine("2", 3)
                .Model;

            response = client.PostAsJsonAsync("/orders", order).Result;
        }

        [Fact]
        public void Should_return_success()
        {
            // Assert
            response.EnsureSuccessStatusCode();
        }
        
        [Fact]
        public async Task Should_be_able_to_retrieve_the_order()
        {
            var orderModel = await response.ReadAsJson<OrderModel>();
            var byIdResponse = await client.GetAsync($"/orders/{orderModel.Id}");
            Assert.Equal(HttpStatusCode.OK, byIdResponse.StatusCode);
            var orderModelRead = await byIdResponse.ReadAsJson<OrderModel>();
            Assert.Equal(orderModel.Id, orderModelRead.Id);
        }
        
        [Fact]
        public async Task Should_publish_an_order_submitted_integration_event()
        {
            var orderModel = await response.ReadAsJson<OrderModel>();
            
            var topicMessages = FakeBrokerHttpClient.Instance.GetTopicMessages("order-submitted");
            Assert.Single(topicMessages);
            var orderSubmittedIntegrationEvent = JsonSerializer.Deserialize<OrderSubmittedIntegrationEvent>(topicMessages.First());
            
            Assert.Equal(orderModel.Id, orderSubmittedIntegrationEvent.OrderId);
        }
        
    }
}