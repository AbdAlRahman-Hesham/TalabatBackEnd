using E_Commerce.Domain.Entities;
using E_Commerce.DTOs.Pagination;
using E_Commerce.DTOs.ProductDTOs;

namespace E_Commerce.Services.ProductServices;

public interface IProductServices
{

    public Task<IReadOnlyList<ProductBrand>> GetBrandsAsync();


    public Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync();


    public Task<Pagination<Product>> GetProductsWithPaginationAsync(ProductSpecParams productSpec);

    public Task<ICollection<Product>> GetFeaturedProductsAsync();


    public Task<Product?> GetProductAsync(int id);
    Task<Product?> CreateProductAsync(Product product);
    Task<bool> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(int id);


}