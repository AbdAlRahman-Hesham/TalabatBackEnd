using E_Commerce.Domain.Entities;
using E_Commerce.DTOs.ErrorResponse;
using E_Commerce.DTOs.Pagination;
using E_Commerce.DTOs.ProductDTOs;
using E_Commerce.Repository.Reprositories_Interfaces;
using E_Commerce.Repository.Specifications;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace E_Commerce.APIs.Controllers;


public class ProductsController(IGenaricRepository<Product> productRepository,
    IGenaricRepository<ProductBrand> productBrandRepository, IGenaricRepository<ProductCategory> productCategoryRepository) : BaseApiController
{
    private readonly IGenaricRepository<Product> _productRepository = productRepository;
    private readonly IGenaricRepository<ProductBrand> _productBrandRepository = productBrandRepository;
    private readonly IGenaricRepository<ProductCategory> _productCategoryRepository = productCategoryRepository;


    // GET: api/Products/brand
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
    {
        var result = await _productBrandRepository.GetAllAsync();

        return Ok(result);
    }

    // GET: api/Products/categories
    [HttpGet("categories")]
    public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
    {
        var result = await _productCategoryRepository.GetAllAsync();

        return Ok(result);
    }

    // GET: api/Products
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecParams productSpec)
    {
        

        var specification = new ProductSpecification();
        specification = ProductSpecification.BuildProductSpecfication(productSpec);
        var result = await _productRepository.GetAllAsyncWithSpecification(specification);
        int count = await _productRepository.GetCountAsync(specification.Criteria);
        return Ok(new Pagination<Product>(productSpec.PageSize!.Value, productSpec.PageIndex!.Value, count, result));
    }


    // GET: api/Products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _productRepository.GetAsync(id);

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
