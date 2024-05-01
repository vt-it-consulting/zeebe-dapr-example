using Zeebe.Client.Accelerator.Abstractions;
using Zeebe.Client.Accelerator.Attributes;

namespace Zeebe_Client_Accelerator_Showcase.Worker;

[JobType("shipmentService")]
[FetchVariables("ApplicantName")] // fetches only the variable 'applicantName' - not the 'businessKey'
public class ShipmentServiceWorker(ILogger<ShipmentServiceWorker> logger) : IAsyncZeebeWorker
{
    public Task HandleJob(ZeebeJob job, CancellationToken cancellationToken)
    {
        Console.WriteLine("The shipment service is beeing called...");
        // get process variables
        ProcessVariables variables = job.getVariables<ProcessVariables>();
        // get custom headers
        AccountServiceHeaders headers = job.getCustomHeaders<AccountServiceHeaders>();

        // call the account service adapter
        logger.LogInformation("Do {Action} Account for {ApplicantName}", headers.Action, variables.ApplicantName);

        // done
        return Task.CompletedTask;

    }
}
 
