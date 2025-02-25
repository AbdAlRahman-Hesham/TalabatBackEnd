using E_Commerce.APIs.Attributes;
using E_Commerce.Domain.Entities;
using E_Commerce.DTOs.ErrorResponse;
using E_Commerce.DTOs.ProductDTOs;
using E_Commerce.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace E_Commerce.APIs.Controllers;

[Authorize]
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
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecParams productSpec)
    {
        

        var result = await _productServices.GetProductsWithPaginationAsync(productSpec);
        return Ok(result);
    }


    // GET: api/Products/5
    [Cached(300)]
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _productServices.GetProductAsync(id);

        if (product == null)
        {
            return NotFound(new ApiResponse((int)HttpStatusCode.NotFound, "The product not found"));
        }

        return Ok(product);
    }



    /*// PUT: api/Products/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Products
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProduct", new { id = product.Id }, product);
    }

    // DELETE: api/Products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }*/


}
