// See https://aka.ms/new-console-template for more information

using System.Net.Http.Json;

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

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (s, e) =>
{
    Console.WriteLine("Canceling...");
    cts.Cancel();
    e.Cancel = true;
};

ConsumeMessages().Wait();

async Task ConsumeMessages()
{
    while (cts.IsCancellationRequested == false)
    {
        try
        {
            await Stream();
        }
        catch (Exception e)
        {
            Console.WriteLine("reconnecting...");
            await Task.Delay(3000);
        }
    }
}

async Task Stream()
{
    var stream = httpClient.GetFromJsonAsAsyncEnumerable<Message>(
        $"topics/{topicName}/subscriptions/{subscriberName}/messages");

    await foreach (var message in stream)
    {
        // here we can deserialize the message and base on it send a command to an
        // appropriate handler that exists in some hypothetical application service 
        Console.WriteLine($"Received message: {message.Payload}");
    }
}