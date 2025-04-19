using Dapr.Client;
using Dapr.Workflow;
using PizzaShared.Messages.Kitchen;
using PizzaWorkflow.Models;

namespace PizzaWorkflow.Activities;

public class CookingActivity : WorkflowActivity<Order, object?>
{
    private readonly DaprClient _daprClient;
    private readonly ILogger<CookingActivity> _logger;

    public CookingActivity(DaprClient daprClient, ILogger<CookingActivity> logger)
    {
        _daprClient = daprClient;
        _logger = logger;
    }

    public override async Task<object?> RunAsync(WorkflowActivityContext context, Order order)
    {
        try
        {
            _logger.LogInformation("Starting ordering process for order {OrderId}", order.OrderId);

            // MessageHelper.FillMessage bruges til at fylde en instans af CookMessage
            var message = MessageHelper.FillMessage<CookMessage>(context, order);

            // message sendes med PublishEventAsync til pubsub
            await _daprClient.PublishEventAsync("pizzapubsub", "kitchen", message);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing order {OrderId}", order.OrderId);
            throw;
        }
    }
}