
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs.OrderDtos;


public class OrderDto
{
    [Required]
    public string BasketId { get; set; }
    [Required]
    public int? DeliveryMethodId { get; set; } = null;
    [Required]
    public AddressDto? ShippingAddress { get; set; } = null;
}
