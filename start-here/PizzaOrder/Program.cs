using PizzaOrder.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add DaprClient
builder.Services.AddControllers().AddDapr();
builder.Services.AddSingleton<IOrderStateService, OrderStateService>();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Dapr will send serialized event object vs. being raw CloudEvent
// Dapr bruger cloudevents til sin pubsub mekanisme
app.UseCloudEvents();

// Needed for Programmatic Dapr pub/sub routing
// Opsætter et endpoint så Dapr kan subscribe til topics
app.MapSubscribeHandler();

app.MapControllers();
app.Run();