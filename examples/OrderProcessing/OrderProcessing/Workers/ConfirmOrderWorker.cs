using OrderProcessing.Models;
using OrderProcessing.Workers.Models;
using Zeebe.Client.Accelerator.Abstractions;
using Zeebe.Client.Accelerator.Attributes;

namespace OrderProcessing.Workers;
 
[JobType("confirm-order")]
[FetchVariables("ApplicantName","orderId","customerId")]  
public class ConfirmOrderWorker(ILogger<ConfirmOrderWorker> logger) : IAsyncZeebeWorker
{
    public Task HandleJob(ZeebeJob job, CancellationToken cancellationToken)
    {
        Console.WriteLine("The Confirm Order Worker is being called...");
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