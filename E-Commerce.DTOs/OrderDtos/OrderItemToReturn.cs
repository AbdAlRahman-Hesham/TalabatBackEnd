using E_Commerce.Domain.Entities.OrderEntities;

namespace E_Commerce.DTOs.OrderDtos;

public class OrderItemToReturn
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string PictureUrl { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}