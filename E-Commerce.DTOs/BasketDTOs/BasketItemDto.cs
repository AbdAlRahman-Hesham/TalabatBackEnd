using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs.BasketDTOs;

public class BasketItemDto
{
    [Required]
    public int? Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Product name must not be empty.")]
    public string ProductName { get; set; }

    [Required]
    [Url(ErrorMessage = "Invalid URL format.")]
    public string PictureUrl { get; set; }

    [Required]
    [Range(0.10, 100000000, ErrorMessage = "Price must be bigger than zero.")]
    public decimal Price { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Category must not be empty.")]
    public string Category { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Brand must not be empty.")]
    public string Brand { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be bigger than zero.")]
    public int Quantity { get; set; }
}
