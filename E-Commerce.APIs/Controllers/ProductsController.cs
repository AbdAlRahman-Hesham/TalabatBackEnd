using E_Commerce.APIs.Attributes;
using E_Commerce.Domain.Entities;
using E_Commerce.DTOs.ErrorResponse;
using E_Commerce.DTOs.Pagination;
using E_Commerce.DTOs.ProductDTOs;
using E_Commerce.Services.ProductServices;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace E_Commerce.APIs.Controllers;

//[Authorize]
public class ProductsController(IProductServices productServices) : BaseApiController
{
    private readonly IProductServices _productServices = productServices;



    // GET: api/Products/brand
    [Cached(600)]
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
    {
        var result = await _productServices.GetBrandsAsync();

        return Ok(result);
    }

    // GET: api/Products/categories
    [Cached(600)]
    [HttpGet("categories")]
    public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
    {
        var result = await _productServices.GetCategoriesAsync();

        return Ok(result);
    }

    // GET: api/Products
    [Cached(300)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams productSpec)
    {
        var result = await _productServices.GetProductsWithPaginationAsync(productSpec);
        var returnResult = new Pagination<ProductToReturnDto>(result.PageSize, result.PageIndex, result.Count, result.Data.Adapt<ICollection<ProductToReturnDto>>());
        return Ok(returnResult);
    }
    // GET: api/Products
    [Cached(600)]
    [HttpGet("featuredproducts")]
    public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetFeaturedProducts()
    {
        var result = await _productServices.GetFeaturedProductsAsync();
        var returnResult = result.Adapt<ICollection<ProductToReturnDto>>();
        return Ok(returnResult);
    }


    // GET: api/Products/5
    [Cached(300)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {

        var product = await _productServices.GetProductAsync(id);

        if (product == null)
        {
            return NotFound(new ApiResponse((int)HttpStatusCode.NotFound, "The product not found"));
        }

        return Ok(product.Adapt<ProductToReturnDto>());
    }



    // POST: api/Products
    [HttpPost]
    public async Task<ActionResult<ProductToReturnDto>> PostProduct(ProductCreateDto productDto)
    {
        if (productDto == null)
        {
            return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, "Invalid product data"));
        }

        var product = productDto.Adapt<Product>();

        var createdProduct = await _productServices.CreateProductAsync(product);
        if (createdProduct == null)
        {
            return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, "Failed to create product"));
        }

        var result = createdProduct.Adapt<ProductToReturnDto>();

        return CreatedAtAction(nameof(GetProduct), new { id = result.Id }, result);
    }

    // PUT: api/Products/5
    [HttpPut("put/{id}")]
    public async Task<IActionResult> PutProduct(int id, [FromBody] ProductCreateDto productDto)
    {
        if (productDto == null)
        {
            return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, "Invalid product data"));
        }

        var existingProduct = await _productServices.GetProductAsync(id);
        if (existingProduct == null)
        {
            return NotFound(new ApiResponse((int)HttpStatusCode.NotFound, "The product not found"));
        }

        var product = productDto.Adapt<Product>();
        product.Id = id;

        var result = await _productServices.UpdateProductAsync(product);
        if (!result)
        {
            return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, "Failed to update product"));
        }

        return NoContent();
    }
    // DELETE: api/Products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _productServices.GetProductAsync(id);
        if (product == null)
        {
            return NotFound(new ApiResponse((int)HttpStatusCode.NotFound, "The product not found"));
        }

        var result = await _productServices.DeleteProductAsync(id);
        if (!result)
        {
            return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, "Failed to delete product"));
        }

        return NoContent();
    }


}
