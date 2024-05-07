using Microsoft.AspNetCore.Mvc;
using OrderProcessing.Models;
using OrderProcessing.Models.Request;
using Zeebe.Client;
using Zeebe.Client.Accelerator.Abstractions;

namespace OrderProcessing.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class OrderProcessingController : ControllerBase
{
    private readonly ILogger<OrderProcessingController> _logger;
    private readonly IZeebeClient _zeebeClient;
    private readonly IZeebeVariablesSerializer _variablesSerializer;

    public OrderProcessingController(
        ILogger<OrderProcessingController> logger, 
        IZeebeClient zeebeClient,
        IZeebeVariablesSerializer variablesSerializer)
    {
        _logger = logger;
        _zeebeClient = zeebeClient;
        _variablesSerializer = variablesSerializer;
    }

    /// <summary>
    /// SubmitApplicationAsync method handles the ordering process for incoming order requests
    /// </summary>
    /// <param name="orderRequest">Object containing all the required information for the order</param>
    /// <returns>Returns an OrderResponse object which contains the process instance key, 
    ///     the unique business key and the Order ID</returns>
    /// <example>
    ///     POST /application
    ///     Content-Type: application/json
    ///     {
    ///         "OrderId":"ORD123",
    ///         "CustomerId":"CUST789",
    ///         "CustomerName":"Joe Doe",
    ///         "Items":[
    ///             {
    ///                 "ItemId":"ITM456",
    ///                 "Quantity":1
    ///             }
    ///         ],
    ///         "OrderDate":"2022-05-07T00:00:00"
    ///     }
    /// </example>
    /// <response code="200">Returns the process instance key, business key, and order id</response>
    [HttpPost("/application")]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    public async Task<OrderResponse> SubmitApplicationAsync([FromBody] OrderRequest orderRequest)
    {
        var variables = new ProcessVariables()
        {
            OrderId = orderRequest.OrderId,
            CustomerId = orderRequest.CustomerId,
            BusinessKey = Guid.NewGuid().ToString(),
            ApplicantName = orderRequest.CustomerName
        };

        var pi = await _zeebeClient.NewCreateProcessInstanceCommand()
            .BpmnProcessId(ProcessConstants.PROCESS_DEFINITION_KEY)
            .LatestVersion()
            .Variables(_variablesSerializer.Serialize(variables))
            .Send();

        return new OrderResponse()
        {
            BusinessKey = variables.BusinessKey,
            OrderId = variables.OrderId,
            ProcessInstanceKey = pi.ProcessInstanceKey,
            OrderDate = orderRequest.OrderDate ?? DateTime.Now
        };
    }
}