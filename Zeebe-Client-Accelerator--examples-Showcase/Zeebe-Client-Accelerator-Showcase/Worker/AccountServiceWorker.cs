using Zeebe.Client.Accelerator.Abstractions;
using Zeebe.Client.Accelerator.Attributes;

namespace Zeebe_Client_Accelerator_Showcase.Worker
{
    [JobType("accountService")]
    [FetchVariables("ApplicantName")] // fetches only the variable 'applicantName' - not the 'businessKey'
    public class AccountServiceWorker(ILogger<AccountServiceWorker> logger) : IAsyncZeebeWorker
    {
        public Task HandleJob(ZeebeJob job, CancellationToken cancellationToken)
        {
            Console.WriteLine("The account service is being called...");
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

    class AccountServiceHeaders
    {
        public string Action { get; set; }
    }

}