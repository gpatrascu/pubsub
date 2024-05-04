using PubSub.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/topics/{topic}/messages", (string topic, PubSubMessage message, IMessageRepository messageRepository) =>
    {
        messageRepository.AddMessage(topic, message);
    })
    .WithName("PostMessageToTopic")
    .WithOpenApi();

app.MapPost("/topics/{topic}/subscriptions/", (string topic, ISubscriptionsRepository subscriptionsRepository) =>
    {
        string subscriptionId = Guid.NewGuid().ToString();
        subscriptionsRepository.CreateSubscription(topic, subscriptionId);
    })
    .WithName("CreateSubscription")
    .WithOpenApi();

app.MapGet("/topics/{topic}/subscriptions/{subscriptionId}/messages", (string topic, ISubscriptionsRepository subscriptionsRepository) =>
    {
        string subscriptionId = Guid.NewGuid().ToString();
        subscriptionsRepository.CreateSubscription(topic, subscriptionId);
    })
    .WithName("read messages")
    .WithOpenApi();

app.Run();