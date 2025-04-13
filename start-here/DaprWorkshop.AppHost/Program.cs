using CommunityToolkit.Aspire.Hosting.Dapr;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// Dapr state store
var statestore = builder.AddDaprStateStore("pizzastatestore");

// Aspire tilføjer PizzaOrder som resource, og konfigurer og opsætter Dapr sidecar 
builder.AddProject<PizzaOrder>("pizzaorderservice")
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppId = "pizza-order",
        DaprHttpPort = 3501
    })
    .WithReference(statestore);

builder.Build().Run();
