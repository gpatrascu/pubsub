using Microsoft.AspNetCore.Mvc;
using PubSub.Api;
using PubSub.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DeleteTopicHandler>();
builder.Services.AddScoped<PublishNewMessageCommandHandler>();
builder.Services.AddScoped<GetSubscriptionsMessagesQueryHandler>();
builder.Services.AddScoped<GetTopicMessagesHandler>();
builder.Services.AddSingleton<IMessageStorage, InMemoryMessages>();
builder.Services.AddSingleton<ISubscriptionsStorage, InMemorySubscriptions>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapDelete("/topics/{topic}",
        (string topic, [FromServices] DeleteTopicHandler deleteTopicHandler) =>
        {
            deleteTopicHandler.Handle(new DeleteTopicCommand(topic));
        })
    .WithName("Delete topic")
    .WithOpenApi();

// we need to separate the pubsubmessage API model from the domain model
// this is not done for the moment.

app.MapPost("/topics/{topic}/messages",
        (string topic, PubSubMessage message, [FromServices] PublishNewMessageCommandHandler handler)
            => handler.Handle(new PublishNewMessageCommand(topic, message)))
    .WithName("PostMessageToTopic")
    .WithOpenApi();

app.MapGet("/topics/{topic}/messages",
        async (string topic, [FromServices] GetTopicMessagesHandler handler) =>
        Results.Ok(await handler.Handle(new GetTopicMessagesQuery(topic))))
    .WithName("ReadMessagesFromTopic")
    .WithDescription("This is simply returning the messages from the topic - no subscription needed")
    .WithOpenApi();

app.MapGet("/topics/{topic}/subscriptions/{subscriptionId}/messages",
        async (string topic, string subscriptionId,
            [FromServices] GetSubscriptionsMessagesQueryHandler handler,
            CancellationToken cancellationToken
        ) => handler.Handle(new GetSubscriptionQuery(topic, subscriptionId), cancellationToken))
    .WithName("read messages")
    .WithOpenApi();

app.Run();

public partial class Program
{
}