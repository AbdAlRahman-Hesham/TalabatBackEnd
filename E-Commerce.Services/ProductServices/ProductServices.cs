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
        var result = await _unitOfWork.Repository<Product>().GetCollectionOfAllAsyncWithSpecification(specification);
        int count = await _unitOfWork.Repository<Product>().GetCountAsync(specification.Criteria);
        return new Pagination<Product>(productSpec.PageSize!.Value, productSpec.PageIndex!.Value, count, result);
    }
    public async Task<ICollection<Product>> GetFeaturedProductsAsync()
    {


        var specification = new ProductSpecification();
       
        var result = await _unitOfWork.Repository<Product>().GetCollectionOfAllAsyncWithSpecification(specification);
        ICollection<Product> featuredProducts = (from p in result
                               group p by p.CategoryId into g
                               select g.FirstOrDefault()).ToList();
        return featuredProducts;
    }
    
    public async Task<Product?> GetProductAsync(int id)
    {
        var specification = new ProductSpecification(p=>p.Id==id);

        var product = await _unitOfWork.Repository<Product>().GetAsyncWithSpecification( specification);
        return product;
    }

    public async Task<Product?> CreateProductAsync(Product product)
    {
        try
        {
            await _unitOfWork.Repository<Product>().AddAsync(product);
            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                return null;

            return product;
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        try
        {
            _unitOfWork.Repository<Product>().Update(product);
            var result = await _unitOfWork.CompleteAsync();

            return result > 0;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        try
        {
            var product = await GetProductAsync(id);
            if (product == null)
                return false;

            _unitOfWork.Repository<Product>().Delete(product);
            var result = await _unitOfWork.CompleteAsync();

            return result > 0;
        }
        catch
        {
            return false;
        }
    }
}
