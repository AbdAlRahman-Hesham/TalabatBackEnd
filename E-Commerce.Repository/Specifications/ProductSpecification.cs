using E_Commerce.Domain.Entities;
using E_Commerce.DTOs.ProductDTOs;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace E_Commerce.Repository.Specifications;

public class ProductSpecification : Specification<Product>
{
    public ProductSpecification(): base(DefaultIncludes)
    {
    }

    public ProductSpecification(Expression<Func<Product, bool>>? criteria,Expression<Func<Product, object>>? orderBy,SortOrder sortOrder, int? skip = 0,int? take = 10)
        : base(DefaultIncludes,criteria, orderBy, sortOrder,skip,take)
    {
    }
    public ProductSpecification(Expression<Func<Product, bool>>? criteria)
        : base(DefaultIncludes,criteria)
    {
    }

    

    public static ProductSpecification BuildProductSpecfication(string? sort,string? sortDirection, Expression<Func<Product, bool>>? criteria = null)
    {
        var (orderBy, sortOrderEnum) = GetSortingParameters(sort, sortDirection);
        return new ProductSpecification(criteria, orderBy, sortOrderEnum);
    }
    public static ProductSpecification BuildProductSpecfication(ProductSpecParams productSpec)
    {
        Expression<Func<Product, bool>>? criteria = GetCriteriaParameters(productSpec.BrandId,productSpec.CategorityId,productSpec.SearchName);
        var (orderBy, sortOrderEnum) = GetSortingParameters(productSpec.Sort, productSpec.SortOrder);
        var (skip, take) = GetPaginationParameters(productSpec.PageSize, productSpec.PageIndex);
        return new ProductSpecification(criteria, orderBy, sortOrderEnum, skip, take);
    }



    private static readonly List<Expression<Func<Product, object>>> DefaultIncludes = new() {p => p.Brand,p => p.Category};


    private static (Expression<Func<Product, object>>? OrderBy, SortOrder SortOrder)
        GetSortingParameters(string? sort, string? sortOrder)
    {
        if (string.IsNullOrEmpty(sort))
        {
            return (null, SortOrder.Ascending);
        }

        Expression<Func<Product, object>>? orderBy = sort.ToLower() switch
        {
            "name" => p => p.Name,
            "price" => p => p.Price,
            _ => null
        };

        if (orderBy == null)
        {
            return (null, SortOrder.Ascending);
        }

        var sortOrderEnum = (!string.IsNullOrEmpty(sortOrder) ? sortOrder.ToLower() : "asc") switch
        {
            "desc" => SortOrder.Descending,
            _ => SortOrder.Ascending
        };

        return (orderBy, sortOrderEnum);
    }
    private static Expression<Func<Product, bool>>? GetCriteriaParameters(int? brandId, int? categorityId, string? searchName)
    {
        Expression<Func<Product, bool>>? criteria = null;
        

        if (brandId.HasValue)
        {
            criteria = p => p.BrandId == brandId;
        }

        if (categorityId.HasValue)
        {
            if (criteria != null)
            {
                criteria = criteria.AndAlso(p => p.CategoryId == categorityId);
            }
            else
            {
                criteria = p => p.CategoryId == categorityId;
            }
        }
        if (!string.IsNullOrEmpty(searchName))
        {
            if (criteria != null)
            {
                criteria = criteria.AndAlso(p => p.Name.Contains(searchName));
            }
            else
            {
                criteria = p => p.Name.Contains(searchName);
            }
        }

        return criteria;
    }
    
    private static (int?,int?) GetPaginationParameters(int? PageSize, int? PageIndex)
    {
        if (PageSize.HasValue && PageIndex.HasValue)
        {
            int skip = (PageIndex.Value - 1) * PageSize.Value;
            return (skip, PageSize.Value);

        }  

        return (null,null);
    }
}
