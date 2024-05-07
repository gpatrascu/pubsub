// See https://aka.ms/new-console-template for more information

using System.Net.Http.Json;
using PubSub.Domain;

string subscriberName = Guid.NewGuid().ToString();
if (args.Length > 0)
{
    subscriberName = args[0];
}
var httpClient = new HttpClient
{
    BaseAddress = new Uri("http://localhost:5117")
};

string topicName = "order-submitted";

ConsumeMessages().Wait();

async Task ConsumeMessages()
{
    var stream = httpClient.GetFromJsonAsAsyncEnumerable<PubSubMessage>(
        $"topics/{topicName}/subscriptions/{subscriberName}/messages");

    await foreach (var message in stream)
    {
        // here we can deserialize the message and base on it send a command to an
        // appropriate handler that exists in some hypothetical application service 
        Console.WriteLine($"Received message: {message.Payload}");
    }
}



