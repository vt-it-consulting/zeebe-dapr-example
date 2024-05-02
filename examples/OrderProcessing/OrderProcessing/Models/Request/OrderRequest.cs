using System.ComponentModel.DataAnnotations;

namespace OrderProcessing.Models.Request;

public record OrderRequest(
   [Required] string OrderId,
   [Required]  string CustomerId,
   [Required]   string CustomerName,
    List<Item> Items,
    DateTime? OrderDate
);

public record Item(
    string ItemId,
    int Quantity
);
