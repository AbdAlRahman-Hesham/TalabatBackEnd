using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs.ProductDTOs;
public class ProductCreateDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    [Range(0.1, 1000000000000, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    [Required]
    public string PictureUrl { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [Required]
    public int BrandId { get; set; }
}

