using E_Commerce.Domain.Entities.OrderEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.DTOs.OrderDtos;

public class OrderToReturnDto
{
    public int Id{ get; set; }
    public string BuyerEmail { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public string Status { get; set; }
    public Address ShippingAddress { get; set; }
    public string DeliveryMethod { get; set; }
    public decimal DeliveryMethodCost { get; set; }
    public ICollection<OrderItemToReturn> OrderItems { get; set; } = new HashSet<OrderItemToReturn>();
    public decimal Subtotal { get; set; }
    public decimal Total { get; set; }
    public string PaymentIntentId { get; set; }

}
