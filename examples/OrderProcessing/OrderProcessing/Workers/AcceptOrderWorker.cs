using OrderProcessing.Models;
using OrderProcessing.Workers.Models;
using Zeebe.Client.Accelerator.Abstractions;
using Zeebe.Client.Accelerator.Attributes;

namespace OrderProcessing.Workers;

[JobType("accept-order")]
[FetchVariables("ApplicantName", "orderId", "customerId")]
public class AcceptOrderWorker(ILogger<AcceptOrderWorker> logger) : IAsyncZeebeWorker
{
    public Task HandleJob(ZeebeJob job, CancellationToken cancellationToken)
    {
        Console.WriteLine("The Accept Order Worker is being called...");
        // get process variables
        ProcessVariables variables = job.getVariables<ProcessVariables>();
        // get custom headers
        AccountHeaders headers = job.getCustomHeaders<AccountHeaders>();

        // call the account service adapter
        logger.LogInformation("Do {Action} Order Id for {OrderId}",
            headers.Action, variables.OrderId);

        // done
        return Task.CompletedTask;
    }
}