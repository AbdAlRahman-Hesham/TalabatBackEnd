using E_Commerce.Domain.Entities;
using E_Commerce.DTOs.Pagination;
using E_Commerce.DTOs.ProductDTOs;
using E_Commerce.Repository.Specifications;
using E_Commerce.Repository.UnitOfWork;

namespace E_Commerce.Services.ProductServices;

public class ProductServices(IUnitOfWork unitOfWork):IProductServices
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
    {
        var result = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();

        return result;
    }

    
    public async Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync()
    {
        var result = await _unitOfWork.Repository<ProductCategory>().GetAllAsync();

        return result;
    }

    
    public async Task<Pagination<Product>> GetProductsWithPaginationAsync(ProductSpecParams productSpec)
    {


        var specification = new ProductSpecification();
        specification = ProductSpecification.BuildProductSpecfication(productSpec);
        var result = await _unitOfWork.Repository<Product>().GetAllAsyncWithSpecification(specification);
        int count = await _unitOfWork.Repository<Product>().GetCountAsync((System.Linq.Expressions.Expression<Func<Product, bool>>?)specification.Criteria);
        return new Pagination<Product>(productSpec.PageSize!.Value, productSpec.PageIndex!.Value, count, result);
    }


    
    public async Task<Product?> GetProductAsync(int id)
    {
        var product = await _unitOfWork.Repository<Product>().GetAsync(id);
        return product;
    }
}
