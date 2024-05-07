using OrderProcessing.Models;
using OrderProcessing.Workers.Models;
using Zeebe.Client.Accelerator.Abstractions;
using Zeebe.Client.Accelerator.Attributes;

namespace OrderProcessing.Workers;

[JobType("accept-order")]
public class AcceptOrderWorker(ILogger<AcceptOrderWorker> logger) : IAsyncZeebeWorker<ProcessVariables, OutPutResonse>
{
    public async Task<OutPutResonse> HandleJob(
        ZeebeJob<ProcessVariables> job,
        CancellationToken cancellationToken)
    {
        Console.WriteLine("The Accept Order Worker is being called...");
        // get process variables
        ProcessVariables variables = job.getVariables<ProcessVariables>();
        // get custom headers
        AccountHeaders headers = job.getCustomHeaders<AccountHeaders>();

        // call the account service adapter
        logger.LogInformation("Do {Action} Order Id for {OrderId}",
            headers.Action, variables.OrderId);
        await Task.CompletedTask;

        return new OutPutResonse(true, DateTime.Now);
    }
}

public record OutPutResonse(bool OrderAccepted, DateTime DateTime);