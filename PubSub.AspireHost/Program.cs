var builder = DistributedApplication.CreateBuilder(args);

var broker = builder.AddProject<Projects.PubSub_Api>("broker");

builder.AddProject<Projects.OrderService_WebApi>("publisher")
    .WithExternalHttpEndpoints()
    .WithReference(broker);

builder.AddProject<Projects.Subscriber_Host>("subscriber")
    .WithExternalHttpEndpoints()
    .WithReference(broker);

builder.Build().Run();
