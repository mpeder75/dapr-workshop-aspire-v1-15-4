using CommunityToolkit.Aspire.Hosting.Dapr;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// Dapr state store
var statestore = builder.AddDaprStateStore("pizzastatestore");
// Dapr PUB/SUB
var pubsubComponent = builder.AddDaprPubSub("pizzapubsub");

// Aspire tilføjer PizzaOrder som resource, og konfigurer og opsætter Dapr sidecar 
builder.AddProject<PizzaOrder>("pizzaorderservice")
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppId = "pizza-order",
        DaprHttpPort = 3501
    })
    .WithReference(statestore)
    .WithReference(pubsubComponent);

// Aspire tilføjer PizzaKitchen som resource, og konfigurer og opsætter Dapr sidecar 
builder.AddProject<Projects.PizzaKitchen>("pizzakitchenservice")
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppId = "pizza-kitchen",
        DaprHttpPort = 3503
    })
    .WithReference(pubsubComponent);

// Aspire tilføjer PizzaStorefront som resource, og konfigurer og opsætter Dapr sidecar 
builder.AddProject<Projects.PizzaStorefront>("pizzastorefrontservice")
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppId = "pizza-storefront",
        DaprHttpPort = 3502
    })
    .WithReference(pubsubComponent);

// Aspire tilføjer PizzaDelivery som resource, og konfigurer og opsætter Dapr sidecar 
builder.AddProject<Projects.PizzaDelivery>("pizzadeliveryservice")
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppId = "pizza-delivery",
        DaprHttpPort = 3504
    })
    .WithReference(pubsubComponent);

builder.Build().Run();
