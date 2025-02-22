using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs.BasketDTOs;

public class BasketItemDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string ProductName { get; set; }
    [Required]
    public string PictureUrl { get; set; }
    [Required]
    [Range(0.10, double.MaxValue,ErrorMessage = "Price must be bigger than zero")]
    public decimal Price { get; set; }
    [Required]
    public string Category { get; set; }
    [Required]
    public string Brand { get; set; }
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be bigger than zero")]
    public int Quantity { get; set; }

}