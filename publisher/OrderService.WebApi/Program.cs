using OrderService.Core;
using OrderService.Core.Ports;
using OrderService.Core.SubmitOrder;
using OrderService.Infrastructure.Catalog;
using OrderService.Infrastructure.PubSub;
using OrderService.WebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<SubmitOrderCommandHandler>();
builder.Services.AddScoped<GetOrderByIdHandler>();
builder.Services.AddScoped<IOrderRepository, InMemoryOrderRepository>();
builder.Services.AddScoped<IProductCatalog, ProductCatalog>();
builder.Services.AddScoped<ICatalogHttpClient, FakeCatalogHttpClient>();
builder.Services.AddScoped<EventPublisher>(); // this is singleton because it is used in repository

builder.Services.AddScoped<IMessageBroker, MessageBroker>();
builder.Services.AddScoped<IEventHandler<OrderSubmittedEvent>, 
    PublishOrderSubmittedIntegrationEventHandler>(); 

builder.Services.AddHttpClient<IBrokerHttpClient, BrokerHttpClient>(
    client =>
    {
        client.BaseAddress = new Uri("http://localhost:5117");
    });

builder.AddServiceDefaults();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/orders", async (SubmitOrderModel model, SubmitOrderCommandHandler handler) =>
    {
        var order = await handler.Handle(model.ToCommand());
        return Results.Created($"/orders/{order.Id}", order.ToModel());
    })
    .WithName("Submit order")
    .WithOpenApi();

app.MapGet("/orders/{orderId}", (string orderId, GetOrderByIdHandler handler) =>
    {
        var order = handler.Handle(new GetOrderByIdQuery(orderId));
        return order == null ? Results.NotFound() : Results.Ok(order.ToModel());
    })
    .WithName("Get order by id")
    .WithOpenApi();

app.Run();

public partial class Program;

