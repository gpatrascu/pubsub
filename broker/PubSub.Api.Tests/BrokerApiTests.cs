using System.Net;
using System.Net.Http.Json;
using PubSub.Domain;
using Xunit.Abstractions;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace PubSub.Api.Tests;

public class BrokerApiTests
{
    private const string TopicName = "test-topic";
    private readonly HttpClient client = TestWebApplicationFactory.Instance.CreateClient();

    protected BrokerApiTests()
    {
        var httpResponseMessage = client.DeleteAsync($"topics/{TopicName}").Result;
        httpResponseMessage.EnsureSuccessStatusCode();
    }

    private async Task<HttpResponseMessage> SendMessage(string topicName, string payload ="{ \"SomeProperty\": \"Some value\" }")
    {
        var request = await client.PostAsJsonAsync($"topics/{topicName}/messages",
            new
            {
                metadata =
                    new Dictionary<string, string>
                    {
                        { "messageId", Guid.NewGuid().ToString() },
                        { "messageType", "json" },
                        { "version", "1" },
                        { "contractClass", "SomeNamespace.SomeType" }
                    },
               payload
            }
        );
        return request;
    }

    public class When_we_read_messages_for_a_subscription : BrokerApiTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly string SubscriptionName = "subscription-name";

        public When_we_read_messages_for_a_subscription(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task Should_get_a_message_as_soon_as_it_is_produced()
        {
            await SendMessage(TopicName, "1");

            var messages =
                client.GetFromJsonAsAsyncEnumerable<PubSubMessage>(
                    $"topics/{TopicName}/subscriptions/{SubscriptionName}/messages");

            await using var asyncEnumerator = messages.GetAsyncEnumerator();
            await asyncEnumerator.MoveNextAsync();
            await SendMessage(TopicName, "2");
            await asyncEnumerator.MoveNextAsync();
            
            Assert.Equal("2", asyncEnumerator.Current.Payload);
        }
    }

    public class When_we_send_a_message : BrokerApiTests
    {
        [Fact]
        public async Task Should_receive_OK_as_response()
        {
            var request = await SendMessage(TopicName, "Some value");
            Assert.Equal(HttpStatusCode.OK, request.StatusCode);
        }

        [Fact]
        public async Task Should_be_able_to_get_the_messages_with_an_api_calls()
        {
            await SendMessage(TopicName, "Some value");
            await SendMessage(TopicName, "Some value");
            var messages = await client.GetFromJsonAsync<IList<PubSubMessage>>($"topics/{TopicName}/messages");
            Assert.Equal(2, messages?.Count);
        }
    }
}