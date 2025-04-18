using CommunityToolkit.Aspire.Hosting.Dapr;
using Projects;
using System.Collections.Immutable;

var builder = DistributedApplication.CreateBuilder(args);

// Dapr state store
// var statestore = builder.AddDaprStateStore("pizzastatestore");
// Dapr PUB/SUB
// var pubsubComponent = builder.AddDaprPubSub("pizzapubsub");

// Aspire tilføjer PizzaOrder som resource, og konfigurer og opsætter Dapr sidecar 
builder.AddProject<PizzaOrder>("pizzaorderservice")
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppId = "pizza-order",
        DaprHttpPort = 3501,
        ResourcesPaths = ImmutableHashSet.Create("../resources")
    });

// Aspire tilføjer PizzaKitchen som resource, og konfigurer og opsætter Dapr sidecar 
builder.AddProject<PizzaKitchen>("pizzakitchenservice")
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppId = "pizza-kitchen",
        DaprHttpPort = 3503,
        ResourcesPaths = ImmutableHashSet.Create("../resources")
    });


// Aspire tilføjer PizzaStorefront som resource, og konfigurer og opsætter Dapr sidecar 
builder.AddProject<PizzaStorefront>("pizzastorefrontservice")
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppId = "pizza-storefront",
        DaprHttpPort = 3502,
        ResourcesPaths = ImmutableHashSet.Create("../resources")
    });

// Aspire tilføjer PizzaDelivery som resource, og konfigurer og opsætter Dapr sidecar 
builder.AddProject<PizzaDelivery>("pizzadeliveryservice")
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppId = "pizza-delivery",
        DaprHttpPort = 3504,
        ResourcesPaths = ImmutableHashSet.Create("../resources")
    });

// Aspire tilføjer PizzaWorkflow som resource, og konfigurer og opsætter Dapr sidecar
builder.AddProject<PizzaWorkflow>("pizzaworkflowservice")
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppId = "pizza-workflow",
        DaprHttpPort = 3505,
        ResourcesPaths = ImmutableHashSet.Create("../resources")
    });

builder.Build().Run();
