namespace E_Commerce.Domain.Entities;

public class UserBasket
{
    public UserBasket(string id)
    {
        Id = id;
        Items = new List<BasketItem>();
    }

    public string Id { get; set; }
    public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    public int? DeliveryMethodId { get; set; }
    public decimal ShippingPrice { get; set; }
    public string? PaymentIntentId { get; set; }
    public string? ClientSecret { get; set; }
}
