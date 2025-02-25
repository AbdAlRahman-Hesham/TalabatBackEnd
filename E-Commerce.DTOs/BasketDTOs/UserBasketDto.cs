using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs.BasketDTOs;

public class UserBasketDto
{
    [Required]
    public string Id { get; set; }
    [Required]
    public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    public int? DeliveryMethodId { get; set; }
    public decimal ShippingPrice { get; set; }
    public int? PaymentIntentId { get; set; }
    public string? ClientSecret { get; set; }
}
