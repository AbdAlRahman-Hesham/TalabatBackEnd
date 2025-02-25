using E_Commerce.Domain.Entities;
using E_Commerce.DTOs.Pagination;
using E_Commerce.DTOs.ProductDTOs;

namespace E_Commerce.Services.ProductServices;

public interface IProductServices
{

    public Task<IReadOnlyList<ProductBrand>> GetBrandsAsync();


    public Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync();


    public Task<Pagination<Product>> GetProductsWithPaginationAsync(ProductSpecParams productSpec);



    public Task<Product?> GetProductAsync(int id);


}