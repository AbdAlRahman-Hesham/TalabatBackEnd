using E_Commerce.Domain.Entities.OrderEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Domain.Entities.OrderEntiti;
public class Order:BaseEntity
{
    public Order()
    {
    }
    public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> orderItems, decimal subtotal)
    {
        BuyerEmail = buyerEmail;
        ShippingAddress = shippingAddress;
        DeliveryMethod = deliveryMethod;
        OrderItems = orderItems;
        Subtotal = subtotal;
    }

    public string BuyerEmail { get; set; }
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public Address ShippingAddress { get; set; }
    public DeliveryMethod DeliveryMethod { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    public decimal Subtotal { get; set; }
    [NotMapped]
    public decimal Total=> Subtotal + DeliveryMethod.Cost;
    public string PaymentIntentId { get; set; } = "";




}
