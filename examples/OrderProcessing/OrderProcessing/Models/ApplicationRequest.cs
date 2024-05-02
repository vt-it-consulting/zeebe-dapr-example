namespace OrderProcessing.Models;

public class OrderResponse
{
    public long ProcessInstanceKey { get; set; }

    public string BusinessKey { get; set; }

    public string OrderId { get; set; }

    public DateTime OrderDate { get; set; }
}