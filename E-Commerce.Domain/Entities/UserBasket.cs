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
}
