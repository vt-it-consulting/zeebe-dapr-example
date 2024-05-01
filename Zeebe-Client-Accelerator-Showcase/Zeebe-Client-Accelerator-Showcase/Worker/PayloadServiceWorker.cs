using Zeebe.Client.Accelerator.Abstractions;
using Zeebe.Client.Accelerator.Attributes;

namespace Zeebe_Client_Accelerator_Showcase.Worker;

[JobType("payloadService")]
[FetchVariables("ApplicantName")] // fetches only the variable 'applicantName' - not the 'businessKey'
public class PayloadServiceWorker : IAsyncZeebeWorker
{

    private readonly ILogger<PayloadServiceWorker> _logger;

    public PayloadServiceWorker(ILogger<PayloadServiceWorker> logger)
    {
        _logger = logger;
    }

    public Task HandleJob(ZeebeJob job, CancellationToken cancellationToken)
    {
        // get process variables
        ProcessVariables variables = job.getVariables<ProcessVariables>();
        // get custom headers
        AccountServiceHeaders headers = job.getCustomHeaders<AccountServiceHeaders>();

        // call the account service adapter
        _logger.LogInformation("Do {Action} Account for {ApplicantName}", headers.Action, variables.ApplicantName);

        // done
        return Task.CompletedTask;

    }
}
 
